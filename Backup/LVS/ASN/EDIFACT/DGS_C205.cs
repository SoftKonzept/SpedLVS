using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class DGS_C205
    {
        internal clsEdiSegmentElement SegElement;
        internal DGS DGS;
        public const string Kennung = "C205";

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
        ///            Gefahrenidentifikation, Code
        /// </summary>
        /// <param name="mySegElement"></param>
        public DGS_C205(DGS myEdiElement)
        {
            this.DGS = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }


        internal const string Kennung_8351 = "8351";
        /// <summary>       
        ///            Gefahrenidentifikation, Code
        /// </summary>
        private string _f_8351;
        public string f_8351
        {
            get
            {
                _f_8351 = null;
                if ((this.sef8351 is clsEdiSegmentElementField) && (this.sef8351.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.DGS.EDICreate, this.sef8351, string.Empty, string.Empty);
                    _f_8351 = v.ReturnValue;
                }
                return _f_8351;
            }
        }
        public clsEdiSegmentElementField sef8351
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == DGS_C205.Kennung_8351);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_8351;
        }



    }
}
