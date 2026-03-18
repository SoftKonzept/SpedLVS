namespace Communicator.Classes
{
    public class up1054

    {
        /// <summary>

        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1054 = "1054";
        public static string SqlString()
        {
            string sql = string.Empty;
            sql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EdiClientWorkspaceValue]') AND type in (N'U')) " +
                  "BEGIN " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EdiAdrWorkspaceAssignment]') AND type in (N'U')) " +
                    "BEGIN " +
                        "exec sp_rename '[dbo].[EdiAdrWorkspaceAssignment]', 'EdiClientWorkspaceValue'; " +
                    "END " +
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
            sql = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EdiClientWorkspaceValue]') AND type in (N'U')) " +
                  "BEGIN " +
                      " INSERT INTO[dbo].[EdiClientWorkspaceValue] ([AdrId], [WorkspaceId], [AsnArtId], [Property], [Value], [Created], [Direction]) " +
                      " SELECT [AdrId],[WorkspaceId], [AsnArtId], [Property], [Value], [Created], [Direction] FROM [EdiAdrWorkspaceAssignment] " +
                  "END ";
            return sql;
        }
    }
}
