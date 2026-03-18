namespace LVS
{
    public class UNA
    {
        public UNA(clsEdiSegment mySegment)
        {
            this.Segment = mySegment;
            CreateValue();
        }
        internal clsEdiSegment Segment;
        public const string const_Trennzeichen_Gruppe_Doppelpunkt = ":";
        public const string const_Trennzeichen_Segement_Plus = "+";
        public const string const_Dezimal = ".";
        public const string const_Fragezeichen = "?";
        public const string const_BLANK = " ";
        public const string const_SegementEndzeichen = "'";

        public const string Name = "UNA";
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
            this.Value += Name;
            this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt;
            this.Value += UNA.const_Trennzeichen_Segement_Plus;
            this.Value += UNA.const_Dezimal;
            this.Value += UNA.const_Fragezeichen;
            this.Value += UNA.const_BLANK;
            this.Value += UNA.const_SegementEndzeichen;
        }



    }
}
