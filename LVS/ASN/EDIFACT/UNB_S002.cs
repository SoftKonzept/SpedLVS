using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class UNB_S002
    {
        internal clsEdiSegmentElement SegElement;
        public const string Kennung_S002 = "S002";
        internal UNB UNB;


        public UNB_S002(UNB myEdiElement)
        {
            this.UNB = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

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
        ///             Absenderbezeichnung Odette-ID des Datensenders
        /// </summary>
        internal const string Kennung_0004 = "0004";
        private string _f_0004 = string.Empty;
        public string f_0004
        {
            get
            {
                _f_0004 = "Odette012345";
                if ((this.sef0004 is clsEdiSegmentElementField) && (this.sef0004.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNB.EDICreate, this.sef0004, string.Empty, string.Empty);
                    _f_0004 = v.ReturnValue;
                }
                return _f_0004;
            }
            set
            {
                _f_0004 = value;
            }
        }

        public clsEdiSegmentElementField sef0004
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNB_S002.Kennung_0004);
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///             Not used
        /// </summary>
        internal const string Kennung_0007 = "0007";
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
                _f_0007 = value;
            }
        }
        public clsEdiSegmentElementField sef0007
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNB_S002.Kennung_0007);
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///             Adresse für Rückleitung
        /// </summary>
        internal const string Kennung_0008 = "0008";
        private string _f_0008 = string.Empty;
        public string f_0008
        {
            get
            {
                _f_0008 = string.Empty;
                if ((this.sef0008 is clsEdiSegmentElementField) && (this.sef0008.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.UNB.EDICreate, this.sef0008, string.Empty, string.Empty);
                    _f_0008 = v.ReturnValue;
                }
                return _f_0008;
            }
        }

        public clsEdiSegmentElementField sef0008
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNB_S002.Kennung_0008);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {

            if (!this.f_0004.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += this.f_0004;
            }
            if (!this.f_0007.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_0007;
            }
            if (!this.f_0008.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_0008;
            }
        }


        //===================================================================================

        /// <summary>
        ///             SENDER IDENTIFICATION 
        /// </summary>
        public string f_0004_SenderIdentification { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public UNB_S002(string myEdiValueString)
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
                            this.f_0004_SenderIdentification = strValue[i].ToString();
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
