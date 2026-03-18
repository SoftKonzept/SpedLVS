using System;

namespace Sped4.Classes.Update
{
    public class up1338
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1338 = "1338";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('RGPositionen','PricePerUnitFactor') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [RGPositionen] ADD [PricePerUnitFactor] [decimal] (18, 3) null; " +
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

            sql += " Update RGPositionen SET ";
            sql += "PricePerUnitFactor = Menge";
            //sql += ", ValueSeparator = '' ";
            sql += "; ";

            return sql;
        }
    }
}
