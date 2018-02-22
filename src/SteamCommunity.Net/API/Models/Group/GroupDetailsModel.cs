using System.Xml.Serialization;

namespace SteamCommunity.API
{
	public class GroupDetailsModel
	{
		[XmlElement("groupName")]
		public string GroupName { get; set; }
		[XmlElement("groupURL")]
		public string GroupUrl { get; set; }
		[XmlElement("headline")]
		public string Headline { get; set; }
		[XmlElement("summary")]
		public string Summary { get; set; }
		[XmlElement("avatarIcon")]
		public string AvatarIcon { get; set; }
		[XmlElement("avatarMedium")]
		public string AvatarMedium { get; set; }
		[XmlElement("avatarFull")]
		public string AvatarFull { get; set; }
		[XmlElement("memberCount")]
		public int MemberCount { get; set; }
		[XmlElement("membersInChat")]
		public int MembersInChat { get; set; }
		[XmlElement("membersInGame")]
		public int MembersInGame { get; set; }
		[XmlElement("membersOnline")]
		public int MembersOnline { get; set; }
	}
}