using System;
namespace Sped4.Classes.Update
{
    public class up1323
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1323 = "1323";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Gueterart','DelforVerweis') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Gueterart] ADD [DelforVerweis] [nvarchar](30) NOT NULL DEFAULT (''); " +
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

            sql += " Update Gueterart SET ";
            sql += "DelforVerweis = ''";

            return sql;
        }
    }
}
