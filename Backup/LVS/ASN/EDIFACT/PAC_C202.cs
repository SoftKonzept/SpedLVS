using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class PAC_C202
    {
        internal clsEdiSegmentElement SegElement;
        internal PAC PAC;
        public const string Kennung_C202 = "C202";

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
        ///            Verpackungsangaben
        /// </summary>
        /// <param name="mySegElement"></param>
        public PAC_C202(PAC myEdiElement)
        {
            this.PAC = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }


        /// <summary>
        ///            Not used
        /// </summary>
        /// 
        public const string Kennung_1131 = "1131";

        public clsEdiSegmentElementField sef1131
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PAC_C202.Kennung_1131);
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
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PAC.EDICreate, this.sef1131, string.Empty, string.Empty);
                    _f_1131 = v.ReturnValue;
                }
                return _f_1131;
            }
        }


        /// <summary>       
        ///            Art der Verpackung, Code Bezeichnung der Verpackung, codiert gemäß Verpackungsdatenblatt 
        ///            (Packmittelcode des Kunden) bzw. zulässige Codes für Ausweichverpackungen,
        ///            wie z. B. 0001PAL oder E352430.
        /// </summary>
        public const string Kennung_7065 = "7065";

        public clsEdiSegmentElementField sef7065
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PAC_C202.Kennung_7065);
                }
                return tmpSef;
            }
        }
        private string _f_7065 = string.Empty;
        public string f_7065
        {
            get
            {
                _f_7065 = string.Empty;
                if ((this.sef7065 is clsEdiSegmentElementField) && (this.sef7065.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PAC.EDICreate, this.sef7065, string.Empty, string.Empty);
                    _f_7065 = v.ReturnValue;
                }
                return _f_7065;
            }
        }


        public const string Kennung_3055 = "3055";
        /// <summary>       
        ///            Verantwortliche Stelle für die Codepflege, Code Verantwortliche Stelle für Codepflege 
        ///            92 Zugewiesen vom Käufer oder dessen Agenten
        /// </summary>
        private string _f_3055;
        public string f_3055
        {
            get
            {
                _f_3055 = string.Empty;
                if ((this.sef3055 is clsEdiSegmentElementField) && (this.sef3055.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PAC.EDICreate, this.sef3055, string.Empty, string.Empty);
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
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PAC_C202.Kennung_3055);
                }
                return tmpSef;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            //if(!f_7065.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)) 
            //{
            //    this.Value += this.f_7065;
            //}
            this.Value += this.f_7065;
            if (!f_1131.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1131;
            }
            if (!f_3055.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
            }

        }


        //===================================================================================

        /// <summary>
        ///             CODE LIST QUALIFIER
        /// </summary>
        public string f_7065_Code { get; set; } = string.Empty;
    }
}
