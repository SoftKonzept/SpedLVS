using LVS.Communicator.EdiVDA.EdiVDAValues;
using LVS.Constants;
using LVS.Fakturierung;
using LVS.Models;
using LVS.Print;
using LVS.ViewData;
using PdfSharp.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using Telerik.Reporting;

namespace LVS
{
    public class clsFaktLager : INotifyPropertyChanged
    {

        public clsFaktLager()
        {
            ZUGFeRDAvailable = new ZUGFeRD.ZUGFeRD_IsAvailable(this.GL_System, 0, (int)this.BenutzerID);
        }
        public clsFaktLager(Globals._GL_SYSTEM myGLSystem, Globals._GL_USER myGLUser, clsSystem mySystem)
        {
            _GL_System = myGLSystem;
            _GL_User = myGLUser;
            Sys = mySystem;
        }

        public const string const_Abrechnungsart_Einlagerung = "Einlagerungskosten";
        public const string const_Abrechnungsart_Auslagerung = "Auslagerungskosten";
        public const string const_Abrechnungsart_Lagerkosten = "Lagerkosten";
        public const string const_Abrechnungsart_LagerTransportkosten = "LagerTransportkosten";
        public const string const_Abrechnungsart_SPL = "Sperrlagerkosten";
        public const string const_Abrechnungsart_Direktanlieferung = "Direktanlieferung";
        public const string const_Abrechnungsart_Ruecklieferung = "Rücklieferung";
        public const string const_Abrechnungsart_Vorfracht = "Vorfracht";
        public const string const_Abrechnungsart_Nebenkosten = "Nebenkosten";
        public const string const_Abrechnungsart_Gleisstellgebuehr = "Gleisstellgebühr";
        public const string const_Abrechnungsart_Maut = "Maut"; //Maut

        public clsReportDocSetting RepDocSettings = new clsReportDocSetting();


        public clsRechnung Rechnung = new clsRechnung();
        public clsSystem Sys;

        public Globals._GL_USER _GL_User;
        public List<string> LogMessages { get; set; } = new List<string>();
        //public Globals._GL_SYSTEM _GL_System;
        private Globals._GL_SYSTEM _GL_System;
        public Globals._GL_SYSTEM GL_System
        {
            get
            {
                if (_GL_System is Globals._GL_SYSTEM)
                {
                }
                else
                {
                    _GL_System = new Globals._GL_SYSTEM();
                }
                return _GL_System;
            }
            set
            {
                _GL_System = value;
                InitZUGFeRD();
                OnPropertyChanged("GL_System");
            }
        }

        public ZUGFeRD.ZUGFeRD_IsAvailable ZUGFeRDAvailable { get; set; }


        //************  User  ***************
        private decimal _BenutzerID;
        public event PropertyChangedEventHandler PropertyChanged;

        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        public bool LagerbestandInclSPL = false;
        public DataTable dtRGAuftraggeber = new DataTable("bestehendeRG"); //bestehende Rechnungen des Auftraggebers
        public DataTable dtRechnung = new DataTable("Rechnung");
        public DataTable dtRGVorschau = new DataTable("Vorschau");


        public DataTable dtArtikelEinlagerung = new DataTable("ArtikelEinlagerung");
        public DataTable dtArtikelEinlagerungUmbuchungGratis = new DataTable("ArtikelEinlagerungUB");
        public DataTable dtArtikelAnfangsbestand = new DataTable("ArtikelAnfangsbestand");
        public DataTable dtArtikelAuslagerung = new DataTable("ArtikelAuslagerung");
        public DataTable dtArtikelAuslagerungUmbuchungGratis = new DataTable("ArtikelAuslagerung");
        public DataTable dtArtikelSperrlager = new DataTable("ArtikelSperrlager");
        public DataTable dtArtikelLagerTransporte = new DataTable("ArtikelLagerTransporte");
        public DataTable dtArtikelDirektanlieferung = new DataTable("ArtikelDirektanlieferung");
        public DataTable dtArtikelRuecklieferung = new DataTable("ArtikelRuecklieferung");
        public DataTable dtArtikelLagerbestand = new DataTable("ArtikelLagerbestand");
        public DataTable dtArtikelVorfracht = new DataTable("ArtikelLagerbestand");
        public DataTable dtArtikelNebenkosten = new DataTable("ArtikelNebenkosten");
        public DataTable dtArtikelGleis = new DataTable("ArtikelGleisstellgebühr");
        public DataTable dtArtikelToll = new DataTable("dtArtikelToll");
        public DataTable dtErrorArtikel = new DataTable("ErrorArtikel");
        public DataTable dtWaggonListe = new DataTable("WaggonListe");
        public DataTable dtArtikelAnfangsbestandLagerdauerUnterschritten = new DataTable("ArtikelAnfangsbestandLagerdauer");
        public DataTable dtSLVS = new DataTable("SLVS");

        private string AbrEinheitLagerbestand = string.Empty;
        private string BasisEinheitLagerbestand = string.Empty;

        public decimal ID { get; set; }
        public decimal RGNr { get; set; }
        private DataTable _dtAbrKunden;
        public DataTable dtAbrKunden
        {
            get
            {
                _dtAbrKunden = GetTableAbzurechnendeKunden();
                return _dtAbrKunden;
            }
            set { _dtAbrKunden = value; }
        }
        public DataTable dtBestandsauflistung { get; set; }

        private decimal _Auftraggeber;
        public decimal Auftraggeber
        {
            get
            {
                return _Auftraggeber;
            }
            set
            {
                _Auftraggeber = value;
                //if ((this.ZUGFeRDAvailable is ZUGFeRD.ZUGFeRD_IsAvailable) && (!_Auftraggeber.Equals(this.ZUGFeRDAvailable.AdrInvoiceReceiver.Id)))
                //{
                //    InvoiceReceiver = _Auftraggeber;
                //}
                OnPropertyChanged("Auftraggeber");
            }
        }
        private decimal _InvoiceReceiver;
        public decimal InvoiceReceiver
        {
            get
            {
                return _InvoiceReceiver;
            }
            set
            {
                _InvoiceReceiver = value;
                this.InitZUGFeRD((int)_InvoiceReceiver);
                OnPropertyChanged("InvoiceReceiver");
            }
        }


        public DateTime VonZeitraum { get; set; }
        public DateTime BisZeitraum { get; set; }
        public decimal Anfangsbestand { get; set; }
        public decimal Einlagerung { get; set; }
        public decimal Lagerbestand { get; set; }
        public decimal Auslagerung { get; set; }
        public decimal Sperrlager { get; set; }
        public decimal LagerTransport { get; set; }
        public decimal Vorfracht { get; set; }
        public decimal Ruecklieferung { get; set; }
        public decimal Direktanlieferung { get; set; }
        public decimal Gleisgebuehr { get; set; }
        public decimal Nebenkosten { get; set; }
        public decimal Toll { get; set; }
        public decimal Endbestand { get; set; }
        public decimal MandantenID { get; set; }
        public string MandantenName { get; set; }
        public decimal Netto { get; set; }
        public DateTime Abrechnungsdatum { get; set; }
        public decimal UBExBestand { get; set; }

        public decimal EinlagerungSaldo { get; set; }
        public decimal AuslagerungSaldo { get; set; }
        //public decimal AnfangsbestandSaldo { get; set; }
        public decimal EndbestandSaldo { get; set; }

        public bool bUseBKZ = false;
        public bool bSLVSMaterialWert = false;
        public bool bCalcPreviewAdmin = false;

        private string _strProgress;
        public string strProgress
        {
            get
            {
                return this._strProgress;
            }
            set
            {
                _strProgress = value;
                OnPropertyChanged("Progress");
            }
        }
        private decimal _decMaxProgress;

        public decimal decMaxProgress
        {
            get
            {
                return this._decMaxProgress;
            }
            set
            {
                _decMaxProgress = value;
                OnPropertyChanged("MaxProgress");
            }
        }

        int iCol0Width = 40;
        int iCol1Width = 120;
        /*****************************************************************************************
         *                      Methoden / Procedure
         * **************************************************************************************/
        public void InitZUGFeRD()
        {
            int iInvoiceReceiver = 0;
            //if ((this.Rechnung is clsRechnung) && (this.Rechnung.ID > 0))
            //{
            //    iInvoiceReceiver = (int)this.Rechnung.Auftraggeber;
            //}
            if (this.Auftraggeber > 0)
            {
                iInvoiceReceiver = (int)this.Auftraggeber;
            }
            InitZF(iInvoiceReceiver);
        }
        public void InitZUGFeRD(int myInvReceiverId)
        {
            InitZF(myInvReceiverId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myInvReceiverId"></param>
        private void InitZF(int myInvReceiverId)
        {
            int iInvoiceReceiver = 0;
            if (myInvReceiverId > 0)
            {
                iInvoiceReceiver = myInvReceiverId;
            }
            ZUGFeRDAvailable = new ZUGFeRD.ZUGFeRD_IsAvailable(this._GL_System, iInvoiceReceiver, (int)this.BenutzerID);
        }

        ///<summary>clsFaktLager / AddArtikelLager_SQL</summary>
        ///<remarks></remarks>
        private DataTable GetTableAbzurechnendeKunden()
        {
            DataTable dtTmp = new DataTable();
            string strSql = string.Empty;
            try
            {
                strSql = "Select DISTINCT " +
                                "c.Auftraggeber as AuftraggeberID" +
                                ", b.ViewID as Matchcode" +
                                ", b.Name1+' - '+b.PLZ+' '+b.Ort as Auftraggeber" +
                                " FROM Artikel a " +
                                    "INNER JOIN LEingang c ON c.ID=a.LEingangTableID " +
                                    "LEFT JOIN LAusgang d ON d.ID=a.LAusgangTableID " +
                                    "LEFT JOIN ADR b ON b.ID = c.Auftraggeber " +
                                    "WHERE " +
                                            "c.AbBereich = " + this.Sys.AbBereich.ID + " " +
                                             "AND " +
                                            "(a.BKZ=1 OR " +
                                            "(c.Date>'" + VonZeitraum.AddDays(-1) + "' AND c.Date<'" + BisZeitraum.AddDays(1) + "') OR " +
                                            "(d.Datum>'" + VonZeitraum.AddDays(-1) + "' AND d.Datum<'" + BisZeitraum.AddDays(1) + "') OR " +
                                            "(c.Date<'" + VonZeitraum + "' AND d.Datum>'" + BisZeitraum.AddDays(1) + "') " +
                                            " OR (a.BKZ=0 AND d.Checked=0 AND c.Date<'" + VonZeitraum + "' AND  d.Datum<'" + VonZeitraum + "')" + // mr 06.06.2020
                                            ") " +
                                            " ORDER BY b.ViewID; ";
                dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "AbzurechnendeArtikel");

                if (dtTmp.Rows.Count == 0)
                {
                    clsError error = new clsError();
                    error.InitClass(this._GL_User, this.Sys);
                    error.Code = "Abzurechnende/Auftraggeber: " + dtTmp.Rows.Count.ToString();
                    error.Aktion = "clsFaktLager - GetTableAbzurechnendeKunden";
                    error.Details = strSql;
                    error.exceptText = string.Empty;
                    error.WriteError();

                }
            }
            catch (Exception ex)
            {
                clsError error = new clsError();
                error.InitClass(this._GL_User, this.Sys);
                error.Code = "";
                error.Aktion = "clsFaktLager - GetTableAbzurechnendeKunden";
                error.exceptText = ex.ToString();
                error.WriteError();
                clsMessages.Allgemein_ERRORTextShow(ex.ToString());
            }
            return dtTmp;
        }
        ///<summary>clsFaktLager / GetBestandsauflistung</summary>
        ///<remarks>In der DataTable dtBestandsauflistung werden die Spalten und dann die entsprechenden Datensätze angelegt und gefüllt:
        ///         - Anfangsbestand
        ///         - Einlagerung
        ///         - Auslagerung
        ///         - Sperrlager
        ///         - Transporte
        ///         - Direktanlieferungen
        ///         - Rücklieferungen
        ///         - UBExBestand
        ///         - Endbestand</remarks>
        public void GetBestandsauflistung(ref clsTarif Tarif)
        {
            dtRGVorschau = new DataTable("Vorschau");
            //dtRGVorschau = Rechnung.GetRechnung();
            if (this.Rechnung.RGRevision != null)
            {
                Rechnung.ID = 0;
            }
            dtRGVorschau = Rechnung.GetRechnung();

            //Reset
            Einlagerung = 0;
            Lagerbestand = 0;
            Auslagerung = 0;
            Anfangsbestand = 0;
            Sperrlager = 0;
            LagerTransport = 0;
            Direktanlieferung = 0;
            Ruecklieferung = 0;
            Endbestand = 0;
            Vorfracht = 0;
            Gleisgebuehr = 0;
            Nebenkosten = 0;
            Toll = 0;

            EinlagerungSaldo = 0;
            AuslagerungSaldo = 0;
            //AnfangsbestandSaldo = 0;
            EndbestandSaldo = 0;

            dtArtikelEinlagerung = new DataTable("ArtikelEinlagerung");
            dtArtikelAnfangsbestand = new DataTable("ArtikelAnfangsbestand");
            dtArtikelAuslagerung = new DataTable("ArtikelAuslagerung");
            dtArtikelSperrlager = new DataTable("ArtikelSperrlager");
            dtArtikelLagerTransporte = new DataTable("ArtikelLagerTransporte");
            dtArtikelDirektanlieferung = new DataTable("ArtikelDirektanlieferung");
            dtArtikelRuecklieferung = new DataTable("ArtikelRuecklieferung");
            dtArtikelLagerbestand = new DataTable("ArtikelLagerbestand");
            dtArtikelVorfracht = new DataTable("ArtikelLagerbestand");
            dtArtikelNebenkosten = new DataTable("ArtikelUBExKosten");
            dtArtikelGleis = new DataTable("ArtikelGleisstellgebühr");
            dtArtikelToll = new DataTable("ArtikelToll");


            string EinheitEinlg = string.Empty;
            string EinheitAuslg = string.Empty;
            string EinheitLg = string.Empty;
            string EinheitSPL = string.Empty;
            string EinheitRL = string.Empty;
            string EinheitDirekt = string.Empty;
            string EinheitLgTransport = string.Empty;
            string EinheitVorfracht = string.Empty;
            string EinheitGleiskosten = string.Empty;
            string EinheitNebenkosten = "€";
            string EinheitToll = string.Empty;

            if (
                (Tarif.dtTarifpositionen != null) &&
                (Tarif.dtTarifpositionen.Rows.Count > 0)
               )
            {
                //Der Anfangsbestand muss bereits hier ermittelt werden
                decimal decTmp = 0;

                string strSQL = GetAnfangsbestand(ref Tarif, true, 0, false, true, bUseBKZ);
                string strAnfBestand = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                Decimal.TryParse(strAnfBestand, out decTmp);
                Anfangsbestand = decTmp;

                //Artikel Anfangsbestand
                string strSQLArtikel = string.Empty;
                strSQLArtikel = GetAnfangsbestand(ref Tarif, false, 0, false, true, bUseBKZ);
                dtArtikelAnfangsbestand = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelAnfangsbestand");
                Int32 iPos = 0;
                //sortieren für die passende Positionen
                //Tarif.dtTarifpositionen.DefaultView.Sort = "StaffelPos desc, TPosVerweis, SortIndex";
                Tarif.dtTarifpositionen.DefaultView.Sort = "SortIndex, StaffelPos desc, TPosVerweis";

                DataTable dtTmpZSpeicher = Tarif.dtTarifpositionen.DefaultView.ToTable();
                Tarif.dtTarifpositionen.Clear();
                Tarif.dtTarifpositionen = dtTmpZSpeicher;
                //durchlaufen der Tarifpostionen
                Int32 iTPosVerweis = -1;

                //Ermittelung der Bewegung Gesamtbestände
                ////Zugang (Einlagerungen)
                string strTmp = string.Empty;
                strTmp = string.Empty;
                strSQL = GetSQLforBestand(ref Tarif, const_Abrechnungsart_Einlagerung, true, Tarif.TarifPosition.ID);
                strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                EinlagerungSaldo = decTmp;


                ////Abgang (Auslagerungen)
                strTmp = string.Empty;
                strSQL = GetSQLforBestand(ref Tarif, const_Abrechnungsart_Auslagerung, true, Tarif.TarifPosition.ID);
                strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                AuslagerungSaldo = decTmp;
                EndbestandSaldo = Anfangsbestand + EinlagerungSaldo - AuslagerungSaldo;


                for (Int32 i = 0; i <= Tarif.dtTarifpositionen.Rows.Count - 1; i++)
                {
                    bool IsActive = (bool)Tarif.dtTarifpositionen.Rows[i]["aktiv"];
                    if (IsActive)
                    {
                        decimal UFact = 1;
                        string strAbrArt = Tarif.dtTarifpositionen.Rows[i]["Art"].ToString();

                        //Tarifposition zuweisen 
                        //Verweis = 0 bedeutet das keine Staffel also immer neue Position
                        if (iTPosVerweis == 0)
                        {
                            iPos++;
                        }
                        else
                        {
                            //Verweis unterschiedlich -> neue Staffel -> neue Position
                            if ((Int32)Tarif.dtTarifpositionen.Rows[i]["TPosVerweis"] != iTPosVerweis)
                            {
                                iPos++;
                            }
                        }
                        iTPosVerweis = (Int32)Tarif.dtTarifpositionen.Rows[i]["TPosVerweis"];

                        Tarif.TarifPosition = new clsTarifPosition();
                        Tarif.TarifPosition._GL_User = this._GL_User;
                        Tarif.TarifPosition.ID = (decimal)Tarif.dtTarifpositionen.Rows[i]["ID"];
                        Tarif.TarifPosition.Fill();

                        decTmp = 0;
                        strTmp = string.Empty;
                        strSQL = string.Empty;
                        strSQLArtikel = string.Empty;
                        DataTable dtTmp = new DataTable();

                        switch (strAbrArt)
                        {
                            //case "Einlagerungskosten":
                            case const_Abrechnungsart_Einlagerung:
                                if (Tarif.TarifPosition.aktiv)
                                {
                                    Rechnung.RGPosEinlagerung = new clsRGPositionen();
                                    Rechnung.RGPosEinlagerung._GL_User = this._GL_User;
                                    Rechnung.RGPosEinlagerung.AbrechnungsArt = strAbrArt;
                                    Rechnung.RGPosEinlagerung.Tariftext = Tarif.Tarifname;
                                    Rechnung.RGPosEinlagerung.CalcModValue = Tarif.TarifPosition.CalcModValue;
                                    Rechnung.RGPosEinlagerung.CalcModus = Tarif.TarifPosition.CalcModus;
                                    Rechnung.RGPosEinlagerung.TarifPricePerUnit = Tarif.TarifPosition.PreisEinheit;

                                    Rechnung.RGPosEinlagerung.AbrEinheit = Tarif.TarifPosition.AbrEinheit;
                                    EinheitEinlg = Rechnung.RGPosEinlagerung.AbrEinheit;
                                    EinheitLg = EinheitEinlg;
                                    //Neubelegung in Ein-Aus- und Lagerkosten
                                    AbrEinheitLagerbestand = Tarif.TarifPosition.AbrEinheit;
                                    BasisEinheitLagerbestand = Tarif.TarifPosition.BasisEinheit;

                                    //Bestand                    
                                    strSQL = GetSQLforBestand(ref Tarif, strAbrArt, true, Tarif.TarifPosition.ID);
                                    strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                                    decTmp = 0;
                                    Decimal.TryParse(strTmp, out decTmp);
                                    Rechnung.RGPosEinlagerung.Menge = decTmp;

                                    ////Artikel
                                    strSQLArtikel = string.Empty;
                                    strSQLArtikel = GetSQLforBestand(ref Tarif, strAbrArt, false, Tarif.TarifPosition.ID);
                                    dtTmp = new DataTable();
                                    dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelEinlagerung");

                                    Fakt_CalculateArticlePosition faktCalc = new Fakt_CalculateArticlePosition(dtTmp, Tarif, VonZeitraum, BisZeitraum);
                                    MergeDataTable(ref dtArtikelEinlagerung, faktCalc.dtDistinctArtID);

                                    //MergeDataTable(ref dtArtikelEinlagerung, dtTmp);

                                    Rechnung.RGPosEinlagerung.Zugang = EinlagerungSaldo;
                                    Rechnung.RGPosEinlagerung.Abgang = AuslagerungSaldo;

                                    Rechnung.RGPosEinlagerung.Anfangsbestand = Anfangsbestand;
                                    Rechnung.RGPosEinlagerung.Endbestand = EndbestandSaldo;

                                    //Berechnung
                                    //CalcTarifPosition(ref Rechnung.RGPosEinlagerung, ref Tarif.TarifPosition, ref iPos, strAbrArt);
                                    CalcTarifPosition(ref Rechnung.RGPosEinlagerung, ref Tarif.TarifPosition, ref iPos, strAbrArt, dtArtikelEinlagerung);
                                }
                                break;

                            //case "Auslagerungskosten":
                            case const_Abrechnungsart_Auslagerung:
                                if (Tarif.TarifPosition.aktiv)
                                {
                                    //Tarifwerte zuweisen
                                    Rechnung.RGPosAuslagerung = new clsRGPositionen();
                                    Rechnung.RGPosAuslagerung._GL_User = this._GL_User;
                                    Rechnung.RGPosAuslagerung.AbrechnungsArt = strAbrArt;
                                    Rechnung.RGPosAuslagerung.Tariftext = Tarif.Tarifname;
                                    Rechnung.RGPosAuslagerung.CalcModValue = Tarif.TarifPosition.CalcModValue;
                                    Rechnung.RGPosAuslagerung.CalcModus = Tarif.TarifPosition.CalcModus;
                                    Rechnung.RGPosAuslagerung.AbrEinheit = Tarif.TarifPosition.AbrEinheit;
                                    Rechnung.RGPosAuslagerung.TarifPricePerUnit = Tarif.TarifPosition.PreisEinheit;

                                    EinheitAuslg = Rechnung.RGPosAuslagerung.AbrEinheit;
                                    EinheitLg = EinheitAuslg;
                                    //Neubelegung in Ein-Aus- und Lagerkosten
                                    AbrEinheitLagerbestand = Tarif.TarifPosition.AbrEinheit;
                                    BasisEinheitLagerbestand = Tarif.TarifPosition.BasisEinheit;

                                    //Bestand                    
                                    strSQL = GetSQLforBestand(ref Tarif, strAbrArt, true, Tarif.TarifPosition.ID);
                                    strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                                    decTmp = 0;
                                    Decimal.TryParse(strTmp, out decTmp);
                                    Rechnung.RGPosAuslagerung.Menge = decTmp;

                                    //Artikel
                                    strSQLArtikel = string.Empty;
                                    strSQLArtikel = GetSQLforBestand(ref Tarif, strAbrArt, false, Tarif.TarifPosition.ID);
                                    dtTmp = new DataTable();
                                    dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelAuslagerung");

                                    Fakt_CalculateArticlePosition faktCalc = new Fakt_CalculateArticlePosition(dtTmp, Tarif, VonZeitraum, BisZeitraum);
                                    MergeDataTable(ref dtArtikelAuslagerung, faktCalc.dtDistinctArtID);

                                    Rechnung.RGPosAuslagerung.Zugang = EinlagerungSaldo;
                                    Rechnung.RGPosAuslagerung.Abgang = AuslagerungSaldo;

                                    Rechnung.RGPosAuslagerung.Anfangsbestand = Anfangsbestand;
                                    Rechnung.RGPosAuslagerung.Endbestand = EndbestandSaldo;

                                    //Berechnung
                                    //CalcTarifPosition(ref Rechnung.RGPosAuslagerung, ref Tarif.TarifPosition, ref iPos, strAbrArt);
                                    CalcTarifPosition(ref Rechnung.RGPosAuslagerung, ref Tarif.TarifPosition, ref iPos, strAbrArt, dtArtikelAuslagerung);
                                }
                                break;

                            //case "Lagerkosten":
                            case const_Abrechnungsart_Lagerkosten:
                                if (Tarif.TarifPosition.aktiv)
                                {
                                    Rechnung.RGPosLagerbestand = new clsRGPositionen();
                                    Rechnung.RGPosLagerbestand._GL_User = this._GL_User;
                                    Rechnung.RGPosLagerbestand.AbrechnungsArt = strAbrArt;
                                    Rechnung.RGPosLagerbestand.Tariftext = Tarif.Tarifname;
                                    Rechnung.RGPosLagerbestand.CalcModValue = Tarif.TarifPosition.CalcModValue;
                                    Rechnung.RGPosLagerbestand.CalcModus = Tarif.TarifPosition.CalcModus;
                                    Rechnung.RGPosLagerbestand.AbrEinheit = Tarif.TarifPosition.AbrEinheit;
                                    Rechnung.RGPosLagerbestand.TarifPricePerUnit = Tarif.TarifPosition.PreisEinheit;

                                    EinheitLg = Rechnung.RGPosLagerbestand.AbrEinheit;

                                    //Neubelegung in Ein-Aus- und Lagerkosten
                                    AbrEinheitLagerbestand = Tarif.TarifPosition.AbrEinheit;
                                    BasisEinheitLagerbestand = Tarif.TarifPosition.BasisEinheit;

                                    //Lagerbestand
                                    decTmp = 0;
                                    strTmp = string.Empty;
                                    //Die Abfrage ermittelt direkt den kompletten Lagerbestand für den Zeitraum
                                    strSQL = GetAnfangsbestand(ref Tarif, true, Tarif.TarifPosition.ID, Tarif.TarifPosition.StaffelPos, false, bUseBKZ);
                                    strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                                    decTmp = 0;
                                    Decimal.TryParse(strTmp, out decTmp);
                                    Rechnung.RGPosLagerbestand.Menge = decTmp;

                                    //Zugang (Einlagerungen)
                                    decTmp = 0;
                                    strTmp = string.Empty;
                                    strSQL = GetSQLforBestand(ref Tarif, "Einlagerungskosten", true, Tarif.TarifPosition.ID);
                                    strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                                    decTmp = 0;
                                    Decimal.TryParse(strTmp, out decTmp);
                                    Rechnung.RGPosLagerbestand.Zugang = decTmp;

                                    //Abgang (Auslagerungen)
                                    decTmp = 0;
                                    strTmp = string.Empty;
                                    strSQL = GetSQLforBestand(ref Tarif, "Auslagerungskosten", true, Tarif.TarifPosition.ID);
                                    strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                                    decTmp = 0;
                                    Decimal.TryParse(strTmp, out decTmp);
                                    Rechnung.RGPosLagerbestand.Abgang = decTmp;

                                    if (Tarif.TarifPosition.StaffelPos)
                                    {
                                        Rechnung.RGPosLagerbestand.Anfangsbestand = Rechnung.RGPosLagerbestand.Menge;
                                        Rechnung.RGPosLagerbestand.Menge = Rechnung.RGPosLagerbestand.Menge + Rechnung.RGPosLagerbestand.Zugang;
                                        Rechnung.RGPosLagerbestand.Endbestand = Rechnung.RGPosLagerbestand.Anfangsbestand + Rechnung.RGPosLagerbestand.Zugang - Rechnung.RGPosLagerbestand.Abgang;
                                    }
                                    else
                                    {
                                        if (
                                            (Tarif.TarifPosition.zeitraumbezogen) ||
                                            (Tarif.TarifPosition.Lagerdauer > 0)
                                           )
                                        {
                                            Rechnung.RGPosLagerbestand.Anfangsbestand = Anfangsbestand;
                                            Rechnung.RGPosLagerbestand.Endbestand = Rechnung.RGPosLagerbestand.Anfangsbestand + Rechnung.RGPosLagerbestand.Zugang - Rechnung.RGPosLagerbestand.Abgang;
                                        }
                                        else
                                        {
                                            Rechnung.RGPosLagerbestand.Anfangsbestand = Rechnung.RGPosLagerbestand.Menge;
                                            Rechnung.RGPosLagerbestand.Menge = Rechnung.RGPosLagerbestand.Menge + Rechnung.RGPosLagerbestand.Zugang;
                                            Rechnung.RGPosLagerbestand.Endbestand = Rechnung.RGPosLagerbestand.Anfangsbestand + Rechnung.RGPosLagerbestand.Zugang - Rechnung.RGPosLagerbestand.Abgang;
                                        }
                                    }

                                    //Artikel Anfangsbestand
                                    DataTable dtArtIDTmp = new DataTable();

                                    strSQLArtikel = string.Empty;
                                    strSQLArtikel = GetAnfangsbestand(ref Tarif, false, Tarif.TarifPosition.ID, true, false, bUseBKZ);
                                    dtTmp = new DataTable();
                                    dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelAnfangsbestand");
                                    MergeDataTable(ref dtArtIDTmp, dtTmp);


                                    ////QuantityBase
                                    switch (Tarif.TarifPosition.QuantityCalcBase)
                                    {
                                        case clsTarifPosition.const_QuantityBase_Einlagerung:
                                            //Lagermenge wird auf Einlagerungsmenge gesetzt
                                            Rechnung.RGPosLagerbestand.Menge = Rechnung.RGPosLagerbestand.Zugang;
                                            dtArtIDTmp.Rows.Clear();
                                            strSQLArtikel = string.Empty;
                                            strSQLArtikel = GetSQLforBestand(ref Tarif, clsFaktLager.const_Abrechnungsart_Einlagerung, false, Tarif.TarifPosition.ID);
                                            dtTmp = new DataTable();
                                            dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelEinlagerung");
                                            break;
                                        case clsTarifPosition.const_QuantityBase_Auslagerung:
                                            Rechnung.RGPosLagerbestand.Menge = Rechnung.RGPosLagerbestand.Abgang;
                                            dtArtIDTmp.Rows.Clear();
                                            strSQLArtikel = string.Empty;
                                            strSQLArtikel = GetSQLforBestand(ref Tarif, clsFaktLager.const_Abrechnungsart_Auslagerung, false, Tarif.TarifPosition.ID);
                                            dtTmp = new DataTable();
                                            dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelAuslagerung");
                                            break;

                                        case clsTarifPosition.const_QuantityBase_Retoure:
                                            break;

                                        case clsTarifPosition.const_QuantityBase_Verlagerung:
                                            break;

                                        case clsTarifPosition.const_QuantityBase_Umbuchung:
                                            break;

                                        case clsTarifPosition.const_QuantityBase_Standard:
                                            strSQLArtikel = string.Empty;
                                            strSQLArtikel = GetSQLforBestand(ref Tarif, clsFaktLager.const_Abrechnungsart_Einlagerung, false, Tarif.TarifPosition.ID);
                                            dtTmp = new DataTable();
                                            dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelEinlagerung");
                                            break;

                                        default:
                                            strSQLArtikel = string.Empty;
                                            strSQLArtikel = GetSQLforBestand(ref Tarif, clsFaktLager.const_Abrechnungsart_Einlagerung, false, Tarif.TarifPosition.ID);
                                            dtTmp = new DataTable();
                                            dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelEinlagerung");
                                            break;
                                    }

                                    MergeDataTable(ref dtArtIDTmp, dtTmp);
                                    List<int> tmpArtInDtDistinctArtId = new List<int>();
                                    DataTable dtDistinctArtID = dtArtIDTmp.Clone();
                                    dtDistinctArtID.Rows.Clear();

                                    Fakt_CalculateArticlePosition faktCalc = new Fakt_CalculateArticlePosition(dtArtIDTmp, Tarif, VonZeitraum, BisZeitraum);
                                    dtDistinctArtID = faktCalc.dtDistinctArtID;
                                    if (dtDistinctArtID.Rows.Count > 0)
                                    {
                                        MergeDataTable(ref dtArtikelLagerbestand, dtDistinctArtID.DefaultView.ToTable(true, "ID", "TarifPosID", "Abrechnungsart", "Menge", "Preis", "CalcModus", "Dauer", "Kosten", "PricePerUnitFactor"));

                                        if (BisZeitraum > new DateTime(2025, 4, 30))
                                        {
                                            //Ermitteln der Lagertage
                                            object SumDauer = new object();
                                            object SumMenge = new object();
                                            object SumPricePerUnitFactor = new object();
                                            object SumKosten = new object();

                                            string strFilter = "CalcModus=" + (int)Tarif.TarifPosition.CalcModus;
                                            //dtArtikelAnfangsbestand.DefaultView.RowFilter = strFilter;
                                            if (dtArtikelLagerbestand.Rows.Count > 0)
                                            {
                                                SumDauer = dtArtikelLagerbestand.Compute("SUM(Dauer)", strFilter);
                                                SumMenge = dtArtikelLagerbestand.Compute("SUM(Menge)", strFilter);
                                                SumPricePerUnitFactor = dtArtikelLagerbestand.Compute("SUM(PricePerUnitFactor)", strFilter);
                                                SumKosten = dtArtikelLagerbestand.Compute("SUM(Kosten)", strFilter);
                                            }
                                            else
                                            {
                                                SumDauer = 0;
                                                SumMenge = 0;
                                                SumPricePerUnitFactor = 0;
                                            }
                                            Int32 iTmp = 0;
                                            Int32.TryParse(SumDauer.ToString(), out iTmp);
                                            Rechnung.RGPosLagerbestand.CalcModValue = iTmp;

                                            decTmp = 0;
                                            Decimal.TryParse(SumMenge.ToString(), out decTmp);
                                            Rechnung.RGPosLagerbestand.Menge = decTmp;

                                            decTmp = 0;
                                            Decimal.TryParse(SumPricePerUnitFactor.ToString(), out decTmp);
                                            Rechnung.RGPosLagerbestand.PricePerUnitFactor = decTmp;
                                        }
                                    }
                                    //Berechnungen
                                    //CalcTarifPosition(ref Rechnung.RGPosLagerbestand, ref Tarif.TarifPosition, ref iPos, strAbrArt);
                                    CalcTarifPosition(ref Rechnung.RGPosLagerbestand, ref Tarif.TarifPosition, ref iPos, strAbrArt, dtArtikelLagerbestand);
                                }
                                break;

                            //case "LagerTransportkosten":
                            case const_Abrechnungsart_LagerTransportkosten:
                                if (Tarif.TarifPosition.aktiv)
                                {
                                    //Tarifwerte zuweisen
                                    Rechnung.RGPosLagerTransporte = new clsRGPositionen();
                                    Rechnung.RGPosLagerTransporte._GL_User = this._GL_User;
                                    Rechnung.RGPosLagerTransporte.AbrechnungsArt = strAbrArt;
                                    Rechnung.RGPosLagerTransporte.Tariftext = Tarif.Tarifname;
                                    Rechnung.RGPosLagerTransporte.CalcModValue = Tarif.TarifPosition.CalcModValue;
                                    Rechnung.RGPosLagerTransporte.CalcModus = Tarif.TarifPosition.CalcModus;
                                    Rechnung.RGPosLagerTransporte.AbrEinheit = Tarif.TarifPosition.AbrEinheit;
                                    Rechnung.RGPosLagerTransporte.TarifPricePerUnit = Tarif.TarifPosition.PreisEinheit;

                                    EinheitLgTransport = Rechnung.RGPosLagerTransporte.AbrEinheit;

                                    //Bestand     
                                    decimal decBestand = 0;
                                    Tarif.TarifPosition.TransDirection = "IN";
                                    strSQL = GetSQLforBestand(ref Tarif, "LagerTransportkosten", true, Tarif.TarifPosition.ID);
                                    strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                                    decTmp = 0;
                                    Decimal.TryParse(strTmp, out decTmp);
                                    decBestand = decTmp;

                                    strSQL = string.Empty;
                                    Tarif.TarifPosition.TransDirection = "OUT";
                                    strSQL = GetSQLforBestand(ref Tarif, "LagerTransportkosten", true, Tarif.TarifPosition.ID);
                                    strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                                    decTmp = 0;
                                    Decimal.TryParse(strTmp, out decTmp);
                                    Rechnung.RGPosLagerTransporte.Menge = decTmp + decBestand;

                                    //Artikel Lagertransport Eingang
                                    DataTable dtArtIDTemp = new DataTable();
                                    Tarif.TarifPosition.TransDirection = "IN";
                                    strSQLArtikel = string.Empty;
                                    strSQLArtikel = GetSQLforBestand(ref Tarif, "LagerTransportkosten", false, Tarif.TarifPosition.ID);
                                    dtTmp = new DataTable();
                                    dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "LagerTransportkostenEingang");
                                    System.Data.DataColumn col = new System.Data.DataColumn("TransDirection", System.Type.GetType("System.String"));
                                    col.DefaultValue = Tarif.TarifPosition.TransDirection;
                                    dtTmp.Columns.Add(col);
                                    MergeDataTable(ref dtArtIDTemp, dtTmp);

                                    //Artikel Lagertransport Ausgang
                                    Tarif.TarifPosition.TransDirection = "OUT";
                                    strSQLArtikel = string.Empty;
                                    strSQLArtikel = GetSQLforBestand(ref Tarif, "LagerTransportkosten", false, Tarif.TarifPosition.ID);
                                    dtTmp = new DataTable();
                                    dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "LagerTransportkostenAusgang");
                                    col = new System.Data.DataColumn("TransDirection", System.Type.GetType("System.String"));
                                    col.DefaultValue = Tarif.TarifPosition.TransDirection;
                                    dtTmp.Columns.Add(col);
                                    MergeDataTable(ref dtArtIDTemp, dtTmp);
                                    if (dtArtIDTemp.Rows.Count > 0)
                                    {
                                        dtArtIDTemp.DefaultView.RowFilter = "TarifPosID=" + Tarif.TarifPosition.ID.ToString();
                                        DataTable dtDistinctArtID = dtArtIDTemp.DefaultView.ToTable(true, "ID", "TarifPosID", "Abrechnungsart", "TransDirection");
                                        MergeDataTable(ref dtArtikelLagerTransporte, dtDistinctArtID);
                                    }

                                    //Berechnungen
                                    //CalcTarifPosition(ref Rechnung.RGPosLagerTransporte, ref Tarif.TarifPosition, ref iPos, strAbrArt);
                                    CalcTarifPosition(ref Rechnung.RGPosLagerTransporte, ref Tarif.TarifPosition, ref iPos, strAbrArt, dtArtikelLagerTransporte);
                                }
                                break;

                            //case "Sperrlagerkosten":
                            case const_Abrechnungsart_SPL:
                                if (Tarif.TarifPosition.aktiv)
                                {
                                    //Tarifwerte zuweisen
                                    Rechnung.RGPosSperrlager = new clsRGPositionen();
                                    Rechnung.RGPosSperrlager._GL_User = this._GL_User;
                                    Rechnung.RGPosSperrlager.AbrechnungsArt = strAbrArt;
                                    Rechnung.RGPosSperrlager.Tariftext = Tarif.Tarifname;
                                    Rechnung.RGPosSperrlager.CalcModValue = Tarif.TarifPosition.CalcModValue;
                                    Rechnung.RGPosSperrlager.CalcModus = Tarif.TarifPosition.CalcModus;
                                    Rechnung.RGPosSperrlager.AbrEinheit = Tarif.TarifPosition.AbrEinheit;
                                    Rechnung.RGPosSperrlager.TarifPricePerUnit = Tarif.TarifPosition.PreisEinheit;

                                    EinheitSPL = Rechnung.RGPosSperrlager.AbrEinheit;

                                    //Bestand                    
                                    strSQL = GetSQLforBestand(ref Tarif, strAbrArt, true, Tarif.TarifPosition.ID);
                                    strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                                    decTmp = 0;
                                    Decimal.TryParse(strTmp, out decTmp);
                                    Rechnung.RGPosSperrlager.Menge = decTmp;
                                    //Artikel
                                    strSQLArtikel = string.Empty;
                                    strSQLArtikel = GetSQLforBestand(ref Tarif, strAbrArt, false, Tarif.TarifPosition.ID);

                                    dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelSPL");
                                    MergeDataTable(ref dtArtikelSperrlager, dtTmp);

                                    if (dtArtikelSperrlager.Rows.Count > 0)
                                    {
                                        //Ermitteln der Lagertage
                                        object SumDauer = new object();
                                        string strFilter = "CalcModus=" + (int)Tarif.TarifPosition.CalcModus;
                                        //dtArtikelAnfangsbestand.DefaultView.RowFilter = strFilter;
                                        SumDauer = dtArtikelSperrlager.Compute("SUM(Dauer)", strFilter);
                                        Int32 iTmp = 0;
                                        Int32.TryParse(SumDauer.ToString(), out iTmp);
                                        Rechnung.RGPosSperrlager.CalcModValue = iTmp;
                                    }
                                    //Berechnungen
                                    //CalcTarifPosition(ref Rechnung.RGPosSperrlager, ref Tarif.TarifPosition, ref iPos, strAbrArt);
                                    CalcTarifPosition(ref Rechnung.RGPosSperrlager, ref Tarif.TarifPosition, ref iPos, strAbrArt, dtArtikelSperrlager);
                                }
                                break;

                            //case "Direktanlieferung":
                            case const_Abrechnungsart_Direktanlieferung:
                                if (Tarif.TarifPosition.aktiv)
                                {
                                    //Tarifwerte zuweisen
                                    Rechnung.RGPosDirektanlieferung = new clsRGPositionen();
                                    Rechnung.RGPosDirektanlieferung._GL_User = this._GL_User;
                                    Rechnung.RGPosDirektanlieferung.AbrechnungsArt = strAbrArt;
                                    Rechnung.RGPosDirektanlieferung.Tariftext = Tarif.Tarifname;
                                    Rechnung.RGPosDirektanlieferung.CalcModValue = Tarif.TarifPosition.CalcModValue;
                                    Rechnung.RGPosDirektanlieferung.CalcModus = Tarif.TarifPosition.CalcModus;
                                    Rechnung.RGPosDirektanlieferung.AbrEinheit = Tarif.TarifPosition.AbrEinheit;
                                    Rechnung.RGPosDirektanlieferung.TarifPricePerUnit = Tarif.TarifPosition.PreisEinheit;

                                    EinheitDirekt = Rechnung.RGPosDirektanlieferung.AbrEinheit;

                                    //Bestand                    
                                    strSQL = GetSQLforBestand(ref Tarif, strAbrArt, true, Tarif.TarifPosition.ID);
                                    strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                                    decTmp = 0;
                                    Decimal.TryParse(strTmp, out decTmp);
                                    Rechnung.RGPosDirektanlieferung.Menge = decTmp;
                                    //Artikel
                                    strSQLArtikel = string.Empty;
                                    strSQLArtikel = GetSQLforBestand(ref Tarif, strAbrArt, false, Tarif.TarifPosition.ID);
                                    dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelDirektanlieferung");
                                    MergeDataTable(ref dtArtikelDirektanlieferung, dtTmp);

                                    //Berechnungen
                                    //CalcTarifPosition(ref Rechnung.RGPosDirektanlieferung, ref Tarif.TarifPosition, ref iPos, strAbrArt);
                                    CalcTarifPosition(ref Rechnung.RGPosDirektanlieferung, ref Tarif.TarifPosition, ref iPos, strAbrArt, dtArtikelDirektanlieferung);
                                }
                                break;

                            //case "Rücklieferung":
                            case const_Abrechnungsart_Ruecklieferung:
                                if (Tarif.TarifPosition.aktiv)
                                {
                                    //Tarifwerte zuweisen
                                    Rechnung.RGPosRuecklieferung = new clsRGPositionen();
                                    Rechnung.RGPosRuecklieferung._GL_User = this._GL_User;
                                    Rechnung.RGPosRuecklieferung.AbrechnungsArt = strAbrArt;
                                    Rechnung.RGPosRuecklieferung.Tariftext = Tarif.Tarifname;
                                    Rechnung.RGPosRuecklieferung.CalcModValue = Tarif.TarifPosition.CalcModValue;
                                    Rechnung.RGPosRuecklieferung.CalcModus = Tarif.TarifPosition.CalcModus;
                                    Rechnung.RGPosRuecklieferung.AbrEinheit = Tarif.TarifPosition.AbrEinheit;
                                    Rechnung.RGPosRuecklieferung.TarifPricePerUnit = Tarif.TarifPosition.PreisEinheit;

                                    EinheitRL = Rechnung.RGPosRuecklieferung.AbrEinheit;

                                    //Bestand                    
                                    strSQL = GetSQLforBestand(ref Tarif, strAbrArt, true, Tarif.TarifPosition.ID);
                                    strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                                    decTmp = 0;
                                    Decimal.TryParse(strTmp, out decTmp);
                                    Rechnung.RGPosRuecklieferung.Menge = decTmp;
                                    //Artikel
                                    strSQLArtikel = string.Empty;
                                    strSQLArtikel = GetSQLforBestand(ref Tarif, strAbrArt, false, Tarif.TarifPosition.ID);
                                    dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelRuecklieferung");
                                    MergeDataTable(ref dtArtikelRuecklieferung, dtTmp);

                                    //Berechnungen
                                    //CalcTarifPosition(ref Rechnung.RGPosRuecklieferung, ref Tarif.TarifPosition, ref iPos, strAbrArt);
                                    CalcTarifPosition(ref Rechnung.RGPosRuecklieferung, ref Tarif.TarifPosition, ref iPos, strAbrArt, dtArtikelRuecklieferung);
                                }
                                break;

                            //case "Vorfracht":
                            case const_Abrechnungsart_Vorfracht:
                                if (Tarif.TarifPosition.aktiv)
                                {
                                    //Tarifwerte zuweisen
                                    Rechnung.RGPosVorfracht = new clsRGPositionen();
                                    Rechnung.RGPosVorfracht._GL_User = this._GL_User;
                                    Rechnung.RGPosVorfracht.AbrechnungsArt = strAbrArt;
                                    Rechnung.RGPosVorfracht.Tariftext = Tarif.Tarifname;
                                    Rechnung.RGPosVorfracht.CalcModValue = Tarif.TarifPosition.CalcModValue;
                                    Rechnung.RGPosVorfracht.CalcModus = Tarif.TarifPosition.CalcModus;
                                    Rechnung.RGPosVorfracht.AbrEinheit = Tarif.TarifPosition.AbrEinheit;
                                    Rechnung.RGPosVorfracht.TarifPricePerUnit = Tarif.TarifPosition.PreisEinheit;

                                    EinheitVorfracht = Rechnung.RGPosVorfracht.AbrEinheit;

                                    //Bestand                    
                                    strSQL = GetSQLforBestand(ref Tarif, strAbrArt, true, Tarif.TarifPosition.ID);
                                    strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                                    decTmp = 0;
                                    Decimal.TryParse(strTmp, out decTmp);
                                    Rechnung.RGPosVorfracht.Menge = decTmp;
                                    //Artikel
                                    strSQLArtikel = string.Empty;
                                    strSQLArtikel = GetSQLforBestand(ref Tarif, strAbrArt, false, Tarif.TarifPosition.ID);
                                    dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelRuecklieferung");
                                    MergeDataTable(ref dtArtikelVorfracht, dtTmp);

                                    //Berechnungen
                                    //CalcTarifPosition(ref Rechnung.RGPosVorfracht, ref Tarif.TarifPosition, ref iPos, strAbrArt);
                                    CalcTarifPosition(ref Rechnung.RGPosVorfracht, ref Tarif.TarifPosition, ref iPos, strAbrArt, dtArtikelVorfracht);
                                }
                                break;
                            //Gleisstellgebühr
                            case const_Abrechnungsart_Gleisstellgebuehr:
                                if (Tarif.TarifPosition.aktiv)
                                {
                                    Rechnung.RGPosGleiskosten = new clsRGPositionen();
                                    Rechnung.RGPosGleiskosten._GL_User = this._GL_User;
                                    Rechnung.RGPosGleiskosten.AbrechnungsArt = strAbrArt;
                                    Rechnung.RGPosGleiskosten.Tariftext = Tarif.Tarifname;
                                    Rechnung.RGPosGleiskosten.CalcModValue = Tarif.TarifPosition.CalcModValue;
                                    Rechnung.RGPosGleiskosten.CalcModus = Tarif.TarifPosition.CalcModus;
                                    Rechnung.RGPosGleiskosten.AbrEinheit = Tarif.TarifPosition.AbrEinheit;
                                    Rechnung.RGPosGleiskosten.TarifPricePerUnit = Tarif.TarifPosition.PreisEinheit;

                                    EinheitGleiskosten = Rechnung.RGPosGleiskosten.AbrEinheit;

                                    //Bestand                    
                                    strSQL = GetSQLforBestand(ref Tarif, strAbrArt, true, Tarif.TarifPosition.ID);

                                    switch (Tarif.TarifPosition.DatenfeldArtikel)
                                    {
                                        case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Eingang_WaggonNo:
                                            DataTable dtQuantity = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Gleisstellgebuehr", "Gleisstellgebühr", BenutzerID);
                                            decTmp = 0;
                                            Decimal.TryParse(dtQuantity.Rows.Count.ToString(), out decTmp);
                                            Rechnung.RGPosGleiskosten.Menge = decTmp;
                                            break;
                                        default:
                                            strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                                            decTmp = 0;
                                            Decimal.TryParse(strTmp, out decTmp);
                                            Rechnung.RGPosGleiskosten.Menge = decTmp;
                                            break;
                                    }

                                    ////-- Anzahl der Zeilen ergibt die Menge
                                    //DataTable dtQuantity = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Gleisstellgebuehr", "Gleisstellgebühr", BenutzerID);
                                    //decTmp = 0;
                                    //Decimal.TryParse(dtQuantity.Rows.Count.ToString(), out decTmp);
                                    //Rechnung.RGPosGleiskosten.Menge = decTmp;
                                    //Artikel
                                    strSQLArtikel = string.Empty;
                                    strSQLArtikel = GetSQLforBestand(ref Tarif, strAbrArt, false, Tarif.TarifPosition.ID);
                                    dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelGleisstellgebühr");
                                    MergeDataTable(ref dtArtikelGleis, dtTmp);

                                    //Berechnungen
                                    //CalcTarifPosition(ref Rechnung.RGPosGleiskosten, ref Tarif.TarifPosition, ref iPos, strAbrArt);
                                    CalcTarifPosition(ref Rechnung.RGPosGleiskosten, ref Tarif.TarifPosition, ref iPos, strAbrArt, dtArtikelGleis);
                                }
                                break;

                            //Nebenkosten
                            case const_Abrechnungsart_Nebenkosten:
                                if (Tarif.TarifPosition.aktiv)
                                {
                                    Rechnung.RGPosNebenkosten = new clsRGPositionen();
                                    Rechnung.RGPosNebenkosten._GL_User = this._GL_User;
                                    Rechnung.RGPosNebenkosten.AbrechnungsArt = strAbrArt;
                                    Rechnung.RGPosNebenkosten.Tariftext = Tarif.Tarifname;
                                    Rechnung.RGPosNebenkosten.CalcModValue = Tarif.TarifPosition.CalcModValue;
                                    Rechnung.RGPosNebenkosten.CalcModus = Tarif.TarifPosition.CalcModus;
                                    Rechnung.RGPosNebenkosten.AbrEinheit = Tarif.TarifPosition.AbrEinheit;
                                    Rechnung.RGPosNebenkosten.TarifPricePerUnit = Tarif.TarifPosition.PreisEinheit;
                                    Rechnung.RGPosNebenkosten.NettoPreis = 0;
                                    Rechnung.RGPosNebenkosten.EinzelPreis = 0;
                                    Rechnung.RGPosNebenkosten.Menge = 1;

                                    //Artikel
                                    strSQLArtikel = string.Empty;
                                    strSQLArtikel = GetSQLArtikelNebenkosten(ref Tarif);
                                    DataTable dtArtIDTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelNebenkosten");
                                    Fakt_CalculateArticlePosition faktCalc = new Fakt_CalculateArticlePosition(dtArtIDTmp, Tarif, VonZeitraum, BisZeitraum);
                                    MergeDataTable(ref dtArtikelNebenkosten, faktCalc.dtDistinctArtID.DefaultView.ToTable(true, "ID", "TarifPosID", "Abrechnungsart", "Menge", "Preis", "CalcModus", "Dauer", "Kosten", "PricePerUnitFactor"));

                                    //Berechnungen
                                    //CalcTarifPosition(ref Rechnung.RGPosNebenkosten, ref Tarif.TarifPosition, ref iPos, strAbrArt);
                                    CalcTarifPosition(ref Rechnung.RGPosNebenkosten, ref Tarif.TarifPosition, ref iPos, strAbrArt, dtArtikelNebenkosten);
                                }
                                break;
                            //Maut
                            case const_Abrechnungsart_Maut:
                                if (Tarif.TarifPosition.aktiv)
                                {
                                    Rechnung.RGPosToll = new clsRGPositionen();
                                    Rechnung.RGPosToll._GL_User = this._GL_User;
                                    Rechnung.RGPosToll.AbrechnungsArt = strAbrArt;
                                    Rechnung.RGPosToll.Tariftext = Tarif.Tarifname;
                                    Rechnung.RGPosToll.CalcModValue = Tarif.TarifPosition.CalcModValue;
                                    Rechnung.RGPosToll.CalcModus = Tarif.TarifPosition.CalcModus;
                                    Rechnung.RGPosToll.AbrEinheit = Tarif.TarifPosition.AbrEinheit;
                                    Rechnung.RGPosToll.TarifPricePerUnit = Tarif.TarifPosition.PreisEinheit;

                                    EinheitToll = Rechnung.RGPosToll.AbrEinheit;

                                    Rechnung.RGPosToll.Anfangsbestand = 0; // Rechnung.RGPosLagerbestand.Menge;
                                    Rechnung.RGPosToll.Menge = 0; // Rechnung.RGPosLagerbestand.Menge + Rechnung.RGPosLagerbestand.Zugang;
                                    Rechnung.RGPosToll.Endbestand = 0; // Rechnung.RGPosLagerbestand.Anfangsbestand + Rechnung.RGPosLagerbestand.Zugang - Rechnung.RGPosLagerbestand.Abgang;

                                    //Artikel Anfangsbestand
                                    DataTable dtArtIDTmp = new DataTable();
                                    ////QuantityBase
                                    switch (Tarif.TarifPosition.QuantityCalcBase)
                                    {
                                        case clsTarifPosition.const_QuantityBase_Einlagerung:
                                            //Zugang (Einlagerungen)
                                            decTmp = 0;
                                            strTmp = string.Empty;
                                            strSQL = GetSQLforBestand(ref Tarif, clsFaktLager.const_Abrechnungsart_Einlagerung, true, Tarif.TarifPosition.ID);
                                            strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                                            decTmp = 0;
                                            Decimal.TryParse(strTmp, out decTmp);
                                            Rechnung.RGPosToll.Zugang = decTmp;
                                            Rechnung.RGPosToll.Menge = Rechnung.RGPosToll.Zugang;

                                            //Artikel
                                            dtArtIDTmp.Rows.Clear();
                                            strSQLArtikel = string.Empty;
                                            strSQLArtikel = GetSQLforBestand(ref Tarif, clsFaktLager.const_Abrechnungsart_Einlagerung, false, Tarif.TarifPosition.ID);
                                            dtTmp = new DataTable();
                                            dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelEinlagerung");
                                            MergeDataTable(ref dtArtIDTmp, dtTmp);
                                            break;

                                        case clsTarifPosition.const_QuantityBase_Auslagerung:
                                            //Abgang (Auslagerungen)
                                            decTmp = 0;
                                            strTmp = string.Empty;
                                            strSQL = GetSQLforBestand(ref Tarif, clsFaktLager.const_Abrechnungsart_Auslagerung, true, Tarif.TarifPosition.ID);
                                            strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                                            decTmp = 0;
                                            Decimal.TryParse(strTmp, out decTmp);
                                            Rechnung.RGPosToll.Abgang = decTmp;
                                            Rechnung.RGPosToll.Menge = Rechnung.RGPosToll.Abgang;

                                            //Artikel
                                            dtArtIDTmp.Rows.Clear();
                                            strSQLArtikel = string.Empty;
                                            strSQLArtikel = GetSQLforBestand(ref Tarif, clsFaktLager.const_Abrechnungsart_Auslagerung, false, Tarif.TarifPosition.ID);
                                            dtTmp = new DataTable();
                                            dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelAuslagerung");
                                            MergeDataTable(ref dtArtIDTmp, dtTmp);
                                            break;

                                        default:
                                            dtArtIDTmp.Rows.Clear();
                                            strSQLArtikel = string.Empty;
                                            strSQLArtikel = GetSQLforBestand(ref Tarif, clsFaktLager.const_Abrechnungsart_Auslagerung, false, Tarif.TarifPosition.ID);
                                            dtTmp = new DataTable();
                                            dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelAuslagerung");
                                            MergeDataTable(ref dtArtIDTmp, dtTmp);
                                            break;
                                    }

                                    Fakt_CalculateArticlePosition faktCalc = new Fakt_CalculateArticlePosition(dtArtIDTmp, Tarif, VonZeitraum, BisZeitraum);
                                    if (faktCalc.dtDistinctArtID.Rows.Count > 0)
                                    {
                                        //MergeDataTable(ref dtArtikelToll, faktCalc.dtDistinctArtID.DefaultView.ToTable(true, "ID", "TarifPosID", "Abrechnungsart", "Menge", "Preis", "CalcModus", "Dauer", "Kosten"));
                                        MergeDataTable(ref dtArtikelToll, faktCalc.dtDistinctArtID.DefaultView.ToTable(true, "ID", "TarifPosID", "Abrechnungsart", "Menge", "Preis", "CalcModus", "Dauer", "Kosten", "PricePerUnitFactor"));
                                    }
                                    CalcTarifPosition(ref Rechnung.RGPosToll, ref Tarif.TarifPosition, ref iPos, strAbrArt, dtArtikelToll);

                                    //MergeDataTable(ref dtArtIDTmp, dtTmp);
                                    ////Berechnungen
                                    //CalcTarifPosition(ref Rechnung.RGPosToll, ref Tarif.TarifPosition, ref iPos, strAbrArt);
                                }
                                break;
                        }
                    }
                }
            }

