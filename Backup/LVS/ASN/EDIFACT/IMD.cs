using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class IMD
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "IMD";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;


        public IMD()
        {
            C272 = new IMD_C272(this);
            C273 = new IMD_C273(this);
        }
        /// <summary>
        ///            Produkt-/Leistungsbeschreibung
        /// </summary>
        /// <param name="myASNArt"></param>
        public IMD(clsEdiVDACreate myEdiCreate)
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
                        case IMD.Kennung_7077:
                            se7077 = new clsEdiSegmentElement();
                            se7077 = itm;
                            break;
                        case IMD.Kennung_7081:
                            se7081 = new clsEdiSegmentElement();
                            se7081 = itm;
                            break;
                        case IMD.Kennung_7383:
                            se7383 = new clsEdiSegmentElement();
                            se7383 = itm;
                            break;

                        case IMD_C272.Kennung:
                            C272 = new IMD_C272(this);
                            break;
                        case IMD_C273.Kennung:
                            C273 = new IMD_C273(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();

            }
        }

        internal IMD_C272 C272;
        internal IMD_C273 C273;


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
        /// 
        public const string Kennung_7077 = "7077";
        private string _f_7077;
        public string f_7077
        {
            get
            {
                _f_7077 = string.Empty;
                if ((this.sef7077 is clsEdiSegmentElementField) && (this.sef7077.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef7077, string.Empty, string.Empty);
                    _f_7077 = v.ReturnValue;
                }
                return _f_7077;
            }
            set
            {
                _f_7077 = value;
            }
        }

        public clsEdiSegmentElement se7077;
        public clsEdiSegmentElementField sef7077
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se7077.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se7077.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == IMD.Kennung_7077);
                }
                return tmpSef;
            }
        }

        /// <summary>
        ///            Not used
        /// </summary>
        /// 
        public const string Kennung_7081 = "7081";
        private string _f_7081;
        public string f_7081
        {
            get
            {
                _f_7081 = string.Empty;
                if ((this.sef7081 is clsEdiSegmentElementField) && (this.sef7081.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef7081, string.Empty, string.Empty);
                    _f_7081 = v.ReturnValue;
                }
                return _f_7081;
            }
            set
            {
                _f_7081 = value;
            }
        }

        public clsEdiSegmentElement se7081;
        public clsEdiSegmentElementField sef7081
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if ((se7081 is clsEdiSegmentElement) && (this.se7081.ListEdiSegmentElementFields.Count > 0))
                {
                    tmpSef = this.se7081.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == IMD.Kennung_7081);
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///            Not used
        /// </summary>
        /// 
        public const string Kennung_7383 = "7383";
        private string _f_7383;
        public string f_7383
        {
            get
            {
                _f_7383 = string.Empty;
                if ((this.sef7383 is clsEdiSegmentElementField) && (this.sef7383.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef7383, string.Empty, string.Empty);
                    _f_7383 = v.ReturnValue;
                }
                else
                {
                    _f_7383 = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                }
                return _f_7383;
            }
            set
            {
                _f_7383 = value;
            }
        }

        public clsEdiSegmentElement se7383;
        public clsEdiSegmentElementField sef7383
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if ((se7383 is clsEdiSegmentElement) && (this.se7383.ListEdiSegmentElementFields.Count > 0))
                {
                    tmpSef = this.se7383.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == IMD.Kennung_7383);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += IMD.Name;
            if (!this.f_7077.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_7077;
            }
            if (!this.f_7081.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_7081;
            }
            if ((this.C272 is IMD_C272) && (!this.C272.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C272.Value;
            }
            if ((this.C273 is IMD_C273) && (!this.C273.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C273.Value;
            }
            if (!this.f_7383.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_7383;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }



    }
}
