using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class PIA
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "PIA";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Zusätzliche Produktidentifikation
        /// </summary>
        /// <param name="myASNArt"></param>
        public PIA(clsEdiVDACreate myEdiCreate)
        {
            this.EDICreate = myEdiCreate;
            this.Segment = myEdiCreate.ediSegment;

            //switch (this.Segment.AsnArt.Typ)
            //{
            //    case clsASNArt.const_Art_EdifactVDA4987:
            //    case clsASNArt.const_Art_DESADV_BMW_4a:
            List<clsEdiSegmentElement> List_SE = this.Segment.ListEdiSegmentElement.Where(x => x.EdiSegmentId == this.Segment.ID && x.IsActive == true).ToList();
            if (List_SE.Count > 0)
            {

                foreach (clsEdiSegmentElement itm in List_SE)
                {
                    SelectedSegmentElement = itm;

                    switch (itm.Name)
                    {
                        case PIA.Kennung_4347:
                            se4347 = new clsEdiSegmentElement();
                            se4347 = itm;
                            break;
                        case PIA_C212.Kennung_C212:
                            C212 = new PIA_C212(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
                //}
                //break;
            }
        }

        internal PIA_C212 C212;


        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        internal const string Kennung_4347 = "4347";
        /// <summary>
        ///            Produkt-/Erzeugnisnummer, Qualifier 
        ///            
        ///             1 Additional identification
        /// </summary>
        private string _f_4347;
        public string f_4347
        {
            get
            {
                _f_4347 = string.Empty;
                if ((this.sef4347 is clsEdiSegmentElementField) && (this.sef4347.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef4347, string.Empty, string.Empty);
                    _f_4347 = v.ReturnValue;
                }
                return _f_4347;
            }
            set
            {
                _f_4347 = value;
            }
        }

        public clsEdiSegmentElement se4347;
        public clsEdiSegmentElementField sef4347
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se4347.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se4347.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PIA.Kennung_4347);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += PIA.Name;
            if (!f_4347.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_4347;
            }
            if (!this.C212.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C212.Value;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }


        //===================================================================================================================

        /// <summary>
        ///             PRODUCT ID. FUNCTION QUALIFIER
        /// </summary>
        public string f_4347_ProducitonIdQualifier { get; set; } = string.Empty;

        public string f_1131_CodeListQualifier { get; set; } = string.Empty;
        public string f_3055_CodeListResponsibleAgency { get; set; } = string.Empty;

        public List<PIA_C212> List_Pia_C212 { get; set; } = new List<PIA_C212>();

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public PIA(string myEdiValueString, string myAsnArt)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List_Pia_C212.Clear();
                List<string> lElements = myEdiValueString.Split(new char[] { '+' }).ToList();
                for (int i = 0; i < lElements.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            break;
                        case 1:
                            if (!lElements[i].Equals(string.Empty))
                            {
                                C212 = new PIA_C212(lElements[i], myAsnArt);
                                List_Pia_C212.Add(C212);
                            }
                            break;
                        case 2:
                            if (!lElements[i].Equals(string.Empty))
                            {
                                C212 = new PIA_C212(lElements[i], myAsnArt);
                                List_Pia_C212.Add(C212);
                            }
                            break;
                        case 3:
                            if (!lElements[i].Equals(string.Empty))
                            {
                                C212 = new PIA_C212(lElements[i], myAsnArt);
                                List_Pia_C212.Add(C212);
                            }
                            break;
                        case 4:
                            if (!lElements[i].Equals(string.Empty))
                            {
                                C212 = new PIA_C212(lElements[i], myAsnArt);
                                List_Pia_C212.Add(C212);
                            }
                            break;
                    }
                }
            }
        }
    }
}
