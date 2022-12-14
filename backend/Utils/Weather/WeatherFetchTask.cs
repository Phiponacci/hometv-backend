using backend.Data;
using backend.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace backend.Utils.Weather
{
    public class WeatherFetchTask : BackgroundService
    {
        private readonly AppDbContext _db;

        private readonly TimeSpan _period = TimeSpan.FromMinutes(1);

        public WeatherFetchTask(IServiceScopeFactory scopeFactory)
        {
            var scope = scopeFactory.CreateScope();

            // Get a Dbcontext from the scope
            _db = scope.ServiceProvider
                .GetRequiredService<AppDbContext>();
            var api = _db.APIDefs.Where(def => def.ApiType == APIType.OPEN_WEATHER_MAP_API).SingleOrDefault();
            if (api == default(APIDefinition))
            {
                var _api = new APIDefinition { Url = "https://api.openweathermap.org/data/2.5/weather/", ApiType = APIType.OPEN_WEATHER_MAP_API, ApiKey = "7e5caadb139e69711ea362871d82a4ed", Name = "Open Weather Map API" };
                _db.APIDefs.Add(_api);
                _db.SaveChanges();
                WeatherApiClient.SetUpApi(_api);
            }
            else
                WeatherApiClient.SetUpApi(api);
            ClearWeatherRecords();
        }

        private void ClearWeatherRecords()
        {
            if (_db.WeatherWindRecords.Count() > 0)
            {
                _db.RemoveRange(_db.WeatherWindRecords.ToList());
                _db.SaveChanges();
            }
            if (_db.WeatherMainRecords.Count() > 0)
            {
                _db.RemoveRange(_db.WeatherMainRecords.ToList());
                _db.SaveChanges();
            }
            if (_db.WeatherSysRecords.Count() > 0)
            {
                _db.RemoveRange(_db.WeatherSysRecords.ToList());
                _db.SaveChanges();
            }
            if (_db.WeatherCoordRecords.Count() > 0)
            {
                _db.RemoveRange(_db.WeatherCoordRecords.ToList());
                _db.SaveChanges();
            }


            if (_db.WeatherRecords.Count() > 0)
            {
                _db.RemoveRange(_db.WeatherRecords.ToList());
                _db.SaveChanges();
            }
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SaveWeather();
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (
                !stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                SaveWeather();
            }
        }

        private void SaveWeather()
        {
            _db.Cities
                .Include(city => city.WeatherRecord!)
                .Include(city => city.WeatherRecord!.wind)
                .Include(city => city.WeatherRecord!.coord)
                .Include(city => city.WeatherRecord!.main)
                .Include(city => city.WeatherRecord!.weather)
                .Include(city => city.WeatherRecord!.sys)
                .Where(city => city.IsActive)
                .ToList()
                .ForEach(city =>
                    {
                        var weather = WeatherApiClient.FetchWeather(city.Latitude, city.Longitude).Result;
                        if (weather != null)
                        {
                            if (city.WeatherRecord != null)
                            {
                                _db.WeatherRecords.Remove(city.WeatherRecord);
                                _db.SaveChanges();
                            }
                            _db.Add(weather);
                            city.WeatherRecord = weather;
                            _db.Update(city);
                            _db.SaveChanges();
                        }
                    });
        }
    }
}

/*
{
  "name": "Torento",
  "longitude": 43.69099111036905,
  "latitude":  -79.41267898901532,
  "isActive": true
}
*/
