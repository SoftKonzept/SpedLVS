namespace Sped4.Classes.Update
{
    public class up1295
    {
        /// <summary>
        ///             Table LEingang um das Datenfeld Umlagerung (bit) erweitern
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1295 = "1295";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('LEingang','Verlagerung') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [LEingang] ADD [Verlagerung] [bit] DEFAULT((0)); " +
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
            sql = "Update LEingang Set Verlagerung=0";
            return sql;
        }
    }
}
