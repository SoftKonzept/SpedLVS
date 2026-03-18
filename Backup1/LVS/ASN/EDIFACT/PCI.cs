using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class PCI
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "PCI";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Labeltyp der Ladeeinheit
        /// </summary>
        /// <param name="myASNArt"></param>
        public PCI(clsEdiVDACreate myEdiCreate)
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
                        case PCI.Kennung_4233:
                            se4233 = new clsEdiSegmentElement();
                            se4233 = itm;
                            break;
                        case PCI.Kennung_8169:
                            se8169 = new clsEdiSegmentElement();
                            se8169 = itm;
                            break;

                        case PCI_C210.Kennung:
                            C210 = new PCI_C210(this);
                            break;
                        case PCI_C827.Kennung:
                            C827 = new PCI_C827(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();

            }
        }

        internal PCI_C210 C210;
        internal PCI_C827 C827;

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        public const string Kennung_4233 = "4233";
        /// <summary>
        ///            Markierungsanweisungen, Code 
        ///            17 Seller's instructions
        /// </summary>
        private string _f_4233;
        public string f_4233
        {
            get
            {
                _f_4233 = "XXX";
                if ((this.sef4233 is clsEdiSegmentElementField) && (this.sef4233.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef4233, string.Empty, string.Empty);
                    _f_4233 = v.ReturnValue;
                }
                return _f_4233;
            }
            set
            {
                _f_4233 = value;
            }
        }

        public clsEdiSegmentElement se4233;
        public clsEdiSegmentElementField sef4233
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se4233.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se4233.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PCI.Kennung_4233);
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///            Not used
        /// </summary>
        public const string Kennung_8169 = "8169";
        private string _f_8169 = string.Empty;
        public string f_8169
        {
            get
            {
                _f_8169 = string.Empty;
                if ((this.sef8169 is clsEdiSegmentElementField) && (this.sef8169.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef8169, string.Empty, string.Empty);
                    _f_8169 = v.ReturnValue;
                }
                return _f_8169;
            }
            set
            {
                _f_8169 = value;
            }
        }

        public clsEdiSegmentElement se8169;
        public clsEdiSegmentElementField sef8169
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se8169.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se8169.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PCI.Kennung_8169);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += PCI.Name;
            if (!f_4233.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_4233;
            }
            if (!this.C210.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C210.Value;
            }
            if (!f_8169.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_8169;
            }
            if (!this.C827.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C827.Value;
            }

            this.Value += UNA.const_SegementEndzeichen;
        }


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public PCI(string myEdiValueString, string myAsnArt)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List<string> strValue = myEdiValueString.Split(new char[] { '+' }).ToList();

                for (int i = 0; i < strValue.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            C210 = new PCI_C210(strValue[i]);
                            break;
                    }
                }

            }
        }
    }
}
