using backend.Data;
using backend.Models.Weather;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CityController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<City> Get()
        {
            return Ok(_db.Cities);
        }

        [HttpPost]
        public ActionResult<City> AddCity([FromBody] City city)
        {
            _db.Cities.Add(city);
            _db.SaveChanges();
            return Ok(city);
        }

        [HttpGet("active")]
        public IEnumerable<City> GetActiveCities()
        {
            return _db.Cities.Where(city => city.IsActive);
        }

        [HttpPut("{cityId:int}")]
        public ActionResult<City> ToggleCity(int cityId)
        {
            var city = _db.Cities.FirstOrDefault(city => city.Id == cityId);
            if (city != null && city != default(City))
            {
                city.IsActive = !city.IsActive;
                _db.Update(city);
                _db.SaveChanges();
                return Ok(city);
            }
            return NotFound();
        }


        [HttpDelete("{cityId:int}")]
        public ActionResult<City> DeleteCity(int cityId)
        {
            var city = _db.Cities.FirstOrDefault(city => city.Id == cityId);
            if (city != null && city != default(City))
            {
                _db.Cities.Remove(city);
                _db.SaveChanges();
                return Ok(city);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
