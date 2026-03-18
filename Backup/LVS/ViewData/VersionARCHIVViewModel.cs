using Common.Enumerations;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class VersionARCHIVViewModel
    {
        //public Vehicles Vehicle { get; set; }
        //private int BenutzerID { get; set; } = 0;
        //public List<Vehicles> ListVehicles { get; set; }

        public VersionARCHIVViewModel()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string sql_ExistTable()
        {
            string sql = string.Empty;
            string strTableName = enumDatabaseARCHIV_TableNames.Version.ToString();

            sql = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '" + strTableName + "'";
            return sql;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool ExistTable()
        {
            bool bReturn = false;
            string strSql = VersionARCHIVViewModel.sql_ExistTable();
            DataTable dt = clsSQLARCHIVE.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Exist", "ExistVersion", 1);
            bReturn = (dt.Rows.Count > 0);
            return bReturn;
        }


    }
}

