using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class IMD_C272
    {
        internal clsEdiSegmentElement SegElement;
        internal IMD IMD;
        public const string Kennung = "C272";

        private string _value = string.Empty;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        ///            Verpackungsangaben
        /// </summary>
        /// <param name="mySegElement"></param>
        public IMD_C272(IMD myEdiElement)
        {
            this.IMD = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_7081 = "7081";
        /// <summary>       
        ///            Art der Verpackung, Code Bezeichnung der Verpackung, codiert gemäß Verpackungsdatenblatt 
        ///            (Packmittelcode des Kunden) bzw. zulässige Codes für Ausweichverpackungen,
        ///            wie z. B. 0001PAL oder E352430.
        /// </summary>
        private string _f_7081 = string.Empty;
        public string f_7081
        {
            get
            {
                _f_7081 = string.Empty;
                if ((this.sef7081 is clsEdiSegmentElementField) && (this.sef7081.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.IMD.EDICreate, this.sef7081, string.Empty, string.Empty);
                    _f_7081 = v.ReturnValue;
                }
                return _f_7081;
            }
        }
        public clsEdiSegmentElementField sef7081
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == IMD_C272.Kennung_7081);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_7081;
        }



    }
}
