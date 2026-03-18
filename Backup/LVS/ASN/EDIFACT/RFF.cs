using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class RFF
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "RFF";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Sendungsnummer, vergeben vom Lieferanten (alt: SLB)
        /// </summary>
        /// <param name="myASNArt"></param>
        public RFF(clsEdiVDACreate myEdiCreate)
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
                        case RFF_C506.Kennung:
                            C506 = new RFF_C506(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
            }
        }

        internal RFF_C506 C506;

        public const string const_ReferenzQualifier_C506_1153_AAS_TransportDocumentNumber = "AAS";
        public const string const_ReferenzQualifier_C506_1153_AAT_MasterLableNumber = "AAT";
        public const string const_ReferenzQualifier_C506_1153_ADE_AcountNumber = "ADE";
        public const string const_ReferenzQualifier_C506_1153_ON_OrderNumber = "ON";
        public const string const_ReferenzQualifier_C506_1153_VN_SalesOrderNumber = "VN";

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += RFF.Name;
            if (!this.C506.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C506.Value;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }


        //===================================================================================================================

        ///// <summary>
        /////             Datum / Uhrzeit
        ///// </summary>
        //public string f_6311_MeasurementQualifier { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public RFF(string myEdiValueString, string myAsnArt)
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
                            C506 = new RFF_C506(strValue[i]);
                            break;
                        case 2:
                            break;
                    }
                }

            }
        }
    }
}
