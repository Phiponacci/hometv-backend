using backend.Data;
using backend.Models.Auth;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {

        private readonly AppDbContext _db;

        public APIController(AppDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IEnumerable<APIDefinition> GetAPIs() => _db.APIDefs;
        

        [HttpGet("{type}")]
        public ActionResult<APIDefinition?> GetByType(APIType type) => _db.APIDefs.Where(api => api.ApiType == type).SingleOrDefault();


        [HttpGet("types")]
        public ActionResult GetTypes() => Ok(Enum.GetNames(typeof(APIType)));

        [HttpPut]
        public IActionResult UpdateAPI([FromBody] APIDefinition api)
        {
            if (_db.APIDefs.Where(_api => api.Id == _api.Id).Count() == 0)
            {
                return NotFound();
            }
            _db.APIDefs.Update(api);
            _db.SaveChanges();
            return Ok(api);
        }
    }
}
