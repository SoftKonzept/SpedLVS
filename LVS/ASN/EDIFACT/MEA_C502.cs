using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class MEA_C502
    {
        internal clsEdiSegmentElement SegElement;
        internal MEA MEA;
        public const string Kennung_C502 = "C502";

        private string _value;
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
        public MEA_C502(MEA myEdiElement)
        {
            this.MEA = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        public const string f_6313_selVal_AAA = "AAA";
        public const string f_6313_selVal_AAB = "AAB";
        public const string f_6313_selVal_AAD = "AAD";
        public const string f_6313_selVal_AAE = "AAE";
        public const string f_6313_selVal_AAL = "AAL";
        public const string f_6313_selVal_G = "G";
        public const string f_6313_selVal_T = "T";
        /// <summary>
        ///             Gemessene Dimension, Code
        ///             
        ///             AAA Net weight
        ///             AAB Goods item gross weight - Ladeeinheit
        ///             AAD Consignment gross weight
        ///             AAL Net weight
        ///             G Gross weight - Brutto Artikel
        ///             T Tare weight
        ///             ABJ Volume
        ///             
        /// </summary>
        /// 
        internal const string Kennung_6313 = "6313";
        private string _f_6313;
        public string f_6313
        {
            get
            {
                _f_6313 = string.Empty;
                if ((this.sef6313 is clsEdiSegmentElementField) && (this.sef6313.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.MEA.EDICreate, this.sef6313, string.Empty, string.Empty);
                    _f_6313 = v.ReturnValue;
                    if (_f_6313.Equals(MEA_C502.f_6313_selVal_AAE))
                    {
                        string sts = string.Empty;
                    }
                }
                return _f_6313;
            }
        }
        public clsEdiSegmentElementField sef6313
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    //tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == "6313");
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == Kennung_6313);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_6313;
        }



        //===================================================================================

        /// <summary>
        ///             MessageTyp 
        /// </summary>
        public string f_6313_MeasurementCode { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public MEA_C502(string myEdiValueString)
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
                            this.f_6313_MeasurementCode = strValue[i].ToString();
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            break;
                    }
                }
            }
        }
    }
}
