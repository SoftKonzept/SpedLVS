namespace Communicator.Classes
{
    public class up1039

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1039 = "1039";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('EdiSegmentElement','Kennung') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiSegmentElement] ADD [Kennung] [nvarchar](100) NULL;" +
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
            sql = "Update EdiSegmentElement SET " +
                        "Kennung = (SELECT Name FROM EdiSegment where Id=EdiSegmentId) +' | '+ Name  ";
            return sql;
        }
    }
}
