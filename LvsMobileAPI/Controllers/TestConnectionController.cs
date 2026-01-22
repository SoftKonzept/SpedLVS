using LvsMobileAPI.Authorization;
using LvsMobileAPI.DataConnection;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LvsMobileAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]

    public class TestConnectionController : ControllerBase
    {

        // --- api/TestConnection/GetDBConnectionInfo
        //[AllowAnonymous]
        [HttpGet("GetDBConnectionInfo")]
        public IActionResult GetDBConnectionInfo()
        {
            TestConnection tc = new TestConnection();
            //return tc.Message;
            return Ok(tc.Message);
        }
        //public string GetDBConnectionInfo()
        //{
        //    TestConnection tc = new TestConnection();
        //    return tc.Message;
        //}
        //[AllowAnonymous]
        //[HttpGet("PublicTest")]
        //public IActionResult PublicTest()
        //{
        //    return Ok("Hi, its TestConnectionController /  PublicTest");
        //}

        /// <summary>
        ///             https://192.168.1.55/api/TestConnection/PublicTestAllowAnonymous
        /// </summary>
        /// <returns></returns>
        //[AllowAnonymous]
        //[HttpGet("PublicTestAllowAnonymous")]
        //public IActionResult PublicTestAllowAnonymous()
        //{
        //    return Ok("Hi, its TestConnectionController /  PublicTestAllowAnonymous");
        //}
    }
}
