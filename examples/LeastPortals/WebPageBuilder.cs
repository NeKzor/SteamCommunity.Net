using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SteamCommunity;
using Portal2Boards.Extensions;

namespace LeastPortals
{
	internal class Map
	{
		[JsonProperty("id")]
		public ulong Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("mode")]
		public Portal2MapType Mode { get; set; }
		[JsonProperty("wr")]
		public int? WorldRecord { get; set; }
	}

	internal class ScoreEntry
	{
		[JsonProperty("id")]
		public ulong Id { get; set; }
		[JsonProperty("mode")]
		public Portal2MapType Mode { get; set; }
		[JsonProperty("score")]
		public int? Score { get; set; }
	}

	internal class Player
	{
		[JsonProperty("id")]
		public ulong Id { get; set; }
		[JsonProperty("entries")]
		public List<ScoreEntry> Entries { get; set; }

		[JsonIgnore]
		private int _singlePlayerScore { get; set; }
		[JsonIgnore]
		private int _cooperativeScore { get; set; }
		[JsonIgnore]
		private int _totalScore => _singlePlayerScore + _cooperativeScore;

		[JsonIgnore]
		public bool IsSinglePlayer => _singlePlayerScore != default;
		[JsonIgnore]
		public bool IsCooperative => _cooperativeScore != default;
		[JsonIgnore]
		public bool IsOverall => IsSinglePlayer && IsCooperative;

		public Player()
		{
			Entries = new List<ScoreEntry>();
		}

		public Player(ulong id, IEnumerable<ulong> excluded)
		{
			Id = id;
			Entries = new List<ScoreEntry>();
			foreach (var map in Portal2.CampaignMaps
				.Where(x => x.IsOfficial)
				.Where(x => !excluded.Contains((ulong)x.BestPortalsId)))
			{
				Entries.Add(new ScoreEntry()
				{
					Id = (ulong)map.BestPortalsId,
					Mode = map.Type
				});
			}
		}

		public void Update(int id, int score)
		{
			Entries.First(x => x.Id == (ulong)id).Score = score;
		}
		public void CalculateTotalScore()
		{
			var sp = Entries.Where(e => e.Mode == Portal2MapType.SinglePlayer);
			var mp = Entries.Where(e => e.Mode == Portal2MapType.Cooperative);

			if (!sp.Any(x => x.Score == default))
				_singlePlayerScore = (int)sp.Sum(e => e.Score);
			if (!mp.Any(x => x.Score == default))
				_cooperativeScore = (int)mp.Sum(e => e.Score);
		}
		public int GetTotalScore(Portal2MapType mode)
		{
			switch (mode)
			{
				case Portal2MapType.SinglePlayer:
					return _singlePlayerScore;
				case Portal2MapType.Cooperative:
					return _cooperativeScore;
			}
			return _singlePlayerScore + _cooperativeScore;
		}
	}

	internal class WebPageBuilder
	{
		private List<Map> _wrs;
		private List<Player> _players;

		private readonly SteamCommunityClient _client;

		public WebPageBuilder(string userAgent)
		{
			_players = new List<Player>();

			_client = new SteamCommunityClient(userAgent, false);
			_client.Log += Logger.LogSteamCommunityClient;

			_wrs = JsonConvert.DeserializeObject<List<Map>>(File.ReadAllText("gh-pages/wrs.json"));
		}

		public async Task Initialize()
		{
			var game = await _client.GetLeaderboardsAsync("Portal 2");

			var excluded = _wrs
				.Where(x => x.WorldRecord == default)
				.Select(x => x.Id);
			var sp = game.Entries
				.Where(lb => lb.Name.StartsWith("challenge_portals_sp"))
				.Where(lb => !excluded.Contains((ulong)lb.Id));
			var mp = game.Entries
				.Where(lb => lb.Name.StartsWith("challenge_portals_mp"))
				.Where(lb => !excluded.Contains((ulong)lb.Id));

			// Local function
			async Task GetPlayers(IEnumerable<IStatsLeaderboardEntry> entries, Portal2MapType mode)
			{
				foreach (var lb in entries)
				{
					var wr = _wrs
						.Where(x => x.Mode == mode)
						.First(x => x.Id == (ulong)lb.Id).WorldRecord;

					var page = await _client.GetLeaderboardAsync("Portal 2", lb.Id);

					// Check if we need a second page
					if (page.Entries.Last().Score == wr)
						Console.Write("[X] ");
					Console.WriteLine($"[\"{lb.DisplayName}\"] = {wr},");

					foreach (var entry in page.Entries
						.Where(entry => entry.Score >= wr))
					{
						if (!_players.Any(p => p.Id == entry.Id))
							_players.Add(new Player(entry.Id, excluded));

						_players
							.First(p => p.Id == entry.Id)
							.Update(lb.Id, entry.Score);
					}
					await Task.Delay(1000);
				}
			}

			await GetPlayers(sp, Portal2MapType.SinglePlayer);
			await GetPlayers(mp, Portal2MapType.Cooperative);
		}
		public Task Filter()
		{
			foreach (var player in _players)
				player.CalculateTotalScore();

			var before = _players.Count;
			_players.RemoveAll(p => !p.IsSinglePlayer && !p.IsCooperative);
			var after = _players.Count;

			Console.WriteLine($"Filtered {after} from {before} players.");
			Console.WriteLine();
			return Task.CompletedTask;
		}
		public async Task Export(string file)
		{
			if (File.Exists(file)) File.Delete(file);
			await File.WriteAllTextAsync(file, JsonConvert.SerializeObject(_players));
		}
		public async Task Import(string file)
		{
			if (!File.Exists(file)) return;
			_players = JsonConvert.DeserializeObject<List<Player>>(await File.ReadAllTextAsync(file));
		}
		public async Task Build(string file, int maxRank = 10)
		{
			if (File.Exists(file)) File.Delete(file);

			foreach (var player in _players)
				player.CalculateTotalScore();

			var cache = new Dictionary<ulong, IPublicProfile>();
			var profilecache = new List<ulong>();

			// Local function 1
			async Task<List<string>> BuildRows(IEnumerable<Player> players, int perfectScore, Portal2MapType mode)
			{
				var rank = 0;
				var current = 0;
				var rows = new List<string>();
				foreach (var player in players
					.OrderBy(p => p.GetTotalScore(mode))
					.ThenBy(p => p.Id))
				{
					if (current != player.GetTotalScore(mode))
					{
						rank++;
						if (rank > maxRank) break;
						current = player.GetTotalScore(mode);
					}

					// Download Steam profile to resolve name
					if (!cache.ContainsKey(player.Id))
					{
						cache.Add(player.Id, (await _client.GetProfileAsync(player.Id)));
						Console.WriteLine($"[{player.Id}] {player.GetTotalScore(mode)} by {cache[player.Id].Name}");
						await Task.Delay(1000);
					}

					rows.Add(FillRow(player, cache[player.Id], perfectScore, rank, mode));
				}
				return rows;
			}
			// Local function 2
			Task<List<string>> BuildProfileRows(IEnumerable<Player> players)
			{
				var rows = new List<string>();
				foreach (var player in players)
				{
					if (!cache.ContainsKey(player.Id) || profilecache.Contains(player.Id)) continue;
					profilecache.Add(player.Id);
					rows.Add(FillProfileRow(player, cache[player.Id]));
				}
				return Task.FromResult(rows);
			}

			var maxsp = _wrs
				.Where(x => x.Mode == Portal2MapType.SinglePlayer)
				.Sum(x => x.WorldRecord);
			var maxmp = _wrs
				.Where(x => x.Mode == Portal2MapType.Cooperative)
				.Sum(x => x.WorldRecord);

			var sp = await BuildRows(_players.Where(p => p.IsSinglePlayer), (int)maxsp, Portal2MapType.SinglePlayer);
			var mp = await BuildRows(_players.Where(p => p.IsCooperative), (int)maxmp, Portal2MapType.Cooperative);
			var ov = await BuildRows(_players.Where(p => p.IsOverall), (int)(maxsp + maxmp), Portal2MapType.Unknown);
			var pr = await BuildProfileRows(_players.Where(x => cache.ContainsKey(x.Id)));

			await File.WriteAllTextAsync(file, GetPage(sp, mp, ov, pr));
		}

		private string GetPage(
			IEnumerable<string> singlePlayerRows,
			IEnumerable<string> cooperativeRows,
			IEnumerable<string> overallRows,
			IEnumerable<string> profileRows)
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
{string.Join("\n", profileRows)}
		<script src=""https://code.jquery.com/jquery-3.3.1.min.js"" integrity=""sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8="" crossorigin=""anonymous""></script>
		<script src=""https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0-alpha.4/js/materialize.min.js""></script>
		<script>
			$(document).ready(function(){{
				$('.tabs').tabs();
				$('.sidenav').sidenav();
				$('.tooltipped').tooltip();
				$('.modal').modal();
			}});
		</script>
	</body>
</html>";
		}
		private string FillRow(Player player, IPublicProfile profile, int possible, int rank, Portal2MapType mode)
		{
			var stats = (player.GetTotalScore(mode) - possible != 0) ? $" ({possible}+{player.GetTotalScore(mode) - possible})" : string.Empty;
			return
$@"						<tr class=""white-text tooltipped modal-trigger"" href=""#{profile.Id}"" data-position=""left"" data-tooltip=""#{rank}"">
							<td class=""valign-wrapper"">
								<img class=""circle responsive-img"" src=""{profile.AvatarIcon}"">
								&nbsp;&nbsp;&nbsp;{profile.Name}
							</td>
							<td title=""{(int)(((double)possible / player.GetTotalScore(mode)) * 100)}%{stats}"">{player.GetTotalScore(mode)}</td>
						</tr>";
		}
		private string FillProfileRow(Player player, IPublicProfile profile)
		{
			var rows = new List<string>();
			foreach (var entry in player.Entries)
			{
				var map = _wrs.First(x => (ulong)x.Id == entry.Id);
				var delta = (entry.Score - map.WorldRecord) ?? 0;
				rows.Add
				(
$@"								<tr>
									<th>{map.Name}</th>
									<th>{((entry.Score == default) ? "-" : $"{entry.Score}")}</th>
									<th>{((delta == 0) ? "-" : $"+{delta}")}</th>
								</tr>"
				);
			}

			return
$@"		<div id=""{profile.Id}"" class=""modal blue-grey darken-3"">
			<div class=""modal-content"">
				<div class=""valign-wrapper"">
					<img class=""circle responsive-img"" src=""{profile.AvatarIcon}"">
					<a class=""white-text"" href=""https://steamcommunity.com/profiles/{profile.Id}"">&nbsp;&nbsp;&nbsp;{profile.Name}</a></td>
				</div>
				<br>
				<div class=""row"">
					<div class=""col s10 push-s1"">
						<table class=""highlight"">
							<thead>
								<tr>
									<th>Map</th>
									<th>Portals</th>
									<th>ΔWR</th>
								</tr>
							</thead>
							<tbody>
{string.Join("\n", rows)}
							</tbody>
						</table>
					</div>
				</div>
			</div>
		</div>";
		}
	}
}
