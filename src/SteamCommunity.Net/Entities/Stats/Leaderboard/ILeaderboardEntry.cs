namespace SteamCommunity
{
	public interface ILeaderboardEntry
	{
		ulong SteamId { get; }
		int Score { get; }
		int Rank { get; }
		string UgcId { get; }
		string Details { get; }
	}
}