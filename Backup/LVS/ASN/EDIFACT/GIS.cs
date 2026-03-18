using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class GIS
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "GIS";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;


        public GIS()
        {
            C529 = new GIS_C529(this);
        }

        /// <summary>
        ///             Name des Modulbehälters (Modulname)
        /// </summary>
        /// <param name="myASNArt"></param>
        public GIS(clsEdiVDACreate myEdiCreate)
        {
            this.EDICreate = myEdiCreate;
            this.Segment = myEdiCreate.ediSegment;

            List<clsEdiSegmentElement> List_SE = this.Segment.ListEdiSegmentElement.Where(x => x.EdiSegmentId == this.Segment.ID && x.IsActive == true).ToList();
            if (List_SE.Count > 0)
            {
                foreach (clsEdiSegmentElement itm in List_SE)
                {
                    SelectedSegmentElement = itm;
                    switch (itm.Name)
                    {
                        case GIN_C208.Kennung:
                            if (C529 is null)
                            {
                                C529 = new GIS_C529(this);
                            }
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
            }
        }

        internal GIS_C529 C529;

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
            this.Value += GIS.Name;
            if (!this.C529.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C529.Value;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }



    }
}
