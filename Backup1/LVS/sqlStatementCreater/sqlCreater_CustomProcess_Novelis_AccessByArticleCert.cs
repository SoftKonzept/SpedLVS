using LVS.ViewData;
using System.Linq;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_CustomProcess_Novelis_AccessByArticleCert
    {
        internal EingangViewData eingangVD;
        internal string ClientMatchcode = string.Empty;
        public sqlCreater_CustomProcess_Novelis_AccessByArticleCert(EingangViewData myEingangViewData)
        {
            this.eingangVD = myEingangViewData;
            ClientMatchcode = LVS.InitValueCommunicator.InitValueCom_Client.Matchcode();
        }

        /// <summary>
        ///              Check Zertifikat für alle Artikel vorhanden
        /// </summary>
        public string sqlCountArtCert
        {
            get
            {
                string sql = string.Empty;

                sql += "SELECT Count(ID) as Anzahl FROM EdiZQMQalityXml ";
                sql += "where ";
                sql += "ArticleId in (" + string.Join(",", eingangVD.ListArticleInEingang.Select(x => x.Id).ToList()) + ") ";
                sql += "and Produktionsnummer in (";
                sql += "SELECT Produktionsnummer FROM " + ClientMatchcode + "LVS.dbo.Artikel where ID in (" + string.Join(",", eingangVD.ListArticleInEingang.Select(x => x.Id).ToList()) + ")) ";
                sql += "and LfsNr in (";
                sql += "SELECT LfsNr FROM SZG_LVS.dbo.LEingang where ID in (";
                sql += "SELECT LEingangTableID FROM " + ClientMatchcode + "LVS.dbo.Artikel where ID in (" + string.Join(",", eingangVD.ListArticleInEingang.Select(x => x.Id).ToList()) + "))) ";

                return sql;
            }
        }

        /// <summary>
        ///              gleichzeitig Artikel nicht mehr im SPL
        /// </summary>
        public string sqlCountArtSplCert
        {
            get
            {
                string sql = string.Empty;

                sql = string.Empty;
                sql += "SELECT Count(ID) as Anzahl FROM SZG_LVS.dbo.Sperrlager ";
                sql += "WHERE ";
                sql += "BKZ = 'IN' ";
                sql += "AND IsCustomCertificateMissing=1 ";
                sql += "AND ID NOT IN (SELECT DISTINCT SPLIDIn FROM SZG_LVS.dbo.Sperrlager WHERE SPLIDIn>0) ";
                sql += "AND ArtikelID IN (" + string.Join(",", eingangVD.ListArticleInEingang.Select(x => x.Id).ToList()) + ") ";

                return sql;
            }
        }
    }
}
