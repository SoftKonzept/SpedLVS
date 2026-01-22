using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class SEL
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "SEL";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Sendungsnummer, vergeben vom Lieferanten (alt: SLB)
        /// </summary>
        /// <param name="myASNArt"></param>
        public SEL(clsEdiVDACreate myEdiCreate)
        {
            this.EDICreate = myEdiCreate;
            this.Segment = myEdiCreate.ediSegment;

            //switch (this.Segment.AsnArt.Typ)
            //{
            //    case clsASNArt.const_Art_EdifactVDA4987:
            List<clsEdiSegmentElement> List_SE = this.Segment.ListEdiSegmentElement.Where(x => x.EdiSegmentId == this.Segment.ID).ToList();
            if (List_SE.Count > 0)
            {

                foreach (clsEdiSegmentElement itm in List_SE)
                {
                    SelectedSegmentElement = itm;

                    switch (itm.Name)
                    {
                        case SEL.Kennung_9308:
                            se9308 = new clsEdiSegmentElement();
                            se9308 = itm;
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
                //}
                //break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        internal const string Kennung_9308 = "9308";
        /// <summary>
        ///            Plombennummer
        /// </summary>
        private string _f_9308;
        public string f_9308
        {
            get
            {
                _f_9308 = "XXX";
                if ((this.sef9308 is clsEdiSegmentElementField) && (this.sef9308.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef9308, string.Empty, string.Empty);
                    _f_9308 = v.ReturnValue;
                }
                return _f_9308;
            }
            set
            {
                _f_9308 = value;
            }
        }

        public clsEdiSegmentElement se9308;
        public clsEdiSegmentElementField sef9308
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se9308.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se9308.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == SEL.Kennung_9308);
                }
                return tmpSef;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += SEL.Name;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_9308;
            this.Value += UNA.const_SegementEndzeichen;
        }



    }
}
