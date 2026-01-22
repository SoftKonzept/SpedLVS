namespace Communicator.Classes
{
    public class up1038

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1038 = "1038";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql =

                "IF COL_LENGTH('EdiSegment','tmpId') IS NULL " +
                "BEGIN " +
                   "ALTER TABLE [EdiSegment] ADD [tmpId] [int] NULL;" +
                "END " +
                "IF COL_LENGTH('EdiSegment','Storable') IS NULL " +
                "BEGIN " +
                  "ALTER TABLE [EdiSegment] ADD [Storable] [bit] NULL;" +
                "END " +
                "IF COL_LENGTH('EdiSegment','Code') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiSegment] ADD [Code] [nvarchar](100) NULL;" +
                  "END " +
                  "IF COL_LENGTH('EdiSegment','SortId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiSegment] ADD [SortId] [int] NOT NULL DEFAULT(0);" +
                  "END " +


                  "IF COL_LENGTH('EdiSegmentElement','tmpId') IS NULL " +
                   "BEGIN " +
                     "ALTER TABLE [EdiSegmentElement] ADD [tmpId] [int] NULL;" +
                "  END " +
                  "IF COL_LENGTH('EdiSegmentElement','Code') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiSegmentElement] ADD [Code] [nvarchar](100) NULL;" +
                  "END " +
                  "IF COL_LENGTH('EdiSegmentElement','SortId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiSegmentElement] ADD [SortId] [int] NOT NULL DEFAULT(0);" +
                  "END " +


                  "IF COL_LENGTH('EdiSegmentElementField','Code') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiSegmentElementField] ADD [Code] [nvarchar](100) NULL;" +
                  "END " +
                  "IF COL_LENGTH('EdiSegmentElementField','SortId') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiSegmentElementField] ADD [SortId] [int] NOT NULL DEFAULT(0);" +
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
            //sql = "Update Jobs SET " +
            //            "CheckCloneFilePath ='' " +
            //            ", CheckCloneFileName='' " +
            //            ", CheckCloneFile = 0 ";
            return sql;
        }
    }
}
