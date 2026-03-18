using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class UNT
    {
        internal clsEdiVDACreate EDICreate;

        /// <summary>
        ///             Nachrichten-Endesegment
        /// </summary>
        /// <param name="myASNArt"></param>
        public UNT(clsEdiVDACreate myEdiCreate)
        {
            this.EDICreate = myEdiCreate;
            this.Segment = myEdiCreate.ediSegment;

            List<clsEdiSegmentElement> List_SE = this.Segment.ListEdiSegmentElement.Where(x => x.EdiSegmentId == this.Segment.ID).ToList();
            if (List_SE.Count > 0)
            {
                CreateValue();
            }
        }
        internal clsEdiSegment Segment;
        public const string Name = "UNT";
        internal clsASNArt asnArt;


        public const string Kennung_0074 = "0074";
        /// <summary>
        ///             Anzahl der Segmente in der Nachricht
        ///              Summe aller Segmente ohne
        ///              UNZ, UNT, UNH
        ///              
        ///             hier -1, da UNZ und UNT noch nicht enthalten sind, sondern nur UNH
        /// </summary>
        private string _f_0074;
        public string f_0074
        {
            get
            {
                _f_0074 = (this.EDICreate.ListEdiVDASatzString.Count - 1).ToString();
                return _f_0074;
            }
        }

        /// <summary>
        ///             Nachrichten-Referenznummer Nachrichtenreferenznummer
        ///             aus UNH.0062
        /// </summary>
        public const string Kennung_0062 = "0062";
        private string _f_0062;
        public string f_0062
        {
            get
            {
                _f_0062 = string.Empty;
                if (this.EDICreate.ListEdiVDASatzString.Count > 0)
                {
                    foreach (string item in this.EDICreate.ListEdiVDASatzString)
                    {
                        if (item.StartsWith(UNH.Name))
                        {
                            List<string> listSegment = item.Split('+').ToList();
                            if ((listSegment.Count > 0) && (listSegment[1] != null))
                            {
                                _f_0062 = listSegment[1].ToString();
                            }
                        }
                    }
                }
                return _f_0062;
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
            this.Value += UNT.Name;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_0074;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_0062;
            this.Value += UNA.const_SegementEndzeichen;
        }



    }
}
