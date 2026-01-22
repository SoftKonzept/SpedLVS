using System;
using System.Collections.ObjectModel;
using System.Data;

namespace LVS.ReportClasses
{
    public class clsRepBestand
    {
        public static DataTable dtGridToReport { get; set; }
        public static string View { get; set; }
        public static ObservableCollection<clsReportArtikel> Data { get; set; }
        public static ObservableCollection<clsReportArtikel> GetData()
        {
            ObservableCollection<clsReportArtikel> returnvalue = new ObservableCollection<clsReportArtikel>();
            if (dtGridToReport == null)
            {
                dtGridToReport = new DataTable();
            }
            foreach (DataRow row in dtGridToReport.Rows)
            {
                string strTmp = string.Empty;
                clsReportArtikel tmp = new clsReportArtikel();
                if (row["ArtikelID"] != null)
                {
                    tmp.ID = Int32.Parse(row["ArtikelID"].ToString());
                }
                if (row["LVSNR"] != null)
                {
                    tmp.LVS = Int32.Parse(row["LVSNR"].ToString());
                }
                if (row["Auftraggeber"] != null)
                {
                    tmp.Auftraggeber = row["Auftraggeber"].ToString();
                }
                try
                {
                    if (row["Produktionsnummer"] != null)
                    {
                        tmp.Produktionsnummer = row["Produktionsnummer"].ToString();
                    }
                }
                catch
                {
                    if (row["Prod.-Nr."] != null)
                    {
                        tmp.Produktionsnummer = row["Prod.-Nr."].ToString();
                    }
                }
                try
                {
                    if (row["Werksnummer"] != null)
                    {
                        tmp.Werksnummer = row["Werksnummer"].ToString();
                    }
                }
                catch
                {

                }
                try
                {
                    if (row["Charge"] != null)
                    {
                        tmp.Charge = row["Charge"].ToString();
                    }
                }
                catch { }
                try
                {
                    if (row["Gut"] != null)
                    {
                        tmp.Gut = row["Gut"].ToString();
                    }
                }
                catch { }
                try
                {
                    if (row["Anzahl"] != null)
                    {
                        tmp.Anzahl = Int32.Parse(row["Anzahl"].ToString());
                    }
                }
                catch { }
                try
                {
                    if (row["Netto"] != null)
                    {
                        tmp.Netto = Decimal.Parse(row["Netto"].ToString());
                    }
                }
                catch { }
                try
                {
                    if (row["Brutto"] != null)
                    {
                        tmp.Brutto = Decimal.Parse(row["Brutto"].ToString());
                    }
                }
                catch { }
                try
                {
                    if (row["Dicke"] != null)
                    {
                        tmp.Dicke = Decimal.Parse(row["Dicke"].ToString());
                    }
                }
                catch { }
                try
                {

                    if (row["Breite"] != null)
                    {
                        tmp.Breite = Decimal.Parse(row["Breite"].ToString());
                    }
                    if (row["Laenge"] != null)
                    {
                        tmp.Laenge = Decimal.Parse(row["Laenge"].ToString());
                    }
                }
                catch { }
                try
                {

                    if (row["Reihe"] != null)
                    {
                        tmp.Reihe = row["Reihe"].ToString();
                    }
                }
                catch { }
                try
                {
                    if (row["Platz"] != null)
                    {
                        tmp.Platz = row["Platz"].ToString();
                    }
                }
                catch { }
                try
                {
                    if (row["Eingang"] != null)
                    {
                        tmp.Eingang = Int32.Parse(row["Eingang"].ToString());
                    }
                }
                catch { }
                try
                {
                    if (row["Eingangsdatum"] != null)
                    {
                        tmp.Eingangsdatum = (DateTime)row["Eingangsdatum"];
                    }
                }
                catch { }
                try
                {
                    if (row["Ausgang"] != null)
                    {
                        strTmp = string.Empty;
                        strTmp = row["Ausgang"].ToString();
                        Int32 iAusgang = 0;
                        Int32.TryParse(strTmp, out iAusgang);
                        tmp.Ausgang = iAusgang;
                    }
                }
                catch { }
                try
                {
                    if (row["Ausgangsdatum"] != null)
                    {
                        tmp.Ausgangsdatum = row["Ausgangsdatum"] != System.DBNull.Value ? (DateTime)row["Ausgangsdatum"] : Globals.DefaultDateTimeMinValue;
                    }
                }
                catch { }
                try
                {
                    if (row["Lagerdauer"] != null)
                    {
                        tmp.Lagerdauer = Int32.Parse(row["ArtikelID"].ToString());
                    }
                }
                catch { }
                try
                {
                    if (row["Lieferschein"] != null)
                    {
                        tmp.Lieferschein = row["Lieferschein"].ToString();
                    }
                }
                catch { }
                returnvalue.Add(tmp);

            }

            Data = returnvalue;
            return returnvalue;
        }



    }

