using Newtonsoft.Json;
using Portal2Boards.Extensions;

namespace SkillPoints
{
    internal class Map
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("mode")]
        public Portal2MapType Mode { get; set; }
        [JsonProperty("wr")]
        public uint? WorldRecord { get; set; }
    }
}
