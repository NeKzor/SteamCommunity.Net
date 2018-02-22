namespace SteamCommunity
{
	public interface IStatsFeedEntry
	{
		string ApiName { get; }
		double Value { get; }
	}
}