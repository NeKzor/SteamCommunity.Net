using System.Collections.Generic;
using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("statsfeed")]
	public class StatsFeedModel
	{
		[XmlElement("steamID64")]
		public ulong SteamId64 { get; set; }
		[XmlArray("stats")]
		[XmlArrayItem("item")]
		public List<StatsFeedItemModel> Stats { get; set; }
		[XmlArray("achievements")]
		[XmlArrayItem("item")]
		public List<StatsFeedItemModel> Achievements { get; set; }
	}
}