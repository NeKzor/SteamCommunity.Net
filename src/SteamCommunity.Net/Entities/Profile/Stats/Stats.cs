using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = SteamCommunity.API.PlayerStatsModel;

namespace SteamCommunity
{
	public class Stats : IStats
	{
		public ulong Id { get; private set; }
		public string CustomUrl { get; private set; }
		public PrivacyState PrivacyState { get; private set; }
		public int VisibilityState { get; private set; }
		public IStatsGame Game { get; private set; }
		public IStatsBase Info { get; private set; }
		public IEnumerable<IAchievement> Achievements { get; private set; }

		internal SteamCommunityClient Client { get; private set; }

		public async Task<IPublicProfile> GetProfileAsync(bool ignoreCache = false)
			=> await Client.GetProfileAsync(Id, ignoreCache)
				.ConfigureAwait(false);
		public async Task<IStatsLeaderboard> GetLeaderboardsAsync(bool ignoreCache = false)
			=> await Client.GetLeaderboardsAsync(Game.FriendlyName, ignoreCache)
				.ConfigureAwait(false);

		internal static Stats Create(SteamCommunityClient client, Model model)
		{
			var achievements = new List<IAchievement>();
			foreach (var item in model.Achievements)
				achievements.Add(Achievement.Create(item));
			
			return new Stats()
			{
				PrivacyState = (PrivacyState)Enum.Parse(typeof(PrivacyState), model.PrivacyState, true),
				VisibilityState = model.VisibilityState,
				Game = StatsGame.Create(model.Game),
				Id = model.Player.SteamId64,
				CustomUrl = model.Player.CustomUrl,
				Info = StatsBase.Create(model.Stats),
				Achievements = achievements,
				Client = client
			};
		}
	}
}