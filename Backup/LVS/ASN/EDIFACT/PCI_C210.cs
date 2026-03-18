using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class PCI_C210
    {
        internal clsEdiSegmentElement SegElement;
        internal PCI PCI;
        public const string Kennung = "C210";

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
        public PCI_C210(PCI myEdiElement)
        {
            this.PCI = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_7102 = "7102";
        /// <summary>       
        ///            Versandmarkierungen
        /// </summary>
        private string _f_7102;
        public string f_7102
        {
            get
            {
                _f_7102 = string.Empty;
                if ((this.sef7102 is clsEdiSegmentElementField) && (this.sef7102.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.PCI.EDICreate, this.sef7102, string.Empty, string.Empty);
                    _f_7102 = v.ReturnValue;
                }
                return _f_7102;
            }
        }
        public clsEdiSegmentElementField sef7102
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == PCI_C210.Kennung_7102);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            //if(!f_7102.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)) 
            //{
            //    this.Value += this.f_7102;
            //}
            this.Value += this.f_7102;
        }

        //===================================================================================

        /// <summary>
        ///             MARKING INSTRUCTIONS, CODED 
        /// </summary>
        public string f_7102_PackageNumber { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public PCI_C210(string myEdiValueString)
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
                            this.f_7102_PackageNumber = strValue[i].ToString();
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
