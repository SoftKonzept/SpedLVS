using System;
using System.Data;
using System.Data.SqlClient;
//using System.Windows.Forms;

namespace LVS
{
    public class clsKommission
    {
        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GL_System;
        public clsSystem _Sys;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }
        //************************************
        public clsTour Tour;
        public bool bDragDrop = false;
        public bool EntLadeZeitChange = false;

        public decimal ID { get; set; }
        public decimal _AuftragPosTableID;
        public decimal AuftragPosTableID
        {
            get { return _AuftragPosTableID; }
            set
            {
                _AuftragPosTableID = value;
            }

        }
        public decimal AuftragPos { get; set; }
        public decimal AuftragID { get; set; }
        private bool _FahrerKontakt;
        public bool FahrerKontakt
        {
            get
            {
                string strSQL = string.Empty;
                strSQL = "Select DISTINCT  ID FROM TourKontaktInfo WHERE TourID=" + TourID +
                                                            " AND KommissionsID=" + ID + ";";
                _FahrerKontakt = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
                return _FahrerKontakt;
            }
            set { _FahrerKontakt = value; }
        }
        private DateTime _BeladeZeit = default(DateTime);
        private DateTime _EntladeZeit = default(DateTime);
        private Int32 _maxBeladePos;
        private Int32 _maxEntladePos;
        private decimal _B_ID;
        public decimal B_ID
        {
            get { return _B_ID; }
            set
            {
                _B_ID = value;
                string strSQL = string.Empty;
                strSQL = "SELECT Ort FROM ADR WHERE ID = " + _B_ID;
                this.Beladestelle = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                this.Beladestelle = this.Beladestelle.Trim();
            }
        }
        private decimal _E_ID;
        public decimal E_ID
        {
            get { return _E_ID; }
            set
            {
                _E_ID = value;
                string strSQL = string.Empty;
                strSQL = "SELECT Ort FROM ADR WHERE ID = " + _E_ID;
                this.Entladestelle = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                this.Entladestelle = this.Entladestelle.Trim();
            }
        }

        public DateTime BeladeZeitFixed;

        public DateTime BeladeZeitShownCtr;
        public DateTime EntladeZeitShownCtr;
        public bool IsActiv;
        public bool bKommiHasRemoved;
        public bool bWeightOverloadOK;
        public bool autoPlaced { get; set; }
        public bool bKommiIsFromDB { get; set; }
        public DateTime TerminVorgabe { get; set; }
        public Int32 km { get; set; }
        public Int32 BeladePos { get; set; }
        public Int32 maxBeladePos
        {
            get
            {
                if (clsTour.ExistTourID(this._GL_User, TourID))
                {
                    string strSQL = string.Empty;
                    strSQL = "Select MAX(BeladePos) FROM Kommission WHERE TourID='" + TourID + "';";
                    string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                    Int32 iTmp = 0;
                    Int32.TryParse(strTmp, out iTmp);
                    _maxBeladePos = iTmp;
                }
                return _maxBeladePos;
            }
            set { _maxBeladePos = value; }
        }
        public Int32 EntladePos { get; set; }
        public Int32 maxEntladePos
        {
            get
            {
                if (clsTour.ExistTourID(this._GL_User, TourID))
                {
                    string strSQL = string.Empty;
                    strSQL = "Select MAX(EntladePos) FROM Kommission WHERE TourID='" + TourID + "';";
                    string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                    Int32 iTmp = 0;
                    Int32.TryParse(strTmp, out iTmp);
                    _maxEntladePos = iTmp;
                }
                return _maxEntladePos;

            }
            set { _maxEntladePos = value; }
        }
        public string Entladestelle { get; set; }
        public string Beladestelle { get; set; }
        public string BeladestelleBez { get; set; }
        public string EntladestelleBez { get; set; }
        public DateTime BeladeZeit { get; set; }
        public DateTime EntladeZeit { get; set; }
        public bool document { get; set; }
        public DateTime KontaktDate { get; set; }
        public string Gut { get; set; }
        public decimal Menge { get; set; }
        public bool documentOld { get; set; }
        public bool FahrerKontaktOld { get; set; }
        public string KontaktInfo { get; set; }
        public Int32 Status { get; set; }
        public decimal Personal { get; set; }
        public decimal Brutto { get; set; }
        public decimal Netto { get; set; }
        public decimal TourID { get; set; }

