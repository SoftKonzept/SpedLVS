using System;

namespace Sped4.Classes.Update
{
    public class up1341
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1341 = "1341";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Mandanten','Register') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [Register] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Mandanten','MagistrateCourt') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [MagistrateCourt] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Mandanten','ManagingDirector') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [ManagingDirector] [nvarchar] (254) NULL; " +
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

            sql += " Update Mandanten SET ";
            sql += "Register = '' ";
            sql += ", MagistrateCourt = '' ";
            sql += ", ManagingDirector = '' ";
            sql += "; ";

            return sql;
        }
    }
}
