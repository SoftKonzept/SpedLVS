using LVS.Constants;
using System;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class VW_SupplierNo
    {
        public const string const_VW_SupplierNo = "#VW_SupplierNo#";

        public static string Execute(Globals._GL_USER myGLUser, clsLagerdaten myLager, clsASN myASN)
        {
            string strRetrun = string.Empty;
            string strTmp = string.Empty;
            string sql = string.Empty;

            int iLength = 9;

            if (myASN.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_VDA4913))
            {
                iLength = 9;

                sql = "select SupplierNo from ADRVerweis " +
                        "where " +
                            "SenderAdrID=" + (Int32)myASN.Job.AdrVerweisID +
                            " AND VerweisArt= 'RECEIVER'";


                if ((myLager.Ausgang is clsLAusgang) && (myLager.Ausgang.LAusgangTableID > 0))
                {
                    sql += " AND VerweisAdrID=" + (Int32)myLager.Ausgang.Empfaenger +
                          " AND ArbeitsbereichID= " + (int)myLager.Ausgang.AbBereichID;
                }
                else if ((myLager.Artikel.Ausgang is clsLAusgang) && (myLager.Artikel.Ausgang.LAusgangTableID > 0))
                {
                    sql += " AND VerweisAdrID=" + (Int32)myLager.Artikel.Ausgang.Empfaenger +
                          " AND ArbeitsbereichID= " + (int)myLager.Artikel.Ausgang.AbBereichID;
                }
                else if (myLager.Eingang is clsLEingang)
                {
                    sql += " AND VerweisAdrID=" + (Int32)myLager.Eingang.Empfaenger +
                          " AND ArbeitsbereichID= " + (int)myLager.Eingang.AbBereichID;
                }
                else if (myLager.Artikel.Eingang is clsLEingang)
                {
                    sql += " AND VerweisAdrID=" + (Int32)myLager.Artikel.Eingang.Empfaenger +
                          " AND ArbeitsbereichID= " + (int)myLager.Artikel.Eingang.AbBereichID;
                }
                strTmp = LVS.clsSQLcon.ExecuteSQL_GetValue(sql, myGLUser.User_ID);
                strTmp = strTmp.Replace("/", "");
            }
            if (myASN.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_EdifactVDA4987))
            {
                iLength = 10;

                sql = "select SupplierNo from ADRVerweis " +
                        "where " +
                            "VerweisArt= 'RECEIVER'";

                if ((myLager.Artikel.Ausgang is clsLAusgang) && (myLager.Artikel.Ausgang.LAusgangTableID > 0))
                {
                    sql += "AND SenderAdrID=" + (Int32)myLager.Artikel.Ausgang.Auftraggeber +
                          " AND VerweisAdrID=" + (Int32)myLager.Artikel.Ausgang.Empfaenger +
                          " AND ArbeitsbereichID= " + (int)myLager.Artikel.Ausgang.AbBereichID;
                }
                else if (myLager.Artikel.Eingang is clsLEingang)
                {
                    sql += " AND SenderAdrID=" + (Int32)myLager.Artikel.Eingang.Auftraggeber +
                           " AND VerweisAdrID=" + (Int32)myLager.Artikel.Eingang.Empfaenger +
                           " AND ArbeitsbereichID= " + (int)myLager.Artikel.Eingang.AbBereichID;
                }
                strTmp = LVS.clsSQLcon.ExecuteSQL_GetValue(sql, myGLUser.User_ID);
                strTmp = strTmp.Replace("/", "");

            }
            while (strTmp.Length < iLength)
            {
                strTmp = "0" + strTmp;
            }
            strRetrun = strTmp;
            return strRetrun;
        }
    }
}
