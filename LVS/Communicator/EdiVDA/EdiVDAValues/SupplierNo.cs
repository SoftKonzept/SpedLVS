using LVS.Constants;
using System;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class SupplierNo
    {
        public const string const_SupplierNo = "#SupplierNo#";
        public const string const_Lieferantennummer = "#Lieferantennummer#";
        public static string Execute(Globals._GL_USER myGLUser, clsLagerdaten myLager, clsASN myASN)
        {
            string strRetrun = string.Empty;
            string strTmp = string.Empty;
            string sql = string.Empty;

            if (myASN.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_VDA4913))
            {
                sql = "select SupplierNo from ADRVerweis " +
                        "where " +
                            "SenderAdrID=" + (Int32)myASN.Job.AdrVerweisID +
                            " AND VerweisArt= 'RECEIVER'";

                if (
                            (myASN.Job.AsnTyp.Typ.Equals(clsASNTyp.const_string_ASNTyp_RLE)) ||
                            (myASN.Job.AsnTyp.Typ.Equals(clsASNTyp.const_string_ASNTyp_RLL))


                    )
                {
                    // mr 2020_05_07
                    // RL ist im Ausgang Empfänger = Auftraggeber kann aber in der Abfrage so nicht 
                    // übernommen werden es muss dann Auftraggeber und Empfänger aus dem Eingang 
                    // verwendet werden, damit die korrekten Werte ermittelt werden

                    sql += " AND VerweisAdrID=" + (Int32)myLager.Artikel.Eingang.Empfaenger +
                           " AND ArbeitsbereichID= " + (int)myLager.Artikel.Eingang.AbBereichID;
                }
                else
                {
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
                }
            }
            //if (myASN.ASNArt.Typ.Equals(clsASNArt.const_Art_EdifactVDA4987))
            else
            {

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
            }
            strTmp = LVS.clsSQLcon.ExecuteSQL_GetValue(sql, myGLUser.User_ID);
            strRetrun = strTmp;
            return strRetrun;
        }
    }
}
