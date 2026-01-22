using LVS.Constants;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class ALI
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "ALI";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Name des Modulbehälters (Modulname)
        /// </summary>
        /// <param name="myASNArt"></param>
        public ALI(clsEdiVDACreate myEdiCreate)
        {
            this.EDICreate = myEdiCreate;
            this.Segment = myEdiCreate.ediSegment;

            switch (this.Segment.AsnArt.Typ)
            {
                case constValue_AsnArt.const_Art_EdifactVDA4987:
                    List<clsEdiSegmentElement> List_SE = this.Segment.ListEdiSegmentElement.Where(x => x.EdiSegmentId == this.Segment.ID).ToList();
                    if (List_SE.Count > 0)
                    {

                        foreach (clsEdiSegmentElement itm in List_SE)
                        {
                            SelectedSegmentElement = itm;

                            switch (itm.Name)
                            {
                                case ALI.Kennung_3239:
                                    se3239 = new clsEdiSegmentElement();
                                    se3239 = itm;
                                    break;
                                case ALI.Kennung_9213:
                                    se9213 = new clsEdiSegmentElement();
                                    se9213 = itm;
                                    break;
                            }
                            SelectedSegmentElement = null;
                        }
                        CreateValue();
                    }
                    break;
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


        internal const string Kennung_3239 = "3239";
        /// <summary>       
        ///                 Ursprungsland, Code 
        ///                 
        ///                 Bitte geben Sie das konkrete außenwirtschaftsrechtliche Ursprungsland an. Jeder Ware kann aufgrund 
        ///                 ihres Entstehungsprozesses ein Ursprungsland zugewiesen werden. Das Ursprungsland entspricht i.d.R. 
        ///                 dem Land in dem die Ware durch ein Unternehmen der letzten wesentlichen, wirtschaftlich gerechtfertigten 
        ///                 Be- oder Verarbeitung unterzogen worden ist. Die Bestimmung des Ursprungslandes richtet sich nach den 
        ///                 nationalen Vorschriften. In der Europäischen Union ist Art. 60 UZK einschlägig. Bei Fragen wenden Sie 
        ///                 sich bitte per E-Mail an die Zollabteilung des VW-Konzerns, E-Mail: wup@volkswagen.de.
        /// </summary>
        private string _f_3239;
        public string f_3239
        {
            get
            {
                _f_3239 = string.Empty;
                if ((this.sef3239 is clsEdiSegmentElementField) && (this.sef3239.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef3239, string.Empty, string.Empty);
                    _f_3239 = v.ReturnValue;
                }
                return _f_3239;
            }
        }
        public clsEdiSegmentElement se3239;
        public clsEdiSegmentElementField sef3239
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se3239.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se3239.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == ALI.Kennung_3239);
                }
                return tmpSef;
            }
        }


        internal const string Kennung_9213 = "9213";
        /// <summary>       
        ///                 Zollregelungsart, Code Muss gefüllt sein, wenn FTX+CUS übertragen wurde (Präferenzerklärung). 
        ///                 Ist der Status unklar, muss "N" übertragen werden. 
        ///                 Y = Präferenzberechtigte Waren 
        ///                 N = keine Präferenzberechtigung 
        ///                     N Nein, Ware ist nicht präferenzberechtigt 
        ///                     Y Ja, Ware ist präferenzberechtigt
        /// </summary>
        private string _f_9213;
        public string f_9213
        {
            get
            {
                _f_9213 = string.Empty;
                if ((this.sef9213 is clsEdiSegmentElementField) && (this.sef9213.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef9213, string.Empty, string.Empty);
                    _f_9213 = v.ReturnValue;
                }
                return _f_9213;
            }
        }
        public clsEdiSegmentElement se9213;
        public clsEdiSegmentElementField sef9213
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se9213.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se9213.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == ALI.Kennung_9213);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += ALI.Name;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_3239;
            this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_9213;
            this.Value += UNA.const_SegementEndzeichen;
        }



        //===================================================================================================================

        /// <summary>
        ///             Datum / Uhrzeit
        /// </summary>
        //public int f_0062_MesRefNo { get; set; } = 0;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public ALI(string myEdiValueString, string myAsnArt)
        {
            //if ((myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            //{
            //    //C002 = new BGM_C002(this);
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
            //                        f_0062_MesRefNo = int.Parse(strValue[i].ToString());
            //                        break;
            //                }
            //                break;
            //                //case 2:
            //                //    switch (myAsnArt)
            //                //    {
            //                //        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
            //                //            f_1004_DesAdvNo = strValue[i].ToString();
            //                //            break;
            //                //    }
            //                //    break;
            //        }
            //    }

            //}
        }
    }
}
