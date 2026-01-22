using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class PCI_C827
    {
        internal clsEdiSegmentElement SegElement;
        internal PCI PCI;
        public const string Kennung = "C827";

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
        public PCI_C827(PCI myEdiElement)
        {
            this.PCI = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_7511 = "7511";
        /// <summary>       
        ///            Markierungsart, Code Der Data Identifier ist der erste Teil eines Transportlabels. 
        ///            Diese ID zeigt an, ob es sich um eine äußere oder innere Verpackung handelt. Für äußere 
        ///            Verpackungen gibt es noch die Unterscheidung in Master-Label und gemischtes Label. 
        ///            6J - entspricht dem früheren M = Master-Label 
        ///            5J - entspricht dem früheren G = Master Mixed Load 
        ///            5J Eindeutiger Identifier für Ladeeinheit mit Mischladung 
        ///            6J Eindeutiger Identifier für Ladeeinheit mit homogener Ladung (gleiche Teile) 
        ///            3J Eindeutiger Identifier für Ladeeinheit - JIS Behälter mit Fächern 
        ///            4J Eindeutiger Identifier für Ladeeinheit - JIS Behälter mit 1..n JIS Packstücken
        /// </summary>
        private string _f_7511;
        public string f_7511
        {
            get
            {
                _f_7511 = string.Empty;
                if ((this.sef7511 is clsEdiSegmentElementField) && (this.sef7511.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PCI.EDICreate, this.sef7511, string.Empty, string.Empty);
                    _f_7511 = v.ReturnValue;
                }
                return _f_7511;
            }
        }
        public clsEdiSegmentElementField sef7511
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PCI_C827.Kennung_7511);
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///            Not used
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
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PCI.EDICreate, this.sef1131, string.Empty, string.Empty);
                    _f_1131 = v.ReturnValue;
                }
                return _f_1131;
            }
            set
            {
                _f_1131 = value;
            }
        }
        public clsEdiSegmentElementField sef1131
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PCI_C827.Kennung_1131);
                }
                return tmpSef;
            }
        }


        internal const string Kennung_3055 = "3055";
        /// <summary> 
        ///             Verantwortliche Stelle für die Codepflege, Code 
        ///             5 ISO(International Organization for Standardization)
        /// </summary>
        private string _f_3055;
        public string f_3055
        {
            get
            {
                _f_3055 = string.Empty;
                if ((this.sef3055 is clsEdiSegmentElementField) && (this.sef3055.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PCI.EDICreate, this.sef3055, string.Empty, string.Empty);
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
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PCI_C827.Kennung_3055);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            //if(!f_7511.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)) 
            //{
            //    this.Value += this.f_7511;
            //}
            this.Value += this.f_7511;
            if (!f_1131.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1131;
            }
            if (!f_3055.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
            }

        }



    }
}
