using LVS.ASN.ASNFormatFunctions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class DTM_C507
    {
        internal clsEdiSegmentElement SegElement;
        internal DTM DTM;
        public const string Kennung = "C507";
        public bool IsActiv = true;
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
        ///             Datum/Uhrzeit/Zeitspanne
        /// </summary>
        /// <param name="mySegElement"></param>
        public DTM_C507(DTM myEdiElement)
        {
            this.DTM = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        public const string f_2005_selVal_2 = "2";
        public const string f_2005_selVal_10 = "10";
        public const string f_2005_selVal_11 = "11";
        public const string f_2005_selVal_12 = "12";
        public const string f_2005_selVal_36 = "36";
        public const string f_2005_selVal_50 = "50";
        public const string f_2005_selVal_94 = "94";
        public const string f_2005_selVal_102 = "102";
        public const string f_2005_selVal_132 = "132";
        public const string f_2005_selVal_137 = "137";
        public const string f_2005_selVal_171 = "171";
        public const string f_2005_selVal_179 = "179";
        public const string f_2005_selVal_182 = "182";
        /// <summary>
        ///             Datums- oder Uhrzeits- oder Zeitspannen-Funktion, Qualifier
        ///             
        ///               2 Liefertermin (-datum/-zeit), gewünschter
        ///              10 Versanddatum/-zeit, verlangt
        ///              11 Versanddatum/-zeit
        ///              36 Verfalldatum
        ///              94 Produktions-/Herstellungsdatum
        ///             132 Ankunftsdatum/-zeit, geschätzt
        ///             137 Dokumenten-/Nachrichtendatum/-zeit
        ///             171 Referenzdatum/-zeit = Lieferscheindatum
        /// </summary>
        /// 
        public const string Kennung_2005 = "2005";
        private string _f_2005;
        public string f_2005
        {
            set
            {
                _f_2005 = value;
            }
            get
            {
                //_f_2005 = string.Empty;
                if ((this.sef2005 is clsEdiSegmentElementField) && (this.sef2005.ID > 0))
                {
                    _f_2005 = this.sef2005.constValue;
                }
                return _f_2005;
            }
        }
        public clsEdiSegmentElementField sef2005
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == "2005");
                }
                return tmpSef;
            }
        }
        /// <summary>
        ///             Datum oder Uhrzeit oder Zeitspanne, Wert 
        ///                     
        ///               2 Liefertermin (-datum/-zeit), gewünschter
        ///              10 Versanddatum/-zeit, verlangt
        ///              11 Versanddatum/-zeit
        ///              36 Verfalldatum
        ///              94 Produktions-/Herstellungsdatum
        ///             132 Ankunftsdatum/-zeit, geschätzt
        ///             137 Dokumenten-/Nachrichtendatum/-zeit
        ///             171 Referenzdatum/-zeit = Lieferscheindatum
        /// </summary>
        /// 
        public const string Kennung_2380 = "2380";
        private string _f_2380;
        public string f_2380
        {
            get
            {
                _f_2380 = string.Empty;

                if ((this.sef2005 is clsEdiSegmentElementField) && (this.sef2005.ID > 0))
                {
                    switch (this.sef2005.constValue)
                    {
                        case DTM_C507.f_2005_selVal_2:
                        case DTM_C507.f_2005_selVal_10:
                        case DTM_C507.f_2005_selVal_11:
                        case DTM_C507.f_2005_selVal_36:
                        case DTM_C507.f_2005_selVal_50:
                        case DTM_C507.f_2005_selVal_94:
                        case DTM_C507.f_2005_selVal_102:
                        case DTM_C507.f_2005_selVal_132:
                        case DTM_C507.f_2005_selVal_137:
                        case DTM_C507.f_2005_selVal_171:
                        case DTM_C507.f_2005_selVal_179:
                        case DTM_C507.f_2005_selVal_182:


                            if ((this.sef2380 is clsEdiSegmentElementField) && (this.sef2380.ID > 0))
                            {
                                clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.DTM.EDICreate, this.sef2380, string.Empty, string.Empty);
                                DateTime tmpDate;
                                if (DateTime.TryParse(v.ReturnValue, out tmpDate))
                                {
                                    _f_2380 = ValueFormat(tmpDate);
                                }
                                else
                                {
                                    _f_2380 = string.Empty;
                                }
                            }
                            break;
                    }
                }
                return _f_2380;
            }
        }
        public clsEdiSegmentElementField sef2380
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == "2380");
                }
                return tmpSef;
            }
        }

        private string ValueFormat(DateTime myDate)
        {
            string strReturn = string.Empty;
            switch (f_2379)
            {
                case DTM_C507.f_2379_selVal_102:
                    strReturn = myDate.ToString("yyyyMMdd");
                    break;
                case DTM_C507.f_2379_selVal_203:
                    strReturn = myDate.ToString("yyyyMMddHHmm");
                    break;
            }
            return strReturn;
        }

        public const string f_2379_selVal_102 = "102";
        public const string f_2379_selVal_203 = "203";
        /// <summary>
        ///             Datums- oder Uhrzeit- oder Zeitspannen-Format, Code
        ///             102 CCYYMMDD 
        ///             203 CCYYMMDDHHMM
        /// </summary>
        /// 
        public const string Kennung_2379 = "2379";
        private string _f_2379;
        public string f_2379
        {
            set { _f_2379 = value; }
            get
            {
                //_f_2379 = string.Empty;

                if ((this.sef2379 is clsEdiSegmentElementField) && (this.sef2379.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.DTM.EDICreate, this.sef2379, string.Empty, string.Empty);
                    switch (v.ReturnValue)
                    {
                        case DTM_C507.f_2379_selVal_102:
                            _f_2379 = DTM_C507.f_2379_selVal_102;
                            break;
                        case DTM_C507.f_2379_selVal_203:
                            _f_2379 = DTM_C507.f_2379_selVal_203;
                            break;
                    }
                }
                return _f_2379;
            }
        }
        public clsEdiSegmentElementField sef2379
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == "2379");
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            if (!f_2005.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += this.f_2005;
                if (this.f_2005.Equals("50"))
                {
                    string str = string.Empty;
                }
            }
            if (!f_2380.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_2380;
            }
            if (!f_2379.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_2379;
            }

        }
        //===================================================================================

        /// <summary>
        ///             Qualifier
        /// </summary>
        public int f_2005_DateQualifier { get; set; } = 0;
        /// <summary>
        ///             Datum / Uhrzeit
        /// </summary>
        public DateTime f_2380_Date { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        ///             Code
        /// </summary>
        public int f_2379_FormatQualifier { get; set; } = 0;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public DTM_C507(string myEdiValueString)
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
                            iTmp = 0;
                            int.TryParse(strValue[i].ToString(), out iTmp);
                            f_2005_DateQualifier = iTmp;
                            break;
                        case 1:
                            string strDate = strValue[i].ToString();
                            if (strDate.Length == 8)
                            {
                                this.f_2380_Date = Format_GlowDateFromEDI.Execute_yyyyMMddToDateTime(strDate);
                            }
                            if (strDate.Length == 12)
                            {
                                this.f_2380_Date = Format_GlowDateFromEDI.Execute_yyyyMMddHHmmToDateTime(strDate);
                            }
                            break;
                        case 2:
                            iTmp = 0;
                            int.TryParse(strValue[i].ToString(), out iTmp);
                            this.f_2379_FormatQualifier = iTmp;
                            break;
                        case 3:
                        case 4:
                            break;
                    }
                }
            }
        }
    }
}
