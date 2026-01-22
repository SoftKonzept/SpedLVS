namespace LVS.ASN.EDIFACT
{
    public class TDT_C001
    {
        internal clsEdiSegmentElement SegElement;
        internal TDT TDT;
        public const string Kennung = "C001";

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
        ///             Art des Transportes
        /// </summary>
        /// <param name="mySegElement"></param>
        public TDT_C001(TDT myEdiElement)
        {
            this.TDT = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_8067 = "8067";
        /// <summary>       
        ///             Art des Transportmittels
        /// </summary>
        private string _f_8067;
        public string f_8067
        {
            get
            {
                _f_8067 = string.Empty;
                return _f_8067;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_8067;
        }



    }
}
