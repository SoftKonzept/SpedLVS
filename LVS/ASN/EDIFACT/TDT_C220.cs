namespace LVS.ASN.EDIFACT
{
    public class TDT_C220
    {
        internal clsEdiSegmentElement SegElement;
        internal TDT TDT;
        public const string Kennung = "C220";

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
        public TDT_C220(TDT myEdiElement)
        {
            this.TDT = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_8067 = "8067";
        /// <summary>       
        ///                 Code für Transortarten
        ///                 
        ///                 10  Seetransport
        ///                 20  Schienentransport
        ///                 30  Straßentransport
        ///                 40  Lufttransport
        ///                 50  Post
        ///                 60  Multimodaler Transport
        /// </summary>
        private string _f_8067;
        public string f_8067
        {
            get
            {
                _f_8067 = "60";
                if (this.TDT.EDICreate.Lager.Ausgang is clsLAusgang)
                {
                    if (this.TDT.EDICreate.Lager.Ausgang.IsWaggon)
                    {
                        _f_8067 = "20";
                    }
                    else
                    {
                        _f_8067 = "30";
                    }
                }
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
