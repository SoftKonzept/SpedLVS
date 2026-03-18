using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class TOD
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "TOD";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Sendungsnummer, vergeben vom Lieferanten (alt: SLB)
        /// </summary>
        /// <param name="myASNArt"></param>
        public TOD(clsEdiVDACreate myEdiCreate)
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
                        case TOD.Kennung_4055:
                            se4055 = new clsEdiSegmentElement();
                            se4055 = itm;
                            break;
                        case TOD.Kennung_4215:
                            se4215 = new clsEdiSegmentElement();
                            se4215 = itm;
                            break;
                        case TOD_C100.Kennung:
                            C100 = new TOD_C100(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
                //}
                //break;
            }
        }

        internal TOD_C100 C100;

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }


        /// <summary>
        ///            Liefer- oder Transportbedingungsfunktion, Code 
        ///            6 Delivery condition
        ///             
        /// </summary>

        internal const string Kennung_4055 = "4055";
        private string _f_4055 = string.Empty;
        public string f_4055
        {
            get
            {
                _f_4055 = "XXX";
                if ((this.sef4055 is clsEdiSegmentElementField) && (this.sef4055.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef4055, string.Empty, string.Empty);
                    _f_4055 = v.ReturnValue;
                }
                return _f_4055;
            }
            set
            {
                _f_4055 = value;
            }
        }

        public clsEdiSegmentElement se4055;
        public clsEdiSegmentElementField sef4055
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se4055.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se4055.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == TOD.Kennung_4055);
                }
                return tmpSef;
            }
        }

        /// <summary>
        ///            Not used
        /// </summary>
        /// 
        internal const string Kennung_4215 = "4215";
        public clsEdiSegmentElement se4215;
        public clsEdiSegmentElementField sef4215
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se4215.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se4215.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == TOD.Kennung_4215);
                }
                return tmpSef;
            }
        }
        private string _f_4215 = string.Empty;
        public string f_4215
        {
            get
            {
                _f_4215 = string.Empty;
                if ((this.sef4215 is clsEdiSegmentElementField) && (this.sef4215.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef4215, string.Empty, string.Empty);
                    _f_4215 = v.ReturnValue;
                }
                return _f_4215;
            }
            set
            {
                _f_4215 = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += TOD.Name;
            if (!f_4055.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_4055;
            }
            if (!f_4215.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_4215;
            }
            if (!this.C100.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C100.Value;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }



    }
}
