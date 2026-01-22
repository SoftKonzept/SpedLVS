using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class PAC_C531
    {
        internal clsEdiSegmentElement SegElement;
        internal PAC PAC;
        public const string Kennung_C531 = "C531";

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
        public PAC_C531(PAC myEdiElement)
        {
            this.PAC = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        public const string Kennung_7075 = "7075";
        /// <summary>
        ///            Not used
        /// </summary>
        private string _f_7075 = string.Empty;
        public string f_7075
        {
            get
            {
                _f_7075 = string.Empty;
                if ((this.sef7075 is clsEdiSegmentElementField) && (this.sef7075.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PAC.EDICreate, this.sef7075, string.Empty, string.Empty);
                    _f_7075 = v.ReturnValue;
                }
                return _f_7075;
            }
        }
        public clsEdiSegmentElementField sef7075
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PAC_C531.Kennung_7075);
                }
                return tmpSef;
            }
        }

        public const string Kennung_7233 = "7233";
        /// <summary>       
        ///            Verpackungsbezogene Informationen, Code 
        ///            Code 35 kennzeichnet ein Hauptpackmittel 
        ///            
        ///            35 Type of package
        ///            37 Package protection
        /// </summary>
        private string _f_7233 = string.Empty;
        public string f_7233
        {
            get
            {
                _f_7233 = string.Empty;
                if ((this.sef7233 is clsEdiSegmentElementField) && (this.sef7233.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PAC.EDICreate, this.sef7233, string.Empty, string.Empty);
                    _f_7233 = v.ReturnValue;
                }
                return _f_7233;
            }
        }
        public clsEdiSegmentElementField sef7233
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PAC_C531.Kennung_7233);
                }
                return tmpSef;
            }
        }

        public const string Kennung_7073 = "7073";
        /// <summary>       
        ///            Verpackungsbedingungen, Code 
        ///            
        ///            AAA Einwegverpackung, Lieferant zahlt 
        ///            AAB Einwegverpackung, Kunde zahlt 
        ///            AAC Mehrwegbehälter des Kunden 
        ///            AAD Mehrwegbehälter des Lieferanten
        /// </summary>
        private string _f_7073 = string.Empty;
        public string f_7073
        {
            get
            {
                _f_7073 = string.Empty;
                if ((this.sef7073 is clsEdiSegmentElementField) && (this.sef7073.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PAC.EDICreate, this.sef7073, string.Empty, string.Empty);
                    _f_7073 = v.ReturnValue;
                }
                return _f_7073;
            }
        }
        public clsEdiSegmentElementField sef7073
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PAC_C531.Kennung_7073);
                }
                return tmpSef;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {

            //if (!f_7075.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            //{
            //    this.Value += this.f_7075;
            //}
            this.Value += this.f_7075;
            if (!f_7233.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_7233;
            }
            if (!f_7073.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_7073;
            }

        }



    }
}
