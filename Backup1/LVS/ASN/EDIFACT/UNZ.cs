using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class UNZ
    {
        internal clsEdiSegmentElement SelectedSegmentElement;
        internal clsEdiVDACreate EDICreate;

        /// <summary>
        ///             Nachrichten-Endesegment
        /// </summary>
        /// <param name="myASNArt"></param>
        public UNZ(clsEdiVDACreate myEdiCreate)
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
        public const string Name = "UNZ";
        internal clsASNArt asnArt;


        public const string Kennung_0036 = "0036";
        /// <summary>
        ///             Datenaustauschzähler Anzahl der Nachrichten in der Datenaustauschdatei
        /// </summary>
        private string _f_0036;
        public string f_0036
        {
            get
            {
                _f_0036 = "1";
                return _f_0036;
            }
            set
            {
                _f_0036 = value;
            }
        }

        /// <summary>
        ///             Datenaustauschreferenz Eindeutige ID der Datenübertragung.
        ///             ID aus UNB | 0020 | 0020
        ///             3. Element
        /// </summary>
        /// 
        public const string Kennung_0020 = "0020";
        private string _f_0020;
        public string f_0020
        {
            get
            {
                string strReturn = string.Empty;
                if (this.EDICreate.ListEdiVDASatzString.Count > 0)
                {
                    string strUNB = this.EDICreate.ListEdiVDASatzString.FirstOrDefault(x => x.StartsWith(UNB.Name));
                    List<string> listElements = strUNB.Split('+').ToList();
                    if (listElements.Count >= 6)
                    {
                        if (listElements[5] != null)
                        {
                            strReturn = listElements[5].ToString();
                        }
                    }
                }

                _f_0020 = strReturn.Replace("'", "");
                return _f_0020;
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
            this.Value += UNZ.Name;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_0036;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_0020;
            this.Value += UNA.const_SegementEndzeichen;
        }



    }
}
