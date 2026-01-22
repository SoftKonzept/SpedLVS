using LVS.ASN.ASNFormatFunctions;
using LVS.Communicator.EdiVDA.EdiVDAValues;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace LVS
{
    public class clsEdiVDAAssignValueAlias
    {
        internal Globals._GL_USER GL_User;

        internal clsVDACreate VDACreate;
        internal clsEdiVDACreate EDICreate;
        internal clsLagerdaten Lager;
        internal clsASN ASN;


        internal string strValueArt;
        internal clsVDAClientValue tmpCV;
        internal clsEdiSegmentElementField myClFeld;
        internal string strFeldSub;
        internal string StringFillValue;
        internal clsASNTyp asnTyp;
        internal string strTmp;
        internal bool bFillLeft;

        internal clsStringCheck StrCheck = new clsStringCheck();
        internal clsVDAClientWorkspaceValue VDAClientWorkspaceValue;

        private string _SIDOld;
        public string SIDOld
        {
            get
            {
                string strTmp = string.Empty;
                if (this.EDICreate.Orga != null)
                {
                    strTmp = this.EDICreate.Orga.SendNrOld.ToString();
                }
                _SIDOld = strTmp;
                return _SIDOld;
            }
            set { _SIDOld = value; }
        }
        private string _SIDNew;
        public string SIDNew
        {
            get
            {
                string strTmp = string.Empty;
                if (this.EDICreate.Orga != null)
                {
                    strTmp = this.EDICreate.Orga.SendNrNew.ToString();
                }
                _SIDNew = strTmp;
                return _SIDNew;
            }
            set { _SIDNew = value; }
        }
        public string ReturnValue { get; set; }

        /// <summary>
        ///             Konstruktor für EDIFACTVDA47987
        /// </summary>
        /// <param name="myValueArt"></param>
        /// <param name="myClientValue"></param>
        /// <param name="mySegmentElementField"></param>
        /// <param name="myFeldSub"></param>
        /// <param name="myStringFillValue"></param>
        /// <param name="myAsnTyp"></param>
        /// <param name="myStrTmp"></param>
        /// <param name="myFillLeft"></param>
        public clsEdiVDAAssignValueAlias(string myValueArt,
                                            clsEdiVDACreate myEdiVDACreate,
                                            //clsVDAClientValue myClientValue,
                                            clsEdiSegmentElementField mySegmentElementField,
                                            string myFeldSub,
                                            //string myStringFillValue,
                                            //clsASNTyp myAsnTyp,
                                            string myStrTmp)
        //bool myFillLeft)
        {
            try
            {
                ReturnValue = string.Empty;

                this.strValueArt = myValueArt;
                //tmpCV = myClientValue;
                myClFeld = mySegmentElementField;
                strFeldSub = myFeldSub;
                //StringFillValue = myStringFillValue;
                //asnTyp= myAsnTyp;
                strTmp = myStrTmp;
                //bFillLeft = myFillLeft;


                this.EDICreate = myEdiVDACreate;
                this.GL_User = this.EDICreate.GL_User;
                this.Lager = this.EDICreate.Lager;
                this.ASN = this.EDICreate.ASN;
                this.asnTyp = this.EDICreate.ASN.ASNTyp;
                this.tmpCV = this.EDICreate.VDAClientVal;

                StringFillValue = this.tmpCV.FillValue;
                bFillLeft = this.tmpCV.FillLeft;

                //-- Ermitteln ob für dieses Feld "myCLFeld" eine Variable zugewiesen wurde
                if (this.EDICreate.VDAClientVal.listVDAClientValueSatz.Count > 0)
                {
                    this.tmpCV = null;
                    this.tmpCV = this.EDICreate.VDAClientVal.listVDAClientValueSatz.FirstOrDefault(x => x.ASNFieldID == myClFeld.ID);
                    if (this.tmpCV is clsVDAClientValue)
                    {
                        StringFillValue = this.tmpCV.FillValue;
                        bFillLeft = this.tmpCV.FillLeft;
                        strValueArt = this.tmpCV.ValueArt;
                        GetValue();
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
            }
        }
        /// <summary>
        ///                         dürfte aktuell nicht aktiv sein
        /// </summary>
        public void GetValue()
        {
            string strTmp = string.Empty;
            ReturnValue = string.Empty;
            switch (strValueArt)
            {
                //case "const":
                case clsEdiVDAValueAlias.const_VDA_Value_const:
                    if (!tmpCV.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
                    {
                        if (tmpCV.Value.Length == myClFeld.Length)
                        {
                            strFeldSub = tmpCV.Value;
                        }
                        else
                        {
                            if (tmpCV.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
                            {
                                strFeldSub = tmpCV.Value;
                            }
                            else
                            {
                                //Length ist kürzer
                                string strFeld = string.Empty;
                                if (tmpCV.Value.Length < myClFeld.Length)
                                {
                                    strFeld = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, tmpCV.Value, myClFeld.Length, bFillLeft);
                                }
                                else
                                {
                                    //length ist größer ==> kürzen
                                    //string strTmp = string.Empty;
                                    //strTmp = tmpCV.Value.Substring(0, myClFeld.Length);
                                    strFeld = ediHelper_FormatString.CutValToLenth(tmpCV.Value, myClFeld.Length);
                                }
                                strFeldSub = strFeld;
                            }
                        }
                    }
                    else
                    {
                        strFeldSub = tmpCV.Value;
                    }
                    break;

                case clsEdiVDAValueAlias.const_VDA_Value_Empty:
                    strFeldSub = string.Empty;
                    break;

                case clsEdiVDAValueAlias.const_VDA_Value_NotUsed:
                    strFeldSub = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;

                case clsEdiVDAValueAlias.const_VDA_Value_TimeNow:
                case clsEdiVDAValueAlias.const_VDA_Value_NOWTIME:
                case clsEdiVDAValueAlias.const_VDA_Value_NOW:
                    strFeldSub = Datetime_NOW.Execute();
                    break;

                case clsEdiVDAValueAlias.const_Lieferantennummer:
                    strTmp = string.Empty;
                    strTmp = SupplierNo.Execute(this.GL_User, this.Lager, this.ASN); //GetLieferantenNummer(ref this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_SupplierNo:
                    strTmp = string.Empty;
                    strTmp = SupplierNo.Execute(this.GL_User, this.Lager, this.ASN);  //GetSupplierNo(ref this.Lager);                    
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    //if (this.EDICreate.ASN.ASNArt.Typ.Equals(clsASNArt.const_Art_EdifactVDA4987))
                    //{
                    //    strFeldSub=ediHelper_FillValueToLength.FillValueToLength(strFeldSub, "0", 10, true);
                    //}
                    break;

                case clsEdiVDAValueAlias.const_DUNSNrSF:
                case clsEdiVDAValueAlias.const_DUNSNrST:
                case clsEdiVDAValueAlias.const_DUNSNrSE:
                case clsEdiVDAValueAlias.const_DUNSNrFW:
                case clsEdiVDAValueAlias.const_DUNSNrMS:
                    strTmp = string.Empty;
                    strTmp = DUNSNr.Execute(this.Lager, strValueArt);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                //case clsEdiVDAValueAlias.const_Reciever:
                //    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, EMPFAENGER, myClFeld.Length, bFillLeft);
                //    break;
                //case clsEdiVDAValueAlias.const_RecieverNo:
                //    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, ReceiverNo, myClFeld.Length, bFillLeft);
                //    break;

                case clsEdiVDAValueAlias.const_Sender:
                    strTmp = string.Empty;
                    strTmp = Sender.Execute(this.GL_User, this.ASN, asnTyp, this.Lager);  //GetSenderID(ref this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_SIDOld:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, SIDOld, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_SIDNew:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, SIDNew, myClFeld.Length, bFillLeft);
                    break;


                case clsEdiVDAValueAlias.const_SystemId_Queue:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.ASN.Queue.ID.ToString(), myClFeld.Length, bFillLeft);
                    break;

                //case clsEdiVDAValueAlias.const_VDA_Value_NOW:
                //    string strYear = String.Format("{0:yy}", DateTime.Now);
                //    string strMonth = String.Format("{0:MM}", DateTime.Now);
                //    string strDay = String.Format("{0:dd}", DateTime.Now);
                //    strFeldSub = strYear + strMonth + strDay;
                //    break;
                //case clsEdiVDAValueAlias.const_VDA_Value_TimeNow:
                //    string strHour = String.Format("{0:HH}", DateTime.Now);
                //    string strMinute = String.Format("{0:mm}", DateTime.Now);
                //    strFeldSub = strHour + strMinute;
                //    break;

                case clsEdiVDAValueAlias.const_VDA_Value_Blanks:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, tmpCV.Value, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_TransportMittelSchlüssel:
                    strFeldSub = TMS.Execute(asnTyp, this.Lager);
                    break;
                case clsEdiVDAValueAlias.const_TransportMittelNummer:
                    strTmp = string.Empty;
                    strTmp = TMN.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_TransportMittelNummerTrim:
                    strTmp = string.Empty;
                    strTmp = TMN.Execute(asnTyp, this.Lager);
                    strTmp = Regex.Replace(strTmp, " ", "");
                    strTmp = Regex.Replace(strTmp, "-", "");
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_Vorgang:
                    strTmp = string.Empty;
                    strTmp = LVS.Communicator.EdiVDA.EdiVDAValues.VGS.Execute(asnTyp);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;


                //********************************************************************** LEingangdaten
                case clsEdiVDAValueAlias.const_Eingang_ID:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.LEingangTableID.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Eingang_EingangID:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.LEingangID.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Eingang_Datum:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.LEingangDate.ToString("yyMMdd");
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Eingang_LfsNr:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang is clsLEingang)
                    {
                        strTmp = this.Lager.Eingang.LEingangLfsNr.ToString();
                    }
                    else
                    {
                        if (this.Lager.Artikel.Eingang is clsLEingang)
                        {
                            strTmp = this.Lager.Artikel.Eingang.LEingangLfsNr.ToString();
                        }
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_Eingang_ExTransportRef:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.ExTransportRef.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Eingang_ExAuftragRef:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.ExAuftragRef.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Eingang_Brutto:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.Brutto.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Eingang_Netto:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.Netto.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Eingang_Anzahl:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.ArtikelCount.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EFunc_LieferantennummerDeleteSlash:
                    strTmp = string.Empty;
                    strTmp = LiefNrDeleteSlash.Execute(this.Lager, this.ASN.ASNArt);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;


                //********************************************************************** LAusgangsdaten
                case clsEdiVDAValueAlias.const_Ausgang_ID:
                    strTmp = string.Empty;
                    if (this.Lager.Ausgang != null)
                    {
                        strTmp = this.Lager.Ausgang.LAusgangTableID.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Ausgang_LAusgangID:
                    strTmp = string.Empty;
                    if (this.Lager.Ausgang != null)
                    {
                        strTmp = this.Lager.Ausgang.LAusgangID.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Ausgang_SLB:
                    strTmp = string.Empty;
                    if (this.Lager.Ausgang != null)
                    {
                        strTmp = this.Lager.Ausgang.SLB.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                //***************************************************************************************** EA


                case clsEdiVDAValueAlias.const_EA_ArtPosEoA:
                    strTmp = string.Empty;
                    strTmp = EA_ArtPosEoA.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EA_ID:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = this.Lager.Eingang.LEingangTableID.ToString();
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = this.Lager.Ausgang.LAusgangTableID.ToString();
                            break;

                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_EingangId:
                    strTmp = string.Empty;
                    strTmp = EA_EingangId.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_AusgangId:
                    strTmp = string.Empty;
                    strTmp = EA_AusgangId.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_EANr:
                    strTmp = string.Empty;
                    strTmp = EA_No.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EAFunc_EANrWithPrefix:
                    strTmp = string.Empty;
                    strTmp = EA_NoWithPrefix.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EA_Datum:
                    strTmp = string.Empty;
                    strFeldSub = EA_Datum.Execute(asnTyp, this.Lager, this.ASN.ASNArt);
                    //strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EA_KFZ:
                    strTmp = string.Empty;
                    strFeldSub = EA_KFZ.Execute(asnTyp, this.Lager, this.ASN.ASNArt);
                    //strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EA_LfsNr:
                    strTmp = string.Empty;
                    strTmp = EA_LfsNr.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_ExTransportRef:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = this.Lager.Eingang.ExTransportRef;
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = this.Lager.Ausgang.exTransportRef;
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_ExAuftragRef:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = this.Lager.Eingang.ExAuftragRef;
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = string.Empty;
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_Brutto:
                    strTmp = string.Empty;
                    strTmp = EA_Brutto.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_Netto:
                    strTmp = string.Empty;
                    strTmp = EA_Netto.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_Anzahl:
                    strTmp = string.Empty;
                    strTmp = EA_Anzahl.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EAFunc_EAAnzahlX1000:
                    strTmp = string.Empty;
                    strTmp = EA_AnzahlX1000.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_ArtikelCount:
                    strTmp = string.Empty;
                    strTmp = EA_ArtikelCount.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_Termin:
                    strTmp = string.Empty;
                    strTmp = EA_Anzahl.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EA_SLB:
                    strTmp = string.Empty;
                    strTmp = EA_SLB.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EA_MATDate:
                    strTmp = string.Empty;
                    strTmp = EA_MATDate.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EA_MATTime:
                    strTmp = string.Empty;
                    strTmp = EA_MATTime.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                //case clsEdiVDAValueAlias.const_EA_ArtPosEoA:
                //    strTmp = string.Empty;
                //    strTmp =

                //    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                //    break;

                //****************************************************************************************** Artikeldaten
                case clsEdiVDAValueAlias.const_Artikel_ID:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.ID.ToString(), myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_LVSNr:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.LVS_ID.ToString(), myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_LVSNrBeforeUB:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.LVSNrBeforeUB.ToString(), myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Netto:
                    //strTmp = GetNetto(this.Lager.Artikel);
                    strTmp = Artikel_Netto.ExecuteXFactor(this.Lager.Artikel, 1000);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Brutto:
                    //strTmp = GetBrutto(this.Lager.Artikel);
                    strTmp = Artikel_Brutto.ExecuteXFactor(this.Lager.Artikel, 1000);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Gut:
                    strTmp = Artikel_Gueterart.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Dicke:
                    strTmp = Functions.FormatDecimalNoDiggits(this.Lager.Artikel.Dicke);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Breite:
                    strTmp = Functions.FormatDecimalNoDiggits(this.Lager.Artikel.Breite);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Laenge:
                    strTmp = Functions.FormatDecimalNoDiggits(this.Lager.Artikel.Laenge);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Anzahl:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Anzahl.ToString(), myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Einheit:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Einheit.ToUpper(), myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Werksnummer:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Werksnummer, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Produktionsnummer:
                    //check Produktionsnummer
                    strTmp = string.Empty;
                    strTmp = this.Lager.Artikel.Produktionsnummer;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            //strTmp = this.Lager.Artikel.Produktionsnummer;
                            this.Lager.Artikel.UpdateASNProduktionsnummer();
                            break;
                        default:
                            //strTmp = this.Lager.Artikel.Produktionsnummer;
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Charge:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Charge, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_BestellNr:
                case "Artikel.BestellNr":
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Bestellnummer, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_exAuftrag:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.exAuftrag, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_exAuftragPos:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.exAuftragPos, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_exBezeichnung:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.exBezeichnung, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_exMaterialNr:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.exMaterialnummer, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Pos:
                case "Artikel.Pos":
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Position, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_ArtIDRef:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.ArtIDRef, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_cFunction_VW_SchadenTopOne:
                    strTmp = string.Empty;
                    strTmp = VW_SchadenText.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_AbrufRef:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.AbrufReferenz, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_TARef:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.TARef, myClFeld.Length, bFillLeft);
                    break;
                //****************************************************************************************** Functions Artikeldaten
                case clsEdiVDAValueAlias.const_ArtFunc_WerksnummerOhneBlank:
                    string strWNrOhneBlank = WerksnummerOhneBlank.Execute(this.Lager); //WerksnummerOhneBlank(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strWNrOhneBlank, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_WerksnummerMitFuehrendemBlank:
                    string strWNrMitFBlank = WerksnummerMitFBlank.Execute(this.Lager); // WerksnummerMitBlank(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strWNrMitFBlank, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunk_WerksnummerFormatVW:
                    string strWNrVWFormat = VW_WerksnummerFormat.Execute(this.Lager.Artikel); // WerksnummerVWFormat(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strWNrVWFormat, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunk_WerksnummerWihtHyphen:
                    string strWNrWithHypen = WerksnummerWithHyphen.Execute(this.Lager.Artikel.Werksnummer);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strWNrWithHypen, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunk_WerksnummerWihtOutHyphen:
                    string strWNrWithOutHypen = WerksnummerWithOutHyphen.Execute(this.Lager.Artikel.Werksnummer);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strWNrWithOutHypen, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_cFunction_VW_BruttoKGorSt:
                    //string strBruttoKGorSt = GetBruttoOrAnzahl(this.Lager.Artikel);
                    string strBruttoKGorSt = VW_BruttoKGorST.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strBruttoKGorSt, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_cFunction_VW_QTY_BruttoOrStk:
                    strTmp = VW_QTY_BruttoOrStk.Execute(this.Lager.Artikel);
                    strFeldSub = ediHelper_FormatString.FillValueToLength(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_cFunction_VW_SupplierNo:
                    strTmp = VW_SupplierNo.Execute(this.GL_User, this.Lager, this.ASN);
                    strFeldSub = ediHelper_FormatString.FillValueToLength(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_VW_NettoKGorSt:
                    //string strNettoKGorSt = GetNettoOrAnzahl(this.Lager.Artikel);
                    string strNettoKGorSt = VW_NettoKGorST.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strNettoKGorSt, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_cFunction_VW_EinheitKGorSt:
                    //string EinheitKGorSt = GetEinheitKGorST(this.Lager.Artikel);
                    string EinheitKGorSt = VW_EinheitKGorST.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, EinheitKGorSt, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_cFunction_VW_QTY_EinheitPCIorKGM:
                    strTmp = VW_QTY_EinheitPCIorKGM.Execute(this.Lager.Artikel);
                    strFeldSub = ediHelper_FormatString.FillValueToLength(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_cFunction_VW_TMN:
                    strTmp = VW_TMN.Execute(this.asnTyp.Typ, ref this.Lager);
                    strFeldSub = ediHelper_FormatString.FillValueToLength(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_ArtFunc_BruttoTO:
                    strTmp = string.Empty;
                    strTmp = Artikel_BruttoTO.Execute(this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_NettoTO:
                    strTmp = string.Empty;
                    strTmp = Artikel_NettoTO.Execute(this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                //******************************************************************************************* spezielle Client Function


                case clsEdiVDAValueAlias.const_cFunction_SZG_TMN:
                    strTmp = string.Empty;
                    //strTmp = szgFunc.GetTMN(asnTyp.Typ, ref this.Lager);
                    strTmp = SZG_TMN.Execute(asnTyp.Typ, ref this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_SZG_LVSForVW:
                    strTmp = string.Empty;
                    //strTmp = szgFunc.GetLVSIdForVW(ref this.Lager);
                    strTmp = VW_LVSForVW.Execute(this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_SLE_VGS:
                //strTmp = string.Empty;
                //strTmp = sleFunc.GetVGS(asnTyp.Typ);
                //strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                //break;

                case clsEdiVDAValueAlias.const_cFunction_Tata_713F03LfsNr:
                    strTmp = string.Empty;
                    strTmp = Tata_713F03LfsNr.Execute(asnTyp, this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_VOEST_EA_SLB:
                    strTmp = string.Empty;
                    strTmp = VOEST_EA_SLB.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_SLBWithPrefix:
                    strTmp = string.Empty;
                    strTmp = SLBWithPrefix.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_Arcelor_EABmwFormat:
                    strTmp = string.Empty;
                    strTmp = Arcelor_EA_BMWFormat.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_SLB:
                    strTmp = string.Empty;
                    strTmp = BMW_SLB.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_VGS:
                    strTmp = string.Empty;
                    strTmp = BMW_VGS.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_EANo:
                    strTmp = string.Empty;
                    strTmp = BMW_EANo.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_EANoWithPrefix:
                    strTmp = string.Empty;
                    strTmp = BMW_EANoWithPrefix.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_PACNumberBolzen:
                    strTmp = string.Empty;
                    strTmp = BMW_PACNumberBolzen.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_MATDate:
                    strTmp = string.Empty;
                    strTmp = BMW_MATDate.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_MATTime:
                    strTmp = string.Empty;
                    strTmp = BMW_MATTime.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_Schaden:
                    strTmp = string.Empty;
                    strTmp = BMW_Schaden.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;


                case clsEdiVDAValueAlias.const_cFunction_BMW_EDIFACTRecipientId:
                    strTmp = string.Empty;
                    strTmp = BMW_UNB_S003_0010.const_BMW_RecipientIdentification;
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_EDIFACTSenderId:
                    strTmp = string.Empty;
                    strTmp = BMW_UNB_S002_0004.const_BMW_SenderIdentification;
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_Einheit:
                    strTmp = string.Empty;
                    strTmp = BMW_Einheit.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_713F17_KGorSTK:
                    strTmp = string.Empty;
                    strTmp = BMW_713F17KGorSTK.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_714F06Brutto:
                    strTmp = string.Empty;
                    strTmp = BMW_714F06Brutto.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;


                case clsEdiVDAValueAlias.const_cFunction_BMW_714F08Netto:
                    strTmp = string.Empty;
                    strTmp = BMW_714F08Netto.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_714F09Einheit:
                    strTmp = string.Empty;
                    strTmp = BMW_714F09Einheit.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_GlowDate:
                    strTmp = string.Empty;
                    strTmp = BMW_GlowDate.Execute(this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_BGM_C002_1000:
                    strTmp = string.Empty;
                    strTmp = BMW_BGM_C002_1000.Execute(this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_BGM_C106_1004_EANr:
                    strTmp = string.Empty;
                    strTmp = BMW_BGM_C106_1004_EANr.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;



                case clsEdiVDAValueAlias.const_cFunction_BMW_RFF_ACD_C506_1154:
                    strTmp = string.Empty;
                    strTmp = BMW_RFF_ACD_C506_1154.Execute(this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_MEA_C174_6314_ArticleNettoTO:
                    strTmp = string.Empty;
                    strTmp = BMW_MEA_C174_6314_ArticleNettoTO.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_MEA_C174_6314_KGorStkBruttoTO:
                    strTmp = string.Empty;
                    strTmp = BMW_MEA_C174_6314_KGorStkBruttoTO.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdifactINVRPT_INV_4501_4501_MessageType:
                    strTmp = string.Empty;
                    strTmp = EdifactINVRPT_INV_4501_4501_MessageType.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdifactINVRPT_INV_7491_7491_TypeMovement:
                    strTmp = string.Empty;
                    strTmp = EdifactINVRPT_INV_7491_7491_TypeMovement.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdifactINVRPT_INV_4499_4499_MovementReason:
                    strTmp = string.Empty;
                    strTmp = EdifactINVRPT_INV_4499_4499_MovementReason.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdifactINVRPT_INV_4503_4503_BalanceMethod:
                    strTmp = string.Empty;
                    strTmp = EdifactINVRPT_INV_4503_4503_BalanceMethod.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_BY_Buyer:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_BY_Buyer.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_CN_Consignee:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_CN_Consignee.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_DP_DeliveryPart:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_DP_DeliveryPart.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_GM_InventoryController:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_GM_InventoryController.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_ST_Recipient:
                case clsEdiVDAValueAlias.const_cFunction_EdiAdrWorkspaceAssign_NAD_C082_3039_ST_Recipient:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_ST_RecipienNo.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_SU_Supplier:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_SU_SupplierNo.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_SE_Seller:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_SE_Seller.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_WH_Warehouse:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_WH_Warehouse.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_UNB_S002_0004_Sender:
                case clsEdiVDAValueAlias.const_cFunction_EdiAdrWorkspaceAssign_UNB_S002_0004_Sender:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_UNB_S002_0004_Sender.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_UNB_S003_0010_Client:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_UNB_S003_0010_Client.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;                

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_UNB_S003_0010_Receiver:
                case clsEdiVDAValueAlias.const_cFunction_EdiAdrWorkspaceAssign_UNB_S003_0010_Receiver:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_UNB_S003_0010_Receiver.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_UNB_S004_0017_DateOfCreation:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_UNB_S004_0017_DateOfCreation.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_Hydro_712F03SLB:
                    strTmp = string.Empty;
                    strTmp = Hydro_712F03SLB.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_MUBEA_712F03SLB:
                    strTmp = string.Empty;
                    strTmp = MUBEA_712F03SLB.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_MENDRITZKI_Produktionsnummer:
                    strTmp = string.Empty;
                    strTmp = MENDRITZKI_Produktionsnummer.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_Novelis_F13F03_LfsNo:
                    strTmp = string.Empty;
                    strTmp = Novelis_F13F03_LfsNo.Execute(this.asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_VW_GIN_ML_C208:
                    strTmp = string.Empty;
                    if (this.Lager.Ausgang != null)
                    {
                        strTmp = VW_GIN_ML_C208.Execute(this.Lager.Ausgang.AdrAuftraggeber, this.SIDNew);
                        strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    }
                    break;

                case clsEdiVDAValueAlias.const_cFunction_VW_RFF_ON_C506:
                    strTmp = string.Empty;
                    strTmp = VW_RFF_ON_C506.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                //******************************************************************************************* allgemeine Function
                case clsEdiVDAValueAlias.const_ArtFunc_ProduktionsnummerFillTo9With0:
                    string strFillTo9With0 = string.Empty; // FillTo9With0(this.Lager.Artikel.Produktionsnummer);
                    strFillTo9With0 = ProduktionsnummerFillTo9With0.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strFillTo9With0, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_Anzahlx1000:
                    strTmp = string.Empty;
                    strTmp = (this.Lager.Artikel.Anzahl * 1000).ToString();
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_LVSNr8Stellig:
                    strTmp = string.Empty;
                    strFeldSub = ediHelper_FormatString.FillValueToLength(true, "0", this.Lager.Artikel.LVS_ID.ToString(), 8, true);
                    //strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.LVS_ID.ToString(), myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_PACBolzenQuantity:
                    strTmp = string.Empty;
                    strTmp = PACBolzenQuantity.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_PACNumber:
                    strTmp = string.Empty;
                    strTmp = PACNumber.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_GlowDateToEdi_ddMMyy:
                    strTmp = string.Empty;
                    strTmp = Format_GlowDateToEdi.Execute(this.Lager.Artikel, Format_GlowDateToEdi.const_Function_GlowDateToEdi_ddMMyy);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_GlowDateToEdi_ddMMyyyy:
                    strTmp = string.Empty;
                    strTmp = Format_GlowDateToEdi.Execute(this.Lager.Artikel, Format_GlowDateToEdi.const_Function_GlowDateToEdi_ddMMyyyy);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_GlowDateToEdi_yyyyMMdd:
                    strTmp = string.Empty;
                    strTmp = Format_GlowDateToEdi.Execute(this.Lager.Artikel, Format_GlowDateToEdi.const_Function_GlowDateToEdi_yyyyMMdd);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_GlowDateToEdi_yyyyMMddOrBlank:
                    strTmp = string.Empty;
                    strTmp = Format_GlowDateToEdi.Execute(this.Lager.Artikel, Format_GlowDateToEdi.const_Function_GlowDateToEdi_yyyyMMddOrBlank);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;


                case clsEdiVDAValueAlias.const_ArtFunc_GlowDateToEdi_yyMMdd:
                    strTmp = string.Empty;
                    strTmp = Format_GlowDateToEdi.Execute(this.Lager.Artikel, Format_GlowDateToEdi.const_Function_GlowDateToEdi_yyMMdd);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                default:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, tmpCV.Value, myClFeld.Length, bFillLeft);
                    break;
            }
            ReturnValue = strFeldSub;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bFillValue"></param>
        /// <param name="StrToFill"></param>
        /// <param name="myValue"></param>
        /// <param name="myLength"></param>
        /// <param name="myLeft"></param>
        /// <returns></returns>
        private string FillValueWithstringToLenth(bool bFillValue, string StrToFill, string myValue, Int32 myLength, bool myLeft)
        {
            string retVal = myValue;
            if (bFillValue)
            {
                string strFillValue = string.Empty;
                if (
                    (StrToFill.Equals(clsEdiVDAValueAlias.const_VDA_Value_Blanks)) || (StrToFill.Equals(""))
                   )
                {
                    strFillValue = " ";
                }
                else
                {
                    strFillValue = StrToFill;
                }
                while (retVal.Length < myLength)
                {
                    if (myLeft)
                    {
                        //0 voranstelle
                        retVal = strFillValue + retVal;
                    }
                    else
                    {
                        retVal = retVal + strFillValue;
                    }
                }
                if (retVal.Length > myLength)
                {
                    retVal = retVal.Substring(0, myLength);
                }
            }
            return retVal;
        }
        /// <summary>
        ///             Erstellt einen Detailsstring zu dem Queue Datensatz
        /// </summary>
        /// <param name="myQueue"></param>
        /// <param name="myErrorText"></param>
        /// <returns></returns>
        private string CreateQueueDetailsString(ref clsQueue myQueue, string myErrorText)
        {
            return myErrorText + Environment.NewLine +
                                           "Details:" + Environment.NewLine +
                                           "ID: [ " + myQueue.ID.ToString() + " ] " + Environment.NewLine +
                                           "Tablename: [ " + myQueue.TableName + " ] " + Environment.NewLine +
                                           "TableID: [ " + myQueue.TableID.ToString() + " ] " + Environment.NewLine +
                                           "Datum: [ " + myQueue.Datum.ToString() + " ] " + Environment.NewLine +
                                           "ASNTypID: [ " + myQueue.ASNTypID.ToString() + " - " + myQueue.ASNTyp.Typ + " ]" + Environment.NewLine +
                                           "ASNID: [ " + myQueue.ASNID.ToString() + " ]" + Environment.NewLine +
                                           "AdrVerweisID: [ " + myQueue.AdrVerweisID.ToString() + " ]" + Environment.NewLine +
                                           "ASNAction: [ " + myQueue.ASNAction.ToString() + " ] " + Environment.NewLine;
        }






    }
}
