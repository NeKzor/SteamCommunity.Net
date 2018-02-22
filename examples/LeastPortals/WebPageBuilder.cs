using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SteamCommunity;

namespace LeastPortals
{
	internal class Player
	{
		public ulong Id { get; set; }
		public int Score { get; set; }
	}

	internal class WebPageBuilder
	{
		private readonly string _gameName;
		private readonly SteamCommunityClient _client;
		private readonly List<Player> _players;

		public WebPageBuilder(string gameName)
		{
			_gameName = gameName;
			_client = new SteamCommunityClient("LeastPortals/1.0", false);
			_client.Log += Logger.LogSteamStatsClient;
			_players = new List<Player>();
		}

		public async Task Initialize()
		{
			var game = await _client.GetLeaderboardsAsync(_gameName);
			foreach (var lb in game.Entries
				.Where(lb => lb.Name.StartsWith("challenge_portals_sp"))
				.Take(3))
			{
				if (lb.Id == 47747) continue; // Skip Fizzler Intro
				Console.WriteLine(lb.DisplayName);

				var page = await _client.GetLeaderboardAsync(_gameName, lb.Id);
				await Task.Delay(50);
				page.Entries.Concat((await (page as IGlobalLeaderboard).GetNextAsync()).Entries);
				/* await Task.Delay(50);
				page.Entries.Concat((await (page as IGlobalLeaderboard).GetNextAsync(2)).Entries); */

				foreach (var entry in page.Entries
					.Where(entry => entry.Score >= 0))
				{
					if (!_players.Any(p => p.Id == entry.SteamId))
					{
						_players.Add(new Player()
						{
							Id = entry.SteamId,
							Score = entry.Score
						});
						continue;
					}
					_players.First(p => p.Id == entry.SteamId).Score++;
				}

				await Task.Delay(100);
			}
			Console.WriteLine();
		}

		public async Task Build(string file)
		{
			if (File.Exists(file)) File.Delete(file);

			var pages = new List<string>();
			// Header and stuff
			pages.Add("");
			pages.Add("");
			pages.Add("");
			pages.Add("");
			pages.Add("");

			// Table
			pages.Add("");
			pages.Add("");
			pages.Add("");
			foreach (var player in _players
				.OrderBy(p => p.Score)
				.ThenBy(p => p.Id)
				.Take(10))
			{
				var profile = await _client.GetProfileAsync(player.Id);
				Console.WriteLine($"[{profile.Id}] {player.Score} by {profile.NameId}");
				pages.Add("");
				pages.Add("");
				pages.Add("");
				pages.Add("");
				await Task.Delay(100);
			}
			await File.WriteAllLinesAsync(file, pages);
		}
	}

	internal static class Logger
	{
		public static Task LogSteamStatsClient(object _, LogMessage message)
		{
			Console.WriteLine(message);
			return Task.CompletedTask;
		}
	}
}