using LVS.Constants;
using System;
using System.Data;



namespace LVS
{
    public class clsUmbuchung
    {
        public Globals._GL_SYSTEM _GL_System;
        public Globals._GL_USER _GL_User;
        public clsSystem System;

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

        public clsArtikel Artikel;
        public clsLEingang LEingang;
        public clsLAusgang LAusgang;
        public DataTable dtUmbuchung = new DataTable();
        private string strSQLTrans = string.Empty;
        private bool bTransActionOK = false;

        private decimal _ArtikelID;
        private decimal _LEingangUB;
        private decimal _LVSNrNeu;
        private decimal _LAusgangID;
        private decimal _MandantenID;
        private decimal _AuftraggeberAltID;
        private decimal _AuftraggeberNeuID;
        private decimal _EmpfaengerID;
        private decimal _EntladestelleID;
        private DateTime _UBDate;           //Datum für den Ausgang und den neuen Eingang
        private decimal _NettoGesamt;
        private decimal _BruttoGesamt;
        private string _Lieferant;
        private decimal _LEingangTableIDNeu;
        private decimal _ArikelInfoUB;

        public decimal ArtikelID
        {
            get { return _ArtikelID; }
            set { _ArtikelID = value; }
        }
        public decimal LEingangUB
        {
            get { return _LEingangUB; }
            set { _LEingangUB = value; }
        }
        public decimal MandantenID
        {
            get { return _MandantenID; }
            set { _MandantenID = value; }
        }
        public decimal AuftraggeberAltID
        {
            get { return _AuftraggeberAltID; }
            set { _AuftraggeberAltID = value; }
        }
        public decimal AuftraggeberNeuID
        {
            get { return _AuftraggeberNeuID; }
            set { _AuftraggeberNeuID = value; }
        }
        public decimal EmpfaengerID
        {
            get { return _EmpfaengerID; }
            set { _EmpfaengerID = value; }
        }
        public decimal EntladestelleID
        {
            get { return _EntladestelleID; }
            set { _EntladestelleID = value; }
        }
        public decimal LAusgangID
        {
            get { return _LAusgangID; }
            set { _LAusgangID = value; }
        }
        public DateTime UBDate
        {
            get { return _UBDate; }
            set { _UBDate = value; }
        }
        public decimal NettoGesamt
        {
            get { return _NettoGesamt; }
            set { _NettoGesamt = value; }
        }
        public decimal BruttoGesamt
        {
            get { return _BruttoGesamt; }
            set { _BruttoGesamt = value; }
        }
        public string Lieferant
        {
            get { return _Lieferant; }
            set { _Lieferant = value; }
        }
        public decimal LEingangTableIDNeu
        {
            get { return _LEingangTableIDNeu; }
            set { _LEingangTableIDNeu = value; }
        }
        public decimal LVSNrNeu
        {
            get { return _LVSNrNeu; }
            set { _LVSNrNeu = value; }
        }
        public decimal ArikelInfoUB
        {
            get { return _ArikelInfoUB; }
            set { _ArikelInfoUB = value; }
        }
        public string LogText { get; set; }

        /*********************************************************************************************
         *                              Methoden
         * ******************************************************************************************/

