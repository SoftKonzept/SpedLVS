using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class TOD_C100
    {
        internal clsEdiSegmentElement SegElement;
        internal TOD TOD;
        public const string Kennung = "C100";

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
        ///             Liefer- oder Transportbedingungen
        /// </summary>
        /// <param name="mySegElement"></param>
        public TOD_C100(TOD myEdiElement)
        {
            this.TOD = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }


        /// <summary>       
        ///                 Liefer- oder Transportbedingungen, Code
        ///                 EXW = entspricht "unfrei" in VDA 4913 
        ///                 CIF/CIP = entspricht "frei Haus" in VDA 4913 
        ///                 Die Liste enthaält auch Codes, die in den INCOTETRMS 2010 nicht mehr enthalten sind, 
        ///                 da sie noch häufig in der Praxis benutzt werden. 
        ///                 
        ///                 CFR Kosten und Fracht (... benannter Bestimmungshafen) 
        ///                 CIF Kosten, Versicherung und Fracht (... benannter Bestimmungshafen) 
        ///                 CIP Fracht und Versicherung bezahlt bis (... benannter Bestimmungsort) 
        ///                 CPT Fracht bezahlt bis (... benannter Bestimmungsort) 
        ///                 DAF Geliefert frei Grenze (... benannter Ort) 
        ///                 DAP Delivered At Place DAT Delivered At Terminal (... named place) 
        ///                 DDP Verzollt geliefert (... benannter Bestimmungsort) 
        ///                 DDU Unverzollt geliefert (... benannter Bestimmungsort) 
        ///                 DEQ Geliefert frei Kai (verzollt) (... benannter Bestimmungshafen) 
        ///                 DES Geliefert Ex Ship (... benannter Bestimmungshafen) 
        ///                 EXW Ab Werk (... benannter Ort) 
        ///                 FAS Frei Längsseite Seeschiff oder Binnenschiff (... benannter Verschiffungshafen) 
        ///                 FCA Frei Spediteur (... benannter Ort) 
        ///                 FOA FOB Airport - Named airport of departure 
        ///                 FOB Frei an Bord (... benannter Verschiffungshafen) 
        ///                 FOR Free On Rail
        /// </summary>
        internal const string Kennung_4053 = "4053";
        private string _f_4053;
        public string f_4053
        {
            get
            {
                _f_4053 = string.Empty;
                if ((this.sef4053 is clsEdiSegmentElementField) && (this.sef4053.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.TOD.EDICreate, this.sef4053, string.Empty, string.Empty);
                    _f_4053 = v.ReturnValue;
                }
                return _f_4053;
            }
        }
        public clsEdiSegmentElementField sef4053
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == TOD_C100.Kennung_4053);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal const string Kennung_1131 = "1131";
        private string _f_1131 = string.Empty;
        public string f_1131
        {
            get
            {
                _f_1131 = string.Empty;
                if ((this.sef1131 is clsEdiSegmentElementField) && (this.sef1131.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.TOD.EDICreate, this.sef1131, string.Empty, string.Empty);
                    _f_1131 = v.ReturnValue;
                }
                else
                {
                    _f_1131 = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
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
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == TOD_C100.Kennung_1131);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
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
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.TOD.EDICreate, this.sef3055, string.Empty, string.Empty);
                    _f_3055 = v.ReturnValue;
                }
                else
                {
                    _f_3055 = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
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
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == TOD_C100.Kennung_3055);
                }
                return tmpSef;
            }
        }

        internal const string Kennung_4052 = "4052";
        private string _f_4052 = string.Empty;
        public string f_4052
        {
            get
            {
                _f_4052 = string.Empty;
                if ((this.sef4052 is clsEdiSegmentElementField) && (this.sef4052.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.TOD.EDICreate, this.sef4052, string.Empty, string.Empty);
                    _f_4052 = v.ReturnValue;
                }
                else
                {
                    _f_4052 = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                }
                return _f_4052;
            }
        }
        public clsEdiSegmentElementField sef4052
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == TOD_C100.Kennung_4052);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_4053;

            if (!f_1131.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1131;
            }
            if (!f_3055.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
            }
            if (!f_4052.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_4052;
            }
        }



    }
}
