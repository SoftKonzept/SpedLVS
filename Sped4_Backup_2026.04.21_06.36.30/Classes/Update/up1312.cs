using System;
namespace Sped4.Classes.Update
{
    public class up1312
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1312 = "1312";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ReportDocSettingAssignment','Report') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ReportDocSettingAssignment] ADD [Report] [varbinary](max) NULL; " +
                  "END " +
                  "IF COL_LENGTH('ReportDocSettingAssignment','FileExtension') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ReportDocSettingAssignment] ADD [FileExtension] [nvarchar](20) NULL; " +
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
