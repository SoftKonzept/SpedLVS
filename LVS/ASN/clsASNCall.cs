using LVS.sqlStatementCreater;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace LVS
{
    public class clsASNCall
    {
        public const string const_AbrufAktion_Abruf = "Abruf";
        public const string const_AbrufAktion_UB = "UB";

        public const string const_Status_NotExist = "nicht vorhanden";
        public const string const_Status_Error = "Error";
        public const string const_Status_deactivated = "deaktiviert";
        public const string const_Status_erstellt = "erstellt";
        public const string const_Status_bearbeitet = "bearbeitet";
        public const string const_Status_MAT = "MAT";
        public const string const_Status_ENTL = "ENTL";

        public Color const_StatusColor_erstellt = Color.Blue;
        public Color const_StatusColor_bearbeitet = Color.Black;
        public Color const_StatusColor_MAT = Color.Yellow;
        public Color const_StatusColor_ENTL = Color.Red;

        private clsSystem _sys;
        public clsSystem sys
        {
            get { return _sys; }
            set
            {
                _sys = value;
                if (this._sys != null)
                {
                    //this.AbBereichID = _sys.AbBereich.ID;
                    //this.MandantenID = _sys.AbBereich.MandantenID;
                }
            }
        }
        public Globals._GL_SYSTEM _GL_System;
        public Globals._GL_USER _GL_User;
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


        public Int32 ID { get; set; }
        public bool IsRead { get; set; }

        //private Int32 artikelID;
        public Int32 ArtikelID { get; set; }
        //{
        //    get
        //    {
        //        return artikelID;
        //    }
        //    set
        //    {
        //        if (value == this.artikelID)
        //        {

        //        }
        //        else
        //        {                 
        //            if (value > 0)
        //            {
        //                clsASNCall tmpCall = new clsASNCall();
        //                tmpCall.InitClassByArtikelId(this._GL_User, this._GL_System, this.sys, value);
        //                //tmpCall.ArtikelID = this.artikelID;
        //                //tmpCall.FillbyArtikelID();
        //                this.CallExist = tmpCall.Copy();
        //            }
        //            artikelID = value;
        //        }


        //    }

        //}
        public Int32 LVSNr { get; set; }
        public string Werksnummer { get; set; }
        public string Produktionsnummer { get; set; }
        private string charge;
        public string Charge
        {
            get
            {
                if (charge is null)
                {
                    charge = string.Empty;
                }
                return charge;
            }
            set { charge = value; }
        }
        public decimal Netto { get; set; }
        public decimal Brutto { get; set; }
        public decimal Dicke { get; set; }
        public decimal Breite { get; set; }


        public Int32 CompanyID { get; set; }
        public string CompanyName { get; set; }
        public Int32 AbBereichID { get; set; }
        public DateTime Datum { get; set; }
        public DateTime EintreffDatum { get; set; }
        public DateTime EintreffZeit { get; set; }
        public Int32 UserID { get; set; }
        public string Benutzername { get; set; }
        public string Schicht { get; set; }
        public string Referenz { get; set; }
        public string Abladestelle { get; set; }
        public string Aktion { get; set; }
        public DateTime Erstellt { get; set; }
        public bool IsCreated { get; set; }
        //public bool IsError { get; set; }

        private string _status;
        public string Status
        {
            get
            {
                if ((_status is null) || (_status.Equals(string.Empty)))
                {
                    _status = const_Status_NotExist;
                }
                return _status;
            }
            set { _status = value; }
        }

        public Int32 LiefAdrID { get; set; }
        public Int32 EmpAdrID { get; set; }

        public Int32 SpedAdrID { get; set; }

        private string logText;
        public string LogText
        {
            get
            {
                if (logText == null)
                {
                    logText = string.Empty;
                }
                return logText;
            }
            set
            {
                logText = value;
                if (logText == null)
                {
                    logText = string.Empty;
                }
            }
        }

        public DataTable dtAbrufUBList = new DataTable();
        public DataTable dtAbrufUBListError = new DataTable();
        public List<Int32> ListAbrufArtikelID = new List<int>();

        public clsCompany Company;

        public string ASNFile { get; set; }
        public string ASNLieferant { get; set; }
        public int ASNQuantity { get; set; }
        public string ASNUnit { get; set; }
        public string Description { get; set; }

        public clsArtikel ArtikelReferenz;

        private clsASNCall callExist;
        public clsASNCall CallExist // { get; set; }
        {
            get
            {
                if (callExist is clsASNCall)
                {
                    if ((callExist.ID > 0) && (callExist.ArtikelID != this.ArtikelID))
                    {
                        //this.callExist = new clsASNCall();
                        this.callExist.InitClassByArtikelId(this._GL_User, this._GL_System, this.sys, this.ArtikelID);
                    }
                }
                else
                {
                    this.callExist = new clsASNCall();
                    this.callExist.InitClassByArtikelId(this._GL_User, this._GL_System, this.sys, this.ArtikelID);
                }
                return callExist;
            }
            set
            {
                callExist = value;
            }

        }

        public Int32 ScanUserId { get; set; }
        public DateTime ScanCheckForStoreOut { get; set; }


        public clsLAusgang AusgangToCreate { get; set; } = new clsLAusgang();

        /*************************************************************************************
         *                      Methoden - Procedure
         * **********************************************************************************/
        public clsASNCall()
        {
        }
        public clsASNCall(int myUserId) : this()
        {
            this._GL_User = new Globals._GL_USER();
            this._GL_User.User_ID = myUserId;
            this._GL_System = new Globals._GL_SYSTEM();
            this.sys = new clsSystem();
        }
        ///<summary>clsASNCall / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSys, clsSystem mySys)
        {
            this._GL_User = myGLUser;
            this._GL_System = myGLSys;
            this.sys = mySys;
        }
        public void InitClassByArtikelId(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSys, clsSystem mySys, int myArtikelId)
        {
            this._GL_User = myGLUser;
            this._GL_System = myGLSys;
            this.sys = mySys;
            if (myArtikelId > 0)
            {
                this.ArtikelID = myArtikelId;
                this.FillbyArtikelID();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="myGLSys"></param>
        /// <param name="mySys"></param>
        /// <param name="myCallId"></param>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSys, clsSystem mySys, int myCallId)
        {
            this._GL_User = myGLUser;
            this._GL_System = myGLSys;
            this.sys = mySys;
            if (myCallId > 0)
            {
                this.ID = myCallId;
                this.Fill();
            }
        }
        ///<summary>clsASNCall / InitClass</summary>
        ///<remarks></remarks>>
        public clsASNCall Copy()
        {
            return (clsASNCall)this.MemberwiseClone();
        }
        ///<summary>clsASNCall / Add</summary>
        ///<remarks></remarks>>
        public bool Add()
        {
            bool bOK = false;
            string strSQL = string.Empty;
            //this.IsCreated = false;
            this.Erstellt = DateTime.Now;
            strSQL = "INSERT INTO Abrufe (IsRead, ArtikelID, LVSNr, Werksnummer, Produktionsnummer, Charge, Brutto, CompanyID, CompanyName, AbBereich, Datum" +
                                           ", EintreffDatum, EintreffZeit,BenutzerID, Benutzername, Schicht, Referenz, Abladestelle, Aktion, Erstellt, IsCreated" +
                                           ", Status, LiefAdrID, EmpAdrID, SpedAdrID, ASNFile, ASNLieferant, ASNQuantity, ASNUnit, Description " +
                                        ") " +
                     "VALUES (" + Convert.ToInt32(this.IsRead) +
                                 ", " + this.ArtikelID +
                                 ", " + this.LVSNr +
                                 ", '" + this.Werksnummer + "'" +
                                 ", '" + this.Produktionsnummer + "'" +
                                 ", '" + this.Charge + "'" +
                                 ", '" + this.Brutto.ToString().Replace(",", ".") + "'" +
                                 ", " + this.CompanyID +
                                 ", '" + this.CompanyName + "'" +
                                 ", " + this.AbBereichID +
                                 ", '" + this.Datum + "'" +
                                 ", '" + this.EintreffDatum + "'" +
                                 ", '" + this.EintreffZeit + "' " +
                                 ", " + this.BenutzerID +
                                 ", '" + this.Benutzername + "'" +
                                 ", '" + this.Schicht + "'" +
                                 ", '" + this.Referenz + "'" +
                                 ", '" + this.Abladestelle + "'" +
                                 ", '" + this.Aktion + "'" +
                                 ", '" + this.Erstellt + "'" +
                                 ", " + Convert.ToInt32(this.IsCreated) +
                                 ", '" + this.Status + "'" +
                                 ", " + this.LiefAdrID +
                                 ", " + this.EmpAdrID +
                                 ", " + this.SpedAdrID +
                                 ", '" + this.ASNFile + "'" +
                                 ", '" + this.ASNLieferant + "'" +
                                 ", '" + this.ASNQuantity + "'" +
                                 ", '" + this.ASNUnit + "'" +
                                 ", '" + this.Description + "'" +
                                 //", " + Convert.ToInt32(this.IsError) +
                                 ");";
            strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
            try
            {
                string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSQL, "INSERTCALL", this.BenutzerID);
                int iTmp = 0;
                int.TryParse(strTmp, out iTmp);
                if (iTmp > 0)
                {
                    bOK = true;
                    this.ID = iTmp;
                }
                else
                {
                    bOK = false;
                }
            }
            catch (Exception ex)
            {
                //this.InfoText = "Der neue Datensatz konnte nicht eingetragen werden. Fehlercode [" + clsErrorCode.const_ErrorCode_200 + "]";
            }
            return bOK;
        }
        ///<summary>clsASNCall / Update</summary>
        ///<remarks></remarks>
        public bool Update()
        {
            bool bOK = false;
            DataTable dt = new DataTable();
            string strSQL = "Update Abrufe SET " +
                                    "IsRead =" + Convert.ToInt32(this.IsRead) +
                                    ", ArtikelID=" + this.ArtikelID +
                                    ", LVSNr =" + this.LVSNr +
                                    ", Werksnummer ='" + this.Werksnummer + "'" +
                                    ", Produktionsnummer ='" + this.Produktionsnummer + "'" +
                                    ", Charge ='" + this.Charge + "'" +
                                    ", Brutto = '" + this.Brutto.ToString().Replace(",", ".") + "'" +
                                    ", CompanyID= " + this.CompanyID +
                                    ", CompanyName ='" + this.CompanyName + "'" +
                                    ", AbBereich = " + this.AbBereichID +
                                    ", Datum ='" + this.Datum + "'" +
                                    ", EintreffDatum ='" + this.EintreffDatum + "'" +
                                    ", EintreffZeit ='" + this.EintreffZeit + "'" +
                                    ", BenutzerID =" + this.BenutzerID +
                                    ", Benutzername = '" + this.Benutzername + "'" +
                                    ", Schicht='" + this.Schicht + "'" +
                                    ", Referenz = '" + this.Referenz + "'" +
                                    ", Abladestelle ='" + this.Abladestelle + "'" +
                                    ", Aktion ='" + this.Aktion + "'" +
                                    ", Erstellt ='" + this.Erstellt + "'" +
                                    ", Status='" + this.Status + "'" +
                                    ", LiefAdrID= " + this.LiefAdrID +
                                    ", EmpAdrID = " + this.EmpAdrID +
                                    ", SpedAdrID = " + this.SpedAdrID +
                                    ", ASNFile = '" + this.ASNFile + "'" +
                                    ", ASNLieferant = '" + this.ASNLieferant + "'" +
                                    ", ASNQuantity = '" + this.ASNQuantity + "'" +
                                    ", ASNUnit = '" + this.ASNUnit + "'" +
                                    ", Description ='" + this.Description + "'" +
                                    //", IsError =" + Convert.ToInt32(this.IsError) +

                                    " WHERE ID=" + this.ID + ";";

            bOK = clsSQLcon.ExecuteSQL(strSQL, this.BenutzerID);
            if (bOK)
            {
                //this.InfoText = "Der Datensatz konnte erfolgreich upgedatet werden!";
            }
            else
            {
                //this.InfoText = "Der Datensatz konnte nicht upgedatet werden. Fehlercode[" + clsErrorCode.const_ErrorCode_201.ToString() + "]";
            }
            return bOK;
        }
        ///<summary>clsASNCall / Update</summary>
        ///<remarks></remarks>
        public DataTable GetCallOrRebookingList(string myAction)
        {
            string strSql = string.Empty;
            dtAbrufUBList = new DataTable();
            //strSql = "Select CAST(0 as bit) as 'Select' " +
            //                ", ab.EintreffDatum as Eintreffdatum " +
            //                ", ab.EintreffZeit as Eintreffzeit" +
            //                ", ab.ID as AbrufID" +
            //                ", ab.Status" +
            //                ", ab.Benutzername as Bearbeiter" +
            //                ", ab.Schicht" +
            //                ", ab.Referenz" +
            //                ", ab.Abladestelle" +
            //                ", ab.Aktion" +
            //                ", ab.erstellt" +
            //                //", ab.Status"+ 
            //                ", a.ID as ArtikelID" +
            //                ", a.LVS_ID as LVSNr" +
            //                ", a.Werksnummer" +
            //                ", a.Produktionsnummer" +
            //                ", a.Charge" +
            //                ", a.Brutto" +
            //                ", a.Dicke" +
            //                ", a.Breite" +
            //                ", a.Anzahl as Menge" +
            //                ", (a.Reihe+'/'+a.Platz) as 'Reihe/Platz'" +
            //                ", (Select Name1 FROM ADR WHERE ID=e.Auftraggeber) as Lieferant" +
            //                ", (Select Name1 FROM ADR WHERE ID=ab.LiefAdrID) as Entladestelle" +
            //                ", (Select Name1 FROM ADR WHERE ID=ab.EmpAdrID) as Empfänger" +
            //                ", (Select Name1 FROM ADR WHERE ID=ab.SpedAdrID) as Spediteur" +
            //                ", e.Auftraggeber" +
            //                ",CASE " +
            //                      "WHEN (SELECT COUNT (*) " +
            //                        " FROM Artikel a1 " +
            //                        " INNER JOIN LEingang c1 ON c1.ID=a1.LEingangTableID " +
            //                        " INNER JOIN SchadenZuweisung d1 ON d1.ArtikelID=a1.ID " +
            //                        " INNER JOIN Schaeden e1 ON e1.ID=d1.SchadenID " +
            //                        " WHERE a1.ID=a.ID) > 0 " +
            //                      " THEN (SELECT e2.Bezeichnung + char(10) " +
            //                          " FROM Artikel a2 " +
            //                          " INNER JOIN LEingang c2 ON c2.ID=a2.LEingangTableID " +
            //                          " LEFT OUTER JOIN SchadenZuweisung d2 ON d2.ArtikelID=a2.ID " +
            //                          " LEFT OUTER JOIN Schaeden e2 ON e2.ID=d2.SchadenID " +
            //                          " WHERE a2.ID=a.ID " +
            //                          " FOR XML PATH ('')) " +
            //                      " ELSE '' " +
            //                      " END as Schaden " +

            //                    " FROM Abrufe ab " +
            //                    "INNER JOIN Artikel a on a.ID=ab.ArtikelID " +
            //                    "INNER JOIN LEingang e on e.ID=a.LEingangTableID " +
            //                            "WHERE " +
            //                                "ab.AbBereich=" + this.sys.AbBereich.ID +
            //                                " AND ab.IsRead=0 " +
            //                                " AND ab.Aktion='" + myAction + "'" +
            //                                " AND ab.IsCreated=1 " +
            //                                " ORDER BY ab.EintreffDatum, ab.EintreffZeit ";

            sqlCreater_Call sqlCreater = new sqlCreater_Call();
            strSql = sqlCreater.sqlString_Call_GetOpenCall(myAction, (int)this.sys.AbBereich.ID, 0, false);
            dtAbrufUBList = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.BenutzerID, "CallOrRebooking");
            return dtAbrufUBList;
        }

        public DataTable GetCallOrRebookingErrorList(string myAction)
        {
            string strSql = string.Empty;
            dtAbrufUBListError = new DataTable();
            strSql = "Select CAST(0 as bit) as 'Select' " +
                            ", ab.EintreffDatum as Eintreffdatum " +
                            ", ab.EintreffZeit as Eintreffzeit" +
                            ", ab.ID as AbrufID" +
                            ", ab.ArtikelID " +
                            ", ab.LVSNr " +
                            ", ab.Werksnummer " +
                            ", ab.Produktionsnummer " +
                            ", ab.Charge " +
                            ", ab.Brutto " +
                            ", ab.Status" +
                            ", ab.Benutzername as Bearbeiter" +
                            ", ab.Schicht" +
                            ", ab.Referenz" +
                            ", ab.Abladestelle" +
                            ", ab.Aktion" +
                            ", ab.erstellt" +
                                " FROM Abrufe ab " +
                                "LEFT JOIN Artikel a on a.ID=ab.ArtikelID " +
                                "LEFT JOIN LEingang e on e.ID=a.LEingangTableID " +
                                        "WHERE " +
                                            "ab.AbBereich=" + this.sys.AbBereich.ID +
                                            " AND ab.IsRead=0 " +
                                            " AND ab.Aktion='" + myAction + "'" +
                                            " AND ab.IsCreated=1 " +
                                            " AND ab.Status= '" + clsASNCall.const_Status_Error + "' " +
                                            " ORDER BY ab.EintreffDatum, ab.EintreffZeit ";
            dtAbrufUBListError = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.BenutzerID, "CallOrRebooking");
            return dtAbrufUBListError;
        }

        ///<summary>clsASNCall / GetSQLMainBestandsdaten</summary>
        ///<remarks>.</remarks>
        public void Fill()
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            strSql = "SELECT * FROM Abrufe WHERE ID=" + this.ID;

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.BenutzerID, "CallOrRebooking");
            FillClass(ref dt);
        }
        ///<summary>clsASNCall / GetSQLMainBestandsdaten</summary>
        ///<remarks>.</remarks>
        public void FillbyArtikelID()
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            strSql = "SELECT * FROM Abrufe WHERE ArtikelID=" + this.ArtikelID;

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, this.BenutzerID, "CallOrRebooking");
            if (dt.Rows.Count > 0)
            {
                FillClass(ref dt);
            }
        }
        ///<summary>clsASNCall / FillClass</summary>
        ///<remarks>.</remarks>
        private void FillClass(ref DataTable myDT)
        {
            for (Int32 i = 0; i <= myDT.Rows.Count - 1; i++)
            {
                Int32 iTmp = 0;
                Int32.TryParse(myDT.Rows[i]["ID"].ToString(), out iTmp);
                this.ID = iTmp;
                this.IsRead = (bool)myDT.Rows[i]["IsRead"];
                iTmp = 0;
                Int32.TryParse(myDT.Rows[i]["ArtikelID"].ToString(), out iTmp);
                this.ArtikelID = iTmp;
                iTmp = 0;
                Int32.TryParse(myDT.Rows[i]["LVSNr"].ToString(), out iTmp);
                this.LVSNr = iTmp;
                this.Werksnummer = myDT.Rows[i]["Werksnummer"].ToString();
                this.Produktionsnummer = myDT.Rows[i]["Produktionsnummer"].ToString();
                this.Charge = myDT.Rows[i]["Charge"].ToString();
                decimal decTmp = 0;
                Decimal.TryParse(myDT.Rows[i]["Brutto"].ToString(), out decTmp);
                this.Brutto = decTmp;
                iTmp = 0;
                Int32.TryParse(myDT.Rows[i]["CompanyID"].ToString(), out iTmp);
                this.CompanyID = iTmp;
                this.CompanyName = myDT.Rows[i]["CompanyName"].ToString();
                iTmp = 0;
                Int32.TryParse(myDT.Rows[i]["AbBereich"].ToString(), out iTmp);
                this.AbBereichID = iTmp;
                this.Datum = (DateTime)myDT.Rows[i]["Datum"];
                this.EintreffDatum = (DateTime)myDT.Rows[i]["EintreffDatum"];
                this.EintreffZeit = (DateTime)myDT.Rows[i]["EintreffZeit"];
                iTmp = 0;
                Int32.TryParse(myDT.Rows[i]["BenutzerID"].ToString(), out iTmp);
                this.BenutzerID = iTmp;
                this.Benutzername = myDT.Rows[i]["Benutzername"].ToString();
                this.Schicht = myDT.Rows[i]["Schicht"].ToString();
                this.Referenz = myDT.Rows[i]["Referenz"].ToString();
                this.Abladestelle = myDT.Rows[i]["Abladestelle"].ToString();
                this.Aktion = myDT.Rows[i]["Aktion"].ToString();
                this.Erstellt = (DateTime)myDT.Rows[i]["Erstellt"];
                this.Status = const_Status_NotExist;
                if (!myDT.Rows[i]["Status"].ToString().Equals(string.Empty))
                {
                    this.Status = myDT.Rows[i]["Status"].ToString();
                }
                iTmp = 0;
                Int32.TryParse(myDT.Rows[i]["LiefAdrID"].ToString(), out iTmp);
                this.LiefAdrID = iTmp;
                iTmp = 0;
                Int32.TryParse(myDT.Rows[i]["EmpAdrID"].ToString(), out iTmp);
                this.EmpAdrID = iTmp;
                iTmp = 0;
                Int32.TryParse(myDT.Rows[i]["SpedAdrID"].ToString(), out iTmp);
                this.SpedAdrID = iTmp;
                this.ASNFile = myDT.Rows[i]["ASNFile"].ToString();
                this.ASNLieferant = myDT.Rows[i]["ASNLieferant"].ToString();
                iTmp = 0;
                Int32.TryParse(myDT.Rows[i]["ASNQuantity"].ToString(), out iTmp);
                this.ASNQuantity = iTmp;
                this.ASNUnit = myDT.Rows[i]["ASNUnit"].ToString();
                this.Description = myDT.Rows[i]["Description"].ToString();


                DateTime dtTmp = new DateTime(19, 1, 1);
                if ((DateTime)myDT.Rows[i]["ScanCheckForStoreOut"] < dtTmp)
                {
                    this.ScanCheckForStoreOut = dtTmp;
                }
                else
                {
                    this.ScanCheckForStoreOut = (DateTime)myDT.Rows[i]["ScanCheckForStoreOut"];
                }
                iTmp = 0;
                Int32.TryParse(myDT.Rows[i]["ScanUserId"].ToString(), out iTmp);
                this.ScanUserId = iTmp;


                //Company
                this.Company = new clsCompany();
                this.Company._GL_User = this._GL_User;
                this.Company.ID = this.CompanyID;
                this.Company.Fill();
                //Status

                //ArtikelReferenz = ARtikel, der Abgerufen wird
                this.ArtikelReferenz = new clsArtikel();
                this.ArtikelReferenz.InitClass(this._GL_User, this._GL_System);
                //if (this.ArtikelID > 0)
                //{
                //    this.ArtikelReferenz.ID = this.ArtikelID;
                //    this.ArtikelReferenz.GetArtikeldatenByTableID();
                //}
                //else
                //{
                //    if (this.Status.Equals(const_Status_Error))
                //    {
                //        this.ArtikelReferenz.GetArtikeldatenByProductionNr(this.LVSNr.ToString(), this.AbBereichID);
                //    }
                //}
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsArtikelIdUniqueInCall()
        {
            bool bReturn = false;
            string strSQL = string.Empty;
            strSQL = "Select ID FROM Abrufe " +
                                        " WHERE " +
                                            " ArtikelID=" + this.ArtikelID +
                                            // " AND IsRead=0 " +
                                            " AND Aktion='" + this.Aktion + "' ; ";
            //" AND IsCreated=1 ";

            bReturn = !clsSQLcon.ExecuteSQL_GetValueBool(strSQL, this.BenutzerID);
            return bReturn;
        }
        /// <summary>
        ///             Prüft, ob es sich hier um einen deaktivierten Abruf handelt?
        /// </summary>
        /// <returns></returns>
        public bool IsArtikelToReaktivate()
        {
            bool bReturn = false;
            string strSQL = string.Empty;
            strSQL = "Select ID FROM Abrufe " +
                                        " WHERE " +
                                            " ArtikelID=" + this.ArtikelID +
                                            " AND [Status] = " + clsASNCall.const_Status_deactivated +
                                            " AND Aktion='" + this.Aktion + "' ; ";

            bReturn = !clsSQLcon.ExecuteSQL_GetValueBool(strSQL, this.BenutzerID);
            return bReturn;
        }
        ///<summary>clsASNCall / ExistNewCallOrRebookingToProceed</summary>
        ///<remarks></remarks>
        public static bool ExistNewCallOrRebookingToProceed(decimal myBenuzter, decimal myAbBereich, string myAction)
        {
            string strSQL = string.Empty;
            strSQL = "Select ID FROM Abrufe " +
                                        " WHERE " +
                                            " AbBereich=" + myAbBereich +
                                            " AND IsRead=0 " +
                                            " AND Aktion='" + myAction + "'" +
                                            " AND IsCreated=1 ";
            return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, myBenuzter);
        }
        ///<summary>clsASNCall / InsertASN</summary>
        ///<remarks></remarks>
        public bool DisableCallOrRebooking(DataTable mydt)
        {
            bool bUpdate = false;
            List<string> ListCallDisable = new List<string>();
            //Eingang
            for (Int32 i = 0; i <= mydt.Rows.Count - 1; i++)
            {
                string strASN = mydt.Rows[i]["AbrufID"].ToString();
                ListCallDisable.Add(strASN);
            }
            if (ListCallDisable.Count > 0)
            {
                string strSql1 = string.Empty;
                strSql1 = "Update Abrufe SET IsRead=1 WHERE ID IN (" + string.Join(",", ListCallDisable.ToArray()) + "); ";
                //bUpdate = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql1, "CallDisable", BenutzerID);
                bUpdate = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql1, "CallDisable", BenutzerID);
            }
            return bUpdate;
        }
        ///<summary>clsASNCall / ProzessCall</summary>
        ///<remarks>Abrufe werden verarbeitet und Lagerausgang wird erstellt</remarks>  
        public void ProzessCall(DataTable dtArtikel, bool IsSped4Action = true)
        {
            try
            {
                //Log
                this.LogText = string.Empty;
                //string strLieferantenString = clsADR.GetADRString((decimal)this.LiefAdrID);

                AddressViewData adrViewData = new AddressViewData(this.LiefAdrID, (int)BenutzerID);
                string strLieferantenString = adrViewData.GetADRString();

                this.LogText = this.LogText + Environment.NewLine + "Lieferant: " + strLieferantenString + Environment.NewLine;

                AusgangToCreate = new clsLAusgang();
                AusgangToCreate.InitDefaultClsAusgang(this._GL_User, this.sys);

                //hier erhalten wir die Artikel von einem Lieferanten
                if (dtArtikel.Rows.Count > 0)
                {
                    clsArtikel tmpArt = new clsArtikel();
                    tmpArt.InitClass(this._GL_User, this._GL_System);
                    tmpArt.ID = this.ArtikelID;
                    tmpArt.GetArtikeldatenByTableID();
                    AusgangToCreate.Auftraggeber = tmpArt.Eingang.Auftraggeber;

                    //AUsgangsdaten die angepasst werden müssen sonst default                
                    AusgangToCreate.Empfaenger = (decimal)this.EmpAdrID;
                    AusgangToCreate.SpedID = (decimal)this.SpedAdrID;
                    AusgangToCreate.Entladestelle = (decimal)this.LiefAdrID;
                    AusgangToCreate.BeladeID = this.sys.Mandant.ADR_ID;
                    //AusgangToCreate.LfsDate = clsSystem.const_DefaultDateTimeValue_Min;
                    AusgangToCreate.MAT = AusgangToCreate.LAusgangsDate.ToString();
                    AusgangToCreate.Checked = true;
                    AusgangToCreate.KFZ = string.Empty;
                    //AusgangToCreate.Lieferant = clsADRVerweis.GetLieferantenVerweisBySenderAndReceiverAdr(AusgangToCreate.Auftraggeber, AusgangToCreate.Empfaenger, this.BenutzerID, constValue_AsnArt.const_Art_VDA4913, this.sys.AbBereich.ID);

                    AusgangToCreate.Lieferant = AddressReferenceViewData.GetLieferantenVerweisBySenderAndReceiverAdr((int)AusgangToCreate.Auftraggeber, (int)AusgangToCreate.Empfaenger, (int)this.BenutzerID, (int)this.sys.AbBereich.ID);

                    AusgangToCreate.Info = "autom. Abruf";
                    AusgangToCreate.exTransportRef = this.Referenz;
                    DateTime dtTermin = Convert.ToDateTime(this.EintreffDatum.ToShortDateString() + " " + this.EintreffZeit.ToShortTimeString());
                    AusgangToCreate.Termin = dtTermin;

                    string strSQLAusgang = "DECLARE @LAusgangTableID decimal; ";
                    strSQLAusgang = strSQLAusgang + AusgangToCreate.AddLAusgang_SQL();
                    strSQLAusgang = strSQLAusgang + "SET @LAusgangTableID=(Select @@IDENTITY); ";

                    clsASNCall tmpCall = this.Copy();
                    bool bSQLInsert = false;
                    string strSQL = string.Empty;
                    int iCount = 0;
                    bool bInsert = true;
                    List<int> ListArtikelIdToVita = new List<int>(); // Artikel die wirklich ausgelagert wurden (BMW immer nur einer)
                    foreach (DataRow row in dtArtikel.Rows)
                    {
                        if (
                            (this.sys.AbBereich.ArtMaxCountInAusgang > 0) &&
                            (iCount >= this.sys.AbBereich.ArtMaxCountInAusgang)
                          )
                        {
                            break;
                        }
                        if (bInsert)
                        {
                            decimal decArtiID = 0;
                            Decimal.TryParse(row["ArtikelID"].ToString(), out decArtiID);
                            if (ArtikelID > 0)
                            {
                                ListArtikelIdToVita.Add((int)decArtiID);
                                Int32 iTmp = 0;
                                Int32.TryParse(row["AbrufID"].ToString(), out iTmp);
                                if (iTmp > 0)
                                {
                                    tmpCall.ID = iTmp;
                                    tmpCall.Fill();

                                    tmpArt = new clsArtikel();
                                    tmpArt.InitClass(this._GL_User, this._GL_System);
                                    tmpArt.ID = decArtiID;
                                    tmpArt.GetArtikeldatenByTableID();

                                    if (tmpArt.LAusgangTableID == 0)
                                    {
                                        //tmpArt.AusgangChecked = true; ;
                                        //tmpArt.AusgangChecked = IsSped4Action;
                                        tmpArt.AbrufReferenz = tmpCall.Referenz;
                                        tmpArt.Abladestelle = tmpCall.Abladestelle;

                                        strSQL = strSQL + tmpArt.UpdateArtikelforCall(IsSped4Action);
                                        strSQL = strSQL + clsArtikelVita.AddArtikelLAusgangAutoSQL(this._GL_User, AusgangToCreate.LAusgangTableID, AusgangToCreate.LAusgangID);

                                        //Status setzen in Abrufe
                                        strSQL = strSQL + " Update Abrufe SET " +
                                                                    "[Status]='" + clsASNCall.const_Status_bearbeitet + "' " +
                                                                    ", IsRead=1 " +
                                                                    " WHERE ID=" + tmpCall.ID + " ; ";
                                        if (tmpArt.bSPL)
                                        {
                                            strSQL = strSQL + tmpArt.SPL.SQL_DoSPLCheckOutByCall(tmpArt);
                                        }
                                        this.LogText = LogText + " > Artikel ID/Ausgang: [" + tmpArt.ID.ToString() + "/" + AusgangToCreate.LAusgangID.ToString() + "] erfolgreich ausgelagert !!!" + Environment.NewLine;

                                        bSQLInsert = true;
                                    }
                                    else
                                    {
                                        //Log
                                        this.LogText = LogText + " > ERROR -> Artikel ID/Ausgang: [" + tmpArt.ID.ToString() + "/" + AusgangToCreate.LAusgangID.ToString() + "] ist bereits ausgelagert !!!" + Environment.NewLine;
                                    }
                                }
                            }
                        }
                        iCount++;
                    }
                    strSQL = strSQL + " Update LAusgang SET " +
                                              "Netto = (Select SUM(Netto) from Artikel WHERE LAusgangTableID=@LAusgangTableID) " +
                                              ", Brutto = (Select SUM(Brutto) from Artikel WHERE LAusgangTableID=@LAusgangTableID) " +
                                              ", SLB = (Select LAusgangID FROM LAusgang WHERE ID=@LAusgangTableID)" +
                                              ", Checked=0 " +
                                              ", LfsNr = '" + AusgangToCreate.AbBereichID.ToString() + "/A '+ CAST(@LAusgangTableID as nvarchar(20))" +
                                              " WHERE ID = @LAusgangTableID " +
                                              " ; ";
                    string strSQLFinal = string.Empty;
                    //wenn bSQLInsert = true, dann gibt es mindestns ein Artikel zum Abrufen
                    if (bSQLInsert)
                    {
                        strSQLFinal = strSQLAusgang + strSQL + " Select @LAusgangTableID;";

                        string strLAusgangTableID = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSQLFinal, "Abruf", this._GL_User.User_ID);
                        decimal decTmp = 0;
                        Decimal.TryParse(strLAusgangTableID, out decTmp);
                        if (decTmp > 0)
                        {
                            AusgangToCreate.LAusgangTableID = decTmp;
                            AusgangToCreate.FillAusgang();
                            //Log
                            this.LogText = LogText + " > Ausgang / ID: [" + AusgangToCreate.LAusgangID.ToString() + "/" + AusgangToCreate.LAusgangTableID.ToString() + "] erstellt..." + Environment.NewLine;

                            //Vita Ausgang
                            clsArtikelVita.AddAuslagerungAuto(this.BenutzerID, AusgangToCreate.LAusgangTableID, AusgangToCreate.LAusgangID);
                            //Vita Artikel
                            foreach (int itm in ListArtikelIdToVita)
                            {
                                clsArtikelVita.AddArtikelLAusgangAuto(this._GL_User, itm, AusgangToCreate.LAusgangID);
                            }

                            //Vita Ausgang abgeschlossen
                            clsArtikelVita.LagerAusgangAutoChecked(this.BenutzerID, AusgangToCreate.LAusgangTableID, AusgangToCreate.LAusgangID);

                            clsLager Lager = new clsLager();
                            Lager.InitClass(this._GL_User, this._GL_System, this.sys);

                            //Lagermeldungen erstellen
                            if (AusgangToCreate.Checked)
                            {
                                clsASNTransfer AsnTransfer = new clsASNTransfer();
                                if (AsnTransfer.DoASNTransfer(this._GL_System, AusgangToCreate.AbBereichID, AusgangToCreate.MandantenID))
                                {
                                    Lager.LAusgangTableID = AusgangToCreate.LAusgangTableID;
                                    Lager.LEingangTableID = 0;
                                    Lager.FillLagerDaten(true);

                                    Lager.ASNAction = new clsASNAction();
                                    Lager.ASNAction.InitClass(ref _GL_User);
                                    Lager.ASNAction.ASNActionProcessNr = clsASNAction.const_ASNAction_Ausgang;
                                    AsnTransfer.CreateLM(ref Lager);
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                if (this.sys.Client != null)
                                {
                                    clsMail EMail = new clsMail();
                                    EMail.InitClass(this._GL_User, this.sys);
                                    EMail.Subject = this.sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error clsASNCall: Fehler beim Einlesen eines Abrufs";
                                    string strTxt = "Function: CreateLAusgang" + Environment.NewLine;
                                    strTxt = strTxt + "Artikel LVSNr/ID: [" + tmpArt.LVS_ID.ToString() + "/" + tmpArt.ID.ToString() + "]" + Environment.NewLine;
                                    strTxt = strTxt + "Abruf ID: [" + tmpCall.ID.ToString() + "]" + Environment.NewLine;
                                    strTxt = strTxt + "sql: " + strSQLFinal + Environment.NewLine;
                                    EMail.Message = strTxt;
                                    EMail.SendError();
                                }
                            }
                            catch (Exception ex1)
                            {
                            }
                            this.LogText = Environment.NewLine + "Lieferant: " + strLieferantenString + Environment.NewLine;
                            this.LogText = this.LogText + " > ERROR -> Fehler in der Datenbankabfrage - Error Mail erstellt!!!" + Environment.NewLine;
                        }
                    }
                    else
                    {
                        this.LogText = this.LogText + " > ERROR -> keine Artikel zur Auslagerung vorhanden !!!" + Environment.NewLine;
                    }
                }
            }
            catch (Exception ex)
            {
                LogText += Environment.NewLine + ex.Message;
                //this.ListLogText.Add(LogText);
            }
        }
        ///<summary>clsASNCall / ProzessRebooking</summary>
        ///<remarks>Umbuchungen werden erstellt</remarks>  
        public void ProzessRebooking(DataTable dtArtikel)
        {
            if (dtArtikel.Rows.Count > 0)
            {
                //Log
                this.LogText = string.Empty;
                string strLieferantenString = clsADR.GetADRString((decimal)this.LiefAdrID);
                this.LogText = this.LogText + Environment.NewLine + "Lieferant: " + strLieferantenString + Environment.NewLine;

                clsUmbuchung UB = new clsUmbuchung();
                UB._GL_System = this._GL_System;
                UB._GL_User = this._GL_User;
                UB.System = this.sys;
                UB.InitUmbuchung();
                UB.NettoGesamt = 0;
                UB.BruttoGesamt = 0;
                UB.UBDate = DateTime.Now;
                UB.dtUmbuchung = new DataTable();
                UB.dtUmbuchung = dtArtikel;

                clsSystem tmpSys = this.sys.Copy();
                clsClient.ctrUmbuchung_CustomizeDefaulUBDaten(ref tmpSys, ref UB);
                UB.AuftraggeberAltID = (decimal)this.LiefAdrID;
                UB.AuftraggeberNeuID = (decimal)this.EmpAdrID;


                string strSQL = UB.DoUmbuchungSQL();
                DataTable dtUB = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "UB", "UB", this.BenutzerID);
                if (dtUB.Rows.Count > 0)
                {
                    //Die Table enthält die beiden Spalten EingangTableID und AusgangTableID
                    foreach (DataRow row in dtUB.Rows)
                    {
                        //LAusgangTabelID
                        decimal decTmp = 0;
                        decimal.TryParse(row["AusgangTableID"].ToString(), out decTmp);
                        if (decTmp > 0)
                        {
                            UB.LAusgang.LAusgangTableID = decTmp;
                            UB.LAusgang.FillAusgang();
                            //Log
                            this.LogText = LogText + " > Ausgang / ID: [" + UB.LAusgang.LAusgangID.ToString() + "/" + UB.LAusgang.LAusgangTableID.ToString() + "] erstellt..." + Environment.NewLine;

                            //Vita Ausgang
                            clsArtikelVita.AddAuslagerungAuto(this.BenutzerID, UB.LAusgang.LAusgangTableID, UB.LAusgang.LAusgangID);
                            //Vita Ausgang abgeschlossen
                            clsArtikelVita.LagerAusgangAutoChecked(this.BenutzerID, UB.LAusgang.LAusgangTableID, UB.LAusgang.LAusgangID);
                            clsLager Lager = new clsLager();
                            Lager.InitClass(this._GL_User, this._GL_System, this.sys);
                            Lager.Ausgang.LAusgangTableID = UB.LAusgang.LAusgangTableID;
                            Lager.Ausgang.FillAusgang();

                            //Lagermeldungen erstellen
                            clsASNTransfer AsnTransfer = new clsASNTransfer();
                            if (AsnTransfer.DoASNTransfer(this._GL_System, UB.LAusgang.AbBereichID, UB.LAusgang.MandantenID))
                            {
                                Lager.ASNAction = new clsASNAction();
                                Lager.ASNAction.InitClass(ref _GL_User);
                                Lager.ASNAction.ASNActionProcessNr = clsASNAction.const_ASNAction_Ausgang;
                                AsnTransfer.CreateLM(ref Lager);
                            }
                        }
                        //LEingangTableID
                        decTmp = 0;
                        decimal.TryParse(row["EingangTableID"].ToString(), out decTmp);
                        if (decTmp > 0)
                        {
                            UB.LEingang.LEingangTableID = decTmp;
                            UB.LEingang.FillEingang();
                            //Log
                            this.LogText = LogText + " > Eingang / ID: [" + UB.LAusgang.LAusgangID.ToString() + "/" + UB.LAusgang.LAusgangTableID.ToString() + "] erstellt..." + Environment.NewLine;

                            //Vita Einlagerung
                            clsArtikelVita.AddEinlagerungAuto(this.BenutzerID, UB.LEingang.LEingangTableID, UB.LEingang.LEingangID);
                            //Vita Ausgang abgeschlossen
                            clsArtikelVita.LagerEingangAutoChecked(this.BenutzerID, UB.LEingang.LEingangTableID, UB.LEingang.LEingangID);
                            clsLager Lager = new clsLager();
                            Lager.InitClass(this._GL_User, this._GL_System, this.sys);

                            //Lagermeldungen erstellen -> 12.12.2015 keine Eingangsmeldung wie im alten System
                            //clsASNTransfer AsnTransfer = new clsASNTransfer();                           
                            //if (AsnTransfer.DoASNTransfer(this._GL_System, UB.LAusgang.AbBereichID, UB.LAusgang.MandantenID))
                            //{
                            //    Lager.ASNAction = new clsASNAction();
                            //    Lager.ASNAction.InitClass(ref _GL_User);
                            //    Lager.ASNAction.ASNActionProcessNr = clsASNAction.const_ASNAction_Eingang;
                            //    AsnTransfer.CreateLM(ref Lager);
                            //}
                        }

                    }
                    //Vita Artikel
                    foreach (DataRow row in dtArtikel.Rows)
                    {
                        decimal decArtiID = 0;
                        Decimal.TryParse(row["ArtikelID"].ToString(), out decArtiID);
                        if (ArtikelID > 0)
                        {
                            clsArtikelVita.AddArtikelLAusgangAuto(this._GL_User, ArtikelID, UB.LAusgang.LAusgangID);
                        }
                    }
                }
                else
                {

                }
            }
            else
            {
                //Log
                this.LogText = this.LogText + " > ERROR -> keine Artikel zur Umbuchung vorhanden !!!" + Environment.NewLine;
            }

        }
        ///<summary>clsASNCall / Delete</summary>
        ///<remarks></remarks> 
        public bool Delete()
        {
            bool bReturn = false;
            string strSql = string.Empty;
            strSql = "Delete FROM Abrufe WHERE ID=" + this.ID + ";";
            bReturn = clsSQLcon.ExecuteSQL(strSql, this.BenutzerID);
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myCallId"></param>
        /// <param name="myGLUser"></param>
        /// <returns></returns>
        public bool DeactivateCall()
        {
            bool bReturn = false;
            string strSql = string.Empty;
            strSql = "Update Abrufe SET " +
                            "IsRead =1 " +
                            ",[Status]='" + clsASNCall.const_Status_deactivated + "'" +
                                    " WHERE ID=" + (Int32)this.ID + ";";
            bReturn = clsSQLcon.ExecuteSQL(strSql, this.BenutzerID);
            if (bReturn)
            {
                this.LogText = clsArtikelVita.Call_DeactivateCall(this._GL_User, this.ArtikelID, (int)this.ID, clsASNCall.const_Status_deactivated);
            }
            return bReturn;
        }

        /*********************************************************************************************************************
         *                              static procedure
         * ******************************************************************************************************************/
        ///<summary>clsASNCall / Delete</summary>
        ///<remarks></remarks> 
        public static bool UpdateAbrufeSetAbrufStatus(decimal myArtID, Globals._GL_USER myGLUser, string myAbrufStatus)
        {
            bool bReturn = false;
            string strSql = string.Empty;
            strSql = "Update Abrufe SET [Status]='" + myAbrufStatus + "' WHERE ArtikelID=" + (Int32)myArtID + ";";
            bReturn = clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
            if (bReturn)
            {


            }
            return bReturn;
        }

    }
}
