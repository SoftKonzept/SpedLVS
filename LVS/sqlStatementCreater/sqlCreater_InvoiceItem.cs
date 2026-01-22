using LVS.Models;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_InvoiceItem
    {
        public sqlCreater_InvoiceItem()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myInvoice"></param>
        /// <returns></returns>
        public static string sql_GetInvoiceItems(Invoices myInvoice)
        {
            string strSql = string.Empty;
            strSql = "Select a.* FROM RGPositionen a WHERE a.RGTableID=" + myInvoice.Id + "; ";
            return strSql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myInvoice"></param>
        /// <returns></returns>
        public static string sql_GetInvoiceItem(int myInvoiceItemId)
        {
            string strSql = string.Empty;
            strSql = "Select a.* FROM RGPositionen a WHERE a.ID=" + myInvoiceItemId + "; ";
            return strSql;
        }



    }
}
