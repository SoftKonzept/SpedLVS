using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class NAD_C082
    {
        internal clsEdiSegmentElement SegElement;
        internal NAD NAD;
        public const string Kennung = "C082";

        private string _value = string.Empty;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        ///             Identifikation des Beteiligten
        /// </summary>
        /// <param name="mySegElement"></param>
        public NAD_C082(NAD myEdiElement)
        {
            this.NAD = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }



        internal const string Kennung_3039 = "3039";
        /// <summary>
        ///                 Eindeutiger Identifier des Geschäftspartners (Kundennummer, Lieferantennummer DUNS oder dgl.)
        ///                 
        ///                 UD Ultimate customer
        ///                 -> Beim OT-Streckengeschäft (BGM 1000 = VAB-DDP) ist hier der Wert aus der DELJIT CALDEL NAD+CN bzw. 
        ///                    aus der VDA 4984/85/86 NAD+ST zu übernehmen. Eindeutiger Identifier des Geschäftspartners 
        ///                    (Kundennummer, Lieferantennummer DUNS oder dgl.)
        ///                    
        ///                 10 stellig
        /// </summary>
        private string _f_3039 = string.Empty;
        public string f_3039
        {
            get
            {
                _f_3039 = string.Empty;
                if ((this.sef3039 is clsEdiSegmentElementField) && (this.sef3039.ID > 0))
                {
                    string strTmp = string.Empty;
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.NAD.EDICreate, this.sef3039, string.Empty, string.Empty);
                    if (v.ReturnValue.Equals(string.Empty))
                    {
                        if (this.NAD.ADR != null)
                        {
                            strTmp = this.NAD.ADR.ViewID.ToString(); //.Replace("/", ""); 
                        }
                    }
                    else
                    {
                        strTmp = v.ReturnValue; //.Replace("/", ""); 
                    }
                    _f_3039 = strTmp;
                }
                return _f_3039;
            }
        }
        public clsEdiSegmentElementField sef3039
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == NAD_C082.Kennung_3039);
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///             Not used
        /// </summary>
        /// 
        internal const string Kennung_1131 = "1131";
        public clsEdiSegmentElementField sef1131
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == NAD_C082.Kennung_1131);
                }
                return tmpSef;
            }
        }
        private string _f_1131 = string.Empty;
        public string f_1131
        {
            get
            {
                _f_1131 = string.Empty;
                if ((this.sef1131 is clsEdiSegmentElementField) && (this.sef1131.ID > 0))
                {
                    string strTmp = string.Empty;
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.NAD.EDICreate, this.sef1131, string.Empty, string.Empty);
                    strTmp = v.ReturnValue;
                    _f_1131 = strTmp;
                }
                return _f_1131;
            }
        }



        /// <summary>
        ///             Verantwortliche Stelle für die Codepflege, 
        ///             Code 
        ///             Verantwortliche Stelle für Codepflege 92 
        ///             Zugewiesen vom Käufer oder dessen Agenten
        /// </summary>
        internal const string Kennung_3055 = "3055";
        private string _f_3055 = string.Empty;
        public string f_3055
        {
            get
            {
                _f_3055 = string.Empty;
                if ((this.sef3055 is clsEdiSegmentElementField) && (this.sef3055.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.NAD.EDICreate, this.sef3055, string.Empty, string.Empty);
                    _f_3055 = v.ReturnValue;
                }
                return _f_3055;
            }
        }
        public clsEdiSegmentElementField sef3055
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == NAD_C082.Kennung_3055);
                }
                return tmpSef;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_3039;
            if (!f_1131.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1131;
            }
            if (!f_3055.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
            }
            //this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1131;
            //this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
        }


        //===================================================================================

        /// <summary>
        ///             PARTY ID IDENTIFICATION 
        /// </summary>
        public string f_3039_PartyId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public NAD_C082(string myEdiValueString)
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
                            this.f_3039_PartyId = strValue[i].ToString();
                            break;
                        case 1:
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
