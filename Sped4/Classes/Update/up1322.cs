using System;
namespace Sped4.Classes.Update
{
    public class up1322
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1322 = "1322";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Artikel','CreatedByScanner') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Artikel] ADD [CreatedByScanner] [bit] NOT NULL DEFAULT (0); " +
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

            sql += " Update Artikel SET ";
            sql += "CreatedByScanner = 0";

            return sql;
        }
    }
}
