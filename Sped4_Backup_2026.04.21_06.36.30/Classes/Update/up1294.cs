namespace Sped4.Classes.Update
{
    public class up1294
    {
        /// <summary>
        ///             Erweiterung der Table Arbeitsbereich 
        ///             Feld zugewiesene AdrID
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1294 = "1294";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Arbeitsbereich','AdrId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Arbeitsbereich] ADD [AdrId] [int] DEFAULT((0)); " +
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
            sql = "Update Arbeitsbereich Set AdrId=0";
            return sql;
        }
    }
}
