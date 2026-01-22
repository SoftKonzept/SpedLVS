using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class LOC
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "LOC";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Beladestelle
        /// </summary>
        /// <param name="myASNArt"></param>
        public LOC(clsEdiVDACreate myEdiCreate)
        {
            this.EDICreate = myEdiCreate;
            this.Segment = myEdiCreate.ediSegment;
            List<clsEdiSegmentElement> List_SE = this.Segment.ListEdiSegmentElement.Where(x => x.EdiSegmentId == this.Segment.ID && x.IsActive == true).ToList();
            if (List_SE.Count > 0)
            {
                foreach (clsEdiSegmentElement itm in List_SE)
                {
                    SelectedSegmentElement = itm;

                    switch (itm.Name)
                    {
                        case LOC.Kennung_3227:
                            se3227 = new clsEdiSegmentElement();
                            se3227 = itm;
                            break;

                        case LOC_C517.Kennung_C517:
                            C517 = new LOC_C517(this);
                            break;
                        case LOC_C519.Kennung_C519:
                            C519 = new LOC_C519(this);
                            break;
                        case LOC_C553.Kennung_C553:
                            C553 = new LOC_C553(this);
                            break;

                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
            }
        }

        internal LOC_C517 C517;
        internal LOC_C519 C519;
        internal LOC_C553 C553;

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }


        public const string f_3227_SelectedValue_1 = "1";
        public const string f_3227_SelectedValue_7 = "7";
        public const string f_3227_SelectedValue_9 = "9";
        public const string f_3227_SelectedValue_11 = "11";
        public const string f_3227_SelectedValue_13 = "13";
        public const string f_3227_SelectedValue_159 = "159";

        public const string Kennung_3227 = "3227";
        /// <summary>
        ///             Ortsangabe, Qualifier 
        ///             1 Für die Lieferbedingung relevanter Ort
        ///             9 Ladeort/Ladehafen
        ///             7 Lieferort
        ///             11 Entladeort/Löschhafen
        ///             13 Umschlagsort
        ///             159 Additional internal destination
        /// </summary>
        private string _f_3227;
        public string f_3227
        {
            get
            {
                _f_3227 = string.Empty;
                if ((this.sef3227 is clsEdiSegmentElementField) && (this.sef3227.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef3227, string.Empty, string.Empty);
                    _f_3227 = v.ReturnValue;
                }
                return _f_3227;
            }
            set
            {
                _f_3227 = value;
            }
        }

        public clsEdiSegmentElement se3227;
        public clsEdiSegmentElementField sef3227
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se3227.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se3227.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == LOC.Kennung_3227);
                }
                return tmpSef;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += LOC.Name;
            if (!f_3227.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_3227;
            }
            if (!this.C517.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C517.Value;
            }
            if ((this.C519 != null) && (!this.C519.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C519.Value;
            }
            if ((this.C519 != null) && (!this.C553.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed)))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C553.Value;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }



        //===================================================================================================================

        ///// <summary>
        /////             PARTY QUALIFIER 
        ///// </summary>
        public string f_3227_LocationCodeQualifier { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public LOC(string myEdiValueString, string myAsnArt)
        {
            if (!(myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {

                List<string> strValue = myEdiValueString.Split(new char[] { ':', '+' }).ToList();

                for (int i = 0; i < strValue.Count; i++)
                {
                    switch (i)
                    {
                        case 0:

                            break;
                        case 1:
                            this.f_3227_LocationCodeQualifier = strValue[i].ToString();
                            break;
                        case 2:
                            C517 = new LOC_C517(strValue[i].ToString(), myAsnArt);
                            break;
                    }
                }

            }
        }
    }
}
