using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class GIR_C206
    {
        internal clsEdiSegmentElement SegElement;
        internal GIR GIR;
        public const string Kennung = "C206";

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
        ///            Identifikationsnummer
        /// </summary>
        /// <param name="mySegElement"></param>
        public GIR_C206(GIR myEdiElement)
        {
            this.GIR = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }


        public const string Kennung_7402 = "7402";
        /// <summary>  
        ///            Objekt, Identifikation
        ///            
        ///            Name of the module (JIS container) Name des Moduls (d.h. des JIS-Behälters)
        ///            
        ///            1 Product
        ///            -> Chargennummer - falls Produktionsdatum oder Haltbarkeitsdatum übertragen werden sollen, 
        ///            ohne dass eine Chargennummer vorhanden ist, muss hier der Wert NONE eingetragen werden, da 
        ///            EDIFACT verlangt, dass dieses Feld nicht leer ist. Erlaubte Zeichen für die 
        ///            Chargen-Nummer sind: A-Z, 0 – 9, "–" (Bindestrich) und "/" (Schrägstrich). 
        ///            Sonderzeichen sind nicht erlaubt.
        /// 
        ///            4 Vehicle reference set
        ///            7405 = AN: Kenn-Nr. - Format n10 oder n6 für Lamborghini 
        ///            7405 = AP: Modell - Format an..3 
        ///            7405 = BF: Schlüsselnummer - Format an..10 
        ///            7405 = VV: Fahrgestellnummer - Format an..17 
        ///            7405 = XA: Teilegruppe/Modul-ID - Format an2 oder an4 
        ///            7405 = XB: Modelljahr - Format n2 7405 = XE: Vorserienkennung - Format an..8 
        ///            7405 = XN: Montagelinien-Nr. - Format n..2 
        ///            7405 = XO: Montagesequenzdaten - Format n..6 
        ///            
        ///            Die Angabe der Kennnummer (7405 = AN) und der Teilegruppe/Modul-ID (7405 = XA) ist für 
        ///            JIS-Prozesse (BGM 1000 = PROD-NR oder JIS-IST) verpflichtend. Fehlen diese Angaben, wird die 
        ///            Nachricht abgewiesen. Die Angabe der Montagesequenzdaten (7405 = XO) und der Montagelinien-Nr.
        ///            (7405 = XN) ist für JIS-Prozesse (BGM 1000 = PROD-NR oder JIS-IST) verpflichtend. Fehlen diese 
        ///            Angaben wird die Nachricht mit Fehlern angenommen.
        /// </summary>
        private string _f_7402;
        public string f_7402
        {
            get
            {
                _f_7402 = string.Empty;
                if ((this.sef7402 is clsEdiSegmentElementField) && (this.sef7402.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.GIR.EDICreate, this.sef7402, string.Empty, string.Empty);
                    _f_7402 = v.ReturnValue;
                }
                return _f_7402;
            }
        }
        public clsEdiSegmentElementField sef7402
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == GIR_C206.Kennung_7402);
                }
                return tmpSef;
            }
        }

        public const string Kennung_7405 = "7405";
        /// <summary>       
        ///            Objektidentifikation, Qualifier 
        ///            
        ///             1 Product
        ///             -> BX Batch number
        ///             
        ///             4 Vehicle reference set
        ///             -> In der DELJIT/SYNCRO werden Modelljahr und Modell mit dem Qualifier TMA in einem 
        ///             Feld (jjaaa = 2-stell. Modelljahr und 3-stell. Modell) übertragen. In der VDA 4987 müssen 
        ///             diese Informationen getrennt übertragen werden, das Modelljahr mit dem Qualifier XB, das Modell 
        ///             mit dem Qualifier AP. 
        ///             
        ///             VV Vehicle identity number 
        ///             AN Manufacturing reference number 
        ///             XB Modelljahr 
        ///             AP Product XA Abrufgruppe BF Door key number 
        ///             XC Sonderlack
        ///             XD Individualtext order -code (z.B. Presse, Messe, ...) 
        ///             XE Vorserienkennzeichen 
        ///             XG Parameter-String (z.B. für Achseinstellung) 
        ///             XH Nachbestell-Schlüssel oder Problemblatt- Nummer 
        ///             XI Fahrzeug-Abrufnummer 
        ///             XJ Bereifung bei Auslieferung 
        ///             XN Montageband 
        ///             XO Sequenznummer (Produktionsfolgenummer) 
        ///             XQ Kennzeichen Ausnahmestatus
        ///             
        ///             17 Seller's instructions
        ///             -> XP Name des Moduls
        /// </summary>
        private string _f_7405;
        public string f_7405
        {
            get
            {
                _f_7405 = string.Empty;
                if ((this.sef7405 is clsEdiSegmentElementField) && (this.sef7405.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.GIR.EDICreate, this.sef7405, string.Empty, string.Empty);
                    _f_7405 = v.ReturnValue;
                }
                return _f_7405;
            }
        }
        public clsEdiSegmentElementField sef7405
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == GIR_C206.Kennung_7405);
                }
                return tmpSef;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_7402;
            this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_7405;
        }

        //===================================================================================

        /// <summary>
        ///             IDENTIFICATION NUMBER 
        /// </summary>
        public string f_7402_CoilNumber { get; set; } = string.Empty;
        public string f_7405_NumberQualifier { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public GIR_C206(string myEdiValueString)
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
                            this.f_7402_CoilNumber = strValue[i].ToString();
                            break;
                        case 1:
                            this.f_7405_NumberQualifier = strValue[i].ToString();
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
