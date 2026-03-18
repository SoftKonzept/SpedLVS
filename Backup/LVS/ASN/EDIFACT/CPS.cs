using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class CPS
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "CPS";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///            Frachtträger / Transportmittel (Anhänger / Wechselbrücke)
        /// </summary>
        /// <param name="myASNArt"></param>
        public CPS(clsEdiVDACreate myEdiCreate)
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
                        case CPS.Kennung_7164:
                            se7164 = new clsEdiSegmentElement();
                            se7164 = itm;
                            break;
                        case CPS.Kennung_7075:
                            se7075 = new clsEdiSegmentElement();
                            se7075 = itm;
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
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

        internal const string Kennung_7164 = "7164";
        /// <summary>
        ///            Hierarchie-Ebene, Identifikation Vom Nachrichtensender generierter aufsteigender Zähler, 
        ///            der einer Packmittelgruppe innerhalb der Nachricht zugeordnet wird. Es wird empfohlen, 
        ///            mit 1 zu beginnen und aufsteigend zu nummerieren.
        /// </summary>
        private string _f_7164;
        public string f_7164
        {
            get
            {
                _f_7164 = string.Empty;
                //_f_7164 = this.EDICreate.CounterLoop.ToString();
                if ((this.sef7164 is clsEdiSegmentElementField) && (this.sef7164.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef7164, string.Empty, string.Empty);
                    _f_7164 = v.ReturnValue;
                }
                return _f_7164;
            }
            set
            {
                _f_7164 = value;
            }
        }

        public clsEdiSegmentElement se7164;
        public clsEdiSegmentElementField sef7164
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se7164.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se7164.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == CPS.Kennung_7164);
                }
                return tmpSef;
            }
        }

        /// <summary>
        ///            Not used
        /// </summary>
        /// 
        public const string Kennung_7166 = "7166";

        public clsEdiSegmentElement se7166;
        public clsEdiSegmentElementField sef7166
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se7166.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se7166.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == CPS.Kennung_7166);
                }
                return tmpSef;
            }
        }
        private string _f_7166;
        public string f_7166
        {
            get
            {
                _f_7166 = string.Empty;
                return _f_7166;
            }
        }

        public const string Kennung_7075 = "7075";
        /// <summary>
        ///            Code für die Ebene der Verpackung. 3 Outer Äußere Ladeeinheit, höchste Verpackungsebene 
        ///            (z.B. Palette oder Großladungsträger) mit weiteren Unterverpackungen 2 Intermediateen.
        ///            
        ///            Code für die Ebene der Verpackung. 
        ///            Code 4 steht für vereinfachte Ladeeinheiten 
        ///            1 Inner 
        ///            2 Intermediate
        ///            3 Outer
        ///            4 No packaging hierarchy
        /// </summary>
        private string _f_7075;
        public string f_7075
        {
            get
            {
                _f_7075 = string.Empty;
                if ((this.sef7075 is clsEdiSegmentElementField) && (this.sef7075.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef7075, string.Empty, string.Empty);
                    _f_7075 = v.ReturnValue;
                }
                return _f_7075;
            }
            set
            {
                _f_7075 = value;
            }
        }

        public clsEdiSegmentElement se7075;
        public clsEdiSegmentElementField sef7075
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se7075.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se7075.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == CPS.Kennung_7075);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += CPS.Name;

            if (!f_7164.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_7164;
            }
            if (!f_7166.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_7166;
            }
            if (!f_7075.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_7075;
            }


            this.Value += UNA.const_SegementEndzeichen;
        }


        //===================================================================================================================

        ///// <summary>
        /////             Datum / Uhrzeit
        ///// </summary>
        //public string f_6311_MeasurementQualifier { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public CPS(string myEdiValueString, string myAsnArt)
        {
            //if ((myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            //{
            //    //C506 = new RFF_C506(this);
            //    //C174 = new MEA_C174(this);
            //    List<string> strValue = myEdiValueString.Split(new char[] { ':', '+' }).ToList();

            //    for (int i = 0; i < strValue.Count; i++)
            //    {
            //        switch (i)
            //        {
            //            case 0:
            //                break;
            //            case 1:
            //                switch (myAsnArt)
            //                {
            //                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
            //                        C506.f_1153_RefQualifier = strValue[i].ToString(); ;
            //                        break;
            //                }
            //                break;
            //            case 2:
            //                switch (myAsnArt)
            //                {
            //                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
            //                        C506.f_1154_RefNumber = strValue[i].ToString();
            //                        break;
            //                }
            //                break;
            //                //case 3:
            //                //    switch (myAsnArt)
            //                //    {
            //                //        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
            //                //            C506.f_1154_RefNumber = strValue[i].ToString();
            //                //            break;
            //                //    }
            //                //    break;
            //                //case 4:
            //                //    switch (myAsnArt)
            //                //    {
            //                //        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
            //                //            int iTmp = 0;
            //                //            int.TryParse(strValue[i].ToString(), out iTmp);
            //                //            C174.f_6314_MeasurementValue = iTmp;
            //                //            break;
            //                //    }
            //                //    break;
            //        }
            //    }

            //}
        }
    }
}
