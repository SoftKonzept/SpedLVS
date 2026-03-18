namespace Communicator.Classes
{
    public class up1040

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1040 = "1040";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('EdiSegmentElementField','Kennung') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiSegmentElementField] ADD [Kennung] [nvarchar](100) NULL;" +
                  "END " +
                  "IF COL_LENGTH('EdiSegmentElementField','EdiSegmentId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiSegmentElementField] ADD [EdiSegmentId] [int] NOT NULL DEFAULT(0);" +
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
            sql = "Update EdiSegmentElementField SET " +
            "Kennung = (SELECT Name FROM EdiSegment where Id=(SELECT EdiSegmentId FROM EdiSegmentElement where Id=EdiSemgentElementId)) +' | '+(SELECT Name FROM EdiSegmentElement where Id=EdiSemgentElementId)+' | ' + Shorcut " +
            ", EdiSegmentId =(SELECT EdiSegmentId FROM EdiSegmentElement where Id=EdiSemgentElementId) ";

            return sql;
        }
    }
}
