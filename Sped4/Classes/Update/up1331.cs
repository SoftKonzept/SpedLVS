using System;

namespace Sped4.Classes.Update
{
    public class up1331
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1331 = "1331";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Kunde','Contact') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Kunde] ADD [Contact] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Kunde','Phone') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Kunde] ADD [Phone] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Kunde','Mailaddress') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Kunde] ADD [Mailaddress] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Kunde','Oranisation') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Kunde] ADD [Oranisation] [nvarchar] (254) NULL; " +
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

            sql += " Update Kunde SET ";
            sql += "Contact = '' ";
            sql += ", Phone = '' ";
            sql += ", Mailaddress = '' ";
            sql += ", Oranisation = '' ";
            sql += "; ";

            return sql;
        }
    }
}
