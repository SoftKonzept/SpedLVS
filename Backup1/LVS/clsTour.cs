using System;
using System.Data;


namespace LVS
{
    public class clsTour
    {
        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GLSystem;
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

        public clsAuftrag Auftrag;
        public clsSystem Sys;
        clsDispoCheck DispoCheck = new clsDispoCheck();


        private DateTime _StartZeit = default(DateTime);
        private DateTime _EndZeit = default(DateTime);
        private DateTime _Date_Add = default(DateTime);
        public DataTable dtKommissionen = new DataTable("Kommissionen");
        public Int32 StatusTour = 0;
        public Int32 AnzahlKommissionen = 0;
        public string StartOrt = string.Empty;
        public string EndOrt = string.Empty;
        public bool DocsOK = false;
        public bool FahrerKontakt = false;
        public bool bPositionChange = false;
        public DateTime StartZeitShownCtr;
        public DateTime EndZeitShownCtr;
        private bool _autoPlaced = false;
        public decimal TourGewicht;



        public decimal ID { get; set; }
        public DateTime StartZeit { get; set; }
        public DateTime EndZeit { get; set; }
        public Int32 km { get; set; }
        public Int32 kmLeer { get; set; }
        public decimal KFZ_ZM { get; set; }
        public decimal KFZ_A { get; set; }
        public DateTime Date_Add { get; set; }
        public bool autoPlaced { get; set; }
        public string KontaktInfo { get; set; }
        public decimal PersonalID { get; set; }
        public string MouseOverInfo { get; set; }

        public DataTable dtOrderList { get; set; }
        public DataTable dtOrderListArtikel { get; set; }
        public string CtrTxt { get; set; }

