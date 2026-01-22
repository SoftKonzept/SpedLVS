using System;

namespace Sped4.Classes.Update
{
    public class up1335
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1335 = "1335";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ADRVerweis','ReferencePart1') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ADRVerweis] ADD [ReferencePart1] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('ADRVerweis','ReferencePart2') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ADRVerweis] ADD [ReferencePart2] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('ADRVerweis','ReferencePart3') IS NULL " +
                  "BEGIN " +
                  "ALTER TABLE [ADRVerweis] ADD [ReferencePart3] [nvarchar] (254) NULL; " +
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
