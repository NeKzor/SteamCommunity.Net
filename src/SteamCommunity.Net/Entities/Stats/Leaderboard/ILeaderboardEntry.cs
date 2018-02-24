namespace SteamCommunity
{
	public interface ILeaderboardEntry
	{
		ulong Id { get; }
		int Score { get; }
		int Rank { get; }
		string UgcId { get; }
		string Details { get; }
	}
}