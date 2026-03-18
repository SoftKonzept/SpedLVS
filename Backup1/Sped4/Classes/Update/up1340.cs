using System;

namespace Sped4.Classes.Update
{
    public class up1340
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1340 = "1340";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Tarife','RequiresCompletedWarehouseEntry') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Tarife] ADD [RequiresCompletedWarehouseEntry] [bit] NOT NULL DEFAULT ((1)); " +
                  "END ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string SqlStringUpdate_UpdateExistingColumns()
        {
            string sql = string.Empty;

            sql += " Update Tarife SET ";
            sql += "RequiresCompletedWarehouseEntry = 1";
            //sql += ", ValueSeparator = '' ";
            sql += "; ";

            return sql;
        }
    }
}
