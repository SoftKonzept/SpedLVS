using System;

namespace Sped4.Classes.Update
{
    public class up1339
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1339 = "1339";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('RGPositionen','TarifPricePerUnit') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [RGPositionen] ADD [TarifPricePerUnit] [money] null; " +
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
            sql += "TarifPricePerUnit = EinzelPreis";
            //sql += ", ValueSeparator = '' ";
            sql += "; ";

            return sql;
        }
    }
}
