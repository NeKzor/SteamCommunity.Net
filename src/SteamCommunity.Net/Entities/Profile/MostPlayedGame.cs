using Model = SteamCommunity.API.MostPlayedGameModel;

namespace SteamCommunity
{
	public class MostPlayedGame : IMostPlayedGame
	{
		public string GameName { get; private set; }
		public string GameLink { get; private set; }
		public string GameIcon { get; private set; }
		public string GameLogo { get; private set; }
		public string GameLogoSmall { get; private set; }
		public string HoursPlayed { get; private set; }
		public string HoursOnRecord { get; private set; }
		public string StatsName { get; private set; }

		internal static MostPlayedGame Create(Model model)
		{
			return new MostPlayedGame()
			{
				GameName = model.GameName,
				GameLink = model.GameLink,
				GameIcon = model.GameIcon,
				GameLogo = model.GameLogo,
				GameLogoSmall = model.GameLogoSmall,
				HoursPlayed = model.HoursPlayed,
				HoursOnRecord = model.HoursOnRecord,
				StatsName = model.StatsName
			};
		}
	}
}