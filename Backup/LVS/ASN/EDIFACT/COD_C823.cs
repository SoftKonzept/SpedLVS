using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class COD_C823
    {
        internal clsEdiSegmentElement SegElement;
        internal COD COD;
        public const string Kennung_C823 = "C823";

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
        ///            Art der Einheit/des Bestandteils
        /// </summary>
        /// <param name="mySegElement"></param>
        public COD_C823(COD myEdiElement)
        {
            this.COD = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        /// <summary>       
        ///            Name of the module (JIS container) Name des Moduls (d.h. des JIS-Behälters)
        /// </summary>

        internal const string Kennung_7505 = "7505";
        public clsEdiSegmentElementField sef7505
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == COD_C823.Kennung_7505);
                }
                return tmpSef;
            }
        }
        private string _f_7505 = string.Empty;
        public string f_7505
        {
            get
            {
                _f_7505 = "NO";
                if ((this.sef7505 is clsEdiSegmentElementField) && (this.sef7505.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.COD.EDICreate, this.sef7505, string.Empty, string.Empty);
                    _f_7505 = v.ReturnValue;
                }
                return _f_7505;
            }
        }

        /// <summary>       
        ///          Code list identification code
        /// </summary>

        internal const string Kennung_1131 = "1131";
        public clsEdiSegmentElementField sef1131
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == COD_C823.Kennung_1131);
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
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.COD.EDICreate, this.sef1131, string.Empty, string.Empty);
                    _f_1131 = v.ReturnValue;
                }
                return _f_1131;
            }
        }

        /// <summary>       
        ///          Code list identification code
        /// </summary>

        internal const string Kennung_3055 = "3055";
        public clsEdiSegmentElementField sef3055
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == COD_C823.Kennung_3055);
                }
                return tmpSef;
            }
        }
        private string _f_3055 = string.Empty;
        public string f_3055
        {
            get
            {
                _f_3055 = string.Empty;
                if ((this.sef3055 is clsEdiSegmentElementField) && (this.sef3055.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.COD.EDICreate, this.sef3055, string.Empty, string.Empty);
                    _f_3055 = v.ReturnValue;
                }
                return _f_3055;
            }
        }
        /// <summary>       
        ///          Unit or component type description
        /// </summary>

        internal const string Kennung_7504 = "7504";
        public clsEdiSegmentElementField sef7504
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == COD_C823.Kennung_7504);
                }
                return tmpSef;
            }
        }
        private string _f_7504 = string.Empty;
        public string f_7504
        {
            get
            {
                _f_7504 = string.Empty;
                if ((this.sef7504 is clsEdiSegmentElementField) && (this.sef7504.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.COD.EDICreate, this.sef7504, string.Empty, string.Empty);
                    _f_7504 = v.ReturnValue;
                }
                return _f_7504;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            //this.Value += this.f_7505;
            if (!this.f_7505.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += this.f_7505;
            }
            if (!this.f_1131.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1131;
            }
            if (!this.f_3055.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
            }
            if (!this.f_7504.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_7504;
            }
        }



    }
}
