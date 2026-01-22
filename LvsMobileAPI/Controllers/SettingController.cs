using Common.ApiModels;
using LvsMobileAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AllowAnonymousAttribute = LvsMobileAPI.Authorization.AllowAnonymousAttribute;

namespace LvsMobileAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class SettingController : Controller
    {
        public ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        //-------------------------------------------------- GET  -------------------------------------------------------------------

        [AllowAnonymous]
        [HttpGet("GET_Printerlist")]
        public IActionResult GET_Printerlist()
        {
            ResponseSettings resSetting = new ResponseSettings();
            resSetting.Success = false;
            var retList = _settingService.GET_Printers();
            if (retList.Count > 0)
            {
                resSetting.Success = true;
                resSetting.ListPrinters = retList;
                return Ok(resSetting);
            }
            else
            {
                resSetting.Success = false;
                resSetting.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resSetting);
            }
        }



    }
}
