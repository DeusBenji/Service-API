using Microsoft.AspNetCore.Mvc;
using ServiceData.ModelLayer;

namespace Service_Api.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : Controller
    {
        // string MainPath = "C:\\Users\\Benqf\\source\\repos\\Service-Api\\Service-Api\\Uploads";
        string MainPath = "C:\\Users\\MagnusMølgaard\\source\\repos\\BenBakRas\\Service-Api\\Service-Api\\Uploads";

        [HttpPost]
        [Route("UploadFile")]
        public Response UploadFile([FromForm] Image image)
        {
            Response response = new Response();
            try
            {
                string path = Path.Combine(MainPath, image.FileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    image.file.CopyTo(stream);
                }
                response.StatusCode = 200;
                response.ErrorMessage = "Image created successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.ErrorMessage = "Something went wrong: " + ex.Message;
            }
            return response;
        }

        [HttpGet]
        [Route("GetImage/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            try
            {
                string path = Path.Combine(MainPath, imageName);

                if (System.IO.File.Exists(path))
                {
                    var fileStream = System.IO.File.OpenRead(path);
                    return File(fileStream, "image/jpeg"); // Change the content type as needed
                }
                else
                {
                    return NotFound("Image not found");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteImage/{imageName}")]
        public IActionResult DeleteImage(string imageName)
        {
            try
            {
                string path = Path.Combine(MainPath, imageName);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return Ok("Image deleted successfully");
                }
                else
                {
                    return NotFound("Image not found");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                return StatusCode(500, "Something went wrong: " + ex.Message);
            }
        }
    }
}
