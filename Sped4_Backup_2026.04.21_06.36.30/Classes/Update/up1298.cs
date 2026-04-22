namespace Sped4.Classes.Update
{
    public class up1298
    {
        /// <summary>
        ///             Table Artikel um das Datenfeld Glühdatum  erweitern
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1298 = "1298";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Artikel','GlowDate') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Artikel] ADD [GlowDate] [datetime2] DEFAULT ('01.01.1900'); " +
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
            //sql = " Update Artikel SET Glowdate = 0 ";            
            return sql;
        }
    }
}
