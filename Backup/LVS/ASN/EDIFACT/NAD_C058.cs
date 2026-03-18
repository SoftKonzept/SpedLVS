using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class NAD_C058
    {
        internal clsEdiSegmentElement SegElement;
        internal NAD NAD;
        public const string Kennung = "C058";

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
        public NAD_C058(NAD myEdiElement)
        {
            this.NAD = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }



        //internal const string Kennung_3039 = "3039";
        /// <summary>
        ///                Zeile für Name und Anschrift
        /// </summary>

        internal const string Kennung_3124 = "3124";
        private string _f_3124 = string.Empty;
        public string f_3124
        {
            get
            {
                _f_3124 = string.Empty;
                if ((this.sef3124 is clsEdiSegmentElementField) && (this.sef3124.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.NAD.EDICreate, this.sef3124, string.Empty, string.Empty);
                    _f_3124 = v.ReturnValue;
                }
                return _f_3124;
            }
        }
        public clsEdiSegmentElementField sef3124
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == NAD_C058.Kennung_3124);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_3124;
            //if (!f_3124.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            //{
            //    this.Value += this.f_3124;
            //}
        }

        //===================================================================================

        /// <summary>
        ///             PARTY ID IDENTIFICATION 
        /// </summary>
        public string f_3124_Adressname { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public NAD_C058(string myEdiValueString)
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
                            this.f_3124_Adressname = strValue[i].ToString();
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
