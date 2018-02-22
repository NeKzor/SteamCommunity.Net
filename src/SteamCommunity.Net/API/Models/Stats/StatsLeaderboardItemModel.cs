using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("leaderboard")]
	public class StatsLeaderboardItemModel
	{
		[XmlElement("url")]
		public string Url { get; set; }
		[XmlElement("lbid")]
		public int LbId { get; set; }
		[XmlElement("name")]
		public string Name { get; set; }
		[XmlElement("display_name")]
		public string DisplayName { get; set; }
		[XmlElement("entries")]
		public int Entries { get; set; }
		[XmlElement("sortmethod")]
		public int SortMethod { get; set; }
		[XmlElement("displaytype")]
		public int DisplayType { get; set; }
	}
}