using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class PAC
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "PAC";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Transportinformationen
        /// </summary>
        /// <param name="myASNArt"></param>
        public PAC(clsEdiVDACreate myEdiCreate)
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
                        case PAC.Kennung_7224:
                            se7224 = new clsEdiSegmentElement();
                            se7224 = itm;
                            break;
                        case PAC_C531.Kennung_C531:
                            C531 = new PAC_C531(this);
                            break;
                        case PAC_C202.Kennung_C202:
                            C202 = new PAC_C202(this);
                            break;
                        case PAC_C402.Kennung_C402:
                            C402 = new PAC_C402(this);
                            break;
                        case PAC_C532.Kennung_C532:
                            C532 = new PAC_C532(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
            }
        }

        internal PAC_C531 C531;
        internal PAC_C202 C202;
        internal PAC_C402 C402;
        internal PAC_C532 C532;


        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        internal const string Kennung_7224 = "7224";
        /// <summary>
        ///            Packstückmenge Anzahl der identischen Ladeeinheiten, die zu dieser Gruppe gehören.Die Anzahl der Hilfspackmittel 
        ///            muss ein ganzzahliges Vielfaches der Anzahl der Ladeeinheiten (Hauptpackmittel) sein, damit sie zuordenbar sind.
        /// </summary>
        private string _f_7224 = string.Empty;
        public string f_7224
        {
            get
            {
                _f_7224 = string.Empty;
                if ((this.sef7224 is clsEdiSegmentElementField) && (this.sef7224.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef7224, string.Empty, string.Empty);
                    _f_7224 = v.ReturnValue;
                }
                return _f_7224;
            }
            set
            {
                _f_7224 = value;
            }
        }

        public clsEdiSegmentElement se7224;
        public clsEdiSegmentElementField sef7224
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se7224.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se7224.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PAC.Kennung_7224);
                }
                return tmpSef;
            }
        }





        /// <summary>
        ///            Not used
        /// </summary>
        private string _f_8101 = string.Empty;
        public string f_8101
        {
            get
            {
                _f_8101 = string.Empty;
                return _f_8101;
            }
            set
            {
                _f_8101 = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += PAC.Name;

            if (!f_7224.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_7224;
            }
            if ((C531 != null) && (!C531.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C531.Value;
            }

            if ((C202 != null) && (!C202.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C202.Value;
            }
            if ((C402 != null) && (!C402.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C402.Value;
            }
            if ((C532 != null) && (!C402.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C532.Value;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }


        //===================================================================================================================
        /// <summary>
        ///             NUMBER OF PACKAGES
        /// </summary>
        public int f_7224_NumberOfPackage { get; set; } = 1;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public PAC(string myEdiValueString, string myAsnArt)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List<string> lElements = myEdiValueString.Split(new char[] { '+' }).ToList();
                for (int i = 0; i < lElements.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            break;
                        case 1:
                            int iTmp = 1;
                            int.TryParse(lElements[i], out iTmp);
                            this.f_7224_NumberOfPackage = iTmp;
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                    }
                }
            }
        }
    }
}
