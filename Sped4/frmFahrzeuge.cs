using LVS;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmFahrzeuge : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        // private decimal ID=0;
        public bool bo_update;
        internal Int32 iIntIDFahrkzeug = 0;
        internal DataTable dtBesitzer = new DataTable("Mandanten");
        public clsFahrzeuge Fahrzeug;
        public ctrFahrzeug_List _ctrFahrzeugList = new ctrFahrzeug_List();
        public bool eingabeOK = true;


        ///<summary>frmFahrzeuge / frmFahrzeuge_Load</summary>
        ///<remarks></remarks>
        public frmFahrzeuge(ctrFahrzeug_List ctrFahrzeugList)
        {
            _ctrFahrzeugList = ctrFahrzeugList;
            InitializeComponent();
            //Klasse Fahrzeug zuweisen
            Fahrzeug = _ctrFahrzeugList.Fahrzeug;
        }
        ///<summary>frmFahrzeuge / frmFahrzeuge_Load</summary>
        ///<remarks></remarks>
        private void frmFahrzeuge_Load(object sender, EventArgs e)
        {
            CleanFrm();
            //ID = 0;
            bo_update = false;

            chbCoil.Enabled = false;
            chbSattel.Enabled = false;
            tbInnenhoehe.Enabled = false;
            tbLaengeA.Enabled = false;
            tbStellplaetze.Enabled = false;
            gbZM.Enabled = false;

            cbBesitzer.Enabled = true;
            //ComboBox Abgas
            comboAbgas.DataSource = Enum.GetNames(typeof(enumAbgasnorm));
            //cbBesitzer.DataSource = Enum.GetNames(typeof(Sped4.Dokumente.clsBriefkopfdaten.enumMandant));
            dtBesitzer = clsMandanten.GetMandatenList(this.GL_User.User_ID);
            cbBesitzer.DataSource = dtBesitzer;
            cbBesitzer.DisplayMember = "Matchcode";
            cbBesitzer.ValueMember = "Mandanten_ID";
            SetMaxKIntern();
            SetInfoKIntern();
        }
        ///<summary>frmFahrzeuge / SetMaxKIntern</summary>
        ///<remarks>Set max Intern</remarks>
        private void SetMaxKIntern()
        {
            //Read Max Kennzeichen Intern
            Int32 MaxKIntern = clsFahrzeuge.GetMaxKennIntern(this.GL_User);
            MaxKIntern = MaxKIntern + 1;
            tbKIntern.Text = MaxKIntern.ToString();
            SetInfoImageForINr();
            gbFahrDaten.Refresh();
        }
        ///<summary>frmFahrzeuge / SetInfoKIntern</summary>
        ///<remarks></remarks>
        private void SetInfoKIntern()
        {
            string strKInternTextZM = string.Empty;
            string strKInternTextA = string.Empty;
            if (iIntIDFahrkzeug > 0)
            {
                //Zugmaschien
                string strKInternZM = clsFahrzeuge.GetKInternFahrzeug(this.GL_User, iIntIDFahrkzeug, true);
                //Auflieger
                string strKInternA = clsFahrzeuge.GetKInternFahrzeug(this.GL_User, iIntIDFahrkzeug, false);

                //ZM
                if (strKInternZM != string.Empty)
                {
                    strKInternTextZM = "[" + iIntIDFahrkzeug.ToString() + " - Zugmaschine: " + strKInternZM + "]";
                }
                else
                {
                    strKInternTextZM = "[ kein Zugmaschine zugewiesen]";
                }
                //Auflieger                
                if (strKInternA != string.Empty)
                {
                    strKInternTextA = "[" + iIntIDFahrkzeug.ToString() + " - Auflieger: " + strKInternA + "]";
                }
                else
                {
                    strKInternTextA = "[ kein Auflieger zugewiesen]";
                }
            }
            else
            {
                strKInternTextZM = "[ kein Zugmaschine zugewiesen]";
                strKInternTextA = "[ kein Auflieger zugewiesen]";
            }
            lInternIDZM.Text = strKInternTextZM;
            lInternIDAuflieger.Text = strKInternTextA;
        }
        ///<summary>frmFahrzeuge / btnAbbruch_Click</summary>
        ///<remarks></remarks>
        private void btnAbbruch_Click(object sender, EventArgs e)
        {
            this.Fahrzeug.ID = 0;
            //ID = 0; 
            this._ctrFahrzeugList._ctrMenu.CloseFrmFahrzeuge();
        }
        ///<summary>frmFahrzeuge / CleanFrm</summary>
        ///<remarks>Leeren der Datenfelder</remarks>
        private void CleanFrm()
        {
            //---------- Zusweisung der Werte
            tbKFZ.Text = string.Empty;
            tbFabrikat.Text = string.Empty;
            tbBezeichnung.Text = string.Empty;
            tbFGNr.Text = string.Empty;
            tbLaengeA.Text = string.Empty;

            dtpTuev.Value = DateTime.Today;
            dtpSP.Value = DateTime.Today;
            dtpBJ.Value = DateTime.Today;
            dtpSeit.Value = DateTime.Today;

            tbLaufleistung.Text = "";

            chbZM.Visible = true;
            chbZM.Checked = false;

            chbTrailer.Visible = true;
            chbTrailer.Checked = false;

            chbPlane.Visible = true;
            chbPlane.Checked = false;

            chbSattel.Visible = true;
            chbSattel.Checked = false;

            chbCoil.Visible = true;
            chbCoil.Checked = false;

            tbLeergewicht.Text = string.Empty;
            tbZlGw.Text = string.Empty;
            tbInnenhoehe.Visible = true;
            tbInnenhoehe.Text = string.Empty;
            tbStellplaetze.Text = string.Empty;
            tbStellplaetze.Visible = true;
            tbBesonderheit.Text = string.Empty;

            cbAbmeldung.Checked = false;
        }
        ///<summary>frmFahrzeuge / AssignVar</summary>
        ///<remarks>Trim und Zuweisen der Inputdaten</remarks>
        private void AssignVar()
        {
            clsFahrzeuge tmpFahr = this.Fahrzeug.Copy();

            //--------- Leerzeichen werden abgeschnitten
            tbKFZ.Text = tbKFZ.Text.Trim();
            tbKIntern.Text = tbKIntern.Text.ToString().Trim();
            tbFabrikat.Text = tbFabrikat.Text.Trim();
            tbBezeichnung.Text = tbBezeichnung.Text.Trim();
            tbFGNr.Text = tbFGNr.Text.Trim();
            tbLaufleistung.Text = tbLaufleistung.Text.Trim();
            tbLeergewicht.Text = tbLeergewicht.Text.Trim();
            tbZlGw.Text = tbZlGw.Text.Trim();
            tbInnenhoehe.Text = tbInnenhoehe.Text.Trim();
            tbStellplaetze.Text = tbStellplaetze.Text.Trim();
            tbBesonderheit.Text = tbBesonderheit.Text.Trim();
            tbLaengeA.Text = tbLaengeA.Text.Trim();


            //---------- Zusweisung der Werte
            this.Fahrzeug.KFZ = tbKFZ.Text;
            this.Fahrzeug.Fabrikat = tbFabrikat.Text;
            this.Fahrzeug.Bezeichnung = tbBezeichnung.Text;
            this.Fahrzeug.FGNr = tbFGNr.Text;
            this.Fahrzeug.Tuev = dtpTuev.Value;
            this.Fahrzeug.SP = dtpSP.Value;
            this.Fahrzeug.BJ = dtpBJ.Value;
            this.Fahrzeug.seit = dtpSeit.Value;

            if (iIntIDFahrkzeug == 0)
            {
                this.Fahrzeug.KIntern = Convert.ToInt32(tbKIntern.Text);
            }
            else
            {
                this.Fahrzeug.KIntern = iIntIDFahrkzeug;
            }

            if (chbZM.Checked)
            {
                this.Fahrzeug.ZM = Convert.ToChar('T');
                this.Fahrzeug.Anhaenger = Convert.ToChar('F');
            }
            else
            {
                this.Fahrzeug.ZM = Convert.ToChar('F');
                this.Fahrzeug.Anhaenger = Convert.ToChar('T');
            }
            //Trailer
            if (chbTrailer.Checked)
            {
                this.Fahrzeug.ZM = Convert.ToChar('F');
                this.Fahrzeug.Anhaenger = Convert.ToChar('T');
            }
            else
            {
                this.Fahrzeug.ZM = Convert.ToChar('T');
                this.Fahrzeug.Anhaenger = Convert.ToChar('F');
            }
            //Plane
            if (chbPlane.Checked)
            { this.Fahrzeug.Plane = Convert.ToChar('T'); }
            else
            { this.Fahrzeug.Plane = Convert.ToChar('F'); }
            //Sattel
            if (chbSattel.Checked)
            { this.Fahrzeug.Sattel = Convert.ToChar('T'); }
            else
            { this.Fahrzeug.Sattel = Convert.ToChar('F'); }
            //Coil
            if (chbCoil.Checked)
            { this.Fahrzeug.Coil = Convert.ToChar('T'); }
            else
            { this.Fahrzeug.Coil = Convert.ToChar('F'); }

            //Abgasnorm
            if (chbZM.Checked == true)
            {
                this.Fahrzeug.AbgasNorm = comboAbgas.Text;
            }

            //Laufleistung
            if (tbLaufleistung.Text != "")
            {
                tbLaufleistung.Text = tbLaufleistung.Text.ToString().Replace(".", "");
                this.Fahrzeug.Laufleistung = Convert.ToInt32(tbLaufleistung.Text);
            }
            else
            {
                this.Fahrzeug.Laufleistung = 0;
            }
            //Stellplätze
            if (tbStellplaetze.Text != "")
            {
                if (tbStellplaetze.Text.IndexOf('.') != -1)
                {
                    tbStellplaetze.Text = tbStellplaetze.Text.Substring(0, tbStellplaetze.Text.IndexOf('.'));
                }
                if (tbStellplaetze.Text.IndexOf(',') != -1)
                {
                    tbStellplaetze.Text = tbStellplaetze.Text.Substring(0, tbStellplaetze.Text.IndexOf(','));
                }
                this.Fahrzeug.Stellplaetze = Convert.ToInt32(tbStellplaetze.Text);
            }
            else
            {
                this.Fahrzeug.Stellplaetze = 0;
            }
            //Leergewicht
            if (tbLeergewicht.Text != "")
            {
                tbLeergewicht.Text = tbLeergewicht.Text.ToString().Replace(".", "");
                this.Fahrzeug.Leergewicht = Convert.ToInt32(tbLeergewicht.Text);
            }
            else
            {
                this.Fahrzeug.Leergewicht = 0;
            }
            //zlGesamtGewicht
            if (tbZlGw.Text != "")
            {
                tbZlGw.Text = tbZlGw.Text.ToString().Replace(".", "");
                this.Fahrzeug.zlGG = Convert.ToInt32(tbZlGw.Text);
            }
            else
            {
                this.Fahrzeug.zlGG = 0;
            }
            //Achsen
            if (chbTrailer.Checked == true)
            {
                this.Fahrzeug.Achsen = Convert.ToInt32(nudAchsen.Value);
            }
            //Länge
            if (tbLaengeA.Text != "")
            {
                tbLaengeA.Text = tbLaengeA.Text.ToString().Replace(".", ",");
                this.Fahrzeug.Laenge = Convert.ToDecimal(tbLaengeA.Text);
            }
            else
            {
                this.Fahrzeug.Laenge = 0.0m;
            }
            //Innenhöhe
            if (tbInnenhoehe.Text != "")
            {
                tbInnenhoehe.Text = tbInnenhoehe.Text.ToString().Replace(".", ",");
                this.Fahrzeug.Innenhoehe = Convert.ToDecimal(tbInnenhoehe.Text);
            }
            else
            {
                this.Fahrzeug.Innenhoehe = 0.0m;
            }
            //

            if (cbAbmeldung.Checked == true)
            {
                this.Fahrzeug.Abmeldung = dtpAbmeldung.Value.Date;
            }
            else
            {
                this.Fahrzeug.Abmeldung = DateTime.MaxValue.Date;
            }
            this.Fahrzeug.Besonderheit = tbBesonderheit.Text;
            this.Fahrzeug.MandantenID = Convert.ToDecimal(cbBesitzer.SelectedValue.ToString());

            //
            if (!bo_update)
            {
                if (GL_User.write_KFZ)
                {
                    // --- Eintrag in DB ----
                    this.Fahrzeug.ID = 0;
                    this.Fahrzeug.AddItem();
                }
                else
                {
                    clsMessages.User_NoAuthen();
                }
            }
            else if (((bo_update) & (this.Fahrzeug.ID > 0)))
            {
                if (GL_User.write_KFZ)
                {
                    //---- update in DB

                    this.Fahrzeug.updateItem(this.Fahrzeug.ID);
                    //---Form closed for update
                    this.Close();
                }
                else
                {
                    clsMessages.User_NoAuthen();
                }
            }
        }
        ///<summary>frmFahrzeuge / chbZM_CheckedChanged</summary>
        ///<remarks>nicht benötigte Eingabefelder werden während der Eingabe ausgeblendet</remarks>
        private void chbZM_CheckedChanged(object sender, EventArgs e)
        {
            if (chbZM.Checked == true)
            {
                CheckZMEinblenden();
            }
            else
            {
                CheckZMAusblenden();
            }
            SetMaxKIntern();
        }
        ///<summary>frmFahrzeuge / CheckZMEinblenden</summary>
        ///<remarks>Einblenden bei ZM </remarks>
        private void CheckZMEinblenden()
        {
            chbTrailer.Visible = false;
            pbTrailer.Visible = false;
            chbSattel.Enabled = false;
            chbPlane.Enabled = true;
            tbLaengeA.Enabled = false;
            gbZM.Enabled = true;

            //SetKInternVisible(true);
        }
        ///<summary>frmFahrzeuge / CheckZMAusblenden</summary>
        ///<remarks>Ausblenden bei ZM bei ZM </remarks>
        private void CheckZMAusblenden()
        {
            chbTrailer.Visible = true;
            pbTrailer.Visible = true;
            chbSattel.Enabled = false;
            tbLaengeA.Enabled = true;
            gbZM.Enabled = false;

            //SetKInternVisible(false);
        }
        ///<summary>frmFahrzeuge / chbTrailer_CheckedChanged</summary>
        ///<remarks>Checkbox Trailer </remarks>
        private void chbTrailer_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTrailer.Checked == true)
            {
                CheckTrailerEinblenden();
            }
            else
            {
                CheckTrailerAusblenden();
            }
            SetMaxKIntern();
        }
        ///<summary>frmFahrzeuge / CheckTrailerEinblenden</summary>
        ///<remarks></remarks>
        private void CheckTrailerEinblenden()
        {
            chbZM.Visible = false;
            pbTruck.Visible = false;
            gbZM.Enabled = false;
            chbSattel.Enabled = true;
            chbPlane.Enabled = true;
            tbLaengeA.Enabled = true;
        }
        ///<summary>frmFahrzeuge / CheckTrailerAusblenden</summary>
        ///<remarks></remarks>
        private void CheckTrailerAusblenden()
        {
            chbSattel.Enabled = false;
            chbZM.Visible = true;
            pbTruck.Visible = true;
        }
        ///<summary>frmFahrzeuge / chbPlane_CheckedChanged</summary>
        ///<remarks>Checkbox Plane</remarks>
        private void chbPlane_CheckedChanged(object sender, EventArgs e)
        {
            if (chbPlane.Checked == true)
            {
                CheckPlaneEinblenden();
            }
            else
            {
                CheckPlaneAusblenden();
            }

        }
        ///<summary>frmFahrzeuge / CheckPlaneEinblenden</summary>
        ///<remarks></remarks>
        private void CheckPlaneEinblenden()
        {
            tbInnenhoehe.Enabled = true;
            tbStellplaetze.Enabled = true;
            tbLaengeA.Enabled = true;
            chbCoil.Enabled = true;
        }
        ///<summary>frmFahrzeuge / CheckPlaneAusblenden</summary>
        ///<remarks></remarks>
        private void CheckPlaneAusblenden()
        {
            chbCoil.Enabled = false;
            tbInnenhoehe.Enabled = false;
            tbStellplaetze.Enabled = false;
            if (chbTrailer.Checked == true)
            {
                tbLaengeA.Enabled = true;
            }
            else
            {
                tbLaengeA.Enabled = false;
            }
        }
        ///<summary>frmFahrzeuge / CheckMissingInput</summary>
        ///<remarks>Check Inputdaten</remarks>
        private bool CheckMissingInput()
        {
            eingabeOK = true;
            string strHelp = "";
            char[] ad = { '@' };
            char[] tele = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '/' };
            char[] bst = { 'a', 'A', 'b', 'c', 'C', 'd', 'D', 'e', 'E', 'F', 'f', 'g', 'G', 'h', 'H', 'i', 'I', 'j', 'J', 'k', 'K', 'l', 'L', 'm', 'M', 'n', 'N', 'o', 'O', 'p', 'P', 'q', 'Q', 'r', 'R', 's', 'S', 't', 'T', 'u', 'U', 'v', 'V', 'w', 'W', 'x', 'X', 'y', 'Y', 'z', 'Z' };
            char[] Uml = { 'ä', 'Ä', 'ö', 'Ö', 'ü', 'Ü' };
            char[] num = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            //strHelp = "Folgende Felder wurden nicht korrekt ausgefüllt:\n";
            //-----  Info welche Felder fehlen  ------------------------
            if (tbKFZ.Text == "")
            {
                strHelp = strHelp + "KFZ \n";
                eingabeOK = false;
            }
            if (tbKFZ.Text.Length > 12)   // Zeichenanzahl in DB Feld KFZ
            {
                strHelp = strHelp + "KFZ-Eingabe ist zu lang max. 12 Zeichen \n";
                eingabeOK = false;
            }
            if (tbFabrikat.Text == "")
            {
                strHelp = strHelp + "Fabrikat \n";
                eingabeOK = false;
            }
            if ((chbTrailer.Checked == false) & (chbZM.Checked == false))
            {
                strHelp = strHelp + "Zugmaschine / Motorwagen bzw. Anhänger nicht gewählt\n";
                eingabeOK = false;
            }
            if (tbBezeichnung.Text == "")
            {
                strHelp = strHelp + "Bezeichnung \n";
                eingabeOK = false;
            }
            //Stellplätze
            if (tbStellplaetze.Text != "")
            {
                if (tbStellplaetze.Text.ToString().IndexOfAny(bst) != -1)
                {
                    strHelp = strHelp + "Stellplätze darf nur Ziffern enthalten \n";
                    eingabeOK = false;
                }
            }
            //Laufleistung
            if (tbLaufleistung.Text != "")
            {
                if (tbLaufleistung.Text.ToString().IndexOfAny(bst) != -1)
                {
                    strHelp = strHelp + "Laufleistung darf nur Ziffern enthalten \n";
                    eingabeOK = false;
                }
            }
            //Leergewicht
            if (tbLeergewicht.Text != "")
            {
                if (tbLeergewicht.Text.ToString().IndexOfAny(bst) != -1)
                {
                    strHelp = strHelp + "Leergewicht darf nur Ziffern enthalten \n";
                    eingabeOK = false;
                }
            }
            else
            {
                strHelp = strHelp + "Leergewicht fehlt \n";
                eingabeOK = false;
            }
            //zlGG
            if (tbZlGw.Text != "")
            {
                if (tbZlGw.Text.ToString().IndexOfAny(bst) != -1)
                {
                    strHelp = strHelp + "zl. Gesamtgewicht darf nur Ziffern enthalten \n";
                    eingabeOK = false;
                }
            }
            else
            {
                if (chbTrailer.Checked == true)
                {
                    strHelp = strHelp + "zl. Gesamtgewicht fehlt \n";
                    eingabeOK = false;
                }
            }
            //Länge
            if (tbLaengeA.Text != "")
            {
                if (tbLaengeA.Text.ToString().IndexOfAny(bst) != -1)
                {
                    strHelp = strHelp + "Länge darf nur Ziffern enthalten \n";
                    eingabeOK = false;
                }
            }
            //Innenhöhe
            if (tbInnenhoehe.Text != "")
            {
                if (tbInnenhoehe.Text.ToString().IndexOfAny(bst) != -1)
                {
                    strHelp = strHelp + "Innenhöhe darf nur Ziffern enthalten \n";
                    eingabeOK = false;
                }
            }
            //Interne Kennzeichen
            if ((tbKIntern.Text == "") & (chbZM.Checked == true))
            {
                strHelp = strHelp + "Der Zugmaschine muss eine interne Nummer vergeben werden \n";
                eingabeOK = false;
            }
            else
            {
                if (tbKIntern.Text.ToString().IndexOfAny(bst) != -1)
                {
                    strHelp = strHelp + "Die interne Nummer muss ein Zahlenwert sein \n";
                    eingabeOK = false;
                }
                //Check,ob die Nummer schon vergeben wurde

            }

            if (!eingabeOK)
            {
                string strEinleitung = "Folgende Felder wurden nicht korrekt ausgefüllt:\n";
                MessageBox.Show(strEinleitung + strHelp, "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            return eingabeOK;
        }
        ///<summary>frmFahrzeuge / ReadDataByID</summary>
        ///<remarks>Check Inputdaten</remarks>
        public void ReadDataByID(decimal myID)
        {
            bo_update = true;
            //ID = fID;

            this.Fahrzeug.ID = myID;
            this.Fahrzeug.Fill();

            tbKFZ.Text = this.Fahrzeug.KFZ;
            iIntIDFahrkzeug = this.Fahrzeug.KIntern;
            tbFabrikat.Text = this.Fahrzeug.Fabrikat;
            tbBezeichnung.Text = this.Fahrzeug.Bezeichnung;
            tbFGNr.Text = this.Fahrzeug.FGNr;
            dtpTuev.Value = this.Fahrzeug.Tuev;
            dtpSP.Value = this.Fahrzeug.SP;
            dtpBJ.Value = this.Fahrzeug.BJ;
            dtpSeit.Value = this.Fahrzeug.seit;
            tbLaufleistung.Text = this.Fahrzeug.Laufleistung.ToString();
            if (this.Fahrzeug.ZM.ToString() == "T")
            {
                chbZM.Checked = true;
                comboAbgas.Text = this.Fahrzeug.AbgasNorm.ToString();
            }
            if (this.Fahrzeug.Anhaenger.ToString() == "T")
            {
                chbTrailer.Checked = true;
                nudAchsen.Value = this.Fahrzeug.Achsen;
            }
            if (this.Fahrzeug.Plane.ToString() == "T")
            {
                chbPlane.Checked = true;
            }
            if (this.Fahrzeug.Sattel.ToString() == "T")
            {
                chbSattel.Checked = true;
            }
            if (this.Fahrzeug.Coil.ToString() == "T")
            {
                chbCoil.Checked = true;
            }
            tbLeergewicht.Text = this.Fahrzeug.Leergewicht.ToString();
            tbZlGw.Text = this.Fahrzeug.zlGG.ToString();
            tbInnenhoehe.Text = Functions.FormatDecimal(this.Fahrzeug.Innenhoehe);
            tbStellplaetze.Text = this.Fahrzeug.Stellplaetze.ToString();
            tbLaengeA.Text = Functions.FormatDecimal(this.Fahrzeug.Laenge);
            tbBesonderheit.Text = this.Fahrzeug.Besonderheit;
            if (this.Fahrzeug.Abmeldung == DateTime.MaxValue.Date)
            {
                cbAbmeldung.Checked = false;
            }
            else
            {
                cbAbmeldung.Checked = true;
            }
            cbBesitzer.SelectedValue = this.Fahrzeug.MandantenID;
            SetInfoKIntern();
        }
        ///<summary>frmFahrzeuge / ReadDataByID</summary>
        ///<remarks>setzt den zu ändernen Datensatz</remarks>
        private void SetUpdateItem(DataSet ds)
        {
            //Baustelle raus???
            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbKFZ.Text = ds.Tables[0].Rows[i]["KFZ"].ToString();
                iIntIDFahrkzeug = (Int32)ds.Tables[0].Rows[i]["KIntern"];
                tbFabrikat.Text = ds.Tables[0].Rows[i]["Fabrikat"].ToString();
                tbBezeichnung.Text = ds.Tables[0].Rows[i]["Bezeichnung"].ToString();
                tbFGNr.Text = ds.Tables[0].Rows[i]["FGNr"].ToString();
                dtpTuev.Value = Convert.ToDateTime(ds.Tables[0].Rows[i]["Tuev"].ToString());
                dtpSP.Value = Convert.ToDateTime(ds.Tables[0].Rows[i]["SP"].ToString());
                dtpBJ.Value = Convert.ToDateTime(ds.Tables[0].Rows[i]["BJ"].ToString());
                dtpSeit.Value = Convert.ToDateTime(ds.Tables[0].Rows[i]["seit"].ToString());
                tbLaufleistung.Text = ds.Tables[0].Rows[i]["Laufleistung"].ToString();
                if (ds.Tables[0].Rows[i]["ZM"].ToString() == "T")
                {
                    chbZM.Checked = true;
                    comboAbgas.Text = ds.Tables[0].Rows[i]["Abgas"].ToString();
                }
                if (ds.Tables[0].Rows[i]["Anhaenger"].ToString() == "T")
                {
                    chbTrailer.Checked = true;
                    nudAchsen.Value = (Int32)ds.Tables[0].Rows[i]["Achsen"];
                }
                if (ds.Tables[0].Rows[i]["Plane"].ToString() == "T")
                {
                    chbPlane.Checked = true;
                }
                if (ds.Tables[0].Rows[i]["Sattel"].ToString() == "T")
                {
                    //chbSattel.Enable = true;
                    chbSattel.Checked = true;
                }
                if (ds.Tables[0].Rows[i]["Coil"].ToString() == "T")
                {
                    //chbCoil.Enabled = true;
                    chbCoil.Checked = true;
                }


                tbLeergewicht.Text = ds.Tables[0].Rows[i]["Leergewicht"].ToString();
                tbZlGw.Text = ds.Tables[0].Rows[i]["zlGG"].ToString();
                tbInnenhoehe.Text = ds.Tables[0].Rows[i]["Innenhoehe"].ToString();
                tbStellplaetze.Text = ds.Tables[0].Rows[i]["Stellplaetze"].ToString();
                tbLaengeA.Text = ds.Tables[0].Rows[i]["Laenge"].ToString();
                tbBesonderheit.Text = ds.Tables[0].Rows[i]["Besonderheit"].ToString();
                //cbBesitzer.SelectedValue = ds.Tables[0].Rows[i]["Besitzer"].ToString();
                cbBesitzer.SelectedItem = ds.Tables[0].Rows[i]["Besitzer"].ToString();
                //Abgemeldet
                //string t1 = ((DateTime)ds.Tables[0].Rows[i]["bis"]).ToString();
                //string t2 = DateTime.MaxValue.Date.ToString();


                if ((DateTime)ds.Tables[0].Rows[i]["bis"] == DateTime.MaxValue.Date)
                {
                    cbAbmeldung.Checked = false;
                }
                else
                {
                    cbAbmeldung.Checked = true;
                }
            }
        }
        ///<summary>frmFahrzeuge / cbAbmeldung_CheckedChanged</summary>
        ///<remarks>checkbox Abmeldung</remarks>
        private void cbAbmeldung_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAbmeldung.Checked == true)
            {
                dtpAbmeldung.Enabled = true;
            }
            else
            {
                dtpAbmeldung.Enabled = false;
            }

        }
        ///<summary>frmFahrzeuge / btnInterneNr_Click</summary>
        ///<remarks>CHECK interne Nummer </remarks>
        private void btnInterneNr_Click(object sender, EventArgs e)
        {
            SetInfoImageForINr();
            SetInfoKIntern();
        }
        ///<summary>frmFahrzeuge / SetInfoImageForINr</summary>
        ///<remarks></remarks>
        private void SetInfoImageForINr()
        {
            Int32 iNr = Convert.ToInt32(tbKIntern.Text);
            if (clsFahrzeuge.ExistInterneNr(this.GL_User, iNr))
            {
                pbINr.Image = Sped4.Properties.Resources.critical.ToBitmap();
                btnIntIDSelect.Enabled = false;
            }
            else
            {
                pbINr.Image = Sped4.Properties.Resources.check;
                btnIntIDSelect.Enabled = true;
            }
        }
        ///<summary>frmFahrzeuge / btnIntIDSelect_Click</summary>
        ///<remarks></remarks>
        private void btnIntIDSelect_Click(object sender, EventArgs e)
        {
            SetCheckedIDtoFahrzeug();
            clsFahrzeuge.updateInterneIDByFahrzeugID(this.GL_User, this.Fahrzeug.ID, iIntIDFahrkzeug);
            SetInfoKIntern();
            btnIntIDSelect.Enabled = false;
        }
        ///<summary>frmFahrzeuge / SetCheckedIDtoFahrzeug</summary>
        ///<remarks></remarks>
        private void SetCheckedIDtoFahrzeug()
        {
            if (chbZM.Checked)
            {
                iIntIDFahrkzeug = Convert.ToInt32(tbKIntern.Text);
            }
            if (chbTrailer.Checked)
            {
                iIntIDFahrkzeug = Convert.ToInt32(tbKIntern.Text);
            }
        }
        ///<summary>frmFahrzeuge / SetKInternVisible</summary>
        ///<remarks></remarks>
        private void SetKInternVisible(bool visible)
        {
            btnInterneNr.Visible = visible;
            tbKIntern.Visible = visible;
            lInternIDZM.Visible = visible;
            pbINr.Visible = visible;
        }
        ///<summary>frmFahrzeuge / tsbtnSavePrint_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSavePrint_Click(object sender, EventArgs e)
        {
            //------ Eingabeüberprüfung der Pflichtfelder-----
            // Adresse mit Straße muss vollständig sein ( Suchbegriff, Name1,Str, PLZ, Ort)
            if (CheckMissingInput())
            {
                //Check, ob Kennzeichen bereits vorhanden
                if (!clsFahrzeuge.IsKFZIn(this.GL_User, tbKFZ.Text.ToString().Trim()) | (bo_update))
                {
                    //in "AsignVar" wird unterschieden in Save und Update - bei Update wird
                    //das Form nach Ausführung geschlossen
                    AssignVar();

                    //-- Eingabemaske auf Null setzen
                    CleanFrm();
                    _ctrFahrzeugList.initList();
                }
                else
                {
                    clsMessages.Fahrzeug_KennzeichenBereitsVorhanden();
                }
            }
            SetMaxKIntern();
        }
        ///<summary>frmFahrzeuge / tsbClose_Click</summary>
        ///<remarks></remarks>
        private void tsbClose_Click(object sender, EventArgs e)
        {
            _ctrFahrzeugList.initList();
            //ID = 0;
            this._ctrFahrzeugList._ctrMenu.CloseFrmFahrzeuge();
        }
    }
}
