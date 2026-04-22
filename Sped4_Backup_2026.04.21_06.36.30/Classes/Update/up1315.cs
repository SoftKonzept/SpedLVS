using System;
namespace Sped4.Classes.Update
{
    public class up1315
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1315 = "1315";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('LAusgang','PrintActionScannerLfs') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LAusgang] ADD [PrintActionScannerLfs] [bit] NOT NULL DEFAULT ((0));  " +
                  "END " +
                  "IF COL_LENGTH('LAusgang','PrintActionScannerKVOFrachtbrief') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LAusgang] ADD [PrintActionScannerKVOFrachtbrief] [bit] NOT NULL DEFAULT ((0));  " +
                  "END " +
                  "IF COL_LENGTH('LAusgang','PrintActionScannerAusgangsliste') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LAusgang] ADD [PrintActionScannerAusgangsliste] [bit] NOT NULL DEFAULT ((0));  " +
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
