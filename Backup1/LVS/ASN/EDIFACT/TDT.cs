using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class TDT
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "TDT";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Transportinformationen
        /// </summary>
        /// <param name="myASNArt"></param>
        public TDT(clsEdiVDACreate myEdiCreate)
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
                        case TDT.Kennung_8051:
                            se8051 = new clsEdiSegmentElement();
                            se8051 = itm;
                            break;
                        case TDT_C001.Kennung:
                            C001 = new TDT_C001(this);
                            break;
                        case TDT_C040.Kennung:
                            C040 = new TDT_C040(this);
                            break;
                        case TDT_C220.Kennung:
                            C220 = new TDT_C220(this);
                            break;
                        case TDT_C222.Kennung:
                            C222 = new TDT_C222(this);
                            break;
                        case TDT_C401.Kennung:
                            C401 = new TDT_C401(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
            }
        }

        internal TDT_C001 C001;
        internal TDT_C040 C040;
        internal TDT_C220 C220;
        internal TDT_C222 C222;
        internal TDT_C401 C401;

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        internal const string Kennung_8051 = "8051";
        /// <summary>
        ///            Transportstrecke/-abschnitt, Qualifier 
        ///            12 Am Abgang
        /// </summary>
        private string _f_8051;
        public string f_8051
        {
            get
            {
                _f_8051 = string.Empty;
                if ((this.sef8051 is clsEdiSegmentElementField) && (this.sef8051.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef8051, string.Empty, string.Empty);
                    _f_8051 = v.ReturnValue;
                }
                return _f_8051;
            }
            set
            {
                _f_8051 = value;
            }
        }

        public clsEdiSegmentElement se8051;
        public clsEdiSegmentElementField sef8051
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se8051.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se8051.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == TDT.Kennung_8051);
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///            Not used
        /// </summary>
        private string _f_8028;
        public string f_8028
        {
            get
            {
                _f_8028 = string.Empty;
                if (this.EDICreate.Lager.Ausgang is clsLAusgang)
                {
                    _f_8028 = this.EDICreate.Lager.Ausgang.LAusgangID.ToString();
                }
                return _f_8028;
            }
        }
        /// <summary>
        ///            Not used
        /// </summary>
        private string _f_8101;
        public string f_8101
        {
            get
            {
                _f_8101 = string.Empty;
                return _f_8101;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += TDT.Name;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_8051;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_8028;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C220.Value;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C001.Value;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C040.Value;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_8101;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C401.Value;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C222.Value;
            this.Value += UNA.const_SegementEndzeichen;
        }


        //===================================================================================================================
        /// <summary>
        ///             NUMBER OF PACKAGES
        /// </summary>
        //public int f_7224_NumberOfPackage { get; set; } = 1;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public TDT(string myEdiValueString, string myAsnArt)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List<string> lElements = myEdiValueString.Split(new char[] { '+' }).ToList();
                if (lElements.Count < 9)
                {
                    while (lElements.Count < 10)
                    {
                        lElements.Add(string.Empty);
                    }
                }

                for (int i = 0; i < lElements.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 3:
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            C222 = new TDT_C222(lElements[i]);
                            break;
                    }
                }
            }
        }
    }
}
