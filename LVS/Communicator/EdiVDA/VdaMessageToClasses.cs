using Common.Helper;
using Common.Models;
using LVS.ASN.GlobalValues;
using LVS.Constants;
using LVS.Models;
using LVS.ViewData;
using LVS.Views;
using System;
using System.Collections.Generic;
using System.Data;

namespace LVS.Communicator.EdiVDA
{
    public class VdaMessageToClasses
    {
        internal clsSystem system { get; set; }
        public DateTime CreatedMessageDate { get; set; } = new DateTime(1900, 1, 1);
        private int BenutzerID { get; set; } = 1;
        public Asn asn { get; set; }
        public ctrASNRead_AsnVdaView AsnVdaView { get; set; } = new ctrASNRead_AsnVdaView();
        internal Dictionary<string, string> Dict713F10OrderID; //= new Dictionary<string, string>();
        internal Dictionary<string, ediHelper_712_TM> Dict712_Transportmittel; //= new Dictionary<string, string>();
        //public event Action<int> ProgressMaxValue;
        //internal BackgroundWorker WorkerBar;

        //public List<string> listEdiSegments_Head = new List<string>();
        //public List<string> listEdiSegments_Article = new List<string>();
        //public EdiClientWorkspaceValueViewData ediClintWorkspaceValueVD = new EdiClientWorkspaceValueViewData();
        //public EdiClientWorkspaceValue ediClientWorkspaceValue = new EdiClientWorkspaceValue();
        //public Dictionary<Eingaenge, List<string>> dictEingang = new Dictionary<Eingaenge, List<string>>();
        //public Dictionary<Articles, List<string>> dictArticle= new Dictionary<Articles, List<string>>();
        //internal Dictionary<string, clsASNArtFieldAssignment> DictASNArtFieldAssignment = new Dictionary<string, clsASNArtFieldAssignment>();
        //internal Dictionary<string, clsASNArtFieldAssignment> DictASNArtFieldAssCopyFieldValue = new Dictionary<string, clsASNArtFieldAssignment>();
        //internal clsASNArtFieldAssignment asign = new clsASNArtFieldAssignment();
        //public AddressViewData adrVD = new AddressViewData();
        //public int BenutzerId { get; set; } = 0;
        //public EingangViewData eingangViewData { get; set; }
        //public Eingaenge eingang { get; set; }
        //public Articles article {  get; set; }
        //public List<Articles> ListArticles { get; set; }

        //internal UnitViewData unitViewData { get; set; }
        //internal Dictionary<string, AddressReferences> DictAddressReferencesSender { get; set; }
        //internal Dictionary<string, AddressReferences> DictAddressReferencesReceiver { get; set; }
        public string ErrorLog { get; set; }

