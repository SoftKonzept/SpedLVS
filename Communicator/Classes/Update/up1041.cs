namespace Communicator.Classes
{
    public class up1041

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1041 = "1041";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('VDAClientOUT','Kennung') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [VDAClientOUT] ADD [Kennung] [nvarchar](100) NULL;" +
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
            sql = "Update VDAClientOUT SET " +
            "Kennung = (SELECT Kennung FROM EdiSegmentElementField where Id=ASNFieldID) ";

            return sql;
        }
    }
}
