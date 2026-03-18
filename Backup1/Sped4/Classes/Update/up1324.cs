using System;
namespace Sped4.Classes.Update
{
    public class up1324
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1324 = "1324";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomProcessExceptions]') AND type in (N'U')) " +
            "BEGIN " +

                    "CREATE TABLE[dbo].[CustomProcessExceptions](" +
                    "[Id][int] IDENTITY(1, 1) NOT NULL," +
                    "[CustomProcessId][int] NULL," +
                    "[GoodsTyepId][int] NULL," +
                    "[Created][datetime2](7) NULL," +
                    "CONSTRAINT[PK_CustomProcessExceptions] PRIMARY KEY CLUSTERED([Id] ASC" +
                    ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]" +
                    ") ON[PRIMARY] " +

                    "GO " +
                    "ALTER TABLE[dbo].[CustomProcessExceptions] ADD CONSTRAINT[DF_CustomProcessExceptions_Created]  DEFAULT(getdate()) FOR[Created] " +
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

            //sql += " Update Gueterart SET ";
            //sql += "DelforVerweis = ''";

            return sql;
        }
    }
}
