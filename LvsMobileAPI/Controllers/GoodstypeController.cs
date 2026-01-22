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
    public class GoodstypeController : Controller
    {
        public IGoodtypeService _goodstypeService;
        public GoodstypeController(IGoodtypeService goodstypeService)
        {
            _goodstypeService = goodstypeService;
        }

        //-------------------------------------------------- GET  -------------------------------------------------------------------

        [AllowAnonymous]
        [HttpGet("GET_GoodsType/{GoodstypeId}")]
        public IActionResult GET_GoodsType(int GoodstypeId)
        {
            ResponseGoodstype resGoodstype = new ResponseGoodstype();
            resGoodstype.Success = false;

            if (GoodstypeId > 0)
            {
                var goodstype = _goodstypeService.GET_Goodstype(GoodstypeId);
                if ((goodstype is ResponseGoodstype) && (goodstype.Id > 0))
                {
                    resGoodstype.Success = true;
                    resGoodstype.Goodstype = goodstype.Copy();
                    return Ok(resGoodstype);
                }
                else
                {
                    resGoodstype.Success = false;
                    resGoodstype.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                    return NotFound(resGoodstype);
                }
            }
            else
            {
                resGoodstype.Success = false;
                resGoodstype.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resGoodstype);
            }
        }

        [AllowAnonymous]
        [HttpGet("GET_GoodstypelistByWorkspace/{WorkspaceId}")]
        public IActionResult GET_GoodstypelistByWorkspace(int WorkspaceId)
        {
            ResponseGoodstype resGoodstype = new ResponseGoodstype();
            resGoodstype.Success = false;
            resGoodstype.Error = string.Empty;
            resGoodstype.Info = string.Empty;

            if (WorkspaceId > 0)
            {
                var response = _goodstypeService.GET_GoodstypeListByWorkspace(WorkspaceId);
                if ((response != null) && (response.Count > 0))
                {
                    resGoodstype.Success = true;
                    resGoodstype.ListGoodstypes = new List<Goodstypes>(response);
                    resGoodstype.Info = "Die Addressliste konnte ermittelt werden!";
                    return Ok(resGoodstype);
                }
                else
                {
                    resGoodstype.Success = false;
                    resGoodstype.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                    return NotFound(resGoodstype);
                }
            }
            else
            {
                resGoodstype.Success = false;
                resGoodstype.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resGoodstype);
            }
        }

        [AllowAnonymous]
        [HttpGet("GET_GoodstypelistByWorkspaceAndAddress/{WorkspaceId}/{AddressId}")]
        public IActionResult GET_GoodstypelistByWorkspaceAndAddress(int WorkspaceId, int AddressId)
        {
            ResponseGoodstype resGoodstype = new ResponseGoodstype();
            resGoodstype.Success = false;
            resGoodstype.Error = string.Empty;
            resGoodstype.Info = string.Empty;

            if (WorkspaceId > 0)
            {
                var response = _goodstypeService.GET_GoodstypeListByWorkspaceAndAddress(WorkspaceId, AddressId);
                if ((response != null) && (response.Count > 0))
                {
                    resGoodstype.Success = true;
                    resGoodstype.ListGoodstypes = new List<Goodstypes>(response);
                    resGoodstype.Info = "Die Addressliste konnte ermittelt werden!";
                    return Ok(resGoodstype);
                }
                else
                {
                    resGoodstype.Success = false;
                    resGoodstype.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                    return NotFound(resGoodstype);
                }
            }
            else
            {
                resGoodstype.Success = false;
                resGoodstype.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resGoodstype);
            }
        }

        [AllowAnonymous]
        [HttpGet("GET_GoodstypeByWorkspaceAndAddressAndWerksnummer/{WorkspaceId}/{AddressId}/{Werksnummer}")]
        public IActionResult GET_GoodstypeByWorkspaceAndAddressAndWerksnummer(int WorkspaceId, int AddressId, string Werksnummer)
        {
            ResponseGoodstype resGoodstype = new ResponseGoodstype();
            resGoodstype.Success = false;
            resGoodstype.Error = string.Empty;
            resGoodstype.Info = string.Empty;

            if (WorkspaceId > 0)
            {
                var response = _goodstypeService.GET_GoodstypeByWorkspaceAndAddressAndWerksnummer(WorkspaceId, AddressId, Werksnummer);
                if (response is Goodstypes)
                {
                    resGoodstype.Goodstype = response.Copy();
                    resGoodstype.Info = "Die Güterart konnte ermittelt werden!";
                    return Ok(resGoodstype);
                }
                else
                {
                    resGoodstype.Success = false;
                    resGoodstype.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                    return NotFound(resGoodstype);
                }
            }
            else
            {
                resGoodstype.Success = false;
                resGoodstype.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resGoodstype);
            }
        }
    }
}
