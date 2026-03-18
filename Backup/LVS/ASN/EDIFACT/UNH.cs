using LVS.ASN.EDIFACT;
using LVS.Constants;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class UNH
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "UNH";
        internal clsEdiSegmentElement SelectedSegmentElement;


        public UNH()
        {
            se0062 = new clsEdiSegmentElement();
            S009 = new UNH_S009(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myASNArt"></param>
        public UNH(clsEdiVDACreate myEdiCreate)
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
                        case UNH.Kennung_0062:
                            se0062 = new clsEdiSegmentElement();
                            se0062 = itm;
                            break;
                        case UNH_S009.Kennung_S009:
                            S009 = new UNH_S009(this);
                            break;
                        case UNH_S010.Kennung_S010:
                            S010 = new UNH_S010(this);
                            break;
                    }
                }
                CreateValue();

            }

        }

        internal UNH_S009 S009;
        internal UNH_S010 S010;

        internal const string Kennung_0062 = "0062";
        /// <summary>
        ///             Nachrichten-Referenznummer 
        ///             Nachrichtenreferenznummer (im Interchange)
        /// </summary>
        private string _f_0062;
        public string f_0062
        {
            get
            {
                _f_0062 = string.Empty;
                if ((this.sef0062 is clsEdiSegmentElementField) && (this.sef0062.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef0062, string.Empty, string.Empty);
                    _f_0062 = v.ReturnValue;
                }
                return _f_0062;
            }
            set
            {
                _f_0062 = value;
            }
        }
        public clsEdiSegmentElement se0062;
        public clsEdiSegmentElementField sef0062
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se0062.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se0062.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNH.Kennung_0062);
                }
                return tmpSef;
            }
        }



        internal const string Kennung_0068 = "0068";
        /// <summary>
        /// </summary>
        private string _f_0068;
        public string f_0068
        {
            get
            {
                _f_0068 = string.Empty;
                if ((this.sef0068 is clsEdiSegmentElementField) && (this.sef0068.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef0068, string.Empty, string.Empty);
                    _f_0068 = v.ReturnValue;
                }
                return _f_0068;
            }
            set
            {
                _f_0068 = value;
            }
        }
        public clsEdiSegmentElement se0068;
        public clsEdiSegmentElementField sef0068
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se0068.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se0068.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNH.Kennung_0068);
                }
                return tmpSef;
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
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += UNH.Name;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_0062;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + S009.Value;
            this.Value += UNA.const_SegementEndzeichen;
        }


        //===================================================================================================================

        /// <summary>
        ///             Datum / Uhrzeit
        /// </summary>
        public string f_0062_MesRefNo { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public UNH(string myEdiValueString, string myAsnArt)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List<string> lElements = myEdiValueString.Split(new char[] { '+' }).ToList();
                switch (myAsnArt)
                {
                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                    case constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A:
                        for (int i = 0; i < lElements.Count; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    break;
                                case 1:
                                    f_0062_MesRefNo = lElements[i].ToString();
                                    break;
                                case 2:
                                    S009 = new UNH_S009(lElements[i].ToString(), myAsnArt);
                                    break;
                                case 3:
                                case 4:
                                    break;
                            }
                        }
                        break;
                }
            }
        }
    }
}
