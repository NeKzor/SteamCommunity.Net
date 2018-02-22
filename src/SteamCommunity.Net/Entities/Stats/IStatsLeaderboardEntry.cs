namespace SteamCommunity
{
	public interface IStatsLeaderboardEntry
	{
		int Id { get; }
		string Url { get; }
		string Name { get; }
		string DisplayName { get; }
		int Entries { get; }
		int SortMethod { get; }
		int DisplayType { get; }
	}
}