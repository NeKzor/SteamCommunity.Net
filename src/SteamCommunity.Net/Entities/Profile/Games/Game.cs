using System.Threading.Tasks;
using Model = SteamCommunity.API.GameModel;

namespace SteamCommunity
{
	public class Game : IGame
	{
		public int AppId { get; private set; }
		public string Name { get; private set; }
		public string Logo { get; private set; }
		public string StoreLink { get; private set; }
		public float HoursOnRecord { get; private set; }
		public string StatsLink { get; private set; }
		public string GlobalStatsLink { get; private set; }

		internal SteamCommunityClient Client { get; private set; }

		public async Task<IStatsLeaderboard> GetLeaderboardsAsync(bool ignoreCache = false)
			=> await Client.GetLeaderboardsAsync(AppId, ignoreCache)
				.ConfigureAwait(false);

		internal static Game Create(SteamCommunityClient client, Model model)
		{
			return new Game()
			{
				AppId = model.AppId,
				Name = model.Name,
				Logo = model.Logo,
				StoreLink = model.StoreLink,
				HoursOnRecord = (model.HoursOnRecord != null) ? float.Parse(model.HoursOnRecord) : 0f,
				StatsLink = model.StatsLink,
				GlobalStatsLink = model.GlobalStatsLink,
				Client = client
			};
		}
	}
}