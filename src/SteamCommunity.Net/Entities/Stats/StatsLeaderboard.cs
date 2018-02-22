using System.Collections.Generic;
using System.Threading.Tasks;
using Model = SteamCommunity.API.StatsLeaderboardModel;

namespace SteamCommunity
{
	public class StatsLeaderboard : IStatsLeaderboard
	{
		public int AppId { get; private set; }
		public string AppFriendlyName { get; private set; }
		public int LeaderboardCount { get; private set; }
		public IEnumerable<IStatsLeaderboardEntry> Entries { get; private set; }

		internal SteamCommunityClient Client { get; private set; }

		public async Task<IGlobalLeaderboard> GetLeaderboardAsync(
			IStatsLeaderboardEntry entry,
			uint entryStart = 0,
			uint entryEnd = 5000,
			bool ignoreCache = false)
			=> await Client.GetLeaderboardAsync(AppFriendlyName, entry.Id, entryStart, entryEnd, ignoreCache)
				.ConfigureAwait(false);

		internal static StatsLeaderboard Create(SteamCommunityClient client, Model model)
		{
			var entries = new List<IStatsLeaderboardEntry>();
			foreach (var item in model.Leaderboard)
				entries.Add(StatsLeaderboardEntry.Create(item));

			return new StatsLeaderboard()
			{
				AppId = model.AppId,
				AppFriendlyName = model.AppFriendlyName,
				LeaderboardCount = model.LeaderboardCount,
				Entries = entries,
				Client = client
			};
		}
	}
}