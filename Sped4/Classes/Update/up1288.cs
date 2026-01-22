namespace Sped4.Classes.Update
{
    public class up1288
    {
        public const string const_up1288 = "1288";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Abrufe','ASNFile') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Abrufe] ADD [ASNFile] [nvarchar] (80) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Abrufe','ASNLieferant') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Abrufe] ADD [ASNLieferant] [nvarchar] (20) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Abrufe','ASNQuantity') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Abrufe] ADD [ASNQuantity] [int] DEFAULT((0)) NOT NULL; " +
                  "END " +
                  "IF COL_LENGTH('Abrufe','ASNUnit') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Abrufe] ADD [ASNUnit] [nvarchar] (20) NULL; " +
                  "END " +
                  "IF COL_LENGTH('Abrufe','Description') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Abrufe] ADD [Description] [Text] NULL; " +
                  "END ";
            return sql;
        }
    }
}
