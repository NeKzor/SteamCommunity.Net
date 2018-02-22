using System.Collections.Generic;

namespace SteamCommunity
{
	public interface IMemberList
	{
		ulong Id { get; }
		IGroup Details { get; }
		int MemberCount { get; }
		int TotalPages { get; }
		int CurrentPage { get; }
		int StartingMember { get; }
		string NextPageLink { get; }
		IEnumerable<ulong> Members { get; }
	}
}