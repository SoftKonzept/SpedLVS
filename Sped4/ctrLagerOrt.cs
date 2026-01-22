using LVS;
using Sped4.Controls;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrLagerOrt : UserControl
    {
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;
        internal clsLagerOrt LagerOrt;
        internal ctrEinlagerung _ctrEinlagerung;
        internal frmTmp _frmTmp;
        public delegate void ctrLagerOrtCloseEventHandler();
        public event ctrLagerOrtCloseEventHandler ctrLagerClose;
        internal bool bTakeOverLagerort = false;
        internal bool bWerkUpdate = false;
        internal bool bHalleUpdate = false;
        internal bool bReiheUpdate = false;
        internal bool bEbeneUpdate = false;
        internal bool bPlatzUpdate = false;
        internal Int32 iWerkSelectedRow = 0;
        internal Int32 iHalleSelectedRow = 0;
        internal Int32 iReiheSelectedRow = 0;
        internal Int32 iEbeneSelectedRow = 0;
        internal Int32 iPlatzSelectedRow = 0;

        internal DataTable dtLagerOrt = new DataTable();

        /***********************************************************************************
         * 
         * ********************************************************************************/
        public ctrLagerOrt()
        {
            InitializeComponent();
        }
        ///<summary>ctrLagerOrt / ctrLagerOrt_Load</summary>
        ///<remarks></remarks>
        private void ctrLagerOrt_Load(object sender, EventArgs e)
        {
            //Combobox für die auswahl des Lagerplatzortes füllen
            tscbLagerOrt.ComboBox.DataSource = Enum.GetNames(typeof(enumLagerOrtTable));
            bool bSetSelecetedIndex0 = true;

            //Hier wird jetzt erst geschaut, ob eine Vorgabe existiert, 
            //-ja -> Vorgabe als Vorauswahl sezten
            //-nein -> Vorgabe leer dann Standard
            //-nein -> Vorgabe nicht vorhanden, falsche Vorgabe
            for (Int32 i = 0; i <= tscbLagerOrt.ComboBox.Items.Count - 1; i++)
            {
                //if (this.GL_User.sys_VE_LagerOrt_AuswahlLagerOrtPlatz.ToString() != string.Empty)
                if (
                        (this._ctrMenu._frmMain.GL_System.VE_LagerOrt_AuswahlLagerOrtPlatz != null) &&
                        (this._ctrMenu._frmMain.GL_System.VE_LagerOrt_AuswahlLagerOrtPlatz.ToString() != string.Empty)
                   )
                {
                    if (this.tscbLagerOrt.ComboBox.Items[i].ToString() == this._ctrMenu._frmMain.GL_System.VE_LagerOrt_AuswahlLagerOrtPlatz.ToString())
                    {
                        bSetSelecetedIndex0 = false;
                        this.tscbLagerOrt.ComboBox.SelectedIndex = i;
                        break;
                    }
                    else
                    {
                        bSetSelecetedIndex0 = true;
                    }
                }
                else
                {
                    bSetSelecetedIndex0 = true;
                }
            }
            if (bSetSelecetedIndex0)
            {
                tscbLagerOrt.ComboBox.SelectedIndex = 0;
            }
            tscbLagerOrt.ComboBox.Enabled = true;
            //wenn externer Lagerort über Artikel gewählt, dann combo 
            //deaktivieren und vorauswahl ex.Festlegen
            if (this._ctrEinlagerung != null)
            {
                /****
                if (this._ctrEinlagerung.bIsExternerLagerOrt)
                {
                    this.tscbLagerOrt.ComboBox.Text= Globals.enumLagerOrtTable.exLagerort.ToString();
                    this.tscbLagerOrt.ComboBox.Enabled = false;
                }
                 * ****/
            }

            //Eingabefelder einblenden
            LagerortEingabeFelderFadeInOut(bTakeOverLagerort);
            //init ClsLagerOrt
            LagerOrt = new clsLagerOrt();
            LagerOrt._GL_User = this.GL_User;
            InitCtrLagerOrt();
        }
        ///<summary>ctrLagerOrt / tsbClose_Click</summary>
        ///<remarks>Ctr schliessen</remarks>
        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseCtr();
        }
        ///<summary>ctrLagerOrt / tsbtnInsertActivate_Click</summary>
        ///<remarks></remarks>
        private void CloseCtr()
        {
            if (this._frmTmp != null)
            {
                Functions.frm_FormTypeClose(typeof(frmTmp));
            }
            else
            {
                Int32 Count = this.ParentForm.Controls.Count;
                for (Int32 i = 0; (i <= (Count - 1)); i++)
                {
                    if (this.ParentForm.Controls[i].Name == "TempSplitterLagerOrt")
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        //i = Count - 1;      // ist nur ein Controll vorhanden
                    }
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrLagerOrt))
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        i = Count - 1;      // ist nur ein Controll vorhanden
                    }
                }
            }
        }
        ///<summary>ctrLagerOrt / tsbtnInsertActivate_Click</summary>
        ///<remarks></remarks>
        private void tsbtnInsertActivate_Click(object sender, EventArgs e)
        {
            bTakeOverLagerort = false;
            //Eingabefelder einblenden
            LagerortEingabeFelderFadeInOut(bTakeOverLagerort);
            InitCtrLagerOrt();
        }
        ///<summary>ctrLagerOrt / tsbtnLagerOrtSearch_Click</summary>
        ///<remarks></remarks>
        private void tsbtnLagerOrtSearch_Click(object sender, EventArgs e)
        {
            bTakeOverLagerort = true;
            //Eingabefelder ausblenden
            LagerortEingabeFelderFadeInOut(bTakeOverLagerort);
            InitCtrLagerOrt();
        }
        ///<summary>ctrLagerOrt / LagerortEingabeFelderFadeInOut</summary>
        ///<remarks></remarks>
        private void LagerortEingabeFelderFadeInOut(bool bFadeIN)
        {
            scWerk.Panel1Collapsed = bFadeIN;
            scHalle.Panel1Collapsed = bFadeIN;
            scReihe.Panel1Collapsed = bFadeIN;
            scEbene.Panel1Collapsed = bFadeIN;
            scPlatz.Panel1Collapsed = bFadeIN;
        }
        ///<summary>ctrLagerOrt / ctrLagerOrt_Load</summary>
        ///<remarks></remarks>
        private void InitCtrLagerOrt()
        {
            //Werk
            InitWerk();
            //Halle
            InitHalle();
            //Reihe
            InitReihe();
            //Ebene
            InitEbene();
            //Platz
            InitPlatz();
            //Grid Lagerort
            LoadDGVLagerort();
        }
        ///<summary>ctrLagerOrt / tscbLagerOrt_SelectedIndexChanged</summary>
        ///<remarks>Ein neuer Lagerortplatz wird gewählt  und es müssen dann die entsprechenden Daten geladen werden</remarks>
        private void tscbLagerOrt_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDGVLagerort();
        }
        ///<summary>ctrLagerOrt / SetSelectedRowInDGV</summary>
        ///<remarks></remarks>
        private void SetSelectedRowInDGV(ref AFGrid myDGV, decimal myID)
        {
            for (Int32 i = 0; i <= myDGV.Rows.Count - 1; i++)
            {
                if (myID > 0)
                {
                    decimal decID = 0;
                    Decimal.TryParse(myDGV.Rows[i].Cells["ID"].Value.ToString(), out decID);
                    if (decID == myID)
                    {
                        myDGV.Rows[i].Selected = true;
                        myDGV.CurrentCell = myDGV["Beschreibung", i];

                        //setzt die selected Row
                        string strDGVName = myDGV.Name.ToString();
                        switch (strDGVName)
                        {
                            case "dgvWerk":
                                iWerkSelectedRow = i;
                                break;
                            case "dgvHalle":
                                iHalleSelectedRow = i;
                                break;
                            case "dgvReihe":
                                iReiheSelectedRow = i;
                                break;
                            case "dgvEbene":
                                iEbeneSelectedRow = i;
                                break;
                            case "dgvPlatz":
                                iPlatzSelectedRow = i;
                                break;
                        }
                        break;
                    }
                }
                else
                {
                    myDGV.Rows[0].Selected = true;
                    break;
                }
            }
        }
        ///<summary>ctrLagerOrt / dgvLagerOrtSearch</summary>
        ///<remarks></remarks>
        private void dgvLagerOrtSearch(string strSearchObj, string strSearchText)
        {
            LoadDGVLagerort();
            DataView dv = dtLagerOrt.DefaultView;
            string strFilter = string.Empty;

            switch (strSearchObj)
            {
                case "Werk":
                    strFilter = "Werk='" + strSearchText + "'";
                    break;
                case "Halle":
                    strFilter = "Werk='" + tstbHalleWerkInfo.Text + "'" +
                                " AND " +
                                "Halle='" + strSearchText + "'";
                    break;
                case "Reihe":
                    strFilter = "Werk='" + tstbHalleWerkInfo.Text + "'" +
                                " AND " +
                                "Halle='" + tstbReiheHallenInfo.Text + "'" +
                                " AND " +
                                "Reihe='" + strSearchText + "'";
                    break;
                case "Ebene":
                    strFilter = "Werk='" + tstbHalleWerkInfo.Text + "'" +
                                " AND " +
                                "Halle='" + tstbReiheHallenInfo.Text + "'" +
                                " AND " +
                                "Reihe='" + tstbEbeneReiheInfo.Text + "'" +
                                " AND " +
                                "Ebene='" + strSearchText + "'";
                    break;
                case "Platz":
                    strFilter = "Werk='" + tstbHalleWerkInfo.Text + "'" +
                               " AND " +
                               "Halle='" + tstbReiheHallenInfo.Text + "'" +
                               " AND " +
                               "Reihe='" + tstbEbeneReiheInfo.Text + "'" +
                              " AND " +
                               "Ebene='" + tstbPlatzEbene.Text + "'" +
                               " AND " +
                               "Platz='" + strSearchText + "'";
                    break;
            }

            dv.RowFilter = strFilter;
            dtLagerOrt = dv.ToTable();
            this.dgvLagerort.DataSource = dtLagerOrt;
        }

        /************************************************************************************************
         *                                    Werk
         * **********************************************************************************************/
        ///<summary>ctrLagerOrt / InitWerk</summary>
        ///<remarks></remarks>
        private void InitWerk()
        {
            SetWerkEingabeFelderEnabled(false);
            LagerOrt.Werk.Init();
            //Clear Eingabefelder Werk
            ClearWerkEingabeFelder();
            //ORderID setzen
            decimal decTmp = (decimal)LagerOrt.Werk.maxOrderID;
            nudWerkOrderID.Maximum = decTmp;
            //Datatable for dgvWerk
            dgvWerk.DataSource = LagerOrt.Werk.dtWerk;
            this.dgvWerk.Columns["ID"].Visible = false;
            this.dgvWerk.Columns["exLagerOrt"].Visible = false;
            this.dgvWerk.Columns["OrderID"].DisplayIndex = 0;
            this.dgvWerk.AutoResizeColumns();
            this.dgvWerk.Columns["Beschreibung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            SetSelectedRowInDGV(ref this.dgvWerk, LagerOrt.Werk.ID);
            decTmp = 0;
            if (this.dgvWerk.Rows.Count > 0)
            {
                try
                {
                    decTmp = (decimal)this.dgvWerk.Rows[iWerkSelectedRow].Cells["ID"].Value;
                }
                catch
                {
                    iWerkSelectedRow = 0;
                    decTmp = (decimal)this.dgvWerk.Rows[iWerkSelectedRow].Cells["ID"].Value;
                }
            }
            SelectetWerkIDChanged(decTmp);
        }
        ///<summary>ctrLagerOrt / InitWerk</summary>
        ///<remarks></remarks>
        private void ClearWerkEingabeFelder()
        {
            tbWerkBeschreibung.Text = string.Empty;
            tbWerkBezeichnung.Text = string.Empty;
        }
        ///<summary>ctrLagerOrt / tsbtnWerkNeu_Click</summary>
        ///<remarks>Datensatz anlegen</remarks>
        private void tsbtnWerkNeu_Click(object sender, EventArgs e)
        {
            bWerkUpdate = false;
            SetWerkEingabeFelderEnabled(true);
            //Clear Werkeingabefelder
            ClearWerkEingabeFelder();
            tbWerkBezeichnung.Focus();
            //OrderID maximum setzen
            nudWerkOrderID.Maximum = nudWerkOrderID.Maximum + 1;
            nudWerkOrderID.Value = LagerOrt.Werk.maxOrderID + 1;
        }
        ///<summary>ctrLagerOrt / CheckWerksbezeichnung</summary>
        ///<remarks></remarks>
        private bool CheckWerksbezeichnung()
        {
            bool EingabeOK = true;
            string strMes = string.Empty;
            if (!bWerkUpdate)
            {
                if (LagerOrt.Werk.ExistWerkByBezeichnung())
                {
                    EingabeOK = false;
                    strMes = strMes + "Werksbezeichnung existiert bereits \n\r";
                }
            }
            LagerOrt.Werk.Bezeichnung = tbWerkBezeichnung.Text;
            if (tbWerkBezeichnung.Text == string.Empty)
            {
                EingabeOK = false;
                strMes = strMes + "Das Feld Werksbezeichnung muss gefüllt sein \n\r";
            }

            if (!EingabeOK)
            {
                MessageBox.Show(strMes, "Achtung");
            }
            return EingabeOK;
        }
        ///<summary>ctrLagerOrt / tbWerkBezeichnung_Validated</summary>
        ///<remarks>Datenbank Spalte nvarchar(50), Pflichtfeld</remarks>
        private void tbWerkBezeichnung_Validated(object sender, EventArgs e)
        {
            LagerOrt.Werk.Bezeichnung = tbWerkBezeichnung.Text;
            Int32 iCount = 0;
            string strTmp = tbWerkBezeichnung.Text;
            iCount = strTmp.Length;
            if (iCount > 0)
            {
                if (iCount > 50)
                {
                    //string auf 50 Zeichen kürzen
                    string strNew = strTmp.Substring(0, 49);
                    //Info 
                    string strError = "Der eingegebene Werkname ist zu lang und wurde auf 50 Zeichen gekürzt!";
                    clsMessages.Allgemein_EingabefeldZuLang(strError);
                    tbWerkBezeichnung.Focus();
                }
            }
        }
        ///<summary>ctrLagerOrt / dgvWerk_DoubleClick</summary>
        ///<remarks>Übername der Daten in die Eingabefelder zum Edit.</remarks>
        private void dgvWerk_DoubleClick(object sender, EventArgs e)
        {
            if (this.dgvWerk.CurrentCell != null)
            {
                bWerkUpdate = true;
                SetWerkEingabeFelderEnabled(true);
                decimal decTmp = 0;
                string strTmp = this.dgvWerk.Rows[this.dgvWerk.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
                decimal.TryParse(strTmp, out decTmp);
                LagerOrt.Werk.ID = decTmp;
                LagerOrt.Werk.Init();
                this.dgvWerk.DataSource = LagerOrt.Werk.dtWerk;
                SetWerkDatenToFrm();

                SetSelectedRowInDGV(ref this.dgvWerk, LagerOrt.Werk.ID);
                dgvWerk_Click(sender, e);
            }
        }
        ///<summary>ctrLagerOrt / SetWerkDatenToFrm</summary>
        ///<remarks>Setz die Daten in die entsprechenden Eingabefelder.</remarks>
        private void SetWerkDatenToFrm()
        {
            ClearWerkEingabeFelder();
            tbWerkBezeichnung.Text = LagerOrt.Werk.Bezeichnung;
            tbWerkBeschreibung.Text = LagerOrt.Werk.Beschreibung;
            cbExLagerOrt.Checked = LagerOrt.Werk.ExLagerOrt;
            if (LagerOrt.Werk.OrderID > 0)
            {
                nudWerkOrderID.Value = (decimal)LagerOrt.Werk.OrderID;
            }
            else
            {
                nudWerkOrderID.Value = nudWerkOrderID.Maximum;
            }
        }
        ///<summary>ctrLagerOrt / nudWerkOrderID_ValueChanged</summary>
        ///<remarks>Pflichtfeld - wenn die OrderID geändert wird, dann muss Sie direkt in der gesamten Table auch angepasst werden.</remarks>
        private void nudWerkOrderID_ValueChanged(object sender, EventArgs e)
        {
            if (bWerkUpdate)
            {
                if (LagerOrt.Werk.ID > 0)
                {
                    Int32 iTmp = (Int32)nudWerkOrderID.Value;
                    if (LagerOrt.Werk.OrderID != iTmp)
                    {
                        LagerOrt.Werk.UpdateOrderID(LagerOrt.Werk.ID, iTmp);
                        LagerOrt.Werk.Init();
                        this.dgvWerk.DataSource = LagerOrt.Werk.dtWerk;
                        SetWerkDatenToFrm();
                        SetSelectedRowInDGV(ref dgvWerk, LagerOrt.Werk.ID);
                    }
                }
            }
        }
        ///<summary>ctrLagerOrt / SetWerkEingabeFelderEnabled</summary>
        ///<remarks>Aktivieren / Deaktivieren der Eingabefelder.</remarks>
        private void SetWerkEingabeFelderEnabled(bool bEnabled)
        {
            tbWerkBezeichnung.Enabled = bEnabled;
            tbWerkBeschreibung.Enabled = bEnabled;
            nudWerkOrderID.Enabled = bEnabled;
        }
        ///<summary>ctrLagerOrt / tsbtnWerkSave_Click</summary>
        ///<remarks>Werkdaten speichern.</remarks>
        private void tsbtnWerkSave_Click(object sender, EventArgs e)
        {
            //Zuweisung der Klasse
            LagerOrt.Werk.Bezeichnung = tbWerkBezeichnung.Text.Trim();
            LagerOrt.Werk.Beschreibung = tbWerkBeschreibung.Text.Trim();
            LagerOrt.Werk.OrderID = (Int32)nudWerkOrderID.Value;
            LagerOrt.Werk.ExLagerOrt = cbExLagerOrt.Checked;

            if (bWerkUpdate)
            {
                LagerOrt.Werk.Update();
            }
            else
            {
                if (CheckWerksbezeichnung())
                {
                    //Insert
                    LagerOrt.Werk.ID = 0;
                    LagerOrt.Werk.Add();
                }
            }
            SetWerkEingabeFelderEnabled(false);
            ClearWerkEingabeFelder();
            InitCtrLagerOrt();
        }
        ///<summary>ctrLagerOrt / tbWerkBezeichnung_TextChanged</summary>
        ///<remarks></remarks>
        private void tbWerkBezeichnung_TextChanged(object sender, EventArgs e)
        {
            //CheckWerksbezeichnung();
        }
        ///<summary>ctrLagerOrt / tsbtnWerkDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnWerkDelete_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                decimal decTmp = 0;
                //Werk ID wurde bereits durch den doppelklick
                //string strTmp = this.dgvWerk.Rows[this.dgvWerk.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
                //decimal.TryParse(strTmp, out decTmp);
                //LagerOrt.Werk.ID = decTmp;
                LagerOrt.Werk.Init();
                LagerOrt.DeleteLagerOrt("Werk");
                LagerOrt.Init();
                LagerOrt.Werk.UpdateOrderID(0, 0);
                InitCtrLagerOrt();
            }
        }
        ///<summary>ctrLagerOrt / tsbtnWerkClear_Click</summary>
        ///<remarks></remarks>
        private void tsbtnWerkClear_Click(object sender, EventArgs e)
        {
            SetWerkEingabeFelderEnabled(false);
            ClearWerkEingabeFelder();
        }
        ///<summary>ctrLagerOrt / tsbtnWerkClear_Click</summary>
        ///<remarks>Setz bei jedem Click die Werk ID um </remarks>
        private void dgvWerk_Click(object sender, EventArgs e)
        {
            iWerkSelectedRow = this.dgvWerk.CurrentCell.RowIndex;
            iHalleSelectedRow = 0;
            iReiheSelectedRow = 0;
            iEbeneSelectedRow = 0;
            iPlatzSelectedRow = 0;

            if (this.dgvWerk.CurrentCell != null)
            {
                decimal decTmp = 0;
                string strTmp = this.dgvWerk.Rows[iWerkSelectedRow].Cells["ID"].Value.ToString();
                decimal.TryParse(strTmp, out decTmp);
                SelectetWerkIDChanged(decTmp);
            }
            //Filter DGV Lagerort
            if (bTakeOverLagerort)
            {
                string strSearch = this.dgvWerk.Rows[iWerkSelectedRow].Cells["Bezeichnung"].Value.ToString();
                dgvLagerOrtSearch("Werk", strSearch);
            }
        }
        ///<summary>ctrLagerOrt / SelectetWerkIDChanged</summary>
        ///<remarks>Setz bei jedem Click die Werk ID um </remarks>
        private void SelectetWerkIDChanged(decimal myWerkID)
        {
            LagerOrt.Werk.ID = myWerkID;
            LagerOrt.Werk.FillDaten();
            LagerOrt.Werk.Halle.WerkID = myWerkID;
            InitHalle();
        }
        /**********************************************************************************************************************
         *                                                  Halle
         * *******************************************************************************************************************/
        ///<summary>ctrLagerOrt / InitHalle</summary>
        ///<remarks></remarks>
        private void InitHalle()
        {
            SetHalleEingabeFelderEnabled(false);
            LagerOrt.Werk.Halle.WerkID = LagerOrt.Werk.ID;

            /***
            decimal decTmp = (decimal)LagerOrt.Werk.Halle.maxOrderID;
            nudHalleOrderID.Maximum = decTmp;
            //ORderID setzen
            if (bHalleUpdate)
            {
                nudHalleOrderID.Value = LagerOrt.Werk.Halle.OrderID; 
            }
            else
            {
                nudHalleOrderID.Value = decTmp;
            }
            ***/

            decimal decTmp = (decimal)LagerOrt.Werk.maxOrderIDHalle;
            nudHalleOrderID.Maximum = decTmp;

            LagerOrt.Werk.Halle.Init();
            //Clear Eingabefelder Werk
            ClearHalleEingabeFelder();
            //Datatable for dgvWerk
            this.dgvHalle.DataSource = LagerOrt.Werk.Halle.dtHalle;
            this.dgvHalle.Columns["ID"].Visible = false;
            this.dgvHalle.Columns["WerkID"].Visible = false;
            this.dgvHalle.Columns["OrderID"].DisplayIndex = 0;
            this.dgvHalle.AutoResizeColumns();
            this.dgvHalle.Columns["Beschreibung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            SetSelectedRowInDGV(ref this.dgvHalle, LagerOrt.Werk.Halle.ID);
            if (this.dgvHalle.Rows.Count > 0)
            {
                try
                {
                    decTmp = (decimal)this.dgvHalle.Rows[iHalleSelectedRow].Cells["ID"].Value;
                }
                catch
                {
                    iHalleSelectedRow = 0;
                    decTmp = (decimal)this.dgvHalle.Rows[iHalleSelectedRow].Cells["ID"].Value;
                }
            }
            SelectetHalleIDChanged(decTmp);
        }
        ///<summary>ctrLagerOrt / ClearHalleEingabeFelder</summary>
        ///<remarks></remarks>
        private void ClearHalleEingabeFelder()
        {
            tbHalleBeschreibung.Text = string.Empty;
            tbHalleBezeichnung.Text = string.Empty;
            tstbHalleWerkInfo.Text = LagerOrt.Werk.Bezeichnung;
        }
        ///<summary>ctrLagerOrt / SetHalleEingabeFelderEnabled</summary>
        ///<remarks>Aktivieren / Deaktivieren der Eingabefelder.</remarks>
        private void SetHalleEingabeFelderEnabled(bool bEnabled)
        {
            tbHalleBezeichnung.Enabled = bEnabled;
            tbHalleBeschreibung.Enabled = bEnabled;
            nudHalleOrderID.Enabled = bEnabled;
        }
        ///<summary>ctrLagerOrt / tsbtnHalleAdd_Click</summary>
        ///<remarks>.</remarks>
        private void tsbtnHalleAdd_Click(object sender, EventArgs e)
        {
            bHalleUpdate = false;
            SetHalleEingabeFelderEnabled(true);
            //Clear Werkeingabefelder
            ClearHalleEingabeFelder();
            tbHalleBezeichnung.Focus();
            //OrderID maximum setzen

            //Baustelle
            //nudHalleOrderID.Maximum = nudHalleOrderID.Maximum + 1;
            //nudHalleOrderID.Value = LagerOrt.Werk.Halle.maxOrderID + 1;

            //Schritt 1
            nudHalleOrderID.Maximum = LagerOrt.Werk.maxOrderIDHalle + 1;
            nudHalleOrderID.Value = nudHalleOrderID.Maximum;

            //angebotene Vorgabe Beschreibung
            tbHalleBeschreibung.Text = tstbHalleWerkInfo.Text;
        }
        ///<summary>ctrLagerOrt / tsbtnHalleSave_Click</summary>
        ///<remarks>.</remarks>
        private void tsbtnHalleSave_Click(object sender, EventArgs e)
        {
            //Zuweisung der Klasse
            LagerOrt.Werk.Halle.Bezeichnung = tbHalleBezeichnung.Text.Trim();
            LagerOrt.Werk.Halle.Beschreibung = tbHalleBeschreibung.Text.Trim();
            LagerOrt.Werk.Halle.OrderID = (Int32)nudHalleOrderID.Value;

            if (CheckHallenBezeichnung())
            {
                if (bHalleUpdate)
                {
                    //Update
                    LagerOrt.Werk.Halle.Update();
                }
                else
                {
                    //Insert
                    LagerOrt.Werk.Halle.WerkID = LagerOrt.Werk.ID;
                    LagerOrt.Werk.Halle.ID = 0;
                    LagerOrt.Werk.Halle.Add();
                }
                InitHalle();
                SetHalleEingabeFelderEnabled(false);
                ClearHalleEingabeFelder();
            }
        }
        ///<summary>ctrLagerOrt / CheckHallenBezeichnung</summary>
        ///<remarks></remarks>
        private bool CheckHallenBezeichnung()
        {
            bool EingabeOK = true;
            string strMes = string.Empty;
            LagerOrt.Werk.Halle.Bezeichnung = tbHalleBezeichnung.Text;

            if (!bHalleUpdate)
            {
                //Check auf Bezeichnung nur wenn es sich um einen Neueintrag handelt
                if (LagerOrt.Werk.Halle.ExistHalleByBezeichnung())
                {
                    EingabeOK = false;
                    strMes = strMes + "Hallenbezeichnung existiert bereits \n\r";
                }
            }
            if (tbHalleBezeichnung.Text == string.Empty)
            {
                EingabeOK = false;
                strMes = strMes + "Das Feld Hallenbezeichnung muss gefüllt sein \n\r";
            }
            if (!EingabeOK)
            {
                MessageBox.Show(strMes, "Achtung");
            }
            return EingabeOK;
        }
        ///<summary>ctrLagerOrt / tsbtnHalleClear_Click</summary>
        ///<remarks></remarks>
        private void tsbtnHalleClear_Click(object sender, EventArgs e)
        {
            SetHalleEingabeFelderEnabled(false);
            ClearHalleEingabeFelder();
        }
        ///<summary>ctrLagerOrt / tsbtnHalleDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnHalleDelete_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                decimal decTmp = 0;
                string strTmp = this.dgvHalle.Rows[this.dgvHalle.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
                decimal.TryParse(strTmp, out decTmp);
                LagerOrt.Werk.Halle.ID = decTmp;
                //LagerOrt.Werk.Halle.Init();

                LagerOrt.DeleteLagerOrt("Halle");
                LagerOrt.Werk.Halle.Init();
                LagerOrt.Werk.Halle.UpdateOrderID(0, 0);
                InitHalle();
            }
        }
        ///<summary>ctrLagerOrt / dgvHalle_DoubleClick</summary>
        ///<remarks></remarks>
        private void dgvHalle_DoubleClick(object sender, EventArgs e)
        {
            if (this.dgvHalle.CurrentCell != null)
            {
                bHalleUpdate = true;
                SetHalleEingabeFelderEnabled(true);
                decimal decTmp = 0;
                string strTmp = this.dgvHalle.Rows[this.dgvHalle.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
                decimal.TryParse(strTmp, out decTmp);
                LagerOrt.Werk.Halle.ID = decTmp;
                LagerOrt.Werk.Halle.Init();
                this.dgvHalle.DataSource = LagerOrt.Werk.Halle.dtHalle;
                SetHallenDatenToFrm();
                SetSelectedRowInDGV(ref this.dgvHalle, LagerOrt.Werk.Halle.ID);
                dgvHalle_Click(sender, e);
            }
        }
        ///<summary>ctrLagerOrt / SetWerkDatenToFrm</summary>
        ///<remarks>Setz die Daten in die entsprechenden Eingabefelder.</remarks>
        private void SetHallenDatenToFrm()
        {
            ClearHalleEingabeFelder();
            tbHalleBezeichnung.Text = LagerOrt.Werk.Halle.Bezeichnung;
            tbHalleBeschreibung.Text = LagerOrt.Werk.Halle.Beschreibung;

            if (bHalleUpdate)
            {
                //Max für OrderID muss jetzt immer neu gesetzt werde
                nudHalleOrderID.Maximum = LagerOrt.Werk.maxOrderIDHalle;
            }
            nudHalleOrderID.Value = nudHalleOrderID.Maximum;

            //Baustelle 
            //hier muss entpsrechende aufgezählt werden OrderID
            /**
            if (LagerOrt.Werk.Halle.OrderID > 0)
            {
                nudHalleOrderID.Value = (decimal)LagerOrt.Werk.Halle.OrderID;
            }
            else
            {
                nudHalleOrderID.Value = nudHalleOrderID.Maximum;
            }
             * ***/
        }
        ///<summary>ctrLagerOrt / nudHalleOrderID_ValueChanged</summary>
        ///<remarks>Setz die Daten in die entsprechenden Eingabefelder.</remarks>
        private void nudHalleOrderID_ValueChanged(object sender, EventArgs e)
        {
            if (bHalleUpdate)
            {
                if (LagerOrt.Werk.Halle.ID > 0)
                {
                    Int32 iTmp = (Int32)nudHalleOrderID.Value;
                    if (LagerOrt.Werk.Halle.OrderID != iTmp)
                    {
                        LagerOrt.Werk.Halle.UpdateOrderID(LagerOrt.Werk.Halle.ID, iTmp);
                        LagerOrt.Werk.Halle.Init();
                        this.dgvHalle.DataSource = LagerOrt.Werk.Halle.dtHalle;
                        SetHallenDatenToFrm();
                        SetSelectedRowInDGV(ref dgvHalle, LagerOrt.Werk.Halle.ID);
                    }
                }
            }
        }
        ///<summary>ctrLagerOrt / SelectetHalleIDChanged</summary>
        ///<remarks>Setz bei jedem Click die Werk ID um </remarks>
        private void SelectetHalleIDChanged(decimal myHalleID)
        {
            LagerOrt.Werk.Halle.ID = myHalleID;
            LagerOrt.Werk.Halle.FillDaten();
            LagerOrt.Werk.Halle.Reihe.HalleID = myHalleID;
            InitReihe();
        }
        ///<summary>ctrLagerOrt / dgvHalle_Click</summary>
        ///<remarks> </remarks>
        private void dgvHalle_Click(object sender, EventArgs e)
        {
            iHalleSelectedRow = this.dgvHalle.CurrentCell.RowIndex;
            iReiheSelectedRow = 0;
            iEbeneSelectedRow = 0;
            iPlatzSelectedRow = 0;

            if (this.dgvHalle.CurrentCell != null)
            {
                decimal decTmp = 0;
                string strTmp = this.dgvHalle.Rows[iHalleSelectedRow].Cells["ID"].Value.ToString();
                decimal.TryParse(strTmp, out decTmp);
                SelectetHalleIDChanged(decTmp);
            }
            //DGV Lagerort
            if (bTakeOverLagerort)
            {
                string strSearch = this.dgvHalle.Rows[iHalleSelectedRow].Cells["Bezeichnung"].Value.ToString();
                dgvLagerOrtSearch("Halle", strSearch);
            }
        }
        /************************************************************************************************************
         *                                      Reihe
         * *********************************************************************************************************/
        ///<summary>ctrLagerOrt / InitReihe</summary>
        ///<remarks></remarks>
        private void InitReihe()
        {
            SetReiheEingabeFelderEnabled(false);
            LagerOrt.Werk.Halle.Reihe.HalleID = LagerOrt.Werk.Halle.ID;

            //Clear Eingabefelder Werk
            ClearReiheEingabeFelder();
            //Schritt 2
            nudReiheOrderID.Maximum = (decimal)LagerOrt.Werk.Halle.maxOrderIDReihe;

            LagerOrt.Werk.Halle.Reihe.Init();
            //Datatable for dgvWerk
            this.dgvReihe.DataSource = LagerOrt.Werk.Halle.Reihe.dtReihe;
            this.dgvReihe.Columns["ID"].Visible = false;
            this.dgvReihe.Columns["HalleID"].Visible = false;
            this.dgvReihe.Columns["OrderID"].DisplayIndex = 0;
            this.dgvReihe.AutoResizeColumns();
            this.dgvReihe.Columns["Beschreibung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            SetSelectedRowInDGV(ref this.dgvReihe, LagerOrt.Werk.Halle.Reihe.ID);
            decimal decTmp = 0;
            if (this.dgvReihe.Rows.Count > 0)
            {
                try
                {
                    decTmp = (decimal)this.dgvReihe.Rows[iReiheSelectedRow].Cells["ID"].Value;
                }
                catch
                {
                    iReiheSelectedRow = 0;
                    decTmp = (decimal)this.dgvReihe.Rows[iReiheSelectedRow].Cells["ID"].Value;
                }
            }
            SelectetReiheIDChanged(decTmp);
        }
        ///<summary>ctrLagerOrt / nudReiheOrderID_ValueChanged</summary>
        ///<remarks></remarks>
        private void nudReiheOrderID_ValueChanged(object sender, EventArgs e)
        {
            if (bReiheUpdate)
            {
                if (LagerOrt.Werk.Halle.Reihe.ID > 0)
                {
                    Int32 iTmp = (Int32)nudReiheOrderID.Value;
                    if (LagerOrt.Werk.Halle.OrderID != iTmp)
                    {
                        LagerOrt.Werk.Halle.Reihe.UpdateOrderID(LagerOrt.Werk.Halle.Reihe.ID, iTmp);
                        LagerOrt.Werk.Halle.Reihe.Init();
                        this.dgvReihe.DataSource = LagerOrt.Werk.Halle.Reihe.dtReihe;
                        SetReihenDatenToFrm();
                        SetSelectedRowInDGV(ref this.dgvReihe, LagerOrt.Werk.Halle.Reihe.ID);
                    }
                }
            }
        }
        ///<summary>ctrLagerOrt / dgvReihe_Click</summary>
        ///<remarks></remarks>
        private void dgvReihe_Click(object sender, EventArgs e)
        {
            //iHalleSelectedRow = 0;
            iReiheSelectedRow = this.dgvReihe.CurrentCell.RowIndex;
            iEbeneSelectedRow = 0;
            iPlatzSelectedRow = 0;

            if (this.dgvReihe.CurrentCell != null)
            {
                decimal decTmp = 0;
                string strTmp = this.dgvReihe.Rows[iReiheSelectedRow].Cells["ID"].Value.ToString();
                decimal.TryParse(strTmp, out decTmp);
                SelectetReiheIDChanged(decTmp);
            }
            //DGV Lagerort
            if (bTakeOverLagerort)
            {
                string strSearch = this.dgvReihe.Rows[iReiheSelectedRow].Cells["Bezeichnung"].Value.ToString();
                dgvLagerOrtSearch("Reihe", strSearch);
            }
        }
        ///<summary>ctrLagerOrt / dgvReihe_DoubleClick</summary>
        ///<remarks></remarks>
        private void dgvReihe_DoubleClick(object sender, EventArgs e)
        {
            if (this.dgvReihe.CurrentCell != null)
            {
                bReiheUpdate = true;
                SetReiheEingabeFelderEnabled(true);
                decimal decTmp = 0;
                string strTmp = this.dgvReihe.Rows[this.dgvReihe.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
                decimal.TryParse(strTmp, out decTmp);
                LagerOrt.Werk.Halle.Reihe.ID = decTmp;
                LagerOrt.Werk.Halle.Reihe.Init();
                this.dgvReihe.DataSource = LagerOrt.Werk.Halle.Reihe.dtReihe;
                SetReihenDatenToFrm();
                SetSelectedRowInDGV(ref this.dgvReihe, LagerOrt.Werk.Halle.Reihe.ID);
                dgvReihe_Click(sender, e);
            }
        }
        ///<summary>ctrLagerOrt / tsbtnReiheSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnReiheSave_Click(object sender, EventArgs e)
        {
            //Zuweisung der Klasse
            LagerOrt.Werk.Halle.Reihe.Bezeichnung = tbReiheBezeichnung.Text.Trim();
            LagerOrt.Werk.Halle.Reihe.Beschreibung = tbReiheBeschreibung.Text.Trim();
            LagerOrt.Werk.Halle.Reihe.OrderID = (Int32)nudReiheOrderID.Value;

            if (CheckReihenBezeichnung())
            {
                if (bReiheUpdate)
                {
                    //Update
                    LagerOrt.Werk.Halle.Reihe.Update();
                }
                else
                {
                    //Insert
                    LagerOrt.Werk.Halle.Reihe.HalleID = LagerOrt.Werk.Halle.ID;
                    LagerOrt.Werk.Halle.Reihe.ID = 0;
                    LagerOrt.Werk.Halle.Reihe.Add();
                }
                InitReihe();
                SetReiheEingabeFelderEnabled(false);
                ClearReiheEingabeFelder();
            }
        }
        ///<summary>ctrLagerOrt / tsbtnReiheDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnReiheDelete_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                decimal decTmp = 0;
                string strTmp = this.dgvReihe.Rows[this.dgvReihe.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
                decimal.TryParse(strTmp, out decTmp);
                LagerOrt.Werk.Halle.Reihe.ID = decTmp;
                //LagerOrt.Werk.Halle.Reihe.Init();

                LagerOrt.DeleteLagerOrt("Reihe");
                LagerOrt.Werk.Halle.Reihe.Init();
                LagerOrt.Werk.Halle.Reihe.UpdateOrderID(0, 0);
                InitReihe();
            }
        }
        ///<summary>ctrLagerOrt / tsbtnReiheAdd_Click</summary>
        ///<remarks></remarks>
        private void tsbtnReiheAdd_Click(object sender, EventArgs e)
        {
            bReiheUpdate = false;
            SetReiheEingabeFelderEnabled(true);
            //Clear Werkeingabefelder
            ClearReiheEingabeFelder();
            tbReiheBezeichnung.Focus();

            //OrderID maximum setzen
            //nudReiheOrderID.Maximum = nudReiheOrderID.Maximum + 1;
            //nudReiheOrderID.Value = LagerOrt.Werk.Halle.Reihe.maxOrderID + 1;

            nudReiheOrderID.Maximum = LagerOrt.Werk.Halle.maxOrderIDReihe + 1;
            nudReiheOrderID.Value = nudReiheOrderID.Maximum;

            //angebotene Vorgabe Beschreibung
            tbReiheBeschreibung.Text = tstbHalleWerkInfo.Text + "|" + tstbReiheHallenInfo.Text;
        }
        ///<summary>ctrLagerOrt / tsbtnReiheClear_Click</summary>
        ///<remarks></remarks>
        private void tsbtnReiheClear_Click(object sender, EventArgs e)
        {
            SetReiheEingabeFelderEnabled(false);
            ClearReiheEingabeFelder();
        }
        ///<summary>ctrLagerOrt / SetReiheEingabeFelderEnabled</summary>
        ///<remarks>Aktivieren / Deaktivieren der Eingabefelder.</remarks>
        private void SetReiheEingabeFelderEnabled(bool bEnabled)
        {
            tbReiheBezeichnung.Enabled = bEnabled;
            tbReiheBeschreibung.Enabled = bEnabled;
            nudReiheOrderID.Enabled = bEnabled;
        }
        ///<summary>ctrLagerOrt / SelectetHalleIDChanged</summary>
        ///<remarks>Setz bei jedem Click die Werk ID um </remarks>
        private void SelectetReiheIDChanged(decimal myReiheID)
        {
            LagerOrt.Werk.Halle.Reihe.ID = myReiheID;
            LagerOrt.Werk.Halle.Reihe.FillDaten();
            LagerOrt.Werk.Halle.Reihe.Ebene.ReiheID = myReiheID;
            InitEbene();
        }
        ///<summary>ctrLagerOrt / SetReihnDatenToFrm</summary>
        ///<remarks>Setz die Daten in die entsprechenden Eingabefelder.</remarks>
        private void SetReihenDatenToFrm()
        {
            ClearReiheEingabeFelder();
            tbReiheBezeichnung.Text = LagerOrt.Werk.Halle.Reihe.Bezeichnung;
            tbReiheBeschreibung.Text = LagerOrt.Werk.Halle.Reihe.Beschreibung;


            //Baustelle Reihenfolge 
            if (bReiheUpdate)
            {
                //Max für OrderID muss jetzt immer neu gesetzt werde
                nudReiheOrderID.Maximum = LagerOrt.Werk.Halle.Reihe.maxOrderID;
            }
            /****
            if (LagerOrt.Werk.Halle.Reihe.OrderID > 0)
            {
                nudReiheOrderID.Value = (decimal)LagerOrt.Werk.Halle.Reihe.OrderID;
            }
            else
            {
                nudReiheOrderID.Value = nudReiheOrderID.Maximum;
            }
            ***/
            nudReiheOrderID.Value = nudReiheOrderID.Maximum;
        }

        ///<summary>ctrLagerOrt / CheckHallenBezeichnung</summary>
        ///<remarks></remarks>
        private bool CheckReihenBezeichnung()
        {
            bool EingabeOK = true;
            string strMes = string.Empty;
            LagerOrt.Werk.Halle.Reihe.Bezeichnung = tbReiheBezeichnung.Text;
            if (!bReiheUpdate)
            {
                if (LagerOrt.Werk.Halle.Reihe.ExistReiheByBezeichnung())
                {
                    EingabeOK = false;
                    strMes = strMes + "Reihenbezeichnung existiert bereits \n\r";
                }
            }
            if (tbReiheBezeichnung.Text == string.Empty)
            {
                EingabeOK = false;
                strMes = strMes + "Das Feld Reihenbezeichnung muss gefüllt sein \n\r";
            }

            if (!EingabeOK)
            {
                MessageBox.Show(strMes, "Achtung");
            }
            return EingabeOK;
        }
        ///<summary>ctrLagerOrt / ClearHalleEingabeFelder</summary>
        ///<remarks></remarks>
        private void ClearReiheEingabeFelder()
        {
            tbReiheBeschreibung.Text = string.Empty;
            tbReiheBezeichnung.Text = string.Empty;
            tstbReiheHallenInfo.Text = LagerOrt.Werk.Halle.Bezeichnung;
        }
        /************************************************************************************************************
         *                                           Ebene
         * *********************************************************************************************************/
        ///<summary>ctrLagerOrt / InitEbene</summary>
        ///<remarks></remarks>
        private void InitEbene()
        {
            SetEbeneEingabeFelderEnabled(false);
            LagerOrt.Werk.Halle.Reihe.Ebene.ReiheID = LagerOrt.Werk.Halle.Reihe.ID;
            //Clear Eingabefelder Werk
            ClearEbeneEingabeFelder();
            //ORderID setzen
            nudEbeneOrderID.Maximum = (decimal)LagerOrt.Werk.Halle.Reihe.maxOrderIDEbene;

            LagerOrt.Werk.Halle.Reihe.Ebene.Init();
            //Datatable for dgvWerk
            this.dgvEbene.DataSource = LagerOrt.Werk.Halle.Reihe.Ebene.dtEbene;
            this.dgvEbene.Columns["ID"].Visible = false;
            this.dgvEbene.Columns["ReiheID"].Visible = false;
            this.dgvEbene.Columns["OrderID"].DisplayIndex = 0;
            this.dgvEbene.AutoResizeColumns();
            this.dgvEbene.Columns["Beschreibung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            SetSelectedRowInDGV(ref this.dgvEbene, LagerOrt.Werk.Halle.Reihe.Ebene.ID);
            decimal decTmp = 0;
            if (this.dgvEbene.Rows.Count > 0)
            {
                try
                {
                    decTmp = (decimal)this.dgvEbene.Rows[iEbeneSelectedRow].Cells["ID"].Value;
                }
                catch
                {
                    iEbeneSelectedRow = 0;
                    decTmp = (decimal)this.dgvEbene.Rows[iEbeneSelectedRow].Cells["ID"].Value;
                }
            }
            SelectetEbeneIDChanged(decTmp);
        }
        ///<summary>ctrLagerOrt / tsbtnEbeneAdd_Click</summary>
        ///<remarks></remarks>
        private void tsbtnEbeneAdd_Click(object sender, EventArgs e)
        {
            bEbeneUpdate = false;
            SetEbeneEingabeFelderEnabled(true);
            //Clear Werkeingabefelder
            ClearEbeneEingabeFelder();
            tbEbeneBezeichnung.Focus();
            //OrderID maximum setzen
            // nudEbeneOrderID.Maximum = nudEbeneOrderID.Maximum + 1;
            // nudEbeneOrderID.Value = LagerOrt.Werk.Halle.Reihe.Ebene.maxOrderID + 1;

            nudEbeneOrderID.Maximum = LagerOrt.Werk.Halle.Reihe.maxOrderIDEbene + 1;
            nudEbeneOrderID.Value = nudEbeneOrderID.Maximum;
            //angebotene Vorgabe Beschreibung
            tbEbeneBeschreibung.Text = tstbHalleWerkInfo.Text + "|" +
                                       tstbReiheHallenInfo.Text + "|" +
                                       tstbEbeneReiheInfo.Text;
        }
        ///<summary>ctrLagerOrt / tsbtnEbeneSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnEbeneSave_Click(object sender, EventArgs e)
        {
            //Zuweisung der Klasse
            LagerOrt.Werk.Halle.Reihe.Ebene.Bezeichnung = tbEbeneBezeichnung.Text.Trim();
            LagerOrt.Werk.Halle.Reihe.Ebene.Beschreibung = tbEbeneBeschreibung.Text.Trim();
            LagerOrt.Werk.Halle.Reihe.Ebene.OrderID = (Int32)nudEbeneOrderID.Value;

            if (CheckEbenenBezeichnung())
            {
                if (bEbeneUpdate)
                {
                    //Update
                    LagerOrt.Werk.Halle.Reihe.Ebene.Update();
                }
                else
                {
                    //Insert
                    LagerOrt.Werk.Halle.Reihe.Ebene.ReiheID = LagerOrt.Werk.Halle.Reihe.ID;
                    LagerOrt.Werk.Halle.Reihe.Ebene.ID = 0;
                    LagerOrt.Werk.Halle.Reihe.Ebene.Add();
                }
                InitEbene();
                SetEbeneEingabeFelderEnabled(false);
                ClearEbeneEingabeFelder();
            }
        }
        ///<summary>ctrLagerOrt / tsbtnEbeneClear_Click</summary>
        ///<remarks></remarks>
        private void tsbtnEbeneClear_Click(object sender, EventArgs e)
        {
            SetEbeneEingabeFelderEnabled(false);
            ClearEbeneEingabeFelder();
        }
        ///<summary>ctrLagerOrt / tsbtnEbeneDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnEbeneDelete_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                decimal decTmp = 0;
                string strTmp = this.dgvEbene.Rows[this.dgvEbene.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
                decimal.TryParse(strTmp, out decTmp);
                //LagerOrt.Werk.Halle.Reihe.Ebene.ID = decTmp;
                //LagerOrt.Werk.Halle.Reihe.Ebene.Init();

                LagerOrt.DeleteLagerOrt("Ebene");
                LagerOrt.Werk.Halle.Reihe.Ebene.Init();
                LagerOrt.Werk.Halle.Reihe.Ebene.UpdateOrderID(0, 0);
                InitEbene();
            }
        }
        ///<summary>ctrLagerOrt / nudEbeneOrderID_ValueChanged</summary>
        ///<remarks></remarks>
        private void nudEbeneOrderID_ValueChanged(object sender, EventArgs e)
        {
            if (bEbeneUpdate)
            {
                if (LagerOrt.Werk.Halle.Reihe.Ebene.ID > 0)
                {
                    Int32 iTmp = (Int32)nudEbeneOrderID.Value;
                    if (LagerOrt.Werk.Halle.Reihe.Ebene.OrderID != iTmp)
                    {
                        LagerOrt.Werk.Halle.Reihe.Ebene.UpdateOrderID(LagerOrt.Werk.Halle.Reihe.Ebene.ID, iTmp);
                        LagerOrt.Werk.Halle.Reihe.Ebene.Init();
                        this.dgvEbene.DataSource = LagerOrt.Werk.Halle.Reihe.Ebene.dtEbene;
                        SetEbeneDatenToFrm();
                        SetSelectedRowInDGV(ref this.dgvEbene, LagerOrt.Werk.Halle.Reihe.Ebene.ID);
                    }
                }
            }
        }
        ///<summary>ctrLagerOrt / dgvEbene_Click</summary>
        ///<remarks></remarks>
        private void dgvEbene_Click(object sender, EventArgs e)
        {
            iEbeneSelectedRow = this.dgvEbene.CurrentCell.RowIndex;
            iPlatzSelectedRow = 0;
            if (this.dgvEbene.CurrentCell != null)
            {
                decimal decTmp = 0;
                string strTmp = this.dgvEbene.Rows[iEbeneSelectedRow].Cells["ID"].Value.ToString();
                decimal.TryParse(strTmp, out decTmp);
                SelectetEbeneIDChanged(decTmp);
            }
            if (bTakeOverLagerort)
            {
                string strSearch = this.dgvEbene.Rows[iEbeneSelectedRow].Cells["Bezeichnung"].Value.ToString();
                dgvLagerOrtSearch("Ebene", strSearch);
            }
        }
        ///<summary>ctrLagerOrt / dgvEbene_DoubleClick</summary>
        ///<remarks></remarks>
        private void dgvEbene_DoubleClick(object sender, EventArgs e)
        {
            if (this.dgvEbene.CurrentCell != null)
            {
                bEbeneUpdate = true;
                SetEbeneEingabeFelderEnabled(true);
                decimal decTmp = 0;
                string strTmp = this.dgvEbene.Rows[this.dgvEbene.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
                decimal.TryParse(strTmp, out decTmp);
                LagerOrt.Werk.Halle.Reihe.Ebene.ID = decTmp;
                LagerOrt.Werk.Halle.Reihe.Ebene.Init();
                this.dgvEbene.DataSource = LagerOrt.Werk.Halle.Reihe.Ebene.dtEbene;
                SetEbeneDatenToFrm();
                SetSelectedRowInDGV(ref this.dgvEbene, LagerOrt.Werk.Halle.Reihe.Ebene.ID);
                dgvEbene_Click(sender, e);
            }
        }

        ///<summary>ctrLagerOrt / SetEbeneEingabeFelderEnabled</summary>
        ///<remarks>Aktivieren / Deaktivieren der Eingabefelder.</remarks>
        private void SetEbeneEingabeFelderEnabled(bool bEnabled)
        {
            tbEbeneBezeichnung.Enabled = bEnabled;
            tbEbeneBeschreibung.Enabled = bEnabled;
            nudEbeneOrderID.Enabled = bEnabled;
        }
        ///<summary>ctrLagerOrt / SelectetEbeneIDChanged</summary>
        ///<remarks>Setz bei jedem Click die Werk ID um </remarks>
        private void SelectetEbeneIDChanged(decimal myEbeneID)
        {
            LagerOrt.Werk.Halle.Reihe.Ebene.ID = myEbeneID;
            LagerOrt.Werk.Halle.Reihe.Ebene.FillDaten();
            LagerOrt.Werk.Halle.Reihe.Ebene.Platz.EbeneID = myEbeneID;
            InitPlatz();
        }
        ///<summary>ctrLagerOrt / SetEbeneDatenToFrm</summary>
        ///<remarks>Setz die Daten in die entsprechenden Eingabefelder.</remarks>
        private void SetEbeneDatenToFrm()
        {
            ClearEbeneEingabeFelder();
            tbEbeneBezeichnung.Text = LagerOrt.Werk.Halle.Reihe.Ebene.Bezeichnung;
            tbEbeneBeschreibung.Text = LagerOrt.Werk.Halle.Reihe.Ebene.Beschreibung;

            if (bEbeneUpdate)
            {
                //Max für OrderID muss jetzt immer neu gesetzt werde
                nudEbeneOrderID.Maximum = LagerOrt.Werk.Halle.Reihe.Ebene.maxOrderID;
            }
            if (LagerOrt.Werk.Halle.Reihe.Ebene.OrderID > 0)
            {
                if (LagerOrt.Werk.Halle.Reihe.Ebene.OrderID > nudEbeneOrderID.Maximum)
                {
                    nudEbeneOrderID.Value = nudEbeneOrderID.Maximum;
                }
                else
                {
                    nudEbeneOrderID.Value = (decimal)LagerOrt.Werk.Halle.Reihe.Ebene.OrderID;
                }
            }
            else
            {
                nudEbeneOrderID.Value = nudEbeneOrderID.Maximum;
            }
        }
        ///<summary>ctrLagerOrt / CheckEbenenBezeichnung</summary>
        ///<remarks></remarks>
        private bool CheckEbenenBezeichnung()
        {
            bool EingabeOK = true;
            string strMes = string.Empty;
            LagerOrt.Werk.Halle.Reihe.Ebene.Bezeichnung = tbEbeneBezeichnung.Text;

            if (!bEbeneUpdate)
            {
                //Check nur bei Neueintrag
                if (LagerOrt.Werk.Halle.Reihe.Ebene.ExistEbeneByBezeichnung())
                {
                    EingabeOK = false;
                    strMes = strMes + "Ebenenbezeichnung existiert bereits \n\r";
                }
            }
            if (tbEbeneBezeichnung.Text == string.Empty)
            {
                EingabeOK = false;
                strMes = strMes + "Das Feld Ebenenbezeichnung muss gefüllt sein \n\r";
            }

            if (!EingabeOK)
            {
                MessageBox.Show(strMes, "Achtung");
            }
            return EingabeOK;
        }
        ///<summary>ctrLagerOrt / ClearEbeneEingabeFelder</summary>
        ///<remarks></remarks>
        private void ClearEbeneEingabeFelder()
        {
            tbEbeneBeschreibung.Text = string.Empty;
            tbEbeneBezeichnung.Text = string.Empty;
            tstbEbeneReiheInfo.Text = LagerOrt.Werk.Halle.Reihe.Bezeichnung;
        }

        /************************************************************************************************************
        *                                           Platz
        * *********************************************************************************************************/
        ///<summary>ctrLagerOrt / InitEbene</summary>
        ///<remarks></remarks>
        private void InitPlatz()
        {
            SetPlatzEingabeFelderEnabled(false);
            LagerOrt.Werk.Halle.Reihe.Ebene.Platz.EbeneID = LagerOrt.Werk.Halle.Reihe.Ebene.ID;
            //Clear Eingabefelder Werk
            ClearPlatzEingabeFelder();
            //ORderID setzen
            nudPlatzOrderID.Maximum = (decimal)LagerOrt.Werk.Halle.Reihe.Ebene.maxOrderIDPlatz;

            //Datatable for dgvWerk
            LagerOrt.Werk.Halle.Reihe.Ebene.Platz.Init();
            this.dgvPlatz.DataSource = LagerOrt.Werk.Halle.Reihe.Ebene.Platz.dtPlatz;
            this.dgvPlatz.Columns["ID"].Visible = false;
            this.dgvPlatz.Columns["EbeneID"].Visible = false;
            this.dgvPlatz.Columns["GArt"].Visible = false;
            this.dgvPlatz.Columns["vGewicht"].Visible = false;
            this.dgvPlatz.Columns["bGewicht"].Visible = false;
            this.dgvPlatz.Columns["OrderID"].DisplayIndex = 0;
            this.dgvPlatz.AutoResizeColumns();
            this.dgvPlatz.Columns["Beschreibung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            SetSelectedRowInDGV(ref this.dgvPlatz, LagerOrt.Werk.Halle.Reihe.Ebene.Platz.ID);
            decimal decTmp = 0;
            if (this.dgvPlatz.Rows.Count > 0)
            {
                try
                {
                    decTmp = (decimal)this.dgvPlatz.Rows[iPlatzSelectedRow].Cells["ID"].Value;
                }
                catch
                {
                    iPlatzSelectedRow = 0;
                    decTmp = (decimal)this.dgvPlatz.Rows[iPlatzSelectedRow].Cells["ID"].Value;
                }
            }
            SelectetPlatzIDChanged(decTmp);
        }
        ///<summary>ctrLagerOrt / tsbtnPlatzAdd_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPlatzAdd_Click(object sender, EventArgs e)
        {
            bPlatzUpdate = false;
            SetPlatzEingabeFelderEnabled(true);
            //Clear Werkeingabefelder
            ClearPlatzEingabeFelder();
            tbPlatzBezeichnung.Focus();
            //OrderID maximum setzen
            //nudPlatzOrderID.Maximum = nudPlatzOrderID.Maximum + 1;
            //nudPlatzOrderID.Value = LagerOrt.Werk.Halle.Reihe.Ebene.Platz.maxOrderID + 1;

            nudPlatzOrderID.Maximum = LagerOrt.Werk.Halle.Reihe.Ebene.maxOrderIDPlatz + 1;
            nudPlatzOrderID.Value = nudPlatzOrderID.Maximum;

            //angebotene Vorgabe Beschreibung
            tbPlatzBeschreibung.Text = tstbHalleWerkInfo.Text + "|" +
                                       tstbReiheHallenInfo.Text + "|" +
                                       tstbEbeneReiheInfo.Text + "|" +
                                       tstbPlatzEbene.Text;
        }
        ///<summary>ctrLagerOrt / tsbtnPlatzSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPlatzSave_Click(object sender, EventArgs e)
        {
            //Zuweisung der Klasse
            LagerOrt.Werk.Halle.Reihe.Ebene.Platz.Bezeichnung = tbPlatzBezeichnung.Text.Trim();
            LagerOrt.Werk.Halle.Reihe.Ebene.Platz.Beschreibung = tbPlatzBeschreibung.Text.Trim();
            LagerOrt.Werk.Halle.Reihe.Ebene.Platz.OrderID = (Int32)nudPlatzOrderID.Value;

            if (CheckPlatzBezeichnung())
            {
                if (bPlatzUpdate)
                {
                    //Update
                    LagerOrt.Werk.Halle.Reihe.Ebene.Platz.Update();
                }
                else
                {
                    //Insert
                    LagerOrt.Werk.Halle.Reihe.Ebene.Platz.EbeneID = LagerOrt.Werk.Halle.Reihe.Ebene.ID;
                    LagerOrt.Werk.Halle.Reihe.Ebene.Platz.ID = 0;
                    LagerOrt.Werk.Halle.Reihe.Ebene.Platz.Add();
                }
                InitPlatz();
                SetPlatzEingabeFelderEnabled(false);
                ClearPlatzEingabeFelder();
            }
        }
        ///<summary>ctrLagerOrt / tsbtnPlatzClear_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPlatzClear_Click(object sender, EventArgs e)
        {
            SetPlatzEingabeFelderEnabled(false);
            ClearPlatzEingabeFelder();
        }
        ///<summary>ctrLagerOrt / tsbtnPlatzDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPlatzDelete_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                decimal decTmp = 0;
                string strTmp = this.dgvPlatz.Rows[this.dgvPlatz.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
                decimal.TryParse(strTmp, out decTmp);
                //LagerOrt.Werk.Halle.Reihe.Ebene.Platz.ID = decTmp;
                //LagerOrt.Werk.Halle.Reihe.Ebene.Platz.Init();

                LagerOrt.DeleteLagerOrt("Platz");
                LagerOrt.Werk.Halle.Reihe.Ebene.Platz.Init();
                LagerOrt.Werk.Halle.Reihe.Ebene.Platz.UpdateOrderID(0, 0);
                InitPlatz();
            }
        }
        ///<summary>ctrLagerOrt / dgvPlatz_Click</summary>
        ///<remarks></remarks>
        private void dgvPlatz_Click(object sender, EventArgs e)
        {
            iPlatzSelectedRow = this.dgvPlatz.CurrentCell.RowIndex;
            if (this.dgvPlatz.CurrentCell != null)
            {

                decimal decTmp = 0;
                string strTmp = this.dgvPlatz.Rows[iPlatzSelectedRow].Cells["ID"].Value.ToString();
                decimal.TryParse(strTmp, out decTmp);
                SelectetPlatzIDChanged(decTmp);
            }
            //DGV Lagerort
            if (bTakeOverLagerort)
            {
                string strSearch = this.dgvPlatz.Rows[iReiheSelectedRow].Cells["Bezeichnung"].Value.ToString();
                dgvLagerOrtSearch("Platz", strSearch);
            }
        }
        ///<summary>ctrLagerOrt / dgvPlatz_DoubleClick</summary>
        ///<remarks></remarks>
        private void dgvPlatz_DoubleClick(object sender, EventArgs e)
        {
            if (this.dgvPlatz.CurrentCell != null)
            {
                bPlatzUpdate = true;
                SetPlatzEingabeFelderEnabled(true);
                decimal decTmp = 0;
                string strTmp = this.dgvPlatz.Rows[this.dgvPlatz.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
                decimal.TryParse(strTmp, out decTmp);
                LagerOrt.Werk.Halle.Reihe.Ebene.Platz.ID = decTmp;
                LagerOrt.Werk.Halle.Reihe.Ebene.Platz.Init();
                this.dgvPlatz.DataSource = LagerOrt.Werk.Halle.Reihe.Ebene.Platz.dtPlatz;
                SetPlatzDatenToFrm();
                SetSelectedRowInDGV(ref this.dgvPlatz, LagerOrt.Werk.Halle.Reihe.Ebene.Platz.ID);
                dgvPlatz_Click(sender, e);
            }
        }
        ///<summary>ctrLagerOrt / nudPlatzOrderID_ValueChanged</summary>
        ///<remarks></remarks>
        private void nudPlatzOrderID_ValueChanged(object sender, EventArgs e)
        {
            if (bPlatzUpdate)
            {
                if (LagerOrt.Werk.Halle.Reihe.Ebene.Platz.ID > 0)
                {
                    Int32 iTmp = (Int32)nudPlatzOrderID.Value;
                    if (LagerOrt.Werk.Halle.Reihe.Ebene.Platz.OrderID != iTmp)
                    {
                        LagerOrt.Werk.Halle.Reihe.Ebene.Platz.UpdateOrderID(LagerOrt.Werk.Halle.Reihe.Ebene.Platz.ID, iTmp);
                        LagerOrt.Werk.Halle.Reihe.Ebene.Platz.Init();
                        this.dgvPlatz.DataSource = LagerOrt.Werk.Halle.Reihe.Ebene.Platz.dtPlatz;
                        SetPlatzDatenToFrm();
                        SetSelectedRowInDGV(ref this.dgvPlatz, LagerOrt.Werk.Halle.Reihe.Ebene.Platz.ID);
                    }
                }
            }
        }
        ///<summary>ctrLagerOrt / SetPlatzEingabeFelderEnabled</summary>
        ///<remarks>Aktivieren / Deaktivieren der Eingabefelder.</remarks>
        private void SetPlatzEingabeFelderEnabled(bool bEnabled)
        {
            tbPlatzBezeichnung.Enabled = bEnabled;
            tbPlatzBeschreibung.Enabled = bEnabled;
            nudPlatzOrderID.Enabled = bEnabled;
        }
        ///<summary>ctrLagerOrt / SelectetPlatzIDChanged</summary>
        ///<remarks>Setz bei jedem Click die Werk ID um </remarks>
        private void SelectetPlatzIDChanged(decimal myPlatzID)
        {
            LagerOrt.Werk.Halle.Reihe.Ebene.Platz.ID = myPlatzID;
            LagerOrt.Werk.Halle.Reihe.Ebene.Platz.FillDaten();
        }
        ///<summary>ctrLagerOrt / SetPlatzDatenToFrm</summary>
        ///<remarks>Setz die Daten in die entsprechenden Eingabefelder.</remarks>
        private void SetPlatzDatenToFrm()
        {
            ClearPlatzEingabeFelder();
            tbPlatzBezeichnung.Text = LagerOrt.Werk.Halle.Reihe.Ebene.Platz.Bezeichnung;
            tbPlatzBeschreibung.Text = LagerOrt.Werk.Halle.Reihe.Ebene.Platz.Beschreibung;

            if (bPlatzUpdate)
            {
                //Max für OrderID muss jetzt immer neu gesetzt werde
                nudPlatzOrderID.Maximum = LagerOrt.Werk.Halle.Reihe.Ebene.Platz.maxOrderID;
            }
            if (LagerOrt.Werk.Halle.Reihe.Ebene.Platz.OrderID > 0)
            {
                if (LagerOrt.Werk.Halle.Reihe.Ebene.Platz.OrderID > nudPlatzOrderID.Maximum)
                {
                    nudPlatzOrderID.Value = nudPlatzOrderID.Maximum;
                }
                else
                {
                    nudPlatzOrderID.Value = (decimal)LagerOrt.Werk.Halle.Reihe.Ebene.Platz.OrderID;
                }
            }
            else
            {
                nudPlatzOrderID.Value = nudPlatzOrderID.Maximum;
            }
        }
        ///<summary>ctrLagerOrt / CheckEbenenBezeichnung</summary>
        ///<remarks></remarks>
        private bool CheckPlatzBezeichnung()
        {
            bool EingabeOK = true;
            string strMes = string.Empty;
            LagerOrt.Werk.Halle.Reihe.Ebene.Platz.Bezeichnung = tbPlatzBezeichnung.Text;
            if (!bPlatzUpdate)
            {
                if (LagerOrt.Werk.Halle.Reihe.Ebene.Platz.ExistPlatzByBezeichnung())
                {
                    EingabeOK = false;
                    strMes = strMes + "Platzbezeichnung existiert bereits \n\r";
                }
            }
            if (tbPlatzBezeichnung.Text == string.Empty)
            {
                EingabeOK = false;
                strMes = strMes + "Das Feld Platzbezeichnung muss gefüllt sein \n\r";
            }

            if (!EingabeOK)
            {
                MessageBox.Show(strMes, "Achtung");
            }
            return EingabeOK;
        }
        ///<summary>ctrLagerOrt / ClearEbeneEingabeFelder</summary>
        ///<remarks></remarks>
        private void ClearPlatzEingabeFelder()
        {
            tbPlatzBeschreibung.Text = string.Empty;
            tbPlatzBezeichnung.Text = string.Empty;
            tstbPlatzEbene.Text = LagerOrt.Werk.Halle.Reihe.Ebene.Bezeichnung;
        }
        /******************************************************************************************************
         *                                    Datagrid Lagerort / Lagerplatz 
         * ***************************************************************************************************/
        ///<summary>ctrLagerOrt / SetSelectedRowInDGV</summary>
        ///<remarks></remarks>
        private void LoadDGVLagerort()
        {
            dtLagerOrt.Clear();
            bool bExLagerOrt = false;
            //Baustelle diese Unterscheidung kann wohl raus
            if (this._ctrEinlagerung != null)
            {
                //bExLagerOrt = this._ctrEinlagerung.bIsExternerLagerOrt;
            }
            if (LagerOrt != null)
            {
                dtLagerOrt = LagerOrt.GetLagerortDataTable(tscbLagerOrt.ComboBox.Text);
                this.dgvLagerort.DataSource = dtLagerOrt;
                if (dgvLagerort.Rows.Count > 0)
                {
                    this.dgvLagerort.Columns["TableID"].Visible = false;
                    this.dgvLagerort.Columns["LVSNr"].Visible = false;
                    this.dgvLagerort.Columns["AbBereich"].Visible = false;
                }
            }
        }
        ///<summary>ctrLagerOrt / tsbtnLagerortDGVRefesh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnLagerortDGVRefesh_Click(object sender, EventArgs e)
        {
            LoadDGVLagerort();
        }
        ///<summary>ctrLagerOrt / toolStripButton1_Click</summary>
        ///<remarks>Übernahme des Lagerorts in eine anderes CTR</remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Einlagerungsform != null
            if (this._ctrEinlagerung != null)
            {
                //Lagerort muss ausgewählt sein
                if (tstbLagerplatzSelect.Text != string.Empty)
                {
                    //Übernahme des Lagerorts in die Form Einlagerung
                    this._ctrEinlagerung.TakeOverLagerOrt(LagerOrt.LagerPlatzID, tscbLagerOrt.ComboBox.Text);
                    CloseCtr();
                }
            }
        }
        ///<summary>ctrLagerOrt / tsbtnLagerortClear_Click</summary>
        ///<remarks>Leert den Lagerort</remarks>
        private void tsbtnLagerortClear_Click(object sender, EventArgs e)
        {
            tstbLagerplatzSelect.Text = string.Empty;
        }
        ///<summary>ctrLagerOrt / dgvLagerort_DoubleClick</summary>
        ///<remarks>Der gewählte Lagerort wird in die Anzeige im Menü übernommen. Es gibt zwei Möglichkeiten einen Lagerplatz zuweisen.
        ///         1. Der fest definierte Lagerplatz, wobei jeder Plazt einzeln definiert werden muss. Dieser Platz ist dann auch einer
        ///            LVSNR zugeordnet. Diese Anzahl ist endlich.</remarks>
        private void dgvLagerort_DoubleClick(object sender, EventArgs e)
        {
            if (this.dgvLagerort.Rows.Count > 0)
            {
                decimal decTmp = 0;
                string strTmp = string.Empty;
                string strLVSCheck = string.Empty;

                //Unterscheidung Lagerpaltz endlich oder unendlich
                if (
                    (tscbLagerOrt.ComboBox.Text == enumLagerOrtTable.Platz.ToString())
                    //||
                    //(tscbLagerOrt.ComboBox.Text == Globals.enumLagerOrtTable.exLagerort.ToString())
                    )
                {
                    strLVSCheck = this.dgvLagerort.Rows[this.dgvLagerort.CurrentCell.RowIndex].Cells["LVSNr"].Value.ToString();
                    strTmp = this.dgvLagerort.Rows[this.dgvLagerort.CurrentCell.RowIndex].Cells["TableID"].Value.ToString();
                    if (Decimal.TryParse(strTmp, out decTmp) && (strLVSCheck == string.Empty))
                    {
                        LagerOrt.LagerPlatzID = decTmp;
                        LagerOrt.LOTable = tscbLagerOrt.ComboBox.Text;
                        LagerOrt.InitLagerPlatz();
                        tstbLagerplatzSelect.Text = LagerOrt.LagerPlatzBezeichungKomplett;
                    }
                    else
                    {
                        tstbLagerplatzSelect.Text = string.Empty;
                    }
                }
                else
                {
                    strTmp = this.dgvLagerort.Rows[this.dgvLagerort.CurrentCell.RowIndex].Cells["TableID"].Value.ToString();
                    if (Decimal.TryParse(strTmp, out decTmp))// && (strLVSCheck == string.Empty))
                    {
                        LagerOrt.LagerPlatzID = decTmp;
                        LagerOrt.LOTable = tscbLagerOrt.ComboBox.Text;
                        LagerOrt.InitLagerPlatz();
                        tstbLagerplatzSelect.Text = LagerOrt.LagerPlatzBezeichungKomplett;
                    }
                    else
                    {
                        tstbLagerplatzSelect.Text = string.Empty;
                    }
                }
            }
        }
        ///<summary>ctrLagerOrt / dgvLagerort_CellFormatting</summary>
        ///<remarks>Datensätze mit LVSNr (belegte Lagerplätze) werden grau hinterlegt</remarks>
        private void dgvLagerort_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                if (e.ColumnIndex == 0)
                {
                    if (
                        (tscbLagerOrt.ComboBox.Text == enumLagerOrtTable.Platz.ToString())
                       )
                    {
                        foreach (DataGridViewRow row in this.dgvLagerort.Rows)
                        {
                            string strTmp = row.Cells["LVSNr"].Value.ToString();

                            if (strTmp != string.Empty)
                            {
                                row.DefaultCellStyle.BackColor = Color.LightGray;
                                row.DefaultCellStyle.ForeColor = Color.Gray;
                                string strToolTipText = String.Format("{0}\t{1}", "Arbeitsbereich: ", row.Cells["AbBereich"].Value.ToString()) + Environment.NewLine +
                                                        String.Format("{0}\t{1}", "LVSNr: ", row.Cells["LVSNr"].Value.ToString());

                                for (Int32 j = 0; j <= this.dgvLagerort.Columns.Count - 1; j++)
                                {
                                    row.Cells[j].ToolTipText = strToolTipText;
                                }
                            }
                            else
                            {
                                row.DefaultCellStyle.BackColor = Color.White;
                                row.DefaultCellStyle.ForeColor = Color.Black;
                            }
                        }
                    }
                }
            }

        }
        ///<summary>ctrLagerOrt / tstbLagerplatzSelect_TextChanged</summary>
        ///<remarks>Sobald ein Lagerplatz augewählt wurde, wird der TakeOverButton aktiviert</remarks>
        private void tstbLagerplatzSelect_TextChanged(object sender, EventArgs e)
        {
            if (tstbLagerplatzSelect.Text == string.Empty)
            {
                tsbtnLagerortTakeOver.Enabled = false;
            }
            else
            {
                tsbtnLagerortTakeOver.Enabled = true;
            }
        }



























    }
}
