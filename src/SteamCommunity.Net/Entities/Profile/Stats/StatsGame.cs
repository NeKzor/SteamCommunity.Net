using Model = SteamCommunity.API.PlayerStatsGameModel;

namespace SteamCommunity
{
	public class StatsGame : IStatsGame
	{
		public string FriendlyName { get; private set; }
		public string Name { get; private set; }
		public string Link { get; private set; }
		public string Icon { get; private set; }
		public string Logo { get; private set; }
		public string LogoSmall { get; private set; }

		internal static StatsGame Create(Model model)
		{
			return new StatsGame()
			{
				FriendlyName = model.GameFriendlyName,
				Name = model.GameName,
				Link = model.GameLink,
				Icon = model.GameIcon,
				Logo = model.GameLogo,
				LogoSmall = model.GameLogoSmall
			};
		}
	}
}