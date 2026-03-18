namespace Communicator.Classes
{
    public class up1042

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1042 = "1042";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('EdiSegmentElementField','ASNArtId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiSegmentElementField] ADD [ASNArtId] [int] NOT NULL DEFAULT(0);" +
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
            sql = "Update EdiSegmentElementField " +
                             "SET ASNArtId = ISNULL((SELECT x.ASNArtId FROM EdiSegment x where x.Id = EdiSegmentId),0); ";

            return sql;
        }
    }
}
