
using System;

namespace Communicator.Classes
{
    public class up1059
    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1059 = "1059";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASN','AsnArtId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ASN] ADD [AsnArtId] [int] default(0);" +
                  "END " +
                  "IF COL_LENGTH('ASN','Created') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ASN] ADD [Created] [DateTime] null;" +
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
            sql = "Update ASN ";
            sql += "SET AsnArtId = 0 ";
            sql += ", Created = '" + DateTime.Now.ToString() + "'";

            return sql;
        }
    }
}
