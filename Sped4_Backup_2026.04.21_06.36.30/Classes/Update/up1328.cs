using System;

namespace Sped4.Classes.Update
{
    public class up1328
    {
        /// <summary>
        ///             
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1328 = "1328";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Sperrlager','IsCustomCertificateMissing') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Sperrlager] ADD [IsCustomCertificateMissing] [bit] NOT NULL DEFAULT (0); " +
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

            sql += " Update Sperrlager SET ";
            sql += "IsCustomCertificateMissing = 0";
            sql += "; ";


            sql += " Update Sperrlager SET ";
            sql += "IsCustomCertificateMissing = 1";
            sql += "where ID in (";
            sql += "SELECT ID FROM Sperrlager where BKZ ='IN' AND SPLIDIn=0 AND Sperrgrund ='Artikel Zertifikat fehlt') ;";
            return sql;
        }
    }
}
