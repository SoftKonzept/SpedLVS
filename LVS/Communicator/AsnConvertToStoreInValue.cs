using LVS.ASN.GlobalValues;
using LVS.Constants;
using System;
using System.Collections.Generic;
using System.Data;

namespace LVS.Views
{
    public class AsnConvertToStoreInValue
    {
        public AsnConvertToStoreInValue()
        {
        }
        public AsnConvertToStoreInValue(Globals._GL_SYSTEM myGLSystem, Globals._GL_USER myGlUser, clsSystem mySystem) : this()
        {
            GLSystem = myGLSystem;
            GLUser = myGlUser;
            Sys = mySystem;
        }

        internal clsSystem Sys { get; set; } = new clsSystem();
        internal Globals._GL_USER GLUser { get; set; } = new Globals._GL_USER();
        internal Globals._GL_SYSTEM GLSystem { get; set; } = new Globals._GL_SYSTEM();

        public List<string> ListCreatedNewGArten { get; set; } = new List<string>();
        internal Dictionary<string, string> Dict713F10OrderID { get; set; } = new Dictionary<string, string>();
        internal Dictionary<string, ediHelper_712_TM> Dict712_Transportmittel { get; set; } = new Dictionary<string, ediHelper_712_TM>();


        public DataTable GetArtikelDaten1(ref DataTable dtASN)
        {
            if (this.Sys.AbBereich.DefaultValue is clsArbeitsbereichDefaultValue)
            { }
            else
            {
                this.Sys.AbBereich.DefaultValue = new clsArbeitsbereichDefaultValue();
                this.Sys.AbBereich.DefaultValue.InitCls(this.Sys.AbBereich.ID);
            }

            Int32 iCountRow4 = dtASN.Rows.Count;

            ListCreatedNewGArten = new List<string>();
            clsADRMan adrManTmp = new clsADRMan();

            DataTable dtArtikel = new DataTable();
            dtArtikel = clsArtikel.GetDataTableArtikelSchema(this.GLUser);
            dtArtikel.Columns.Add("Gut", typeof(string));
            dtArtikel.Columns.Add("ASN", typeof(decimal));
            dtArtikel.Columns.Add("LfsNr", typeof(string));
            dtArtikel.Columns.Add("ChildID", typeof(string));
            dtArtikel.Columns.Add("TMS", typeof(string));
            dtArtikel.Columns.Add("VehicleNo", typeof(string));
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

                    if (decTmpASN == 166)
                    {
                        string s = string.Empty;
                    }

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
                    clsADR tmpAdrA = new clsADR();
                    tmpAdrA._GL_User = this.GLUser;
                    tmpAdrA.ID = decASNSender;
                    tmpAdrA.FillClassOnly();

                    clsADR tmpAdrE = new clsADR();
                    tmpAdrE._GL_User = this.GLUser;
                    tmpAdrE.ID = decASNReceiver;
                    tmpAdrE.FillClassOnly();

                    bool bIsRead16 = false;
                    bool bIsRead46 = false;
                    string strLastLfsNr = string.Empty;
                    string strTMS = string.Empty;
                    string strVehicleNo = string.Empty;

                    Int32 iArtikelPos = 0;
                    clsASNArtFieldAssignment ASNArtFieldAssign = new clsASNArtFieldAssignment();
                    ASNArtFieldAssign._GL_User = this.GLUser;
                    Dictionary<string, clsASNArtFieldAssignment> DictASNArtFieldAssignment = ASNArtFieldAssign.GetArtikelFieldAssignment(decASNSender, decASNReceiver, (int)this.Sys.AbBereich.ID);
                    Dictionary<string, clsASNArtFieldAssignment> DictASNArtFieldAssCopyFieldValue = ASNArtFieldAssign.GetArtikelFieldAssignmentCopyFields(decASNSender, decASNReceiver, (int)this.Sys.AbBereich.ID);

                    clsASNTableCombiValue ASNTableCombiVal = new clsASNTableCombiValue();
                    ASNTableCombiVal.InitClass(this.GLUser, this.GLSystem);
                    Dictionary<string, clsASNTableCombiValue> DictASNTableCombiValue = ASNTableCombiVal.GetArtikelFieldAssignment(decASNSender, decASNReceiver);

                    //Zur Zwischenspeicherung der Bestell / Auftragsnummer, um diese im Artikel zu hinterlegen
                    Dict713F10OrderID = new Dictionary<string, string>();

                    //Zur Zwischenspeicherung der Lfs / VehicleNlo , um diese im Artikel zu hinterlegen
                    Dict712_Transportmittel = new Dictionary<string, ediHelper_712_TM>();


                    clsArtikel tmpArtZS = new clsArtikel();

                    clsArtikel AddArtikel = new clsArtikel();
                    AddArtikel.InitClass(this.GLUser, this.GLSystem);
                    AddArtikel.sys = this.Sys;
                    AddArtikel.GlowDate = new DateTime(1900, 1, 1);

                    int iGlobalArtCount = GlobalFieldVal_ArticleCountInEdi.Check(ref DictASNArtFieldAssignment, dtASNValue);

                    //Dictionary<string, string> dict713ZS = new Dictionary<string, string>();
                    for (Int32 x = 0; x <= dtASNValue.Rows.Count - 1; x++)
                    {
                        //prüfen, ob Datenfelder speziell zugewiesen wurden 
                        Int32 iASNField = 0;
                        Int32.TryParse(dtASNValue.Rows[x]["ASNFieldID"].ToString(), out iASNField);
                        string strKennung = dtASNValue.Rows[x]["Kennung"].ToString();
                        string Value = dtASNValue.Rows[x]["Value"].ToString();

                        //Bestellnummer
                        if (strKennung.Equals("SATZ713F08"))
                        {
                            string str = string.Empty;
                        }
                        if (strKennung.Equals("SATZ713F04"))
                        {
                            string str = string.Empty;
                        }
                        if (strKennung.Equals("SATZ714F08"))
                        {
                            string str = string.Empty;
                        }
                        if (strKennung.Equals("SATZ715F04"))
                        {
                            string str = string.Empty;
                        }
                        if (strKennung.Equals("SATZ716F04"))
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
                                //ZS_Bestellnummer = AddArtikel.Bestellnummer;
                                AddToDictOrderID(ref AddArtikel, ZS_Bestellnummer);
                            }
                            if ((ASNArtFieldAssign != null) && (ASNArtFieldAssign.ArtField.Equals("Artikel.Produktionsnummer")))
                            {
                                AddToDictOrderID(ref AddArtikel, ZS_Bestellnummer);
                            }

                            if (CheckForGArtVerweis(strKennung))
                            {
                                if (this.Sys.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_Honeselmann + "_"))
                                {
                                    Tmp.clsTmpVerwTWB tmp = new Tmp.clsTmpVerwTWB();
                                    tmp.GL_User = this.GLUser;
                                    Int32 iTmp2 = 0;
                                    Int32.TryParse(Value, out iTmp2);
                                    tmp.SAP = iTmp2;
                                    tmp.FillBySAP();

                                    AddArtikel.Dicke = tmp.Dicke;
                                    AddArtikel.Breite = tmp.Breite;
                                    AddArtikel.Laenge = tmp.Laenge;
                                    AddArtikel.Guete = tmp.Guete;
                                    AddArtikel.GArtID = tmp.GArtID;
                                }
                                else
                                {
                                    AddArtikel.GArtID = clsGut.GetGutByADRAndVerweis(this.GLUser, tmpAdrA, AddArtikel.Werksnummer, this.Sys.AbBereich.ID);
                                    AddArtikel.Dicke = 0;
                                    AddArtikel.Breite = 0;
                                    AddArtikel.Laenge = 0;
                                    AddArtikel.Hoehe = 0;
                                    if (this.Sys.Client.Modul.ASN_AutoCreateNewGArtByASN)
                                    {
                                        if (AddArtikel.GArtID == 1)
                                        {
                                            string strTxt = string.Empty;
                                            if (AddArtikel.GArt.CreateNewGArtByASN(decASNSender, Value))
                                            {
                                                AddArtikel.GArtID = AddArtikel.GArt.ID;
                                                //Güterart wurde erfolgreich angelegt
                                                strTxt = "NEU -> Güterart ID[" + AddArtikel.GArt.ID.ToString() + "] - Matchcode [" + AddArtikel.GArt.ViewID + "]";
                                            }
                                            else
                                            {
                                                //Güterart konnte nicht angelegt werden
                                                strTxt = "Achtung - Güterart konnte nicht erstellt werden!";
                                            }
                                            ListCreatedNewGArten.Add(strTxt);
                                        }
                                    }
                                    if (
                                        (AddArtikel.GArtID > 1) &&
                                        (this.Sys.Client.Modul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues)
                                       )
                                    {
                                        try
                                        {
                                            SetASNGArtValue(ref AddArtikel);
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
                                        clsADRVerweis adrverweis = new clsADRVerweis();
                                        adrverweis.FillClassByVerweis(strVerweisA, constValue_AsnArt.const_Art_VDA4913);
                                        if ((adrverweis.ID == 0) || (adrverweis.UseS713F13))
                                        {
                                            strVerweisA += "#" + Value;
                                            adrverweis.FillClassByVerweis(strVerweisA, constValue_AsnArt.const_Art_VDA4913);
                                        }

                                        tmpAdrA = new clsADR();
                                        tmpAdrA._GL_User = this.GLUser;
                                        tmpAdrA.ID = adrverweis.VerweisAdrID;
                                        tmpAdrA.Fill();
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

                                            //AddArtikel.Position = iArtikelPos.ToString();
                                            //Prüfen, ob Defaultvalue für Datenfelder vorliegt und entsprechend ändern
                                            if (this.Sys.AbBereich.DefaultValue.DictArbeitsbereichDefaultValue.Count > 0)
                                            {
                                                this.Sys.AbBereich.DefaultValue.SetDefaultValue(ref AddArtikel);
                                            }
                                            //Prüfen ob Datenfelder zusammengelegt / combiniert werden müssen
                                            if (DictASNTableCombiValue.Count > 0)
                                            {
                                                SetASNColCombiValue(ref AddArtikel, ref DictASNTableCombiValue);
                                            }
                                            SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                            iCountArt = 0;
                                            tmpArtZS = new clsArtikel();
                                            tmpArtZS = AddArtikel.Copy();
                                            AddArtikel = new clsArtikel();
                                            AddArtikel.AbBereichID = this.Sys.AbBereich.ID;
                                            AddArtikel._GL_User = this.GLUser;
                                            AddArtikel.AuftragID = 0;
                                            AddArtikel.AuftragPos = 0;
                                            AddArtikel.AuftragPosTableID = 0;
                                            AddArtikel.LVS_ID = 0;
                                            AddArtikel.EingangChecked = false;
                                            AddArtikel.AusgangChecked = false;
                                            AddArtikel.UB_AltCalcAuslagerung = false;
                                            AddArtikel.IsLagerArtikel = true;
                                            AddArtikel.LEingangTableID = 0;
                                            AddArtikel.LAusgangTableID = 0;
                                            AddArtikel.FreigabeAbruf = false;
                                            //AddArtikel.Bestellnummer = ZS_Bestellnummer;
                                            AddArtikel.exAuftrag = string.Empty;
                                            AddArtikel.exAuftragPos = string.Empty;
                                            AddArtikel.GlowDate = new DateTime(1900, 1, 1);
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
                                        clsADRVerweis adrverweisE = new clsADRVerweis();
                                        adrverweisE.FillClassByVerweis(strVerweisE, constValue_AsnArt.const_Art_VDA4913);
                                        if ((adrverweisE.ID == 0) || (adrverweisE.UseS713F13))
                                        {
                                            strVerweisE += "#" + Value;
                                            adrverweisE.FillClassByVerweis(strVerweisE, constValue_AsnArt.const_Art_VDA4913);
                                        }
                                        tmpAdrE = new clsADR();
                                        tmpAdrE._GL_User = this.GLUser;
                                        tmpAdrE.ID = adrverweisE.VerweisAdrID;
                                        tmpAdrE.Fill();
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
                                            if (this.Sys.AbBereich.DefaultValue.DictArbeitsbereichDefaultValue.Count > 0)
                                            {
                                                this.Sys.AbBereich.DefaultValue.SetDefaultValue(ref AddArtikel);
                                            }
                                            //Prüfen ob Datenfelder zusammengelegt / combiniert werden müssen
                                            if (DictASNTableCombiValue.Count > 0)
                                            {
                                                SetASNColCombiValue(ref AddArtikel, ref DictASNTableCombiValue);
                                            }
                                            //AddArtikel.Position = iArtikelPos.ToString();
                                            //SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);

                                            //--- mr 2024_06_04
                                            //SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                            if (iGlobalArtCount > 0)
                                            {
                                                if (iCountArt <= iGlobalArtCount)
                                                {
                                                    SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                                }
                                            }
                                            else
                                            {
                                                SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                                            }

                                            iArtikelPos++;
                                            tmpArtZS = new clsArtikel();
                                            tmpArtZS = AddArtikel.Copy();

                                            AddArtikel = new clsArtikel();
                                            AddArtikel.AbBereichID = this.Sys.AbBereich.ID;
                                            AddArtikel.Position = iArtikelPos.ToString();
                                            AddArtikel._GL_User = this.GLUser;
                                            AddArtikel.AuftragID = 0;
                                            AddArtikel.AuftragPos = 0;
                                            AddArtikel.AuftragPosTableID = 0;
                                            AddArtikel.LVS_ID = 0;
                                            AddArtikel.EingangChecked = false;
                                            AddArtikel.AusgangChecked = false;
                                            AddArtikel.UB_AltCalcAuslagerung = false;
                                            AddArtikel.IsLagerArtikel = true;
                                            AddArtikel.LEingangTableID = 0;
                                            AddArtikel.LAusgangTableID = 0;
                                            AddArtikel.FreigabeAbruf = false;
                                            AddArtikel.GlowDate = new DateTime(1900, 1, 1);
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
                                        if (this.Sys.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_Honeselmann + "_"))
                                        {
                                            Tmp.clsTmpVerwTWB tmp = new Tmp.clsTmpVerwTWB();
                                            tmp.GL_User = this.GLUser;
                                            Int32 iTmp2 = 0;
                                            Int32.TryParse(Value, out iTmp2);
                                            tmp.SAP = iTmp2;
                                            tmp.FillBySAP();

                                            AddArtikel.Dicke = tmp.Dicke;
                                            AddArtikel.Breite = tmp.Breite;
                                            AddArtikel.Laenge = tmp.Laenge;
                                            AddArtikel.Guete = tmp.Guete;
                                            AddArtikel.GArtID = tmp.GArtID;
                                        }
                                        else
                                        {
                                            AddArtikel.GArtID = clsGut.GetGutByADRAndVerweis(this.GLUser, tmpAdrA, AddArtikel.Produktionsnummer, this.Sys.AbBereich.ID);
                                            //--testMR
                                            //AddArtikel.GArt.ID = AddArtikel.GArtID;
                                            //AddArtikel.GArt.Fill();
                                            AddArtikel.Dicke = 0;
                                            AddArtikel.Breite = 0;
                                            AddArtikel.Laenge = 0;
                                            AddArtikel.Hoehe = 0;
                                            AddArtikel.Bestellnummer = string.Empty;
                                            if (this.Sys.Client.Modul.ASN_AutoCreateNewGArtByASN)
                                            {
                                                if (AddArtikel.GArtID == 1)
                                                {
                                                    string strTxt = string.Empty;
                                                    if (AddArtikel.GArt.CreateNewGArtByASN(decASNSender, Value))
                                                    {
                                                        AddArtikel.GArtID = AddArtikel.GArt.ID;
                                                        //Güterart wurde erfolgreich angelegt
                                                        strTxt = "NEU -> Güterart ID[" + AddArtikel.GArt.ID.ToString() + "] - Matchcode [" + AddArtikel.GArt.ViewID + "]";
                                                    }
                                                    else
                                                    {
                                                        //Güterart konnte nicht angelegt werden
                                                        strTxt = "Achtung - Güterart konnte nicht erstellt werden!";
                                                    }
                                                    ListCreatedNewGArten.Add(strTxt);
                                                }
                                            }
                                            if (
                                                (AddArtikel.GArtID > 1) &&
                                                (this.Sys.Client.Modul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues)
                                               )
                                            {
                                                SetASNGArtValue(ref AddArtikel);
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
                                        if (this.Sys.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_Honeselmann + "_"))
                                        {
                                            Tmp.clsTmpVerwTWB tmp = new Tmp.clsTmpVerwTWB();
                                            tmp.GL_User = this.GLUser;
                                            Int32 iTmp2 = 0;
                                            Int32.TryParse(Value, out iTmp2);
                                            tmp.SAP = iTmp2;
                                            tmp.FillBySAP();

                                            AddArtikel.Dicke = tmp.Dicke;
                                            AddArtikel.Breite = tmp.Breite;
                                            AddArtikel.Laenge = tmp.Laenge;
                                            AddArtikel.Guete = tmp.Guete;
                                            AddArtikel.GArtID = tmp.GArtID;
                                        }
                                        else
                                        {
                                            AddArtikel.GArtID = clsGut.GetGutByADRAndVerweis(this.GLUser, tmpAdrA, AddArtikel.Werksnummer, this.Sys.AbBereich.ID);
                                            //---testMR
                                            //AddArtikel.GArt.ID = AddArtikel.GArtID;
                                            //AddArtikel.GArt.Fill();
                                            AddArtikel.Dicke = 0;
                                            AddArtikel.Breite = 0;
                                            AddArtikel.Laenge = 0;
                                            AddArtikel.Hoehe = 0;
                                            AddArtikel.Bestellnummer = string.Empty;
                                            if (this.Sys.Client.Modul.ASN_AutoCreateNewGArtByASN)
                                            {
                                                if (AddArtikel.GArtID == 1)
                                                {
                                                    string strTxt = string.Empty;
                                                    if (AddArtikel.GArt.CreateNewGArtByASN(decASNSender, Value))
                                                    {
                                                        AddArtikel.GArtID = AddArtikel.GArt.ID;
                                                        //Güterart wurde erfolgreich angelegt
                                                        strTxt = "NEU -> Güterart ID[" + AddArtikel.GArt.ID.ToString() + "] - Matchcode [" + AddArtikel.GArt.ViewID + "]";
                                                    }
                                                    else
                                                    {
                                                        //Güterart konnte nicht angelegt werden
                                                        strTxt = "Achtung - Güterart konnte nicht erstellt werden!";
                                                    }
                                                    ListCreatedNewGArten.Add(strTxt);
                                                }
                                            }
                                            if (
                                                (AddArtikel.GArtID > 1) &&
                                                (this.Sys.Client.Modul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues)
                                               )
                                            {
                                                SetASNGArtValue(ref AddArtikel);
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

                            }

                            if (
                                (AddArtikel.GArtID > 1) &&
                                (this.Sys.Client.Modul.ASN_VDA4913_LVS_ReadASN_TakeOverGArtValues)
                               )
                            {
                                SetASNGArtValue(ref AddArtikel);
                            }
                        }
                    }
                    if (iCountArt > 0)
                    {
                        //Check Zuweisung Bestellnummer in Customized Artikelfeld
                        CheckFor713F10OrderIDValue(ref AddArtikel, ref DictASNArtFieldAssignment);

                        if (this.Sys.AbBereich.DefaultValue.DictArbeitsbereichDefaultValue.Count > 0)
                        {
                            this.Sys.AbBereich.DefaultValue.SetDefaultValue(ref AddArtikel);
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
                                SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                            }
                        }
                        else
                        {
                            SetRowValue(ref row, ref dtArtikel, AddArtikel.Copy(), decTmpASN, strLastLfsNr, ref DictASNArtFieldAssCopyFieldValue);
                        }

                    }
                }
            }
            return dtArtikel;
        }

