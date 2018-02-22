using System.Collections.Generic;
using System.Threading.Tasks;
using Model = SteamCommunity.API.GamesListModel;

namespace SteamCommunity
{
	public class GameList : IGameList
	{
		public ulong Id { get; private set; }
		public string ProfileName { get; private set; }
		public IEnumerable<IGame> Games { get; private set; }

		internal SteamCommunityClient Client { get; private set; }

		public async Task<IStatsLeaderboard> GetLeaderboardsAsync(IGame game, bool ignoreCache = false)
			=> await Client.GetLeaderboardsAsync(game.Id)
				.ConfigureAwait(false);

		internal static GameList Create(SteamCommunityClient client, Model model)
		{
			var games = new List<IGame>();
			foreach (var item in model.Games)
				games.Add(Game.Create(client, item));
			
			return new GameList()
			{
				Id = model.SteamId64,
				ProfileName = model.SteamId,
				Games = games,
				Client = client
			};
		}
	}
}