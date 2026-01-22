namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class DUNSNr
    {

        public const string const_DUNSNrST = "#DUNSNrST#";  // Warenempfänger / Empfänger
        public const string const_DUNSNrFW = "#DUNSNrFW#";  // Spediteur / Transportunternehmen
        public const string const_DUNSNrSE = "#DUNSNrSE#";  // Verkäufer
        public const string const_DUNSNrMS = "#DUNSNrMS#";  // EDI Sender   => hier MS und SF = SZG
        public const string const_DUNSNrSF = "#DUNSNrSF#";  // Warenversender / Lieferant
        public static string Execute(clsLagerdaten myLager, string myDunsNrArt)
        {
            string strTmp = string.Empty;

            switch (myDunsNrArt)
            {
                // EDI Sender
                // Warenversender / Lieferant
                // Spediteur / Transportunternehmen
                case DUNSNr.const_DUNSNrSF:
                case DUNSNr.const_DUNSNrMS:
                case DUNSNr.const_DUNSNrFW:
                    strTmp = myLager.Sys.Client.ADR.DUNSNr.ToString();
                    break;

                // Warenempfänger / Empfänger
                case DUNSNr.const_DUNSNrST:
                    strTmp = myLager.Ausgang.AdrEmpfaenger.DUNSNr.ToString();
                    break;

                // Verkäufer
                case DUNSNr.const_DUNSNrSE:
                    strTmp = myLager.Ausgang.AdrAuftraggeber.DUNSNr.ToString();
                    break;
            }
            return strTmp;
        }
    }
}