        ///<summary>clsUmbuchung / InitUmbuchung</summary>
        ///<remarks>Folgende Funktionen werden ausgeführt:
        ///         </remarks>
        public void InitUmbuchung()
        {
            Artikel = new clsArtikel();
            Artikel._GL_User = this._GL_User;
            Artikel.AbBereichID = System.AbBereich.ID; //this._GL_System.sys_ArbeitsbereichID;

            LEingang = new clsLEingang();
            LEingang._GL_User = this._GL_User;
            LEingang.AbBereichID = System.AbBereich.ID;  //this._GL_System.sys_ArbeitsbereichID;

            if (this.ArtikelID > 0)
            {
                Artikel.ID = this.ArtikelID;
                Artikel.GetArtikeldatenByTableID();
                this.MandantenID = Artikel.MandantenID;

                LEingang.LEingangTableID = Artikel.LEingangTableID;
                LEingang.FillEingang();
                this.Lieferant = LEingang.Lieferant;
                this.AuftraggeberAltID = LEingang.Auftraggeber;
                this.AuftraggeberNeuID = 0;
                this.EmpfaengerID = LEingang.Empfaenger;

            }
            else
            {
                //Mandant wird im CtrUmbuchung bereits übergeben
                //this.LieferantenID = 0;
                //this.AuftraggeberAltID = this.;
                //this.AuftraggeberNeuID = 0;
                //this.EmpfaengerID =0;
                LEingang.MandantenID = this.MandantenID;
            }

            LEingangUB = clsLEingang.GetNewLEingangID(this._GL_User, this.System);
            LAusgangID = clsLAusgang.GetNewLAusgangID(this._GL_User, this.System);
            //Lagereingangsdaten alt für die aktuellen adressen            
        }
        ///<summary>clsUmbuchung / DoUmbuchungSQL</summary>
        ///<remarks></remarks>  
        public string DoUmbuchungSQL()
        {
            this.LogText = string.Empty;
            string strSQL = string.Empty;
            string strSQLEingang = string.Empty;
            string strSQLArtikel = string.Empty;
            string strSQLAusgang = string.Empty;

            if (this.dtUmbuchung.Rows.Count > 0)
            {
                LAusgang = new clsLAusgang();
                LAusgang.InitDefaultClsAusgang(this._GL_User, this.System);
                LAusgang.LAusgangsDate = DateTime.Now;
                LAusgang.Auftraggeber = this.AuftraggeberAltID;
                LAusgang.Empfaenger = this.AuftraggeberNeuID;
                LAusgang.Lieferant = clsADRVerweis.GetLieferantenVerweisBySenderAndReceiverAdr(LAusgang.Auftraggeber, LAusgang.Empfaenger, this.BenutzerID, constValue_AsnArt.const_Art_VDA4913, this.System.AbBereich.ID);
                //LAusgang.LfsDate = clsSystem.const_DefaultDateTimeValue_Min;
                LAusgang.MAT = LAusgang.LAusgangsDate.ToString();
                LAusgang.Checked = true;
                LAusgang.KFZ = string.Empty;
                LAusgang.Info = "autom. UB zu Lagereingang: ";

                strSQLAusgang += "DECLARE @LAusgangTableID decimal; ";
                strSQLAusgang += LAusgang.AddLAusgang_SQL();
                strSQLAusgang += "SET @LAusgangTableID=(Select @@IDENTITY); ";

                LEingang = new clsLEingang();
                LEingang.InitDefaultClsEingang(this._GL_User, this.System);
                //UBEingang.LEingangID = this.LEingangUB;
                //UBEingang.LEingangDate = this.UBDate;
                //UBEingang.LEingangLfsNr = "A" + ausgangsNr; => Update
                LEingang.Auftraggeber = this.AuftraggeberNeuID;
                LEingang.Empfaenger = this.EmpfaengerID;
                LEingang.Versender = this.AuftraggeberAltID;
                LEingang.Lieferant = clsADRVerweis.GetLieferantenVerweisBySenderAndReceiverAdr(LEingang.Auftraggeber, LEingang.Empfaenger, this.BenutzerID, constValue_AsnArt.const_Art_VDA4913, this.System.AbBereich.ID);
                //LEingang.IsPrintDoc = true;  ==> prüfen
                LEingang.Checked = true;


                strSQLEingang += "DECLARE @LEingangTableID decimal; ";
                //strSQLEingang += UBAusgang.AddLAusgang_SQL();
                strSQLEingang += LEingang.AddLagerEingangSQL();
                strSQLEingang += "SET @LEingangTableID=(Select @@IDENTITY); ";


                //Update der alten Artikeldaten 
                strSQLArtikel += "DECLARE @ArtID decimal; ";
                strSQLArtikel += "DECLARE @LvsID decimal; ";
                foreach (DataRow row in this.dtUmbuchung.Rows)
                {
                    decimal decArtID = 0;
                    Decimal.TryParse(row["ArtikelID"].ToString(), out decArtID);
                    if (decArtID > 0)
                    {
                        strSQLArtikel += " Update Artikel SET " +
                                                        "LAusgangTableID=@LAusgangTableID " +
                                                        ", CheckArt=1 " +
                                                        ", LA_Checked=1 " +
                                                        ", BKZ=0 " +
                                                        ", UB=1 " +
                                                        " WHERE ID=" + (Int32)decArtID + " ;";

                        //Abruf austragen
                        strSQLArtikel += " Update Abrufe SET " +
                                                        "IsRead=1 " +
                                                        ", Status='" + clsASNCall.const_Status_bearbeitet.ToString() + "'" +
                                                        " WHERE ArtikelID=" + (Int32)decArtID + " ;";
                        this.Artikel = new clsArtikel();
                        this.Artikel.InitClass(this._GL_User, this._GL_System);
                        this.Artikel.ID = decArtID;
                        this.Artikel.GetArtikeldatenByTableID();
                        this.Artikel.ArtIDAlt = decArtID;
                        this.Artikel.LVSNrVorUB = this.Artikel.LVS_ID;
                        this.Artikel.interneInfo = "UB aus Eingang: " + Artikel.Eingang.LEingangID.ToString() + " vom " + Artikel.Eingang.LEingangDate.ToShortDateString();
                        this.Artikel.AusgangChecked = false;
                        this.Artikel.LAusgangTableID = 0;
                        this.Artikel.EingangChecked = true;
                        this.Artikel.ID = 0;

                        //Artikel wird neu anlgelegt
                        strSQLArtikel += this.Artikel.AddArtikelLager_SQL(true, true);
                        //neue Artikel ID wird zugewiesen
                        strSQLArtikel += " SET @ArtID=(Select @@IDENTITY); ";
                        //neue LVSNR ermittelt
                        strSQLArtikel += " SET @LvsID=(SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + this.System.AbBereich.MandantenID + ") +1; ";
                        //neue LVSNR / EIngangtableID dem neuen Artikel zugewiesen
                        strSQLArtikel += " Update Artikel SET " +
                                                        "LVS_ID=@LvsID " +
                                                        ", LEingangTableID=@LEingangTableID " +
                                                        ", CheckArt=1" +
                                                        ", LA_Checked=0 " +
                                                        ", LAusgangTableID=0 " +
                                                        ", BKZ=1 " +
                                                        ", UB=0 " +
                                                        " WHERE ID = @ArtID; ";
                        strSQLArtikel += " UPDATE PrimeKeys SET LvsNr = @LvsID WHERE Mandanten_ID=" + this.System.AbBereich.MandantenID + ";";

                        //Lieferant 
                        //this.LEingang.Lieferant = this.Artikel.Eingang.Lieferant;
                    }
                }
            }

