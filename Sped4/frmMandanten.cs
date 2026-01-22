using Common.Models;
using LVS;
using LVS.Helper;
using LVS.ViewData;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    ///<summary>frmMandanten</summary>
    ///<remarks>Über dieses Formular können neue Mandanten angelegt, bestehende Mandantendaten geänder oder
    ///         ein Mandant deaktiviert werden.
    ///         Vorgehensweisen der verschiedenen Aktionen:
    ///         - Neuer Mandant: 
    ///                 -> Über die ADR-Such-Maske eine Adresse auswählen
    ///                    oder einen Neue ADR anlegen
    ///                 -> ADR-ID nach Auswahl wird übergeben
    ///                 -> entsprechende Daten in werden in den Textboxfelder angezeigt
    ///                 -> bUpdate-Flag auf FALSE für Neuen Mandanten setzen
    ///                 
    ///                 -> Button Speichern startet die folgenden Funktionen
    ///                 -> Check der Eingabefelder Matchcode, Bemerkung, aktiv
    ///                 -> Check Userberechtigung für Aktion
    ///                 -> clsMandant füllen 
    ///                 -> Daten werde in DB eingetragen
    ///                 -> Clear Form
    ///          - Update Mandant:
    ///                 -> per Doppelclick wird die Daten aus dem Grid-Mandanten übernommen
    ///                 -> bUpdate = true
    ///                 -> Check der Eingabefelder Matchcode, Bemerkung, aktiv
    ///                 -> Check Userberechtigung für Aktion
    ///                 -> clsMandant füllen 
    ///                 -> Daten werde in DB aktualisiert
    ///                 -> Clear Form
    ///           -Primärschlüssel
    ///                 -> die Primärschlüssel werden angezeigt, wenn die Mandantdaten in die Eingabeform übernommen wurden
    ///                 -> es wird der aktuelle Schlüssel angezeigt
    ///                 -> beim Speichern nocheinmal geprüft</remarks>
    public partial class frmMandanten : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        internal clsMandanten clsMandant;
        public ctrMenu _ctrMenu;
        internal clsPrimeKeys primekeys;

        internal Int32 iSplitterContainerDistance;
        internal Int32 iGrdListWidth;
        internal bool bUpdate;
        internal bool bValueCheck;
        public Int32 SearchButton = 0;
        internal decimal decADRID = 0;
        internal decimal decMandantenID = 0;

        internal MandantenViewData mandantenViewData = new MandantenViewData();


        /********************************************************************************************
         *                                      Methoden / Funktionen
         * *****************************************************************************************/
        //
        ///<summary>frmMandanten/frmMandanten</summary>
        ///<remarks>Initialisierung der Form.</remarks>
        public frmMandanten()
        {
            InitializeComponent();
        }
        ///<summary>frmMandanten/frmMandanten_Load</summary>
        ///<remarks>Grundlegenden Werte für die Form werden gesetzt:
        ///         - SplitterDistance >>> sorgt für eine feste Größe des panAdd
        ///         - Instanz clsMandant wird initialisiert und der User zugewiesen
        ///         - InitFrm >>> Textboxen und Grid werden initialisiert</remarks>
        private void frmMandanten_Load(object sender, EventArgs e)
        {
            iSplitterContainerDistance = panAdd.Width;
            clsMandant = new clsMandanten();
            primekeys = new clsPrimeKeys();
            clsMandant.BenutzerID = GL_User.User_ID;
            InitFrm();
        }
        ///<summary>frmMandanten/tsbtnClose_Click</summary>
        ///<remarks>Das Formular wird beendet / geschlossen.</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ///<summary>frmMandanten/InitFrm</summary>
        ///<remarks>bUpdate-Flag wird auf False gesetzt >>> Neuer Mandant
        ///         Über die Funktionen ClearFrm werden alle Eingabefelder und 
        ///         Variablen für die Form reseted und für die Funktion InitGrd 
        ///         wird die Mandatenliste geladen.</remarks>
        public void InitFrm()
        {
            bUpdate = false;
            ClearFrm();
            InitGrd();
        }
        ///<summary>frmMandanten/ClearFrm</summary>
        ///<remarks>Resetet alle Variablen und Eingabefelder:
        ///         - Variablen
        ///             -> decADRID >>> ID Adressen
        ///             -> decMandantenID >>> ID Mandanten
        ///         - Eingabefelder
        ///             -> tdMDiD >>> ID Kombi Adressen und Mandant
        ///             -> tbMC >>> Matchcode
        ///             -> tbFirma >>> Firmenname / Mandantenname
        ///             -> tbStrasse, tbPLZ, tbOrt, tbLand >>> Adresse
        ///             -> tbBeschreibung >>> Infotext/Beschreibung für den Mandanten
        ///             -> cbStatus >>> Flag Mandant aktiv / inaktiv
        ///             -> cbDefaultSped / cbDefaultLager >>> Flag für den Standard-Mandanten 
        ///          Über die Funktion CheckButton werden entsprechend die Menübutton aktiviert.</remarks>
        public void ClearFrm()
        {
            decADRID = 0.0M;
            decMandantenID = 0.0M;
            tbMdID.Text = string.Empty;
            tbMC.Text = string.Empty;
            tbFirma.Text = string.Empty;
            tbStrasse.Text = string.Empty;
            tbPLZ.Text = string.Empty;
            tbOrt.Text = string.Empty;
            tbLand.Text = string.Empty;
            tbBeschreibung.Text = string.Empty;
            cbStatus.Checked = true;
            cbDefaultLager.Checked = false;
            cbDefaultSped.Checked = false;
            CheckButtons(false);
            tbReportPath.Text = string.Empty;

            //eRechnungsdaten
            tbBank.Text = string.Empty;
            tbBLZ.Text = string.Empty;
            tbBic.Text = string.Empty;
            tbIban.Text = string.Empty;
            tbKonto.Text = string.Empty;
            tbAP.Text = string.Empty;
            tbMail.Text = string.Empty;
            tbHomepage.Text = string.Empty;
            tbPhone.Text = string.Empty;
            tbSteuernummer.Text = string.Empty;
            tbUstId.Text = string.Empty;
            tbOrganisation.Text = string.Empty;

            CleanTBPrimeKeys();
        }
        ///<summary>frmMandanten/CheckButtons</summary>
        ///<remarks>Die folgenden Button werden Enable gesetzt:
        ///         - tsbSpeichern >>> Speichern der Daten 
        ///         - tsbClearFrm  >>> manuelles Reseten/Clear der Eingabefelder und Variablen
        ///         - tsbnNew >>> ein neuer Mandant soll erstellt werden
        ///         - tbtnPrimeKeyShow >>> zeigt die Primekey 
        ///         !!! Der Button tsbtnNew muss immer den gegenteiligen Wert wie die anderen 
        ///         Button erhalten für die entsprechende Bearbeitungmöglichkeit von Neu und Update.</remarks>
        ///<param name="bEnable">Wird als bool übergeben und setzt entsprechend den Parameter
        ///                      Enable.</param>
        private void CheckButtons(Boolean bEnable)
        {
            tsbSpeichern.Enabled = bEnable;
            tsbtnClearFrm.Enabled = bEnable;
            tsbtnNew.Enabled = (!bEnable);
            tsbtnPrimeKeyShow.Enabled = bEnable;
        }
        ///<summary>frmMandanten/tsbtnNew_Click</summary>
        ///<remarks>Öffnet die Form "frmADRSearch" zur Auswahl ein bestehenden Adresse bzw. zum 
        ///         neuanlegen einer Adresse.</remarks>
        ///<param name="Object">Hier wird das Object übergeben und dann in der Funkton entsprechend
        ///         gecasted, sodass die Übergabe der AdressID und das Aufrufen der verschiedenen 
        ///         Funktionen aus der Adresssuche funktioniert.</param>
        private void tsbtnNew_Click(object sender, EventArgs e)
        {
            this._ctrMenu.OpenADRSearch(this);
            cbDefaultLager.Checked = false;
            cbDefaultSped.Checked = false;
        }
        ///<summary>frmMandanten/TakeOverAdressID</summary>
        ///<remarks>Delegatenfunktion aus der ADRSuche zur Übergabe der Adress ID. Anhand der ADR-ID 
        ///         werden die entsprechenden Adressdaten ausgelesen und die Daten entsprechend der 
        ///         Eingabefelder auf der Form gesetzt. Entsprechend müssen nun die Button entsprechend
        ///         aktiviert werden.</remarks>
        ///<param name="decTmp">Adress ID</param>
        ///<returns>decTmp ist der Rückgabewert, den wir aus der Adresssuche erhalten.</returns>
        public void TakeOverAdressID(decimal decTmp)
        {
            decADRID = decTmp;
            if (decADRID > 0)
            {
                FillEingabeFelder();
                bUpdate = false;
                //Buttons freigeben
                CheckButtons(true);
            }
        }
        ///<summary>frmMandanten/FillEingabeFelder</summary>
        ///<remarks>Adressdaten werden anhand der ID ermittelt und in die entsprechenden
        ///         Eingabefelder übergeben. Der Matchcode aus den Adressen wird vorerst
        ///         hier mitübernommen wird später noch einmal überschrieben.</remarks>
        private void FillEingabeFelder()
        {
            DataTable dt = clsADR.GetADRbyID(decADRID, GL_User.User_ID);
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                tbMdID.Text = "MandantID/" + decADRID.ToString();
                tbMC.Text = dt.Rows[i]["ViewID"].ToString();
                tbFirma.Text = dt.Rows[i]["Name1"].ToString();
                tbStrasse.Text = dt.Rows[i]["Str"].ToString();
                tbPLZ.Text = dt.Rows[i]["PLZ"].ToString();
                tbOrt.Text = dt.Rows[i]["Ort"].ToString();
                tbLand.Text = dt.Rows[i]["Land"].ToString();

            }
        }
        ///<summary>frmMandanten/tsbSpeichern_Click</summary>
        ///<remarks>Der Speichervorgang wird gestartet und dadurch verschiedene Unterfunktionen:
        ///         - Check Userberechtigung
        ///         - Check Eingabefelder
        ///         - clsMandant bekommt die Daten zugewiesen
        ///         - Unterscheidung Update / NEU
        ///         - Update / Add Mandantdaten
        ///         - Reset der Form</remarks>
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            //GroupBox PrimeKeys ausblenden
            gbPrimeKeys.Enabled = false;

            //Check UserBerechtigungen
            if (GL_User.write_Mandant)
            {
                //Check EingabeFelder
                if (DoCheck())
                {
                    Mandanten man = new Mandanten();
                    mandantenViewData.Mandant.AddressId = (int)decADRID;
                    mandantenViewData.Mandant.Matchcode = tbMC.Text.Trim();
                    mandantenViewData.Mandant.Description = tbBeschreibung.Text.Trim();
                    mandantenViewData.Mandant.IsActiv = cbStatus.Checked;
                    mandantenViewData.Mandant.IsDefaultSped = cbDefaultSped.Checked;
                    mandantenViewData.Mandant.IsDefaultStore = cbDefaultLager.Checked;
                    mandantenViewData.Mandant.ReportPath = tbReportPath.Text.Trim();
                    mandantenViewData.Mandant.Bank = tbBank.Text.Trim();
                    mandantenViewData.Mandant.Blz = tbBLZ.Text.Trim();
                    mandantenViewData.Mandant.Bic = tbBic.Text.Trim();
                    mandantenViewData.Mandant.Konto = tbKonto.Text.Trim();
                    mandantenViewData.Mandant.Iban = tbIban.Text.Trim();
                    mandantenViewData.Mandant.Contact = tbAP.Text.Trim();
                    mandantenViewData.Mandant.Mail = tbMail.Text.Trim();
                    mandantenViewData.Mandant.Homepage = tbHomepage.Text.Trim();
                    mandantenViewData.Mandant.Phone = tbPhone.Text.Trim();
                    mandantenViewData.Mandant.TaxNumber = tbSteuernummer.Text.Trim();
                    mandantenViewData.Mandant.VatId = tbUstId.Text.Trim();
                    mandantenViewData.Mandant.Organisation = tbOrganisation.Text.Trim();

                    //clsMandant.ADR_ID = decADRID;
                    //clsMandant.MatchCode = tbMC.Text.Trim();
                    //clsMandant.Beschreibung = tbBeschreibung.Text.Trim();
                    //clsMandant.aktiv = cbStatus.Checked;
                    //clsMandant.Default_Lager = cbDefaultLager.Checked;
                    //clsMandant.Default_Sped = cbDefaultSped.Checked;
                    //clsMandant.ReportPath = tbReportPath.Text.Trim();

                    if (bUpdate)
                    {
                        //clsMandant.UpdateMandant();
                        mandantenViewData.Update();
                    }
                    else
                    {
                        //clsMandant.Add();
                        mandantenViewData.Add();
                    }
                    InitFrm();
                }
            }
        }
        ///<summary>frmMandanten/DoCheck</summary>
        ///<remarks>Check der Eingabedaten vor dem Speichern:
        ///         - Neu >>> Matchcode muss eindeutig sein 
        ///         - Eingabefelder != string.empty
        ///         - Anzeige einer Fehlemeldung</remarks>
        ///<returns>Rückgabebool für Status Check</returns>
        private Boolean DoCheck()
        {
            bool bOK = true;
            string strMes = string.Empty;
            //Matchcode existiert bereits
            if (
                clsMandanten.ExistMandantByMC(tbMC.Text.Trim(), GL_User.User_ID)
                &&
                (!bUpdate)
               )
            {
                strMes = strMes + "Der Matchcode existiert bereits!" + Environment.NewLine;
                bOK = false;
            }
            //Matchcode leer
            if (tbMC.Text.Trim() == string.Empty)
            {
                strMes = strMes + "Das Eingabefeld Matchcode ist leer!" + Environment.NewLine;
                bOK = false;
            }
            //Reportpath leer
            if (tbReportPath.Text.Trim() == string.Empty)
            {
                strMes = strMes + "Das Eingabefeld Reportpfad ist leer!" + Environment.NewLine;
                bOK = false;
            }

            //------------------ e Rechnung Eingabefelder ------------------------
            // UST
            if (tbUstId.Text.Trim() == string.Empty)
            {
                strMes = strMes + "Das Eingabefeld USId ist leer!" + Environment.NewLine;
                bOK = false;
            }
            else
            {
                Helper_VAT_Validation vat = new Helper_VAT_Validation(tbUstId.Text);
                bOK = vat.ValidationOK;
                if (!bOK)
                {
                    strMes = strMes + "Die UStId: " + tbUstId.Text + " entspricht nicht den Vorgaben eienr UStId!" + Environment.NewLine;
                }
            }
            //-- Bank
            if (tbBank.Text.Trim() == string.Empty)
            {
                strMes = strMes + "Das Eingabefeld Bankname ist leer!" + Environment.NewLine;
                bOK = false;
            }
            //-- ´BLZ
            if (tbBLZ.Text.Trim() == string.Empty)
            {
                strMes = strMes + "Das Eingabefeld BLZ ist leer!" + Environment.NewLine;
                bOK = false;
            }
            //-- ´BIC
            if (tbBLZ.Text.Trim() == string.Empty)
            {
                strMes = strMes + "Das Eingabefeld BIC ist leer!" + Environment.NewLine;
                bOK = false;
            }
            //-- ´Konto
            if (tbKonto.Text.Trim() == string.Empty)
            {
                strMes = strMes + "Das Eingabefeld Konto ist leer!" + Environment.NewLine;
                bOK = false;
            }
            //-- ´IBAN
            if (tbIban.Text.Trim() == string.Empty)
            {
                strMes = strMes + "Das Eingabefeld IBAN ist leer!" + Environment.NewLine;
                bOK = false;
            }
            //------------------------- Kontaktdaten ------------------------
            //-- Ansprechpartner
            if (tbAP.Text.Trim() == string.Empty)
            {
                strMes = strMes + "Das Eingabefeld Ansprechpartner ist leer!" + Environment.NewLine;
                bOK = false;
            }
            //-- Telefon
            if (tbPhone.Text.Trim() == string.Empty)
            {
                strMes = strMes + "Das Eingabefeld Telefon ist leer!" + Environment.NewLine;
                bOK = false;
            }
            //-- E-Mail
            if (tbMail.Text.Trim() == string.Empty)
            {
                strMes = strMes + "Das Eingabefeld E-Mail ist leer!" + Environment.NewLine;
                bOK = false;
            }


            if (!bOK)
            {
                clsMessages.Allgemein_EingabeDatenFehlerhaft(strMes);
            }
            return bOK;
        }
        ///<summary>frmMandanten/SetFrmWidth</summary>
        ///<remarks>Die Formgröße wird dynamisch an das Gridgröße angepasst. Die Größe für das 
        ///         Eingabepanel bleibt konstant.</remarks>
        private void SetFrmWidth()
        {
            this.scMain.SplitterDistance = iSplitterContainerDistance + 5;
            //grd.Width wird ermittelt
            iGrdListWidth = Functions.dgv_GetWidthShownGrid(ref grd);
            //Frm.width wird berechnet
            this.Width = iSplitterContainerDistance + iGrdListWidth + 30;
        }
        ///<summary>frmMandanten/InitGrd</summary>
        ///<remarks>Die Daten für die Mandantenliste wird ermittelt und verschiedene Gridformatierungen
        ///         vorgenommen.</remarks>
        private void InitGrd()
        {
            DataTable dt = new DataTable();
            dt = clsMandanten.GetMandatenList(GL_User.User_ID);
            grd.DataSource = dt;
            grd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //Textumbruch für diese Spalte - Text wird mehrzeilig angezeigt
            this.grd.Columns["Beschreibung"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //Anpassung der Formbreite
            SetFrmWidth();
        }
        ///<summary>frmMandanten/grd_CellDoubleClick</summary>
        ///<remarks>Auswahl eines Mandanten per doppelklick auf das Grid. Durch Mandanten- und 
        ///         Adress ID werden die entsprechenden Daten ermittelt und entsprechend ange-
        ///         zeigt. Dabei wird das bUpdate-Flag auf True gesetzt und entsprechend die 
        ///         Menübutton aktiviert/deaktiviert.</remarks>
        private void grd_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (
                (Decimal.TryParse(this.grd.Rows[this.grd.CurrentRow.Index].Cells["ADR_ID"].Value.ToString(), out decADRID))
               &
                (Decimal.TryParse(this.grd.Rows[this.grd.CurrentRow.Index].Cells["Mandanten_ID"].Value.ToString(), out decMandantenID))
               )
            {
                gbPrimeKeys.Enabled = false;
                CleanTBPrimeKeys();
                clsMandant.ID = decMandantenID;
                clsMandant.ADR_ID = decADRID;
                FillEingabeFelder();
                SetMandantenDatenByID();
                bUpdate = true;
                CheckButtons(true);
            }
        }
        ///<summary>frmMandanten/SetMandantenDatenByID</summary>
        ///<remarks>Anhand der MandantenID werden die restlichen Daten entsprechend gesetzt.</remarks>
        private void SetMandantenDatenByID()
        {
            //Chekc Mandanten ID > 0 sein
            if (decMandantenID > 0)
            {
                mandantenViewData = new MandantenViewData((int)decMandantenID);
                if (mandantenViewData.Mandant.Id > 0)
                {
                    tbMdID.Text = mandantenViewData.Mandant.Id.ToString() + "/" + mandantenViewData.Mandant.AddressId.ToString();
                    tbMC.Text = mandantenViewData.Mandant.Matchcode;
                    tbBeschreibung.Text = mandantenViewData.Mandant.Description;
                    tbReportPath.Text = mandantenViewData.Mandant.ReportPath;
                    cbStatus.Checked = mandantenViewData.Mandant.IsActiv;
                    cbDefaultSped.Checked = mandantenViewData.Mandant.IsDefaultSped;
                    cbDefaultLager.Checked = mandantenViewData.Mandant.IsDefaultStore;

                    tbBank.Text = mandantenViewData.Mandant.Bank;
                    tbBLZ.Text = mandantenViewData.Mandant.Blz;
                    tbBic.Text = mandantenViewData.Mandant.Bic;
                    tbKonto.Text = mandantenViewData.Mandant.Konto;
                    tbIban.Text = mandantenViewData.Mandant.Iban;
                    tbAP.Text = mandantenViewData.Mandant.Contact;
                    tbMail.Text = mandantenViewData.Mandant.Mail;
                    tbHomepage.Text = mandantenViewData.Mandant.Homepage;
                    tbPhone.Text = mandantenViewData.Mandant.Phone;
                    tbUstId.Text = mandantenViewData.Mandant.VatId;
                    tbSteuernummer.Text = mandantenViewData.Mandant.TaxNumber;
                    tbOrganisation.Text = mandantenViewData.Mandant.Organisation;
                }
                ////2.Check, ob die ID auch in der DB existiert
                //if (clsMandant.GetMandantByID())
                //{
                //    tbMdID.Text = clsMandant.ID.ToString() + "/" + clsMandant.ADR_ID.ToString();
                //    tbMC.Text = clsMandant.MatchCode;
                //    tbBeschreibung.Text = clsMandant.Beschreibung;
                //    tbReportPath.Text = clsMandant.ReportPath;
                //    cbStatus.Checked = clsMandant.aktiv;
                //    cbDefaultSped.Checked = clsMandant.Default_Sped;
                //    cbDefaultLager.Checked = clsMandant.Default_Lager;
                //}
            }
        }
        ///<summary>frmMandanten/cbStatus_CheckedChanged</summary>
        ///<remarks>Bei der Statusänderung (aktiv/inaktiv) wird entsprechend der Text für die 
        ///         Checkbox mit geändert.
        ///         - Checked >>> aktiv</remarks>
        private void cbStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (cbStatus.Checked)
            {
                cbStatus.Text = "aktiv";
            }
            else
            {
                cbStatus.Text = "inaktiv";
            }
        }
        ///<summary>frmMandanten/tsbtnClearFrm_Click</summary>
        ///<remarks>Manuelles Reset der Form (Clean) wird durchgeführt.</remarks>
        private void tsbtnClearFrm_Click(object sender, EventArgs e)
        {
            //GroupBox PrimeKeys ausblenden
            gbPrimeKeys.Enabled = false;
            ClearFrm();
        }
        ///<summary>frmMandanten/grd_CellFormatting</summary>
        ///<remarks>Beim Schreiben der Daten in Grid wird ein Trim der Daten vorgenommen.</remarks>
        private void grd_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.Value = grd.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim();
        }
        ///<summary>frmMandanten/frmMandanten_ResizeEnd</summary>
        ///<remarks>Beim Ändern der Größe wird darauf geachtet, dass die größe des PanelAdd und 
        ///         der SplitterDistance konstant bleibt über die Funktion SetFrmWidth</remarks>
        private void frmMandanten_ResizeEnd(object sender, EventArgs e)
        {
            SetFrmWidth();
        }
        ///<summary>frmMandanten/splitContainer1_SplitterMoved</summary>
        ///<remarks>Beim Ändern der Größe wird darauf geachtet, dass die größe des PanelAdd und 
        ///         der SplitterDistance konstant bleibt über die Funktion SetFrmWidth
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SetFrmWidth();
        }

        /**********************************************************
         * 
         * *******************************************************/
        private void btnSaveANr_Click(object sender, EventArgs e)
        {
            if (decMandantenID > 0)
            {
                DoUpdatePrimeKey(sender);
            }
        }
        private void btnSaveLfsNr_Click(object sender, EventArgs e)
        {
            if (decMandantenID > 0)
            {
                DoUpdatePrimeKey(sender);
            }
        }
        private void btnSaveLvsNr_Click(object sender, EventArgs e)
        {
            if (decMandantenID > 0)
            {
                DoUpdatePrimeKey(sender);
            }
        }
        private void btnSaveRGNr_Click(object sender, EventArgs e)
        {
            if (decMandantenID > 0)
            {
                DoUpdatePrimeKey(sender);
            }
        }
        private void btnSaveGSNr_Click(object sender, EventArgs e)
        {
            if (decMandantenID > 0)
            {
                DoUpdatePrimeKey(sender);
            }
        }
        private void btnSaveLEingangID_Click(object sender, EventArgs e)
        {
            if (decMandantenID > 0)
            {
                DoUpdatePrimeKey(sender);
            }
        }
        private void btnSaveLAusgangID_Click(object sender, EventArgs e)
        {
            if (decMandantenID > 0)
            {
                DoUpdatePrimeKey(sender);
            }
        }
        //
        private void DoUpdatePrimeKey(object sender)
        {
            string strSender = string.Empty;

            primekeys.Mandanten_ID = decMandantenID;
            primekeys.AuftragsNr = Convert.ToDecimal(tbAuftragsNr.Text);
            primekeys.LfsNr = Convert.ToDecimal(tbLfsNr.Text);
            primekeys.RGNr = Convert.ToDecimal(tbRGNr.Text);
            primekeys.LvsNr = Convert.ToDecimal(tbLVSNr.Text);
            primekeys.GSNr = Convert.ToDecimal(tbGSNr.Text);
            primekeys.LEingangID = Convert.ToDecimal(tbLEingangID.Text);
            primekeys.LAusgangID = Convert.ToDecimal(tbLAusgangID.Text);

            strSender = ((Button)sender).Name.ToString();
            if (strSender == "btnSaveANr")
            {
                primekeys.UpdatePrimeKeyAuftragsNr();
            }
            else
                if (strSender == "btnSaveGSNr")
            {
                primekeys.UpdatePrimeKeyGSNr();
            }
            else
                    if (strSender == "btnSaveRGNr")
            {
                primekeys.UpdatePrimeKeyRGNr();
            }
            else
                        if (strSender == "btnSaveLvsNr")
            {
                primekeys.UpdatePrimeKeyLvsNr();
            }
            else
                            if (strSender == "btnSaveLfsNr")
            {
                primekeys.UpdatePrimeKeyLfsNr();
            }
            else
                                if (strSender == "btnSaveLEingangID")
            {
                primekeys.UpdatePrimeKeyLEingangID();
            }
            else
                                    if (strSender == "btnSaveLAusgangID")
            {
                primekeys.UpdatePrimeKeyLAusgangID();
            }
            FillPrimeKeysGroupBox();
        }
        ///<summary>frmMandaten / tsbtnPrimeKeyShow_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPrimeKeyShow_Click(object sender, EventArgs e)
        {
            //bUpdate = True >>> dann sind die Mandantendaten in die Eingabeform geladen
            //somit ist das die Bestätigung, dass ein Mandant ausgewählt wurde
            if ((bUpdate) & (decMandantenID > 0))
            {
                CleanTBPrimeKeys();
                if (gbPrimeKeys.Enabled)
                {
                    gbPrimeKeys.Enabled = false;
                }
                else
                {
                    gbPrimeKeys.Enabled = true;
                }
                FillPrimeKeysGroupBox();
            }
        }
        ///<summary>frmMandaten / FillPrimeKeysGroupBox</summary>
        ///<remarks></remarks>
        private void FillPrimeKeysGroupBox()
        {
            primekeys = new clsPrimeKeys();
            primekeys.BenutzerID = GL_User.User_ID;
            primekeys.Mandanten_ID = decMandantenID;
            primekeys.Fill();

            tbAuftragsNr.Text = primekeys.AuftragsNr.ToString();
            tbLfsNr.Text = primekeys.LfsNr.ToString();
            tbLVSNr.Text = primekeys.LvsNr.ToString();
            tbRGNr.Text = primekeys.RGNr.ToString();
            tbGSNr.Text = primekeys.GSNr.ToString();
            tbLEingangID.Text = primekeys.LEingangID.ToString();
            tbLAusgangID.Text = primekeys.LAusgangID.ToString();
        }
        //
        private void CleanTBPrimeKeys()
        {
            tbAuftragsNr.Text = string.Empty;
            tbLfsNr.Text = string.Empty;
            tbLVSNr.Text = string.Empty;
            tbRGNr.Text = string.Empty;
            tbGSNr.Text = string.Empty;
            tbLEingangID.Text = string.Empty;
            tbLAusgangID.Text = string.Empty;
        }
    }
}
