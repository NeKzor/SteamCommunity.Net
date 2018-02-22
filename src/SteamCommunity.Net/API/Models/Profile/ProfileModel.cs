using System.Collections.Generic;
using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("profile")]
	public class ProfileModel
	{
		[XmlElement("steamID64")]
		public ulong SteamId64 { get; set; }
		[XmlElement("steamID")]
		public string SteamId { get; set; }
		[XmlElement("onlineState")]
		public string OnlineState { get; set; }
		[XmlElement("stateMessage")]
		public string StateMessage { get; set; }
		[XmlElement("privacyState")]
		public string PrivacyState { get; set; }
		[XmlElement("visibilityState")]
		public int VisibilityState { get; set; }
		[XmlElement("avatarIcon")]
		public string AvatarIcon { get; set; }
		[XmlElement("avatarMedium")]
		public string AvatarMedium { get; set; }
		[XmlElement("avatarFull")]
		public string AvatarFull { get; set; }
		[XmlElement("vacBanned")]
		public int VacBanned { get; set; }
		[XmlElement("tradeBanState")]
		public string TradeBanState { get; set; }
		[XmlElement("isLimitedAccount")]
		public int IsLimitedAccount { get; set; }
		// Public only
		[XmlElement("customURL")]
		public string CustomUrl { get; set; }
		[XmlElement("memberSince")]
		public string MemberSince { get; set; }
		[XmlElement("hoursPlayed2Wk")]
		public string HoursPlayed2Wk { get; set; }
		[XmlElement("headline")]
		public string Headline { get; set; }
		[XmlElement("location")]
		public string Location { get; set; }
		[XmlElement("realname")]
		public string Realname { get; set; }
		[XmlElement("summary")]
		public string Summary { get; set; }
		[XmlArray("mostPlayedGames")]
		public List<MostPlayedGameModel> MostPlayedGames { get; set; }
		[XmlArray("groups")]
		public List<GroupModel> Groups { get; set; }
	}
}