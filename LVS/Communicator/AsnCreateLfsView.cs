using Common.Models;
using Common.Views;
using LVS.Constants;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Data;

namespace LVS.Communicator
{
    public class AsnCreateLfsView
    {
        /// <summary>
        ///             Verarbeiten ASN vom Typ VDA
        /// </summary>
        public AsnCreateLfsView()
        { }
        public AsnCreateLfsView(Globals._GL_SYSTEM myGLSystem, Globals._GL_USER myGlUser, clsSystem mySystem) : this()
        {
            GLSystem = myGLSystem;
            GLUser = myGlUser;
            Sys = mySystem;

            workspaceVD = new WorkspaceViewData((int)Sys.AbBereich.ID);
            Workspace = workspaceVD.Workspace.Copy();
        }
        internal WorkspaceViewData workspaceVD { get; set; }
        internal Workspaces Workspace { get; set; }
        internal clsSystem Sys { get; set; } = new clsSystem();
        internal Globals._GL_USER GLUser { get; set; } = new Globals._GL_USER();
        internal Globals._GL_SYSTEM GLSystem { get; set; } = new Globals._GL_SYSTEM();
        public decimal ASNSender { get; set; } = 0;
        public decimal ASNReceiver { get; set; } = 0;
        public DataTable GetLfsKopfdaten(ref DataTable dtASN)
        {
            //dtASN.Columns.Add("ASNSender", typeof(decimal));
            //dtASN.Columns.Add("ASNReceiver", typeof(decimal));

            ASNSender = 0;
            ASNReceiver = 0;
            clsLEingang Eingang = new clsLEingang();
            DataTable dtEingang = clsLEingang.GetLEingangTableColumnSchema(this.GLUser);
            //zusaätziche Felder für die Übersicht 
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
            dtEingang.Columns["Select"].SetOrdinal(0);
            dtEingang.Columns["ASN"].SetOrdinal(1);

            clsADRMan adrManTmp = new clsADRMan();
            if (dtASN.Rows.Count > 0)
            {
                //Liste der verschiedenen Eingägne erstellen
                DataTable dtASNID = dtASN.DefaultView.ToTable(true, "ASNID");
                for (Int32 i = 0; i <= dtASNID.Rows.Count - 1; i++)
                {
                    DataRow row = dtEingang.NewRow();

                    string asnIDTmp = dtASNID.Rows[i]["ASNID"].ToString();
                    decimal decASNID = 0;
                    Decimal.TryParse(asnIDTmp, out decASNID);
                    row["Select"] = false;
                    row["ASN"] = decASNID;


                    dtASN.DefaultView.RowFilter = string.Empty;
                    dtASN.DefaultView.RowFilter = "ASNID=" + asnIDTmp;
                    DataTable dtASNValue = dtASN.DefaultView.ToTable();
                    //Table mit den XMLDaten aus der Message

                    //bool bTRAN_PART = false;
                    //bool bLS_PART = false;
                    bool bIsRead16 = false;
                    bool bIsRead46 = false;
                    string strLastLfsNr = string.Empty;

                    for (Int32 x = 0; x <= dtASNValue.Rows.Count - 1; x++)
                    {
                        string knot = dtASNValue.Rows[x]["FieldName"].ToString();
                        string Value = dtASNValue.Rows[x]["Value"].ToString();


                        Int32 iASNField = 0;
                        Int32.TryParse(dtASNValue.Rows[x]["ASNFieldID"].ToString(), out iASNField);
                        string strKennung = dtASNValue.Rows[x]["Kennung"].ToString();
                        //string strValue = dtASNValue.Rows[i]["Value"].ToString();
                        switch (strKennung)
                        {
                            case clsASN.const_VDA4913SatzField_SATZ711F01:
                                break;
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
                                row["ASNRef"] = Value;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ711F06:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ711F07:
                                //Functions.GetDateFromStringVDA(Value);
                                DateTime dtASNDate = DateTime.ParseExact(Value, "yyMMdd", System.Globalization.CultureInfo.InvariantCulture); // CF hh -> HH
                                row["ASN-Datum"] = dtASNDate;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ711F08:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ711F09:
                                break;

                            //SLB NR
                            case clsASN.const_VDA4913SatzField_SATZ712F03:
                                row["ExTransportRef"] = Value;
                                break;
                            //case 16:
                            case clsASN.const_VDA4913SatzField_SATZ712F04:
                                if (!bIsRead16)
                                {
                                    // TEST
                                    string tmp = row["Ref.Auftraggeber"].ToString();
                                    clsADRVerweis adrverweis = new clsADRVerweis();
                                    adrverweis.FillClassByVerweis(row["Ref.Auftraggeber"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    if ((adrverweis.ID == 0) || (adrverweis.UseS712F04))
                                    {
                                        row["Ref.Auftraggeber"] += "#" + Value;
                                        tmp = row["Ref.Auftraggeber"].ToString();
                                        adrverweis.FillClassByVerweis(row["Ref.Auftraggeber"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    }
                                    row["Auftraggeber"] = adrverweis.VerweisAdrID;
                                    clsADR ADR = new clsADR();
                                    ADR._GL_User = this.GLUser;
                                    ADR.ID = adrverweis.VerweisAdrID;
                                    ASNSender = adrverweis.VerweisAdrID;
                                    ADR.Fill();
                                    row["Lieferantennummer"] = adrverweis.LieferantenVerweis;
                                    row["AuftraggeberView"] = ADR.ViewID;
                                    bIsRead16 = true;
                                }
                                break;
                            //Transportmittel Schlüssel
                            case clsASN.const_VDA4913SatzField_SATZ712F14:
                                row["Transportmittel"] = Value.ToString();
                                break;

                            //Transportmittel - Nummer
                            case clsASN.const_VDA4913SatzField_SATZ712F15:
                                string strF712F15 = row["Transportmittel"].ToString();
                                string strValue = Value.ToString();
                                ediHelper_712_TM tmp712TM = new ediHelper_712_TM(strF712F15, strValue);

                                row["KFZ"] = string.Empty;
                                row["WaggonNo"] = string.Empty;
                                row["IsWaggon"] = false;
                                row["Ship"] = string.Empty;
                                row["IsShip"] = false;

                                switch (tmp712TM.TMS)
                                {
                                    case "01":
                                        if (this.GLSystem.Modul_VDA_Use_KFZ)
                                        {
                                            row["KFZ"] = tmp712TM.VehicleNo;
                                        }
                                        break;
                                    case "08":
                                        row["WaggonNo"] = tmp712TM.VehicleNo;
                                        row["IsWaggon"] = true;
                                        break;
                                    case "11":
                                        row["Ship"] = row["WaggonNo"] = tmp712TM.VehicleNo; ;
                                        row["IsShip"] = true;
                                        break;
                                }
                                break;

                            //Lieferscheinnummer
                            case clsASN.const_VDA4913SatzField_SATZ713F03:
                                //Check auf Lieferscheinnummer, wenn neue Lieferscheinnummer, dann neuen Eingang
                                row["LfsNr"] = Value;
                                strLastLfsNr = Value;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F04:
                                DateTime dtVSDate = DateTime.ParseExact(Value, "yyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                                row["VS-Datum"] = dtVSDate;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F05:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F06:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F07:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F08:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F09:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F10:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F11:
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ713F12:
                                break;

                            case clsASN.const_VDA4913SatzField_SATZ713F13:
                                if (!bIsRead46)
                                {
                                    string tmp = row["Ref.Empfaenger"].ToString();
                                    clsADRVerweis adrverweisE = new clsADRVerweis();
                                    adrverweisE.FillClassByVerweis(row["Ref.Empfaenger"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    if ((adrverweisE.ID == 0) || (adrverweisE.UseS713F13))
                                    {
                                        row["Ref.Empfaenger"] += "#" + Value;
                                        tmp = row["Ref.Empfaenger"].ToString();
                                        adrverweisE.FillClassByVerweis(row["Ref.Empfaenger"].ToString(), constValue_AsnArt.const_Art_VDA4913);
                                    }
                                    //clsADRVerweis adrverweisE = new clsADRVerweis();
                                    //adrverweisE.FillClassByVerweis(row["Ref.Empfaenger"].ToString(), clsASN.const_ASNFiledTyp_VDA4913);
                                    row["Empfaenger"] = adrverweisE.VerweisAdrID;
                                    clsADR ADRE = new clsADR();
                                    ADRE._GL_User = this.GLUser;
                                    ADRE.ID = adrverweisE.VerweisAdrID;
                                    ASNReceiver = adrverweisE.VerweisAdrID;
                                    ADRE.Fill();
                                    row["EmpfaengerView"] = ADRE.ViewID;
                                    bIsRead46 = true;
                                }
                                break;
                        }

                        //im letzten Durchlauf die Row der Talbe EIngang hinzufügen
                        if (x == dtASNValue.Rows.Count - 1)
                        {
                            dtEingang.Rows.Add(row);
                            UpdateASNTableSenderAndReceiver(ref dtASN, ASNSender, ASNReceiver, decASNID);
                        }
                    }

                }
            }
            return dtEingang;
        }

        ///<summary>clsLagerdaten / UpdateASNTableSenderAndReceiver</summary>
        ///<remarks></remarks>
        private void UpdateASNTableSenderAndReceiver(ref DataTable dt, decimal mySender, decimal myReceiver, decimal myASNNr)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmpASN = 0;
                Decimal.TryParse(dt.Rows[i]["ASNID"].ToString(), out decTmpASN);
                if (decTmpASN > 0)
                {
                    //Check gleiche ASN
                    if (myASNNr == decTmpASN)
                    {
                        dt.Rows[i]["ASNSender"] = mySender;
                        dt.Rows[i]["ASNReceiver"] = myReceiver;
                    }
                }
            }
        }


        public List<AsnLfsView> ConvertDataTableToList(DataTable dt)
        {
            List<AsnLfsView> RetList = new List<AsnLfsView>();

            foreach (DataRow r in dt.Rows)
            {
                AsnLfsView tmpLfs = SetValueLfs(r);
                tmpLfs.WorkspaceId = Workspace.Id;
                tmpLfs.Workspace = Workspace.Copy();
                if (tmpLfs.AsnId > 0)
                {
                    tmpLfs.LfdNr = RetList.Count + 1;
                    RetList.Add(tmpLfs);
                }
            }
            return RetList;
        }

        private AsnLfsView SetValueLfs(DataRow myRow)
        {
            AsnLfsView AsnLfsValue = new AsnLfsView();

            //int iTmp = 0;
            //int.TryParse(myRow["ID"].ToString(), out iTmp);
            //this.AsnLfsValue.Id = iTmp;

            int iTmp = 0;
            int.TryParse(myRow["ASN"].ToString(), out iTmp);
            AsnLfsValue.AsnId = iTmp;

            DateTime dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(myRow["ASN-Datum"].ToString(), out dtTmp);
            AsnLfsValue.AsnDatum = dtTmp;

            dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(myRow["VS-Datum"].ToString(), out dtTmp);
            AsnLfsValue.VsDatum = dtTmp;

            AsnLfsValue.TransportNr = myRow["TransportNr"].ToString();

            iTmp = 0;
            int.TryParse(myRow["Auftraggeber"].ToString(), out iTmp);
            AsnLfsValue.Auftraggeber = iTmp;

            //this.AsnLfsValue.AuftraggeberString = myRow["AuftraggeberView"].ToString();

            iTmp = 0;
            int.TryParse(myRow["Empfaenger"].ToString(), out iTmp);
            AsnLfsValue.Empfaenger = iTmp;

            //this.AsnLfsValue.EmpfaengerString = myRow["EmpfaengerView"].ToString();
            AsnLfsValue.Transportmittel = myRow["Transportmittel"].ToString();
            AsnLfsValue.Lieferantennummer = myRow["Lieferantennummer"].ToString();
            AsnLfsValue.LfsNr = myRow["LfsNr"].ToString();

            if (AsnLfsValue.Auftraggeber > 0)
            {
                AddressViewData adressViewData = new AddressViewData(AsnLfsValue.Auftraggeber, (int)GLUser.User_ID);
                AsnLfsValue.AuftraggeberAdr = adressViewData.Address.Copy();
            }
            if (AsnLfsValue.Empfaenger > 0)
            {
                AddressViewData adressViewData = new AddressViewData(AsnLfsValue.Empfaenger, (int)GLUser.User_ID);
                AsnLfsValue.EmpfaengerAdr = adressViewData.Address.Copy();
            }
            return AsnLfsValue;
        }
    }
}