    public class ViewMode
    {
        public string Mode
        {
            get { return clsRepBestand.View; }

        }


    }

    public class clsReportArtikel
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int lvs;
        public int LVS
        {
            get { return lvs; }
            set { lvs = value; }
        }

        private string auftraggeber;
        public string Auftraggeber
        {
            get { return auftraggeber; }
            set { auftraggeber = value; }
        }

        private string produktionsnummer;
        public string Produktionsnummer
        {
            get { return produktionsnummer; }
            set { produktionsnummer = value; }
        }

        private string gut;
        public string Gut
        {
            get { return gut; }
            set { gut = value; }
        }

        private int anzahl;
        public int Anzahl
        {
            get { return anzahl; }
            set { anzahl = value; }
        }

        private decimal netto;
        public decimal Netto
        {
            get { return netto; }
            set { netto = value; }
        }

        private decimal brutto;
        public decimal Brutto
        {
            get { return brutto; }
            set { brutto = value; }
        }

        private decimal dicke;
        public decimal Dicke
        {
            get { return dicke; }
            set { dicke = value; }
        }

        private decimal breite;
        public decimal Breite
        {
            get { return breite; }
            set { breite = value; }
        }

        private decimal laenge;
        public decimal Laenge
        {
            get { return laenge; }
            set { laenge = value; }
        }

        private string reihe;
        public string Reihe
        {
            get { return reihe; }
            set { reihe = value; }
        }

        private string platz;
        public string Platz
        {
            get { return platz; }
            set { platz = value; }
        }

        private DateTime eingangsdatum;
        public DateTime Eingangsdatum
        {
            get { return eingangsdatum; }
            set { eingangsdatum = value; }
        }

        private DateTime ausgangsdatum;
        public DateTime Ausgangsdatum
        {
            get { return ausgangsdatum; }
            set { ausgangsdatum = value; }
        }

        private string werksnummer;
        public string Werksnummer
        {
            get { return werksnummer; }
            set { werksnummer = value; }
        }

        private int lagerdauer;
        public int Lagerdauer
        {
            get { return lagerdauer; }
            set { lagerdauer = value; }
        }

        private string charge;
        public string Charge
        {
            get { return charge; }
            set { charge = value; }
        }

        private string exbezeichnung;
        public string ExBezeichnung
        {
            get { return exbezeichnung; }
            set { exbezeichnung = value; }
        }
        private string exMaterialnummer;
        public string ExMaterialnummer
        {
            get { return exMaterialnummer; }
            set { exMaterialnummer = value; }
        }
        private string bestellnummer;
        public string Bestellnummer
        {
            get { return bestellnummer; }
            set { bestellnummer = value; }
        }
        private int eingang;
        public int Eingang
        {
            get { return eingang; }
            set { eingang = value; }
        }
        private int ausgang;
        public int Ausgang
        {
            get { return ausgang; }
            set { ausgang = value; }
        }
        private string bemerkung;
        public string Bemerkung
        {
            get { return bemerkung; }
            set { bemerkung = value; }
        }
        private string waggonnr;
        public string WaggonNr
        {
            get { return waggonnr; }
            set { waggonnr = value; }
        }
        private string lieferschein;
        public string Lieferschein
        {
            get { return lieferschein; }
            set { lieferschein = value; }
        }
        private int gartid;
        public int GArtID
        {
            get { return gartid; }
            set { gartid = value; }
        }
        private string artidref;
        public string ArtIDRef
        {
            get { return artidref; }
            set { artidref = value; }
        }


    }



}
