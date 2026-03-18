using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class LOC_C553
    {
        internal clsEdiSegmentElement SegElement;
        internal LOC LOC;
        public const string Kennung_C553 = "C553";

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
        ///             Ortsangabe
        /// </summary>
        /// <param name="mySegElement"></param>
        public LOC_C553(LOC myEdiElement)
        {
            this.LOC = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }


        /// <summary>       
        /// 
        /// </summary>

        internal const string Kennung_3233 = "3233";
        private string _f_3233 = string.Empty;
        public string f_3233
        {
            get
            {
                _f_3233 = string.Empty;
                if ((this.sef3233 is clsEdiSegmentElementField) && (this.sef3233.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.LOC.EDICreate, this.sef3233, string.Empty, string.Empty);
                    _f_3233 = v.ReturnValue;
                }
                return _f_3233;
            }
        }
        public clsEdiSegmentElementField sef3233
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LOC_C553.Kennung_3233);
                }
                return tmpSef;
            }
        }

        /// <summary>
        ///             Not used
        /// </summary>
        /// 
        internal const string Kennung_1131 = "1131";
        public clsEdiSegmentElementField sef1131
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LOC_C517.Kennung_1131);
                }
                return tmpSef;
            }
        }

        private string _f_1131 = string.Empty;
        public string f_1131
        {
            get
            {
                _f_1131 = string.Empty;
                if ((this.sef1131 is clsEdiSegmentElementField) && (this.sef1131.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.LOC.EDICreate, this.sef1131, string.Empty, string.Empty);
                    _f_1131 = v.ReturnValue;
                }
                return _f_1131;
            }
        }
        /// <summary>
        ///
        ///
        /// </summary>
        internal const string Kennung_3055 = "3055";
        private string _f_3055 = string.Empty;
        public string f_3055
        {
            get
            {
                _f_3055 = string.Empty;
                if ((this.sef3055 is clsEdiSegmentElementField) && (this.sef3055.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.LOC.EDICreate, this.sef3055, string.Empty, string.Empty);
                    _f_3055 = CheckValue_3055(v.ReturnValue);
                }
                return _f_3055;
            }
        }
        public clsEdiSegmentElementField sef3055
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LOC_C517.Kennung_3055);
                }
                return tmpSef;
            }
        }

        /// <summary>
        ///             LOC.3227 = 9 => LOC.C517.3055 => 16 oder 91
        /// </summary>
        private string CheckValue_3055(string myValToCheck)
        {
            string strReturn = string.Empty;
            strReturn = myValToCheck;

            switch (this.LOC.f_3227)
            {
                case LOC.f_3227_SelectedValue_9:
                    if ((!myValToCheck.Equals("16")) && (!myValToCheck.Equals("91")))
                    {
                        strReturn = "91";
                    }
                    break;
                default:
                    strReturn = myValToCheck;
                    break;
            }
            return strReturn;
        }


        /// <summary>
        /// 
        /// </summary>
        internal const string Kennung_3232 = "3232";
        private string _f_3232 = string.Empty;
        public string f_3232
        {
            get
            {
                _f_3232 = string.Empty;
                if ((this.sef3232 is clsEdiSegmentElementField) && (this.sef3232.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.LOC.EDICreate, this.sef3232, string.Empty, string.Empty);
                    _f_3232 = v.ReturnValue;
                }
                return _f_3232;
            }
        }
        public clsEdiSegmentElementField sef3232
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LOC_C553.Kennung_3232);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_3233;
            if (!f_1131.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1131;
            }
            if (!f_3055.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
            }
            if (!f_3232.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3232;
            }
        }



    }
}
