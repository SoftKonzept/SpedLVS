using LVS.ASN.EDIFACT;
using LVS.Constants;
using LVS.ViewData;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class BGM
    {

        public BGM()
        {
            se1225 = new clsEdiSegmentElement();
            C002 = new BGM_C002(this);
            C106 = new BGM_C106(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myASNArt"></param>
        public BGM(clsEdiVDACreate myEdiCreate)
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
                        case BGM.Kennung_1225:
                            se1225 = new clsEdiSegmentElement();
                            se1225 = itm;
                            break;

                        case BGM_C002.Kennung_C002:
                            C002 = new BGM_C002(this);
                            break;
                        case BGM_C106.Kennung_C106:
                            C106 = new BGM_C106(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
            }
        }
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "BGM";
        internal clsEdiSegmentElement SelectedSegmentElement;


        internal BGM_C002 C002;
        internal BGM_C106 C106;

        public const int const_DocumentMessageNameCode_C002_1001_351_DespatchAdvice = 351;


        /// <summary>
        ///             Nachrichtenfunktion, Code 9 Original
        /// </summary>
        internal const string Kennung_1225 = "1225";
        private string _f_1225 = string.Empty;
        public string f_1225
        {
            get
            {
                _f_1225 = "9";
                if ((this.sef1225 is clsEdiSegmentElementField) && (this.sef1225.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef1225, string.Empty, string.Empty);
                    _f_1225 = v.ReturnValue;
                }
                return _f_1225;
            }
            set
            {
                _f_1225 = value;
            }
        }

        public clsEdiSegmentElement se1225;
        public clsEdiSegmentElementField sef1225
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se1225.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se1225.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == BGM.Kennung_1225);
                }
                return tmpSef;
            }
        }


        /// <summary>
        ///            
        /// </summary>
        internal const string Kennung_4343 = "4343";
        private string _f_4343 = string.Empty;
        public string f_4343
        {
            get
            {
                _f_4343 = string.Empty;
                if ((this.sef4343 is clsEdiSegmentElementField) && (this.sef4343.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef4343, string.Empty, string.Empty);
                    _f_4343 = v.ReturnValue;
                }
                return _f_4343;
            }
            set
            {
                _f_4343 = value;
            }
        }

        public clsEdiSegmentElement se4343;
        public clsEdiSegmentElementField sef4343
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se4343.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se4343.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == BGM.Kennung_4343);
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
            this.Value += BGM.Name;
            if (!this.C002.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C002.Value;
            }
            if (!this.C106.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C106.Value;
            }
            if (!f_1225.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_1225;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }

        //===================================================================================================================

        /// <summary>
        ///             Datum / Uhrzeit
        /// </summary>
        public string f_1004_DesAdvNo { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public BGM(string myEdiValueString, string myAsnArt)
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
                                    C002 = new BGM_C002(lElements[i].ToString(), myAsnArt);
                                    break;
                                case 2:
                                    f_1004_DesAdvNo = lElements[i].ToString();
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


        /// <summary>
        /// 
        /// </summary>

        public static int GetMessageId(string myEdiValueString, int myAsnArtId)
        {
            int iReturn = 0;

            AsnArtViewData asnArtVD = new AsnArtViewData(myAsnArtId, 1, false);

            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List<string> lSegementElements = myEdiValueString.Split(new char[] { '\'' }).ToList();
                foreach (string s in lSegementElements)
                {
                    if (s.StartsWith(BGM.Name))
                    {
                        BGM b = new BGM(s, asnArtVD.AsnArt.Typ);
                        int iTmp = 0;
                        int.TryParse(b.f_1004_DesAdvNo, out iTmp);
                        iReturn = iTmp;
                        break;
                    }
                }
            }
            return iReturn;
        }

    }
}
