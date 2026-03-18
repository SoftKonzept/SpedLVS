namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class Sender
    {
        public const string const_Sender = "#Sender#";

        public static string Execute(Globals._GL_USER myGLUser, clsASN myASN, clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;
            string sql = "select SenderVerweis from ADRVerweis " +
                                                        "where SenderAdrID=" + (int)myASN.Job.AdrVerweisID +
                                                             //" AND VerweisAdrID=" + (int)myLager.Eingang.Empfaenger +
                                                             " AND ArbeitsbereichID= " + (int)myASN.Job.ArbeitsbereichID;


            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                    if (myLager.Eingang is clsLEingang)
                    {
                        sql = sql + " AND VerweisAdrID=" + (int)myLager.Eingang.Empfaenger;
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                case clsASNTyp.const_string_ASNTyp_TSE:
                case clsASNTyp.const_string_ASNTyp_TSL:
                    if (myLager.Artikel.Eingang is clsLEingang)
                    {
                        sql = sql + " AND VerweisAdrID=" + (int)myLager.Artikel.Eingang.Empfaenger;
                    }
                    break;


                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
                    if (myLager.Ausgang is clsLAusgang)
                    {
                        sql = sql + " AND VerweisAdrID=" + (int)myLager.Ausgang.Empfaenger;
                    }
                    break;

                // mr 2020_05_07
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    if (myLager.Ausgang is clsLAusgang)
                    {
                        sql = sql + " AND VerweisAdrID=" + (int)myLager.Eingang.Empfaenger;
                    }
                    break;


            }

            strTmp = LVS.clsSQLcon.ExecuteSQL_GetValue(sql, myGLUser.User_ID);
            return strTmp;
        }
    }
}
