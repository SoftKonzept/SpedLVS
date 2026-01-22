using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class NAD_C059
    {
        internal clsEdiSegmentElement SegElement;
        internal NAD NAD;
        public const string Kennung = "C059";

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
        ///             Straße
        /// </summary>
        /// <param name="mySegElement"></param>
        public NAD_C059(NAD myEdiElement)
        {
            this.NAD = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }



        internal const string Kennung_3042 = "3042";
        /// <summary>
        ///                Straße und Hausnummer oder Postfach 
        ///                Identifiziert die Lokation eines Hauses oder Gebäudes als Teil einer Adresse, üblicherweise in einer Strasse.
        /// </summary>
        private string _f_3042;
        public string f_3042
        {
            get
            {
                _f_3042 = string.Empty;
                if ((this.sef3042 is clsEdiSegmentElementField) && (this.sef3042.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.NAD.EDICreate, this.sef3042, string.Empty, string.Empty);
                    _f_3042 = v.ReturnValue;
                    if (!_f_3042.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
                    {
                        if ((this.NAD.ADR is clsADR) && (this.NAD.ADR.ID > 0))
                        {
                            string strTmp = string.Empty;
                            strTmp += UNOC_Zeichensatz.Execute(this.NAD.ADR.Str);
                            strTmp += UNA.const_BLANK;
                            strTmp += UNOC_Zeichensatz.Execute(this.NAD.ADR.HausNr);
                            _f_3042 = ediHelper_FormatString.CutValToLenth(strTmp, this.sef3042.Length);
                        }
                    }
                }
                return _f_3042;
            }
        }
        public clsEdiSegmentElementField sef3042
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == NAD_C059.Kennung_3042);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_3042;
            //if (!f_3042.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            //{
            //    this.Value += this.f_3042;
            //}
        }



    }
}
