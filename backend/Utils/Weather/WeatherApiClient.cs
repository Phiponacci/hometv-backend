using backend.Models.Auth;
using backend.Models.Weather;
using Newtonsoft.Json;

namespace backend.Utils.Weather
{
    public class WeatherApiClient
    {
        private static readonly HttpClient client = new HttpClient();

        private static APIDefinition _api;

        public async static Task<WeatherRecord?> FetchWeather(double lat, double lon)
        {
            //
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{_api.Url}?lat={lat}&lon={lon}&appid={_api.ApiKey}&units=metric");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var weather = JsonConvert.DeserializeObject<WeatherRecord>(result);
                    return weather;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static void SetUpApi(APIDefinition api)
        {
            _api = api;
        }
    }
}
