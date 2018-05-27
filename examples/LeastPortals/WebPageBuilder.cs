using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SteamCommunity;

namespace LeastPortals
{
	internal class Player
	{
		[JsonProperty("id")]
		public ulong Id { get; set; }
		[JsonProperty("score")]
		public int Score { get; set; }
		[JsonProperty("entries")]
		public int Entries { get; set; }

		public void Update(int score)
		{
			Score += score;
			Entries++;
		}
	}

	internal class WebPageBuilder
	{
		private List<Player> _spPlayers;
		private List<Player> _mpPlayers;
		private List<Player> _ovPlayers;

		private readonly string _gameName;
		private readonly SteamCommunityClient _client;

		public WebPageBuilder(string gameName)
		{
			_spPlayers = new List<Player>();
			_mpPlayers = new List<Player>();
			_ovPlayers = new List<Player>();

			_gameName = gameName;
			_client = new SteamCommunityClient("LeastPortals/1.0", false);
			_client.Log += Logger.LogSteamCommunityClient;
		}
		
		public async Task Initialize()
		{
			var game = await _client.GetLeaderboardsAsync(_gameName);

			var excluded = WorldRecords.Excluded.Select(x => x.Key);
			var sp = game.Entries
				.Where(lb => lb.Name.StartsWith("challenge_portals_sp"))
				.Where(lb => !excluded.Contains(lb.DisplayName));
			var mp = game.Entries
				.Where(lb => lb.Name.StartsWith("challenge_portals_mp"))
				.Where(lb => !excluded.Contains(lb.DisplayName));

			// Local function
			async Task InitPlayers(
				List<Player> players,
				IEnumerable<IStatsLeaderboardEntry> entries,
				Dictionary<string, int> mode)
			{
				foreach (var lb in entries)
				{
					var wr = mode[lb.DisplayName];
					var page = await _client.GetLeaderboardAsync(_gameName, lb.Id);

					// Check if we need a second page
					if (page.Entries.Last().Score == wr)
						Console.WriteLine("Yes");
					Console.WriteLine($"[\"{lb.DisplayName}\"] = {wr},");

					//Console.WriteLine($"{lb.DisplayName} = {wr}");
					foreach (var entry in page.Entries
						.Where(entry => entry.Score >= wr))
					{
						if (!players.Any(p => p.Id == entry.Id))
						{
							players.Add(new Player()
							{
								Id = entry.Id,
								Score = entry.Score,
								Entries = 1
							});
						}
						else
						{
							players
								.First(p => p.Id == entry.Id)
								.Update(entry.Score);
						}
					}
					await Task.Delay(1000);
				}

				// Filter if player appeared on all leaderboards
				var all = entries.Count();
				var before = players.Count;
				players.RemoveAll(p => p.Entries != all);
				var after = players.Count;

				Console.WriteLine($"Filtered {after} from {before} players.");
				Console.WriteLine();
			}

			await InitPlayers(_spPlayers, sp, WorldRecords.SinglePlayer);
			await InitPlayers(_mpPlayers, mp, WorldRecords.Cooperative);
			await InitOverall();
		}
		public async Task Export()
		{
			if (File.Exists("gh-pages/sp.json")) File.Delete("gh-pages/sp.json");
			if (File.Exists("gh-pages/mp.json")) File.Delete("gh-pages/mp.json");
			await File.WriteAllTextAsync("gh-pages/sp.json", JsonConvert.SerializeObject(_spPlayers));
			await File.WriteAllTextAsync("gh-pages/mp.json", JsonConvert.SerializeObject(_mpPlayers));
		}
		public async Task Import()
		{
			_spPlayers.Clear();
			_mpPlayers.Clear();
			_ovPlayers.Clear();

			_spPlayers = JsonConvert.DeserializeObject<List<Player>>(await File.ReadAllTextAsync("gh-pages/sp.json"));
			_mpPlayers = JsonConvert.DeserializeObject<List<Player>>(await File.ReadAllTextAsync("gh-pages/mp.json"));

			await InitOverall();
		}
		public Task InitOverall()
		{
			foreach (var player in _spPlayers)
			{
				foreach (var match in _mpPlayers)
				{
					if (player.Id == match.Id)
					{
						_ovPlayers.Add(new Player()
						{
							Id = player.Id,
							Score = player.Score + match.Score,
							Entries = player.Entries + match.Entries
						});
					}
				}
			}
			return Task.CompletedTask;
		}
		public async Task Build(string file, int maxRank = 10)
		{
			if (File.Exists(file)) File.Delete(file);
			var cache = new Dictionary<ulong, IPublicProfile>();

			// Local function
			async Task<List<string>> BuildRows(List<Player> source, int perfectScore)
			{
				var players = source
					.OrderBy(p => p.Score)
					.ThenBy(p => p.Id);

				var rank = 0;
				var current = 0;
				var rows = new List<string>();
				foreach (var player in players)
				{
					if (current != player.Score)
					{
						rank++;
						if (rank > maxRank) break;
						current = player.Score;
					}

					// Get Steam profile to resolve name
					if (!cache.ContainsKey(player.Id))
					{
						cache.Add(player.Id, (await _client.GetProfileAsync(player.Id)));
						Console.WriteLine($"[{player.Id}] {player.Score} by {cache[player.Id].Name}");
					}
					
					rows.Add(FillRow(player, cache[player.Id], perfectScore));
					await Task.Delay(1000);
				}
				return rows;
			}

			var maxsp = WorldRecords.SinglePlayer.Sum(x => x.Value);
			var maxmp = WorldRecords.Cooperative.Sum(x => x.Value);

			var sp = await BuildRows(_spPlayers, maxsp);
			var mp = await BuildRows(_mpPlayers, maxmp);
			var ov = await BuildRows(_ovPlayers, maxsp + maxmp);

			await File.WriteAllTextAsync(file, GetPage(sp, mp, ov));
		}

