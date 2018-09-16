using Newtonsoft.Json;

namespace Seum
{
    internal class ScoreItem
    {
        [JsonProperty("rank_one")]
        public uint RankOne { get; set; }
        [JsonProperty("rank_ten")]
        public uint RankTen { get; set; }
    }
}
