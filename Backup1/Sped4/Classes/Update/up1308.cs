using System;
namespace Sped4.Classes.Update
{
    public class up1308
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1308 = "1308";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Arbeitsbereich','AbrufDefEmpfaengerId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Arbeitsbereich] ADD [AbrufDefEmpfaengerId] [int] NOT NULL DEFAULT ((0)); " +
                  "END " +
                  "IF COL_LENGTH('Arbeitsbereich','EingangDefEmpfaengerId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Arbeitsbereich] ADD [EingangDefEmpfaengerId] [int] NOT NULL DEFAULT ((0)); " +
                  "END " +
                  "IF COL_LENGTH('Arbeitsbereich','EingangDefEntladeId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Arbeitsbereich] ADD [EingangDefEntladeId] [int] NOT NULL DEFAULT ((0)); " +
                  "END " +
                  "IF COL_LENGTH('Arbeitsbereich','EingangDefBeladeId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Arbeitsbereich] ADD [EingangDefBeladeId] [int] NOT NULL DEFAULT ((0)); " +
                  "END " +
                  "IF COL_LENGTH('Arbeitsbereich','AusgangDefEmpfaengerId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Arbeitsbereich] ADD [AusgangDefEmpfaengerId] [int] NOT NULL DEFAULT ((0)); " +
                  "END " +
                  "IF COL_LENGTH('Arbeitsbereich','AusgangDefVersenderId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Arbeitsbereich] ADD [AusgangDefVersenderId] [int] NOT NULL DEFAULT ((0)); " +
                  "END " +
                  "IF COL_LENGTH('Arbeitsbereich','AusgangDefEntladeId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Arbeitsbereich] ADD [AusgangDefEntladeId] [int] NOT NULL DEFAULT ((0)); " +
                  "END " +
                   "IF COL_LENGTH('Arbeitsbereich','AusgangDefBeladeId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Arbeitsbereich] ADD [AusgangDefBeladeId] [int] NOT NULL DEFAULT ((0)); " +
                  "END " +
                  "IF COL_LENGTH('Arbeitsbereich','UBDefEmpfaengerId') IS NULL " +
                   "BEGIN " +
                     "ALTER TABLE [Arbeitsbereich] ADD [UBDefEmpfaengerId] [int] NOT NULL DEFAULT ((0)); " +
                   "END " +
                   "IF COL_LENGTH('Arbeitsbereich','UBDefAuftraggeberNeuId') IS NULL " +
                   "BEGIN " +
                      "ALTER TABLE [Arbeitsbereich] ADD [UBDefAuftraggeberNeuId] [int] NOT NULL DEFAULT ((0)); " +
                   "END ";

            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string SqlStringUpdate_UpdateExistingColumns()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;

            sql += " Update Arbeitsbereich SET ";
            sql += "AbrufDefEmpfaengerId = 0";
            sql += ", EingangDefEmpfaengerId=0";
            sql += ", EingangDefEntladeId=0";
            sql += ", EingangDefBeladeId=0";
            sql += ", AusgangDefEmpfaengerId=0";
            sql += ", AusgangDefVersenderId=0";
            sql += ", AusgangDefEntladeId=0";
            sql += ", AusgangDefBeladeId=0";
            sql += ", UBDefEmpfaengerId=0";
            sql += ", UBDefAuftraggeberNeuId=0";
            return sql;
        }
    }
}
