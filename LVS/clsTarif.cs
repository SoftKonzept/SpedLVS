using System;
using System.Data;
using System.Data.SqlClient;

namespace LVS
{
    public class clsTarif
    {
        //TEilabrechnungsarten:
        //1. Einlagerungskosten
        //2. Auslagerungskosten
        //3. Lagerkosten
        //4. Sperrlagerkosten
        //5. Transportkosten
        //6. Direktanlieferung
        //7. Rücklieferung
        //8. Vorfracht
        //9. Nebenkosten
        //10. Gleisstellgebühr
        //11. Maut
        internal Int32 iCountTeilAbrechnungsArten = 11;

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
        //************************************

        public clsTarifPosition TarifPosition = new clsTarifPosition();
        public clsKundenTarife KundenTarif = new clsKundenTarife();
        public clsTarifGArtZuweisung TarifGArtZuweisung = new clsTarifGArtZuweisung();
        public clsArbeitsbereichTarif AbBereichTarif = new clsArbeitsbereichTarif();
        public clsArbeitsbereiche AbBereich = new clsArbeitsbereiche();
        internal DataTable dtTarifPositionen = new DataTable("TarifPositionen");


        public decimal ID { get; set; }
        public string Tarifname { get; set; }
        public string Beschreibung { get; set; }
        public string Art { get; set; }
        public bool aktiv { get; set; }
        private bool _ExistTarif;
        public bool ExistTarif
        {
            get
            {
                string strSQL = string.Empty;
                strSQL = "Select ID FROM TarifPositionen WHERE ID=" + ID + ";";
                _ExistTarif = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
                return _ExistTarif;
            }
            set { _ExistTarif = value; }
        }
        public decimal AdrID { get; set; }

        public bool CalcSPLKosten { get; set; }
        public bool CalcEinlagerunskosten { get; set; }
        public bool CalcAuslagerungskosten { get; set; }
        public bool CalcTransportkosten { get; set; }
        public bool CalcLagerkosten { get; set; }
        public bool CalcRLKosten { get; set; }
        public bool CalcDirectDeliveryKosten { get; set; }
        public bool CalcVorfracht { get; set; }

