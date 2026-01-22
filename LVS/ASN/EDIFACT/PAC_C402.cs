using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class PAC_C402
    {
        internal clsEdiSegmentElement SegElement;
        internal PAC PAC;
        public const string Kennung_C402 = "C402";

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
        public PAC_C402(PAC myEdiElement)
        {
            this.PAC = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_7077 = "7077";
        /// <summary>       
        ///            Beschreibungsformat, Code X Teilstrukturiert (Code und Text)
        /// </summary>
        private string _f_7077;
        public string f_7077
        {
            get
            {
                _f_7077 = string.Empty;
                if ((this.sef7077 is clsEdiSegmentElementField) && (this.sef7077.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PAC.EDICreate, this.sef7077, string.Empty, string.Empty);
                    _f_7077 = v.ReturnValue;
                }
                return _f_7077;
            }
        }
        public clsEdiSegmentElementField sef7077
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PAC_C402.Kennung_7077);
                }
                return tmpSef;
            }
        }

        /// <summary>       
        ///            Art der Verpackung Bezeichnung der Verpackung, codiert (Packmittelcode des Lieferanten).
        /// </summary>
        /// 

        internal const string Kennung_7064 = "7064";
        private string _f_7064;
        public string f_7064
        {
            get
            {
                _f_7064 = string.Empty;
                if ((this.sef7064 is clsEdiSegmentElementField) && (this.sef7064.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PAC.EDICreate, this.sef7064, string.Empty, string.Empty);
                    _f_7064 = v.ReturnValue;
                }
                return _f_7064;
            }
        }
        public clsEdiSegmentElementField sef7064
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PAC_C402.Kennung_7064);
                }
                return tmpSef;
            }
        }


        /// <summary>       
        ///            Art der Verpackung Bezeichnung der Verpackung, codiert (Packmittelcode des Lieferanten).
        /// </summary>
        internal const string Kennung_7143 = "7143";
        private string _f_7143;
        public string f_7143
        {
            get
            {
                _f_7143 = string.Empty;
                if ((this.sef7143 is clsEdiSegmentElementField) && (this.sef7143.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PAC.EDICreate, this.sef7143, string.Empty, string.Empty);
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
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PAC_C402.Kennung_7143);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            //if (!f_7077.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            //{
            //    this.Value += this.f_7077;
            //}
            this.Value += this.f_7077;
            if (!f_7064.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_7064;
            }
            if (!f_7143.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_7143;
            }
        }



    }
}
