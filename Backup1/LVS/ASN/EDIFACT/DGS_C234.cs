using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class DGS_C234
    {
        internal clsEdiSegmentElement SegElement;
        internal DGS DGS;
        public const string Kennung = "C234";

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
        ///            Art der Einheit/des Bestandteils
        /// </summary>
        /// <param name="mySegElement"></param>
        public DGS_C234(DGS myEdiElement)
        {
            this.DGS = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_7124 = "7124";
        /// <summary>       
        ///            Gefahrgut-Identifikation der Vereinten Nationen (UNDG) Vierstelliger UNDG Code
        /// </summary>
        private string _f_7124;
        public string f_7124
        {
            get
            {
                _f_7124 = null;
                if ((this.sef7124 is clsEdiSegmentElementField) && (this.sef7124.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.DGS.EDICreate, this.sef7124, string.Empty, string.Empty);
                    _f_7124 = v.ReturnValue;
                }
                return _f_7124;
            }
        }
        public clsEdiSegmentElementField sef7124
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == DGS_C234.Kennung_7124);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_7124;
        }



    }
}
