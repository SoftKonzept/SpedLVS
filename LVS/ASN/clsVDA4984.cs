using LVS.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace LVS.ASN
{
    public class clsVDA4984
    {
        public const string const_InfoNoVDA = "---";

        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GLSystem;
        public clsSystem Sys;
        internal clsADRVerweis AdrVerweis;
        internal clsGut Gut;

        //public decimal AsnID { get; set; }
        //public DateTime Datum { get; set; }
        //public decimal TransID { get; set; }
        //public decimal TransDaum { get; set; }
        //public string Verweis { get; set; }
        //public decimal Werk { get; set; }
        //public string Werksnummer { get; set; }
        //public string Einheit { get; set; }
        //public decimal VDA4905ReceiverID { get; set; }

        //public DataTable dtHead { get; set; }
        //public DataTable dtQuantity { get; set; }
        //internal List<decimal> ListASNID { get; set; }

        public Dictionary<decimal, clsBSInfoArt> DictBSInfoArt;
        public DataTable dtBSInfoSource { get; set; }
        public DataTable dtSL4984LE { get; set; }

        public event EventHandler EventWorkingReport;
        protected virtual void OnWorkingReportChange(EventArgs e)
        {
            EventHandler handler = EventWorkingReport;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public string WorkingReportText { get; set; }

        /****************************************************************************************
        *                           Procedure / Methoden
        *****************************************************************************************/
        /// <summary>
        ///             InitClass
        /// </summary>
        /// <param name="GLUser"></param>
        /// <param name="myGLSystem"></param>
        /// <param name="mySys"></param>
        public void InitClass(Globals._GL_USER GLUser, Globals._GL_SYSTEM myGLSystem, clsSystem mySys)
        {
            this._GL_User = GLUser;
            this._GLSystem = myGLSystem;
            this.Sys = mySys;
            AdrVerweis = new clsADRVerweis();
            AdrVerweis.InitClass(this._GL_User);
            Gut = new clsGut();
            Gut.InitClass(this._GL_User, this._GLSystem);
        }



        public void GetLiefereinteilungen(decimal myVDA4905ReceiverID)
        {
            //this.VDA4905ReceiverID = myVDA4905ReceiverID;
            //if (VDA4905ReceiverID > 0)
            //{
            //    //Ermitteln des Verweis für 4905
            //    //Select   SUBSTRING(ADRVerweis.Verweis,PATINDEX ( '%#%' , ADRVerweis.Verweis)+1,3) FROM [SIL_COM].[dbo].ADRVerweis  WHERE ASNFileTyp='VDA4905' and SenderAdrID=5)
            //    Dictionary<decimal, clsADRVerweis> dictAdrVerweis = clsADRVerweis.GetAdrVerweis(this._GL_User, myVDA4905ReceiverID, this.Sys.AbBereich.MandantenID, this.Sys.AbBereich.ID);
            //    AdrVerweis = null;
            //    if (dictAdrVerweis.TryGetValue(VDA4905ReceiverID, out AdrVerweis))
            //    {
            //        //Mit dem Verweis kann nun die ASNIDs ermittelt werden für den ZEitraum
            //        GetASNHead(ref AdrVerweis);
            //        GetQuantity();
            //    }
            //}
        }

        ///<summary>clsVDA4905 / LoadBSInfo4905</summary>
        ///<remarks></remarks>
        public void LoadBSInfo4984(bool bChecked, bool InclSPL, bool bActiveGT)
        {
            dtBSInfoSource = new DataTable("BSInfoVDA4984");
            CreateBSInfoTableColumns();
            //1. Ermitteln der Güterartendaten
            this.Gut.FillDictGutVda4095(bActiveGT);
            FillArtikelDataForBSInfo(bChecked, InclSPL);

            //2. Ermitteln der einzelnen VDA4905 zu den Güterarten
            //3. Ermitteln der Lieferanten mit den Beständen als Sub

            for (Int32 i = 0; i <= this.Gut.ListGutIDVDA4905.Count - 1; i++)
            {
                this.WorkingReportText = "[" + i.ToString() + "/" + this.Gut.ListGutIDVDA4905.Count.ToString() + "] Datensätze verarbeitet";
                this.OnWorkingReportChange(EventArgs.Empty);
                DataRow row = dtBSInfoSource.NewRow();

                Decimal decTmp = 0;
                Decimal.TryParse(this.Gut.ListGutIDVDA4905[i].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    decimal decMinBestand = 0;
                    decimal decMenge = 0;
                    decimal decBrutto = 0;
                    clsGut TmpGut;
                    row["Nr"] = decTmp;
                    if (this.Gut.DictGutVda4905.TryGetValue(decTmp, out TmpGut))
                    {
                        if (TmpGut.ViewID.Equals("418"))
                        {
                            string st = string.Empty;
                        }

                        //Füllen Güterartdaten
                        row["Matchcode"] = TmpGut.ViewID.Trim();
                        row["Verweis"] = TmpGut.Verweis.Trim();
                        row["Gut"] = TmpGut.Bezeichnung.Trim();
                        row["Text"] = TmpGut.VDA4905LieferantenInfo;
                        row["Dicke"] = TmpGut.Dicke;
                        row["Breite"] = TmpGut.Breite;
                        row["min.BS"] = TmpGut.MindestBestand;
                        decMinBestand = TmpGut.MindestBestand;

                    }
                    else
                    {
                        row["Matchcode"] = TmpGut.ViewID.Trim();
                        row["Verweis"] = string.Empty;
                        row["Gut"] = string.Empty;
                        row["Text"] = string.Empty;
                        row["Dicke"] = 0;
                        row["Breite"] = 0;
                        row["min.BS"] = 0;
                    }
                    clsBSInfoArt tmpBSInfo;
                    if (this.DictBSInfoArt.TryGetValue(decTmp, out tmpBSInfo))
                    {
                        //Füllen Artikeldaten
                        row["Menge"] = tmpBSInfo.Menge;
                        row["BS Brutto"] = tmpBSInfo.Brutto;
                    }
                    else
                    {
                        row["Menge"] = 0;
                        row["BS Brutto"] = 0;
                    }

                    if (tmpBSInfo != null)
                    {
                        switch (TmpGut.ArtikelArt)
                        {
                            case "Coils":
                                decMenge = tmpBSInfo.Brutto;
                                break;
                            case "Platinen":
                                decMenge = tmpBSInfo.Menge;
                                break;
                            default:
                                decMenge = tmpBSInfo.Brutto;
                                break;
                        }
                    }
                    //Berechnungen
                    //...|DIfferenz ==> Menge - Mindesbestand
                    decimal decDiff = 0;
                    decDiff = decMenge - decMinBestand;
                    Decimal.TryParse(decDiff.ToString(), out decDiff);
                    row["Diff"] = decDiff;
                    //...|Faktor ==> Menge / Mindesbestand
                    decimal decFaktor = 0;
                    if (decMinBestand > 0)
                    {
                        decFaktor = decMenge / decMinBestand;
                    }
                    Decimal.TryParse(decFaktor.ToString(), out decFaktor);
                    row["Faktor"] = decFaktor;

                    //Zeile hinzufügen
                    dtBSInfoSource.Rows.Add(row);
                }
            }
        }
        ///<summary>clsVDA4905 / LoadBSInfo4905</summary>
        ///<remarks></remarks>
        private void CreateBSInfoTableColumns()
        {
            dtBSInfoSource.Clear();
            dtBSInfoSource.Columns.Add("Nr", typeof(decimal));
            dtBSInfoSource.Columns.Add("Matchcode", typeof(string));
            dtBSInfoSource.Columns.Add("Verweis", typeof(string));
            dtBSInfoSource.Columns.Add("Gut", typeof(string));
            dtBSInfoSource.Columns.Add("Text", typeof(string));
            dtBSInfoSource.Columns.Add("Dicke", typeof(decimal));
            dtBSInfoSource.Columns.Add("Breite", typeof(decimal));
            dtBSInfoSource.Columns.Add("Menge", typeof(decimal));
            dtBSInfoSource.Columns.Add("BS Brutto", typeof(decimal));
            dtBSInfoSource.Columns.Add("min.BS", typeof(decimal));
            dtBSInfoSource.Columns.Add("Diff", typeof(decimal));
            dtBSInfoSource.Columns.Add("Faktor", typeof(decimal));
            dtBSInfoSource.Columns.Add("MB", typeof(string));

        }
        ///<summary>clsVDA4905 / FillArtikelDataForBSInfo4905</summary>
        ///<remarks></remarks>
        private void FillArtikelDataForBSInfo(bool bChecked, bool InclSPL)
        {
            DictBSInfoArt = new Dictionary<decimal, clsBSInfoArt>();

            for (Int32 i = 0; i <= this.Gut.ListGutIDVDA4905.Count - 1; i++)
            {
                Int32 iTmpGA = 0;
                Int32.TryParse(this.Gut.ListGutIDVDA4905[i].ToString(), out iTmpGA);
                if (iTmpGA > 0)
                {
                    if (iTmpGA == 22)
                    {
                        string st = string.Empty;
                    }

                    DataTable dt = new DataTable();
                    string strSQL = "Select " +
                                            "g.ID" +
                                            ", CASE " +
                                                "WHEN ISNULL(SUM(a.Anzahl),0)=0 THEN 0 " +
                                                "ELSE SUM(a.Anzahl) " +
                                                "END as Menge" +
                                            ", CASE " +
                                                "WHEN ISNULL(SUM(a.Brutto),0)=0 THEN 0 " +
                                                "ELSE SUM(a.Brutto) " +
                                                "END as Brutto" +
                                            " FROM Artikel a " +
                                                " INNER JOIN Gueterart g ON g.ID=a.GArtID " +
                                                " INNER JOIN LEingang e ON e.ID = a.LEingangTableID " +
                                                " WHERE " +
                                                    " a.LAusgangTableID=0 " +
                                                    " AND e.AbBereich=" + this.Sys.AbBereich.ID +
                                                    //" AND g.ID IN (" + string.Join(",", this.Gut.ListGutIDVDA4905.ToArray()) + ") ";
                                                    " AND g.ID=" + iTmpGA + " ";
                    if (bChecked)
                    {
                        strSQL = strSQL + " AND e.[Check]=1 ";
                    }
                    if (!InclSPL)
                    {
                        strSQL = strSQL + " AND a.ID NOT IN (SELECT DISTINCT d.ArtikelID " +
                                                                            "FROM Sperrlager d " +
                                                                            "WHERE " +
                                                                                "d.BKZ='IN' AND d.SPLIDIn=0 " +
                                                                                "AND d.ID NOT IN (SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0) " +
                                                            ") ";
                    }
                    strSQL = strSQL + " Group BY g.ID " +
                                        " Order BY g.ID";

                    dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, this._GL_User.User_ID, "VDA4984");

                    decimal decMenge = 0;
                    decimal decBurtto = 0;
                    clsBSInfoArt TmpBSInfo = new clsBSInfoArt();
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            decMenge = 0;
                            Decimal.TryParse(row["Menge"].ToString(), out decMenge);
                            decBurtto = 0;
                            Decimal.TryParse(row["Brutto"].ToString(), out decBurtto);
                            TmpBSInfo = new clsBSInfoArt();
                            TmpBSInfo.Menge = decMenge;
                            TmpBSInfo.Brutto = decBurtto;
                            //DictBSInfoArt.Add(decTmp, TmpBSInfo);
                            DictBSInfoArt.Add(iTmpGA, TmpBSInfo);

                            if (decMenge == 0)
                            {
                                string st = string.Empty;

                            }
                        }
                    }
                    else
                    {
                        //kein Bestand mit 0 füllen
                        decMenge = 0;
                        decBurtto = 0;
                        TmpBSInfo = new clsBSInfoArt();
                        TmpBSInfo.Menge = decMenge;
                        TmpBSInfo.Brutto = decBurtto;
                        DictBSInfoArt.Add(iTmpGA, TmpBSInfo);
                    }
                }
            }
        }
        ///<summary>clsVDA4905 / InitSubBSInfoSL</summary>
        ///<remarks></remarks>
        public void InitSubBSInfoSL(DataTable dtSource, bool myRueckStand, bool myEingangCheck, bool myInclSPL)
        {
            dtSL4984LE = new DataTable();
            dtSL4984LE = dtSource.Copy();
            dtSL4984LE.Clear();
            //dtSL4905LE.Columns.Add("GutVerweis", typeof(string));
            dtSL4984LE.Columns.Add("Lieferant", typeof(string));
            dtSL4984LE.Columns.Add("4984", typeof(DateTime));
            dtSL4984LE.Columns.Add("FZ dazu", typeof(Int32));
            dtSL4984LE.Columns.Add("Prüfpunkt", typeof(DateTime));
            dtSL4984LE.Columns.Add("PP FZ dazu", typeof(Int32));
            dtSL4984LE.Columns.Add("FZ Diff", typeof(Int32));
            dtSL4984LE.Columns.Add("Einheit", typeof(string));
            dtSL4984LE.Columns.Add("Bestand", typeof(Int32));
            dtSL4984LE.Columns.Add("Ausgang", typeof(Int32));
            dtSL4984LE.Columns.Add("B+A", typeof(Int32));
            dtSL4984LE.Columns.Add("PP zu IST", typeof(Int32));
            dtSL4984LE.Columns.Add("Faktor SL", typeof(decimal));
            dtSL4984LE.Columns.Add("MB SL", typeof(decimal));
            dtSL4984LE.Columns.Add("Log", typeof(string));
            dtSL4984LE.Columns.Add("LE#", typeof(int));
            dtSL4984LE.Columns.Add("GArt", typeof(clsGut));
            dtSL4984LE.Columns.Add("ediVDA4984Value", typeof(clsEdiVDA4984Value));
            dtSL4984LE.Columns.Add("TableRowNo", typeof(Int32));

            Dictionary<string, clsADRVerweis> DictAdrVerweis = clsADRVerweis.FillDictAdrVerweis(this.Sys.AbBereich.MandantenID, this.Sys.AbBereich.ID, this._GL_User.User_ID, constValue_AsnArt.const_Art_EdifactVDA4984);

            //--- 1. Schleifendurchlauf für die Güterarten
            for (Int32 i = 0; i <= this.Gut.ListGutIDVDA4905.Count - 1; i++)
            {
                // ermitteln der Güterart ID
                Decimal decTmp = 0;

                Decimal.TryParse(this.Gut.ListGutIDVDA4905[i].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsGut TmpGut = new clsGut();
                    TmpGut.InitClass(this._GL_User, this._GLSystem);
                    TmpGut.ID = decTmp;
                    TmpGut.Fill();

                    if (TmpGut.ID.Equals(575))
                    {
                        string str = string.Empty;
                    }

                    DataRow row = dtSL4984LE.NewRow();
                    dtSource.DefaultView.RowFilter = "Verweis='" + TmpGut.Verweis.Trim() + "'";
                    DataTable dtTmp = dtSource.DefaultView.ToTable();

                    FillNewRowFor4984(ref row, dtSL4984LE, dtTmp, true);

                    this.WorkingReportText = "Güterart [" + TmpGut.ViewID + " - Datensatz " + (1 + i).ToString() + " von " + this.Gut.ListGutIDVDA4905.Count.ToString() + "] wird verarbeitet...";
                    this.OnWorkingReportChange(EventArgs.Empty);

                    ////-- ermitteln der Stahllieferanten, für die eine DFÜ innhalb der letzen 217 Tage für diese Güterart vorliegt
                    List<string> listSL = new List<string>();
                    listSL = ListSLofLast217Days(TmpGut.Verweis.Trim(), constValue_AsnArt.const_Art_EdifactVDA4984);

                    if (listSL.Count == 0)
                    {
                        SetRowNoVDA(ref row);
                        row["Log"] = "kein SL hinterlegt";
                    }
                    else
                    {
                        string str = string.Empty;
                    }

                    //-- durchlaufen der Liste der SL in einer Schleife
                    for (Int32 j = 0; j <= listSL.Count - 1; j++)
                    {
                        if (listSL.Count > 1)
                        {
                            string strT = string.Empty;
                        }
                        //init 
                        Int32 iFZ = 0;
                        Int32 iFZDiff = 0;
                        Int32 iRueckStand = 0;
                        DateTime DateLastLE = Globals.DefaultDateTimeMinValue;
                        DateTime dtCheckPoint = Globals.DefaultDateTimeMinValue;

                        string strLieferantenNr = listSL[j].ToString();

                        //  Lieferanten ADRid und Daten anhander der Lieferantennummer ermitteln
                        //- ComTable Job ist die SenderAdrID hinterlegt, die der ASNId zugeordnet werden kann
                        //- Hier ist die Verknüpfung zum AdressVerweis LVSTable ADRVerweis.SenderAdrID=Jobs.SenderAdrID
                        clsADRVerweis AdrVerTmp = new clsADRVerweis();
                        if (DictAdrVerweis.TryGetValue(strLieferantenNr + "#" + this.Sys.Mandant.VDA4905Verweis, out AdrVerTmp))
                        {
                            //-- Lieferatenadresse
                            clsADR AdrTmp = new clsADR();
                            AdrTmp.InitClass(this._GL_User, this._GLSystem, AdrVerTmp.VerweisAdrID, true);
                            row["Lieferant"] = strLieferantenNr + " - [" + AdrTmp.ViewID + "]";
                            row["GArt"] = TmpGut;
                            row["LE#"] = 0;

                            if (TmpGut.Verweis.Trim().Equals("ECE 7G0 704 97"))
                            {
                                string str = string.Empty;
                            }
                            //this.WorkingReportText = "Gut [" + TmpGut.ViewID + "]  (" + (1 + i).ToString() + "/" + this.Gut.ListGutIDVDA4905.Count.ToString() + ")  / SL [" + AdrTmp.ViewID + "] wird verarbeitet...";
                            //this.OnWorkingReportChange(EventArgs.Empty);

                            DataTable dtQTY = GetLELast217Days(TmpGut.Verweis, strLieferantenNr);
                            if (dtQTY.Rows.Count > 0)
                            {
                                //row["4905"]  => Datum letzte Liefereinteilung
                                //row["4905"] = clsVDA4984.const_InfoNoVDA;
                                dtQTY.DefaultView.Sort = "DocDate";
                                string tmpLastDate = dtQTY.DefaultView[0]["DocDate"].ToString();
                                if (DateTime.TryParse(tmpLastDate, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateLastLE))
                                {
                                    //row["4905"] = DateLastLE.ToString("dd.MM.yyyy");
                                    row["4984"] = DateLastLE.ToString("dd.MM.yyyy");
                                    //LE#
                                    int iTmp = 0;
                                    int.TryParse(dtQTY.DefaultView[0]["DocNo"].ToString(), out iTmp);
                                    row["LE#"] = iTmp;
                                }
                                //row["FZ dazu"]  => FZZ zur letzte Liefereinteilung
                                iFZ = 0;
                                row["FZ dazu"] = iFZ;
                                if (int.TryParse(dtQTY.DefaultView[0]["EfzQTY"].ToString(), out iFZ))
                                {
                                    row["FZ dazu"] = iFZ;
                                }
                                dtQTY.DefaultView.Sort = string.Empty;
                                dtQTY.DefaultView.RowFilter = string.Empty;

                                dtQTY.DefaultView.Sort = "DeliveryDate";
                                foreach (DataRow r in dtQTY.Rows)
                                {
                                    int iTmp = 0;
                                    string strTmp = r["DeliveryQTY"].ToString();
                                    int.TryParse(strTmp, out iTmp);
                                    iFZ = iFZ + iTmp;

                                    string tmpCheckDate = r["DeliveryDate"].ToString();
                                    if (DateTime.TryParse(tmpCheckDate, CultureInfo.CurrentCulture, DateTimeStyles.None, out dtCheckPoint))
                                    {
                                        if (dtCheckPoint > DateTime.Now)
                                        {
                                            row["Prüfpunkt"] = dtCheckPoint.ToString("dd.MM.yyyy");

                                            //row["PP FZ dazu"] => FZZ zum Prüfpunkt;
                                            row["PP FZ dazu"] = iFZ + iRueckStand;

                                            //row["FZ Diff"] => FZZ zum Prüfpunkt;
                                            iFZDiff = ((Int32)row["FZ dazu"] - (Int32)row["PP FZ dazu"]);
                                            row["FZ Diff"] = iFZDiff;
                                            row["Einheit"] = TmpGut.Einheit;
                                            break;
                                        }
                                    }
                                } //Ende foreach  

                                Int32 iBestand = 0;
                                Int32 iAusgang = 0;
                                decimal decFaktorTmp = 0;
                                decimal decFaktorPPIST = 0;
                                //Bestand
                                iBestand = GetBestandNowForGut(AdrVerTmp.VerweisAdrID, TmpGut, myEingangCheck, myInclSPL);
                                //Ausgang
                                iAusgang = GetAusgangFromVDA4905Date(DateLastLE, AdrVerTmp.VerweisAdrID, TmpGut);
                                //Bestand 
                                row["Bestand"] = iBestand;
                                //Ausgang
                                row["Ausgang"] = iAusgang;
                                //Bestand + Ausgang
                                row["B+A"] = iAusgang + iBestand;

                                iFZDiff = 0;
                                Int32.TryParse(row["FZ Diff"].ToString(), out iFZDiff);
                                if (iFZDiff < 0)
                                {
                                    iFZDiff = (-1) * iFZDiff;
                                }
                                row["PP zu IST"] = (Int32)row["B+A"] - iFZDiff;
                                decimal decBA = 0;
                                decimal.TryParse(row["B+A"].ToString(), out decBA);
                                decimal decFZD = 0;
                                Decimal.TryParse(row["FZ Diff"].ToString(), out decFZD);
                                if (decFZD < 0)
                                {
                                    decFZD = decFZD * (-1);    // (decFZD / decBA) / 100;
                                }

                                if (decFZD > 0)
                                {
                                    decFaktorTmp = decBA / decFZD;    // (decFZD / decBA) / 100;
                                }

                                //decFaktorTmp = decBA / decFZD;    // (decFZD / decBA) / 100;

                                Decimal.TryParse(decFaktorTmp.ToString("#,##0.00"), out decFaktorPPIST);
                                row["Faktor SL"] = decFaktorPPIST;
                            }
                            else
                            {
                                FillNewRowFor4984(ref row, dtSL4984LE, dtTmp, true);
                            }
                        }
                    }

                    //prüfen, wenn ASNID leer dann =0 für Filter im Grid
                    //Int32 iTmpLE = 0;
                    //Int32.TryParse(row["LE#"].ToString(), out iTmpLE);
                    //row["LE#"] = iTmpLE;
                    this.dtSL4984LE.Rows.Add(row);
                }
            }
            if (this.dtSL4984LE.Rows.Count > 0)
            {
                Int32 iCount = 0;
                foreach (DataRow row in this.dtSL4984LE.Rows)
                {
                    iCount++;
                    row["TableRowNo"] = iCount;
                }
            }
        }
        ///<summary>clsVDA4905 / SetRowNoVDA</summary>
        ///<remarks></remarks>
        private void SetRowNoVDA(ref DataRow row)
        {
            row["Lieferant"] = const_InfoNoVDA;
            //row["4905"] = const_InfoNoVDA;
            //row["FZ dazu"] = const_InfoNoVDA;
            //row["Prüfpunkt"] = const_InfoNoVDA;
            //row["PP FZ dazu"] = const_InfoNoVDA;
            //row["FZ Diff"] = const_InfoNoVDA;
            //row["Einheit"] = const_InfoNoVDA;
            //row["Bestand"] = const_InfoNoVDA;
            //row["Ausgang"] = const_InfoNoVDA;
            //row["B+A"] = const_InfoNoVDA;
            //row["PP zu IST"] = const_InfoNoVDA;
            //row["Faktor SL"] = const_InfoNoVDA;
            //row["MB SL"] = 0;
        }
        /// <summary>
        ///             ListSLofLast217Days
        /// </summary>
        /// <param name="strGutVerweis"></param>
        /// <param name="myASNFileType"></param>
        /// <returns></returns>
        public List<string> ListSLofLast217Days(string strGutVerweis, string myASNFileType)
        {
            DateTime dtVor217Tage = DateTime.Now.AddDays(-217);
            List<string> ListSLofLast217Days = new List<string>();
            DataTable dt = new DataTable();
            string strSQL = string.Empty;

            switch (myASNFileType)
            {
                case "VDA4905":
                    strSQL = "Select DISTINCT a.Value " +
                                            "FROM ASNValue a " +
                                            "INNER JOIN ASN b ON b.ID=a.ASNID " +
                                            "INNER JOIN ASNArtSatzFeld c ON c.ID=a.ASNFieldID " +
                                                "WHERE " +
                                                    "b.ASNFileTyp='VDA4905' " +
                                                    "AND b.MandantenID=" + this.Sys.AbBereich.MandantenID + " " +
                                                    "AND b.ArbeitsbereichID=" + this.Sys.AbBereich.ID + " " +
                                                    "AND c.Kennung= 'SATZ511F04' " +
                                                    "AND a.ASNID IN (" +
                                                                        "SELECT DISTINCT ASNValue.ASNID " +
                                                                                        "FROM ASNValue " +
                                                                                        "INNER JOIN ASNArtSatzFeld  ON ASNArtSatzFeld.ID=ASNValue.ASNFieldID " +
                                                                                        "WHERE " +
                                                                                            "ASNArtSatzFeld.Kennung='SATZ512F08' AND ASNValue.Value IN ('" + strGutVerweis.Trim() + "') " +
                                                                                            "AND ASNValue.ASNID IN (" +
                                                                                                                     "SELECT DISTINCT ASNValue.ASNID " +
                                                                                                                                    "FROM ASNValue " +
                                                                                                                                    "INNER JOIN ASNArtSatzFeld  ON ASNArtSatzFeld.ID=ASNValue.ASNFieldID " +
                                                                                                                                    "WHERE " +
                                                                                                                                        "ASNArtSatzFeld.Kennung='SATZ512F05' " +
                                                                                                                                        "AND ISDATE(CAST(ASNValue.Value as nvarchar))=1 " +
                                                                                                                                        "AND CAST(CAST(ASNValue.Value as nvarchar) as date) >= '" + dtVor217Tage.ToShortDateString() + "' " +
                                                                                                                   ") " +
                                                                     ")";
                    dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, this._GL_User.User_ID, "ANSID");
                    foreach (DataRow row in dt.Rows)
                    {
                        ListSLofLast217Days.Add(row["Value"].ToString());
                    }
                    break;

                case "EdiVDA4984":
                    strSQL = "SELECT DISTINCT SupplierId FROM EdiVDA4984Value " +
                                        " WHERE " +
                                            " MandantId=" + this.Sys.AbBereich.MandantenID + " " +
                                            " AND ArbeitsbereichId= " + this.Sys.AbBereich.ID + " " +
                                            " AND REPLACE(ArtikelVerweis,' ', '')= REPLACE('" + strGutVerweis.Trim() + "', ' ', '') " +
                                            " AND DeliveryDate >= '" + dtVor217Tage.ToShortDateString() + "' ";

                    dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, this._GL_User.User_ID, "LE");
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            ListSLofLast217Days.Add(row["SupplierId"].ToString());
                        }
                    }
                    break;

            }


            return ListSLofLast217Days;
        }



        ///<summary>clsVDA4905 / GetAusgangFromVDA4905Date</summary>
        ///<remarks></remarks>
        private Int32 GetAusgangFromVDA4905Date(DateTime myLAFrom, decimal myAdrLiefID, clsGut myGut)
        {
            if (myLAFrom < Globals.DefaultDateTimeMinValue)
            {
                myLAFrom = Globals.DefaultDateTimeMinValue;
            }
            string strSQL = string.Empty;
            switch (myGut.ArtikelArt)
            {
                case clsGut.const_GArtArt_Platinen:
                    strSQL = "Select SUM(a.Anzahl) ";
                    break;

                default:
                    strSQL = "Select SUM(a.Brutto) ";
                    break;
            }

            strSQL += "FROM Artikel a " +
                        "INNER JOIN LAusgang b on b.ID = a.LAusgangTableID " +
                        "INNER JOIN Gueterart g on g.ID=a.GArtID " +
                        "WHERE " +
                            "b.Checked=1 " +
                            "AND b.Datum >='" + myLAFrom.ToShortDateString() + "' " +
                            "AND b.Auftraggeber = " + (Int32)myAdrLiefID + " " +
                            " AND a.GArtID=" + (Int32)myGut.ID + " ;";

            string strValue = clsSQLcon.ExecuteSQL_GetValue(strSQL, this._GL_User.User_ID);
            decimal decTmp = 0;
            Decimal.TryParse(strValue, out decTmp);
            if (decTmp > 0)
            {
                string strSt = string.Empty;
            }
            return (Int32)decTmp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="dtRowSource"></param>
        /// <param name="dtTmp"></param>
        /// <param name="bSetNewRow"></param>
        private void FillNewRowFor4984(ref DataRow row, DataTable dtRowSource, DataTable dtTmp, bool bSetNewRow)
        {
            string strTmpLog = string.Empty;
            if (bSetNewRow)
            {
                if (row["Log"] != null)
                {
                    strTmpLog = row["Log"].ToString();
                }

                row = null;
                row = dtRowSource.NewRow();
            }
            decimal decTmp = 0;
            Decimal.TryParse(dtTmp.Rows[0]["Nr"].ToString(), out decTmp);
            row["Nr"] = decTmp;
            row["Matchcode"] = dtTmp.Rows[0]["Matchcode"].ToString();
            row["Verweis"] = dtTmp.Rows[0]["Verweis"].ToString();
            row["Gut"] = dtTmp.Rows[0]["Gut"].ToString();
            row["Text"] = dtTmp.Rows[0]["Text"].ToString();
            decTmp = 0;
            Decimal.TryParse(dtTmp.Rows[0]["Dicke"].ToString(), out decTmp);
            row["Dicke"] = decTmp;
            decTmp = 0;
            Decimal.TryParse(dtTmp.Rows[0]["Breite"].ToString(), out decTmp);
            row["Breite"] = decTmp;
            decTmp = 0;
            Decimal.TryParse(dtTmp.Rows[0]["Menge"].ToString(), out decTmp);
            row["Menge"] = decTmp;
            decTmp = 0;
            decimal.TryParse(dtTmp.Rows[0]["BS Brutto"].ToString(), out decTmp);
            row["BS Brutto"] = decTmp;
            decTmp = 0;
            Decimal.TryParse(dtTmp.Rows[0]["min.BS"].ToString(), out decTmp);
            row["min.BS"] = decTmp;
            decTmp = 0;
            Decimal.TryParse(dtTmp.Rows[0]["Diff"].ToString(), out decTmp);
            row["Diff"] = decTmp;
            decTmp = 0;
            Decimal.TryParse(dtTmp.Rows[0]["Faktor"].ToString(), out decTmp);
            row["Faktor"] = decTmp;
            decTmp = 0;
            Decimal.TryParse(dtTmp.Rows[0]["MB"].ToString(), out decTmp);
            row["MB"] = decTmp;
            row["Lieferant"] = const_InfoNoVDA;
            row["Log"] = strTmpLog;
        }
        ///<summary>clsVDA4905 / GetBestandNow</summary>
        ///<remarks></remarks>
        private Int32 GetBestandNowForGut(decimal myAdrLiefID, clsGut myGut, bool IsEingangChecked, bool InclSPL)
        {
            string strSQL = string.Empty;
            switch (myGut.ArtikelArt)
            {
                case clsGut.const_GArtArt_Platinen:
                    strSQL = "Select SUM(a.Anzahl) ";
                    break;

                default:
                    strSQL = "Select SUM(a.Brutto) ";
                    break;

            }
            strSQL += "FROM Artikel a " +
                    "INNER JOIN LEingang b on b.ID = a.LEingangTableID " +
                    "WHERE " +
                        "b.[Check]=" + Convert.ToInt32(IsEingangChecked) + " AND a.LAusgangTableID=0 " +
                        "AND b.Auftraggeber = " + (Int32)myAdrLiefID + " " +
                        "AND a.GArtID=" + (Int32)myGut.ID + " ";

            if (!InclSPL)
            {
                strSQL += " AND a.ID NOT IN (" +
                                   "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                                         "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                                 ");";
            }
            string strBrutto = clsSQLcon.ExecuteSQL_GetValue(strSQL, this._GL_User.User_ID);
            decimal decTmp = 0;
            Decimal.TryParse(strBrutto, out decTmp);
            return (Int32)decTmp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetLELast217Days(string myArtVerweis, string myLieferantennummer)
        {
            DateTime dtVor217Tage = DateTime.Now.AddDays(-217);
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM EdiVDA4984Value " +
                                " WHERE " +
                                    " MandantId=" + this.Sys.AbBereich.MandantenID + " " +
                                    " AND ArbeitsbereichId= " + this.Sys.AbBereich.ID + " " +
                                    " AND REPLACE(ArtikelVerweis,' ', '')= REPLACE('" + myArtVerweis.Trim() + "', ' ', '') " +
                                    " AND DeliveryDate >= '" + dtVor217Tage.ToShortDateString() + "' " +
                                    " AND SupplierId = '" + myLieferantennummer + "' " +
                                    " AND CallNo = (" +
                                                        "SELECT MAX(CallNo) FROM EdiVDA4984Value " +
                                                                " WHERE " +
                                                                    " MandantId=" + this.Sys.AbBereich.MandantenID + " " +
                                                                    " AND ArbeitsbereichId= " + this.Sys.AbBereich.ID + " " +
                                                                    " AND REPLACE(ArtikelVerweis,' ', '')= REPLACE('" + myArtVerweis.Trim() + "', ' ', '') " +
                                                                    " AND DeliveryDate >= '" + dtVor217Tage.ToShortDateString() + "' " +
                                                                    " AND SupplierId = '" + myLieferantennummer + "'" +
                                                   ")" +

                                    " ORDER BY DeliveryDate";

            DataTable dtReturn = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, this._GL_User.User_ID, "LEL");
            return dtReturn;
        }
        /******************************************************************************************
        *                                   static procedure
        ******************************************************************************************/
        ///<summary>clsVDA4905 / ConvertValueForTimePeriode</summary>
        ///<remarks>Der beschriebene Zeitraum kann in folgenden Formaten vorkommen:
        ///         1. JJWWWW -> Jahr KW von/bis (163839 => KW38-39/16)
        ///         2. JJMM00 -> Bedarf für den Monat MM
        ///         3. JJ00WW -> Bedarf für die KW </remarks>
        public static string ConvertValueForTimePeriode(string myVal)
        {
            string strReturn = string.Empty;
            string strJahr = myVal.Substring(0, 2);

            string strTmp = myVal.Substring(2, 2).ToString();
            if (myVal.Substring(2, 2).ToString().Equals("00"))
            {
                //Bedarf in einer bestimmten KW
                strReturn = "KW " + myVal.Substring(4, 2).ToString() + "/" + strJahr;
            }
            else
            {
                if (myVal.Substring(4, 2).ToString().Equals("00"))
                {
                    //Bedarf für den MONAT
                    strReturn = myVal.Substring(2, 2).ToString() + "/" + strJahr;
                }
                else
                {
                    //Bedarf für den einen Zeitraum 
                    strReturn = "KW " + myVal.Substring(2, 2).ToString() + "-" + myVal.Substring(4, 2).ToString() + "/" + strJahr;
                }
            }
            return strReturn;
        }
    }
}
