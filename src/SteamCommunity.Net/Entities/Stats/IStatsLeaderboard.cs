using System.Collections.Generic;

namespace SteamCommunity
{
	public interface IStatsLeaderboard
	{
		int AppId { get; }
		string AppFriendlyName { get; }
		int LeaderboardCount { get; }
		IEnumerable<IStatsLeaderboardEntry> Entries { get; }
	}
}