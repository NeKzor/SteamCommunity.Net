using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("player")]
	public class PlayerStatsPlayerModel
	{
		[XmlElement("steamID64")]
		public ulong SteamId64 { get; set; }
		[XmlElement("customURL")]
		public string CustomUrl { get; set; }
	}
}