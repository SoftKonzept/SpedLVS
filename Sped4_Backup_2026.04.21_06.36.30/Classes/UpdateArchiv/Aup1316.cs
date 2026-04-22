using System;

namespace Sped4.Classes.UpdateArchive
{
    public class Aup1316
    {
        /// <summary>
        ///             Archive - Up
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1316 = "1316";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF(" +
                     "NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'PrintQueue'))" +
                     "BEGIN " +
                       "CREATE TABLE[dbo].[PrintQueue](" +
                        "[Id][int] IDENTITY(1, 1) NOT NULL," +
                        "[RepoortDocSettingId] [int] NULL," +
                        "[RepoortDocSettingAssignmentId] [int] NULL," +
                        "[Created][datetime] NULL," +
                        "[IsActiv][bit] NULL," +
                        "[TableName][nvarchar](50) NULL," +
                        "[TableId][int] NULL," +
                        "[WorkspaceId][int] NULL," +
                     "CONSTRAINT[PK_PrintQueue] PRIMARY KEY CLUSTERED([Id] ASC" +
                    ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " +
                    ") ON[PRIMARY]" +

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
