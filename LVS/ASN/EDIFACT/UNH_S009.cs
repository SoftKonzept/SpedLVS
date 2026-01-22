using LVS.Constants;
using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class UNH_S009
    {

        //internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        internal clsEdiSegmentElement SelectedSegmentElement;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySegElement"></param>
        //public UNH_S009(clsEdiSegmentElement mySegElement)
        //{
        //    this.SegElement = mySegElement;
        //    CreateValue();
        //}

        public UNH_S009(UNH myUnh)
        {
            this.UNH = myUnh;
            this.SegElement = UNH.SelectedSegmentElement;
            CreateValue();
        }

        internal UNH UNH;
        internal clsEdiSegmentElement SegElement;
        public const string Kennung_S009 = "S009";
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
        internal const string Kennung_0065 = "0065";

        public clsEdiSegmentElement se0065;
        public clsEdiSegmentElementField sef0065
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == Kennung_0065);
                }
                return tmpSef;
            }
        }

        private string _f_0065 = string.Empty;
        public string f_0065
        {
            get
            {
                //_f_0065 = "DESADV";               
                if ((this.sef0065 is clsEdiSegmentElementField) && (this.sef0065.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNH.EDICreate, this.sef0065, string.Empty, string.Empty);
                    _f_0065 = v.ReturnValue;
                }

                return _f_0065;
            }
        }
        /// <summary>
        ///             D Entwurfs-Version
        /// </summary>
        /// 
        internal const string Kennung_0052 = "0052";
        private string _f_0052 = string.Empty;
        public string f_0052
        {
            get
            {
                //_f_0052 = "D";
                if ((this.sef0052 is clsEdiSegmentElementField) && (this.sef0052.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNH.EDICreate, this.sef0052, string.Empty, string.Empty);
                    _f_0052 = v.ReturnValue;
                }
                return _f_0052;
            }
        }
        public clsEdiSegmentElement se0052;
        public clsEdiSegmentElementField sef0052
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == Kennung_0052);
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///             07A Ausgabe 2007 - A
        /// </summary>
        /// 
        internal const string Kennung_0054 = "0054";
        private string _f_0054 = string.Empty;
        public string f_0054
        {
            get
            {
                //_f_0054 = "07A";
                _f_0054 = string.Empty;
                if ((this.sef0054 is clsEdiSegmentElementField) && (this.sef0054.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNH.EDICreate, this.sef0054, string.Empty, string.Empty);
                    _f_0054 = v.ReturnValue;
                }
                return _f_0054;
            }
        }

        public clsEdiSegmentElement se0054;
        public clsEdiSegmentElementField sef0054
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == Kennung_0054);
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///             Verwaltende Organisation 
        ///             UN UN/CEFACT
        /// </summary>
        /// 
        internal const string Kennung_0051 = "0051";
        private string _f_0051 = string.Empty;
        public string f_0051
        {
            get
            {
                //_f_0051 = "UN";
                _f_0051 = string.Empty;
                if ((this.sef0051 is clsEdiSegmentElementField) && (this.sef0051.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNH.EDICreate, this.sef0051, string.Empty, string.Empty);
                    _f_0051 = v.ReturnValue;
                }
                return _f_0051;
            }
        }

        public clsEdiSegmentElement se0051;
        public clsEdiSegmentElementField sef0051
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == Kennung_0051);
                }
                return tmpSef;
            }
        }

        /// <summary>
        ///             Kennzeichnung des verwendeten Subsets, zugewiesen vom VDA. 
        ///             GAVF13 VDA DESADV Version 1.3 
        ///             GAVF20 VDA DESADV Version 2.0 
        ///             GAVF21 VDA DESADV Version 2.1 
        ///             GAVF22 VDA DESADV Version 2.2
        /// </summary>
        /// 
        internal const string Kennung_0057 = "0057";

        public clsEdiSegmentElement se0057;
        public clsEdiSegmentElementField sef0057
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == Kennung_0057);
                }
                return tmpSef;
            }
        }


        private string _f_0057 = string.Empty;
        public string f_0057
        {
            get
            {
                //_f_0057 = "GAVF22";
                _f_0057 = string.Empty;
                if ((this.sef0057 is clsEdiSegmentElementField) && (this.sef0057.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNH.EDICreate, this.sef0057, string.Empty, string.Empty);
                    _f_0057 = v.ReturnValue;
                }
                return _f_0057;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_0065;
            if (!this.f_0052.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_0052;
            }
            if (!this.f_0054.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_0054;
            }
            if (!this.f_0051.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_0051;
            }
            if (!this.f_0057.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_0057;
            }
        }


        //===================================================================================

        /// <summary>
        ///             MessageTyp 
        /// </summary>
        public string f_0065_MessageTypIdentifier { get; set; } = string.Empty;
        public string f_0052_MessageTypVersion { get; set; } = string.Empty;
        public string f_0054_MessageRelease { get; set; } = string.Empty;
        public string f_0051_ControllingAgency { get; set; } = string.Empty;
        public string f_0057_AssignedCode { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public UNH_S009(string myEdiValueString, string myAsnArt)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List<string> strValue = myEdiValueString.Split(new char[] { ':' }).ToList();
                switch (myAsnArt)
                {
                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                    case constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A:
                        for (int i = 0; i < strValue.Count; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    this.f_0065_MessageTypIdentifier = strValue[i];
                                    break;
                                case 1:
                                    this.f_0052_MessageTypVersion = strValue[i];
                                    break;
                                case 2:
                                    this.f_0054_MessageRelease = strValue[i];
                                    break;
                                case 3:
                                    this.f_0051_ControllingAgency = strValue[i];
                                    break;
                                case 4:
                                    this.f_0057_AssignedCode = strValue[i];
                                    break;
                            }
                        }
                        break;
                }
            }
        }
    }
}