        public VdaMessageToClasses(LVS.clsSystem mySystem)
        {
            this.system = mySystem;
            this.ErrorLog = string.Empty;
        }
        //public VdaMessageToClasses(LVS.clsSystem mySystem, BackgroundWorker myBgWorker)
        //{
        //    this.system = mySystem;
        //    this.ErrorLog = string.Empty;
        //    this.WorkerBar = myBgWorker;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAsn"></param>
        /// <param name="dtASN"></param>
        public void CreateHeadVdaMessageValue(Asn myAsn, DataTable dtASN)
        {
            asn = myAsn;
            if (
                (asn is Asn)
                && (asn.Id > 0)
            //&& (asn.EdiMessageValue.Length>0)
            )
            {
                DataTable dtEingang = new DataTable("Eingang");
                dtEingang.Columns.Add("Select", typeof(bool));
                dtEingang.Columns.Add("ASN-Datum", typeof(DateTime));
                dtEingang.Columns.Add("Ref.Auftraggeber", typeof(string));
                dtEingang.Columns.Add("Ref.Empfaenger", typeof(string));
                dtEingang.Columns.Add("TransportNr", typeof(string));
                dtEingang.Columns.Add("VS-Datum", typeof(DateTime));
                dtEingang.Columns.Add("AuftraggeberView", typeof(string));
                dtEingang.Columns.Add("EmpfaengerView", typeof(string));
                dtEingang.Columns.Add("Transportmittel", typeof(string));
                dtEingang.Columns.Add("Lieferantennummer", typeof(string));
                dtEingang.Columns.Add("Log", typeof(string));


                EingangViewData eVD = new EingangViewData();
                eVD.Eingang.ArbeitsbereichId = myAsn.WorkspaceId;
                eVD.Eingang.MandantenId = myAsn.MandantenId;

                // Sortiere die DefaultView nach der Spalte "ID"
                //dtASN.DefaultView.Sort = "ID ASC";
                DataTable dtASNID = dtASN.DefaultView.ToTable(true, "ASNID");


                for (Int32 i = 0; i <= dtASNID.Rows.Count - 1; i++)
                {

                    DataRow row = dtEingang.NewRow();

                    string asnIDTmp = dtASNID.Rows[i]["ASNID"].ToString();
                    decimal decASNID = 0;
                    Decimal.TryParse(asnIDTmp, out decASNID);
                    //row["Select"] = false;
                    //row["ASN"] = decASNID;


                    dtASN.DefaultView.RowFilter = string.Empty;
                    dtASN.DefaultView.RowFilter = "ASNID=" + asnIDTmp;
                    DataTable dtASNValue = dtASN.DefaultView.ToTable();
                    //Table mit den XMLDaten aus der Message

                    //bool bTRAN_PART = false;
                    //bool bLS_PART = false;
                    bool bIsRead16 = false;
                    bool bIsRead46 = false;
                    string strLastLfsNr = string.Empty;

                    bool finishLoop = false;
                    for (Int32 x = 0; x <= dtASNValue.Rows.Count - 1; x++)
                    {
                        if (finishLoop)
                        {
                            break;
                        }

                        string knot = dtASNValue.Rows[x]["FieldName"].ToString();
                        string Value = dtASNValue.Rows[x]["Value"].ToString();

                        Int32 iASNField = 0;
                        Int32.TryParse(dtASNValue.Rows[x]["ASNFieldID"].ToString(), out iASNField);
                        string strKennung = dtASNValue.Rows[x]["Kennung"].ToString();
                        //string strValue = dtASNValue.Rows[i]["Value"].ToString();

                        switch (strKennung)
                        {
                            case clsASN.const_VDA4913SatzField_SATZ711F01:
                            case clsASN.const_VDA4913SatzField_SATZ711F02:
                                break;

                            //case 3:
                            case clsASN.const_VDA4913SatzField_SATZ711F03:
                                row["Ref.Empfaenger"] = Value;
                                break;

                            //DAten Empfännger
                            //case 4:
                            case clsASN.const_VDA4913SatzField_SATZ711F04:
                                row["Ref.Auftraggeber"] = Value + "#" + row["Ref.Empfaenger"];
                                row["Ref.Empfaenger"] += "#" + Value;
                                break;

                            //Übertragungsnummer alt
                            case clsASN.const_VDA4913SatzField_SATZ711F05:
                                eVD.Eingang.ASNRef = Value;
                                break;

                            case clsASN.const_VDA4913SatzField_SATZ711F06:
                                break;

                            case clsASN.const_VDA4913SatzField_SATZ711F07:
                                //Functions.GetDateFromStringVDA(Value);
                                DateTime dtASNDate = DateTime.ParseExact(Value, "yyMMdd", System.Globalization.CultureInfo.InvariantCulture); // CF hh -> HH
                                row["ASN-Datum"] = dtASNDate;
                                asn.Datum = dtASNDate;
                                break;

                            case clsASN.const_VDA4913SatzField_SATZ711F08:
                            case clsASN.const_VDA4913SatzField_SATZ711F09:
                                break;

                            //SLB NR
                            case clsASN.const_VDA4913SatzField_SATZ712F03:
                                eVD.Eingang.ExTransportRef = Value;
                                break;
                            //case 16:
                            case clsASN.const_VDA4913SatzField_SATZ712F04:
                                if (!bIsRead16)
                                {
                                    // TEST
                                    string tmp = row["Ref.Auftraggeber"].ToString();
                                    //clsADRVerweis adrverweis = new clsADRVerweis();
                                    //adrverweis.FillClassByVerweis(row["Ref.Auftraggeber"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    //if ((adrverweis.ID == 0) || (adrverweis.UseS712F04))
                                    //{
                                    //    row["Ref.Auftraggeber"] += "#" + Value;
                                    //    tmp = row["Ref.Auftraggeber"].ToString();
                                    //    adrverweis.FillClassByVerweis(row["Ref.Auftraggeber"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    //}
                                    //row["Auftraggeber"] = adrverweis.VerweisAdrID;

                                    AddressReferenceViewData adrRefVD = new AddressReferenceViewData();
                                    adrRefVD.FillClassByVerweis(row["Ref.Auftraggeber"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    if ((adrRefVD.adrReference.Id == 0) || (adrRefVD.adrReference.UseS712F04))
                                    {
                                        row["Ref.Auftraggeber"] += "#" + Value;
                                        tmp = row["Ref.Auftraggeber"].ToString();
                                        adrRefVD.FillClassByVerweis(row["Ref.Auftraggeber"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    }
                                    //row["Auftraggeber"] = adrRefVD.adrReference.VerweisAdrId;
                                   
                                    AddressViewData adrVD = new AddressViewData((int)adrRefVD.adrReference.VerweisAdrId, BenutzerID);
                                    //row["Lieferantennummer"] = adrRefVD.adrReference.SupplierReference;
                                    //row["AuftraggeberView"] = adrVD.Address.ViewId;

                                    eVD.Eingang.Auftraggeber = adrVD.Address.Id;
                                    eVD.Eingang.AuftraggeberString = adrVD.Address.ViewIdString;
                                    eVD.Eingang.Lieferant = adrRefVD.adrReference.SupplierNo;

                                    //clsADR ADR = new clsADR();
                                    //ADR._GL_User = this.GLUser;
                                    //ADR.ID = adrverweis.VerweisAdrID;
                                    //ASNSender = adrverweis.VerweisAdrID;
                                    //ADR.Fill();
                                    //row["Lieferantennummer"] = adrverweis.LieferantenVerweis;
                                    //row["AuftraggeberView"] = ADR.ViewID;
                                    bIsRead16 = true;
                                }
                                break;
                            //Transportmittel Schlüssel
                            case clsASN.const_VDA4913SatzField_SATZ712F14:
                                row["Transportmittel"] = Value.ToString();
                                string str = Value.ToString();
                                break;

                            //Transportmittel - Nummer
                            case clsASN.const_VDA4913SatzField_SATZ712F15:
                                string strF712F15 = row["Transportmittel"].ToString();
                                string strValue = Value.ToString();
                                ediHelper_712_TM tmp712TM = new ediHelper_712_TM(strF712F15, strValue);

                                //row["KFZ"] = string.Empty;
                                //row["WaggonNo"] = string.Empty;
                                //row["IsWaggon"] = false;
                                //row["Ship"] = string.Empty;
                                //row["IsShip"] = false;

                                eVD.Eingang.KFZ = string.Empty;
                                eVD.Eingang.WaggonNr = string.Empty;
                                eVD.Eingang.IsWaggon = false;
                                eVD.Eingang.Ship = string.Empty;
                                eVD.Eingang.IsShip = false;

                                switch (tmp712TM.TMS)
                                {
                                    case "01":
                                        //if (this.GLSystem.Modul_VDA_Use_KFZ)
                                        //{
                                        //    row["KFZ"] = tmp712TM.VehicleNo;
                                        //    eVD.Eingang.KFZ = tmp712TM.VehicleNo;
                                        //}
                                        //row["KFZ"] = tmp712TM.VehicleNo;
                                        eVD.Eingang.KFZ = tmp712TM.VehicleNo;
                                        break;
                                    case "08":
                                        //row["WaggonNo"] = tmp712TM.VehicleNo;
                                        //row["IsWaggon"] = true;

                                        eVD.Eingang.WaggonNr = tmp712TM.VehicleNo;
                                        eVD.Eingang.IsWaggon = true;
                                        break;
                                    case "11":
                                        //row["Ship"] = row["WaggonNo"] = tmp712TM.VehicleNo; 
                                        //row["IsShip"] = true;

                                        eVD.Eingang.WaggonNr = tmp712TM.VehicleNo;
                                        eVD.Eingang.Ship = tmp712TM.VehicleNo;
                                        eVD.Eingang.IsShip = true;
                                        break;
                                    default:
                                        //-- 2025.12.12. -> TKS sendet TM = 00
                                        eVD.Eingang.KFZ = tmp712TM.VehicleNo;
                                        break;
                                }
                                break;

                            //Lieferscheinnummer
                            case clsASN.const_VDA4913SatzField_SATZ713F03:
                                //Check auf Lieferscheinnummer, wenn neue Lieferscheinnummer, dann neuen Eingang
                                //row["LfsNr"] = Value;
                                eVD.Eingang.LfsNr = Value;
                                strLastLfsNr = Value;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F04:
                                DateTime dtVSDate = DateTime.ParseExact(Value, "yyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                                row["VS-Datum"] = dtVSDate;
                                break;

                            case clsASN.const_VDA4913SatzField_SATZ713F05:
                            case clsASN.const_VDA4913SatzField_SATZ713F06:
                            case clsASN.const_VDA4913SatzField_SATZ713F07:
                            case clsASN.const_VDA4913SatzField_SATZ713F08:
                            case clsASN.const_VDA4913SatzField_SATZ713F09:
                            case clsASN.const_VDA4913SatzField_SATZ713F10:
                            case clsASN.const_VDA4913SatzField_SATZ713F11:
                            case clsASN.const_VDA4913SatzField_SATZ713F12:
                                break;

                            case clsASN.const_VDA4913SatzField_SATZ713F13:
                                if (!bIsRead46)
                                {
                                    string tmp = row["Ref.Empfaenger"].ToString();

                                    AddressReferenceViewData adrverweisE = new AddressReferenceViewData();
                                    adrverweisE.FillClassByVerweis(row["Ref.Empfaenger"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    if ((adrverweisE.adrReference.Id == 0) || (adrverweisE.adrReference.UseS713F13))
                                    {
                                        row["Ref.Empfaenger"] += "#" + Value;
                                        tmp = row["Ref.Empfaenger"].ToString();
                                        adrverweisE.FillClassByVerweis(row["Ref.Empfaenger"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    }
                                    //row["Empfaenger"] = adrverweisE.adrReference.VerweisAdrId;

                                    AddressViewData adrVD = new AddressViewData((int)adrverweisE.adrReference.VerweisAdrId, BenutzerID);
                                    //row["Lieferantennummer"] = adrverweisE.adrReference.SupplierReference;
                                    //row["AuftraggeberView"] = adrVD.Address.ViewId;


                                    //clsADRVerweis adrverweisE = new clsADRVerweis();
                                    //adrverweisE.FillClassByVerweis(row["Ref.Empfaenger"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    //if ((adrverweisE.ID == 0) || (adrverweisE.UseS713F13))
                                    //{
                                    //    row["Ref.Empfaenger"] += "#" + Value;
                                    //    tmp = row["Ref.Empfaenger"].ToString();
                                    //    adrverweisE.FillClassByVerweis(row["Ref.Empfaenger"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    //}
                                    ////clsADRVerweis adrverweisE = new clsADRVerweis();
                                    ////adrverweisE.FillClassByVerweis(row["Ref.Empfaenger"].ToString(), clsASN.const_ASNFiledTyp_VDA4913);
                                    //row["Empfaenger"] = adrverweisE.VerweisAdrID;
                                    //clsADR ADRE = new clsADR();
                                    //ADRE._GL_User = this.GLUser;
                                    //ADRE.ID = adrverweisE.VerweisAdrID;
                                    //ASNReceiver = adrverweisE.VerweisAdrID;
                                    //ADRE.Fill();
                                    //row["EmpfaengerView"] = ADRE.ViewID;

                                    eVD.Eingang.Empfaenger = adrVD.Address.Id;
                                    eVD.Eingang.EmpfaengerString = adrVD.Address.ViewIdString;
                                    bIsRead46 = true;
                                    finishLoop = true;
                                }
                                break;
                        }

                        //im letzten Durchlauf die Row der Talbe EIngang hinzufügen
                        //if (x == dtASNValue.Rows.Count - 1)
                        //{
                        //    dtEingang.Rows.Add(row);
                        //    UpdateASNTableSenderAndReceiver(ref dtASN, ASNSender, ASNReceiver, decASNID);
                        //}
                    }//end For

                    //--- bevor die Datatable dtAsn hinzugefügt werden kann muss die Table für die weitere Verarbeitung bearbeitet werden
                    ctrASNRead_Helper_EditVdaValueTableToUser helper_EditVdaValueTableToUser = new ctrASNRead_Helper_EditVdaValueTableToUser(dtASN);

                    // Prüfen, ob die Quellspalte existiert
                    foreach (DataRow r in helper_EditVdaValueTableToUser.dtOrg.Rows)
                    {
                        r["ASNSender"] = eVD.Eingang.Auftraggeber;
                        r["ASNReceiver"] = eVD.Eingang.Empfaenger;
                    }

                    //--- Erstellung der ViewKlasse
                    AsnVdaView = new ctrASNRead_AsnVdaView(myAsn, eVD, helper_EditVdaValueTableToUser.dtOrg);
                    DateTime dtTmp = new DateTime(1900, 1, 1);
                    DateTime.TryParse(row["VS-Datum"].ToString(), out dtTmp);
                    AsnVdaView.VS_Datum = dtTmp;
                    AsnVdaView.RefAuftraggeber = row["Ref.Auftraggeber"].ToString();
                    AsnVdaView.RefEmpfaenger = row["Ref.Empfaenger"].ToString();

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAsnVdaView"></param>
        /// <returns></returns>
        public List<ctrASNRead_AsnArticleVdaView> ArtikelVdaMessageValueInit(List<ctrASNRead_AsnVdaView> myAsnVdaView)
        {
            List<ctrASNRead_AsnArticleVdaView> listReturn = new List<ctrASNRead_AsnArticleVdaView>();
            foreach (ctrASNRead_AsnVdaView itm in myAsnVdaView)
            {
                ctrASNRead_AsnArticleVdaView tmpView = new ctrASNRead_AsnArticleVdaView(itm.eingang, new Articles());
                if (!listReturn.Contains(tmpView))
                {
                    listReturn.Add(tmpView);
                }
            }
            return listReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAsnValue"></param>
        public List<ctrASNRead_AsnArticleVdaView> CreateArtikelVdaMessageValueList(DataTable dtASN, Eingaenge myEingang)
        {
            Int32 iCountRow4 = dtASN.Rows.Count;
            List<ctrASNRead_AsnArticleVdaView> listReturn = new List<ctrASNRead_AsnArticleVdaView>();

            List<string> ListCreatedNewGArten = new List<string>();
            clsADRMan adrManTmp = new clsADRMan();

            DataTable dtArtikel = new DataTable();
            //dtArtikel = clsArtikel.GetDataTableArtikelSchema(this.GLUser);
            dtArtikel = ArticleViewData.GetDataTableArtikelSchema();
            dtArtikel.Columns.Add("Gut", typeof(string));
            dtArtikel.Columns.Add("ASN", typeof(int));
            dtArtikel.Columns.Add("LfsNr", typeof(string));
            dtArtikel.Columns.Add("ChildID", typeof(string));
            dtArtikel.Columns.Add("TMS", typeof(string));
            dtArtikel.Columns.Add("VehicleNo", typeof(string));
            //dtArtikel.Columns.Add("Guete", typeof(string));
            Int32 j = 0;
            dtArtikel.Columns["ASN"].SetOrdinal(j);

            if (dtASN.Rows.Count > 0)
            {
                //DataTable dtASNID = dtASN.DefaultView.ToTable(true, "ASNID");
                dtASN.DefaultView.RowFilter = string.Empty;
                //dtASN.DefaultView.Sort = "ID";
                DataTable dtASNID = dtASN.DefaultView.ToTable(true, "ASNID", "ASNSender", "ASNReceiver");
                for (Int32 i = 0; i <= dtASNID.Rows.Count - 1; i++)
                {
                    Int32 iCountArt = 0;
                    DataRow row = dtArtikel.NewRow();
                    string asnIDTmp = dtASNID.Rows[i]["ASNID"].ToString();
                    decimal decTmp = 0;
                    decimal decTmpASN = 0;
                    Decimal.TryParse(asnIDTmp, out decTmpASN);
                    row["ASN"] = decTmpASN;
                    row["ChildID"] = ((Int32)decTmpASN).ToString();

                    Int32 iCountRow3 = dtASN.Rows.Count;
                    //dtASN.DefaultView.Sort = "ID";
                    dtASN.DefaultView.Sort = "LfdNr";
                    dtASN.DefaultView.RowFilter = string.Empty;
                    dtASN.DefaultView.RowFilter = "ASNID=" + asnIDTmp;

                    decimal decASNSender = 0;
                    decimal.TryParse(dtASNID.Rows[i]["ASNSender"].ToString(), out decASNSender);
                    decimal decASNReceiver = 0;
                    decimal.TryParse(dtASNID.Rows[i]["ASNReceiver"].ToString(), out decASNReceiver);

                    DataTable dtASNValue = dtASN.DefaultView.ToTable();

                    Int32 iCountRow1 = dtASN.Rows.Count;
                    Int32 iCountRow = dtASNValue.Rows.Count;

                    //Table mit den XMLDaten aus der Message
                    Int32 iTmp = 0;
                    bool bTRAN_PART = false;
                    bool bLS_PART = false;

                    string ZS_Bestellnummer = string.Empty;
                    string strVerweisE = string.Empty;
                    string strVerweisA = string.Empty;
                    //clsADR tmpAdrA = new clsADR();
                    //tmpAdrA._GL_User = this.GLUser;
                    //tmpAdrA.ID = decASNSender;
                    //tmpAdrA.FillClassOnly();
                    //clsADR tmpAdrE = new clsADR();
                    //tmpAdrE._GL_User = this.GLUser;
                    //tmpAdrE.ID = decASNReceiver;
                    //tmpAdrE.FillClassOnly();

                    AddressViewData tmpAdrA = new AddressViewData((int)decASNSender, 1);
                    AddressViewData tmpAdrE = new AddressViewData((int)decASNReceiver, 1);

                    bool bIsRead16 = false;
                    bool bIsRead46 = false;
                    string strLastLfsNr = string.Empty;
                    string strTMS = string.Empty;
                    string strVehicleNo = string.Empty;

                    Int32 iArtikelPos = 0;
                    //clsASNArtFieldAssignment ASNArtFieldAssign = new clsASNArtFieldAssignment();
                    ASNArtFieldAssignment ASNArtFieldAssign = new ASNArtFieldAssignment();
                    //ASNArtFieldAssign._GL_User = this.GLUser;
                    //Dictionary<string, clsASNArtFieldAssignment> DictASNArtFieldAssignment = ASNArtFieldAssign.GetArtikelFieldAssignment(decASNSender, decASNReceiver, (int)this.Sys.AbBereich.ID);
                    //Dictionary<string, clsASNArtFieldAssignment> DictASNArtFieldAssCopyFieldValue = ASNArtFieldAssign.GetArtikelFieldAssignmentCopyFields(decASNSender, decASNReceiver, (int)this.Sys.AbBereich.ID);

                    ASNArtFieldAssignmentViewData asnfaVD = new ASNArtFieldAssignmentViewData((int)decASNSender, (int)decASNReceiver, BenutzerID, myEingang.ArbeitsbereichId, false);
                    Dictionary<string, ASNArtFieldAssignment> DictASNArtFieldAssignment = new Dictionary<string, ASNArtFieldAssignment>(asnfaVD.DictASNArtFieldAssignment);
                    Dictionary<string, ASNArtFieldAssignment> DictASNArtFieldAssCopyFieldValue = new Dictionary<string, ASNArtFieldAssignment>(asnfaVD.DictASNArtFieldAssCopyFieldValue);

                    // clsASNTableCombiValue ASNTableCombiVal = new clsASNTableCombiValue();
                    //ASNTableCombiVal.InitClass(this.GLUser, this.GLSystem);
                    //Dictionary<string, clsASNTableCombiValue> DictASNTableCombiValue = ASNTableCombiVal.GetArtikelFieldAssignment(decASNSender, decASNReceiver);
                    AsnTableCombiValueViewData asntcvVD = new AsnTableCombiValueViewData((int)decASNSender, (int)decASNReceiver, myEingang.ArbeitsbereichId, false);
                    Dictionary<string, AsnTableCombiValues> DictASNTableCombiValue = new Dictionary<string, AsnTableCombiValues>(asntcvVD.DictAsnTableCombiValues);

                    //Zur Zwischenspeicherung der Bestell / Auftragsnummer, um diese im Artikel zu hinterlegen
                    Dict713F10OrderID = new Dictionary<string, string>();

                    //Zur Zwischenspeicherung der Lfs / VehicleNlo , um diese im Artikel zu hinterlegen
                    Dict712_Transportmittel = new Dictionary<string, ediHelper_712_TM>();


                    //    //clsArtikel tmpArtZS = new clsArtikel();
                    //    //clsArtikel AddArtikel = new clsArtikel();
                    //    //AddArtikel.InitClass(this.GLUser, this.GLSystem);
                    //    //AddArtikel.sys = this.Sys;
                    //    //AddArtikel.GlowDate = new DateTime(1900, 1, 1);

                    
                    Articles tmpArtZS = new Articles();
                    Articles AddArtikel = new Articles();
                    AddArtikel.GlowDate = new DateTime(1900, 1, 1);

                    int iGlobalArtCount = GlobalFieldVal_ArticleCountInEdi.Check(ref DictASNArtFieldAssignment, dtASNValue);

                    for (Int32 x = 0; x <= dtASNValue.Rows.Count - 1; x++)
                    {
                        //prüfen, ob Datenfelder speziell zugewiesen wurden 
                        Int32 iASNField = 0;
                        Int32.TryParse(dtASNValue.Rows[x]["ASNFieldID"].ToString(), out iASNField);
                        string strKennung = dtASNValue.Rows[x]["Kennung"].ToString();
                        string Value = dtASNValue.Rows[x]["Value"].ToString();

                        //Bestellnummer
                        if (strKennung.Equals("SATZ713F03"))
                        {
                            string str = string.Empty;
                        }
                        if (strKennung.Equals("SATZ715F05"))
                        {
                            string str = string.Empty;
                        }

                        if (DictASNArtFieldAssignment.TryGetValue(strKennung, out ASNArtFieldAssign))
                        {
                            SetASNArtFieldAssignment(ASNArtFieldAssign.ArtField, ref AddArtikel, ref ASNArtFieldAssign, Value, iArtikelPos, false);

                            //Bestellnummer
                            if (strKennung.Equals("SATZ713F08"))
                            {
                                ZS_Bestellnummer = Value;
                                AddToDictOrderID(ref AddArtikel, ZS_Bestellnummer);
                            }
                            if ((ASNArtFieldAssign != null) && (ASNArtFieldAssign.ArtField.Equals("Artikel.Produktionsnummer")))
                            {
                                AddToDictOrderID(ref AddArtikel, ZS_Bestellnummer);
                            }

                            if (CheckForGArtVerweis(strKennung))
                            {
                                if (this.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_Honeselmann + "_"))
                                {
                                    //Tmp.clsTmpVerwTWB tmp = new Tmp.clsTmpVerwTWB();
                                    //tmp.GL_User = this.GLUser;
                                    //Int32 iTmp2 = 0;
                                    //Int32.TryParse(Value, out iTmp2);
                                    //tmp.SAP = iTmp2;
                                    //tmp.FillBySAP();

                                    //AddArtikel.Dicke = tmp.Dicke;
                                    //AddArtikel.Breite = tmp.Breite;
                                    //AddArtikel.Laenge = tmp.Laenge;
                                    //AddArtikel.Guete = tmp.Guete;
                                    //AddArtikel.GArtID = tmp.GArtID;
                                }
                                else
                                {

                                    AddArtikel.GArtID = GoodstypeViewData.GetGutByADRAndVerweis(this.BenutzerID, (int)decASNSender, AddArtikel.Werksnummer, (int)this.system.AbBereich.ID);
                                    //AddArtikel.GArtID = clsGut.GetGutByADRAndVerweis(this.GLUser, tmpAdrA, AddArtikel.Werksnummer, this.Sys.AbBereich.ID);
                                    AddArtikel.Dicke = 0;
                                    AddArtikel.Breite = 0;
                                    AddArtikel.Laenge = 0;
                                    AddArtikel.Hoehe = 0;
                                    if (this.system.Client.Modul.ASN_AutoCreateNewGArtByASN)
                                    {
                                        if (AddArtikel.GArtID == 1)
                                        {
                                            string strTxt = string.Empty;
                                            var gut = GoodstypeViewData.CreateNewGArtByASN((int)decASNSender, Value, (int)this.system.AbBereich.ID);
                                            if (gut.Id > 0)
                                            {
                                                AddArtikel.GArtID = gut.Id;
                                                AddArtikel.Gut = gut;
                                                //Güterart wurde erfolgreich angelegt
                                                strTxt = "NEU -> Güterart ID[" + gut.Id.ToString() + "] - Matchcode [" + gut.ViewID + "]";
                                            }
                                            else
                                            {
                                                //Güterart konnte nicht angelegt werden
                                                strTxt = "Achtung - Güterart konnte nicht erstellt werden!";
                                            }
                                            //if (AddArtikel.GArt.CreateNewGArtByASN(decASNSender, Value))
                                            //{
                                            //    AddArtikel.GArtID = AddArtikel.GArt.ID;
                                            //    //Güterart wurde erfolgreich angelegt
                                            //    strTxt = "NEU -> Güterart ID[" + AddArtikel.GArt.ID.ToString() + "] - Matchcode [" + AddArtikel.GArt.ViewID + "]";
                                            //}
                                            //else
                                            //{
                                            //    //Güterart konnte nicht angelegt werden
                                            //    strTxt = "Achtung - Güterart konnte nicht erstellt werden!";
                                            //}
                                            ListCreatedNewGArten.Add(strTxt);
                                        }
                                    }
                                    if (
                                        (AddArtikel.GArtID > 1) &&
                                        (this.system.Client.Modul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues)
                                       )
                                    {
                                        try
                                        {

                                            //SetASNGArtValue(ref AddArtikel);
                                            ctrASNRead_Helper_SetGArtValueToArticle setAsnArtValue = new ctrASNRead_Helper_SetGArtValueToArticle(AddArtikel, this.system);
                                            AddArtikel = setAsnArtValue.Article;
                                        }
                                        catch (Exception ex)
                                        {
                                            string str = ex.ToString();
                                        }
                                    }

                                }
                            }
                        }
                        else
                        {
                            switch (strKennung)
                            {
                                //case 3:
                                case clsASN.const_VDA4913SatzField_SATZ711F03:
                                    strVerweisE = Value;
                                    break;
                                //DAten Empfännger
                                //case 4:
                                case clsASN.const_VDA4913SatzField_SATZ711F04:
                                    strVerweisA = Value + "#" + strVerweisE;
                                    strVerweisE += "#" + Value;
                                    break;

                                //case 16:
                                case clsASN.const_VDA4913SatzField_SATZ712F04:
                                    if (!bIsRead16)
                                    {
                                        AddressReferenceViewData adrRefVD = new AddressReferenceViewData();
                                        adrRefVD.FillClassByVerweis(strVerweisA, constValue_AsnArt.const_Art_VDA4913);
                                        if ((adrRefVD.adrReference.Id == 0) || (adrRefVD.adrReference.UseS712F04))
                                        {
                                            strVerweisA += "#" + Value;
                                            adrRefVD.FillClassByVerweis(strVerweisA, constValue_AsnArt.const_Art_VDA4913);
                                        }
                                        tmpAdrA = new AddressViewData(adrRefVD.adrReference.VerweisAdrId, 1);


                                        //clsADRVerweis adrverweis = new clsADRVerweis();
                                        //adrverweis.FillClassByVerweis(strVerweisA, constValue_AsnArt.const_Art_VDA4913);
                                        //if ((adrverweis.ID == 0) || (adrverweis.UseS713F13))
                                        //{
                                        //    strVerweisA += "#" + Value;
                                        //    adrverweis.FillClassByVerweis(strVerweisA, constValue_AsnArt.const_Art_VDA4913);
                                        //}
                                        //tmpAdrA = new clsADR();
                                        //tmpAdrA._GL_User = this.GLUser;
                                        //tmpAdrA.ID = adrverweis.VerweisAdrID;
                                        //tmpAdrA.Fill();
                                        bIsRead16 = true;
                                    }
                                    break;

                                // TMS
                                case clsASN.const_VDA4913SatzField_SATZ712F14:
                                    strTMS = Value;
                                    //AddArtikel.ASN_TMS = strTMS;
                                    break;
                                // VehicleNo
                                case clsASN.const_VDA4913SatzField_SATZ712F15:
                                    strVehicleNo = Value;
                                    //AddArtikel.ASN_VehicleNo = strVehicleNo;
                                    break;

                                //Lieferscheinnummer
                                case clsASN.const_VDA4913SatzField_SATZ713F03:
                                    ////Check auf Lieferscheinnummer, wenn neue Lieferscheinnummer, dann neuen Eingang
                                    if (!strLastLfsNr.Equals(string.Empty))
                                    {
                                        if (!strLastLfsNr.Equals(Value))
                                        {
                                            //Check Zuweisung Bestellnummer in Customized Artikelfeld
                                            CheckFor713F10OrderIDValue(ref AddArtikel, ref DictASNArtFieldAssignment);
                                            //Prüfen, ob Defaultvalue für Datenfelder vorliegt und entsprechend ändern

                                            if (this.system.AbBereich.DefaultValue.DictArbeitsbereichDefaultValue.Count > 0)
                                            {
                                                this.system.AbBereich.DefaultValue.SetDefaultValue(ref AddArtikel);
                                            }
                                            //Prüfen ob Datenfelder zusammengelegt / combiniert werden müssen
                                            if (DictASNTableCombiValue.Count > 0)
                                            {
                                                SetASNColCombiValue(ref AddArtikel, ref DictASNTableCombiValue);
                                            }
                                            //SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                            //SetRowValue(ref row, ref dtArtikel, AddArtikel, decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);

                                            //--- mr 2024_06_04
                                            //SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                            if (iGlobalArtCount > 0)
                                            {
                                                if (iCountArt <= iGlobalArtCount)
                                                {
                                                    //SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                                    SetRowValue(ref row, ref dtArtikel, AddArtikel, decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                                }
                                            }
                                            else
                                            {
                                                //SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                                SetRowValue(ref row, ref dtArtikel, AddArtikel, decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                            }

                                            ctrASNRead_AsnArticleVdaView adrViewToAdd = new ctrASNRead_AsnArticleVdaView(myEingang, AddArtikel);
                                            if (!listReturn.Contains(adrViewToAdd))
                                            {
                                                listReturn.Add(adrViewToAdd);
                                            }

                                            iCountArt = 0;
                                            //tmpArtZS = new clsArtikel();
                                            //tmpArtZS = AddArtikel.Copy();
                                            //AddArtikel = new clsArtikel();
                                            //AddArtikel.AbBereichID = (int)this.system.AbBereich.ID;
                                            //AddArtikel._GL_User = this.GLUser;
                                            //AddArtikel.AuftragID = 0;
                                            //AddArtikel.AuftragPos = 0;
                                            //AddArtikel.AuftragPosTableID = 0;
                                            //AddArtikel.LVS_ID = 0;
                                            //AddArtikel.EingangChecked = false;
                                            //AddArtikel.AusgangChecked = false;
                                            //AddArtikel.UB_AltCalcAuslagerung = false;
                                            //AddArtikel.IsLagerArtikel = true;
                                            //AddArtikel.LEingangTableID = 0;
                                            //AddArtikel.LAusgangTableID = 0;
                                            //AddArtikel.FreigabeAbruf = false;
                                            ////AddArtikel.Bestellnummer = ZS_Bestellnummer;
                                            //AddArtikel.exAuftrag = string.Empty;
                                            //AddArtikel.exAuftragPos = string.Empty;
                                            //AddArtikel.GlowDate = new DateTime(1900, 1, 1);
                                            //CheckForCopyToNewArtikelValue(ref tmpArtZS, ref AddArtikel, ref DictASNArtFieldAssignment);

                                            tmpArtZS = new Articles();
                                            tmpArtZS = AddArtikel.Copy();
                                            AddArtikel = new Articles();
                                            AddArtikel.GlowDate = new DateTime(1900, 1, 1);
                                            AddArtikel.AbBereichID = (int)this.system.AbBereich.ID;
                                            AddArtikel.Bestellnummer = ZS_Bestellnummer;
                                            CheckForCopyToNewArtikelValue(ref tmpArtZS, ref AddArtikel, ref DictASNArtFieldAssignment);
                                        }
                                    }

                                    iArtikelPos = 1;
                                    AddArtikel.Position = iArtikelPos.ToString();

                                    row["LfsNr"] = Value;
                                    strLastLfsNr = Value;
                                    AddToDict712_TM(strTMS, strVehicleNo, strLastLfsNr);
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ713F08:
                                    ZS_Bestellnummer = string.Empty;
                                    ZS_Bestellnummer = Value;
                                    AddArtikel.Bestellnummer = Value;
                                    break;

                                case clsASN.const_VDA4913SatzField_SATZ713F13:
                                    if (!bIsRead46)
                                    {
                                        //clsADRVerweis adrverweisE = new clsADRVerweis();
                                        //adrverweisE.FillClassByVerweis(strVerweisE, constValue_AsnArt.const_Art_VDA4913);
                                        //if ((adrverweisE.ID == 0) || (adrverweisE.UseS713F13))
                                        //{
                                        //    strVerweisE += "#" + Value;
                                        //    adrverweisE.FillClassByVerweis(strVerweisE, constValue_AsnArt.const_Art_VDA4913);
                                        //}
                                        //tmpAdrE = new clsADR();
                                        //tmpAdrE._GL_User = this.GLUser;
                                        //tmpAdrE.ID = adrverweisE.VerweisAdrID;
                                        //tmpAdrE.Fill();

                                        AddressReferenceViewData adrverweisE = new AddressReferenceViewData();
                                        adrverweisE.FillClassByVerweis(strVerweisA, constValue_AsnArt.const_Art_VDA4913);
                                        if ((adrverweisE.adrReference.Id == 0) || (adrverweisE.adrReference.UseS713F13))
                                        {
                                            strVerweisE += "#" + Value;
                                            adrverweisE.FillClassByVerweis(strVerweisE, constValue_AsnArt.const_Art_VDA4913);
                                        }
                                        tmpAdrE = new AddressViewData(adrverweisE.adrReference.VerweisAdrId, 1);
                                        bIsRead46 = true;
                                    }
                                    break;

                                //case 55:
                                case clsASN.const_VDA4913SatzField_SATZ714F01:
                                    if (Value == "714")
                                    {
                                        if (iCountArt > 0)
                                        {
                                            //Check Zuweisung Bestellnummer in Customized Artikelfeld
                                            CheckFor713F10OrderIDValue(ref AddArtikel, ref DictASNArtFieldAssignment);

                                            //Prüfen, ob Defaultvalue für Datenfelder vorliegt und entsprechend ändern
                                            if (this.system.AbBereich.DefaultValue is null)
                                            {
                                                this.system.AbBereich.InitDefaultValue();
                                            }
                                            if (this.system.AbBereich.DefaultValue.DictArbeitsbereichDefaultValue.Count > 0)
                                            {
                                                this.system.AbBereich.DefaultValue.SetDefaultValue(ref AddArtikel);
                                            }
                                            //Prüfen ob Datenfelder zusammengelegt / combiniert werden müssen
                                            if (DictASNTableCombiValue.Count > 0)
                                            {
                                                SetASNColCombiValue(ref AddArtikel, ref DictASNTableCombiValue);
                                            }

                                            //--- mr 2024_06_04
                                            //SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                            if (iGlobalArtCount > 0)
                                            {
                                                if (iCountArt <= iGlobalArtCount)
                                                {
                                                    //SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                                    SetRowValue(ref row, ref dtArtikel, AddArtikel, decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                                    ctrASNRead_AsnArticleVdaView adrViewToAdd = new ctrASNRead_AsnArticleVdaView(myEingang, AddArtikel);
                                                    if (!listReturn.Contains(adrViewToAdd))
                                                    {
                                                        listReturn.Add(adrViewToAdd);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                                SetRowValue(ref row, ref dtArtikel, AddArtikel, decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                                ctrASNRead_AsnArticleVdaView adrViewToAdd = new ctrASNRead_AsnArticleVdaView(myEingang, AddArtikel);
                                                if (!listReturn.Contains(adrViewToAdd))
                                                {
                                                    listReturn.Add(adrViewToAdd);
                                                }
                                            }

                                            //ctrASNRead_AsnArticleVdaView adrViewToAdd = new ctrASNRead_AsnArticleVdaView(myEingang, AddArtikel);
                                            //if (!listReturn.Contains(adrViewToAdd))
                                            //{
                                            //    listReturn.Add(adrViewToAdd);
                                            //}

                                            iArtikelPos++;
                                            //tmpArtZS = new clsArtikel();
                                            //tmpArtZS = AddArtikel.Copy();

                                            //AddArtikel = new clsArtikel();
                                            //AddArtikel.AbBereichID = this.Sys.AbBereich.ID;
                                            //AddArtikel.Position = iArtikelPos.ToString();
                                            //AddArtikel._GL_User = this.GLUser;
                                            //AddArtikel.AuftragID = 0;
                                            //AddArtikel.AuftragPos = 0;
                                            //AddArtikel.AuftragPosTableID = 0;
                                            //AddArtikel.LVS_ID = 0;
                                            //AddArtikel.EingangChecked = false;
                                            //AddArtikel.AusgangChecked = false;
                                            //AddArtikel.UB_AltCalcAuslagerung = false;
                                            //AddArtikel.IsLagerArtikel = true;
                                            //AddArtikel.LEingangTableID = 0;
                                            //AddArtikel.LAusgangTableID = 0;
                                            //AddArtikel.FreigabeAbruf = false;
                                            //AddArtikel.GlowDate = new DateTime(1900, 1, 1);
                                            //AddArtikel.Guete = string.Empty;
                                            //CheckForCopyToNewArtikelValue(ref tmpArtZS, ref AddArtikel, ref DictASNArtFieldAssignment);

                                            tmpArtZS = new Articles();
                                            tmpArtZS = AddArtikel.Copy();
                                            AddArtikel = new Articles();
                                            AddArtikel.GlowDate = new DateTime(1900, 1, 1);
                                            AddArtikel.AbBereichID = (int)this.system.AbBereich.ID;
                                            AddArtikel.Position = iArtikelPos.ToString();
                                            CheckForCopyToNewArtikelValue(ref tmpArtZS, ref AddArtikel, ref DictASNArtFieldAssignment);
                                        }
                                        iCountArt++;
                                    }
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F02:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F03:
                                    AddArtikel.Produktionsnummer = Value;
                                    if (CheckForGArtVerweis(strKennung))
                                    {
                                        if (this.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_Honeselmann + "_"))
                                        {
                                            //Tmp.clsTmpVerwTWB tmp = new Tmp.clsTmpVerwTWB();
                                            //tmp.GL_User = this.GLUser;
                                            //Int32 iTmp2 = 0;
                                            //Int32.TryParse(Value, out iTmp2);
                                            //tmp.SAP = iTmp2;
                                            //tmp.FillBySAP();

                                            //AddArtikel.Dicke = tmp.Dicke;
                                            //AddArtikel.Breite = tmp.Breite;
                                            //AddArtikel.Laenge = tmp.Laenge;
                                            //AddArtikel.Guete = tmp.Guete;
                                            //AddArtikel.GArtID = tmp.GArtID;
                                        }
                                        else
                                        {
                                            //AddArtikel.GArtID = clsGut.GetGutByADRAndVerweis(this.GLUser, tmpAdrA, AddArtikel.Produktionsnummer, this.system.AbBereich.ID);                                            
                                            AddArtikel.GArtID = GoodstypeViewData.GetGutByADRAndVerweis(BenutzerID, tmpAdrA.Address.Id, AddArtikel.Produktionsnummer, (int)this.system.AbBereich.ID);

                                            //--testMR
                                            //AddArtikel.GArt.ID = AddArtikel.GArtID;
                                            //AddArtikel.GArt.Fill();
                                            AddArtikel.Dicke = 0;
                                            AddArtikel.Breite = 0;
                                            AddArtikel.Laenge = 0;
                                            AddArtikel.Hoehe = 0;
                                            AddArtikel.Bestellnummer = string.Empty;
                                            if (this.system.Client.Modul.ASN_AutoCreateNewGArtByASN)
                                            {
                                                if (AddArtikel.GArtID == 1)
                                                {
                                                    string strTxt = string.Empty;
                                                    var gut = GoodstypeViewData.CreateNewGArtByASN((int)decASNSender, Value, (int)this.system.AbBereich.ID);
                                                    if (gut.Id > 0)
                                                    {
                                                        AddArtikel.GArtID = gut.Id;
                                                        AddArtikel.Gut = gut;
                                                        //Güterart wurde erfolgreich angelegt
                                                        strTxt = "NEU -> Güterart ID[" + gut.Id.ToString() + "] - Matchcode [" + gut.ViewID + "]";
                                                    }
                                                    else
                                                    {
                                                        //Güterart konnte nicht angelegt werden
                                                        strTxt = "Achtung - Güterart konnte nicht erstellt werden!";
                                                    }
                                                    //if (AddArtikel.GArt.CreateNewGArtByASN(decASNSender, Value))
                                                    //{
                                                    //    AddArtikel.GArtID = AddArtikel.GArt.ID;
                                                    //    //Güterart wurde erfolgreich angelegt
                                                    //    strTxt = "NEU -> Güterart ID[" + AddArtikel.GArt.ID.ToString() + "] - Matchcode [" + AddArtikel.GArt.ViewID + "]";
                                                    //}
                                                    //else
                                                    //{
                                                    //    //Güterart konnte nicht angelegt werden
                                                    //    strTxt = "Achtung - Güterart konnte nicht erstellt werden!";
                                                    //}
                                                    ListCreatedNewGArten.Add(strTxt);
                                                }
                                            }
                                            if (
                                                (AddArtikel.GArtID > 1) &&
                                                (this.system.Client.Modul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues)
                                               )
                                            {
                                                //SetASNGArtValue(ref AddArtikel);
                                                ctrASNRead_Helper_SetGArtValueToArticle setAsnArtValue = new ctrASNRead_Helper_SetGArtValueToArticle(AddArtikel, this.system);
                                                AddArtikel = setAsnArtValue.Article;
                                            }
                                        }
                                    }
                                    AddToDictOrderID(ref AddArtikel, ZS_Bestellnummer);
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F04:
                                    AddArtikel.Werksnummer = Value;
                                    //Ermitteln der Güte und Abmessungen
                                    if (CheckForGArtVerweis(strKennung))
                                    {
                                        if (this.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_Honeselmann + "_"))
                                        {
                                            //Tmp.clsTmpVerwTWB tmp = new Tmp.clsTmpVerwTWB();
                                            //tmp.GL_User = this.GLUser;
                                            //Int32 iTmp2 = 0;
                                            //Int32.TryParse(Value, out iTmp2);
                                            //tmp.SAP = iTmp2;
                                            //tmp.FillBySAP();

                                            //AddArtikel.Dicke = tmp.Dicke;
                                            //AddArtikel.Breite = tmp.Breite;
                                            //AddArtikel.Laenge = tmp.Laenge;
                                            //AddArtikel.Guete = tmp.Guete;
                                            //AddArtikel.GArtID = tmp.GArtID;
                                        }
                                        else
                                        {
                                            //AddArtikel.GArtID = clsGut.GetGutByADRAndVerweis(this.GLUser, tmpAdrA, AddArtikel.Werksnummer, this.Sys.AbBereich.ID);
                                            AddArtikel.GArtID = GoodstypeViewData.GetGutByADRAndVerweis(BenutzerID, tmpAdrA.Address.Id, AddArtikel.Werksnummer, (int)this.system.AbBereich.ID);
                                            //---testMR
                                            //AddArtikel.GArt.ID = AddArtikel.GArtID;
                                            //AddArtikel.GArt.Fill();
                                            AddArtikel.Dicke = 0;
                                            AddArtikel.Breite = 0;
                                            AddArtikel.Laenge = 0;
                                            AddArtikel.Hoehe = 0;
                                            AddArtikel.Bestellnummer = string.Empty;
                                            if (this.system.Client.Modul.ASN_AutoCreateNewGArtByASN)
                                            {
                                                if (AddArtikel.GArtID == 1)
                                                {
                                                    string strTxt = string.Empty;
                                                    var gut = GoodstypeViewData.CreateNewGArtByASN((int)decASNSender, Value, (int)this.system.AbBereich.ID);
                                                    if (gut.Id > 0)
                                                    {
                                                        AddArtikel.GArtID = gut.Id;
                                                        AddArtikel.Gut = gut;
                                                        //Güterart wurde erfolgreich angelegt
                                                        strTxt = "NEU -> Güterart ID[" + gut.Id.ToString() + "] - Matchcode [" + gut.ViewID + "]";
                                                    }
                                                    else
                                                    {
                                                        //Güterart konnte nicht angelegt werden
                                                        strTxt = "Achtung - Güterart konnte nicht erstellt werden!";
                                                    }

                                                    //if (AddArtikel.GArt.CreateNewGArtByASN(decASNSender, Value))
                                                    //{
                                                    //    AddArtikel.GArtID = AddArtikel.GArt.ID;
                                                    //    //Güterart wurde erfolgreich angelegt
                                                    //    strTxt = "NEU -> Güterart ID[" + AddArtikel.GArt.ID.ToString() + "] - Matchcode [" + AddArtikel.GArt.ViewID + "]";
                                                    //}
                                                    //else
                                                    //{
                                                    //    //Güterart konnte nicht angelegt werden
                                                    //    strTxt = "Achtung - Güterart konnte nicht erstellt werden!";
                                                    //}
                                                    ListCreatedNewGArten.Add(strTxt);
                                                }
                                            }
                                            if (
                                                (AddArtikel.GArtID > 1) &&
                                                (this.system.Client.Modul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues)
                                               )
                                            {
                                                //SetASNGArtValue(ref AddArtikel);
                                                ctrASNRead_Helper_SetGArtValueToArticle setAsnArtValue = new ctrASNRead_Helper_SetGArtValueToArticle(AddArtikel, this.system);
                                                AddArtikel = setAsnArtValue.Article;
                                            }
                                        }
                                    }
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F05:
                                    break;
                                //Brutto
                                case clsASN.const_VDA4913SatzField_SATZ714F06:
                                    decTmp = 0;
                                    decimal.TryParse(Value, out decTmp);
                                    AddArtikel.Brutto = decTmp / 1000;
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F07:
                                    AddArtikel.Einheit = Value;
                                    break;
                                //netto
                                case clsASN.const_VDA4913SatzField_SATZ714F08:
                                    decTmp = 0;
                                    decimal.TryParse(Value, out decTmp);
                                    AddArtikel.Netto = decTmp / 1000;
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F09:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F10:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F11:
                                    break;
                                //Pos
                                //case 66:
                                case clsASN.const_VDA4913SatzField_SATZ714F12:
                                    //AddArtikel.Position = Value;
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ714F13:
                                    break;
                                //charge
                                //case 68:
                                case clsASN.const_VDA4913SatzField_SATZ714F14:
                                    AddArtikel.Charge = Value;
                                    break;
                                //gef. Stoffe -> für LZZ
                                //case 70:
                                case clsASN.const_VDA4913SatzField_SATZ714F16:
                                    Int32 iYear = 1900;
                                    Int32 iKW = 1;
                                    if (Value.Length >= 6)
                                    {
                                        string strYear = string.Empty;
                                        string strKW = string.Empty;
                                        strYear = Value.Substring(0, 4);
                                        Int32.TryParse(strYear, out iYear);
                                        strKW = Value.Substring(4, Value.Length - 4);
                                        Int32.TryParse(strKW, out iKW);
                                    }
                                    AddArtikel.LZZ = Functions.GetDateFromLastDayOfCalWeek(iKW, iYear);
                                    break;

                                case clsASN.const_VDA4913SatzField_SATZ715F01:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F02:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F03:
                                    switch (Value)
                                    {
                                        case clsASN.const_VDA715F03_VerpackungsCodierung_Bund:
                                            AddArtikel.Einheit = "Bund";
                                            break;
                                        case clsASN.const_VDA715F03_VerpackungsCodierung_Bleche:
                                            AddArtikel.Einheit = "Paket";
                                            break;
                                        case clsASN.const_VDA715F03_VerpackungsCodierung_Pal:
                                            AddArtikel.Einheit = "Palette";
                                            break;
                                    }
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F04:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F05:
                                    iTmp = 0;
                                    Int32.TryParse(Value.ToString(), out iTmp);
                                    AddArtikel.Anzahl = iTmp;
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F06:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F07:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F08:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F09:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ715F10:
                                    break;

                                case clsASN.const_VDA4913SatzField_SATZ716F01:
                                    //s716 = new ASN.vda4913.Satz716();
                                    //s716.InitClass(dtASNValue.Copy());
                                    break;

                                case clsASN.const_VDA4913SatzField_SATZ717F01:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ717F02:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ717F03:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ717F04:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ717F05:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ717F06:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ717F07:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ717F08:
                                    break;

                                case clsASN.const_VDA4913SatzField_SATZ718F01:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F02:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F03:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F04:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F05:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F06:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F07:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F08:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F09:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F10:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F11:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F12:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F13:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F14:
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ718F15:
                                    break;
                            }

                            if (
                                (AddArtikel.GArtID > 1) &&
                                (this.system.Client.Modul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues)
                               )
                            {
                                //SetASNGArtValue(ref AddArtikel);
                                ctrASNRead_Helper_SetGArtValueToArticle setAsnArtValue = new ctrASNRead_Helper_SetGArtValueToArticle(AddArtikel, this.system);
                                AddArtikel = setAsnArtValue.Article;
                            }
                        }
                    }
                    if (iCountArt > 0)
                    {
                        //Check Zuweisung Bestellnummer in Customized Artikelfeld
                        CheckFor713F10OrderIDValue(ref AddArtikel, ref DictASNArtFieldAssignment);

                        if (this.system.AbBereich.DefaultValue == null)
                        {
                            this.system.AbBereich.InitDefaultValue();
                        }

                        if (this.system.AbBereich.DefaultValue.DictArbeitsbereichDefaultValue.Count > 0)
                        {
                            this.system.AbBereich.DefaultValue.SetDefaultValue(ref AddArtikel);
                        }
                        //Prüfen ob Datenfelder zusammengelegt / combiniert werden müssen
                        if (DictASNTableCombiValue.Count > 0)
                        {
                            SetASNColCombiValue(ref AddArtikel, ref DictASNTableCombiValue);
                        }
                        //SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);

                        //--- mr 2024_06_04
                        if (iGlobalArtCount > 0)
                        {
                            if (iCountArt <= iGlobalArtCount)
                            {
                                //SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                SetRowValue(ref row, ref dtArtikel, AddArtikel, decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                ctrASNRead_AsnArticleVdaView adrViewToAdd = new ctrASNRead_AsnArticleVdaView(myEingang, AddArtikel);
                                if (!listReturn.Contains(adrViewToAdd))
                                {
                                    listReturn.Add(adrViewToAdd);
                                }
                            }
                            //SetRowValue(ref row, ref dtArtikel, AddArtikel, decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                        }
                        else
                        {
                            //SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                            SetRowValue(ref row, ref dtArtikel, AddArtikel, decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                            ctrASNRead_AsnArticleVdaView adrViewToAdd = new ctrASNRead_AsnArticleVdaView(myEingang, AddArtikel);
                            if (!listReturn.Contains(adrViewToAdd))
                            {
                                listReturn.Add(adrViewToAdd);
                            }
                        }
                        //ctrASNRead_AsnArticleVdaView adrViewToAdd = new ctrASNRead_AsnArticleVdaView(myEingang, AddArtikel);
                        //if (!listReturn.Contains(adrViewToAdd))
                        //{
                        //    listReturn.Add(adrViewToAdd);
                        //}
                    }
                }            
            }
            return listReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAddArt"></param>
        /// <param name="myZS_Bestellnummer"></param>
        private void AddToDictOrderID(ref Articles myAddArt, string myZS_Bestellnummer)
        {
            if (!myZS_Bestellnummer.Equals(string.Empty))
            {
                if ((myAddArt.Produktionsnummer != null) && (!myAddArt.Produktionsnummer.Equals(string.Empty)))
                {
                    if (!Dict713F10OrderID.ContainsValue(myZS_Bestellnummer))
                    {
                        if (!Dict713F10OrderID.ContainsKey(myAddArt.Produktionsnummer))
                        {
                            Dict713F10OrderID.Add(myAddArt.Produktionsnummer, myZS_Bestellnummer);
                        }
                    }
                }
            }
        }

        //private void SetASNGArtValue(ref Articles myArt)
        //{
        //    if (myArt.GArtID > 0)
        //    {
        //        //---testMR
        //        //myArt.GArt.ID = myArt.GArtID;
        //        //myArt.GArt.Fill();

        //        if (!myArt.GArt.Werksnummer.Equals(string.Empty))
        //        {
        //            myArt.Werksnummer = myArt.GArt.Werksnummer;
        //        }
        //        myArt.Einheit = myArt.GArt.Einheit;
        //        if (myArt.Dicke == 0M)
        //        {
        //            myArt.Dicke = myArt.GArt.Dicke;
        //        }
        //        if (myArt.Breite == 0M)
        //        {
        //            myArt.Breite = myArt.GArt.Breite;
        //        }
        //        if (myArt.Laenge == 0M)
        //        {
        //            myArt.Laenge = myArt.GArt.Laenge;
        //        }
        //        if (myArt.Hoehe == 0M)
        //        {
        //            myArt.Hoehe = myArt.GArt.Hoehe;
        //        }
        //        //if (myArt.Bestellnummer.Equals(string.Empty))
        //        //{
        //        //    myArt.Bestellnummer = myArt.GArt.BestellNr;
        //        //}
        //        //--- Bestellnummer
        //        if ((myArt.Netto == 0) && (myArt.GArt.Netto > 0))
        //        {
        //            myArt.Netto = myArt.GArt.Netto;
        //        }
        //        if ((myArt.Brutto == 0) && (myArt.GArt.Brutto > 0))
        //        {
        //            myArt.Brutto = myArt.GArt.Brutto;
        //        }

        //        this.Sys.Client.clsLagerdaten_Customized_ASNArtikel_Bestellnummer(ref myArt, myArt.GArt.BestellNr);

        //        //-- IsMulde
        //        if (
        //                (myArt.GArt.ArtikelArt.IndexOf("COIL") > -1) ||
        //                (myArt.GArt.ArtikelArt.IndexOf("Coil") > -1)
        //            )
        //        {
        //            myArt.IsMulde = true;
        //        }
        //        //-- IsStackable
        //        myArt.IsStackable = myArt.GArt.IsStackable;
        //    }
        //}
        private bool SetASNArtFieldAssignment(string strArtField, ref Articles myArt, ref ASNArtFieldAssignment myArtAssign, string strValue, Int32 myArtPos, bool IsFieldCopy)
        {
            clsASNFormatFunctions ASNfunc = new clsASNFormatFunctions();

            bool bReturn = false;
            if (myArtAssign != null)
            {
                decimal decTmp = 0;
                switch (strArtField)
                {
                    case clsArtikel.ArtikelField_Anzahl:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        Int32 iTmp = 0;
                        Int32.TryParse(strValue, out iTmp);
                        myArt.Anzahl = iTmp;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_LVSID:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Dicke:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        myArt.Dicke = decTmp / 1000;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Breite:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        myArt.Breite = decTmp;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Länge:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        string strHelp = Functions.FormatDecimal(decTmp);
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        myArt.Laenge = decTmp;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Höhe:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        myArt.Hoehe = decTmp;
                        bReturn = true;
                        break;

                    case clsArtikel.ArtikelField_Abmessungen:
                        //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        //hier string.Empty, da die Werte direkt in die clsArtikel myArt geschrieben werden
                        strValue = string.Empty;
                        break;

                    case clsArtikel.ArtikelField_Netto:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        Decimal.TryParse(Functions.FormatDecimal(decTmp), out decTmp);
                        myArt.Netto = decTmp;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Brutto:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        Decimal.TryParse(Functions.FormatDecimal(decTmp), out decTmp);
                        myArt.Brutto = decTmp;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Einheit:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        myArt.Einheit = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Produktionsnummer:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        myArt.Produktionsnummer = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Werksnummer:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        myArt.Werksnummer = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Charge:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        myArt.Charge = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Bestellnummer:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        myArt.Bestellnummer = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_exBezeichnung:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        //Formatierung
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        myArt.exBezeichnung = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_exMaterialnummer:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        myArt.exMaterialnummer = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Gut:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        //myArt.g = strValue;
                        bReturn = false;
                        break;
                    case clsArtikel.ArtikelField_Güte:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        myArt.Guete = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Position:
                        if (myArtAssign.IsDefValue)
                        {
                            switch (myArtAssign.DefValue)
                            {
                                case clsArtikel.ArtikelFunction_ArtikelPosition:
                                    strValue = myArtPos.ToString(); ;
                                    break;
                                default:
                                    strValue = myArtAssign.DefValue;
                                    break;
                            }
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        myArt.Position = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_exAuftrag:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        myArt.exAuftrag = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_exAuftragPos:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        myArt.exAuftragPos = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_ArtikelIDRef:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        myArt.ArtIDRef = strValue;
                        bReturn = true;
                        break;

                    case clsArtikel.ArtikelField_TARef:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        myArt.TARef = strValue;
                        bReturn = true;
                        break;

                    case clsArtikel.ArtikelField_GlowDate:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (myArtAssign.FormatFunction != null) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            //strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, myArt);
                        }
                        DateTime tmpDT = Globals.DefaultDateTimeMinValue;
                        DateTime.TryParse(strValue, out tmpDT);
                        myArt.GlowDate = tmpDT;
                        bReturn = true;
                        break;

                    default:
                        break;
                }
            }
            return bReturn;
        }

        private bool CheckForGArtVerweis(string myKennung)
        {
            bool bReturn = false;
            switch (myKennung)
            {
                case clsASN.const_VDA4913SatzField_SATZ714F03:
                    if (
                        (this.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SIL.ToString() + "_")) ||
                        (this.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SZG.ToString() + "_"))
                        )
                    {
                        bReturn = true;
                    }
                    break;
                case clsASN.const_VDA4913SatzField_SATZ714F04:
                    if (
                            (this.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SLE.ToString() + "_")) ||
                            (this.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_Honeselmann.ToString() + "_"))
                       )
                    {
                        bReturn = true;
                    }
                    break;
            }
            return bReturn;
        }
        ///<summary>clsLagerdaten / CheckForCopyToNewArtikelValue</summary>
        ///<remarks>
        ///         Es wird geprüft ob im DictASNArtFieldAssignment sich Verweise auf 
        ///         Felder im Satz713 befinden, diese werden dann in der Artikelklasse entsprechend kopiert
        ///</remarks>
        private void CheckFor713F10OrderIDValue(ref Articles myArtDest, ref Dictionary<string, ASNArtFieldAssignment> myDictASNArtFieldAssignment)
        {
            foreach (KeyValuePair<string, ASNArtFieldAssignment> itm in myDictASNArtFieldAssignment)
            {
                // do something with entry.Value or entry.Key
                switch (itm.Key)
                {
                    ////711
                    //case clsASN.const_VDA4913SatzField_SATZ711F01:
                    //case clsASN.const_VDA4913SatzField_SATZ711F02:
                    //case clsASN.const_VDA4913SatzField_SATZ711F03:
                    //case clsASN.const_VDA4913SatzField_SATZ711F04:
                    //case clsASN.const_VDA4913SatzField_SATZ711F05:
                    //case clsASN.const_VDA4913SatzField_SATZ711F06:
                    //case clsASN.const_VDA4913SatzField_SATZ711F07:
                    //case clsASN.const_VDA4913SatzField_SATZ711F08:
                    //case clsASN.const_VDA4913SatzField_SATZ711F09:
                    //case clsASN.const_VDA4913SatzField_SATZ711F10:
                    //case clsASN.const_VDA4913SatzField_SATZ711F11:
                    //case clsASN.const_VDA4913SatzField_SATZ711F12:
                    ////712
                    //case clsASN.const_VDA4913SatzField_SATZ712F01:
                    //case clsASN.const_VDA4913SatzField_SATZ712F02:
                    //case clsASN.const_VDA4913SatzField_SATZ712F03:
                    //case clsASN.const_VDA4913SatzField_SATZ712F04:
                    //case clsASN.const_VDA4913SatzField_SATZ712F05:
                    //case clsASN.const_VDA4913SatzField_SATZ712F06:
                    //case clsASN.const_VDA4913SatzField_SATZ712F07:
                    //case clsASN.const_VDA4913SatzField_SATZ712F08:
                    //case clsASN.const_VDA4913SatzField_SATZ712F09:
                    //case clsASN.const_VDA4913SatzField_SATZ712F10:
                    //case clsASN.const_VDA4913SatzField_SATZ712F11:
                    //case clsASN.const_VDA4913SatzField_SATZ712F12:
                    //case clsASN.const_VDA4913SatzField_SATZ712F13:
                    //case clsASN.const_VDA4913SatzField_SATZ712F14:
                    //case clsASN.const_VDA4913SatzField_SATZ712F15:
                    //case clsASN.const_VDA4913SatzField_SATZ712F16:
                    //case clsASN.const_VDA4913SatzField_SATZ712F17:
                    //case clsASN.const_VDA4913SatzField_SATZ712F18:
                    //case clsASN.const_VDA4913SatzField_SATZ712F19:
                    //case clsASN.const_VDA4913SatzField_SATZ712F20:
                    //case clsASN.const_VDA4913SatzField_SATZ712F21:
                    ////713
                    //case clsASN.const_VDA4913SatzField_SATZ713F01:
                    //case clsASN.const_VDA4913SatzField_SATZ713F02:
                    //case clsASN.const_VDA4913SatzField_SATZ713F03:
                    //case clsASN.const_VDA4913SatzField_SATZ713F04:
                    //case clsASN.const_VDA4913SatzField_SATZ713F05:
                    //case clsASN.const_VDA4913SatzField_SATZ713F06:
                    //case clsASN.const_VDA4913SatzField_SATZ713F07:
                    case clsASN.const_VDA4913SatzField_SATZ713F08:
                        //case clsASN.const_VDA4913SatzField_SATZ713F09:
                        //case clsASN.const_VDA4913SatzField_SATZ713F10:
                        //case clsASN.const_VDA4913SatzField_SATZ713F11:
                        //case clsASN.const_VDA4913SatzField_SATZ713F12:
                        //case clsASN.const_VDA4913SatzField_SATZ713F13:
                        //case clsASN.const_VDA4913SatzField_SATZ713F14:
                        //case clsASN.const_VDA4913SatzField_SATZ713F15:
                        //case clsASN.const_VDA4913SatzField_SATZ713F16:
                        //case clsASN.const_VDA4913SatzField_SATZ713F17:
                        //case clsASN.const_VDA4913SatzField_SATZ713F18:
                        //case clsASN.const_VDA4913SatzField_SATZ713F19:
                        //case clsASN.const_VDA4913SatzField_SATZ713F20:
                        //case clsASN.const_VDA4913SatzField_SATZ713F21:
                        ASNArtFieldAssignment tmpASFA = (ASNArtFieldAssignment)itm.Value;
                        if (tmpASFA is ASNArtFieldAssignment)
                        {
                            foreach (KeyValuePair<string, string> xItm in this.Dict713F10OrderID)
                            {
                                if ((myArtDest.Produktionsnummer != null) && (!myArtDest.Produktionsnummer.Equals(string.Empty)))
                                {
                                    if (myArtDest.Produktionsnummer.Equals(xItm.Key.ToString()))
                                    {
                                        myArtDest.SetArtValue(tmpASFA.ArtField, xItm.Value.ToString());
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }
        private void SetASNColCombiValue(ref Articles myArt, ref Dictionary<string, AsnTableCombiValues> myDict)
        {
            foreach (KeyValuePair<string, AsnTableCombiValues> pair in myDict)
            {
                string colTarget = pair.Key.ToString();
                AsnTableCombiValues TmpVal = (AsnTableCombiValues)pair.Value;
                AsnTableCombiValueViewData tmpVD = new AsnTableCombiValueViewData(TmpVal);
                //myArt.CombinateValue(colTarget, TmpVal.ListColsForCombination);
                myArt.CombinateValue(colTarget, tmpVD.ListColsForCombination);
            }
        }

        private void SetRowValue(ref DataRow myRow, ref DataTable mydtArtikel, Articles myArt, decimal myASN, string myLastLfsNR, ref Dictionary<string, ASNArtFieldAssignment> myDictASNArtFieldAssCopyFieldValue)
        {
            //CHeck CopyValue to 
            if (myDictASNArtFieldAssCopyFieldValue.Count > 0)
            {
                foreach (KeyValuePair<string, ASNArtFieldAssignment> item in myDictASNArtFieldAssCopyFieldValue)
                {
                    ASNArtFieldAssignment tmpAss = (ASNArtFieldAssignment)item.Value;
                    string strFieldSource = tmpAss.CopyToField;
                    //tmpAss.ArtField = tmpAss.CopyToField;
                    Int32 iPos = 0;
                    if (myArt.Position == null)
                    {
                        myArt.Position = "0";
                    }

                    Int32.TryParse(myArt.Position.ToString(), out iPos);
                    //string strFieldValueToCopy = myArt.GetArtValueByField(tmpAss.ArtField);
                    string strFieldValueToCopy = Artikel_GetValue.GetArtValueByField(myArt, tmpAss.ArtField);
                    SetASNArtFieldAssignment(strFieldSource, ref myArt, ref tmpAss, strFieldValueToCopy, iPos, true);
                }
            }

            DataRow myTmpRow = mydtArtikel.NewRow();
            myTmpRow["ASN"] = myASN;
            myTmpRow["Position"] = myArt.Position;
            myTmpRow["Werksnummer"] = myArt.Werksnummer;
            myTmpRow["Produktionsnummer"] = myArt.Produktionsnummer;
            myTmpRow["Charge"] = myArt.Charge;
            myTmpRow["Dicke"] = myArt.Dicke;
            myTmpRow["Breite"] = myArt.Breite;
            myTmpRow["Laenge"] = myArt.Laenge;
            myTmpRow["Hoehe"] = myArt.Hoehe;
            myTmpRow["Netto"] = myArt.Netto;
            myTmpRow["Brutto"] = myArt.Brutto;
            myTmpRow["Anzahl"] = myArt.Anzahl;
            myTmpRow["Einheit"] = myArt.Einheit;
            myTmpRow["Bestellnummer"] = myArt.Bestellnummer;
            myTmpRow["exBezeichnung"] = myArt.exBezeichnung;
            myTmpRow["exAuftrag"] = myArt.exAuftrag;
            myTmpRow["exAuftragPos"] = myArt.exAuftragPos;
            myTmpRow["exMaterialnummer"] = myArt.exMaterialnummer;
            myTmpRow["TARef"] = myArt.TARef;
            myTmpRow["GArtID"] = myArt.GArtID;
            myTmpRow["Gut"] = string.Empty;
            if ((myArt.Gut is Goodstypes) && (myArt.Gut.Id > 0))
            {
                myTmpRow["Gut"] = myArt.Gut.Bezeichnung;
            }
            myTmpRow["ArtIDRef"] = myArt.ArtIDRef;
            myTmpRow["LfsNr"] = myLastLfsNR;
            myArt.Lfs = myLastLfsNR;
            myTmpRow["ChildID"] = ((Int32)myASN).ToString() + myLastLfsNR;

            if (this.Dict712_Transportmittel.Count > 0)
            {
                ediHelper_712_TM tmpTM = new ediHelper_712_TM(string.Empty, string.Empty);
                this.Dict712_Transportmittel.TryGetValue(myLastLfsNR, out tmpTM);
                myArt.ASN_TMS = tmpTM.TMS;
                myArt.ASN_VehicleNo = tmpTM.VehicleNo;
            }
            myTmpRow["TMS"] = myArt.ASN_TMS;
            myTmpRow["VehicleNo"] = myArt.ASN_VehicleNo;
            myTmpRow["GlowDate"] = myArt.GlowDate;
            myTmpRow["Guete"] = myArt.Guete;

            mydtArtikel.Rows.Add(myTmpRow);
        }
        ///
        ///<remarks>Es wird geprüft ob im DictASNArtFieldAssignment sich Verweise auf Felder im Satz713 befinden, diese werden dann in der Artikelklasse entsprechend kopiert</remarks>
        private void CheckForCopyToNewArtikelValue(ref Articles ArtSource, ref Articles ArdDest, ref Dictionary<string, ASNArtFieldAssignment> myDictASNArtFieldAssignment)
        {
            //AddArtikel.Bestellnummer = ZS_Bestellnummer;
            //Datenvalue
            foreach (KeyValuePair<string, ASNArtFieldAssignment> itm in myDictASNArtFieldAssignment)
            {
                // do something with entry.Value or entry.Key
                switch (itm.Key)
                {
                    //711
                    case clsASN.const_VDA4913SatzField_SATZ711F01:
                    case clsASN.const_VDA4913SatzField_SATZ711F02:
                    case clsASN.const_VDA4913SatzField_SATZ711F03:
                    case clsASN.const_VDA4913SatzField_SATZ711F04:
                    case clsASN.const_VDA4913SatzField_SATZ711F05:
                    case clsASN.const_VDA4913SatzField_SATZ711F06:
                    case clsASN.const_VDA4913SatzField_SATZ711F07:
                    case clsASN.const_VDA4913SatzField_SATZ711F08:
                    case clsASN.const_VDA4913SatzField_SATZ711F09:
                    case clsASN.const_VDA4913SatzField_SATZ711F10:
                    case clsASN.const_VDA4913SatzField_SATZ711F11:
                    case clsASN.const_VDA4913SatzField_SATZ711F12:
                    //712
                    case clsASN.const_VDA4913SatzField_SATZ712F01:
                    case clsASN.const_VDA4913SatzField_SATZ712F02:
                    case clsASN.const_VDA4913SatzField_SATZ712F03:
                    case clsASN.const_VDA4913SatzField_SATZ712F04:
                    case clsASN.const_VDA4913SatzField_SATZ712F05:
                    case clsASN.const_VDA4913SatzField_SATZ712F06:
                    case clsASN.const_VDA4913SatzField_SATZ712F07:
                    case clsASN.const_VDA4913SatzField_SATZ712F08:
                    case clsASN.const_VDA4913SatzField_SATZ712F09:
                    case clsASN.const_VDA4913SatzField_SATZ712F10:
                    case clsASN.const_VDA4913SatzField_SATZ712F11:
                    case clsASN.const_VDA4913SatzField_SATZ712F12:
                    case clsASN.const_VDA4913SatzField_SATZ712F13:
                    case clsASN.const_VDA4913SatzField_SATZ712F14:
                    case clsASN.const_VDA4913SatzField_SATZ712F15:
                    case clsASN.const_VDA4913SatzField_SATZ712F16:
                    case clsASN.const_VDA4913SatzField_SATZ712F17:
                    case clsASN.const_VDA4913SatzField_SATZ712F18:
                    case clsASN.const_VDA4913SatzField_SATZ712F19:
                    case clsASN.const_VDA4913SatzField_SATZ712F20:
                    case clsASN.const_VDA4913SatzField_SATZ712F21:
                    //713
                    case clsASN.const_VDA4913SatzField_SATZ713F01:
                    case clsASN.const_VDA4913SatzField_SATZ713F02:
                    case clsASN.const_VDA4913SatzField_SATZ713F03:
                    case clsASN.const_VDA4913SatzField_SATZ713F04:
                    case clsASN.const_VDA4913SatzField_SATZ713F05:
                    case clsASN.const_VDA4913SatzField_SATZ713F06:
                    case clsASN.const_VDA4913SatzField_SATZ713F07:
                    case clsASN.const_VDA4913SatzField_SATZ713F08:
                    case clsASN.const_VDA4913SatzField_SATZ713F09:
                    case clsASN.const_VDA4913SatzField_SATZ713F10:
                    case clsASN.const_VDA4913SatzField_SATZ713F11:
                    case clsASN.const_VDA4913SatzField_SATZ713F12:
                    case clsASN.const_VDA4913SatzField_SATZ713F13:
                    case clsASN.const_VDA4913SatzField_SATZ713F14:
                    case clsASN.const_VDA4913SatzField_SATZ713F15:
                    case clsASN.const_VDA4913SatzField_SATZ713F16:
                    case clsASN.const_VDA4913SatzField_SATZ713F17:
                    case clsASN.const_VDA4913SatzField_SATZ713F18:
                    case clsASN.const_VDA4913SatzField_SATZ713F19:
                    case clsASN.const_VDA4913SatzField_SATZ713F20:
                    case clsASN.const_VDA4913SatzField_SATZ713F21:
                        ASNArtFieldAssignment tmpASFA = (ASNArtFieldAssignment)itm.Value;
                        if (tmpASFA is ASNArtFieldAssignment)
                        {
                            ArdDest.CopyArtValue(tmpASFA.ArtField, ref ArtSource);
                        }
                        break;
                }
            }
        }
        private void AddToDict712_TM(string myTM, string myVehicleNo, string myLfs)
        {
            if (!myLfs.Equals(string.Empty))
            {
                if (!Dict712_Transportmittel.ContainsKey(myLfs))
                {
                    ediHelper_712_TM tmpTM = new ediHelper_712_TM(myTM, myVehicleNo);
                    Dict712_Transportmittel.Add(myLfs, tmpTM);
                }
            }
        }
    }
}
