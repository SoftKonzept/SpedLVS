using System;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class clsASNArtSatz
    {
        public clsASNArtSatzFeld asnSatzFeld;
        internal clsJobs Job;
        public clsSQLCOM SQLConIntern;
        public Globals._GL_USER GL_User;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        public Dictionary<string, clsASNArtSatz> DictVDASatz;
        //internal List<clsASNArtSatz> ListSatz;
        //internal List<string> ListSatzString;
        List<clsASNArtSatzFeld> ListField;
        public List<clsLogbuchCon> ListError;
        public List<string> ListSatzString;
        public List<clsASNArtSatz> ListSatz;
        public List<clsASNValue> ListASNValue;


        public decimal ID { get; set; }
        public decimal ASNArtID { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public Int32 Length { get; set; }
        public string Kennung { get; set; }
        public Int32 KennungVon { get; set; }
        public Int32 KennungBis { get; set; }
        public System.Data.DataTable dtASNSaetze { get; set; }
        public string ErrorText { get; set; }

        public string Verweis_711F03 { get; set; }
        public string Verweis_711F04 { get; set; }
        public string Verweis_712F04 { get; set; }
        public string Verweis_713F13 { get; set; }

        /**********************************************************************************
         *                              Methoden
         * ********************************************************************************/
        ///<summary>clsASNArtSatz / InitClass</summary>
        ///<remarks></remarks>
        //public void InitClass(ref Globals._GL_USER myGLUser, clsSQLConnections mySQLCon, decimal myASNArtID)
        public void InitClass(ref Globals._GL_USER myGLUser, clsSQLCOM mySQLCon)
        {
            this.GL_User = myGLUser;
            this.SQLConIntern = mySQLCon;
            asnSatzFeld = new clsASNArtSatzFeld();
            asnSatzFeld.InitClass(ref this.GL_User, this.SQLConIntern);

            Job = new clsJobs();
        }
        ///<summary>clsASNArtSatz / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {

        }
        ///<summary>clsASNArtSatz / Update</summary>
        ///<remarks></remarks>
        public void Update()
        {

        }
        ///<summary>clsASNArtSatz / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            System.Data.DataTable dt = new System.Data.DataTable("ASNArtSatz");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM ASNArtSatz WHERE ID=" + ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASNArtSatz");
            FillClassValue(dt);
        }
        ///<summary>clsASNArtSatz / FillbyASNArtID</summary>
        ///<remarks></remarks>
        public void FillbyASNArtID()
        {
            System.Data.DataTable dt = new System.Data.DataTable("ASNArtSatz");
            string strSQL = string.Empty;
            strSQL = "SELECT Top(1) * FROM ASNArtSatz WHERE ASNArtID=" + ASNArtID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASNArtSatz");
            FillClassValue(dt);
        }
        ///<summary>clsASNArtSatz / FillClassValue</summary>
        ///<remarks></remarks>
        private void FillClassValue(System.Data.DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.ASNArtID = (decimal)dt.Rows[i]["ASNArtID"];
                this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                this.Length = (Int32)dt.Rows[i]["Length"];
                this.Kennung = dt.Rows[i]["Kennung"].ToString();
                this.KennungVon = (Int32)dt.Rows[i]["KennungVon"];
                this.KennungBis = (Int32)dt.Rows[i]["KennungBis"];
                this.dtASNSaetze = GetASNSaetze();
            }
        }
        ///<summary>clsASNArtSatz / GetASNSaetze</summary>
        ///<remarks></remarks>
        private System.Data.DataTable GetASNSaetze()
        {
            string strSQL = "Select b.* FROM ASNArtSatz b " +
                                        "INNER JOIN ASNArt a ON a.ID = b.ASNArtID " +
                                        "WHERE a.ID=" + ASNArtID + " ;";
            System.Data.DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASNSaetze");
            return dt;
        }
        ///<summary>clsASNArtSatz / FillList</summary>
        ///<remarks>Füllt die SAtzlist mit den Datensätzen des Datensatzes</remarks>
        public void FillList()
        {
            asnSatzFeld.ASNSatzID = this.ID;
            asnSatzFeld.FillByASNSatzID();
            asnSatzFeld.GetSatzFields();

            if (asnSatzFeld.dtASNSatzField.Rows.Count > 0)
            {
                ListField = new List<clsASNArtSatzFeld>();
                for (Int32 i = 0; i <= asnSatzFeld.dtASNSatzField.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(asnSatzFeld.dtASNSatzField.Rows[i]["ID"].ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        clsASNArtSatzFeld tmpField = new clsASNArtSatzFeld();
                        tmpField.InitClass(ref this.GL_User, this.SQLConIntern);
                        tmpField.ID = decTmp;
                        tmpField.Fill();

                        ListField.Add(tmpField);
                    }
                }
            }
        }
        ///<summary>clsASNArtSatz / FillListSatz</summary>
        ///<remarks></remarks>
        public void FillListSatzAndDictVDA4913Satz()
        {
            DictVDASatz = new Dictionary<string, clsASNArtSatz>();
            ListSatz = new List<clsASNArtSatz>();
            System.Data.DataTable dt = GetASNSaetze();
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                clsASNArtSatz tmpSatz = new clsASNArtSatz();
                tmpSatz.GL_User = this.GL_User;
                tmpSatz.ID = (decimal)dt.Rows[i]["ID"];

                tmpSatz.Fill();
                ListSatz.Add(tmpSatz);
                DictVDASatz.Add(tmpSatz.Kennung.Trim(), tmpSatz);
            }
        }
        ///<summary>clsASNArtSatz / CreateSatzStringIN</summary>
        ///<remarks>Teilt die eingehende Meldung entsprechen in die verschiedenen Satzstring auf.
        ///         Vorgehen:
        ///         - Substring mit der entsprechenden Länge kopieren
        ///         - CHeck Kopierten Substring auf Korrektheit</remarks>
        public void CreateSatzStringIN(string myStringIN)
        {
            ListError = new List<clsLogbuchCon>();
            ListSatzString = new List<string>();
            bool Is719Exist = false;

            if (myStringIN != string.Empty)
            {
                /************************************************/
                Int32 iLengthStringIN = myStringIN.Length;
                //Check Sätze x Länge muss Gesamtlänge ergeben

                Int32 iCopyStart = 0;
                Int32 iLength = 3;

                //Zeilenumbruch am Schluss entfernen
                if (myStringIN.EndsWith("\n"))
                {
                    myStringIN = myStringIN.TrimEnd('\n');
                }
                while (myStringIN.Length > 0)
                {
                    if (myStringIN.StartsWith("\n"))
                    {
                        myStringIN = myStringIN.TrimStart('\n');
                    }

                    if (myStringIN.Length < 3)
                    {
                        string str = string.Empty;
                    }
                    //Satz ermitteln
                    string strSatz = myStringIN.Substring(0, 3);

                    //Classe für den Satz aus der Dictionary
                    clsASNArtSatz tmpSatz;
                    DictVDASatz.TryGetValue(strSatz, out tmpSatz);
                    if (
                            (tmpSatz != null) &&
                            (!Is719Exist)
                       )
                    {
                        try
                        {
                            iLength = tmpSatz.Length;
                            //Satz in ZV kopieren
                            int iLengtStringIN = myStringIN.Length;

                            string strTmpSatz = myStringIN.Substring(0, iLength);
                            //zur Liste hinzufügen
                            ListSatzString.Add(strTmpSatz);
                            //
                            string strZV = myStringIN.Remove(0, iLength);
                            myStringIN = strZV;

                            if ((!Is719Exist) && (strSatz.Equals("719")))
                            {
                                Is719Exist = (strSatz.Equals("719"));
                            }
                        }
                        catch (Exception ex)
                        {
                            ListSatzString.Add(myStringIN);

                            string str = ex.ToString();
                            string strError = "Datei: " + this.Job.FileName + " kann nicht verarbeitet werden. Dateiaufbau fehlerhaft!";
                            strError = strError + Environment.NewLine;
                            strError = strError + ex.ToString();
                            clsLogbuchCon tmpLog = new clsLogbuchCon();
                            tmpLog.GL_User = this.GL_User;
                            tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                            tmpLog.LogText = "[Task_readVDA/Task_CALLRead].[VDA4905]/[VDA4913] - " + strError;
                            tmpLog.LogText += "Zeile: " + myStringIN + " - Length: " + myStringIN.Length;
                            tmpLog.TableName = string.Empty;

                            int iZeile = 0;
                            string vda = string.Empty;
                            foreach (string s in ListSatzString)
                            {
                                vda += String.Format("{0}  : |{1}| Länge: {2}", iZeile.ToString("00"), s, s.Length) + Environment.NewLine;
                                iZeile++;
                            }
                            tmpLog.LogText += Environment.NewLine;
                            tmpLog.LogText += Environment.NewLine;
                            tmpLog.LogText += vda + Environment.NewLine;

                            decimal decTmp = 0;
                            tmpLog.TableID = decTmp;
                            this.ListError.Add(tmpLog);
                            break;
                        }
                    }
                    else
                    {
                        ListSatzString.Add(myStringIN);

                        string strError = "Datei: " + this.Job.FileName + " kann nicht verarbeitet werden. Keine ASCII VDA-Datei!";
                        strError = strError + Environment.NewLine;
                        clsLogbuchCon tmpLog = new clsLogbuchCon();
                        tmpLog.GL_User = this.GL_User;
                        tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                        tmpLog.LogText = "[Task_readVDA/Task_CALLRead].[VDA4905]/[VDA4913] - " + strError;
                        tmpLog.TableName = string.Empty;

                        int iZeile = 0;
                        string vda = string.Empty;
                        foreach (string s in ListSatzString)
                        {
                            vda += String.Format("{0}  : |{1}| Länge: {2}", iZeile.ToString("00"), s, s.Length) + Environment.NewLine;
                            iZeile++;
                        }
                        tmpLog.LogText += Environment.NewLine;
                        tmpLog.LogText += Environment.NewLine;
                        tmpLog.LogText += vda + Environment.NewLine;

                        decimal decTmp = 0;
                        tmpLog.TableID = decTmp;
                        this.ListError.Add(tmpLog);
                        break;
                    }
                }
            }
        }
        ///<summary>clsASNArtSatz / CreateSatzFieldList</summary>
        ///<remarks></remarks>
        public void CreateSatzFieldstringIN()
        {
            ErrorText = string.Empty;
            ListASNValue = new List<clsASNValue>();
            bool bFillListASNValue = true;
            bool bClearASNValue = false;
            bool bAddASNValue = true;

            this.Verweis_711F03 = string.Empty;
            this.Verweis_711F04 = string.Empty;
            this.Verweis_712F04 = string.Empty;
            this.Verweis_713F13 = string.Empty;

            if (bFillListASNValue)
            {
                for (Int32 j = 0; j <= ListSatzString.Count - 1; j++)
                {
                    string strVDA4905Check = string.Empty;

                    Int32 iCopyStart = 0;
                    Int32 iLength = 0;
                    string strSatzNr = ListSatzString[j].Substring(0, 3); //die ersten drei STellen geben den Satz an
                    string strTmpSatz = ListSatzString[j];

                    if (strSatzNr.Equals("712"))
                    {
                        string strTest = Kennung;
                    }

                    //--- neu ab 28.02.2024
                    clsASNArtSatz tmpAsnArtSatz = ListSatz.FirstOrDefault(x => x.Kennung.Trim() == strSatzNr);
                    if ((tmpAsnArtSatz.ID > 0) && (tmpAsnArtSatz.Kennung.Trim() == strSatzNr))
                    {
                        this.asnSatzFeld.ASNSatzID = tmpAsnArtSatz.ID;
                        this.asnSatzFeld.GetSatzFields();

                        //Jetzt haben wir die Liste der Satzfelder
                        for (Int32 x = 0; x <= this.asnSatzFeld.ListSatzField.Count - 1; x++)
                        {
                            //ab hier können die einzelenen Feldwerte aus dem Satzstring ermittelt werden
                            iLength = this.asnSatzFeld.ListSatzField[x].Length;
                            string FieldValue = strTmpSatz.Substring(iCopyStart, iLength).Trim();

                            switch (this.asnSatzFeld.ListSatzField[x].Kennung)
                            {
                                //VDA4905
                                //case "SATZ511F04":
                                case clsASN.const_VDA4905SatzField_SATZ511F04:
                                    //strVDA4905Check = FieldValue;
                                    break;
                                //case "SATZ512F01":
                                case clsASN.const_VDA4905SatzField_SATZ512F01:
                                    bAddASNValue = true;
                                    ErrorText = string.Empty;
                                    break;
                                //case "SATZ512F03":
                                case clsASN.const_VDA4905SatzField_SATZ512F03:
                                    //über die Kombination S511F04+"#"+S512F03 wird der Empfänger identifiziert
                                    //=> die Liefereinteilung ist für diesen Lieferanten bestimmt,
                                    //alle anderen werden nicht gespeichert.
                                    // 2015_06_08
                                    //Check nur auf 512F03 Werk = VDA4905Verweis aus JobTable
                                    //strVDA4905Check = strVDA4905Check+"#"+FieldValue;
                                    strVDA4905Check = FieldValue;
                                    //Es können auch mehrer 512 Sätze vorkommmen, deshalb über den bool bAddASNValue
                                    bAddASNValue = this.Job.VerweisVDA4905.Equals(strVDA4905Check);
                                    if (!bAddASNValue)
                                    {
                                        //x = this.asnSatzFeld.ListSatzField.Count;
                                        //i = ListSatz.Count;
                                        //j = ListSatzString.Count;
                                        //this.ListASNValue.Clear();
                                        //FieldValue = string.Empty;
                                        ErrorText = " Error: Falsche Lieferanten/Werk in Satz512F03 [" + strVDA4905Check + "]";
                                    }
                                    break;
                                //case "SATZ519F01":
                                case clsASN.const_VDA4905SatzField_SATZ519F01:
                                    bClearASNValue = !(ErrorText == string.Empty);
                                    break;

                                //VDA4913
                                case clsASN.const_VDA4913SatzField_SATZ711F03:
                                    this.Verweis_711F03 = FieldValue;
                                    if (FieldValue.Equals(string.Empty)) FieldValue = "CTN";
                                    break;

                                case clsASN.const_VDA4913SatzField_SATZ711F04:
                                    this.Verweis_711F04 = FieldValue;
                                    if (FieldValue.Equals(string.Empty)) FieldValue = "CTN";
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ712F04:
                                    this.Verweis_712F04 = FieldValue;
                                    if (FieldValue.Equals(string.Empty)) FieldValue = "CTN";
                                    break;
                                case clsASN.const_VDA4913SatzField_SATZ713F13:
                                    this.Verweis_713F13 = FieldValue;
                                    if (FieldValue.Equals(string.Empty)) FieldValue = "CTN";
                                    break;
                            }

                            if ((FieldValue != string.Empty) && (bAddASNValue))
                            {
                                clsASNValue TmpAValue = new clsASNValue();
                                TmpAValue.GL_User = this.GL_User;
                                TmpAValue.ASNID = tmpAsnArtSatz.ASNArtID; // this.ListSatz[i].ASNArtID;
                                TmpAValue.ASNFieldID = this.asnSatzFeld.ListSatzField[x].ID;
                                TmpAValue.FieldName = this.asnSatzFeld.ListSatzField[x].Datenfeld;
                                TmpAValue.Value = FieldValue;
                                TmpAValue.Kennung = this.asnSatzFeld.ListSatzField[x].Kennung;
                                this.ListASNValue.Add(TmpAValue);
                            }
                            iCopyStart = iCopyStart + iLength;
                        }
                    }

                    //for (Int32 i = 0; i <= ListSatz.Count - 1; i++)
                    //{
                    //    if (this.ListSatz[i].Kennung.TrimEnd().Equals(strSatzNr))
                    //    {
                    //        this.asnSatzFeld.ASNSatzID = this.ListSatz[i].ID;
                    //        this.asnSatzFeld.GetSatzFields();

                    //        if (strSatzNr.Equals("SATZ712F04"))
                    //        {
                    //            string strTest = Kennung;
                    //        }

                    //        //Jetzt haben wir die Liste der Satzfelder
                    //        for (Int32 x = 0; x <= this.asnSatzFeld.ListSatzField.Count - 1; x++)
                    //        {
                    //            //ab hier können die einzelenen Feldwerte aus dem Satzstring ermittelt werden
                    //            iLength = this.asnSatzFeld.ListSatzField[x].Length;
                    //            string FieldValue = strTmpSatz.Substring(iCopyStart, iLength).Trim();

                    //            switch (this.asnSatzFeld.ListSatzField[x].Kennung)
                    //            {
                    //                //VDA4905
                    //                //case "SATZ511F04":
                    //                case clsASN.const_VDA4905SatzField_SATZ511F04:
                    //                    //strVDA4905Check = FieldValue;
                    //                    break;
                    //                //case "SATZ512F01":
                    //                case clsASN.const_VDA4905SatzField_SATZ512F01:
                    //                    bAddASNValue = true;
                    //                    ErrorText = string.Empty;
                    //                    break;
                    //                //case "SATZ512F03":
                    //                case clsASN.const_VDA4905SatzField_SATZ512F03:
                    //                    //über die Kombination S511F04+"#"+S512F03 wird der Empfänger identifiziert
                    //                    //=> die Liefereinteilung ist für diesen Lieferanten bestimmt,
                    //                    //alle anderen werden nicht gespeichert.
                    //                    // 2015_06_08
                    //                    //Check nur auf 512F03 Werk = VDA4905Verweis aus JobTable
                    //                    //strVDA4905Check = strVDA4905Check+"#"+FieldValue;
                    //                    strVDA4905Check = FieldValue;
                    //                    //Es können auch mehrer 512 Sätze vorkommmen, deshalb über den bool bAddASNValue
                    //                    bAddASNValue = this.Job.VerweisVDA4905.Equals(strVDA4905Check);
                    //                    if (!bAddASNValue)
                    //                    {
                    //                        //x = this.asnSatzFeld.ListSatzField.Count;
                    //                        //i = ListSatz.Count;
                    //                        //j = ListSatzString.Count;
                    //                        //this.ListASNValue.Clear();
                    //                        //FieldValue = string.Empty;
                    //                        ErrorText = " Error: Falsche Lieferanten/Werk in Satz512F03 [" + strVDA4905Check + "]";
                    //                    }
                    //                    break;
                    //                //case "SATZ519F01":
                    //                case clsASN.const_VDA4905SatzField_SATZ519F01:
                    //                    bClearASNValue = !(ErrorText == string.Empty);
                    //                    break;

                    //                //VDA4913
                    //                case clsASN.const_VDA4913SatzField_SATZ711F03:
                    //                    this.Verweis_711F03 = FieldValue;
                    //                    if (FieldValue.Equals(string.Empty)) FieldValue = "CTN";
                    //                    break;

                    //                case clsASN.const_VDA4913SatzField_SATZ711F04:
                    //                    this.Verweis_711F04 = FieldValue;
                    //                    if (FieldValue.Equals(string.Empty)) FieldValue = "CTN";
                    //                    break;
                    //                case clsASN.const_VDA4913SatzField_SATZ712F04:
                    //                    this.Verweis_712F04 = FieldValue;
                    //                    if (FieldValue.Equals(string.Empty)) FieldValue = "CTN";
                    //                    break;
                    //                case clsASN.const_VDA4913SatzField_SATZ713F13:
                    //                    this.Verweis_713F13 = FieldValue;
                    //                    if (FieldValue.Equals(string.Empty)) FieldValue = "CTN";
                    //                    break;
                    //            }

                    //            if ((FieldValue != string.Empty) && (bAddASNValue))
                    //            {
                    //                clsASNValue TmpAValue = new clsASNValue();
                    //                TmpAValue.GL_User = this.GL_User;
                    //                TmpAValue.ASNID = this.ListSatz[i].ASNArtID;
                    //                TmpAValue.ASNFieldID = this.asnSatzFeld.ListSatzField[x].ID;
                    //                TmpAValue.FieldName = this.asnSatzFeld.ListSatzField[x].Datenfeld;
                    //                TmpAValue.Value = FieldValue;
                    //                TmpAValue.Kennung = this.asnSatzFeld.ListSatzField[x].Kennung;
                    //                this.ListASNValue.Add(TmpAValue);
                    //            }
                    //            iCopyStart = iCopyStart + iLength;
                    //        }
                    //    }
                    //}

                }

                if (bClearASNValue)
                {
                    this.ListASNValue.Clear();
                    ErrorText = ErrorText + " - ASN List geleert!";
                }
                else
                {
                    string strstop = string.Empty;
                }
            }
        }

    }
}
