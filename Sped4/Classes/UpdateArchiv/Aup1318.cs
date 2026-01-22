namespace Sped4.Classes.UpdateArchive
{
    public class Aup1318
    {
        /// <summary>
        ///             Archive - Up
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1318 = "1318";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF(" +
                         "EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'PrintQueue'))" +
                         "BEGIN " +
                          "IF COL_LENGTH('PrintQueue','PrinterName') IS NULL " +
                          "BEGIN " +
                            "ALTER TABLE [PrintQueue] ADD [PrinterName] [nvarchar] (50) NULL; " +
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
