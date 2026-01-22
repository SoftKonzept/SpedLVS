using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class INV
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "INV";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;


        public INV()
        {
            C522 = new INV_C522(this);
        }
        /// <summary>
        ///            Produkt-/Leistungsbeschreibung
        /// </summary>
        /// <param name="myASNArt"></param>
        public INV(clsEdiVDACreate myEdiCreate)
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
                        case INV_C522.Kennung_C522:
                            C522 = new INV_C522(this);
                            break;

                        case INV.Kennung_4501:
                            se4501 = new clsEdiSegmentElement();
                            se4501 = itm;
                            break;
                        case INV.Kennung_7491:
                            se7491 = new clsEdiSegmentElement();
                            se7491 = itm;
                            break;
                        case INV.Kennung_4499:
                            se4499 = new clsEdiSegmentElement();
                            se4499 = itm;
                            break;
                        case INV.Kennung_4503:
                            se4503 = new clsEdiSegmentElement();
                            se4503 = itm;
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
            }
        }

        internal INV_C522 C522;


        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }


        /// <summary>
        ///            Inventory Movement Direction Coded
        ///            1 = Movement out of inventory
        //             2 = Movement into inventory
        /// </summary>
        /// 
        public const string Kennung_4501 = "4501";
        private string _f_4501;
        public string f_4501
        {
            get
            {
                _f_4501 = string.Empty;
                if ((this.sef4501 is clsEdiSegmentElementField) && (this.sef4501.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef4501, string.Empty, string.Empty);
                    _f_4501 = v.ReturnValue;
                }
                return _f_4501;
            }
        }
        public clsEdiSegmentElement se4501;
        public clsEdiSegmentElementField sef4501
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se4501.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se4501.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == INV.Kennung_4501);
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///            Type of Inventory Movement affected Coded
        ///                1 = Accepted product inventory
        ///                2 = Damaged product inventory
        ///                3 = Bonded inventory
        ///                4 = Reserved inventory
        ///                5 = Products for Inspection
        /// </summary>
        /// 
        public const string Kennung_7491 = "7491";
        private string _f_7491;
        public string f_7491
        {
            get
            {
                _f_7491 = string.Empty;
                if ((this.sef7491 is clsEdiSegmentElementField) && (this.sef7491.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef7491, string.Empty, string.Empty);
                    _f_7491 = v.ReturnValue;
                }
                return _f_7491;
            }
        }
        public clsEdiSegmentElement se7491;
        public clsEdiSegmentElementField sef7491
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se7491.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se7491.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == INV.Kennung_7491);
                }
                return tmpSef;
            }
        }

        /// <summary>
        ///            Stock Movement Reason Coded
        ///            
        ///                1 = Reception
        ///                2 = Delivery
        ///                3 = Scrapped parts
        ///                4 = Difference
        ///                5 = Property transfer within warehouse
        ///                6 = Inventory recycling
        ///                7 = Reversal of previous movement
        ///                8 = Defects(technical)
        ///                9 = Commercial
        ///                10 = Conversion
        ///                11 = Consumption
        /// </summary>
        public const string Kennung_4499 = "4499";
        private string _f_4499;
        public string f_4499
        {
            get
            {
                _f_4499 = string.Empty;
                if ((this.sef4499 is clsEdiSegmentElementField) && (this.sef4499.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef4499, string.Empty, string.Empty);
                    _f_4499 = v.ReturnValue;
                }
                return _f_4499;
            }
        }
        public clsEdiSegmentElement se4499;
        public clsEdiSegmentElementField sef4499
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se4499.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se4499.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == INV.Kennung_4499);
                }
                return tmpSef;
            }
        }

        /// <summary>
        ///            Stock Movement Reason Coded
        ///            
        ///                1 = Reception
        ///                2 = Delivery
        ///                3 = Scrapped parts
        ///                4 = Difference
        ///                5 = Property transfer within warehouse
        ///                6 = Inventory recycling
        ///                7 = Reversal of previous movement
        ///                8 = Defects(technical)
        ///                9 = Commercial
        ///                10 = Conversion
        ///                11 = Consumption
        /// </summary>
        public const string Kennung_4503 = "4503";
        private string _f_4503;
        public string f_4503
        {
            get
            {
                _f_4503 = string.Empty;
                if ((this.sef4503 is clsEdiSegmentElementField) && (this.sef4503.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef4503, string.Empty, string.Empty);
                    _f_4503 = v.ReturnValue;
                }
                return _f_4503;
            }
        }
        public clsEdiSegmentElement se4503;
        public clsEdiSegmentElementField sef4503
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se4503.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se4503.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == INV.Kennung_4503);
                }
                return tmpSef;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += INV.Name;
            if (!f_4501.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_4501;
            }
            if (!f_7491.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_7491;
            }
            if (!f_4499.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_4499;
            }
            if (!f_4503.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_4503;
            }
            if (!this.C522.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C522.Value;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }



    }
}