        public bool CalcToll { get; set; }  //Maut
        public bool LagerBestandIncSPL { get; set; }
        public DataTable dtTarifpositionen { get; set; }
        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }
        public decimal parentID { get; set; }
        public bool ArtEinzelAbrechnung { get; set; }
        public bool ExistLinkedGArt { get; set; }
        public bool CalcNebenkosten { get; set; }
        public Int32 Modus { get; set; }
        public enumCalcultationModus enumModus
        {
            get
            {
                enumCalcultationModus tmpEnum = enumCalcultationModus.Standard;
                Enum.TryParse(this.Modus.ToString(), out tmpEnum);
                return tmpEnum;
            }
        }
        public decimal VersPreis { get; set; }
        public bool CalcGleis { get; set; }
        public bool ISVersPauschal { get; set; }
        public decimal VersMaterialWert { get; set; }
        public Int32 Zahlungsziel { get; set; }
        public string ZZText { get; set; }
        public string ZZTextEdit { get; set; }
        public string RGText { get; set; }
        /************************************************************************
        *                        Methoden Table Tarif
        * *********************************************************************/
        ///<summary>clsTarif / InitClass</summary>
        ///<remarks></remarks>  
        public void InitClass(Globals._GL_USER GLUser, decimal myDecTarifID, decimal myAdrID)
        {
            this._GL_User = GLUser;
            this.ID = myDecTarifID;
            this.AdrID = myAdrID;
            Fill();
        }
        ///<summary>clsTarif / InitSubClasses</summary>
        ///<remarks></remarks>  
        private void InitSubClasses()
        {
            TarifPosition = new clsTarifPosition();
            TarifPosition._GL_User = this._GL_User;
            TarifPosition.TarifID = this.ID;
            TarifPosition.FillByTarifID();

            //Baustelle Check KundenTarif
            //KundenTarif = new clsKundenTarife();

            TarifGArtZuweisung = new clsTarifGArtZuweisung();
            TarifGArtZuweisung._GL_User = this._GL_User;
            TarifGArtZuweisung.TarifID = this.ID;
            TarifGArtZuweisung.GetTarifGArten();
        }
        ///<summary>clsTarif / AddTarif</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB. Jedoch muss bei diesem
        ///         Eintrag folgende Einträge zusätzlich über eine Transaction gemacht werden:
        ///         - Defaultdatensatz in Table TarifPositionen
        ///         - Eintrag in Table KundenTarife</remarks>
        public void AddTarif()
        {
            string strSql = string.Empty;
            //Eintrag in Table Tarife
            strSql = "INSERT INTO Tarife (Tarifname, Beschreibung, aktiv, Art, CalcEingangskosten, CalcAusgangskosten," +
                                         "CalcLagerkosten, CalcSPLKosten, CalcTransportkosten, CalcDirectDeliveryKosten, " +
                                         "CalcRLKosten, LagerBestandIncSPL, CalcVorfracht, Von, Bis, parentID, ArtEinzelAbrechnung, " +
                                         "CalcNebenkosten, Modus, VersPreis, CalcGleis, IsVersPauschal,VersMaterialWert, " +
                                         "Zahlungsziel, ZZText, ZZTextEdit, RGText, CalcToll" +
                                          ") " +
                                           "VALUES ('" + Tarifname.ToString().Trim() + "'" +
                                                    ",'" + Beschreibung.ToString().Trim() + "'" +
                                                    "," + Convert.ToInt32(aktiv) +
                                                    ", '" + Art.ToString().Trim() + "'" +
                                                    "," + Convert.ToInt32(CalcEinlagerunskosten) +
                                                    "," + Convert.ToInt32(CalcAuslagerungskosten) +
                                                    "," + Convert.ToInt32(CalcLagerkosten) +
                                                    "," + Convert.ToInt32(CalcSPLKosten) +
                                                    "," + Convert.ToInt32(CalcTransportkosten) +
                                                    "," + Convert.ToInt32(CalcDirectDeliveryKosten) +
                                                    "," + Convert.ToInt32(CalcRLKosten) +
                                                    "," + Convert.ToInt32(LagerBestandIncSPL) +
                                                    "," + Convert.ToInt32(CalcVorfracht) +
                                                    ",'" + Von + "'" +
                                                    ",'" + Bis + "'" +
                                                    ", " + Convert.ToInt32(parentID) +
                                                    ", " + Convert.ToInt32(ArtEinzelAbrechnung) +
                                                    ", " + Convert.ToInt32(CalcNebenkosten) +
                                                    ", " + Modus +
                                                    ", '" + VersPreis.ToString().Replace(",", ".") + "'" +
                                                    ", " + Convert.ToInt32(CalcGleis) +
                                                    ", " + Convert.ToInt32(ISVersPauschal) +
                                                    ", '" + VersMaterialWert.ToString().Replace(",", ".") + "'" +
                                                    ", " + this.Zahlungsziel +
                                                    ", '" + this.ZZText + "'" +
                                                    ", '" + this.ZZTextEdit + "'" +
                                                    ", '" + this.RGText + "'" +
                                                    "," + Convert.ToInt32(this.CalcToll) +
                                                    "); ";

            //ID aus dem vorhergegangenen Eintrag in Table Tarife muss ermittelt werden
            strSql = strSql +
                     "INSERT INTO KundenTarife (TarifID, AdrID) " +
                                                "VALUES ((SELECT IDENT_CURRENT('Tarife')), '" + Convert.ToInt32(AdrID) + "'); ";

            //Eigenschaftswerte für Default-Eintrag setzen
            TarifPosition = new clsTarifPosition();
            TarifPosition.BasisEinheit = "KG";
            TarifPosition.AbrEinheit = "KG";
            TarifPosition.Lagerdauer = 0;
            Int32 iZeitbez = 0;
            TarifPosition.PreisEinheit = 0.00M;
            TarifPosition.EinheitenVon = 0;
            TarifPosition.EinheitenBis = 0;
            TarifPosition.MargeProzentEinheit = 0;
            TarifPosition.MargePreisEinheit = 0;

            //Eintrag in Table TarifPositionen
            //Für jede Teilabrechnungsart muss ein 
            //Ist Art = Lager dann müssen jeweils ein Datensatz für Einlagerung, Auslagerung und Lagergeld 
            //hinzugefügt werden
            bool bAktiv = false;

            if (Art.ToString().Trim() == enumTarifArt.Lager.ToString())
            {
                //Die Schleife wird für alle Teilabrechnungsarten durchlaufen
                //wurde die entsprechende Abrechnungsart ausgewählt, so wird der entsprechende
                //Teilabrechnungsdatensatz gespeichert
                for (Int32 i = 0; i <= iCountTeilAbrechnungsArten - 1; i++)
                {
                    bAktiv = false;
                    switch (i)
                    {
                        case 0:
                            TarifPosition.TarifPosArt = enumTarifArtLager.Einlagerungskosten.ToString().Trim();
                            bAktiv = CalcEinlagerunskosten;
                            break;
                        case 1:
                            TarifPosition.TarifPosArt = enumTarifArtLager.Lagerkosten.ToString().Trim();
                            bAktiv = CalcLagerkosten;
                            break;
                        case 2:
                            TarifPosition.TarifPosArt = enumTarifArtLager.Auslagerungskosten.ToString().Trim();
                            bAktiv = CalcAuslagerungskosten;
                            break;
                        case 3:
                            TarifPosition.TarifPosArt = enumTarifArtLager.LagerTransportkosten.ToString().Trim();
                            bAktiv = CalcTransportkosten;
                            break;
                        case 4:
                            TarifPosition.TarifPosArt = enumTarifArtLager.Sperrlagerkosten.ToString().Trim();
                            bAktiv = CalcSPLKosten;
                            break;
                        case 5:
                            TarifPosition.TarifPosArt = enumTarifArtLager.Direktanlieferung.ToString().Trim();
                            bAktiv = CalcDirectDeliveryKosten;
                            break;
                        case 6:
                            TarifPosition.TarifPosArt = enumTarifArtLager.Rücklieferung.ToString().Trim();
                            bAktiv = CalcRLKosten;
                            break;
                        case 7:
                            TarifPosition.TarifPosArt = enumTarifArtLager.Vorfracht.ToString().Trim();
                            bAktiv = CalcVorfracht;
                            break;
                        case 8:
                            TarifPosition.TarifPosArt = enumTarifArtLager.Nebenkosten.ToString().Trim();
                            bAktiv = CalcNebenkosten;
                            break;
                        case 9:
                            TarifPosition.TarifPosArt = enumTarifArtLager.Gleisstellgebühr.ToString().Trim();
                            bAktiv = CalcGleis;
                            break;
                        case 10:
                            TarifPosition.TarifPosArt = enumTarifArtLager.Maut.ToString().Trim();
                            bAktiv = CalcGleis;
                            break;
                    }
                    //switch
                    TarifPosition.SortIndex = TarifPosition.SortIndex;
                    TarifPosition.aktiv = bAktiv;
                    TarifPosition.MasterPos = true;
                    TarifPosition.StaffelPos = false;
                    TarifPosition.OrderID = 1;
                    TarifPosition.DatenfeldArtikel = "Brutto";
                    TarifPosition.Beschreibung = "Standard";
                    TarifPosition.TEinheiten = string.Empty;

                    strSql = strSql +
                    "INSERT INTO TarifPositionen (TarifID, BasisEinheit, AbrEinheit, Lagerdauer, Zeitraumbezogen, " +
                                           "PreisEinheit, EinheitVon, EinheitBis, MargeProzentEinheit, MargePreisEinheit," +
                                          "TarifPosArt, aktiv, MasterPos, StaffelPos, OrderID, DatenfeldArtikel, Beschreibung, " +
                                          "TEinheiten, SortIndex) " +
                       "VALUES ((SELECT IDENT_CURRENT('Tarife'))" +
                                   ",'" + TarifPosition.BasisEinheit + "'" +
                                   ",'" + TarifPosition.AbrEinheit + "'" +
                                   "," + TarifPosition.Lagerdauer +
                                   "," + iZeitbez +
                                   ",'" + TarifPosition.PreisEinheit.ToString().Replace(",", ".") + "'" +
                                   "," + TarifPosition.EinheitenVon +
                                   "," + TarifPosition.EinheitenBis +
                                   ",'" + TarifPosition.MargeProzentEinheit.ToString().Replace(",", ".") + "'" +
                                   ",'" + TarifPosition.MargePreisEinheit.ToString().Replace(",", ".") + "'" +
                                   ",'" + TarifPosition.TarifPosArt.ToString().Trim() + "'" +
                                   "," + Convert.ToInt32(TarifPosition.aktiv) +
                                   "," + Convert.ToInt32(TarifPosition.MasterPos) +
                                   "," + Convert.ToInt32(TarifPosition.StaffelPos) +
                                   "," + TarifPosition.OrderID +
                                   ",'" + TarifPosition.DatenfeldArtikel + "'" +
                                   ",'" + TarifPosition.Beschreibung + "'" +
                                   ",'" + TarifPosition.TEinheiten + "'" +
                                   "," + TarifPosition.SortIndex +
                                   "); ";
                }//for
            }
            else
            {
                //muss später bei Anpassung spediton bearbeitet werden
                TarifPosition.aktiv = true;
                TarifPosition.MasterPos = true;
                TarifPosition.StaffelPos = false;
                TarifPosition.OrderID = 1;
                TarifPosition.TarifPosArt = string.Empty;
                strSql = strSql +
                    "INSERT INTO TarifPositionen (TarifID, BasisEinheit, AbrEinheit, Lagerdauer, Zeitraumbezogen, " +
                                           "PreisEinheit, EinheitVon, EinheitBis, MargeProzentEinheit, MargePreisEinheit," +
                                          "TarifPosArt, aktiv) " +
                       "VALUES ((SELECT IDENT_CURRENT('Tarife'))" +
                                   ",'" + TarifPosition.BasisEinheit + "'" +
                                   ",'" + TarifPosition.AbrEinheit + "'" +
                                   "," + TarifPosition.Lagerdauer +
                                   "," + iZeitbez +
                                   ",'" + TarifPosition.PreisEinheit.ToString().Replace(",", ".") + "'" +
                                   "," + TarifPosition.EinheitenVon +
                                   "," + TarifPosition.EinheitenBis +
                                   ",'" + TarifPosition.MargeProzentEinheit.ToString().Replace(",", ".") + "'" +
                                   ",'" + TarifPosition.MargePreisEinheit.ToString().Replace(",", ".") + "'" +
                                   ",'" + TarifPosition.TarifPosArt.ToString().Trim() + "'" +
                                   "," + Convert.ToInt32(bAktiv) +
                                   "); ";
            }

            clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "TarifInsert", BenutzerID);
            GetTarifID();

