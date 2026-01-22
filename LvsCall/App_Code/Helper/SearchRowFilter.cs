using System;

/// <summary>
/// Zusammenfassungsbeschreibung für SearchRowFilter
/// </summary>
public class SearchRowFilter
{
    public SearchRowFilter()
    {
        //
        //TODO: Hier Konstruktorlogik hinzufügen
        //
    }

    public static string GetSearchstring(string SearchDataField, string SearchValue, string Searchstring)
    {
        string strReturnString = string.Empty;
        decimal decDicke = 0;
        decimal decBreite = 0;

        if (
            (SearchValue != null) &&
            (!SearchValue.Equals(string.Empty))
           )
        {
            try
            {
                switch (SearchDataField)
                {
                    case "LVSNr":
                        //strReturnString = Searchstring + SearchDataField + " >= " + SearchValue;
                        strReturnString = Searchstring + SearchDataField + " = " + SearchValue;
                        break;
                    case "Produktionsnummer":
                    case "Charge":
                    case "Werksnummer":
                        strReturnString = Searchstring + SearchDataField + " LIKE '" + SearchValue + "*'";
                        break;

                    case "Materialnummer":
                        strReturnString = Searchstring + " Werksnummer LIKE '" + SearchValue + "*'";
                        break;

                    case "Benennung":
                        strReturnString = Searchstring + " Gut LIKE '" + SearchValue + "*'";
                        break;

                    case "Abmessung(Dicke-Breite)":
                        // SearchValue zerlegen üfr Dicke und Breite 
                        // Aufbau  Dicke-Breite
                        decDicke = 0;
                        decBreite = 0;
                        if (SearchValue.Length > 0)
                        {
                            string strTmp = string.Empty;
                            if (SearchValue.Contains("-"))
                            {
                                //--Dicke ermitteln
                                strTmp = SearchValue.Substring(0, SearchValue.IndexOf('-'));
                                Decimal.TryParse(strTmp, out decDicke);
                                strReturnString = Searchstring + " Dicke = " + decDicke.ToString().Replace(',', '.');

                                //--Breite ermitteln
                                strTmp = string.Empty;
                                int iFrom = SearchValue.IndexOf('-');
                                int iTo = SearchValue.Length;
                                if (iFrom + 1 < iTo)
                                {
                                    strTmp = SearchValue.Substring(iFrom + 1, (iTo - iFrom) - 1);
                                    Decimal.TryParse(strTmp, out decBreite);
                                    strReturnString = strReturnString + Searchstring + " AND Breite=" + decBreite.ToString().Replace(',', '.');
                                }
                            }
                            else
                            {
                                strTmp = SearchValue;
                                Decimal.TryParse(strTmp, out decDicke);
                                strReturnString = Searchstring + " Dicke = " + decDicke.ToString().Replace(',', '.');
                            }
                        }
                        break;
                    case "Dicke":
                        // SearchValue zerlegen üfr Dicke und Breite 
                        decDicke = 0;
                        if (SearchValue.Length > 0)
                        {
                            string strTmp = string.Empty;
                            strTmp = SearchValue;
                            Decimal.TryParse(strTmp, out decDicke);
                            if (decDicke > 0)
                            {
                                strReturnString = Searchstring + " Dicke = " + decDicke.ToString().Replace(',', '.');
                            }
                        }
                        break;
                    case "Breite":
                        // SearchValue zerlegen üfr Dicke und Breite 
                        decBreite = 0;
                        if (SearchValue.Length > 0)
                        {
                            string strTmp = string.Empty;
                            strTmp = SearchValue;
                            Decimal.TryParse(strTmp, out decBreite);
                            if (decBreite > 0)
                            {
                                strReturnString = Searchstring + " Breite = " + decBreite.ToString().Replace(',', '.');
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.ToString();
            }
        }
        return strReturnString;
    }

}