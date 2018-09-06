using Newtonsoft.Json;
using Portal2Boards.Extensions;

namespace SkillPoints
{
    internal class ScoreEntry
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("mode")]
        public Portal2MapType Mode { get; set; }
        [JsonProperty("score")]
        public decimal Score { get; set; }
    }
}
