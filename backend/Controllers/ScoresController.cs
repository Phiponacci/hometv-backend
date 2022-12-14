using backend.Data;
using backend.Models.Auth;
using backend.Models.Sports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScoresController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ScoresController(AppDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IEnumerable<ScoreRecord> GetAllScores() => _db.Scores.Include(score => score.scores);


        [HttpGet("active")]
        public IEnumerable<ScoreRecord> GetActiveScores()
        {
            var activeSports = _db.Sports.Where(sport => sport.IsActive).Select(sport => sport.key).ToList();
            return _db.Scores.Where(score => activeSports.Contains(score.sport_key!)).Include(score => score.scores);
        }

        [HttpGet("{sport}")]
        public IEnumerable<ScoreRecord> GetBySport(string sport) => _db.Scores.Where(score => score.sport_key == sport).Include(score=>score.scores);

    }
}