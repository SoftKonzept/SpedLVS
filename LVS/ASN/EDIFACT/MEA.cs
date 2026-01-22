using LVS.ASN.EDIFACT;
using LVS.Constants;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class MEA
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "MEA";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Bruttogewicht der Sendung
        /// </summary>
        /// <param name="myASNArt"></param>
        public MEA(clsEdiVDACreate myEdiCreate)
        {
            this.EDICreate = myEdiCreate;
            this.Segment = myEdiCreate.ediSegment;


            List<clsEdiSegmentElement> List_SE = this.Segment.ListEdiSegmentElement.Where(x => x.EdiSegmentId == this.Segment.ID).ToList();
            if (List_SE.Count > 0)
            {
                foreach (clsEdiSegmentElement itm in List_SE)
                {
                    SelectedSegmentElement = itm;
                    switch (itm.Name)
                    {
                        case MEA_C502.Kennung_C502:
                            C502 = new MEA_C502(this);
                            break;
                        case MEA_C174.Kennung_C174:
                            C174 = new MEA_C174(this);
                            break;
                        case MEA.Kennung_6311:
                            se6311 = new clsEdiSegmentElement();
                            se6311 = itm;
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();

            }
        }

        internal MEA_C502 C502;
        internal MEA_C174 C174;
        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        public const string const_MeasurementQualifier_6311_AAX_ConsignmentMeasurement = "AAX";
        public const string const_MeasurementQualifier_6311_AAY_PackageMeasurement = "AAY";
        public const string const_MeasurementQualifier_6311_PD_Dimensions = "PD";

        public const string const_MeasuredCoded_C502_6313_AAD_TotalGross = "AAD";
        public const string const_MeasuredCoded_C502_6313_AAC_TotalNet = "AAC";
        public const string const_MeasuredCoded_C502_6313_AAG_PackageGross = "AAG";
        public const string const_MeasuredCoded_C502_6313_AAL_PackageNet = "AAL";
        public const string const_MeasuredCoded_C502_6313_LN_Length = "LN";
        public const string const_MeasuredCoded_C502_6313_WD_Width = "WD";
        public const string const_MeasuredCoded_C502_6313_TH_Thickness = "TH";

        public const string const_MeasuredUnitQualifier_C174_6411_MMT_Millimeter = "MMT";
        public const string const_MeasuredUnitQualifier_C174_6411_KGM_Kilogramm = "KGM";

        internal const string Kennung_6311 = "6311";
        /// <summary>
        ///             Messung, Zweck, Qualifier
        ///             
        ///             AAZ Handling unit measurement - Ladeeinheit
        ///             AAY Package measurement
        ///             AAX Consignment measurement
        ///             
        /// </summary>
        private string _f_6311;
        public string f_6311
        {
            get
            {
                _f_6311 = string.Empty;
                if ((this.sef6311 is clsEdiSegmentElementField) && (this.sef6311.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef6311, string.Empty, string.Empty);
                    _f_6311 = v.ReturnValue;
                }
                return _f_6311;
            }
            set
            {
                _f_6311 = value;
            }
        }

        public clsEdiSegmentElement se6311;
        public clsEdiSegmentElementField sef6311
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se6311.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se6311.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == MEA.Kennung_6311);
                }
                return tmpSef;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += MEA.Name;
            if (!f_6311.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_6311;
            }
            if (!this.C502.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C502.Value;
            }
            if (!this.C174.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C174.Value;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }



        //===================================================================================================================

        /// <summary>
        ///             Datum / Uhrzeit
        /// </summary>
        public string f_6311_MeasurementQualifier { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public MEA(string myEdiValueString, string myAsnArt)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List<string> strValue = myEdiValueString.Split(new char[] { '+' }).ToList();
                for (int i = 0; i < strValue.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            break;
                        case 1:
                            switch (myAsnArt)
                            {
                                case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                    f_6311_MeasurementQualifier = strValue[i].ToString();
                                    break;
                            }
                            break;
                        case 2:
                            C502 = new MEA_C502(strValue[i]);
                            //switch (myAsnArt)
                            //{
                            //    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                            //        C502.f_6313_MeasurementCode = strValue[i].ToString();
                            //        break;
                            //}
                            break;
                        case 3:
                            C174 = new MEA_C174(strValue[i]);
                            //switch (myAsnArt)
                            //{
                            //    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                            //        C174.f_6411_MeasureUnitQualifier = strValue[i].ToString();
                            //        break;
                            //}
                            break;
                        case 4:
                            break;
                    }
                }

            }
        }
    }
}
