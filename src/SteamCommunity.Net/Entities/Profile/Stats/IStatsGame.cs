namespace SteamCommunity
{
	public interface IStatsGame
	{
		string FriendlyName { get; }
		string Name { get; }
		string Link { get; }
		string Icon { get; }
		string Logo { get; }
		string LogoSmall { get; }
	}
}