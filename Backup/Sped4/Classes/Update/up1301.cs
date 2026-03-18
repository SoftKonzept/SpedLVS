using LVS;
using System;
namespace Sped4.Classes.Update
{
    public class up1301
    {
        /// <summary>
        ///             Table Inventory um Datenfelder erweitern
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1301 = "1301";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Inventories','Status') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Inventories] ADD [Status] [int] DEFAULT((0)); " +
                  "END " +
                  "IF COL_LENGTH('Inventories','CloseDate') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Inventories] ADD [CloseDate] [datetime2](7) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Inventories','CloseUserId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Inventories] ADD [CloseUserId] [int] DEFAULT((0)); " +
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
            sql = " Update Inventories SET " +
                            "[Status] = " + (int)enumInventoryStatus.erstellt +
                            ", CloseDate = '" + tmpDT + "'" +
                            ", CloseUserId = 0 " +
                            ";";

            return sql;
        }
    }
}
