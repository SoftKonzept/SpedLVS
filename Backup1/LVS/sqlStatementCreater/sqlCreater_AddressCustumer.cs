using Common.Models;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_AddressCustumer
    {
        internal Addresses adr { get; set; } = new Addresses();
        internal AddressCustomer customer { get; set; } = new AddressCustomer();
        public sqlCreater_AddressCustumer(Addresses myAdr)
        {
            adr = myAdr.Copy();
        }
        public sqlCreater_AddressCustumer(AddressCustomer myAdrCustomer)
        {
            customer = myAdrCustomer.Copy();
        }
        /// <summary>
        ///                 sql - Get by AdrId
        /// </summary>
        /// <returns></returns>
        public string sql_GetByAdrId
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM Kunde WHERE ADR_ID=" + adr.Id + ";";
                return strSql;
            }
        }
        /// <summary>
        ///                 sql - Get by Id
        /// </summary>
        /// <returns></returns>
        public string sql_GetById
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM Kunde WHERE ID =" + customer.Id + ";";
                return strSql;
            }
        }



    }
}
