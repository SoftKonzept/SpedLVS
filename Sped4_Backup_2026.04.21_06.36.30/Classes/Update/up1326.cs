using System;

namespace Sped4.Classes.Update
{
    public class up1326
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1326 = "1326";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Abrufe','EdiDelforD97AValueId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Abrufe] ADD [EdiDelforD97AValueId] [int] NOT NULL DEFAULT (0); " +
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

            sql += " Update Abrufe SET ";
            sql += "EdiDelforD97AValueId = 0";

            return sql;
        }
    }
}
