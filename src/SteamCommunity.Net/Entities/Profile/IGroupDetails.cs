namespace SteamCommunity
{
	public interface IGroupDetails
	{
		ulong Id { get; }
		string Name { get; }
		string Url { get; }
		string Headline { get; }
		string Summary { get; }
		string AvatarIcon { get; }
		string AvatarMedium { get; }
		string AvatarFull { get; }
		int MemberCount { get; }
		int MembersInChat { get; }
		int MembersInGame { get; }
		int MembersOnline { get; }
	}
}