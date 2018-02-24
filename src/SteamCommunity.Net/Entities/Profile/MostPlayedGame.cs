using Model = SteamCommunity.API.MostPlayedGameModel;

namespace SteamCommunity
{
	public class MostPlayedGame : IMostPlayedGame
	{
		public string Name { get; private set; }
		public string Link { get; private set; }
		public string Icon { get; private set; }
		public string Logo { get; private set; }
		public string LogoSmall { get; private set; }
		public string HoursPlayed { get; private set; }
		public string HoursOnRecord { get; private set; }
		public string StatsName { get; private set; }

		internal static MostPlayedGame Create(Model model)
		{
			return new MostPlayedGame()
			{
				Name = model.GameName,
				Link = model.GameLink,
				Icon = model.GameIcon,
				Logo = model.GameLogo,
				LogoSmall = model.GameLogoSmall,
				HoursPlayed = model.HoursPlayed,
				HoursOnRecord = model.HoursOnRecord,
				StatsName = model.StatsName
			};
		}
	}
}