namespace LVS.Clients
{
    public class szg_ctrUmbuchung_Customize_SetLieferantenNr
    {
        internal const int const_Arbeitsbereich_VW = 1;
        internal const int const_Arbeitsbereich_BMW = 5;
        public static string Execute(ref clsSystem mySytem, clsUmbuchung myUB)
        {
            string strRet = string.Empty;
            switch ((int)mySytem.AbBereich.ID)
            {
                case const_Arbeitsbereich_VW:
                    strRet = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(myUB.EmpfaengerID,
                                                                               myUB.EmpfaengerID,
                                                                               mySytem.BenutzerID,
                                                                               enumASNFileTyp.VDA4913.ToString(),
                                                                               mySytem.AbBereich.ID);
                    break;

                case const_Arbeitsbereich_BMW:
                    if ((myUB.Artikel.Eingang is clsLEingang) && (myUB.Artikel.Eingang.LEingangTableID > 0))
                    {
                        if (
                                (myUB.Artikel.Eingang.Lieferant != null) &&
                                (!myUB.Artikel.Eingang.Lieferant.Equals(string.Empty))
                            )
                        {
                            strRet = myUB.Artikel.Eingang.Lieferant;
                        }
                        else
                        {
                            strRet = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(myUB.Artikel.Eingang.Auftraggeber,
                                                               myUB.Artikel.Eingang.Empfaenger,
                                                               mySytem.BenutzerID,
                                                               enumASNFileTyp.VDA4913.ToString(),
                                                               mySytem.AbBereich.ID);
                        }
                    }
                    else
                    {
                        strRet = clsADRVerweis.GetSupplierNoBySenderAndReceiverAdr(myUB.AuftraggeberAltID,
                                                                                   myUB.EmpfaengerID,
                                                                                   mySytem.BenutzerID,
                                                                                   enumASNFileTyp.VDA4913.ToString(),
                                                                                   mySytem.AbBereich.ID);
                    }
                    break;

                default:

                    break;
            }

            return strRet;
        }
    }
}
