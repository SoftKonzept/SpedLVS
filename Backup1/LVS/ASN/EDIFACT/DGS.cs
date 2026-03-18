using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class DGS
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "DGS";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Name des Modulbehälters (Modulname)
        /// </summary>
        /// <param name="myASNArt"></param>
        public DGS(clsEdiVDACreate myEdiCreate)
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
                        case DGS.Kennung_8273:
                            se8273 = new clsEdiSegmentElement();
                            se8273 = itm;
                            break;
                        case DGS_C205.Kennung:
                            C205 = new DGS_C205(this);
                            break;
                        case DGS_C234.Kennung:
                            C234 = new DGS_C234(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
                //}
                //break;
            }
        }

        internal DGS_C205 C205;
        internal DGS_C234 C234;

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        internal const string Kennung_8273 = "8273";
        /// <summary>       
        ///                 Gefahrgutvorschrift, Code
        /// </summary>
        private string _f_8273;
        public string f_8273
        {
            get
            {
                _f_8273 = string.Empty;
                if ((this.sef8273 is clsEdiSegmentElementField) && (this.sef8273.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef8273, string.Empty, string.Empty);
                    _f_8273 = v.ReturnValue;
                }
                return _f_8273;
            }
        }
        public clsEdiSegmentElement se8273;
        public clsEdiSegmentElementField sef8273
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se8273.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se8273.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == DGS.Kennung_8273);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += DGS.Name;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_8273;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C205.Value;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C234.Value;
            this.Value += UNA.const_SegementEndzeichen;
        }



    }
}
