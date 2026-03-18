using LVS.Constants;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_ASNValue
    {

        public sqlCreater_ASNValue()
        {

        }
        public static string sqlString_ASNValue_VDA4913Open(int myWorkspaceId, int myCallId)
        {
            string strSql = string.Empty;
            strSql = "Select " +
                                "ASNValue.* " +
                                ",ASN.ASNFileTyp " +
                                ", ASNTyp.Typ as Typ " +
                                ", ASNArtSatzFeld.Kennung" +
                                ", ASNArtSatz.Kennung as SatzKennung" +
                                " FROM ASNValue " +
                                "INNER JOIN ASN ON ASN.ID=ASNValue.ASNID " +
                                "INNER JOIN ASNTyp ON ASNTyp.ID=ASN.ASNTypID " +
                                "INNER JOIN ASNArtSatzFeld ON ASNArtSatzFeld.ID = ASNValue.ASNFieldID " +
                                "INNER JOIN ASNArtSatz ON ASNArtSatz.ID =ASNArtSatzFeld.ASNSatzID " +
                                "WHERE " +
                                        "ASN.ASNFileTyp='" + constValue_AsnArt.const_Art_VDA4913 + "'" +
                                        " AND ASN.IsRead=0 ";
            if (myWorkspaceId > 0)
            {
                strSql += " AND ASN.ArbeitsbereichID= " + myWorkspaceId;
            }
            if (myCallId > 0)
            {
                strSql += " AND ASN.ID= " + myCallId;
            }

            strSql += " Order by ASNValue.ID; ";

            return strSql;
        }
    }
}
