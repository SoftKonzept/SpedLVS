using System.Collections.Generic;

namespace LVS
{
    public class clsINIDocuments
    {
        //public const string const_DocumentArt_Eingang = "Eingang";
        //public const string const_DocumentArt_Ausgang = "Ausgang";
        //public const string const_DocumentArt_RG = "Rechnung";
        //public const string const_DocumentArt_Mail = "Mail";


        //Die Key geben die Keynamen in der INI-Datei an
        public const string const_Key_LabelAll = "LabelAll";
        public const string const_View_LabelAll = "alle Artikellable";

        public const string const_Key_LabelOne = "LabelOne";
        public const string const_View_LabelOne = "Artikellabel";

        public const string const_Key_SPLLabel = "SPLLabel";
        public const string const_View_SPLLabel = "SPL-Artikellabel";

        public const string const_Key_SchadenLable = "SchadenLable";
        public const string const_View_SchadenLable = "Schaden-Artikellable";

        public const string const_Key_SPLDoc = "SPLDoc";
        public const string const_View_SPLDoc = "SPL-Meldung";

        public const string const_Key_SchadenDoc = "SchadenDoc";
        public const string const_View_SchadenDoc = "Schaden-Meldung";

        public const string const_Key_EingangAnzeige = "EingangAnzeige";
        public const string const_View_EingangAnzeige = "Eingangsanzeige";

        public const string const_Key_EingangLfs = "EingangLfs";
        public const string const_View_EingangLfs = "Eingangslieferschein";

        public const string const_Key_Eingangsliste = "Eingangsliste";
        public const string const_View_Eingangsliste = "Eingangsliste";

        public const string const_Key_EingangAnzeigeMail = "EingangAnzeigeMail";
        public const string const_View_EingangAnzeigeMail = "EingangAnzeigeMail";

        public const string const_Key_EingangAnzeigePerDayMail = "EingangAnzeigePerDayMail";
        public const string const_View_EingangAnzeigePerDayMail = "EingangAnzeigePerDayMail";

        public const string const_Key_EingangLfsMail = "EingangLfsMail";
        public const string const_View_EingangLfsMail = "EingangLfsMail";

        public const string const_Key_EingangDocMatKunden = "EingangDocMatKunden";
        public const string const_View_EingangDocMatKunden = "EingangDocMatKunden";

        public const string const_Key_AusgangDoc = "AusgangDoc";
        public const string const_View_AusgangDoc = "Ausgangsdokument";

        public const string const_Key_AusgangAnzeige = "AusgangAnzeige";
        public const string const_View_AusgangAnzeige = "Ausgangsanzeige";

        public const string const_Key_AusgangLfs = "AusgangLfs";
        public const string const_View_AusgangLfs = "Ausgangslieferschein";

        public const string const_Key_Ausgangsliste = "Ausgangsliste";
        public const string const_View_Ausgangsliste = "Ausgangsliste";

        public const string const_Key_AusgangAnzeigeMail = "AusgangAnzeigeMail";
        public const string const_View_AusgangAnzeigeMail = "AusgangAnzeigeMail";

        public const string const_Key_AusgangAnzeigePerDayMail = "AusgangAnzeigePerDayMail";
        public const string const_View_AusgangAnzeigePerDayMail = "AusgangAnzeigePerDayMail";

        public const string const_Key_AusgangLfsMail = "AusgangLfsMail";
        public const string const_View_AusgangLfsMail = "AusgangLfsMail";

        public const string const_Key_CMRFrachtbrief = "CMRFrachtbrief";
        public const string const_View_CMRFrachtbrief = "CMR-Frachtbrief";

        public const string const_Key_KVOFrachtbrief = "KVOFrachtbrief";
        public const string const_View_KVOFrachtbrief = "KVOFrachtbrief";

        public const string const_Key_Lagerrechnung = "Lagerrechnung";
        public const string const_View_Lagerrechnung = "Lagerrechnung";

        public const string const_Key_Manuellerechnung = "Manuellerechnung";
        public const string const_View_Manuellerechnung = "Manuellerechnung";

        public const string const_Key_ManuelleGutschrift = "ManuelleGutschrift";
        public const string const_View_ManuelleGutschrift = "ManuelleGutschrift";

        public const string const_Key_RGAnhang = "RGAnhang";
        public const string const_View_RGAnhang = "RGAnhang";

