using Common.ApiModels;
using LvsMobileAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AllowAnonymousAttribute = LvsMobileAPI.Authorization.AllowAnonymousAttribute;

namespace LvsMobileAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class LoginController : Controller
    {
        private ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        ///             POST - Login
        ///             /api/Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>

        [AllowAnonymous]
        [HttpPost()]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var response = _loginService.Authenticate(userLogin);
                if (response is ResponseLogin)
                {
                    if (response.AccessGranted)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(response);
                    }
                }
                else
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return BadRequest(str);
            }
        }

        [AllowAnonymous]
        [HttpGet("Public")]

        public IActionResult Public()
        {
            return Ok("Hi, its Login /  public");
        }
    }
}
