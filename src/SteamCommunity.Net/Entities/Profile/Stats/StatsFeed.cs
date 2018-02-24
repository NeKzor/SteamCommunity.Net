using System.Collections.Generic;
using System.Threading.Tasks;
using Model = SteamCommunity.API.StatsFeedModel;

namespace SteamCommunity
{
	public class StatsFeed : IStatsFeed
	{
		public ulong Id { get; private set; }
		public IEnumerable<IStatsFeedEntry> Stats { get; private set; }
		public IEnumerable<IStatsFeedEntry> Achievements { get; private set; }

		internal SteamCommunityClient Client { get; private set; }

		public async Task<IPublicProfile> GetProfileAsync(bool ignoreCache = false)
			=> await Client.GetProfileAsync(Id, ignoreCache)
				.ConfigureAwait(false);

		internal static StatsFeed Create(SteamCommunityClient client, Model model)
		{
			var stats = new List<IStatsFeedEntry>();
			var achievements = new List<IStatsFeedEntry>();
			foreach (var item in model.Stats)
				stats.Add(StatsFeedEntry.Create(item));
			foreach (var item in model.Achievements)
				achievements.Add(StatsFeedEntry.Create(item));
			
			return new StatsFeed()
			{
				Id = model.SteamId64,
				Stats = stats,
				Achievements = achievements,
				Client = client
			};
		}
	}
}