using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class BGM_C106
    {
        /// <summary>
        ///            Dokumenten-/Nachrichten-Identifikation
        /// </summary>
        /// <param name="mySegElement"></param>
        public BGM_C106(BGM myBGM)
        {
            this.BGM = myBGM;
            this.SegElement = myBGM.SelectedSegmentElement;
            CreateValue();
        }
        internal clsEdiSegmentElement SegElement;
        internal BGM BGM;
        public const string Kennung_C106 = "C106";
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
        ///             Lieferavis 
        ///             Nummer Vom Lieferanten vergebene eindeutige Nummer der DESADV. 
        ///             Darf sich im Lauf eines Jahres nicht wiederholen.
        /// </summary>
        /// 
        public const string Kennung_1004 = "1004";
        private string _f_1004 = string.Empty;
        public string f_1004
        {
            get
            {
                clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.BGM.EDICreate, this.sef1004, string.Empty, string.Empty);
                _f_1004 = v.ReturnValue;
                return _f_1004;
            }
        }

        public clsEdiSegmentElementField sef1004
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == "1004");
                }
                return tmpSef;
            }
        }


        public const string Kennung_1056 = "1056";
        private string _f_1056 = string.Empty;
        public string f_1056
        {
            get
            {
                clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.BGM.EDICreate, this.sef1056, string.Empty, string.Empty);
                _f_1056 = v.ReturnValue;
                return _f_1056;
            }
        }

        public clsEdiSegmentElementField sef1056
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == Kennung_1056);
                }
                return tmpSef;
            }
        }



        public const string Kennung_1060 = "1060";
        private string _f_1060 = string.Empty;
        public string f_1060
        {
            get
            {
                clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.BGM.EDICreate, this.sef1060, string.Empty, string.Empty);
                _f_1060 = v.ReturnValue;
                return _f_1060;
            }
        }

        public clsEdiSegmentElementField sef1060
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == Kennung_1060);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            if (!f_1004.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += this.f_1004;
            }
        }



    }
}
