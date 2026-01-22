using LVS.Models;
using LVS.ViewData;

namespace Communicator.Classes
{
    public class up1049

    {

        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1049 = "1049";
        public static string SqlString()
        {
            string sql = string.Empty;
            var arts = AsnArtViewData.CheckTableAsnArtForUpdate();
            if (arts.Count > 0)
            {
                foreach (AsnArt item in arts)
                {
                    AsnArtViewData artVD = new AsnArtViewData(item);
                    sql += artVD.sql_Add;
                }
            }
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string SqlStringUpdate_UpdateExistingColumns()
        {
            string sql = string.Empty;
            //sql = "Update Jobs " +
            //                 "SET DelforVerweis = ''; ";

            return sql;
        }
    }
}
