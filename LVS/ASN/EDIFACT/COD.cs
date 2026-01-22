using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class COD
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "COD";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Name des Modulbehälters (Modulname)
        /// </summary>
        /// <param name="myASNArt"></param>
        public COD(clsEdiVDACreate myEdiCreate)
        {
            this.EDICreate = myEdiCreate;
            this.Segment = myEdiCreate.ediSegment;

            //switch (this.Segment.AsnArt.Typ)
            //{
            //    case clsASNArt.const_Art_EdifactVDA4987:
            //    case clsASNArt.const_Art_DESADV_BMW_4a:
            List<clsEdiSegmentElement> List_SE = this.Segment.ListEdiSegmentElement.Where(x => x.EdiSegmentId == this.Segment.ID).ToList();
            if (List_SE.Count > 0)
            {

                foreach (clsEdiSegmentElement itm in List_SE)
                {
                    SelectedSegmentElement = itm;

                    switch (itm.Name)
                    {
                        case COD_C823.Kennung_C823:
                            C823 = new COD_C823(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
            }
            //        break;
            //}
        }

        internal COD_C823 C823;

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
            this.Value += COD.Name;
            if (!this.C823.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C823.Value;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }



    }
}
