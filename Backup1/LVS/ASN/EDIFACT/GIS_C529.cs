using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class GIS_C529
    {
        internal clsEdiSegmentElement SegElement;
        internal GIS GIS;
        public const string Kennung = "C529";

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
        ///            Identifikationsnummer
        /// </summary>
        /// <param name="mySegElement"></param>
        public GIS_C529(GIS myEdiElement)
        {
            this.GIS = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }


        internal const string Kennung_7365 = "7365";
        /// <summary>  

        /// </summary>
        private string _f_7365;
        public string f_7365
        {
            get
            {
                _f_7365 = string.Empty;
                if ((this.sef7365 is clsEdiSegmentElementField) && (this.sef7365.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.GIS.EDICreate, this.sef7365, string.Empty, string.Empty);
                    _f_7365 = v.ReturnValue;
                }
                return _f_7365;
            }
        }
        public clsEdiSegmentElementField sef7365
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == GIR_C206.Kennung_7402);
                }
                return tmpSef;
            }
        }

        internal const string Kennung_1131 = "1131";
        /// <summary>       

        /// </summary>
        private string _f_1131;
        public string f_1131
        {
            get
            {
                _f_1131 = string.Empty;
                if ((this.sef1131 is clsEdiSegmentElementField) && (this.sef1131.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.GIS.EDICreate, this.sef1131, string.Empty, string.Empty);
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
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == GIS_C529.Kennung_1131);
                }
                return tmpSef;
            }
        }



        internal const string Kennung_3055 = "3055";
        /// <summary>       

        /// </summary>
        private string _f_3055;
        public string f_3055
        {
            get
            {
                _f_3055 = string.Empty;
                if ((this.sef3055 is clsEdiSegmentElementField) && (this.sef3055.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.GIS.EDICreate, this.sef3055, string.Empty, string.Empty);
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
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == GIS_C529.Kennung_3055);
                }
                return tmpSef;
            }
        }


        internal const string Kennung_7187 = "7187";
        /// <summary>       

        /// </summary>
        private string _f_7187;
        public string f_7187
        {
            get
            {
                _f_7187 = string.Empty;
                if ((this.sef7187 is clsEdiSegmentElementField) && (this.sef7187.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.GIS.EDICreate, this.sef7187, string.Empty, string.Empty);
                    _f_7187 = v.ReturnValue;
                }
                return _f_7187;
            }
        }

        public clsEdiSegmentElementField sef7187
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == GIS_C529.Kennung_7187);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_7365;
            this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1131;
            this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
            this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_7187;
        }



    }
}
