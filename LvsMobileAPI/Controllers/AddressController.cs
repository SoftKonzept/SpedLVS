using Common.ApiModels;
using Common.Enumerations;
using Common.Models;
using LvsMobileAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AllowAnonymousAttribute = LvsMobileAPI.Authorization.AllowAnonymousAttribute;

namespace LvsMobileAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        public IAddressService _adrService;
        public AddressController(IAddressService addressService)
        {
            _adrService = addressService;
        }

        //-------------------------------------------------- GET  -------------------------------------------------------------------

        [AllowAnonymous]
        [HttpGet("GET_Address/{AddressId}")]
        public IActionResult GET_Address(int AddressId)
        {
            ResponseAddress resAdr = new ResponseAddress();
            resAdr.Success = false;

            if (AddressId > 0)
            {
                var adr = _adrService.GET_Adress(AddressId);

                if ((adr is Addresses) && (adr.Id == AddressId))
                {
                    resAdr.Success = true;
                    resAdr.Address = adr.Copy();
                    return Ok(resAdr);
                }
                else
                {
                    resAdr.Success = false;
                    resAdr.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                    return NotFound(resAdr);
                }
            }
            else
            {
                resAdr.Success = false;
                resAdr.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resAdr);
            }
        }

        [AllowAnonymous]
        [HttpGet("GET_Addresslist/{myEnumAppProcessId}/{myWorkspaceId}")]
        public IActionResult GET_Addresslist(int myEnumAppProcessId, int myWorkspaceId)
        {
            enumAppProcess tmpAppProcess = enumAppProcess.NotSet;
            Enum.TryParse(myEnumAppProcessId.ToString(), out tmpAppProcess);

            ResponseAddress resAdr = new ResponseAddress();
            resAdr.Success = false;
            resAdr.WorkspaceId = myWorkspaceId;
            resAdr.AppProcess = tmpAppProcess;

            switch (resAdr.AppProcess)
            {
                case enumAppProcess.StoreOut:
                case enumAppProcess.StoreIn:
                case enumAppProcess.NotSet:
                    resAdr = _adrService.GET_AddressList(resAdr);
                    if ((resAdr.Address is Addresses) && (resAdr.Address.Id > 0))
                    {
                        resAdr.Success = true;
                        resAdr.Info = "Die Addressliste konnte ermittelt werden!";
                        return Ok(resAdr);
                    }
                    else
                    {
                        resAdr.Success = false;
                        resAdr.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                        return NotFound(resAdr);
                    }


                default:
                    resAdr.Success = false;
                    resAdr.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                    return NotFound(resAdr);

            }
        }

        [AllowAnonymous]
        [HttpPost("Post_AdressSupplierNo")]
        public IActionResult Post_AdressSupplierNo(ResponseAddress myResponseAddress)
        {
            myResponseAddress = _adrService.POST_AddressSupplierNo(myResponseAddress);
            return Ok(myResponseAddress);
        }


    }
}
