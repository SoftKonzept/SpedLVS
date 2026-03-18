namespace Sped4.Classes.Update
{
    public class up1293
    {
        /// <summary>
        ///             damit eine Unterscheidung der Texte für die einzelnen Arbeitsbereich erreicht wird
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1293 = "1293";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('Tarife','CalcToll') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [Tarife] ADD [CalcToll] [bit] DEFAULT((0)); " +
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
            sql = "Update Tarife Set " +
                        "CalcToll=0";
            return sql;
        }
    }
}
