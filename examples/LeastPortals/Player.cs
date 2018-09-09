using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Portal2Boards.Extensions;

namespace LeastPortals
{
    internal class Player
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }
        [JsonProperty("entries")]
        public List<ScoreEntry> Entries { get; set; }

        [JsonIgnore]
        private int _singlePlayerScore { get; set; }
        [JsonIgnore]
        private int _cooperativeScore { get; set; }
        [JsonIgnore]
        private int _totalScore => _singlePlayerScore + _cooperativeScore;

        [JsonIgnore]
        public bool IsSinglePlayer => _singlePlayerScore != default;
        [JsonIgnore]
        public bool IsCooperative => _cooperativeScore != default;
        [JsonIgnore]
        public bool IsOverall => IsSinglePlayer && IsCooperative;

        public Player()
        {
            Entries = new List<ScoreEntry>();
        }
        public Player(ulong id, IEnumerable<ulong> excluded) : this()
        {
            Id = id;
            foreach (var map in Portal2.CampaignMaps
                .Where(x => x.IsOfficial)
                .Where(x => !excluded.Contains((ulong)x.BestPortalsId)))
            {
                Entries.Add(new ScoreEntry()
                {
                    Id = (ulong)map.BestPortalsId,
                    Mode = map.Type
                });
            }
        }

        public void Update(int id, int score)
        {
            Entries.First(x => x.Id == (ulong)id).Score = score;
        }
        public void CalculateTotalScore()
        {
            var sp = Entries.Where(e => e.Mode == Portal2MapType.SinglePlayer);
            var mp = Entries.Where(e => e.Mode == Portal2MapType.Cooperative);

            if (!sp.Any(x => x.Score == default))
                _singlePlayerScore = (int)sp.Sum(e => e.Score);
            if (!mp.Any(x => x.Score == default))
                _cooperativeScore = (int)mp.Sum(e => e.Score);
        }
        public int GetTotalScore(Portal2MapType mode)
        {
            switch (mode)
            {
                case Portal2MapType.SinglePlayer:
                    return _singlePlayerScore;
                case Portal2MapType.Cooperative:
                    return _cooperativeScore;
            }
            return _singlePlayerScore + _cooperativeScore;
        }
    }
}
