using System.Collections.Generic;

namespace SteamCommunity
{
	public interface IGroup
	{
		ulong Id { get; }
		IGroupDetails Details { get; }
		int MemberCount { get; }
		int TotalPages { get; }
		int CurrentPage { get; }
		int StartingMember { get; }
		string NextPageLink { get; }
		IEnumerable<ulong> Members { get; }
	}
}