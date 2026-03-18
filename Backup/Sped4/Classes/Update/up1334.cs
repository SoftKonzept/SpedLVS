using System;

namespace Sped4.Classes.Update
{
    public class up1334
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1334 = "1334";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Gueterart','IgnoreEdi') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Gueterart] ADD [IgnoreEdi] [bit] NOT NULL DEFAULT (0); " +
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

            sql += " Update Gueterart SET ";
            sql += "IgnoreEdi = 0";
            sql += "; ";

            return sql;
        }
    }
}