        /*************************************************************************************
         *                          Methoden 
         * **********************************************************************************/
        ///<summary>clsAuftrag / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, clsSystem myClsSystem)
        {
            this._GLSystem = myGLSystem;
            this._GL_User = myGLUser;
            this.Sys = myClsSystem;

            Auftrag = new clsAuftrag();
            Auftrag.InitClass(this._GL_User, this._GLSystem, this.Sys);
        }
        ///<summary>clsTour / InitArbeitsbereich</summary>
        ///<remarks>Daten zur Datenbank hinzufügen.</remarks>
        //public void IniTourDaten(ref AFKalenderItemTour myCtrTour)
        public void InitTourDaten(decimal TourID, DataTable dtKommissionen)
        {
            //Tourdaten werden übernommen
            Fill();
            //Alle KommissionsID werden in der DataTable dtKommission gespeichert

            //myCtrTour.Kommission.TourID = myCtrTour.Tour.ID;        
            //dtKommissionen = myCtrTour.Kommission.GetTourenKommssionen();
            //weitere INfos setzen
            //Anzahl Kommissionen
            AnzahlKommissionen = dtKommissionen.Rows.Count;
            //1.Beladeort der Tour
            StartOrt = GetStartOrt();
            //letzte Entladestelle der Tour
            EndOrt = GetEndOrt();
            //Gesamtstatus der Tour
            StatusTour = GetStatusTour();
            //Dokumente alle erstellt
            DocsOK = clsKommission.GetAllDocsAreCreated(this._GL_User, ID);
            //TourGewicht
            TourGewicht = GetTourGewicht();
            //Baustelle fehlt noch
            // FahrerKontakt = clsKommission.GetAllFahrerKontakt(this._GL_User, ID);

            //MouseOverInfo erstellen
            GetMousOverInfo();
        }
        ///<summary>clsTour / InitArbeitsbereich</summary>
        ///<remarks>Daten zur Datenbank hinzufügen.</remarks>
        public void GetOrder(Int32 listenArt, DateTime SearchDateVon, DateTime SearchDateBis)
        {
            dtOrderList = new DataTable();
            dtOrderListArtikel = new DataTable();
            CtrTxt = string.Empty;

            Int32 OrderStatus = 0;
            switch (listenArt)
            {
                case 2:
                    //offene
                    OrderStatus = 2;
                    //vergabe = false;
                    CtrTxt = "offene Aufträge [" + SearchDateVon.ToShortDateString() + " bis " + SearchDateBis.ToShortDateString() + "]";
                    break;
                case 3:
                    //disponierte
                    OrderStatus = 4;
                    //vergabe = false;
                    CtrTxt = "disponierte Aufträge [" + SearchDateVon.ToShortDateString() + " bis " + SearchDateBis.ToShortDateString() + "]";
                    break;
                case 4:
                    //durchgeführte
                    OrderStatus = 5;
                    //vergabe = false;
                    CtrTxt = "durchgeführte Aufträge [" + SearchDateVon.ToShortDateString() + " bis " + SearchDateBis.ToShortDateString() + "]";
                    break;

                case 6:
                    //stornierte Aufträge
                    OrderStatus = 3;
                    //vergabe = false;
                    CtrTxt = "stornierte Aufträge vom [" + SearchDateVon.ToShortDateString() + " bis " + SearchDateBis.ToShortDateString() + "]";
                    break;

                case 7:
                    //Auflistung aller Aufträge
                    OrderStatus = 0;
                    //vergabe = false;
                    CtrTxt = "Auftragsübersicht: alle Aufträge";
                    break;

                default:
                    //offene
                    SearchDateVon = clsSystem.const_DefaultDateTimeValue_Min;  //Es sollen auch die noch nicht disponierten Aufträge aus der Vergangenheit angezeigt werden
                    SearchDateBis = DateTime.Today.Date.AddDays(3); // 3Tage in die Zukunft
                    OrderStatus = 2;
                    //vergabe = false;
                    // afColorLabel1.myText = "offene Aufträge [" + SearchDateVon.ToShortDateString() + " bis " + SearchDateBis.ToShortDateString() + "]";
                    CtrTxt = "offene Aufträge [ bis " + SearchDateBis.ToShortDateString() + "]"; ;
                    break;


            }
            dtOrderList = Auftrag.GetAuftragsdatenByZeitraumAndStatus(SearchDateVon, SearchDateBis, OrderStatus, false);
            dtOrderListArtikel = Auftrag.GetArtikelForOrderList();
            //return dt;
        }
        ///<summary>clsTour / GetMousOverInfo</summary>
        ///<remarks>Ermittelt den ersten Beladeort der Tour.</remarks>
        private void GetMousOverInfo()
        {
            string strSQL = string.Empty;
            strSQL = "SELECT " +
                            "(Select KFZ FROM Fahrzeuge WHERE ID = b.KFZ_ZM) as ZM" +
                            ", (Select KFZ FROM Fahrzeuge WHERE ID=b.KFZ_A) as Auflieger" +
                            ", (Select Name +' '+Vorname FROM Personal WHERE ID=b.PersonalID) as Fahrer" +
                            ", b.StartZeit" +
                            ", b.EndZeit" +
                            ", (Select CAST(Auftrag_ID as varchar)+'/'+CAST(AuftragPos as varchar) From AuftragPos WHERE ID=a.PosID) as AuftragPos" +
                            ", (Select PLZ+' '+Ort FROM ADR WHERE ID=d.B_ID) as Beladestelle" +
                            ", (Select PLZ+' '+Ort FROM ADR WHERE ID=d.E_ID) as Entladestelle" +
                            ", (Select SUM(Brutto) FROM Artikel WHERE AuftragPosTableID=c.ID) as Gewicht" +
                            ", b.KM" +

                            " FROM Tour b " +
                            "INNER JOIN Kommission a ON b.ID=a.TourID " +
                            "INNER JOIN AuftragPos c ON a.PosID=c.ID " +
                            "INNER JOIN Auftrag d ON d.ID = c.AuftragTableID " +
                            "WHERE b.ID=" + ID + " ORDER BY a.BeladePos;";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "TourInfo");

