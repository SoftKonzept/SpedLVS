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
    public class ArticleController : Controller
    {
        public IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        //-------------------------------------------------- GET  -------------------------------------------------------------------

        [AllowAnonymous]
        [HttpGet("GET_Article/{ArticleId}/{UserId}")]
        public IActionResult GET_Article(int ArticleId, int UserId)
        {
            if (ArticleId > 0)
            {
                var art = _articleService.GET_Article(ArticleId, UserId);

                if ((art is Articles) && (art.Id == ArticleId))
                {
                    return Ok(art.Copy());
                }
                else
                {
                    return NotFound("Achtung - Es konnte keine Daten ermittelt werden!");
                }
            }
            else
            {
                return Problem("Achtung - Die Suchparameter sind fehlerhaft!");
            }
        }

        /// <summary>
        ///             GET - ExistArticleValue
        ///             GetExistArticleValue/{SearchValueString}/{enumArticleDatafieldId}
        /// </summary>
        /// <param name="SearchValueString"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("GET_Article_ExistArticleValue/{SearchValueString}/{enumArticleDatafieldId}")]
        public IActionResult GET_Article_ExistArticleValue(string SearchValueString, int enumArticleDatafieldId)
        {
            if (!SearchValueString.Equals(string.Empty))
            {
                var art = _articleService.GET_Article_ExistArticleValue(SearchValueString, enumArticleDatafieldId);
                if ((art is Articles) && (art.Id > 0))
                {
                    return Ok(art.Copy());
                }
                else
                {
                    return NotFound("Achtung - Es konnte keine Daten ermittelt werden!");
                }
            }
            else
            {
                return Problem("Achtung - Die Suchparameter sind fehlerhaft!");
            }
        }

        /// <summary>
        ///             GET - ExistArticleValue
        ///             GET_Article_ExistArticleLvsForStoreLocationChange/{LvsSearchString}
        /// </summary>
        /// <param name="SearchValueString"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("GET_Article_ExistArticleLvsForStoreLocationChange/{LvsSearchString}")]
        public IActionResult GET_Article_ExistArticleLvsForStoreLocationChange(string LvsSearchString)
        {
            if (!LvsSearchString.Equals(string.Empty))
            {
                var art = _articleService.GET_Article_ExistArticleLvsForStoreLocationChange(LvsSearchString);
                if ((art is Articles) && (art.Id > 0))
                {
                    return Ok(art.Copy());
                }
                else
                {
                    return NotFound("Achtung - Es konnte keine Daten ermittelt werden!");
                }
            }
            else
            {
                return Problem("Achtung - Die Suchparameter sind fehlerhaft!");
            }
        }

        [AllowAnonymous]
        [HttpGet("GET_Article_SearchArticle/{LvsNoString}/{ProductionnumberString}")]
        public IActionResult GET_Article_SearchArticle(string LvsNoString, string ProductionnumberString)
        {
            if (!LvsNoString.Equals(string.Empty))
            {
                var art = _articleService.GET_Article_SearchArticle(LvsNoString, ProductionnumberString);
                if ((art is Articles) && (art.Id > 0))
                {
                    return Ok(art.Copy());
                }
                else
                {
                    return NotFound("Achtung - Es konnte keine Daten ermittelt werden!");
                }
            }
            else
            {
                return Problem("Achtung - Die Suchparameter sind fehlerhaft!");
            }
        }

        [AllowAnonymous]
        [HttpGet("GET_Article_SearchArticleForStoredLocationChange/{LvsNoString}")]
        public IActionResult GET_Article_SearchArticleForStoredLocationChange(string LvsNoString)
        {
            if (!LvsNoString.Equals(string.Empty))
            {
                var art = _articleService.GET_Article_SearchArticleForStoredLocationChange(LvsNoString);
                if ((art is Articles) && (art.Id > 0))
                {
                    return Ok(art.Copy());
                }
                else
                {
                    return NotFound("Achtung - Es konnte keine Daten ermittelt werden!");
                }
            }
            else
            {
                return Problem("Achtung - Die Suchparameter sind fehlerhaft!");
            }
        }

        [AllowAnonymous]
        [HttpGet("GET_Article_GetArticleInStoreInByProductionNo/{ProductionnumberString}")]
        public IActionResult GET_Article_GET_Article_GetArticleInStoreInByProductionNo(string ProductionnumberString)
        {
            if (!ProductionnumberString.Equals(string.Empty))
            {
                var lst = _articleService.GET_Article_SearchArticleInStoreINByProductionNo(ProductionnumberString);

                if (lst.Count > 0)
                {
                    return Ok(lst);
                }
                else
                {
                    return NotFound("Achtung - Es konnte keine Daten ermittelt werden!");
                }
            }
            else
            {
                return Problem("Achtung - Die Suchparameter sind fehlerhaft!");
            }
        }

        [AllowAnonymous]
        [HttpPost("POST_Article_AddByScanner")]
        public IActionResult POST_Article_AddByScanner([FromBody] ResponseArticle resArticle)
        {
            var response = _articleService.POST_Article_AddByScanner(resArticle);
            resArticle = response.Copy();
            if (resArticle.Success)
            {
                return Ok(resArticle);
            }
            else
            {
                return NotFound(resArticle);
            }
        }

        [AllowAnonymous]
        [HttpPost("POST_Article_Update_StoreLocation")]
        public IActionResult POST_Article_Update_StoreLocation([FromBody] ResponseStoreLocationChange responseStoreLocChange)
        {
            var result = _articleService.POST_Article_Update_StoreLocation(responseStoreLocChange);
            if (result.SuccessStoreLocationChange)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [AllowAnonymous]
        [HttpPost("POST_Article_Update_ScanValue")]
        public IActionResult POST_Article_Update_ScanValue([FromBody] ResponseArticle resArticle)
        {
            var response = _articleService.POST_Article_Update_ScanValue(resArticle);
            resArticle = response.Copy();
            if (resArticle.Success)
            {
                return Ok(resArticle);
            }
            else
            {
                return NotFound(resArticle);
            }
        }

        [AllowAnonymous]
        [HttpPost("POST_Article_Update_Checked")]
        public IActionResult POST_Article_Update_Checked([FromBody] ResponseArticle resArticle)
        {
            var response = _articleService.POST_Article_Update_Checked(resArticle);
            resArticle = response.Copy();
            if (resArticle.Success)
            {
                return Ok(resArticle);
            }
            else
            {
                return NotFound(resArticle);
            }
        }

        [AllowAnonymous]
        [HttpPost("POST_Article_Update_ScanIdentification")]
        public IActionResult POST_Article_Update_ScanIdentification([FromBody] ResponseArticle resArticle)
        {
            var response = _articleService.POST_Article_Update_ScanIdentification(resArticle);
            resArticle = response.Copy();
            if (resArticle.Success)
            {
                return Ok(resArticle);
            }
            else
            {
                return NotFound(resArticle);
            }
        }


        [AllowAnonymous]
        [HttpPost("POST_Article_Update_ManualEdit")]
        public IActionResult POST_Article_Update_ManualEdit([FromBody] ResponseArticle resArticle)
        {
            var response = _articleService.POST_Article_Update_ManualEdit(resArticle);
            resArticle = response.Copy();
            if (resArticle.Success)
            {
                return Ok(resArticle);
            }
            else
            {
                return NotFound(resArticle);
            }
        }






    }
}
