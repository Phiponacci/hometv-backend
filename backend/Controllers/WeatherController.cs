using backend.Data;
using backend.Models.Weather;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly AppDbContext _db;

        public WeatherController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<WeatherRecord> Get()
        {
            var records = _db.Cities
                .Include(city => city.WeatherRecord!)
                .Include(city => city.WeatherRecord!.wind)
                .Include(city => city.WeatherRecord!.coord)
                .Include(city => city.WeatherRecord!.main)
                .Include(city => city.WeatherRecord!.weather)
                .Include(city => city.WeatherRecord!.sys)
                .ToList();
            return Ok(records);
        }

        [HttpGet("active")]
        public ActionResult<WeatherRecord> GetActiveCities()
        {
            var records = _db.Cities
                .Include(city => city.WeatherRecord!)
                .Include(city => city.WeatherRecord!.wind)
                .Include(city => city.WeatherRecord!.coord)
                .Include(city => city.WeatherRecord!.main)
                .Include(city => city.WeatherRecord!.weather)
                .Include(city => city.WeatherRecord!.sys)
                .Where(city => city.IsActive)
                .ToList();
            return Ok(records);
        }


    }
}
