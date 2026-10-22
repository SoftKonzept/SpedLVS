using LVS;
using LVS.ViewData;
using Sped4.Classes;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;

namespace Sped4
{
    public partial class ctrReihen : UserControl
    {
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;
        public delegate void IDTakeOverEventHandler(decimal TakeOverID);
        public event IDTakeOverEventHandler getIDTakeOver;

        public delegate void frmGArtenAuftragsErfassungEventHandler();
        public event frmGArtenAuftragsErfassungEventHandler ClosefrmGArtenAuftragserfassung;

        public bool bUpdateGArtDaten = false;
        internal bool bUpdateStyleSheet = false;
        internal Int32 iListWidth = 0;
        internal Int32 iTabGArtEditWidth = 0;
        internal Int32 MenuItemListArt = 0;
        internal string GArtFilter = string.Empty;
        internal string FileExportName = string.Empty;

        public bool SearchMatchcode = false;
        public bool SearchGArt = false;//Standardsuche

        public DataTable dtGArt = new DataTable();
        public DataTable tempTable = new DataTable();


        internal clsLagerOrt LagerOrt;
        internal bool bReiheUpdate = false;

        internal string _lTextGArtID = "Güterart ID: #";
        internal ReiheViewData ReiheVD = new ReiheViewData();


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        ///<summary>ctrGueterArtListe / ctrGueterArtListe</summary>
        ///<remarks></remarks>
        public ctrReihen()
        {
            InitializeComponent();

            //Menü für die Güterarten nach den ARbeitsbereichen erstellen
            RadGridLocalizationProvider.CurrentProvider = new clsGermanRadGridLocalizationProvider();
            bUpdateGArtDaten = false;

            //Width
            iListWidth = this.scGArt.Panel1.Width + 80;
            iTabGArtEditWidth = this.scGArt.Panel2.Width + 20;
            this.scGArt.Panel2Collapsed = true;
            //ResetCtrGueterArtListeWidth();

            if (SearchGArt == true)
            {
                cbGArt.Checked = true;
                cbMC.Checked = false;
            }
            else
            {
                cbMC.Checked = true;
                cbGArt.Checked = false;
            }
        }
        ///<summary>ctrGueterArtListe / ctrGueterArtListe_Load</summary>
        ///<remarks></remarks>
        private void ctrGueterArtListe_Load(object sender, EventArgs e)
        {
            //Headline
            this.GL_User = this._ctrMenu.GL_User;
            MinMAxPanelExpand();
            InitDGV();
        }
        ///<summary>ctrGueterArtListe / SettsbtnOpenEditImage</summary>
        ///<remarks></remarks>
        private void SettsbtnOpenEditImage()
        {
            if (this.scGArt.Panel2Collapsed == true)
            {
                this.tsbtnOpenEdit.Image = Sped4.Properties.Resources.layout_left;
            }
            else
            {
                this.tsbtnOpenEdit.Image = Sped4.Properties.Resources.layout;
            }
        }
 
