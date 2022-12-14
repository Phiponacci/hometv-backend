using backend.Data;
using backend.Models.News;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public NewsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<NewsLink> GetNewsLinks()
        {
            return _db.NewsLinks;
        }

        [HttpGet("active")]
        public IEnumerable<NewsLink> GetActiveLinks()
        {
            return _db.NewsLinks.Where(link => link.IsActive);
        }

        [HttpGet("rss")]
        public IActionResult Rss()
        {
            var activeLinks = _db.NewsLinks.Where(link => link.IsActive).ToList();
            var _feed = new List<Item>();

            activeLinks.ToList().ForEach(rss =>
            {
                XmlDocument rssXmlDoc = new XmlDocument();

                // Load the RSS file from the RSS URL
                rssXmlDoc.Load(rss.Link);

                // Serialize the Items in the RSS file
                var channel = rssXmlDoc.SelectSingleNode("rss/channel");
                var jsonString = JsonConvert.SerializeXmlNode(channel);
                var mrss = JsonConvert.DeserializeObject<Root>(jsonString);

                _feed.AddRange(mrss.channel.item);
            });
            return Ok(_feed);
        }

        [HttpPost]
        public ActionResult<NewsLink> AddNewsLink(NewsLink link)
        {
            link.IsActive = false;
            _db.NewsLinks.Add(link);
            _db.SaveChanges();
            return Ok(link);
        }

        [HttpPut("{linkId:int}")]
        public ActionResult<NewsLink> ToggleNewsLink(int linkId)
        {
            var link = _db.NewsLinks.FirstOrDefault(link => link.Id == linkId);
            if (link != null && link != default(NewsLink))
            {
                link.IsActive = !link.IsActive;
                _db.Update(link);
                _db.SaveChanges();
                return Ok(link);
            }
            return NotFound();
        }

        [HttpDelete("{linkId:int}")]
        public ActionResult<NewsLink> DeleteNewsLink(int linkId)
        {
            var link = _db.NewsLinks.FirstOrDefault(link => link.Id == linkId);
            if (link != null && link != default(NewsLink))
            {
                _db.NewsLinks.Remove(link);
                _db.SaveChanges();
                return Ok(link);
            }
            return NotFound();
        }
    }
}