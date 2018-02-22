using System.Collections.Generic;
using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("memberList")]
	public class MemberListModel
	{
		[XmlElement("groupID64")]
		public ulong GroupId64 { get; set; }
		[XmlElement("groupDetails")]
		public GroupDetailsModel GroupDetails { get; set; }
		[XmlElement("memberCount")]
		public int MemberCount { get; set; }
		[XmlElement("totalPages")]
		public int TotalPages { get; set; }
		[XmlElement("currentPage")]
		public int CurrentPage { get; set; }
		[XmlElement("startingMember")]
		public int StartingMember { get; set; }
		[XmlElement("nextPageLink")]
		public string NextPageLink { get; set; }
		[XmlArray("members")]
		[XmlArrayItem("steamID64")]
		public List<ulong> Members { get; set; }
	}
}