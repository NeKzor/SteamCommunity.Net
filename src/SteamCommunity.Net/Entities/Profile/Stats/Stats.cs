using System.Collections.Generic;
using Model = SteamCommunity.API.PlayerStatsModel;

namespace SteamCommunity
{
	public class Stats : IStats
	{
		public string PrivacyState { get; private set; }
		public int VisibilityState { get; private set; }
		public IStatsGame Game { get; private set; }
		public ulong SteamId64 { get; private set; }
		public string CustomUrl { get; private set; }
		public IStatsBase Info { get; private set; }
		public IEnumerable<IAchievement> Achievements { get; private set; }

		internal SteamCommunityClient Client { get; private set; }

		internal static Stats Create(SteamCommunityClient client, Model model)
		{
			var achievements = new List<IAchievement>();
			foreach (var item in model.Achievements)
				achievements.Add(Achievement.Create(item));
			
			return new Stats()
			{
				PrivacyState = model.PrivacyState,
				VisibilityState = model.VisibilityState,
				Game = StatsGame.Create(model.Game),
				SteamId64 = model.SteamId64,
				CustomUrl = model.CustomUrl,
				Info = StatsBase.Create(model.Stats),
				Achievements = achievements,
				Client = client
			};
		}
	}
}