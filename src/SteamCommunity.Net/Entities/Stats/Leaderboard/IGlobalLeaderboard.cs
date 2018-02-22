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
		Task<IGlobalLeaderboard> GetNextAsync(uint jumps);
		Task<IGlobalLeaderboard> GetNextAsync(bool ignoreCache);
		Task<IGlobalLeaderboard> GetNextAsync(uint jumps, bool ignoreCache);
		Task<IGlobalLeaderboard> GetPreviousAsync();
		Task<IGlobalLeaderboard> GetPreviousAsync(uint jumps);
		Task<IGlobalLeaderboard> GetPreviousAsync(bool ignoreCache);
		Task<IGlobalLeaderboard> GetPreviousAsync(uint jumps, bool ignoreCache);
	}
}