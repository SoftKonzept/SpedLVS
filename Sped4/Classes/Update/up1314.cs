using System;
namespace Sped4.Classes.Update
{
    public class up1314
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1314 = "1314";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('LEingang','PrintActionScannerAllLable') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LEingang] ADD [PrintActionScannerAllLable] [bit] NOT NULL DEFAULT ((0));  " +
                  "END " +
                  "IF COL_LENGTH('LEingang','PrintActionScannerEingangsliste') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LEingang] ADD [PrintActionScannerEingangsliste] [bit] NOT NULL DEFAULT ((0));  " +
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

            //sql += " Update Arbeitsbereich SET ";
            //sql += "AbrufDefEmpfaengerId = 0";
            //sql += ", EingangDefEmpfaengerId=0";
            //sql += ", EingangDefEntladeId=0";
            //sql += ", EingangDefBeladeId=0";
            //sql += ", AusgangDefEmpfaengerId=0";
            //sql += ", AusgangDefVersenderId=0";
            //sql += ", AusgangDefEntladeId=0";
            //sql += ", AusgangDefBeladeId=0";
            //sql += ", UBDefEmpfaengerId=0";
            //sql += ", UBDefAuftraggeberNeuId=0";
            return sql;
        }
    }
}