            MouseOverInfo = string.Empty;
            if (dt.Rows.Count > 0)
            {
                string strTmp = string.Empty;
                strTmp = strTmp + "Tour - INFO Nr: " + ID;
                strTmp = strTmp + Environment.NewLine;
                strTmp = strTmp + Environment.NewLine;
                //Resourcen
                strTmp = strTmp + String.Format("{0}\t\t{1}", "Zugmaschine: ", dt.Rows[0]["ZM"].ToString()) + Environment.NewLine;
                strTmp = strTmp + String.Format("{0}\t\t{1}", "Auflieger: ", dt.Rows[0]["Auflieger"].ToString()) + Environment.NewLine;
                strTmp = strTmp + String.Format("{0}\t\t{1}", "Fahrer: ", dt.Rows[0]["Fahrer"].ToString()) + Environment.NewLine;
                //Tourzeiten
                strTmp = strTmp + Environment.NewLine;
                strTmp = strTmp + String.Format("{0}\t{1}", "Tour - Gewicht: ", Functions.FormatDecimal(TourGewicht) + " [kg]") + Environment.NewLine;
                strTmp = strTmp + String.Format("{0}\t{1}", "Tour - Start: ", ((DateTime)dt.Rows[0]["StartZeit"]).ToString() + " Uhr") + Environment.NewLine;
                strTmp = strTmp + String.Format("{0}\t{1}", "Tour - Ende: ", ((DateTime)dt.Rows[0]["EndZeit"]).ToString() + " Uhr") + Environment.NewLine;
                strTmp = strTmp + String.Format("{0}\t{1}", "Tour - Strecke: ", dt.Rows[0]["KM"].ToString() + " [km]") + Environment.NewLine;
                strTmp = strTmp + Environment.NewLine;
                Int32 iCount = 0;
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    iCount++;
                    strTmp = strTmp + String.Format("{0}\t{1}\t{2}{3}{4}{5}{6}", iCount.ToString() + ". Stelle: ",
                                                                                    dt.Rows[i]["AuftragPos"].ToString(),
                                                                                    dt.Rows[i]["Beladestelle"].ToString(),
                                                                                     "  ->  ",
                                                                                     dt.Rows[i]["Entladestelle"].ToString(),
                                                                                     "  -  ",
                                                                                      Functions.FormatDecimal((decimal)dt.Rows[i]["Gewicht"]) + "[kg]"

                                                    );
                    strTmp = strTmp + Environment.NewLine;

                }
                strTmp = strTmp + Environment.NewLine;
                MouseOverInfo = strTmp;

            }
        }
        ///<summary>clsTour / GetStartOrt</summary>
        ///<remarks>Ermittelt den ersten Beladeort der Tour.</remarks>
        private string GetStartOrt()
        {
            string strSQL = string.Empty;
            strSQL = "Select RTRIM(PLZ) +' '+ RTRIM(ORT) as Ort FROM ADR " +
                                                "WHERE ID =" +
                                                "(Select TOP(1) a.B_ID FROM Auftrag a " +
                                                        "INNER JOIN AuftragPos b ON b.AuftragTableID=a.ID " +
                                                        "INNER JOIN Kommission c ON c.PosID=b.ID " +
                                                        "INNER JOIN Tour d ON d.ID=c.TourID " +
                                                        "WHERE d.ID='" + ID + "' ORDER BY c.BeladePos )";
            return clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
        }
        ///<summary>clsTour / GetTourGewicht</summary>
        ///<remarks>Ermittelt das Gesamtgewicht der Tour.</remarks>
        private decimal GetTourGewicht()
        {
            string strSQL = string.Empty;
            strSQL = "Select SUM(a.Brutto) as TourGewicht FROM Artikel a " +
                                                            "INNER JOIN Kommission b ON b.PosID=a.AuftragPosTableID " +
                                                            "INNER JOIN Tour c ON c.ID = b.TourID " +
                                                            "WHERE c.ID='" + ID + "';";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        ///<summary>clsTour / GetEndOrt</summary>
        ///<remarks>Ermittel den letzten Entladeort der Tour.</remarks>
        private string GetEndOrt()
        {
            string strSQL = string.Empty;
            strSQL = "Select RTRIM(PLZ) +' '+ RTRIM(ORT) as Ort FROM ADR " +
                                                "WHERE ID =" +
                                                "(Select TOP(1) a.E_ID FROM Auftrag a " +
                                                        "INNER JOIN AuftragPos b ON b.AuftragTableID=a.ID " +
                                                        "INNER JOIN Kommission c ON c.PosID=b.ID " +
                                                        "INNER JOIN Tour d ON d.ID=c.TourID " +
                                                        "WHERE d.ID='" + ID + "' ORDER BY c.EntladePos DESC )";
            return clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
        }
        ///<summary>clsTour / GetStatusTour</summary>
        ///<remarks>Ermittel den Status der Tour. Ausschlaggebend ist hierbei der kleinste Statuswert
        ///         aller Kommissionen bzw. AuftragPositionen</remarks>
        private Int32 GetStatusTour()
        {
            string strSQL = string.Empty;
            strSQL = "Select TOP(1) b.Status FROM AuftragPos b " +
                                                        "INNER JOIN Kommission c ON c.PosID=b.ID " +
                                                        "INNER JOIN Tour d ON d.ID=c.TourID " +
                                                        "WHERE d.ID='" + ID + "' ORDER BY b.Status";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }
        ///<summary>clsTour / GetStartAdrID</summary>
        ///<remarks>Ermittel die ADR ID</remarks>
        public decimal GetStartAdrID()
        {
            string strSQL = string.Empty;
            strSQL = "Select Top(1) e.B_ID FROM Auftrag e " +
                                                            "INNER JOIN AuftragPos b  ON b.AuftragTableID=e.ID " +
                                                            "INNER JOIN Kommission c ON c.PosID=b.ID " +
                                                            "INNER JOIN Tour d ON d.ID=c.TourID " +
                                                            "WHERE d.ID=" + ID + "  ORDER BY c.BeladePos;";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        ///<summary>clsTour / GetEndAdrID</summary>
        ///<remarks>Ermittel die ADR ID</remarks>
        public decimal GetEndAdrID()
        {
            string strSQL = string.Empty;
            strSQL = "Select Top(1) e.E_ID FROM Auftrag e " +
                                                            "INNER JOIN AuftragPos b  ON b.AuftragTableID=e.ID " +
                                                            "INNER JOIN Kommission c ON c.PosID=b.ID " +
                                                            "INNER JOIN Tour d ON d.ID=c.TourID " +
                                                            "WHERE d.ID=" + ID + "  ORDER BY c.EntladePos DESC;";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        ///<summary>clsTour / InitArbeitsbereich</summary>
        ///<remarks>Daten zur Datenbank hinzufügen.</remarks>
        public void AddToDB()
        {
            Date_Add = DateTime.Now;
            try
            {
                string strSQL = string.Empty;
                strSQL = "INSERT INTO Tour (" +
                                            "KFZ_ZM, " +
                                            "KFZ_A, " +
                                            //"oldZM, " +
                                            "PersonalID, " +
                                            "StartZeit, " +
                                            "EndZeit, " +
                                            "Date_Add) " +

                                "VALUES ('" +
                                            KFZ_ZM + "', '" +
                                            KFZ_A + "', '" +
                                            // oldZM + "', '" +
                                            PersonalID + "', '" +
                                            StartZeit + "', '" +
                                            EndZeit + "', '" +
                                            Date_Add + "') ";

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
            finally
            {
                //Add Logbucheintrag Eintrag
                string Beschreibung = "Disposition - Tour :" + ID + " mit Fahrzeug: " + KFZ_ZM + " - Datum: " + StartZeit.Date.ToShortDateString() + " disponiert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Dispo.ToString(), Beschreibung);
            }
        }
        ///<summary>clsTour / UpdateTourDaten</summary>
        ///<remarks>Update Tour Daten.</remarks>
        public void UpdateTourDaten()
        {
            string strSQL = string.Empty;
            strSQL = "Update Tour SET " +
                                    "KFZ_ZM ='" + KFZ_ZM + "'" +
                                    ", KFZ_A ='" + KFZ_A + "'" +
                                    ", PersonalID ='" + PersonalID + "'" +
                                    ", StartZeit ='" + StartZeit + "'" +
                                    ", EndZeit ='" + EndZeit + "'" +
                                    ", LeerKM ='" + kmLeer + "'" +
                                    ", km ='" + km + "'" +
                                    "WHERE ID=" + ID + " ; ";
            clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
        }
        ///<summary>clsTour / DeleteTour</summary>
        ///<remarks>Update Tour Daten.</remarks>
        public bool DeleteTour()
        {
            string strSQL = string.Empty;
            //DispoCheck
            strSQL = "Delete FROM DispoCheck WHERE TourID=" + ID + " ;";
            //Status der AuftragPos auf 2 setzen
            strSQL = strSQL + "Update AuftragPos SET Status=2 WHERE ID IN (Select PosID FROM Kommission WHERE TourID=" + ID + ");";
            //Lieferschein löschen
            strSQL = strSQL + "DELETE FROM Lieferschein WHERE AP_ID IN (Select PosID FROM Kommission WHERE TourID=" + ID + ");";
            //TourKontaktInfo
            strSQL = strSQL + "DELETE FROM TourKontaktInfo WHERE KommissionsID IN (Select ID FROM Kommission WHERE TourID=" + ID + ");";
            //Kommission
            strSQL = strSQL + "Delete FROM Kommission WHERE TourID=" + ID + ";";
            //Tour
            strSQL = strSQL + "Delete FROM Tour WHERE ID='" + ID + "';";
            bool bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "TourDelete", BenutzerID);

            return bOK;
        }
        ///<summary>clsTour / UpdateStartZeit</summary>
        ///<remarks>Update Tour StartZeit.</remarks>
        public bool UpdateStartZeit()
        {
            string strSQL = string.Empty;
            //Kommission
            strSQL = strSQL + "Update Kommission SET B_Zeit ='" + StartZeit + "' WHERE ID IN (" +
                                        "Select ID FROM Kommission WHERE TourID='" + ID + "' AND " +
                                                                   "(B_Zeit<'" + StartZeit + "' OR B_Zeit>'" + EndZeit + "')); ";
            //Tour
            strSQL = strSQL + "Update Tour SET StartZeit='" + StartZeit + "' WHERE ID=" + ID + " ;";
            bool bTmp = clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "TourStartZeitChange", BenutzerID);
            return bTmp;
        }
        ///<summary>clsTour / UpdateEndZeit</summary>
        ///<remarks>Update Tour EndZeit.</remarks>
        public bool UpdateEndZeit()
        {
            string strSQL = string.Empty;
            //Kommission
            strSQL = strSQL + "Update Kommission SET E_Zeit ='" + EndZeit + "' WHERE ID IN (" +
                                        "Select ID FROM Kommission WHERE TourID='" + ID + "' AND " +
                                                                   "(E_Zeit<'" + StartZeit + "' OR E_Zeit>'" + EndZeit + "')); ";
            //Tour
            strSQL = strSQL + "Update Tour SET EndZeit='" + EndZeit + "' WHERE ID='" + ID + "' ;";
            bool bTmp = clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "TourEndZeitChange", BenutzerID);
            return bTmp;
        }
        ///<summary>clsTour / Fill</summary>
        ///<remarks>Ermittel anhand der ID die Daten.</remarks>
        public void Fill()
        {
            //ID Exist austauschen
            //Baustelle
            if (ID > 0)
            {
                DataTable dt = new DataTable();
                string strSQL = string.Empty;
                strSQL = "Select * FROM Tour WHERE ID='" + ID + "' ";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Tour");

                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        KFZ_ZM = (decimal)dt.Rows[i]["KFZ_ZM"];  // Zugmaschine
                        KFZ_A = (decimal)dt.Rows[i]["KFZ_A"];    //Auflieger
                        //oldZM  = (decimal)dt.Rows[i]["oldZM"];
                        PersonalID = (decimal)dt.Rows[i]["PersonalID"];
                        StartZeit = (DateTime)dt.Rows[i]["StartZeit"];
                        EndZeit = (DateTime)dt.Rows[i]["EndZeit"];
                        StartZeitShownCtr = StartZeit;
                        EndZeitShownCtr = EndZeit;
                        Date_Add = (DateTime)dt.Rows[i]["Date_Add"];
                        km = (Int32)dt.Rows[i]["KM"];
                        kmLeer = (Int32)dt.Rows[i]["LeerKM"];
                    }
                    //1.Beladeort der Tour
                    StartOrt = GetStartOrt();
                    //letzte Entladestelle der Tour
                    EndOrt = GetEndOrt();
                }
            }
        }
        ///<summary>clsTour / UpdateKFZ</summary>
        ///<remarks>Update KFZ.</remarks>
        public DataTable GetTourByZM()
        {
            string strSQL = string.Empty;
            DataTable dt = new DataTable();
            if (KFZ_ZM > 0)
            {
                strSQL = "Select ID FROM Tour  WHERE " +
                            "KFZ_ZM='" + KFZ_ZM + "' AND " +
                            "(((StartZeit >= '" + StartZeit + "') AND ((EndZeit>= '" + StartZeit + "') OR (EndZeit<='" + EndZeit + "'))) " +
                            "OR " +
                            "(((StartZeit >='" + StartZeit + "') OR (StartZeit<='" + EndZeit + "')) AND (EndZeit > '" + EndZeit + "')) " +
                            "OR " +
                            "((StartZeit >= '" + StartZeit + "') AND (EndZeit <= '" + EndZeit + "')) " +
                            "OR " +
                            "((StartZeit < '" + StartZeit + "') AND (EndZeit <= '" + EndZeit + "')) " +
                            ") ";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Touren");
            }
            return dt;
        }
        ///<summary>clsTour / GetLeerKMByZM</summary>
        ///<remarks></remarks>
        public DataTable GetLeerKMByZM()
        {
            //Baustelle
            string strSQL = string.Empty;
            DataTable dt = new DataTable();
            if (KFZ_ZM > 0)
            {
                strSQL = "Select ID, StartZeit, EndZeit  FROM Tour  WHERE " +
                            "KFZ_ZM='" + KFZ_ZM + "' AND " +
                            "(((StartZeit >= '" + StartZeit + "') AND ((EndZeit>= '" + StartZeit + "') OR (EndZeit<='" + EndZeit + "'))) " +
                            "OR " +
                            "(((StartZeit >='" + StartZeit + "') OR (StartZeit<='" + EndZeit + "')) AND (EndZeit > '" + EndZeit + "')) " +
                            "OR " +
                            "((StartZeit >= '" + StartZeit + "') AND (EndZeit <= '" + EndZeit + "')) " +
                            "OR " +
                            "((StartZeit < '" + StartZeit + "') AND (EndZeit <= '" + EndZeit + "')) " +
                            ") ORDER BY StartZeit ";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Touren");
            }
            return dt;
        }
        ///<summary>clsTour / ExistTourID</summary>
        ///<remarks>Prüft, ob die entsprechende Tour vorhanden ist.</remarks>
        public static bool ExistTourID(Globals._GL_USER myGLUser, decimal myTourID)
        {
            string strSQL = string.Empty;
            strSQL = "Select ID FROM Tour WHERE ID='" + myTourID + "' ";
            return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, myGLUser.User_ID);
        }
        ///<summary>clsTour / UpdateKFZ</summary>
        ///<remarks>Update KFZ.</remarks>
        public void UpdateKFZ(bool ZM)
        {
            string strSQL = string.Empty;
            if (ZM)
            {
                strSQL = "UPDATE Tour " +
                                        "SET KFZ_ZM = " + KFZ_ZM +
                                        " WHERE ID = " + ID;
            }
            else
            {
                strSQL = "UPDATE Tour " +
                                        "SET KFZ_A = " + KFZ_A +
                                        " WHERE ID = " + ID;
            }

            clsSQLcon.ExecuteSQL(strSQL, BenutzerID);

            if (ZM)
            {
                //Add Logbucheintrag Eintrag
                string Beschreibung = "Disposition - Tour :" + ID + " mit Fahrzeug: " + KFZ_ZM + " - Datum: " + StartZeit.Date.ToShortDateString() + " umdisponiert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Dispo.ToString(), Beschreibung);
            }
        }
        ///<summary>clsTour / UpdateKFZ</summary>
        ///<remarks>Update KFZ.</remarks>
        public void UpdateFahrer()
        {
            if (ID > 0)
            {
                string strSQL = string.Empty;
                strSQL = "UPDATE Tour " +
                                          "SET Personal ='" + PersonalID +
                                          "' WHERE ID = " + ID;

                clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            }
        }
        ///<summary>clsTour / UpdateKFZ</summary>
        ///<remarks>Update KFZ.</remarks>
        public void SetFahrerKontakt(bool boFahrerKontakt)
        {
            Int32 iBit = 0;
            if (boFahrerKontakt)
            {
                iBit = 1;
            }
            try
            {
                if (ID > 0)
                {
                    string strSQL = string.Empty;
                    strSQL = "UPDATE Tour " +
                                                      "SET " +
                                                      "FahrerKontakt ='" + iBit + "' " +
                                                      "WHERE ID ='" + ID + "'";

                    clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
                }
            }
            catch (Exception ex)
            {
                Functions.AddLogbuch(BenutzerID, "SetFahrerKontakt", ex.ToString());
            }
        }
        ///<summary>clsTour / SetPapiere</summary>
        ///<remarks>Update KFZ.</remarks>
        public void SetPapiere(bool boPapiere)
        {
            Int32 iBit = 0;
            if (boPapiere)
            {
                iBit = 1;
            }
            try
            {
                if (ID > 0)
                {
                    string strSQL = string.Empty;
                    strSQL = "UPDATE Kommission " +
                                                      "SET " +
                                                      "Papiere ='" + iBit + "' " +
                                                      "WHERE ID ='" + ID + "'";

                    clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
                }
            }
            catch (Exception ex)
            {
                Functions.AddLogbuch(BenutzerID, "SetPapiere", ex.ToString());
            }
        }
        ///<summary>clsTour / SetAndDeletByDispoChange</summary>
        ///<remarks>Die gesamte Tour wurde einem anderen Fahrezug (ZM) zugewiesen. Folgende Daten müssen
        ///         nun geändert werden:
        ///         - erstellte Lieferscheine müssen gelöscht werden
        ///         - erstellte Abholscheine müssen gelöscht werden
        ///         - Fremdlieferschein müssen gelöscht werden</remarks>
        public void SetAndDeleteByDispoChange()
        {
            KontaktInfo = String.Empty;
            UpdateTourDaten();
            /***
            //Druck Lieferscheine muss wieder gelöscht werden
            if (clsLieferscheine.LieferscheinExist(ctrTour.Kommission.AuftragPos_ID))
            {
                clsLieferscheine.DeletePrint(ctrTour.Kommission.AuftragPos_ID);
            }
            //Abholschein muss gelöscht werden
            if (clsDocScan.CheckDocScanIsIn(GL_User, KommiCtr.Kommission.AuftragID, Globals.enumDokumentenart.Abholschein.ToString()))
            {
                clsDocScan.DeleteAuftragScan(KommiCtr.Kommission.AuftragID, Globals.enumDokumentenart.Abholschein.ToString());
            }
            //Fremdlieferschein muss gelöscht werden
            if (clsDocScan.CheckDocScanIsIn(GL_User, KommiCtr.Kommission.AuftragID, Globals.enumDokumentenart.Fremdlieferschein.ToString()))
            {
                clsDocScan.DeleteAuftragScan(KommiCtr.Kommission.AuftragID, Globals.enumDokumentenart.Fremdlieferschein.ToString());
            }
             * ***/
        }
        //BAustelle Fahrerkontakt Info
        ///<summary>clsTour / GetAllFahrerKontakt</summary>
        ///<remarks>Ermittel, ob alle Dokumente für die erstellt wurden.</remarks>
        public static bool GetAllFahrerKontakt(Globals._GL_USER myGLUser, decimal myDecTourID)
        {
            bool retVal = false;
            if (clsKommission.ExistTourIDinKommission(myGLUser, myDecTourID))
            {
                DataTable dt = new DataTable();
                string strSQL = string.Empty;
                strSQL = "SELECT DISTINCT FahrerKontakt FROM Kommission WHERE TourID='" + myDecTourID + "' ";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Kommission");

                if (dt.Rows.Count > 0)
                {
                    //jetzt true setzen -> wenn nur eine Kommission false ist, dann ist 
                    //der Wert für die Gesamte Tour false;
                    retVal = true;
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        if ((bool)dt.Rows[i]["FahrerKontakt"] == false)
                        {
                            retVal = false;
                        }
                    }
                }
            }
            return retVal;
        }

        ///<summary>clsTour / GetNameByAP_ID</summary>
        ///<remarks>Ermittel den Fahrer Namen.</remarks>
        public static string GetNameByAP_ID(Globals._GL_USER myGLUser, decimal apID)
        {
            string strSQL = string.Empty;
            strSQL = "Select Name FROM Personal WHERE ID =" +
                                    "(Select a.PersonalID FROM Tour a " +
                                    "INNER JOIN Kommission b ON b.TourID=a.ID " +
                                    "WHERE b.PosID ='" + apID + "')";

            return clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
        }
        ///<summary>clsTour / GetNameByAP_ID</summary>
        ///<remarks>Ermittel den Fahrer Vornamen.</remarks>
        public static string GetVorNameByAP_ID(Globals._GL_USER myGLUser, decimal apID)
        {
            string strSQL = string.Empty;
            strSQL = "Select Vorname FROM Personal WHERE ID =" +
                                    "(Select a.PersonalID FROM Tour a " +
                                    "INNER JOIN Kommission b ON b.TourID=a.ID " +
                                    "WHERE b.PosID ='" + apID + "')";

            return clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
        }
        ///<summary>clsTour / GetNameByAP_ID</summary>
        ///<remarks>Ermittel den Fahrer Vornamen.</remarks>
        public static string GetZM_KFZByAP_ID(Globals._GL_USER myGLUser, decimal apID)
        {
            string strSQL = string.Empty;
            strSQL = "Select KFZ FROM Fahrzeuge WHERE ID =" +
                                    "(Select a.KFZ_ZM FROM Tour a " +
                                    "INNER JOIN Kommission b ON b.TourID=a.ID " +
                                    "WHERE b.PosID ='" + apID + "')";

            return clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
        }
        ///<summary>clsTour / GetA_KFZByAP_ID</summary>
        ///<remarks>.</remarks>
        public static string GetA_KFZByAP_ID(Globals._GL_USER myGLUser, decimal apID)
        {
            string strSQL = string.Empty;
            strSQL = "Select KFZ FROM Fahrzeuge WHERE ID =" +
                                    "(Select a.KFZ_A FROM Tour a " +
                                    "INNER JOIN Kommission b ON b.TourID=a.ID " +
                                    "WHERE b.PosID ='" + apID + "')";

            return clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
        }
        ///<summary>clsTour / GetA_KFZByAP_ID</summary>
        ///<remarks>.</remarks>
        public void TourCalculation()
        {
            if (ID > 0)
            {
                DataTable dtPoints = new DataTable("Wegepunkte");
                //Beladestellen
                string strSQL = string.Empty;
                strSQL = "Select TOP(100) a.B_ID as POINT FROM Auftrag a " +
                                                        "INNER JOIN AuftragPos b ON b.AuftragTableID=a.ID " +
                                                        "INNER JOIN Kommission c ON c.PosID=b.ID " +
                                                        "INNER JOIN Tour d ON d.ID=c.TourID " +
                                                        "WHERE d.ID=" + ID + " ORDER BY c.BeladePos;";

                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, this._GL_User.User_ID, "Punkte");
                dtPoints = dt.Copy();

                //Entladestellen
                strSQL = string.Empty;
                strSQL = "Select TOP(100) a.E_ID as POINT FROM Auftrag a " +
                                                            "INNER JOIN AuftragPos b ON b.AuftragTableID=a.ID " +
                                                            "INNER JOIN Kommission c ON c.PosID=b.ID " +
                                                            "INNER JOIN Tour d ON d.ID=c.TourID " +
                                                            "WHERE d.ID=" + ID + " ORDER BY c.entladePos DESC;";

                dt.Clear();
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, this._GL_User.User_ID, "Punkte");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        dtPoints.ImportRow(dt.Rows[i]);
                    }
                }

                if (dtPoints.Rows.Count > 0)
                {
                    this.km = 0;
                    Int32 iNext = 0;
                    for (Int32 i = 0; i < dtPoints.Rows.Count - 1; i++)
                    {
                        iNext = i + 1;
                        decimal decStart = 0;
                        Decimal.TryParse(dtPoints.Rows[i]["Point"].ToString(), out decStart);

                        decimal decEnd = 0;
                        Decimal.TryParse(dtPoints.Rows[iNext]["Point"].ToString(), out decEnd);

                        if (decStart != decEnd)
                        {
                            if ((decStart > 0) && (decEnd > 0))
                            {
                                clsDistance strecke = new clsDistance();
                                strecke.GL_User = this._GL_User;
                                strecke.FillByAdrID(decStart, decEnd);
                                this.km = km + strecke.km;
                            }
                        }
                    }
                    strSQL = string.Empty;
                    strSQL = "Update Tour SET KM=" + km + " WHERE ID=" + ID + " ;";
                    clsSQLcon.ExecuteSQL(strSQL, BenutzerID);

                }
            }
        }

        /****************************************************************************************
         *                        Methoden Kommission
         * *************************************************************************************/
        //
        /***
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
                                       "((KO.B_Zeit >= '" + BeladeZeit + "') AND (KO.E_Zeit <= '" + EntladeZeit + "')) "+
                                       "OR "+
                                       "((KO.B_Zeit < '" + BeladeZeit + "') AND (KO.E_Zeit <= '" + EntladeZeit + "')) "+
                                        ") ";

                dtRet = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "Aufträge");
                return dtRet;
            }
        ****/






    }
}
