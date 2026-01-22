using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace LVS.Uniport
{
    public class clsUniPort
    {
        public clsLagerdaten Lager;
        public Globals._GL_USER GLUser;
        public Globals._GL_SYSTEM GLSystem;
        public const string const_Mandant = "HONS";
        public const string const_Receiver = "UNIPORT";
        public const string const_Task_ENTL = "ENTL";
        public const string const_Task_VERL = "VERL";

        public decimal ANSID { get; set; }    //ID der neuen ENTL Meldung
        public string Mandant { get; set; }
        public string Created { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Task { get; set; }
        public string TaskNo { get; set; }
        public string ATG { get; set; }
        public string TranExt { get; set; }

        public string LfsNo { get; set; }
        public string AuftragNoo { get; set; }
        public string LfsPos { get; set; }
        public string AuftragPos { get; set; }
        public string Charge { get; set; }
        public string EntlDatum { get; set; }
        public string EntlTime { get; set; }
        public string VerlDatum { get; set; }
        public string VerlTime { get; set; }
        public string KFZ { get; set; }
        public string WaggonNr { get; set; }
        public string BahnOrLKW { get; set; }
        public string LVSRef { get; set; }  //ArtikleID+/+LvsNR

        public DataTable dtAdrMan_Sped = new DataTable();
        clsADR ADR = new clsADR();
        List<clsADRMan> ListAdrMan = new List<clsADRMan>();
        /********************************************************
         *       Methoden / Prozeduren
         * *****************************************************/
        ///<summary>clsUniPort / Fill</summary>
        ///<remarks></remarks>
        public List<clsASNValue> Fill(clsXmlMessages myXmlMess, clsXmlStructure myXmlStruct, clsASN myASN)
        {
            DataTable dtLMStruct = new DataTable();
            dtLMStruct.Columns.Add("Nr", typeof(Int32));
            dtLMStruct.Columns.Add("xmlTxt", typeof(string));
            dtLMStruct.Columns.Add("Value", typeof(string));

            DataTable dtArtikel = new DataTable();
            DataTable dtEingang = new DataTable();
            DataTable dtAusgang = new DataTable();
            if (
                (myXmlStruct != null) &
                (myXmlMess != null) &
                (myASN != null)
               )
            {
                this.GLUser = myASN._GL_User;
                Lager = new clsLagerdaten();
                Lager.InitClass(this.GLUser, this.GLSystem);

                Dictionary<decimal, clsXmlStructure> dictXmlStructTmp = new Dictionary<decimal, clsXmlStructure>();
                //ID -> ID aus ASN Table ermittlen

                //Mandant
                this.Mandant = const_Mandant;
                //Created
                this.Created = DateTime.Now.ToString("yyyyMMddHHmmss");
                //Sender
                this.Sender = const_Mandant;
                //Receiver
                this.Receiver = const_Receiver;
                //Task
                //Task No
                switch (myASN.Queue.ASNTyp.Typ)
                {
                    case "EML":
                        //dies heißt bei Honselmann ENLT
                        dtArtikel.Clear();
                        dtEingang.Clear();
                        dtAusgang.Clear();
                        dtArtikel = Lager.GetArtikelDatenFromLVS(myASN.Queue.TableName, myASN.Queue.TableID);
                        dtEingang = Lager.GetLEingangDatenFromLVS(myASN.Queue.TableName, myASN.Queue.TableID);
                        dictXmlStructTmp = myXmlStruct.dictEM;
                        this.Task = const_Task_ENTL;
                        this.TaskNo = "41";
                        break;
                    case "AML":
                        //dies heißt bei Honselmann VERL
                        dtArtikel.Clear();
                        dtEingang.Clear();
                        dtAusgang.Clear();
                        dtArtikel = Lager.GetArtikelDatenFromLVS(myASN.Queue.TableName, myASN.Queue.TableID);
                        dtEingang = Lager.GetLEingangDatenFromLVS(myASN.Queue.TableName, myASN.Queue.TableID);
                        dtAusgang = Lager.GetLAusgangDatenFromLVS(myASN.Queue.TableName, myASN.Queue.TableID);
                        dictXmlStructTmp = myXmlStruct.dictAM;
                        this.Task = const_Task_VERL;
                        this.TaskNo = "80";
                        break;
                    default:
                        this.TaskNo = "0";
                        break;
                }
                // ATG asu ASN
                this.ATG = string.Empty;
                DataTable dtASN = clsASNValue.GetASNValueDataTableByASNId(this.GLUser.User_ID, myASN.Queue.ASNID);
                for (Int32 i = 0; i <= dtASN.Rows.Count - 1; i++)
                {
                    string strFieldName = dtASN.Rows[i]["FieldName"].ToString();
                    switch (strFieldName)
                    {
                        case "ATG":
                            this.ATG = dtASN.Rows[i]["Value"].ToString();
                            break;
                            // case "TRAN_EXT":
                            //     this.TranExt = dtASN.Rows[i]["Value"].ToString();
                            //    break;
                    }
                }

                //TRAN_EX
                //ENTLDAT / ENTLTIME
                //LS_NO
                //AUFTRAG_NOO
                for (Int32 i = 0; i <= dtEingang.Rows.Count - 1; i++)
                {
                    this.TranExt = dtEingang.Rows[i]["ExTransportRef"].ToString();
                    DateTime dtTmp = (DateTime)dtEingang.Rows[i]["Date"];
                    this.EntlDatum = dtTmp.Date.ToString("yyyyMMdd");
                    this.EntlTime = dtTmp.ToString("hhmm");

                    if (myASN.Queue.ASNTyp.Typ == "EML")
                    {
                        this.LfsNo = dtEingang.Rows[i]["LfsNr"].ToString();
                        //this.LfsPos = dtArtikel.Rows[0]["ExLsPosA"].ToString();
                        this.AuftragNoo = dtArtikel.Rows[0]["exAuftrag"].ToString();
                    }
                }
                for (Int32 i = 0; i <= dtAusgang.Rows.Count - 1; i++)
                {
                    DateTime dtTmp = (DateTime)dtAusgang.Rows[i]["Datum"];
                    this.VerlDatum = dtTmp.Date.ToString("yyyyMMdd");
                    this.VerlTime = dtTmp.ToString("hhmm");
                    this.TranExt = dtAusgang.Rows[i]["ExTransportRef"].ToString();



                    //this.AuftragNoo = dtAusgang.Rows[i]["ExAuftragRef"].ToString();
                    if (dtAusgang.Rows[i]["KFZ"].ToString() != string.Empty)
                    {
                        this.KFZ = dtAusgang.Rows[i]["KFZ"].ToString();
                        this.BahnOrLKW = "L";
                    }
                    if (dtAusgang.Rows[i]["WaggonNo"].ToString() != string.Empty)
                    {
                        this.WaggonNr = dtAusgang.Rows[i]["WaggonNo"].ToString();
                        this.BahnOrLKW = "B";
                    }
                    if (myASN.Queue.ASNTyp.Typ == "AML")
                    {
                        //this.LfsNo = dtAusgang.Rows[i]["LfsLieferant"].ToString();
                        this.LfsNo = dtArtikel.Rows[0]["exLsNoA"].ToString();
                        this.LfsPos = dtArtikel.Rows[0]["ExLsPosA"].ToString();
                        this.AuftragNoo = dtArtikel.Rows[0]["exAuftrag"].ToString();
                    }
                }
                Int32 iCount = 1;
                //Füllen der Table dtLMStruct mit Daten, hieraus wird dann die Meldung erzeugt
                Int32 iCountLsNo = 0;
                foreach (var item in dictXmlStructTmp)
                {
                    DataRow row = dtLMStruct.NewRow();
                    clsXmlStructure xmlStructTmp = (clsXmlStructure)item.Value;
                    string stXmlTxt = xmlStructTmp.XmlTxt;

                    switch (stXmlTxt)
                    {
                        case "ID":
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = ANSID.ToString();
                            dtLMStruct.Rows.Add(row);
                            iCount++;
                            break;
                        case "MANDANT":
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = Mandant;
                            dtLMStruct.Rows.Add(row);
                            iCount++;
                            break;
                        case "CREATED":
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = Created;
                            dtLMStruct.Rows.Add(row);
                            iCount++;
                            break;
                        case "SENDER":
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = Sender;
                            dtLMStruct.Rows.Add(row);
                            iCount++;
                            break;
                        case "RECEIVER":
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = Receiver;
                            dtLMStruct.Rows.Add(row);
                            iCount++;
                            break;
                        case "TASK":
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = Task;
                            dtLMStruct.Rows.Add(row);
                            iCount++;
                            break;
                        case "TASK_NO":
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = TaskNo.ToString();
                            dtLMStruct.Rows.Add(row);
                            iCount++;
                            break;
                        case "ATG":
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = ATG;
                            dtLMStruct.Rows.Add(row);
                            iCount++;
                            break;
                        case "TRAN_EXT":
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = TranExt;
                            dtLMStruct.Rows.Add(row);
                            iCount++;
                            break;
                        case "VKMT_KZ":
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = KFZ;
                            dtLMStruct.Rows.Add(row);
                            iCount++;
                            break;
                        case "VKMT_TYP":
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = BahnOrLKW;
                            dtLMStruct.Rows.Add(row);
                            iCount++;
                            break;
                        case "LS_NO":
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = LfsNo;
                            dtLMStruct.Rows.Add(row);

                            iCount++;
                            break;
                        case "AUFTRAG_NOO":
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = AuftragNoo;
                            dtLMStruct.Rows.Add(row);
                            iCount++;
                            break;
                        case "WAGGONNR":
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = WaggonNr;
                            dtLMStruct.Rows.Add(row);
                            iCount++;
                            break;
                        case "LS_POS":
                        case "AUFTRAG_POS":
                        case "CHRG":
                        case "ENTLDAT":
                        case "ENTLTIME":
                        case "VERLDAT":
                        case "VERLTIME":
                        case "CHARGE_NOO":
                            //die Artikel Daten werden für alle dann unter CHARGE erstellt und eingetragen
                            break;
                        case "CHARGE":
                            //Ab hier muss jetzt der Lieferschein / die Artikel durchlaufen werden 
                            for (Int32 x = 0; x <= dtArtikel.Rows.Count - 1; x++)
                            {
                                if (x == 0)
                                {
                                    //LS_POS
                                    row = dtLMStruct.NewRow();
                                    this.LfsPos = string.Empty;
                                    if (myASN.Queue.ASNTyp.Typ == "EML")
                                        this.LfsPos = dtArtikel.Rows[x]["Position"].ToString();
                                    else
                                    {
                                        this.LfsPos = dtArtikel.Rows[x]["exLsPosA"].ToString(); // CF Verwenden des neuen Feldes
                                    }
                                    row["Nr"] = iCount;
                                    row["xmlTxt"] = "LS_POS";
                                    row["Value"] = this.LfsPos;
                                    dtLMStruct.Rows.Add(row);
                                    iCount++;

                                    //AUFTRAG_POS
                                    row = dtLMStruct.NewRow();
                                    this.AuftragPos = string.Empty;
                                    this.AuftragPos = dtArtikel.Rows[x]["exAuftragPos"].ToString();
                                    row["Nr"] = iCount;
                                    row["xmlTxt"] = "AUFTRAG_POS";
                                    row["Value"] = this.AuftragPos;
                                    dtLMStruct.Rows.Add(row);
                                    iCount++;
                                }
                                //CHRG
                                row = dtLMStruct.NewRow();
                                row["Nr"] = iCount;
                                row["xmlTxt"] = "CHRG";
                                row["Value"] = string.Empty;
                                dtLMStruct.Rows.Add(row);
                                iCount++;

                                //CHARGE
                                row = dtLMStruct.NewRow();
                                this.Charge = string.Empty;
                                this.Charge = dtArtikel.Rows[x]["Charge"].ToString();
                                row["Nr"] = iCount;
                                row["xmlTxt"] = "CHARGE";
                                row["Value"] = Charge;
                                dtLMStruct.Rows.Add(row);
                                iCount++;

                                if (myASN.Queue.ASNTyp.Typ == "EML")
                                {
                                    //ENTLDAT
                                    row = dtLMStruct.NewRow();
                                    row["Nr"] = iCount;
                                    row["xmlTxt"] = "ENTLDAT";
                                    row["Value"] = this.EntlDatum;
                                    dtLMStruct.Rows.Add(row);
                                    iCount++;

                                    //ENTLTIME
                                    row = dtLMStruct.NewRow();
                                    row["Nr"] = iCount;
                                    row["xmlTxt"] = "ENTLTIME";
                                    row["Value"] = this.EntlTime;
                                    dtLMStruct.Rows.Add(row);
                                    iCount++;

                                    //ArtikelID/LVSNR
                                    row = dtLMStruct.NewRow();
                                    this.LVSRef = string.Empty;
                                    this.LVSRef = dtArtikel.Rows[x]["ID"].ToString() + "/" + dtArtikel.Rows[x]["LVS_ID"].ToString();
                                    row["Nr"] = iCount;
                                    row["xmlTxt"] = "CHARGE_NOO";
                                    row["Value"] = LVSRef;
                                    dtLMStruct.Rows.Add(row);
                                    iCount++;
                                }

                                if (myASN.Queue.ASNTyp.Typ == "AML")
                                {
                                    //VERLDAT
                                    row = dtLMStruct.NewRow();
                                    row["Nr"] = iCount;
                                    row["xmlTxt"] = "VERLDAT";
                                    row["Value"] = this.VerlDatum;
                                    dtLMStruct.Rows.Add(row);
                                    iCount++;

                                    //VERLTIME
                                    row = dtLMStruct.NewRow();
                                    row["Nr"] = iCount;
                                    row["xmlTxt"] = "VERLTIME";
                                    row["Value"] = this.VerlTime;
                                    dtLMStruct.Rows.Add(row);
                                    //iCount++;
                                }
                                //Nach jedem Artikel muss <CHRG> neu gesetzt werden
                                if (
                                        (x > 0) &
                                        (x < dtArtikel.Rows.Count)
                                  )
                                {
                                    //row = dtLMStruct.NewRow();
                                    //row["Nr"] = iCount;
                                    //row["xmlTxt"] = "CHRG";
                                    //row["Value"] = string.Empty;
                                    //dtLMStruct.Rows.Add(row);
                                    //iCount++;
                                }
                            }

                            break;
                        default:
                            row["Nr"] = iCount;
                            row["xmlTxt"] = stXmlTxt;
                            row["Value"] = string.Empty;
                            dtLMStruct.Rows.Add(row);
                            iCount++;
                            break;
                    }

                }
            }
            List<clsASNValue> listLM = new List<clsASNValue>();
            //Table in LIST Schreiben
            for (Int32 i = 0; i <= dtLMStruct.Rows.Count - 1; i++)
            {
                clsASNValue tmpValue = new clsASNValue();
                tmpValue.GL_User = this.GLUser;
                tmpValue.ASNID = 0;
                tmpValue.ASNFieldID = 0;
                tmpValue.FieldName = dtLMStruct.Rows[i]["xmlTxt"].ToString();
                tmpValue.Value = dtLMStruct.Rows[i]["Value"].ToString();
                listLM.Add(tmpValue);
            }
            return listLM;
        }
        ///<summary>clsUniPort / WriteUniPortXML_ENTL</summary>
        ///<remarks></remarks>
        public void WriteUniPortXML_VERL(DataTable dtXML, string FilePath)
        {
            if (dtXML.Rows.Count > 0)
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", null, null);
                dec.Encoding = "ISO-8859-2";
                XmlElement root = xmlDoc.DocumentElement;
                xmlDoc.InsertBefore(dec, root);

                XmlNode myRoot
                        , nodeHeader
                        , nodeTransport
                        , nodeLS
                        , nodeLS_NO
                        , nodeAUFTRAG_NOO
                        , nodeWAGGONNR
                        , nodeTRAN_EXT
                        , nodeVKMT_KZ
                        , nodeVKMT_TYP
                        , nodeLSPOS
                        , nodeCHARG
                        , nodeChild
                        , nodeLS_POS
                        , nodeAUFTRAG_POS;


                myRoot = xmlDoc.CreateElement("UNIPORT");
                xmlDoc.AppendChild(myRoot);
                nodeHeader = xmlDoc.CreateElement("HEADER");
                myRoot.AppendChild(nodeHeader);
                nodeTransport = xmlDoc.CreateElement("TRANSPORT");
                myRoot.AppendChild(nodeTransport);
                nodeTRAN_EXT = xmlDoc.CreateElement("TRAN_EXT");
                nodeTransport.AppendChild(nodeTRAN_EXT);
                nodeVKMT_KZ = xmlDoc.CreateElement("VKMT_KZ");
                nodeTransport.AppendChild(nodeVKMT_KZ);
                nodeVKMT_TYP = xmlDoc.CreateElement("VKMT_TYP");
                nodeTransport.AppendChild(nodeVKMT_TYP);
                nodeLS = xmlDoc.CreateElement("LS");
                nodeTransport.AppendChild(nodeLS);
                nodeLS_NO = xmlDoc.CreateElement("LS_NO");
                nodeLS.AppendChild(nodeLS_NO);
                nodeAUFTRAG_NOO = xmlDoc.CreateElement("AUFTRAG_NOO");
                nodeLS.AppendChild(nodeAUFTRAG_NOO);
                nodeWAGGONNR = xmlDoc.CreateElement("WAGGONNR");
                nodeLS.AppendChild(nodeWAGGONNR);
                nodeLSPOS = xmlDoc.CreateElement("LSPOS");
                nodeLS.AppendChild(nodeLSPOS);
                nodeLS_POS = xmlDoc.CreateElement("LS_POS");
                nodeLSPOS.AppendChild(nodeLS_POS);
                nodeAUFTRAG_POS = xmlDoc.CreateElement("AUFTRAG_POS");
                nodeLSPOS.AppendChild(nodeAUFTRAG_POS);
                nodeCHARG = xmlDoc.CreateElement("CHRG");
                nodeLSPOS.AppendChild(nodeCHARG);



                for (Int32 i = 0; i <= dtXML.Rows.Count - 1; i++)
                {
                    string strXmlTxt = dtXML.Rows[i]["FieldName"].ToString();
                    string strValue = dtXML.Rows[i]["Value"].ToString();
                    switch (strXmlTxt)
                    {
                        case "ID":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "MANDANT":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "CREATED":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "SENDER":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "RECEIVER":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "TASK":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "TASK_NO":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "ATG":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "TRAN_EXT":
                            nodeTRAN_EXT.InnerText = strValue;
                            break;
                        case "VKMT_KZ":
                            nodeVKMT_KZ.InnerText = strValue;
                            break;
                        case "VKMT_TYP":
                            nodeVKMT_TYP.InnerText = strValue;
                            break;
                        case "LS":
                            //nodeLS = xmlDoc.CreateElement(strXmlTxt);
                            //nodeTransport.AppendChild(nodeLS);
                            break;
                        case "LS_NO":
                            nodeLS_NO.InnerText = strValue;
                            break;
                        case "AUFTRAG_NOO":
                            nodeAUFTRAG_NOO.InnerText = strValue;
                            break;
                        case "WAGGONNR":
                            nodeWAGGONNR.InnerText = strValue;
                            break;
                        case "LSPOS":
                            //nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            //.AppendChild(nodeChild);
                            break;
                        case "LS_POS":
                            nodeLS_POS.InnerText = strValue;
                            break;
                        case "AUFTRAG_POS":
                            string stTmp = FillValueWithstringToLenth("0", strValue, 3, true);
                            nodeAUFTRAG_POS.InnerText = stTmp;
                            break;
                        case "CHARGE":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeCHARG.AppendChild(nodeChild);
                            break;
                        case "VERLDAT":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeCHARG.AppendChild(nodeChild);
                            break;
                        case "VERLTIME":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeCHARG.AppendChild(nodeChild);
                            break;
                        case "CHARGE_NOO":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeCHARG.AppendChild(nodeChild);
                            break;


                    }
                }
                try
                {
                    xmlDoc.Save(@FilePath);
                }
                catch (Exception ex)
                {

                }

            }
        }
        ///<summary>clsUniPort / WriteUniPortXML_ENTL</summary>
        ///<remarks></remarks>
        public void WriteUniPortXML_ENTL(DataTable dtXML, string FilePath)
        {
            if (dtXML.Rows.Count > 0)
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", null, null);
                dec.Encoding = "ISO-8859-2";
                XmlElement root = xmlDoc.DocumentElement;
                xmlDoc.InsertBefore(dec, root);

                XmlNode myRoot
                        , nodeHeader
                        , nodeTransport
                        , nodeLS
                        , nodeLS_NO
                        , nodeAUFTRAG_NOO
                        , nodeTRAN_EXT
                        , nodeLSPOS
                        , nodeCHRG
                        , nodeChild
                        , nodeLS_POS
                        , nodeAUFTRAG_POS;

                myRoot = xmlDoc.CreateElement("UNIPORT");
                xmlDoc.AppendChild(myRoot);
                nodeHeader = xmlDoc.CreateElement("HEADER");
                myRoot.AppendChild(nodeHeader);
                nodeTransport = xmlDoc.CreateElement("TRANSPORT");
                myRoot.AppendChild(nodeTransport);
                nodeTRAN_EXT = xmlDoc.CreateElement("TRAN_EXT");
                nodeTransport.AppendChild(nodeTRAN_EXT);
                nodeLS = xmlDoc.CreateElement("LS");
                nodeTransport.AppendChild(nodeLS);
                nodeLS_NO = xmlDoc.CreateElement("LS_NO");
                nodeLS.AppendChild(nodeLS_NO);
                nodeAUFTRAG_NOO = xmlDoc.CreateElement("AUFTRAG_NOO");
                nodeLS.AppendChild(nodeAUFTRAG_NOO);
                nodeLSPOS = xmlDoc.CreateElement("LSPOS");
                nodeLS.AppendChild(nodeLSPOS);
                nodeLS_POS = xmlDoc.CreateElement("LS_POS");
                nodeLSPOS.AppendChild(nodeLS_POS);
                nodeAUFTRAG_POS = xmlDoc.CreateElement("AUFTRAG_POS");
                nodeLSPOS.AppendChild(nodeAUFTRAG_POS);

                dtXML.DefaultView.RowFilter = "FieldName='CHRG'";
                DataTable dtTmp = dtXML.DefaultView.ToTable();
                Int32 iArtAnzahl = dtTmp.Rows.Count;
                dtXML.DefaultView.RowFilter = string.Empty;
                List<XmlNode> listNodeCHRG = new List<XmlNode>();
                for (Int32 x = 0; x <= iArtAnzahl - 1; x++)
                {
                    nodeCHRG = xmlDoc.CreateElement("CHRG");
                    //nodeLSPOS.AppendChild(nodeCHRG);
                    listNodeCHRG.Add(nodeCHRG);
                }

                Int32 iCountCreateCHRGNode = 0;
                for (Int32 i = 0; i <= dtXML.Rows.Count - 1; i++)
                {
                    string strXmlTxt = dtXML.Rows[i]["FieldName"].ToString();
                    string strValue = dtXML.Rows[i]["Value"].ToString();
                    switch (strXmlTxt)
                    {
                        case "ID":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "MANDANT":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "CREATED":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "SENDER":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "RECEIVER":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "TASK":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "TASK_NO":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "ATG":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            nodeHeader.AppendChild(nodeChild);
                            break;
                        case "TRAN_EXT":
                            nodeTRAN_EXT.InnerText = strValue;
                            break;
                        case "LS":
                            //nodeLS = xmlDoc.CreateElement(strXmlTxt);
                            //nodeTransport.AppendChild(nodeLS);
                            break;
                        case "LS_NO":
                            nodeLS_NO.InnerText = strValue;
                            break;
                        case "AUFTRAG_NOO":
                            nodeAUFTRAG_NOO.InnerText = strValue;
                            break;
                        case "LSPOS":
                            //nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            //.AppendChild(nodeChild);
                            break;
                        case "LS_POS":
                            nodeLS_POS.InnerText = strValue;
                            break;
                        case "AUFTRAG_POS":
                            string stTmp = FillValueWithstringToLenth("0", strValue, 3, true);
                            nodeAUFTRAG_POS.InnerText = stTmp;
                            break;
                        case "CHRG":
                            nodeLSPOS.AppendChild(listNodeCHRG[iCountCreateCHRGNode]);
                            break;
                        case "CHARGE":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            //nodeCHRG.AppendChild(nodeChild);
                            listNodeCHRG[iCountCreateCHRGNode].AppendChild(nodeChild);
                            break;
                        case "ENTLDAT":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            //nodeCHRG.AppendChild(nodeChild);
                            listNodeCHRG[iCountCreateCHRGNode].AppendChild(nodeChild);
                            break;
                        case "ENTLTIME":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            //nodeCHRG.AppendChild(nodeChild);
                            listNodeCHRG[iCountCreateCHRGNode].AppendChild(nodeChild);
                            break;
                        case "CHARGE_NOO":
                            nodeChild = xmlDoc.CreateElement(strXmlTxt);
                            nodeChild.InnerText = strValue;
                            //nodeCHRG.AppendChild(nodeChild);
                            listNodeCHRG[iCountCreateCHRGNode].AppendChild(nodeChild);
                            iCountCreateCHRGNode++;
                            break;
                    }
                }
                try
                {
                    xmlDoc.Save(@FilePath);
                }
                catch (Exception ex)
                {

                }
            }
        }
        ///<summary>clsUniPort / FillValueWithstringToLenth</summary>
        ///<remarks></remarks>
        private string FillValueWithstringToLenth(string StrToFill, string myValue, Int32 myLength, bool myLeft)
        {
            string retVal = myValue;
            while (retVal.Length < myLength)
            {
                if (myLeft)
                {
                    //0 voranstelle
                    retVal = StrToFill + retVal;
                }
                else
                {
                    retVal = retVal + StrToFill;
                }
            }
            return retVal;
        }
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
