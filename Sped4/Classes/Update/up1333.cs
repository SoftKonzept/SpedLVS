using System;

namespace Sped4.Classes.Update
{
    public class up1333
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1333 = "1333";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Kunde','Oranisation') IS NOT NULL " +
                  "BEGIN " +
                    "EXEC sp_rename 'Kunde.Oranisation', 'Organisation', 'COLUMN'; " +
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

            //sql += " Update Mandanten SET ";
            //sql += "TaxNumber = '' ";
            //sql += ", VatId = '' ";
            //sql += ", Organisation='' ";
            //sql += "; ";

            return sql;
        }
    }
}
