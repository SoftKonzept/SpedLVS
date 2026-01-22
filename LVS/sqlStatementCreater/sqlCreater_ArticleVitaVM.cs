using LVS.Models;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_ArticleVitaVM
    {
        internal ArticleVita artVita;
        public sqlCreater_ArticleVitaVM(ArticleVita myArtVita)
        {
            artVita = myArtVita;
        }

        public string sql_Add
        {
            get
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung" +
                                    ") " +
                                    "VALUES (" + artVita.TableId +
                                             ", '" + artVita.TableName + "'" +
                                             ", '" + artVita.Action + "'" +
                                             ", '" + artVita.Date + "'" +
                                             ", " + artVita.UserId +
                                             ", '" + artVita.Description + "'" +
                                             "); ";
                return strSql;
            }
        }
    }

}
