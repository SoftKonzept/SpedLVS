using LVS;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Data;

namespace Sped4.Classes.UpdateArchive
{
    public class Aup1313
    {
        /// <summary>
        ///             Archive - Up
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1313 = "1313";
        public static string SqlString()
        {
            System.DateTime tmpDT = new DateTime(1900, 1, 1);
            string sql = string.Empty;
            sql = "IF(" +
                     "EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Images'))" +
                     "BEGIN " +
                      "IF COL_LENGTH('Images','WorkspaceId') IS NULL " +
                      "BEGIN " +
                        "ALTER TABLE [Images] ADD [WorkspaceId] [int] NOT NULL DEFAULT ((0)); " +
                      "END " +
                  "END; ";
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static void SqlStringUpdate_WorkspaceId()
        {
            DataTable dt = ImageViewData.GetRowsWithoutWorkspaceId();
            List<string> sqlList = new List<string>();

            foreach (DataRow r in dt.Rows)
            {
                int iTableId = 0;
                int.TryParse(r["TableID"].ToString(), out iTableId);
                int iId = 0;
                int.TryParse(r["Id"].ToString(), out iId);
                if ((iTableId > 0) && (iId > 0))
                {
                    string sql = "Update Images SET WorkspaceId = (Select CAST(AB_ID as int) FROM SZG_LVS.dbo.Artikel WHERE ID =" + iTableId + ") WHERE Id =" + iId + "; ";
                    clsSQLARCHIVE.ExecuteSQLWithTRANSACTION(sql, "UpdateImages", 1);
                    //sqlList.Add(sql);
                }
            }

            string strSql = string.Empty;
            //foreach (string s in sqlList)
            //{
            //    strSql += s;
            //}
            //return strSql;
        }
    }
}
