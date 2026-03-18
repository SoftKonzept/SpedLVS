using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class EQD
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "EQD";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///            Frachtträger / Transportmittel (Anhänger / Wechselbrücke)
        /// </summary>
        /// <param name="myASNArt"></param>
        public EQD(clsEdiVDACreate myEdiCreate)
        {
            this.EDICreate = myEdiCreate;
            this.Segment = myEdiCreate.ediSegment;

            //switch (this.Segment.AsnArt.Typ)
            //{
            //    case clsASNArt.const_Art_EdifactVDA4987:
            List<clsEdiSegmentElement> List_SE = this.Segment.ListEdiSegmentElement.Where(x => x.EdiSegmentId == this.Segment.ID).ToList();
            if (List_SE.Count > 0)
            {

                foreach (clsEdiSegmentElement itm in List_SE)
                {
                    SelectedSegmentElement = itm;

                    switch (itm.Name)
                    {
                        case EQD.Kennung_8053:
                            se8053 = new clsEdiSegmentElement();
                            se8053 = itm;
                            break;
                        case EQD_C237.Kennung:
                            C237 = new EQD_C237(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
                //}
                //break;
            }
        }

        internal EQD_C237 C237;
        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        public const string Kennung_8053 = "8053";
        /// <summary>
        ///            Equipment, Qualifier
        ///            Qualifier für die Art des Equipments. 
        ///            CN Container 
        ///            RR Eisenbahnwaggon 
        ///            SW Wechselbehälter Wechselbrücke 
        ///            TE Anhänger
        ///             
        /// </summary>
        private string _f_8053;
        public string f_8053
        {
            get
            {
                _f_8053 = "XXX";
                if ((this.sef8053 is clsEdiSegmentElementField) && (this.sef8053.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef8053, string.Empty, string.Empty);
                    _f_8053 = v.ReturnValue;
                }
                return _f_8053;
            }
            set
            {
                _f_8053 = value;
            }
        }

        public clsEdiSegmentElement se8053;
        public clsEdiSegmentElementField sef8053
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se8053.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se8053.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == EQD.Kennung_8053);
                }
                return tmpSef;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += EQD.Name;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_8053;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C237.Value;
            this.Value += UNA.const_SegementEndzeichen;
        }


        //===================================================================================================================

        ///// <summary>
        /////             Datum / Uhrzeit
        ///// </summary>
        //public string f_6311_MeasurementQualifier { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public EQD(string myEdiValueString, string myAsnArt)
        {
            //if ((myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            //{
            //    //C506 = new RFF_C506(this);
            //    //C174 = new MEA_C174(this);
            //    List<string> strValue = myEdiValueString.Split(new char[] { ':', '+' }).ToList();

            //    for (int i = 0; i < strValue.Count; i++)
            //    {
            //        switch (i)
            //        {
            //            case 0:
            //                break;
            //            case 1:
            //                switch (myAsnArt)
            //                {
            //                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
            //                        C506.f_1153_RefQualifier = strValue[i].ToString(); ;
            //                        break;
            //                }
            //                break;
            //            case 2:
            //                switch (myAsnArt)
            //                {
            //                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
            //                        C506.f_1154_RefNumber = strValue[i].ToString();
            //                        break;
            //                }
            //                break;
            //                //case 3:
            //                //    switch (myAsnArt)
            //                //    {
            //                //        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
            //                //            C506.f_1154_RefNumber = strValue[i].ToString();
            //                //            break;
            //                //    }
            //                //    break;
            //                //case 4:
            //                //    switch (myAsnArt)
            //                //    {
            //                //        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
            //                //            int iTmp = 0;
            //                //            int.TryParse(strValue[i].ToString(), out iTmp);
            //                //            C174.f_6314_MeasurementValue = iTmp;
            //                //            break;
            //                //    }
            //                //    break;
            //        }
            //    }

            //}
        }
    }
}
