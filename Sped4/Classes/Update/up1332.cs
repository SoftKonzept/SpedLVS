using System;

namespace Sped4.Classes.Update
{
    public class up1332
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1332 = "1332";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Mandanten','TaxNumber') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [TaxNumber] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Mandanten','VatId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [VatId] [nvarchar] (254) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Mandanten','Organisation') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Mandanten] ADD [Organisation] [nvarchar] (254) NULL; " +
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
            sql += "TaxNumber = '' ";
            sql += ", VatId = '' ";
            sql += ", Organisation='' ";
            sql += "; ";

            return sql;
        }
    }
}