            //Table Rechnung
            InitDataTableRechnung();
            FormatDataTableRechnung();

            dtBestandsauflistung = new DataTable();

            DataTable dt = new DataTable();
            //Columns definieren
            System.Data.DataColumn col1 = new System.Data.DataColumn("Bestandsart", typeof(String));
            dt.Columns.Add(col1);
            col1 = new System.Data.DataColumn("Tarif", typeof(String));
            dt.Columns.Add(col1);
            col1 = new System.Data.DataColumn("Bestand", typeof(Decimal));
            dt.Columns.Add(col1);
            col1 = new System.Data.DataColumn("Einheit", typeof(String));
            dt.Columns.Add(col1);

            //Umrechnung der Gesamtbestände kg/to usw
            Fakt_ConversionUnitBasicToCalcUnit factConv = new Fakt_ConversionUnitBasicToCalcUnit(BasisEinheitLagerbestand, AbrEinheitLagerbestand);
            decimal decUFact = factConv.ConversionFactor;

            //decimal decUFact = UmrechnungEinheitenVonBasisAufAbrechnungsEinheit(BasisEinheitLagerbestand, AbrEinheitLagerbestand);
            Anfangsbestand = Anfangsbestand * decUFact;

            //decUFact = UmrechnungEinheitenVonBasisAufAbrechnungsEinheit(BasisEinheitLagerbestand, AbrEinheitLagerbestand);
            Endbestand = Endbestand * decUFact;

            //Anfangsbestand
            DataRow row = dt.NewRow();
            row[0] = "Anfangsbestand";
            row[1] = string.Empty;
            row[2] = Anfangsbestand;
            row[3] = EinheitLg;
            dt.Rows.Add(row);
            //InitZwischenBestaende(ref dt, Globals.enumTarifArtLager.Lagerkosten.ToString(), EinheitLg);

            //Einlagerung
            row = dt.NewRow();
            row[0] = "Einlagerung";
            row[1] = string.Empty;
            row[2] = Einlagerung;
            row[3] = EinheitLg;
            dt.Rows.Add(row);
            InitZwischenBestaende(ref dt, enumTarifArtLager.Einlagerungskosten.ToString(), EinheitLg);

            //Lagerbestand
            row = dt.NewRow();
            row[0] = "Lagerbestand";
            row[1] = string.Empty;
            row[2] = Lagerbestand;
            row[3] = EinheitLg;
            dt.Rows.Add(row);
            InitZwischenBestaende(ref dt, enumTarifArtLager.Lagerkosten.ToString(), EinheitLg);

            //Auslagerung
            row = dt.NewRow();
            row[0] = "Auslagerung";
            row[1] = string.Empty;
            row[2] = Auslagerung;
            row[3] = EinheitLg;
            dt.Rows.Add(row);
            InitZwischenBestaende(ref dt, enumTarifArtLager.Auslagerungskosten.ToString(), EinheitLg);

            //Sperrlager
            row = dt.NewRow();
            row[0] = "Sperrlager";
            row[1] = string.Empty;
            row[2] = Sperrlager;
            row[3] = EinheitSPL;
            dt.Rows.Add(row);
            InitZwischenBestaende(ref dt, enumTarifArtLager.Sperrlagerkosten.ToString(), EinheitSPL);

            //LagerTransport
            row = dt.NewRow();
            row[0] = "LagerTransport";
            row[1] = string.Empty;
            row[2] = LagerTransport;
            row[3] = EinheitLgTransport;
            dt.Rows.Add(row);
            InitZwischenBestaende(ref dt, enumTarifArtLager.LagerTransportkosten.ToString(), EinheitLgTransport);

            //Rücklieferungen
            row = dt.NewRow();
            row[0] = "Rücklieferung";
            row[1] = string.Empty;
            row[2] = Ruecklieferung;
            row[3] = EinheitRL;
            dt.Rows.Add(row);
            InitZwischenBestaende(ref dt, enumTarifArtLager.Rücklieferung.ToString(), EinheitRL);

            //Direktanlieferung
            row = dt.NewRow();
            row[0] = "Direktanlieferung";
            row[1] = string.Empty;
            row[2] = Direktanlieferung;
            row[3] = EinheitDirekt;
            dt.Rows.Add(row);
            InitZwischenBestaende(ref dt, enumTarifArtLager.Direktanlieferung.ToString(), EinheitDirekt);

            //Vorfracht
            row = dt.NewRow();
            row[0] = "Vorfracht";
            row[1] = string.Empty;
            row[2] = Vorfracht;
            row[3] = EinheitVorfracht;
            dt.Rows.Add(row);


            //UBExKosten
            row = dt.NewRow();
            row[0] = "Gleisstellgebühr";
            row[1] = string.Empty;
            row[2] = Gleisgebuehr;
            row[3] = EinheitGleiskosten;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = "Nebenkosten";
            row[1] = string.Empty;
            row[2] = Nebenkosten;
            row[3] = EinheitNebenkosten;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = "Maut";
            row[1] = string.Empty;
            row[2] = Toll;
            row[3] = EinheitToll;
            dt.Rows.Add(row);

            //Endbestand
            Endbestand = GetEndbestand(ref Tarif);
            row = dt.NewRow();
            row[0] = "Endbestand";
            row[1] = string.Empty;
            row[2] = Endbestand;
            row[3] = EinheitLg;
            dt.Rows.Add(row);
            //InitZwischenBestaende(ref dt, Globals.enumTarifArtLager.Auslagerungskosten.ToString(), EinheitLg);

