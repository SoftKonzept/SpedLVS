using Common.ApiModels;
using LvsMobileAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AllowAnonymousAttribute = LvsMobileAPI.Authorization.AllowAnonymousAttribute;

namespace LvsMobileAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class EingangController : Controller
    {
        public IEingangService _eingangService;
        public EingangController(IEingangService eingangService)
        {
            _eingangService = eingangService;
        }

        //-------------------------------------------------- GET  -------------------------------------------------------------------

        [AllowAnonymous]
        [HttpGet("GET_Eingang/{Id}")]
        public IActionResult GET_Eingang(int Id)
        {
            ResponseEingang resEingang = new ResponseEingang();
            resEingang.UserId = 0;

            if (Id > 0)
            {
                var result = _eingangService.GET_Eingang(Id);

                resEingang = result;
                if (resEingang.Success)
                {
                    resEingang.Success = true;
                    resEingang.Info = "Eingang konnte ermittelt werden!";
                    return Ok(resEingang);
                }
                else
                {
                    resEingang.Success = false;
                    resEingang.Error = "\"Achtung - Es konnte keine Daten ermittelt werden!";
                    return NotFound(resEingang);
                }
            }
            else
            {
                resEingang.Success = false;
                resEingang.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resEingang);
            }
        }

        [AllowAnonymous]
        [HttpGet("GET_EingangList_Open")]
        public IActionResult GET_EingangList_Open()
        {
            ResponseEingang resEingang = new ResponseEingang();
            var result = _eingangService.GET_EingangList_Open();
            resEingang = result;
            if (resEingang.Success)
            {
                resEingang.Success = true;
                resEingang.Info = "Liste konnte ermittelt werden!";
                return Ok(resEingang);
            }
            else
            {
                resEingang.Success = false;
                resEingang.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resEingang);
            }
        }

        [AllowAnonymous]
        [HttpPost("POST_Eingang_Update_WizStoreIn")]
        public IActionResult POST_Eingang_Update_WizStoreIn([FromBody] ResponseEingang resEingang)
        {
            var response = _eingangService.POST_Eingang_UpdateWizStoreIn(resEingang);
            resEingang = response.Copy();
            if (resEingang.Success)
            {
                return Ok(resEingang);
            }
            else
            {
                return NotFound(resEingang);
            }
        }

        [AllowAnonymous]
        [HttpPost("POST_Eingang_Add")]
        public IActionResult POST_Eingang_Add([FromBody] ResponseEingang resEingang)
        {
            var response = _eingangService.POST_Eingang_Add(resEingang);
            resEingang = response.Copy();
            if (resEingang.Success)
            {
                return Ok(resEingang);
            }
            else
            {
                return NotFound(resEingang);
            }
        }




    }
}
