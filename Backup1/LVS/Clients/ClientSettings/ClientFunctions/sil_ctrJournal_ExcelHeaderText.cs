namespace LVS.Clients
{
    public class sil_ctrJournal_ExcelHeaderText
    {
        /// <summary>
        ///             tbexAuftrag -> Bestellnummer von TATA gespeichert und wird für EDI gebraucht
        /// </summary>
        public static string Execute(string myHeaderText, int myRowIndex)
        {
            string strReturn = string.Empty;
            switch (myRowIndex)
            {
                case 1:
                    switch (myHeaderText)
                    {
                        case "nur Eingänge":
                            strReturn = "Lagereingänge gesamt";
                            break;
                        case "nur Ausgänge":
                            strReturn = "Lagerausgänge";
                            break;
                        case "mit Schaden":
                            strReturn = "Lagereingänge mit Schaden";
                            break;
                        case "ohne Schaden":
                            strReturn = "Lagereingänge ohne Schaden";
                            break;
                        default:
                            strReturn = myHeaderText;
                            break;
                    }
                    break;
                case 2:
                    strReturn = string.Empty;
                    break;
            }
            return strReturn;
        }
    }
}
