using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class UNB_S003
    {
        internal clsEdiSegmentElement SegElement;
        public const string Kennung_S003 = "S003";
        internal UNB UNB;

        public UNB_S003(UNB myEdiElement)
        {
            this.UNB = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }


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
        ///             Empfängerbezeichnung Odette-ID des Empfängers
        /// </summary>
        public const string Kennung_0010 = "0010";
        private string _f_0010;
        public string f_0010
        {
            get
            {
                _f_0010 = string.Empty;
                if ((this.sef0010 is clsEdiSegmentElementField) && (this.sef0010.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNB.EDICreate, this.sef0010, string.Empty, string.Empty);
                    _f_0010 = v.ReturnValue;
                }
                return _f_0010;
            }
        }

        public clsEdiSegmentElementField sef0010
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == "0010");
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///             Not used
        /// </summary>
        /// 
        public const string Kennung_0007 = "0007";
        public clsEdiSegmentElementField sef0007
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == "0007");
                }
                return tmpSef;
            }
        }
        private string _f_0007 = string.Empty;
        public string f_0007
        {
            get
            {
                _f_0007 = string.Empty;
                if ((this.sef0007 is clsEdiSegmentElementField) && (this.sef0007.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNB.EDICreate, this.sef0007, string.Empty, string.Empty);
                    _f_0007 = v.ReturnValue;
                }

                return _f_0007;
            }
            set
            {
                //_f_0007 = string.Empty;
                _f_0007 = value;
            }
        }
        /// <summary>
        ///             System-Kennzeichen
        ///             Ursprungs-ERP-System im Volkswagen-Konzern, mit dem der Nachrichten-Inhalt erzeugt wurde. 
        ///             Addresse einer Anwendung oder eines internen Systems beim Empfänger: Bei einigen Herstellern 
        ///             können die Lieferabrufe o.ä. aus unterschiedlichen ERP Systemen generiert werden. Die 
        ///             Lieferavise müssen dann nach Eingang beim Kunden an dieses System weitergeleitet und dort 
        ///             verarbeitet werden. Grundsätzlich sieht EDIFACT im UNB Segment eine Adresse für die 
        ///             Rückleitung (würde z.B. gefüllt im Lieferabruf) und eine Weiterleitungsadresse vor 
        ///             (würde dann im Lieferavis zurückgegeben werden).
        ///             
        ///             EDL hier
        /// </summary>
        /// 
        public const string Kennung_0014 = "0014";
        private string _f_0014 = string.Empty;
        public string f_0014
        {
            get
            {
                _f_0014 = string.Empty;
                if ((this.sef0014 is clsEdiSegmentElementField) && (this.sef0014.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNB.EDICreate, this.sef0014, string.Empty, string.Empty);
                    _f_0014 = v.ReturnValue;
                }
                return _f_0014;
            }
            set
            {
                _f_0014 = value;
            }
        }

        public clsEdiSegmentElementField sef0014
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == "0014");
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            //this.Value += this.f_0010;
            //this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_0007;
            //this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_0014;
            if (!this.f_0010.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += this.f_0010;
            }
            if (!this.f_0007.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_0007;
            }
            if (!this.f_0014.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_0014;
            }
        }


        //===================================================================================

        /// <summary>
        ///             RECIPIENT IDENTIFICATION 
        /// </summary>
        public string f_0010_ReceiverIdentification { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public UNB_S003(string myEdiValueString)
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
                            this.f_0010_ReceiverIdentification = strValue[i].ToString();
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            break;
                    }
                }
            }
        }
    }
}
