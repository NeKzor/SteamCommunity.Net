using System.Collections.Generic;
using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("friendsList")]
	public class FriendsListModel
	{
		[XmlElement("steamID64")]
		public ulong SteamId64 { get; set; }
		[XmlElement("steamID")]
		public string SteamId { get; set; }
		[XmlArray("friends")]
		[XmlArrayItem("friend")]
		public List<ulong> Friends { get; set; }
	}
}