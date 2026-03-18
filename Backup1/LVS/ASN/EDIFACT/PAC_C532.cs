using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class PAC_C532
    {
        internal clsEdiSegmentElement SegElement;
        internal PAC PAC;
        public const string Kennung_C532 = "C532";

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
        ///            Verpackungsangaben
        /// </summary>
        /// <param name="mySegElement"></param>
        public PAC_C532(PAC myEdiElement)
        {
            this.PAC = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }




        /// <summary>       
        ///            Verpackungsbezogene Informationen, Code 
        ///            Code 35 kennzeichnet ein Hauptpackmittel 
        ///            
        ///            35 Type of package
        ///            37 Package protection
        /// </summary>
        /// 
        internal const string Kennung_8395 = "8395";
        private string _f_8395 = string.Empty;
        public string f_8395
        {
            get
            {
                _f_8395 = string.Empty;
                if ((this.sef8395 is clsEdiSegmentElementField) && (this.sef8395.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PAC.EDICreate, this.sef8395, string.Empty, string.Empty);
                    _f_8395 = v.ReturnValue;
                }
                return _f_8395;
            }
        }
        public clsEdiSegmentElementField sef8395
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PAC_C532.Kennung_8395);
                }
                return tmpSef;
            }
        }


        /// <summary>       
        ///            Verpackungsbedingungen, Code 
        ///            
        ///            AAA Einwegverpackung, Lieferant zahlt 
        ///            AAB Einwegverpackung, Kunde zahlt 
        ///            AAC Mehrwegbehälter des Kunden 
        ///            AAD Mehrwegbehälter des Lieferanten
        /// </summary>
        /// 
        internal const string Kennung_8393 = "8393";
        private string _f_8393 = string.Empty;
        public string f_8393
        {
            get
            {
                _f_8393 = string.Empty;
                if ((this.sef8393 is clsEdiSegmentElementField) && (this.sef8393.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PAC.EDICreate, this.sef8393, string.Empty, string.Empty);
                    _f_8393 = v.ReturnValue;
                }
                return _f_8393;
            }
        }
        public clsEdiSegmentElementField sef8393
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PAC_C532.Kennung_8393);
                }
                return tmpSef;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            //if (!f_8395.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            //{
            //    this.Value += this.f_8395;
            //}
            this.Value = this.f_8395;
            if (!f_8393.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_8393;
            }
        }
    }
}
