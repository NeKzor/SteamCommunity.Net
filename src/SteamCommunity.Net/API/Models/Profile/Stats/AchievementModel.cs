using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("achievement")]
	public class AchievementModel
	{
		[XmlElement("iconClosed")]
		public string IconClosed { get; set; }
		[XmlElement("iconOpen")]
		public string IconOpen { get; set; }
		[XmlElement("name")]
		public string Name { get; set; }
		[XmlElement("apiname")]
		public string ApiName { get; set; }
		[XmlElement("description")]
		public string Description { get; set; }
		[XmlElement("unlockTimestamp")]
		public ulong UnlockTimestamp { get; set; }
	}
}