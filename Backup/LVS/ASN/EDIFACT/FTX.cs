using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class FTX
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "FTX";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Präferenzberechtigung
        /// </summary>
        /// <param name="myASNArt"></param>
        public FTX(clsEdiVDACreate myEdiCreate)
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

                        case FTX.Kennung_4453:
                            se4453 = new clsEdiSegmentElement();
                            se4453 = itm;
                            break;

                        case FTX.Kennung_4451:
                            se4451 = new clsEdiSegmentElement();
                            se4451 = itm;
                            break;
                        case FTX.Kennung_3453:
                            se3453 = new clsEdiSegmentElement();
                            se3453 = itm;
                            break;
                        case FTX_C107.Kennung_C107:
                            C107 = new FTX_C107(this);
                            break;
                        case FTX_C108.Kennung_C108:
                            C108 = new FTX_C108(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
                //}
                //break;
            }
        }

        internal FTX_C107 C107;
        internal FTX_C108 C108;

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        internal const string Kennung_4451 = "4451";
        /// <summary>
        ///            Textbezug, Qualifier 
        ///            CUS Zollanmeldungsinformation        ///             
        /// </summary>
        private string _f_4451 = string.Empty;
        public string f_4451
        {
            get
            {
                _f_4451 = "XXX";
                if ((this.sef4451 is clsEdiSegmentElementField) && (this.sef4451.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef4451, string.Empty, string.Empty);
                    _f_4451 = v.ReturnValue;
                }
                return _f_4451;
            }
            set
            {
                _f_4451 = value;
            }
        }

        public clsEdiSegmentElement se4451;
        public clsEdiSegmentElementField sef4451
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se4451.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se4451.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == FTX.Kennung_4451);
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///            Not used
        /// </summary>
        internal const string Kennung_4453 = "4453";

        private string _f_4453 = string.Empty;
        public string f_4453
        {
            get
            {
                _f_4453 = string.Empty;
                if ((this.sef4453 is clsEdiSegmentElementField) && (this.sef4453.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef4453, string.Empty, string.Empty);
                    _f_4453 = v.ReturnValue;
                }
                return _f_4453;
            }
            set
            {
                _f_4453 = value;
            }
        }
        public clsEdiSegmentElement se4453;
        public clsEdiSegmentElementField sef4453
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se4453.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se4453.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == FTX.Kennung_4453);
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///            Sprachenname, Code 
        ///            Sprache, codiert; verwende ISO 639-1988            
        /// </summary>
        internal const string Kennung_3453 = "3453";
        private string _f_3453 = string.Empty;
        public string f_3453
        {
            get
            {
                //_f_3453 = "XXX";
                if ((this.sef3453 is clsEdiSegmentElementField) && (this.sef3453.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef3453, string.Empty, string.Empty);
                    _f_3453 = v.ReturnValue;
                }
                return _f_3453;
            }
            set
            {
                _f_3453 = value;
            }
        }

        public clsEdiSegmentElement se3453;
        public clsEdiSegmentElementField sef3453
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se3453.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se3453.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == FTX.Kennung_3453);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += FTX.Name;
            if (!f_4451.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_4451;
            }
            if (!f_4453.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_4453;
            }
            if (!this.C107.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C107.Value;
            }
            if (!this.C108.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C108.Value;
            }
            if (!this.f_3453.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_3453;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }



    }
}
