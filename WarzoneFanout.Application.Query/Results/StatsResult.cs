using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarzoneFanout.Application.Query.Results
{
    public class StatsResult
    {
        public StatsResult(string gameType, int totalKills, int totalDeaths, int score, int matchesPlayed)
        {
            GameType = gameType;
            TotalKills = totalKills;
            TotalDeaths = totalDeaths;
            Score = score;
            MatchesPlayed = matchesPlayed;
        }
        public string GameType { get; set; }

        public int TotalKills { get; set; }

        public int TotalDeaths { get; set; }

        public int Score { get; set; }

        public int MatchesPlayed { get; set; }
    }
}
