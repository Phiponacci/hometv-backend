namespace backend.Models.Sports
{
    public class ScoreRecord
    {
        public string id { get; set; }
        public string? sport_key { get; set; }
        public string? sport_title { get; set; }
        public DateTime? commence_time { get; set; }
        public bool completed { get; set; }
        public string? home_team { get; set; }
        public string? away_team { get; set; }
        public List<TeamScore>? scores { get; set; }
        public DateTime? last_update { get; set; }
    }

    public class TeamScore
    {
        public int Id { get; set; }
        public string? name { get; set; }
        public string? score { get; set; }
    }
}
