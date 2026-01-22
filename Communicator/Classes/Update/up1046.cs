namespace Communicator.Classes
{
    public class up1046

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1046 = "1046";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EdiDelforD97AValue]') AND type in (N'U')) " +
                 "BEGIN " +
                    "CREATE TABLE [dbo].[EdiDelforD97AValue](" +
                    "[Id][int] IDENTITY(1, 1) NOT NULL," +
                    "[DocumentDate] [datetime] NULL," +
                    "[DocumentNo][int] NULL," +
                    "[DeliveryScheduleNumber] [int] null," +
                    "[Position][int] NULL," +
                    "[Client] [int] NULL, " +
                    "[Supplier][int] NULL," +
                    "[Recipient][int] NULL," +
                    "[Werksnummer][nvarchar] (50) NULL," +
                    "[OrderNo][nvarchar] (50) NULL," +
                    "[CumQuantityReceived][int] NULL," +
                    "[CumQuantityStartDate][datetime] NULL," +
                    "[ReceivedQuantity][int] NULL," +
                    "[SID][nvarchar] (50) NULL," +
                    "[GoodReceiptDate][datetime] NULL," +
                    "[SchedulingConditions][int] NULL," +
                    "[CallQuantity][int] NULL," +
                    "[DeliveryDate][datetime] NULL," +
                    "[IsActive] [bit] NULL," +
                    "[WorkspaceId][int] NULL," +
                    "[Description] [nvarchar](254) NULL," +
                    "CONSTRAINT[PK_EdiDelforD97AValue] PRIMARY KEY CLUSTERED([Id] ASC" +

                    ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY] " +
                    ") ON[PRIMARY]" +
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
            //sql = "Update EdiSegment " +
            //                 "SET IsActive = 1 ";                             

            return sql;
        }
    }
}
