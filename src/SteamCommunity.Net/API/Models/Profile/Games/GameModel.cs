using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("game")]
	public class GameModel
	{
		[XmlElement("appID")]
		public int AppId { get; set; }
		[XmlElement("name")]
		public string Name { get; set; }
		[XmlElement("logo")]
		public string Logo { get; set; }
		[XmlElement("storeLink")]
		public string StoreLink { get; set; }
		[XmlElement("hoursOnRecord")]
		public string HoursOnRecord { get; set; }
		[XmlElement("statsLink")]
		public string StatsLink { get; set; }
		[XmlElement("globalStatsLink")]
		public string GlobalStatsLink { get; set; }
	}
}