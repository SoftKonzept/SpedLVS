using System;

namespace Sped4.Classes.UpdateArchive
{
    public class Aup1309
    {
        /// <summary>
        ///             Archive - Up
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1309 = "1309";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF(" +
                     "NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Version'))" +
                     "BEGIN " +
                          "CREATE TABLE[dbo].[Version](" +
                          "[ID][int] NOT NULL, " +
                          "[Versionsnummer] [decimal](4, 0) NOT NULL, " +
                          "[LastUpdate] [datetime] NULL," +
                          " CONSTRAINT[PK_Version] PRIMARY KEY CLUSTERED([ID] ASC" +
                          ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " +
                          ") ON[PRIMARY]  " +
                    "END; ";
            return sql;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string SqlStringUpdate_InsertFirstRow()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string strSql = string.Empty;

            strSql += "IF NOT EXISTS (SELECT ID FROM Version)";
            strSql += "BEGIN ";
            strSql += "INSERT INTO Version ";
            strSql += "(";
            strSql += " ID, Versionsnummer, LastUpdate";
            strSql += ")";
            strSql += " VALUES ";
            strSql += "(";
            strSql += " 1";
            strSql += ", 1308";
            strSql += ", '" + DateTime.Now.ToString() + "'";
            strSql += ") ";
            strSql += "END ";

            return strSql;
        }
    }
}
