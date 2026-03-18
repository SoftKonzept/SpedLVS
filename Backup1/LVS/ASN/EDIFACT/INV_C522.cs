using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class INV_C522
    {
        internal clsEdiSegmentElement SegElement;
        internal INV INV;
        public const string Kennung_C522 = "C522";

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
        public INV_C522(INV myEdiElement)
        {
            this.INV = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        internal const string Kennung_4403 = "4403";
        /// <summary>       
        /// </summary>
        private string _f_4403 = string.Empty;
        public string f_4403
        {
            get
            {
                _f_4403 = string.Empty;
                if ((this.sef4403 is clsEdiSegmentElementField) && (this.sef4403.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.INV.EDICreate, this.sef4403, string.Empty, string.Empty);
                    _f_4403 = v.ReturnValue;
                }
                return _f_4403;
            }
        }
        public clsEdiSegmentElementField sef4403
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == INV_C522.Kennung_4403);
                }
                return tmpSef;
            }
        }

        internal const string Kennung_4401 = "4401";
        /// <summary>       
        /// </summary>
        private string _f_4401 = string.Empty;
        public string f_4401
        {
            get
            {
                _f_4401 = string.Empty;
                if ((this.sef4401 is clsEdiSegmentElementField) && (this.sef4401.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.INV.EDICreate, this.sef4401, string.Empty, string.Empty);
                    _f_4401 = v.ReturnValue;
                }
                return _f_4401;
            }
        }
        public clsEdiSegmentElementField sef4401
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == INV_C522.Kennung_4401);
                }
                return tmpSef;
            }
        }


        internal const string Kennung_1131 = "1131";
        /// <summary>       
        /// </summary>
        private string _f_1131 = string.Empty;
        public string f_1131
        {
            get
            {
                _f_1131 = string.Empty;
                if ((this.sef1131 is clsEdiSegmentElementField) && (this.sef1131.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.INV.EDICreate, this.sef1131, string.Empty, string.Empty);
                    _f_1131 = v.ReturnValue;
                }
                return _f_1131;
            }
        }
        public clsEdiSegmentElementField sef1131
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == INV_C522.Kennung_1131);
                }
                return tmpSef;
            }
        }

        internal const string Kennung_3055 = "3055";
        /// <summary>       
        /// </summary>
        private string _f_3055 = string.Empty;
        public string f_3055
        {
            get
            {
                _f_3055 = string.Empty;
                if ((this.sef3055 is clsEdiSegmentElementField) && (this.sef3055.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.INV.EDICreate, this.sef3055, string.Empty, string.Empty);
                    _f_3055 = v.ReturnValue;
                }
                return _f_3055;
            }
        }
        public clsEdiSegmentElementField sef3055
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == INV_C522.Kennung_3055);
                }
                return tmpSef;
            }
        }

        internal const string Kennung_4400 = "4400";
        /// <summary>       
        /// </summary>
        private string _f_4400 = string.Empty;
        public string f_4400
        {
            get
            {
                _f_4400 = string.Empty;
                if ((this.sef4400 is clsEdiSegmentElementField) && (this.sef4400.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.INV.EDICreate, this.sef4400, string.Empty, string.Empty);
                    _f_4400 = v.ReturnValue;
                }
                return _f_4400;
            }
        }
        public clsEdiSegmentElementField sef4400
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == INV_C522.Kennung_4400);
                }
                return tmpSef;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_4403;
            if (!f_4401.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_4401;
            }
            if (!_f_1131.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this._f_1131;
            }
            if (!f_3055.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
            }
            if (!f_4400.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_4400;
            }
        }



    }
}
