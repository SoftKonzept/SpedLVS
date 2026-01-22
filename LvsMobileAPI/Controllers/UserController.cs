using LvsMobileAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AllowAnonymousAttribute = LvsMobileAPI.Authorization.AllowAnonymousAttribute;

namespace LvsMobileAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        ///             GET - Public
        ///             /api/User/Public
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Test")]

        public IActionResult Test()
        {

            return Ok("Hi, its Users Test");
        }

        /// <summary>
        ///             GET - GetCurrentUser
        ///             /api/User/GetUser
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                int iUserId = 0;
                var strNameIdentifier = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (strNameIdentifier != null)
                {
                    int.TryParse(strNameIdentifier, out iUserId);
                    if (iUserId > 0)
                    {
                        var loggedUser = _userService.GetUser(iUserId);

                        //userVD = new UsersViewData(new LVS.Globals._GL_USER(), iUserId);
                        if (
                                (loggedUser is Common.Models.Users) &&
                                (loggedUser.Id > 0) &&
                                (loggedUser.UserAuthorization.access_App)
                              )
                        {
                            return Ok(loggedUser);
                        }
                        else
                        {
                            return NotFound("Zugriffsdaten fehlerhaft oder keine Userdaten vorhanden!");
                        }
                    }
                    else
                    {
                        return NotFound("Tokendaten fehlerhaft!");
                    }
                }
                else
                {
                    return NotFound("Tokendaten fehlerhaft!");
                }

            }
            else
            {
                return NotFound("Zugriffsdaten fehlerhaft oder keine Userdaten vorhanden!");
            }
        }

    }
}