            dtBestandsauflistung = dt;
        }
        ///<summary>clsFaktLager / CalcAll</summary>
        ///<remarks></remarks>
        //public void CalcAll(DateTime AbrVon, DateTime AbrBis, DateTime RGDate, ref frmMAIN myMain, decimal myKunde = 0)
        public void CalcAll(DateTime AbrVon, DateTime AbrBis, DateTime RGDate, bool mySingelTarifCalc, decimal myKunde = 0, decimal myTarifID = 0)
        {
            //Fibu KontoNr ermitteln
            clsTableOfAccount FibuKonto = new clsTableOfAccount();
            FibuKonto._GL_User = this._GL_User;
            FibuKonto.FillDataTableOfAccount(true, -1);

            Int32 KtoUmschlag = 0;
            Int32 KtoLagergeld = 0;
            Int32 KtoGleis = 0;
            Int32 KtoSonstiges = 0;

            FibuKonto.DictFibuKto.TryGetValue("Umschlag", out KtoUmschlag);
            FibuKonto.DictFibuKto.TryGetValue("Lagergeld", out KtoLagergeld);
            FibuKonto.DictFibuKto.TryGetValue("Gleisstellgebühr", out KtoGleis);
            FibuKonto.DictFibuKto.TryGetValue("Sonstiges", out KtoSonstiges);

            this.Rechnung = new clsRechnung();
            this.Rechnung._GL_System = this._GL_System;
            this.Rechnung._GL_User = this._GL_User;

            this.VonZeitraum = AbrVon;
            // berechung erfolgt für den aktuellen monat
            if (this.VonZeitraum.Month == DateTime.Now.Month && this.VonZeitraum.Year == DateTime.Now.Year)
            {
                AbrBis = DateTime.Now;
                this.BisZeitraum = DateTime.Now;
                RGDate = DateTime.Now;
            }
            else
                this.BisZeitraum = AbrBis;


            bool bSLVSPauschal = false;
            decimal decPauschalSLVL = 0;


            //Alle RG-Empfänger ermittlen
            DataTable dtKunden = this.dtAbrKunden;

            this.decMaxProgress = dtKunden.Rows.Count;
            string sqlMissingArtikel = string.Empty;
            string sqlMissingArtikelE = string.Empty;
            string sqlMissingArtikelA = string.Empty;
            string sqlMissingArtikelL = string.Empty;
            //myMain.ResetStatusBar();
            //myMain.InitStatusBar(dtKunden.Rows.Count);
            for (Int32 x = 0; x <= dtKunden.Rows.Count - 1; x++)
            {
                sqlMissingArtikel = string.Empty;
                sqlMissingArtikelE = string.Empty;
                sqlMissingArtikelA = string.Empty;
                sqlMissingArtikelL = string.Empty;

                string strStatus = x.ToString() + "/" + dtKunden.Rows.Count.ToString();
                this.strProgress = strStatus;
                //myMain.StatusBarWork(true, strStatus);
                decimal decTmp = 0;
                Decimal.TryParse(dtKunden.Rows[x]["AuftraggeberID"].ToString(), out decTmp);

                if (
                   //(decTmp == 121) ||
                   //    (decTmp == 104) ||
                   //(decTmp==126)
                   (decTmp == myKunde || myKunde == 0)
                   // (decTmp == 24)
                   //(decTmp==113)
                   )
                {
                    bSLVSPauschal = false;
                    decPauschalSLVL = 0;

                    this.Rechnung = new clsRechnung();
                    this.Rechnung._GL_System = this._GL_System;
                    this.Rechnung._GL_User = this._GL_User;
                    clsADR Adr = new clsADR();
                    Adr._GL_User = this._GL_User;
                    Adr.ID = decTmp;
                    Adr.Fill();
                    this.Auftraggeber = Adr.ID;

                    //alle Tarife des Auftraggeber zu ermitteln
                    DataTable dtTarife;
                    if (mySingelTarifCalc)
                    {
                        dtTarife = clsTarif.GetTarifeByTarifID(this._GL_User, myTarifID, this._GL_System.sys_ArbeitsbereichID);
                    }
                    else
                    {
                        dtTarife = clsTarif.GetTarifeByAdrID(this._GL_User, decTmp, true, this.VonZeitraum, this.BisZeitraum, this._GL_System.sys_ArbeitsbereichID);
                    }
                    if (dtTarife.Rows.Count > 0)
                    {
                        dtRGVorschau = new DataTable("Vorschau");
                        dtRGVorschau = Rechnung.GetRechnung();
                        //Reset
                        Einlagerung = 0;
                        Lagerbestand = 0;
                        Auslagerung = 0;
                        Anfangsbestand = 0;
                        Sperrlager = 0;
                        LagerTransport = 0;
                        Direktanlieferung = 0;
                        Ruecklieferung = 0;
                        Endbestand = 0;
                        Vorfracht = 0;

                        dtArtikelEinlagerung = new DataTable("ArtikelEinlagerung");
                        dtArtikelAnfangsbestand = new DataTable("ArtikelAnfangsbestand");
                        dtArtikelAuslagerung = new DataTable("ArtikelAuslagerung");
                        dtArtikelSperrlager = new DataTable("ArtikelSperrlager");
                        dtArtikelLagerTransporte = new DataTable("ArtikelLagerTransporte");
                        dtArtikelDirektanlieferung = new DataTable("ArtikelDirektanlieferung");
                        dtArtikelRuecklieferung = new DataTable("ArtikelRuecklieferung");
                        dtArtikelLagerbestand = new DataTable("ArtikelLagerbestand");
                        dtArtikelVorfracht = new DataTable("ArtikelLagerbestand");
                        dtArtikelNebenkosten = new DataTable("ArtikelNebenkosten");
                        dtArtikelGleis = new DataTable("ArtikelGleisstellgebühr");
                        dtWaggonListe = new DataTable("WaggonList");
                        dtSLVS = new DataTable("SLVS");

                        int TarifPosEK = 0;
                        int TarifPosAK = 0;
                        int TarifPosLK = 0;
                        DataTable dtTMP;

                        for (Int32 t = 0; t <= dtTarife.Rows.Count - 1; t++)
                        {
                            decimal decAuftraggeber = decTmp;
                            decTmp = 0;
                            Decimal.TryParse(dtTarife.Rows[t]["ID"].ToString(), out decTmp);
                            if (decTmp > 0)
                            {
                                //Die Tarife des Kunden ermitteln
                                clsTarif Tarif = new clsTarif();
                                Tarif.InitClass(this._GL_User, decTmp, decAuftraggeber);

                                if (
                                    (Tarif.dtTarifpositionen != null) &&
                                    (Tarif.dtTarifpositionen.Rows.Count > 0)
                                   )
                                {

                                    bSLVSPauschal = Tarif.ISVersPauschal;
                                    decPauschalSLVL = Tarif.VersPreis;
                                    //Der Anfangsbestand muss bereits hier ermittelt werden
                                    decTmp = 0;
                                    string strSQL = string.Empty;
                                    //Artikel Anfangsbestand
                                    string strSQLArtikel = string.Empty;
                                    string strSQLWaggons = string.Empty;
                                    Int32 iPos = 0;
                                    //sortieren für die passende Positionen
                                    Tarif.dtTarifpositionen.DefaultView.Sort = "StaffelPos desc, TPosVerweis, SortIndex";

                                    DataTable dtTmpEinlagerung = new DataTable();
                                    DataTable dtTmpAuslagerung = new DataTable();
                                    DataTable dtTmpAnfangbestand = new DataTable();
                                    DataTable dtTmpSLVS = new DataTable();
                                    DataTable dtTmpVorfracht = new DataTable();
                                    DataTable dtTmpLagerTransportkosten = new DataTable();
                                    DataTable dtTmpDirektanlieferung = new DataTable();
                                    DataTable dtTmpRuecklieferunge = new DataTable();

                                    DataTable dtTmpZSpeicher = Tarif.dtTarifpositionen.DefaultView.ToTable();
                                    Tarif.dtTarifpositionen.Clear();
                                    Tarif.dtTarifpositionen = dtTmpZSpeicher;
                                    //durchlaufen der Tarifpostionen
                                    Int32 iTPosVerweis = -1;

                                    for (Int32 i = 0; i <= Tarif.dtTarifpositionen.Rows.Count - 1; i++)
                                    {
                                        bool IsActive = (bool)Tarif.dtTarifpositionen.Rows[i]["aktiv"];
                                        if (IsActive)
                                        {
                                            string strAbrArt = Tarif.dtTarifpositionen.Rows[i]["Art"].ToString();
                                            //Tarifposition zuweisen 
                                            //Verweis = 0 bedeutet das keine Staffel also immer neue Position
                                            if (iTPosVerweis == 0)
                                            {
                                                iPos++;
                                            }
                                            else
                                            {
                                                //Verweis unterschiedlich -> neue Staffel -> neue Position
                                                if ((Int32)Tarif.dtTarifpositionen.Rows[i]["TPosVerweis"] != iTPosVerweis)
                                                {
                                                    iPos++;
                                                }
                                            }
                                            iTPosVerweis = (Int32)Tarif.dtTarifpositionen.Rows[i]["TPosVerweis"];

                                            Tarif.TarifPosition = new clsTarifPosition();
                                            Tarif.TarifPosition._GL_User = this._GL_User;
                                            Tarif.TarifPosition.ID = (decimal)Tarif.dtTarifpositionen.Rows[i]["ID"];
                                            Tarif.TarifPosition.Fill();

                                            decTmp = 0;
                                            string strTmp = string.Empty;
                                            strSQL = string.Empty;
                                            strSQLArtikel = string.Empty;
                                            switch (strAbrArt)
                                            {
                                                //case "Einlagerungskosten":
                                                case const_Abrechnungsart_Einlagerung:
                                                    //Artikel
                                                    strSQLArtikel = string.Empty;
                                                    strSQLArtikel = GetSQLforBestand(ref Tarif, strAbrArt, false, Tarif.TarifPosition.ID);
                                                    strSQLArtikel = GetSQLforAbmessung(ref Tarif, strSQLArtikel);
                                                    dtTmpEinlagerung = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelEinlagerung");
                                                    MergeDataTable(ref dtArtikelEinlagerung, dtTmpEinlagerung);
                                                    break;

                                                //case "Auslagerungskosten":
                                                case const_Abrechnungsart_Auslagerung:
                                                    //Tarifwerte zuweisen
                                                    //Artikel
                                                    strSQLArtikel = string.Empty;
                                                    strSQLArtikel = GetSQLforBestand(ref Tarif, strAbrArt, false, Tarif.TarifPosition.ID);
                                                    strSQLArtikel = GetSQLforAbmessung(ref Tarif, strSQLArtikel);
                                                    dtTmpAuslagerung = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelAuslagerung");
                                                    if (dtArtikelAuslagerung.Rows.Count == 0)
                                                    {
                                                        dtArtikelAuslagerung = dtTmpAuslagerung.Copy();
                                                        dtArtikelAuslagerung.Clear();
                                                    }
                                                    MergeDataTable(ref dtArtikelAuslagerung, dtTmpAuslagerung);
                                                    break;

                                                //case "Lagerkosten":
                                                case const_Abrechnungsart_Lagerkosten:
                                                    //Artikel Anfangsbestand
                                                    strSQLArtikel = string.Empty;
                                                    strSQLArtikel = GetAnfangsbestand(ref Tarif, false, Tarif.TarifPosition.ID, true, false, bUseBKZ);
                                                    strSQLArtikel = GetSQLforAbmessungAltBestand(ref Tarif, strSQLArtikel);
                                                    string strSQLSLVS;
                                                    string strSQLArtikel2 = string.Empty;

                                                    strSQLSLVS = GetSQLforSLVS(ref Tarif, strSQLArtikel);
                                                    dtTmpSLVS = clsSQLcon.ExecuteSQL_GetDataTable(strSQLSLVS, BenutzerID, "SLVS");
                                                    MergeDataTable(ref dtSLVS, dtTmpSLVS);
                                                    if (Tarif.TarifPosition.Lagerdauer > 0)
                                                    {
                                                        strSQLArtikel = strSQLArtikel.Replace(";", " ");
                                                        // Liste der Artikel mit zu kurzer Lagerdauer
                                                        strSQLArtikel2 = "Select * from (" + strSQLArtikel + ") as x WHERE DauerX <= " + Tarif.TarifPosition.Lagerdauer;
                                                        strSQLArtikel = "Select * from (" + strSQLArtikel + ") as x WHERE DauerX > " + Tarif.TarifPosition.Lagerdauer;
                                                    }
                                                    DataTable dtTmpAnfangbestand2 = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel2, BenutzerID, "ArtikelAnfangsbestand2");
                                                    if (dtTmpAnfangbestand2.Columns.Contains("Preis"))
                                                    {
                                                        int iTmpPos = dtTmpAnfangbestand2.Columns["Preis"].Ordinal;
                                                        System.Data.DataColumn dc = new System.Data.DataColumn("Preis");
                                                        dc.DataType = typeof(decimal);
                                                        dc.DefaultValue = 0.00;
                                                        dtTmpAnfangbestand2.Columns.Remove("Preis");
                                                        dtTmpAnfangbestand2.Columns.Add(dc);
                                                        dtTmpAnfangbestand2.Columns["preis"].SetOrdinal(iTmpPos);
                                                    }
                                                    dtTmpAnfangbestand = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelAnfangsbestand");
                                                    MergeDataTable(ref dtTmpAnfangbestand, dtTmpAnfangbestand2);

                                                    DataTable dtMerge = new DataTable();
                                                    strSQLArtikel = GetSQLforBestand(ref Tarif, "Einlagerungskosten", false, Tarif.TarifPosition.ID);
                                                    strSQLArtikel = GetSQLforAbmessung(ref Tarif, strSQLArtikel);

                                                    strSQLSLVS = GetSQLforSLVS(ref Tarif, strSQLArtikel);
                                                    dtTmpSLVS = clsSQLcon.ExecuteSQL_GetDataTable(strSQLSLVS, BenutzerID, "SLVS");
                                                    MergeDataTable(ref dtSLVS, dtTmpSLVS);
                                                    if (Tarif.TarifPosition.Lagerdauer > 0)
                                                    {
                                                        strSQLArtikel = strSQLArtikel.Replace(";", " ");
                                                        strSQLArtikel2 = "Select * from (" + strSQLArtikel + ") as x WHERE Dauer <= " + Tarif.TarifPosition.Lagerdauer;
                                                        strSQLArtikel = "Select * from (" + strSQLArtikel + ") as x WHERE Dauer > " + Tarif.TarifPosition.Lagerdauer;

                                                    }

                                                    DataTable dtMerge2 = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel2, BenutzerID, "ArtikelLagerkosten");

                                                    dtMerge = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelLagerkosten");
                                                    MergeDataTable(ref dtArtikelAnfangsbestand, dtTmpAnfangbestand);
                                                    // CF AUSKOMMENTIERT DIE ARTIKELMENGE WIRD NEU GEHOLT :: 
                                                    foreach (DataRow row in dtMerge.Rows)
                                                    {
                                                        row["TarifPosID"] = Tarif.TarifPosition.ID;
                                                        row["Abrechnungsart"] = strAbrArt;
                                                        //if ((int)row["Dauer"] > 0)
                                                        //   row["Dauer"] = (int)row["Dauer"] - 1;
                                                        decTmp = 0;
                                                        Decimal.TryParse(Tarif.TarifPosition.PreisEinheit.ToString(), out decTmp);
                                                        row["Preis"] = decTmp;
                                                        row["TarifModus"] = Tarif.Modus;
                                                        decTmp = 0;
                                                        Decimal.TryParse(Tarif.VersPreis.ToString(), out decTmp);
                                                        row["VersPreis"] = decTmp;
                                                        row["BasisEinheit"] = Tarif.TarifPosition.BasisEinheit;
                                                        row["AbrEinheit"] = Tarif.TarifPosition.AbrEinheit;
                                                    }

                                                    MergeDataTable(ref dtArtikelLagerbestand, dtMerge);

                                                    foreach (DataRow row in dtMerge2.Rows)
                                                    {
                                                        row["TarifPosID"] = Tarif.TarifPosition.ID;
                                                        row["Abrechnungsart"] = strAbrArt;
                                                        //if ((int)row["Dauer"] > 0)
                                                        //   row["Dauer"] = (int)row["Dauer"] - 1;
                                                        decTmp = 0;
                                                        row["Preis"] = decTmp;
                                                        row["TarifModus"] = Tarif.Modus;
                                                        decTmp = 0;
                                                        Decimal.TryParse(Tarif.VersPreis.ToString(), out decTmp);
                                                        row["VersPreis"] = decTmp;
                                                        row["BasisEinheit"] = Tarif.TarifPosition.BasisEinheit;
                                                        row["AbrEinheit"] = Tarif.TarifPosition.AbrEinheit;
                                                    }

                                                    MergeDataTable(ref dtArtikelLagerbestand, dtMerge2);
                                                    //Ab hier können die Tmp Table wieder freigegeben werden
                                                    dtTmpEinlagerung = new DataTable();
                                                    dtTmpAuslagerung = new DataTable();
                                                    dtTmpAnfangbestand = new DataTable();
                                                    break;

                                                //case "LagerTransportkosten":
                                                case const_Abrechnungsart_LagerTransportkosten:
                                                    //Artikel Lagertransport Eingang
                                                    Tarif.TarifPosition.TransDirection = "IN";
                                                    strSQLArtikel = string.Empty;
                                                    strSQLArtikel = GetSQLforBestand(ref Tarif, const_Abrechnungsart_LagerTransportkosten, false, Tarif.TarifPosition.ID);
                                                    dtTmpLagerTransportkosten = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "LagerTransportkostenEingang");
                                                    //prüfen ob Spalte bereits existiert
                                                    if (dtTmpLagerTransportkosten.Columns.Contains("TransDirection"))
                                                    {
                                                        System.Data.DataColumn col = new System.Data.DataColumn("TransDirection", System.Type.GetType("System.String"));
                                                        col.DefaultValue = Tarif.TarifPosition.TransDirection;
                                                        dtTmpLagerTransportkosten.Columns.Add(col);
                                                    }
                                                    if (dtArtikelLagerTransporte.Rows.Count == 0)
                                                    {
                                                        dtArtikelLagerTransporte = dtTmpAuslagerung.Copy();
                                                        dtArtikelLagerTransporte.Clear();
                                                    }
                                                    MergeDataTable(ref dtArtikelLagerTransporte, dtTmpLagerTransportkosten);

                                                    //Artikel Lagertransport Ausgang
                                                    Tarif.TarifPosition.TransDirection = "OUT";
                                                    strSQLArtikel = string.Empty;
                                                    strSQLArtikel = GetSQLforBestand(ref Tarif, const_Abrechnungsart_LagerTransportkosten, false, Tarif.TarifPosition.ID);
                                                    dtTmpLagerTransportkosten.Rows.Clear();
                                                    dtTmpLagerTransportkosten = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "LagerTransportkostenAusgang");
                                                    //prüfen ob Spalte bereits existiert
                                                    if (dtTmpLagerTransportkosten.Columns.Contains("TransDirection"))
                                                    {
                                                        System.Data.DataColumn col1 = new System.Data.DataColumn("TransDirection", System.Type.GetType("System.String"));
                                                        col1.DefaultValue = Tarif.TarifPosition.TransDirection;
                                                        dtTmpLagerTransportkosten.Columns.Add(col1);
                                                    }

                                                    if (dtArtikelLagerTransporte.Rows.Count > 0)
                                                    {
                                                        dtArtikelLagerTransporte = dtTmpAuslagerung.Copy();
                                                        dtArtikelLagerTransporte.Clear();
                                                    }
                                                    MergeDataTable(ref dtArtikelLagerTransporte, dtTmpLagerTransportkosten);
                                                    break;
                                                case "Sperrlagerkosten":
                                                case "Direktanlieferung":
                                                case "Rücklieferung":
                                                case "Vorfracht":
                                                    break;

                                                //case "Nebenkosten":
                                                case const_Abrechnungsart_Nebenkosten:
                                                    //Artikel mit Nebenkosten ermitteln
                                                    strSQLArtikel = string.Empty;
                                                    strSQLArtikel = GetSQLArtikelNebenkosten(ref Tarif);
                                                    DataTable dtTmpArtikelNebenkosten = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelNebenkosten");
                                                    MergeDataTable(ref dtArtikelNebenkosten, dtTmpArtikelNebenkosten);
                                                    break;

                                                //case "Gleisstellgebühr":
                                                case const_Abrechnungsart_Gleisstellgebuehr:
                                                    strSQLArtikel = string.Empty;
                                                    strSQLArtikel = GetSQLGleisStellKosten(ref Tarif);
                                                    strSQLWaggons = GetSQLWaggonAnzahl(ref Tarif);
                                                    //strSQLArtikel = GetSQLforAbmessung(ref Tarif, strSQLArtikel);
                                                    DataTable dtTmpGleisKosten = clsSQLcon.ExecuteSQL_GetDataTable(strSQLArtikel, BenutzerID, "ArtikelNebenkosten");
                                                    MergeDataTable(ref dtArtikelGleis, dtTmpGleisKosten);
                                                    DataTable dTmpWaggons = clsSQLcon.ExecuteSQL_GetDataTable(strSQLWaggons, BenutzerID, "ArtikelNebenkosten");
                                                    MergeDataTable(ref dtWaggonListe, dTmpWaggons);
                                                    break;

                                                case const_Abrechnungsart_Maut:

                                                    break;
                                            }
                                        }
                                    }
                                }
                            }//TarifID >0
                        }
                        // EIngelagerte Artikel für die Korrekte Berechnung erst nachträglich Mergen // CF
                        MergeDataTable(ref dtArtikelLagerbestand, dtArtikelAnfangsbestand);

                        /* Auf Umbuchungen prüfen, dabei gibt es folgende Konstellationen:
                         * Neuer Kunde + Einlagerung : Wenn der Artikel im selben Monat eingelagert worden ist, fallen kosten an
                         * Neuer Kunde + Auslagerung : Kunde zahlt sobald der Artikel ausgelagert wird / wurde
                         * Alter Kunde + Einlagerung : Kunde zahlt falls in diesem Monat eingelagert wurde 
                         * Alter Kunde + Auslagerung : Kunde zahlt falls in diesem Monat ausgelagert worden ist.
                         * Problem doppelt Umgebuchte Artikel?
                         * */
                        //foreach (DataRow row in dtArtikelEinlagerung.Rows)

                        dtArtikelEinlagerungUmbuchungGratis = dtArtikelEinlagerung.Copy();
                        dtArtikelEinlagerungUmbuchungGratis.Rows.Clear();
                        dtArtikelAuslagerungUmbuchungGratis = dtArtikelAuslagerung.Copy();
                        dtArtikelAuslagerungUmbuchungGratis.Rows.Clear();
                        for (int i = 0; i < dtArtikelEinlagerung.Rows.Count; i++)
                        {
                            DataRow row = dtArtikelEinlagerung.Rows[i];
                            decTmp = 0;
                            Decimal.TryParse(row["ID"].ToString(), out decTmp);
                            clsArtikel art = new clsArtikel();
                            art.InitClass(this._GL_User, this._GL_System);
                            art.ID = decTmp;
                            art.GetArtikeldatenByTableID();
                            // neuer Artikel
                            string strMonatCheck = string.Empty;
                            string strMonatAbr = AbrVon.Year.ToString() + AbrVon.Month.ToString();
                            clsUmbuchung ub = new clsUmbuchung();
                            ub.BenutzerID = this.BenutzerID;
                            ub.MandantenID = this.MandantenID;
                            //if ((!art.Umbuchung) & (art.ArtIDAlt > 0))
                            if (art.isUBNew) // es ist der neue artikel
                            {
                                if (art.isUBNew_CalcEinlagerung)
                                {
                                    clsArtikel AltArt = new clsArtikel();
                                    AltArt.InitClass(this._GL_User, this._GL_System);
                                    AltArt.ID = art.UBAltArtID;
                                    AltArt.GetArtikeldatenByTableID();
                                    AltArt.Eingang = new clsLEingang();
                                    AltArt.Eingang.LEingangTableID = AltArt.LEingangTableID;
                                    AltArt.Eingang.FillEingang();
                                    strMonatCheck = AltArt.Eingang.LEingangDate.Year.ToString() + AltArt.Eingang.LEingangDate.Month.ToString();
                                    if (strMonatAbr == strMonatCheck)
                                    {
                                        // belasse den Artikel in der DataTable
                                    }
                                    else
                                    {
                                        // Artikel wurde bereits vorher eingelager
                                        //DataRow desRow = dtArtikelEinlagerungUmbuchungGratis.NewRow();
                                        //desRow.ItemArray = row.ItemArray.Clone() as object[];
                                        //dtArtikelEinlagerungUmbuchungGratis.Rows.Add(desRow);
                                        //dtArtikelEinlagerung.Rows.Remove(row);
                                        //i--;
                                    }
                                }
                                else
                                {
                                    DataRow desRow = dtArtikelEinlagerungUmbuchungGratis.NewRow();
                                    desRow.ItemArray = row.ItemArray.Clone() as object[];
                                    dtArtikelEinlagerungUmbuchungGratis.Rows.Add(desRow);
                                    dtArtikelEinlagerung.Rows.Remove(row);
                                    i--;
                                }
                            }
                            if (art.isUBAlt)
                            {
                                if (!art.isUBAlt_CalcEinlagerung)
                                {
                                    DataRow desRow = dtArtikelEinlagerungUmbuchungGratis.NewRow();
                                    desRow.ItemArray = row.ItemArray.Clone() as object[];
                                    dtArtikelEinlagerungUmbuchungGratis.Rows.Add(desRow);
                                    dtArtikelEinlagerung.Rows.Remove(row);
                                    i--;
                                }
                            }

                        }
                        for (int i = 0; i < dtArtikelAuslagerung.Rows.Count; i++)
                        {
                            DataRow row = dtArtikelAuslagerung.Rows[i];
                            decTmp = 0;
                            Decimal.TryParse(row["ID"].ToString(), out decTmp);
                            clsArtikel art = new clsArtikel();
                            art.InitClass(this._GL_User, this._GL_System);
                            art.ID = decTmp;
                            art.GetArtikeldatenByTableID();
                            // neuer Artikel
                            string strMonatCheck = string.Empty;
                            string strMonatAbr = AbrVon.Year.ToString() + AbrVon.Month.ToString();
                            if (art.isUBAlt)
                            {
                                if (art.isUBAlt_CalcAuslagerung)
                                {
                                    clsArtikel NeuArt = new clsArtikel();
                                    NeuArt.InitClass(this._GL_User, this._GL_System);
                                    NeuArt.ID = art.UBNewArtID;
                                    NeuArt.GetArtikeldatenByTableID();
                                    NeuArt.Ausgang = new clsLAusgang();
                                    NeuArt.Ausgang.LAusgangTableID = NeuArt.LAusgangTableID;
                                    NeuArt.Ausgang.FillAusgang();
                                    strMonatCheck = NeuArt.Ausgang.LAusgangsDate.Year.ToString() + NeuArt.Ausgang.LAusgangsDate.Month.ToString();
                                    if (strMonatAbr == strMonatCheck)
                                    {
                                        // belasse den Artikel in der DataTable
                                    }
                                    else
                                    {
                                        // Wenn der Haken gesetzt ist soll definitiv bezahlt werden
                                        //// Artikel wurde bereits vorher eingelagert
                                        //DataRow desRow = dtArtikelAuslagerungUmbuchungGratis.NewRow();
                                        //desRow.ItemArray = row.ItemArray.Clone() as object[];
                                        //dtArtikelAuslagerungUmbuchungGratis.Rows.Add(desRow);
                                        //dtArtikelAuslagerung.Rows.Remove(row);
                                        //i--;
                                    }
                                }
                                else
                                {
                                    DataRow desRow = dtArtikelAuslagerungUmbuchungGratis.NewRow();
                                    desRow.ItemArray = row.ItemArray.Clone() as object[];
                                    dtArtikelAuslagerungUmbuchungGratis.Rows.Add(desRow);

                                    dtArtikelAuslagerung.Rows.Remove(row);
                                    i--;
                                }

                            }
                            else if (art.isUBNew)
                            {
                                if (!art.isUBNew_CalcAuslagerung)
                                {
                                    DataRow desRow = dtArtikelAuslagerungUmbuchungGratis.NewRow();
                                    desRow.ItemArray = row.ItemArray.Clone() as object[];
                                    dtArtikelAuslagerungUmbuchungGratis.Rows.Add(desRow);
                                    dtArtikelAuslagerung.Rows.Remove(row);
                                    i--;
                                }
                            }
                        }
                        decimal decUFact = 1;

                        Int32 iCountTarifPos = 0;
                        //Artikel für Lagerrechnung für aktuelle Kunden ermittel, jetzt müsse aus den einzelenn 
                        //Artikeltabellen die Summen addiert und zugewiesen werden                    
                        /*****************************************************************
                         *                  Einlagerung
                         * **************************************************************/
                        DataTable dtTmpZP = dtArtikelLagerbestand.Copy();
                        dtTmpZP.TableName = "Tmp";
                        dtTmpZP.Rows.Clear();

                        Rechnung.RGPosEinlagerung = new clsRGPositionen();
                        Rechnung.RGPosEinlagerung._GL_User = this._GL_User;
                        Rechnung.RGPosEinlagerung.Position = iCountTarifPos++; ;
                        Rechnung.RGPosEinlagerung.RGText = enumTarifArtLager.Einlagerungskosten.ToString();
                        Rechnung.RGPosEinlagerung.AbrEinheit = "to";
                        Rechnung.RGPosEinlagerung.AbrechnungsArt = enumTarifArtLager.Einlagerungskosten.ToString();
                        Rechnung.RGPosEinlagerung.Tariftext = string.Empty;
                        Rechnung.RGPosEinlagerung.RGPosText = enumTarifArtLager.Einlagerungskosten.ToString();
                        Rechnung.RGPosEinlagerung.EinzelPreis = 0;
                        Rechnung.RGPosEinlagerung.MargeProzent = 0;
                        Rechnung.RGPosEinlagerung.MargeEuro = 0;
                        Rechnung.RGPosEinlagerung.TarifPosID = 0;
                        Rechnung.RGPosEinlagerung.FibuKto = KtoUmschlag;

                        Einlagerung = 0;
                        decimal decEinlagerungGesamtKosten = 0;
                        decimal decVersPraemieGesamt = 0;
                        foreach (DataRow row in dtArtikelEinlagerung.Rows)
                        {
                            string strBasis = row["BasisEinheit"].ToString();
                            string strAbr = row["AbrEinheit"].ToString();
                            Fakt_ConversionUnitBasicToCalcUnit factConv = new Fakt_ConversionUnitBasicToCalcUnit(strBasis, strAbr);
                            decUFact = factConv.ConversionFactor;

                            //decUFact = UmrechnungEinheitenVonBasisAufAbrechnungsEinheit(strBasis, strAbr);
                            //Menge
                            decimal decTmpMenge = 0;
                            Decimal.TryParse(row["Menge"].ToString(), out decTmpMenge);
                            decTmpMenge = decTmpMenge * decUFact;
                            Einlagerung = Einlagerung + decTmpMenge;
                            //Preis
                            decimal decTmpPreis = 0;
                            Decimal.TryParse(row["Preis"].ToString(), out decTmpPreis);
                            //Lagerdauer Tage
                            Int32 iTmpDauer = 0;
                            Int32.TryParse(row["Dauer"].ToString(), out iTmpDauer);
                            //Kosten
                            decimal decTmpKosten = 0;
                            //Ermitteln der Kosten für den einzelnen Artikel
                            decTmp = 0;
                            Decimal.TryParse(row["ID"].ToString(), out decTmp);
                            clsArtikel art = new clsArtikel();
                            art.InitClass(this._GL_User, this._GL_System);
                            art.ID = decTmp;
                            art.GetArtikeldatenByTableID();
                            decTmpKosten = Math.Round(decTmpMenge * decTmpPreis, 2, MidpointRounding.AwayFromZero);

                            //wenn UB=true dann ist das der original artikel und
                            //die kosten würden für den AltenAuftraggeber anfallen
                            //Umbuchung -> original Datensatz
                            string strMonatCheck = string.Empty;
                            string strMonatAbr = AbrVon.Year.ToString() + AbrVon.Month.ToString();
                            // Original
                            if ((art.Umbuchung) & (art.ArtIDAlt == 0))
                            {
                                //dies ist der alte originale Datensatz 
                                //prüfen Einlagerdatum vom orig. Datensatz
                                strMonatCheck = string.Empty;
                                strMonatCheck = art.Eingang.LEingangDate.Year.ToString() + art.Eingang.LEingangDate.Month.ToString();
                                //prüfen ob die Abrechnung im Eingangmonat stattfindet
                                if (strMonatAbr == strMonatCheck) // CF strMonatAbr=strMonatAbr
                                {
                                    //Einlagerungsmonat im Abrechnungsmonat 
                                    //prüfen auf Kostenzuweisung für den orign. Datensatz
                                    if (art.UB_AltCalcEinlagerung)
                                    {
                                        /* Eigentliche UMlagerung wird nicht berechnet */
                                        ////neue Row anlegen und hinzufügen als Abrechnungsposition für die UB
                                        ////zwischenspeichern in dtTmpEinlagerung;
                                        ////decTmpKosten = 0;
                                        //row["Kosten"] = decTmpKosten;
                                        ////row["IsUBCalc"] = true; // nur auf true, da hier die Row in dtTmpSZ kopiert wird und die Umbuchungssatz markiert
                                        ////dtTmpZP.ImportRow(row);
                                        //DataRow tmpRow = dtTmpZP.NewRow();
                                        //tmpRow.ItemArray = row.ItemArray;
                                        //tmpRow["IsUBCalc"] = true;
                                        //dtTmpZP.Rows.Add(tmpRow);
                                        ////dtTmpZP.ImportRow(tmpRow1);   
                                        //decTmpKosten = decTmpKosten;   
                                    }
                                }
                            }
                            //Umbuchung -> neuer umgebuchter Datensatz
                            if ((!art.Umbuchung) & (art.ArtIDAlt > 0))
                            {
                                //prüfen Einlagerdatum  neuer umgebuchter Datensatz
                                strMonatCheck = string.Empty;
                                strMonatCheck = art.Ausgang.LAusgangsDate.Year.ToString() + art.Ausgang.LAusgangsDate.Month.ToString();
                                //prüfen ob die Abrechnung im Eingangmonat stattfindet
                                if (strMonatAbr == strMonatCheck)
                                {
                                    //Auslagerung im Abrechnungsmonat  = Umbuchungsdatum
                                    //prüfen auf Kostenzuweisung für neuer umgebuchter Datensatz
                                    if (art.UB_NeuCalcAuslagerung)
                                    {
                                        //neue Row anlegen und hinzufügen als Abrechnungsposition für die UB
                                        //zwischenspeichern in dtTmpEinlagerung;
                                        //row["Kosten"] = decTmpKosten;
                                        //row["IsUBCalc"] = true;//==> bleibt false das hier row die normel Einlagerung beinhaltet
                                        //DataRow tmpRow = dtTmpZP.NewRow();
                                        //tmpRow.ItemArray = row.ItemArray;
                                        //tmpRow["IsUBCalc"] = true;
                                        //dtTmpZP.Rows.Add(tmpRow);
                                    }

                                    if (art.UB_NeuCalcEinlagerung)
                                    {
                                        //neue Row anlegen und hinzufügen als Abrechnungsposition für die UB
                                        //zwischenspeichern in dtTmpEinlagerung;
                                        //row["Kosten"] = decTmpKosten;
                                        //row["IsUBCalc"] = true;//==> bleibt false das hier row die normel Einlagerung beinhaltet
                                        //DataRow tmpRow = dtTmpZP.NewRow();
                                        //tmpRow.ItemArray = row.ItemArray;
                                        //tmpRow["IsUBCalc"] = true;
                                        //dtTmpZP.Rows.Add(tmpRow);
                                    }
                                    else
                                    {
                                        decTmpKosten = 0;
                                    }
                                }
                            }
                            row["Kosten"] = decTmpKosten;
                            decEinlagerungGesamtKosten = decEinlagerungGesamtKosten + decTmpKosten;
                        }
                        dtTmpZP.Rows.Clear();

                        Int32 iStellen = 3;
                        Einlagerung = Math.Round(Einlagerung, iStellen);
                        Rechnung.RGPosEinlagerung.Menge = Einlagerung;
                        Rechnung.RGPosEinlagerung.EinzelPreis = 0;
                        Rechnung.RGPosEinlagerung.NettoPreis = decEinlagerungGesamtKosten;
                        Rechnung.RGPosEinlagerung.Zugang = Rechnung.RGPosEinlagerung.Menge;
                        Rechnung.RGPosEinlagerung.Endbestand = Rechnung.RGPosEinlagerung.Anfangsbestand + Rechnung.RGPosEinlagerung.Zugang - Rechnung.RGPosEinlagerung.Abgang;
                        AddNewRowToDataTableRGVorschau(ref Rechnung.RGPosEinlagerung);

                        /*****************************************************************
                         *                  Auslagerung
                         * **************************************************************/
                        Rechnung.RGPosAuslagerung = new clsRGPositionen();
                        Rechnung.RGPosAuslagerung._GL_User = this._GL_User;
                        Rechnung.RGPosAuslagerung.AbrechnungsArt = enumTarifArtLager.Auslagerungskosten.ToString();
                        Rechnung.RGPosAuslagerung.Tariftext = string.Empty;
                        Rechnung.RGPosAuslagerung.RGText = enumTarifArtLager.Auslagerungskosten.ToString();
                        Rechnung.RGPosAuslagerung.RGPosText = enumTarifArtLager.Auslagerungskosten.ToString();
                        Rechnung.RGPosAuslagerung.EinzelPreis = 0;
                        Rechnung.RGPosAuslagerung.MargeProzent = 0;
                        //myRGPos.RGText = myTarifPos.Beschreibung;
                        Rechnung.RGPosAuslagerung.Position = iCountTarifPos++; ;
                        Rechnung.RGPosAuslagerung.TarifPosID = 0;
                        Rechnung.RGPosAuslagerung.AbrEinheit = "to";
                        Rechnung.RGPosAuslagerung.EinzelPreis = 0;
                        Rechnung.RGPosAuslagerung.MargeProzent = 0;
                        Rechnung.RGPosAuslagerung.MargeEuro = 0;
                        Rechnung.RGPosAuslagerung.FibuKto = KtoUmschlag;

                        decimal decAuslagerungGesamtKosten = 0;
                        Auslagerung = 0;
                        foreach (DataRow row in dtArtikelAuslagerung.Rows)
                        {

                            string strBasis = row["BasisEinheit"].ToString();
                            string strAbr = row["AbrEinheit"].ToString();
                            //decUFact = UmrechnungEinheitenVonBasisAufAbrechnungsEinheit(strBasis, strAbr);
                            Fakt_ConversionUnitBasicToCalcUnit factConv = new Fakt_ConversionUnitBasicToCalcUnit(strBasis, strAbr);
                            decUFact = factConv.ConversionFactor;

                            //Menge
                            decimal decTmpMenge = 0;
                            Decimal.TryParse(row["Menge"].ToString(), out decTmpMenge);
                            decTmpMenge = decTmpMenge * decUFact;
                            Auslagerung = Auslagerung + decTmpMenge;
                            //Preis
                            decimal decTmpPreis = 0;
                            Decimal.TryParse(row["Preis"].ToString(), out decTmpPreis);
                            //Lagerdauer Tage
                            Int32 iTmpDauer = 0;
                            Int32.TryParse(row["Dauer"].ToString(), out iTmpDauer);
                            //Kosten
                            decimal decTmpKosten = 0;

                            //Ermitteln der Kosten für den einzelnen Artikel
                            decTmp = 0;
                            Decimal.TryParse(row["ID"].ToString(), out decTmp);
                            clsArtikel art = new clsArtikel();
                            art.InitClass(this._GL_User, this._GL_System);
                            art.ID = decTmp;
                            art.GetArtikeldatenByTableID();

                            //Fällt bei der Auslagerung weg, da diese Kosten nur einmalig auftreten

                            Int32 iTmpModus = 1;
                            Int32.TryParse(row["TarifModus"].ToString(), out iTmpModus);
                            switch (iTmpModus)
                            {
                                case 1:
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    break;
                            }
                            decTmpKosten = Math.Round(decTmpMenge * decTmpPreis, 2, MidpointRounding.AwayFromZero);
                            //wenn UB=true dann ist das der original artikel und
                            //die kosten würden für den AltenAuftraggeber anfallen
                            //Umbuchung -> original Datensatz
                            string strMonatCheck = string.Empty;
                            string strMonatAbr = AbrVon.Year.ToString() + AbrVon.Month.ToString();

                            if ((art.Umbuchung) & (art.ArtIDAlt == 0))
                            {
                                //Einlagerungsmonat im Abrechnungsmonat 
                                //prüfen auf Kostenzuweisung für den orign. Datensatz

                                if (art.UB_AltCalcAuslagerung)
                                {
                                    /* Umlagerung wird generall nicht berechnet */
                                    ////neue Row anlegen und hinzufügen als Abrechnungsposition für die UB
                                    ////zwischenspeichern in dtTmpEinlagerung;
                                    ////row["Kosten"] = decTmpKosten;
                                    //DataRow tmpRow = dtTmpZP.NewRow();
                                    //tmpRow.ItemArray = row.ItemArray;
                                    //tmpRow["IsUBCalc"] = true;
                                    //dtTmpZP.Rows.Add(tmpRow);
                                }
                                else
                                {
                                    decTmpKosten = 0;
                                }

                            }
                            //Umbuchung -> neuer umgebuchter Datensatz
                            if ((!art.Umbuchung) & (art.ArtIDAlt > 0))
                            {
                                if (art.UB_NeuCalcAuslagerung)
                                {
                                    //neue Row anlegen und hinzufügen als Abrechnungsposition für die UB
                                    //zwischenspeichern in dtTmpEinlagerung;
                                    row["Kosten"] = decTmpKosten;
                                    if (dtTmpZP.Columns.Count != row.Table.Columns.Count)
                                    {
                                        dtTmpZP = row.Table.Copy();
                                        dtTmpZP.Clear();
                                    }
                                    DataRow tmpRow = dtTmpZP.NewRow();
                                    tmpRow.ItemArray = row.ItemArray;
                                    tmpRow["IsUBCalc"] = true;
                                    dtTmpZP.Rows.Add(tmpRow);
                                }
                                else
                                {
                                    decTmpKosten = 0;
                                }
                            }
                            row["Kosten"] = decTmpKosten;
                            decAuslagerungGesamtKosten = decAuslagerungGesamtKosten + decTmpKosten;

                        }
                        //MergeDataTable(ref dtArtikelAuslagerung, dtTmpZP);
                        dtTmpZP.Rows.Clear();
                        iStellen = 3;
                        Auslagerung = Math.Round(Auslagerung, iStellen);
                        Rechnung.RGPosAuslagerung.Menge = Auslagerung;
                        Rechnung.RGPosAuslagerung.NettoPreis = decAuslagerungGesamtKosten;
                        Rechnung.RGPosAuslagerung.Zugang = Rechnung.RGPosAuslagerung.Menge;
                        Rechnung.RGPosAuslagerung.Endbestand = Rechnung.RGPosAuslagerung.Anfangsbestand + Rechnung.RGPosAuslagerung.Zugang - Rechnung.RGPosAuslagerung.Abgang;
                        AddNewRowToDataTableRGVorschau(ref Rechnung.RGPosAuslagerung);

