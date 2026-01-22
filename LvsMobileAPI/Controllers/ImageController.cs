using Common.ApiModels;
using LvsMobileAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AllowAnonymousAttribute = LvsMobileAPI.Authorization.AllowAnonymousAttribute;

namespace LvsMobileAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        public IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        //-------------------------------------------------- GET  -------------------------------------------------------------------
        //public IActionResult POST_Image_Add()
        [AllowAnonymous]
        [HttpPost("POST_Image_Add")]
        public IActionResult POST_Image_Add([FromBody] ResponseImage response)
        {
            //response = new ResponseImage();
            //response.Image = new Images();
            //response.Image.AuftragTableID = 1;
            //response.Image.LEingangTableID = 2;
            //response.Image.LAusgangTableID = 3;
            //response.Image.PicNum = 1;
            //response.Image.Pfad = "a/b/c";
            //response.Image.ScanFilename = "test.bmp";
            //response.Image.ImageArt = "Bilder";
            //response.Image.AuftragPosTableID = 4;
            //Image tmpImage = Image.FromFile(@"C:\LVS\test.bmp");
            //response.Image.DocImage = helper_Image.ImageToByteArray(tmpImage);
            //response.Image.TableName = "Artikel";
            //response.Image.TableId = 12345;
            //response.Image.Thumbnail = helper_Image.ImageToByteArray(tmpImage);
            //response.Image.IsForSPLMessage = true;
            //response.Image.WorkspaceId = 99;


            var res = _imageService.POST_Image_Add(response);
            response = res.Copy();
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }
    }
}