            //Add Logbucheintrag Exception
            string myBeschreibung = "Tarif angelegt: " + Tarifname + " für Adress ID :" + AdrID;
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);
        }

        public void UpdateTarifePauschalSLVS()
        {
            string strSql = "Update Tarife SET " +
                                       " VersPreis=" + VersPreis.ToString().Replace(",", ".") +
                                       ", IsVersPauschal=" + Convert.ToInt32(ISVersPauschal) +
                                       ", VersMaterialWert=" + VersPreis.ToString().Replace(",", ".") +
                                                " WHERE ID in(select TarifID from KundenTarife where AdrId=" + AdrID + ");";
            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
                string myBeschreibung = "Tarife für Kunde geändert: Kunden ID:" + AdrID.ToString();
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), myBeschreibung);
            }
        }
        ///<summary>clsTarif / UpdateTarif</summary>
        ///<remarks>Ändern des Tarifdatensatzes.</remarks>     
        public void UpdateTarif()
        {
            //Tarifdaten vor dem Update, damit nach dem Update die Tarifpositionen angepasst werden können
            clsTarif TarifVorUpdate = new clsTarif();
            TarifVorUpdate._GL_User = this._GL_User;
            TarifVorUpdate.ID = this.ID;
            TarifVorUpdate.Fill();

            string strSql = string.Empty;
            strSql = "Update Tarife SET Tarifname ='" + Tarifname + "'" +
                                                ", Beschreibung ='" + Beschreibung + "'" +
                                                ", aktiv =" + Convert.ToInt32(aktiv) +
                                                ", Art ='" + Art.Trim() + "'" +
                                                ", CalcEingangskosten=" + Convert.ToInt32(CalcEinlagerunskosten) +
                                                ", CalcAusgangskosten=" + Convert.ToInt32(CalcAuslagerungskosten) +
                                                ", CalcLagerkosten=" + Convert.ToInt32(CalcLagerkosten) +
                                                ", CalcSPLKosten=" + Convert.ToInt32(CalcSPLKosten) +
                                                ", CalcTransportkosten=" + Convert.ToInt32(CalcTransportkosten) +
                                                ", CalcDirectDeliveryKosten=" + Convert.ToInt32(CalcDirectDeliveryKosten) +
                                                ", CalcRLKosten=" + Convert.ToInt32(CalcRLKosten) +
                                                ", LagerBestandIncSPL=" + Convert.ToInt32(LagerBestandIncSPL) +
                                                ", CalcVorfracht=" + Convert.ToInt32(CalcVorfracht) +
                                                ", Von='" + Von + "'" +
                                                ", Bis='" + Bis + "'" +
                                                ", parentID=" + parentID +
                                                ", ArtEinzelAbrechnung=" + Convert.ToInt32(ArtEinzelAbrechnung) +
                                                ", CalcNebenkosten=" + Convert.ToInt32(CalcNebenkosten) +
                                                ", Modus=" + Modus +
                                                ", VersPreis='" + VersPreis.ToString().Replace(",", ".") + "'" +
                                                ", CalcGleis=" + Convert.ToInt32(CalcGleis) +
                                                ", IsVersPauschal=" + Convert.ToInt32(ISVersPauschal) +
                                                ", VersMaterialWert='" + VersMaterialWert.ToString().Replace(",", ".") + "'" +
                                                ", Zahlungsziel=" + this.Zahlungsziel +
                                                ", ZZText = '" + this.ZZText + "'" +
                                                ", ZZTextEdit = '" + this.ZZTextEdit + "'" +
                                                ", RGText ='" + this.RGText + "'" +
                                                ", CalcToll=" + Convert.ToInt32(this.CalcToll) +

                                                " WHERE ID=" + ID + ";";

            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
                //Add Logbucheintrag Exception
                string myBeschreibung = "Tarif geändert: Tarif ID:" + ID.ToString();
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), myBeschreibung);

                //Nach dem Update müssen entsprechend die Teilabrechnungsarten angepasst werden
                //generell Teilabrechnungsart = true ==> aktiv auf True setzen
                //generell Teilabrechnungsart = false ==> aktiv auf false setzen und alle anderen 
                //Tarifpositionen bis auf MasterPos löschen
                clsTarifPosition tp = new clsTarifPosition();
                tp._GL_User = this._GL_User;

                //Einlagerungskosten
                if (this.CalcEinlagerunskosten != TarifVorUpdate.CalcEinlagerunskosten)
                {
                    tp.ID = clsTarifPosition.GetTarifPosIDFromMasterPos(this._GL_User, enumTarifArtLager.Einlagerungskosten.ToString(), this.ID);
                    tp.Fill();
                    //Veränderungen setzen
                    tp.aktiv = this.CalcEinlagerunskosten;
                    //SubPositionen löschen oder deaktivieren wenn nicht aktiv
                    if (!this.CalcEinlagerunskosten)
                    {
                        //tp.DeleteTarifPositionenSubTarifPos();
                    }
                    tp.UpdateTarifPositionen();
                }
                //Auslagerungskosten
                if (this.CalcAuslagerungskosten != TarifVorUpdate.CalcAuslagerungskosten)
                {
                    tp.ID = clsTarifPosition.GetTarifPosIDFromMasterPos(this._GL_User, enumTarifArtLager.Auslagerungskosten.ToString(), this.ID);
                    tp.Fill();
                    //Veränderungen setzen
                    tp.aktiv = this.CalcAuslagerungskosten;
                    //SubPositionen löschen oder deaktivieren wenn nicht aktiv
                    if (!this.CalcAuslagerungskosten)
                    {
                        //tp.DeleteTarifPositionenSubTarifPos();
                    }
                    tp.UpdateTarifPositionen();
                }
                //Lagerkosten
                if (this.CalcLagerkosten != TarifVorUpdate.CalcLagerkosten)
                {
                    tp.ID = clsTarifPosition.GetTarifPosIDFromMasterPos(this._GL_User, enumTarifArtLager.Lagerkosten.ToString(), this.ID);
                    tp.Fill();
                    //Veränderungen setzen
                    tp.aktiv = this.CalcLagerkosten;
                    //SubPositionen löschen oder deaktivieren wenn nicht aktiv
                    if (!this.CalcLagerkosten)
                    {
                        //tp.DeleteTarifPositionenSubTarifPos();
                    }
                    tp.UpdateTarifPositionen();
                }
                //Sperrlagerkosten
                if (this.CalcSPLKosten != TarifVorUpdate.CalcSPLKosten)
                {
                    tp = new clsTarifPosition();
                    tp._GL_User = this._GL_User;
                    tp.ID = clsTarifPosition.GetTarifPosIDFromMasterPos(this._GL_User, enumTarifArtLager.Sperrlagerkosten.ToString(), this.ID);
                    tp.Fill();
                    //Veränderungen setzen
                    tp.aktiv = this.CalcSPLKosten;
                    //SubPositionen löschen oder deaktivieren wenn nicht aktiv
                    if (!this.CalcSPLKosten)
                    {
                        //tp.DeleteTarifPositionenSubTarifPos();
                    }
                    tp.UpdateTarifPositionen();
                }
                //LagerTransportkosten
                if (this.CalcTransportkosten != TarifVorUpdate.CalcTransportkosten)
                {
                    tp.ID = clsTarifPosition.GetTarifPosIDFromMasterPos(this._GL_User, enumTarifArtLager.LagerTransportkosten.ToString(), this.ID);
                    tp.Fill();
                    //Veränderungen setzen
                    tp.aktiv = this.CalcTransportkosten;
                    //SubPositionen löschen oder deaktivieren wenn nicht aktiv
                    if (!this.CalcTransportkosten)
                    {
                        //tp.DeleteTarifPositionenSubTarifPos();
                    }
                    tp.UpdateTarifPositionen();
                }
                //Direktlieferung
                if (this.CalcDirectDeliveryKosten != TarifVorUpdate.CalcDirectDeliveryKosten)
                {
                    tp.ID = clsTarifPosition.GetTarifPosIDFromMasterPos(this._GL_User, enumTarifArtLager.Direktanlieferung.ToString(), this.ID);
                    tp.Fill();
                    //Veränderungen setzen
                    tp.aktiv = this.CalcDirectDeliveryKosten;
                    //SubPositionen löschen oder deaktivieren wenn nicht aktiv
                    if (!this.CalcDirectDeliveryKosten)
                    {
                        //tp.DeleteTarifPositionenSubTarifPos();
                    }
                    tp.UpdateTarifPositionen();
                }
                //RL Kosten
                if (this.CalcRLKosten != TarifVorUpdate.CalcRLKosten)
                {
                    tp.ID = clsTarifPosition.GetTarifPosIDFromMasterPos(this._GL_User, enumTarifArtLager.Rücklieferung.ToString(), this.ID);
                    tp.Fill();
                    //Veränderungen setzen
                    tp.aktiv = this.CalcRLKosten;
                    //SubPositionen löschen oder deaktivieren wenn nicht aktiv
                    if (!this.CalcRLKosten)
                    {
                        //tp.DeleteTarifPositionenSubTarifPos();
                    }
                    tp.UpdateTarifPositionen();
                }
                //Vorfracht
                if (this.CalcVorfracht != TarifVorUpdate.CalcVorfracht)
                {
                    tp.ID = clsTarifPosition.GetTarifPosIDFromMasterPos(this._GL_User, enumTarifArtLager.Vorfracht.ToString(), this.ID);
                    tp.Fill();
                    //Veränderungen setzen
                    tp.aktiv = this.CalcVorfracht;
                    //SubPositionen löschen oder deaktivieren wenn nicht aktiv
                    if (!this.CalcVorfracht)
                    {
                        //tp.DeleteTarifPositionenSubTarifPos();
                    }
                    tp.UpdateTarifPositionen();
                }
                //Nebenkosten
                if (this.CalcNebenkosten != TarifVorUpdate.CalcNebenkosten)
                {
                    tp.ID = clsTarifPosition.GetTarifPosIDFromMasterPos(this._GL_User, enumTarifArtLager.Nebenkosten.ToString(), this.ID);
                    if (tp.ID == 0)
                    {
                        // Masterposition hinzufügen
                        clsTarifPosition.AddTarifMasterPosByTarifID(this._GL_User, enumTarifArtLager.Nebenkosten.ToString(), this.ID);
                        tp.ID = clsTarifPosition.GetTarifPosIDFromMasterPos(this._GL_User, enumTarifArtLager.Nebenkosten.ToString(), this.ID);
                    }
                    tp.Fill();
                    //Veränderungen setzen
                    tp.aktiv = this.CalcNebenkosten;
                    //SubPositionen löschen oder deaktivieren wenn nicht aktiv
                    if (!this.CalcNebenkosten)
                    {
                        //tp.DeleteTarifPositionenSubTarifPos();
                    }
                    tp.UpdateTarifPositionen();
                }
                //Gleisstellgeübhr
                if (this.CalcGleis != TarifVorUpdate.CalcGleis)
                {
                    tp.ID = clsTarifPosition.GetTarifPosIDFromMasterPos(this._GL_User, enumTarifArtLager.Gleisstellgebühr.ToString(), this.ID);
                    if (tp.ID == 0)
                    {
                        // Masterposition hinzufügen
                        clsTarifPosition.AddTarifMasterPosByTarifID(this._GL_User, enumTarifArtLager.Gleisstellgebühr.ToString(), this.ID);
                        tp.ID = clsTarifPosition.GetTarifPosIDFromMasterPos(this._GL_User, enumTarifArtLager.Gleisstellgebühr.ToString(), this.ID);
                    }
                    tp.Fill();
                    //Veränderungen setzen
                    tp.aktiv = this.CalcGleis;
                    //SubPositionen löschen oder deaktivieren wenn nicht aktiv
                    if (!this.CalcGleis)
                    {
                        //tp.DeleteTarifPositionenSubTarifPos();
                    }
                    tp.UpdateTarifPositionen();
                }
                //Maut
                if (this.CalcToll != TarifVorUpdate.CalcToll)
                {
                    tp.ID = clsTarifPosition.GetTarifPosIDFromMasterPos(this._GL_User, enumTarifArtLager.Maut.ToString(), this.ID);
                    if (tp.ID == 0)
                    {
                        // Masterposition hinzufügen
                        clsTarifPosition.AddTarifMasterPosByTarifID(this._GL_User, enumTarifArtLager.Maut.ToString(), this.ID);
                        tp.ID = clsTarifPosition.GetTarifPosIDFromMasterPos(this._GL_User, enumTarifArtLager.Maut.ToString(), this.ID);
                    }
                    tp.Fill();
                    //Veränderungen setzen
                    tp.aktiv = this.CalcToll;
                    //SubPositionen löschen oder deaktivieren wenn nicht aktiv
                    if (!this.CalcToll)
                    {
                        //tp.DeleteTarifPositionenSubTarifPos();
                    }
                    tp.UpdateTarifPositionen();
                }
            }
        }
        ///<summary>clsTarif / GetTarife</summary>
        ///<remarks>.</remarks>
        ///<param name="myGL_User">GL_User</param>
        ///<param name="myDecAdrID">Adress ID</param>
        ///<returns>Returns DataTable</returns>
        public static DataTable GetTarife(Globals._GL_USER myGL_User, decimal myDecAdrID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select a.ID" +
                            ", a.Tarifname" +
                            ", a.Beschreibung" +
                            ", a.Art" +
                            ", a.aktiv" +
                            // ", b.Bezeichnung"+
                            // ", a.CalcEingangskosten"+
                            // ", a.CalcAusgangskosten"+
                            // ", a.CalcLagerkosten"+
                            // ", a.CalcTransportkosten"+
                            " FROM Tarife a " +
                                "INNER JOIN KundenTarife c ON c.TarifID=a.ID " +
                                "WHERE c.AdrID='" + myDecAdrID + "' " +
                                "ORDER BY a.ID ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGL_User.User_ID, "Tarife");
            return dt;
        }
        ///<summary>clsTarif / GetTarifeByAdrID</summary>
        ///<remarks>Läd die Tarife einer Adress ID.</remarks>
        ///<param name="myGL_User">GL_User</param>
        ///<param name="myDecAdrID">Adress ID</param>
        ///<returns>Returns DataTable</returns>
        public static DataTable GetTarifeByAdrID(Globals._GL_USER myGL_User, decimal myDecAdrID, bool bLager, DateTime AbrVon, DateTime AbrBis, decimal myAbBereichID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select Tarife.ID" +
                            ", Tarife.Tarifname" +
                            ", Tarife.Beschreibung" +
                            ", Tarife.Art" +
                            ", Tarife.aktiv" +
                            " FROM Tarife " +
                                "INNER JOIN KundenTarife ON KundenTarife.TarifID=Tarife.ID " +
                                "INNER JOIN ArbeitsbereichTarif on ArbeitsbereichTarif.TarifID=Tarife.ID " +
                                " WHERE KundenTarife.AdrID=" + (Int32)myDecAdrID + " " +
                                " AND ArbeitsbereichTarif.AbBereichID=" + (Int32)myAbBereichID + " " +
                                "AND Art='Lager' " +
                                "AND aktiv=1 " +
                                "AND ((DATEDIFF(dd,'" + AbrVon.ToString() + "', Tarife.Von)<=0) " +
                                "AND (DATEDIFF(dd,'" + AbrBis.ToString() + "', Tarife.Bis)>=0)) " +

                                   "ORDER BY ID ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGL_User.User_ID, "Tarife");
            return dt;
        }
        ///<summary>clsTarif / GetTarifeByAdrID</summary>
        ///<remarks>Läd die Tarife einer Adress ID.</remarks>
        ///<param name="myGL_User">GL_User</param>
        ///<param name="myDecAdrID">Adress ID</param>
        ///<returns>Returns DataTable</returns>
        public static DataTable GetTarifeByTarifID(Globals._GL_USER myGL_User, decimal myTarifID, decimal myAbBereichID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select Tarife.ID" +
                            ", Tarife.Tarifname" +
                            ", Tarife.Beschreibung" +
                            ", Tarife.Art" +
                            ", Tarife.aktiv" +
                            " FROM Tarife " +
                                "INNER JOIN KundenTarife ON KundenTarife.TarifID=Tarife.ID " +
                                "INNER JOIN ArbeitsbereichTarif on ArbeitsbereichTarif.TarifID=Tarife.ID " +
                                " WHERE Tarife.ID=" + (Int32)myTarifID + " " +
                                      " AND ArbeitsbereichTarif.AbBereichID=" + (Int32)myAbBereichID + " " +
                                   //"AND Art='Lager' " +
                                   //"AND aktiv=1 " +
                                   //"AND ((DATEDIFF(dd,'" + AbrVon.ToString() + "', Tarife.Von)<=0) " +
                                   //"AND (DATEDIFF(dd,'" + AbrBis.ToString() + "', Tarife.Bis)>=0)) " +

                                   "ORDER BY ID ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGL_User.User_ID, "Tarife");
            return dt;
        }
        ///<summary>clsTarif / GetTarifeByAdrID</summary>
        ///<remarks>Läd die Tarife einer Adress ID.</remarks>
        ///<returns>Returns DataTable</returns>
        public static decimal GetTarifIDByTarifPosID(Globals._GL_USER myGL_User, decimal myTarifPosID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select Tarife.ID" +
                            " FROM Tarife " +
                                "INNER JOIN TarifPositionen ON TarifPositionen.TarifID=Tarife.ID " +
                                " WHERE TarifPositionen.ID=" + (Int32)myTarifPosID + " ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGL_User.User_ID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        ///<summary>clsTarif / DelteTarif</summary>
        ///<remarks>Löschen des Tarifdatensatzes.</remarks>
        public void DelteTarifByID()
        {
            string strSql = string.Empty;
            //strSql = "Delete from Tarife WHERE ID='" + ID.ToString() + "'";
            strSql = "Delete from Tarife WHERE ID=" + ID;

            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
                //Add Logbucheintrag Exception
                string myBeschreibung = "Tarif geöscht: Tarif ID:" + ID.ToString();
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
            }
        }
        ///<summary>clsTarif / DelteTarifbyAdrID</summary>
        ///<remarks>Löschen des Tarifdatensatzes anhand der Adr ID.</remarks>
        public void DelteTarifbyAdrID()
        {
            if (AdrID > 0)
            {
                string strSql = string.Empty;
                strSql = " Delete Tarife FROM Tarife " +
                                "INNER JOIN KundenTarife ON KundenTarife.TarifID=Tarife.ID WHERE KundenTarife.AdrID='" + AdrID + "' ";

                if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
                {
                    //Add Logbucheintrag Exception
                    string myBeschreibung = "Tarif geöscht: Tarif ID:" + ID.ToString() + " für Adr ID :" + AdrID.ToString();
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
                }
            }
        }
        ///<summary>clsTarif / ExistTarifName</summary>
        ///<remarks>Prüft anhand des Tarifnamens und der Adressnummer, ob zu dieser Adresse und 
        ///         dem Tarifnamen ein Datensatz existiert.</remarks>
        ///<param name="myGL_User">GL_User</param>
        ///<param name="myDecAdrID">Adress ID</param>
        ///<param name="myStrTarifName">Tarifname</param>
        ///<returns>Returns Booalean</returns>
        public static bool ExistTarifName(Globals._GL_USER myGL_User, decimal myDecAdrID, string myStrTarifName)
        {
            string strResult = string.Empty;
            string strSql = string.Empty;
            strSql = "Select a.ID" +
                            ", a.Tarifname" +
                            ", a.Beschreibung" +
                            ", a.aktiv" +
                            ", a.Art " +
                            "FROM Tarife a " +
                                "INNER JOIN KundenTarife b ON b.TarifID=a.ID " +
                                "WHERE b.AdrID='" + myDecAdrID + "' " +
                                "AND a.Tarifname='" + myStrTarifName + "' " +
                                "ORDER BY ID ";
            strResult = clsSQLcon.ExecuteSQL_GetValue(strSql, myGL_User.User_ID);
            if (strResult == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        ///<summary>clsTarif / ExistTarifID</summary>
        ///<remarks>Prüft anhand der TarifID od der Tarif existiert.</remarks>
        ///<returns>Returns Booalean</returns>
        public static bool ExistTarifID(decimal myTarifID, decimal myBenutzerID)
        {
            string strSql = string.Empty;
            strSql = "Select a.Tarifname" +
                            " FROM Tarife a " +
                                "INNER JOIN KundenTarife b ON b.TarifID=a.ID " +
                                "WHERE a.ID=" + myTarifID + "; ";
            bool reVal = clsSQLcon.ExecuteSQL_GetValueBool(strSql, myBenutzerID);
            return reVal;
        }
        ///<summary>clsTarif / GetTarifID</summary>
        ///<remarks>Ermittel anhand der Adress ID und dem Tarifnamen die Tarif ID.</remarks>
        public void GetTarifID()
        {
            string strResult = string.Empty;
            string strSql = string.Empty;
            strSql = "Select a.ID " +
                            "FROM Tarife a " +
                                "INNER JOIN KundenTarife b ON b.TarifID=a.ID " +
                                "WHERE b.AdrID=" + AdrID + " " +
                                "AND a.Tarifname='" + Tarifname + "' " +
                                "ORDER BY ID ";
            strResult = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strResult, out decTmp);
            ID = decTmp;
        }
        ///<summary>clsTarif / GetTarifArtByTarifeTableID</summary>
        ///<remarks>Ermittel anhand der Adress ID und dem Tarifnamen die Tarif ID.</remarks>
        public void GetTarifArtByTarifeTableID()
        {
            if (ID > 0)
            {
                string strResult = string.Empty;
                string strSql = string.Empty;
                strSql = "Select a.Art " +
                                "FROM Tarife a " +
                                    "INNER JOIN KundenTarife b ON b.TarifID=a.ID " +
                                    "WHERE a.ID='" + ID + "' ";

                strResult = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                Art = strResult;
            }
        }
        ///<summary>clsTarif / Fill</summary>
        ///<remarks>Füll die Klasse anhand der ID.</remarks>
        public void Fill()
        {
            if (clsTarif.ExistTarifID(ID, BenutzerID))
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select a.* " +
                                "FROM Tarife a WHERE a.ID=" + ID + "; ";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Tarif");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        this.ID = (decimal)dt.Rows[i]["ID"];
                        this.Tarifname = dt.Rows[i]["Tarifname"].ToString();
                        this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                        this.aktiv = (bool)dt.Rows[i]["aktiv"];
                        this.Art = dt.Rows[i]["Art"].ToString();
                        this.CalcEinlagerunskosten = (bool)dt.Rows[i]["CalcEingangskosten"];
                        this.CalcAuslagerungskosten = (bool)dt.Rows[i]["CalcAusgangskosten"];
                        this.CalcLagerkosten = (bool)dt.Rows[i]["CalcLagerkosten"];
                        this.CalcSPLKosten = (bool)dt.Rows[i]["CalcSPLKosten"];
                        this.CalcTransportkosten = (bool)dt.Rows[i]["CalcTransportkosten"];
                        this.CalcDirectDeliveryKosten = (bool)dt.Rows[i]["CalcDirectDeliveryKosten"];
                        this.CalcRLKosten = (bool)dt.Rows[i]["CalcRLKosten"];
                        this.LagerBestandIncSPL = (bool)dt.Rows[i]["LagerBestandIncSPL"];
                        this.CalcVorfracht = (bool)dt.Rows[i]["CalcVorfracht"];
                        this.Von = (DateTime)dt.Rows[i]["Von"];
                        this.Bis = (DateTime)dt.Rows[i]["Bis"];
                        this.parentID = (decimal)dt.Rows[i]["parentID"];
                        this.ArtEinzelAbrechnung = (bool)dt.Rows[i]["ArtEinzelAbrechnung"];
                        this.CalcNebenkosten = (bool)dt.Rows[i]["CalcNebenkosten"];
                        Int32 iTmp = 1; //Standard
                        Int32.TryParse(dt.Rows[i]["Modus"].ToString(), out iTmp);
                        this.Modus = iTmp;
                        Decimal decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["VersPreis"].ToString(), out decTmp);
                        this.VersPreis = decTmp;
                        this.CalcGleis = (bool)dt.Rows[i]["CalcGleis"];
                        this.ISVersPauschal = (bool)dt.Rows[i]["IsVersPauschal"];
                        this.VersMaterialWert = (decimal)dt.Rows[i]["VersMaterialWert"];
                        iTmp = 0;
                        Int32.TryParse(dt.Rows[i]["Zahlungsziel"].ToString(), out iTmp);
                        this.Zahlungsziel = iTmp;
                        this.ZZText = dt.Rows[i]["ZZText"].ToString();
                        this.ZZTextEdit = dt.Rows[i]["ZZTextEdit"].ToString();
                        this.RGText = dt.Rows[i]["RGText"].ToString();
                        this.CalcToll = (bool)dt.Rows[i]["CalcToll"];
                    }
                }
                dtTarifpositionen = clsTarifPosition.GetTarifePositionen(this._GL_User, this.ID);
                //zugewiesene Güterarten ermitteln
                this.TarifGArtZuweisung = new clsTarifGArtZuweisung();
                this.TarifGArtZuweisung._GL_User = this._GL_User;
                this.TarifGArtZuweisung.TarifID = this.ID;
                this.TarifGArtZuweisung.FillByTarifID();
                this.ExistLinkedGArt = this.TarifGArtZuweisung.GArtAssign;

            }
        }
        /************************************************************************
        *                        Methoden Table KundenTarife
        * ***********************************************************************/

        public string TarifArt;
        public bool MoreMaxKm = false;
        public DataTable dtMaxKm = new DataTable("MaxKm");
        internal bool kmGrMax = false;

        private Int32 _km;
        private Int32 _Maxkm;
        private Int32 _zuKm;
        private decimal _PreisTo;
        private decimal _aufPreisTo;
        private decimal _fpflGewicht;
        private string _FrachtText;
        private decimal _Fracht;
        private decimal _MargeProzent;
        private decimal _MargeEuro;
        private bool _Pauschal;
        private string _Col;
        private string _TarifAngabe;
        public decimal Fracht
        {
            get { return _Fracht; }
            set { _Fracht = value; }
        }
        public Int32 km
        {
            get { return _km; }
            set { _km = value; }
        }
        public Int32 Maxkm
        {
            get { return _Maxkm; }
            set { _Maxkm = value; }
        }
        public Int32 zuKm
        {
            get { return _zuKm; }
            set { _zuKm = value; }
        }
        public decimal PreisTo
        {
            get { return _PreisTo; }
            set { _PreisTo = value; }
        }
        public decimal aufPreisTo
        {
            get { return _aufPreisTo; }
            set { _aufPreisTo = value; }
        }
        public decimal fpflGewicht
        {
            get { return _fpflGewicht; }
            set { _fpflGewicht = value; }
        }
        public string FrachtText
        {
            get { return _FrachtText; }
            set { _FrachtText = value; }
        }
        public decimal MargeProzent
        {
            get { return _MargeProzent; }
            set { _MargeProzent = value; }
        }
        public decimal MargeEuro
        {
            get { return _MargeEuro; }
            set { _MargeEuro = value; }
        }
        public bool Pauschal
        {
            get { return _Pauschal; }
            set { _Pauschal = value; }
        }
        public string Col
        {
            get { return _Col; }
            set { _Col = value; }
        }
        public string TarifAngabe
        {
            get
            {
                // _TarifAngabe = TarifArt + "-" + Functions.FromatDecimal(Math.Round((fpflGewicht / 1000), 1, MidpointRounding.AwayFromZero)) + "to-" + km + "km-" + Functions.FromatDecimal(PreisTo) + "€/to";
                _TarifAngabe = TarifArt + "-" + Functions.FormatDecimal((Math.Ceiling((fpflGewicht / 100)) / 10)) + "to-" + km + "km-" + Functions.FormatDecimal(PreisTo) + "€/to";
                return _TarifAngabe;
            }
            set { _TarifAngabe = value; }
        }

        /*****************************************************************************************************
         * 
         * 
         * **************************************************************************************************/
        //
        //
        //
        public void GetFracht()
        {
            //Daten einlesen
            Int32 Suchgewicht = 1 + Convert.ToInt32(Math.Round((fpflGewicht / 1000), 0));
            Suchgewicht = SetColToMin(Suchgewicht);
            Col = Convert.ToString(Suchgewicht) + "t";


            GetMaxKmTable();
            SetMaxKmAndZugschlag();

            switch (TarifArt)
            {
                case "GNT":
                    if (Maxkm < km)
                    {
                        kmGrMax = true;
                        //Tarif geht bis max 200 km ab dann alle 5 km + Frachtsatz
                        kmGNT();
                        GetMaxKmFrachtrate();
                        GetAufPreisFrachtrate();
                        Frachtberechnung();
                    }
                    else
                    {
                        kmGNT();
                        GetFrachtrate();
                        Frachtberechnung();
                    }
                    break;

                case "GFT":
                    if (Maxkm < km)
                    {

                    }
                    else
                    {
                        kmGFT();
                        GetFrachtrate();
                        Frachtberechnung();
                    }

                    break;

                case "GNTalt":
                    if (Maxkm < km)
                    {
                        kmGrMax = true;
                        //Tarif geht bis max 200 km ab dann alle 5 km + Frachtsatz
                        kmGNT();
                        GetMaxKmFrachtrate();
                        GetAufPreisFrachtrate();
                        Frachtberechnung();
                    }
                    else
                    {
                        kmGNT();
                        GetFrachtrate();
                        Frachtberechnung();
                    }
                    break;

                case "Kundentarif":

                    break;
            }
        }
        //
        //
        //
        private Int32 SetColToMin(Int32 _Suchgewicht)
        {
            Int32 SGewicht = _Suchgewicht;
            switch (TarifArt)
            {
                case "GNT":
                    if (SGewicht <= 5)
                    {
                        SGewicht = 5;
                        fpflGewicht = 5000;
                    }
                    if (SGewicht >= 29)
                    {
                        SGewicht = 29;
                    }
                    break;

                case "GFT":
                    if (SGewicht <= 5)
                    {
                        SGewicht = 5;
                        fpflGewicht = 5000;
                    }
                    if (SGewicht >= 26)
                    {
                        SGewicht = 26;
                    }
                    break;

                case "GNTalt":
                    if (SGewicht <= 5)
                    {
                        SGewicht = 5;
                        fpflGewicht = 5000;
                    }
                    if (SGewicht >= 29)
                    {
                        SGewicht = 29;
                    }
                    break;

                case "Kundentarif":

                    break;
            }
            return SGewicht;
        }
        //
        //
        //
        private void GetFrachtrate()
        {
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;


            Command.CommandText = GetSQL();

            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();

            if (obj == null)
            {
                PreisTo = 0.00m;
            }
            else
            {
                PreisTo = (decimal)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        //
        //----------- Frachtrate für die Maxmimale Entfernung ------------------
        //
        private void GetMaxKmFrachtrate()
        {
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = GetSQLMaxKM();

            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();

            if (obj == null)
            {
                PreisTo = 0.00m;
            }
            else
            {
                PreisTo = (decimal)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        //
        //
        //
        private string GetSQL()
        {
            string sql = string.Empty;
            switch (TarifArt)
            {
                case "GNT":
                    sql = "SELECT [" + Col + "] From GNT WHERE km= '" + km + "'";
                    break;

                case "GFT":
                    sql = "SELECT [" + Col + "] From GFT WHERE km= '" + km + "'";
                    break;

                case "GNTalt":
                    sql = "SELECT [" + Col + "] From GNTalt WHERE km= '" + km + "'";
                    break;

                case "Kundentarif":

                    break;
            }
            return sql;
        }
        //
        //-------------------  SQL Max Km -----------------------
        //
        private string GetSQLMaxKM()
        {
            string sql = string.Empty;
            switch (TarifArt)
            {
                case "GNT":
                    sql = "SELECT [" + Col + "] From GNT WHERE km='" + Maxkm + "'";
                    break;

                case "GNTalt":
                    sql = "SELECT [" + Col + "] From GNTalt WHERE km='" + Maxkm + "'";
                    break;
            }
            return sql;
        }
        //
        //------------------ sql zusätzl km ---------------------
        //
        private string GetSQLZusatzKM()
        {
            string sql = string.Empty;
            switch (TarifArt)
            {
                case "GNT":
                    sql = "SELECT [" + Col + "] From GNT WHERE km='" + zuKm * 1000 + "'";
                    break;

                case "GNTalt":
                    sql = "SELECT [" + Col + "] From GNTalt WHERE km='" + zuKm * 1000 + "'";
                    break;
            }
            return sql;
        }
        //
        //--------------- Frachtrate Aufpreis -------------------
        //
        private void GetAufPreisFrachtrate()
        {
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = GetSQLZusatzKM();

            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();

            if (obj == null)
            {
                aufPreisTo = 0.00m;
            }
            else
            {
                aufPreisTo = (decimal)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        //
        //
        //
        private void GetMaxKmTable()
        {
            try
            {

                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT TOP(2)km From " + TarifArt + " ORDER BY km DESC";

                Globals.SQLcon.Open();
                ada.Fill(dtMaxKm);
                ada.Dispose();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        //
        //----------- setzt max Km und zuschlagkm ------------------
        //
        private void SetMaxKmAndZugschlag()
        {
            Maxkm = (Int32)dtMaxKm.Rows[1]["km"];
            zuKm = (Int32)dtMaxKm.Rows[0]["km"];
            zuKm = zuKm / 1000;                       //Zeile: je weitere X km - X wurd in DB mit 1000 multipliziert
        }
        //
        //--------- da die km gestaffelt sind, muss hier der richtige km Wert errechnet werden ---------------
        //
        private void kmGNT()
        {
            //siehe DB GNT
            if (km <= 29)
            {
                Int32 mod = km % 2;
                if (mod != 0)
                {
                    km = km + (2 - mod);
                    kmGNT();
                }
            }
            if ((km >= 30) & (km < 99))
            {
                Int32 mod = km % 5;
                if (mod != 0)
                {
                    km = km + (5 - mod);
                    kmGNT();
                }
            }
            if ((km >= 100) & (km <= 200))
            {
                Int32 mod = km % 10;
                if (mod != 0)
                {
                    km = km + (10 - mod);
                    kmGNT();
                }
            }
        }
        //
        //--------- km Berechnung nach GFT ----------------
        //
        private void kmGFT()
        {
            //siehe DB GNT
            if (km <= 99)
            {
                Int32 mod = km % 5;
                if (mod != 0)
                {
                    km = km + (5 - mod);
                    kmGFT();
                }
            }
            if ((km >= 100) & (km < 299))
            {
                Int32 mod = km % 10;
                if (mod != 0)
                {
                    km = km + (10 - mod);
                    kmGFT();
                }
            }
            if ((km >= 300) & (km < 799))
            {
                Int32 mod = km % 20;
                if (mod != 0)
                {
                    km = km + (20 - mod);
                    kmGFT();
                }
            }
            if ((km >= 800) & (km <= 900))
            {
                Int32 mod = km % 50;
                if (mod != 0)
                {
                    km = km + (50 - mod);
                    kmGFT();
                }
            }
        }
        //
        //----------- berechnung der Fracht ---------------
        //
        private void Frachtberechnung()
        {
            //public static decimal RoundStep( this decimal d, decimal step ) {
            //      return Math.Round(d / step + step) * step;
            if (kmGrMax)
            {
                Int32 moreKm = km - Maxkm;
                Int32 mod = moreKm % zuKm;


                if (mod == 0)
                {
                    PreisTo = PreisTo + ((moreKm / zuKm) * aufPreisTo);
                }
                else
                {
                    moreKm = moreKm + (zuKm - mod);
                    PreisTo = PreisTo + ((moreKm / zuKm) * aufPreisTo);
                }

            }

            //decimal gewicht = Math.Round((fpflGewicht / 1000), 1, MidpointRounding.AwayFromZero);
            // Gewicht wird aufgerundet auf volle hundert kg
            decimal gewicht = Math.Ceiling((fpflGewicht / 100));
            gewicht = gewicht / 10;

            PreisTo = Math.Round(PreisTo, 2, MidpointRounding.AwayFromZero);
            Fracht = gewicht * PreisTo;
            Fracht = Math.Round(Fracht, 2, MidpointRounding.AwayFromZero);

            if (MargeEuro > 0.00m)
            {
                Fracht = Fracht - MargeEuro;
                Fracht = Math.Round(Fracht, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                if (MargeProzent > 0.0m)
                {
                    Fracht = Fracht - (Fracht * (MargeProzent / 100));
                    Fracht = Math.Round(Fracht, 2, MidpointRounding.AwayFromZero);
                }
            }
        }

    }
}
