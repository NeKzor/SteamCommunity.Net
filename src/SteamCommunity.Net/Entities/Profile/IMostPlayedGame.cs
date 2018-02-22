namespace SteamCommunity
{
	public interface IMostPlayedGame
	{
		string GameName { get; }
		string GameLink { get; }
		string GameIcon { get; }
		string GameLogo { get; }
		string GameLogoSmall { get; }
		string HoursPlayed { get; }
		string HoursOnRecord { get; }
		string StatsName { get; }
	}
}