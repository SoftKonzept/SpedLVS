using System;
namespace Sped4.Classes.Update
{
    public class up1325
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1325 = "1325";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aaTest]') AND type in (N'U')) " +
                  "BEGIN " +
                        "DROP TABLE [dbo].[aaTest]" +
                   "END ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string SqlStringUpdate_UpdateExistingColumns()
        {
            //System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;

            //sql += " Update Gueterart SET ";
            //sql += "DelforVerweis = ''";

            return sql;
        }
    }
}
