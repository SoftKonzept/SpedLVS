using LVS.Dokumente;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
//using System.Windows.Forms;



namespace LVS
{
    public class clsAuftrag
    {
        public const string const_DBTableName = "Auftrag";
        public clsADRMan AdrManuell = new clsADRMan();
        ///<summary>clsAuftrag
        ///         Eigenschaften:
        ///             - ID
        ///             - ANr
        ///             - Mandanten ID
        ///             - Arbeitsbereich
        ///             - ADate  >>> Auftragsdatum
        ///             - KD_ID  >>> Kundennummer
        ///             - B_ID  >>>> Beladestelle 
        ///             - E_ID  >>> Enladestelle
        ///             - nB_ID >>> neutrale Beladestelle
        ///             - nE_ID >>> neutrale Entladestelle
        ///             - Status 
        ///             - Gewicht >>> Gesamtgewicht
        ///             - Date Add >>> Auftragserstellungsdatum
        ///             - Relation
        ///             - SearchDateVon
        ///             - SearchDateBis
        ///             - vFracht   >>> Fracht auf VB
        ///             - km >>> Entfernung
        ///             - BenutzerID</summary>
        public Globals._GL_USER GL_User;
        public Globals._GL_SYSTEM GL_System;
        public clsSystem Sys;
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

        public clsAuftragPos AuftragPos;
        public clsADR adrAuftraggeber;
        public clsADR adrBS;
        public clsADR adrES;
        public clsDocScan Docs;

        private DateTime _ADate = default(DateTime);
        private DateTime _Date_Add = default(DateTime);


        public decimal ID { get; set; }
        public decimal ANr { get; set; }
        public decimal Mandanten_ID { get; set; }
        public decimal ArbBereich_ID { get; set; }
        public DateTime ADate { get; set; }
        public decimal KD_ID { get; set; }
        public decimal B_ID { get; set; }
        public decimal nB_ID { get; set; }
        public decimal E_ID { get; set; }
        public decimal nE_ID { get; set; }
        public decimal Gewicht { get; set; }
        public DateTime Date_Add { get; set; }
        public string Relation { get; set; }
        public DateTime SearchDateVon { get; set; }
        public DateTime SearchDateBis { get; set; }
        public decimal vFracht { get; set; }
        public Int32 km { get; set; }

