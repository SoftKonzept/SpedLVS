using LVS.ASN.ASNFormatFunctions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class UNB_S004
    {
        public UNB_S004(clsEdiSegmentElement mySegElement)
        {
            this.SegElement = mySegElement;
            CreateValue();
        }
        internal clsEdiSegmentElement SegElement;
        public const string Kennung_S004 = "S004";
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
        ///             Datum der Erstellung Format JJMMTT
        /// </summary>
        /// 
        public const string Kennung_0017 = "0017";
        private string _f_0017;
        public string f_0017
        {
            get
            {
                _f_0017 = DateTime.Now.ToString("yyMMdd");
                return _f_0017;
            }
        }
        /// <summary>
        ///             Uhrzeit der Erstellung Format SSMM
        /// </summary>
        /// 
        public const string Kennung_0019 = "0019";
        private string _f_0019;
        public string f_0019
        {
            get
            {
                _f_0019 = DateTime.Now.ToString("HHmm");
                return _f_0019;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            if (!f_0017.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += this.f_0017;
            }
            if (!f_0019.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_0019;
            }
        }


        //===================================================================================

        /// <summary>
        ///             RECIPIENT IDENTIFICATION 
        /// </summary>
        public DateTime f_0017_CreationDateTransmittion { get; set; } = new DateTime(1900, 1, 1);
        public DateTime f_0019_CreationTimeTransmittion { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public UNB_S004(string myEdiValueString)
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
                            string date = strValue[i];
                            this.f_0017_CreationDateTransmittion = Format_DateTimeFromEDI.Execute_yyMMddToDateTime(date);
                            break;
                        case 1:
                            string time = strValue[i];
                            this.f_0019_CreationTimeTransmittion = Format_DateTimeFromEDI.Execute_HHmmToTime(time);
                            break;
                        case 2:
                        case 3:
                        case 4:
                            break;
                    }
                }
            }
        }


        public static DateTime GetMessageCreationDate(string myEdiValueString)
        {
            DateTime dateReturn = DateTime.MinValue;

            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List<string> lSegementElements = myEdiValueString.Split(new char[] { '\'' }).ToList();
                foreach (string s in lSegementElements)
                {
                    if (s.StartsWith(UNB.Name))
                    {
                        List<string> lElements = s.Split(new char[] { '+' }).ToList();
                        for (int i = 0; i < lElements.Count; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                case 1:
                                case 2:
                                case 3:
                                    break;
                                case 4:
                                    UNB_S004 S004 = new UNB_S004(lElements[i].ToString());
                                    DateTime dtDate = S004.f_0017_CreationDateTransmittion;
                                    DateTime dtTime = S004.f_0019_CreationTimeTransmittion;

                                    dateReturn = dtDate;
                                    dateReturn = dateReturn.AddHours(dtTime.Hour);
                                    dateReturn = dateReturn.AddMinutes(dtTime.Minute);
                                    i = lElements.Count;
                                    break;
                            }
                        }
                        if (dateReturn > DateTime.MinValue)
                        {
                            break;
                        }
                    }
                }
            }
            return dateReturn;
        }
    }
}
