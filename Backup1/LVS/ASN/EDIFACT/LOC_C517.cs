using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class LOC_C517
    {
        internal clsEdiSegmentElement SegElement;
        internal LOC LOC;
        public const string Kennung_C517 = "C517";

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
        public LOC_C517(LOC myEdiElement)
        {
            this.LOC = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_3225 = "3225";
        /// <summary>       
        ///                 9 Ladeort/Ladehafen
        ///                 7 Lieferort
        ///                 13 Umschlagsort
        /// </summary>
        private string _f_3225 = string.Empty;
        public string f_3225
        {
            get
            {
                _f_3225 = string.Empty;
                if ((this.sef3225 is clsEdiSegmentElementField) && (this.sef3225.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.LOC.EDICreate, this.sef3225, string.Empty, string.Empty);
                    _f_3225 = v.ReturnValue;
                }
                return _f_3225;
            }
        }
        public clsEdiSegmentElementField sef3225
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LOC_C517.Kennung_3225);
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
                if ((this.sef3225 is clsEdiSegmentElementField) && (this.sef3225.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.LOC.EDICreate, this.sef1131, string.Empty, string.Empty);
                    _f_1131 = v.ReturnValue;
                }
                return _f_1131;
            }
        }

        internal const string Kennung_3055 = "3055";
        /// <summary>
        ///             Verantwortliche Stelle für die Codepflege, Code
        ///             16 DUNS (Dun & Bradstreet) 
        ///             91 Zugewiesen vom Verkäufer oder dessen Agenten
        ///
        /// </summary>
        private string _f_3055 = string.Empty;
        public string f_3055
        {
            get
            {
                _f_3055 = string.Empty;
                if ((this.sef3055 is clsEdiSegmentElementField) && (this.sef3055.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.LOC.EDICreate, this.sef3055, string.Empty, string.Empty);
                    //_f_3055 = CheckValue_3055(v.ReturnValue);
                    _f_3055 = v.ReturnValue;
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
        //private string CheckValue_3055(string myValToCheck)
        //{
        //    string strReturn = string.Empty;
        //    strReturn = myValToCheck;

        //    switch (this.LOC.f_3227)
        //    {
        //        case LOC.f_3227_SelectedValue_9:
        //            if ((!myValToCheck.Equals("16")) && (!myValToCheck.Equals("91")))
        //            {
        //                strReturn = "91";
        //            }
        //            break;
        //        default:
        //            strReturn = myValToCheck;
        //            break;
        //    }
        //    return strReturn;
        //}

        internal const string Kennung_3224 = "3224";
        /// <summary>
        ///             Ortsangabe Ort / Platz / Lokation Name
        /// </summary>
        private string _f_3224 = string.Empty;
        public string f_3224
        {
            get
            {
                _f_3224 = string.Empty;
                if ((this.sef3224 is clsEdiSegmentElementField) && (this.sef3224.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.LOC.EDICreate, this.sef3224, string.Empty, string.Empty);
                    _f_3224 = v.ReturnValue;
                }
                else
                {
                    _f_3224 = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                }
                return _f_3224;
            }
        }
        public clsEdiSegmentElementField sef3224
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LOC_C517.Kennung_3224);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_3225;
            if (!f_1131.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1131;
            }
            if (!f_3055.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
            }
            if (!f_3224.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3224;
            }
        }

        //===================================================================================================================

        ///// <summary>
        /////             PARTY QUALIFIER 
        ///// </summary>
        public string f_3225_LocationIdentifier { get; set; } = string.Empty;

        public string f_1131_CodeIdentification { get; set; } = string.Empty;
        public string f_3055_CodeAgency { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public LOC_C517(string myEdiValueString, string myAsnArt)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List<string> strValue = myEdiValueString.Split(new char[] { ':', '+' }).ToList();
                for (int i = 0; i < strValue.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.f_3225_LocationIdentifier = strValue[i].ToString();
                            break;
                        case 1:
                            this.f_1131_CodeIdentification = strValue[i].ToString();
                            break;
                        case 2:
                            this.f_3055_CodeAgency = strValue[i].ToString();
                            break;
                            //case 3:
                            //    C080 = new NAD_C080(strValue[i]);
                            //    break;
                    }
                }

            }
        }

    }
}
