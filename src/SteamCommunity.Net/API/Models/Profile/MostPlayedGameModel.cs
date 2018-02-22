using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("mostPlayedGame")]
	public class MostPlayedGameModel
	{
		[XmlElement("gameName")]
		public string GameName { get; set; }
		[XmlElement("gameLink")]
		public string GameLink { get; set; }
		[XmlElement("gameIcon")]
		public string GameIcon { get; set; }
		[XmlElement("gameLogo")]
		public string GameLogo { get; set; }
		[XmlElement("gameLogoSmall")]
		public string GameLogoSmall { get; set; }
		[XmlElement("hoursPlayed")]
		public string HoursPlayed { get; set; }
		[XmlElement("hoursOnRecord")]
		public string HoursOnRecord { get; set; }
		[XmlElement("statsName")]
		public string StatsName { get; set; }
	}
}