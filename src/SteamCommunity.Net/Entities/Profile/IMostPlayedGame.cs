namespace SteamCommunity
{
	public interface IMostPlayedGame
	{
		string Name { get; }
		string Link { get; }
		string Icon { get; }
		string Logo { get; }
		string LogoSmall { get; }
		string HoursPlayed { get; }
		string HoursOnRecord { get; }
		string StatsName { get; }
	}
}