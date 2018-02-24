using System.Collections.Generic;
using System.Threading.Tasks;
using Model = SteamCommunity.API.GamesListModel;

namespace SteamCommunity
{
	public class GameList : IGameList
	{
		public ulong Id { get; private set; }
		public string Name { get; private set; }
		public IEnumerable<IGame> Games { get; private set; }

		internal SteamCommunityClient Client { get; private set; }

		public async Task<IPublicProfile> GetProfileAsync(bool ignoreCache = false)
			=> await Client.GetProfileAsync(Id, ignoreCache)
				.ConfigureAwait(false);
		public async Task<IStats> GetStatsAsync(IGame game, bool ignoreCache = false)
			=> await Client.GetStatsAsync(Id, game.Name, ignoreCache)
				.ConfigureAwait(false);
		public async Task<IStatsFeed> GetStatsFeedAsync(IGame game, bool ignoreCache = false)
			=> await Client.GetStatsFeedAsync(Id, game.AppId, ignoreCache)
				.ConfigureAwait(false);

		internal static GameList Create(SteamCommunityClient client, Model model)
		{
			var games = new List<IGame>();
			foreach (var item in model.Games)
				games.Add(Game.Create(client, item));
			
			return new GameList()
			{
				Id = model.SteamId64,
				Name = model.SteamId,
				Games = games,
				Client = client
			};
		}
	}
}