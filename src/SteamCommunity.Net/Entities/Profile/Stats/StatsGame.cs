using Model = SteamCommunity.API.PlayerStatsGameModel;

namespace SteamCommunity
{
	public class StatsGame : IStatsGame
	{
		public string GameFriendlyName { get; private set; }
		public string GameName { get; private set; }
		public string GameLink { get; private set; }
		public string GameIcon { get; private set; }
		public string GameLogo { get; private set; }
		public string GameLogoSmall { get; private set; }

		internal static StatsGame Create(Model model)
		{
			return new StatsGame()
			{
				GameFriendlyName = model.GameFriendlyName,
				GameName = model.GameName,
				GameLink = model.GameLink,
				GameIcon = model.GameIcon,
				GameLogo = model.GameLogo,
				GameLogoSmall = model.GameLogoSmall
			};
		}
	}
}