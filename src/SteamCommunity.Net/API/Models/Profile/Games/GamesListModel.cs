using System.Collections.Generic;
using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("gamesList")]
	public class GamesListModel
	{
		[XmlElement("steamID64")]
		public ulong SteamId64 { get; set; }
		[XmlElement("steamID")]
		public string SteamId { get; set; }
		[XmlArray("games")]
		[XmlArrayItem("game")]
		public List<GameModel> Games { get; set; }
	}
}