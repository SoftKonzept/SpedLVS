using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class PIA_C212
    {
        internal clsEdiSegmentElement SegElement;
        internal PIA PIA;
        public const string Kennung_C212 = "C212";

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        ///            Waren-/Leistungsnummer, Identifikation
        /// </summary>
        /// <param name="mySegElement"></param>
        public PIA_C212(PIA myEdiElement)
        {
            this.PIA = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_7140 = "7140";
        /// <summary>       
        ///            ID Nummer / Produktionsnummer
        ///            
        /// </summary>
        private string _f_7140;
        public string f_7140
        {
            get
            {
                _f_7140 = string.Empty;
                if ((this.sef7140 is clsEdiSegmentElementField) && (this.sef7140.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PIA.EDICreate, this.sef7140, string.Empty, string.Empty);
                    _f_7140 = v.ReturnValue;
                }
                return _f_7140;
            }
        }
        public clsEdiSegmentElementField sef7140
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PIA_C212.Kennung_7140);
                }
                return tmpSef;
            }
        }

        internal const string Kennung_7143 = "7143";
        /// <summary>       
        ///            Art der Produkt-/Leistungsnummer, Code 
        ///            DR - Teilegenerationsstand (ändert sich mit dem Werkzeug, das zur Herstellung des Teils verwendet wurde) 
        ///            AG - Softwarestand 
        ///            BT - Hardwarestand 
        ///            
        ///                 DR Drawing revision number AG Software revision number 
        ///                 BT Technical phase 
        ///                 SA Supplier's article number
        ///            
        /// </summary>
        private string _f_7143;
        public string f_7143
        {
            get
            {
                _f_7143 = string.Empty;
                if ((this.sef7143 is clsEdiSegmentElementField) && (this.sef7143.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PIA.EDICreate, this.sef7143, string.Empty, string.Empty);
                    _f_7143 = v.ReturnValue;
                }
                return _f_7143;
            }
        }
        public clsEdiSegmentElementField sef7143
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PIA_C212.Kennung_7143);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            if (!f_7140.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += this.f_7140;
            }
            if (!f_7143.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_7143;
            }
        }



        //===================================================================================================================

        /// <summary>
        ///             ITEM NUMBER
        /// </summary>
        public string f_7140_SupplierArticleReference { get; set; } = string.Empty;
        public string f_7143_NumberCode { get; set; } = string.Empty;



        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public PIA_C212(string myEdiValueString, string myAsnArt)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                int iTmp = 0;
                List<string> strValue = myEdiValueString.Split(new char[] { ':' }).ToList();
                for (int i = 0; i < strValue.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.f_7140_SupplierArticleReference = strValue[i].ToString();
                            break;
                        case 1:
                            this.f_7143_NumberCode = strValue[i].ToString();
                            break;
                        case 2:
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
