namespace backend.Models.Auth
{
    public class APIDefinition
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? ApiKey { get; set; }
        public APIType ApiType { get; set; }
    }

    public enum APIType
    {
        LIVE_SPORTS_ODDS_API,
        OPEN_WEATHER_MAP_API
    }
}
