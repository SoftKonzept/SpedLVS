using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class MEA_C174
    {
        internal clsEdiSegmentElement SegElement;
        internal MEA MEA;
        public const string Kennung_C174 = "C174";

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
        ///             Einzelheiten zu Maßangaben
        /// </summary>
        /// <param name="mySegElement"></param>
        public MEA_C174(MEA myEdiElement)
        {
            this.MEA = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        public const string f_6411_selVal_KGM = "KGM";

        /// <summary>
        ///             Maßeinheit, Code         ///             
        ///             KGM kilogram
        /// </summary>
        internal const string Kennung_6411 = "6411";
        private string _f_6411 = string.Empty;
        public string f_6411
        {
            get
            {
                _f_6411 = string.Empty;
                if ((this.sef6411 is clsEdiSegmentElementField) && (this.sef6411.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.MEA.EDICreate, this.sef6411, string.Empty, string.Empty);
                    _f_6411 = v.ReturnValue;
                }
                return _f_6411;
            }
        }
        public clsEdiSegmentElementField sef6411
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    //tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == "6411");
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == Kennung_6411);
                }
                return tmpSef;
            }
        }

        /// <summary>
        ///             Messwert Bruttogewicht - Gewicht (Masse) ausschließlich Transportausrüstung (carriers equipment) 
        ///             Das Gewicht ist auf volle Kilogramm aufzurunden, außer bei Sendungsgewichten kleiner 1 Kilogramm.
        /// </summary>
        internal const string Kennung_6314 = "6314";
        private string _f_6314 = string.Empty;
        public string f_6314
        {
            get
            {
                _f_6314 = string.Empty;
                if ((this.sef6314 is clsEdiSegmentElementField) && (this.sef6314.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.MEA.EDICreate, this.sef6314, string.Empty, string.Empty);
                    int iTmp = 0;
                    int.TryParse(v.ReturnValue, out iTmp);
                    if (iTmp > 0)
                    {
                        _f_6314 = ValueFormat(v.ReturnValue);
                    }
                    else
                    {
                        _f_6314 = v.ReturnValue;
                    }
                }
                return _f_6314;
            }
        }
        public clsEdiSegmentElementField sef6314
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    //tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == "6314");
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == Kennung_6314);
                }
                return tmpSef;
            }
        }


        /// <summary>
        ///             Gemessene Dimension, Code
        ///             
        ///             AAA Net weight
        ///             AAB Goods item gross weight - Ladeeinheit
        ///             AAD Consignment gross weight
        ///             AAE Measurement- Anzahl
        ///             AAL Net weight
        ///             G Gross weight - Brutto Artikel
        ///             T Tare weight
        ///             
        /// </summary>
        private string ValueFormat(string myVal)
        {
            string strReturn = string.Empty;
            decimal decTmp = 0;
            if (this.MEA.C502.f_6313.Equals(string.Empty))
            {
                switch (this.MEA.f_6311)
                {
                    case MEA_C502.f_6313_selVal_AAE:
                        int iTmp = 0;
                        int.TryParse(myVal, out iTmp);
                        strReturn = iTmp.ToString();
                        break;
                }
            }
            else
            {
                switch (this.MEA.C502.f_6313)
                {
                    case MEA_C502.f_6313_selVal_AAA:
                        strReturn = myVal;
                        break;
                    case MEA_C502.f_6313_selVal_AAB:
                        strReturn = myVal;
                        break;
                    case MEA_C502.f_6313_selVal_AAD:
                    case MEA_C502.f_6313_selVal_AAL:
                    case MEA_C502.f_6313_selVal_AAE:
                        strReturn = myVal;
                        break;

                    case MEA_C502.f_6313_selVal_G:
                        decimal.TryParse(myVal, out decTmp);
                        if ((decimal.TryParse(myVal, out decTmp)) && (decTmp > 0))
                        {
                            strReturn = ediHelper_FormatString.FormatValue_Decimal((decTmp / 1000), string.Empty, 3);
                        }
                        break;
                    case MEA_C502.f_6313_selVal_T:
                        decimal.TryParse(myVal, out decTmp);
                        if ((decimal.TryParse(myVal, out decTmp)) && (decTmp > 0))
                        {
                            //strReturn = ediHelper_FormatString.FormatValue_Decimal((decTmp / 1000), string.Empty, 0);
                            strReturn = ediHelper_FormatString.FormatValue_Decimal((decTmp), string.Empty, 0);
                        }
                        break;
                }
            }
            return strReturn;
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_6411;
            if (!this.f_6411.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_6314;
            }
        }

        //===================================================================================

        /// <summary>
        ///             MessageTyp 
        /// </summary>
        public string f_6411_MeasureUnitQualifier { get; set; } = string.Empty;
        //public int f_6314_MeasurementValue { get; set; } = 0;
        public decimal f_6314_MeasurementValue { get; set; } = 0M;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public MEA_C174(string myEdiValueString)
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
                            this.f_6411_MeasureUnitQualifier = strValue[i].ToString();
                            break;
                        case 1:
                            //iTmp = 0;
                            //int.TryParse(strValue[i].ToString(), out iTmp);
                            //this.f_6314_MeasurementValue = iTmp;
                            string strVal = strValue[i].ToString();
                            if (strVal.Contains("."))
                            {
                                strVal = strVal.Replace('.', ',');
                            }
                            decimal decTmp = 0M;
                            decimal.TryParse(strVal, out decTmp);
                            this.f_6314_MeasurementValue = decTmp;
                            break;
                        case 2:
                            break;
                        case 3:
                        case 4:
                            break;
                    }
                }
            }
        }
    }
}