        private void SetRowValue(ref DataRow myRow, ref DataTable mydtArtikel, clsArtikel myArt, decimal myASN, string myLastLfsNR, ref Dictionary<string, clsASNArtFieldAssignment> myDictASNArtFieldAssCopyFieldValue)
        {
            //CHeck CopyValue to 
            if (myDictASNArtFieldAssCopyFieldValue.Count > 0)
            {
                foreach (KeyValuePair<string, clsASNArtFieldAssignment> item in myDictASNArtFieldAssCopyFieldValue)
                {
                    clsASNArtFieldAssignment tmpAss = (clsASNArtFieldAssignment)item.Value;
                    string strFieldSource = tmpAss.CopyToField;
                    //tmpAss.ArtField = tmpAss.CopyToField;
                    Int32 iPos = 0;
                    if (myArt.Position == null)
                    {
                        myArt.Position = "0";
                    }

                    Int32.TryParse(myArt.Position.ToString(), out iPos);
                    string strFieldValueToCopy = myArt.GetArtValueByField(tmpAss.ArtField);
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
            myTmpRow["Gut"] = myArt.GArt.Bezeichnung;
            myTmpRow["ArtIDRef"] = myArt.ArtIDRef;
            myTmpRow["LfsNr"] = myLastLfsNR;
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

            mydtArtikel.Rows.Add(myTmpRow);
        }

        private bool SetASNArtFieldAssignment(string strArtField, ref clsArtikel myArt, ref clsASNArtFieldAssignment myArtAssign, string strValue, Int32 myArtPos, bool IsFieldCopy)
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        decTmp = 0;
                        Decimal.TryParse(strValue, out decTmp);
                        myArt.Hoehe = decTmp;
                        bReturn = true;
                        break;

                    case clsArtikel.ArtikelField_Abmessungen:
                        strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        myArt.Gut = strValue;
                        bReturn = true;
                        break;
                    case clsArtikel.ArtikelField_Güte:
                        if (myArtAssign.IsDefValue)
                        {
                            strValue = myArtAssign.DefValue;
                        }
                        if (
                            (!IsFieldCopy) &&
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
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
                            (!myArtAssign.FormatFunction.Equals(string.Empty))
                          )
                        {
                            strValue = ASNfunc.CustomFormatValue(myArtAssign.FormatFunction, strValue, ref myArt);
                        }
                        DateTime tmpDT = new DateTime(1900, 1, 1); //) Globals.DefaultDateTimeMinValue;
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

        private void AddToDictOrderID(ref clsArtikel myAddArt, string myZS_Bestellnummer)
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

        private bool CheckForGArtVerweis(string myKennung)
        {
            bool bReturn = false;
            switch (myKennung)
            {
                case clsASN.const_VDA4913SatzField_SATZ714F03:
                    if (
                        (this.Sys.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SIL.ToString() + "_")) ||
                        (this.Sys.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SZG.ToString() + "_"))
                        )
                    {
                        bReturn = true;
                    }
                    break;
                case clsASN.const_VDA4913SatzField_SATZ714F04:
                    if (
                            (this.Sys.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SLE.ToString() + "_")) ||
                            (this.Sys.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_Honeselmann.ToString() + "_"))
                       )
                    {
                        bReturn = true;
                    }
                    break;
            }
            return bReturn;
        }

        private void SetASNGArtValue(ref clsArtikel myArt)
        {
            if (myArt.GArtID > 0)
            {
                //---testMR
                //myArt.GArt.ID = myArt.GArtID;
                //myArt.GArt.Fill();

                if (!myArt.GArt.Werksnummer.Equals(string.Empty))
                {
                    myArt.Werksnummer = myArt.GArt.Werksnummer;
                }
                myArt.Einheit = myArt.GArt.Einheit;
                if (myArt.Dicke == 0M)
                {
                    myArt.Dicke = myArt.GArt.Dicke;
                }
                if (myArt.Breite == 0M)
                {
                    myArt.Breite = myArt.GArt.Breite;
                }
                if (myArt.Laenge == 0M)
                {
                    myArt.Laenge = myArt.GArt.Laenge;
                }
                if (myArt.Hoehe == 0M)
                {
                    myArt.Hoehe = myArt.GArt.Hoehe;
                }
                //if (myArt.Bestellnummer.Equals(string.Empty))
                //{
                //    myArt.Bestellnummer = myArt.GArt.BestellNr;
                //}
                //--- Bestellnummer
                if ((myArt.Netto == 0) && (myArt.GArt.Netto > 0))
                {
                    myArt.Netto = myArt.GArt.Netto;
                }
                if ((myArt.Brutto == 0) && (myArt.GArt.Brutto > 0))
                {
                    myArt.Brutto = myArt.GArt.Brutto;
                }

                this.Sys.Client.clsLagerdaten_Customized_ASNArtikel_Bestellnummer(ref myArt, myArt.GArt.BestellNr);

                //-- IsMulde
                if (
                        (myArt.GArt.ArtikelArt.IndexOf("COIL") > -1) ||
                        (myArt.GArt.ArtikelArt.IndexOf("Coil") > -1)
                    )
                {
                    myArt.IsMulde = true;
                }
                //-- IsStackable
                myArt.IsStackable = myArt.GArt.IsStackable;
            }
        }


        private void CheckFor713F10OrderIDValue(ref clsArtikel myArtDest, ref Dictionary<string, clsASNArtFieldAssignment> myDictASNArtFieldAssignment)
        {
            foreach (KeyValuePair<string, clsASNArtFieldAssignment> itm in myDictASNArtFieldAssignment)
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
                        clsASNArtFieldAssignment tmpASFA = (clsASNArtFieldAssignment)itm.Value;
                        if (tmpASFA is clsASNArtFieldAssignment)
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

        private void SetASNColCombiValue(ref clsArtikel myArt, ref Dictionary<string, clsASNTableCombiValue> myDict)
        {
            foreach (KeyValuePair<string, clsASNTableCombiValue> pair in myDict)
            {
                string colTarget = pair.Key.ToString();
                clsASNTableCombiValue TmpVal = (clsASNTableCombiValue)pair.Value;
                myArt.CombinateValue(colTarget, TmpVal.ListColsForCombination);
            }
        }



        ///<summary>clsLagerdaten / CheckForCopyToNewArtikelValue</summary>
        ///<remarks>Es wird geprüft ob im DictASNArtFieldAssignment sich Verweise auf Felder im Satz713 befinden, diese werden dann in der Artikelklasse entsprechend kopiert</remarks>
        private void CheckForCopyToNewArtikelValue(ref clsArtikel ArtSource, ref clsArtikel ArdDest, ref Dictionary<string, clsASNArtFieldAssignment> myDictASNArtFieldAssignment)
        {
            //AddArtikel.Bestellnummer = ZS_Bestellnummer;
            //Datenvalue
            foreach (KeyValuePair<string, clsASNArtFieldAssignment> itm in myDictASNArtFieldAssignment)
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
                        clsASNArtFieldAssignment tmpASFA = (clsASNArtFieldAssignment)itm.Value;
                        if (tmpASFA is clsASNArtFieldAssignment)
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
