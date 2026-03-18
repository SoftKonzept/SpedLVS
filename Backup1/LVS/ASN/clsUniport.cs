using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsUniport
    {
        public Globals._GL_USER GLUser;
        public DataTable dtAdrMan_Sped = new DataTable();
        clsADR ADR = new clsADR();
        List<clsADRMan> ListAdrMan = new List<clsADRMan>();

        /*****
        private Dictionary<string, string> DictXMLAssignment = new Dictionary<string, string> ()
        {        
           {"UNIPOR",""},
           {"HEADER", ""},
           {"ID", ""},
           {"MANDANT", ""},
           {"CREATED", ""},
           {"SENDER", ""},
           {"RECEIVER", ""},
           {"TASK",""},
           {"TASK_NO",""},
           {"ACTION", ""},
           {"ATG",""},
           {"ATG_CONVERT",""},
           {"LAST", ""},
           {"QUIT",""},
           {"TRANSPORT",""},
           {"TRAN_NO", ""},
           {"Tran_NOO",""},
           {"TRAN_EXT",""},
           {"TRAN_NO_AVIS",""},
           {"VAVERMERK", ""},
           {"DISPONENT",""},
           {"SCHNELLSCHUSS",""},
           {"TAN_VELOSA",""},
           {"VKMT_KZ", "LEingang.KFZ"},
           {"VKMT_TYP", "bLKW"},   // L=LKW, B=BAHN, S=Schiff
           {"SUBCONTRACTOR", ""},
           {"DRIVER",""},
           {"VSDT", ""},    //neu 
           {"VSTIME", ""},  //neu ASNVSDate
           {"ARRIVALDT", ""}, //Ankuftsdatum = Lagereingang
           {"ARRIVALTIME", ""},
           {"DOCUDT", ""},
           {"DOCUTIME",""},
           {"VERL_FINISHDT",""},
           {"VERL_FINISHTIME",""},
           {"ENTL_FINISHDT",""},
           {"ENTL_FINISHTIME",""},
           {"TRAN_TXT", ""},
           {"TYP",""},
           {"TXT",""},
           {"TRAN_PART",""},  //*************************************** Transportdaten
           {"PART_NO_INT", ""},//Partner ID
           {"PART_EXT", "LEingang.Lieferant"},
           {"PART_TYP",""},  //TU=TRansportunternehmer, UML=Umschlagslager EMpfäner, UMLA=UmschlagslagerA
           {"PART_NAME1",""},
           {"PART_NAME2", ""},
           {"PART_NAME3",""},
           {"PART_STRASSE",""},
           {"PART_LKZ", ""},
           {"PART_ZIP", ""},
           {"PART_ORT",""},
           {"LS",""}, //******************************************** Lieferscheinkopf
           {"LS_NO","LEingang.LfsNr"},
           {"LS_EXT",""},
           {"LS_TYP", ""},
           {"AUFTRAG_NO", "Artikel.exAuftrag"},
           {"AUFTRAG_NO2",""},
           {"AUFT_NOO", ""},
           {"AUFTRAG_NOO",""},
           {"WAGGONNR", "LEingang.WaggonNo"},
           {"DISTANZ",""},
           {"LS_STATUS",""},
           {"LS_BRUTTO", ""},
           {"LS_ANZAHL", ""},
           {"LS_LAENGE" , ""},
           {"LS_SUFFIX", ""},
           {"LS_TXT",""},
           {"TYP",""},
           {"TXT" ,""},
           {"LS_PART",""},    //++++++++ Lieferschein Empfänger
           {"PART_EXT", ""},
           {"PART_TYP", ""},
           {"PART_NAME1", ""},
           {"PART_NAME2", ""},
           {"PART_NAME3", ""},
           {"PART_STRASSE", ""},
           {"PART_LKZ", ""},
           {"PART_ZIP", ""},
           {"PART_ORT", ""},     
           {"LSPOS", ""},//**************************************** Lieferscheinpositionen
           {"LS_POS", "Artikel.Pos"},
           {"AUFTRAG_POS", "Artikel.exAuftragPos"},
           {"MATBEZ",""},
           {"MATNR_KUNDE", "Artikel.exMaterialnummer"},
           {"MATNR_LIEF", "Artikel.Materialnummer"},
           {"LSPOS_STATUS",""},
           {"LSPOS_BRUTTO", ""},
           {"LSPOS_ANZAHL", ""},
           {"LSPOS_LAENGE", ""},
           {"ARTK_NO", ""},
           {"ARTK_NOO", ""},
           {"KONTRAKT_NO", ""},
           {"KONTRAKT_POS", ""},
           {"LSPOS_TXT", ""},
           {"TYP", ""},
           {"TXT", "Artikel.Info"},
           {"CHRG", ""},
           {"CHARGE", "Artikel.Charge"},
           {"LS_FAKT", ""},
           {"GEWICHT_BRUTTO", "Artikel.Brutto"},
           {"GEWICHT_NETTO", "Artieke.Netto"},
           {"DICKE", "Artikel.Dicke"},
           {"BREITE", "Artikel.Breite"},           
           {"LAENGE", "Artikel.Laenge"},
           {"HOEHE", "Artikel.Hoehe"},
           {"AUSSENDURCHMESSER",""},
           {"VERSANDSTELLE", ""},
           {"ANZAHL", "Artikel.Anzahl"},
           {"EINHEIT", "Artikel.Einheit"},
           {"VERLDAT",""},
           {"VERLTIME",""},
           {"ENTLDAT", ""},
           {"ENTLTIME",""},
           {"SPERRGRUND",""},
           {"CONTAINER",""},
           {"CHRG_STATUS",""},
           {"SCHMELZNR",""},
           {"TPOS_NO", ""},
           {"ABRP_VSDT",""},
           {"ABRP_ENTLDT", ""},
           {"TPOS_VSDT",""},
           {"AVIS_NO", ""},
           {"TPOS_ENTLDT",""}    
        };
        *****/

        public DataTable GetLfsKopfdaten(ref DataTable dtASN)
        {
            DataTable dtEingang = new DataTable("Eingang");
            clsLEingang Eingang = new clsLEingang();
            dtEingang = clsLEingang.GetLEingangTableColumnSchema(this.GLUser);

            //zusaätziche Felder für die Übersicht 
            dtEingang.Columns.Add("Select", typeof(bool));
            dtEingang.Columns.Add("ASN-Datum", typeof(DateTime));
            dtEingang.Columns.Add("Ref.Auftraggeber", typeof(string));
            dtEingang.Columns.Add("TransportNr", typeof(string));
            dtEingang.Columns.Add("VS-Datum", typeof(DateTime));
            dtEingang.Columns.Add("AuftraggeberView", typeof(string));
            dtEingang.Columns.Add("EmpfaengerView", typeof(string));

            dtEingang.Columns["Select"].SetOrdinal(0);
            dtEingang.Columns["ASN"].SetOrdinal(1);
            dtEingang.Columns["ASN-Datum"].SetOrdinal(2);
            dtEingang.Columns["Ref.Auftraggeber"].SetOrdinal(3);
            dtEingang.Columns["TransportNr"].SetOrdinal(4);
            dtEingang.Columns["VS-Datum"].SetOrdinal(5);
            dtEingang.Columns["AuftraggeberView"].SetOrdinal(6);
            //dtEingang.Columns["EmpfaengerView"].SetOrdinal(0);

            // Loop over pairs with foreach
            clsADRMan adrManTmp = new clsADRMan();
            if (dtASN.Rows.Count > 0)
            {
                //Liste der verschiedenen Eingägne erstellen
                DataTable dtASNID = dtASN.DefaultView.ToTable(true, "ASNID");
                for (Int32 i = 0; i <= dtASNID.Rows.Count - 1; i++)
                {
                    DataRow row = dtEingang.NewRow();

                    string asnIDTmp = dtASNID.Rows[i]["ASNID"].ToString();
                    decimal decTmp = 0;
                    Decimal.TryParse(asnIDTmp, out decTmp);
                    row["Select"] = false;
                    row["ASN"] = decTmp;
                    dtASN.DefaultView.RowFilter = string.Empty;
                    dtASN.DefaultView.RowFilter = "ASNID=" + asnIDTmp;
                    DataTable dtASNValue = dtASN.DefaultView.ToTable();
                    //Table mit den XMLDaten aus der Message

                    bool bTRAN_PART = false;
                    bool bLS_PART = false;
                    for (Int32 x = 0; x <= dtASNValue.Rows.Count - 1; x++)
                    {
                        string knot = dtASNValue.Rows[x]["FieldName"].ToString();
                        string Value = dtASNValue.Rows[x]["Value"].ToString();
                        switch (knot)
                        {
                            case "CREATED":
                                DateTime dtASNDate = DateTime.ParseExact(Value, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture); // CF hh -> HH
                                row["ASN-Datum"] = dtASNDate;
                                break;
                            case "ATG":
                                row["Ref.Auftraggeber"] = Value;
                                break;

                            case "TRAN_NO":
                                row["TransportNr"] = Value;
                                break;

                            case "VSDT":
                                DateTime dtVSDate = DateTime.ParseExact(Value, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                                row["VS-Datum"] = dtVSDate;
                                break;
                            //SpedID
                            case "TRAN_PART":
                                ListAdrMan = new List<clsADRMan>();
                                ADR = new clsADR();
                                adrManTmp = new clsADRMan();
                                adrManTmp.TableName = "LEingang";
                                bTRAN_PART = true;
                                break;
                            //Lieferschein
                            case "VKMT_TYP":
                                switch (Value)
                                {
                                    case "L":
                                        row["WaggonNo"] = string.Empty;
                                        break;
                                    case "B":
                                        row["SpedID"] = 0;
                                        break;
                                    case "S":
                                        break;
                                }
                                break;
                            //Lieferschein
                            case "LS_NO":
                                row["LfsNr"] = Value;
                                break;
                            case "WAGGONNR":
                                row["WaggonNo"] = Value;
                                break;
                            //Lieferschein Adressen - Auftraggeber / EMpfänger / Verbrauchter
                            case "LS_PART":
                                ADR = new clsADR();
                                adrManTmp = new clsADRMan();
                                adrManTmp.TableName = "LEingang";
                                bLS_PART = true;
                                break;
                            case "PART_EXT":
                                break;
                            case "PART_TYP":
                                //Transporteur
                                if (bTRAN_PART)
                                {
                                    //Verweis ist in der kommenden Row zu finden
                                    if (dtASNValue.Rows[x + 1]["Value"] != null)
                                    {
                                        string strNextValue = dtASNValue.Rows[x + 1]["Value"].ToString();
                                        ADR = new clsADR();
                                        ADR._GL_User = this.GLUser;
                                        ADR.Verweis = strNextValue;
                                        ADR.GetADRByVerweis();
                                    }
                                    string strArt = string.Empty;
                                    switch (Value)
                                    {
                                        case "TU":
                                            if (ADR.ID > 0)
                                            {
                                                row["SpedID"] = ADR.ID;
                                            }
                                            else
                                            {
                                                adrManTmp.AdrArtID = 5;
                                                adrManTmp.DictAdrArt.TryGetValue(adrManTmp.AdrArtID, out strArt);
                                                adrManTmp.AdrArt = strArt;
                                            }
                                            break;
                                        case "UML":
                                            adrManTmp.AdrArtID = -1;
                                            adrManTmp.DictAdrArt.TryGetValue(adrManTmp.AdrArtID, out strArt);
                                            adrManTmp.AdrArt = strArt;
                                            break;
                                        case "UMLA":
                                            adrManTmp.AdrArtID = -1;
                                            adrManTmp.DictAdrArt.TryGetValue(adrManTmp.AdrArtID, out strArt);
                                            adrManTmp.AdrArt = strArt;
                                            break;
                                    }
                                }
                                //Lieferschein adressen
                                if (bLS_PART)
                                {
                                    //Verweis ist in der kommenden Row zu finden
                                    if (dtASNValue.Rows[x + 1]["Value"] != null)
                                    {
                                        string strNextValue = dtASNValue.Rows[x + 1]["Value"].ToString();
                                        ADR = new clsADR();
                                        ADR._GL_User = this.GLUser;
                                        ADR.Verweis = strNextValue;
                                        ADR.GetADRByVerweis();
                                    }
                                    string strArt = string.Empty;
                                    switch (Value)
                                    {
                                        case "ABS":
                                            if (ADR.ID > 0)
                                            {
                                                row["Auftraggeber"] = ADR.ID;
                                                row["AuftraggeberView"] = ADR.Name1;
                                            }
                                            else
                                            {
                                                adrManTmp.AdrArtID = 0;
                                                adrManTmp.DictAdrArt.TryGetValue(adrManTmp.AdrArtID, out strArt);
                                                adrManTmp.AdrArt = strArt;
                                            }
                                            break;
                                        case "EMP":

                                            if (ADR.ID > 0)
                                            {
                                                row["Empfaenger"] = ADR.ID;
                                                row["EmpfaengerView"] = ADR.Name1;
                                            }
                                            else
                                            {
                                                adrManTmp.AdrArtID = 3;
                                                adrManTmp.DictAdrArt.TryGetValue(adrManTmp.AdrArtID, out strArt);
                                                adrManTmp.AdrArt = strArt;
                                            }
                                            break;
                                        case "VBR":
                                        case "LIE":
                                        case "LB":
                                        case "FZ":
                                            adrManTmp.AdrArtID = -1;
                                            adrManTmp.DictAdrArt.TryGetValue(adrManTmp.AdrArtID, out strArt);
                                            adrManTmp.AdrArt = strArt;
                                            break;
                                    }
                                }
                                break;
                            case "PART_NAME1":
                                adrManTmp.Name1 = Value;
                                break;
                            case "PART_NAME2":
                                adrManTmp.Name2 = Value;
                                break;
                            case "PART_NAME3":
                                adrManTmp.Name3 = Value;
                                break;
                            case "PART_STRASSE":
                                adrManTmp.Str = Value;
                                break;
                            case "PART_LKZ":
                                adrManTmp.LKZ = Value;
                                break;
                            case "PART_ZIP":
                                adrManTmp.PLZ = Value;
                                break;
                            case "PART_ORT":
                                adrManTmp.PLZ = Value;
                                //letzte Eintrag also cls zur Liste hinzufügen
                                if (bLS_PART)
                                {
                                    ListAdrMan.Add(adrManTmp);
                                    bLS_PART = false;
                                }
                                if (bTRAN_PART)
                                {
                                    ListAdrMan.Add(adrManTmp);
                                    bTRAN_PART = false;
                                }
                                break;
                        }

                        //im letzten Durchlauf die Row der Talbe EIngang hinzufügen
                        if (x == dtASNValue.Rows.Count - 1)
                        {
                            dtEingang.Rows.Add(row);
                        }
                    }
                }
            }
            return dtEingang;
        }

        public DataTable GetArtikelDaten(ref DataTable dtASN)
        {
            clsADRMan adrManTmp = new clsADRMan();

            DataTable dtArtikel = new DataTable();
            dtArtikel = clsArtikel.GetDataTableArtikelSchema(this.GLUser);
            dtArtikel.Columns.Add("ASN", typeof(decimal));

            Int32 j = 0;
            dtArtikel.Columns["ASN"].SetOrdinal(j);
            j++;
            dtArtikel.Columns["Position"].SetOrdinal(j);
            j++;
            dtArtikel.Columns["Charge"].SetOrdinal(j);
            j++;
            dtArtikel.Columns["Dicke"].SetOrdinal(j);
            j++;
            dtArtikel.Columns["Breite"].SetOrdinal(j);
            j++;
            dtArtikel.Columns["Laenge"].SetOrdinal(j);
            j++;
            dtArtikel.Columns["Hoehe"].SetOrdinal(j);
            j++;
            dtArtikel.Columns["Netto"].SetOrdinal(j);
            j++;
            dtArtikel.Columns["Brutto"].SetOrdinal(j);
            j++;
            dtArtikel.Columns["Anzahl"].SetOrdinal(j);
            j++;
            dtArtikel.Columns["Einheit"].SetOrdinal(j);
            j++;
            dtArtikel.Columns["exAuftrag"].SetOrdinal(j);
            j++;
            dtArtikel.Columns["exAuftragPos"].SetOrdinal(j);
            j++;
            dtArtikel.Columns["exMaterialnummer"].SetOrdinal(j);


            if (dtASN.Rows.Count > 0)
            {
                //Liste der verschiedenen Eingägne erstellen
                DataTable dtASNID = dtASN.DefaultView.ToTable(true, "ASNID");
                for (Int32 i = 0; i <= dtASNID.Rows.Count - 1; i++)
                {
                    DataRow row = dtArtikel.NewRow();

                    string asnIDTmp = dtASNID.Rows[i]["ASNID"].ToString();
                    decimal decTmp = 0;
                    Decimal.TryParse(asnIDTmp, out decTmp);
                    row["ASN"] = decTmp;
                    dtASN.DefaultView.RowFilter = string.Empty;
                    dtASN.DefaultView.RowFilter = "ASNID=" + asnIDTmp;
                    DataTable dtASNValue = dtASN.DefaultView.ToTable();
                    //Table mit den XMLDaten aus der Message
                    Int32 iTmp = 0;
                    bool bTRAN_PART = false;
                    bool bLS_PART = false;
                    for (Int32 x = 0; x <= dtASNValue.Rows.Count - 1; x++)
                    {
                        string knot = dtASNValue.Rows[x]["FieldName"].ToString();
                        string Value = dtASNValue.Rows[x]["Value"].ToString();
                        switch (knot)
                        {
                            case "AUFTRAG_NOO":
                                row["exAuftrag"] = Value;
                                break;
                            case "LS_PART":
                                bLS_PART = true;
                                break;
                            case "PART_EXT":
                                break;
                            case "PART_TYP":
                                //Lieferschein adressen
                                if (bLS_PART)
                                {
                                    string strArt = string.Empty;
                                    switch (Value)
                                    {
                                        case "ABS":
                                            bLS_PART = false;
                                            break;
                                        case "EMP":
                                            bLS_PART = false;
                                            break;
                                        case "VBR":
                                            bLS_PART = true;
                                            break;
                                        case "LIE":
                                        case "LB":
                                        case "FZ":
                                            bLS_PART = false;
                                            break;
                                    }
                                }
                                break;
                            case "PART_NAME1":
                                if (bLS_PART)
                                {
                                    row["ASNVerbraucher"] = Value + Environment.NewLine;
                                }
                                break;
                            case "PART_NAME2":
                                if (bLS_PART)
                                {
                                    row["ASNVerbraucher"] = row["ASNVerbraucher"] + Value + Environment.NewLine;
                                }
                                break;
                            case "PART_NAME3":
                                if (bLS_PART)
                                {
                                    row["ASNVerbraucher"] = row["ASNVerbraucher"] + Value + Environment.NewLine;
                                }
                                break;
                            case "PART_STRASSE":
                                if (bLS_PART)
                                {
                                    row["ASNVerbraucher"] = row["ASNVerbraucher"] + Value + Environment.NewLine;
                                }
                                break;
                            case "PART_ZIP":
                                if (bLS_PART)
                                {
                                    row["ASNVerbraucher"] = row["ASNVerbraucher"] + " - " + Value;
                                }
                                break;
                            case "PART_ORT":
                                if (bLS_PART)
                                {
                                    row["ASNVerbraucher"] = row["ASNVerbraucher"] + " " + Value;
                                }
                                break;
                            case "LS_POS":
                                row["Position"] = Value;
                                break;
                            case "AUFTRAG_POS":
                                row["exAuftragPos"] = Value;
                                break;
                            case "MATNR_KUNDE":
                                row["exMaterialnummer"] = Value;
                                break;
                            case "CHARGE":
                                row["CHARGE"] = Value;
                                break;
                            case "GEWICHT_BRUTTO":
                                decTmp = 0;
                                Decimal.TryParse(Value, out decTmp);
                                row["Brutto"] = decTmp;
                                break;
                            case "GEWICHT_NETTO":
                                decTmp = 0;
                                Decimal.TryParse(Value, out decTmp);
                                row["Netto"] = decTmp;
                                break;
                            case "DICKE":
                                decTmp = 0;
                                Decimal.TryParse(Value, out decTmp);
                                row["Dicke"] = decTmp;
                                break;
                            case "BREITE":
                                decTmp = 0;
                                Decimal.TryParse(Value, out decTmp);
                                row["Breite"] = decTmp;
                                break;
                            case "LAENGE":
                                decTmp = 0;
                                Decimal.TryParse(Value, out decTmp);
                                row["Laenge"] = decTmp;
                                break;
                            case "HOEHE":
                                decTmp = 0;
                                Decimal.TryParse(Value, out decTmp);
                                row["Hoehe"] = decTmp;
                                break;
                            case "ANZAHL":
                                iTmp = 0;
                                Int32.TryParse(Value, out iTmp);
                                row["Anzahl"] = iTmp;
                                break;
                            case "EINHEIT":
                                row["Einheit"] = "kg";
                                break;
                        }
                        //im letzten Durchlauf die Row der Talbe EIngang hinzufügen
                        if (x == dtASNValue.Rows.Count - 1)
                        {
                            dtArtikel.Rows.Add(row);
                        }
                    }
                }
            }
            return dtArtikel;
        }








    }
}
