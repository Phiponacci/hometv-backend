using backend.Data;
using backend.Models.News;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocalHeadlineController : ControllerBase
    {
        private readonly AppDbContext _db;

        public LocalHeadlineController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<LocalHeadline> Get()
        {
            return Ok(_db.LocalHeadlines);
        }

        [HttpGet("active")]
        public IEnumerable<LocalHeadline> GetActiveHeadlines()
        {
            return _db.LocalHeadlines.Where(headline => headline.IsActive);
        }


        [HttpPost]
        public ActionResult<LocalHeadline> AddHeadline([FromBody] LocalHeadline localHeadline)
        {
            _db.LocalHeadlines.Add(localHeadline);
            _db.SaveChanges();
            return Ok(localHeadline);
        }


        [HttpPut("{headlineId:int}")]
        public ActionResult<LocalHeadline> ToggleHeadline(int? headlineId)
        {
            var headline = _db.LocalHeadlines.FirstOrDefault(headline => headline.Id == headlineId);
            if (headline != null && headline != default(LocalHeadline))
            {
                headline.IsActive = !headline.IsActive;
                _db.Update(headline);
                _db.SaveChanges();
                return Ok(headline);
            }
            return NotFound();
        }


        [HttpDelete("{headlineId:int}")]
        public ActionResult<LocalHeadline> DeleteHeadline(int? headlineId)
        {
            var headline = _db.LocalHeadlines.FirstOrDefault(headline => headline.Id == headlineId);
            if (headline != null && headline != default(LocalHeadline))
            {
                _db.LocalHeadlines.Remove(headline);
                _db.SaveChanges();
                return Ok(headline);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
