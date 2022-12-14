using backend.Models.Auth;
using backend.Models.Sports;
using backend.Models.Weather;
using Newtonsoft.Json;

namespace backend.Utils.Sports
{
    public class ScoresApiClient
    {
        private static readonly HttpClient client = new HttpClient();
        private static APIDefinition _api;
        private const int DaysFrom = 3; // between 1 and 3

        public async static Task<List<ScoreRecord>?> FetchScores(string sport)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{_api.Url}{sport}/scores/?apiKey={_api.ApiKey}&daysFrom={DaysFrom}");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var score = JsonConvert.DeserializeObject<List<ScoreRecord>>(result);
                    return score;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string MakeScoresUrl(APIDefinition apiDefinition, SportCategory sport)
        {
            if (apiDefinition.ApiType != APIType.LIVE_SPORTS_ODDS_API)
                throw new NotSupportedException(message: "The API type is not supported, please provide a live sports odds api!");
            var fullUrl = $"{apiDefinition.Url}/scores/?apiKey={apiDefinition.ApiKey}&daysFrom=3";
            return "";
        }

        public static void SetUpApi(APIDefinition api)
        {
            _api = api;
        }
    }
}
