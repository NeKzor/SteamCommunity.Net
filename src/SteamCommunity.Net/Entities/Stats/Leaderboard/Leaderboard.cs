using System.Collections.Generic;
using System.Threading.Tasks;
using Model = SteamCommunity.API.LeaderboardModel;

namespace SteamCommunity
{
	public class Leaderboard : IGlobalLeaderboard, IFriendsLeaderboard
	{
		public int Id { get; private set; }
		public int AppId { get; private set; }
		public string AppFriendlyName { get; private set; }
		public int TotalLeaderboardEntries { get; private set; }
		public ulong? SteamId { get; private set; }
		public int? EntryStart { get; private set; }
		public int? EntryEnd { get; private set; }
		public string NextRequestUrl { get; private set; }
		public int ResultCount { get; private set; }
		public IEnumerable<ILeaderboardEntry> Entries { get; private set; }

		internal SteamCommunityClient Client { get; private set; }

		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetNextAsync()
			=> await Client.GetLeaderboardAsync(AppFriendlyName, Id, (int)EntryEnd + 1, ResultCount)
				.ConfigureAwait(false);
		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetNextAsync(int jumps)
			=> await Client.GetLeaderboardAsync(AppFriendlyName, Id, (int)EntryEnd + 1 * jumps, ResultCount)
				.ConfigureAwait(false);
		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetNextAsync(bool ignoreCache)
			=> await Client.GetLeaderboardAsync(AppFriendlyName, Id, (int)EntryEnd + 1, ResultCount, ignoreCache)
				.ConfigureAwait(false);
		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetNextAsync(int jumps, bool ignoreCache)
			=> await Client.GetLeaderboardAsync(AppFriendlyName, Id, ((int)EntryEnd + 1) * jumps, ResultCount, ignoreCache)
				.ConfigureAwait(false);
		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetPreviousAsync()
			=> await Client.GetLeaderboardAsync(AppFriendlyName, Id, (int)EntryStart - ResultCount, ResultCount)
				.ConfigureAwait(false);
		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetPreviousAsync(int jumps)
			=> await Client.GetLeaderboardAsync(AppFriendlyName, Id, (int)EntryStart - (ResultCount * jumps), ResultCount)
				.ConfigureAwait(false);
		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetPreviousAsync(bool ignoreCache)
			=> await Client.GetLeaderboardAsync(AppFriendlyName, Id, (int)EntryStart - ResultCount, ResultCount, ignoreCache)
				.ConfigureAwait(false);
		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetPreviousAsync(int jumps, bool ignoreCache)
			=> await Client.GetLeaderboardAsync(AppFriendlyName, Id, (int)EntryStart - (ResultCount * jumps), ResultCount, ignoreCache)
				.ConfigureAwait(false);

		internal static Leaderboard Create(SteamCommunityClient client, Model model)
		{
			var entries = new List<ILeaderboardEntry>();
			foreach (var item in model.Entries)
				entries.Add(LeaderboardEntry.Create(item));

			return new Leaderboard()
			{
				Id = model.LeaderboardId,
				AppId = model.AppId,
				AppFriendlyName = model.AppFriendlyName,
				TotalLeaderboardEntries = model.TotalLeaderboardEntries,
				SteamId = model.SteamId64,
				EntryStart = model.EntryStart,
				EntryEnd = model.EntryEnd,
				NextRequestUrl = model.NextRequestUrl,
				ResultCount = model.ResultCount,
				Entries = entries,
				Client = client
			};
		}
	}
}