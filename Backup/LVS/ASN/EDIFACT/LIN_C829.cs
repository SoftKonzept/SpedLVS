using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class LIN_C829
    {
        internal clsEdiSegmentElement SegElement;
        internal LIN LIN;
        public const string Kennung_C829 = "C829";

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
        ///            Waren-/Leistungsnummer, Identifikation
        /// </summary>
        /// <param name="mySegElement"></param>
        public LIN_C829(LIN myEdiElement)
        {
            this.LIN = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        /// <summary>     
        ///             Sub-line indicator code
        /// </summary>
        public const string Kennung_5495 = "5495";
        private string _f_5495 = string.Empty;
        public string f_5495
        {
            get
            {
                _f_5495 = string.Empty;
                if ((this.sef5495 is clsEdiSegmentElementField) && (this.sef5495.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.LIN.EDICreate, this.sef5495, string.Empty, string.Empty);
                    _f_5495 = v.ReturnValue;
                }
                return _f_5495;
            }
        }
        public clsEdiSegmentElementField sef5495
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LIN_C829.Kennung_5495);
                }
                return tmpSef;
            }
        }


        /// <summary>     
        ///             Line item identifier 
        /// </summary>
        public const string Kennung_1082 = "1082";
        private string _f_1082 = string.Empty;
        public string f_1082
        {
            get
            {
                _f_1082 = string.Empty;
                if ((this.sef1082 is clsEdiSegmentElementField) && (this.sef1082.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.LIN.EDICreate, this.sef1082, string.Empty, string.Empty);
                    _f_1082 = v.ReturnValue;
                }
                return _f_1082;
            }
        }
        public clsEdiSegmentElementField sef1082
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LIN_C829.Kennung_1082);
                }
                return tmpSef;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            //if (!this.f_5495.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            //{
            //    this.Value += this.f_5495;
            //}
            this.Value += this.f_5495;
            if (!this.f_1082.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1082;
            }
        }

        //===================================================================================================================

        /// <summary>
        ///             
        /// </summary>
        public string f_5495_SubLineIndicator { get; set; } = string.Empty;

        public string f_1082_LineItem { get; set; } = string.Empty;
        public string f_1222_ConfigurationLevel { get; set; } = string.Empty;
        public string f_7083_ConfigurationCode { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public LIN_C829(string myEdiValueString, string myAsnArt)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                int iTmp = 0;
                List<string> strValue = myEdiValueString.Split(new char[] { ':' }).ToList();
                for (int i = 0; i < strValue.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.f_5495_SubLineIndicator = strValue[i].ToString();
                            break;
                        case 1:
                            this.f_1082_LineItem = strValue[i].ToString();
                            break;
                        case 2:
                            this.f_1222_ConfigurationLevel = strValue[i].ToString();
                            break;
                        case 3:
                            this.f_7083_ConfigurationCode = strValue[i].ToString();
                            break;
                        case 4:
                            break;
                    }
                }
            }
        }

    }
}