            strSQLArtikel += " Update LEingang SET " +
                                    "[Check]=1 " +
                                    ",Lieferant='" + this.LEingang.Lieferant.Trim() + "'" +
                                    ",LfsNr='A'+ CAST((SELECT LAusgangID FROM LAusgang WHERE ID =@LAusgangTableID) as nvarchar) " +
                                    " WHERE ID= @LEingangTableID ;";
            strSQLArtikel += " Update LAusgang SET " +
                                                "Info='autom. UB zu Lagereingang: '+ CAST((SELECT LEingangID FROM LEingang WHERE ID =@LEingangTableID) as nvarchar) " +
                                                ",LfsNr='A'+ CAST( LAusgangID as nvarchar) " +
                                                ",SLB= LAusgangID " +
                                                ",Netto = (SELECT SUM(Netto) FROM Artikel WHERE LAusgangTableID=@LAusgangTableID) " +
                                                ",Brutto =  (SELECT SUM(Brutto) FROM Artikel WHERE LAusgangTableID=@LAusgangTableID) " +
                                                " WHERE ID = @LAusgangTableID ;";
            strSQL = strSQLAusgang + strSQLEingang + strSQLArtikel + " SELECT @LEingangTableID as EingangTableID, @LAusgangTableID as AusgangTableID";
            return strSQL;
        }
        ///<summary>clsUmbuchung / ctrUmbuchung_Load</summary>
        ///<remarks>Folgende Funktionen werden ausgeführt:
        ///         </remarks>  
        public bool DoUmbuchung()
        {
            //Ausgang erstellen
            decimal ausgangsnummer = CreateLAusgang();
            if (bTransActionOK)
            {
                UpdateArtikelForLAusgang();
                if (bTransActionOK)
                {
                    //neuer Eingang wird ertellt
                    CreateLEingang(ausgangsnummer);
                    if (bTransActionOK)
                    {
                        //Artikeldaten neue gespeichert
                        InsertArtikelForNewEingang();
                    }
                }
            }
            if (bTransActionOK)
            {
                clsMessages.UB_Ok();
            }
            else
            {
                clsMessages.UB_Failed();
            }
            return bTransActionOK;
        }
        ///<summary>clsUmbuchung / ctrUmbuchung_Load</summary>
        ///<remarks>Lagerausgang wird erstellt</remarks>  
        private decimal CreateLAusgang()
        {
            LAusgang = new clsLAusgang();
            LAusgang.Sys = this.System;
            LAusgang._GL_User = this._GL_User;
            LAusgang.AbBereichID = this.System.AbBereich.ID;
            LAusgang.MandantenID = this.System.AbBereich.MandantenID;

            //Ausgangsdaten zuweisen
            LAusgang.LAusgangID = this.LAusgangID;

            LAusgang.LAusgangsDate = this.UBDate;
            LAusgang.GewichtNetto = this.NettoGesamt;
            LAusgang.GewichtBrutto = this._BruttoGesamt;

            LAusgang.Auftraggeber = this.AuftraggeberAltID;
            LAusgang.Empfaenger = this.AuftraggeberNeuID;
            LAusgang.Versender = this.AuftraggeberAltID;
            LAusgang.Entladestelle = 0;
            LAusgang.Lieferant = clsADRVerweis.GetLieferantenVerweisBySenderAndReceiverAdr(LAusgang.Auftraggeber, LAusgang.Empfaenger, this.BenutzerID, constValue_AsnArt.const_Art_VDA4913, this._GL_System.sys_ArbeitsbereichID);
            //LAusgang.Lieferant = string.Empty;
            //LAusgang.LfsDate = clsSystem.const_DefaultDateTimeValue_Min;
            LAusgang.SLB = LAusgang.LAusgangID;
            LAusgang.MAT = string.Empty;
            LAusgang.Checked = true;
            LAusgang.SpedID = 0;
            LAusgang.KFZ = string.Empty;
            LAusgang.Info = "UB zu Lagereingang: " + this.LEingangUB.ToString();
            LAusgang.Termin = Globals.DefaultDateTimeMaxValue;
            LAusgang.IsPrintDoc = true;
            //Eintrag in DB
            bTransActionOK = LAusgang.AddLAusgang();
            //In der clsLAusgang wird ein Ausgang immer mit Checked=false angelegt
            //deshalb jetzt hier noch einmal ein update, da bei der Umbuchung der Ausgang nicht mehr 
            //veränderbar sein soll
            LAusgang.Checked = true;
            LAusgang.UpdateLagerAusgang();
            return LAusgang.LAusgangID;
        }
        ///<summary>clsUmbuchung / UpdateArtikelForLAusgang</summary>
        ///<remarks>Alle Datensätze in der Table Umbuchung müssen upgedatet werden.</remarks>  
        private void UpdateArtikelForLAusgang()
        {
            if (dtUmbuchung.Rows.Count > 0)
            {
                Artikel = new clsArtikel();
                Artikel.sys = this.System;
                Artikel.MandantenID = this.System.AbBereich.MandantenID;
                Artikel.AbBereichID = this.System.AbBereich.ID;
                Artikel._GL_User = this._GL_User;
                for (Int32 i = 0; i <= dtUmbuchung.Rows.Count - 1; i++)
                {
                    string strTmp = dtUmbuchung.Rows[i]["ArtikelID"].ToString();
                    decimal decTmp = 0;
                    if (decimal.TryParse(strTmp, out decTmp))
                    {
                        Artikel.ID = decTmp;
                        Artikel.GetArtikeldatenByTableID();
                        //LA Checked
                        Artikel.AusgangChecked = true;
                        //als umgebuchten Artikel markieren
                        Artikel.Umbuchung = true;
                        Artikel.BKZ = 0; // ausgebucht
                        Artikel.LAusgangTableID = this.LAusgang.LAusgangTableID;
                        Artikel.UB_AltCalcEinlagerung = (bool)dtUmbuchung.Rows[i]["UB_AltCalcEinlagerung"];
                        Artikel.UB_AltCalcAuslagerung = (bool)dtUmbuchung.Rows[i]["UB_AltCalcAuslagerung"];
                        Artikel.UB_AltCalcLagergeld = (bool)dtUmbuchung.Rows[i]["UB_AltCalcLagergeld"];
                        Artikel.UB_NeuCalcEinlagerung = (bool)dtUmbuchung.Rows[i]["UB_NeuCalcEinlagerung"];
                        Artikel.UB_NeuCalcAuslagerung = (bool)dtUmbuchung.Rows[i]["UB_NeuCalcAuslagerung"];
                        Artikel.UB_NeuCalcLagergeld = (bool)dtUmbuchung.Rows[i]["UB_NeuCalcLagergeld"];
                        if (Artikel.LZZ < clsSystem.const_DefaultDateTimeValue_Min)
                            Artikel.LZZ = clsSystem.const_DefaultDateTimeValue_Min;

                        Artikel.UpdateArtikelLager();
                    }
                }
            }
        }
        ///<summary>clsUmbuchung / CreateLEingang</summary>
        ///<remarks>Lagereingang wird erstellt</remarks>  
        private void CreateLEingang(decimal ausgangsNr)
        {
            LEingang = new clsLEingang();
            LEingang.sys = this.System;
            LEingang._GL_User = this._GL_User;
            LEingang.AbBereichID = this.System.AbBereich.ID;
            LEingang.MandantenID = this.System.AbBereich.MandantenID;

            LEingang.Auftraggeber = this.AuftraggeberNeuID;
            LEingang.Empfaenger = this.EmpfaengerID;

            LEingang.LEingangID = this.LEingangUB;
            LEingang.LEingangDate = this.UBDate;
            LEingang.LEingangLfsNr = "A" + ausgangsNr;

            LEingang.Versender = this.AuftraggeberAltID;
            LEingang.Lieferant = this.System.Client.ctrUmbuchung_Customize_SetLieferantenNr(ref this.System, this);
            LEingang.SpedID = 0;
            LEingang.KFZ = string.Empty;
            LEingang.WaggonNr = string.Empty;
            LEingang.IsPrintDoc = true;
            LEingang.Checked = true;
            LEingang.Verlagerung = false;
            LEingang.Umbuchung = true;
            bTransActionOK = LEingang.AddLagerEingang();
            this.LEingangTableIDNeu = LEingang.LEingangTableID;
            clsLager.UpdateLEingangCheck(_GL_User.User_ID, true, this.LEingangTableIDNeu);
        }
        ///<summary>clsUmbuchung / InsertArtikelForNewEingang</summary>
        ///<remarks>Alle Datensätze müssen neu in Artikel eingetragen werden.</remarks>  
        private void InsertArtikelForNewEingang()
        {
            if (dtUmbuchung.Rows.Count > 0)
            {
                //Artikel Info erstellen
                clsLEingang einAlt = new clsLEingang();
                einAlt.sys = this.System;
                einAlt._GL_User = this._GL_User;
                einAlt.AbBereichID = this.System.AbBereich.ID;
                einAlt.MandantenID = this.System.AbBereich.MandantenID;
                einAlt.LEingangTableID = Artikel.LEingangTableID;
                einAlt.FillEingang();
                string ArtInfo = "UB aus Eingang: " + einAlt.LEingangID.ToString() + " vom " + einAlt.LEingangDate.ToShortDateString();



                Artikel = new clsArtikel();
                Artikel.sys = this.System;
                Artikel.AbBereichID = this.System.AbBereich.ID;
                Artikel.MandantenID = this.System.AbBereich.MandantenID;
                Artikel._GL_User = this._GL_User;
                for (Int32 i = 0; i <= dtUmbuchung.Rows.Count - 1; i++)
                {
                    string strTmp = dtUmbuchung.Rows[i]["ArtikelID"].ToString();
                    decimal decTmp = 0;
                    if (decimal.TryParse(strTmp, out decTmp))
                    {
                        //Artikel mit der ID laden, damit die Artikeldaten übernommen werden können
                        Artikel.ID = decTmp;

                        Artikel.GetArtikeldatenByTableID();
                        Artikel.LVSNrVorUB = Artikel.LVS_ID;
                        //zu ändernde Artikeldaten
                        Artikel.ID = 0;
                        Artikel.LVS_ID = Artikel.GetNewLVSNr();
                        Artikel.ArtIDAlt = decTmp;
                        Artikel.AusgangChecked = false;
                        Artikel.EingangChecked = true;

                        Artikel.Umbuchung = false; //true bei dem umgebuchten Artikel
                        Artikel.BKZ = 1;   // im Bestand
                        Artikel.LAusgangTableID = 0;

                        if (this.System.Client.Modul.Lager_UB_ArikelProduktionsnummerChange)
                        {
                            Artikel.Produktionsnummer = Artikel.Produktionsnummer + "U";
                        }
                        Artikel.Info = ArtInfo;
                        //muss nach dem Erstellen der Artikel info kommen, da sonst LEingangsTableID falsch ist
                        Artikel.LEingangTableID = this.LEingangTableIDNeu;

                        //Eintrag in DB
                        Artikel.AddArtikelLager_UB();

                        clsSchaeden schaeden = new clsSchaeden();
                        schaeden.ArtikelID = Artikel.ArtIDAlt;
                        DataTable dtSchaeden = schaeden.GetArtikelSchäden(true);

                        schaeden.ArtikelID = Artikel.ID;
                        schaeden.AddSchadenZuweisung(dtSchaeden, true);

                        //sperrlager eintrag
                        clsSPL splager = new clsSPL();
                        splager.ArtikelID = Artikel.ArtIDAlt;
                        splager.FillLastINByArtikelID();
                        if (splager.CheckArtikelInSPL())
                        {
                            splager.DoSPLUmbuchung(Artikel.ID, Artikel.LEingangTableID);
                        }

                        //Kostenzuweisung für den neuen Artikel
                        Artikel.UB_AltCalcEinlagerung = (bool)dtUmbuchung.Rows[i]["UB_AltCalcEinlagerung"];
                        Artikel.UB_AltCalcAuslagerung = (bool)dtUmbuchung.Rows[i]["UB_AltCalcAuslagerung"];
                        Artikel.UB_AltCalcLagergeld = (bool)dtUmbuchung.Rows[i]["UB_AltCalcLagergeld"];
                        Artikel.UB_NeuCalcEinlagerung = (bool)dtUmbuchung.Rows[i]["UB_NeuCalcEinlagerung"];
                        Artikel.UB_NeuCalcAuslagerung = (bool)dtUmbuchung.Rows[i]["UB_NeuCalcAuslagerung"];
                        Artikel.UB_NeuCalcLagergeld = (bool)dtUmbuchung.Rows[i]["UB_NeuCalcLagergeld"];
                        Artikel.UpdateArtikelUBCostAssignment();
                        //Die neuen Daten in die Umbuchungstabelle schreiben
                        InsertIntoUmbuchung();


                    }
                }
            }
        }
        ///<summary>clsUmbuchung / InsertIntoUmbuchung</summary>
        ///<remarks></remarks> 
        private bool InsertIntoUmbuchung()
        {
            string strSql = "INSERT INTO [dbo].[Umbuchungen] " +
                    "([ArtIDAlt] " +
                    ",[ArtIDNeu] " +
                    ",[UB_AltCalcEinlagerung] " +
                    ",[UB_AltCalcAuslagerung] " +
                    ",[UB_AltCalcLagergeld] " +
                    ",[UB_NeuCalcEinlagerung] " +
                    ",[UB_NeuCalcAuslagerung] " +
                    ",[UB_NeuCalcLagergeld])	 " +
                    "VALUES( " +
                    Artikel.ArtIDAlt + ", " +
                    Artikel.ID + ", " +
                    Convert.ToInt32(Artikel.UB_AltCalcEinlagerung) + ", " +
                    Convert.ToInt32(Artikel.UB_AltCalcAuslagerung) + ", " +
                    Convert.ToInt32(Artikel.UB_AltCalcLagergeld) + ", " +
                    Convert.ToInt32(Artikel.UB_NeuCalcEinlagerung) + ", " +
                    Convert.ToInt32(Artikel.UB_NeuCalcAuslagerung) + ", " +
                    Convert.ToInt32(Artikel.UB_NeuCalcLagergeld) +
                    ");";
            bool bExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            return bExecOK;
        }
        /// <summary>
        /// clsUmbuchung / isArtikelUBNew
        /// </summary>
        /// <param name="decArtID"></param>
        /// <returns></returns>
        public static bool isArtikelUBNew(decimal decArtID, decimal decBenutzerID, string strKat = "")
        {
            string sql = string.Empty;
            sql = "Select * from Umbuchungen where ArtIdNeu=" + decArtID;
            DataTable tmpDT = clsSQLcon.ExecuteSQL_GetDataTable(sql, decBenutzerID, "Temp");
            bool bReturn = false;
            if (tmpDT.Rows.Count > 0)
            {

                switch (strKat)
                {
                    case "":
                        bReturn = true;
                        break;
                    case "Einlagerung":
                        bReturn = (bool)tmpDT.Rows[0]["UB_NeuCalcEinlagerung"];
                        break;
                    case "Auslagerung":
                        bReturn = (bool)tmpDT.Rows[0]["UB_NeuCalcAuslagerung"];
                        break;
                    case "Lagergeld":
                        bReturn = (bool)tmpDT.Rows[0]["UB_neuCalcLagergeld"];
                        break;
                }
            }
            return bReturn;
        }
        /// <summary>
        /// clsUmbuchung / isArtikelUBNew
        /// </summary>
        /// <param name="decArtID"></param>
        /// <returns></returns>
        public static bool isArtikelUBAlt(decimal decArtID, decimal decBenutzerID, string strKat = "")
        {
            string sql = string.Empty;
            sql = "Select * from Umbuchungen where ArtIdAlt=" + decArtID;
            DataTable tmpDT = clsSQLcon.ExecuteSQL_GetDataTable(sql, decBenutzerID, "Temp");
            bool bReturn = false;
            if (tmpDT.Rows.Count > 0)
            {

                switch (strKat)
                {
                    case "":
                        bReturn = true;
                        break;
                    case "Einlagerung":
                        bReturn = (bool)tmpDT.Rows[0]["UB_AltCalcEinlagerung"];
                        break;
                    case "Auslagerung":
                        bReturn = (bool)tmpDT.Rows[0]["UB_AltCalcAuslagerung"];
                        break;
                    case "Lagergeld":
                        bReturn = (bool)tmpDT.Rows[0]["UB_AltCalcLagergeld"];
                        break;
                }
            }
            return bReturn;
        }
        /// <summary>
        /// clsUmbuchung / getArtikelUBNew
        /// </summary>
        /// <param name="decArtID"></param>
        /// <returns></returns>
        public static decimal getArtikelUBAlt(decimal decArtID, decimal decBenutzerID)
        {
            string sql = string.Empty;
            sql = "Select * from Umbuchungen where ArtIdNeu=" + decArtID;
            DataTable tmpDT = clsSQLcon.ExecuteSQL_GetDataTable(sql, decBenutzerID, "Temp");
            decimal decTmp = 0;


            if (tmpDT.Rows.Count > 0)
            {
                decimal.TryParse(tmpDT.Rows[0]["ArtIDAlt"].ToString(), out decTmp);
            }
            return decTmp;
        }
        /// <summary>
        /// clsUmbuchung / getArtikelUBAlt
        /// </summary>
        /// <param name="decArtID"></param>
        /// <returns></returns>
        public static decimal getArtikelUBNew(decimal decArtID, decimal decBenutzerID)
        {
            string sql = string.Empty;
            sql = "Select * from Umbuchungen where ArtIdAlt=" + decArtID;
            DataTable tmpDT = clsSQLcon.ExecuteSQL_GetDataTable(sql, decBenutzerID, "Temp");
            decimal decTmp = 0;


            if (tmpDT.Rows.Count > 0)
            {
                decimal.TryParse(tmpDT.Rows[0]["ArtIDNeu"].ToString(), out decTmp);
            }
            return decTmp;
        }
    }

}

