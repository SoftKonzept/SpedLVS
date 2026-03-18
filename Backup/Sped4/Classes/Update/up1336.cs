using System;

namespace Sped4.Classes.Update
{
    public class up1336
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1336 = "1336";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASNArtFieldAssignment','SubASNField') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ASNArtFieldAssignment] ADD [SubASNField] [nvarchar] (50) NULL; " +
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

            //sql += " Update ADRVerweis SET ";
            //sql += "IgnoreEdi = 0";
            //sql += "; ";

            return sql;
        }
    }
}