        public decimal BruttoGesamtgewicht { get; set; }
        public decimal NettoGesamtGewicht { get; set; }
        public DataTable dtAuftragPositonByAuftrag { get; set; }
        private string _MouseOverInfo;
        public string MouseOverInfo
        {
            get
            {
                GetMouseOverInfo();
                return _MouseOverInfo;
            }
            set { _MouseOverInfo = value; }
        }
        public string StartupPath { get; set; }
        //**********************************************************************************
        //                                   Methoden
        //**********************************************************************************
        ///<summary>clsAuftrag / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, clsSystem myClsSystem)
        {
            this.GL_System = myGLSystem;
            this.GL_User = myGLUser;
            this.Sys = myClsSystem;
            this.StartupPath = this.Sys.StartupPath;
            this.Mandanten_ID = myClsSystem.AbBereich.MandantenID;
            this.ArbBereich_ID = myClsSystem.AbBereich.ID;

            AuftragPos = new clsAuftragPos();
            AuftragPos.InitClass(this.GL_User, this.GL_System, this.Sys);

            Docs = new clsDocScan();
            Docs.InitClass(this.GL_User, this.GL_System, this.Sys);

            AdrManuell = new clsADRMan();
        }
        ///<summary>clsAuftrag / Copy</summary>
        ///<remarks></remarks>
        public clsAuftrag Copy()
        {
            return (clsAuftrag)this.MemberwiseClone();
        }
        ///<summary>clsAuftrag / GetLastAuftragsnummer</summary>
        ///<remarks>Ermittelt die letzte gespeicherte Auftragsnummer des Mandanten.</remarks>
        ///<param name="strMC">strMC >>> Matchcode</param>
        ///<param name="myDecMandantenID">decBenutzerID >>> User ID zum Eintrag in die LOG DB.</param>
        public static decimal GetLastAuftragsnummer(Globals._GL_USER myGL_User, decimal myDecMandantenID)
        {
            string strSql = string.Empty;
            string strTmp = string.Empty;
            decimal ReturnValue;
            strSql = "SELECT TOP(1) Auftrag.ID " +
                                        "FROM AuftragPos " +
                                        "INNER JOIN Auftrag ON Auftrag.ID=AuftragPos.AuftragTableID " +
                                        "WHERE AuftragPos.Status<>3 " +
                                        "AND Auftrag.MandantenID=" + myDecMandantenID + " " +
                                        "ORDER BY AuftragPos.ID DESC ";

            strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGL_User.User_ID);
            decimal decTmp = 0.0M;
            Decimal.TryParse(strTmp, out decTmp);
            ReturnValue = decTmp;
            return ReturnValue;
        }
        ///<summary>clsAuftrag / GetLastAuftragsnummer</summary>
        ///<remarks>Ermittelt die letzte gespeicherte Auftragsnummer des Mandanten.</remarks>
        ///<param name="strMC">strMC >>> Matchcode</param>
        ///<param name="myDecMandantenID">decBenutzerID >>> User ID zum Eintrag in die LOG DB.</param>
        public void GetLastOrder()
        {
            string strSql = string.Empty;
            string strTmp = string.Empty;
            strSql = "SELECT TOP(1) AuftragPos.ID " +
                                        "FROM AuftragPos " +
                                        "INNER JOIN Auftrag ON Auftrag.ID=AuftragPos.AuftragTableID " +
                                        "WHERE AuftragPos.Status<3 " +
                                        "AND Auftrag.ArbeitsbereichID=" + ArbBereich_ID + " " +
                                        "ORDER BY AuftragPos.ID DESC ";

            strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0.0M;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.AuftragPos.ID = decTmp;
                this.AuftragPos.Fill();
                this.ID = this.AuftragPos.AuftragTableID;
                this.Fill();
            }
        }
        ///<summary>clsAuftrag / GetLastAuftragsnummer</summary>
        ///<remarks>Ermittelt die letzte gespeicherte Auftragsnummer des Mandanten.</remarks>
        ///<param name="strMC">strMC >>> Matchcode</param>
        ///<param name="myDecMandantenID">decBenutzerID >>> User ID zum Eintrag in die LOG DB.</param>
        public void GetFirstOrder()
        {
            string strSql = string.Empty;
            string strTmp = string.Empty;
            strSql = "SELECT TOP(1) AuftragPos.ID " +
                                        "FROM AuftragPos " +
                                        "INNER JOIN Auftrag ON Auftrag.ID=AuftragPos.AuftragTableID " +
                                        "WHERE AuftragPos.Status <3 " +
                                        "AND Auftrag.ArbeitsbereichID=" + ArbBereich_ID + " " +
                                        "ORDER BY Auftrag.ANr, AuftragPos.ID ";

            strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0.0M;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.AuftragPos.ID = decTmp;
                this.AuftragPos.Fill();

                this.ID = this.AuftragPos.AuftragTableID;
                this.Fill();
            }
        }
        ///<summary>clsAuftrag / GetLastAuftragsnummer</summary>
        ///<remarks>Ermittelt die erste gespeicherte Auftragsnummer des Mandanten.</remarks>
        ///<param name="strMC">strMC >>> Matchcode</param>
        ///<param name="myDecMandantenID">decBenutzerID >>> User ID zum Eintrag in die LOG DB.</param>
        public static decimal GetFirstAuftragsnummer(Globals._GL_USER myGL_User, decimal myDecMandantenID)
        {
            string strSql = string.Empty;
            string strTmp = string.Empty;
            decimal ReturnValue;
            strSql = "SELECT MIN(Auftrag_ID) FROM AuftragPos " +
                            "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                            "WHERE AuftragPos.Status<3 " +
                            "AND Auftrag.MandantenID='" + myDecMandantenID + "'";
            strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGL_User.User_ID);
            decimal decTmp = 0.0M;
            Decimal.TryParse(strTmp, out decTmp);
            ReturnValue = decTmp;
            return ReturnValue;
        }
        ///<summary>clsAuftrag / GetLastAuftragsnummer</summary>
        ///<remarks>Ermittelt die nächst kleine / größere Auftragsnummer als die übergebene Auftragsnummer.</remarks>
        ///<param name="myGL_User">myGL_Userr</param>
        ///<param name="myDecMandantenID">myDecMandantenID </param>
        ///<param name="myDecAuftragsID">myDecAuftragsID </param>
        ///<param name="bSmaller">bSmaller >>> kleiner / größer</param>
        public void GetNextOrder(decimal myDecAuftragsID, bool bSmaller)
        {
            decimal decAuftragPos = 0;
            decimal decAuftrag = 0;
            string strSql = string.Empty;
            string strTmp = string.Empty;
            strSql = "SELECT TOP(1) Auftrag.ID as Auftrag, AuftragPos.ID as AuftragPos " +
                                                    "FROM AuftragPos " +
                                                    "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                                    "WHERE AuftragPos.Status<3 " +
                                                    "AND Auftrag.MandantenID=" + Mandanten_ID + " ";
            if (bSmaller)
            {
                strSql = strSql +
                        "AND AuftragPos.Auftrag_ID<" + myDecAuftragsID + " ORDER BY Auftrag_ID DESC ";
            }
            else
            {
                strSql = strSql +
                        "AND AuftragPos.Auftrag_ID>" + myDecAuftragsID + " ORDER BY Auftrag_ID ";
            }
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Auftrag");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                Decimal.TryParse(dt.Rows[i]["Auftrag"].ToString(), out decAuftrag);
                Decimal.TryParse(dt.Rows[i]["AuftragPos"].ToString(), out decAuftragPos);
            }
            if (decAuftrag > 0)
            {
                this.ID = decAuftrag;
                this.Fill();
                this.AuftragPos.ID = decAuftragPos;
                this.AuftragPos.Fill();
            }
        }
        ///<summary>clsAuftrag / GetLastAuftragsnummer</summary>
        ///<remarks>Ermittelt die nächst kleine / größere Auftragsnummer als die übergebene Auftragsnummer.</remarks>
        ///<param name="myGL_User">myGL_Userr</param>
        ///<param name="myDecMandantenID">myDecMandantenID </param>
        ///<param name="myDecAuftragsID">myDecAuftragsID </param>
        ///<param name="bSmaller">bSmaller >>> kleiner / größer</param>
        public static decimal GetNextAuftragsnummer(Globals._GL_USER myGL_User, decimal myDecMandantenID, decimal myDecAuftragsID, bool bSmaller)
        {
            string strSql = string.Empty;
            string strTmp = string.Empty;
            decimal ReturnValue;
            strSql = "SELECT TOP(1)(Auftrag_ID) FROM AuftragPos " +
                            "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                            "WHERE AuftragPos.Status<>3 " +
                            "AND Auftrag.MandantenID='" + myDecMandantenID + "' ";
            if (bSmaller)
            {
                strSql = strSql +
                        "AND AuftragPos.Auftrag_ID<'" + myDecAuftragsID + "' ORDER BY Auftrag_ID DESC ";
            }
            else
            {
                strSql = strSql +
                        "AND AuftragPos.Auftrag_ID>'" + myDecAuftragsID + "' ORDER BY Auftrag_ID ";
            }
            strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGL_User.User_ID);
            decimal decTmp = 0.0M;
            Decimal.TryParse(strTmp, out decTmp);
            ReturnValue = decTmp;
            return ReturnValue;
        }
        ///<summary>clsAuftrag / CheckAuftragIDExistOhneCancledOrder</summary>
        ///<remarks>Prüft, ob die Auftragsnummer existiert und nicht storniert ist.</remarks>
        ///<param name="myGL_User">GL_User >>> Globale Uservariable</param>
        ///<param name="myDecAuftrag">decAuftrag</param>
        ///<param name="myDecMandantenID">decMandantenID</param>
        public static bool CheckAuftragIDExistOhneCancledOrder(Globals._GL_USER myGL_User, decimal myDecAuftrag, decimal myDecMandantenID)
        {
            bool IsIn = false;
            string strSql = string.Empty;
            strSql = "SELECT AuftragPos.ID " +
                    "FROM AuftragPos " +
                    "INNER JOIN  Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                    "WHERE AuftragPos.Auftrag_ID='" + myDecAuftrag + "' AND AuftragPos.Status=3 " +
                    "AND Auftrag.MandantenID='" + myDecMandantenID + "' ";
            IsIn = clsSQLcon.ExecuteSQL_GetValueBool(strSql, myGL_User.User_ID);
            return IsIn;
        }
        ///<summary>clsAuftrag / CheckAuftragIDExist</summary>
        ///<remarks>Prüft, ob die Auftragsnummer existiert.</remarks>
        ///<param name="myGL_User">GL_User >>> Globale Uservariable</param>
        ///<param name="myDecAuftrag">decAuftrag</param>
        ///<param name="myDecMandantenID">decMandantenID</param>
        public static bool CheckAuftragIDExist(Globals._GL_USER myGL_User, decimal myDecAuftrag, decimal myDecMandantenID)
        {
            bool IsIn = false;
            string strSql = string.Empty;
            strSql = "Select ID FROM Auftrag WHERE ANr=" + myDecAuftrag + " AND MandantenID=" + myDecMandantenID + " ";
            IsIn = clsSQLcon.ExecuteSQL_GetValueBool(strSql, myGL_User.User_ID);
            return IsIn;
        }
        ///<summary>clsAuftrag / CheckAuftragIDExistByAB</summary>
        ///<remarks>Prüft, ob die Auftragsnummer existiert.</remarks>
        ///<param name="myGL_User">GL_User >>> Globale Uservariable</param>
        ///<param name="myDecAuftrag">decAuftrag</param>
        ///<param name="myABID">ArbeitsbereichID</param>
        public static bool CheckAuftragIDExistByAB(Globals._GL_USER myGL_User, decimal myDecAuftrag, decimal myAbID)
        {
            bool IsIn = false;
            string strSql = string.Empty;
            strSql = "Select ID FROM Auftrag WHERE ANr=" + myDecAuftrag + " AND ArbeitsbereichID=" + myAbID + " ";
            IsIn = clsSQLcon.ExecuteSQL_GetValueBool(strSql, myGL_User.User_ID);
            return IsIn;
        }
        ///<summary>clsAuftrag / CheckAndDeleteStornoAuftrag</summary>
        ///<remarks>Stornierte Aufträge werden nach 1 Monat aus der Datenbank gelöscht. Hierzu müssen die entsprechenden
        ///         Daten aus folgenden Tabellen gelöscht werden:
        ///         - Table Auftrag
        ///         - Table AuftragPos
        ///         - Artikel
        ///         - Lieferscheine
        ///         Dabei ist Mandant und Arbeitsbereich unrelevant.</remarks>
        ///<param name="myGL_User">GL_User >>> Globale Uservariable</param>
        ///<param name="myDecMandantenID">decMandantenID</param>
        public static void CheckAndDeleteStornoAuftrag(Globals._GL_USER myGL_User, decimal myDecMandantenID)
        {
            string strSql = string.Empty;
            strSql = "SELECT AuftragPos.ID, AuftragPos.Auftrag_ID, AuftragPos.AuftragPos " +
                     "FROM AuftragPos " +
                     "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                     "WHERE Auftrag.ADate <'" + DateTime.Now.AddMonths(-1).ToShortDateString() + "' " +
                     "AND AuftragPos.Status=3 ";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGL_User.User_ID, "AuftragToDelete");

            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    decimal decTmpID = (decimal)dt.Rows[i]["ID"];
                    decimal decTmpAuftrag = (decimal)dt.Rows[i]["Auftrag_ID"];
                    decimal decTmpAuftragPos = (decimal)dt.Rows[i]["AuftragPos"];
                    /************************************
                     * entgültiges löschen der Aufträge
                     * - in Auftrag
                     * - in AuftragPos
                     * - in Artikel 
                     * **********************************/

                    //löschen aus AuftragPos Table
                    clsAuftragPos.DeleteAuftragPos(decTmpAuftrag, decTmpAuftragPos, myGL_User.User_ID);

                    //löschen aus Artikel
                    clsArtikel.DeleteArtikel(decTmpAuftrag, decTmpAuftragPos, myGL_User.User_ID);

                    //Lieferscheine löschen
                    if (clsLieferscheine.LieferscheinExist(decTmpID))
                    {
                        clsLieferscheine.DeleteLieferscheinByAP_ID(decTmpID);
                    }
                    //Auftrag aus Kommission löschen, wenn Status nicht durchgeführt
                    clsKommission.DeleteKommiPosByAuftragAuftragPos(decTmpAuftrag, decTmpAuftragPos);
                    //DB OrderPosRec 
                    if (clsOrderPosRectangle.IsAuftragAuftragPosIn(decTmpAuftrag, decTmpAuftragPos))
                    {
                        clsOrderPosRectangle.DeleteRectanglePosByAuftragAuftragPos(decTmpAuftrag, decTmpAuftragPos);
                    }
                }

                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    decimal decTmpAuftrag = (decimal)dt.Rows[i]["Auftrag_ID"];
                    //Check ob noch eine AuftragPos vorhanden ist, dann erst löschen
                    if (!clsAuftragPos.IsAuftragPosAuftragIn(decTmpAuftrag, myGL_User.User_ID))
                    {
                        clsAuftrag.DeleteAuftrag(myGL_User, decTmpAuftrag);
                    }
                }
            }
        }
        ///<summary>clsAuftrag / DeleteAuftrag</summary>
        ///<remarks>Löscht den Datensatz anhand der AuftragsID aus der Table.</remarks>
        ///<param name="myGL_User">GL_User >>> Globale Uservariable</param>
        ///<param name="myDecAuftragID">myDecAuftragID</param>
        public static void DeleteAuftrag(Globals._GL_USER myGL_User, decimal myDecAuftragID)
        {
            string strSql = string.Empty;
            strSql = "DELETE FROM Auftrag WHERE ANr='" + myDecAuftragID + "'";
            clsSQLcon.ExecuteSQL(strSql, myGL_User.User_ID);
        }
        ///<summary>frmAuftrag_Fast / Update</summary>
        ///<remarks></remarks>
        public void Update()
        {
            Date_Add = DateTime.Now;
            string strSql = "Update Auftrag SET " +
                                              "ANr=" + (Int32)ANr +
                                              ", ADate ='" + ADate + "'" +
                                              ", KD_ID=" + (Int32)KD_ID +
                                              ", B_ID=" + (Int32)B_ID +
                                              ", E_ID=" + (Int32)E_ID +
                                              ", Gewicht='" + Gewicht.ToString().Replace(",", ".") + "'" +
                                              ", Relation='" + Relation + "'" +
                                              ", vFracht='" + vFracht.ToString().Replace(",", ".") + "'" +
                                              ", nB_ID=" + (Int32)nB_ID +
                                              ", nE_ID=" + (Int32)nE_ID +
                                              ", km=" + km +
                                              ", MandantenID=" + Mandanten_ID +
                                              ", ArbeitsbereichID = " + ArbBereich_ID +

                                                 " WHERE ID=" + this.ID + ";";
            clsSQLcon.ExecuteSQL(strSql, BenutzerID);
        }
        ///<summary>clsAuftrag / DeleteAuftragKomplett</summary>
        ///<remarks>Löscht den Datensatz anhand der der Auftrag ID. Es werden auch AuftragPos und Artikel gelsöcht;</remarks>
        public void DeleteAuftragKomplett()
        {
            bool bDeleteDocs = false;
            DataTable dtDocScanAuftrag = new DataTable();
            try
            {
                dtDocScanAuftrag = clsDocScan.GetDocScanTableByAuftrag(ID, this.GL_User);
                string strSql = string.Empty;
                strSql = "DELETE FROM Artikel WHERE AuftragPosTableID IN (" +
                                    "Select ID FROM AuftragPos WHERE AuftragTableID=" + ID + " " +
                                                                          ");" +
                         clsAuftragRead.GetSQLDeleteReadAuftragAuftragTableID(ID) +
                         "DELETE FROM AuftragPos WHERE AuftragTableID=" + ID + "; " +
                         "DELETE FROM Auftrag WHERE ID=" + ID + "; ";
                strSql = strSql + clsDocScan.GetSQLDeleteByAuftrag(ID);

                //clsSQLcon.ExecuteSQL(strSql, BenutzerID);
                bDeleteDocs = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "OrderDelete", this.GL_User.User_ID);
                //bDeleteDocs = true;
            }
            catch (Exception ex)
            {
                bDeleteDocs = false;
            }
            finally
            {
                //eingescannte Dokumente löschen
                if (bDeleteDocs)
                {
                    string Beschreibung = "Auftrag [" + ID.ToString() + " / " + ANr.ToString() + "] gelöscht";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
                    for (Int32 i = 0; i <= dtDocScanAuftrag.Rows.Count - 1; i++)
                    {
                        //string Path = Application.StartupPath + dtDocScanAuftrag.Rows[i]["Pfad"].ToString();
                        string Path = StartupPath + dtDocScanAuftrag.Rows[i]["Pfad"].ToString();
                        if (System.IO.Directory.Exists(Path))
                        {
                            bool bDeleteOK = false;
                            string fileDelete = Path + dtDocScanAuftrag.Rows[i]["ScanFilename"].ToString();
                            try
                            {
                                // Ensure that the target does not exist.
                                File.Delete(fileDelete);
                                bDeleteOK = true;
                            }
                            catch (Exception e)
                            {
                                bDeleteOK = false;
                                Console.WriteLine("The process failed: {0}", e.ToString());
                                Beschreibung = "Dokumente zu Auftrag [" + ID.ToString() + " / " + ANr.ToString() + "] löschen - Exception:" + e.ToString();
                                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), Beschreibung);
                            }
                            finally
                            {
                                //Eintrag in Logbuch
                                if (bDeleteOK)
                                {
                                    Beschreibung = "Dokumente zu Auftrag [" + ID.ToString() + " / " + ANr.ToString() + "] gelöscht";
                                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
                                }
                            }
                        }
                    }
                }
            }
        }
        ///<summary>clsAuftrag / GetGesamtgewichtAuftrag</summary>
        ///<remarks></remarks>
        public void GetGesamtgewichtAuftrag()
        {
            string strSQL = "Select SUM(a.Brutto) as Brutto" +
                                    ", SUM(a.Netto) as Netto " +
                                        "FROM Artikel a " +
                                            "INNER JOIN AuftragPos b ON b.ID =a.AuftragPosTableID " +
                                            "INNER JOIN Auftrag c ON c.ID = b.AuftragTableID " +
                                            "WHERE c.ID=" + this.ID;
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "AuftragGewicht");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Brutto"].ToString(), out decTmp);
                this.BruttoGesamtgewicht = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Netto"].ToString(), out decTmp);
                this.NettoGesamtGewicht = decTmp;
            }
        }
        ///<summary>clsAuftrag / Add</summary>
        ///<remarks>Löscht den Datensatz anhand der AuftragsID aus der Table.</remarks>
        ///<param name="myGL_User">GL_User >>> Globale Uservariable</param>
        ///<param name="myDecAuftragID">myDecAuftragID</param>
        public void Add()
        {
            if (GL_User.User_ID > 0)
            {
                Date_Add = DateTime.Now;
                string strSql = string.Empty;
                //----- SQL Abfrage -----------------------
                strSql = "INSERT INTO Auftrag (ANr, " +
                                                "ADate, " +
                                                "KD_ID, " +
                                                "B_ID, " +
                                                "E_ID, " +
                                                "Gewicht, " +
                                                "Date_Add, " +
                                                "Relation, " +
                                                "vFracht, " +
                                                "nB_ID, " +
                                                "nE_ID, " +
                                                "km, " +
                                                "MandantenID, " +
                                                "ArbeitsbereichID) " +
                                        "VALUES (" + (Int32)ANr +
                                                 ", '" + ADate + "'" +
                                                 ", " + (Int32)KD_ID +
                                                 ", " + (Int32)B_ID +
                                                 ", " + (Int32)E_ID +
                                                 ", '" + Gewicht.ToString().Replace(",", ".") + "'" +
                                                 ", '" + Date_Add + "'" +
                                                 ", '" + Relation + "'" +
                                                 ", '" + vFracht.ToString().Replace(",", ".") + "'" +
                                                 ", " + (Int32)nB_ID +
                                                 ", " + (Int32)nE_ID +
                                                 ", " + km +
                                                 ", " + (Int32)Mandanten_ID +
                                                 ", " + (Int32)ArbBereich_ID +
                                                 "); ";
                strSql = strSql + "Select @@IDENTITY as 'ID' ;";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    this.ID = decTmp;
                    Fill();
                }
            }
        }
        ///<summary>clsAuftrag / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select * FROM Auftrag WHERE ID=" + ID + ";";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Auftrag");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                    this.ID = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["ANr"].ToString(), out decTmp);
                    this.ANr = decTmp;
                    this.ADate = (DateTime)dt.Rows[i]["ADate"];
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["KD_ID"].ToString(), out decTmp);
                    this.KD_ID = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["B_ID"].ToString(), out decTmp);
                    this.B_ID = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["E_ID"].ToString(), out decTmp);
                    this.E_ID = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["Gewicht"].ToString(), out decTmp);
                    this.Gewicht = decTmp;
                    this.Date_Add = (DateTime)dt.Rows[i]["Date_Add"];
                    this.Relation = dt.Rows[i]["Relation"].ToString();
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["vFracht"].ToString(), out decTmp);
                    this.vFracht = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["nB_ID"].ToString(), out decTmp);
                    this.nB_ID = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["nE_ID"].ToString(), out decTmp);
                    this.nE_ID = decTmp;
                    Int32 iTmp = 0;
                    Int32.TryParse(dt.Rows[i]["km"].ToString(), out iTmp);
                    this.km = iTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["MandantenID"].ToString(), out decTmp);
                    this.Mandanten_ID = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["ArbeitsbereichID"].ToString(), out decTmp);
                    this.ArbBereich_ID = decTmp;
                }
                GetGesamtgewichtAuftrag();
                GetTableAuftragPosByAuftrag();
                //manuelle Adressen
                AdrManuell = new clsADRMan();
                AdrManuell.InitClass(this.GL_User, this.ID, "Auftrag");

                Docs = new clsDocScan();
                Docs.InitClass(this.GL_User, this.GL_System, this.Sys);
            }
        }
        ///<summary>clsAuftrag / GetTableAuftragPosByAuftrag</summary>
        ///<remarks></remarks>
        public void GetTableAuftragPosByAuftrag()
        {
            dtAuftragPositonByAuftrag = new DataTable();
            string strSql = string.Empty;
            strSql = "Select b.* " +
                              ", (Select SUM(Artikel.Brutto) FROM Artikel WHERE Artikel.AuftragPosTableID=b.ID) as Gewicht " +
                                    "FROM  AuftragPos b " +

                                    "INNER JOIN Auftrag c ON c.ID = b.AuftragTableID " +
                                    "WHERE c.ID=" + ID + ";";

            dtAuftragPositonByAuftrag = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Auftrag");
        }
        ///<summary>clsAuftrag / InitADRinClass</summary>
        ///<remarks></remarks>
        public void InitADRinClass()
        {
            //Init ADR
            //-Auftraggeber
            if (this.KD_ID > 0)
            {
                adrAuftraggeber = new clsADR();
                adrAuftraggeber.InitClass(this.GL_User, this.GL_System, this.KD_ID, true);
            }
            //-Beladestelle
            if (this.B_ID > 0)
            {
                adrBS = new clsADR();
                adrBS.InitClass(this.GL_User, this.GL_System, this.B_ID, true);
            }
            //-Entladestelle
            if (this.E_ID > 0)
            {
                adrES = new clsADR();
                adrES.InitClass(this.GL_User, this.GL_System, this.E_ID, true);
            }
        }
        ///<summary>clsAuftrag / GetIDbyValue</summary>
        ///<remarks>Ermitteln der ID von Table Auftrag anhand von folgenden Eigenschaftswerte.</remarks>
        ///<param name="myGL_User">GL_User >>> Globale Uservariable</param>
        ///<param name="myDecAuftragID">myDecAuftragID</param>
        public static decimal GetIDbyValue(Globals._GL_USER myGL_User, decimal myDecKD, decimal myDecGewicht, DateTime myDTauftragsdate)
        {
            decimal decTmpAuftrag = 0; ;
            string strTmp = string.Empty;
            string strSql = string.Empty;
            //----- SQL Abfrage -----------------------
            strSql = "SELECT ID FROM Artikel " +
                                "WHERE KD_ID='" + myDecKD + "' " +
                                "AND Gewicht='" + myDecGewicht + "' " +
                                "AND ADate='" + myDTauftragsdate + "'";
            strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGL_User.User_ID);
            if (strTmp != String.Empty)
            {
                Decimal.TryParse(strTmp, out decTmpAuftrag);
            }
            return decTmpAuftrag;
        }
        ///<summary>clsAuftrag / GetAuftragTableIDByAuftragPosTableID</summary>
        ///<remarks></remarks>
        ///<param name="myGL_User">GL_User >>> Globale Uservariable</param>
        ///<param name="myAuftragPosTableID">AUftragPos Table ID</param>
        public static decimal GetAuftragTableIDByAuftragPosTableID(Globals._GL_USER myGL_User, decimal myAuftragPosTableID)
        {
            decimal decTmp = 0;
            if (myAuftragPosTableID > 0)
            {
                string strTmp = string.Empty;
                string strSql = string.Empty;
                //----- SQL Abfrage -----------------------
                strSql = "Select a.ID FROM Auftrag a INNER JOIN AuftragPos b ON b.AuftragTableID=a.ID " +
                                                        "WHERE b.ID='" + myAuftragPosTableID + "';";

                strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGL_User.User_ID);
                if (!Decimal.TryParse(strTmp, out decTmp))
                {
                    decTmp = 0;
                }
            }
            return decTmp;
        }
        ///<summary>clsAuftrag / GetMouseOverInfo</summary>
        ///<remarks></remarks>
        private void GetMouseOverInfo()
        {
            Int32 Col1 = 0;
            Int32 Col2 = 10;
            string strTmp = string.Empty;
            strTmp = strTmp + "Auftragdaten:  ";
            strTmp = strTmp + Environment.NewLine;
            strTmp = strTmp + Environment.NewLine;

            strTmp = strTmp + String.Format("{0}\t{1}", "[ID]: ", this.ID.ToString()) + Environment.NewLine;
            strTmp = strTmp + String.Format("{0}\t{1}", "[Nr.]: ", this.ANr.ToString()) + Environment.NewLine;
            strTmp = strTmp + String.Format("{0}\t{1}", "[Datum]: ", this.ADate.ToLongDateString()) + Environment.NewLine;
            strTmp = strTmp + Environment.NewLine;
            MouseOverInfo = strTmp;
        }
        ///<summary>clsAuftrag / GetAuftragIDbyANr</summary>
        ///<remarks>Ermitteln der ID von Table Auftrag anhand von der Auftragsnummer.</remarks>
        ///<param name="myGL_User">GL_User >>> Globale Uservariable</param>
        ///<param name="myDecAuftragID">Auftragsnummer</param>
        public static decimal GetAuftragIDbyANr(Globals._GL_USER myGL_User, decimal myDecANr)
        {
            decimal myDecAuftragID = 0;
            if (myDecANr > 0)
            {
                string strTmp = string.Empty;
                string strSql = string.Empty;
                //----- SQL Abfrage -----------------------
                strSql = "SELECT ID FROM Auftrag " +
                                    "WHERE ANr='" + myDecANr + "' ";

                strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGL_User.User_ID);
                if (strTmp != String.Empty)
                {
                    if (!Decimal.TryParse(strTmp, out myDecAuftragID))
                    {
                        myDecAuftragID = 0;
                    }
                }
            }
            return myDecAuftragID;
        }
        ///<summary>clsAuftrag / GetAuftragIDbyANrAndAbBereich</summary>
        ///<remarks>Ermitteln der ID von Table Auftrag anhand von der Auftragsnummer.</remarks>
        ///<param name="myGL_User">GL_User >>> Globale Uservariable</param>
        ///<param name="myDecAuftragID">Auftragsnummer</param>
        public static decimal GetAuftragIDbyANrAndAbBereich(Globals._GL_USER myGL_User, decimal myDecANr, decimal myAbID)
        {
            decimal myDecAuftragID = 0;
            if (myDecANr > 0)
            {
                string strTmp = string.Empty;
                string strSql = string.Empty;
                //----- SQL Abfrage -----------------------
                strSql = "SELECT ID FROM Auftrag " +
                                    "WHERE ANr=" + myDecANr + " AND ArbeitsbereichID=" + myAbID + "; ";

                strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGL_User.User_ID);
                if (strTmp != String.Empty)
                {
                    if (!Decimal.TryParse(strTmp, out myDecAuftragID))
                    {
                        myDecAuftragID = 0;
                    }
                }
            }
            return myDecAuftragID;
        }
        ///<summary>clsAuftrag / GetANrByID</summary>
        ///<remarks>Ermitteln der ANr von Table Auftrag anhand von der ID.</remarks>
        ///<param name="myGL_User">GL_User >>> Globale Uservariable</param>
        ///<param name="myDecTableID">ID der Table Auftrag</param>
        public static decimal GetANrByID(Globals._GL_USER myGL_User, decimal myDecTableID)
        {
            decimal myDecANr = 0;
            if (myDecTableID > 0)
            {
                string strTmp = string.Empty;
                string strSql = string.Empty;
                //----- SQL Abfrage -----------------------
                strSql = "SELECT ANr FROM Auftrag " +
                                    "WHERE ID='" + myDecTableID + "' ";

                strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGL_User.User_ID);
                if (strTmp != String.Empty)
                {
                    if (!Decimal.TryParse(strTmp, out myDecANr))
                    {
                        myDecANr = 0;
                    }
                }
            }
            return myDecANr;
        }
        ///<summary>clsAuftrag / GetNeutraleADR</summary>
        ///<remarks></remarks>
        public static decimal GetNeutraleADR(decimal Auftrag, string ColName)
        {
            decimal decVal = 0;
            if (Auftrag > 0)
            {
                string strCol = string.Empty;

                if (ColName == "Versender")
                {
                    strCol = "nB_ID";
                }
                if (ColName == "Empfaenger")
                {
                    strCol = "nE_ID";
                }

                try
                {
                    DataTable ANr_Table = new DataTable();
                    SqlDataAdapter ada = new SqlDataAdapter();
                    SqlCommand Command = new SqlCommand();
                    Command.Connection = Globals.SQLcon.Connection;
                    ada.SelectCommand = Command;
                    Command.CommandText = "SELECT " + strCol + " FROM Auftrag WHERE ANr='" + Auftrag + "'";

                    Globals.SQLcon.Open();

                    object obj = Command.ExecuteScalar();


                    if ((obj == null) | (obj is DBNull))
                    {
                        decVal = 0;
                    }
                    else
                    {
                        decVal = (decimal)obj;
                    }
                    Command.Dispose();
                    Globals.SQLcon.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                }
            }
            return decVal;
        }
        ///<summary>clsAuftrag / ReadDataByAuftragPosTableID</summary>
        ///<remarks></remarks>
        public static DataSet ReadDataByAuftragPosTableID(Globals._GL_USER myGLUser, decimal myAuftragPosTableID)
        {
            DataSet ds = new DataSet();
            ds.Clear();
            string strSQL = "SELECT AuftragPos.ID," +
                                            "AuftragPos.LieferTermin, " +
                                            "AuftragPos.LieferZF, " +
                                            "AuftragPos.VSB, " +
                                            "AuftragPos.Status, " +
                                            "AuftragPos.Ladenummer, " +
                                            "AuftragPos.LadeNrRequire, " +
                                             "AuftragPos.Prioritaet as 'Prio', " +
                                             "AuftragPos.Notiz, " +  //aus DB AuftragPos und nicht aus Auftrag
                                             "(Select SUM(gemGewicht) FROM Artikel WHERE Artikel.AuftragPosTableID='" + myAuftragPosTableID + "') as 'gemPosGewicht', " +
                                             "(Select SUM(Netto) FROM Artikel WHERE Artikel.AuftragPosTableID='" + myAuftragPosTableID + "') as 'Netto', " +
                                             "(Select SUM(Brutto) FROM Artikel WHERE Artikel.AuftragPosTableID='" + myAuftragPosTableID + "') as 'Brutto', " +
                                             "Auftrag.ADate as 'ADate', " +
                                             "Auftrag.KD_ID as 'KD_ID', " +
                                             "Auftrag.B_ID as 'B_ID', " +
                                             "Auftrag.E_ID as 'E_ID', " +
                                             "Auftrag.nB_ID as 'nB_ID', " +
                                             "Auftrag.nE_ID as 'nE_ID', " +
                                             "Auftrag.Relation as 'Relation', " +
                                             "Auftrag.km  as 'km', " +
                                             "AuftragPos.vKW, " +
                                             "AuftragPos.bKW, " +
                                             "Auftrag.vFracht as 'vFracht', " +
                                             "Auftrag.MandantenID, " +
                                             "Auftrag.ArbeitsbereichID " +
                                             "FROM AuftragPos " +
                                             "INNER JOIN Auftrag ON Auftrag.ID=AuftragPos.AuftragTableID " +
                                             "WHERE AuftragPos.ID='" + myAuftragPosTableID + "' ";

            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Auftrag");
            ds.Tables.Add(dt);
            return ds;
        }
        ///<summary>clsAuftrag / GetAuftragsdatenByZeitraumAndStatus</summary>
        ///<remarks></remarks>
        public DataTable GetAuftragsdatenByZeitraumAndStatus(DateTime Date_von, DateTime Date_bis, Int32 Status, bool vergabe)
        {
            DataTable dataTable = new DataTable();
            string strSQL = string.Empty;
            strSQL = "DECLARE @crlf char(2); " +
                     "SET @crlf = char(13) + char(10); " +
                     "SELECT " +
                              "AuftragPos.ID " +
                              ", Auftrag.ID as AuftragTableID" +
                              ", Auftrag.ANr as AuftragID " +
                              ", AuftragPos.AuftragPos as AuftragPos" +
                              ", Auftrag.KD_ID as Auftraggeber " + //Auftraggeber ID in ADR die ID
                              ", Auftrag.B_ID as Beladestelle " +
                              ", Auftrag.E_ID as Entladestelle " +

                              ", (Select ViewID FROM ADR WHERE ADR.ID =Auftrag.KD_ID) as 'AuftraggeberMC' " +
                              ", (Select Name1 FROM ADR WHERE ADR.ID =Auftrag.KD_ID) as 'AuftraggeberName' " +
                              ", (Select PLZ FROM ADR WHERE ADR.ID =Auftrag.KD_ID) as 'AuftraggeberPLZ' " +
                              ", (Select Ort FROM ADR WHERE ADR.ID =Auftrag.KD_ID) as 'AuftraggeberOrt' " +

                              ", (Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_Name' " +
                              ", (Select ADR.Str FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_Strasse' " +
                              ", (Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.B_ID) as 'B_PLZ' " +
                              ", (Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'B_Ort' " +

                              ", (Select ADR.Name1 FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_Name' " +
                              ", (Select ADR.Str FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_Strasse' " +
                              ", (Select ADR.PLZ FROM ADR WHERE ADR.ID= Auftrag.E_ID) as 'E_PLZ' " +
                              ", (Select ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'E_Ort' " +

                              ", (Select ADR.PLZ +' '+ ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as StandardADR_A " +
                              ", (Select ADR.Name1 + @crlf + ADR.PLZ +' '+ ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.KD_ID) as DetailADR_A " +

                              ", CASE " +
                                "WHEN (Select b.ID FROM ADRMan b where b.TableID=Auftrag.ID AND b.TableName='Auftrag' AND b.AdrArtID=1)>0 " +
                                "THEN (Select b.PLZ+' '+b.Ort FROM ADRMan b where b.TableID=Auftrag.ID AND b.TableName='Auftrag' AND b.AdrArtID=1) " +
                                "ELSE (Select ADR.PLZ +' '+ ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.B_ID) " +
                                "END as StandardADR_V " +
                             ", CASE " +
                                "WHEN (Select b.ID FROM ADRMan b where b.TableID=Auftrag.ID AND b.TableName='Auftrag' AND b.AdrArtID=1)>0 " +
                                "THEN (Select b.Name1+ @crlf + b.PLZ+' '+b.Ort FROM ADRMan b where b.TableID=Auftrag.ID AND b.TableName='Auftrag' AND b.AdrArtID=1) " +
                                "ELSE (Select ADR.Name1 + @crlf +  ADR.PLZ +' '+ ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.B_ID) " +
                                "END as DetailADR_V " +
                             ", CASE " +
                                "WHEN (Select b.ID FROM ADRMan b where b.TableID=Auftrag.ID AND b.TableName='Auftrag' AND b.AdrArtID=3)>0 " +
                                "THEN (Select b.PLZ+' '+b.Ort FROM ADRMan b where b.TableID=Auftrag.ID AND b.TableName='Auftrag' AND b.AdrArtID=3) " +
                                "ELSE (Select ADR.PLZ +' '+ ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.E_ID) " +
                                "END as StandardADR_E " +
                             ", CASE " +
                                "WHEN (Select b.ID FROM ADRMan b where b.TableID=Auftrag.ID AND b.TableName='Auftrag' AND b.AdrArtID=3)>0 " +
                                "THEN (Select b.Name1+ @crlf + b.PLZ+' '+b.Ort FROM ADRMan b where b.TableID=Auftrag.ID AND b.TableName='Auftrag' AND b.AdrArtID=3) " +
                                "ELSE (Select ADR.Name1 + @crlf +  ADR.PLZ +' '+ ADR.Ort FROM ADR WHERE ADR.ID=Auftrag.E_ID) " +
                                "END as DetailADR_E " +


                              ", (Select ADR.WAvon FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'WAvB' " +
                              ", (Select ADR.WAbis FROM ADR WHERE ADR.ID=Auftrag.B_ID) as 'WAbB' " +

                              ", (Select ADR.WAvon FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'WAvE' " +
                              ", (Select ADR.WAbis FROM ADR WHERE ADR.ID=Auftrag.E_ID) as 'WAbE' " +

                              ", (Select SUM(Artikel.Netto) FROM Artikel WHERE Artikel.AuftragPosTableID=AuftragPos.ID) as Netto " +
                              ", (Select SUM(Artikel.Brutto) FROM Artikel WHERE Artikel.AuftragPosTableID=AuftragPos.ID) as Brutto " +
                              ", (Select SUM(Artikel.Brutto) FROM Artikel " +
                                                             "INNER JOIN AuftragPos Pos ON Artikel.AuftragPosTableID = Pos.ID " +
                                                             "INNER JOIN Auftrag a ON a.ID = Pos.AuftragTableID " +
                                                             "WHERE a.ID=Auftrag.ID) as Gesamtgewicht " +
                              ", (SELECT drDate FROM Lieferschein WHERE Lieferschein.AP_ID=AuftragPos.ID) as drDate " +
                              ", (Select Top(1) Gueterart.Bezeichnung FROM Artikel " +
                                                                      "INNER JOIN AuftragPos on Artikel.AuftragPosTableID=AuftragPos.ID " +
                                                                      "LEFT JOIN Gueterart on Gueterart.ID = Artikel.GArtID) as Gut " +
                              ", AuftragPos.VSB as VSB " +

                              ", AuftragPos.LieferTermin" +
                              ", AuftragPos.LieferZF " +
                              ", AuftragPos.LieferZFRequire " +

                              ", AuftragPos.LadeTermin" +
                              ", AuftragPos.LadeZF" +
                              ", AuftragPos.LadeZFRequire " +

                              ", AuftragPos.Status as Status " +
                              ", AuftragPos.Prioritaet as Prio " +
                              ", AuftragPos.Ladenummer " +
                              ", Auftrag.Relation as 'Relation' " +
                              ", AuftragPos.vKW" +
                              ", AuftragPos.bKW " +
                              ", CASE " +
                                  "WHEN ISNULL((Select AuftragRead.ID FROM AuftragRead WHERE AuftragRead.AuftragPosID=AuftragPos.ID AND AuftragRead.UserID=" + this.GL_User.User_ID + "),0) >0 " +
                                  "THEN 1 " +
                                  "ELSE 0 " +
                                  "End as [Read] " +
                              ", CASE " +
                                  "WHEN ISNULL((Select Top(1) DocScan.ID FROM DocScan WHERE DocScan.AuftragTableID=Auftrag.ID),0) >0 " +
                                  "THEN 1 " +
                                  "ELSE 0 " +
                                  "End as Scan ";

            if (!vergabe)
            {
                if (Status == 0)
                {
                    //In dieser Liste müssen alle Aufträge aufgeführt werden:
                    //- offenen Aufträge
                    //- disponierte Aufträge 
                    //- vergebene Aufträge an SU
                    //über diese drei Mengen muss dann ein Union gemacht werden, damit eine SQL - Abfrage entsteht
                    string strSQLDisponiert = string.Empty;
                    string strSQLFremd = string.Empty;
                    string strSQLOffen = string.Empty;
                    strSQLDisponiert = strSQL;
                    strSQLFremd = strSQL;
                    strSQLOffen = strSQL;

                    //offenen Aufträge
                    strSQLOffen = strSQLOffen +
                                  ",'' as 'Beladezeit' " +
                                  ",''  as 'Entladezeit'  " +
                                  ", '' as 'Ressource' " +
                                  "FROM AuftragPos " +
                                      "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                      "WHERE " +
                                              " AuftragPos.Status<4 " +
                                              "AND Auftrag.ArbeitsbereichID=" + this.ArbBereich_ID + " ";


                    strSQLDisponiert = strSQLDisponiert + " " +
                            ",(Select Tour.StartZeit FROM Tour INNER JOIN Kommission ON Kommission.TourID=Tour.ID WHERE Kommission.PosID=AuftragPos.ID)  as 'Beladezeit' " +
                            ",(Select Tour.EndZeit FROM Tour INNER JOIN Kommission ON Kommission.TourID=Tour.ID WHERE Kommission.PosID=AuftragPos.ID)   as 'Entladezeit'  " +
                            ", (Select Fahrzeuge.KFZ FROM Tour INNER JOIN Kommission ON Kommission.TourID=Tour.ID INNER JOIN Fahrzeuge ON Fahrzeuge.ID=Tour.KFZ_ZM WHERE Kommission.PosID=AuftragPos.ID) as 'Ressource' " +
                             "FROM AuftragPos " +
                            "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                            "WHERE " +
                                  "(AuftragPos.Status>=4) " +
                                  "AND Auftrag.ArbeitsbereichID=" + this.ArbBereich_ID + " " +
                                  "AND AuftragPos.ID IN (SELECT PosID FROM Kommission) ";


                    strSQLFremd = strSQLFremd +
                            ", (Select Frachtvergabe.B_Date FROM Frachtvergabe WHERE Frachtvergabe.ID_AP=AuftragPos.ID) as 'Beladezeit' " +
                            ", (Select Frachtvergabe.E_Date FROM Frachtvergabe WHERE Frachtvergabe.ID_AP=AuftragPos.ID) as 'Entladezeit' " +
                            ", (Select ADR.Name1 FROM Frachtvergabe INNER JOIN ADR ON ADR.ID=Frachtvergabe.SU WHERE Frachtvergabe.ID_AP=AuftragPos.ID) as 'Ressource' " +
                             "FROM AuftragPos " +
                                     "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                     "WHERE " +
                                             "(AuftragPos.Status>=4) " +
                                             "AND Auftrag.ArbeitsbereichID=" + this.ArbBereich_ID + " " +
                                             "AND AuftragPos.ID IN (SELECT ID_AP FROM Frachtvergabe) ";



                    strSQL = string.Empty;
                    strSQL = strSQLOffen +
                             " UNION " +
                             strSQLDisponiert +
                             " UNION " +
                             strSQLFremd +

                             " Order by Auftrag.ANr DESC; ";
                }

                if (Status == 3) // Storniert
                {
                    if ((Date_von == DateTime.MinValue) & (Date_bis == DateTime.MaxValue))
                    {
                        strSQL = strSQL + " " +
                                              "FROM AuftragPos " +
                                              "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                              "WHERE " +
                                                  "AuftragPos.Status=3 " +
                                                  "AND Auftrag.ArbeitsbereichID=" + this.GL_System.sys_ArbeitsbereichID + " " +
                                                  "AND AuftragPos.ID NOT IN (SELECT PosID FROM Kommission) " +
                                              "Order by AuftragPos.LieferTermin ";
                    }
                    else
                    {
                        strSQL = strSQL + " " +
                            "FROM AuftragPos " +
                            "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                            "WHERE " +
                                  "AuftragPos.Status='3' " +
                                  "AND AuftragPos.LieferTermin>'" + Date_von.AddDays(-1).ToShortDateString() + "' " +
                                  "AND AuftragPos.LieferTermin<'" + Date_bis.AddDays(1).ToShortDateString() + "' " +
                                  "AND Auftrag.ArbeitsbereichID=" + this.ArbBereich_ID + " " +
                                  "AND AuftragPos.ID NOT IN (SELECT PosID FROM Kommission) " +
                            "Order by AuftragPos.LieferTermin ";
                    }
                }
                if ((Status > 0) && (Status < 3))
                {
                    strSQL = strSQL +
                             "FROM AuftragPos " +
                                                    "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                                    "WHERE " +
                                                            "AuftragPos.Status<'3' " +
                                                            " AND Auftrag.ArbeitsbereichID=" + ArbBereich_ID + " " +
                                                            "AND (" +
                                                                    "(DATEDIFF(dd, AuftragPos.LieferTermin,'" + Date_bis.AddDays(1).ToShortDateString() + "')>-1) " +
                                                                     "OR " +
                                                                     "(AuftragPos.LieferTermin='" + ((DateTime)Globals.DefaultDateTimeMaxValue).ToString() + "') " +
                                                                  ") " +
                                                            "AND Auftrag.ArbeitsbereichID=" + this.ArbBereich_ID + " " +
                                                            "AND AuftragPos.ID NOT IN (SELECT PosID FROM Kommission) " +
                                                    "Order by AuftragPos.LieferTermin ";
                }
                if (Status >= 4) //disponiert oder durchgeführt
                {
                    strSQL = strSQL +
                              ", (Select Tour.StartZeit FROM Tour INNER JOIN Kommission ON Kommission.TourID=Tour.ID WHERE Kommission.PosID=AuftragPos.ID)  as 'Beladezeit' " +
                              ", (Select Tour.EndZeit FROM Tour INNER JOIN Kommission ON Kommission.TourID=Tour.ID WHERE Kommission.PosID=AuftragPos.ID)   as 'Entladezeit'  " +
                              " FROM AuftragPos " +
                                                    "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                                    "WHERE " +
                                                            "AuftragPos.Status='" + Status + "' " +
                                                            " AND Auftrag.ArbeitsbereichID=" + ArbBereich_ID + " " +
                                                            " AND (" +
                                                                   "( (AuftragPos.LieferTermin>'" + Date_von.AddDays(-1).ToShortDateString() + "') " +
                                                                       "AND " +
                                                                      "(AuftragPos.LieferTermin<'" + Date_bis.AddDays(1).ToShortDateString() + "') " +
                                                                   ")" +
                                                                   "OR (AuftragPos.LieferTermin='" + ((DateTime)Globals.DefaultDateTimeMaxValue).ToString() + "') "
                                                                + ") " +
                                                            "AND (AuftragPos.Status='" + Status + "') " +
                                                            "AND Auftrag.ArbeitsbereichID=" + this.ArbBereich_ID + " " +
                                                            "AND AuftragPos.ID IN (SELECT PosID FROM Kommission) " +
                                                    "Order by AuftragPos.LieferTermin ";
                }
            }
            else // an SU vergeben
            {
                strSQL = strSQL +

                        ", (Select Frachtvergabe.B_Date FROM Frachtvergabe WHERE Frachtvergabe.ID_AP=AuftragPos.ID) as 'Beladezeit' " +
                        ", (Select Frachtvergabe.E_Date FROM Frachtvergabe WHERE Frachtvergabe.ID_AP=AuftragPos.ID) as 'Entladezeit' " +
                        ", (Select Frachtvergabe.SU FROM Frachtvergabe WHERE Frachtvergabe.ID_AP=AuftragPos.ID) as 'SU_ID'  " +
                        " FROM AuftragPos " +
                                      "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                      "WHERE " +
                                              "(AuftragPos.Status>'3' " +
                                              "AND AuftragPos.Status<'7') " +
                                              " AND Auftrag.ArbeitsbereichID=" + ArbBereich_ID + " " +
                                              "AND (" +
                                                      "(AuftragPos.LieferTermin>'" + Date_von.AddDays(-1).ToShortDateString() + "' " +
                                                      "AND AuftragPos.LieferTermin<'" + Date_bis.AddDays(1).ToShortDateString() + "'" +
                                                   ") " +
                                              "OR (AuftragPos.LieferTermin='" + ((DateTime)Globals.DefaultDateTimeMaxValue).ToString() + "')) " +
                                              "AND Auftrag.ArbeitsbereichID=" + this.ArbBereich_ID + " " +
                                              "AND AuftragPos.ID IN (SELECT ID_AP FROM Frachtvergabe) " +
                                              "Order by 'Beladezeit' ";

            }

            dataTable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, this.GL_User.User_ID, "Aufträge");
            return dataTable;
        }
        ///<summary>clsAuftrag / updateRelation</summary>
        ///<remarks></remarks>
        public static void updateRelation(decimal AuftragNr, string strRelation, Globals._GL_USER GLUser)
        {
            string strSQL = "Update Auftrag SET Relation='" + strRelation + "' " +
                                                 "WHERE ANr='" + AuftragNr + "'";

            bool bOK = clsSQLcon.ExecuteSQL(strSQL, GLUser.User_ID);
        }
        ///<summary>clsAuftrag / updateADR_ID</summary>
        ///<remarks></remarks>
        public static void updateADR_ID(decimal Auftragnummer, string ADRBezeichnung, decimal ADR_ID)
        {
            string sql = string.Empty;
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                switch (ADRBezeichnung)
                {
                    case "Auftraggeber":
                        sql = "Update Auftrag SET KD_ID='" + ADR_ID + "' WHERE ANr='" + Auftragnummer + "'";
                        break;
                    case "Versender":
                        sql = "Update Auftrag SET B_ID='" + ADR_ID + "' WHERE ANr='" + Auftragnummer + "'";
                        break;
                    case "Empfänger":
                        sql = "Update Auftrag SET E_ID='" + ADR_ID + "' WHERE ANr='" + Auftragnummer + "'";
                        break;
                    case "nVersender":
                        sql = "Update Auftrag SET nB_ID='" + ADR_ID + "' WHERE ANr='" + Auftragnummer + "'";
                        break;
                    case "nEmpfänger":
                        sql = "Update Auftrag SET nE_ID='" + ADR_ID + "' WHERE ANr='" + Auftragnummer + "'";
                        break;
                }

                Command.CommandText = sql;

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

            }
            finally
            {
                //MessageBox.Show("Update durchgeführt");
            }
        }
        ///<summary>clsAuftrag / GetArtikelForOrderList</summary>
        ///<remarks></remarks>
        public DataTable GetArtikelForOrderList()
        {
            string strSQL = string.Empty;
            strSQL = "Select " +
                              "Artikel.ID" +
                              ", Artikel.Werksnummer" +
                              ", Artikel.Produktionsnummer" +
                              ", Artikel.Dicke" +
                              ", Artikel.Breite" +
                              ", Artikel.Laenge" +
                              //", Artikel.Netto "+
                              ", Artikel.Brutto " +
                              ", Artikel.AuftragPosTableID" +
                              " FROM Artikel " +
                                  "WHERE Artikel.AuftragPosTableID IN(" +
                                                   "SELECT " +
                                                      "AuftragPos.ID " +
                                                          "FROM AuftragPos " +
                                                          "INNER JOIN Auftrag ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                                          "WHERE AuftragPos.Status<4 " +
                                                  ")";

            DataTable dataTable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, this.GL_User.User_ID, "Artikel");
            return dataTable;
        }
        ///<summary>clsAuftrag / GetVFrachtByAuftragID</summary>
        ///<remarks></remarks>
        public static decimal GetVFrachtByAuftragID(decimal auftrag)
        {
            decimal vFracht = 0.0m;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT vFracht FROM Auftrag WHERE ANr='" + auftrag + "'";
            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();
            if ((obj == null) | (obj is DBNull))
            {
                vFracht = 0.0m;
            }
            else
            {
                vFracht = (decimal)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return vFracht;

        }
    }
}
