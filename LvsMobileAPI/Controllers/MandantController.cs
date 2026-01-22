using Common.ApiModels;
using Common.Models;
using LvsMobileAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AllowAnonymousAttribute = LvsMobileAPI.Authorization.AllowAnonymousAttribute;

namespace LvsMobileAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class MandantController : Controller
    {
        public IMandantService _mandantService;
        public MandantController(IMandantService mandantService)
        {
            _mandantService = mandantService;
        }

        //-------------------------------------------------- GET  -------------------------------------------------------------------

        [AllowAnonymous]
        [HttpGet("GET_Mandant/{MandantId}")]
        public IActionResult GET_Mandant(int MandantId)
        {
            ResponseMandant resMandant = new ResponseMandant();
            resMandant.Success = false;

            if (MandantId > 0)
            {
                var man = _mandantService.GET_Mandant(MandantId);

                if ((man is Mandanten) && (man.Id == MandantId))
                {
                    resMandant.Success = true;
                    resMandant.Mandant = man.Copy();
                    return Ok(resMandant);
                }
                else
                {
                    resMandant.Success = false;
                    resMandant.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                    return NotFound(resMandant);
                }
            }
            else
            {
                resMandant.Success = false;
                resMandant.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resMandant);
            }
        }

        [AllowAnonymous]
        [HttpGet("GET_Mandantenlist")]
        public IActionResult GET_Mandantenlist(ResponseMandant resMandant)
        {
            resMandant.Success = false;
            resMandant.Error = string.Empty;
            resMandant.Info = string.Empty;

            var response = _mandantService.GET_MandantenList();
            if (response != null)
            {
                resMandant.Success = true;
                resMandant.Mandantlist = new List<Mandanten>(response);
                resMandant.Info = "Die Addressliste konnte ermittelt werden!";
                return Ok(resMandant);
            }
            else
            {
                resMandant.Success = false;
                resMandant.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resMandant);
            }
        }


    }
}
