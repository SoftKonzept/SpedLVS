using System;

namespace Sped4.Classes.Update
{
    public class up1329
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1329 = "1329";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ADRVerweis','Description') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ADRVerweis] ADD [Description] [Text] NULL; " +
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

            sql += " Update ADRVerweis SET ";
            sql += "Description = ''";
            sql += "; ";

            return sql;
        }
    }
}
