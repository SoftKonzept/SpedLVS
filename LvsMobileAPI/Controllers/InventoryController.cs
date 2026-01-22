using Common.ApiModels;
using Common.Models;
using LvsMobileAPI.Authorization;
using LvsMobileAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LvsMobileAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        private IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        //-------------------------------------------- Inventory --------------------------------------------
        [AllowAnonymous]
        [HttpGet("GetInventory/{InventoryId}")]
        public IActionResult GetInventory(int InventoryId)
        {
            ResponseInventory resInv = new ResponseInventory();
            var inv = _inventoryService.GetInventory(InventoryId);
            if ((inv is Inventories) && (inv.Id > 0))
            {
                resInv.Success = true;
                resInv.Inventory = inv.Copy();
                return Ok(resInv);
            }
            else
            {
                resInv.Success = false;
                resInv.Inventory = null;
                resInv.Error = "Es konnten keine Datensätze zur Inventur gefunden werden!";
                return NotFound(resInv);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetInventoryList")]
        public IActionResult GetInventoryList()
        {
            ResponseInventory resInv = new ResponseInventory();
            List<Inventories> invList = _inventoryService.GetInventoryList();

            if (invList.Count > 0)
            {
                resInv.Success = true;
                resInv.ListInventories.AddRange(invList);
                return Ok(resInv);
            }
            else
            {
                resInv.Success = false;
                resInv.Inventory = null;
                resInv.Error = "Es konnten keine Datensätze gefunden werden!";
                return NotFound(resInv);
            }
        }

        //----------------------------------------- InventoryArticle --------------------------------------------
        [AllowAnonymous]
        [HttpGet("GetInventoryArticle/{InventoryArticleId}")]
        public IActionResult GetInventoryArticle(int InventoryArticleId)
        {
            ResponseInventoryArticle resInvArt = new ResponseInventoryArticle();
            var invArticle = _inventoryService.GetInventoryArticle(InventoryArticleId);
            if ((invArticle is InventoryArticles) && (invArticle.Id > 0))
            {
                resInvArt.Success = true;
                resInvArt.InventoryArticle = invArticle;
                resInvArt.Error = String.Empty;
                return Ok(resInvArt);
            }
            else
            {
                resInvArt.Success = false;
                resInvArt.Info = String.Empty;
                resInvArt.Error = "Es konnten keine Datensätze gefunden werden!";
                return NotFound(resInvArt);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetInventoryArticleList/{InventoryId}")]
        public IActionResult GetInventoryArticleList(int InventoryId)
        {
            ResponseInventoryArticle resInvArt = new ResponseInventoryArticle();
            List<InventoryArticles> ListInvArticle = _inventoryService.GetInventoryArticleList(InventoryId);

            if (ListInvArticle.Count > 0)
            {
                resInvArt.Success = true;
                resInvArt.ListInventoryArticle.AddRange(ListInvArticle);
                resInvArt.Error = String.Empty;
                return Ok(resInvArt);
            }
            else
            {
                resInvArt.Success = false;
                resInvArt.Info = String.Empty;
                resInvArt.ListInventoryArticle = new List<InventoryArticles>();
                resInvArt.Error = "Es konnten keine Datensätze gefunden werden!";
                return NotFound(resInvArt);
            }
        }

        [AllowAnonymous]
        [HttpPost("POST_Update_InventoryArticle_Status")]
        //public IActionResult POST_Update_InventoryArticle_Status([FromBody] InventoryArticles inventoryArticle)
        public IActionResult POST_Update_InventoryArticle_Status([FromBody] ResponseInventoryArticle myResonsdeInventoryArticle)
        {
            var bReturn = _inventoryService.POST_Update_InventoryArticle_Status(myResonsdeInventoryArticle);
            myResonsdeInventoryArticle.Success = bReturn;
            myResonsdeInventoryArticle.ListInventoryArticle = new List<InventoryArticles>();
            myResonsdeInventoryArticle.Error = string.Empty;
            myResonsdeInventoryArticle.Info = string.Empty;

            if (bReturn)
            {
                InventoryArticles updatetInventoryArticle = _inventoryService.GetInventoryArticle(myResonsdeInventoryArticle.InventoryArticle.Id);
                myResonsdeInventoryArticle.InventoryArticle = updatetInventoryArticle.Copy();
                myResonsdeInventoryArticle.Info = "Statusupdate erfolgreich durchgeführt";
                return Ok(myResonsdeInventoryArticle);
            }
            else
            {
                myResonsdeInventoryArticle.Error = "Update konnte nicht durchgeführt werden";
                return NotFound(myResonsdeInventoryArticle);
            }
        }




    }
}