        public const string const_Key_RGBuch = "RGBuch";
        public const string const_View_RGBuch = "RGBuch";

        public const string const_Key_LagerrechnungMail = "LagerrechnungMail";
        public const string const_View_LagerrechnungMail = "LagerrechnungMail";

        public const string const_Key_ManuellerechnungMail = "ManuellerechnungMail";
        public const string const_View_ManuellerechnungMail = "ManuellerechnungMail";


        private Dictionary<string, string> _DictINIDokuments;
        public Dictionary<string, string> DictINIDokuments
        {
            get { return _DictINIDokuments; }
            set { _DictINIDokuments = value; }
        }

        private Dictionary<string, string> _DictINIEingangsDocuments;
        public Dictionary<string, string> DictINIEingangsDocuments
        {
            get { return _DictINIEingangsDocuments; }
            set { _DictINIEingangsDocuments = value; }
        }

        private Dictionary<string, string> _DictINIAusgangsDocuments;
        public Dictionary<string, string> DictINIAusgangsDocuments
        {
            get { return _DictINIAusgangsDocuments; }
            set { _DictINIAusgangsDocuments = value; }
        }

        /************************************************************************************
         *                      Procedure / Methoden
         * *********************************************************************************/
        ///<summary>clsINIDocuments / InitClass</summary>
        ///<remarks>strClientMatchCode kommt hier aus der Ini</remarks>
        public void InitClass()
        {
            FillDictionary();
        }
        ///<summary>clsINIDocuments / InitClass</summary>
        ///<remarks></remarks>
        private void FillDictionary()
        {
            this.DictINIDokuments = new Dictionary<string, string>();
            //...|Eingangsdokumente
            DictINIDokuments.Add(const_Key_LabelAll, const_View_LabelAll);
            DictINIDokuments.Add(const_Key_LabelOne, const_View_LabelOne);
            DictINIDokuments.Add(const_Key_SPLLabel, const_View_SPLLabel);
            DictINIDokuments.Add(const_Key_SchadenLable, const_View_SchadenLable);
            DictINIDokuments.Add(const_Key_SPLDoc, const_View_SPLDoc);
            DictINIDokuments.Add(const_Key_SchadenDoc, const_View_SchadenDoc);
            DictINIDokuments.Add(const_Key_EingangAnzeige, const_View_EingangAnzeige);
            DictINIDokuments.Add(const_Key_EingangLfs, const_View_EingangLfs);
            DictINIDokuments.Add(const_Key_Eingangsliste, const_View_Eingangsliste);
            DictINIDokuments.Add(const_Key_EingangAnzeigeMail, const_View_EingangAnzeigeMail);
            DictINIDokuments.Add(const_Key_EingangAnzeigePerDayMail, const_View_EingangAnzeigePerDayMail);
            DictINIDokuments.Add(const_Key_EingangLfsMail, const_View_EingangLfsMail);
            DictINIDokuments.Add(const_Key_EingangDocMatKunden, const_View_EingangDocMatKunden);


            //...|Ausgangsdokumente
            DictINIDokuments.Add(const_Key_AusgangDoc, const_View_AusgangDoc);
            DictINIDokuments.Add(const_Key_AusgangAnzeige, const_View_AusgangAnzeige);
            DictINIDokuments.Add(const_Key_AusgangLfs, const_View_AusgangLfs);
            DictINIDokuments.Add(const_Key_Ausgangsliste, const_View_Ausgangsliste);
            DictINIDokuments.Add(const_Key_AusgangAnzeigeMail, const_View_AusgangAnzeigeMail);
            DictINIDokuments.Add(const_Key_AusgangAnzeigePerDayMail, const_View_AusgangAnzeigePerDayMail);
            DictINIDokuments.Add(const_Key_CMRFrachtbrief, const_View_CMRFrachtbrief);
            DictINIDokuments.Add(const_Key_KVOFrachtbrief, const_View_KVOFrachtbrief);

            //....|Rechnungsdokumente
            DictINIDokuments.Add(const_Key_Lagerrechnung, const_View_Lagerrechnung);
            DictINIDokuments.Add(const_Key_Manuellerechnung, const_View_Manuellerechnung);
            DictINIDokuments.Add(const_Key_ManuelleGutschrift, const_View_ManuelleGutschrift);
            DictINIDokuments.Add(const_Key_RGAnhang, const_View_RGAnhang);
            DictINIDokuments.Add(const_Key_RGBuch, const_View_RGBuch);
            DictINIDokuments.Add(const_Key_LagerrechnungMail, const_View_LagerrechnungMail);
            DictINIDokuments.Add(const_Key_ManuellerechnungMail, const_View_ManuellerechnungMail);

            FillDictByClsMainDict(this.DictINIDokuments);

        }
        ///<summary>clsINIDocuments / FillDictByClsMainDict</summary>
        ///<remarks></remarks>
        public void FillDictByClsMainDict(Dictionary<string, string> myDict)
        {
            //...|Einlagerungsdokumente
            FillDictEingangDocuments(this.DictINIDokuments);
            //...|Ausgangsdokumente
            FillDictAusgangDocuments(this.DictINIDokuments);
        }
        ///<summary>clsINIDocuments / FillDictEingangDocuments</summary>
        ///<remarks></remarks>
        private void FillDictEingangDocuments(Dictionary<string, string> myDict)
        {
            this.DictINIEingangsDocuments = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in myDict)
            {
                switch (pair.Key)
                {
                    //Ausgangsdokumente
                    case clsINIDocuments.const_Key_LabelAll:
                    case clsINIDocuments.const_Key_LabelOne:
                    case clsINIDocuments.const_Key_SPLLabel:
                    case clsINIDocuments.const_Key_SchadenLable:
                    case clsINIDocuments.const_Key_SPLDoc:
                    case clsINIDocuments.const_Key_SchadenDoc:
                    case clsINIDocuments.const_Key_EingangAnzeige:
                    case clsINIDocuments.const_Key_EingangLfs:
                    case clsINIDocuments.const_Key_Eingangsliste:
                    case clsINIDocuments.const_Key_EingangAnzeigeMail:
                    case clsINIDocuments.const_Key_EingangAnzeigePerDayMail:
                    case clsINIDocuments.const_Key_EingangLfsMail:
                    case clsINIDocuments.const_Key_EingangDocMatKunden:
                        DictINIEingangsDocuments.Add(pair.Key, pair.Value.ToString());
                        break;
                    default:
                        break;
                }
            }
        }
        ///<summary>clsINIDocuments / FillDictAusgangDocuments</summary>
        ///<remarks></remarks>
        private void FillDictAusgangDocuments(Dictionary<string, string> myDict)
        {
            this.DictINIAusgangsDocuments = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in myDict)
            {
                switch (pair.Key)
                {
                    //Ausgangsdokumente
                    case clsINIDocuments.const_Key_AusgangDoc:
                    case clsINIDocuments.const_Key_AusgangAnzeige:
                    case clsINIDocuments.const_Key_AusgangLfs:
                    case clsINIDocuments.const_Key_Ausgangsliste:
                    case clsINIDocuments.const_Key_AusgangAnzeigeMail:
                    case clsINIDocuments.const_Key_AusgangAnzeigePerDayMail:
                    case clsINIDocuments.const_Key_CMRFrachtbrief:
                    case clsINIDocuments.const_Key_KVOFrachtbrief:
                        DictINIAusgangsDocuments.Add(pair.Key, pair.Value.ToString());
                        break;
                    default:
                        break;
                }
            }
        }

        //private Dictionary<string, string> _DictINIRGDocuments;
        //public Dictionary<string, string> DictINIRGDocuments
        //{
        //    get
        //    {
        //        _DictINIRGDocuments = new Dictionary<string, string>();

        //        ////...|Eingangsdokumente
        //        //_DictINIEingangsDocuments.Add(const_Key_LabelAll, const_View_LabelAll);
        //        //_DictINIEingangsDocuments.Add(const_Key_LabelOne, const_View_LabelOne);
        //        //_DictINIEingangsDocuments.Add(const_Key_SPLLabel, const_View_SPLLabel);
        //        //_DictINIEingangsDocuments.Add(const_Key_SchadenLable, const_View_SchadenLable);
        //        //_DictINIEingangsDocuments.Add(const_Key_SPLDoc, const_View_SPLDoc);
        //        //_DictINIEingangsDocuments.Add(const_Key_SchadenDoc, const_View_SchadenDoc);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangAnzeige, const_View_EingangAnzeige);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangLfs, const_View_EingangLfs);
        //        //_DictINIEingangsDocuments.Add(const_Key_Eingangsliste, const_View_Eingangsliste);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangAnzeigeMail, const_View_EingangAnzeigeMail);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangAnzeigePerDayMail, const_View_EingangAnzeigePerDayMail);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangLfsMail, const_View_EingangLfsMail);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangDocMatKunden, const_View_EingangDocMatKunden);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangLfsMail, const_View_EingangLfsMail);

        //        //...|Ausgangsdokumente
        //        //_DictINIAusgangsDocuments.Add(const_Key_AusgangDoc, const_View_AusgangDoc);
        //        //_DictINIAusgangsDocuments.Add(const_Key_AusgangAnzeige, const_View_AusgangAnzeige);
        //        //_DictINIAusgangsDocuments.Add(const_Key_AusgangLfs, const_View_AusgangLfs);
        //        //_DictINIAusgangsDocuments.Add(const_Key_Ausgangsliste, const_View_Ausgangsliste);
        //        //_DictINIAusgangsDocuments.Add(const_Key_AusgangAnzeigeMail, const_View_AusgangAnzeigeMail);
        //        //_DictINIAusgangsDocuments.Add(const_Key_AusgangAnzeigePerDayMail, const_View_AusgangAnzeigePerDayMail);
        //        //_DictINIAusgangsDocuments.Add(const_Key_CMRFrachtbrief, const_View_CMRFrachtbrief);
        //        //_DictINIAusgangsDocuments.Add(const_Key_KVOFrachtbrief, const_View_KVOFrachtbrief);

        //        //....|Rechnungsdokumente
        //        _DictINIRGDocuments.Add(const_Key_Lagerrechnung, const_View_Lagerrechnung);
        //        _DictINIRGDocuments.Add(const_Key_Manuellerechnung, const_View_Manuellerechnung);
        //        _DictINIRGDocuments.Add(const_Key_ManuelleGutschrift, const_View_ManuelleGutschrift);
        //        _DictINIRGDocuments.Add(const_Key_RGAnhang, const_View_RGAnhang);
        //        _DictINIRGDocuments.Add(const_Key_RGBuch, const_View_RGBuch);
        //        _DictINIRGDocuments.Add(const_Key_LagerrechnungMail, const_View_LagerrechnungMail);
        //        _DictINIRGDocuments.Add(const_Key_ManuellerechnungMail, const_View_ManuellerechnungMail);

        //        return _DictINIRGDocuments;
        //    }
        //    set
        //    {
        //        _DictINIRGDocuments = value;
        //    }
        //}


        //private Dictionary<string,string> _DictINIDokuments;
        //public Dictionary<string,string> DictINIDokuments
        //{
        //    get 
        //    {
        //        _DictINIDokuments = new Dictionary<string, string>();

        //        //...|Eingangsdokumente
        //        _DictINIDokuments.Add(const_Key_LabelAll, const_View_LabelAll);
        //        _DictINIDokuments.Add(const_Key_LabelOne, const_View_LabelOne);
        //        _DictINIDokuments.Add(const_Key_SPLLabel, const_View_SPLLabel);
        //        _DictINIDokuments.Add(const_Key_SchadenLable, const_View_SchadenLable);
        //        _DictINIDokuments.Add(const_Key_SPLDoc, const_View_SPLDoc);
        //        _DictINIDokuments.Add(const_Key_SchadenDoc, const_View_SchadenDoc);
        //        _DictINIDokuments.Add(const_Key_EingangAnzeige, const_View_EingangAnzeige);
        //        _DictINIDokuments.Add(const_Key_EingangLfs, const_View_EingangLfs);
        //        _DictINIDokuments.Add(const_Key_Eingangsliste, const_View_Eingangsliste);
        //        _DictINIDokuments.Add(const_Key_EingangAnzeigeMail, const_View_EingangAnzeigeMail);
        //        _DictINIDokuments.Add(const_Key_EingangAnzeigePerDayMail, const_View_EingangAnzeigePerDayMail);
        //        _DictINIDokuments.Add(const_Key_EingangLfsMail, const_View_EingangLfsMail);
        //        _DictINIDokuments.Add(const_Key_EingangDocMatKunden, const_View_EingangDocMatKunden);
        //        _DictINIDokuments.Add(const_Key_EingangLfsMail, const_View_EingangLfsMail);

        //        //...|Ausgangsdokumente
        //        _DictINIDokuments.Add(const_Key_AusgangDoc, const_View_AusgangDoc);
        //        _DictINIDokuments.Add(const_Key_AusgangAnzeige, const_View_AusgangAnzeige);
        //        _DictINIDokuments.Add(const_Key_AusgangLfs, const_View_AusgangLfs);
        //        _DictINIDokuments.Add(const_Key_Ausgangsliste, const_View_Ausgangsliste);
        //        _DictINIDokuments.Add(const_Key_AusgangAnzeigeMail, const_View_AusgangAnzeigeMail);
        //        _DictINIDokuments.Add(const_Key_AusgangAnzeigePerDayMail, const_View_AusgangAnzeigePerDayMail);
        //        _DictINIDokuments.Add(const_Key_CMRFrachtbrief, const_View_CMRFrachtbrief);
        //        _DictINIDokuments.Add(const_Key_KVOFrachtbrief, const_View_KVOFrachtbrief);

        //        //....|Rechnungsdokumente
        //        _DictINIDokuments.Add(const_Key_Lagerrechnung, const_View_Lagerrechnung);
        //        _DictINIDokuments.Add(const_Key_Manuellerechnung, const_View_Manuellerechnung);
        //        _DictINIDokuments.Add(const_Key_ManuelleGutschrift, const_View_ManuelleGutschrift);
        //        _DictINIDokuments.Add(const_Key_RGAnhang, const_View_RGAnhang);
        //        _DictINIDokuments.Add(const_Key_RGBuch, const_View_RGBuch);
        //        _DictINIDokuments.Add(const_Key_LagerrechnungMail, const_View_LagerrechnungMail);
        //        _DictINIDokuments.Add(const_Key_ManuellerechnungMail, const_View_ManuellerechnungMail);

        //        return _DictINIDokuments; 
        //    }
        //    set
        //    {
        //        _DictINIDokuments = value;
        //    }
        //}

        //private Dictionary<string, string> _DictINIEingangsDocuments;
        //public Dictionary<string, string> DictINIEingangsDocuments
        //{
        //    get
        //    {
        //        _DictINIEingangsDocuments = new Dictionary<string, string>();

        //        //...|Eingangsdokumente
        //        _DictINIEingangsDocuments.Add(const_Key_LabelAll, const_View_LabelAll);
        //        _DictINIEingangsDocuments.Add(const_Key_LabelOne, const_View_LabelOne);
        //        _DictINIEingangsDocuments.Add(const_Key_SPLLabel, const_View_SPLLabel);
        //        _DictINIEingangsDocuments.Add(const_Key_SchadenLable, const_View_SchadenLable);
        //        _DictINIEingangsDocuments.Add(const_Key_SPLDoc, const_View_SPLDoc);
        //        _DictINIEingangsDocuments.Add(const_Key_SchadenDoc, const_View_SchadenDoc);
        //        _DictINIEingangsDocuments.Add(const_Key_EingangAnzeige, const_View_EingangAnzeige);
        //        _DictINIEingangsDocuments.Add(const_Key_EingangLfs, const_View_EingangLfs);
        //        _DictINIEingangsDocuments.Add(const_Key_Eingangsliste, const_View_Eingangsliste);
        //        _DictINIEingangsDocuments.Add(const_Key_EingangAnzeigeMail, const_View_EingangAnzeigeMail);
        //        _DictINIEingangsDocuments.Add(const_Key_EingangAnzeigePerDayMail, const_View_EingangAnzeigePerDayMail);
        //        _DictINIEingangsDocuments.Add(const_Key_EingangLfsMail, const_View_EingangLfsMail);
        //        _DictINIEingangsDocuments.Add(const_Key_EingangDocMatKunden, const_View_EingangDocMatKunden);
        //        _DictINIEingangsDocuments.Add(const_Key_EingangLfsMail, const_View_EingangLfsMail);

        //        ////...|Ausgangsdokumente
        //        //_DictINIDokuments.Add(const_Key_AusgangDoc, const_View_AusgangDoc);
        //        //_DictINIDokuments.Add(const_Key_AusgangAnzeige, const_View_AusgangAnzeige);
        //        //_DictINIDokuments.Add(const_Key_AusgangLfs, const_View_AusgangLfs);
        //        //_DictINIDokuments.Add(const_Key_Ausgangsliste, const_View_Ausgangsliste);
        //        //_DictINIDokuments.Add(const_Key_AusgangAnzeigeMail, const_View_AusgangAnzeigeMail);
        //        //_DictINIDokuments.Add(const_Key_AusgangAnzeigePerDayMail, const_View_AusgangAnzeigePerDayMail);
        //        //_DictINIDokuments.Add(const_Key_CMRFrachtbrief, const_View_CMRFrachtbrief);
        //        //_DictINIDokuments.Add(const_Key_KVOFrachtbrief, const_View_KVOFrachtbrief);

        //        ////....|Rechnungsdokumente
        //        //_DictINIDokuments.Add(const_Key_Lagerrechnung, const_View_Lagerrechnung);
        //        //_DictINIDokuments.Add(const_Key_Manuellerechnung, const_View_Manuellerechnung);
        //        //_DictINIDokuments.Add(const_Key_ManuelleGutschrift, const_View_ManuelleGutschrift);
        //        //_DictINIDokuments.Add(const_Key_RGAnhang, const_View_RGAnhang);
        //        //_DictINIDokuments.Add(const_Key_RGBuch, const_View_RGBuch);
        //        //_DictINIDokuments.Add(const_Key_LagerrechnungMail, const_View_LagerrechnungMail);
        //        //_DictINIDokuments.Add(const_Key_ManuellerechnungMail, const_View_ManuellerechnungMail);

        //        return _DictINIEingangsDocuments;
        //    }
        //    set
        //    {
        //        _DictINIEingangsDocuments = value;
        //    }
        //}

        //private Dictionary<string, string> _DictINIAusgangsDocuments;
        //public Dictionary<string, string> DictINIAusgangsDocuments
        //{
        //    get
        //    {
        //        _DictINIAusgangsDocuments = new Dictionary<string, string>();

        //        ////...|Eingangsdokumente
        //        //_DictINIEingangsDocuments.Add(const_Key_LabelAll, const_View_LabelAll);
        //        //_DictINIEingangsDocuments.Add(const_Key_LabelOne, const_View_LabelOne);
        //        //_DictINIEingangsDocuments.Add(const_Key_SPLLabel, const_View_SPLLabel);
        //        //_DictINIEingangsDocuments.Add(const_Key_SchadenLable, const_View_SchadenLable);
        //        //_DictINIEingangsDocuments.Add(const_Key_SPLDoc, const_View_SPLDoc);
        //        //_DictINIEingangsDocuments.Add(const_Key_SchadenDoc, const_View_SchadenDoc);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangAnzeige, const_View_EingangAnzeige);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangLfs, const_View_EingangLfs);
        //        //_DictINIEingangsDocuments.Add(const_Key_Eingangsliste, const_View_Eingangsliste);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangAnzeigeMail, const_View_EingangAnzeigeMail);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangAnzeigePerDayMail, const_View_EingangAnzeigePerDayMail);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangLfsMail, const_View_EingangLfsMail);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangDocMatKunden, const_View_EingangDocMatKunden);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangLfsMail, const_View_EingangLfsMail);

        //        //...|Ausgangsdokumente
        //        _DictINIAusgangsDocuments.Add(const_Key_AusgangDoc, const_View_AusgangDoc);
        //        _DictINIAusgangsDocuments.Add(const_Key_AusgangAnzeige, const_View_AusgangAnzeige);
        //        _DictINIAusgangsDocuments.Add(const_Key_AusgangLfs, const_View_AusgangLfs);
        //        _DictINIAusgangsDocuments.Add(const_Key_Ausgangsliste, const_View_Ausgangsliste);
        //        _DictINIAusgangsDocuments.Add(const_Key_AusgangAnzeigeMail, const_View_AusgangAnzeigeMail);
        //        _DictINIAusgangsDocuments.Add(const_Key_AusgangAnzeigePerDayMail, const_View_AusgangAnzeigePerDayMail);
        //        _DictINIAusgangsDocuments.Add(const_Key_CMRFrachtbrief, const_View_CMRFrachtbrief);
        //        _DictINIAusgangsDocuments.Add(const_Key_KVOFrachtbrief, const_View_KVOFrachtbrief);

        //        ////....|Rechnungsdokumente
        //        //_DictINIDokuments.Add(const_Key_Lagerrechnung, const_View_Lagerrechnung);
        //        //_DictINIDokuments.Add(const_Key_Manuellerechnung, const_View_Manuellerechnung);
        //        //_DictINIDokuments.Add(const_Key_ManuelleGutschrift, const_View_ManuelleGutschrift);
        //        //_DictINIDokuments.Add(const_Key_RGAnhang, const_View_RGAnhang);
        //        //_DictINIDokuments.Add(const_Key_RGBuch, const_View_RGBuch);
        //        //_DictINIDokuments.Add(const_Key_LagerrechnungMail, const_View_LagerrechnungMail);
        //        //_DictINIDokuments.Add(const_Key_ManuellerechnungMail, const_View_ManuellerechnungMail);

        //        return _DictINIAusgangsDocuments;
        //    }
        //    set
        //    {
        //        _DictINIAusgangsDocuments = value;
        //    }
        //} 

        //private Dictionary<string, string> _DictINIRGDocuments;
        //public Dictionary<string, string> DictINIRGDocuments
        //{
        //    get
        //    {
        //        _DictINIRGDocuments = new Dictionary<string, string>();

        //        ////...|Eingangsdokumente
        //        //_DictINIEingangsDocuments.Add(const_Key_LabelAll, const_View_LabelAll);
        //        //_DictINIEingangsDocuments.Add(const_Key_LabelOne, const_View_LabelOne);
        //        //_DictINIEingangsDocuments.Add(const_Key_SPLLabel, const_View_SPLLabel);
        //        //_DictINIEingangsDocuments.Add(const_Key_SchadenLable, const_View_SchadenLable);
        //        //_DictINIEingangsDocuments.Add(const_Key_SPLDoc, const_View_SPLDoc);
        //        //_DictINIEingangsDocuments.Add(const_Key_SchadenDoc, const_View_SchadenDoc);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangAnzeige, const_View_EingangAnzeige);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangLfs, const_View_EingangLfs);
        //        //_DictINIEingangsDocuments.Add(const_Key_Eingangsliste, const_View_Eingangsliste);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangAnzeigeMail, const_View_EingangAnzeigeMail);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangAnzeigePerDayMail, const_View_EingangAnzeigePerDayMail);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangLfsMail, const_View_EingangLfsMail);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangDocMatKunden, const_View_EingangDocMatKunden);
        //        //_DictINIEingangsDocuments.Add(const_Key_EingangLfsMail, const_View_EingangLfsMail);

        //        //...|Ausgangsdokumente
        //        //_DictINIAusgangsDocuments.Add(const_Key_AusgangDoc, const_View_AusgangDoc);
        //        //_DictINIAusgangsDocuments.Add(const_Key_AusgangAnzeige, const_View_AusgangAnzeige);
        //        //_DictINIAusgangsDocuments.Add(const_Key_AusgangLfs, const_View_AusgangLfs);
        //        //_DictINIAusgangsDocuments.Add(const_Key_Ausgangsliste, const_View_Ausgangsliste);
        //        //_DictINIAusgangsDocuments.Add(const_Key_AusgangAnzeigeMail, const_View_AusgangAnzeigeMail);
        //        //_DictINIAusgangsDocuments.Add(const_Key_AusgangAnzeigePerDayMail, const_View_AusgangAnzeigePerDayMail);
        //        //_DictINIAusgangsDocuments.Add(const_Key_CMRFrachtbrief, const_View_CMRFrachtbrief);
        //        //_DictINIAusgangsDocuments.Add(const_Key_KVOFrachtbrief, const_View_KVOFrachtbrief);

        //        //....|Rechnungsdokumente
        //        _DictINIRGDocuments.Add(const_Key_Lagerrechnung, const_View_Lagerrechnung);
        //        _DictINIRGDocuments.Add(const_Key_Manuellerechnung, const_View_Manuellerechnung);
        //        _DictINIRGDocuments.Add(const_Key_ManuelleGutschrift, const_View_ManuelleGutschrift);
        //        _DictINIRGDocuments.Add(const_Key_RGAnhang, const_View_RGAnhang);
        //        _DictINIRGDocuments.Add(const_Key_RGBuch, const_View_RGBuch);
        //        _DictINIRGDocuments.Add(const_Key_LagerrechnungMail, const_View_LagerrechnungMail);
        //        _DictINIRGDocuments.Add(const_Key_ManuellerechnungMail, const_View_ManuellerechnungMail);

        //        return _DictINIRGDocuments;
        //    }
        //    set
        //    {
        //        _DictINIRGDocuments = value;
        //    }
        //}

    }
}
