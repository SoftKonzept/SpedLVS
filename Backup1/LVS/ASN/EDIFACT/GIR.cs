using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class GIR
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "GIR";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Name des Modulbehälters (Modulname)
        /// </summary>
        /// <param name="myASNArt"></param>
        public GIR(clsEdiVDACreate myEdiCreate)
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
                        case GIR.Kennung_7297:
                            se7297 = new clsEdiSegmentElement();
                            se7297 = itm;
                            break;
                        case GIR_C206.Kennung:
                            C206 = new GIR_C206(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();

            }
        }


        internal GIR_C206 C206;

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }


        /// <summary>
        ///            Markierungsanweisungen, Code 
        ///            
        ///             1 Product
        ///             4 Vehicle reference set
        ///            17 Seller's instructions
        ///            
        /// </summary>
        public const string Kennung_7297 = "7297";
        private string _f_7297;
        public string f_7297
        {
            get
            {
                _f_7297 = "XXX";
                if ((this.sef7297 is clsEdiSegmentElementField) && (this.sef7297.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef7297, string.Empty, string.Empty);
                    _f_7297 = v.ReturnValue;
                }
                return _f_7297;
            }
            set
            {
                _f_7297 = value;
            }
        }

        public clsEdiSegmentElement se7297;
        public clsEdiSegmentElementField sef7297
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se7297.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se7297.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == GIR.Kennung_7297);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += GIR.Name;
            if (!f_7297.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_7297;
            }
            if (!this.C206.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C206.Value;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }


        //===================================================================================================================
        /// <summary>
        ///             SET IDENTIFICATION QUALIFIER
        /// </summary>
        public int f_7297_IdentifierQualifier { get; set; } = 0;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public GIR(string myEdiValueString, string myAsnArt)
        {
            int iTmp = 0;
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
                            iTmp = 0;
                            int.TryParse(lElements[i].ToString(), out iTmp);
                            this.f_7297_IdentifierQualifier = iTmp;
                            break;
                        case 2:
                            C206 = new GIR_C206(lElements[i].ToString());
                            break;
                        case 3:
                        case 4:
                            break;
                    }
                }

            }
        }
    }
}
