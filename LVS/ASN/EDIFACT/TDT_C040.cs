namespace LVS.ASN.EDIFACT
{
    public class TDT_C040
    {
        internal clsEdiSegmentElement SegElement;
        internal TDT TDT;
        public const string Kennung = "C040";

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
        ///             Frachtführernummer
        /// </summary>
        /// <param name="mySegElement"></param>
        public TDT_C040(TDT myEdiElement)
        {
            this.TDT = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_3127 = "3127";
        /// <summary>       
        ///             Frachtführer, Nummer
        /// </summary>
        private string _f_3127;
        public string f_3127
        {
            get
            {
                _f_3127 = string.Empty;
                return _f_3127;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_3127;
        }



    }
}
