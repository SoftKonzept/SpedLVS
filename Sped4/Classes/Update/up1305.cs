using System;
namespace Sped4.Classes.Update
{
    public class up1305
    {
        /// <summary>
        ///             Table Ausgang um Datenfelder erweitern mit Flag für PrintAction
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1305 = "1305";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            //sql = "DECLARE @Date as Datetime; "+
            //      "Set @Date = CAST('01.01.1900' as datetime); "+

            sql = "IF COL_LENGTH('LAusgang','PrintActionByScanner') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LAusgang] ADD [PrintActionByScanner] [bit] NOT NULL DEFAULT ((0)); " +
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

            sql += " Update LAusgang SET " +
                     "PrintActionByScanner = 0 ";
            return sql;
        }
    }
}
