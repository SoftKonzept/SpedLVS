using Common.ApiModels;
using LvsMobileAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AllowAnonymousAttribute = LvsMobileAPI.Authorization.AllowAnonymousAttribute;

namespace LvsMobileAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class DamageController : Controller
    {
        public IDamageService _damageService;
        public DamageController(IDamageService damageService)
        {
            _damageService = damageService;
        }

        //-------------------------------------------------- GET  -------------------------------------------------------------------

        [AllowAnonymous]
        [HttpGet("GET_Damage/{Id}")]
        public IActionResult GET_Ausgang(int Id)
        {
            ResponseDamage response = new ResponseDamage();
            response.UserId = 0;

            if (Id > 0)
            {
                var result = _damageService.GET_Damage(Id);

                response = result;
                if (response.Success)
                {
                    response.Success = true;
                    response.Info = "Der Schaden konnte ermittelt werden!";
                    return Ok(response);
                }
                else
                {
                    response.Success = false;
                    response.Error = "\"Achtung - Es konnte keine Daten ermittelt werden!";
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
        [HttpGet("GET_Damage_List")]
        public IActionResult GET_Damage_List()
        {
            ResponseDamage response = new ResponseDamage();
            var result = _damageService.GET_Damage_List();
            response = result;
            if (response.Success)
            {
                response.Success = true;
                response.Info = "Liste konnte ermittelt werden!";
                return Ok(response);
            }
            else
            {
                response.Success = false;
                response.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(response);
            }
        }

        [AllowAnonymous]
        [HttpGet("GET_Damage_DamageArticleList/{ArticleId}/{UserId}")]
        public IActionResult GET_Damage_DamageArticleList(int ArticleId, int UserId)
        {
            ResponseDamage response = new ResponseDamage();
            response.UserId = UserId;

            var result = _damageService.GET_Damage_DamageArticleList(ArticleId, UserId);
            response = result;
            if (response.Success)
            {
                response.Success = true;
                response.Info = "Liste konnte ermittelt werden!";
                return Ok(response);
            }
            else
            {
                response.Success = false;
                response.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(response);
            }
        }

        [AllowAnonymous]
        [HttpPost("POST_Damage_Update")]
        public IActionResult POST_Damage_Update([FromBody] ResponseDamage response)
        {
            var res = _damageService.POST_Damage_Update(response);
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

        [AllowAnonymous]
        [HttpPost("POST_Damage_AddDamageArticleAssignment")]
        public IActionResult POST_Damage_AddDamageArticleAssignment([FromBody] ResponseDamage response)
        {
            var res = _damageService.POST_Damage_AddDamageArticleAssignment(response);
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

        [AllowAnonymous]
        [HttpDelete("DELETE_Damage_DamageArticleAssignmentItem/{DamageArticleAssignemtnId}/{UserId}")]
        public IActionResult DELETE_Damage_DamageArticleAssignmentItem(int DamageArticleAssignemtnId, int UserId)
        {
            ResponseDamage response = new ResponseDamage();
            response.Success = false;
            response.Article = null;
            response.UserId = UserId;


            if (DamageArticleAssignemtnId > 0)
            {
                var result = _damageService.DELETE_Damage_DamageArticleAssignment_DeleteItem(DamageArticleAssignemtnId, UserId);

                response = result;
                if (response.Success)
                {
                    response.Success = true;
                    response.Info = "Die Schadenszuweisung wurde gelöscht!";
                    return Ok(response);
                }
                else
                {
                    response.Success = false;
                    response.Error = "\"Achtung - Die Schadenszuweisung konnte nicht gelöscht werden!";
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

    }
}
