using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class SCC
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "SCC";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;


        public SCC()
        {
            //C329 = new SSC_C329(this);
        }
        /// <summary>
        ///            Produkt-/Leistungsbeschreibung
        /// </summary>
        /// <param name="myASNArt"></param>
        public SCC(clsEdiVDACreate myEdiCreate)
        {
            this.EDICreate = myEdiCreate;
            this.Segment = myEdiCreate.ediSegment;

            List<clsEdiSegmentElement> List_SE = this.Segment.ListEdiSegmentElement.Where(x => x.EdiSegmentId == this.Segment.ID).ToList();
            if (List_SE.Count > 0)
            {

                foreach (clsEdiSegmentElement itm in List_SE)
                {
                    SelectedSegmentElement = itm;

                    //switch (itm.Name)
                    //{
                    //    case SSC_C329.Kennung:
                    //        C329 = new SSC_C329(this);
                    //        break;
                    //}
                    SelectedSegmentElement = null;
                }
                CreateValue();
            }
        }

        //internal SSC_329 C329;



        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }


        /// <summary>
        ///            Not used
        /// </summary>
        private string _f_4017;
        public string f_4017
        {
            get
            {
                _f_4017 = string.Empty;
                return _f_4017;
            }
            set
            {
                _f_4017 = value;
            }
        }


        /// <summary>
        ///            Not used
        /// </summary>
        private string _f_4493;
        public string f_4493
        {
            get
            {
                _f_4493 = string.Empty;
                return _f_4493;
            }
            set
            {
                _f_4493 = value;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value +=
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_4017;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_4493;
            //this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C329.Value;
            this.Value += UNA.const_SegementEndzeichen;
        }



    }
}
