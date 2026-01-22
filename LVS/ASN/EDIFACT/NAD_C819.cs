using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class NAD_C819
    {
        internal clsEdiSegmentElement SegElement;
        internal NAD NAD;
        public const string Kennung = "C819";

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
        ///             Einzelheiten zu Maßangaben
        /// </summary>
        /// <param name="mySegElement"></param>
        public NAD_C819(NAD myEdiElement)
        {
            this.NAD = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        /// <summary>
        ///                 Eindeutiger Identifier des Geschäftspartners (Kundennummer, Lieferantennummer DUNS oder dgl.)
        /// </summary>
        internal const string Kennung_3229 = "3229";
        public clsEdiSegmentElementField sef3229
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == NAD_C819.Kennung_3229);
                }
                return tmpSef;
            }
        }

        private string _f_3229 = string.Empty;
        public string f_3229
        {
            get
            {
                _f_3229 = string.Empty;
                if ((this.sef3229 is clsEdiSegmentElementField) && (this.sef3229.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.NAD.EDICreate, this.sef3229, string.Empty, string.Empty);
                    _f_3229 = v.ReturnValue;
                }
                return _f_3229;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_3229;
            //if (!f_3229.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)) 
            //{
            //    this.Value += this.f_3229;
            //}
        }



    }
}
