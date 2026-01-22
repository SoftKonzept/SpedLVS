using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class LIN_C212
    {
        internal clsEdiSegmentElement SegElement;
        internal LIN LIN;
        public const string Kennung_C212 = "C212";

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
        public LIN_C212(LIN myEdiElement)
        {
            this.LIN = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }


        public const string Kennung_7140 = "7140";
        /// <summary>     
        ///             Produkt-/Leistungsnummer 
        ///             Die Original-Sachnummer ist unverändert (ggf. inklusive aller Blanks) 
        ///             aus dem Lieferabruf zu übernehmen.
        ///             ==> hier Werksnummer
        /// </summary>
        private string _f_7140 = string.Empty;
        public string f_7140
        {
            get
            {
                _f_7140 = string.Empty;
                if ((this.sef7140 is clsEdiSegmentElementField) && (this.sef7140.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.LIN.EDICreate, this.sef7140, string.Empty, string.Empty);
                    _f_7140 = v.ReturnValue;
                }
                return _f_7140;
            }
        }
        public clsEdiSegmentElementField sef7140
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LIN_C212.Kennung_7140);
                }
                return tmpSef;
            }
        }


        /// <summary>     
        ///             Art der Produkt-/Leistungsnummer, Code 
        ///             
        ///             IN Buyer's item number.
        /// </summary>
        internal const string Kennung_7143 = "7143";
        private string _f_7143 = string.Empty;
        public string f_7143
        {
            get
            {
                _f_7143 = string.Empty;
                if ((this.sef7143 is clsEdiSegmentElementField) && (this.sef7143.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.LIN.EDICreate, this.sef7143, string.Empty, string.Empty);
                    _f_7143 = v.ReturnValue;
                }
                else
                {
                    _f_7143 = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                }
                return _f_7143;
            }
        }
        public clsEdiSegmentElementField sef7143
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LIN_C212.Kennung_7143);
                }
                return tmpSef;
            }
        }


        /// <summary>     
        ///
        /// </summary>
        internal const string Kennung_1131 = "1131";
        private string _f_1131 = string.Empty;
        public string f_1131
        {
            get
            {
                _f_1131 = string.Empty;
                if ((this.sef1131 is clsEdiSegmentElementField) && (this.sef1131.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.LIN.EDICreate, this.sef1131, string.Empty, string.Empty);
                    _f_1131 = v.ReturnValue;
                }
                else
                {
                    _f_1131 = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                }
                return _f_1131;
            }
        }
        public clsEdiSegmentElementField sef1131
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LIN_C212.Kennung_1131);
                }
                return tmpSef;
            }
        }

        /// <summary>     
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
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.LIN.EDICreate, this.sef3055, string.Empty, string.Empty);
                    _f_3055 = v.ReturnValue;
                }
                else
                {
                    _f_3055 = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
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
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LIN_C212.Kennung_3055);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            if (!this.f_7140.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += this.f_7140;
            }
            if (!this.f_7143.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_7143;
            }
            if (!this.f_1131.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1131;
            }
            if (!this.f_3055.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
            }
        }


        //===================================================================================================================

        /// <summary>
        ///             Datum / Uhrzeit
        /// </summary>
        public string f_7140_ArticleReference { get; set; } = string.Empty;

        public string f_7143_TypeCode { get; set; } = string.Empty;
        public string f_1131_CodeListQualifier { get; set; } = string.Empty;
        public string f_3055_CodeListAgency { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public LIN_C212(string myEdiValueString, string myAsnArt)
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
                            this.f_7140_ArticleReference = strValue[i].ToString();
                            break;
                        case 1:
                            this.f_7143_TypeCode = strValue[i].ToString();
                            break;
                        case 2:
                            this.f_1131_CodeListQualifier = strValue[i].ToString();
                            break;
                        case 3:
                            this.f_3055_CodeListAgency = strValue[i].ToString();
                            break;
                        case 4:
                            break;
                    }
                }
            }
        }
    }
}
