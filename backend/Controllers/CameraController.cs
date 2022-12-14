using backend.Data;
using backend.Models.Camera;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CameraController : ControllerBase
    {
        private const string folderName = "images";
        private readonly AppDbContext _db;

        public CameraController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<Camera> GetCameras()
        {
            return Ok(_db.Cameras);
        }

        [HttpPost("add_link")]
        public ActionResult<Camera> AddCamera(Camera camera)
        {
            camera.IsImage = false;
            camera.IsActive = false;
            _db.Cameras.Add(camera);
            _db.SaveChanges();
            return Ok(camera);
        }

        [HttpPost("add_image")]
        public async Task<ActionResult<Camera>> AddImage([FromForm] ImageAd imageAd)
        {
            try
            {
                // check if the file not empty
                if (imageAd.File.Length > 0)
                {
                    // get the storage folder
                    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, folderName));
                    //check if the directory exists
                    if (!Directory.Exists(path))
                    {
                        // if not exists, create one
                        Directory.CreateDirectory(path);
                    }
                    // copy the received file to the destination folder
                    var imgName = $"{imageAd.Name}-{imageAd.File.FileName}";
                    var fullPath = Path.Combine(path, imgName);
                    var camera = new Camera { IsActive = false, Link = $"camera/images/{imgName}", Name = imageAd.Name, IsImage = true };
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await imageAd.File.CopyToAsync(fileStream);
                    }
                    _db.Cameras.Add(camera);
                    _db.SaveChanges();
                    return Ok(camera);
                }
                else
                {
                    // in case of empty file
                    return NotFound();
                }
            }

            catch (Exception ex)
            {
                throw new Exception("File Upload Failed", ex);
            }

        }

        [HttpGet("images/{filename}")]
        public IActionResult GetImage(String filename)
        {
            // get the full path of the pdf storage folder
            string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, folderName));
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


        [HttpGet("active")]
        public IEnumerable<Camera> GetActiveCameras()
        {
            return _db.Cameras.Where(camera => camera.IsActive);
        }

        [HttpPut("{cameraId:int}")]
        public ActionResult<Camera> ToggleCamera(int cameraId)
        {
            var camera = _db.Cameras.FirstOrDefault(camera => camera.Id == cameraId);
            if (camera != null && camera != default(Camera))
            {
                camera.IsActive = !camera.IsActive;
                _db.Update(camera);
                _db.SaveChanges();
                return Ok(camera);
            }
            return NotFound();
        }

        [HttpDelete("{cameraId:int}")]
        public ActionResult<Camera> DeleteCamera(int cameraId)
        {
            var camera = _db.Cameras.FirstOrDefault(camera => camera.Id == cameraId);
            if (camera != null && camera != default(Camera))
            {
                if (camera.IsImage)
                {
                    // get the full path of the image storage folder
                    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, folderName));
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
                    string filePath = Path.Combine(path, camera.Link.Split("/").Last());
                    // construct a file Info object to verify if the file exists
                    FileInfo file = new FileInfo(filePath);
                    if (file.Exists)
                    {
                        // delete the file: this require high privildge to run it 
                        file.Delete();
                    }
                }
                _db.Cameras.Remove(camera);
                _db.SaveChanges();
                return Ok(camera);
            }
            else
            {
                return NotFound();
            }
        }


    }
}