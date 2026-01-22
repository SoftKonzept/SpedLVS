using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class UNB_S001
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySegElement"></param>
        public UNB_S001(UNB myUNB)
        {
            this.UNB = myUNB;
            this.SegElement = this.UNB.SelectedSegmentElement;
            CreateValue();
        }

        internal clsEdiSegmentElement SegElement;
        internal UNB UNB;
        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
            }
        }
        public const string Kennung_S001 = "S001";

        /// <summary>
        ///             Syntax-Kennung 
        ///             UNOA UN/ECE-Zeichensatz A 
        ///             UNOB UN/ECE-Zeichensatz B 
        ///             UNOC UN/ECE-Zeichensatz C 
        ///             UNOD UN/ECE-Zeichensatz D
        /// </summary>
        internal const string Kennung_0001 = "0001";
        private string _f_0001 = string.Empty;
        public string f_0001
        {
            get
            {
                //_f_0001 = "UNOC";
                if ((this.sef0001 is clsEdiSegmentElementField) && (this.sef0001.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNB.EDICreate, this.sef0001, string.Empty, string.Empty);
                    _f_0001 = v.ReturnValue;
                }
                return _f_0001;
            }
        }

        public clsEdiSegmentElementField sef0001
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNB_S001.Kennung_0001);
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///             Versionsnummer 3
        /// </summary>
        internal const string Kennung_0002 = "0002";
        private string _f_0002 = string.Empty;
        public string f_0002
        {
            get
            {
                //_f_0002 = "3";
                if ((this.sef0002 is clsEdiSegmentElementField) && (this.sef0002.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNB.EDICreate, this.sef0002, string.Empty, string.Empty);
                    _f_0002 = v.ReturnValue;
                }
                return _f_0002;
            }
        }
        public clsEdiSegmentElementField sef0002
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNB_S001.Kennung_0002);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            if (!this.f_0001.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += this.f_0001;
            }
            if (!this.f_0002.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_0002;
            }
        }



    }
}
