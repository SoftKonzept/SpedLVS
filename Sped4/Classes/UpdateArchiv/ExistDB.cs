using Common.Enumerations;
using LVS.InitValueSped4;

namespace Sped4.Classes.UpdateArchive
{
    public class ExistDB
    {
        /// <summary>
        ///             Archive - Up
        /// </summary>
        /// <returns></returns>
        /// 
        //public const string const_up1308 = "1308";
        public static string SqlString()
        {
            string sql = string.Empty;
            string strMatchCode = InitValueSped4_Client.Matchcode();
            string strDBName = strMatchCode + enumDatabase.ARCHIV.ToString();

            sql = "SELECT name FROM master.sys.databases WHERE name = N'" + strDBName + "'";
            return sql;

        }
    }
}
