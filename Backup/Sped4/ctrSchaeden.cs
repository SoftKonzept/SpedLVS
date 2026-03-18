using LVS;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace Sped4
{
    public partial class ctrSchaeden : UserControl
    {
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;
        public frmTmp _frmTmp;
        internal clsSchaeden Schaden = new clsSchaeden();
        internal DataTable dtSchaden = new DataTable("Schaeden");
        public decimal _ArtikelID = 0;
        public bool _IsSchadensAuswahlAktion = false;
        public ctrEinlagerung _ctrEinlagerung;
        const string const_FileName = "_Schäden";
        const string const_Headline = "Schäden / Mängel";
        public const string const_ControlName = "Bestandsliste";
        public Int32 iListToShow = 0;
        internal bool bUpdate = false;

        /************************************************************************
         *                  Proceduren / Methoden
         * **********************************************************************/
        ///<summary>ctrSchaeden / ctrSchaeden</summary>
        ///<remarks></remarks>
        public ctrSchaeden()
        {
            InitializeComponent();
            iListToShow = clsSchaeden.const_Art_SchadenUndMängel;
        }
        ///<summary>ctrSchaeden / ctrSchaeden_Load</summary>
        ///<remarks></remarks>
        private void ctrSchaeden_Load(object sender, EventArgs e)
        {
            //iListToShow = clsSchaeden.const_Art_SchadenUndMängel;
            comboArt.DataSource = clsSchaeden.DictSchadensArt().ToList();
            comboArt.DisplayMember = "Value";
            comboArt.ValueMember = "Key";

            InitDGV(iListToShow);
            if (_IsSchadensAuswahlAktion)
            {
                //_ArtikelID = _ctrEinlagerung.Lager.Artikel.ID;
                _ArtikelID = _ctrEinlagerung.Lager.Artikel.ID;
                this.tsmSchadenSelect.Visible = true;
            }
            else
            {
                this.tsmSchadenSelect.Visible = false;
            }
        }
        ///<summary>ctrSchaeden / ClearInputFields</summary>
        ///<remarks></remarks>
        private void SetHeadLine(Int32 iSwitchHeadLine)
        {
            string strZusatz = string.Empty;
            switch (iSwitchHeadLine)
            {
                //Alle
                case clsSchaeden.const_Art_SchadenUndMängel:
                    break;
                //Active
                case clsSchaeden.const_Art_Active:
                    strZusatz = " - [aktive Schäden / Mängel]";
                    break;
                //Passive
                case clsSchaeden.const_Art_Passiv:
                    strZusatz = " - [passive Schäden / Mängel]";
                    break;
                //nur Schäden
                case clsSchaeden.const_Art_Schaden:
                    strZusatz = " - [nur Schäden]";
                    break;
                //nur Mängel
                case clsSchaeden.const_Art_Mangel:
                    strZusatz = " - [nur Mängel]";
                    break;
            }
            afColorLabel1.myText = const_Headline + strZusatz;
        }
        ///<summary>ctrSchaeden / ClearInputFields</summary>
        ///<remarks></remarks>
        private void ClearInputFields()
        {
            tbBeschreibung.Text = string.Empty;
            tbBezeichnung.Text = string.Empty;
            cbActiv.Checked = false;
            cbAutoSPL.Checked = false;
            comboArt.SelectedIndex = 0;
            tbCode.Text = string.Empty;
        }
        ///<summary>ctrSchaeden / SetEingabefelderEnabled</summary>
        ///<remarks></remarks>
        private void SetEingabefelderEnabled(bool bEnabled)
        {
            tbBeschreibung.Enabled = bEnabled;
            tbBezeichnung.Enabled = bEnabled;
            cbActiv.Enabled = bEnabled;
            comboArt.Enabled = bEnabled;
            cbAutoSPL.Enabled = bEnabled;
            tbCode.Enabled = bEnabled;
        }
        ///<summary>ctrSchaeden / InitDGV</summary>
        ///<remarks></remarks>
        private void InitDGV(Int32 iShowList)
        {
            dtSchaden.Clear();
            dtSchaden = clsSchaeden.GetSchaeden(this.GL_User, iShowList);
            this.dgv.DataSource = dtSchaden.DefaultView;
            for (Int32 i = 0; i <= this.dgv.Columns.Count - 1; i++)
            {
                string strColName = this.dgv.Columns[i].HeaderText.ToString();
                this.dgv.Columns[i].IsVisible = clsClient.ctrSchaeden_CustomizeDGVView(this._ctrMenu._frmMain.system.Client.MatchCode, strColName);
                switch (strColName)
                {
                    case "Select":
                        if (this._ctrEinlagerung != null)
                        {
                            this.dgv.Columns[i].IsVisible = true;
                            this.dgv.Columns[i].BestFit();
                            this.dgv.Columns.Move(this.dgv.Columns[i].Index, 0);
                        }
                        else
                        {
                            this.dgv.Columns[i].IsVisible = false;
                        }
                        break;
                    case "Bezeichnung":
                        this.dgv.Columns[i].WrapText = true;
                        this.dgv.Columns[i].BestFit();
                        break;
                    case "Beschreibung":
                        this.dgv.Columns[i].WrapText = true;
                        //this.dgv.Columns[i].Width=this.dgv.Width-(this.dgv.Columns["Bezeichnung"].Width-15);
                        this.dgv.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells;
                        break;
                    case "Code":
                        this.dgv.Columns[i].WrapText = true;
                        this.dgv.Columns[i].BestFit();
                        break;
                    case "AutoSPL":
                        if (_IsSchadensAuswahlAktion)
                        {
                            this.dgv.Columns[i].BestFit();
                        }
                        else
                        {
                            //this.dgv.Columns[i].IsVisible = false;
                        }
                        break;
                    //Spalten ausblenden
                    default:
                        //this.dgv.Columns[i].IsVisible = false;
                        break;
                }
            }
            this.dgv.BestFitColumns();
            SetHeadLine(iShowList);
        }
        ///<summary>ctrSchaeden / tsbtnClose_Click</summary>
        ///<remarks>Ctr schliessen</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            if (this._frmTmp != null)
            {
                this._frmTmp.CloseFrmTmp();
            }
            else
            {
                this._ctrMenu.CloseCtrSchaden();
            }
        }
        ///<summary>ctrSchaeden / tsbtnClear_Click</summary>
        ///<remarks>Eingabefelder leeren</remarks>
        private void tsbtnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            SetEingabefelderEnabled(true);
        }
        ///<summary>ctrSchaeden / tsbtnSave_Click</summary>
        ///<remarks>Schaden speichern</remarks>
        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            if (tbBezeichnung.Text.Trim() != string.Empty)
            {
                clsSchaeden TmpSchaden = new clsSchaeden();
                TmpSchaden = Schaden;

                Schaden = new clsSchaeden();
                Schaden._GL_User = this.GL_User;
                Schaden.Bezeichnung = tbBezeichnung.Text.Trim();
                Schaden.Beschreibung = tbBeschreibung.Text.Trim();
                Schaden.aktiv = cbActiv.Checked;
                Int32 iTmp = 0;
                Int32.TryParse(comboArt.SelectedValue.ToString(), out iTmp);
                Schaden.Art = iTmp;
                Schaden.AutoSPL = cbAutoSPL.Checked;
                Schaden.Code = tbCode.Text.Trim();

                if (bUpdate)
                {
                    Schaden.ID = TmpSchaden.ID;
                    Schaden.UpdateSchaden();
                }
                else
                {
                    Schaden.AddSchaden();
                }
                ClearInputFields();
                SetEingabefelderEnabled(false);
                InitDGV(iListToShow);
            }
        }
        ///<summary>ctrSchaeden / dgv_MouseClick</summary>
        ///<remarks></remarks>
        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                /***
                decimal decTmpID = 0;
                Decimal.TryParse(this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["ID"].Value.ToString(), out decTmpID);
                if (decTmpID > 0)
                {
                    Schaden = new clsSchaeden();
                    Schaden._GL_User = this.GL_User;
                    Schaden.ID = decTmpID;
                    Schaden.FillByID();
                }
                 * ***/
            }
        }
        ///<summary>ctrSchaeden / tsbtnDelete_Click</summary>
        ///<remarks>Ein Schaden der noch nicht verwendet wird kann gelöscht werden.</remarks>
        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            //Check 
            if (!Schaden.SchadenInUse())
            {
                Schaden.DeleteSchaden();
                ClearInputFields();
                InitDGV(iListToShow);
                bUpdate = false;
                SetEingabefelderEnabled(false);
            }
            else
            {
                clsMessages.Schaeden_DeletDenied();
            }
        }
        ///<summary>ctrSchaeden / tsbtnSchadenSelectClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSchadenSelectClose_Click(object sender, EventArgs e)
        {
            if (this._frmTmp != null)
            {
                this._frmTmp.CloseFrmTmp();
            }
            else
            {
                this._ctrMenu.CloseCtrSchaden();
            }
        }
        ///<summary>ctrSchaeden / tsbtnSchadenSelect_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSchadenSelect_Click(object sender, EventArgs e)
        {
            Schaden.ArtikelID = _ArtikelID;
            Schaden.AddSchadenZuweisung(this.dtSchaden);
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_AutoSPL)
            {
                //prüfen, ob ein Schaden mit AutoSPL ausgewählt wurde
                this.dtSchaden.DefaultView.RowFilter = "Select=1 AND AutoSPL=1";
                DataTable dt = this.dtSchaden.DefaultView.ToTable();
                this.dtSchaden.DefaultView.RowFilter = string.Empty;
                if (dt.Rows.Count > 0)
                {
                    if (!this._ctrEinlagerung.Lager.Artikel.bSPL)
                    {
                        this._ctrEinlagerung.Lager.SPL._GL_User = this.GL_User;
                        this._ctrEinlagerung.Lager.SPL.ArtikelID = Schaden.ArtikelID;
                        this._ctrEinlagerung.Lager.SPL.Add(true);
                        clsMessages.Sperrlager_addAuto();
                    }
                }
            }
            this._ctrEinlagerung.InitDGVSchaden();
            //this._ctrEinlagerung.SetArtikelToForm(Schaden.ArtikelID, false);
            this._ctrEinlagerung.EingangBrowse(0, Schaden.ArtikelID, enumBrowseAcivity.ArtInItem);

            this._ctrEinlagerung.tabArtikel.SelectedTab = this._ctrEinlagerung.tabPageSchaden;
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_Schaeden_ShowPrintCenterAfterSelection)
            {
                this._ctrEinlagerung._bArtPrint = true;
                this._ctrEinlagerung.Lager.Eingang.FillEingang();
                this._ctrMenu.OpenCtrPrintLagerInFrm(this._ctrEinlagerung);
            }
            this._frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrSchaeden / tsbtnSchadenSelect_Click</summary>
        ///<remarks></remarks>
        private void dgv_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (this.dgv.Rows[e.RowIndex] != null)
                {
                    bool bCell = (bool)this.dgv.Rows[e.RowIndex].Cells["Select"].Value;
                    this.dgv.Rows[e.RowIndex].Cells["Select"].Value = (!bCell);

                    decimal decTmpID = 0;
                    Decimal.TryParse(this.dgv.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out decTmpID);
                    if (decTmpID > 0)
                    {
                        Schaden = new clsSchaeden();
                        Schaden._GL_User = this.GL_User;
                        Schaden.ID = decTmpID;
                        Schaden.FillByID();
                    }
                }
            }
        }
        ///<summary>ctrSchaeden / dgv_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgv_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (!_IsSchadensAuswahlAktion)
            {
                if (this.dgv.Rows.Count > 0)
                {
                    ClearInputFields();
                    tbBezeichnung.Text = Schaden.Bezeichnung;
                    tbBeschreibung.Text = Schaden.Beschreibung;
                    cbActiv.Checked = Schaden.aktiv;
                    cbAutoSPL.Checked = Schaden.AutoSPL;
                    tbCode.Text = Schaden.Code;

                    tsbtnDelete.Enabled = (!Schaden.SchadenInUse());
                    bUpdate = true;
                    SetEingabefelderEnabled(true);
                }
            }
        }
        ///<summary>ctrSchaeden / tsbtnAdd_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAdd_Click(object sender, EventArgs e)
        {
            bUpdate = false;
            ClearInputFields();
            SetEingabefelderEnabled(true);
        }
        ///<summary>ctrSchaeden / miActive_Click</summary>
        ///<remarks></remarks>
        private void miActive_Click(object sender, EventArgs e)
        {
            iListToShow = clsSchaeden.const_Art_Active;
            InitDGV(iListToShow);
        }
        ///<summary>ctrSchaeden / miPassive_Click</summary>
        ///<remarks></remarks>
        private void miPassive_Click(object sender, EventArgs e)
        {
            iListToShow = clsSchaeden.const_Art_Passiv;
            InitDGV(iListToShow);
        }
        ///<summary>ctrSchaeden / miSchaden_Click</summary>
        ///<remarks></remarks>
        private void miSchaden_Click(object sender, EventArgs e)
        {
            iListToShow = clsSchaeden.const_Art_Schaden;
            InitDGV(iListToShow);
        }
        ///<summary>ctrSchaeden / miMangel_Click</summary>
        ///<remarks></remarks>
        private void miMangel_Click(object sender, EventArgs e)
        {
            iListToShow = clsSchaeden.const_Art_Mangel;
            InitDGV(iListToShow);
        }
        ///<summary>ctrSchaeden / alleSchädenMängelToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void alleSchädenMängelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iListToShow = clsSchaeden.const_Art_SchadenUndMängel;
            InitDGV(iListToShow);
        }
        ///<summary>ctrSchaeden / tsbtnRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitDGV(iListToShow);
        }





    }
}
