using System;
namespace Sped4.Classes.Update
{
    public class up1321
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1321 = "1321";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('LEingang','CreatedByScanner') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LEingang] ADD [CreatedByScanner] [bit] NOT NULL DEFAULT (0); " +
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

            sql += " Update LEingang SET ";
            sql += "CreatedByScanner = 0";

            return sql;
        }
    }
}
