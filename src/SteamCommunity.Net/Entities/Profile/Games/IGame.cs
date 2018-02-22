namespace SteamCommunity
{
	public interface IGame
	{
		int Id { get; }
		string Name { get; }
		string Logo { get; }
		string StoreLink { get; }
		float HoursOnRecord { get; }
		string StatsLink { get; }
		string GlobalStatsLink { get; }
	}
}