        ///<summary>ctrGueterArtListe / InitDGVGart</summary>
        ///<remarks></remarks>
        public void InitDGV()
        {
            InitReihe();
        }
        ///<summary>ctrGueterArtListe / tabGArt_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tabGArt_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tmpTab = (TabControl)sender;
            Int32 iIndex = tmpTab.SelectedIndex;
            switch (iIndex)
            {
                case 0:
                    InitTabGArtEdit();
                    //MinMaxPanel
                    MinMAxPanelExpand();
                    break;
            }
        }
        ///<summary>ctrGueterArtListe / MinMAxPanelExpandonGArtenEdit</summary>
        ///<remarks></remarks>
        private void MinMAxPanelExpand()
        {

        }
        ///<summary>ctrGueterArtListe / tsbtnOpenEdit_Click</summary>
        ///<remarks></remarks>
        private void tsbtnOpenEdit_Click(object sender, EventArgs e)
        {
            ShowAndHideTabPage();
        }
        ///<summary>ctrGueterArtListe / ShowAndHideTabPage</summary>
        ///<remarks></remarks>
        private void ShowAndHideTabPage()
        {
            if (this.scGArt.Panel2Collapsed == true)
            {
                this.scGArt.Panel2Collapsed = false;
                SettsbtnOpenEditImage();
            }
            else
            {
                this.scGArt.Panel2Collapsed = true;
                SettsbtnOpenEditImage();
            }
            //ResetCtrGueterArtListeWidth();
        }
        private void dgvReihe_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrGueterArtListe / tsbtnVerpackungsdatenChange_Click</summary>
        ///<remarks>TabVerpackungenEdit anzeigen</remarks>
        private void tsbtnVerpackungsdatenChange_Click(object sender, EventArgs e)
        {
            bUpdateGArtDaten = true;
            scGArt.Panel2Collapsed = false;
            //ResetCtrGueterArtListeWidth();
            InitTabGArtEingabeFelder();
        }
        ///<summary>ctrGueterArtListe / tsbtnVerpackungsdatenChange_Click</summary>
        ///<remarks>TabVerpackungenEdit anzeigen</remarks>
        private void tsbGArtDatenChange_Click(object sender, EventArgs e)
        {
            if (this.LagerOrt.Werk.Halle.Reihe.dtReihe.Rows.Count > 0)
            {
                bUpdateGArtDaten = true;
                if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                {
                    InitTabGArtEingabeFelder();
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
                    tbReiheBeschreibung.Text = "";

                }
                else
                {
                    InitTabGArtEingabeFelder();
                }
                scGArt.Panel2Collapsed = false;
            }
        }
        ///<summary>ctrGueterArtListe / miCloseCtr_Click</summary>
        ///<remarks></remarks>
        private void miCloseCtr_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrReihe();
        }
        ///<summary>ctrGueterArtListe / dgvGArtList_MouseClick</summary>
        ///<remarks></remarks>
        private void dgvReihe_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.scGArt.Panel2Collapsed)
            {
                this.scGArt.Panel2Collapsed = true;
                //ResetCtrGueterArtListeWidth();
            }
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }
        ///<summary>ctrGueterArtListe / dgvGArtList_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvReihe_CellClick(object sender, GridViewCellEventArgs e)
        {
            decimal decTmpReihe = 0;
            //string strTmp = this.dgvReihe.Rows[this.dgvReihe.CurrentRow.Index].Cells["ID"].Value.ToString();
            string strTmp = this.dgvReihe.CurrentRow.Cells["Id"].Value.ToString();
            if (Decimal.TryParse(strTmp, out decTmpReihe))
            {
                this.LagerOrt.Werk.Halle.Reihe.ID = decTmpReihe;
                this.LagerOrt.Werk.Halle.Reihe.FillDaten();              
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReihe_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (this.LagerOrt.Werk.Halle.Reihe.ID > 0)
            {
                // Sicherstellen, dass Daten geladen sind
                this.LagerOrt.Werk.Halle.Reihe.GetReiheGutAssignmentList();

                // Panel öffnen
                this.scGArt.Panel2Collapsed = false;
                bReiheUpdate = true;

                // UI aktualisieren
                InitTabGArtEingabeFelder();
                SetReiheEingabeFelderEnabled(true);
            }
        }
        ///<summary>ctrGueterArtListe / dgvGArtList_CellDoubleClick</summary>
        ///<remarks>Der Güterartendatensatz soll neu erstell werden. Tab soll dann eingeblendet werden und
        ///         alle Eingabefelder entsprechend geleert werden</remarks>
        private void miAdd_Click(object sender, EventArgs e)
        {
            bUpdateGArtDaten = false;
            if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
            {
                InitTabGArtEingabeFelder();
                //ClearTabGArtEdit();
            }
            else
            {
                InitTabPageWG();
                ClearTabPageWGInputFields();
            }
            this.scGArt.Panel2Collapsed = false;
            //ResetCtrGueterArtListeWidth();
        }
        ///<summary>ctrGueterArtListe / toolStripButton1_Click</summary>
        ///<remarks>Aüteratenliste neuladen</remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            InitDGV();
        }
        ///<summary>ctrGueterArtListe / miDelete_Click</summary>
        ///<remarks></remarks>
        private void miDelete_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                LagerOrt.DeleteLagerOrt("Reihe");
                LagerOrt.Werk.Halle.Reihe.Init();
                LagerOrt.Werk.Halle.Reihe.UpdateOrderID(0, 0);
                InitReihe();
            }

            ShowAndHideTabPage();
        }
        ///<summary>frmGueterArten/ tsbExcel_Click</summary>
        ///<remarks></remarks>
        private void tsbExcel_Click(object sender, EventArgs e)
        {
            string strFileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_" + FileExportName;

            saveFileDialog.FileName = strFileName;
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }
            strFileName = this.saveFileDialog.FileName;
            bool openExportFile = false;

            Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgvReihe, strFileName, ref openExportFile, this.GL_User, true);

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(strFileName);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "Güterart - LIST - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                }
            }
        }
        ///<summary>frmGueterArten/ txtSearch_TextChanged</summary>
        ///<remarks>Suche im Grid Güterarten</remarks>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string SearchText = txtSearch.Text.ToString();
            string strSuchSpalte = string.Empty;
            if (cbGArt.Checked == true)
            {
                strSuchSpalte = "Bezeichnung";
            }
            if (cbMC.Checked == true)
            {
                strSuchSpalte = "ViewID";
            }
            //GArtFilter = "(" + GArtFilter + ") AND " + strSuchSpalte + " LIKE '" + SearchText + "%'";
            GArtFilter = strSuchSpalte + " LIKE '" + SearchText + "%'";
            dtGArt.DefaultView.RowFilter = GArtFilter;
        }
        ///<summary>frmGueterArten/ cbMC_CheckedChanged</summary>
        ///<remarks>Suchstatus ändern</remarks>
        private void cbMC_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMC.Checked == true)
            {
                cbGArt.Checked = false;
            }
            else
            {
                cbMC.Checked = false;
                cbGArt.Checked = true;
            }
        }
        ///<summary>frmGueterArten/ cbGArt_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbGArt_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGArt.Checked == true)
            {
                cbMC.Checked = false;
            }
            else
            {
                cbMC.Checked = true;
                cbGArt.Checked = false;
            }
        }
        ///<summary>frmGueterArten/ InitTabGArtEingabeFelder</summary>
        ///<remarks></remarks>
        private void InitTabGArtEingabeFelder()
        {
            //tabGArtEdit
            InitTabGArtEdit();
            //TabVerpackungEdit
            InitTabVerpackungEdit();
        }
        /********************************************************************************
         *                  tabGArtEdit
         * *****************************************************************************/
        ///<summary>frmGueterArten/ ResetEnableTbBox</summary>
        ///<remarks></remarks>
        private void InitTabGArtEdit()
        {
            MinMAxPanelExpand();
            //ClearTabGArtEdit();      // ist komplett leer
            this.scGArt.Panel2Collapsed = false;
            //ResetCtrGueterArtListeWidth(); // ist komplett auskommentiert
            SettsbtnOpenEditImage();
            //-- Daten auf das Ctr in die Eingabefelder setzen
            SetGArtenWGDatenToTab();
        }
        ///<summary>frmGueterArten/ ResetEnableTbBox</summary>
        ///<remarks>Daten auf das Ctr in die Eingabefelder setzen</remarks>
        private void SetGArtenWGDatenToTab()
        {
            tbReiheBezeichnung.Text = LagerOrt.Werk.Halle.Reihe.Bezeichnung;
            tbReiheBeschreibung.Text = LagerOrt.Werk.Halle.Reihe.Beschreibung;
            if (LagerOrt.Werk.Halle.Reihe.OrderID < nudReiheOrderID.Minimum)
            {
                nudReiheOrderID.Value = nudReiheOrderID.Minimum;
            }
            else if (LagerOrt.Werk.Halle.Reihe.OrderID > nudReiheOrderID.Maximum)
            {
                nudReiheOrderID.Value = nudReiheOrderID.Maximum;
            }
            else
            {
                nudReiheOrderID.Value = LagerOrt.Werk.Halle.Reihe.OrderID;
            }
            //
            if (LagerOrt.Werk.Halle.Reihe.Anzahl < nudMassAnzahl.Minimum)
            {
                nudMassAnzahl.Value = nudMassAnzahl.Minimum;
            }
            else if (LagerOrt.Werk.Halle.Reihe.Anzahl > nudMassAnzahl.Maximum)
            {
                nudMassAnzahl.Value = nudMassAnzahl.Maximum;
            }
            else
            {
                nudMassAnzahl.Value = LagerOrt.Werk.Halle.Reihe.Anzahl;
            }
            //
            if (LagerOrt.Werk.Halle.Reihe.DickeVon < nudDickeVon.Minimum)
            {
                nudDickeVon.Value = nudDickeVon.Minimum;
            }
            else if (LagerOrt.Werk.Halle.Reihe.DickeVon > nudDickeVon.Maximum)
            {
                nudDickeVon.Value = nudDickeVon.Maximum;
            }
            else
            {
                nudDickeVon.Value = LagerOrt.Werk.Halle.Reihe.DickeVon;
            }
            //
            if (LagerOrt.Werk.Halle.Reihe.DickeBis < nudDickeBis.Minimum)
            {
                nudDickeBis.Value = nudDickeVon.Minimum;
            }
            else if (LagerOrt.Werk.Halle.Reihe.DickeBis > nudDickeBis.Maximum)
            {
                nudDickeBis.Value = nudDickeVon.Maximum;
            }
            else
            {
                nudDickeBis.Value = LagerOrt.Werk.Halle.Reihe.DickeBis;
            }
            //
            if (LagerOrt.Werk.Halle.Reihe.BreiteVon < nudBreiteVon.Minimum)
            {
                nudBreiteVon.Value = nudDickeVon.Minimum;
            }
            else if (LagerOrt.Werk.Halle.Reihe.BreiteVon > nudBreiteVon.Maximum)
            {
                nudBreiteVon.Value = nudDickeVon.Maximum;
            }
            else
            {
                nudBreiteVon.Value = LagerOrt.Werk.Halle.Reihe.BreiteVon;
            }
            //
            if (LagerOrt.Werk.Halle.Reihe.BreiteBis < nudBreiteVon.Minimum)
            {
                nudBreiteBis.Value = nudDickeVon.Minimum;
            }
            else if (LagerOrt.Werk.Halle.Reihe.BreiteBis > nudBreiteVon.Maximum)
            {
                nudBreiteBis.Value = nudDickeVon.Maximum;
            }
            else
            {
                nudBreiteBis.Value = LagerOrt.Werk.Halle.Reihe.BreiteBis;
            }
            //
            if (LagerOrt.Werk.Halle.Reihe.LaengeVon < nudBreiteVon.Minimum)
            {
                nudLaengeVon.Value = nudDickeVon.Minimum;
            }
            else if (LagerOrt.Werk.Halle.Reihe.LaengeVon > nudBreiteVon.Maximum)
            {
                nudLaengeVon.Value = nudDickeVon.Maximum;
            }
            else
            {
                nudLaengeVon.Value = LagerOrt.Werk.Halle.Reihe.LaengeVon;
            }
            //
            if (LagerOrt.Werk.Halle.Reihe.LaengeBis < nudLaengeBis.Minimum)
            {
                nudLaengeBis.Value = nudDickeVon.Minimum;
            }
            else if (LagerOrt.Werk.Halle.Reihe.LaengeBis > nudLaengeBis.Maximum)
            {
                nudLaengeBis.Value = nudDickeVon.Maximum;
            }
            else
            {
                nudLaengeBis.Value = LagerOrt.Werk.Halle.Reihe.LaengeBis;
            }
            //
            if (LagerOrt.Werk.Halle.Reihe.HoeheVon < nudHoeheVon.Minimum)
            {
                nudHoeheVon.Value = nudDickeVon.Minimum;
            }
            else if (LagerOrt.Werk.Halle.Reihe.HoeheVon > nudHoeheVon.Maximum)
            {
                nudHoeheVon.Value = nudDickeVon.Maximum;
            }
            else
            {
                nudHoeheVon.Value = LagerOrt.Werk.Halle.Reihe.HoeheVon;
            }
            //
            if (LagerOrt.Werk.Halle.Reihe.HoeheBis < nudHoeheBis.Minimum)
            {
                nudHoeheBis.Value = nudDickeVon.Minimum;
            }
            else if (LagerOrt.Werk.Halle.Reihe.HoeheBis > nudHoeheBis.Maximum)
            {
                nudHoeheBis.Value = nudDickeVon.Maximum;
            }
            else
            {
                nudHoeheBis.Value = LagerOrt.Werk.Halle.Reihe.HoeheBis;
            }
            //
            if (LagerOrt.Werk.Halle.Reihe.BruttoVon < nudBruttoVon.Minimum)
            {
                nudBruttoVon.Value = nudDickeVon.Minimum;
            }
            else if (LagerOrt.Werk.Halle.Reihe.BruttoVon > nudBruttoVon.Maximum)
            {
                nudBruttoVon.Value = nudDickeVon.Maximum;
            }
            else
            {
                nudBruttoVon.Value = LagerOrt.Werk.Halle.Reihe.BruttoVon;
            }
            //
            if (LagerOrt.Werk.Halle.Reihe.BruttoBis < nudBruttoBis.Minimum)
            {
                nudBruttoBis.Value = nudDickeVon.Minimum;
            }
            else if (LagerOrt.Werk.Halle.Reihe.BruttoBis > nudBruttoBis.Maximum)
            {
                nudBruttoBis.Value = nudDickeVon.Maximum;
            }
            else
            {
                nudBruttoBis.Value = LagerOrt.Werk.Halle.Reihe.BruttoBis;
            }
            InitDgvGutList();
            //TakeOverGueterArt(this.LagerOrt.Werk.Halle.Reihe.GArt.ID);
        }
        /************************************************************************
         *                    TABVerpackungEdit
         * **********************************************************************/
        ///<summary>ctrADR_List / InitTabVerpackungEdit</summary>
        ///<remarks></remarks>
        private void InitTabVerpackungEdit()
        {
            //ClearTabVerpackungEdit();   // ist komplett leer
            this.scGArt.Panel2Collapsed = false;
            //ResetCtrGueterArtListeWidth();   // ist komplett auskommentiert
            SettsbtnOpenEditImage();
            //SetGArtenDatenToTabVerpackungenEdit(); // ist komplett leer
        }
         /************************************************************************
        *                    TabStyleSheet
        * **********************************************************************/
        ///<summary>ctrADR_List / tsbtnStyleAdd_Click</summary>
        ///<remarks>Neue Vorlage hinzufügen</remarks>
        private void tsbtnStyleAdd_Click(object sender, EventArgs e)
        {
            bUpdateStyleSheet = false;
            ClearTabStyleSheet();
            SetTabStyleSheetEingabefelderEnabled(true);
        }
        ///<summary>ctrADR_List / SetTabStyleSheetEingabefelderEnabled</summary>
        ///<remarks></remarks>
        private void SetTabStyleSheetEingabefelderEnabled(bool bEnabled)
        {

        }
        ///<summary>ctrADR_List / ClearTabStyleSheet</summary>
        ///<remarks></remarks>
        private void ClearTabStyleSheet()
        {

        }
        ///<summary>ctrADR_List / tsbtnCloseFormat_Click</summary>
        ///<remarks>Check Eingabe auf Decimal</remarks>
        private void InitTabStyleSheet()
        {

        }
        ///<summary>ctrADR_List / tsbtnCloseFormat_Click</summary>
        ///<remarks>Check Eingabe auf Decimal</remarks>
        private void tsbtnSaveFormat_Click(object sender, EventArgs e)
        {
            AssignVarStyleSheet();
            ClearTabStyleSheet();
            SetTabStyleSheetEingabefelderEnabled(false);
            InitTabStyleSheet();
        }
        ///<summary>ctrADR_List / tsbtnCloseFormat_Click</summary>
        ///<remarks>Check Eingabe auf Decimal</remarks>
        private void tsbtnCloseFormat_Click(object sender, EventArgs e)
        {
            this.scGArt.Panel2Collapsed = true;
            //ResetCtrGueterArtListeWidth();
            ClearTabStyleSheet();
        }
        ///<summary>ctrADR_List / dgvStyleSheet_CellClick</summary>
        ///<remarks></remarks>
        private void dgvStyleSheet_CellClick(object sender, GridViewCellEventArgs e)
        {
            //Style Sheet ID ermitteln
            decimal decTmp = 0;

        }
        ///<summary>ctrADR_List / dgvStyleSheet_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvStyleSheet_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            SetStyleSheetDataToTabStyleSheet();
            SetTabStyleSheetEingabefelderEnabled(true);

        }
        ///<summary>ctrADR_List / SetStyleSheetDataToTabStyleSheet</summary>
        ///<remarks></remarks>
        private void SetStyleSheetDataToTabStyleSheet()
        {
        }
        ///<summary>ctrADR_List / SetStyleSheetDataToTabStyleSheet</summary>
        ///<remarks></remarks>
        private void AssignVarStyleSheet()
        {
        }
        ///<summary>ctrADR_List / dgvStyleSheet_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void dgvStyleSheet_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrADR_List / tsbGArtenEditSave_Click</summary>
        ///<remarks></remarks>
        private void tsbGArtenEditSave_Click(object sender, EventArgs e)
        {
            //SaveGutDaten();
            bUpdateGArtDaten = false;
        }
        ///<summary>ctrADR_List / tsbGArtenEditSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnGArtEditClose_Click(object sender, EventArgs e)
        {
            this.scGArt.Panel2Collapsed = true;
            //ResetCtrGueterArtListeWidth();
        }
        /***************************************************************************************
         *                        tabPage WG
         * ************************************************************************************/
        ///<summary>ctrADR_List / tsbtnWGClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnWGClose_Click(object sender, EventArgs e)
        {
            this.scGArt.Panel2Collapsed = true;
            //ResetCtrGueterArtListeWidth();
        }
        ///<summary>ctrADR_List / tsbtnWGSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnWGSave_Click(object sender, EventArgs e)
        {
            //SaveGutDaten();
            bReiheUpdate = false;
        }
        ///<summary>frmGueterArten/ InitTabPageWG</summary>
        ///<remarks></remarks>
        private void InitTabPageWG()
        {
            MinMAxPanelExpand();
            ClearTabPageWGInputFields();
            this.scGArt.Panel2Collapsed = false;
            //ResetCtrGueterArtListeWidth();
            SettsbtnOpenEditImage();
            //-- Daten auf das Ctr in die Eingabefelder setzen
            SetGArtenWGDatenToTab();
        }
        ///<summary>frmGueterArten/ ClearTabPageWGInputFields</summary>
        ///<remarks></remarks>
        private void ClearTabPageWGInputFields()
        {

        }

        private void lBreite_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lLaenge_Click(object sender, EventArgs e)
        {

        }

        private void nudDicke_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tsbtnReiheSave_Click(object sender, EventArgs e)
        {
            LagerOrt.Werk.Halle.Reihe.Bezeichnung = tbReiheBezeichnung.Text.Trim();
            LagerOrt.Werk.Halle.Reihe.Beschreibung = tbReiheBeschreibung.Text.Trim();
            LagerOrt.Werk.Halle.Reihe.OrderID = (Int32)nudReiheOrderID.Value;
            LagerOrt.Werk.Halle.Reihe.Anzahl = nudMassAnzahl.Value;
            LagerOrt.Werk.Halle.Reihe.DickeVon = nudDickeVon.Value;
            LagerOrt.Werk.Halle.Reihe.DickeBis = nudDickeBis.Value;
            LagerOrt.Werk.Halle.Reihe.BreiteVon = nudBreiteVon.Value;
            LagerOrt.Werk.Halle.Reihe.BreiteBis = nudBreiteBis.Value;
            LagerOrt.Werk.Halle.Reihe.LaengeVon = nudLaengeVon.Value;
            LagerOrt.Werk.Halle.Reihe.LaengeBis = nudLaengeBis.Value;
            LagerOrt.Werk.Halle.Reihe.HoeheVon = nudHoeheVon.Value;
            LagerOrt.Werk.Halle.Reihe.HoeheBis = nudHoeheBis.Value;
            LagerOrt.Werk.Halle.Reihe.BruttoVon = nudBruttoVon.Value;
            LagerOrt.Werk.Halle.Reihe.BruttoBis = nudBruttoBis.Value;

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
        ///<summary>ctrLagerOrt / SetReiheEingabeFelderEnabled</summary>
        ///<remarks>Aktivieren / Deaktivieren der Eingabefelder.</remarks>
        private void SetReiheEingabeFelderEnabled(bool bEnabled)
        {
            tbReiheBezeichnung.Enabled = bEnabled;
            tbReiheBeschreibung.Enabled = bEnabled;
            nudReiheOrderID.Enabled = bEnabled;
            nudMassAnzahl.Enabled = bEnabled;
            nudDickeVon.Enabled = bEnabled;
            nudDickeBis.Enabled = bEnabled;
            nudBreiteVon.Enabled = bEnabled;
            nudBreiteBis.Enabled = bEnabled;
            nudBruttoBis.Enabled = bEnabled;
            nudLaengeVon.Enabled = bEnabled;
            nudLaengeBis.Enabled = bEnabled;
            nudHoeheVon.Enabled = bEnabled;
            nudHoeheBis.Enabled = bEnabled;
            nudBruttoVon.Enabled = bEnabled;
            nudBruttoBis.Enabled = bEnabled;

            //tbGArtSearch.Enabled = bEnabled;
            //btnGArt.Enabled = bEnabled;
            //tbGArt.Enabled = bEnabled;

        }
        ///<summary>ctrLagerOrt / ClearHalleEingabeFelder</summary>
        ///<remarks></remarks>
        private void ClearReiheEingabeFelder()
        {
            tbReiheBeschreibung.Text = string.Empty;
            tbReiheBezeichnung.Text = string.Empty;
            nudMassAnzahl.Value = 0;
            nudDickeVon.Value = 0;
            nudDickeBis.Value = 0;
            nudBreiteVon.Value = 0;
            nudBreiteBis.Value = 0;
            nudBruttoBis.Value = 0;
            nudLaengeVon.Value = 0;
            nudLaengeBis.Value = 0;
            nudHoeheVon.Value = 0;
            nudHoeheBis.Value = 0;
            nudBruttoVon.Value = 0;
            nudBruttoBis.Value = 0;

            dgvGutlist.DataSource = null;

            //tbGArtSearch.Text = string.Empty;
            //tbGArt.Text = string.Empty;

        }
        ///<summary>ctrLagerOrt / InitReihe</summary>
        ///<remarks></remarks>
        private void InitReihe()
        {
            SetReiheEingabeFelderEnabled(false);
            if (LagerOrt == null)
            {
                LagerOrt = new clsLagerOrt();
                LagerOrt.Init();
            }

            if (LagerOrt.Werk.dtWerk.Rows.Count == 0)
            {
                LagerOrt.Werk.Bezeichnung = "1";
                LagerOrt.Werk.Add();

            }
            else
            {
                // erweiterung für das werk (tab)
                LagerOrt.Werk.ID = Convert.ToDecimal(LagerOrt.Werk.dtWerk.Rows[0]["ID"]);
                LagerOrt.Werk.FillDaten();
            }
            LagerOrt.Werk.Halle.GetHallenDatenForDataTable(true);

            if (LagerOrt.Werk.Halle.dtHalle.Rows.Count == 0)
            {
                if (LagerOrt.Werk.dtWerk.Rows.Count > 0)
                {
                    LagerOrt.Werk.ID = Convert.ToDecimal(LagerOrt.Werk.dtWerk.Rows[0]["ID"]);
                    LagerOrt.Werk.FillDaten();
                    LagerOrt.Werk.Halle.WerkID = LagerOrt.Werk.ID;
                    LagerOrt.Werk.Halle.Bezeichnung = "1";
                    LagerOrt.Werk.Halle.Add();
                }
            }
            else
            {
                // erweiterung für die Halle (tab)
                LagerOrt.Werk.Halle.ID = Convert.ToDecimal(LagerOrt.Werk.Halle.dtHalle.Rows[0]["ID"]);
                LagerOrt.Werk.Halle.Reihe.HalleID = LagerOrt.Werk.Halle.ID;
            }

            //Clear Eingabefelder Werk
            ClearReiheEingabeFelder();
            //Schritt 2

            LagerOrt.Werk.Halle.Reihe.Init();
            //Datatable
            if (LagerOrt.Werk.Halle.Reihe.dtReihe.Columns.Contains("Belegung"))
                LagerOrt.Werk.Halle.Reihe.dtReihe.Columns["Belegung"].SetOrdinal(5);
            this.dgvReihe.DataSource = LagerOrt.Werk.Halle.Reihe.dtReihe;
            //this.dgvReihe.Columns["ID"].IsVisible = false;
            this.dgvReihe.Columns["ID"].IsVisible = (this.GL_User.IsAdmin);
            this.dgvReihe.Columns["HalleID"].IsVisible = false;
            this.dgvReihe.Columns["GArtID"].IsVisible = false;

            //this.dgvReihe.Columns["OrderID"]. = 0;

            //this.dgvReihe.AutoSize = true;

            //this.dgvReihe.Columns["Beschreibung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //SetSelectedRowInDGV(ref this.dgvReihe, LagerOrt.Werk.Halle.Reihe.ID);
            decimal decTmp = 0;
            if (this.dgvReihe.Rows.Count > 0)
            {
                try
                {
                    //decTmp = (decimal)this.dgvReihe.Rows[iReiheSelectedRow].Cells["ID"].Value;
                }
                catch
                {
                    //iReiheSelectedRow = 0;
                    //decTmp = (decimal)this.dgvReihe.Rows[iReiheSelectedRow].Cells["ID"].Value;
                }
            }
            //SelectetReiheIDChanged(decTmp);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnReiheAdd_Click(object sender, EventArgs e)
        {
            bReiheUpdate = false;
            SetReiheEingabeFelderEnabled(true);
            //Clear Werkeingabefelder
            ClearReiheEingabeFelder();
            tbReiheBezeichnung.Focus();

            //OrderID maximum setzen
            nudReiheOrderID.Maximum = nudReiheOrderID.Maximum + 1;
            nudReiheOrderID.Value = LagerOrt.Werk.Halle.Reihe.maxOrderID + 1;

            //nudReiheOrderID.Maximum = LagerOrt.Werk.Halle.maxOrderIDReihe + 1;
            //nudReiheOrderID.Value = nudReiheOrderID.Maximum;

            //angebotene Vorgabe Beschreibung
            tbReiheBeschreibung.Text = "";

        }

        private void tsbtnReiheDelete_Click(object sender, EventArgs e)
        {

            if (clsMessages.DeleteAllgemein())
            {
                LagerOrt.DeleteLagerOrt("Reihe");
                LagerOrt.Werk.Halle.Reihe.Init();
                LagerOrt.Werk.Halle.Reihe.UpdateOrderID(0, 0);
                InitReihe();
            }
        }

        private void tsbtnReiheClear_Click(object sender, EventArgs e)
        {
            ClearReiheEingabeFelder();
        }

        private void btnGArt_Click(object sender, EventArgs e)
        {
            this._ctrMenu.OpenFrmGArtenList(this);
        }

        internal void TakeOverGueterArt(decimal TakeOver_ID)
        {
            //-- bis 21.10.2026
            //LagerOrt.Werk.Halle.Reihe.GArt.ID = TakeOver_ID;
            //LagerOrt.Werk.Halle.Reihe.GArt.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System);
            //LagerOrt.Werk.Halle.Reihe.GArt.Fill();

            //--- Güterart wird hinterlegt
            //----- Save in Table 
            if ((LagerOrt.Werk.Halle.Reihe.ID > 0) && (TakeOver_ID > 0))
            {
                if (!ReiheGutAssignmentViewData.ExistByReiheAndGArt((int)LagerOrt.Werk.Halle.Reihe.ID, (int)TakeOver_ID, (int)GL_User.User_ID))
                {
                    ReiheGutAssignmentViewData rgaVD = new ReiheGutAssignmentViewData();
                    rgaVD.ReiheGutAssignment.Id = 0;
                    decimal decTmp = 0;
                    Decimal.TryParse(LagerOrt.Werk.Halle.Reihe.ID.ToString(), out decTmp);
                    rgaVD.ReiheGutAssignment.ReiheId = decTmp;
                    rgaVD.ReiheGutAssignment.GArtId = TakeOver_ID;
                    rgaVD.Add();

                    LagerOrt.Werk.Halle.Reihe.GetReiheGutAssignmentList();

                }
                else
                {
                    string strMes = "Die Güterart ist bereits der Reihe zugeordnet.";
                    clsMessages.Allgemein_ERRORTextShow(strMes);
                }
            }
            else
            {
                string strMes = "Keine Güterart gewählt bzw. Id=0.";
                clsMessages.Allgemein_ERRORTextShow(strMes);
            }
            InitDgvGutList();
            //SetGArtDatenFromSelectedGut();
        }
        ///<summary>ctrEinlagerung / SetLabelGArdIDInfo</summary>
        ///<remarks>Set den GüterartID Info.</remarks>  
        private void SetLabelGArdIDInfo()
        {
            //lGArtID.Text = _lTextGArtID + LagerOrt.Werk.Halle.Reihe.GArt.ID;
        }


        private void tsbtnReiheLabelPrint_Click(object sender, EventArgs e)
        {
            //TODO: Drucken
            InitPrint();
        }

        private void InitPrint()
        {
            this._ctrMenu.OpenFrmReporView(this, false);
        }

        private void tsbtnOpenGut_Click(object sender, EventArgs e)
        {
            this._ctrMenu.OpenFrmGArtenList(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnGutDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvGutlist.CurrentRow != null && dgvGutlist.CurrentRow.Index >= 0)
                {
                    // ✅ KORREKT: Direkt CurrentRow verwenden, nicht Rows[Index]
                    int iGutAssignmentId = 0;

                    if (int.TryParse(dgvGutlist.CurrentRow.Cells["Id"].Value?.ToString() ?? "0", out iGutAssignmentId))
                    {
                        if (iGutAssignmentId > 0)
                        {
                            if (ReiheGutAssignmentViewData.ExistByReiheGutAssignmentById(iGutAssignmentId, (int)GL_User.User_ID))
                            {
                                ReiheGutAssignmentViewData rgaVD = new ReiheGutAssignmentViewData(iGutAssignmentId);

                                if (clsMessages.DeleteAllgemein())
                                {
                                    rgaVD.Delete();
                                    LagerOrt.Werk.Halle.Reihe.GetReiheGutAssignmentList();
                                    InitDgvGutList();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Fehler in tsbtnGutDelete_Click: " + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgvGutList()
        {
            dgvGutlist.DataSource = null;
            dgvGutlist.DataSource = LagerOrt.Werk.Halle.Reihe.ListReiheGutAssignmentView;

            // Scrollbalken anzeigen
            dgvGutlist.TableElement.VScrollBar.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            dgvGutlist.TableElement.HScrollBar.Visibility = Telerik.WinControls.ElementVisibility.Visible;

            // Spalten ausblenden
            //dgvGutlist.Columns["Reihe"].IsVisible = false;
            //dgvGutlist.Columns["Gut"].IsVisible = false;

            dgvGutlist.Columns["Id"].IsVisible = true;
            dgvGutlist.Columns["ReiheId"].IsVisible = false;
            dgvGutlist.Columns["ReihenBezeichnung"].IsVisible = true;
            dgvGutlist.Columns["ReiheOrderId"].IsVisible = false;
            dgvGutlist.Columns["GArtId"].IsVisible = false;
            dgvGutlist.Columns["HalleId"].IsVisible = false;

            // Spaltenbreite individuell festlegen
            dgvGutlist.Columns["Id"].Width = 30;
            dgvGutlist.Columns["ReihenBezeichnung"].Width = 70;
            dgvGutlist.Columns["Matchcode"].Width = 100;
            dgvGutlist.Columns["Güterart"].Width = 250;

            // Optional: AutoSizeMode deaktivieren für feste Breiten
            dgvGutlist.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.None;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefreshReiheGutAssignment_Click(object sender, EventArgs e)
        {
            InitDgvGutList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvGutlist_Click(object sender, EventArgs e)
        {
            if (dgvGutlist.CurrentRow != null)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dgvGutlist.CurrentRow.Cells["Id"].Value.ToString(), out decTmp);
                if (decTmp > 0)
                {
                   
                }
            }
        }


    }
}


