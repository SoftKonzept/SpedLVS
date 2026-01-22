using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class TDT_C222
    {
        internal clsEdiSegmentElement SegElement;
        internal TDT TDT;
        public const string Kennung = "C222";

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
        ///             Transportmittel-Identifikation
        /// </summary>
        /// <param name="mySegElement"></param>
        public TDT_C222(TDT myEdiElement)
        {
            this.TDT = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_8213 = "8213";
        /// <summary>       
        ///                 Transportmittel, Identifikation
        ///                 
        ///                 Abhängig von der Transportmittelart ist das polizeiliche Kennzeichen des LKWs bzw. 
        ///                 die Waggon- oder Wechselbrückennummer, der Schiffsname oder Flugnummer einzusetzen. 
        ///                 Die Identifikation eines Anhängers, Sattelaufliegers oder anderer zusätzlicher 
        ///                 Transportausrüstung erfolgt im EQD-Segment.
        /// </summary>
        private string _f_8213;
        public string f_8213
        {
            get
            {
                _f_8213 = string.Empty;
                if ((this.sef8213 is clsEdiSegmentElementField) && (this.sef8213.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.TDT.EDICreate, this.sef8213, string.Empty, string.Empty);
                    _f_8213 = v.ReturnValue;
                }
                return _f_8213;
            }
        }
        public clsEdiSegmentElementField sef8213
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == TDT_C222.Kennung_8213);
                }
                return tmpSef;
            }
        }

        /// <summary>       
        ///                 Not used
        /// </summary>
        private string _f_1131;
        public string f_1131
        {
            get
            {
                _f_1131 = string.Empty;
                return _f_1131;
            }
        }

        internal const string Kennung_3055 = "3055";
        /// <summary>       
        ///                 Transportmittel, Identifikation
        ///                 
        ///                 Abhängig von der Transportmittelart ist das polizeiliche Kennzeichen des LKWs bzw. 
        ///                 die Waggon- oder Wechselbrückennummer, der Schiffsname oder Flugnummer einzusetzen. 
        ///                 Die Identifikation eines Anhängers, Sattelaufliegers oder anderer zusätzlicher 
        ///                 Transportausrüstung erfolgt im EQD-Segment.
        /// </summary>
        private string _f_3055;
        public string f_3055
        {
            get
            {
                _f_8213 = string.Empty;
                if ((this.sef3055 is clsEdiSegmentElementField) && (this.sef3055.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.TDT.EDICreate, this.sef3055, string.Empty, string.Empty);
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
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == TDT_C222.Kennung_3055);
                }
                return tmpSef;
            }
        }

        internal const string Kennung_8453 = "8453";
        /// <summary>       
        ///                 Nationalität des Transportmittels, 
        ///                 Code Identifikation des Ländernamens oder eines anderen.
        /// </summary>
        private string _f_8453;
        public string f_8453
        {
            get
            {
                _f_8453 = string.Empty;
                if ((this.sef8453 is clsEdiSegmentElementField) && (this.sef8453.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.TDT.EDICreate, this.sef8453, string.Empty, string.Empty);
                    _f_8453 = v.ReturnValue;
                }
                return _f_8453;
            }
        }
        public clsEdiSegmentElementField sef8453
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == TDT_C222.Kennung_8453);
                }
                return tmpSef;
            }
        }
        /// <summary>       
        ///                 Not used
        /// </summary>
        private string _f_8212;
        public string f_8212
        {
            get
            {
                _f_8212 = string.Empty;
                return _f_8212;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_8213;
            this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1131;
            this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
            this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_8212;
            this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_8453;
        }


        //===================================================================================

        /// <summary>
        ///             ID OF MEANS OF TRANSPORT  
        /// </summary>
        public string f_8212_VehicleId { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public TDT_C222(string myEdiValueString)
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
                        case 1:
                        case 2:
                            break;
                        case 3:
                            this.f_8212_VehicleId = strValue[i].ToString();
                            break;
                        case 4:
                            break;
                    }
                }
            }
            else
            {
                this.f_8212_VehicleId = string.Empty;
            }
        }
    }
}
