using System;
namespace Sped4.Classes.Update
{
    public class up1304
    {
        /// <summary>
        ///             Table Userberechtigungen  um Datenfelder erweitern
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1304 = "1304";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            //sql = "DECLARE @Date as Datetime; "+
            //      "Set @Date = CAST('01.01.1900' as datetime); "+

            sql = "IF COL_LENGTH('Artikel','ScanIn') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Artikel] ADD [ScanIn] [datetime] NOT NULL DEFAULT (''); " +
                  "END " +
                  "IF COL_LENGTH('Artikel','ScanInUser') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Artikel] ADD [ScanInUser] [int] NOT NULL DEFAULT ((0)); " +
                  "END " +
                  "IF COL_LENGTH('Artikel','ScanOut') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Artikel] ADD [ScanOut] [datetime] NOT NULL DEFAULT (''); " +
                  "END " +
                  "IF COL_LENGTH('Artikel','ScanOutUser') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Artikel] ADD [ScanOutUser] [int] NOT NULL DEFAULT ((0)); " +
                  "END ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public static string SqlStringUpdate_UpdateExistingColumns()
        //{
        //    System.DateTime tmpDT = new DateTime(1900, 1, 1);
        //    string sql = string.Empty;

        //    sql += " Update Artikel SET " +
        //             "ScanIn = '" + tmpDT + "' " +
        //             ", ScanInUser = 0  " +
        //             ", ScanOut = '" + tmpDT + "' " +
        //             ", ScanOutUser = 0  ";
        //    return sql;
        //}
    }
}