                        /*****************************************************************
                         *                  Lagergeld
                         * **************************************************************/
                        Rechnung.RGPosLagerbestand = new clsRGPositionen();
                        Rechnung.RGPosLagerbestand._GL_User = this._GL_User;
                        Rechnung.RGPosLagerbestand.AbrechnungsArt = enumTarifArtLager.Lagerkosten.ToString();
                        Rechnung.RGPosLagerbestand.Tariftext = string.Empty;
                        Rechnung.RGPosLagerbestand.RGText = enumTarifArtLager.Lagerkosten.ToString();
                        Rechnung.RGPosLagerbestand.RGPosText = enumTarifArtLager.Lagerkosten.ToString();
                        Rechnung.RGPosLagerbestand.EinzelPreis = 0;
                        Rechnung.RGPosLagerbestand.MargeProzent = 0;
                        //myRGPos.RGText = myTarifPos.Beschreibung;
                        Rechnung.RGPosLagerbestand.Position = iCountTarifPos++; ;
                        Rechnung.RGPosLagerbestand.TarifPosID = 0;
                        Rechnung.RGPosLagerbestand.AbrEinheit = "to";
                        Rechnung.RGPosLagerbestand.EinzelPreis = 0;
                        Rechnung.RGPosLagerbestand.MargeProzent = 0;
                        Rechnung.RGPosLagerbestand.MargeEuro = 0;
                        Rechnung.RGPosLagerbestand.FibuKto = KtoLagergeld;

