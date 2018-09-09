using Newtonsoft.Json;
using Portal2Boards.Extensions;

namespace LeastPortals
{
    internal class ScoreEntry
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("mode")]
        public Portal2MapType Mode { get; set; }
        [JsonProperty("score")]
        public int? Score { get; set; }
    }
}
