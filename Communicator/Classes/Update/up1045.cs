namespace Communicator.Classes
{
    public class up1045

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1045 = "1045";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF COL_LENGTH('EdiSegment','IsActive') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiSegment] ADD [IsActive] [bit] DEFAULT(1);" +
                  "END " +
                  "IF COL_LENGTH('EdiSegment','EdiSegmentCheckFunction') IS NULL " +
                  "BEGIN " +
                    "ALTER TABLE [EdiSegment] ADD [EdiSegmentCheckFunction] [nvarchar] (254) NULL;" +
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
            sql = "Update EdiSegment " +
                             "SET IsActive = 1 ";

            return sql;
        }
    }
}
