using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("entry")]
	public class LeaderboardEntryModel
	{
		[XmlElement("steamid")]
		public ulong SteamId { get; set; }
		[XmlElement("score")]
		public int Score { get; set; }
		[XmlElement("rank")]
		public int Rank { get; set; }
		[XmlElement("ugcid")]
		public string UgcId { get; set; }
		[XmlElement("details")]
		public string Details { get; set; }
	}
}