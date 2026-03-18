using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class GIN
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "GIN";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Name des Modulbehälters (Modulname)
        /// </summary>
        /// <param name="myASNArt"></param>
        public GIN(clsEdiVDACreate myEdiCreate)
        {
            this.EDICreate = myEdiCreate;
            this.Segment = myEdiCreate.ediSegment;

            //switch (this.Segment.AsnArt.Typ)
            //{
            //    case clsASNArt.const_Art_EdifactVDA4987:
            List<clsEdiSegmentElement> List_SE = this.Segment.ListEdiSegmentElement.Where(x => x.EdiSegmentId == this.Segment.ID && x.IsActive == true).ToList();
            if (List_SE.Count > 0)
            {

                foreach (clsEdiSegmentElement itm in List_SE)
                {
                    SelectedSegmentElement = itm;

                    switch (itm.Name)
                    {
                        case GIN.Kennung_7405:
                            if (se7405 is null)
                            {
                                se7405 = new clsEdiSegmentElement();
                                se7405 = itm;
                            }
                            break;
                        case GIN_C208.Kennung:
                            if (C208 is null)
                            {
                                C208 = new GIN_C208(this);
                            }
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
                //}
                //break;
            }
        }

        internal GIN_C208 C208;

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        public const string Kennung_7405 = "7405";
        /// <summary>
        ///            Objektidentifikation, Qualifier 
        ///            
        ///            AO Position number in package
        ///            AW Serial shipping container code
        ///            BN Serial number
        ///            BU Package buyer assigned identifier
        ///            CQ Internal control number - Behälternummer
        ///            ML Marking/label number
        /// </summary>
        private string _f_7405;
        public string f_7405
        {
            get
            {
                _f_7405 = string.Empty;
                if ((this.sef7405 is clsEdiSegmentElementField) && (this.sef7405.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef7405, string.Empty, string.Empty);
                    _f_7405 = v.ReturnValue;

                    if (_f_7405.Equals("BU"))
                    {
                        string str = string.Empty;
                    }
                }
                return _f_7405;
            }
            set
            {
                _f_7405 = value;
            }
        }

        public clsEdiSegmentElement se7405;
        public clsEdiSegmentElementField sef7405
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se7405.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se7405.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == GIN.Kennung_7405);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += GIN.Name;
            if (!f_7405.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_7405;
            }
            if (!this.C208.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C208.Value;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }



    }
}
