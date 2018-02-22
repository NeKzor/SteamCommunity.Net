using System.Threading.Tasks;
using Model = SteamCommunity.API.GameModel;

namespace SteamCommunity
{
	public class Game : IGame
	{
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Logo { get; private set; }
		public string StoreLink { get; private set; }
		public float HoursOnRecord { get; private set; }
		public string StatsLink { get; private set; }
		public string GlobalStatsLink { get; private set; }

		internal SteamCommunityClient Client { get; private set; }

		public async Task<IStatsLeaderboard> GetLeaderboardsAsync(IGame game, bool ignoreCache = false)
			=> await Client.GetLeaderboardsAsync(game.Id)
				.ConfigureAwait(false);

		internal static Game Create(SteamCommunityClient client, Model model)
		{
			return new Game()
			{
				Id = model.AppId,
				Name = model.Name,
				Logo = model.Logo,
				StoreLink = model.StoreLink,
				HoursOnRecord = float.Parse(model.HoursOnRecord),
				StatsLink = model.StatsLink,
				GlobalStatsLink = model.GlobalStatsLink,
				Client = client
			};
		}
	}
}