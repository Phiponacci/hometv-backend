namespace backend.Models.Weather
{
    public class City
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public WeatherRecord? WeatherRecord { get; set; }
        public bool IsActive { get; set; }

    }
}
