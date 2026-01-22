using Common.ApiModels;
using LvsMobileAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AllowAnonymousAttribute = LvsMobileAPI.Authorization.AllowAnonymousAttribute;

namespace LvsMobileAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class AusgangController : Controller
    {
        public IAusgangService _ausgangService;
        public AusgangController(IAusgangService ausgangService)
        {
            _ausgangService = ausgangService;
        }

        //-------------------------------------------------- GET  -------------------------------------------------------------------

        [AllowAnonymous]
        [HttpGet("GET_Ausgang/{Id}")]
        public IActionResult GET_Ausgang(int Id)
        {
            ResponseAusgang resAusgang = new ResponseAusgang();
            resAusgang.UserId = 0;

            if (Id > 0)
            {
                var result = _ausgangService.GET_Ausgang(Id);

                resAusgang = result;
                if (resAusgang.Success)
                {
                    resAusgang.Success = true;
                    resAusgang.Info = "Ausgang konnte ermittelt werden!";
                    return Ok(resAusgang);
                }
                else
                {
                    resAusgang.Success = false;
                    resAusgang.Error = "\"Achtung - Es konnte keine Daten ermittelt werden!";
                    return NotFound(resAusgang);
                }
            }
            else
            {
                resAusgang.Success = false;
                resAusgang.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resAusgang);
            }
        }


        [AllowAnonymous]
        [HttpGet("GET_AusgangByLvsNr/{LvsNr}")]
        public IActionResult GET_AusgangByLvsN(int LvsNr)
        {
            ResponseAusgang resAusgang = new ResponseAusgang();
            resAusgang.UserId = 0;

            if (LvsNr > 0)
            {
                var result = _ausgangService.GET_AusgangByLvsNr(LvsNr);

                resAusgang = result;
                if (resAusgang.Success)
                {
                    resAusgang.Success = true;
                    resAusgang.Info = "Ausgang konnte ermittelt werden!";
                    return Ok(resAusgang);
                }
                else
                {
                    resAusgang.Success = false;
                    resAusgang.Error = "\"Achtung - Es konnte keine Daten ermittelt werden!";
                    return NotFound(resAusgang);
                }
            }
            else
            {
                resAusgang.Success = false;
                resAusgang.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resAusgang);
            }
        }

        [AllowAnonymous]
        [HttpGet("GET_AusgangList_Open")]
        public IActionResult GET_AusgangList_Open()
        {
            ResponseAusgang resAusgang = new ResponseAusgang();
            var result = _ausgangService.GET_AusgangList_Open();
            resAusgang = result;
            if (resAusgang.Success)
            {
                resAusgang.Success = true;
                resAusgang.Info = "Liste konnte ermittelt werden!";
                return Ok(resAusgang);
            }
            else
            {
                resAusgang.Success = false;
                resAusgang.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resAusgang);
            }
        }

        [AllowAnonymous]
        [HttpPost("POST_Ausgang_Update_WizStoreOut")]
        public IActionResult POST_Ausgang_Update_WizStoreOut([FromBody] ResponseAusgang resStoreOut)
        {
            var response = _ausgangService.POST_Ausgang_UpdateWizStoreOut(resStoreOut);
            resStoreOut = response.Copy();
            if (resStoreOut.Success)
            {
                return Ok(resStoreOut);
            }
            else
            {
                return NotFound(resStoreOut);
            }
        }

        [AllowAnonymous]
        [HttpPost("POST_Ausgang_Test")]
        public IActionResult POST_Ausgang_Test()
        {
            var response = _ausgangService.POST_Ausgang_Test();
            return Ok(response);
        }


    }
}
