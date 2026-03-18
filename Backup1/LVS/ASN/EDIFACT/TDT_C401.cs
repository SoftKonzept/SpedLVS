using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class TDT_C401
    {
        internal clsEdiSegmentElement SegElement;
        internal TDT TDT;
        public const string Kennung = "C401";

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
        ///             Sonderfahrt
        /// </summary>
        /// <param name="mySegElement"></param>
        public TDT_C401(TDT myEdiElement)
        {
            this.TDT = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_8457 = "8457";
        /// <summary>       
        ///             Besonderer Transport, Grund, Code
        ///             ZZZ ist nur ein Platzhalter, da das DE den Status M hat. 
        ///             In der Nachricht wird nur die Sonderfahrt Nummer (oder ähnliche Referenz) im DE 7130 übertragen. 
        ///             Die verantwortlichkeit wird außerhalb und unabhänging vom EDI Austausch geklärt. 
        ///             ZZZ Mutually defined
        /// </summary>
        private string _f_8457;
        public string f_8457
        {
            get
            {
                _f_8457 = "ZZZ";
                return _f_8457;
            }
        }
        /// <summary>
        ///             Besonderer Transport, Verantwortlichkeit, Code 
        ///             X Responsibility to be determined
        /// </summary>
        private string _f_8459;
        public string f_8459
        {
            get
            {
                _f_8459 = "X";
                return _f_8459;
            }
        }

        internal const string Kennung_7130 = "7130";
        /// <summary>
        ///             Kunden-Sendungsfreigabenummer Sonderfahrtnummer
        /// </summary>
        private string _f_7130;
        public string f_7130
        {
            get
            {
                _f_7130 = string.Empty;
                if ((this.sef7130 is clsEdiSegmentElementField) && (this.sef7130.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.TDT.EDICreate, this.sef7130, string.Empty, string.Empty);
                    _f_7130 = v.ReturnValue;
                }
                return _f_7130;
            }
        }

        public clsEdiSegmentElementField sef7130
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == TDT_C401.Kennung_7130);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_8457;
            this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_8459;
            this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_7130;
        }



    }
}
