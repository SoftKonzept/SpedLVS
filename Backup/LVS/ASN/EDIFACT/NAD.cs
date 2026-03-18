using LVS.ASN.EDIFACT;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class NAD
    {

        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "NAD";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;



        public const string const_PartyQualifier_3035_BY_Buyer = "BY";
        public const string const_PartyQualifier_3035_CA_Carrier = "CA";
        public const string const_PartyQualifier_3035_CN_Consignee = "CN"; // Empfänger
        public const string const_PartyQualifier_3035_CZ_Consignor = "CZ";
        public const string const_PartyQualifier_3035_DP_Delivery = "DP";
        public const string const_PartyQualifier_3035_FW_ = "FW";
        public const string const_PartyQualifier_3035_GM_InventoryController = "GM";
        public const string const_PartyQualifier_3035_MS_ = "MS";
        public const string const_PartyQualifier_3035_MW_ = "MW";
        public const string const_PartyQualifier_3035_SE_Seller = "SE";
        public const string const_PartyQualifier_3035_SF_ShipFrom = "SF";
        public const string const_PartyQualifier_3035_ST_ShipTo = "ST";
        public const string const_PartyQualifier_3035_WH_Warehouse = "WH";


        /// <summary>
        ///             Sendungsnummer, vergeben vom Lieferanten (alt: SLB)
        /// </summary>
        /// <param name="myASNArt"></param>
        public NAD(clsEdiVDACreate myEdiCreate)
        {
            this.EDICreate = myEdiCreate;
            this.Segment = myEdiCreate.ediSegment;

            try
            {
                List<clsEdiSegmentElement> List_SE = this.Segment.ListEdiSegmentElement.Where(x => x.EdiSegmentId == this.Segment.ID).ToList();
                if (List_SE.Count > 0)
                {

                    foreach (clsEdiSegmentElement itm in List_SE)
                    {
                        SelectedSegmentElement = itm;
                        switch (itm.Name)
                        {
                            case NAD.Kennung_3035:
                                se3035 = new clsEdiSegmentElement();
                                se3035 = itm;
                                if (this.ADR is null)
                                {
                                    SetAdrValue();
                                }
                                break;
                            case NAD.Kennung_3164:
                                se3164 = new clsEdiSegmentElement();
                                se3164 = itm;
                                break;

                            case NAD.Kennung_3207:
                                se3207 = new clsEdiSegmentElement();
                                se3207 = itm;
                                break;

                            case NAD.Kennung_3251:
                                se3251 = new clsEdiSegmentElement();
                                se3251 = itm;
                                break;

                            case NAD_C082.Kennung:
                                C082 = new NAD_C082(this);
                                break;
                            case NAD_C058.Kennung:
                                C058 = new NAD_C058(this);
                                break;
                            case NAD_C080.Kennung:
                                C080 = new NAD_C080(this);
                                break;
                            case NAD_C059.Kennung:
                                C059 = new NAD_C059(this);
                                break;
                            case NAD_C819.Kennung:
                                C819 = new NAD_C819(this);
                                break;
                        }
                        SelectedSegmentElement = null;

                    }

                    CreateValue();

                }
            }
            catch (Exception e)
            {
                string strVal = string.Empty;
                strVal += string.Format("Name: {0}", NAD.Name) + Environment.NewLine;
                strVal += string.Format(NAD.Name + "." + NAD.Kennung_3035 + ": {0}", ValOrNullSegElementField.Execute(this.f_3035)) + Environment.NewLine;
                //strVal += string.Format(NAD_C082.Kennung +": {0}", ValOrNullSegElementField.Execute(this.C082.Value)) + Environment.NewLine;
                //strVal += string.Format(NAD_C058.Kennung + ": {0}", ValOrNullSegElementField.Execute(this.C058.Value)) + Environment.NewLine;
                //strVal += string.Format(NAD_C080.Kennung + ": {0}", ValOrNullSegElementField.Execute(this.C080.Value)) + Environment.NewLine;
                //strVal += string.Format(NAD_C059.Kennung + ": {0}", ValOrNullSegElementField.Execute(this.C059.Value)) + Environment.NewLine;
                //strVal += string.Format(NAD.Name + "." + NAD.Kennung_3164 + ": {0}", ValOrNullSegElementField.Execute(this.f_3164)) + Environment.NewLine;
                //strVal += string.Format(NAD_C819.Kennung + ": {0}", ValOrNullSegElementField.Execute(this.C819.Value)) + Environment.NewLine;
                //strVal += string.Format(NAD.Name + "." + NAD.Kennung_3251 + ": {0}", ValOrNullSegElementField.Execute(this.f_3251)) + Environment.NewLine;
                //strVal += string.Format(NAD.Name + "." + NAD.Kennung_3207 + ": {0}", ValOrNullSegElementField.Execute(this.f_3207)) + Environment.NewLine;

                string strError = e.Message.ToString();
                strError += Environment.NewLine + e.StackTrace.ToString();
                strError += Environment.NewLine + strVal;

                clsLogbuchCon tmpLog = new clsLogbuchCon();
                tmpLog.GL_User = this.EDICreate.GL_User;
                tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                tmpLog.LogText = this.EDICreate.Prozess + ".[VDA4987] - " + strError;
                tmpLog.TableName = "Queue";
                //decimal decTmp = 0;
                tmpLog.TableID = this.EDICreate.ASN.Queue.ID;
                this.EDICreate.ListErrorVDA.Add(tmpLog);
            }
        }


        internal NAD_C082 C082;
        internal NAD_C058 C058;
        internal NAD_C080 C080;
        internal NAD_C059 C059;
        internal NAD_C819 C819;
        internal clsADR ADR;

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        ///             Beteiligter, Qualifier 
        ///             BY Buyer
        ///             CA Frachtführer
        ///             MS Dokumenten-/Nachrichtenaussteller bzw. - absender
        ///             SE Verkäufer
        ///             SF Ship from -Warenversender
        ///             ST Ship to - Name und Anschrift des Warenempfängers
        ///             UD Ultimate customer
        /// </summary>
        internal const string Kennung_3035 = "3035";
        private string _f_3035;
        public string f_3035
        {
            get
            {
                _f_3035 = string.Empty;
                if ((this.sef3035 is clsEdiSegmentElementField) && (this.sef3035.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef3035, string.Empty, string.Empty);
                    _f_3035 = v.ReturnValue;
                }
                return _f_3035;
            }
            set
            {
                _f_3035 = value;
            }
        }

        public clsEdiSegmentElement se3035;
        public clsEdiSegmentElementField sef3035
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se3035.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se3035.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == NAD.Kennung_3035);
                }
                return tmpSef;
            }
        }


        /// <summary>
        ///            Ort Name des Ortes / der Stadt dieser Adresse.                   
        /// </summary>

        internal const string Kennung_3164 = "3164";
        private string _f_3164 = string.Empty;
        public string f_3164
        {
            get
            {
                _f_3164 = string.Empty;
                if ((this.sef3164 is clsEdiSegmentElementField) && (this.sef3164.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef3164, string.Empty, string.Empty);
                    _f_3164 = v.ReturnValue;
                    if (!_f_3164.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
                    {
                        if ((this.ADR is clsADR) && (this.ADR.ID > 0))
                        {
                            string strTmp = string.Empty;
                            strTmp += UNOC_Zeichensatz.Execute(this.ADR.Ort);
                            _f_3164 = ediHelper_FormatString.CutValToLenth(strTmp, this.sef3164.Length);
                        }
                    }
                }
                return _f_3164;
            }
            set
            {
                _f_3035 = value;
            }
        }
        public clsEdiSegmentElement se3164;
        public clsEdiSegmentElementField sef3164
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se3164.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se3164.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == NAD.Kennung_3164);
                }
                return tmpSef;
            }
        }



        /// <summary>
        ///            Postleitzahl, Code 
        ///            Ein Identifier für ein oder mehrere Eigenschaften der Adressdaten entsprechend des im Land verwendeten Postsystems.
        /// </summary>

        internal const string Kennung_3251 = "3251";

        public clsEdiSegmentElement se3251;
        public clsEdiSegmentElementField sef3251
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se3251.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se3251.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == NAD.Kennung_3251);
                }
                return tmpSef;
            }
        }

        private string _f_3251 = string.Empty;
        public string f_3251
        {
            get
            {
                _f_3251 = string.Empty;
                if ((this.sef3251 is clsEdiSegmentElementField) && (this.sef3251.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef3251, string.Empty, string.Empty);
                    _f_3251 = v.ReturnValue;
                    if (!_f_3251.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
                    {
                        if ((this.ADR is clsADR) && (this.ADR.ID > 0))
                        {
                            _f_3251 = this.ADR.PLZ;
                        }
                    }
                }
                return _f_3251;
            }
            set
            {
                _f_3251 = value;
            }
        }

        /// <summary>
        ///           Ländername, Code Land codiert nach ISO 3166-1
        /// </summary>
        internal const string Kennung_3207 = "3207";

        public clsEdiSegmentElement se3207;
        public clsEdiSegmentElementField sef3207
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.se3207.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.se3207.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == NAD.Kennung_3207);
                }
                return tmpSef;
            }
        }



        private string _f_3207 = string.Empty;
        public string f_3207
        {
            get
            {
                _f_3207 = string.Empty;
                if ((this.sef3207 is clsEdiSegmentElementField) && (this.sef3207.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.EDICreate, this.sef3207, string.Empty, string.Empty);
                    _f_3207 = v.ReturnValue;
                    if (!_f_3207.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
                    {
                        if ((this.ADR is clsADR) && (this.ADR.ID > 0))
                        {
                            _f_3207 = LKZ.Execute(this.ADR);
                        }
                    }
                }
                return _f_3207;
            }
            set
            {
                _f_3207 = value;
            }
        }
        /// <summary>
        ///           BY Buyer = Empfänger 
        ///           CA Frachtführer
        ///           FW Spediteur
        ///           MS Dokumenten-/Nachrichtenaussteller bzw. - absender
        ///           SE Verkäufer
        ///           SF Ship from -Warenversender
        ///           ST Ship to - Name und Anschrift des Warenempfängers
        ///           UD Ultimate customer
        /// </summary>
        private void SetAdrValue()
        {
            switch (this.f_3035.ToUpper())
            {
                case const_PartyQualifier_3035_BY_Buyer:
                case const_PartyQualifier_3035_ST_ShipTo:
                case const_PartyQualifier_3035_DP_Delivery:
                case "UD":
                    switch (this.EDICreate.ASN.Queue.ASNTyp.Typ)
                    {
                        case clsASNTyp.const_string_ASNTyp_EML:
                        case clsASNTyp.const_string_ASNTyp_EME:
                        case clsASNTyp.const_string_ASNTyp_BML:
                        case clsASNTyp.const_string_ASNTyp_BME:
                        case clsASNTyp.const_string_ASNTyp_UBE:
                        case clsASNTyp.const_string_ASNTyp_UBL:
                        case clsASNTyp.const_string_ASNTyp_TSE:
                        case clsASNTyp.const_string_ASNTyp_TSL:
                            if (this.EDICreate.Lager.Eingang is clsLEingang)
                            {
                                ADR = this.EDICreate.Lager.Artikel.Eingang.AdrEmpfaenger;
                            }
                            break;

                        case clsASNTyp.const_string_ASNTyp_AML:
                        case clsASNTyp.const_string_ASNTyp_AME:
                        case clsASNTyp.const_string_ASNTyp_AVL:
                        case clsASNTyp.const_string_ASNTyp_AVE:
                        case clsASNTyp.const_string_ASNTyp_RLL:
                        case clsASNTyp.const_string_ASNTyp_RLE:
                            if (this.EDICreate.Lager.Ausgang is clsLAusgang)
                            {
                                ADR = this.EDICreate.Lager.Artikel.Ausgang.AdrEmpfaenger;
                            }
                            break;
                    }
                    break;

                case const_PartyQualifier_3035_SE_Seller:
                case const_PartyQualifier_3035_GM_InventoryController:
                    switch (this.EDICreate.ASN.Queue.ASNTyp.Typ)
                    {
                        case clsASNTyp.const_string_ASNTyp_EML:
                        case clsASNTyp.const_string_ASNTyp_EME:
                        case clsASNTyp.const_string_ASNTyp_BML:
                        case clsASNTyp.const_string_ASNTyp_BME:
                        case clsASNTyp.const_string_ASNTyp_UBE:
                        case clsASNTyp.const_string_ASNTyp_UBL:
                        case clsASNTyp.const_string_ASNTyp_TSE:
                        case clsASNTyp.const_string_ASNTyp_TSL:
                            if (this.EDICreate.Lager.Eingang is clsLEingang)
                            {
                                ADR = this.EDICreate.Lager.Artikel.Eingang.AdrAuftraggeber;
                            }
                            break;

                        case clsASNTyp.const_string_ASNTyp_AML:
                        case clsASNTyp.const_string_ASNTyp_AME:
                        case clsASNTyp.const_string_ASNTyp_AVL:
                        case clsASNTyp.const_string_ASNTyp_AVE:
                        case clsASNTyp.const_string_ASNTyp_RLL:
                        case clsASNTyp.const_string_ASNTyp_RLE:
                            if (this.EDICreate.Lager.Ausgang is clsLAusgang)
                            {
                                ADR = this.EDICreate.Lager.Artikel.Ausgang.AdrAuftraggeber;
                            }
                            break;
                    }
                    break;

                case const_PartyQualifier_3035_WH_Warehouse:
                case const_PartyQualifier_3035_CN_Consignee:
                case const_PartyQualifier_3035_SF_ShipFrom:
                case const_PartyQualifier_3035_FW_:
                case const_PartyQualifier_3035_MW_:
                    ADR = this.EDICreate.Sys.Client.ADR;
                    break;

                case const_PartyQualifier_3035_CA_Carrier:
                    //ADR = this.EDICreate.Lager.Artikel.Ausgang.;
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += NAD.Name;
            if (!f_3035.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_3035;
            }
            if (!this.C082.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C082.Value;
            }
            if (!this.C058.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C058.Value;
            }
            if (!this.C080.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C080.Value;
            }
            if (!this.C059.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C059.Value;
            }
            if (!f_3164.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_3164;
            }
            if (!this.C819.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C819.Value;
            }

            if (!f_3251.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_3251;
            }
            if (!f_3207.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.f_3207;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }


        //===================================================================================================================

        ///// <summary>
        /////             PARTY QUALIFIER 
        ///// </summary>
        public string f_3035_PartyQualifier { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public NAD(string myEdiValueString, string myAsnArt)
        {
            if (myEdiValueString != null)
            {
                if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
                {
                    List<string> strValue = myEdiValueString.Split(new char[] { ':', '+' }).ToList();
                    for (int i = 0; i < strValue.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                break;
                            case 1:
                                this.f_3035_PartyQualifier = strValue[i].ToString();
                                break;
                            case 2:
                                C082 = new NAD_C082(strValue[i]);
                                break;
                            case 3:
                                C080 = new NAD_C080(strValue[i]);
                                break;
                        }
                    }

                }
            }
        }
    }
}
