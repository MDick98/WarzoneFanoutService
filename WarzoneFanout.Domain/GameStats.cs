namespace WarzoneFanout.Domain
{
    public class GameStats
    {
        public string GameType { get; set; }

        public int TotalKills { get; set; }

        public int TotalDeaths { get; set; }

        public decimal KDRatio => (decimal)TotalKills/TotalDeaths;

        public int Score { get; set; }

        public int MatchesPlayed { get; set; }
    }
}