        /****************************************************************************************
        *                            Mehtoden / Procedure
        * *************************************************************************************/
        ///<summary>clsKommission / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER GLUser, Globals._GL_SYSTEM GLSystem, clsSystem myClsSystem)
        {
            this._GL_User = GLUser;
            this._GL_System = GLSystem;
            this._Sys = myClsSystem;
            Tour = new clsTour();
            Tour.InitClass(GLUser, GLSystem, myClsSystem);
        }
        ///<summary>clsKommission / GetSQLFillByAuftragPosTableID</summary>
        ///<remarks></remarks>
        private string GetSQLFillByAuftragPosTableID()
        {
            string strSQL = string.Empty;
            if (ExistKommissionByAuftragPosID())
            {
                strSQL = "SELECT a.ID " +
                                ", b.ID as PosID" +
                                ", b.AuftragTableID" +
                                ", c.ANr as Auftrag" +
                                ", b.AuftragPos" +
                                ", c.B_ID" +
                                ", c.E_ID" +
                                ", a.B_Zeit" +
                                ", a.E_Zeit" +
                                ", (Select SUM(Netto) FROM Artikel WHERE Artikel.AuftragPosTableID=b.ID) as Netto" +
                                ", (Select SUM(Brutto) FROM Artikel WHERE Artikel.AuftragPosTableID=b.ID) as Brutto" +
                                ", b.T_Date as Termin" +
                                ", c.km" +
                                ", a.Docs" +
                                ", a.TourID" +
                                ", a.BeladePos" +
                                ", a.EntladePos" +
                              " FROM Kommission a " +
                              "INNER JOIN AuftragPos b ON b.ID=a.PosID " +
                              "INNER JOIN Auftrag c ON c.ID=b.AuftragTableID " +
                              "WHERE b.ID=" + this.AuftragPosTableID + " ";
            }
            else
            {
                strSQL = "SELECT " +
                        "0 as ID " +
                        ", b.ID as PosID" +
                        ", b.AuftragTableID" +
                        ", c.ANr as Auftrag" +
                        ", b.AuftragPos" +
                        ", c.B_ID" +
                        ", c.E_ID" +
                        ", 0 as B_Zeit" +
                        ", b.T_Date as E_Zeit" +
                        ", (Select SUM(Netto) FROM Artikel WHERE Artikel.AuftragPosTableID=b.ID) as Netto" +
                        ", (Select SUM(Brutto) FROM Artikel WHERE Artikel.AuftragPosTableID=b.ID) as Brutto" +
                        ", b.T_Date as Termin" +
                        ", c.km" +
                        ", CAST(0 as BIT) as Docs" +
                        ", CAST(0 as DECIMAL (28,0)) as TourID" +
                        ", CAST(0 as INT) as BeladePos" +
                        ", CAST(0 as INT) as EntladePos" +
                      " FROM AuftragPos b " +
                      "INNER JOIN Auftrag c ON c.ID=b.AuftragTableID " +
                      "WHERE b.ID=" + this.AuftragPosTableID + " ";
            }
            return strSQL;
        }
        ///<summary>clsKommission / FillByAuftragPosTableID</summary>
        ///<remarks>Ermittel anhand der ID die Daten.</remarks>
        public void FillByAuftragPosTableID()
        {
            DataTable dt = new DataTable();
            string strSQL = GetSQLFillByAuftragPosTableID();
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Kommission");
            if (dt.Rows.Count > 0)
            {
                SetClassValue(ref dt);
            }
        }
        ///<summary>clsKommission / FillByID</summary>
        ///<remarks>Ermittel anhand der ID die Daten.</remarks>
        public void FillByID()
        {
            if (ExistKommission(this._GL_User, ID))
            {
                DataTable dt = new DataTable();
                string strSQL = string.Empty;
                strSQL = "SELECT a.ID " +
                                ", b.ID as PosID" +
                                ", b.AuftragTableID" +
                                ", c.ANr as Auftrag" +
                                ", b.AuftragPos" +
                                ", c.B_ID" +
                                ", c.E_ID" +
                                ", a.B_Zeit" +
                                ", a.E_Zeit" +
                                ", (Select SUM(Netto) FROM Artikel WHERE Artikel.AuftragPosTableID=b.ID) as Netto" +
                                ", (Select SUM(Brutto) FROM Artikel WHERE Artikel.AuftragPosTableID=b.ID) as Brutto" +
                                ", b.T_Date as Termin" +
                                ", c.km" +
                                ", a.Docs" +
                                ", a.TourID" +
                                ", a.BeladePos" +
                                ", a.EntladePos" +
                              " FROM Kommission a " +
                              "INNER JOIN AuftragPos b ON b.ID=a.PosID " +
                              "INNER JOIN Auftrag c ON c.ID=b.AuftragTableID " +
                              "WHERE a.ID=" + ID + " ";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Kommission");
                if (dt.Rows.Count > 0)
                {
                    SetClassValue(ref dt);
                }
            }
        }
        ///<summary>clsKommission / GetAllDocsAreCreated</summary>
        ///<remarks>Ermittel, ob alle Dokumente für die erstellt wurden.</remarks>
        public static bool GetAllDocsAreCreated(Globals._GL_USER myGLUser, decimal myDecTourID)
        {
            bool retVal = false;
            if (ExistTourIDinKommission(myGLUser, myDecTourID))
            {
                DataTable dt = new DataTable();
                string strSQL = string.Empty;
                strSQL = "SELECT DISTINCT Docs FROM Kommission WHERE TourID='" + myDecTourID + "' ";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Kommission");

                if (dt.Rows.Count > 0)
                {
                    //jetzt true setzen -> wenn nur eine Kommission false ist, dann ist 
                    //der Wert für die Gesamte Tour false;
                    retVal = true;
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        if ((bool)dt.Rows[i]["Docs"] == false)
                        {
                            retVal = false;
                        }
                    }
                }
            }
            return retVal;
        }
        ///<summary>clsKommission / Fill</summary>
        ///<remarks></remarks>
        private void SetClassValue(ref DataTable dt)
        {
            try
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    this.ID = Convert.ToDecimal(dt.Rows[i]["ID"].ToString());
                    this.AuftragPosTableID = (decimal)dt.Rows[i]["PosID"];
                    AuftragID = (decimal)dt.Rows[i]["Auftrag"];
                    AuftragPos = (decimal)dt.Rows[i]["AuftragPos"];
                    B_ID = (decimal)dt.Rows[i]["B_ID"];
                    E_ID = (decimal)dt.Rows[i]["E_ID"];
                    DateTime dtTmp = DateTime.MinValue;
                    DateTime.TryParse(dt.Rows[i]["B_Zeit"].ToString(), out dtTmp);
                    BeladeZeit = dtTmp;
                    BeladeZeitShownCtr = BeladeZeit;
                    dtTmp = DateTime.MaxValue;
                    DateTime.TryParse(dt.Rows[i]["E_Zeit"].ToString(), out dtTmp);
                    EntladeZeit = dtTmp;
                    EntladeZeitShownCtr = EntladeZeit;
                    Netto = (decimal)dt.Rows[i]["Netto"];
                    Brutto = (decimal)dt.Rows[i]["Brutto"];
                    TerminVorgabe = (DateTime)dt.Rows[i]["Termin"];
                    km = (Int32)dt.Rows[i]["km"];
                    if (dt.Rows[i]["Docs"] != null)
                    {
                        document = Convert.ToBoolean(dt.Rows[i]["Docs"]);
                    }
                    else
                    {
                        document = false;
                    }
                    if (dt.Rows[i]["TourID"] != null)
                    {
                        TourID = (decimal)dt.Rows[i]["TourID"];
                    }
                    else
                    {
                        TourID = 0;
                    }
                    BeladePos = (Int32)dt.Rows[i]["BeladePos"];
                    EntladePos = (Int32)dt.Rows[i]["EntladePos"];

                    this.Tour = new clsTour();
                    this.Tour.InitClass(this._GL_User, this._GL_System, this._Sys);
                    this.Tour.Auftrag.AuftragPos.ID = this.AuftragPosTableID;
                    this.Tour.Auftrag.AuftragPos.Fill();
                    this.Tour.Auftrag.ID = this.Tour.Auftrag.AuftragPos.AuftragTableID;
                    this.Tour.Auftrag.Fill();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        ///<summary>clsKommission / SetData</summary>
        ///<remarks>Weisst die Daten der Klasse zu.</remarks>
        public DataTable GetTourenKommssionen()
        {
            DataTable dt = new DataTable();
            if (clsTour.ExistTourID(_GL_User, TourID))
            {
                string strSQL = string.Empty;
                strSQL = "Select ID From Kommission where TourID='" + TourID + "' ";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Kommissionen");
            }
            return dt;
        }
        ///<summary>clsKommission / FillByAuftragPosTableID</summary>
        ///<remarks>Ermittel anhand der ID die Daten.</remarks>
        public void Add()
        {
            KontaktDate = DateTime.MinValue;
            try
            {
                string strSQL = string.Empty;
                strSQL = "INSERT INTO Kommission (" +
                                                    "PosID, " +
                                                    "B_Zeit, " +
                                                    "E_Zeit, " +
                                                    "Docs, " +
                                                    "TourID, " +
                                                    "BeladePos, " +
                                                    "EntladePos) " +

                                "VALUES (" +
                                            AuftragPosTableID +
                                         ", '" + BeladeZeit + "'" +
                                         ", '" + EntladeZeit + "'" +
                                         ", " + Convert.ToInt32(document) +
                                         ", " + TourID +
                                         ", " + BeladePos +
                                         ", " + EntladePos +

                                         ") ";

                strSQL = strSQL + "Select @@IDENTITY as 'ID' ";

                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                decimal decTmp = 0;
                decimal.TryParse(strTmp, out decTmp);
                ID = decTmp;
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string Beschreibung = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }
        }
        ///<summary>clsKommission / UpdateKommission</summary>
        ///<remarks>Update der Kommissionsdaten.</remarks>
        public void UpdateKommission()
        {
            string strSQL = string.Empty;
            strSQL = "Update Kommission SET " +
                                    "PosID =" + AuftragPosTableID +
                                    ", B_Zeit ='" + BeladeZeit + "'" +
                                    ", E_Zeit ='" + EntladeZeit + "'" +
                                    ", Docs =" + Convert.ToInt32(document) +
                                    ", TourID =" + TourID +
                                    ", BeladePos =" + BeladePos +
                                    ", EntladePos =" + EntladePos +
                                    "WHERE ID=" + ID + " ";
            clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
        }
        ///<summary>clsKommission / ExistTourIDinKommission</summary>
        ///<remarks>Prüft, ob die entsprechende Tour vorhanden ist.</remarks>
        public static bool ExistTourIDinKommission(Globals._GL_USER myGLUser, decimal myTourID)
        {
            string strSQL = string.Empty;
            strSQL = "Select DISTINCT ID FROM Kommission WHERE TourID=" + myTourID + "; ";
            return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, myGLUser.User_ID);
        }
        ///<summary>clsKommission / ExistKommission</summary>
        ///<remarks>Prüft, ob die entsprechende Kommission vorhanden ist.</remarks>
        public static bool ExistKommission(Globals._GL_USER myGLUser, decimal myKommiID)
        {
            string strSQL = string.Empty;
            strSQL = "Select ID FROM Kommission WHERE ID=" + myKommiID + "; ";
            return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, myGLUser.User_ID);
        }
        ///<summary>clsKommission / UpdateStartZeit</summary>
        ///<remarks>Update Tour StartZeit.</remarks>
        public bool UpdateBeladeZeit()
        {
            //Baustelle
            //Normal darf sich die KOmmissions Be- und Entladezeit nur in dem Zeitfenster von Tour Start- und EndZeit bewegen
            string strSQL = string.Empty;
            //Kommission
            strSQL = strSQL + "Update Kommission SET B_Zeit ='" + BeladeZeit + "' WHERE ID IN (" +
                                        "Select ID FROM Kommission " +
                                                        "WHERE " +
                                                                "TourID=" + ID +
                                                                " AND (B_Zeit<'" + BeladeZeit + "' OR B_Zeit>'" + EntladeZeit + "')); ";
            //Tour
            strSQL = strSQL + "Update Tour SET StartZeit=" + BeladeZeit + "' WHERE ID=" + ID + ";";
            return clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "TourStartZeitChange", BenutzerID);
        }
        ///<summary>clsKommission / UpdateEndZeit</summary>
        ///<remarks>Update Tour EndZeit.</remarks>
        public bool UpdateEntladeZeit()
        {
            //Baustelle
            //Normal darf sich die KOmmissions Be- und Entladezeit nur in dem Zeitfenster von Tour Start- und EndZeit bewegen
            string strSQL = string.Empty;
            //Kommission
            strSQL = strSQL + "Update Kommission SET E_Zeit ='" + EntladeZeit + "' WHERE ID IN (" +
                                        "Select ID FROM Kommission WHERE TourID=" + ID + " AND " +
                                                                   "(E_Zeit<'" + EntladeZeit + "' OR E_Zeit>'" + EntladeZeit + "')); ";
            //Tour
            strSQL = strSQL + "Update Tour SET EndZeit=" + EntladeZeit + "' WHERE ID=" + ID + " ;";
            return clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "TourEndZeitChange", BenutzerID);
        }
        ///<summary>clsKommission / ExistKommission</summary>
        ///<remarks>Prüft, ob die entsprechende Kommission vorhanden ist.</remarks>
        public bool ChangeBeAndEntladePosition(Int32 myNewPos, bool bBeladePosToChange)
        {
            string strSQL = string.Empty;
            if (bBeladePosToChange)
            {
                //Updaten für den Datensatz mit der momentan die neue Pos. besitzt
                strSQL = "Update Kommission SET BeladePos=" + BeladePos + " " +
                                "WHERE TourID=" + TourID + " AND BeladePos=" + myNewPos + " ;";

                //Update des eigenen Datensatzes mit der neuen Pos
                strSQL = strSQL + "Update Kommission Set BeladePos=" + myNewPos + " WHERE ID=" + ID + " ;";
            }
            else
            {
                //Updaten für den Datensatz mit der momentan die neue Pos. besitzt
                strSQL = "Update Kommission SET EntladePos=" + EntladePos + " " +
                                "WHERE TourID=" + TourID + " AND EntladePos=" + myNewPos + " ;";

                //Update des eigenen Datensatzes mit der neuen Pos
                strSQL = strSQL + "Update Kommission Set EntladePos=" + myNewPos + " WHERE ID=" + ID + " ;";
            }

            return clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "KommissionPosChange", BenutzerID);
        }
        ///<summary>clsKommission / DeleteKommission</summary>
        ///<remarks>Update Tour EndZeit.</remarks>
        public void DeleteKommission()
        {
            try
            {
                string strSQL = string.Empty;
                strSQL = "DELETE FROM Kommission WHERE ID = " + ID;
                clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            }
            catch (Exception ex)
            {
                //Add Logbucheintrag Exception
                string BeschreibungEx = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
            }
        }
        ///<summary>clsKommission / DeleteKommiFromTourByID</summary>
        ///<remarks>Löscht eine einzelne Kommission aus einer Tour. Hierzu müssen folgende Table 
        ///         berücksichtigt werden:
        ///         - AuftragPos -> Status setzen
        ///         - Lieferscheine / Dokumente löschen
        ///         - Kommission</remarks>
        public bool DeleteKommiFromTourByID()
        {
            string strSQL = string.Empty;
            //Status der AuftragPos auf 2 setzen
            strSQL = strSQL + "Update AuftragPos SET Status=2 WHERE ID =" + this.AuftragPosTableID + " ;";
            //Lieferschein löschen
            strSQL = strSQL + "DELETE FROM Lieferschein WHERE AP_ID =" + this.AuftragPosTableID + ";";
            //TourKontaktInfo löschen
            strSQL = strSQL + "DELETE FROM TourKontaktInfo WHERE KommissionsID IN (Select ID FROM Kommission WHERE PosID=" + this.AuftragPosTableID + ");";
            //Kommission
            strSQL = strSQL + "Delete FROM Kommission WHERE PosID=" + this.AuftragPosTableID + ";";

            bool retVal = clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "TourDelete", BenutzerID);
            CheckBeEntladePosAfterKommiDelete();

            return retVal;
        }
        ///<summary>clsKommission / CheckKommiChangeByDateTimeAndPositions</summary>
        ///<remarks>Ermittelt, ob bei einer Kommission Änderungen vorgenommen wurden.</remarks>
        public bool CheckKommiChangeByDateTimeAndPositions()
        {
            string strSQL = string.Empty;
            strSQL = "Select ID From Kommission WHERE B_Zeit='" + BeladeZeit + "' AND " +
                                                        "E_Zeit='" + EntladeZeit + "' AND " +
                                                        "BeladePos='" + BeladePos + "' AND " +
                                                        "EntladePos='" + EntladePos + "' AND " +
                                                        "ID='" + ID + "';";
            return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
        }
        ///<summary>clsKommission / GetKommissionenForTourDetails</summary>
        ///<remarks></remarks>
        public DataTable GetKommissionenForTourDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                string strSQL = string.Empty;
                strSQL = "SELECT a.ID " +
                                ", CAST(b.Auftrag_ID as varchar) +'/'+ CAST(b.AuftragPos as varchar) as AuftragPosition" +
                                ", a.B_Zeit as Beladezeit" +
                                ", a.E_Zeit as Entladezeit" +
                                ", a.BeladePos as Beladeposition" +
                                ", a.EntladePos as Entladeposition" +
                                ", c.km as km" +
                              " FROM Kommission a " +
                              "INNER JOIN AuftragPos b ON b.ID=a.PosID " +
                              "INNER JOIN Auftrag c ON c.ID=b.AuftragTableID " +
                              "WHERE a.TourID='" + TourID + "' Order By a.BeladePos ";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Kommissionen");
            }
            catch (Exception ex)
            {
                //Add Logbucheintrag Exception
                string BeschreibungEx = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
            }
            return dt;
        }
        ///<summary>clsKommission / Dokuments</summary>
        ///<remarks>Ermittel den Status der Dokumente</remarks>
        public bool Dokuments(ref clsKommission Kommission)
        {
            ////Kontrolle in db Lieferschein und Docs (Abholschein
            //if (clsLieferscheine.LieferscheinExist(Kommission.AuftragPos_ID))
            //{
            //    Kommission.document = true;
            //}
            ////Check Abholschein
            //if (clsDocScan.CheckDocScanIsIn(_GL_User, Kommission.AuftragID, Globals.enumDokumentenart.Abholschein.ToString()))
            //{
            //    Kommission.document = true;
            //}
            ////Check Fremdlieferschein Holzrichter
            //if (clsDocScan.CheckDocScanIsIn(_GL_User, Kommission.AuftragID, Globals.enumDokumentenart.Fremdlieferschein.ToString()))
            //{
            //    Kommission.document = true;
            //}
            ////nun update in Kommission
            //this.UpdateKommission();
            //return Kommission.document;
            return false;
        }
        ///<summary>clsKommission / CheckKommiChangeByDateTimeAndPositions</summary>
        ///<remarks>Ermittelt, ob bei einer Kommission Änderungen vorgenommen wurden.</remarks>
        public void CheckBeEntladePosAfterKommiDelete()
        {
            string strUpdate = string.Empty;
            Int32 PosCount = 0;
            //BeladePos
            string strSQL = string.Empty;
            strSQL = "Select ID, PosID, BeladePos From Kommission WHERE TourID =" + TourID + " ORDER BY BeladePos; ";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Kommission");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    PosCount++;
                    strUpdate = strUpdate + " Update Kommission Set BeladePos=" + PosCount + " WHERE ID=" + Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                }
            }

            //EntladePos
            strSQL = string.Empty;
            dt.Clear();
            PosCount = 0;
            strSQL = "Select ID, PosID, EntladePos From Kommission WHERE TourID =" + TourID + " ORDER BY EntladePos; ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Kommission");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    PosCount++;
                    strUpdate = strUpdate + " Update Kommission Set EntladePos=" + PosCount + " WHERE ID=" + Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                }
            }

            if (strUpdate != string.Empty)
            {
                clsSQLcon.ExecuteSQLWithTRANSACTION(strUpdate, "KommiPosCheck", BenutzerID);
            }
        }
        ///<summary>clsKommission / UpdateDocsByAuftragPosTableID</summary>
        ///<remarks>Flag für erstellte Dokumente</remarks>
        public static void UpdateDocsByAuftragPosTableID(Globals._GL_USER myGLUser, bool erstellt, decimal myAuftragPosTableID)
        {
            Int32 iBit = 0;
            if (erstellt)
            {
                iBit = 1;
            }
            string strSQL = string.Empty;
            strSQL = "Update Kommission SET Docs=" + iBit + " WHERE PosID=" + myAuftragPosTableID + ";";
            clsSQLcon.ExecuteSQL(strSQL, myGLUser.User_ID);
        }



        ///<summary>clsKommission / CheckKommiChangeByDateTimeAndPositions</summary>
        ///<remarks>Ermittelt, ob bei einer Kommission Änderungen vorgenommen wurden.</remarks>
        public void InsertFahrerInfo()
        {
            string strSQL = string.Empty;
            strSQL = "INSERT INTO TourKontaktInfo (" +
                                                "TourID, " +
                                                "KommissionsID, " +
                                                "InfoText, " +
                                                "DateAdd) " +
                                    "VALUES ('" +
                                                TourID + "', '" +
                                                ID + "', '" +
                                                KontaktInfo + "', '" +
                                                DateTime.Now + "') ";
            clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
        }
        ///<summary>clsKommission / GetKontaktInfo</summary>
        ///<remarks>Ermittelt die eingetragenen Kontakte zur Kommission.</remarks>
        public void GetKontaktInfo()
        {
            KontaktInfo = string.Empty;
            string strSQL = string.Empty;
            strSQL = "Select DateAdd, InfoText From TourKontaktInfo " +
                                                        "WHERE KommissionsID=" + ID + " ORDER BY DateAdd DESC; ";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "KontaktInfo");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    KontaktInfo = KontaktInfo
                                                + ((DateTime)dt.Rows[i]["DateAdd"]).ToString() + Environment.NewLine +
                                                dt.Rows[i]["InfoText"].ToString()
                                                //+ Environment.NewLine
                                                + Environment.NewLine;
                }
            }
        }
        ///<summary>clsKommission / GetIDfromKommission</summary>
        ///<remarks></remarks>
        public decimal GetIDfromKommission()
        {
            decimal KommiID = 0;
            string strSQL = "Select ID FROM Kommission WHERE PosID =" + AuftragPosTableID + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            Decimal.TryParse(strTmp, out KommiID);
            return KommiID;
        }



        /**************************************************
         * 
         * 
         * 
         * **********************************************/
        //----------------- KFZ ZM


        public bool ExistKommissionByAuftragPosID()
        {
            string strSql = string.Empty;
            strSql = "Select ID FROM Kommission WHERE PosID =" + this.AuftragPosTableID + ";";
            return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
        }
        //
        public static string GetBeladedatumByAP_ID(decimal AuftragPosID, decimal decBenutzerID)
        {
            string strSql = string.Empty;
            string strVal = string.Empty;
            strSql = "(Select B_Zeit FROM Kommission WHERE PosID ='" + AuftragPosID + "')";
            strVal = clsSQLcon.ExecuteSQL_GetValue(strSql, decBenutzerID);
            return strVal;
        }
        //
        //
        //
        public static string GetEntladedatumByAP_ID(decimal AuftragPosID, decimal decBenutzerID)
        {
            string strSql = string.Empty;
            string strVal = string.Empty;
            strSql = "(Select E_Zeit FROM Kommission WHERE PosID ='" + AuftragPosID + "')";
            strVal = clsSQLcon.ExecuteSQL_GetValue(strSql, decBenutzerID);
            return strVal;
        }
        //
        //
        //
        public DataTable GetKommissionByZM_ID(decimal decZM_ID, decimal decBenutzerID)
        {
            string strSql = string.Empty;
            DataTable dtRet = new DataTable();
            strSql = "SELECT " +
                                "KO.ID " +
                                   "FROM " +
                                        "Kommission KO " +
                                   "WHERE KFZ_ZM='" + decZM_ID + "' AND " +

                                   "(((KO.B_Zeit >= '" + BeladeZeit + "') AND ((KO.E_Zeit >= '" + BeladeZeit + "') OR (KO.E_Zeit<='" + EntladeZeit + "'))) " +
                                   "OR " +
                                   "(((KO.B_Zeit >='" + BeladeZeit + "') OR (KO.B_Zeit<='" + EntladeZeit + "')) AND (KO.E_Zeit > '" + EntladeZeit + "')) " +
                                   "OR " +
                                   "((KO.B_Zeit >= '" + BeladeZeit + "') AND (KO.E_Zeit <= '" + EntladeZeit + "')) " +
                                   "OR " +
                                   "((KO.B_Zeit < '" + BeladeZeit + "') AND (KO.E_Zeit <= '" + EntladeZeit + "')) " +
                                    ") ";

            dtRet = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "Aufträge");
            return dtRet;
        }






        //
        public void UpdateBeladeZeit(DateTime newBeladeZeit, decimal KomID)
        {
            if (newBeladeZeit >= DateTime.MinValue)
            {
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = "UPDATE Kommission " +
                                            "SET B_Zeit = '" + newBeladeZeit + "' " +
                                            "WHERE ID = " + KomID;
                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
        }
        //
        //
        //
        public void UpdateEntladeZeit(DateTime newEntladeZeit, decimal KomID)
        {
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "UPDATE Kommission " +
                                        "SET E_Zeit = '" + newEntladeZeit + "' " +
                                        "WHERE ID = " + KomID;
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        //
        //
        //
        public void SQLInsertData()
        {
            Int32 iBit0 = 0;
            try
            {
                string strSQL = string.Empty;
                strSQL = "INSERT INTO Kommission (" +
                                                                   "PosID, " +
                                                                   "B_Zeit, " +
                                                                   "E_Zeit, " +
                                                                   "BeladePos, " +
                                                                   "EntladePos, " +
                                                                   "TourID) " +

                                                   "VALUES ('" +
                                                                  AuftragPosTableID + "', '" +      // FK = ID in AuftragPos
                                                                  BeladeZeit + "', '" +
                                                                  EntladeZeit + "', '" +
                                                                  BeladePos + "', '" +
                                                                  EntladePos + "', '" +
                                                                  TourID + "')";


                clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string Beschreibung = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }
        }
        //
        //
        //
        /************************************************************************************************
         *                      public static procedures
         * *********************************************************************************************/
        ///<summary>clsKommission / GetMaxEntladeZeit</summary>
        ///<remarks>max. Entladezeit</remarks>
        public static DateTime GetMaxEntladeZeit(decimal ZM_ID, Globals._GL_USER myGLUser)
        {
            string strSQL = string.Empty;  // "SELECT MAX(E_Zeit) FROM Kommission WHERE KFZ_ZM=" + ZM_ID + ";";
            strSQL = "SELECT MAX(a.E_Zeit) FROM Kommission a " +
                                            "INNER JOIN Tour b ON b.ID=a.TourID " +
                                            "WHERE b.KFZ_ZM=" + ZM_ID + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
            DateTime dtTmp = DateTime.Now;
            DateTime.TryParse(strTmp, out dtTmp);
            return dtTmp;
        }






        //
        //----------------- Update Papiere in Kommission ----------------
        //
        public static void UpdateDokumenteKommissionByKommiID(bool erstellt, decimal ID)
        {
            char Papiere = 'F';
            if (ID > 0)
            {
                if (erstellt)
                {
                    Papiere = 'T';
                }

                try
                {
                    SqlCommand InsertCommand = new SqlCommand();
                    InsertCommand.Connection = Globals.SQLcon.Connection;
                    InsertCommand.CommandText = "Update Kommission SET Papiere='" + Papiere + "' " +
                                                                        "WHERE ID='" + ID + "'";

                    Globals.SQLcon.Open();
                    InsertCommand.ExecuteNonQuery();

                    Globals.SQLcon.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                }
            }
        }




        //
        //
        //
        //

        //
        //
        //
        public DataSet getAuftragRecByAuftragID(decimal AuftragID, decimal AuftragPos)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT " +
                                            "ID, " +
                                            "AuftragPos, " +
                                            "Auftrag_ID, " +
                                            "(Select SUM(gemGewicht) FROM Artikel WHERE AuftragID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "') as 'gemGewicht', " +
                                            "(Select SUM(Brutto) FROM Artikel WHERE AuftragID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "') as 'Brutto', " +
                                            "(SELECT Auftrag.B_ID FROM Auftrag WHERE Auftrag.ANr='" + AuftragID + "') as 'B_ID' , " +
                                            "T_Date as Termin," +
                                            "(SELECT Auftrag.E_ID FROM Auftrag WHERE Auftrag.ANr='" + AuftragID + "') as 'E_ID' " +
                                        "FROM " +
                                            "AuftragPos " +
                                        "WHERE AuftragPos.Auftrag_ID ='" + AuftragID + "' AND AuftragPos.AuftragPos='" + AuftragPos + "'";

            ada.Fill(ds);
            Command.Dispose();
            Globals.SQLcon.Close();
            return ds;
        }
        //
        //-------- läd die B_ID von Auftrag  ------------------------------------------
        //
        private decimal GetBIDFromAuftrag(decimal Auftrag, enumLadestelle BeOREnt)
        {
            SqlCommand SelectCommand = new SqlCommand();
            decimal LadeID = 0.0M;
            SelectCommand.Connection = Globals.SQLcon.Connection;
            if (BeOREnt == enumLadestelle.Beladestelle)
            {
                //SelectCommand.CommandText = ("SELECT BeladeID FROM AuftragPos WHERE ID = " + AuftragPosID);
                SelectCommand.CommandText = ("SELECT B_ID FROM Auftrag " +
                                                "JOIN AuftragPos ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                                "WHERE Auftrag.ANr = " + Auftrag);
            }
            else if (BeOREnt == enumLadestelle.Entladestelle)
            {
                SelectCommand.CommandText = ("SELECT E_ID FROM Auftrag " +
                                                "JOIN AuftragPos ON Auftrag.ANr=AuftragPos.Auftrag_ID " +
                                                "WHERE Auftrag.ANr = " + Auftrag);
            }
            else
            {
                return 0;
            }
            Globals.SQLcon.Open();

            object obj = SelectCommand.ExecuteScalar();

            if (obj != null)
            {
                LadeID = (decimal)SelectCommand.ExecuteScalar();
            }
            Globals.SQLcon.Close();
            return LadeID;
        }



        public static void SetFahrerKontakt(decimal iKommID, bool boFahrerKontakt)
        {
            Int32 iBit = 0;
            if (boFahrerKontakt)
            {
                iBit = 1;
            }
            try
            {
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = "UPDATE Kommission " +
                                                  "SET " +
                                                  "FahrerKontakt ='" + iBit + "' " +
                                                  "WHERE ID ='" + iKommID + "'";

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                decimal decUser = -1.0M;
                Functions.AddLogbuch(decUser, "SetFahrerKontakt", ex.ToString());
            }
        }
        //
        //----------------- Papiere Update ____________________
        //
        public static void SetPapiere(decimal iKommID, bool boPapiere)
        {
            Int32 iBit = 0;
            if (boPapiere)
            {
                iBit = 1;
            }
            try
            {
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = "UPDATE Kommission " +
                                                  "SET " +
                                                  "Papiere ='" + iBit + "' " +
                                                  "WHERE ID ='" + iKommID + "'";

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                decimal decUser = -1.0M;
                Functions.AddLogbuch(decUser, "SetPapiere", ex.ToString());
            }
        }
        //
        //------- löschen Kommissionseinträge per Auftrag und AuftragPos-------------------
        //
        public static void DeleteKommiPosByAuftragAuftragPos(decimal AuftragID, decimal AuftragPos)
        {
            try
            {
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = ("DELETE FROM Kommission WHERE Auftrag='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'");
                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();

                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch
            {
            }
        }
        //
        //----------------- 
        //


        //
        //--------- KONTAKT MIT fARHER ------------------
        //
        public bool Kontakt(decimal PosID)
        {
            bool bolKontakt = false;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = ("Select FahrerKontakt FROM Kommission WHERE PosID ='" + PosID + "'");
            Globals.SQLcon.Open();

            object obj = Command.ExecuteScalar();

            if (obj != null)
            {
                bolKontakt = (bool)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
            return bolKontakt;
        }
        //
        //---------   ------------------
        //---- 0=neu / 1= schon disponiert ------------
        public static bool GetDispoSet(decimal AuftragID, decimal AuftragPos)
        {
            bool dispoSet = false;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Select DispoSet FROM Kommission WHERE Auftrag='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";
            Globals.SQLcon.Open();

            if (Command.ExecuteScalar() == null)
            {
                dispoSet = false;
            }
            else
            {
                if ((Int32)Command.ExecuteScalar() == 0)
                {
                    dispoSet = false;
                }
                else
                {
                    dispoSet = true;
                }
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return dispoSet;
        }
        //
        //
        //
        private void KommissionSetErledigt()
        {
            try
            {
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = ("Update Kommission SET erledigt='T' WHERE ID = " + ID);
                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();

                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        //
        //
        //
        private void AuftragPosSetStatus(Int32 iStatus, decimal AuftragID, decimal AuftragPos)
        {
            try
            {
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = ("Update AuftragPos SET Status='" + iStatus + "' WHERE Auftrag_ID='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'");
                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();

                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        //
        //
        //
        public static DataSet getKommiRecByAuftragID(decimal AuftragID, decimal AuftragPos)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT * " +
                                        "FROM " +
                                            "Kommission " +
                                        "WHERE Auftrag ='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'";

            ada.Fill(ds);
            Command.Dispose();
            Globals.SQLcon.Close();
            return ds;
        }
        //
        //
        //--- 0 = neu / 1 = schon disponiert -----------------
        public static void KommissionSetDispoSet(Int32 DispoSet, decimal AuftragID, decimal AuftragPos)
        {
            try
            {
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = ("Update Kommission SET DispoSet='" + DispoSet + "' WHERE Auftrag='" + AuftragID + "' AND AuftragPos='" + AuftragPos + "'");
                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();

                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        //
        //----- Anzahl der Kommission zu einem Zeipunkt auf dem Fahrzeug  --------------
        //-- mit die größe der Fahrzeugrows berechnet werden (vergrößert werden kann)
        public Int32 GetCountKommiOnSameTime()
        {

            Int32 count = 1;
            /****
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          Command.CommandText = "SELECT COUNT(b.ID) " +
                                        "FROM Kommission b " +
                                        "INNER JOIN Artikel a ON a.AuftragPosTableID=b.PosID " +
                                        "WHERE " +
                                            "b.KFZ_ZM='" + KFZ_ZM + "' AND " +
                                            "(( " +
                                            "b.B_Zeit = '" + BeladeZeit + "' AND b.E_Zeit = '" + EntladeZeit + "' " +
                                            ") OR (" +
                                            " (b.B_Zeit > '" + BeladeZeit + "' AND b.B_Zeit < '" + EntladeZeit + "') AND " +
                                            " b.E_Zeit>= '" + EntladeZeit + "' " +
                                            ") OR (" +
                                            "(b.B_Zeit <= '" + BeladeZeit + "' AND b.E_Zeit > '" + BeladeZeit + "') AND " +
                                            "b.E_Zeit<'" + EntladeZeit + "' " +
                                            ") OR (" +
                                            "b.B_Zeit < '" + BeladeZeit + "' AND b.E_Zeit > '" + EntladeZeit + "' " +
                                            ") OR (" +
                                            "b.B_Zeit > '" + BeladeZeit + "' AND b.E_Zeit < '" + EntladeZeit + "' " +
                                            "))";
          Globals.SQLcon.Open();
          if (Command.ExecuteScalar() is DBNull)
          {
            count = 1;
          }
          else
          {
            count = (Int32)Command.ExecuteScalar();
          }
          Command.Dispose();
          Globals.SQLcon.Close();
          if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
          {
            Command.Connection.Close();
          }
             * ***/
            return count;
        }
        //
        //------------ berechnen des gesamten Gewichts aller Kommi einer Tour / Zeitraum -----
        //
        public decimal GetGesamtGewichtOnSameTime(bool bo_Brutto)
        {
            decimal GesamtGewicht = 0.00m;
            string strSQL = string.Empty;
            /****
          if (bo_Brutto)
          {
              strSQL = "SELECT SUM(a.Brutto) as 'Gewicht'  " +
                                        "FROM Kommission b " +
                                        "INNER JOIN Artikel a ON a.AuftragPosTableID=b.PosID " +
                                        "WHERE " +
                                            "b.KFZ_ZM='" + KFZ_ZM + "' AND " +
                                            "(( " +
                                            "b.B_Zeit = '" + BeladeZeit + "' AND b.E_Zeit = '" + EntladeZeit + "' " +
                                            ") OR (" +
                                            " (b.B_Zeit > '" + BeladeZeit + "' AND b.B_Zeit < '" + EntladeZeit + "') AND " +
                                            " b.E_Zeit>= '" + EntladeZeit + "' " +
                                            ") OR (" +
                                            "(b.B_Zeit <= '" + BeladeZeit + "' AND b.E_Zeit > '" + BeladeZeit + "') AND " +
                                            "b.E_Zeit<'" + EntladeZeit + "' " +
                                            ") OR (" +
                                            "b.B_Zeit < '" + BeladeZeit + "' AND b.E_Zeit > '" + EntladeZeit + "' " +
                                            ") OR (" +
                                            "b.B_Zeit > '" + BeladeZeit + "' AND b.E_Zeit < '" + EntladeZeit + "' " +
                                            "))";



          }
          else
          {
              strSQL = "SELECT SUM(a.Netto) as 'Gewicht' " +
                                        "FROM Kommission b " +
                                        "INNER JOIN Artikel a ON a.AuftragPosTableID=b.PosID " +
                                        "WHERE " +
                                            "b.KFZ_ZM='" + KFZ_ZM + "' AND " +
                                            "(( " +
                                            "b.B_Zeit = '" + BeladeZeit + "' AND b.E_Zeit = '" + EntladeZeit + "' " +
                                            ") OR (" +
                                            " (b.B_Zeit > '" + BeladeZeit + "' AND b.B_Zeit < '" + EntladeZeit + "') AND " +
                                            " b.E_Zeit>= '" + EntladeZeit + "' " +
                                            ") OR (" +
                                            "(b.B_Zeit <= '" + BeladeZeit + "' AND b.E_Zeit > '" + BeladeZeit + "') AND " +
                                            "b.E_Zeit<'" + EntladeZeit + "' " +
                                            ") OR (" +
                                            "b.B_Zeit < '" + BeladeZeit + "' AND b.E_Zeit > '" + EntladeZeit + "' " +
                                            ") OR (" +
                                            "b.B_Zeit > '" + BeladeZeit + "' AND b.E_Zeit < '" + EntladeZeit + "' " +
                                            "))";
          }

          string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
          Decimal.TryParse(strTmp, out GesamtGewicht);
             * ****/
            return GesamtGewicht;
        }
        //
        //
        //
        public DataTable GetKommission()
        {
            DataTable KommiTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT " +
                                        "KO.ID " +
                                   "FROM " +
                                        "Kommission KO " +
                                   "WHERE " +

                                        "( " +
                                        "((KO.B_Zeit < '" + BeladeZeit + "' AND KO.E_Zeit > '" + BeladeZeit + "')) " +
                                        "OR " +
                                        "((KO.B_Zeit = '" + BeladeZeit + "' AND KO.E_Zeit < '" + EntladeZeit + "')) " +
                                        "OR " +
                                        "((KO.B_Zeit < '" + EntladeZeit + "' AND KO.E_Zeit > '" + EntladeZeit + "')) " +
                                        "OR " +
                                        "((KO.B_Zeit > '" + BeladeZeit + "' AND KO.E_Zeit = '" + EntladeZeit + "')) " +
                                        "OR " +
                                        "((KO.B_Zeit < '" + BeladeZeit + "' AND KO.E_Zeit > '" + EntladeZeit + "')) " +
                                        "OR " +
                                        "((KO.B_Zeit = '" + BeladeZeit + "' AND KO.E_Zeit = '" + EntladeZeit + "')) " +
                                        "OR " +
                                        "((KO.B_Zeit > '" + BeladeZeit + "' AND KO.E_Zeit < '" + EntladeZeit + "')) " +
                                        ") ";
            ada.Fill(KommiTable);
            Command.Dispose();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
            return KommiTable;
        }
        //
        //---------- Check ob Auftrag und Position bereits disponiert ----------
        //
        public static bool IsAuftragDisponiert(decimal auftrag, decimal auftragPos)
        {
            bool Disponiert = true;
            try
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT ID FROM Kommission WHERE Auftrag='" + auftrag + "' AND AuftragPos ='" + auftragPos + "'";
                Globals.SQLcon.Open();
                if (Command.ExecuteScalar() == null)
                {
                    Disponiert = false;
                }
                else
                {
                    Disponiert = true;
                }
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            return Disponiert;
        }
        //
        //---------------- Get Kommissionsdaten für Lieferscheine------
        //
        public static DataTable GetKommiDatenForLieferschein(Globals._GL_USER myGLUser, decimal myAuftragPosTableID)
        {
            DataTable dt = new DataTable();
            string strSQL = string.Empty;
            strSQL = "Select a.ID, a.Auftrag_ID, a.AuftragPos" +
                            ",(Select KFZ FROM Fahrzeuge WHERE ID=c.KFZ_ZM) as ZM" +
                            ",(Select KFZ FROM Fahrzeuge WHERE ID=c.KFZ_A) as Auflieger" +
                            ",(Select Name FROM Personal WHERE ID=c.PersonalID) as Nachname" +
                            ",(Select Vorname FROM Personal WHERE ID=c.PersonalID) as Vorname" +
                            " FROM AuftragPos a " +
                            "INNER JOIN Kommission b ON b.PosID=a.ID " +
                            "INNER JOIN Tour c ON c.ID=b.TourID " +
                            "WHERE a.ID=" + myAuftragPosTableID + " ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Kommission");
            return dt;
        }
        //
        //---------- Check ob AP_ID in DB Kommission enthalten - Transport im Selbsteintritt ----------
        //
        public static bool IsAuftragPositionIn(Globals._GL_USER myGLUser, decimal AP_ID)
        {
            string strSQL = string.Empty;
            strSQL = "SELECT ID FROM Kommission WHERE PosID='" + AP_ID + "' ";
            return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, myGLUser.User_ID);
        }


    }
}
