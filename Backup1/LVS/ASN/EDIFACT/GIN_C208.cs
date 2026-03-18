using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class GIN_C208
    {
        internal clsEdiSegmentElement SegElement;
        internal GIN GIN;
        public const string Kennung = "C208";

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
        public GIN_C208(GIN myEdiElement)
        {
            this.GIN = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }



        /// <summary>     
        /// 
        ///            ML Marking/label number
        ///            
        /// 
        ///            AW Serial shipping container code
        ///            -> Objekt, Identifikation ID des in einer äußeren oder Zwischenverpackung enthaltenen Packstücks. 
        ///            Dabei kann es sich auch um ein Fach in einem JIS Behälter oder um einen virtuellen Behälter handeln 
        ///            (siehe Prozessdokumentation und Verpackungsbeispiele). Es sind nur Ziffern, 
        ///            ggf. mit führenden Nullen, zulässig
        ///            
        ///            CQ Internal control number - Behälternummer
        /// 
        ///            BU Package buyer assigned identifier
        ///            -> LHM Nummer (Format n12), (BGM 1000 = VAB-DDP) Manifest-Nr. (Format n10), (BGM 1000 = VAB-CHA) 
        ///            Nur Chattanooga! (BGM 1000 = VAB-CHA) Die Manifest-Nr. ist zwingend für Anlieferungen an das Werk 
        ///            Chattanooga zurück zu übertragen, für die ein Versandabruf von VW (GLOBAL DELJIT - VAB) gesendet wurde. 
        ///            Im Aftermarket Bypass-Prozess (VW) (BGM 1000 = VAB-DDP) wird die LHM Nummer übertragen, die nach Vorgaben 
        ///            des Kunden vom Lieferanten zu bilden ist und von der sonst üblichen Packstücknummer abweicht. Bei 
        ///            Avis OT Streckengeschäft: 
        ///            LHM-Nummer (n12) die ersten drei Stellen der Lieferantennummer + Index + 8stellige Packstücknummer. 
        ///            Beispiel: 
        ///            Alte Lieferanten-Nr. 6-stellig und 1 Stelle Index: 
        ///            0252210 = 25221/0 => 252012345678 
        ///            Neue Lieferanten-Nr. 7-stellig und 2 Stellen Index: 
        ///            0012563300 = 125633/0 => 125012345678
        ///            
        ///             hier
        ///             1JUN + DUNSNr Lieferant+ Produktionsnummer
        /// </summary>
        internal const string Kennung_7402 = "7402";
        private string _f_7402;
        public string f_7402
        {
            get
            {
                _f_7402 = string.Empty;
                //_f_7402 = "1JUN" + " DUNSNrLieferant ";
                if ((this.sef7402 is clsEdiSegmentElementField) && (this.sef7402.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.GIN.EDICreate, this.sef7402, string.Empty, string.Empty);
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



        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_7402;
        }



    }
}
