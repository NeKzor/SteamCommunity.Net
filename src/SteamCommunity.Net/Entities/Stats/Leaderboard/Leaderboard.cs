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
			=> await Client.GetLeaderboardAsync(AppFriendlyName, AppId, (uint)EntryEnd + 1)
				.ConfigureAwait(false);
		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetNextAsync(uint jumps)
			=> await Client.GetLeaderboardAsync(AppFriendlyName, AppId, (uint)(EntryEnd + 1) * jumps)
				.ConfigureAwait(false);
		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetNextAsync(bool ignoreCache)
			=> await Client.GetLeaderboardAsync(AppFriendlyName, AppId, (uint)EntryEnd + 1, ignoreCache: ignoreCache)
				.ConfigureAwait(false);
		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetNextAsync(uint jumps, bool ignoreCache)
			=> await Client.GetLeaderboardAsync(AppFriendlyName, AppId, (uint)(EntryEnd + 1) * jumps, ignoreCache: ignoreCache)
				.ConfigureAwait(false);
		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetPreviousAsync()
			=> await Client.GetLeaderboardAsync(AppFriendlyName, AppId, (uint)(EntryStart - ResultCount))
				.ConfigureAwait(false);
		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetPreviousAsync(uint jumps)
			=> await Client.GetLeaderboardAsync(AppFriendlyName, AppId, (uint)(EntryStart - (ResultCount * jumps)))
				.ConfigureAwait(false);
		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetPreviousAsync(bool ignoreCache)
			=> await Client.GetLeaderboardAsync(AppFriendlyName, AppId, (uint)(EntryStart - ResultCount), ignoreCache: ignoreCache)
				.ConfigureAwait(false);
		async Task<IGlobalLeaderboard> IGlobalLeaderboard.GetPreviousAsync(uint jumps, bool ignoreCache)
			=> await Client.GetLeaderboardAsync(AppFriendlyName, AppId, (uint)(EntryStart - (ResultCount * jumps)), ignoreCache: ignoreCache)
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