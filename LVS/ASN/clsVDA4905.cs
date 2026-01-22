using LVS.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace LVS.ASN
{
    public class clsVDA4905
    {
        public const string const_InfoNoVDA = "---";
        public const string const_ExportTxtMB_SIL_GREEN = "OK";
        public const string const_ExportTxtMB_SIL_YELLOW = "OK?";
        public const string const_ExportTxtMB_SIL_RED = "!!!";

        public const string const_AbrufDatum_000000 = "000000";
        public const string const_AbrufDatum_222222 = "222222";
        public const string const_AbrufDatum_333333 = "333333";
        public const string const_AbrufDatum_444444 = "444444";
        public const string const_AbrufDatum_555555 = "555555";
        public const string const_Menge_999999 = "999999";

        public const string const_DateFormat_JJMMTT = "JJMMTT";
        public const string const_DateFormat_JJWWWW = "JJWWWW";
        public const string const_DateFormat_JJMM00 = "JJMM00";
        public const string const_DateFormat_JJ00WW = "JJ00WW";

        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GLSystem;
        public clsSystem Sys;
        internal clsADRVerweis AdrVerweis;
        internal clsGut Gut;

        public decimal AsnID { get; set; }
        public DateTime Datum { get; set; }
        public decimal TransID { get; set; }
        public decimal TransDaum { get; set; }
        public string Verweis { get; set; }
        public decimal Werk { get; set; }
        public string Werksnummer { get; set; }
        public string Einheit { get; set; }
        public decimal VDA4905ReceiverID { get; set; }

        public DataTable dtHead { get; set; }
        public DataTable dtQuantity { get; set; }
        internal List<decimal> ListASNID { get; set; }
        public Dictionary<decimal, clsBSInfoArt> DictBSInfoArt;
        public DataTable dtBSInfoSource { get; set; }
        public DataTable dtSL4905LE { get; set; }

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
        ///<summary>clsVDA4905 / InitClass</summary>
        ///<remarks></remarks>
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

        ///<summary>clsVDA4905 / GetLiefereinteilungen</summary>
        ///<remarks></remarks>
        public void GetLiefereinteilungen(decimal myVDA4905ReceiverID)
        {
            this.VDA4905ReceiverID = myVDA4905ReceiverID;
            if (VDA4905ReceiverID > 0)
            {
                //Ermitteln des Verweis für 4905
                //Select   SUBSTRING(ADRVerweis.Verweis,PATINDEX ( '%#%' , ADRVerweis.Verweis)+1,3) FROM [SIL_COM].[dbo].ADRVerweis  WHERE ASNFileTyp='VDA4905' and SenderAdrID=5)
                Dictionary<decimal, clsADRVerweis> dictAdrVerweis = clsADRVerweis.GetAdrVerweis(this._GL_User, myVDA4905ReceiverID, this.Sys.AbBereich.MandantenID, this.Sys.AbBereich.ID);
                AdrVerweis = null;
                if (dictAdrVerweis.TryGetValue(VDA4905ReceiverID, out AdrVerweis))
                {
                    //Mit dem Verweis kann nun die ASNIDs ermittelt werden für den ZEitraum
                    GetASNHead(ref AdrVerweis);
                    GetQuantity();
                }
            }
        }
        ///<summary>clsVDA4905 / GetASNHead</summary>
        ///<remarks></remarks>
        private void GetASNHead(ref clsADRVerweis myAdrVerweis)
        {
            dtHead = new DataTable();
            string strSQL = "Select " +
                            "a.ID" +
                            ", a.Datum" +
                            ", (Select Name1 FROM [" + this.Sys.con_Database + "].[dbo].ADR WHERE ID IN (Select VerweisAdrID FROM [" + this.Sys.con_Database + "].[dbo].ADRVerweis WHERE SenderAdrID=" + myAdrVerweis.SenderAdrID + " AND Verweis=(Select CAST(Value as nvarchar(9)) FROM ASNValue WHERE ASNID=a.ID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ511F04')))) as Lieferant " +
                            ", (Select ID FROM [" + this.Sys.con_Database + "].[dbo].ADR WHERE ID IN (Select VerweisAdrID FROM [" + this.Sys.con_Database + "].[dbo].ADRVerweis WHERE SenderAdrID=" + myAdrVerweis.SenderAdrID + " AND Verweis=(Select CAST(Value as nvarchar(9)) FROM ASNValue WHERE ASNID=a.ID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ511F04')))) as AdrIDLieferant " +
                            ", (Select CAST(Value as DateTime) FROM ASNValue WHERE ASNID=a.ID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ511F07')) as TransDatum" +
                            ", (Select CAST(Value as INT)		FROM ASNValue WHERE ASNID=a.ID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ511F06')) as TransID" +
                            ", (Select CAST(Value as nvarchar(9))FROM ASNValue WHERE ASNID=a.ID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ511F04')) as Verweis" +
                            ", (Select top(1) CAST(Value as nvarchar(3))FROM ASNValue WHERE ASNID=a.ID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ512F03')) as Werk" +
                            ", (Select top(1) CAST(Value as nvarchar(50))FROM ASNValue WHERE ASNID=a.ID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ512F08')) as Werksnummer" +
                            ", (Select top(1) CAST(Value as nvarchar(5))FROM ASNValue WHERE ASNID=a.ID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ512F13')) as Einheit" +
                            " FROM ASN a " +
                            "INNER JOIN ASNArt b ON b.ID=a.ASNNr " +
                            "INNER JOIN ASNValue c ON c.ASNID = a.ID " +
                            "WHERE " +
                                //"DATEDIFF(dd, a.Datum,CAST('22.11.2014' as DateTime))>=0 " +
                                "DATEDIFF(dd, a.Datum,GETDATE())>=0 " +
                                "AND a.ID IN (" +
                                                "Select a.ASNID FROM ASNValue a " +
                                                                "INNER JOIN ASNArtSatzFeld b ON b.ID = a.ASNFieldID " +
                                                                "WHERE b.Kennung='SATZ512F03' AND a.Value= SUBSTRING('" + myAdrVerweis.Verweis + "',PATINDEX ( '%#%' , '" + myAdrVerweis.Verweis + "')+1,3)) " +
                                "Group By a.ID, a.Datum ;";
            dtHead = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, this._GL_User.User_ID, "VDA4905");
            ListASNID = new List<decimal>();
            foreach (DataRow row in dtHead.Rows)
            {
                decimal decTmp = 0;
                Decimal.TryParse(row["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    ListASNID.Add(decTmp);
                }
            }
        }
        ///<summary>clsVDA4905 / GetASNHead</summary>
        ///<remarks></remarks>
        private void GetQuantity()
        {
            dtQuantity = new DataTable();
            dtQuantity.Columns.Add("ASN", typeof(decimal));
            dtQuantity.Columns.Add("Zeitraum", typeof(string));
            dtQuantity.Columns.Add("Menge", typeof(decimal));

            string strSQL = "Select " +
                                    "a.ASNID" +
                                    ", (Select top(1) CAST(Value as nvarchar(6)) FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ513F08')) as SATZ513F08 " +
                                    ", (Select top(1)  CAST(Value as INT)		 FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ513F09')) as SATZ513F09 " +
                                    ", (Select top(1)  CAST(Value as nvarchar(6)) FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ513F10')) as SATZ513F10 " +
                                    ", (Select top(1)  CAST(Value as INT)		 FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ513F11')) as SATZ513F11 " +
                                    ", (Select top(1)  CAST(Value as nvarchar(6)) FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ513F12')) as SATZ513F12 " +
                                    ", (Select top(1)  CAST(Value as INT)		 FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ513F13')) as SATZ513F13 " +
                                    ", (Select top(1)  CAST(Value as nvarchar(6)) FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ513F14')) as SATZ513F14 " +
                                    ", (Select top(1)  CAST(Value as INT)		 FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ513F15')) as SATZ513F15 " +
                                    ", (Select top(1)  CAST(Value as nvarchar(6)) FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ513F16')) as SATZ513F16 " +
                                    ", (Select top(1)  CAST(Value as INT)         FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ513F17')) as SATZ513F17 " +

                                    ", (Select top(1)  CAST(Value as nvarchar(6)) FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F03')) as SATZ514F03 " +
                                    ", (Select top(1)  CAST(Value as INT)         FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F04')) as SATZ514F04 " +
                                    ", (Select top(1)  CAST(Value as nvarchar(6)) FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F05')) as SATZ514F05 " +
                                    ", (Select top(1)  CAST(Value as INT)         FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F06')) as SATZ514F06 " +
                                    ", (Select top(1)  CAST(Value as nvarchar(6)) FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F07')) as SATZ514F07 " +
                                    ", (Select top(1)  CAST(Value as INT)         FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F08')) as SATZ514F08 " +
                                    ", (Select top(1)  CAST(Value as nvarchar(6)) FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F09')) as SATZ514F09 " +
                                    ", (Select top(1)  CAST(Value as INT)         FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F10')) as SATZ514F10 " +
                                    ", (Select top(1)  CAST(Value as nvarchar(6)) FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F11')) as SATZ514F11 " +
                                    ", (Select top(1)  CAST(Value as INT)         FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F12')) as SATZ514F12 " +
                                    ", (Select top(1)  CAST(Value as nvarchar(6)) FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F13')) as SATZ514F13 " +
                                    ", (Select top(1)  CAST(Value as INT)         FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F14')) as SATZ514F14 " +
                                    ", (Select top(1)  CAST(Value as nvarchar(6)) FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F15')) as SATZ514F15 " +
                                    ", (Select top(1)  CAST(Value as INT)         FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F16')) as SATZ514F16 " +
                                    ", (Select top(1)  CAST(Value as nvarchar(6)) FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F17')) as SATZ514F17 " +
                                    ", (Select top(1)  CAST(Value as INT)         FROM ASNValue WHERE ASNID=a.ASNID AND ASNFieldID =(Select ID FROM ASNArtSatzFeld WHERE Kennung='SATZ514F18')) as SATZ514F18 " +


                                    "FROM ASNValue a " +
                                        "INNER JOIN ASNArtSatzFeld e ON e.ID=a.ASNFieldID " +
                                        "INNER JOIN ASN b ON b.ID = a.ASNID " +
                                        "WHERE " +
                                            "a.ASNID IN (" + string.Join(",", this.ListASNID.ToArray()) + ") " +
                                            "GROUP BY a.ASNID ";
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, this._GL_User.User_ID, "VDA4905");
            //Zeile
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decASN = 0;
                DataRow row = dtQuantity.NewRow();
                //Spalte
                bool bVDA4905_const_AbrufDatum_555555 = false;
                bool bAddRow = true;
                for (Int32 x = 0; x <= dt.Columns.Count - 1; x++)
                {
                    decimal decTmp = 0;
                    string strColName = dt.Columns[x].ColumnName;
                    string strDateFormat = clsVDA4905.const_DateFormat_JJMMTT;


                    switch (strColName)
                    {
                        case "ASNID":
                            row = dtQuantity.NewRow();
                            Decimal.TryParse(dt.Rows[i][x].ToString(), out decASN);
                            row["ASN"] = decASN;
                            break;

                        default:
                            //Unterteilung 
                            if ((x % 2) != 0)
                            {
                                string strAbrufDatum = string.Empty;
                                string TmpDatum = dt.Rows[i][x].ToString();
                                switch (TmpDatum)
                                {
                                    case clsVDA4905.const_AbrufDatum_000000:
                                        //Letzte Abruffeld für diese Materialnummer
                                        x = dt.Columns.Count;
                                        bAddRow = false;
                                        break;
                                    case clsVDA4905.const_AbrufDatum_222222:
                                        //es liegt kein Bedarf für diese Material vor
                                        x = dt.Columns.Count;
                                        bAddRow = false;
                                        break;

                                    case clsVDA4905.const_AbrufDatum_333333:
                                        //Kennzeichnet die dazugehörige Menge als Rückstand
                                        bAddRow = true;
                                        break;

                                    case clsVDA4905.const_AbrufDatum_444444:
                                        //Sofortbedarf
                                        bAddRow = true;
                                        break;

                                    case clsVDA4905.const_AbrufDatum_555555:
                                        bAddRow = false;
                                        bVDA4905_const_AbrufDatum_555555 = true;
                                        break;

                                    case clsVDA4905.const_Menge_999999:
                                        break;

                                    default:
                                        if (!bVDA4905_const_AbrufDatum_555555)
                                        {
                                            strDateFormat = clsVDA4905.const_DateFormat_JJMMTT;
                                            bVDA4905_const_AbrufDatum_555555 = false;
                                        }
                                        else
                                        {
                                            Int32 strIndex = 0;
                                            strIndex = TmpDatum.IndexOf("00", 0);
                                            switch (strIndex)
                                            {
                                                case 2:
                                                    strDateFormat = clsVDA4905.const_DateFormat_JJ00WW;
                                                    break;

                                                case 4:
                                                    strDateFormat = clsVDA4905.const_DateFormat_JJMM00;
                                                    break;
                                                default:
                                                    strDateFormat = clsVDA4905.const_DateFormat_JJWWWW;
                                                    break;
                                            }
                                        }
                                        break;
                                }
                                strAbrufDatum = FormatDateTimefor4905(strDateFormat, TmpDatum);
                                row["Zeitraum"] = strAbrufDatum;
                            }
                            else
                            {
                                Decimal.TryParse(dt.Rows[i][x].ToString(), out decTmp);
                                row["Menge"] = decTmp;

                                if (bAddRow)
                                {
                                    dtQuantity.Rows.Add(row);
                                }
                                bAddRow = true;
                                row = dtQuantity.NewRow();
                                row["ASN"] = decASN;
                            }
                            break;
                    }
                }
            }
        }
        ///<summary>clsVDA4905 / FormatDateTimefor4905</summary>
        ///<remarks></remarks>
        private string FormatDateTimefor4905(string myDateFormat, string strDate)
        {
            string retVal = string.Empty;
            string strJahr = "20" + strDate.Substring(0, 2);
            switch (myDateFormat)
            {
                case clsVDA4905.const_DateFormat_JJ00WW:
                    string strKW = strDate.Substring(4, 2);
                    retVal = "KW " + strKW + "/" + strJahr;
                    break;

                case clsVDA4905.const_DateFormat_JJMM00:
                    string strMonat = strDate.Substring(2, 2);
                    retVal = "MO " + strMonat + "/" + strJahr;
                    break;

                case clsVDA4905.const_DateFormat_JJWWWW:
                    string strVonKW = strDate.Substring(2, 2);
                    string strBisKW = strDate.Substring(4, 2);
                    retVal = "KW " + strVonKW + " - " + strBisKW + "/" + strJahr;
                    break;
                case clsVDA4905.const_DateFormat_JJMMTT:
                    string strMM = strDate.Substring(2, 2);
                    string strTT = strDate.Substring(4, 2);
                    retVal = strTT + "." + strMM + "." + strJahr;
                    break;
            }
            return retVal;
        }
        ///<summary>clsVDA4905 / LoadBSInfo4905</summary>
        ///<remarks></remarks>
        public void LoadBSInfo4905(bool bChecked, bool InclSPL, bool bActiveGT)
        {
            dtBSInfoSource = new DataTable("BSInfoVDA4905");
            CreateBSInfoTableColumns();
            //1. Ermitteln der Güterartendaten
            this.Gut.FillDictGutVda4095(bActiveGT);
            FillArtikelDataForBSInfo4905(bChecked, InclSPL);

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

                    //if (decFaktor > 149)  // ab 150
                    //{
                    //    //STatus grün
                    //    row["MB"] = "3";
                    //}
                    //if ((decFaktor > 99) && (decFaktor < 150)) // 100 <= Faktor >150
                    //{
                    //    //STatus gelb
                    //    row["MB"] = "2";
                    //}
                    //if ((decFaktor < 100))   //0<= Faktor <100 
                    //{
                    //    //STatus rot
                    //    row["MB"] = "1";
                    //}
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
        private void FillArtikelDataForBSInfo4905(bool bChecked, bool InclSPL)
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

                    dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, this._GL_User.User_ID, "VDA4905");

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
            dtSL4905LE = new DataTable();
            dtSL4905LE = dtSource.Copy();
            dtSL4905LE.Clear();
            //dtSL4905LE.Columns.Add("GutVerweis", typeof(string));
            dtSL4905LE.Columns.Add("Lieferant", typeof(string));
            dtSL4905LE.Columns.Add("4905", typeof(DateTime));
            dtSL4905LE.Columns.Add("FZ dazu", typeof(Int32));
            dtSL4905LE.Columns.Add("Prüfpunkt", typeof(DateTime));
            dtSL4905LE.Columns.Add("PP FZ dazu", typeof(Int32));
            dtSL4905LE.Columns.Add("FZ Diff", typeof(Int32));
            dtSL4905LE.Columns.Add("Einheit", typeof(string));
            dtSL4905LE.Columns.Add("Bestand", typeof(Int32));
            dtSL4905LE.Columns.Add("Ausgang", typeof(Int32));
            dtSL4905LE.Columns.Add("B+A", typeof(Int32));
            dtSL4905LE.Columns.Add("PP zu IST", typeof(Int32));
            dtSL4905LE.Columns.Add("Faktor SL", typeof(decimal));
            dtSL4905LE.Columns.Add("MB SL", typeof(decimal));
            dtSL4905LE.Columns.Add("Log", typeof(string));
            dtSL4905LE.Columns.Add("ASNID", typeof(decimal));
            dtSL4905LE.Columns.Add("GArt", typeof(clsGut));
            dtSL4905LE.Columns.Add("ediVDA4984Value", typeof(clsEdiVDA4984Value));
            dtSL4905LE.Columns.Add("TableRowNo", typeof(Int32));

            Dictionary<string, clsADRVerweis> DictAdrVerweis = clsADRVerweis.FillDictAdrVerweis(this.Sys.AbBereich.MandantenID, this.Sys.AbBereich.ID, this._GL_User.User_ID, constValue_AsnArt.const_Art_VDA4905);

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

                    DataRow row = dtSL4905LE.NewRow();
                    dtSource.DefaultView.RowFilter = "Verweis='" + TmpGut.Verweis.Trim() + "'";
                    DataTable dtTmp = dtSource.DefaultView.ToTable();

                    FillNewRowFor4905(ref row, dtSL4905LE, dtTmp, true);

                    this.WorkingReportText = "Güterart [" + TmpGut.ViewID + " - Datensatz " + (1 + i).ToString() + " von " + this.Gut.ListGutIDVDA4905.Count.ToString() + "] wird verarbeitet...";
                    this.OnWorkingReportChange(EventArgs.Empty);

                    //-- ermitteln der Stahllieferanten, für die eine DFÜ innhalb der letzen 217 Tage für diese Güterart vorliegt
                    List<string> listSL = new List<string>();
                    listSL = ListSLofLast217Days(TmpGut.Verweis.Trim());

                    if (listSL.Count == 0)
                    {
                        SetRowNoVDA(ref row);
                        row["Log"] = "kein SL hinterlegt";
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
                            row["Lieferant"] = strLieferantenNr + "[" + AdrTmp.ViewID + "]";
                            row["GArt"] = TmpGut;
                            row["ASNID"] = 0;
                            //this.WorkingReportText = "Gut [" + TmpGut.ViewID + "]  (" + (1 + i).ToString() + "/" + this.Gut.ListGutIDVDA4905.Count.ToString() + ")  / SL [" + AdrTmp.ViewID + "] wird verarbeitet...";
                            //this.OnWorkingReportChange(EventArgs.Empty);

                            //--- ermitteln der ASN-Daten für diesen SL für die Güterart, die DFÜ kann die Abrufe für mehrer Güterarten enthalten,
                            // deshalb muss auch auf die korrekte Güterart geprüft werden und kann dann erst berechnet werden
                            List<decimal> ListASN4905forSL = new List<decimal>();
                            ListASN4905forSL = GetASN4905ForSL(TmpGut.Verweis);
                            for (Int32 k = 0; k <= ListASN4905forSL.Count - 1; k++)
                            {
                                //Sortierung bei der Liste, somit muss der letzte in die kommenden beiden spalten 
                                //eingetragen werden 
                                //Ab hier müssen jetzt die einzelen Werte addiert werden                                            
                                decimal decAsnID = ListASN4905forSL[k];
                                row["ASNID"] = decAsnID;

                                DataTable dtSL = clsASNValue.GetASNValueDataTableByASNIdinclKennung(this._GL_User.User_ID, decAsnID);
                                Int32 iCountDL = 0;
                                bool bChangePPFZdazu = false;
                                //-- Merker zeigt, ob die Prufüng auf Werk und Güterart korret sind True heisst die Prüfung auf Werk und Güterart sind nicht korrekt nächster Satz512
                                bool bNextASNItem = false;
                                bool bSumm = true;
                                string strCheckDate = string.Empty;
                                bool bBreakProzess = false;
                                if (dtSL.Rows.Count == 0)
                                {
                                    //keine VDA4905 vorhanden 
                                    SetRowNoVDA(ref row);
                                }
                                foreach (DataRow rowTmp in dtSL.Rows)
                                {
                                    Int32 iTmp = 0;
                                    string strKennung = rowTmp["Kennung"].ToString();
                                    switch (strKennung)
                                    {
                                        //Check Lieferwerk -> wenn nicht korrekt weiter zur Nächsten Satz SATZ512F03 mit der nächsten Liefeinteilung
                                        case clsASN.const_VDA4905SatzField_SATZ512F03:
                                            string strCHeckWerk = rowTmp["Value"].ToString();
                                            bNextASNItem = (!strCHeckWerk.Equals(this.Sys.Mandant.VDA4905Verweis));
                                            break;

                                        //Datum Lieferabruf  Spalte 4905
                                        case clsASN.const_VDA4905SatzField_SATZ512F05:
                                            if (!bNextASNItem)
                                            {
                                                //iCountDL = 1;
                                                DateTime dateForGrid = Globals.DefaultDateTimeMinValue;
                                                if (row["4905"].ToString().Equals(string.Empty))
                                                {
                                                    row["4905"] = dateForGrid;
                                                }
                                                //Datumswert aus VDA4905 ASN
                                                DateTime dateVDA4905ASN = Globals.DefaultDateTimeMinValue;
                                                DateTime.TryParseExact(rowTmp["Value"].ToString(), "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateVDA4905ASN);
                                                if (dateForGrid < dateVDA4905ASN)
                                                {
                                                    row["4905"] = dateVDA4905ASN;
                                                    bChangePPFZdazu = true;
                                                }
                                                else
                                                {
                                                    bChangePPFZdazu = false;
                                                }
                                            }
                                            break;

                                        // check Güterart
                                        case clsASN.const_VDA4905SatzField_SATZ512F08:
                                            if (!bNextASNItem)
                                            {
                                                string strSachnummerKunde = rowTmp["Value"].ToString();
                                                bNextASNItem = !(TmpGut.Verweis.Trim().Equals(strSachnummerKunde.Trim()));
                                            }
                                            break;

                                        /**************************************************
                                        *    Summieren der Liefermengen
                                        * ************************************************/
                                        //Fortschrittszahl  FZ dazu
                                        case clsASN.const_VDA4905SatzField_SATZ513F07:
                                            if (!bNextASNItem)
                                            {
                                                iTmp = 0;
                                                Int32.TryParse(rowTmp["Value"].ToString(), out iTmp);
                                                row["FZ dazu"] = iTmp;
                                                bChangePPFZdazu = false;
                                                iFZ = iFZ + iTmp;
                                            }
                                            break;

                                        // Abrufedatum 
                                        case clsASN.const_VDA4905SatzField_SATZ513F08:
                                        case clsASN.const_VDA4905SatzField_SATZ513F10:
                                        case clsASN.const_VDA4905SatzField_SATZ513F12:
                                        case clsASN.const_VDA4905SatzField_SATZ513F14:
                                        case clsASN.const_VDA4905SatzField_SATZ513F16:
                                        case clsASN.const_VDA4905SatzField_SATZ514F03:
                                        case clsASN.const_VDA4905SatzField_SATZ514F05:
                                        case clsASN.const_VDA4905SatzField_SATZ514F07:
                                        case clsASN.const_VDA4905SatzField_SATZ514F09:
                                        case clsASN.const_VDA4905SatzField_SATZ514F11:
                                        case clsASN.const_VDA4905SatzField_SATZ514F13:
                                        case clsASN.const_VDA4905SatzField_SATZ514F15:
                                        case clsASN.const_VDA4905SatzField_SATZ514F17:
                                            if (!bNextASNItem)
                                            {
                                                strCheckDate = rowTmp["Value"].ToString();
                                                switch (strCheckDate)
                                                {
                                                    //-- Rückstand berechnen
                                                    case clsVDA4905.const_AbrufDatum_333333:
                                                        //SetRowNoVDA(ref row);
                                                        row["Log"] = strCheckDate;
                                                        break;

                                                    case clsVDA4905.const_AbrufDatum_222222:  //kein Bedarf
                                                        //SetRowNoVDA(ref row);
                                                        row["Log"] = strCheckDate;
                                                        k = ListASN4905forSL.Count;
                                                        bSumm = false;
                                                        //bBreakProzess = true;
                                                        break;

                                                    case clsVDA4905.const_AbrufDatum_000000:  //ENDE
                                                    case clsVDA4905.const_AbrufDatum_555555:  //Zeitraumangabe (Woche Monat)
                                                    case clsVDA4905.const_Menge_999999:  //Rest(Vorschaumenge mehrere Monate
                                                        k = ListASN4905forSL.Count;
                                                        row["Log"] = strCheckDate;
                                                        bSumm = false;
                                                        break;
                                                    default:
                                                        //Datumswert aus VDA4905 ASN
                                                        if (row["Log"].ToString().Equals(clsVDA4905.const_AbrufDatum_555555))
                                                        {
                                                            //zeitraum bezogen / Value wert muss umgewandelt werden
                                                        }
                                                        else
                                                        {
                                                            DateTime dtPP = Globals.DefaultDateTimeMinValue;
                                                            DateTime.TryParseExact(rowTmp["Value"].ToString(), "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtPP);

                                                            //Datumwert aus dem aktuelle GRID
                                                            DateTime dtPPinGrid = Globals.DefaultDateTimeMinValue;
                                                            DateTime.TryParse(row["Prüfpunkt"].ToString(), out dtPPinGrid);

                                                            if (dtPP >= dtPPinGrid)
                                                            {
                                                                row["Prüfpunkt"] = dtPP;
                                                                if (bSumm)
                                                                {
                                                                    row["Prüfpunkt"] = dtPP;
                                                                    if (dtPP > DateTime.Now.Date)
                                                                    {
                                                                        bSumm = false;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    bSumm = true;
                                                                    row["Prüfpunkt"] = dtPPinGrid;
                                                                    bNextASNItem = true;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                }
                                            }
                                            break;

                                        // Abrufe die ggf. zur Fortschrittszahl addiert werden müssen
                                        case clsASN.const_VDA4905SatzField_SATZ513F09:
                                        case clsASN.const_VDA4905SatzField_SATZ513F11:
                                        case clsASN.const_VDA4905SatzField_SATZ513F13:
                                        case clsASN.const_VDA4905SatzField_SATZ513F15:
                                        case clsASN.const_VDA4905SatzField_SATZ513F17:
                                        case clsASN.const_VDA4905SatzField_SATZ514F04:
                                        case clsASN.const_VDA4905SatzField_SATZ514F06:
                                        case clsASN.const_VDA4905SatzField_SATZ514F08:
                                        case clsASN.const_VDA4905SatzField_SATZ514F10:
                                        case clsASN.const_VDA4905SatzField_SATZ514F12:
                                        case clsASN.const_VDA4905SatzField_SATZ514F14:
                                        case clsASN.const_VDA4905SatzField_SATZ514F16:
                                        case clsASN.const_VDA4905SatzField_SATZ514F18:
                                            if (!bNextASNItem)
                                            {
                                                switch (strCheckDate)
                                                {
                                                    //-- Rückstand berechnen
                                                    case clsVDA4905.const_AbrufDatum_333333:
                                                        if (myRueckStand)
                                                        {
                                                            iTmp = 0;
                                                            Int32.TryParse(rowTmp["Value"].ToString(), out iTmp);
                                                            iRueckStand = iRueckStand + iTmp;
                                                            strCheckDate = string.Empty;
                                                        }
                                                        break;

                                                    //-- Sofortbedarf
                                                    case clsVDA4905.const_AbrufDatum_444444:
                                                        iTmp = 0;
                                                        Int32.TryParse(rowTmp["Value"].ToString(), out iTmp);
                                                        iFZ = iFZ + iTmp;
                                                        //strCheckDate = string.Empty;
                                                        row["Log"] = strCheckDate;
                                                        //Prüfpunkt setzen falls 01.01.0001, dann Prüfpunkt = Datum DFÜ
                                                        DateTime dtCheckPP = DateTime.MinValue;
                                                        DateTime.TryParse(row["Prüfpunkt"].ToString(), out dtCheckPP);
                                                        if (dtCheckPP.Date == DateTime.MinValue.Date)
                                                        {
                                                            row["Prüfpunkt"] = row["4905"];
                                                        }
                                                        break;

                                                    case clsVDA4905.const_AbrufDatum_222222:  //kein Bedarf
                                                        //SetRowNoVDA(ref row);
                                                        row["Log"] = strCheckDate;
                                                        k = ListASN4905forSL.Count;
                                                        bSumm = false;
                                                        bBreakProzess = true;
                                                        break;

                                                    case clsVDA4905.const_AbrufDatum_000000:  //ENDE
                                                    case clsVDA4905.const_AbrufDatum_555555:  //Zeitraumangabe (Woche Monat)
                                                    case clsVDA4905.const_Menge_999999:  //Rest(Vorschaumenge mehrere Monate
                                                        k = ListASN4905forSL.Count;
                                                        row["Log"] = strCheckDate;
                                                        bSumm = false;
                                                        //bBreakProzess = true;
                                                        break;

                                                    default:
                                                        DateTime dtCheck = Globals.DefaultDateTimeMinValue;
                                                        //Check auf Datum
                                                        if (DateTime.TryParseExact(strCheckDate, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtCheck))
                                                        {
                                                            iTmp = 0;
                                                            Int32.TryParse(rowTmp["Value"].ToString(), out iTmp);
                                                            iFZ = iFZ + iTmp;
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                    }
                                }
                                if (!bBreakProzess)
                                {
                                    //row["PP FZ dazu"] = iFZ;
                                    //iFZDiff = iRueckStand + ((Int32)row["FZ dazu"] - (Int32)row["PP FZ dazu"]);
                                    row["PP FZ dazu"] = iFZ + iRueckStand;
                                    iFZDiff = ((Int32)row["FZ dazu"] - (Int32)row["PP FZ dazu"]);

                                    row["FZ Diff"] = iFZDiff;
                                    row["Einheit"] = TmpGut.Einheit;

                                    Int32 iBestand = 0;
                                    Int32 iAusgang = 0;
                                    //PP zu IST                               
                                    // (decFZD / decBA) / 100;
                                    decimal decFaktorTmp = 0;
                                    decimal decFaktorPPIST = 0;
                                    //if (!bSumm)
                                    //{
                                    //Bestand
                                    iBestand = GetBestandNowForGut(AdrVerTmp.VerweisAdrID, TmpGut, myEingangCheck, myInclSPL);
                                    //Ausgang
                                    DateTime dtASNDate = (DateTime)row["4905"];
                                    iAusgang = GetAusgangFromVDA4905Date(dtASNDate, AdrVerTmp.VerweisAdrID, TmpGut);
                                    //}
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
                                    FillNewRowFor4905(ref row, dtSL4905LE, dtTmp, true);
                                }
                            }
                        }
                    }

                    //prüfen, wenn ASNID leer dann =0 für Filter im Grid
                    Int32 iTmpASNID = 0;
                    Int32.TryParse(row["ASNID"].ToString(), out iTmpASNID);
                    row["ASNID"] = (decimal)iTmpASNID;
                    this.dtSL4905LE.Rows.Add(row);
                }
                //Test
                //if (i == 10)
                //{
                //    i = this.Gut.ListGutIDVDA4905.Count;
                //}
            }
            if (this.dtSL4905LE.Rows.Count > 0)
            {
                Int32 iCount = 0;
                foreach (DataRow row in this.dtSL4905LE.Rows)
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
        ///<summary>clsVDA4905 / FillNewRowFor4905</summary>
        ///<remarks></remarks>
        private void FillNewRowFor4905(ref DataRow row, DataTable dtRowSource, DataTable dtTmp, bool bSetNewRow)
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
        ///<summary>clsVDA4905 / GetASN4905ForSL</summary>
        ///<remarks></remarks>
        private List<decimal> GetASN4905ForSL(string myGutVerweis)
        {
            List<decimal> ListASN4905forSLTmp = new List<decimal>();
            DateTime dtVor217Tage = DateTime.Now.AddDays(-217);
            DataTable dt = new DataTable();
            string strSQL = "Select MAX(a.ASNID) as ASNID " +
                                        "FROM ASNValue a " +
                                        "INNER JOIN ASN b ON b.ID=a.ASNID " +
                                        "INNER JOIN ASNArtSatzFeld c ON c.ID=a.ASNFieldID " +
                                            "WHERE " +
                                                "b.ASNFileTyp='VDA4905' " +
                                                "AND b.MandantenID=" + this.Sys.AbBereich.MandantenID + " " +
                                                "AND b.ArbeitsbereichID=" + this.Sys.AbBereich.ID + " " +
                                                "AND a.ASNID IN (" +
                                                                "SELECT DISTINCT ASNValue.ASNID FROM ASNValue " +
                                                                            "INNER JOIN ASNArtSatzFeld  ON ASNArtSatzFeld.ID = ASNValue.ASNFieldID " +
                                                                            "WHERE " +

                                                                            " ASNArtSatzFeld.Kennung = 'SATZ512F08' " +
                                                                            " AND ASNValue.Value IN ('" + myGutVerweis.Trim() + "') " +
                                                                            " AND ASNValue.ASNID in (" +
                                                                                                    "SELECT DISTINCT ASNValue.ASNID FROM ASNValue " +
                                                                                                                                    "INNER JOIN ASNArtSatzFeld  ON ASNArtSatzFeld.ID = ASNValue.ASNFieldID " +
                                                                                                                                    " WHERE " +
                                                                                                                                    " ASNArtSatzFeld.Kennung = 'SATZ512F05' " +
                                                                                                                                    " AND ISDATE(CAST(ASNValue.Value as nvarchar)) = 1 " +
                                                                                                                                    " AND CAST(CAST(ASNValue.Value as nvarchar) as date) >=  '" + dtVor217Tage.ToShortDateString() + "' " +
                                                                                                //" AND CAST(CAST(ASNValue.Value as nvarchar) as date) >=  '" + DateTime.Now.ToShortDateString() + "' " +
                                                                                                ") " +
                                                               ") " +
                                                               " ; ";// ORDER BY a.ASNID DESC ";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, this._GL_User.User_ID, "ANSID");
            foreach (DataRow row in dt.Rows)
            {
                decimal decTmp = 0;
                Decimal.TryParse(row["ASNID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    ListASN4905forSLTmp.Add(decTmp);
                }
            }
            return ListASN4905forSLTmp;
        }
        ///<summary>clsVDA4905 / ListSLofLast217Days</summary>
        ///<remarks>Ermittel die Lieferantennummern der Lieferanten für die lezten 217 Tage</remarks>
        public List<string> ListSLofLast217Days(string strGutVerweis)
        {
            DateTime dtVor217Tage = DateTime.Now.AddDays(-217);
            List<string> ListSLofLast217Days = new List<string>();
            DataTable dt = new DataTable();
            string strSQL = "Select DISTINCT a.Value " +
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
            return ListSLofLast217Days;
        }
        ///<summary>clsVDA4905 / GetAusgangFromVDA4905Date</summary>
        ///<remarks></remarks>
        private Int32 GetAusgangFromVDA4905Date(DateTime myLAFrom, decimal myAdrLiefID, clsGut myGut)
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
