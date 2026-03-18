using System;

namespace Sped4.Classes.UpdateArchive
{
    public class Aup1310
    {
        /// <summary>
        ///             Archive - Up
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1310 = "1310";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF(" +
                     "NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Archive')) " +
                     "BEGIN " +
                         "CREATE TABLE [dbo].[Archive](" +
                        "[Id][int] IDENTITY(1, 1) NOT NULL," +
                        "[TableName] [nvarchar] (50) NULL," +
                        "[TableId][int] NULL," +
                        "[FileArt][int] NULL," +
                        "[FileData][varbinary] (max)NULL," +
                        "[Extension][nvarchar] (10) NULL," +
                        "[Filename][nvarchar] (50) NULL," +
                        "[Created][datetime] NULL," +
                        "[UserId][int] NULL," +
                        "[Description][varchar] (max)NULL," +
                        "[ReportDocSettingAssignmentId][int] NULL," +
                        "[ReportDocSettingId][int] NULL," +
                        "[DocKey][nvarchar] (50) NULL," +
                        "[WorkspaceId][int] NULL," +
                        "[DocKeyID][int] NULL," +
                        "CONSTRAINT[PK_Archive] PRIMARY KEY CLUSTERED([Id] ASC" +
                       ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
                       ") ON[PRIMARY] TEXTIMAGE_ON[PRIMARY] " +
                   "END ";
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

            //strSql = "INSERT INTO Version ";
            //strSql += "(";
            //strSql += " Versionsnummer, LastUpdate";
            //strSql += ")";
            //strSql += " VALUES ";
            //strSql += "(";
            //strSql+= "1400";
            //strSql+= ", '"+DateTime.Now.ToString() +"'";
            //strSql+= ")";

            return strSql;
        }
    }
}
