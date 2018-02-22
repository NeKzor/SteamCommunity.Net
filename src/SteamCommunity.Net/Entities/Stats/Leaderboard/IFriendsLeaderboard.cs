using System.Collections.Generic;

namespace SteamCommunity
{
	public interface IFriendsLeaderboard
	{
		int Id { get; }
		int AppId { get; }
		string AppFriendlyName { get; }
		int TotalLeaderboardEntries { get; }
		ulong? SteamId { get; }
		string NextRequestUrl { get; }
		int ResultCount { get; }
		IEnumerable<ILeaderboardEntry> Entries { get; }
	}
}