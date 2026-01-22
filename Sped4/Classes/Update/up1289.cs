namespace Sped4.Classes.Update
{
    public class up1289
    {
        /// <summary>
        ///             13.01.2019 - mr
        ///             Einstellungsmöglichkeit über die Anzahl der Artikelanzahl pro Lagerausgang
        ///             0 => Standard, nicht festfelegt
        ///             >0 => gibt die genaue max. Anzahl von Artiklen pro Ausgang an
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1289 = "1289";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Arbeitsbereich','ArtMaxCountInAusgang') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Arbeitsbereich] ADD [ArtMaxCountInAusgang] [int] DEFAULT((0)) NOT NULL; " +
                  "END ";
            return sql;
        }
    }
}
