namespace SteamCommunity
{
	public interface IAchievement
	{
		string IconClosed { get; }
		string IconOpen { get; }
		string Name { get; }
		string ApiName { get; }
		string Description { get; }
		ulong UnlockTimestamp { get; }
	}
}