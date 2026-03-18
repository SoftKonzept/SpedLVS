using System;

namespace Sped4.Classes.Update
{
    public class up1330
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1330 = "1330";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Mandanten','Bank') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [Bank] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Mandanten','BLZ') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [BLZ] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Mandanten','BIC') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [BIC] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Mandanten','Konto') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [Konto] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Mandanten','IBAN') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [IBAN] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Mandanten','Contact') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [Contact] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Mandanten','Mail') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [Mail] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Mandanten','Homepage') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [Homepage] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Mandanten','Phone') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [Phone] [nvarchar] (254) NULL; " +
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

            sql += " Update Mandanten SET ";
            sql += "Bank = '' ";
            sql += ", BLZ = '' ";
            sql += ", BIC = '' ";
            sql += ", Konto = '' ";
            sql += ", IBAN = '' ";
            sql += ", Contact = '' ";
            sql += ", Mail = '' ";
            sql += ", Homepage = '' ";
            sql += ", Phone = '' ";

            sql += "; ";

            return sql;
        }
    }
}
