using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class UNH_S010
    {

        //internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        internal clsEdiSegmentElement SelectedSegmentElement;

        public UNH_S010(UNH myUnh)
        {
            this.UNH = myUnh;
            this.SegElement = UNH.SelectedSegmentElement;
            CreateValue();
        }

        internal UNH UNH;
        internal clsEdiSegmentElement SegElement;
        public const string Kennung_S010 = "S010";
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
        ///             DESADV Liefermeldung
        /// </summary>
        /// 
        internal const string Kennung_0070 = "0070";

        public clsEdiSegmentElement se0070;
        public clsEdiSegmentElementField sef0070
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == Kennung_0070);
                }
                return tmpSef;
            }
        }

        private string _f_0070 = string.Empty;
        public string f_0070
        {
            get
            {
                _f_0070 = string.Empty;
                if ((this.sef0070 is clsEdiSegmentElementField) && (this.sef0070.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNH.EDICreate, this.sef0070, string.Empty, string.Empty);
                    _f_0070 = v.ReturnValue;
                }

                return _f_0070;
            }
        }
        /// <summary>

        internal const string Kennung_0073 = "0073";
        private string _f_0073 = string.Empty;
        public string f_0073
        {
            get
            {
                _f_0073 = string.Empty;
                return _f_0073;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_0070;
            if (!this.f_0073.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_0073;
            }
        }



    }
}
