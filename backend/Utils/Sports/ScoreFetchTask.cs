using backend.Data;
using backend.Models.Auth;
using backend.Models.Sports;
using Microsoft.EntityFrameworkCore;

namespace backend.Utils.Sports
{
    public class ScoreFetchTask : BackgroundService
    {
        private readonly AppDbContext _db;

        private readonly TimeSpan _period = TimeSpan.FromHours(10);

        public ScoreFetchTask(IServiceScopeFactory scopeFactory)
        {
            var scope = scopeFactory.CreateScope();

            // Get a Dbcontext from the scope
            _db = scope.ServiceProvider
                .GetRequiredService<AppDbContext>();
            var api = _db.APIDefs.Where(def => def.ApiType == APIType.LIVE_SPORTS_ODDS_API).SingleOrDefault();
            if (api == default(APIDefinition))
            {
                var _api = new APIDefinition { Url = "https://api.the-odds-api.com/v4/sports/", ApiType = APIType.LIVE_SPORTS_ODDS_API, ApiKey = "ae66b72ca2f913fef13e9647427429dc", Name = "The Odds API" };
                _db.APIDefs.Add(_api);
                AddSports();
                _db.SaveChanges();
                ScoresApiClient.SetUpApi(_api);
            }
            else
                ScoresApiClient.SetUpApi(api);
            ClearScores();
        }

        private void ClearScores()
        {
            if(_db.Scores.Count()>0)
            {
                _db.Scores.RemoveRange(_db.Scores);
                _db.SaveChanges();
            }    
        }

        private void AddSports()
        {
            if (_db.Sports.Count() == 0)
                _db.Sports.AddRange(
                    new SportCategory
                    {
                        key = "americanfootball_nfl",
                        group = "American Football",
                        title = "NFL",
                        description = "US Football",
                    },
                    new SportCategory
                    {
                        key = "baseball_mlb",
                        group = "Baseball",
                        title = "MLB",
                        description = "Major League Baseball",
                    },
                    new SportCategory
                    {
                        key = "soccer_usa_mls",
                        group = "Soccer",
                        title = "MLS",
                        description = "Major League Soccer",
                    },
                    new SportCategory
                    {
                        key = "basketball_nba",
                        group = "Basketball",
                        title = "NBA",
                        description = "US Basketball",
                    },
                    new SportCategory
                    {
                        key = "icehockey_nhl",
                        group = "Ice Hockey",
                        title = "NHL",
                        description = "US Ice Hockey"
                    },
                    new SportCategory
                    {
                        key = "soccer_fifa_world_cup",
                        group = "Soccer",
                        title = "FIFAWC",
                        description = "FIFA World Cup"
                    }
                    );
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SaveScore();
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (
                !stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                SaveScore();
            }
        }

        private void SaveScore()
        {
            var scores = new List<ScoreRecord>();
            _db.Sports.Where(sport => sport.IsActive).ToList().ForEach(sport =>
            {
                var result = ScoresApiClient.FetchScores(sport.key).Result;
                if (result == null)
                    return;
                scores.AddRange(result!);
            });
            if(scores.Count() > 0){
                List<ScoreRecord> outdatedScores = _db.Scores.Where(sport => sport.completed && sport.commence_time!.Value.DayOfYear - DateTime.Now.DayOfYear < -3).ToList();
                if(outdatedScores.Count() > 0)
                {
                    _db.Scores.RemoveRange(outdatedScores);
                    _db.SaveChanges();
                }
                if (_db.Scores.Count() > 0)
                {
                    _db.Scores.UpdateRange(scores);
                }
                else
                {
                    _db.Scores.AddRange(scores);
                }
                _db.SaveChanges();
            }
        }
    }
}

