using System;

namespace Sped4.Classes.UpdateArchive
{
    public class Aup1311
    {
        /// <summary>
        ///             Archive - Up
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1311 = "1311";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF(" +
                     "NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Images'))" +
                     "BEGIN " +
                       "CREATE TABLE[dbo].[Images](" +
                      "[ID][decimal](28, 0) IDENTITY(1, 1) NOT NULL," +
                      "[AuftragTableID] [decimal](28, 0) NULL," +
                      "[LEingangTableID][decimal](28, 0) NULL," +
                      "[LAusgangTableID][decimal](28, 0) NULL," +
                      "[PicNum][int] NULL," +
                      "[Pfad][varchar] (max)NULL," +
                      "[ScanFilename][varchar] (max)NULL," +
                      "[ImageArt][nvarchar] (255) NULL," +
                      "[AuftragPosTableID][decimal](28, 0) NOT NULL," +
                      "[DocImage] [varbinary] (max)NULL," +
                      "[TableName][nvarchar] (50) NULL," +
                      "[TableID][decimal](28, 0) NOT NULL," +
                      "[Thumbnail] [varbinary] (max)NULL," +
                      "[IsForSPLMessage][bit] NOT NULL," +
                      "[Created] [datetime] NULL," +
                      "CONSTRAINT[PK_AuftragScan] PRIMARY KEY CLUSTERED([ID] ASC" +
                      ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
                    ") ON[PRIMARY] TEXTIMAGE_ON[PRIMARY] " +


                    "ALTER TABLE[dbo].[Images] ADD CONSTRAINT[DF_DocScan_AuftragID]  DEFAULT((0)) FOR[AuftragTableID] " +
                    "ALTER TABLE[dbo].[Images] ADD CONSTRAINT[DF__DocScan__LEingan__178D7CA5]  DEFAULT((0)) FOR[LEingangTableID] " +
                    "ALTER TABLE[dbo].[Images] ADD CONSTRAINT[DF__DocScan__LAusgan__1881A0DE]  DEFAULT((0)) FOR[LAusgangTableID] " +
                    "ALTER TABLE[dbo].[Images] ADD DEFAULT((0)) FOR[AuftragPosTableID] " +
                    "ALTER TABLE[dbo].[Images] ADD DEFAULT((0)) FOR[TableID] " +
                    "ALTER TABLE[dbo].[Images] ADD DEFAULT((0)) FOR[IsForSPLMessage] " +
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
