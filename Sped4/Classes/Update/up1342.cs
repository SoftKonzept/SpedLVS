using System;

namespace Sped4.Classes.Update
{
    public class up1342
    {
        public const string const_up1342 = "1342";

        public static string SqlString()
        {
            string sql = string.Empty;
            // Neue Spalte: verschlüsselte Credentials als Binärdaten (varbinary(max))
            sql += "IF COL_LENGTH('User','MailCredentialsData') IS NULL " +
                   "BEGIN " +
                     "ALTER TABLE [User] ADD [MailCredentialsData] VARBINARY(MAX) NULL; " +
                   "END ";

            // Optional: Dateiname / Metadaten separat speichern
            sql += "IF COL_LENGTH('User','MailCredentialsFileName') IS NULL " +
                   "BEGIN " +
                     "ALTER TABLE [User] ADD [MailCredentialsFileName] NVARCHAR(260) NULL; " +
                   "END ";

            return sql;
        }

        public static string SqlStringUpdate_UpdateExistingColumns()
        {
            string sql = string.Empty;

            // Keine zwingende Initialisierung der varbinary-Spalte nötig; bei Bedarf auf NULL belassen.
            // Optional vorhandene Dateinamen initialisieren
            sql += " IF COL_LENGTH('User','MailCredentialsFileName') IS NOT NULL " +
                   " BEGIN " +
                   "   UPDATE [User] SET MailCredentialsFileName = ISNULL(MailCredentialsFileName, ''); " +
                   " END ";

            return sql;
        }
    }
}
