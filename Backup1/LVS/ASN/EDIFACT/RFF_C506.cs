using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class RFF_C506
    {
        internal clsEdiSegmentElement SegElement;
        internal RFF RFF;
        public const string Kennung = "C506";

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        ///             Einzelheiten zu Maßangaben
        /// </summary>
        /// <param name="mySegElement"></param>
        public RFF_C506(RFF myEdiElement)
        {
            this.RFF = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;

            if (this.SegElement.ID.Equals(1407))
            {
                string st = string.Empty;
            }
            CreateValue();
        }



        public const string f_1153_selVal_CRN = "CRN";

        /// <summary>       
        ///                 AAA Auftragsbestätigungsnummer
        ///                 AAD Dangerous goods technical name
        ///                 AAN Lieferabrufs-/plannummer
        ///                 AAO Sendungsreferenznummer des Empfängers
        ///                 AAU Lieferscheinnummer
        ///                 AKI Ordering customer's second reference number
        ///                 ANK Reference number assigned by third party
        ///                 CRN Reisenummer
        ///                 COF Call off order number
        ///                 HAZ Hazard information
        ///                 IV Rechnungsnummer
        ///                 ON Auftragsnummer (Einkauf)
        ///                 TIN Transport instruction number
        ///                 UC Ultimate customer's reference number
        /// </summary>
        /// 
        internal const string Kennung_1153 = "1153";
        private string _f_1153;
        public string f_1153
        {
            get
            {
                _f_1153 = string.Empty;
                if ((this.sef1153 is clsEdiSegmentElementField) && (this.sef1153.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.RFF.EDICreate, this.sef1153, string.Empty, string.Empty);
                    _f_1153 = v.ReturnValue;
                }
                return _f_1153;
            }
        }
        public clsEdiSegmentElementField sef1153
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == RFF_C506.Kennung_1153);
                }
                return tmpSef;
            }
        }
        internal const string Kennung_1154 = "1154";
        /// <summary>
        ///             Referenz, Identifikation 
        ///
        ///             AAA Auftragsbestätigungsnummer
        ///             
        ///             AAN Lieferabrufs-/plannummer
        ///             -> Versandabrufnummer Stelle 1 - 6 = VAB-Nr. Stelle 7 - 8 = Versionsnummer
        ///             
        ///             AAU Lieferscheinnummer
        ///             -> Lieferscheinnummer: Identnummer, die der Lieferant einem Lieferschein zuteilt, darf sich 
        ///                innerhalb eines Jahres nicht wiederholen. Ein oder mehrere gleichartige zusammengefasste 
        ///                Ladeeinheiten dürfen maximal eine einzige Lieferscheinnummer umfassen. Das gilt auch für 
        ///                Mischverpackungen. Volkswagen lässt nur eine Lieferscheinnummer je Ladeeinheit zu.         
        /// 
        ///             AAO Sendungsreferenznummer des Empfängers
        ///             -> Transport-ID of the NLK dispatch call-off
        ///             
        ///             AKI Ordering customer's second reference number
        ///             ->  Transportkettenreferenz
        ///     
        ///             ANK Reference number assigned by third party
        ///             -> DUNS Nummer
        /// 
        ///             CRN
        ///             -> Eindeutige Referenznummer, die einer Sendung / Tour / Abfahrt eines Transportmittels zugeordnet ist. 
        ///                Entspricht der Sendungs-Ladungs-Bezugsnummer der VDA Empfehlung 4913. Bezugsnummer, die der Verlader 
        ///                der Sendung / Ladung zuteilt. Wiederholung der Nummer ist innerhalb eines Jahres nicht erlaubt. 
        ///                Für jede Transportrelation Beladewerk des Lieferanten <-> Anlieferwerk ist die Vergabe mindestens 
        ///                einer Sendungsummer notwendig. Für NLK Versandabrufe (BGM 1000 = VAB-NLK) ist hier 
        ///                die Pick-up-sheet-Nummer aus dem Versandabruf zu übernehmen. 
        ///                Die Sendungsnummer besteht aus bis zu 8 Ziffern, führende Nullen sind zulässig.
        ///                
        ///             COF Call off order number
        ///             
        ///             ON  Abschluss-/Bestellnummer            
        /// 
        ///             IV Rechnungsnummer
        /// 
        ///             TIN Transport instruction number
        ///             ->  Transportauftragnummer, die vom Versender vergebene Referenznummer des Abholtransports.
        ///             
        ///             UC Ultimate customer's reference number
        ///             
        /// </summary>
        private string _f_1154;
        public string f_1154
        {
            get
            {
                _f_1154 = string.Empty;
                if ((this.sef1154 is clsEdiSegmentElementField) && (this.sef1154.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.RFF.EDICreate, this.sef1154, string.Empty, string.Empty);
                    _f_1154 = v.ReturnValue;
                }
                return _f_1154;
            }
        }
        public clsEdiSegmentElementField sef1154
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == RFF_C506.Kennung_1154);
                }
                return tmpSef;
            }
        }


        /// <summary>
        ///             Positionsnummer im Lieferschein Mussangabe außer bei JIS (BGM 1000 = JIS-IST und PROD-NR).
        /// </summary>
        internal const string Kennung_1156 = "1156";
        private string _f_1156 = string.Empty;
        public string f_1156
        {
            get
            {
                _f_1156 = null;
                if ((this.sef1156 is clsEdiSegmentElementField) && (this.sef1156.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.RFF.EDICreate, this.sef1156, string.Empty, string.Empty);
                    _f_1156 = v.ReturnValue;
                }
                return _f_1156;
            }
        }
        public clsEdiSegmentElementField sef1156
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == RFF_C506.Kennung_1156);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            if (!f_1153.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += this.f_1153;
                if (f_1153.Equals("CRN"))
                {
                    string st = "";
                }
            }
            if (!f_1154.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1154;
            }
            if ((f_1156 != null) && (!f_1156.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1156;
            }
        }




        //===================================================================================

        /// <summary>
        ///             REFERENCE QUALIFIER 
        /// </summary>
        public string f_1153_RefQualifier { get; set; } = string.Empty;
        /// <summary>
        ///             REFERENCE Value
        /// </summary>
        public string f_1154_RefNumber { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public RFF_C506(string myEdiValueString)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                int iTmp = 0;
                List<string> strValue = myEdiValueString.Split(new char[] { ':' }).ToList();
                for (int i = 0; i < strValue.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.f_1153_RefQualifier = strValue[i].ToString();
                            break;
                        case 1:
                            this.f_1154_RefNumber = strValue[i].ToString();
                            break;
                        case 2:
                            break;
                        case 3:
                        case 4:
                            break;
                    }
                }
            }
        }
    }
}
