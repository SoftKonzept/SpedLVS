using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class LIN
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "LIN";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Name des Modulbehälters (Modulname)
        /// </summary>
        /// <param name="myASNArt"></param>
        public LIN(clsEdiVDACreate myEdiCreate)
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
                        case LIN.Kennung_1082:
                            se1082 = new clsEdiSegmentElement();
                            se1082 = itm;
                            break;

                        case LIN.Kennung_1222:
                            se1222 = new clsEdiSegmentElement();
                            se1222 = itm;
                            break;

                        case LIN.Kennung_1229:
                            se1229 = new clsEdiSegmentElement();
                            se1229 = itm;
                            break;

                        case LIN.Kennung_7083:
                            se7083 = new clsEdiSegmentElement();
                            se7083 = itm;
                            break;

                        case LIN_C212.Kennung_C212:
                            C212 = new LIN_C212(this);
                            break;
                        case LIN_C829.Kennung_C829:
                            C829 = new LIN_C829(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
            }
        }

        internal LIN_C212 C212;
        internal LIN_C829 C829;

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        ///             Not used
        /// </summary>
        internal const string Kennung_1082 = "1082";

        public clsEdiSegmentElement se1082;
        public clsEdiSegmentElementField sef1082
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se1082.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se1082.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LIN.Kennung_1082);
                }
                return tmpSef;
            }
        }


        private string _f_1082 = string.Empty;
        public string f_1082
        {
            get
            {
                _f_1082 = string.Empty;
                if ((this.sef1082 is clsEdiSegmentElementField) && (this.sef1082.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef1082, string.Empty, string.Empty);
                    _f_1082 = v.ReturnValue;
                }
                return _f_1082;
            }
            set
            {
                _f_1082 = value;
            }
        }

        /// <summary>
        ///             Not used
        /// </summary>
        internal const string Kennung_1229 = "1229";

        public clsEdiSegmentElement se1229;
        public clsEdiSegmentElementField sef1229
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se1229.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se1229.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LIN.Kennung_1229);
                }
                return tmpSef;
            }
        }

        private string _f_1229 = string.Empty;
        public string f_1229
        {
            get
            {
                _f_1229 = string.Empty;
                if ((this.sef1229 is clsEdiSegmentElementField) && (this.sef1229.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef1229, string.Empty, string.Empty);
                    _f_1229 = v.ReturnValue;
                }
                return _f_1229;
            }
            set
            {
                _f_1229 = value;
            }
        }

        /// <summary>
        ///             Not used
        /// </summary>
        internal const string Kennung_1222 = "1222";

        public clsEdiSegmentElement se1222;
        public clsEdiSegmentElementField sef1222
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if ((this.se1222 != null) && (this.se1222.ListEdiSegmentElementFields.Count > 0))
                {
                    tmpSef = this.se1222.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LIN.Kennung_1222);
                }
                return tmpSef;
            }
        }

        private string _f_1222 = string.Empty;
        public string f_1222
        {
            get
            {
                _f_1222 = string.Empty;
                if ((this.sef1222 is clsEdiSegmentElementField) && (this.sef1222.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef1222, string.Empty, string.Empty);
                    _f_1222 = v.ReturnValue;
                }
                else
                {
                    _f_1222 = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                }
                return _f_1222;
            }
        }

        /// <summary>
        ///             Not used
        /// </summary>
        internal const string Kennung_7083 = "7083";

        public clsEdiSegmentElement se7083;
        public clsEdiSegmentElementField sef7083
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if ((this.se7083 != null) && (this.se7083.ListEdiSegmentElementFields.Count > 0))
                {
                    tmpSef = this.se7083.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LIN.Kennung_7083);
                }
                return tmpSef;
            }
        }

        private string _f_7083 = string.Empty;
        public string f_7083
        {
            get
            {
                _f_1222 = string.Empty;
                if ((this.sef7083 is clsEdiSegmentElementField) && (this.sef7083.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef7083, string.Empty, string.Empty);
                    _f_7083 = v.ReturnValue;
                }
                else
                {
                    _f_7083 = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                }
                return _f_7083;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += LIN.Name;
            if (!f_1082.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_1082;
            }
            if (!f_1229.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_1229;
            }
            if ((this.C212 is LIN_C212) && (!this.C212.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C212.Value;
            }
            if ((this.C829 is LIN_C829) && (!this.C829.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C829.Value;
            }
            if (!f_1222.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_1222;
            }
            if (!f_7083.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_7083;
            }

            this.Value += UNA.const_SegementEndzeichen;
        }


        //===================================================================================================================

        /// <summary>
        ///             Datum / Uhrzeit
        /// </summary>
        //public string f_6311_MeasurementQualifier { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public LIN(string myEdiValueString, string myAsnArt)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List<string> lElements = myEdiValueString.Split(new char[] { '+' }).ToList();
                for (int i = 0; i < lElements.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            var str0 = lElements[i];
                            break;
                        case 1:
                            var str1 = lElements[i];
                            break;
                        case 2:
                            var str2 = lElements[i];
                            break;
                        case 3:
                            C212 = new LIN_C212(lElements[i], myAsnArt);
                            break;
                        case 4:
                            C829 = new LIN_C829(lElements[i], myAsnArt);
                            break;
                        case 5:
                            var str5 = lElements[i];
                            break;
                        case 6:
                            break;
                    }
                }
            }
        }
    }
}
