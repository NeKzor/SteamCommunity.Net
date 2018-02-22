using System.Xml.Serialization;

namespace SteamCommunity.API
{
	public class PlayerStatsGameModel
	{
		[XmlElement("gameFriendlyName")]
		public string GameFriendlyName { get; set; }
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
	}
}