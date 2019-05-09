using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamCommunity
{
	public interface IGlobalLeaderboard
	{
		int Id { get; }
		int AppId { get; }
		string AppFriendlyName { get; }
		int TotalLeaderboardEntries { get; }
		int? EntryStart { get; }
		int? EntryEnd { get; }
		string NextRequestUrl { get; }
		int ResultCount { get; }
		IEnumerable<ILeaderboardEntry> Entries { get; }
		Task<IGlobalLeaderboard> GetNextAsync();
		Task<IGlobalLeaderboard> GetNextAsync(int jumps);
		Task<IGlobalLeaderboard> GetNextAsync(bool ignoreCache);
		Task<IGlobalLeaderboard> GetNextAsync(int jumps, bool ignoreCache);
		Task<IGlobalLeaderboard> GetPreviousAsync();
		Task<IGlobalLeaderboard> GetPreviousAsync(int jumps);
		Task<IGlobalLeaderboard> GetPreviousAsync(bool ignoreCache);
		Task<IGlobalLeaderboard> GetPreviousAsync(int jumps, bool ignoreCache);
	}
}