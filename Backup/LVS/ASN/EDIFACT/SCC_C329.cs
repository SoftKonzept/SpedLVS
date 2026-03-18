namespace LVS.ASN.EDIFACT
{
    public class SCC_C329
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "C329";
        public bool IsActiv = true;

        internal clsEdiSegmentElement SegElement;
        internal SCC SSC;
        private string _value = string.Empty;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
            }
        }

        public SCC_C329(LVS.ASN.EDIFACT.SCC myEdiElement)
        {
            this.SSC = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        /// </summary>
        private string _f_2013;
        public string f_2013
        {
            get
            {
                _f_2013 = string.Empty;
                return _f_2013;
            }
            set
            {
                _f_2013 = value;
            }
        }


        /// <summary>
        ///            Not used
        /// </summary>
        private string _f_2015;
        public string f_2015
        {
            get
            {
                _f_2015 = string.Empty;
                return _f_2015;
            }
            set
            {
                _f_2015 = value;
            }
        }

        /// <summary>
        ///            Not used
        /// </summary>
        private string _f_2017;
        public string f_2017
        {
            get
            {
                _f_2017 = string.Empty;
                return _f_2017;
            }
            set
            {
                _f_2017 = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_2013;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_2015;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_2017;
            this.Value += UNA.const_SegementEndzeichen;
        }



    }
}
