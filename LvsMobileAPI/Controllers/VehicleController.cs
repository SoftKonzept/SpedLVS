using Common.ApiModels;
using LvsMobileAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AllowAnonymousAttribute = LvsMobileAPI.Authorization.AllowAnonymousAttribute;

namespace LvsMobileAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        public IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        //-------------------------------------------------- GET  -------------------------------------------------------------------


        [AllowAnonymous]
        [HttpGet("GET_VehicleList")]
        public IActionResult GET_VehicleList()
        {
            ResponseVehicle resVehicle = new ResponseVehicle();
            resVehicle.Success = false;

            var result = _vehicleService.GET_VehicleList();

            resVehicle = result;
            if (resVehicle.Success)
            {
                resVehicle.Success = true;
                resVehicle.Info += "Liste konnte ermittelt werden!";
                return Ok(resVehicle);
            }
            else
            {
                resVehicle.Success = false;
                resVehicle.Error += "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resVehicle);
            }
        }





        //[AllowAnonymous]
        //[HttpGet("GET_Call/{CallId}/{UserId}")]
        //public IActionResult GET_Call(int CallId, int UserId)
        //{
        //    ResponseCall resCall = new ResponseCall();
        //    resCall.Success = false;

        //    if (CallId > 0)
        //    {
        //        var response = _callService.GetCall(CallId, UserId);
        //        resCall = response;
        //        if (resCall.Success)
        //        {
        //            return Ok(resCall);
        //        }
        //        else
        //        {
        //            resCall.Success = false;
        //            resCall.Error += "Achtung - Es konnte keine Daten ermittelt werden!";
        //            return NotFound(resCall);
        //        }
        //    }
        //    else
        //    {
        //        resCall.Success = false;
        //        resCall.Error += "Achtung - Es konnte keine Daten ermittelt werden!";
        //        return NotFound(resCall);
        //    }
        //}

        //[AllowAnonymous]
        //[HttpGet("GET_CallByLVSNr/{LvsNr}/{UserId}")]
        //public IActionResult GET_CallByLVSNr(int LvsNr, int UserId)
        //{
        //    ResponseCall resCall = new ResponseCall();
        //    resCall.Success = false;

        //    if (LvsNr > 0)
        //    {
        //        var response = _callService.GetCallByLvsNr(LvsNr, UserId);
        //        resCall = response;
        //        if (resCall.Success)
        //        {
        //            return Ok(resCall);
        //        }
        //        else
        //        {
        //            resCall.Success = false;
        //            resCall.Error += "Achtung - Es konnte keine Daten ermittelt werden!";
        //            return NotFound(resCall);
        //        }
        //    }
        //    else
        //    {
        //        resCall.Success = false;
        //        resCall.Error += "Achtung - Es konnte keine Daten ermittelt werden!";
        //        return NotFound(resCall);
        //    }
        //}





        //[AllowAnonymous]
        //[HttpPost("POST_Call_Update_WizStoreOut")]
        //public IActionResult POST_Call_Update_WizStoreOut([FromBody] ResponseCall resCall)
        //{
        //    resCall.Success = false;
        //    var result = _callService.POST_Call_UpdateWizStoreOut(resCall);
        //    resCall = result;
        //    if (resCall.Success)
        //    {
        //        resCall.Success = true;
        //        resCall.Info += "Liste konnte ermittelt werden!";
        //        return Ok(resCall);
        //    }
        //    else
        //    {
        //        resCall.Success = false;
        //        resCall.Error += "Achtung - Es konnte keine Daten ermittelt werden!";
        //        return NotFound(resCall);
        //    }
        //}
        //[AllowAnonymous]
        //[HttpPost("POST_Call_CreateStoreOut")]
        //public IActionResult POST_Call_CreateStoreOut([FromBody] ResponseCall respCall)
        //{
        //    var result = _callService.POST_Call_CreateStoreOut(respCall);
        //    respCall = result;
        //    if (respCall.Success)
        //    {
        //        //respCall.Success = true;
        //        //respCall.Info += "Achtung - Die Ausgänge zu den Abrufen wurden erstellt!";
        //        return Ok(respCall);
        //    }
        //    else
        //    {
        //        //respCall.Success = false;
        //        respCall.Error += "Es ist ein Fehler aufgetreten. Es konnten keine Ausgänge erstellt werden!";
        //        return NotFound(respCall);
        //    }
        //}
    }
}
