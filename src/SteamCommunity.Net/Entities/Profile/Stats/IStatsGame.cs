namespace SteamCommunity
{
	public interface IStatsGame
	{
		string GameFriendlyName { get; }
		string GameName { get; }
		string GameLink { get; }
		string GameIcon { get; }
		string GameLogo { get; }
		string GameLogoSmall { get; }
	}
}