using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Import;
using LVS;

namespace Import
{
    public class helper_sql_clsADR
    {
        public static List<clsADR> GetAdrList(clsSystemImport mySysImport)
        {
            List<clsADR>  ListADR = new List<clsADR>();
            string strSql = string.Empty;
            DataTable dt = clsADR.ADRTable();
            foreach (DataRow row in dt.Rows)
            {
                decimal decTmp = 0;
                decimal.TryParse(row["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsADR tmpCls = new clsADR();
                    tmpCls.InitClass(mySysImport.GLUser, mySysImport.GLSystem, decTmp, true);
                    ListADR.Add(tmpCls);
                }
            }
            return ListADR;
        }
    }
}
