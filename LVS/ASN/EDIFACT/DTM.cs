using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class DTM
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "DTM";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        public DTM()
        {
            C507 = new DTM_C507(this);
        }

        /// <summary>
        ///             Datum der DESADV Nachricht
        /// </summary>
        /// <param name="myASNArt"></param>
        public DTM(clsEdiVDACreate myEdiCreate)
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
                        case DTM_C507.Kennung:
                            C507 = new DTM_C507(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
            }
        }

        public const int const_DateQualifier_C507_2005_4_OrdertDate = 4;
        public const int const_DateQualifier_C507_2005_11_ShipmentDate = 11;
        public const int const_DateQualifier_C507_2005_94_ProductionDate = 94;
        public const int const_DateQualifier_C507_2005_132_ArrivalDate = 132;
        public const int const_DateQualifier_C507_2005_137_MessageDate = 137;


        internal DTM_C507 C507;

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
            this.Value += DTM.Name;
            if (!this.C507.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C507.Value;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }


        //===================================================================================================================

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public DTM(string myEdiValueString)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List<string> lElements = myEdiValueString.Split(new char[] { '+' }).ToList();
                for (int i = 0; i < lElements.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            break;
                        case 1:
                            C507 = new DTM_C507(lElements[i].ToString());
                            break;
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
