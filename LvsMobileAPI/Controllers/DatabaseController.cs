using Common.Enumerations;
using Common.Models;
using LVS.ViewData;
using LvsMobileAPI.DataConnection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LvsMobileAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class DatabaseController : ControllerBase
    {
        internal SvrSettings srv;
        public InventoryViewData inventoryViewData { get; set; }
        public InvnetoryArticleViewData inventoryArticleViewData { get; set; }


        //[HttpPatch("PATCH_DatabaseAction_Update/{DatabaseManipulationString}")]
        public string PATCH_DatabaseAction_Update(string DatabaseManipulationString)
        {

            srv = new SvrSettings();
            string strReturn = false.ToString();
            bool bReturn = false;


            var dbmValue = JsonConvert.DeserializeObject<DatabaseManipulations>(DatabaseManipulationString);
            if (
                (dbmValue != null) &&
                (dbmValue is DatabaseManipulations)
              )
            {
                DatabaseManipulations dbm = dbmValue;

                switch (Enum.Parse(typeof(enumDatabaseSped4_TableNames), dbm.TableName))
                {
                    case enumDatabaseSped4_TableNames.InventoryArticle:
                        inventoryArticleViewData = new InvnetoryArticleViewData(dbm);
                        inventoryArticleViewData.InventoryArticle.Id = dbm.TableId;
                        bReturn = inventoryArticleViewData.Update();
                        break;
                }
            }
            strReturn = bReturn.ToString();
            return strReturn;
        }


        //[HttpPatch("PATCH_DatabaseAction_UpdateTest/{DatabaseManipulationString}")]
        //public IActionResult PATCH_DatabaseAction_UpdateTest(string DatabaseManipulationString)
        //{

        //    srv = new SvrSettings();
        //    string strReturn = false.ToString();
        //    bool bReturn = false;


        //    if (!DatabaseManipulationString.Equals(string.Empty))
        //    {
        //        return Ok("Funktion erfolgreich aufgerufen!");
        //    }
        //    else
        //    {
        //        return Problem("Achtung - Funktion erfolgreich aufgerufen!!");
        //    }
        //}

    }
}
