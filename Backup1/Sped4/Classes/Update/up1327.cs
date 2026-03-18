using System;

namespace Sped4.Classes.Update
{
    public class up1327
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1327 = "1327";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('ASNArtFieldAssignment','IsGlobalFieldVar') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [ASNArtFieldAssignment] ADD [IsGlobalFieldVar] [bit] NOT NULL DEFAULT (0); " +
                  "END " +
                  "IF COL_LENGTH('ASNArtFieldAssignment','GlobalFieldVar') IS NULL " +
                  "BEGIN " +
                     "ALTER TABLE [ASNArtFieldAssignment] ADD [GlobalFieldVar] [nvarchar] (100) Null; " +
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

            sql += " Update ASNArtFieldAssignment SET ";
            sql += "IsGlobalFieldVar = 0";
            sql += ", GlobalFieldVar = '' ";
            sql += " ;";

            return sql;
        }
    }
}
