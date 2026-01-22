using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class FTX_C108
    {
        internal clsEdiSegmentElement SegElement;
        internal FTX FTX;
        public const string Kennung_C108 = "C108";

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
        public FTX_C108(FTX myEdiElement)
        {
            this.FTX = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_4440 = "4440";
        /// <summary>       
        ///                 Freier Text, Code
        /// </summary>
        private string _f_4440 = string.Empty;
        public string f_4440
        {
            get
            {
                _f_4440 = string.Empty;
                if ((this.sef4440 is clsEdiSegmentElementField) && (this.sef4440.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.FTX.EDICreate, this.sef4440, string.Empty, string.Empty);
                    _f_4440 = v.ReturnValue;
                }
                return _f_4440;
            }
        }
        public clsEdiSegmentElementField sef4440
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == FTX_C108.Kennung_4440);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            if (!f_4440.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += this.f_4440;
            }
        }



    }
}
