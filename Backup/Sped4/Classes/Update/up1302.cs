using System;
namespace Sped4.Classes.Update
{
    public class up1302
    {
        /// <summary>
        ///             Table InventoryArticle um Datenfelder erweitern
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1302 = "1302";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('InventoryArticle','ScannedUserId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [InventoryArticle] ADD [ScannedUserId] [int] DEFAULT((0)); " +
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
            sql = " Update InventoryArticle SET " +
                            "ScannedUserId = 0 " +
                            ";";
            return sql;
        }
    }
}
