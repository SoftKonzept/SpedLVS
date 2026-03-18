using Common.Enumerations;
using Common.Models;
using LVS.Constants;
using LVS.InitValue;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;

namespace LVS
{
    public class clsReportDocSetting
    {
        public clsDocKey DocKeys;

        public const string const_ReportSetting = "RepDocSetting";
        public const string const_ReportSettingAssignment_All = "ReportSettingAssignmentAll";
        public const string const_ReportSettingAssignment_default = "ReportSettingAssignmentDefault";
        public const string const_ReportSettingAssignment_defaultWS = "ReportSettingAssignmentDefaultWS";  //Arbeitsbereich
        public const string const_ReportSettingAssignment_defaultSystem = "ReportSettingAssignmentDefaultSystem";
        public const string const_ReportSettingAssignment_customize = "ReportSettingAssignmentCustomize";

        //public const string const_localPrinterINIPath = @"C:\LVS\Config\printer.ini";
        //public const string const_localTempReportPath = @"C:\LVS\Report\";
        //public const string const_localTempPDFReportPath = @"C:\LVS\Report\PDF\";

        public string TempReportFileName
        {
            get
            {
                string strTmp = this.RSAId + "_" + this.AdrID + "_" + this.ReportFileName;
                return strTmp;
            }
        }

        public const string const_DocumentArt_Eingang = "Eingang";
        public const string const_DocumentArt_Ausgang = "Ausgang";
        public const string const_DocumentArt_RG = "Rechnung";
        public const string const_DocumentArt_Mail = "Mail";
        public const string const_DocumentArt_ListPrint = "List";
        public const string const_DocumentArt_Miscellaneous = "Diverse";

        internal clsReportDocSettingAssignment repDocSettAssignment;
        private clsArbeitsbereiche abBereich;
        public clsArbeitsbereiche AbBereich
        {
            get
            {
                if (this.AbBereichID > 0)
                {
                    abBereich = new clsArbeitsbereiche();
                    abBereich.InitCls(this.GLUser, this.AbBereichID);
                }
                return abBereich;
            }
        }
        public string IniDocKeyName { get; set; }
        public string ctrDocView { get; set; }
        public string IniDocKeyValuePath { get; set; }

