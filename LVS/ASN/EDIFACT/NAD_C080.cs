using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class NAD_C080
    {
        internal clsEdiSegmentElement SegElement;
        internal NAD NAD;
        public const string Kennung = "C080";

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
        ///             Einzelheiten zu Maßangaben
        /// </summary>
        /// <param name="mySegElement"></param>
        public NAD_C080(NAD myEdiElement)
        {
            this.NAD = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }


        internal const string Kennung_3036 = "3036";
        /// <summary>
        ///            Beteiligter Textzeile für den Namen
        /// </summary>
        private string _f_3036;
        public string f_3036
        {
            get
            {
                _f_3036 = string.Empty;
                //if ((this.NAD.ADR is clsADR) && (this.NAD.ADR.ID > 0))
                //{
                //    string strTmp = UNOC_Zeichensatz.Execute(this.NAD.ADR.Name1);
                //    _f_3036 = ediHelper_FormatString.CutValToLenth(strTmp, this.sef3036.Length);
                //}
                if ((this.sef3036 is clsEdiSegmentElementField) && (this.sef3036.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.NAD.EDICreate, this.sef3036, string.Empty, string.Empty);
                    _f_3036 = v.ReturnValue;

                    if (!_f_3036.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
                    {
                        if ((this.NAD.ADR is clsADR) && (this.NAD.ADR.ID > 0))
                        {
                            string strTmp = UNOC_Zeichensatz.Execute(this.NAD.ADR.Name1);
                            _f_3036 = ediHelper_FormatString.CutValToLenth(strTmp, this.sef3036.Length);
                        }
                    }
                }
                return _f_3036;
            }
        }
        public clsEdiSegmentElementField sef3036
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == NAD_C080.Kennung_3036);
                }
                return tmpSef;
            }
        }
        ///// <summary>
        /////             Not used
        ///// </summary>
        //private string _f_1131;
        //public string f_1131
        //{
        //    get { return _f_1131; }
        //    set
        //    {
        //        _f_1131 = string.Empty;
        //    }
        //}


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_3036;

            //if (!f_3036.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            //{
            //    this.Value += this.f_3036;
            //}
        }

        //===================================================================================

        /// <summary>
        ///             NAME OF A PARTY 
        /// </summary>
        public string f_3036_Adressname2 { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public NAD_C080(string myEdiValueString)
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
                            this.f_3036_Adressname2 = strValue[i].ToString();
                            break;
                        case 1:
                            break;
                        case 2:
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
