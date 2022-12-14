using backend.Data;
using backend.Models.Auth;
using backend.Models.Sports;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SportsController : ControllerBase
    {
        private readonly AppDbContext _db;

        private const string folderPath = "images/sports";

        public SportsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<SportCategory> GetAllSports() => _db.Sports;

        [HttpGet("active")]
        public IEnumerable<SportCategory> GetActiveSports() => _db.Sports.Where(sport => sport.IsActive);

        [HttpPut("toggle/{sportKey}")]
        public ActionResult<SportCategory> ActivateSport(string sportKey)
        {
            var sport = _db.Sports.Where(sport => sport.key == sportKey).FirstOrDefault();
            if (sport == default(SportCategory))
                return NotFound();
            sport!.IsActive = !sport!.IsActive;
            _db.Sports.Update(sport);
            _db.SaveChanges();
            return sport;
        }

        [HttpGet("images/{filename}")]
        public IActionResult GetImage(String filename)
        {
            // get the full path of the pdf storage folder
            string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, folderPath));
            //check if the directory exists
            if (!Directory.Exists(path))
            {
                // if not exists, return Not Found
                return NotFound(new
                {
                    Message = "Directory does not exist!"
                });
            }
            // get the full path of the pdf file
            string filePath = Path.Combine(path, filename);
            // construct a file Info object to verify if the file exists
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                // return the file as a Physical file with the content type of application/pdf
                return PhysicalFile(filePath, "image/*", filename);
            }
            // in case of the file not exist, return not found error with a message
            return NotFound(new
            {
                Message = "File Not found"
            });
        }
    }
}