        public Globals._GL_USER GLUser;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = GLUser.User_ID;
                return _BenutzerID;
            }
            set
            {
                _BenutzerID = value;
            }
        }

        public bool ExistDocKey
        {
            get
            {
                string strSQL = string.Empty;
                strSQL = "Select Distinct DocKey FROM ReportDocSetting " +
                                        "WHERE DocKey='" + this.DocKey + "' ";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
            }
        }

        /*********************************************************************************************************/
        public Int32 ID { get; set; }
        public Int32 RSAId { get; set; }
        public Int32 DocKeyID { get; set; }
        public string DocKey { get; set; }              // Key in INI
        public string ViewID { get; set; }              // ViewID / Ausgabe in CTR
        public string Path { get; set; }             // Value / Wert zum Key in INIdoc
        public string ReportFileName { get; set; }
        public string PrinterName { get; set; }         // zugewiesener Drucker kommt aus der Ini auf dem Rechner
        public string PaperSource { get; set; }         // zugewiesene Papierquelle kommt aus der Ini auf dem Rechner
        public Int32 PrintCount { get; set; }           // Druckanzahl -> zu druckende Dokumente
        public string Art { get; set; }
        public bool activ { get; set; }
        public decimal AdrID { get; set; }          // zugewiesene Adressid
        public decimal AbBereichID { get; set; }    // Arbeitsbereich
        public byte[] ReportDataFile { get; set; }
        public bool ReportDataFileExist { get; set; } = false;   // Report liegt als Datei in der Datenbank

        public bool CanUseTxtModul { get; set; }    // Dokument kann Textbausteine verwenden
        public bool IsDefault { get; set; }
        public bool IsCustomize { get; set; }

        public List<clsReportDocSetting> ListReportDocSettingAll { get; set; }
        private string _DocFileNameAndPath;
        public string DocFileNameAndPath
        {
            get
            {
                _DocFileNameAndPath = this.Path;
                _DocFileNameAndPath = _DocFileNameAndPath + this.ReportFileName;
                return _DocFileNameAndPath;
            }
        }

        public string StartupPath
        {
            get
            {
                return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        public Dictionary<string, clsReportDocSetting> DictReportDoc = new Dictionary<string, clsReportDocSetting>();
        public Dictionary<string, clsReportDocSetting> DictReportDocEingang = new Dictionary<string, clsReportDocSetting>();
        public Dictionary<string, clsReportDocSetting> DictReportDocAusgang = new Dictionary<string, clsReportDocSetting>();
        public Dictionary<string, clsReportDocSetting> DictReportDocMail = new Dictionary<string, clsReportDocSetting>();
        public Dictionary<string, clsReportDocSetting> DictReportDocRechnung = new Dictionary<string, clsReportDocSetting>();
        public Dictionary<string, clsReportDocSetting> DictReportDocMiscellaneous = new Dictionary<string, clsReportDocSetting>();

        public List<clsReportDocSetting> ListReportDoc = new List<clsReportDocSetting>();
        public List<clsReportDocSetting> ListReportDocEingang = new List<clsReportDocSetting>();
        public List<clsReportDocSetting> ListReportDocAusgang = new List<clsReportDocSetting>();
        public List<clsReportDocSetting> ListReportDocMail = new List<clsReportDocSetting>();
        public List<clsReportDocSetting> ListReportDocRechnung = new List<clsReportDocSetting>();
        public List<clsReportDocSetting> ListReportDocListPrint = new List<clsReportDocSetting>();
        public List<clsReportDocSetting> ListReportDocMiscellaneous = new List<clsReportDocSetting>();

        public List<clsReportDocSetting> ListDocCanUseTxtModul = new List<clsReportDocSetting>();
        /*********************************************************************************************************
         *                              Procedure / Methoden
         * ******************************************************************************************************/
        ///<summary>clsReportDoc / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSys, clsSystem mySys, decimal myAdrID, decimal myAbBereich)
        {
            this.GLUser = myGLUser;
            this.AdrID = myAdrID;
            this.AbBereichID = myAbBereich;
            DocKeys = new clsDocKey();
            repDocSettAssignment = new clsReportDocSettingAssignment();
            repDocSettAssignment.InitClass(this.GLUser, myGLSys, mySys);

            //FillDictReportDoc(myAdrID);
            ListDocCanUseTxtModul = new List<clsReportDocSetting>();
            ListReportDocSettingAll = new List<clsReportDocSetting>();
            DictReportDoc = new Dictionary<string, clsReportDocSetting>();
            DictReportDoc = FillDictReportDoc(string.Empty, this.AdrID);
            FillDictReportDoc(clsDocKey.const_DocumentArt_Eingang, this.AdrID);
            FillDictReportDoc(clsDocKey.const_DocumentArt_Ausgang, this.AdrID);
            FillDictReportDoc(clsDocKey.const_DocumentArt_Mail, this.AdrID);
            FillDictReportDoc(clsDocKey.const_DocumentArt_RG, this.AdrID);
            FillDictReportDoc(clsDocKey.const_DocumentArt_ListPrint, this.AdrID);
            FillDictReportDoc(clsDocKey.const_DocumentArt_ListMiscellaneous, this.AdrID);
        }
        ///<summary>clsReportDoc / Copy</summary>
        ///<remarks></remarks>
        public clsReportDocSetting Copy()
        {
            return (clsReportDocSetting)this.MemberwiseClone();
        }
        ///<summary>clsReportDoc / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            string strSQL = string.Empty;
            strSQL = this.AddSQL();
            strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            if (iTmp > 0)
            {
                this.ID = iTmp;
                //Add Logbucheintrag Eintrag
                //string Beschreibung = "Adresse: " + ViewID + " - " + Name1 + " hinzugefügt";
                //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
            }
        }
        ///<summary>clsReportDoc / AddSQL</summary>
        ///<remarks></remarks>
        public string AddSQL()
        {
            string strSQL = string.Empty;
            strSQL = "INSERT INTO ReportDocSetting (DocKey, ViewID, PrintCount, Art, activ, DocKeyID, CanUseTxtModul" +
                                         ") " +
                                         "VALUES ('" + this.DocKey + "'" +
                                                ", '" + this.ViewID + "'" +
                                                //", '" + this.DocPath + "'" +
                                                ", '" + this.PrintCount + "'" +
                                                ", '" + this.Art + "'" +
                                                ", " + Convert.ToInt32(this.activ) +
                                                ", " + this.DocKeyID +
                                                ", " + Convert.ToInt32(CanUseTxtModul) +
                                         "); ";
            return strSQL;
        }

        public void FillByAssId(int myRdsId, int myRdsaId)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;

            //strSql = "DECLARE @AdrID as int; " +
            //         "SET @AdrID=" + (Int32)this.AdrID + ";  " +

            strSql = "Select " +
                        "rds.ID " +
                        ", rds.DocKey " +
                        ", rds.ViewID " +
                        ", rds.PrintCount " +
                        ", rds.Art " +
                        ", rds.activ " +
                        ", rds.DocKeyId" +
                        ", rds.CanUseTxtModul" +
                        ", (SELECT [Path] FROM ReportDocSettingAssignment WHERE ID=" + (int)myRdsaId + ")  as [Path] " +
                        ", (SELECT ReportFileName FROM ReportDocSettingAssignment WHERE ID=" + (int)myRdsaId + ")  as ReportFileName " +
                        ", (SELECT IsDefault FROM ReportDocSettingAssignment WHERE ID=" + (int)myRdsaId + ") as IsDefault " +
                        ", (SELECT AdrID FROM ReportDocSettingAssignment WHERE ID=" + (int)myRdsaId + ") as AdrID " +
                        ", (SELECT AbBereichID FROM ReportDocSettingAssignment WHERE ID=" + (int)myRdsaId + ") as AbBereichID " +
                        ", (SELECT ID FROM ReportDocSettingAssignment WHERE ID=" + (int)myRdsaId + ") as [RSAId] " +
                        "FROM ReportDocSetting rds " +
                        " WHERE " +
                            "rds.id = " + myRdsId + "; ";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ReportDocSetting");
            SetValue(dt);
        }
        ///<summary>clsReportDoc / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            //PrinterSettings ps = new PrinterSettings();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            //strSql = "SELECT * FROM ReportDocSetting WHERE ID=" + ID + ";";

            strSql = "DECLARE @AdrID as int; " +
                     "SET @AdrID=" + (Int32)this.AdrID + ";  " +

                     "Select " +
                        "rds.ID " +
                        ", rds.DocKey " +
                        ", rds.ViewID " +
                        ", rds.PrintCount " +
                        ", rds.Art " +
                        ", rds.activ " +
                        ", rds.DocKeyId" +
                        ", rds.CanUseTxtModul" +
                        ", CASE " +
                            "WHEN (SELECT Top(1) ID FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and AdrID = @AdrID AND AbBereichID=" + (int)this.AbBereichID + ")> 0 " +
                            "THEN (SELECT Top(1)[Path] FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and AdrID = @AdrID AND AbBereichID=" + (int)this.AbBereichID + ") " +
                            "ELSE (SELECT Top(1)[Path] FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and IsDefault = 1 AND AbBereichID=" + (int)this.AbBereichID + ") " +
                            "END as [Path] " +
                        ", CASE " +
                            "WHEN (SELECT Top(1) ID FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and AdrID = @AdrID AND AbBereichID=" + (int)this.AbBereichID + ")> 0 " +
                            "THEN (SELECT Top(1) ReportFileName FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and AdrID = @AdrID AND (ReportFileName is not null AND ReportFileName <> '' AND AbBereichID=" + (int)this.AbBereichID + ")) " +
                            "ELSE (SELECT Top(1) ReportFileName FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and IsDefault = 1 AND (ReportFileName is not null AND ReportFileName <> '' AND AbBereichID=" + (int)this.AbBereichID + ")) " +
                            "END as ReportFileName " +
                        ", CASE " +
                            "WHEN (SELECT Top(1) ID FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and AdrID = @AdrID AND AbBereichID=" + (int)this.AbBereichID + ")> 0 " +
                            "THEN (SELECT Top(1) IsDefault FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and AdrID = @AdrID AND AbBereichID=" + (int)this.AbBereichID + ") " +
                            "ELSE (SELECT Top(1) IsDefault FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and IsDefault = 1 AND AbBereichID=" + (int)this.AbBereichID + ") " +
                            "END as IsDefault " +
                        ", CASE " +
                            "WHEN (SELECT Top(1) ID FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and AdrID = @AdrID AND AbBereichID=" + (int)this.AbBereichID + ")> 0 " +
                            "THEN (SELECT Top(1) AdrID FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and AdrID = @AdrID AND AbBereichID=" + (int)this.AbBereichID + ") " +
                            "ELSE (SELECT Top(1) AdrID FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and IsDefault = 1 AND AbBereichID=" + (int)this.AbBereichID + ") " +
                            "END as AdrID " +
                        ", CASE " +
                            "WHEN (SELECT Top(1) ID FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and AdrID = @AdrID AND AbBereichID=" + (int)this.AbBereichID + ")> 0 " +
                            "THEN (SELECT Top(1) AbBereichID FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and AdrID = @AdrID AND AbBereichID=" + (int)this.AbBereichID + ") " +
                            "ELSE (SELECT Top(1) AbBereichID FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and IsDefault = 1 AND AbBereichID=" + (int)this.AbBereichID + ") " +
                            "END as AbBereichID " +
                        ", CASE " +
                            "WHEN (SELECT Top(1) ID FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and AdrID = @AdrID AND AbBereichID=" + (int)this.AbBereichID + ")> 0 " +
                            "THEN (SELECT Top(1) ID FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and AdrID = @AdrID AND AbBereichID=" + (int)this.AbBereichID + ") " +
                            "ELSE (SELECT Top(1) ID FROM ReportDocSettingAssignment WHERE DocKey = rds.DocKey and IsDefault = 1 AND AbBereichID=" + (int)this.AbBereichID + ") " +
                            "END as [RSAId] " +

                        "FROM ReportDocSetting rds " +
                        " WHERE " +
                            "rds.id = " + this.ID + "; ";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ReportDocSetting");
            SetValue(dt);
        }

        private void SetValue(DataTable dt)
        {
            //-- mr 2025_07_12
            //    PrinterSettings ps = new PrinterSettings();
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                this.ID = iTmp;
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["RSAId"].ToString(), out iTmp);
                this.RSAId = iTmp;
                if (this.RSAId == 474)
                {
                    string st = string.Empty;
                }
                this.ViewID = dt.Rows[i]["ViewID"].ToString();
                this.DocKey = dt.Rows[i]["DocKey"].ToString();
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["DocKeyID"].ToString(), out iTmp);
                this.DocKeyID = iTmp;
                this.CanUseTxtModul = (bool)dt.Rows[i]["CanUseTxtModul"];
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["PrintCount"].ToString(), out iTmp);
                this.PrintCount = iTmp;
                this.Art = dt.Rows[i]["Art"].ToString();
                this.activ = (bool)dt.Rows[i]["activ"];

                //aus Table ReportSettingAssignment
                this.Path = dt.Rows[i]["Path"].ToString();
                this.ReportFileName = dt.Rows[i]["ReportFileName"].ToString();
                decimal decTmp = 0;
                decimal.TryParse(dt.Rows[i]["AdrID"].ToString(), out decTmp);
                this.AdrID = decTmp;
                this.IsDefault = (bool)dt.Rows[i]["IsDefault"];

                this.PrinterName = string.Empty; // InitValue_PrintServicePrinter_Default.DefaultPrinter();

                this.ReportDataFile = null;
                this.ReportDataFileExist = false;

                if (this.RSAId > 0)
                {
                    string strSqlTmp = "SELECT Report FROM ReportDocSettingAssignment  WHERE ID=" + this.RSAId;
                    object obj = clsSQLcon.ExecuteSQLWithTRANSACTIONGetObject(strSqlTmp, "Report", GLUser.User_ID);
                    if (obj == DBNull.Value)
                    {
                        try
                        {
                            string tmpFilePath = this.StartupPath + this.DocFileNameAndPath;
                            if (helper_IOFile.CheckFile(tmpFilePath))
                            {
                                this.ReportDataFile = helper_IOFile.FileToByteArray(tmpFilePath);
                                this.ReportDataFileExist = this.ReportDataFile.Length > 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            this.ReportDataFile = null;
                            this.ReportDataFileExist = false;
                        }
                    }
                    else
                    {
                        this.ReportDataFile = (byte[])obj;
                        this.ReportDataFileExist = true;
                    }
                }


                enumAppType appTyp = enumAppType.NotSet;
                try
                {
                    appTyp = InitValue_GlobalSettings.AppType();
                }
                catch (Exception ex)
                {
                    appTyp = enumAppType.NotSet;
                }
                switch (appTyp)
                {
                    case enumAppType.NotSet:
                    case enumAppType.LvsPrintService:
                    case enumAppType.Communicator:
                    case enumAppType.Sped4:
                        try
                        {
                            PrinterSettings ps = new PrinterSettings();

                            constValue_PrinterIni constPrinterIni = new constValue_PrinterIni((int)BenutzerID);
                            clsINI.clsINI ini = new clsINI.clsINI(constPrinterIni.IniFilePaht);
                            if (ini.ReadString("Druckereinstellungen", this.DocKey + "_Drucker", string.Empty) != null)
                            {
                                try
                                {
                                    this.PrinterName = ini.ReadString("Druckereinstellungen", this.DocKey + "_Drucker", "");
                                    if (this.PrinterName.Equals(string.Empty))
                                    {
                                        this.PrinterName = ps.PrinterName;
                                    }
                                    this.PaperSource = ini.ReadString("Druckereinstellungen", this.DocKey + "_Fach", "");
                                }
                                catch (Exception ex) { }
                            }
                        }
                        catch (Exception ex)
                        {
                            string str = ex.Message;
                        }
                        break;

                    case enumAppType.LvsMobileAPI:
                        this.PrinterName = string.Empty;
                        this.PaperSource = string.Empty;
                        break;
                }
            }
        }
        ///<summary>clsReportDoc / FillCls</summary>
        ///<remarks>Füll nur diese Klasse mit den Werte aus der DB</remarks>
        public void FillClsbyID()
        {
            PrinterSettings ps = new PrinterSettings();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ReportDocSetting WHERE ID=" + ID + ";";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ReportDocSetting");
            FillClassValue(dt);
        }
        ///<summary>clsReportDoc / FillClsByDocKey</summary>
        ///<remarks>Füll nur diese Klasse mit den Werte aus der DB</remarks>
        public void FillClsByDocKey()
        {
            PrinterSettings ps = new PrinterSettings();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT Top(1) * FROM ReportDocSetting WHERE DocKey='" + this.DocKey + "';";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ReportDocSetting");
            FillClassValue(dt);
        }
        ///<summary>clsReportDoc / FillClassValue</summary>
        ///<remarks></remarks>
        private void FillClassValue(DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                this.ID = iTmp;
                this.ViewID = dt.Rows[i]["ViewID"].ToString();
                this.DocKey = dt.Rows[i]["DocKey"].ToString();
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["PrintCount"].ToString(), out iTmp);
                this.PrintCount = iTmp;
                this.Art = dt.Rows[i]["Art"].ToString();
                this.activ = (bool)dt.Rows[i]["activ"];
                this.CanUseTxtModul = (bool)dt.Rows[i]["CanUseTxtModul"];
            }
        }
        ///<summary>clsReportDoc / Update</summary>
        ///<remarks>Es wird nur der Datensatz aus der Table ReportSetting upgedated.</remarks>
        public void Update()
        {
            string strSQL = string.Empty;
            strSQL = "UPDATE ReportDocSetting SET " +
                                "DocKey= '" + this.DocKey + "' " +
                                ", DocKeyID = " + this.DocKeyID +
                                ", ViewID = '" + this.ViewID + "'" +
                                ", PrintCount = " + this.PrintCount +
                                ", Art = '" + this.Art + "'" +
                                ", activ = " + Convert.ToInt32(this.activ) +
                                ", CanUseTxtModul =" + Convert.ToInt32(this.CanUseTxtModul) +

                                "WHERE ID=" + (Int32)this.ID + " ;";
            clsSQLcon.ExecuteSQL(strSQL, this.BenutzerID);
        }
        public void DeleteFileData()
        {
            string strSQL = string.Empty;
            strSQL = "UPDATE ReportDocSetting SET " +
                                "DocKey= '" + this.DocKey + "' " +


                                "WHERE ID=" + (Int32)this.ID + " ;";
            clsSQLcon.ExecuteSQL(strSQL, this.BenutzerID);
        }
        ///<summary>clsReportDoc / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            string strSQL = string.Empty;
            strSQL = "DELETE FROM ReportDocSettingAssignment where DocKeyID IN (SELECT DocKeyID FROM ReportDocSettingAssignment WHERE ID=" + (Int32)this.ID + "); " +
                     " Delete ReportDocSetting WHERE ID=" + (Int32)this.ID + " ;";
            clsSQLcon.ExecuteSQL(strSQL, this.BenutzerID);
        }
        ///<summary>clsReportDoc / FillDictReportDoc</summary>
        ///<remarks></remarks>
        public Dictionary<string, clsReportDocSetting> FillDictReportDoc(string myArt, decimal myAdrID)
        {
            Dictionary<string, clsReportDocSetting> DictReportDocReturn = new Dictionary<string, clsReportDocSetting>();
            List<clsReportDocSetting> listTmp = new List<clsReportDocSetting>();

            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select DISTINCT " +
                         "rds.ID" +
                        ",ass.DocKey" +
                        ", ass.Path" +
                        ", ass.ReportFileName" +
                        ", ass.IsDefault" +
                        ", ass.AdrID" +
                        ", ass.AbBereichID" +
                        ", rds.Art" +
                        ", ass.Report " +
                        ", case when ass.Report is null then 0 else 1 end as FileExist " +
                        " FROM ReportDocSettingAssignment ass " +
                        "INNER JOIN ReportDocSetting rds on rds.DocKey = ass.DocKey ";
            strSql = strSql + " WHERE ass.IsDefault = 1 ";
            strSql = strSql + " AND ass.AbBereichID=" + (Int32)this.AbBereichID +
                              " AND ass.MandantenID=" + (int)this.AbBereich.MandantenID;

            if (!myArt.Equals(string.Empty))
            {
                strSql = strSql + " AND rds.Art = '" + myArt + "' ";
            }
            strSql = strSql + " AND (ass.ReportFileName is not null AND ass.ReportFileName<>'') " +
                              " AND rds.activ=1 " +
                              " Order by rds.Art";


            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ReportDocs");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                if (iTmp == 28)
                {
                    string str = string.Empty;
                }
                clsReportDocSetting tmpDoc = new clsReportDocSetting();
                tmpDoc.GLUser = this.GLUser;
                tmpDoc.ID = iTmp;
                tmpDoc.AbBereichID = this.AbBereichID;
                tmpDoc.AdrID = myAdrID;
                tmpDoc.Fill();
                if (!DictReportDocReturn.ContainsKey(tmpDoc.DocKey))
                {
                    DictReportDocReturn.Add(tmpDoc.DocKey, tmpDoc);
                    listTmp.Add(tmpDoc);
                }

                if ((tmpDoc.IsDefault) && (tmpDoc.CanUseTxtModul))
                {
                    if (ListDocCanUseTxtModul.FindIndex(p => p.DocKeyID == tmpDoc.DocKeyID) < 0)
                    {
                        ListDocCanUseTxtModul.Add(tmpDoc);
                    }
                }
            }
            switch (myArt)
            {
                case const_DocumentArt_Eingang:
                    this.ListReportDocEingang = new List<clsReportDocSetting>();
                    this.ListReportDocEingang = listTmp;
                    this.ListReportDocSettingAll.AddRange(listTmp);
                    break;
                case const_DocumentArt_Ausgang:
                    this.ListReportDocAusgang = new List<clsReportDocSetting>();
                    this.ListReportDocAusgang = listTmp;
                    this.ListReportDocSettingAll.AddRange(listTmp);
                    break;
                case const_DocumentArt_ListPrint:
                    this.ListReportDocListPrint = new List<clsReportDocSetting>();
                    this.ListReportDocListPrint = listTmp;
                    this.ListReportDocSettingAll.AddRange(listTmp);
                    break;
                case const_DocumentArt_Miscellaneous:
                    this.ListReportDocMiscellaneous = new List<clsReportDocSetting>();
                    this.ListReportDocMiscellaneous = listTmp;
                    this.ListReportDocSettingAll.AddRange(listTmp);
                    break;
                case const_DocumentArt_Mail:
                    this.ListReportDocMail = new List<clsReportDocSetting>();
                    this.ListReportDocMail = listTmp;
                    this.ListReportDocSettingAll.AddRange(listTmp);
                    break;
                case const_DocumentArt_RG:
                    this.ListReportDocRechnung = new List<clsReportDocSetting>();
                    this.ListReportDocRechnung = listTmp;
                    this.ListReportDocSettingAll.AddRange(listTmp);
                    break;


            }
            return DictReportDocReturn;
        }
        ///<summary>clsReportDoc / GetClassByDocKey</summary>
        ///<remarks></remarks>
        public clsReportDocSetting GetClassByDocKey(string myDocKey)
        {
            clsReportDocSetting retCls = new clsReportDocSetting();
            if (this.DictReportDoc.Count > 0)
            {
                this.DictReportDoc.TryGetValue(myDocKey, out retCls);
            }
            return retCls;
        }
        /***********************************************************************************
        *                   static Procedure
        ***********************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <returns></returns>
        public bool ExistReportSetting(enumIniDocKey myDocKeyEnum)
        {
            bool bReturn = false;
            clsReportDocSetting tmpRepSettings = this.GetClassByDocKey(enumIniDocKey.LagerrechnungMail.ToString());
            if ((tmpRepSettings is clsReportDocSetting) && (tmpRepSettings.ID > 0))
            {
                bReturn = true;
            }
            return bReturn;
        }
        ///<summary>clsReportDoc / GetClassByDocKey</summary>
        ///<remarks></remarks>
        public static List<string> GetDocKey(Globals._GL_USER myGLUser)
        {
            List<string> listTmp = new List<string>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select DISTINCT DocKey FROM ReportDocSetting WHERE activ=1 Order by DocKey";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "ReportDocs");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                string strKey = dt.Rows[i]["DocKey"].ToString();
                if (!listTmp.Contains(strKey))
                {
                    listTmp.Add(strKey);
                }
            }
            return listTmp;
        }
        ///<summary>clsReportDoc / GetClassByDocKey</summary>
        ///<remarks></remarks>
        public static List<string> GetDocArt(Globals._GL_USER myGLUser)
        {
            List<string> listTmp = new List<string>();
            //DataTable dt = new DataTable();
            //string strSql = string.Empty;
            //strSql = "Select DISTINCT Art FROM ReportDocSetting ORDER By Art";
            //dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "DocArt");
            //for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            //{
            //    string strKey = dt.Rows[i]["Art"].ToString();
            //    if (!listTmp.Contains(strKey))
            //    {
            //        listTmp.Add(strKey);
            //    }
            //}

            listTmp.Add(clsReportDocSetting.const_DocumentArt_Ausgang);
            listTmp.Add(clsReportDocSetting.const_DocumentArt_Eingang);
            listTmp.Add(clsReportDocSetting.const_DocumentArt_ListPrint);
            listTmp.Add(clsReportDocSetting.const_DocumentArt_Mail);
            listTmp.Add(clsReportDocSetting.const_DocumentArt_Miscellaneous);
            listTmp.Add(clsReportDocSetting.const_DocumentArt_RG);

            return listTmp;
        }
        ///<summary>clsReportDoc / GetReportSettings</summary>
        ///<remarks></remarks>
        public static DataTable GetReportSettings(Globals._GL_USER myGLUser, string myRangeList, clsArbeitsbereiche myAbBereich)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            switch (myRangeList)
            {
                case const_ReportSetting:
                    strSql = "Select rs.* FROM ReportDocSetting rs ";

                    break;

                case const_ReportSettingAssignment_All:
                    strSql = "Select rsa.* " +
                                     ", case when Report is null then 0 else 1 end as FileExist " +
                                    "FROM ReportDocSettingAssignment rsa " +
                                                 "INNER JOIN Arbeitsbereich a on a.ID = rsa.AbBereichID " +
                                                 " WHERE " +
                                                    " rsa.AbBereichID=" + myAbBereich.ID +
                                                    " AND rsa.MandantenID=" + myAbBereich.MandantenID;
                    break;

                case const_ReportSettingAssignment_default:
                    strSql = "Select rsa.* " +
                                    ", case when Report is null then 0 else 1 end as FileExist " +
                                    " FROM ReportDocSettingAssignment rsa " +
                                                 "INNER JOIN Arbeitsbereich a on a.ID = rsa.AbBereichID " +
                                                 " WHERE " +
                                                    " rsa.IsDefault=1 ";
                    break;

                case const_ReportSettingAssignment_defaultWS:
                    strSql = "Select rsa.*" +
                                    ", case when Report is null then 0 else 1 end as FileExist " +
                                    " FROM ReportDocSettingAssignment rsa " +
                                                 "INNER JOIN Arbeitsbereich a on a.ID = rsa.AbBereichID " +
                                                 " WHERE " +
                                                    " rsa.AbBereichID=" + myAbBereich.ID +
                                                    " AND rsa.MandantenID=" + myAbBereich.MandantenID +
                                                    " AND rsa.IsDefault=1 ";
                    break;

                case const_ReportSettingAssignment_customize:
                    strSql = "Select rsa.*" +
                                    ", case when Report is null then 0 else 1 end as FileExist " +
                                    " FROM ReportDocSettingAssignment rsa " +
                                                 "INNER JOIN Arbeitsbereich a on a.ID = rsa.AbBereichID " +
                                                 " WHERE " +
                                                    " rsa.AbBereichID=" + myAbBereich.ID +
                                                    " AND rsa.MandantenID=" + myAbBereich.MandantenID +
                                                    " AND rsa.IsDefault=0 " +
                                                    " AND rsa.AdrID>0 ";
                    break;
            }
            strSql = strSql + " Order by DocKey; ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "ReportDocSetting");
            return dt;
        }
        ///<summary>clsReportDoc / InitFillTable</summary>
        ///<remarks></remarks>
        public void InitFillTable()
        {
            string strSql = string.Empty;
            clsDocKey tmpDocKeys = new clsDocKey();
            foreach (var pair in DocKeys.DictDocKey)
            {
                clsReportDocSetting tmpRDS = new clsReportDocSetting();
                tmpRDS.GLUser = this.GLUser;
                tmpRDS.DocKey = pair.Value.ToString();
                if (tmpRDS.DocKey.Equals(enumIniDocKey.Bestandsliste))
                {
                    string str = string.Empty;
                }

                tmpRDS.DocKeyID = pair.Key;
                if (!tmpRDS.ExistDocKey)
                {
                    tmpRDS.ViewID = tmpRDS.DocKey;
                    tmpRDS.PrintCount = 1;
                    tmpRDS.activ = true;
                    tmpRDS.Art = tmpDocKeys.GetDocKeyArt(tmpRDS.DocKey);
                    strSql = strSql + tmpRDS.AddSQL();
                }
                else
                {
                    //Update KeyDocID
                    tmpRDS.FillClsByDocKey();
                    tmpRDS.Art = tmpDocKeys.GetDocKeyArt(tmpRDS.DocKey);
                    if (tmpRDS.ID > 0)
                    {
                        tmpRDS.Update();
                    }
                }
            }
            if (!strSql.Equals(string.Empty))
            {
                clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "ReportDocSetting", this.BenutzerID);
            }
        }

        public clsReportDocSetting GetReportDocSettingForPrintAusgangDocument(Ausgaenge myAusgang, enumDokumentenArt myDocumentArt)
        {
            clsReportDocSetting tmpSetting = new clsReportDocSetting();
            if (!myDocumentArt.Equals(enumDokumentenArt.NotSet))
            {
                if (myAusgang.Auftraggeber > 0)
                {
                    //tmpSetting = ReportDocSetting.ListReportDocSettingAll.FirstOrDefault(x => x.AdrID == Ausgang.Auftraggeber);
                    //tmpSetting = ReportDocSetting.ListReportDocAusgang.FirstOrDefault(x => x.AdrID == myAusgang.Auftraggeber & x.DocKey == PrintDocumentArt.ToString());
                    tmpSetting = ListReportDocAusgang.FirstOrDefault(x => x.AdrID == myAusgang.Auftraggeber & x.DocKey == myDocumentArt.ToString());
                }
                if (tmpSetting is null)
                {
                    //tmpSetting = ReportDocSetting.ListReportDocSettingAll.FirstOrDefault(x => x.DocKey == PrintDocumentArt.ToString());
                    //tmpSetting = ReportDocSetting.ListReportDocAusgang.FirstOrDefault(x => x.DocKey == PrintDocumentArt.ToString());
                    tmpSetting = ListReportDocAusgang.FirstOrDefault(x => x.DocKey == myDocumentArt.ToString());
                }
            }
            return tmpSetting;
        }
    }
}
