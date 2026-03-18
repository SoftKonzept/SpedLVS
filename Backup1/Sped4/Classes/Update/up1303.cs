using System;
namespace Sped4.Classes.Update
{
    public class up1303
    {
        /// <summary>
        ///             Table Userberechtigungen  um Datenfelder erweitern
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1303 = "1303";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Userberechtigungen','access_App') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Userberechtigungen] ADD [access_App] [bit] NOT NULL DEFAULT ((0)); " +
                  "END " +
                  "IF COL_LENGTH('Userberechtigungen','access_AppStoreIn') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Userberechtigungen] ADD [access_AppStoreIn] [bit] NOT NULL DEFAULT ((0)); " +
                  "END " +
                  "IF COL_LENGTH('Userberechtigungen','access_AppStoreOut') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Userberechtigungen] ADD [access_AppStoreOut] [bit] NOT NULL DEFAULT ((0)); " +
                  "END " +
                  "IF COL_LENGTH('Userberechtigungen','access_AppInventory') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Userberechtigungen] ADD [access_AppInventory] [bit] NOT NULL DEFAULT ((0)); " +
                  "END " +
                  "IF COL_LENGTH('Userberechtigungen','read_Inventory') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Userberechtigungen] ADD [read_Inventory] [bit] NOT NULL DEFAULT ((0)); " +
                  "END " +
                  "IF COL_LENGTH('Userberechtigungen','write_Inventory') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Userberechtigungen] ADD [write_Inventory] [bit] NOT NULL DEFAULT ((0)); " +
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
            sql += " Update Userberechtigungen SET " +
                            "access_App = 0 ; ";
            sql += " Update Userberechtigungen SET " +
                            "access_AppStoreIn = 0 ; ";
            sql += " Update Userberechtigungen SET " +
                            "access_AppStoreOut = 0 ; ";
            sql += " Update Userberechtigungen SET " +
                            "access_AppInventory = 0 ; ";
            sql += " Update Userberechtigungen SET " +
                                "read_Inventory = 0 ; ";
            sql += " Update Userberechtigungen SET " +
                                "write_Inventory = 0 ; ";

            sql += " Update Userberechtigungen SET " +
                     "access_App = 1 " +
                     ", access_AppStoreIn = 1  " +
                     ", access_AppStoreOut = 1  " +
                     ", access_AppInventory = 1  " +
                     ", read_Inventory = 1 " +
                     ", write_Inventory = 1 " +
                     " where UserId = 1 ;";

            return sql;
        }
    }
}
