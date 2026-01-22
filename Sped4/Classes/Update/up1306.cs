using System;
namespace Sped4.Classes.Update
{
    public class up1306
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1306 = "1306";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Abrufe','ScanCheckForStoreOut') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Abrufe] ADD [ScanCheckForStoreOut] [datetime] NOT NULL DEFAULT (''); " +
                  "END " +
                  "IF COL_LENGTH('Abrufe','ScanUserId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Abrufe] ADD [ScanUserId] [int] NOT NULL DEFAULT ((0)); " +
                  "END ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string SqlStringUpdate_UpdateExistingColumns()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;

            sql += " Update Abrufe SET " +
                     "ScanCheckForStoreOut = '" + tmpDT + "'" +
                     ", ScanUserId=0";
            return sql;
        }
    }
}
