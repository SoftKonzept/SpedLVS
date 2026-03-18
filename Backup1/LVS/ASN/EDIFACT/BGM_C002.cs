using LVS.Constants;
using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class BGM_C002
    {


        internal clsEdiSegment Segment;
        internal clsEdiSegmentElement SelectedSegmentElement;


        /// <summary>
        ///             Dokumenten-/Nachrichtenname
        /// </summary>
        /// <param name="mySegElement"></param>
        public BGM_C002(BGM myBGM)
        {
            this.BGM = myBGM;
            this.SegElement = myBGM.SelectedSegmentElement;
            CreateValue();
        }

        internal clsEdiSegmentElement SegElement;
        internal BGM BGM;
        public const string Kennung_C002 = "C002";
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
        ///             351 Liefermeldung
        /// </summary>
        /// 
        internal const string Kennung_1001 = "1001";

        public clsEdiSegmentElement se1001;
        public clsEdiSegmentElementField sef1001
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == Kennung_1001);
                }
                return tmpSef;
            }
        }

        private string _f_1001 = string.Empty;
        public string f_1001
        {
            get
            {
                _f_1001 = "351";
                if ((this.sef1001 is clsEdiSegmentElementField) && (this.sef1001.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.BGM.EDICreate, this.sef1001, string.Empty, string.Empty);
                    _f_1001 = v.ReturnValue;
                }
                return _f_1001;
            }
        }
        /// <summary>
        ///             Not used
        /// </summary>
        internal const string Kennung_1131 = "1131";

        public clsEdiSegmentElement se1131;
        public clsEdiSegmentElementField sef1131
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == Kennung_1131);
                }
                return tmpSef;
            }
        }
        private string _f_1131 = string.Empty;
        public string f_1131
        {
            get
            {
                _f_1131 = string.Empty;
                if ((this.sef1131 is clsEdiSegmentElementField) && (this.sef1131.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.BGM.EDICreate, this.sef1131, string.Empty, string.Empty);
                    _f_1131 = v.ReturnValue;
                }
                return _f_1131;
            }
        }
        /// <summary>
        ///             Not used
        /// </summary>
        /// 
        internal const string Kennung_3055 = "3055";

        public clsEdiSegmentElement se3055;
        public clsEdiSegmentElementField sef3055
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == Kennung_3055);
                }
                return tmpSef;
            }
        }

        private string _f_3055 = string.Empty;
        public string f_3055
        {
            get
            {
                _f_3055 = string.Empty;
                if ((this.sef3055 is clsEdiSegmentElementField) && (this.sef3055.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.BGM.EDICreate, this.sef3055, string.Empty, string.Empty);
                    _f_3055 = v.ReturnValue;
                }
                return _f_3055;
            }
        }
        /// <summary>
        ///             Prozesskennzeichen 
        ///             Prozesskennzeichen aus dem Lieferabruf, der der Lieferung zugrunde liegt. Das Prozesskennzeichen wird derzeit 
        ///             nur in der VDA 4984 übertragen. Bei Lieferungen, bei denen noch alte EDI-Abruf-Formate ohne Prozesskennzeichen 
        ///             verwendet werden, wie z. B. die VDA 4905, sind die Prozesskennzeichen wie beschrieben zu verwenden. Bei Prozessen 
        ///             mit unterschiedlichen, aufeinanderfolgenden Abrufen, wie z. B. NLK- Abrufvorschau und NLK-Versandabruf ist 
        ///             immer das Kennzeichen des letzten verbindlichen Abrufs zu übertragen. In dem Beispiel also NLK. LAB-ED Lieferabruf 
        ///             mit Eintreffdatum (VDA 4905 / EDIFACT DELFOR als LAB) VAB-DDP OT-Sonderprozesse (DELJIT/CALDEL) - Versandabruf
        ///             Direct Delivery Process VAB-CHA NLK-Versandabruf für Chattanooga VAB-NLK NLK-Versandabruf PROD- Fahrzeugbezogene 
        ///             Abrufe NR JIS-IST Ist-Sequenz-Abwicklung STEEL Sonderprozesse für bestimmte StahllieferungenProzesskennzeichen 
        ///             im Zuammenhang mit Zusatzangaben für Stahllieferungen in SG 14 EDL Externer-Dienstleister-Prozess Beim Prozesskennzeichen 
        ///             STEEL sind zwingend Stahl- spezifische Informationen in der SG 14 zu übertragen. Zuätzlich ist die RFID des 
        ///             Behälters zu übertragen. Diesen Prozess wird zum Zeitpunkt der Einführung dieses Prozesskennzeichens nur von 
        ///             SEAT und VW Navarra geben.
        ///             
        ///             hier EDL
        /// </summary>
        /// 
        internal const string Kennung_1000 = "1000";

        public clsEdiSegmentElement se1000;
        public clsEdiSegmentElementField sef1000
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                //if (this.se1000.ListEdiSegmentElementFields.Count > 0)
                //{
                //    tmpSef = this.se1000.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shorcut == BGM_C002.Kennung_1000);
                //}
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == BGM_C002.Kennung_1000);
                }
                return tmpSef;
            }
        }

        private string _f_1000 = string.Empty;
        public string f_1000
        {
            get
            {
                _f_1000 = "EDL";
                if ((this.sef1000 is clsEdiSegmentElementField) && (this.sef1000.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.BGM.EDICreate, this.sef1000, string.Empty, string.Empty);
                    _f_1000 = v.ReturnValue;
                }
                return _f_1000;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {

            if (!f_1001.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += this.f_1001;
            }
            if (!f_1131.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1131;
            }
            if (!f_3055.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_3055;
            }
            if (!f_1000.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_1000;
            }
        }


        //===================================================================================

        /// <summary>
        ///             MessageTyp 
        /// </summary>
        public int f_1001_MessageTyp { get; set; } = 0;

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public BGM_C002(string myEdiValueString, string myAsnArt)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List<string> strValue = myEdiValueString.Split(new char[] { ':' }).ToList();

                for (int i = 0; i < strValue.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            switch (myAsnArt)
                            {
                                case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                    int iTmp = 0;
                                    int.TryParse(strValue[i].ToString(), out iTmp);
                                    this.f_1001_MessageTyp = iTmp;
                                    break;
                            }
                            break;
                            //case 1:
                            //    switch (myAsnArt)
                            //    {
                            //        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                            //            C002.f_1001_MessageTyp = int.Parse(strValue[i].ToString());
                            //            break;
                            //    }
                            //    break;
                            //case 2:
                            //    switch (myAsnArt)
                            //    {
                            //        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                            //            f_1004_DesAdvNo = strValue[i].ToString();
                            //            break;
                            //    }
                            //    break;
                    }
                }

            }
        }

    }
}
