using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class UNB_S005
    {
        public UNB_S005(UNB myEdiElement)
        {
            this.UNB = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }
        internal UNB UNB;
        internal clsEdiSegmentElement SegElement;
        public const string Kennung_S005 = "S005";
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
        ///             Referenz oder Paßwort des Empfängers
        /// </summary>
        public const string Kennung_0022 = "0022";
        private string _f_0022 = string.Empty;
        public string f_0022
        {
            get
            {
                _f_0022 = string.Empty;
                if ((this.sef0022 is clsEdiSegmentElementField) && (this.sef0022.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNB.EDICreate, this.sef0022, string.Empty, string.Empty);
                    _f_0022 = v.ReturnValue;
                }
                return _f_0022;
            }
            set
            {
                _f_0022 = value;
            }
        }

        public clsEdiSegmentElementField sef0022
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNB_S005.Kennung_0022);
                }
                return tmpSef;
            }
        }

        /// <summary>
        ///             Referenz oder Paßwort des Empfängers
        /// </summary>
        public const string Kennung_0025 = "0025";
        private string _f_0025 = string.Empty;
        public string f_0025
        {
            get
            {
                _f_0025 = string.Empty;
                if ((this.sef0025 is clsEdiSegmentElementField) && (this.sef0025.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNB.EDICreate, this.sef0025, string.Empty, string.Empty);
                    _f_0025 = v.ReturnValue;
                }
                else
                {
                    _f_0025 = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                }
                return _f_0025;
            }
        }

        public clsEdiSegmentElementField sef0025
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNB_S005.Kennung_0025);
                }
                return tmpSef;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            //if(!f_0022.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)) 
            //{
            //    this.Value += this.f_0022;
            //}
            this.Value += this.f_0022;
            if (!f_0025.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_0025;
            }
        }



    }
}
