namespace WarzoneFanout.Domain
{
    public class AllStats
    {
        public string? Username { get; set; }

        public List<GameStats>? GameStats { get; set; }

        public string? ErrorMessage { get; set; }
    }
}