		private string GetPage(
			IEnumerable<string> singlePlayerRows,
			IEnumerable<string> cooperativeRows,
			IEnumerable<string> overallRows)
		{
			return
$@"<!DOCTYPE html>
<html>
	<head>
		<title>Least Portals | nekzor.github.io</title>
		<link href=""https://fonts.googleapis.com/css?family=Roboto"" rel=""stylesheet"">
		<link href=""https://fonts.googleapis.com/icon?family=Material+Icons"" rel=""stylesheet"">
		<link href=""https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0-alpha.4/css/materialize.min.css"" rel=""stylesheet"">
	</head>
	<body class=""white-text blue-grey darken-4"">
		<nav class=""nav-extended blue-grey darken-3"">
			<div class=""nav-wrapper"">
				<div class=""col s12 hide-on-small-only"">
					<a href=""index.html"" class=""breadcrumb"">&nbsp;&nbsp;&nbsp;nekzor.github.io</a>
					<a href=""lp.html"" class=""breadcrumb"">Least Portals</a>
				</div>
				<div class=""col s12 hide-on-med-and-up"">
					<a href=""#"" data-target=""slide-out"" class=""sidenav-trigger""><i class=""material-icons"">menu</i></a>
					<a href=""lp.html"" class=""brand-logo center"">LP</a>
				</div>
			</div>
			<div class=""nav-content"">
				<ul class=""tabs tabs-transparent"">
					<li class=""tab""><a href=""#sp"">Single Player</a></li>
					<li class=""tab""><a href=""#mp"">Cooperative</a></li>
					<li class=""tab""><a href=""#all"">Overall</a></li>
				</ul>
			</div>
		</nav>
		<ul id=""slide-out"" class=""sidenav hide-on-med-and-up"">
			<li><a href=""index.html"">nekzor.github.io</a></li>
			<li><a href=""lp.html"">Least Portals</a></li>
		</ul>
		<div id=""sp"" class=""row"">
			<div class=""col s10 push-s1"">
				<table class=""highlight"">
					<thead>
						<tr>
							<th>Player</th>
							<th>Portals<sup>1</sup></th>
						</tr>
					</thead>
					<tbody>
	{string.Join("\n", singlePlayerRows)}
					</tbody>
				</table>
			</div>
		</div>
		<div id=""mp"" class=""row"">
			<div class=""col s10 push-s1"">
				<table class=""highlight"">
					<thead>
						<tr>
							<th>Player</th>
							<th>Portals</th>
						</tr>
					</thead>
					<tbody>
	{string.Join("\n", cooperativeRows)}
					</tbody>
				</table>
			</div>
		</div>
		<div id=""all"" class=""row"">
			<div class=""col s10 push-s1"">
				<table class=""highlight"">
					<thead>
						<tr>
							<th>Player</th>
							<th>Portals<sup>1</sup></th>
						</tr>
					</thead>
					<tbody>
	{string.Join("\n", overallRows)}
					</tbody>
				</table>
			</div>
		</div>
		<div align=""center"">
			<br>
			<br>
			<br>Last Update: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss '(UTC)'")}
			<br>
			<br>
			<br><small><sup>1</sup> Excluding Smooth Jazz, Jail Break, Neurotoxin Sabotage, Dual Lasers, Fizzler Intro, Laser Relays and Turret Intro</small>
		</div>
		<script src=""https://code.jquery.com/jquery-3.3.1.min.js"" integrity=""sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8="" crossorigin=""anonymous""></script>
		<script src=""https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0-alpha.4/js/materialize.min.js""></script>
		<script>
			$(document).ready(function(){{
				$('.tabs').tabs();
				$('.sidenav').sidenav();
			}});
		</script>
	</body>
</html>";
		}
		private string FillRow(Player player, IPublicProfile profile, int possible)
		{
			var stats = (player.Score - possible != 0) ? $" ({possible}+{player.Score - possible})" : string.Empty;
			return
$@"					<tr>
						<td class=""valign-wrapper"">
							<img class=""circle responsive-img"" src=""{profile.AvatarIcon}"">
							<a class=""white-text"" href=""https://steamcommunity.com/profiles/{profile.Id}"">&nbsp;&nbsp;&nbsp;{profile.Name}</a></td>
						<td title=""{(int)(((double)possible / player.Score) * 100)}%{stats}"">{player.Score}</td>
					</tr>";
		}
	}
}
