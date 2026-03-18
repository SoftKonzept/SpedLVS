using LVS.ASN.EDIFACT;
using LVS.ViewData;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class UNB
    {
        internal clsEdiSegmentElement SelectedSegmentElement;
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "UNB";
        internal clsASNArt asnArt;

        internal UNB_S001 S001;
        internal UNB_S002 S002;
        internal UNB_S003 S003;
        internal UNB_S004 S004;
        internal UNB_S005 S005;



        public EdiSegmentViewData ediSegmentVD = new EdiSegmentViewData();

        public UNB()
        {
            S001 = new UNB_S001(this);
            S002 = new UNB_S002(this);
            S003 = new UNB_S003(this);
            S004 = new UNB_S004(new clsEdiSegmentElement());
            S005 = new UNB_S005(this);
            se0020 = new clsEdiSegmentElement();
            se0026 = new clsEdiSegmentElement();
            se0029 = new clsEdiSegmentElement();
            se0031 = new clsEdiSegmentElement();
            se0032 = new clsEdiSegmentElement();
            se0035 = new clsEdiSegmentElement();
            InitEdiSegmentValue();

        }

        public void InitEdiSegmentValue()
        {
            ediSegmentVD = new EdiSegmentViewData();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myASNArt"></param>
        public UNB(clsEdiVDACreate myEdiCreate)
        {

            this.EDICreate = myEdiCreate;
            this.Segment = myEdiCreate.ediSegment;

            //switch (this.Segment.AsnArt.Typ)
            //{
            //    case clsASNArt.const_Art_EdifactVDA4987:
            //    case clsASNArt.const_Art_DESADV_BMW_4a:
            List<clsEdiSegmentElement> List_SE = this.Segment.ListEdiSegmentElement.Where(x => x.EdiSegmentId == this.Segment.ID).ToList();
            if (List_SE.Count > 0)
            {
                foreach (clsEdiSegmentElement itm in List_SE)
                {
                    SelectedSegmentElement = itm;

                    switch (itm.Name)
                    {
                        case UNB_S001.Kennung_S001:
                            S001 = new UNB_S001(this);
                            break;
                        case UNB_S002.Kennung_S002:
                            S002 = new UNB_S002(this);
                            break;
                        case UNB_S003.Kennung_S003:
                            S003 = new UNB_S003(this);
                            break;
                        case UNB_S004.Kennung_S004:
                            S004 = new UNB_S004(itm);
                            break;
                        case UNB_S005.Kennung_S005:
                            S005 = new UNB_S005(this);
                            break;
                        case UNB.Kennung_0020:
                            se0020 = new clsEdiSegmentElement();
                            se0020 = itm;
                            break;
                        case UNB.Kennung_0026:
                            se0026 = new clsEdiSegmentElement();
                            se0026 = itm;
                            break;
                        case UNB.Kennung_0029:
                            se0029 = new clsEdiSegmentElement();
                            se0029 = itm;
                            break;
                        case UNB.Kennung_0031:
                            se0031 = new clsEdiSegmentElement();
                            se0031 = itm;
                            break;
                        case UNB.Kennung_0032:
                            se0032 = new clsEdiSegmentElement();
                            se0032 = itm;
                            break;
                        case UNB.Kennung_0035:
                            se0035 = new clsEdiSegmentElement();
                            se0035 = itm;
                            break;

                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
                //}
                //break;            
            }
        }

        /// <summary>
        ///             Datenaustauschreferenz Eindeutige ID einer Datenübertragung.
        ///             Lieferantennummer
        /// </summary>
        internal const string Kennung_0020 = "0020";

        public clsEdiSegmentElement se0020;
        public clsEdiSegmentElementField sef0020
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se0020.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se0020.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNB.Kennung_0020);
                }
                return tmpSef;
            }
        }
        private string _f_0020;
        public string f_0020
        {
            get
            {
                _f_0020 = "123456789";
                if ((this.sef0020 is clsEdiSegmentElementField) && (this.sef0020.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef0020, string.Empty, string.Empty);
                    _f_0020 = v.ReturnValue;
                }
                return _f_0020;
            }
            set
            {
                _f_0020 = value;
            }
        }
        /// <summary>
        ///             Not used
        /// </summary>

        internal const string Kennung_0026 = "0026";
        public clsEdiSegmentElement se0026;
        public clsEdiSegmentElementField sef0026
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se0026.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se0026.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNB.Kennung_0026);
                }
                return tmpSef;
            }
        }

        private string _f_0026 = string.Empty;
        public string f_0026
        {
            get
            {
                if ((this.sef0026 is clsEdiSegmentElementField) && (this.sef0026.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef0026, string.Empty, string.Empty);
                    _f_0026 = v.ReturnValue;
                }
                return _f_0026;
            }
            set
            {
                _f_0026 = string.Empty;
            }
        }
        /// <summary>
        ///             Not used
        /// </summary>
        internal const string Kennung_0029 = "0029";
        public clsEdiSegmentElement se0029;
        public clsEdiSegmentElementField sef0029
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se0029.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se0029.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNB.Kennung_0029);
                }
                return tmpSef;
            }
        }

        private string _f_0029 = string.Empty;
        public string f_0029
        {
            get
            {
                if ((this.sef0029 is clsEdiSegmentElementField) && (this.sef0029.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef0029, string.Empty, string.Empty);
                    _f_0029 = v.ReturnValue;
                }
                return _f_0029;
            }
            set
            {
                _f_0029 = string.Empty;
            }
        }
        /// <summary>
        ///             Not used
        /// </summary>
        internal const string Kennung_0031 = "0031";
        public clsEdiSegmentElement se0031;
        public clsEdiSegmentElementField sef0031
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se0031.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se0031.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNB.Kennung_0031);
                }
                return tmpSef;
            }
        }

        private string _f_0031 = string.Empty;
        public string f_0031
        {
            get
            {
                if ((this.sef0031 is clsEdiSegmentElementField) && (this.sef0031.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef0031, string.Empty, string.Empty);
                    _f_0031 = v.ReturnValue;
                }
                return _f_0031;
            }
            set
            {
                _f_0031 = string.Empty;
            }
        }
        /// <summary>
        ///             Not used
        /// </summary>
        internal const string Kennung_0032 = "0032";
        public clsEdiSegmentElement se0032;
        public clsEdiSegmentElementField sef0032
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se0032.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se0032.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNB.Kennung_0032);
                }
                return tmpSef;
            }
        }

        private string _f_0032 = string.Empty;
        public string f_0032
        {
            get
            {
                if ((this.sef0032 is clsEdiSegmentElementField) && (this.sef0032.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef0032, string.Empty, string.Empty);
                    _f_0032 = v.ReturnValue;
                }
                return _f_0032;
            }
            set
            {
                _f_0032 = string.Empty;
            }
        }
        /// <summary>
        ///             Test-Kennzeichen
        ///             Bei Übertragung des Testkennzeichens wird die Nachricht nicht an das 
        ///             Empfänger-System weitergeleitet. Es erfolgt nur eine Prüfung der Nachricht 
        ///             und der Absender erhält ein Prüfungsbericht. 
        ///             1 Übertragungsdatei ist ein Test Wird nur benutzt, wenn der Datenaustausch zu
        /// </summary>
        /// 
        internal const string Kennung_0035 = "0035";
        public clsEdiSegmentElement se0035;
        public clsEdiSegmentElementField sef0035
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se0035.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se0035.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == UNB.Kennung_0035);
                }
                return tmpSef;
            }
        }
        private string _f_0035 = string.Empty;
        public string f_0035
        {
            get
            {
                //if (this.EDICreate.Sys.DebugModeCOM)
                //{
                //    _f_0035 = "1";
                //}
                //else
                //{
                //    if ((this.sef0035 is clsEdiSegmentElementField) && (this.sef0035.ID > 0))
                //    {
                //        clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef0035, string.Empty, string.Empty);
                //        _f_0035 = v.ReturnValue;
                //    }
                //    _f_0035 = string.Empty;
                //}
                if ((this.sef0035 is clsEdiSegmentElementField) && (this.sef0035.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef0035, string.Empty, string.Empty);
                    _f_0035 = v.ReturnValue;
                }
                //_f_0035 = string.Empty;
                return _f_0035;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += UNB.Name;
            if (!S001.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + S001.Value;
            }
            if (!S002.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + S002.Value;
            }
            if (!S003.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + S003.Value;
            }
            if (!S004.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + S004.Value;
            }
            if (!f_0020.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_0020;
            }
            if (!S005.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + S005.Value;
            }
            if (!f_0026.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_0026;
            }
            if (!f_0029.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_0029;
            }
            if (!f_0031.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_0031;
            }
            if (!f_0032.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_0032;
            }
            if (!f_0035.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_0035;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }


        //===================================================================================================================

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public UNB(string myEdiValueString)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List<string> lElements = myEdiValueString.Split(new char[] { '+' }).ToList();
                for (int i = 0; i < lElements.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2:
                            S002 = new UNB_S002(lElements[i].ToString());
                            break;
                        case 3:
                            S003 = new UNB_S003(lElements[i].ToString());
                            break;
                        case 4:
                            S004 = new UNB_S004(lElements[i].ToString());
                            break;
                    }
                }

            }
        }

    }
}
