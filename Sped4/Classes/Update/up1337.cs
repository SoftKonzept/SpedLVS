using System;

namespace Sped4.Classes.Update
{
    public class up1337
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1337 = "1337";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASNTableCombiValue','UseValueSeparator') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ASNTableCombiValue] ADD [UseValueSeparator] [bit] DEFAULT (0); " +
                  "END " +
                  "IF COL_LENGTH('ASNTableCombiValue','ValueSeparator') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ASNTableCombiValue] ADD [ValueSeparator] [nvarchar] (10); " +
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

            sql += " Update ASNTableCombiValue SET ";
            sql += "UseValueSeparator = 0";
            sql += ", ValueSeparator = '' ";
            sql += "; ";

            return sql;
        }
    }
}
