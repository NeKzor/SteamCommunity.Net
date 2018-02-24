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

		private readonly string _gameName;
		private readonly SteamCommunityClient _client;

		public WebPageBuilder(string gameName)
		{
			_spPlayers = new List<Player>();
			_mpPlayers = new List<Player>();

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
			_spPlayers = JsonConvert.DeserializeObject<List<Player>>(await File.ReadAllTextAsync("gh-pages/sp.json"));
			_mpPlayers = JsonConvert.DeserializeObject<List<Player>>(await File.ReadAllTextAsync("gh-pages/mp.json"));
		}
		public async Task Build(string file, int maxRank = 10)
		{
			if (File.Exists(file)) File.Delete(file);
			var cache = new Dictionary<ulong, string>();

			// Local function
			async Task<List<string>> BuildRows(List<Player> source, Dictionary<string, int> mode)
			{
				var players = source
					.OrderBy(p => p.Score);

				var rank = 0;
				var current = -1;
				var span = 0;
				var rows = new List<string>();
				var perfectscore = mode.Sum(x => x.Value);

				foreach (var player in players)
				{
					// Get Steam profile to resolve name
					if (!cache.ContainsKey(player.Id))
						cache.Add(player.Id, (await _client.GetProfileAsync(player.Id)).Name);

					if (current != player.Score)
					{
						rank++;
						if (rank > maxRank) break;

						current = player.Score;
						span = players.Count(p => p.Score == current);
						rows.Add(StartRow(rank, span, player, cache[player.Id], perfectscore));
					}
					else
					{
						rows.Add(FillRow(player, cache[player.Id], perfectscore));
					}

					Console.WriteLine($"[{player.Id}] {player.Score} by {cache[player.Id]}");
					await Task.Delay(1000);
				}
				return rows;
			}

			var sp = await BuildRows(_spPlayers, WorldRecords.SinglePlayer);
			var mp = await BuildRows(_mpPlayers, WorldRecords.Cooperative);

			await File.WriteAllTextAsync(file, GetPage(sp, mp));
		}

		private string GetPage(IEnumerable<string> singlePlayerRows, IEnumerable<string> cooperativeRows)
		{
			return
$@"<!DOCTYPE html>
<html>
	<head>
		<title>SteamCommunity.Net | Portal 2 Least Portals</title>
		<link href=""https://fonts.googleapis.com/css?family=Roboto"" rel=""stylesheet"">
		<style>table,td,th{{border-collapse:collapse;border:1px solid #ddd;text-align: center;}}table.wrs{{width:25%;}}table.wrholders{{width:20%;}}th,td{{padding: 3px;}}a{{color:inherit;text-decoration:none;}}a:hover{{color:#FF8C00;}}</style>
	</head>
	<body style=""font-family:'Roboto',sans-serif;color:#FFFFFF;background-color:#23272A;"">
		<div>
			<h1 align=""center""><a href=""/SteamCommunity.Net"">Portal 2 Least Portals</a></h1>
		</div>
		<div>
			<h4 align=""center"">Single Player</h4>
			<table align=""center"" class=""wrs"">
				<thead>
					<tr>
						<th>Rank</th>
						<th>Player</th>
						<th>Total<sup>1</sup></th>
					</tr>
				</thead>
				<tbody>
{string.Join("\n", singlePlayerRows)}
				</tbody>
			</table>
		</div>
		<div>
			<h4 align=""center"">Cooperative</h4>
			<table align=""center"" class=""wrs"">
				<thead>
					<tr>
						<th>Rank</th>
						<th>Player</th>
						<th>Total</th>
					</tr>
				</thead>
				<tbody>
{string.Join("\n", cooperativeRows)}
				</tbody>
			</table>
		</div>
		<div align=""center"">
			<br>
			<br>
			<br>Last Update: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss '(UTC)'")}
			<br>
			<br>
			<br><small><sup>1</sup> Excluding Jail Break, Neurotoxin Sabotage, Dual Lasers, Fizzler Intro, Laser Relays and Turret Intro</small>
		</div>
	</body>
</html>";
		}
		private string StartRow(int rank, int rowSpan, Player player, string name, int possible)
		{
			var stats = (player.Score - possible != 0) ? $" ({possible}+{player.Score - possible})" : string.Empty;
			return
$@"					<tr>
						<td rowspan=""{rowSpan}"">{rank}.</td>
						<td><a href=""https://steamcommunity.com/profiles/{player.Id}"">{name}</a></td>
						<td title=""{(int)(((double)possible / player.Score) * 100)}%{stats}"">{player.Score}</td>
					</tr>";
		}
		private string FillRow(Player player, string name, int possible)
		{
			var stats = (player.Score - possible != 0) ? $" ({possible}+{player.Score - possible})" : string.Empty;
			return
$@"					<tr>
						<td><a href=""https://steamcommunity.com/profiles/{player.Id}"">{name}</a></td>
						<td title=""{(int)(((double)possible / player.Score) * 100)}%{stats}"">{player.Score}</td>
					</tr>";
		}
	}
}