using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class FTX_C107
    {
        internal clsEdiSegmentElement SegElement;
        internal FTX FTX;
        public const string Kennung_C107 = "C107";

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
        ///             Liefer- oder Transportbedingungen
        /// </summary>
        /// <param name="mySegElement"></param>
        public FTX_C107(FTX myEdiElement)
        {
            this.FTX = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_4441 = "4441";
        /// <summary>       
        ///                 Freier Text, Code
        /// </summary>
        private string _f_4441 = string.Empty;
        public string f_4441
        {
            get
            {
                _f_4441 = string.Empty;
                if ((this.sef4441 is clsEdiSegmentElementField) && (this.sef4441.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.FTX.EDICreate, this.sef4441, string.Empty, string.Empty);
                    _f_4441 = v.ReturnValue;
                }
                return _f_4441;
            }
        }
        public clsEdiSegmentElementField sef4441
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == FTX_C107.Kennung_4441);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            if (!f_4441.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += this.f_4441;
            }
        }



    }
}
