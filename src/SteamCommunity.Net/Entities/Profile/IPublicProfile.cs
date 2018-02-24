namespace SteamCommunity
{
	public interface IPublicProfile
	{
		ulong Id { get; }
		string Name { get; }
		OnlineState State { get; }
		string StateMessage { get; }
		PrivacyState Privacy { get; }
		int VisibilityState { get; }
		string AvatarIcon { get; }
		string AvatarMedium { get; }
		string AvatarFull { get; }
		bool IsVacBanned { get; }
		TradeBanState TradeBan { get; }
		bool IsLimitedAccount { get; }
	}
}