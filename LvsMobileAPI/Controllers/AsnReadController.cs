using Common.ApiModels;
using LvsMobileAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AllowAnonymousAttribute = LvsMobileAPI.Authorization.AllowAnonymousAttribute;

namespace LvsMobileAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class AsnReadController : Controller
    {
        public IAsnReadService _asnReadService;
        public AsnReadController(IAsnReadService asnReadService)
        {
            _asnReadService = asnReadService;
        }

        //-------------------------------------------------- GET  -------------------------------------------------------------------

        [AllowAnonymous]
        [HttpGet("GET_ASN_GetLfsArticleListFromAsn")]
        public IActionResult GET_ASN_GetLfsArticleListFromAsn()
        {
            ResponseASN response = new ResponseASN();
            response.UserId = 0;
            var result = _asnReadService.GET_ASN_GetLfsArticleListFromAsn();

            response = result;
            if (response.Success)
            {
                response.Success = true;

                response.Info = "Die Lfs- und Artikeldaten konnten ermittelt werden!";
                return Ok(response);
            }
            else
            {
                response.Success = false;
                response.Error = "Achtung - Es konnten keine Daten ermittelt werden!";
                return NotFound(response);
            }
        }


        [AllowAnonymous]
        [HttpGet("GET_ASN_GetLfsArticleByProductionnumber/{Poductionnumber}/{UserId}")]
        public IActionResult GET_ASN_GetLfsArticleByProductionnumber(string Poductionnumber, int UserId)
        {
            ResponseASN response = new ResponseASN();
            response.UserId = UserId;

            if (Poductionnumber.Length > 0)
            {
                var result = _asnReadService.GET_ASN_GetLfsArticleByProductionnumber(Poductionnumber, UserId);

                response = result;
                if (response.Success)
                {
                    response.Success = true;
                    response.Info = result.Info;
                    response.Info += "Die Lfs- und Artikeldaten konnten ermittelt werden!";
                    return Ok(response);
                }
                else
                {
                    response.Success = false;
                    response.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                    return NotFound(response);
                }
            }
            else
            {
                response.Success = false;
                response.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(response);
            }
        }

        [AllowAnonymous]
        [HttpPost("POST_ASN_CreateStoreIn")]
        public IActionResult POST_ASN_CreateStoreIn(ResponseASN responseASN)
        {
            var res = _asnReadService.POST_ASN_CreateStoreInByAsnId(responseASN);
            responseASN = res.Copy();
            if (responseASN.Success)
            {
                return Ok(responseASN);
            }
            else
            {
                return NotFound(responseASN);
            }
        }

    }
}
