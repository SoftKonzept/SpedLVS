using System;

namespace Sped4.Classes.UpdateArchive
{
    public class Aup1317
    {
        /// <summary>
        ///             Archive - Up
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1317 = "1317";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF(" +
                         "EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'PrintQueue'))" +
                         "BEGIN " +
                          "IF COL_LENGTH('PrintQueue','PrintCount') IS NULL " +
                          "BEGIN " +
                            "ALTER TABLE [PrintQueue] ADD [PrintCount] [int] NOT NULL DEFAULT ((1)); " +
                          "END " +
                      "END; ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static void SqlStringUpdate()
        {
            string strSql = string.Empty;
            //foreach (string s in sqlList)
            //{
            //    strSql += s;
            //}
            //return strSql;
        }
    }
}
