using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class EQD_C237
    {
        internal clsEdiSegmentElement SegElement;
        internal EQD EQD;
        public const string Kennung = "C237";

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
        public EQD_C237(EQD myEdiElement)
        {
            this.EQD = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_8260 = "8260";
        /// <summary>       
        ///                 AAN Lieferabrufs-/plannummer
        ///                 AAO Sendungsreferenznummer des Empfängers
        ///                 AKI Ordering customer's second reference number
        ///                 ANK Reference number assigned by third party
        ///                 CRN Reisenummer
        ///                 TIN Transport instruction number
        /// </summary>
        private string _f_8260;
        public string f_8260
        {
            get
            {
                _f_8260 = string.Empty;
                if ((this.sef8260 is clsEdiSegmentElementField) && (this.sef8260.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EQD.EDICreate, this.sef8260, string.Empty, string.Empty);
                    _f_8260 = v.ReturnValue;
                }
                return _f_8260;
            }
        }
        public clsEdiSegmentElementField sef8260
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == EQD_C237.Kennung_8260);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_8260;
        }



    }
}
