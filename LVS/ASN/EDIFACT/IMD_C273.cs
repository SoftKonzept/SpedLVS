using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class IMD_C273
    {
        internal clsEdiSegmentElement SegElement;
        internal IMD IMD;
        public const string Kennung = "C273";

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
        ///            Produkt-/Leistungsbeschreibung
        /// </summary>
        /// <param name="mySegElement"></param>
        public IMD_C273(IMD myEdiElement)
        {
            this.IMD = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_7009 = "7009";
        /// <summary>       
        ///            Verwendungsschlüssel 
        ///            
        ///             11 Produktion 
        ///             12 Dienstleistung 
        ///             17 Erstmuster
        /// </summary>
        private string _f_7009 = string.Empty;
        public string f_7009
        {
            get
            {
                _f_7009 = string.Empty;
                if ((this.sef7009 is clsEdiSegmentElementField) && (this.sef7009.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.IMD.EDICreate, this.sef7009, string.Empty, string.Empty);
                    _f_7009 = v.ReturnValue;
                }
                return _f_7009;
            }
        }
        public clsEdiSegmentElementField sef7009
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == IMD_C273.Kennung_7009);
                }
                return tmpSef;
            }
        }

        /// <summary>       
        ///            not used
        /// </summary>
        /// 
        internal const string Kennung_1131 = "1131";
        private string _f_1131;
        public string f_1131
        {
            get
            {
                _f_1131 = string.Empty;
                if ((this.sef1131 is clsEdiSegmentElementField) && (this.sef1131.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.IMD.EDICreate, this.sef1131, string.Empty, string.Empty);
                    _f_1131 = v.ReturnValue;
                }
                return _f_1131;
            }
        }
        public clsEdiSegmentElementField sef1131
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == IMD_C273.Kennung_1131);
                }
                return tmpSef;
            }
        }

        internal const string Kennung_3055 = "3055";
        /// <summary>       
        ///           Verantwortliche Stelle für die Codepflege, Code 
        ///           
        ///             272 Joint Automotive Industry agency
        /// </summary>
        private string _f_3055 = string.Empty;
        public string f_3055
        {
            get
            {
                _f_3055 = string.Empty;
                if ((this.sef3055 is clsEdiSegmentElementField) && (this.sef3055.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.IMD.EDICreate, this.sef3055, string.Empty, string.Empty);
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
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == IMD_C273.Kennung_3055);
                }
                return tmpSef;
            }
        }

        internal const string Kennung_7008 = "7008";
        /// <summary>   
        ///            Produkt-/Leistungsbeschreibung Die Beschreibung / die Kurzbezeichnung des Artikels 
        ///            in einfachem Text.Artikelbezeichnung, wird z. Z.im Wareneingang nicht verarbeitet, 
        ///            wird jedoch für die Erstellung der Transport- und Sendungsbelege nach VDA 4939 
        ///            aus der DESADV (AMES-T) benötigt.
        ///            DELFOR SG 12: IMD; C273: DE7708
        /// </summary>
        private string _f_7008 = string.Empty;
        public string f_7008
        {
            get
            {
                _f_7008 = string.Empty;
                if ((this.sef7008 is clsEdiSegmentElementField) && (this.sef7008.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.IMD.EDICreate, this.sef7008, string.Empty, string.Empty);
                    _f_7008 = UNOC_Zeichensatz.Execute(v.ReturnValue);
                }
                return _f_7008;
            }
        }
        public clsEdiSegmentElementField sef7008
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == IMD_C273.Kennung_7008);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_7009;
            //this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1131;
            //this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
            //this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_7008;

            if (!this.f_1131.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1131;
            }
            if (!this.f_3055.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
            }
            if (!this.f_7008.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_7008;
            }
        }



    }
}