                        decimal decLagerkostenGesamt = 0;
                        Lagerbestand = 0;
                        foreach (DataRow row in dtArtikelLagerbestand.Rows)
                        {
                            string strBasis = row["BasisEinheit"].ToString();
                            string strAbr = row["AbrEinheit"].ToString();
                            //decUFact = UmrechnungEinheitenVonBasisAufAbrechnungsEinheit(strBasis, strAbr);
                            Fakt_ConversionUnitBasicToCalcUnit factConv = new Fakt_ConversionUnitBasicToCalcUnit(strBasis, strAbr);
                            decUFact = factConv.ConversionFactor;

                            //Menge
                            decimal decTmpMenge = 0;
                            Decimal.TryParse(row["Menge"].ToString(), out decTmpMenge);
                            decTmpMenge = decTmpMenge * decUFact;
                            Lagerbestand = Lagerbestand + decTmpMenge;
                            //Preis
                            decimal decTmpPreis = 0;
                            Decimal.TryParse(row["Preis"].ToString(), out decTmpPreis);
                            //Lagerdauer Tage
                            Int32 iTmpDauer = 0;
                            Int32.TryParse(row["Dauer"].ToString(), out iTmpDauer);
                            //Kosten
                            decimal decTmpKosten = 0;

                            //Ermitteln der Kosten für den einzelnen Artikel
                            decTmp = 0;
                            Decimal.TryParse(row["ID"].ToString(), out decTmp);
                            clsArtikel art = new clsArtikel();
                            art.InitClass(this._GL_User, this._GL_System);
                            art.ID = decTmp;
                            art.GetArtikeldatenByTableID();

                            Int32 iTmpModus = 1;
                            Int32.TryParse(row["TarifModus"].ToString(), out iTmpModus);
                            switch (iTmpModus)
                            {
                                case 1:
                                    decTmpKosten = Math.Round(decTmpMenge * decTmpPreis, 2, MidpointRounding.AwayFromZero);
                                    break;

                                case 2:
                                    decTmpKosten = Math.Round(decTmpMenge * decTmpPreis, 2, MidpointRounding.AwayFromZero);
                                    //hat der Artikel einen Ausgang?

                                    //Einlagerung nach dem 15. dann halb
                                    string strCheck = art.Eingang.LEingangDate.Year.ToString() + art.Eingang.LEingangDate.Month.ToString();
                                    strAbr = AbrVon.Year.ToString() + AbrVon.Month.ToString();
                                    if (strCheck == strAbr)
                                    {
                                        if (art.Eingang.LEingangDate.Day > 15)
                                        {
                                            decTmpKosten = Math.Round(decTmpKosten * 1 / 2, 2, MidpointRounding.AwayFromZero);
                                        }
                                        else if (art.LAusgangTableID > 0)
                                        {
                                            //Auslagerungsdatum vor dem 15. des Monats => 1/2 Preis
                                            if (art.Ausgang.LAusgangsDate.Day <= 15)
                                            {
                                                decTmpKosten = Math.Round(decTmpKosten * 1 / 2, 2, MidpointRounding.AwayFromZero);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (art.LAusgangTableID > 0)
                                        {
                                            //Auslagerungsdatum vor dem 15. des Monats => 1/2 Preis
                                            strCheck = art.Ausgang.LAusgangsDate.Year.ToString() + art.Ausgang.LAusgangsDate.Month.ToString();
                                            if (art.Ausgang.LAusgangsDate.Day <= 15 && strCheck == strAbr)
                                            {
                                                decTmpKosten = Math.Round(decTmpKosten * 1 / 2, 2, MidpointRounding.AwayFromZero);
                                            }
                                        }
                                    }
                                    break;

                                case 3:
                                    //Menge * Preis * Dauer * Tagen
                                    decTmpKosten = Math.Round(decTmpMenge * decTmpPreis * iTmpDauer, 2, MidpointRounding.AwayFromZero);
                                    break;
                            }
                            //wenn UB=true dann ist das der original artikel und
                            //die kosten würden für den AltenAuftraggeber anfallen
                            //Umbuchung -> original Datensatz
                            string strMonatCheck = string.Empty;
                            string strMonatAbr = AbrVon.Year.ToString() + AbrVon.Month.ToString();
                            if ((art.Umbuchung) & (art.ArtIDAlt == 0))
                            {
                                //dies ist der alte originale Datensatz 
                                //prüfen Einlagerdatum vom orig. Datensatz
                                //nur Berechnen im Umbuchungsmonat = Auslagerungsmonat
                                strMonatCheck = string.Empty;
                                strMonatCheck = art.Ausgang.LAusgangsDate.Year.ToString() + art.Ausgang.LAusgangsDate.Month.ToString();
                                //prüfen ob die Abrechnung stattfindet
                                if (strMonatAbr == strMonatAbr)
                                {
                                    //Einlagerungsmonat im Abrechnungsmonat 
                                    //prüfen auf Kostenzuweisung für den orign. Datensatz
                                    if (art.UB_AltCalcLagergeld)
                                    {
                                        //neue Row anlegen und hinzufügen als Abrechnungsposition für die UB
                                        //zwischenspeichern in dtTmpEinlagerung;
                                        //decTmpKosten = 0;
                                        //row["Kosten"] = decTmpKosten;
                                        //row["IsUBCalc"] = true; // nur auf true, da hier die Row in dtTmpSZ kopiert wird und die Umbuchungssatz markiert
                                        //dtTmpZP.ImportRow(row);
                                        //DataRow tmpRow = dtTmpZP.NewRow();
                                        //tmpRow.ItemArray = row.ItemArray;
                                        //tmpRow["IsUBCalc"] = true;
                                        //dtTmpZP.Rows.Add(tmpRow);
                                        //dtTmpZP.ImportRow(tmpRow1);                                    
                                    }
                                    else
                                    {
                                        decTmpKosten = 0;
                                    }
                                }
                            }
                            //Umbuchung -> neuer umgebuchter Datensatz
                            if ((!art.Umbuchung) & (art.ArtIDAlt > 0))
                            {
                                //dies ist der alte originale Datensatz 
                                //prüfen Einlagerdatum vom orig. Datensatz
                                strMonatCheck = string.Empty;
                                strMonatCheck = art.Ausgang.LAusgangsDate.Year.ToString() + art.Ausgang.LAusgangsDate.Month.ToString();
                                //prüfen ob die Abrechnung im Eingangmonat stattfindet
                                if (strMonatAbr == strMonatAbr)
                                {
                                    //Auslagerung im Abrechnungsmonat  = Umbuchungsdatum
                                    //prüfen auf Kostenzuweisung für neuer umgebuchter Datensatz
                                    if (art.UB_NeuCalcLagergeld)
                                    {
                                        //neue Row anlegen und hinzufügen als Abrechnungsposition für die UB
                                        //zwischenspeichern in dtTmpEinlagerung;
                                        //row["Kosten"] = decTmpKosten;
                                        //DataRow tmpRow = dtTmpZP.NewRow();
                                        //tmpRow.ItemArray = row.ItemArray;
                                        //tmpRow["ID"] = art.ArtIDAfterUB;
                                        //tmpRow["IsUBCalc"] = true;
                                        //dtTmpZP.Rows.Add(tmpRow);     
                                    }
                                    else
                                    {
                                        decTmpKosten = 0;
                                    }

                                }
                            }
                            row["Kosten"] = decTmpKosten;
                            decLagerkostenGesamt = decLagerkostenGesamt + decTmpKosten;
                        }
                        MergeDataTable(ref dtArtikelLagerbestand, dtTmpZP);
                        dtTmpZP.Rows.Clear();

                        iStellen = 3;
                        Lagerbestand = Math.Round(Lagerbestand, iStellen);
                        Rechnung.RGPosLagerbestand.Menge = Lagerbestand;
                        Rechnung.RGPosLagerbestand.NettoPreis = decLagerkostenGesamt;

                        /******************************************************************************
                        *                      LagerTransportkosten
                        * ***************************************************************************/
                        Rechnung.RGPosLagerTransporte = new clsRGPositionen();
                        Rechnung.RGPosLagerTransporte._GL_User = this._GL_User;
                        Rechnung.RGPosLagerTransporte.Position = iCountTarifPos++;
                        Rechnung.RGPosLagerTransporte.RGText = enumTarifArtLager.LagerTransportkosten.ToString();
                        Rechnung.RGPosLagerTransporte.AbrEinheit = "";
                        Rechnung.RGPosLagerTransporte.AbrechnungsArt = enumTarifArtLager.LagerTransportkosten.ToString();
                        Rechnung.RGPosLagerTransporte.Tariftext = string.Empty;
                        Rechnung.RGPosLagerTransporte.RGPosText = enumTarifArtLager.LagerTransportkosten.ToString();
                        Rechnung.RGPosLagerTransporte.EinzelPreis = 0;
                        Rechnung.RGPosLagerTransporte.MargeProzent = 0;
                        Rechnung.RGPosLagerTransporte.MargeEuro = 0;
                        Rechnung.RGPosLagerTransporte.TarifPosID = 0;
                        Rechnung.RGPosLagerTransporte.FibuKto = KtoSonstiges;

                        decimal decLagerTransportkosten = 0;
                        LagerTransport = 0;
                        foreach (DataRow row in dtArtikelLagerTransporte.Rows)
                        {
                            string strBasis = row["BasisEinheit"].ToString();
                            string strAbr = row["AbrEinheit"].ToString();
                            //decUFact = UmrechnungEinheitenVonBasisAufAbrechnungsEinheit(strBasis, strAbr);
                            Fakt_ConversionUnitBasicToCalcUnit factConv = new Fakt_ConversionUnitBasicToCalcUnit(strBasis, strAbr);
                            decUFact = factConv.ConversionFactor;

                            //Menge
                            decimal decTmpMenge = 0;
                            Decimal.TryParse(row["Menge"].ToString(), out decTmpMenge);
                            decTmpMenge = decTmpMenge * decUFact;
                            LagerTransport = LagerTransport + (decTmpMenge);
                            //Preis
                            decimal decTmpPreis = 0;
                            Decimal.TryParse(row["Preis"].ToString(), out decTmpPreis);
                            //Kosten
                            decimal decTmpKosten = 0;
                            decTmpKosten = Math.Round(decTmpMenge * decTmpPreis, 2, MidpointRounding.AwayFromZero);

                            row["Kosten"] = decTmpKosten;
                            decLagerTransportkosten = decLagerTransportkosten + decTmpKosten;
                        }
                        Rechnung.RGPosLagerTransporte.Menge = LagerTransport;
                        Rechnung.RGPosLagerTransporte.EinzelPreis = 0;
                        Rechnung.RGPosLagerTransporte.NettoPreis = decLagerTransportkosten;
                        Rechnung.RGPosLagerTransporte.Zugang = 0;
                        Rechnung.RGPosLagerTransporte.Endbestand = 0;
                        AddNewRowToDataTableRGVorschau(ref Rechnung.RGPosLagerTransporte);


                        /******************************************************************************
                         *                      Zusatzkosten
                         * ***************************************************************************/
                        Rechnung.RGPosNebenkosten = new clsRGPositionen();
                        Rechnung.RGPosNebenkosten._GL_User = this._GL_User;
                        Rechnung.RGPosNebenkosten.Position = iCountTarifPos++; ;
                        Rechnung.RGPosNebenkosten.RGText = enumTarifArtLager.Nebenkosten.ToString();
                        Rechnung.RGPosNebenkosten.AbrEinheit = "";
                        Rechnung.RGPosNebenkosten.AbrechnungsArt = enumTarifArtLager.Nebenkosten.ToString();
                        Rechnung.RGPosNebenkosten.Tariftext = string.Empty;
                        Rechnung.RGPosNebenkosten.RGPosText = enumTarifArtLager.Nebenkosten.ToString();
                        Rechnung.RGPosNebenkosten.EinzelPreis = 0;
                        Rechnung.RGPosNebenkosten.MargeProzent = 0;
                        Rechnung.RGPosNebenkosten.MargeEuro = 0;
                        Rechnung.RGPosNebenkosten.TarifPosID = 0;
                        Rechnung.RGPosNebenkosten.FibuKto = KtoSonstiges;

                        decimal decNebenkostenGesamtKosten = 0;
                        foreach (DataRow row in dtArtikelNebenkosten.Rows)
                        {
                            string strBasis = row["BasisEinheit"].ToString();
                            string strAbr = row["AbrEinheit"].ToString();
                            //decUFact = UmrechnungEinheitenVonBasisAufAbrechnungsEinheit(strBasis, strAbr);
                            Fakt_ConversionUnitBasicToCalcUnit factConv = new Fakt_ConversionUnitBasicToCalcUnit(strBasis, strAbr);
                            decUFact = factConv.ConversionFactor;


                            decimal decTmpKosten = 0;
                            //Menge
                            decimal decTmpMenge = 0;
                            Decimal.TryParse(row["Menge"].ToString(), out decTmpMenge);
                            decTmpMenge = decTmpMenge * decUFact;
                            //Preis
                            decimal decTmpPreis = 0;
                            Decimal.TryParse(row["Preis"].ToString(), out decTmpPreis);

                            decTmpKosten = Math.Round(decTmpMenge * decTmpPreis, 2, MidpointRounding.AwayFromZero);

                            row["kosten"] = decTmpKosten;


                            if (decTmpKosten > 0)
                            {
                                decNebenkostenGesamtKosten = decNebenkostenGesamtKosten + (decTmpKosten);
                            }
                        }
                        Rechnung.RGPosNebenkosten.Menge = 0;
                        Rechnung.RGPosNebenkosten.EinzelPreis = 0;
                        Rechnung.RGPosNebenkosten.NettoPreis = decNebenkostenGesamtKosten;
                        Rechnung.RGPosNebenkosten.Zugang = 0;
                        Rechnung.RGPosNebenkosten.Endbestand = 0;
                        AddNewRowToDataTableRGVorschau(ref Rechnung.RGPosNebenkosten);

                        /******************************************************************************
                         *                      Gleisstellgebühr
                         * ***************************************************************************/
                        Rechnung.RGPosGleiskosten = new clsRGPositionen();
                        Rechnung.RGPosGleiskosten._GL_User = this._GL_User;
                        Rechnung.RGPosGleiskosten.Position = 6;
                        Rechnung.RGPosGleiskosten.RGText = enumTarifArtLager.Gleisstellgebühr.ToString();
                        Rechnung.RGPosGleiskosten.AbrEinheit = "";
                        Rechnung.RGPosGleiskosten.AbrechnungsArt = enumTarifArtLager.Gleisstellgebühr.ToString();
                        Rechnung.RGPosGleiskosten.Tariftext = string.Empty;
                        Rechnung.RGPosGleiskosten.RGPosText = enumTarifArtLager.Gleisstellgebühr.ToString();
                        Rechnung.RGPosGleiskosten.MargeProzent = 0;
                        Rechnung.RGPosGleiskosten.MargeEuro = 0;
                        Rechnung.RGPosGleiskosten.TarifPosID = 0;
                        Rechnung.RGPosGleiskosten.FibuKto = KtoGleis;

                        decimal decGleiskostenGesamtKosten = 0;
                        if (dtWaggonListe.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtWaggonListe.Rows)
                            {
                                decimal decAnzahl = 0;
                                decimal decPreis = 0;
                                decTmp = 0;
                                Decimal.TryParse(row["Preis"].ToString(), out decPreis);
                                Decimal.TryParse(row["Anzahl"].ToString(), out decAnzahl);
                                if (decPreis > 0 && decAnzahl > 0)
                                {
                                    decGleiskostenGesamtKosten = decGleiskostenGesamtKosten + decAnzahl * Math.Round(decPreis, 2, MidpointRounding.AwayFromZero);
                                }
                            }
                        }

                        Rechnung.RGPosGleiskosten.Menge = dtArtikelGleis.Rows.Count;
                        Rechnung.RGPosGleiskosten.EinzelPreis = 0;
                        Rechnung.RGPosGleiskosten.NettoPreis = decGleiskostenGesamtKosten;
                        Rechnung.RGPosGleiskosten.Zugang = 0;
                        Rechnung.RGPosGleiskosten.Endbestand = 0;
                        AddNewRowToDataTableRGVorschau(ref Rechnung.RGPosGleiskosten);
                        /******************************************************************************
                         *                            SLVS
                         * ***************************************************************************/

                        decimal decSLVS = 0;
                        if (!bSLVSMaterialWert)
                        {
                            if (bSLVSPauschal)
                            {
                                decSLVS = decPauschalSLVL;
                            }
                            else
                            {
                                foreach (DataRow row in dtSLVS.Rows)
                                {
                                    decimal decVersPreis = 0;
                                    Decimal.TryParse(row["VersPreis"].ToString(), out decVersPreis);
                                    decimal decVersMaterialWertPreis = 0;
                                    Decimal.TryParse(row["VersMaterialWert"].ToString(), out decVersMaterialWertPreis);
                                    decimal decMenge = 0;
                                    Decimal.TryParse(row["Menge"].ToString(), out decMenge);
                                    decSLVS += Math.Round((decMenge / 1000) * decVersMaterialWertPreis * (decVersPreis / 1000), 2, MidpointRounding.AwayFromZero);
                                }
                            }
                        }
                        Rechnung.VersPraemie = decSLVS;

                        /*****************************************************************************
                         *                          Rechnung
                         * **************************************************************************/
                        //**** Bestände sezten
                        //Anfangsbestand
                        decTmp = 0;
                        if (dtArtikelAnfangsbestand.Rows.Count > 0)
                        {
                            object objAB = dtArtikelAnfangsbestand.Compute("SUM(Menge)", string.Empty);
                            Decimal.TryParse(objAB.ToString(), out decTmp);
                            Anfangsbestand = decTmp;
                        }
                        Rechnung.RGPosLagerbestand.Anfangsbestand = Anfangsbestand;
                        //Einlagerung
                        decTmp = 0;
                        if (dtArtikelEinlagerung.Rows.Count > 0)
                        {
                            object objEin = dtArtikelEinlagerung.Compute("SUM(Menge)", string.Empty);
                            Decimal.TryParse(objEin.ToString(), out decTmp);
                        }
                        Rechnung.RGPosLagerbestand.Zugang = decTmp;
                        decTmp = 0;
                        if (dtArtikelEinlagerungUmbuchungGratis.Rows.Count > 0)
                        {
                            object objEin = dtArtikelEinlagerungUmbuchungGratis.Compute("SUM(Menge)", string.Empty);
                            Decimal.TryParse(objEin.ToString(), out decTmp);
                        }
                        Rechnung.RGPosLagerbestand.Zugang += decTmp;

                        //Auslagerung
                        decTmp = 0;
                        if (dtArtikelAuslagerung.Rows.Count > 0)
                        {
                            object objAus = dtArtikelAuslagerung.Compute("SUM(Menge)", string.Empty);
                            Decimal.TryParse(objAus.ToString(), out decTmp);
                        }
                        Rechnung.RGPosLagerbestand.Abgang = decTmp;
                        decTmp = 0;
                        if (dtArtikelAuslagerungUmbuchungGratis.Rows.Count > 0)
                        {
                            object objAus = dtArtikelAuslagerungUmbuchungGratis.Compute("SUM(Menge)", string.Empty);
                            Decimal.TryParse(objAus.ToString(), out decTmp);
                        }
                        Rechnung.RGPosLagerbestand.Abgang += decTmp;
                        Rechnung.RGPosLagerbestand.Endbestand = Rechnung.RGPosLagerbestand.Anfangsbestand + Rechnung.RGPosLagerbestand.Zugang - Rechnung.RGPosLagerbestand.Abgang;
                        AddNewRowToDataTableRGVorschau(ref Rechnung.RGPosLagerbestand);

                        //Eintrag der Rechnung des Kunden in die Datenbank
                        InitDataTableRechnung();
                        FormatDataTableRechnung();
                        Rechnung.Empfaenger = Adr.ID;
                        Rechnung.Auftraggeber = Adr.ID;
                        Rechnung.RGNr = 0;
                        Rechnung.AbrechnungsTarifName = "Alle Tarife";
                        Rechnung.Datum = RGDate;
                        Rechnung.faellig = RGDate.AddDays(Adr.Kunde.Zahlungziel);
                        Rechnung.AbrZeitraumVon = AbrVon;
                        Rechnung.AbrZeitraumBis = AbrBis;
                        Rechnung.MwStSatz = Adr.Kunde.MwStSatz;

                        Rechnung.bezahlt = Globals.DefaultDateTimeMaxValue; //aktuelle nicht vorgesehen eventuell für Mahnungen
                        Rechnung.Druck = false;
                        Rechnung.Druckdatum = Globals.DefaultDateTimeMaxValue;
                        Rechnung.RGArt = enumTarifArt.Lager.ToString();
                        Rechnung.MandantenID = this._GL_System.sys_MandantenID;
                        Rechnung.exFibu = false;
                        Rechnung.Anfangsbestand = Anfangsbestand;

                        if (bSLVSMaterialWert)
                        {
                            if (bSLVSPauschal)
                            {
                                Rechnung.VersPraemie = decPauschalSLVL;
                            }
                            else
                            {
                                Rechnung.VersPraemie = decVersPraemieGesamt;
                            }
                        }


                        //alle RG Positionen müssten in er RGVorschau enthalten sein, hier könnte man dynamisch den Nettobetrag errechnen
                        Rechnung.NettoBetrag = decimal.Round(Rechnung.RGPosLagerbestand.NettoPreis, 2, MidpointRounding.AwayFromZero)
                                             + decimal.Round(Rechnung.RGPosEinlagerung.NettoPreis, 2, MidpointRounding.AwayFromZero)
                                             + decimal.Round(Rechnung.RGPosAuslagerung.NettoPreis, 2, MidpointRounding.AwayFromZero)
                                             + decimal.Round(Rechnung.RGPosNebenkosten.NettoPreis, 2, MidpointRounding.AwayFromZero)
                                             + decimal.Round(Rechnung.RGPosLagerTransporte.NettoPreis, 2, MidpointRounding.AwayFromZero)
                                             + decimal.Round(Rechnung.RGPosGleiskosten.NettoPreis, 2, MidpointRounding.AwayFromZero)
                                             + decimal.Round(Rechnung.VersPraemie, 2, MidpointRounding.AwayFromZero);

                        Rechnung.MwStBetrag = Rechnung.NettoBetrag * Rechnung.MwStSatz / 100;
                        Rechnung.BruttoBetrag = Rechnung.NettoBetrag + Rechnung.MwStBetrag;

                        Rechnung.dtRGPositionen = dtRechnung;

                        //Artikel der einzelnen Positionen
                        Rechnung.dtArtikelEinlagerung = dtArtikelEinlagerung;
                        Rechnung.dtArtikelAnfangsbestand = dtArtikelAnfangsbestand;
                        Rechnung.dtArtikelAuslagerung = dtArtikelAuslagerung;
                        Rechnung.dtArtikelDirektanlieferung = dtArtikelDirektanlieferung;
                        Rechnung.dtArtikelLagerTransporte = dtArtikelLagerTransporte;
                        Rechnung.dtArtikelRuecklieferung = dtArtikelRuecklieferung;
                        Rechnung.dtArtikelSperrlager = dtArtikelSperrlager;
                        Rechnung.dtArtikelLagerbestand = dtArtikelLagerbestand;
                        Rechnung.dtArtikelVorfracht = dtArtikelVorfracht;
                        Rechnung.dtArtikelNebenkosten = dtArtikelNebenkosten;
                        Rechnung.dtArtikelGleis = dtArtikelGleis;

                        Rechnung.GS = false;
                        Rechnung.Storno = false;

                        if (Rechnung.dtRGPositionen.Rows.Count > 0)
                        {
                            if (Rechnung.NettoBetrag > 0)
                            {
                                Rechnung.AddAllTarifInOne();
                            }
                        }
                        else
                        {
                            string str = string.Empty;
                        }
                    }//dtTarif.Rows.Count
                }//Auftraggeber ermittelt
            }//abzur. Kunden   
            dtErrorArtikel = dtErrorArtikel;
        }
        /// <summary>
        /// GetSQLforAbmessungMissing
        /// </summary>
        /// <param name="myTarif"></param>
        /// <param name="strSQLTmp"></param>
        /// <returns></returns>
        private string GetSQLforAbmessungMissing(ref clsTarif myTarif)
        {
            string temp = "(";
            if (myTarif.TarifPosition.BruttoVon >= 0 && myTarif.TarifPosition.BruttoBis > 0)
            {
                temp += " a.Brutto>=" + myTarif.TarifPosition.BruttoVon + " AND " +
                                        " a.Brutto<=" + myTarif.TarifPosition.BruttoBis + " ";
            }
            if (myTarif.TarifPosition.DickeVon >= 0 && myTarif.TarifPosition.DickeBis > 0)
            {
                if (temp.Length > 1) temp += " AND ";
                temp += " a.Dicke>=" + myTarif.TarifPosition.DickeVon + " AND " +
                                        " a.Dicke<=" + myTarif.TarifPosition.DickeBis + " ";
            }
            if (myTarif.TarifPosition.BreiteVon >= 0 && myTarif.TarifPosition.BreiteBis > 0)
            {
                if (temp.Length > 1) temp += " AND ";
                temp += "  a.Breite>=" + myTarif.TarifPosition.BreiteVon + " AND " +
                                        " a.Breite<=" + myTarif.TarifPosition.BreiteBis + " ";
            }
            if (myTarif.TarifPosition.LaengeVon >= 0 && myTarif.TarifPosition.LaengeBis > 0)
            {
                if (temp.Length > 1) temp += " AND ";
                temp += "  a.Laenge>=" + myTarif.TarifPosition.LaengeVon + " AND " +
                                        " a.Laenge<=" + myTarif.TarifPosition.LaengeBis + " ";
            }
            temp += ")";
            if (temp.Length == 2)
                temp = string.Empty;
            return temp;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetSQLforBestandMissing(string strAbrechnungsart)
        {
            string strSQLTmp = "";
            if (strAbrechnungsart == "Lagerkosten")
            {
                strSQLTmp =
                   "Select '" + strAbrechnungsart + "' as AbrArt,* From Artikel a INNER JOIN LEingang b ON b.ID = a.LEingangTableID INNER JOIN Gueterart e ON e.ID=a.GArtID LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID WHERE " +
    "(" +
       "(" +
          "b.Auftraggeber=34 AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 AND b.DirectDelivery=0 AND b.Mandant=1  AND a.GArtID IN (1) AND " +
          "DATEDIFF(dd, b.Date, '" + BisZeitraum.ToShortDateString() + "')>=0 " +
          "AND b.Date <'" + VonZeitraum.ToShortDateString() + "' " +
       ") " +
       "OR " +
       "(" +
          "b.Auftraggeber=34 AND " +
          "(((" +
             "c.Datum between '" + VonZeitraum.ToShortDateString() + "' AND '" + DateTime.Now.ToShortDateString() + "') AND " +
             "a.BKZ=0) OR a.BKZ=1 )AND " +
             "a.CheckArt=1 AND " +
             "b.[Check]=1 AND " +
             "b.DirectDelivery=0 AND " +
             "b.Mandant=1  AND " +
             "a.GArtID IN (1) AND " +
             "DATEDIFF(dd, b.Date, '" + BisZeitraum.ToShortDateString() + "')>=0 AND b.Date <'" + BisZeitraum.ToShortDateString() + "' " +
          ")" +
        ") " +
                             "AND(not a.GArtID in(Select GArtID from TarifGArtZuweisung left join KundenTarife on KundenTarife.TarifID=TarifGArtZuweisung.TarifID where AdrID=" + Auftraggeber + ")";
            }

            else
            {
                strSQLTmp =
                        "Select '" + strAbrechnungsart + "' as AbrArt,* from Artikel a " +
                        "left join LEingang b on a.LEingangTableID=b.ID " +
                        "left join LAusgang c on a.LAusgangTableID=c.ID " +
                        "WHERE " +
                        "b.[Check]=1 AND " +
                        "b.DirectDelivery=0 AND " +
                        "b.Mandant=1 AND " +
                        "b.Auftraggeber=34 AND " +
                        "((b.Date between '" + VonZeitraum.ToShortDateString() + "' AND '" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "')OR " +
                        "(c.Datum between '" + VonZeitraum.ToShortDateString() + "' AND '" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "'))" +
                        "AND (c.Checked=1 OR c.Checked is null)" +
                        "AND(not a.GArtID in(Select GArtID from TarifGArtZuweisung left join KundenTarife on KundenTarife.TarifID=TarifGArtZuweisung.TarifID where AdrID=" + Auftraggeber + ")";
            }
            return strSQLTmp;
        }

        private string GetSQLforSLVS(ref clsTarif myTarif, string strSQLTmp)
        {
            string strSQL = "select sum(x.Menge) as Menge" +
                            ", CAST(" + myTarif.VersPreis.ToString().Replace(",", ".") + " as money) as VersPreis " +
                            ", CAST(" + myTarif.VersMaterialWert.ToString().Replace(",", ".") + " as money) as VersMaterialWert " +
                            "FROM ( " +
                            strSQLTmp.Replace(";", "") +
                            ") as x";

            return strSQL;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myTarif"></param>
        /// <param name="strSQLTmp"></param>
        /// <returns></returns>
        private string GetSQLforAbmessung(ref clsTarif myTarif, string strSQLTmp)
        {
            if (myTarif.TarifPosition.BruttoVon >= 0 && myTarif.TarifPosition.BruttoBis > 0)
            {
                strSQLTmp = strSQLTmp + " AND a.Brutto>=" + myTarif.TarifPosition.BruttoVon + " AND " +
                                        " a.Brutto<=" + myTarif.TarifPosition.BruttoBis + " ";
            }
            if (myTarif.TarifPosition.DickeVon >= 0 && myTarif.TarifPosition.DickeBis > 0)
            {
                strSQLTmp = strSQLTmp + " AND a.Dicke>=" + myTarif.TarifPosition.DickeVon + " AND " +
                                        " a.Dicke<=" + myTarif.TarifPosition.DickeBis + " ";
            }
            if (myTarif.TarifPosition.BreiteVon >= 0 && myTarif.TarifPosition.BreiteBis > 0)
            {
                strSQLTmp = strSQLTmp + " AND a.Breite>=" + myTarif.TarifPosition.BreiteVon + " AND " +
                                        " a.Breite<=" + myTarif.TarifPosition.BreiteBis + " ";
            }
            if (myTarif.TarifPosition.LaengeVon >= 0 && myTarif.TarifPosition.LaengeBis > 0)
            {
                strSQLTmp = strSQLTmp + " AND a.Laenge>=" + myTarif.TarifPosition.LaengeVon + " AND " +
                                        " a.Laenge<=" + myTarif.TarifPosition.LaengeBis + " ";
            }
            strSQLTmp = strSQLTmp + ";";
            return strSQLTmp;
        }
        private string GetSQLforAbmessungAltBestand(ref clsTarif myTarif, string strSQLTmp)
        {
            if (myTarif.TarifPosition.BruttoVon >= 0 && myTarif.TarifPosition.BruttoBis > 0)
            {
                strSQLTmp = strSQLTmp + " AND a.Brutto>=" + myTarif.TarifPosition.BruttoVon + " AND " +
                                        " a.Brutto<=" + myTarif.TarifPosition.BruttoBis + " ";
            }
            if (myTarif.TarifPosition.DickeVon >= 0 && myTarif.TarifPosition.DickeBis > 0)
            {
                strSQLTmp = strSQLTmp + " AND a.Dicke>=" + myTarif.TarifPosition.DickeVon + " AND " +
                                        " a.Dicke<=" + myTarif.TarifPosition.DickeBis + " ";
            }
            if (myTarif.TarifPosition.BreiteVon >= 0 && myTarif.TarifPosition.BreiteBis > 0)
            {
                strSQLTmp = strSQLTmp + " AND a.Breite>=" + myTarif.TarifPosition.BreiteVon + " AND " +
                                        " a.Breite<=" + myTarif.TarifPosition.BreiteBis + " ";
            }
            if (myTarif.TarifPosition.LaengeVon >= 0 && myTarif.TarifPosition.LaengeBis > 0)
            {
                strSQLTmp = strSQLTmp + " AND a.Laenge>=" + myTarif.TarifPosition.LaengeVon + " AND " +
                                        " a.Laenge<=" + myTarif.TarifPosition.LaengeBis + " ";
            }
            strSQLTmp = strSQLTmp + ";";
            return strSQLTmp;
        }
        ///<summary>clsFaktLager / InitZwischenBestaende</summary>
        ///<remarks></remarks>
        private void InitZwischenBestaende(ref DataTable SourceTable, string strBestandsArt, string strEinheit)
        {
            for (Int32 i = 0; i <= dtRGVorschau.Rows.Count - 1; i++)
            {
                if (dtRGVorschau.Rows[i]["Abrechnungsart"].ToString() == strBestandsArt)
                {
                    DataRow row = SourceTable.NewRow();
                    row[0] = string.Empty;
                    row[1] = dtRGVorschau.Rows[i]["Text"].ToString();
                    row[2] = (decimal)dtRGVorschau.Rows[i]["Menge"];
                    row[3] = strEinheit;
                    SourceTable.Rows.Add(row);
                }

            }
        }
        ///<summary>clsFaktLager / CalcTarifPosition</summary>
        ///<remarks>Berechnung und Zuweisung der Tarifpositionswerte</remarks>
        private void CalcTarifPosition(ref clsRGPositionen myRGPos, ref clsTarifPosition myTarifPos, ref Int32 iPos, string myAbrArt, DataTable myArtikelTarifPosTable = null)
        {
            if (!myTarifPos.StaffelPos)
            {
                myRGPos.RGText = myRGPos.Tariftext;
                myRGPos.RGPosText = myTarifPos.Beschreibung;
            }
            else
            {
                //myRGPos.RGText = myTarifPos.Beschreibung;
                //myRGPos.RGPosText = myTarifPos.TarifPosArt;

                myRGPos.RGText = myTarifPos.TarifPosArt;
                myRGPos.RGPosText = myTarifPos.Beschreibung;
            }

            myRGPos.EinzelPreis = myTarifPos.PreisEinheit;
            myRGPos.MargeProzent = myTarifPos.MargeProzentEinheit;
            //myRGPos.RGText = myTarifPos.Beschreibung;
            myRGPos.Position = iPos;
            myRGPos.TarifPosID = myTarifPos.ID;
            myRGPos.AbrEinheit = myTarifPos.AbrEinheit;


            decimal decUFact = 1;
            if (myTarifPos.BasisEinheit != myTarifPos.AbrEinheit)
            {
                Fakturierung.Fakt_ConversionUnitBasicToCalcUnit fakt_ConversionUnitBasicToCalcUnit = new Fakturierung.Fakt_ConversionUnitBasicToCalcUnit(myTarifPos.BasisEinheit, myTarifPos.AbrEinheit);
                decUFact = fakt_ConversionUnitBasicToCalcUnit.ConversionFactor; // UmrechnungEinheitenVonBasisAufAbrechnungsEinheit(myTarifPos.BasisEinheit, myTarifPos.AbrEinheit);
                myRGPos.Menge = myRGPos.Menge * decUFact;
                myRGPos.PricePerUnitFactor = myRGPos.Menge;
                myRGPos.Anfangsbestand = myRGPos.Anfangsbestand * decUFact;
                myRGPos.Endbestand = myRGPos.Endbestand * decUFact;
                myRGPos.Zugang = myRGPos.Zugang * decUFact;
                myRGPos.Abgang = myRGPos.Abgang * decUFact;
            }


            switch (myAbrArt)
            {
                //case "Einlagerungskosten":
                case clsFaktLager.const_Abrechnungsart_Einlagerung:
                    Einlagerung = Einlagerung * decUFact;
                    Einlagerung = Einlagerung + myRGPos.Menge;
                    break;

                //case "Auslagerungskosten":
                case clsFaktLager.const_Abrechnungsart_Auslagerung:
                    Auslagerung = Auslagerung * decUFact;
                    Auslagerung = Auslagerung + myRGPos.Menge;
                    myRGPos.Abgang = myRGPos.Menge;
                    break;

                //case "Lagerkosten":
                case clsFaktLager.const_Abrechnungsart_Lagerkosten:
                    Lagerbestand = myRGPos.Menge;
                    if (Einlagerung == 0)
                    {
                        Einlagerung = myRGPos.Zugang;
                    }
                    if (Auslagerung == 0)
                    {
                        Auslagerung = myRGPos.Abgang;
                    }
                    break;

                //case "LagerTransportkosten":
                case clsFaktLager.const_Abrechnungsart_LagerTransportkosten:
                    LagerTransport = LagerTransport * decUFact;
                    LagerTransport = LagerTransport + myRGPos.Menge;
                    break;

                //case "Sperrlagerkosten":
                case clsFaktLager.const_Abrechnungsart_SPL:
                    Sperrlager = Sperrlager * decUFact;
                    Sperrlager = Sperrlager + myRGPos.Menge;
                    break;

                //case "Direktanlieferung":
                case clsFaktLager.const_Abrechnungsart_Direktanlieferung:
                    Direktanlieferung = Direktanlieferung * decUFact;
                    Direktanlieferung = Direktanlieferung + myRGPos.Menge;
                    break;

                //case "Rücklieferung":
                case clsFaktLager.const_Abrechnungsart_Ruecklieferung:
                    Ruecklieferung = Ruecklieferung * decUFact;
                    Ruecklieferung = Ruecklieferung + myRGPos.Menge;
                    break;

                //case "Vorfracht":
                case clsFaktLager.const_Abrechnungsart_Vorfracht:
                    Vorfracht = Vorfracht * decUFact;
                    Vorfracht = Vorfracht + myRGPos.Menge;
                    break;

                case clsFaktLager.const_Abrechnungsart_Gleisstellgebuehr:
                    Gleisgebuehr = Gleisgebuehr * decUFact;
                    Gleisgebuehr = Gleisgebuehr + myRGPos.Menge;
                    break;

                case clsFaktLager.const_Abrechnungsart_Nebenkosten:
                    Nebenkosten = Nebenkosten + myRGPos.NettoPreis;
                    break;

                case clsFaktLager.const_Abrechnungsart_Maut:
                    Toll = Toll * decUFact;
                    Toll = Toll + myRGPos.Menge;
                    break;
            }

            //Margen berechnen   
            if (myAbrArt.Contains(const_Abrechnungsart_Nebenkosten))
            {
                myRGPos.Menge = 1;
                myRGPos.NettoPreis = 0M;
                myRGPos.EinzelPreis = 0M;
                myRGPos.CalcModValue = 0;
                myRGPos.PricePerUnitFactor = myRGPos.Menge;
                myRGPos.AbrEinheit = "Stk.";
                if (
                        (myArtikelTarifPosTable != null) &&
                        (myArtikelTarifPosTable.Rows.Count > 0)
                    )
                {
                    Fakt_CalcModusValue calcModusVal = new Fakt_CalcModusValue(myArtikelTarifPosTable, myTarifPos);
                    myRGPos.Menge = calcModusVal.SumQuantity;
                    myRGPos.NettoPreis = calcModusVal.SumKost;
                    myRGPos.EinzelPreis = calcModusVal.SumKost;
                    myRGPos.CalcModValue = calcModusVal.SumDuration;
                    myRGPos.PricePerUnitFactor = calcModusVal.SumPricePerUnitFactor;
                    myRGPos.AbrEinheit = "Stk.";
                }
            }
            else
            {
                //Bestände in RGPos übernehmen
                myRGPos.Endbestand = myRGPos.Anfangsbestand + myRGPos.Zugang - myRGPos.Abgang;
                myRGPos.PricePerUnitFactor = myRGPos.Menge;
                //myRGPos.NettoPreis = myRGPos.Menge * myRGPos.EinzelPreis;
                myRGPos.NettoPreis = myRGPos.PricePerUnitFactor * myRGPos.EinzelPreis;

                if (
                     (myArtikelTarifPosTable != null) &&
                     (myArtikelTarifPosTable.Rows.Count > 0) &&
                     (!myTarifPos.CalcModus.Equals(enumCalcultationModus.Standard))
                  )
                {
                    Fakt_CalcModusValue calcModusVal = new Fakt_CalcModusValue(myArtikelTarifPosTable, myTarifPos);
                    myRGPos.Menge = calcModusVal.SumQuantity;
                    myRGPos.NettoPreis = calcModusVal.SumKost;
                    myRGPos.CalcModValue = calcModusVal.SumDuration;
                    myRGPos.PricePerUnitFactor = calcModusVal.SumPricePerUnitFactor;
                }

                if (myTarifPos.MargePreisEinheit > 0)
                {
                    //myRGPos.MargeEuro = myRGPos.Menge * myTarifPos.MargePreisEinheit;
                    myRGPos.MargeEuro = myRGPos.PricePerUnitFactor * myTarifPos.MargePreisEinheit;
                    myRGPos.NettoPreis = myRGPos.NettoPreis - myRGPos.MargeEuro;
                }
                if (myTarifPos.MargeProzentEinheit > 0)
                {
                    myRGPos.NettoPreis = myRGPos.NettoPreis - (myRGPos.NettoPreis * myRGPos.MargeProzent);
                }
            }
            myRGPos.NettoPreis = Math.Round(myRGPos.NettoPreis, 2, MidpointRounding.AwayFromZero);

            //switch (myAbrArt)
            //{
            //    //case "Einlagerungskosten":
            //    case clsFaktLager.const_Abrechnungsart_Einlagerung:
            //        Einlagerung = Einlagerung * decUFact;
            //        Einlagerung = Einlagerung + myRGPos.Menge;
            //        break;

            //    //case "Auslagerungskosten":
            //    case clsFaktLager.const_Abrechnungsart_Auslagerung:
            //        Auslagerung = Auslagerung * decUFact;
            //        Auslagerung = Auslagerung + myRGPos.Menge;
            //        myRGPos.Abgang = myRGPos.Menge;
            //        break;

            //    //case "Lagerkosten":
            //    case clsFaktLager.const_Abrechnungsart_Lagerkosten:
            //        Lagerbestand = myRGPos.Menge;
            //        if (Einlagerung == 0)
            //        {
            //            Einlagerung = myRGPos.Zugang;
            //        }
            //        if (Auslagerung == 0)
            //        {
            //            Auslagerung = myRGPos.Abgang;
            //        }
            //        break;

            //    //case "LagerTransportkosten":
            //    case clsFaktLager.const_Abrechnungsart_LagerTransportkosten:
            //        LagerTransport = LagerTransport * decUFact;
            //        LagerTransport = LagerTransport + myRGPos.Menge;
            //        break;

            //    //case "Sperrlagerkosten":
            //    case clsFaktLager.const_Abrechnungsart_SPL:
            //        Sperrlager = Sperrlager * decUFact;
            //        Sperrlager = Sperrlager + myRGPos.Menge;
            //        break;

            //    //case "Direktanlieferung":
            //    case clsFaktLager.const_Abrechnungsart_Direktanlieferung:
            //        Direktanlieferung = Direktanlieferung * decUFact;
            //        Direktanlieferung = Direktanlieferung + myRGPos.Menge;
            //        break;

            //    //case "Rücklieferung":
            //    case clsFaktLager.const_Abrechnungsart_Ruecklieferung:
            //        Ruecklieferung = Ruecklieferung * decUFact;
            //        Ruecklieferung = Ruecklieferung + myRGPos.Menge;
            //        break;

            //    //case "Vorfracht":
            //    case clsFaktLager.const_Abrechnungsart_Vorfracht:
            //        Vorfracht = Vorfracht * decUFact;
            //        Vorfracht = Vorfracht + myRGPos.Menge;
            //        break;

            //    case clsFaktLager.const_Abrechnungsart_Gleisstellgebuehr:
            //        Gleisgebuehr = Gleisgebuehr * decUFact;
            //        Gleisgebuehr = Gleisgebuehr + myRGPos.Menge;
            //        break;

            //    case clsFaktLager.const_Abrechnungsart_Nebenkosten:
            //        Nebenkosten = Nebenkosten + myRGPos.NettoPreis;
            //        break;

            //    case clsFaktLager.const_Abrechnungsart_Maut:
            //        Toll = Toll * decUFact;
            //        Toll = Toll + myRGPos.Menge;
            //        break;
            //}

            //Bestände runden
            Int32 iStellen = 3;
            Einlagerung = Math.Round(Einlagerung, iStellen);
            Auslagerung = Math.Round(Auslagerung, iStellen);
            Lagerbestand = Math.Round(Lagerbestand, iStellen);
            LagerTransport = Math.Round(LagerTransport, iStellen);
            Direktanlieferung = Math.Round(Direktanlieferung, iStellen);
            Ruecklieferung = Math.Round(Ruecklieferung, iStellen);
            Vorfracht = Math.Round(Vorfracht, iStellen);
            Gleisgebuehr = Math.Round(Gleisgebuehr, iStellen);
            Nebenkosten = Math.Round(Nebenkosten, 4);
            Toll = Math.Round(Toll, iStellen);


            if (this.Sys.Client.Modul.Fakt_eInvoiceIsAvailable)
            { 
                //-- Check Rechnungsposition = 0 und soll doch angezeigt werden,
                //   dann muss für eRechnung
                //   Anzahl = 1 und
                //   €/to = 0
                //   Netto Rechnungspositon = 0
                //   gesetzt werden
                if (
                        ((myRGPos.Menge == 0) && (myRGPos.PricePerUnitFactor == 0) && (myRGPos.EinzelPreis > 0) && (myRGPos.NettoPreis == 0)) ||
                        ((myRGPos.Menge > 0) && (myRGPos.PricePerUnitFactor > 0) && (myRGPos.EinzelPreis == 0) && (myRGPos.NettoPreis == 0)) ||
                        ((myRGPos.Menge > 0) && (myRGPos.PricePerUnitFactor == 0) && (myRGPos.EinzelPreis > 0) && (myRGPos.NettoPreis == 0))
                   )
                {
                    myRGPos.PricePerUnitFactor = 1;
                    myRGPos.EinzelPreis = 0M;
                    myRGPos.NettoPreis = 0M;
                }
            }

            AddNewRowToDataTableRGVorschau(ref myRGPos);


            //-------------------------------------------------
            //      alte Version
            //-------------------------------------------------

            //Fakt_CalcModusValue calcModusVal = new Fakt_CalcModusValue(myArtikelTarifPosTable, myTarifPos);
            //myRGPos.Menge = calcModusVal.SumQuantity;
            //myRGPos.NettoPreis = calcModusVal.SumKost;
            //myRGPos.CalcModValue = calcModusVal.SumDuration;

            //Ermitteln der Lagertage
            //object SumMenge = new object();
            //object SumKosten = new object();
            //object SumDauer = new object();
            //string strFilter = "CalcModus=" + (int)myTarifPos.CalcModus;
            //decimal decTmp = 0;
            //switch (myTarifPos.CalcModus)
            //{
            //    case enumCalcultationModus.täglich:
            //        if (myArtikelTarifPosTable != null)
            //        {
            //            if (myArtikelTarifPosTable.Rows.Count > 0)
            //            {
            //                if (myArtikelTarifPosTable.Columns.Contains("Menge"))
            //                {
            //                    SumMenge = myArtikelTarifPosTable.Compute("SUM(Menge)", strFilter);
            //                }
            //                decTmp = 0;
            //                Decimal.TryParse(SumMenge.ToString(), out decTmp);
            //                myRGPos.Menge = decTmp * decUFact;
            //                if (myArtikelTarifPosTable.Columns.Contains("Kosten"))
            //                {
            //                    SumKosten = myArtikelTarifPosTable.Compute("SUM(Kosten)", strFilter);
            //                }
            //                decTmp = 0;
            //                Decimal.TryParse(SumKosten.ToString(), out decTmp);
            //                myRGPos.NettoPreis = decTmp;

            //                if (myArtikelTarifPosTable.Columns.Contains("Dauer"))
            //                {
            //                    SumDauer = myArtikelTarifPosTable.Compute("SUM(Dauer)", strFilter);
            //                }
            //                int iTmp = 0;
            //                int.TryParse(SumDauer.ToString(), out iTmp);
            //                myRGPos.CalcModValue = iTmp;
            //            }
            //        }
            //        else
            //        {
            //            // mr 202050506
            //            if (dtArtikelLagerbestand.Rows.Count > 0)
            //            {                             
            //                if (dtArtikelLagerbestand.Columns.Contains("Menge"))
            //                {
            //                    SumMenge = dtArtikelLagerbestand.Compute("SUM(Menge)", strFilter);
            //                }
            //                decTmp = 0;
            //                Decimal.TryParse(SumMenge.ToString(), out decTmp);
            //                myRGPos.Menge = decTmp * decUFact;
            //                if (dtArtikelLagerbestand.Columns.Contains("Kosten"))
            //                {
            //                    SumKosten = dtArtikelLagerbestand.Compute("SUM(Kosten)", strFilter);
            //                }
            //                decTmp = 0;
            //                Decimal.TryParse(SumKosten.ToString(), out decTmp);
            //                myRGPos.NettoPreis = decTmp;

            //                if (dtArtikelLagerbestand.Columns.Contains("Dauer"))
            //                {
            //                    SumDauer = dtArtikelLagerbestand.Compute("SUM(Dauer)", strFilter);
            //                }
            //                int iTmp = 0;
            //                int.TryParse(SumDauer.ToString(), out iTmp);
            //                myRGPos.CalcModValue = iTmp;
            //            }
            //        }
            //        break;

            //    case enumCalcultationModus.monatlich:
            //    case enumCalcultationModus.Zeitraum30Tage:
            //        if (myTarifPos.QuantityCalcBase.Equals(clsTarifPosition.const_QuantityBase_Auslagerung))
            //        {
            //            if (myArtikelTarifPosTable != null)
            //            {
            //                if (myArtikelTarifPosTable.Rows.Count > 0)
            //                {
            //                    if (myArtikelTarifPosTable.Columns.Contains("Menge"))
            //                    {
            //                        SumMenge = myArtikelTarifPosTable.Compute("SUM(Menge)", strFilter);
            //                    }
            //                    decTmp = 0;
            //                    Decimal.TryParse(SumMenge.ToString(), out decTmp);
            //                    myRGPos.Menge = decTmp * decUFact;
            //                    if (myArtikelTarifPosTable.Columns.Contains("Kosten"))
            //                    {
            //                        SumKosten = myArtikelTarifPosTable.Compute("SUM(Kosten)", strFilter);
            //                    }
            //                    decTmp = 0;
            //                    Decimal.TryParse(SumKosten.ToString(), out decTmp);
            //                    myRGPos.NettoPreis = decTmp;

            //                    if (myArtikelTarifPosTable.Columns.Contains("Dauer"))
            //                    {
            //                        SumDauer = myArtikelTarifPosTable.Compute("SUM(Dauer)", strFilter);
            //                    }
            //                    int iTmp = 0;
            //                    int.TryParse(SumDauer.ToString(), out iTmp);
            //                    myRGPos.CalcModValue = iTmp;
            //                }
            //            }
            //            else
            //            {
            //                // mr 202050506
            //                if (dtArtikelLagerbestand.Rows.Count > 0)
            //                {
            //                    if (dtArtikelLagerbestand.Columns.Contains("Menge"))
            //                    {
            //                        SumMenge = dtArtikelLagerbestand.Compute("SUM(Menge)", strFilter);
            //                    }
            //                    decTmp = 0;
            //                    Decimal.TryParse(SumMenge.ToString(), out decTmp);
            //                    myRGPos.Menge = decTmp * decUFact;
            //                    if (dtArtikelLagerbestand.Columns.Contains("Kosten"))
            //                    {
            //                        SumKosten = dtArtikelLagerbestand.Compute("SUM(Kosten)", strFilter);
            //                    }
            //                    decTmp = 0;
            //                    Decimal.TryParse(SumKosten.ToString(), out decTmp);
            //                    myRGPos.NettoPreis = decTmp;

            //                    if (dtArtikelLagerbestand.Columns.Contains("Dauer"))
            //                    {
            //                        SumDauer = dtArtikelLagerbestand.Compute("SUM(Dauer)", strFilter);
            //                    }
            //                    int iTmp = 0;
            //                    int.TryParse(SumDauer.ToString(), out iTmp);
            //                    myRGPos.CalcModValue = iTmp;
            //                }
            //            }
            //            //if (myArtikelTarifPosTable.Rows.Count > 0)
            //            //{
            //            //    if (myArtikelTarifPosTable.Columns.Contains("Menge"))
            //            //    {
            //            //        SumMenge = myArtikelTarifPosTable.Compute("SUM(Menge)", strFilter);
            //            //    }
            //            //    decTmp = 0;
            //            //    Decimal.TryParse(SumMenge.ToString(), out decTmp);
            //            //    myRGPos.Menge = decTmp * decUFact;
            //            //    if (myArtikelTarifPosTable.Columns.Contains("Kosten"))
            //            //    {
            //            //        SumKosten = myArtikelTarifPosTable.Compute("SUM(Kosten)", strFilter);
            //            //    }
            //            //    decTmp = 0;
            //            //    Decimal.TryParse(SumKosten.ToString(), out decTmp);
            //            //    myRGPos.NettoPreis = decTmp;

            //            //    if (myArtikelTarifPosTable.Columns.Contains("Dauer"))
            //            //    {
            //            //        SumDauer = myArtikelTarifPosTable.Compute("SUM(Dauer)", strFilter);
            //            //    }
            //            //    int iTmp = 0;
            //            //    int.TryParse(SumDauer.ToString(), out iTmp);
            //            //    myRGPos.CalcModValue = iTmp;
            //            //}
            //        }
            //        else
            //        {
            //            if (myArtikelTarifPosTable != null)
            //            {
            //                if (myArtikelTarifPosTable.Rows.Count > 0)
            //                {
            //                    if (myArtikelTarifPosTable.Columns.Contains("Menge"))
            //                    {
            //                        SumMenge = myArtikelTarifPosTable.Compute("SUM(Menge)", strFilter);
            //                    }
            //                    decTmp = 0;
            //                    Decimal.TryParse(SumMenge.ToString(), out decTmp);
            //                    myRGPos.Menge = decTmp * decUFact;
            //                    if (myArtikelTarifPosTable.Columns.Contains("Kosten"))
            //                    {
            //                        SumKosten = myArtikelTarifPosTable.Compute("SUM(Kosten)", strFilter);
            //                    }
            //                    decTmp = 0;
            //                    Decimal.TryParse(SumKosten.ToString(), out decTmp);
            //                    myRGPos.NettoPreis = decTmp;

            //                    if (myArtikelTarifPosTable.Columns.Contains("Dauer"))
            //                    {
            //                        SumDauer = myArtikelTarifPosTable.Compute("SUM(Dauer)", strFilter);
            //                    }
            //                    int iTmp = 0;
            //                    int.TryParse(SumDauer.ToString(), out iTmp);
            //                    myRGPos.CalcModValue = iTmp;
            //                }
            //            }
            //            else
            //            {
            //                // mr 202050506
            //                if (dtArtikelLagerbestand.Rows.Count > 0)
            //                {
            //                    if (dtArtikelLagerbestand.Columns.Contains("Menge"))
            //                    {
            //                        SumMenge = dtArtikelLagerbestand.Compute("SUM(Menge)", strFilter);
            //                    }
            //                    decTmp = 0;
            //                    Decimal.TryParse(SumMenge.ToString(), out decTmp);
            //                    myRGPos.Menge = decTmp * decUFact;
            //                    if (dtArtikelLagerbestand.Columns.Contains("Kosten"))
            //                    {
            //                        SumKosten = dtArtikelLagerbestand.Compute("SUM(Kosten)", strFilter);
            //                    }
            //                    decTmp = 0;
            //                    Decimal.TryParse(SumKosten.ToString(), out decTmp);
            //                    myRGPos.NettoPreis = decTmp;

            //                    if (dtArtikelLagerbestand.Columns.Contains("Dauer"))
            //                    {
            //                        SumDauer = dtArtikelLagerbestand.Compute("SUM(Dauer)", strFilter);
            //                    }
            //                    int iTmp = 0;
            //                    int.TryParse(SumDauer.ToString(), out iTmp);
            //                    myRGPos.CalcModValue = iTmp;
            //                }
            //            }
            //            //if (dtArtikelLagerbestand.Rows.Count > 0)
            //            //{
            //            //    if (dtArtikelLagerbestand.Columns.Contains("Menge"))
            //            //    {
            //            //        SumMenge = dtArtikelLagerbestand.Compute("SUM(Menge)", strFilter);
            //            //    }
            //            //    decTmp = 0;
            //            //    Decimal.TryParse(SumMenge.ToString(), out decTmp);
            //            //    myRGPos.Menge = decTmp * decUFact;
            //            //    if (dtArtikelLagerbestand.Columns.Contains("Kosten"))
            //            //    {
            //            //        SumKosten = dtArtikelLagerbestand.Compute("SUM(Kosten)", strFilter);
            //            //    }
            //            //    decTmp = 0;
            //            //    Decimal.TryParse(SumKosten.ToString(), out decTmp);
            //            //    myRGPos.NettoPreis = decTmp;

            //            //    if (myArtikelTarifPosTable.Columns.Contains("Dauer"))
            //            //    {
            //            //        SumDauer = myArtikelTarifPosTable.Compute("SUM(Dauer)", strFilter);
            //            //    }
            //            //    int iTmp = 0;
            //            //    int.TryParse(SumDauer.ToString(), out iTmp);
            //            //    myRGPos.CalcModValue = iTmp;
            //            //}
            //        }
            //        break;

            //    case enumCalcultationModus.Pauschal:
            //        if (myArtikelTarifPosTable.Rows.Count > 0)
            //        {
            //            if (myArtikelTarifPosTable.Columns.Contains("Menge"))
            //            {
            //                SumMenge = myArtikelTarifPosTable.Compute("SUM(Menge)", strFilter);
            //            }
            //            decTmp = 0;
            //            Decimal.TryParse(SumMenge.ToString(), out decTmp);
            //            myRGPos.Menge = decTmp * decUFact;
            //            myRGPos.NettoPreis = myTarifPos.PreisEinheit;
            //        }                
            //        else
            //        {
            //            if (dtArtikelLagerbestand.Columns.Contains("Menge"))
            //            {
            //                SumMenge = dtArtikelLagerbestand.Compute("SUM(Menge)", strFilter);
            //            }
            //            decTmp = 0;
            //            Decimal.TryParse(SumMenge.ToString(), out decTmp);
            //            myRGPos.Menge = decTmp * decUFact;
            //            myRGPos.NettoPreis = myTarifPos.PreisEinheit;
            //        }
            //        break;

            //    default:

            //        break;
            //}


            //    if (myTarifPos.MargePreisEinheit > 0)
            //    {
            //        myRGPos.MargeEuro = myRGPos.Menge * myTarifPos.MargePreisEinheit;
            //        myRGPos.NettoPreis = myRGPos.NettoPreis - myRGPos.MargeEuro;
            //    }
            //    if (myTarifPos.MargeProzentEinheit > 0)
            //    {
            //        myRGPos.NettoPreis = myRGPos.NettoPreis - (myRGPos.NettoPreis * myRGPos.MargeProzent);
            //    }
            //}
            //myRGPos.NettoPreis = Math.Round(myRGPos.NettoPreis, 2, MidpointRounding.AwayFromZero);

            ////Bestände runden
            //Int32 iStellen = 3;
            //Einlagerung = Math.Round(Einlagerung, iStellen);
            //Auslagerung = Math.Round(Auslagerung, iStellen);
            //Lagerbestand = Math.Round(Lagerbestand, iStellen);
            //LagerTransport = Math.Round(LagerTransport, iStellen);
            //Direktanlieferung = Math.Round(Direktanlieferung, iStellen);
            //Ruecklieferung = Math.Round(Ruecklieferung, iStellen);
            //Vorfracht = Math.Round(Vorfracht, iStellen);
            //Gleisgebuehr = Math.Round(Gleisgebuehr, iStellen);
            //Nebenkosten = Math.Round(Nebenkosten, 4);
            //Toll = Math.Round(Toll, iStellen);

            //AddNewRowToDataTableRGVorschau(ref myRGPos);
        }
        ///<summary>clsFaktLager / UmrechnungEinheitenVonBasisAufAbrechnungsEinheit</summary>
        ///<remarks>Rechnet die Einheit der Basis-einheit in die der Abrechnungseinheit um.
        ///         Die Funktion liefert den Faktor, mit der die Einheitsanzahl multipliziert werden muss, damit
        ///         die Menge der Abrechnungseinheit entspricht</remarks>
        //public decimal UmrechnungEinheitenVonBasisAufAbrechnungsEinheit(string strBasisEinheit, string strAbrEinheit)
        //{
        //    decimal decFaktor = 1;
        //    if (strBasisEinheit != strAbrEinheit)
        //    {
        //        //Umrechnung für vorgegebene Einheiten
        //        switch (strBasisEinheit)
        //        {
        //            case "to":
        //            case "TO":
        //            case "To":
        //                if ((strAbrEinheit == "kg") || (strAbrEinheit == "Kg") || (strAbrEinheit == "KG"))
        //                {
        //                    decFaktor = 1000;
        //                }
        //                break;

        //            case "kg":
        //            case "Kg":
        //            case "KG":
        //                if ((strAbrEinheit == "to") || (strAbrEinheit == "To") || (strAbrEinheit == "TO"))
        //                {
        //                    decFaktor = 0.001M;
        //                }
        //                break;
        //        }
        //    }
        //    return decFaktor;
        //}
        ///<summary>clsFaktLager / GetRGFromDB</summary>
        ///<remarks>Erstellt die Datatable für das Rechnungsgrid.</remarks>
        public void GetRGFromDB()
        {
            dtRGVorschau = new DataTable("Vorschau");
            dtRGVorschau = Rechnung.GetRechnung();
            //Table Rechnung
            InitDataTableRechnung();
            FormatDataTableRechnung();
        }
        ///<summary>clsFaktLager / AddNewRowToDataTableRGVorschau</summary>
        ///<remarks>Erstellt die Datatable für das Rechnungsgrid.</remarks>
        private void AddNewRowToDataTableRGVorschau(ref clsRGPositionen RGPos)
        {
            //if (dtRGVorschau.Rows.Count > 0)
            //{
            DataRow row = dtRGVorschau.NewRow();
            row["ID"] = 0;
            row["RGNr"] = 0;
            row["Datum"] = DateTime.Now;
            row["RGTableID"] = 0;
            row["Position"] = RGPos.Position;
            row["Text"] = RGPos.RGText;
            row["Abrechnungseinheit"] = RGPos.AbrEinheit;
            row["Einzelpreis"] = RGPos.EinzelPreis;
            row["Menge"] = RGPos.Menge;
            row["NettoPreis"] = RGPos.NettoPreis;
            row["CalcModValue"] = RGPos.CalcModValue;
            row["CalcModus"] = RGPos.CalcModus;
            row["Abrechnungsart"] = RGPos.AbrechnungsArt;
            row["TarifPosId"] = RGPos.TarifPosID;
            row["Tariftext"] = RGPos.Tariftext;
            row["MargeEuro"] = RGPos.MargeEuro;
            row["MargeProzent"] = RGPos.MargeProzent;
            row["Anfangsbestand"] = RGPos.Anfangsbestand;
            row["Abgang"] = RGPos.Abgang;
            row["Zugang"] = RGPos.Zugang;
            row["Endbestand"] = RGPos.Endbestand;
            row["RGPosText"] = RGPos.RGPosText;
            row["FibuKto"] = RGPos.FibuKto;
            row["PricePerUnitFactor"] = RGPos.PricePerUnitFactor;
            //row["SortIndex"] = RGPos.Position;
            dtRGVorschau.Rows.Add(row);
            //}
        }
        ///<summary>clsFaktLager / InitDataTableRechnung</summary>
        ///<remarks>Erstellt die Datatable für das Rechnungsgrid.</remarks>
        private void InitDataTableRechnung()
        {
            dtRechnung = new DataTable("Rechnung");
            dtRechnung.Columns.Add("Pos", typeof(Int32));
            dtRechnung.Columns.Add("Text", typeof(String));
            dtRechnung.Columns.Add("Menge", typeof(Decimal));
            dtRechnung.Columns.Add("Einheit", typeof(String));
            dtRechnung.Columns.Add("€/Einheit", typeof(Decimal));
            dtRechnung.Columns.Add("CalcModus", typeof(Int32));
            dtRechnung.Columns.Add("CalcModValue", typeof(Int32));     //CalcModValue
            dtRechnung.Columns.Add("Marge €", typeof(Decimal));
            dtRechnung.Columns.Add("Marge %", typeof(Decimal));
            dtRechnung.Columns.Add("Netto €", typeof(Decimal));
            dtRechnung.Columns.Add("SumCalc", typeof(Boolean));
            dtRechnung.Columns.Add("Abrechnungsart", typeof(String));
            dtRechnung.Columns.Add("TarifPosID", typeof(Decimal));
            dtRechnung.Columns.Add("Tariftext", typeof(String));
            dtRechnung.Columns.Add("Anfangsbestand", typeof(String));
            dtRechnung.Columns.Add("Abgang", typeof(String));
            dtRechnung.Columns.Add("Zugang", typeof(String));
            dtRechnung.Columns.Add("Endbestand", typeof(String));
            dtRechnung.Columns.Add("RGPosText", typeof(String));
            dtRechnung.Columns.Add("FibuKto", typeof(Int32));
            dtRechnung.Columns.Add("PricePerUnitFactor", typeof(Decimal)); 
            dtRechnung.Columns.Add("TarifPricePerUnit", typeof(Decimal));

        }
        ///<summary>clsFaktLager / FormatDataTableRechnung</summary>
        ///<remarks>Datatable wird formatiert und die Daten entsprechend der Ausgabe eingefügt.</remarks>
        private void FormatDataTableRechnung()
        {
            string strTmpRGText = string.Empty;
            Netto = 0;

            if (dtRGVorschau.Rows.Count > 0)
            {
                Int32 i = 0;
                while (i <= dtRGVorschau.Rows.Count - 1)
                {
                    DataRow row = dtRechnung.NewRow();

                    if (strTmpRGText != dtRGVorschau.Rows[i]["Text"].ToString())
                    {
                        strTmpRGText = dtRGVorschau.Rows[i]["Text"].ToString();
                        row["Text"] = strTmpRGText;
                        row["SumCalc"] = false;
                        //dtRechnung.Rows.Add(row);
                    }
                    else
                    {
                        row["Pos"] = (Int32)dtRGVorschau.Rows[i]["Position"];
                        string strTmp = dtRGVorschau.Rows[i]["Text"].ToString();
                        row["Text"] = dtRGVorschau.Rows[i]["Text"].ToString();
                        row["Menge"] = (decimal)dtRGVorschau.Rows[i]["Menge"];
                        row["Einheit"] = dtRGVorschau.Rows[i]["Abrechnungseinheit"].ToString();
                        row["€/Einheit"] = (decimal)dtRGVorschau.Rows[i]["Einzelpreis"];
                        row["CalcModus"] = (Int32)dtRGVorschau.Rows[i]["CalcModus"];
                        row["CalcModValue"] = (Int32)dtRGVorschau.Rows[i]["CalcModValue"];
                        row["Marge €"] = (decimal)dtRGVorschau.Rows[i]["MargeEuro"];
                        row["Marge %"] = (decimal)dtRGVorschau.Rows[i]["MargeProzent"];
                        row["Netto €"] = (decimal)dtRGVorschau.Rows[i]["NettoPreis"];
                        row["SumCalc"] = true;
                        row["Abrechnungsart"] = dtRGVorschau.Rows[i]["Abrechnungsart"].ToString();
                        row["TarifPosId"] = (decimal)dtRGVorschau.Rows[i]["TarifPosId"];
                        row["Tariftext"] = dtRGVorschau.Rows[i]["Tariftext"].ToString();
                        row["Anfangsbestand"] = (decimal)dtRGVorschau.Rows[i]["Anfangsbestand"];
                        row["Abgang"] = (decimal)dtRGVorschau.Rows[i]["Abgang"];
                        row["Zugang"] = (decimal)dtRGVorschau.Rows[i]["Zugang"];
                        row["Endbestand"] = (decimal)dtRGVorschau.Rows[i]["Endbestand"];
                        row["RGPosText"] = dtRGVorschau.Rows[i]["RGPosText"].ToString();
                        row["FibuKto"] = (Int32)dtRGVorschau.Rows[i]["FibuKto"];
                        row["PricePerUnitFactor"] = (decimal)dtRGVorschau.Rows[i]["PricePerUnitFactor"];
                        dtRechnung.Rows.Add(row);
                        Netto = Netto + (decimal)dtRGVorschau.Rows[i]["NettoPreis"];
                        i++;
                    }
                }
            }
        }
        ///<summary>clsFaktLager / GetSQLGleisStellKosten</summary>
        ///<remarks></remarks>  
        private string GetSQLGleisStellKosten(ref clsTarif myTarif)
        {
            string strSqlReturn = string.Empty;
            decimal decTmp = 0;
            /*********************
             * CalcGleisModus : 
             * 0 = Tagesbasiert 
             * 1 = Eingang
             * 2 = Ausgang 
             * 3 = Ein + Aus 
            **********************/
            decimal CalcGleisModus = 3; // aktuell für honselmann
            if (CalcGleisModus == 0 || CalcGleisModus == 1 || CalcGleisModus == 3)
            {
                strSqlReturn = "Select a.ID " +
                                        ", CAST(" + myTarif.TarifPosition.ID + " as decimal) as TarifPosID " +
                                        ", '" + enumTarifArtLager.Gleisstellgebühr.ToString() + "' as Abrechnungsart" +
                                        ", CAST(0 as decimal) as Menge" +
                                        ", CAST('" + myTarif.TarifPosition.PreisEinheit.ToString().Replace(",", ".") + "' as money) as Preis " +
                                        ", CAST(0 as INT) as Dauer " +
                                        ", CAST(0 as money) as Kosten " +
                                        ", CAST(" + myTarif.Modus + " as Int) as TarifModus " +
                                        ", CAST(0 as money) as VersPreis " +
                                        ", CAST(0 as bit) as IsUBCalc " +
                                        ", '" + clsSystem.const_DefaultDateTimeValue_Min + "' as EDatum " +
                                        ", '" + myTarif.TarifPosition.BasisEinheit + "' as BasisEinheit " +
                                        ", '" + myTarif.TarifPosition.AbrEinheit + "' as AbrEinheit " +
                                        ", Cast(b.Date as date) as Datum " +
                                        ", b.WaggonNo " +

                                        "FROM Artikel a  " +
                                            "INNER JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                            "WHERE " +
                                                "b.Auftraggeber=" + Auftraggeber + " " +
                                                //"AND (b.Date between '" + VonZeitraum.Date.ToShortDateString() + "' and '" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') " +
                                                "AND (b.Date between '" + VonZeitraum.Date.ToShortDateString() + "' and '" + BisZeitraum.AddDays(1).Date.ToShortDateString() + "') " +
                                                //"AND b.SpedID=0 AND b.KFZ='' AND b.WaggonNo<>'' " +
                                                "AND b.KFZ='' AND (b.WaggonNo<>'' or isWaggon=1) and b.[check]=1 " +
                                                "AND a.GArtID IN (" + myTarif.TarifGArtZuweisung.SQLGArtIDString + ")" +
                                                "AND a.ID NOT IN ( " +
                                                        "Select " +
                                                            "a.ArtikelID " +
                                                            "FROM RGPosArtikel a " +
                                                                "INNER JOIN TarifPositionen b ON b.ID=a.TarifPosID " +
                                                                "INNER JOIN RGPositionen p ON p.ID = a.RGPosID " +
                                                                "INNER JOIN Rechnungen r ON r.ID=p.RGTableID " +
                                                                "WHERE " +
                                                                    "AbgerechnetVon='" + VonZeitraum.ToShortDateString() + "' " +
                                                                    "AND AbgerechnetBis= '" + BisZeitraum.ToShortDateString() + "'  " +
                                                                    "AND b.TarifPosArt='" + enumTarifArtLager.Gleisstellgebühr.ToString() + "' " +
                                                                    "AND r.ID NOT IN (Select ID FROM Rechnungen " +
                                                                                                "WHERE ID IN(Select StornoID FROM Rechnungen WHERE StornoID>0)) " +
                                                ") ";
            }
            if (CalcGleisModus == 0 || CalcGleisModus == 3)
            {
                strSqlReturn += " UNION ";
            }
            if (CalcGleisModus == 0 || CalcGleisModus == 2 || CalcGleisModus == 3)
            {
                strSqlReturn += "Select a.ID " +
                        ", CAST(" + myTarif.TarifPosition.ID + " as decimal) as TarifPosID " +
                        ", '" + enumTarifArtLager.Gleisstellgebühr.ToString() + "' as Abrechnungsart" +
                        ", CAST(0 as decimal) as Menge" +
                        ", CAST('" + myTarif.TarifPosition.PreisEinheit.ToString().Replace(",", ".") + "' as money) as Preis " +
                        ", CAST(1 as INT) as Dauer " +
                        ", CAST(0 as money) as Kosten " +
                        ", CAST(" + myTarif.Modus + " as Int) as TarifModus " +
                        ", CAST(0 as money) as VersPreis " +
                        ", CAST(0 as bit) as IsUBCalc " +
                        ", '" + clsSystem.const_DefaultDateTimeValue_Min + "' as EDatum " +
                        ", '" + myTarif.TarifPosition.BasisEinheit + "' as BasisEinheit " +
                        ", '" + myTarif.TarifPosition.AbrEinheit + "' as AbrEinheit " +
                        ", Cast(c.Datum as date) as Datum " +
                        ", c.WaggonNo " +

                        "FROM Artikel a " +
                            "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                            "WHERE " +
                                "c.Auftraggeber=" + Auftraggeber + " " +
                                "AND (c.Datum between '" + VonZeitraum.Date.ToShortDateString() + "' and '" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') " +
                                //"AND (c.Datum between '" + VonZeitraum.Date.ToShortDateString() + "' and '" + BisZeitraum.Date.ToShortDateString() + "') " +
                                //"AND c.SpedID=0 AND c.KFZ='' AND c.WaggonNo<>'' " +
                                "AND c.KFZ='' AND (c.WaggonNo<>'' or isWaggon=1) AND Checked=1 " +
                                "AND a.GArtID IN (" + myTarif.TarifGArtZuweisung.SQLGArtIDString + ")" +
                                "AND a.ID NOT IN ( " +
                                        "Select " +
                                            "a.ArtikelID " +
                                            "FROM RGPosArtikel a " +
                                                "INNER JOIN TarifPositionen b ON b.ID=a.TarifPosID " +
                                                "INNER JOIN RGPositionen p ON p.ID = a.RGPosID " +
                                                "INNER JOIN Rechnungen r ON r.ID=p.RGTableID " +
                                                "WHERE " +
                                                    "AbgerechnetVon='" + VonZeitraum.ToShortDateString() + "' " +
                                                    "AND AbgerechnetBis= '" + BisZeitraum.ToShortDateString() + "'  " +
                                                    "AND b.TarifPosArt='" + enumTarifArtLager.Gleisstellgebühr.ToString() + "' " +
                                                    "AND r.ID NOT IN (Select ID FROM Rechnungen " +
                                                                                "WHERE ID IN(Select StornoID FROM Rechnungen WHERE StornoID>0)) " +
                                ")";
            }

            return strSqlReturn;
        }
        ///<summary>clsFaktLager / GetSQLArtikelNebenkosten</summary>
        ///<remarks></remarks>  
        private string GetSQLArtikelNebenkosten(ref clsTarif myTarif)
        {
            string strSqlReturn = string.Empty;
            decimal decTmp = 0;
            strSqlReturn = "Select  a.ArtikelID as ID" +
                                        ", CAST(" + myTarif.TarifPosition.ID + " as decimal) as TarifPosID" +
                                        ", '" + myTarif.TarifPosition.TarifPosArt + "' as Abrechnungsart " +
                                        ", a.Menge as Menge" +
                                        ", CAST(a.Preis as money) as Preis " +                                       
                                        ", 0 as Dauer " +
                                        ", (a.Menge * a.Preis) as Kosten " +                                       
                                        ", CAST(" + myTarif.Modus + " as Int) as TarifModus " +
                                        ", CAST(" + myTarif.VersPreis.ToString().Replace(",", ".") + " as money) as VersPreis " +
                                        ", CAST(0 as bit) as IsUBCalc " +
                                        ", d.Date as EDatum " +
                                        ", aus.Datum as ADatum "+
                                        ", '" + myTarif.TarifPosition.BasisEinheit + "' as BasisEinheit " +
                                        ", 0 as CalcModus " + //mr 17.1.22025
                                        ", a.Menge as PricePerUnitFactor" + // mr 17.11.2025
                                        ", e.Einheit as AbrEinheit " +
                                        ", a.RGText as ArtRGTxt " +
                                        ", a.Datum as LstDatum " +
                                        ", a.Einheit as LstEinheit " +

                                        "FROM ExtraChargeAssignment a " +
                                            "INNER JOIN ExtraCharge e ON e.ID=a.ExtraChargeID " +
                                            "INNER JOIN Artikel b ON b.ID=a.ArtikelID " +
                                            "INNER JOIN LEingang d ON d.ID=b.LEingangTableID " +
                                            "LEFT JOIN LAusgang aus on aus.ID = b.LAusgangTableID "+
                                            "WHERE " +
                                            "d.Auftraggeber=" + Auftraggeber + " " +
                                            " AND b.AB_ID=" + (int)myTarif.AbBereich.ID +
                                            " AND (a.Datum between '" + VonZeitraum.Date.ToShortDateString() + "' and '" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') ";

            //bezgogen auf bestimmte Güterarten
            if (myTarif.ExistLinkedGArt)
            {
                strSqlReturn = strSqlReturn + " AND b.GArtID IN (" + myTarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
            }

            strSqlReturn = strSqlReturn + " " +
                            "AND a.ID NOT IN ( " +
                                                "Select " +
                                                    "a.ArtikelID " +
                                                    "FROM RGPosArtikel a " +
                                                        "INNER JOIN TarifPositionen b ON b.ID=a.TarifPosID " +
                                                        "INNER JOIN RGPositionen p ON p.ID = a.RGPosID " +
                                                        "INNER JOIN Rechnungen r ON r.ID=p.RGTableID " +
                                                        "WHERE " +
                                                            "AbgerechnetVon='" + VonZeitraum.ToShortDateString() + "' " +
                                                            "AND AbgerechnetBis= '" + BisZeitraum.ToShortDateString() + "'  " +
                                                            "AND b.TarifPosArt='" + enumTarifArtLager.Nebenkosten.ToString() + "' " +
                                                            "AND r.ID NOT IN (Select ID FROM Rechnungen " +
                                                                                        "WHERE ID IN(Select StornoID FROM Rechnungen WHERE StornoID>0)) " +
                                            ") ORDER BY a.Datum;";
            return strSqlReturn;
        }
        ///<summary>clsFaktLager / GetSQLArtikelNebenkosten</summary>
        ///<remarks></remarks>  
        private string GetSQLWaggonAnzahl(ref clsTarif myTarif)
        {
            string strSqlReturn = string.Empty;
            decimal decTmp = 0;
            /*********************
             * CalcGleisModus : 
             * 0 = Tagesbasiert 
             * 1 = Eingang
             * 2 = Ausgang 
             * 3 = Ein + Aus 
            **********************/
            decimal CalcGleisModus = 3; // aktuell für honselmann

            strSqlReturn = "Select count(*) as Anzahl ,CAST('" + myTarif.TarifPosition.PreisEinheit.ToString().Replace(",", ".") + "' as money) as Preis from (	Select * from 	( ";
            if (CalcGleisModus == 0 || CalcGleisModus == 1 || CalcGleisModus == 3)
            {
                strSqlReturn += "Select WaggonNo,Cast(b.Date as date ) as Datum ";
                if (CalcGleisModus == 3)
                {
                    strSqlReturn += ",'E' as Dir ";
                }
                strSqlReturn += "FROM Artikel a " +
                                            "INNER JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                            "WHERE " +
                                                "b.Auftraggeber=" + Auftraggeber + " " +
                                                "AND (b.Date between '" + VonZeitraum.Date.ToShortDateString() + "' and '" + BisZeitraum.AddDays(1).Date.ToShortDateString() + "') " +
                                                "AND b.KFZ='' AND (b.WaggonNo<>'' or isWaggon=1) and b.[check]=1 " +
                                                "AND a.GArtID IN (" + myTarif.TarifGArtZuweisung.SQLGArtIDString + ") " +
                                                "AND a.ID NOT IN ( " +
                                                        "Select " +
                                                            "a.ArtikelID " +
                                                            "FROM RGPosArtikel a " +
                                                                "INNER JOIN TarifPositionen b ON b.ID=a.TarifPosID " +
                                                                "INNER JOIN RGPositionen p ON p.ID = a.RGPosID " +
                                                                "INNER JOIN Rechnungen r ON r.ID=p.RGTableID " +
                                                                "WHERE " +
                                                                    "AbgerechnetVon='" + VonZeitraum.ToShortDateString() + "' " +
                                                                    "AND AbgerechnetBis= '" + BisZeitraum.ToShortDateString() + "'  " +
                                                                    "AND b.TarifPosArt='" + enumTarifArtLager.Gleisstellgebühr.ToString() + "' " +
                                                                    "AND r.ID NOT IN (Select ID FROM Rechnungen " +
                                                                                                "WHERE ID IN(Select StornoID FROM Rechnungen WHERE StornoID>0)) " +
                                                ") ";
            }
            if (CalcGleisModus == 0 || CalcGleisModus == 3)
            {
                strSqlReturn += " UNION ";
            }
            if (CalcGleisModus == 0 || CalcGleisModus == 2 || CalcGleisModus == 3)
            {
                strSqlReturn += "Select WaggonNo,Cast(c.Datum as date ) as Datum ";
                if (CalcGleisModus == 3)
                {
                    strSqlReturn += ",'A' as Dir ";
                }
                strSqlReturn += "FROM Artikel a " +
                            "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                            "WHERE " +
                                "c.Auftraggeber=" + Auftraggeber + " " +
                                "AND (c.Datum between '" + VonZeitraum.Date.ToShortDateString() + "' and '" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') " +
                                "AND c.KFZ='' AND (c.WaggonNo<>'' or isWaggon=1) AND Checked=1 " +
                                "AND a.GArtID IN (" + myTarif.TarifGArtZuweisung.SQLGArtIDString + ") " +
                                "AND a.ID NOT IN ( " +
                                        "Select " +
                                            "a.ArtikelID " +
                                            "FROM RGPosArtikel a " +
                                                "INNER JOIN TarifPositionen b ON b.ID=a.TarifPosID " +
                                                "INNER JOIN RGPositionen p ON p.ID = a.RGPosID " +
                                                "INNER JOIN Rechnungen r ON r.ID=p.RGTableID " +
                                                "WHERE " +
                                                    "AbgerechnetVon='" + VonZeitraum.ToShortDateString() + "' " +
                                                    "AND AbgerechnetBis= '" + BisZeitraum.ToShortDateString() + "'  " +
                                                    "AND b.TarifPosArt='" + enumTarifArtLager.Gleisstellgebühr.ToString() + "' " +
                                                    "AND r.ID NOT IN (Select ID FROM Rechnungen " +
                                                                                "WHERE ID IN(Select StornoID FROM Rechnungen WHERE StornoID>0)) " +
                                ")";

            }
            strSqlReturn += ") as Liste group by ";
            if (CalcGleisModus == 3)
            {
                strSqlReturn += "Dir, ";
            }
            strSqlReturn += "Datum,WaggonNo ) as Waggonliste ";

            return strSqlReturn;
        }


        ///<summary>clsFaktLager / GetAnfangsbestand</summary>
        ///<remarks>Ermittel den Anfangsbestand des Kunden zu dem gewünschten Zeitpunkt.
        ///         Der Anfangsbestand enthält alle Artikel, die vor dem Zeitpunkt X. Der Anfangsbestand wird auf der Tarifbasis 
        ///         des Lagergeld ermittelt</remarks>  
        private string GetAnfangsbestand(ref clsTarif myTarif, bool myBForBestand, decimal myTarifPosID, bool myBCalcStaffel, bool myIsAnfangsBestandCalculation, bool bUseBKZ)
        {
            string strSqlReturn = string.Empty;
            string strSQLTmp = string.Empty;
            string strAbrArt = string.Empty;
            if (myTarifPosID == 0)
            {
                for (Int32 i = 0; i <= myTarif.dtTarifpositionen.Rows.Count - 1; i++)
                {
                    strAbrArt = myTarif.dtTarifpositionen.Rows[i]["Art"].ToString();
                    if (
                            strAbrArt.Equals(const_Abrechnungsart_Einlagerung.ToString()) ||
                            strAbrArt.Equals(const_Abrechnungsart_Auslagerung.ToString()) ||
                            strAbrArt.Equals(const_Abrechnungsart_Lagerkosten.ToString())
                        )
                    {
                        //Tarifposition zuweisen 
                        myTarif.TarifPosition = new clsTarifPosition();
                        myTarif.TarifPosition._GL_User = this._GL_User;
                        myTarif.TarifPosition.ID = (decimal)myTarif.dtTarifpositionen.Rows[i]["ID"];
                        myTarif.TarifPosition.Fill();
                        break;
                    }
                }
            }
            //SQL für Bestandsabfrage
            if (
                (myTarif.TarifPosition.DatenfeldArtikel != string.Empty) ||
                (myTarif.TarifPosition.DatenfeldArtikel == null)
                )
            {
                strSQLTmp = "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                                  "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                                  "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                                  "WHERE " +
                                                  "(" +
                                                  "( " +
                                                        "b.Auftraggeber=" + Auftraggeber + " ";
                if (this.bUseBKZ)
                {
                    strSQLTmp += " and a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                }
                else
                {
                    strSQLTmp += " and  a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
                }

                strSQLTmp += " AND b.DirectDelivery=0 " +
                             "AND b.Mandant=" + MandantenID + " " +
                             "AND b.AbBereich=" + this.Sys.AbBereich.ID + " ";
                //bezgogen auf bestimmte Güterarten
                if (myTarif.ExistLinkedGArt)
                {
                    strSQLTmp = strSQLTmp + " AND a.GArtID IN (" + myTarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                }
                if (myIsAnfangsBestandCalculation)
                {
                    strSQLTmp = strSQLTmp + "AND b.Date <'" + VonZeitraum.Date.ToShortDateString() + "' ";
                }
                else
                {
                    //FReie Lagertage
                    strSQLTmp = strSQLTmp + "AND DATEDIFF(dd, b.Date, '" + BisZeitraum.Date.ToShortDateString() + "')>=" + myTarif.TarifPosition.Lagerdauer + " ";

                    if (myBCalcStaffel)
                    {
                        //Staffel
                        if (myTarif.TarifPosition.einheitenbezogen)
                        {
                            strSQLTmp = strSQLTmp + " AND a.Brutto>=" + myTarif.TarifPosition.EinheitenVon + " AND " +
                                                      "a.Brutto<=" + myTarif.TarifPosition.EinheitenBis + " ";
                        }
                        //Zeitraumbezogen
                        if (myTarif.TarifPosition.zeitraumbezogen)
                        {
                            if (myTarif.TarifPosition.TEinheiten != string.Empty)
                            {
                                DateTime dtVonTmp = VonZeitraum.Date;
                                DateTime dtBisTmp = BisZeitraum.Date;
                                //Verlgleich EInlagerdatum + angebene Zeitspanne
                                switch (myTarif.TarifPosition.TEinheiten)
                                {
                                    case "Tage":
                                        dtVonTmp = VonZeitraum.Date.AddDays(-myTarif.TarifPosition.TEinheitenBis);
                                        dtVonTmp = Functions.GetFirstDayOfMonth(dtVonTmp);
                                        dtBisTmp = BisZeitraum.Date.AddDays(-myTarif.TarifPosition.TEinheitenVon);
                                        dtBisTmp = Functions.GetLastDayOfMonth(dtBisTmp);
                                        break;
                                    case "Monate":
                                        //Da hier zurückgerechnet werden muss Bsp. Abrechnungsmonat = Januar und TEinheiten sind 0-1 dann muss also 
                                        //das Datum für den Dezember berücksichtigt werden ;
                                        dtVonTmp = VonZeitraum.Date.AddMonths(-myTarif.TarifPosition.TEinheitenBis);
                                        dtVonTmp = Functions.GetFirstDayOfMonth(dtVonTmp);
                                        dtBisTmp = BisZeitraum.Date.AddMonths(-myTarif.TarifPosition.TEinheitenVon);
                                        dtBisTmp = Functions.GetLastDayOfMonth(dtBisTmp);
                                        break;

                                }
                                strSQLTmp = strSQLTmp + " AND (b.Date between '" + dtVonTmp.ToShortDateString() + "' AND '" + dtBisTmp.Date.AddDays(1).ToShortDateString() + "') ";
                            }
                        }
                        else
                        {
                            strSQLTmp = strSQLTmp + "AND b.Date <'" + VonZeitraum.Date.ToShortDateString() + "' ";
                        }
                    }
                    else
                    {
                        //Zeitraumbezogen
                        if (myTarif.TarifPosition.zeitraumbezogen)
                        {
                            DateTime dtVonTmp = VonZeitraum.Date;
                            DateTime dtBisTmp = BisZeitraum.Date;
                            //Verlgleich EInlagerdatum + angebene Zeitspanne
                            switch (myTarif.TarifPosition.TEinheiten)
                            {
                                case "Tage":
                                    dtVonTmp = VonZeitraum.Date.AddDays(-myTarif.TarifPosition.TEinheitenBis);
                                    dtVonTmp = Functions.GetFirstDayOfMonth(dtVonTmp);
                                    dtBisTmp = BisZeitraum.Date.AddDays(-myTarif.TarifPosition.TEinheitenVon);
                                    dtBisTmp = Functions.GetLastDayOfMonth(dtBisTmp);
                                    break;
                                case "Monate":
                                    //Da hier zurückgerechnet werden muss Bsp. Abrechnungsmonat = Januar und TEinheiten sind 0-1 dann muss also 
                                    //das Datum für den Dezember berücksichtigt werden 
                                    dtVonTmp = VonZeitraum.Date.AddMonths(-myTarif.TarifPosition.TEinheitenBis);
                                    dtVonTmp = Functions.GetFirstDayOfMonth(dtVonTmp);
                                    dtBisTmp = BisZeitraum.Date.AddMonths(-myTarif.TarifPosition.TEinheitenVon);
                                    dtBisTmp = Functions.GetLastDayOfMonth(dtBisTmp);
                                    break;

                            }
                            strSQLTmp = strSQLTmp + " AND (b.Date between '" + dtVonTmp.ToShortDateString() + "' AND '" + dtBisTmp.Date.AddDays(1).ToShortDateString() + "') ";
                        }
                        else
                        {
                            strSQLTmp = strSQLTmp + "AND b.Date <'" + VonZeitraum.Date.ToShortDateString() + "' ";
                        }
                    }
                }
                strSQLTmp = strSQLTmp + ") " +
                                    "OR " +
                                    "(" +
                                        "b.Auftraggeber=" + Auftraggeber + " " +
                                        //"AND (c.Datum between '" + VonZeitraum.Date.ToShortDateString() + "' AND '" + DateTime.Now.Date.AddDays(1).ToShortDateString() + "') ";
                                        "AND c.Datum >= '" + VonZeitraum.Date.ToShortDateString() + "' " +
                                        "AND b.Date < '" + VonZeitraum.Date.ToShortDateString() + "' ";

                //-- Korrektur 14.05.2024 war falsch
                //"AND b.Date <= '" + BisZeitraum.Date.ToShortDateString() + "' "+
                //"AND c.Datum >= '" + BisZeitraum.Date.ToShortDateString() + "' ";


                if (bUseBKZ)//if (bUseBKZ)
                {
                    strSQLTmp += " and a.BKZ=0 AND a.CheckArt=1 AND b.[Check]=1 ";
                }
                else
                {
                    strSQLTmp += " and a.CheckArt=1 AND b.[Check]=1 and c.Checked=1 ";
                }
                strSQLTmp += " AND b.DirectDelivery=0 " +
                             " AND b.Mandant=" + MandantenID + " " +
                             " AND b.AbBereich=" + this.Sys.AbBereich.ID + " ";

                //bezgogen auf bestimmte Güterarten
                if (myTarif.ExistLinkedGArt)
                {
                    strSQLTmp = strSQLTmp + " AND a.GArtID IN (" + myTarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                }
                if (myIsAnfangsBestandCalculation)
                {
                    strSQLTmp = strSQLTmp + "AND b.Date <'" + VonZeitraum.Date.ToShortDateString() + "' ";
                }
                else
                {
                    //freie Lagertage
                    // Korrektur 05.10.2016
                    strSQLTmp = strSQLTmp + "AND DATEDIFF(dd, b.Date, c.Datum)>=" + myTarif.TarifPosition.Lagerdauer + " ";
                    //strSQLTmp = strSQLTmp + "AND DATEDIFF(dd, b.Date, '" + BisZeitraum.Date.ToShortDateString() + "')>=" + myTarif.TarifPosition.Lagerdauer + " ";
                    if (myBCalcStaffel)
                    {
                        //Staffel
                        if (myTarif.TarifPosition.einheitenbezogen)
                        {
                            strSQLTmp = strSQLTmp + " AND a.Brutto>=" + myTarif.TarifPosition.EinheitenVon + " AND " +
                                                      "a.Brutto<=" + myTarif.TarifPosition.EinheitenBis + " ";
                        }
                        //Zeitraumbezogen
                        if (myTarif.TarifPosition.zeitraumbezogen)
                        {
                            if (myTarif.TarifPosition.TEinheiten != string.Empty)
                            {
                                DateTime dtVonTmp = VonZeitraum.Date;
                                DateTime dtBisTmp = BisZeitraum.Date;
                                //Verlgleich EInlagerdatum + angebene Zeitspanne
                                switch (myTarif.TarifPosition.TEinheiten)
                                {
                                    case "Tage":
                                        dtVonTmp = VonZeitraum.Date.AddDays(-myTarif.TarifPosition.TEinheitenBis);
                                        dtVonTmp = Functions.GetFirstDayOfMonth(dtVonTmp);
                                        dtBisTmp = BisZeitraum.Date.AddDays(-myTarif.TarifPosition.TEinheitenVon);
                                        dtBisTmp = Functions.GetLastDayOfMonth(dtBisTmp);
                                        break;
                                    case "Monate":
                                        //Da hier zurückgerechnet werden muss Bsp. Abrechnungsmonat = Januar und TEinheiten sind 0-1 dann muss also 
                                        //das Datum für den Dezember berücksichtigt werden 
                                        dtVonTmp = VonZeitraum.Date.AddMonths(-myTarif.TarifPosition.TEinheitenBis);
                                        dtVonTmp = Functions.GetFirstDayOfMonth(dtVonTmp);
                                        dtBisTmp = BisZeitraum.Date.AddMonths(-myTarif.TarifPosition.TEinheitenVon);
                                        dtBisTmp = Functions.GetLastDayOfMonth(dtBisTmp);
                                        break;

                                }
                                strSQLTmp = strSQLTmp + " AND (b.Date between '" + dtVonTmp.ToShortDateString() + "' AND '" + dtBisTmp.Date.AddDays(1).ToShortDateString() + "') ";

                            }
                            else
                            {
                                strSQLTmp = strSQLTmp + "AND b.Date <'" + VonZeitraum.Date.ToShortDateString() + "' ";
                            }
                        }
                    }
                    else
                    {
                        //Zeitraumbezogen
                        if (myTarif.TarifPosition.zeitraumbezogen)
                        {
                            DateTime dtVonTmp = VonZeitraum.Date;
                            DateTime dtBisTmp = BisZeitraum.Date;
                            //Verlgleich EInlagerdatum + angebene Zeitspanne
                            switch (myTarif.TarifPosition.TEinheiten)
                            {
                                case "Tage":
                                    dtVonTmp = VonZeitraum.Date.AddDays(-myTarif.TarifPosition.TEinheitenBis);
                                    dtVonTmp = Functions.GetFirstDayOfMonth(dtVonTmp);
                                    dtBisTmp = BisZeitraum.Date.AddDays(-myTarif.TarifPosition.TEinheitenVon);
                                    dtBisTmp = Functions.GetLastDayOfMonth(dtBisTmp);
                                    break;
                                case "Monate":
                                    //Da hier zurückgerechnet werden muss Bsp. Abrechnungsmonat = Januar und TEinheiten sind 0-1 dann muss also 
                                    //das Datum für den Dezember berücksichtigt werden 
                                    dtVonTmp = VonZeitraum.Date.AddMonths(-myTarif.TarifPosition.TEinheitenBis);
                                    dtVonTmp = Functions.GetFirstDayOfMonth(dtVonTmp);
                                    dtBisTmp = BisZeitraum.Date.AddMonths(-myTarif.TarifPosition.TEinheitenVon);
                                    dtBisTmp = Functions.GetLastDayOfMonth(dtBisTmp);
                                    break;

                            }
                            strSQLTmp = strSQLTmp + " AND (b.Date between '" + dtVonTmp.ToShortDateString() + "' AND '" + dtBisTmp.Date.AddDays(1).ToShortDateString() + "') ";
                        }
                        else
                        {
                            strSQLTmp = strSQLTmp + "AND b.Date <'" + VonZeitraum.Date.ToShortDateString() + "' ";
                        }
                    }
                }
                strSQLTmp = strSQLTmp + ") )";

                //Sperrlager im Anfangbestand immer rein - SLE will es drin haben 
                //wenn SPL Abrechnung dann wird es abgezogen
                ////--- ohne Sperrlager
                //strSQLTmp = strSQLTmp + " AND a.ID NOT IN (" +
                //                                               "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                //                                                     "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                //                                             ") ";

            }
            if (myBForBestand)
            {
                if (myTarif.TarifPosition.DatenfeldArtikel != null)
                {
                    //if (myTarif.TarifPosition.DatenfeldArtikel.Equals("ID"))
                    //{
                    //    strSqlReturn = "Select COUNT(a." + myTarif.TarifPosition.DatenfeldArtikel + ") as Quantity From Artikel a " + strSQLTmp;
                    //}
                    //else
                    //{
                    //    strSqlReturn = "Select SUM(a." + myTarif.TarifPosition.DatenfeldArtikel + ") as Quantity From Artikel a " + strSQLTmp;
                    //}

                    switch (myTarif.TarifPosition.DatenfeldArtikel)
                    {
                        case "ID":
                            strSqlReturn = "Select COUNT(a." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Artikel.", "") + ") as Quantity FROM Artikel a ";
                            break;
                        case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_ID:
                            strSqlReturn = "Select COUNT(a." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Artikel.", "") + ") as Quantity FROM Artikel a  ";
                            break;

                        case "Netto":
                        case "Brutto":
                        case "Anzahl":
                            strSqlReturn = "Select SUM(a." + myTarif.TarifPosition.DatenfeldArtikel + ") as Quantity FROM Artikel a  ";
                            break;

                        case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_Netto:
                        case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_Brutto:
                        case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_Anzahl:
                            strSqlReturn = "Select SUM(a." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Artikel.", "") + ") as Quantity FROM Artikel a  ";
                            break;

                        case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Eingang_Id:
                            strSqlReturn = "Select COUNT(b." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Eingang.", "") + ") as Quantity FROM Artikel a  ";
                            break;

                        case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Eingang_WaggonNo:
                            strSqlReturn = "Select COUNT(DISTINCT b." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Eingang.", "") + ") as Quantity FROM Artikel a  ";
                            break;
                    }
                    strSqlReturn += strSQLTmp;
                }
                else
                {
                    strSqlReturn = "Select 0 as Quantity From Artikel a " + strSQLTmp;
                }
            }
            else
            {
                //decimal decTmp = 0;
                strSqlReturn = GetMainSQLArtikelBestand(ref myTarif, strAbrArt, myBForBestand, myTarifPosID)
                                + strSQLTmp;
            }

            //Admin Preview
            if (!bCalcPreviewAdmin)
            {
                strSqlReturn = strSqlReturn + " " +
                                               "AND a.ID NOT IN ( " +
                                                                   "Select " +
                                                                       "a.ArtikelID " +
                                                                       "FROM RGPosArtikel a " +
                                                                           "INNER JOIN TarifPositionen b ON b.ID=a.TarifPosID " +
                                                                           "INNER JOIN RGPositionen p ON p.ID = a.RGPosID " +
                                                                           "INNER JOIN Rechnungen r ON r.ID=p.RGTableID " +
                                                                           "WHERE " +
                                                                               "AbgerechnetVon='" + VonZeitraum.ToShortDateString() + "' " +
                                                                               "AND AbgerechnetBis= '" + BisZeitraum.ToShortDateString() + "'  " +
                                                                               "AND b.TarifPosArt='" + enumTarifArtLager.Lagerkosten.ToString() + "' " +
                                                                               "AND b.ID=" + myTarif.TarifPosition.ID + " " +
                                                                               "AND r.Empfaenger=" + Auftraggeber + " " +
                                                                                   "AND r.ID NOT IN (Select ID FROM Rechnungen " +
                                                                                   "WHERE ID IN( Select StornoID FROM Rechnungen WHERE StornoID>0) OR Storno=1) " +
                                                               ") ";
            }

            return strSqlReturn;
        }
        ///<summary>clsFaktLager / GetSQLforBestand</summary>
        ///<remarks>Ermittel die korrekte SQLAnweisung zur Bestandsermittlung.</remarks>
        private string GetSQLforBestand(ref clsTarif myTarif, string myStrAbrArt, bool myBForBestand, decimal myTarifPosID)
        {
            string strSQLResult = string.Empty;
            string strSqlMenge = string.Empty;
            string strSqlArtID = string.Empty;
            string strSQLTmp = string.Empty;
            string strSqlGroup = string.Empty;
            if (myTarif.TarifPosition.DatenfeldArtikel != string.Empty)
            {
                switch (myTarif.TarifPosition.DatenfeldArtikel)
                {
                    case "ID":
                        strSqlMenge = "Select COUNT(a." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Artikel.", "") + ") as Quantity FROM Artikel a  ";
                        break;
                    case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_ID:
                        strSqlMenge = "Select COUNT(a." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Artikel.", "") + ") as Quantity FROM Artikel a  ";
                        break;

                    case "Netto":
                    case "Brutto":
                    case "Anzahl":
                        strSqlMenge = "Select SUM(a." + myTarif.TarifPosition.DatenfeldArtikel + ") as Quantity FROM Artikel a  ";
                        break;

                    case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_Netto:
                    case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_Brutto:
                    case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_Anzahl:
                        strSqlMenge = "Select SUM(a." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Artikel.", "") + ") as Quantity FROM Artikel a  ";
                        break;

                    case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Eingang_Id:
                        strSqlMenge = "Select COUNT(b." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Eingang.", "") + ") as Quantity FROM Artikel a  ";
                        break;

                    case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Eingang_WaggonNo:
                        //strSqlMenge = "Select COUNT(DISTINCT b." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Eingang.", "") + ") as Quantity FROM Artikel a  ";

                        strSqlMenge = "Select ";
                        //strSqlMenge += "b.LEingangID";
                        //strSqlMenge += ", b." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Eingang.", "");

                        strSqlMenge += "b." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Eingang.", "");
                        strSqlMenge += ", CAST(b.Date as date) as Datum ";
                        strSqlMenge += " FROM Artikel a ";

                        strSqlGroup = " Group by b." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Eingang.", "") + ", CAST(b.Date as date) ";
                        break;
                }

                //Summe
                //if (
                //    (myTarif.TarifPosition.DatenfeldArtikel.Equals("ID"))
                //   )
                //{
                //    strSqlMenge = "Select COUNT(a." + myTarif.TarifPosition.DatenfeldArtikel + ") as Quantity FROM Artikel a  ";
                //}
                //else
                //{
                //    strSqlMenge = "Select SUM(a." + myTarif.TarifPosition.DatenfeldArtikel + ") as Quantity FROM Artikel a  ";
                //}
                //Artikel
                strSqlArtID = GetMainSQLArtikelBestand(ref myTarif, myStrAbrArt, myBForBestand, myTarifPosID);

                switch (myStrAbrArt)
                {
                    //case "Einlagerungskosten":
                    case clsFaktLager.const_Abrechnungsart_Einlagerung:
                        strSQLTmp = string.Empty;
                        strSQLTmp = "INNER JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                    "LEFT JOIN LAusgang c ON c.ID=a.LAusgangTableID " +
                                                "WHERE " +
                                                        "b.[Check]=1 AND " +
                                                        "b.DirectDelivery=0 AND " +
                                                        "b.Mandant=" + MandantenID + " AND " +
                                                        "b.AbBereich=" + this.Sys.AbBereich.ID + " AND " +
                                                        "b.Auftraggeber=" + Auftraggeber + " AND " +
                                                        "(b.Date between '" + VonZeitraum.ToShortDateString() + "' AND '" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') ";// AND " +

                        if (!myBForBestand)
                        {
                            //Zeitraumbezogen
                            if (myTarif.TarifPosition.zeitraumbezogen)
                            {
                                if (myTarif.TarifPosition.TEinheiten != string.Empty)
                                {
                                    string strDatePart = string.Empty;
                                    //Verlgleich EInlagerdatum + angebene Zeitspanne
                                    if (myTarif.TarifPosition.TEinheiten == "Tage")
                                    {
                                        strDatePart = "day";
                                        strSQLTmp = strSQLTmp + " AND (" +
                                                                        "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)) > " + myTarif.TarifPosition.TEinheitenVon + " - 1 " +
                                                                           " AND " +
                                                                        "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)) <" + myTarif.TarifPosition.TEinheitenBis + " + 1 " +
                                                                          " AND " +
                                                                        "(YEAR(b.Date)=YEAR('" + VonZeitraum.ToShortDateString() + "') " +
                                                                      ") ";
                                    }
                                    else
                                    {
                                        strDatePart = "month";
                                        strSQLTmp = strSQLTmp + " AND (" +
                                                                        "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)) > (" + myTarif.TarifPosition.TEinheitenVon + " - 1) " +
                                                                           " AND " +
                                                                        "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)) < (" + myTarif.TarifPosition.TEinheitenBis + " + 1) " +
                                                                           " AND " +
                                                                        "(YEAR(b.Date)=YEAR('" + VonZeitraum.ToShortDateString() + "')) " +
                                                                      ") ";
                                    }
                                }
                            }
                            else
                            {
                                //wenn nicht zeitraumbezogen, dann muss hier noch der Filter für den Abrechnungszeitraum gesetzt werden
                                //strSQLTmp = strSQLTmp + " AND (b.Date>'" + VonZeitraum.Date.AddDays(-1).ToShortDateString() + "' AND b.Date<'" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') ";
                            }
                        }

                        switch (myTarif.TarifPosition.QuantityCalcBase)
                        {
                            case clsTarifPosition.const_QuantityBase_Einlagerung:
                                //strSQLTmp = strSQLTmp + " AND b.Retoure=0 AND b.Verlagerung=0 ";
                                strSQLTmp = strSQLTmp + " AND b.Verlagerung=0 ";
                                break;
                            case clsTarifPosition.const_QuantityBase_Auslagerung:
                                break;

                            case clsTarifPosition.const_QuantityBase_Retoure:
                                strSQLTmp = strSQLTmp + " AND b.Retoure=1 ";
                                break;

                            case clsTarifPosition.const_QuantityBase_Verlagerung:
                                strSQLTmp = strSQLTmp + " AND b.Verlagerung=1 ";
                                break;

                            case clsTarifPosition.const_QuantityBase_Umbuchung:
                                strSQLTmp = strSQLTmp + " AND b.Umbuchung=1 ";
                                break;

                            default:
                                strSQLTmp = strSQLTmp + " AND b.Retoure=0 AND b.Verlagerung=0 ";
                                break;
                        }
                        break;

                    case clsFaktLager.const_Abrechnungsart_Auslagerung:
                        //case "Auslagerungskosten":
                        strSQLTmp = string.Empty;
                        strSQLTmp = "INNER JOIN LAusgang c ON c.ID=a.LAusgangTableID " +
                                     "LEFT JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                                "WHERE " +
                                                    "c.Checked=1 AND " +
                                                    "c.DirectDelivery=0 AND " +
                                                    "c.IsRL=0 AND " +
                                                    "c.MandantenID=" + MandantenID + " AND " +
                                                    "c.AbBereich=" + this.Sys.AbBereich.ID + " AND " +
                                                    "c.Auftraggeber=" + Auftraggeber + " AND " +
                                                    //"(b.Datum>'" + VonZeitraum.Date.AddDays(-1).ToShortDateString() + "' AND b.Datum<'" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') AND " +
                                                    "(c.Datum between '" + VonZeitraum.ToShortDateString() + "' AND '" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') ";//AND " + // LAGERDAUER BEI AUSLAGERUNG?
                                                                                                                                                                                 //"DATEDIFF(day, c.Datum, '" + BisZeitraum.Date.ToShortDateString() + "')>=" + myTarif.TarifPosition.Lagerdauer + " ";
                        if (!myBForBestand)
                        {
                            //Zeitraumbezogen
                            if (myTarif.TarifPosition.zeitraumbezogen)
                            {
                                if (myTarif.TarifPosition.TEinheiten != string.Empty)
                                {
                                    string strDatePart = string.Empty;
                                    //Verlgleich EInlagerdatum + angebene Zeitspanne
                                    if (myTarif.TarifPosition.TEinheiten == "Tage")
                                    {
                                        strDatePart = "day";
                                        strSQLTmp = strSQLTmp + " AND (" +
                                                                        "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)) >= " + myTarif.TarifPosition.TEinheitenVon +
                                                                           " AND " +
                                                                        "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)) <=" + myTarif.TarifPosition.TEinheitenBis +
                                                                      ") ";
                                    }
                                    else
                                    {
                                        strDatePart = "month";
                                        strSQLTmp = strSQLTmp + " AND (" +
                                                                        "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)) >= " + myTarif.TarifPosition.TEinheitenVon +
                                                                           " AND " +
                                                                        "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)) <=" + myTarif.TarifPosition.TEinheitenBis +
                                                                      ") ";

                                    }
                                }
                            }
                            else
                            {
                                //wenn nicht zeitraumbezogen, dann muss hier noch der Filter für den Abrechnungszeitraum gesetzt werden
                                //strSQLTmp = strSQLTmp + " AND (b.Datum>'" + VonZeitraum.Date.AddDays(-1).ToShortDateString() + "' AND b.Datum<'" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') ";
                            }
                        }

                        switch (myTarif.TarifPosition.QuantityCalcBase)
                        {
                            case clsTarifPosition.const_QuantityBase_Einlagerung:
                                break;
                            case clsTarifPosition.const_QuantityBase_Auslagerung:
                                //strSQLTmp = strSQLTmp + " AND b.Verlagerung=0 ";
                                break;

                            case clsTarifPosition.const_QuantityBase_Retoure:
                                strSQLTmp = strSQLTmp + " AND b.Retoure=1 ";
                                break;

                            case clsTarifPosition.const_QuantityBase_Verlagerung:
                                strSQLTmp = strSQLTmp + " AND b.Verlagerung=1 ";
                                break;

                            case clsTarifPosition.const_QuantityBase_Umbuchung:
                                strSQLTmp = strSQLTmp + " AND b.Umbuchung=1 ";
                                break;

                            default:
                                strSQLTmp = strSQLTmp + " AND b.Retoure=0 AND b.Verlagerung=0 ";
                                break;
                        }
                        break;

                    //case "Lagerkosten":
                    case clsFaktLager.const_Abrechnungsart_Lagerkosten:
                        //siehe Procedure GetAnfangsbestand
                        break;

                    case "LagerTransportkosten":

                        if (myTarif.TarifPosition.TransDirection == "IN")
                        {
                            strSQLTmp = string.Empty;
                            strSQLTmp = "INNER JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                        "LEFT JOIN LAusgang c ON c.ID=a.LAusgangTableID " +
                                            "WHERE " +
                                                    "b.[Check]=1 AND " +
                                                    "b.LagerTransport=1 AND " +
                                                    "b.Mandant=" + MandantenID + " AND " +
                                                    "b.AbBereich=" + this.Sys.AbBereich.ID + " AND " +
                                                    "b.Auftraggeber=" + Auftraggeber + " AND " +
                                                    //"(b.Date>='" + VonZeitraum.Date + "' AND " +
                                                    //"b.Date<'" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') AND " +
                                                    "DATEDIFF(day, b.Date, '" + BisZeitraum.Date.ToShortDateString() + "')>=" + myTarif.TarifPosition.Lagerdauer + " ";

                            //Zeitraumbezogen
                            if (myTarif.TarifPosition.zeitraumbezogen)
                            {
                                if (myTarif.TarifPosition.TEinheiten != string.Empty)
                                {
                                    string strDatePart = string.Empty;
                                    //Verlgleich EInlagerdatum + angebene Zeitspanne
                                    if (myTarif.TarifPosition.TEinheiten == "Tage")
                                    {
                                        strDatePart = "day";
                                        strSQLTmp = strSQLTmp + " AND (" +
                                                                        "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)-1) >= " + myTarif.TarifPosition.TEinheitenVon +
                                                                           " AND " +
                                                                        "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)-1) <=" + myTarif.TarifPosition.TEinheitenBis +
                                                                      ") ";
                                    }
                                    else
                                    {
                                        strDatePart = "month";
                                        strSQLTmp = strSQLTmp + " AND (" +
                                                                        "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)-1) >= " + myTarif.TarifPosition.TEinheitenVon +
                                                                           " AND " +
                                                                        "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)-1) <=" + myTarif.TarifPosition.TEinheitenBis +
                                                                      ") ";
                                    }
                                }
                            }
                            else
                            {
                                //wenn nicht zeitraumbezogen, dann muss hier noch der Filter für den Abrechnungszeitraum gesetzt werden
                                //strSQLTmp = strSQLTmp + " AND (b.Date>'" + VonZeitraum.Date.AddDays(-1).ToShortDateString() + "' AND b.Date<'" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') ";
                                strSQLTmp = strSQLTmp + " AND (b.Date between '" + VonZeitraum.ToShortDateString() + "' AND '" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') ";
                            }

                        }
                        if (myTarif.TarifPosition.TransDirection == "OUT")
                        {

                            strSQLTmp = string.Empty;
                            strSQLTmp = "INNER JOIN LAusgang c ON c.ID=a.LAusgangTableID " +
                                        "LEFT JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                                    "WHERE " +
                                                        "c.Checked=1 AND " +
                                                        "c.LagerTransport =1 AND " +
                                                        "c.MandantenID=" + MandantenID + " AND " +
                                                        "c.AbBereich=" + this.Sys.AbBereich.ID + " AND " +
                                                        "c.Auftraggeber=" + Auftraggeber + " AND " +
                                                        //"c.Datum>='" + VonZeitraum.Date + "' AND " +
                                                        //"c.Datum<='" + BisZeitraum.Date + "' AND " +
                                                        "DATEDIFF(day, c.Datum, '" + BisZeitraum.Date.ToShortDateString() + "')>=" + myTarif.TarifPosition.Lagerdauer + " ";
                            //Zeitraumbezogen
                            if (myTarif.TarifPosition.zeitraumbezogen)
                            {
                                if (myTarif.TarifPosition.TEinheiten != string.Empty)
                                {
                                    string strDatePart = string.Empty;
                                    //Verlgleich EInlagerdatum + angebene Zeitspanne
                                    if (myTarif.TarifPosition.TEinheiten == "Tage")
                                    {
                                        strDatePart = "day";
                                        strSQLTmp = strSQLTmp + " AND (" +
                                                                        "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(c.Date)-1) >= " + myTarif.TarifPosition.TEinheitenVon +
                                                                           " AND " +
                                                                        "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(c.Date)-1) <=" + myTarif.TarifPosition.TEinheitenBis +
                                                                      ") ";
                                    }
                                    else
                                    {
                                        strDatePart = "month";
                                        strSQLTmp = strSQLTmp + " AND (" +
                                                                        "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(c.Date)-1) >= " + myTarif.TarifPosition.TEinheitenVon +
                                                                           " AND " +
                                                                        "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(c.Date)-1) <=" + myTarif.TarifPosition.TEinheitenBis +
                                                                      ") ";
                                    }
                                }
                            }
                            else
                            {
                                //wenn nicht zeitraumbezogen, dann muss hier noch der Filter für den Abrechnungszeitraum gesetzt werden
                                //strSQLTmp = strSQLTmp + " AND (c.Datum>'" + VonZeitraum.Date.AddDays(-1).ToShortDateString() + "' AND c.Datum<'" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') ";
                                strSQLTmp = strSQLTmp + " AND (c.Datum between '" + VonZeitraum.ToShortDateString() + "' AND '" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') ";
                            }

                        }
                        break;

                    //case "Sperrlagerkosten":
                    case clsFaktLager.const_Abrechnungsart_SPL:
                        strSQLTmp = string.Empty;
                        strSQLTmp = "INNER JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                    "INNER JOIN Sperrlager d ON d.ArtikelID=a.ID " +
                                    "LEFT JOIN LAusgang c ON c.ID=a.LAusgangTableID " +
                                    "WHERE " +
                                            "b.Mandant=" + MandantenID + " AND " +
                                            "b.AbBereich=" + this.Sys.AbBereich.ID + " AND " +
                                            "b.Auftraggeber=" + Auftraggeber + " AND " +
                                            "d.Datum<'" + BisZeitraum.Date.AddDays(1).Date + "' AND " +
                                            "DATEDIFF(day, d.Datum, '" + BisZeitraum.Date + "')>=" + myTarif.TarifPosition.Lagerdauer + " " +
                                            "AND a.ID IN (SELECT Sperr.ArtikelID FROM Sperrlager Sperr " +
                                            "WHERE Sperr.BKZ = 'IN' AND Sperr.ID NOT IN " +
                                            "(SELECT DISTINCT cd.SPLIDIn FROM Sperrlager cd WHERE cd.SPLIDIn>0)) ";


                        break;

                    //case "Direktanlieferung":
                    case clsFaktLager.const_Abrechnungsart_Direktanlieferung:
                        strSQLTmp = string.Empty;
                        strSQLTmp = "INNER JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                    "LEFT JOIN LAusgang c ON c.ID=a.LAusgangTableID " +
                                        "WHERE " +
                                                "b.[Check]=1 AND " +
                                                "b.DirectDelivery=1 AND " +
                                                "b.Mandant=" + MandantenID + " AND " +
                                                "b.AbBereich=" + this.Sys.AbBereich.ID + " AND " +
                                                "b.Auftraggeber=" + Auftraggeber + " AND " +
                                                "b.Date>='" + VonZeitraum.Date + "' AND " +
                                                "b.Date<'" + BisZeitraum.Date.AddDays(1) + "' AND " +
                                                "DATEDIFF(day, b.Date, '" + BisZeitraum.Date.ToShortDateString() + "')>=" + myTarif.TarifPosition.Lagerdauer + " ";

                        //Zeitraumbezogen
                        if (myTarif.TarifPosition.zeitraumbezogen)
                        {
                            if (myTarif.TarifPosition.TEinheiten != string.Empty)
                            {
                                string strDatePart = string.Empty;
                                //Verlgleich EInlagerdatum + angebene Zeitspanne
                                if (myTarif.TarifPosition.TEinheiten == "Tage")
                                {
                                    strDatePart = "day";
                                    strSQLTmp = strSQLTmp + " AND (" +
                                                                    "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)-1) >= " + myTarif.TarifPosition.TEinheitenVon +
                                                                       " AND " +
                                                                    "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)-1) <=" + myTarif.TarifPosition.TEinheitenBis +
                                                                  ") ";
                                }
                                else
                                {
                                    strDatePart = "month";
                                    strSQLTmp = strSQLTmp + " AND (" +
                                                                    "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)-1) >= " + myTarif.TarifPosition.TEinheitenVon +
                                                                       " AND " +
                                                                    "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)-1) <=" + myTarif.TarifPosition.TEinheitenBis +
                                                                  ") ";
                                }
                            }
                        }
                        else
                        {
                            //wenn nicht zeitraumbezogen, dann muss hier noch der Filter für den Abrechnungszeitraum gesetzt werden
                            strSQLTmp = strSQLTmp + " AND (b.Date>'" + VonZeitraum.Date.AddDays(-1).ToShortDateString() + "' AND b.Date<'" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') ";
                        }

                        break;

                    case "Retoure":
                        strSQLTmp = string.Empty;
                        strSQLTmp = "INNER JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                    "LEFT JOIN LAusgang c ON c.ID=a.LAusgangTableID " +
                                        "WHERE " +
                                                "b.[Check]=1 AND " +
                                                "b.Retoure=1 AND " +
                                                "b.Mandant=" + MandantenID + " AND " +
                                                "b.AbBereich=" + this.Sys.AbBereich.ID + " AND " +
                                                "b.Auftraggeber=" + Auftraggeber + " AND " +
                                                "(b.Date>='" + VonZeitraum.Date + "' AND " +
                                                "b.Date<'" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') AND " +
                                                "DATEDIFF(day, b.Date, '" + BisZeitraum.Date.ToShortDateString() + "')>=" + myTarif.TarifPosition.Lagerdauer + " ";
                        //Zeitraumbezogen
                        if (myTarif.TarifPosition.zeitraumbezogen)
                        {
                            if (myTarif.TarifPosition.TEinheiten != string.Empty)
                            {
                                string strDatePart = string.Empty;
                                //Verlgleich EInlagerdatum + angebene Zeitspanne
                                if (myTarif.TarifPosition.TEinheiten == "Tage")
                                {
                                    strDatePart = "day";
                                    strSQLTmp = strSQLTmp + " AND (" +
                                                                    "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)-1) >= " + myTarif.TarifPosition.TEinheitenVon +
                                                                       " AND " +
                                                                    "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)-1) <=" + myTarif.TarifPosition.TEinheitenBis +
                                                                  ") ";
                                }
                                else
                                {
                                    strDatePart = "month";
                                    strSQLTmp = strSQLTmp + " AND (" +
                                                                    "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)-1) >= " + myTarif.TarifPosition.TEinheitenVon +
                                                                       " AND " +
                                                                    "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)-1) <=" + myTarif.TarifPosition.TEinheitenBis +
                                                                  ") ";
                                }
                            }
                        }
                        else
                        {
                            //wenn nicht zeitraumbezogen, dann muss hier noch der Filter für den Abrechnungszeitraum gesetzt werden
                            strSQLTmp = strSQLTmp + " AND (b.Date>'" + VonZeitraum.Date.AddDays(-1).ToShortDateString() + "' AND b.Date<'" + BisZeitraum.Date.AddDays(1) + "') ";
                        }

                        break;

                    //case "Rücklieferung":
                    case clsFaktLager.const_Abrechnungsart_Ruecklieferung:
                        strSQLTmp = string.Empty;
                        strSQLTmp = "INNER JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                    "LEFT JOIN LAusgang c ON c.ID=a.LAusgangTableID " +
                                        "WHERE " +
                                                "c.[Checked]=1 AND " +
                                                "c.IsRL=1 AND " +
                                                "b.Mandant=" + MandantenID + " AND " +
                                                "b.AbBereich=" + this.Sys.AbBereich.ID + " AND " +
                                                "b.Auftraggeber=" + Auftraggeber + " AND " +
                                                "AND (c.Datum between '" + VonZeitraum.Date.ToShortDateString() + "' AND '" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') " +
                                                //"(b.Date>='" + VonZeitraum.Date + "' AND " +
                                                //"b.Date<'" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') AND " +
                                                "DATEDIFF(day, b.Date, '" + BisZeitraum.Date.ToShortDateString() + "')>=" + myTarif.TarifPosition.Lagerdauer + " ";
                        //Zeitraumbezogen
                        if (myTarif.TarifPosition.zeitraumbezogen)
                        {
                            if (myTarif.TarifPosition.TEinheiten != string.Empty)
                            {
                                string strDatePart = string.Empty;
                                //Verlgleich EInlagerdatum + angebene Zeitspanne
                                if (myTarif.TarifPosition.TEinheiten == "Tage")
                                {
                                    strDatePart = "day";
                                    strSQLTmp = strSQLTmp + " AND (" +
                                                                    "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)-1) >= " + myTarif.TarifPosition.TEinheitenVon +
                                                                       " AND " +
                                                                    "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)-1) <=" + myTarif.TarifPosition.TEinheitenBis +
                                                                  ") ";
                                }
                                else
                                {
                                    strDatePart = "month";
                                    strSQLTmp = strSQLTmp + " AND (" +
                                                                    "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)-1) >= " + myTarif.TarifPosition.TEinheitenVon +
                                                                       " AND " +
                                                                    "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)-1) <=" + myTarif.TarifPosition.TEinheitenBis +
                                                                  ") ";
                                }
                            }
                        }
                        else
                        {
                            //wenn nicht zeitraumbezogen, dann muss hier noch der Filter für den Abrechnungszeitraum gesetzt werden
                            strSQLTmp = strSQLTmp + " AND (b.Date>'" + VonZeitraum.Date.AddDays(-1).ToShortDateString() + "' AND b.Date<'" + BisZeitraum.Date.AddDays(1) + "') ";
                        }

                        break;
                    //case "Vorfracht":
                    case clsFaktLager.const_Abrechnungsart_Vorfracht:
                        strSQLTmp = string.Empty;
                        strSQLTmp = "INNER JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                    "LEFT JOIN LAusgang c ON c.ID=a.LAusgangTableID " +
                                        "WHERE " +
                                                "b.[Check]=1 AND " +
                                                "b.DirectDelivery=0 AND " +
                                                "b.Vorfracht=1 AND " +
                                                "b.Mandant=" + MandantenID + " AND " +
                                                "b.AbBereich=" + this.Sys.AbBereich.ID + " AND " +
                                                "b.Auftraggeber=" + Auftraggeber + " AND " +
                                                "(b.Date>='" + VonZeitraum.Date + "' AND " +
                                                "b.Date<'" + BisZeitraum.Date.AddDays(1) + "') AND " +
                                                "DATEDIFF(day, b.Date, '" + BisZeitraum.Date.ToShortDateString() + "')>=" + myTarif.TarifPosition.Lagerdauer + " ";
                        //Zeitraumbezogen
                        if (myTarif.TarifPosition.zeitraumbezogen)
                        {
                            if (myTarif.TarifPosition.TEinheiten != string.Empty)
                            {
                                string strDatePart = string.Empty;
                                //Verlgleich EInlagerdatum + angebene Zeitspanne
                                if (myTarif.TarifPosition.TEinheiten == "Tage")
                                {
                                    strDatePart = "day";
                                    strSQLTmp = strSQLTmp + " AND (" +
                                                                    "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)-1) >= " + myTarif.TarifPosition.TEinheitenVon +
                                                                       " AND " +
                                                                    "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)-1) <=" + myTarif.TarifPosition.TEinheitenBis +
                                                                  ") ";
                                }
                                else
                                {
                                    strDatePart = "month";
                                    strSQLTmp = strSQLTmp + " AND (" +
                                                                    "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)-1) >= " + myTarif.TarifPosition.TEinheitenVon +
                                                                       " AND " +
                                                                    "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)-1) <=" + myTarif.TarifPosition.TEinheitenBis +
                                                                  ") ";
                                }
                            }
                        }
                        else
                        {
                            //wenn nicht zeitraumbezogen, dann muss hier noch der Filter für den Abrechnungszeitraum gesetzt werden
                            strSQLTmp = strSQLTmp + " AND (b.Date>'" + VonZeitraum.Date.AddDays(-1).ToShortDateString() + "' AND b.Date<'" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') ";
                        }
                        break;

                    case clsFaktLager.const_Abrechnungsart_Gleisstellgebuehr:
                        strSQLTmp = string.Empty;
                        strSQLTmp = "INNER JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                    "LEFT JOIN LAusgang c ON c.ID=a.LAusgangTableID " +
                                                "WHERE " +
                                                        "b.IsWaggon=1 AND " +
                                                        "b.[Check]=1 AND " +
                                                        "b.DirectDelivery=0 AND " +
                                                        "b.Mandant=" + MandantenID + " AND " +
                                                        "b.AbBereich=" + this.Sys.AbBereich.ID + " AND " +
                                                        "b.Auftraggeber=" + Auftraggeber + " AND " +
                                                        "(b.Date between '" + VonZeitraum.ToShortDateString() + "' AND '" + BisZeitraum.Date.AddDays(1).ToShortDateString() + "') ";// AND " +

                        //Zeitraumbezogen
                        if (myTarif.TarifPosition.zeitraumbezogen)
                        {
                            if (myTarif.TarifPosition.TEinheiten != string.Empty)
                            {
                                string strDatePart = string.Empty;
                                //Verlgleich EInlagerdatum + angebene Zeitspanne
                                if (myTarif.TarifPosition.TEinheiten == "Tage")
                                {
                                    strDatePart = "day";
                                    strSQLTmp = strSQLTmp + " AND (" +
                                                                    "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)) > " + myTarif.TarifPosition.TEinheitenVon + " - 1 " +
                                                                       " AND " +
                                                                    "(DAY('" + VonZeitraum.ToShortDateString() + "') - DAY(b.Date)) <" + myTarif.TarifPosition.TEinheitenBis + " + 1 " +
                                                                      " AND " +
                                                                    "(YEAR(b.Date)=YEAR('" + VonZeitraum.ToShortDateString() + "') " +
                                                                  ") ";
                                }
                                else
                                {
                                    strDatePart = "month";
                                    strSQLTmp = strSQLTmp + " AND (" +
                                                                    "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)) > (" + myTarif.TarifPosition.TEinheitenVon + " - 1) " +
                                                                       " AND " +
                                                                    "(MONTH('" + VonZeitraum.ToShortDateString() + "') - MONTH(b.Date)) < (" + myTarif.TarifPosition.TEinheitenBis + " + 1) " +
                                                                       " AND " +
                                                                    "(YEAR(b.Date)=YEAR('" + VonZeitraum.ToShortDateString() + "')) " +
                                                                  ") ";
                                }
                            }
                        }
                        break;
                }

                //bezgogen auf bestimmte Güterarten
                if (myTarif.ExistLinkedGArt)
                {
                    strSQLTmp = strSQLTmp + " AND a.GArtID IN (" + myTarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                }
                //Staffel
                if (myTarif.TarifPosition.einheitenbezogen)
                {
                    strSQLTmp = strSQLTmp + " AND a.Brutto>=" + myTarif.TarifPosition.EinheitenVon + " AND " +
                                             "a.Brutto<=" + myTarif.TarifPosition.EinheitenBis + " ";
                }

            }
            //Ermitteln der Artikel
            strSqlArtID = strSqlArtID + strSQLTmp;
            //Summe
            strSqlMenge = strSqlMenge + strSQLTmp;

            //Menge oder Artikel
            if (myBForBestand)
            {
                strSQLResult = strSqlMenge;
            }
            else
            {
                strSQLResult = strSqlArtID;
            }

            //nur Artikel, die noch nicht abgerechnet sind
            //Admin Preview 
            if (!bCalcPreviewAdmin)
            {
                strSQLResult = strSQLResult + " " +
                                "AND a.ID NOT IN ( " +
                                                    "Select " +
                                                        "a.ArtikelID " +
                                                        "FROM RGPosArtikel a " +
                                                            "INNER JOIN TarifPositionen b ON b.ID=a.TarifPosID " +
                                                            "INNER JOIN RGPositionen p ON p.ID = a.RGPosID " +
                                                            "INNER JOIN Rechnungen r ON r.ID=p.RGTableID " +
                                                            "WHERE " +
                                                                "AbgerechnetVon='" + VonZeitraum.ToShortDateString() + "' " +
                                                                "AND AbgerechnetBis= '" + BisZeitraum.ToShortDateString() + "'  " +
                                                                "AND b.TarifPosArt='" + myStrAbrArt + "' " +
                                                                "AND r.Empfaenger=" + Auftraggeber + " " +
                                                                "AND b.ID=" + myTarif.TarifPosition.ID + " " +
                                                                "AND r.ID NOT IN (Select ID FROM Rechnungen " +
                                                                                            "WHERE ID IN( Select StornoID FROM Rechnungen WHERE StornoID>0)) " +
                                                 ") ";
            }
            if (myBForBestand)
            {
                //-- dies nur an strSqlMenge 
                strSQLResult += strSqlGroup;
            }
            return strSQLResult;
        }
        ///<summary>clsFaktLager / GetMailSQLArtikelBestand</summary>
        ///<remarks></remarks>
        private string GetMainSQLArtikelBestand(ref clsTarif myTarif, string myStrAbrArt, bool myBForBestand, decimal myTarifPosID)
        {
            string strSqlArtID = string.Empty;
            decimal decTmp = 0;
            //strSqlArtID = "Select a.ID " +
            //                      ", CAST(" + myTarifPosID + " as decimal) as TarifPosID " +
            //                      ", '" + myStrAbrArt + "' as Abrechnungsart";

            switch (myTarif.TarifPosition.DatenfeldArtikel)
            {
                case "ID":
                case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_ID:
                    strSqlArtID += "Select a.ID ";
                    strSqlArtID += ", CAST(" + myTarifPosID + " as decimal) as TarifPosID ";
                    //strSqlArtID += ", '" + myStrAbrArt + "' as Abrechnungsart";
                    strSqlArtID += ", '" + myTarif.TarifPosition.TarifPosArt + "' as Abrechnungsart";
                    strSqlArtID += ", Count(a.ID) as Menge";
                    break;

                case "Anzahl":
                case "Brutto":
                case "Netto":
                case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_Anzahl:
                case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_Brutto:
                case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_Netto:
                    strSqlArtID += "Select a.ID ";
                    strSqlArtID += ", CAST(" + myTarifPosID + " as decimal) as TarifPosID ";
                    //strSqlArtID += ", '" + myStrAbrArt + "' as Abrechnungsart";
                    strSqlArtID += ", '" + myTarif.TarifPosition.TarifPosArt + "' as Abrechnungsart";

                    strSqlArtID += ", a." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Artikel.", "") + " as Menge";
                    break;

                case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Eingang_Id:
                case constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Eingang_WaggonNo:
                    strSqlArtID += "Select a.ID ";
                    strSqlArtID += ", CAST(" + myTarifPosID + " as decimal) as TarifPosID ";
                    //strSqlArtID += ", '" + myStrAbrArt + "' as Abrechnungsart";
                    strSqlArtID += ", '" + myTarif.TarifPosition.TarifPosArt + "' as Abrechnungsart";

                    strSqlArtID += ", b." + myTarif.TarifPosition.DatenfeldArtikel.Replace("Eingang.", "") + " as Menge";
                    break;

                default:
                    strSqlArtID += ", a.Brutto as Menge";
                    break;
            }


            strSqlArtID += ", CAST(" + myTarif.TarifPosition.PreisEinheit.ToString().Replace(",", ".") + " as money)  as Preis " +
                           ", Datediff(dd,b.date,ISNULL(c.Datum,'" + BisZeitraum.Date.ToShortDateString() + "')) +1 as DauerX ";

            switch (myStrAbrArt)
            {
                case clsFaktLager.const_Abrechnungsart_SPL:
                    strSqlArtID = strSqlArtID + ", Datediff(" +
                                                             "dd" +
                                                             ",(SELECT aSPL.Datum FROM Sperrlager aSPL " +
                                                                        "WHERE aSPL.BKZ = 'IN' " +
                                                                        "AND aSPL.ArtikelID IN (a.ID) " +
                                                                        "AND aSPL.ID NOT IN(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn > 0))" +
                                                             ",'" + BisZeitraum.Date.ToShortDateString() + "') " +
                                                             "as Dauer ";
                    break;

                default:
                    //--- tagesgenaue Abrechnung
                    strSqlArtID = strSqlArtID +
                                  ", CASE " +
                                        "WHEN (DATEDIFF(dd, ISNULL(c.Datum,'31.12.2500'),'" + BisZeitraum.Date.ToShortDateString() + "')>0) AND (CHECKED=1) " +
                                            "THEN " +
                                                "CASE " +
                                                "WHEN ((DATEDIFF(dd, b.Date, c.Datum) +1 ) - " + myTarif.TarifPosition.Lagerdauer + " <0) " +
                                                //"WHEN (DATEDIFF(dd, b.Date, c.Datum) - " + myTarif.TarifPosition.Lagerdauer + " <0) " +
                                                "THEN 0 " +
                                                "ELSE (DATEDIFF(dd, b.Date, c.Datum) + 1) - " + myTarif.TarifPosition.Lagerdauer + " " +
                                                //"ELSE DATEDIFF(dd, b.Date, c.Datum) - " + myTarif.TarifPosition.Lagerdauer + " " +
                                                "END " +
                                            "ELSE " +
                                                "CASE " +
                                                "WHEN ((DATEDIFF(dd, b.Date, '" + BisZeitraum.Date.ToShortDateString() + "') + 1) - " + myTarif.TarifPosition.Lagerdauer + " <0) " +
                                                //"WHEN (DATEDIFF(dd, b.Date, '" + BisZeitraum.Date.ToShortDateString() + "') - " + myTarif.TarifPosition.Lagerdauer + " <0) " +
                                                "THEN 0 " +
                                                "ELSE (DATEDIFF(dd, b.Date, '" + BisZeitraum.Date.ToShortDateString() + "') + 1) - " + myTarif.TarifPosition.Lagerdauer + " " +
                                                //"ELSE DATEDIFF(dd, b.Date, '" + BisZeitraum.Date.ToShortDateString() + "') - " + myTarif.TarifPosition.Lagerdauer + " " +
                                                "END " +
                                     "END as Dauer ";
                    break;

            }

            strSqlArtID = strSqlArtID +
                                  ", CAST(" + decTmp.ToString().Replace(",", ".") + " as money) as Kosten " +
                                  ", CAST(" + myTarif.Modus + " as Int) as TarifModus " +
                                  ", CAST(" + myTarif.VersPreis.ToString().Replace(",", ".") + " as money) as VersPreis " +
                                  ", CAST(0 as bit) as IsUBCalc " +
                                  ", b.Date as EDatum " +
                                  ", c.Datum as ADatum " +
                                  ", '" + myTarif.TarifPosition.BasisEinheit + "' as BasisEinheit " +
                                  ", '" + myTarif.TarifPosition.AbrEinheit + "' as AbrEinheit " +
                                  ", " + (int)myTarif.TarifPosition.CalcModus + " as CalcModus " +
                                  ", CAST( 0 as decimal) as PricePerUnitFactor " +

                                " FROM Artikel a ";

            return strSqlArtID;
        }
        ///<summary>clsFaktLager / GetLagerTransport</summary>
        ///<remarks>Ermittel den Transportbestand des Kunden für den gewünschten Zeitraum.</remarks>
        private decimal GetLagerTransport(ref clsTarif myTarif)
        {
            string strTmp = string.Empty; ;
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        ///<summary>clsFaktLager / GetRetoure</summary>
        ///<remarks>Ermittel den Rücklieferungen des Kunden für den gewünschten Zeitraum.</remarks>
        private decimal GetRetoure(ref clsTarif myTarif)
        {
            string strTmp = string.Empty; ;
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        ///<summary>clsFaktLager / GetDirektanlieferung</summary>
        ///<remarks>Ermittel der Direktanlieferungen des Kunden für den gewünschten Zeitraum.</remarks>
        private decimal GetEndbestand(ref clsTarif myTarif)
        {
            decimal decTmp = Anfangsbestand + Einlagerung - Auslagerung;
            Decimal.TryParse(decTmp.ToString(), out decTmp);
            return decTmp;
        }
        ///<summary>clsFaktLager / GetDirektanlieferung</summary>
        ///<remarks>Ermittel der Direktanlieferungen des Kunden für den gewünschten Zeitraum.</remarks>
        public void GetExistRGAuftraggeber()
        {
            dtRGAuftraggeber = new DataTable("bestehendeRG");
            //dtRGAuftraggeber = clsRechnung.GetExistRG(this._GL_User, Auftraggeber);
            dtRGAuftraggeber = this.Rechnung.GetExistRG(Auftraggeber);
        }

        ///<summary>clsFaktLager / MergeDataTable</summary>
        ///<remarks>.</remarks>
        private void MergeDataTable(ref DataTable dtDest, DataTable dtSource)
        {
            if (dtSource.Rows.Count > 0)
            {
                dtDest.Merge(dtSource);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName">Der Name der Eigenschaft</param>
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        ///<summary>clsFaktLager / SendAnhangAsMail</summary>
        ///<remarks>.</remarks>
        public void SendAnhangAsMail(DateTime dtAbrBis, clsSystem sys)
        {
            bool bSendOK = true;

            clsMailingList Mailinglist = new clsMailingList();
            clsMail Mail;
            List<decimal> ListAdrIDExcel = clsMailingList.GetAutoMailingList(this._GL_User, "#RGAnhangExcel#");
            if (ListAdrIDExcel.Count > 0)
            {
                Mailinglist = new clsMailingList();

                for (Int32 i = 0; i <= ListAdrIDExcel.Count - 1; i++)
                {
                    decimal AdrID = (decimal)ListAdrIDExcel[i];

                    clsADR tmpADR = new clsADR();
                    tmpADR._GL_User = this._GL_User;
                    tmpADR.ID = AdrID;
                    tmpADR.Fill();
                    string dir = string.Empty;

                    Mailinglist.InitClass(this._GL_User, this._GL_System, AdrID);
                    Mailinglist.FillListMailAdressenForAuto(AdrID, "#RGAnhangExcel#");

                    if (Mailinglist.ListMailadressen.Count > 0)
                    {
                        /*                  
                         // KEIN REPORT SONDERN EINE TABELLE ALS EXCEL FORMAT 
                        uRepSource = new UriReportSource();
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("AuftraggeberID", AdrID));
                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("Stichtag", DateTime.Now.AddDays(-1)));
                        uRepSource.Uri = Application.StartupPath + this.GLSystem.Doc_DocBestandAutoMail;
                        string FilePath = AttachmentPath + "\\" + tmpADR.ID.ToString() + "_" + FileBestandName;
                        clsPrint.PrintDirectToPDF(FileBestandName, FilePath, uRepSource);
                        //PrintDirectToPDF(FileBestandName, FilePath, uRepSource);
                        */
                        string strSql = string.Empty;
                        string mailSubject = "Journal";


                        strSql = "Select distinct Artikel.LVS_ID as LVSNr " +
                                 ",CASE 	WHEN (Artikel.Laenge>0) " +
                                 "THEN CAST(Artikel.Dicke as varchar (20))+' x '+ CAST(CAST(Artikel.Breite as int) as varchar(20))+' x '+CAST(CAST(Artikel.Laenge as int) as varchar(20)) " +
                                 "ELSE CAST(Artikel.Dicke as varchar (20))+' x '+ CAST(Cast(Artikel.Breite as int) as varchar(20)) " +
                                 "END as Abmessung " +
                                 ", Artikel.Produktionsnummer , Artikel.Brutto as Gewicht , Cast(LEingang.Date as Date) as Eingangsdatum " +
                                 ", Case 	When LAusgang.Checked=1 and datediff(month,Lausgang.Datum,Rechnungen.AbrZeitraumBis)=0 " +
                                 "Then Cast(LAusgang.Datum as date)  	Else null End 	as Ausgangsdatum" +
                                 ", Cast(Rechnungen.AbrZeitraumBis as date)  as AbrZeitraumBis " +
                                 ", CASE	WHEN Artikel.LAusgangTableID>0 THEN DATEDIFF(dd,LEingang.Date, LAusgang.Datum)+1 " +
                                 "ELSE DATEDIFF(dd,LEingang.Date, Rechnungen.AbrZeitraumBis)+1 " +
                                 "END as Lagerdauer " +
                                 ",(Select Sum(RGPosArtikel.Kosten) from RGPosArtikel left join RGPositionen on RGPosArtikel.RGPosID=RGPositionen.ID where RGPositionen.Abrechnungsart='Einlagerungskosten' AND RGPositionen.RGTableID=(select Max(ID) from Rechnungen where RGArt='Lager' and Auftraggeber=" + AdrID + " and Storno=0) And ArtikelID=Artikel.ID) as Einlagerung " +
                                 ",(Select Sum(RGPosArtikel.Kosten) from RGPosArtikel left join RGPositionen on RGPosArtikel.RGPosID=RGPositionen.ID where RGPositionen.Abrechnungsart='Auslagerungskosten' AND RGPositionen.RGTableID=(select Max(ID) from Rechnungen where RGArt='Lager' and Auftraggeber=" + AdrID + " and Storno=0) And ArtikelID=Artikel.ID) as Auslagerung " +
                                 ",(Select Sum(RGPosArtikel.Kosten) from RGPosArtikel left join RGPositionen on RGPosArtikel.RGPosID=RGPositionen.ID where RGPositionen.Abrechnungsart='Lagerkosten' AND RGPositionen.RGTableID=(select Max(ID) from Rechnungen where RGArt='Lager' and Auftraggeber=" + AdrID + " and Storno=0) And ArtikelID=Artikel.ID) as Lagergeld " +
                                 ",CAST((Select Sum(RGPosArtikel.Kosten) from RGPosArtikel left join RGPositionen on RGPosArtikel.RGPosID=RGPositionen.ID where RGPositionen.Abrechnungsart='Nebenkosten' AND RGPositionen.RGTableID=(select Max(ID) from Rechnungen where RGArt='Lager' and Auftraggeber=" + AdrID + " and Storno=0) And ArtikelID=Artikel.ID)as nvarchar) as Nebenkosten " +
                                 "FROM RGPosArtikel INNER JOIN RGPositionen ON RGPositionen.ID=RGPosArtikel.RGPosID " +
                                 "INNER JOIN Artikel ON Artikel.ID=RGPosArtikel.ArtikelID  INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID " +
                                 "INNER JOIN Rechnungen ON Rechnungen.ID = RGPositionen.RGTableID LEFT JOIN LAusgang ON LAusgang.ID =Artikel.LAusgangTableID " +
                                 "WHERE RGPositionen.RGTableID=(select Max(ID) from Rechnungen where RGArt='Lager' and Auftraggeber=" + AdrID + " and Storno=0) ORDER BY LVSnr ";
                        mailSubject = "Rechnungsanhang zur Rechnung vom " + dtAbrBis.ToString("dd.MM.yyyy");


                        DataTable dtGewBestand = new DataTable("Rechnungsanhang");
                        dtGewBestand = clsSQLcon.ExecuteSQL_GetDataTable(strSql, _GL_User.User_ID, "Rechnungsanhang");
                        if (dtGewBestand.Rows.Count > 0)
                        {
                            LVS.clsExcel Excel = new clsExcel();
                            //string FileName = mailSubject;
                            //string FilePath = Excel.ExportDataTableToExcel(dtGewBestand, FileName, "C:\\LVS\\Export\\");

                            string sqlRGNr = "select top(1) RGNr from Rechnungen where RGArt='Lager' and Auftraggeber=" + AdrID + " and Storno=0 order by RGnr desc";
                            string value = clsSQLcon.ExecuteSQL_GetValue(sqlRGNr, _GL_User.User_ID);
                            decimal dectmpRG = 0;
                            Decimal.TryParse(value, out dectmpRG);
                            string FileName = "Rechnungsanhang_R-" + dtAbrBis.Month.ToString("00") + "-" + dectmpRG.ToString("00000");
                            string FilePath = Excel.ExportDataTableToWorksheet(dtGewBestand, "C:\\\\LVS\\Export\\" + FileName);

                            //string FilePath = AttachmentPath + "\\" + FileName;
                            List<string> listAttach = new List<string>();
                            listAttach.Add(FilePath);
                            if (listAttach.Count > 0)
                            {
                                Mail = new clsMail();
                                Mail._GL_System = this._GL_System;
                                //Mail.InitClass(GL_User, _ctrMenu);
                                Mail.InitClass(_GL_User, sys);
                                Mail.ListAttachment = listAttach;

                                try
                                {
                                    Mail.ListMailReceiver = Mailinglist.ListMailadressen;
                                    Mail.Subject = mailSubject;
                                    //bSendOK = Mail.SendNoReply(); // test
                                }
                                finally
                                {
                                    if (bSendOK)
                                    {
                                        //clsLogbuch tmpLog = new clsLogbuch();
                                        //tmpLog.ID = 0;
                                        //tmpLog.Typ = Globals.enumLogArtItem.autoMail.ToString();
                                        //tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> Mail erfolgreich versendet...";
                                        //ListLogInsert.Add(tmpLog);
                                    }
                                    else
                                    {
                                        //clsLogbuch tmpLog = new clsLogbuch();
                                        //tmpLog.ID = 0;
                                        //tmpLog.Typ = Globals.enumLogArtItem.autoMail.ToString();
                                        //tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> nicht Mail erfolgreich versendet...";
                                        //ListLogInsert.Add(tmpLog);
                                    }
                                }
                            }
                            else
                            {
                                ////kein Attachment - keine Anhang
                                //clsLogbuch tmpLog = new clsLogbuch();
                                //tmpLog.ID = 0;
                                //tmpLog.Typ = Globals.enumLogArtItem.autoMail.ToString();
                                //tmpLog.LogText = " -> Adresse ID/Matchcode: [" + tmpADR.ID.ToString() + "/" + tmpADR.ViewID + "] -> es konnte kein Anhang gefunden werden...";
                                //ListLogInsert.Add(tmpLog);
                            }
                        }
                    }
                }
            }
        }
        ///<summary>clsFaktLager / SendRGAndRGAnhangAsMail</summary>
        ///<remarks>.</remarks>
        public List<string> CreateRGAndRGAnhangToPDF(clsSystem mySys)
        {
            LogMessages = new List<string>();
            LogMessages.Add("-" + Environment.NewLine);
            LogMessages.Add("---> Auftruf clsFaktLager.cs -> CreateRGAndRGAnhangToPDF(clsSystem mySys)");
            LogMessages.Add("     |- Z 4913 -  CreateRGAndRGAnhangToPDF(clsSystem mySys)");
            //LogMessages.Add("Auftruf clsFaktLager.cs -> CreateRGAndRGAnhangToPDF(clsSystem mySys)");
            //LogMessages.Add("       -> Ziel  this.FaktLager.CreateRGAndRGAnhangToPDF(this._ctrMenu._frmMain.system)");
            //LogMessages.Add(" > Paremter:");
            //LogMessages.Add(string.Format("{0,0} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}"," >", "mySourcePath", mySourcePath));

            List<string> retList = new List<string>();
            bool retBool = true;
            string AttachmentPath = string.Empty;
            string strDateTime = DateTime.Now.Year.ToString() + "_" +
                                 DateTime.Now.Month.ToString() + "_" +
                                 DateTime.Now.Day.ToString() + "_" +
                                 DateTime.Now.Second.ToString();

            Telerik.Reporting.UriReportSource uRepSource = new Telerik.Reporting.UriReportSource();

            //Rechnung als PDF
            clsReportDocSetting tmpRepSettings = new clsReportDocSetting();

            if (tmpRepSettings is clsReportDocSetting)
            {
                string FilePathTemp = string.Empty;
                try
                {
                    string FileExtension = string.Empty;
                    switch (this.Rechnung.RGArt)
                    {
                        case clsRechnung.const_RechnungsArt_Lager:
                            //tmpRepSettings = tmpRepSettings.GetClassByDocKey(enumIniDocKey.LagerrechnungMail.ToString());
                            tmpRepSettings = mySys.ReportDocSetting.GetClassByDocKey(enumIniDocKey.LagerrechnungMail.ToString());

                            if ((tmpRepSettings.RSAId > 0) && (tmpRepSettings.ReportDataFileExist))
                            {
                                //--check of reportbook, hier kann die Reportdatei nicht aus der DB kommen, das funktioniert nicht
                                FileExtension = Path.GetExtension(tmpRepSettings.ReportFileName);
                                if (FileExtension.ToUpper().Equals(".TRBP"))
                                {
                                    FilePathTemp = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                                    if (FilePathTemp.EndsWith("\\"))
                                    {
                                        FilePathTemp.Replace("\\", "");
                                    }
                                    FilePathTemp += tmpRepSettings.DocFileNameAndPath;

                                    if (File.Exists(FilePathTemp))
                                    {
                                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this.Rechnung.ID));
                                        uRepSource.Uri = FilePathTemp;

                                        if (mySys.Client.Modul.Fakt_eInvoiceIsAvailable)
                                        {
                                            try
                                            {
                                                //AttachmentPath = mySys.WorkingPathExport + "\\" + strDateTime + "_RGNr" + this.Rechnung.RGNr.ToString() + "_inclAnhang.pdf";
                                                AttachmentPath = mySys.WorkingPathExport + "\\" + helper_InvoiceFileName.GetInvoiceAndAttachmentFileNameWithDatePrefix((int)this.Rechnung.RGNr);
                                                TelerikPrint_DirectPrintToPDF printPDF = new TelerikPrint_DirectPrintToPDF(uRepSource, AttachmentPath, false, mySys.Client.Modul.Fakt_eInvoiceIsAvailable, (int)this.Rechnung.ID);
                                                LogMessages.AddRange(printPDF.LogMessages);
                                                AttachmentPath = printPDF.AttachmentFilePath;

                                                retList.Add(AttachmentPath);
                                                if (!printPDF.InvoiceViewData.ZugferdCheck.IsZUGFeRDAvailable)
                                                {
                                                    ZUGFeRD.ZUGFeRD_ErrorMail mail = new ZUGFeRD.ZUGFeRD_ErrorMail(printPDF.InvoiceViewData, AttachmentPath, this._GL_User, this.Sys);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                clsMail ErrorMail = new clsMail();
                                                ErrorMail.InitClass(new Globals._GL_USER(), null);
                                                ErrorMail.Subject = "clsFaktLager | TelerikPrint_DirectPrintToPDF - Error Mail E-Rechnung";

                                                string strMes = "Exception bei Aufruf clsFaktLager [TelerikPrint_DirectPrintToPDF > Zeile 4972]" + Environment.NewLine;

                                                strMes += Environment.NewLine + Environment.NewLine;
                                                strMes += "-----------------------------------" + Environment.NewLine;
                                                strMes += "CreateRGAndRGAnhangToPDF(clsSystem mySys)";

                                                strMes += "-----------------------------------" + Environment.NewLine;
                                                strMes += "TelerikPrint_DirectPrintToPDF";
                                                strMes += "Paremter:";
                                                strMes += string.Format("{0,0} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", "uRepSource:".PadRight(iCol0Width), uRepSource.ToString());
                                                strMes += string.Format("{0,0} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", "AttachmentPath:".PadRight(iCol0Width), AttachmentPath);
                                                strMes += string.Format("{0,0} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", "Fakt_eInvoiceIsAvailable:".PadRight(iCol0Width), mySys.Client.Modul.Fakt_eInvoiceIsAvailable.ToString());
                                                strMes += string.Format("{0,0} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", "Rechnung.ID".PadRight(iCol0Width), (int)this.Rechnung.ID);
                                                strMes += "Zusatzinfo:";
                                                strMes += string.Format("{0,0} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", "Rechnung.Nr".PadRight(iCol0Width), (int)this.Rechnung.RGNr);
                                                strMes += string.Format("{0,0} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", "Rechnung.Nr".PadRight(iCol0Width), this.Rechnung.Datum.ToString("dd.MM.yyyy"));
                                                strMes += string.Format("{0,0} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", "Receiver".PadRight(iCol0Width), this.Rechnung.ADR_RGEmpfaenger.ADRStringShort);


                                                strMes += ">>>" + Environment.NewLine;
                                                strMes += ">>> ex.Message:" + Environment.NewLine;
                                                strMes += ex.Message;
                                                strMes += ">>> ex.InnerException:" + Environment.NewLine;
                                                strMes += ex.InnerException.ToString();

                                                strMes += ">>> Log aus TelerikPrint_DirectPrintToPDF" + Environment.NewLine;
                                                foreach (string logMsg in LogMessages)
                                                {
                                                    strMes += logMsg + Environment.NewLine;
                                                }

                                                ErrorMail.Message = strMes;
                                                ErrorMail.SendError();
                                            }
                                        }
                                        else
                                        {
                                            //AttachmentPath = mySys.WorkingPathExport + "\\" + strDateTime + "_RGNr" + this.Rechnung.RGNr.ToString() + "_inclAnhang.pdf";
                                            AttachmentPath = mySys.WorkingPathExport + "\\" + helper_InvoiceFileName.GetInvoiceAndAttachmentFileNameWithDatePrefix((int)this.Rechnung.RGNr);
                                            TelerikPrint_DirectPrintToPDF printPDF = new TelerikPrint_DirectPrintToPDF(uRepSource, AttachmentPath, false);
                                            AttachmentPath = printPDF.AttachmentFilePath;
                                            retList.Add(AttachmentPath);
                                        }
                                    }
                                    else
                                    {
                                        string strTExt = "Die Reportdatein [" + FilePathTemp + "] kann nicht gefunden werden. Der Vorgang wird abgebrochen!";
                                        clsMessages.Allgemein_ERRORTextShow(strTExt);
                                    }
                                }
                                else
                                {
                                    FilePathTemp = TelerikPrint.SaveReportFileToHDForUse(tmpRepSettings);

                                    if (File.Exists(FilePathTemp))
                                    {
                                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this.Rechnung.ID));
                                        uRepSource.Uri = FilePathTemp;

                                        //AttachmentPath = mySys.WorkingPathExport + "\\" + strDateTime + "_RGNr" + this.Rechnung.RGNr.ToString() + ".pdf";
                                        AttachmentPath = mySys.WorkingPathExport + "\\" + helper_InvoiceFileName.GetInvoiceFileNameWithDatePrefix((int)this.Rechnung.RGNr);
                                        if (mySys.Client.Modul.Fakt_eInvoiceIsAvailable)
                                        {
                                            //AttachmentPath = mySys.WorkingPathExport + "\\" + strDateTime + "_RGNr" + this.Rechnung.RGNr.ToString() + ".pdf";
                                            TelerikPrint_DirectPrintToPDF printPDF = new TelerikPrint_DirectPrintToPDF(uRepSource, AttachmentPath, false, mySys.Client.Modul.Fakt_eInvoiceIsAvailable, (int)this.Rechnung.ID);
                                            AttachmentPath = printPDF.AttachmentFilePath;
                                            retList.Add(AttachmentPath);
                                            if (!printPDF.InvoiceViewData.ZugferdCheck.IsZUGFeRDAvailable)
                                            {
                                                ZUGFeRD.ZUGFeRD_ErrorMail mail = new ZUGFeRD.ZUGFeRD_ErrorMail(printPDF.InvoiceViewData, AttachmentPath, this._GL_User, this.Sys);
                                            }
                                        }
                                        else
                                        {
                                            //AttachmentPath = mySys.WorkingPathExport + "\\" + strDateTime + "_RGNr" + this.Rechnung.RGNr.ToString() + ".pdf";
                                            TelerikPrint_DirectPrintToPDF printPDF = new TelerikPrint_DirectPrintToPDF(uRepSource, AttachmentPath, false);
                                            AttachmentPath = printPDF.AttachmentFilePath;
                                            retList.Add(AttachmentPath);
                                        }

                                        FileExtension = Path.GetExtension(FilePathTemp);
                                        //--mr
                                        // wenn als Report ein Reportbook verwendet wird, dann kann man davon ausgehen, dass
                                        // der Anhang im Reportbook enthalten ist, deshalb hier die Abfrage auf Reportbook.
                                        if (!FileExtension.ToUpper().Equals(".TRBP"))
                                        {
                                            //RGAnhang als PDF
                                            AttachmentPath = string.Empty;
                                            //tmpRepSettings = tmpRepSettings.GetClassByDocKey(enumIniDocKey.RGAnhang.ToString());
                                            tmpRepSettings = mySys.ReportDocSetting.GetClassByDocKey(enumIniDocKey.RGAnhang.ToString());
                                            if ((tmpRepSettings is clsReportDocSetting) && (tmpRepSettings.ID > 0) && (tmpRepSettings.ReportDataFileExist))
                                            {
                                                AttachmentPath = string.Empty;
                                                //AttachmentPath = mySys.WorkingPathExport + "\\" + strDateTime + "_RGAnhangNr" + this.Rechnung.RGNr.ToString() + ".pdf";
                                                //AttachmentPath = mySys.WorkingPathExport + "\\" + helper_InvoiceFileName.GetInvoiceAndAttachmentFileNameWithDatePrefix((int)this.Rechnung.RGNr);
                                                AttachmentPath = mySys.WorkingPathExport + "\\" + helper_InvoiceFileName.GetInvoiceAttachmentFileNameWithDatePrefix((int)this.Rechnung.RGNr);
                                                uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this.Rechnung.ID));
                                                FilePathTemp = string.Empty;

                                                string tmpRGAnhangFileExtenstion = Path.GetExtension(tmpRepSettings.DocFileNameAndPath);
                                                if (tmpRGAnhangFileExtenstion.ToUpper().Equals(".TRDX"))
                                                {
                                                    FilePathTemp = Path.Combine(mySys.WorkingPathExport + "\\", tmpRepSettings.DocFileNameAndPath);
                                                    FilePathTemp = TelerikPrint.SaveReportFileToHDForUse(tmpRepSettings);
                                                }
                                                else if (tmpRGAnhangFileExtenstion.ToUpper().Equals(".TRBP"))
                                                {
                                                    string tmpPath = string.Empty;
                                                    if (tmpRepSettings.Path.StartsWith("\\"))
                                                    {
                                                        tmpRepSettings.Path = tmpRepSettings.Path.TrimStart('\\');
                                                    }
                                                    FilePathTemp = Path.Combine(Sys.StartupPath, tmpRepSettings.DocFileNameAndPath);
                                                }

                                                if (System.IO.File.Exists(FilePathTemp))
                                                {
                                                    uRepSource.Uri = FilePathTemp;
                                                    TelerikPrint_DirectPrintToPDF printAnhangPDF = new TelerikPrint_DirectPrintToPDF(uRepSource, AttachmentPath, false);
                                                    AttachmentPath = printAnhangPDF.AttachmentFilePath;
                                                    retList.Add(AttachmentPath);
                                                }
                                            }
                                        }
                                        else
                                        {
                                        }
                                    }
                                    else
                                    {
                                        string strTExt = "Die Reportdatein [" + FilePathTemp + "] kann nicht gefunden werden. Der Vorgang wird abgebrochen!";
                                        clsMessages.Allgemein_ERRORTextShow(strTExt);
                                    }
                                }
                            }
                            else
                            {
                                string strTExt = "Es konnte keine passende Reportdatein gefunden werden. Der Vorgang wird abgebrochen!";
                                clsMessages.Allgemein_ERRORTextShow(strTExt);
                            }
                            break;
                        case clsRechnung.const_RechnungsArt_Manuell:
                            tmpRepSettings = mySys.ReportDocSetting.GetClassByDocKey(enumIniDocKey.ManuellerechnungMail.ToString());
                            FilePathTemp = TelerikPrint.SaveReportFileToHDForUse(tmpRepSettings);

                            if ((tmpRepSettings.RSAId > 0) && (tmpRepSettings.ReportDataFileExist))
                            {
                                //--check of reportbook, hier kann die Reportdatei nicht aus der DB kommen, das funktioniert nicht
                                FileExtension = Path.GetExtension(tmpRepSettings.ReportFileName);
                                if (FileExtension.ToUpper().Equals(".TRBP"))
                                {
                                    FilePathTemp = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                                    if (FilePathTemp.EndsWith("\\"))
                                    {
                                        FilePathTemp.Replace("\\", "");
                                    }
                                    FilePathTemp += tmpRepSettings.DocFileNameAndPath;
                                    //FilePathTemp = System.IO.Path.Combine(FilePathTemp, tmpRepSettings.ReportFileName);

                                    if (File.Exists(FilePathTemp))
                                    {
                                        uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this.Rechnung.ID));
                                        uRepSource.Uri = FilePathTemp;

                                        //AttachmentPath = mySys.WorkingPathExport + "\\" + strDateTime + "_RGNr" + this.Rechnung.RGNr.ToString() + "_inclAnhang.pdf";
                                        //TelerikPrint_DirectPrintToPDF printPDF = new TelerikPrint_DirectPrintToPDF(uRepSource, AttachmentPath, false);
                                        //retList.Add(AttachmentPath);

                                        //AttachmentPath = mySys.WorkingPathExport + "\\" + strDateTime + "_RGNr" + this.Rechnung.RGNr.ToString() + "_inclAnhang.pdf";
                                        AttachmentPath = mySys.WorkingPathExport + "\\" + helper_InvoiceFileName.GetInvoiceAndAttachmentFileNameWithDatePrefix((int)this.Rechnung.RGNr);
                                        if (mySys.Client.Modul.Fakt_eInvoiceIsAvailable)
                                        {
                                            TelerikPrint_DirectPrintToPDF printPDF = new TelerikPrint_DirectPrintToPDF(uRepSource, AttachmentPath, false, mySys.Client.Modul.Fakt_eInvoiceIsAvailable, (int)this.Rechnung.ID);
                                            AttachmentPath = printPDF.AttachmentFilePath;
                                            if (!printPDF.InvoiceViewData.ZugferdCheck.IsZUGFeRDAvailable)
                                            {
                                                ZUGFeRD.ZUGFeRD_ErrorMail mail = new ZUGFeRD.ZUGFeRD_ErrorMail(printPDF.InvoiceViewData, AttachmentPath, this._GL_User, this.Sys);
                                            }
                                            retList.Add(AttachmentPath);
                                        }
                                        else
                                        {
                                            TelerikPrint_DirectPrintToPDF printPDF = new TelerikPrint_DirectPrintToPDF(uRepSource, AttachmentPath, false);
                                            AttachmentPath = printPDF.AttachmentFilePath;
                                            retList.Add(AttachmentPath);
                                        }
                                    }
                                    else
                                    {
                                        string strTExt = "Die Reportdatein [" + FilePathTemp + "] kann nicht gefunden werden. Der Vorgang wird abgebrochen!";
                                        clsMessages.Allgemein_ERRORTextShow(strTExt);
                                    }
                                }
                                else
                                {
                                    uRepSource.Parameters.Add(new Telerik.Reporting.Parameter("RGTableID", this.Rechnung.ID));
                                    uRepSource.Uri = FilePathTemp;

                                    //AttachmentPath = mySys.WorkingPathExport + "\\" + strDateTime + "_RGNr" + this.Rechnung.RGNr.ToString() + ".pdf";
                                    AttachmentPath = mySys.WorkingPathExport + "\\" + helper_InvoiceFileName.GetInvoiceFileNameWithDatePrefix((int)this.Rechnung.RGNr);
                                    if (File.Exists(FilePathTemp))
                                    {
                                        //TelerikPrint_DirectPrintToPDF printPDF = new TelerikPrint_DirectPrintToPDF(uRepSource, AttachmentPath, false);
                                        if (mySys.Client.Modul.Fakt_eInvoiceIsAvailable)
                                        {
                                            TelerikPrint_DirectPrintToPDF printPDF = new TelerikPrint_DirectPrintToPDF(uRepSource, AttachmentPath, false, mySys.Client.Modul.Fakt_eInvoiceIsAvailable, (int)this.Rechnung.ID);
                                            AttachmentPath = printPDF.AttachmentFilePath;

                                            if (!printPDF.InvoiceViewData.ZugferdCheck.IsZUGFeRDAvailable)
                                            {
                                                ZUGFeRD.ZUGFeRD_ErrorMail mail = new ZUGFeRD.ZUGFeRD_ErrorMail(printPDF.InvoiceViewData, AttachmentPath, this._GL_User, this.Sys);
                                            }
                                            //retList.Add(AttachmentPath);
                                        }
                                        else
                                        {
                                            TelerikPrint_DirectPrintToPDF printPDF = new TelerikPrint_DirectPrintToPDF(uRepSource, AttachmentPath, false);
                                            AttachmentPath = printPDF.AttachmentFilePath;
                                            //retList.Add(AttachmentPath);
                                        }

                                        if (File.Exists(AttachmentPath))
                                        {
                                            retList.Add(AttachmentPath);
                                        }
                                        //clsPrint.PrintDirectToPDF("RechnungPDF", AttachmentPath, uRepSource);
                                        //retList.Add(AttachmentPath);
                                        tmpRepSettings = null;
                                        AttachmentPath = string.Empty;
                                    }
                                    else
                                    {
                                        string strTExt = "Die Reportdatein [" + FilePathTemp + "] kann nicht gefunden werden. Der Vorgang wird abgebrochen!";
                                        clsMessages.Allgemein_ERRORTextShow(strTExt);
                                    }
                                }
                            }
                            else
                            {
                                string strTExt = "Es konnte keine passende Reportdatein gefunden werden. Der Vorgang wird abgebrochen!";
                                clsMessages.Allgemein_ERRORTextShow(strTExt);
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    clsError Error = new clsError();
                    Error._GL_User = this._GL_User;
                    Error.Code = clsError.code6_201;
                    Error.Aktion = "Rechnung To PDF for Mail";
                    Error.exceptText += "ID: " + tmpRepSettings.ID.ToString() + Environment.NewLine;
                    Error.exceptText += "RSAId: " + tmpRepSettings.RSAId.ToString() + Environment.NewLine;
                    Error.exceptText += "ViewID: " + tmpRepSettings.ViewID.ToString() + Environment.NewLine;
                    Error.exceptText += "Path: " + tmpRepSettings.Path.ToString() + Environment.NewLine;
                    Error.exceptText += "Report: " + tmpRepSettings.ReportFileName.ToString() + Environment.NewLine;
                    Error.exceptText += "FilePath: " + tmpRepSettings.DocFileNameAndPath.ToString() + Environment.NewLine;
                    Error.exceptText += "Execption: " + Environment.NewLine + ex.ToString();

                    Error.WriteError();

                    //ex.ToString();
                    //clsMessages.Allgemein_ERRORTextShow(strError);
                }


            }
            else
            {

            }
            return retList;
        }
        ///<summary>clsFaktLager / Dictionary</summary>
        ///<remarks>.</remarks>
        public Dictionary<string, decimal> CalcArtikel(clsArtikel clsArtikel)
        {
            Dictionary<string, decimal> dict = new Dictionary<string, decimal>();

            string sql = "select * from Artikel a " +
                         "left join Leingang b on a.LEingangTableID=b.ID " +
                        "left join KundenTarife c on b.Auftraggeber=c.AdrID " +
                        "left join TarifPositionen d on c.TarifID=d.TarifID " +
                        "left join TarifGArtZuweisung e on d.TarifID=e.TarifID " +
                        "where LVS_ID=60000 and d.aktiv=1 and " +
                        "(DickeVon<=2 and DickeBis>=2 or (DickeVon=0 and DickeBis=0)) and  " +
                        "(BreiteVon<=1500 and BreiteBis>=1500 or (BreiteVon=0 and BreiteBis=0)) and  " +
                        "(LaengeVon<=0.00 and LaengeBis>=0.00 or (LaengeVon=0 and LaengeBis=0)) and  " +
                        "(BruttoVon<=13635.00 and BruttoBis>=13635.00 or (BruttoVon=0 and BruttoBis=0)) ";

            DataTable dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(sql, BenutzerID, "ArtikelPreise");

            for (int i = 0; i < dtTmp.Rows.Count; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dtTmp.Rows[i]["PreisEinheit"].ToString(), out decTmp);
                dict.Add(dtTmp.Rows[i]["TarifPosArt"].ToString(), decTmp);
            }
            return dict;
        }
    }
}
