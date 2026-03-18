using LVS;
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

            InitTsddbtnGArtenListen();
            RadGridLocalizationProvider.CurrentProvider = new clsGermanRadGridLocalizationProvider();
            bUpdateGArtDaten = false;

            //Width
            iListWidth = this.scGArt.Panel1.Width + 80;
            iTabGArtEditWidth = this.scGArt.Panel2.Width + 20;
            this.scGArt.Panel2Collapsed = true;
            ResetCtrGueterArtListeWidth();

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
        ///<summary>ctrGueterArtListe / InitTsddbtnGArtenListen</summary>
        ///<remarks>Erstellt das DropDownMenüliste der verfügbaren Güterartenlisten für die verschiedenen Arbeitsbereiche
        ///         und eine Gesamtübersicht.Das ClickEvent wird in der Funktion TsddbtnGArtenListenItem_click ausgeführt</remarks>
        private void InitTsddbtnGArtenListen()
        {
            //tsbtnListGArtenListen.DropDownItems.Clear();
            //DataTable dtArbeitsbereiche = clsArbeitsbereiche.GetArbeitsbereichList(this.GL_User.User_ID);
            ////Alle Güterarten
            //ToolStripMenuItem tsmItem = new ToolStripMenuItem("Alle");
            //tsmItem.Tag = "0";
            //tsmItem.Text = "Alle";
            //tsmItem.Click += new EventHandler(TsddbtnGArtenListenItem_click);
            //tsbtnListGArtenListen.DropDownItems.Add(tsmItem);

            //tsmItem = new ToolStripMenuItem("aktiv");
            //tsmItem.Tag = "-1";
            //tsmItem.Text = "aktiv";
            //tsmItem.Click += new EventHandler(TsddbtnGArtenListenItem_click);
            //tsbtnListGArtenListen.DropDownItems.Add(tsmItem);

            //tsmItem = new ToolStripMenuItem("deaktiviert");
            //tsmItem.Tag = "-2";
            //tsmItem.Text = "deaktiviert";
            //tsmItem.Click += new EventHandler(TsddbtnGArtenListenItem_click);
            //tsbtnListGArtenListen.DropDownItems.Add(tsmItem);

            //for (Int32 i = 0; i <= dtArbeitsbereiche.Rows.Count - 1; i++)
            //{
            //    //Liste der Arbeitsbereiche
            //    string strABName = dtArbeitsbereiche.Rows[i]["Arbeitsbereich"].ToString();
            //    tsmItem = new ToolStripMenuItem(strABName);
            //    tsmItem.Tag = dtArbeitsbereiche.Rows[i]["ID"].ToString();
            //    tsmItem.Text = strABName;
            //    tsmItem.Click += new EventHandler(TsddbtnGArtenListenItem_click);

            //    tsbtnListGArtenListen.DropDownItems.Add(tsmItem);
            //}
        }
        ///<summary>ctrGueterArtListe / TsddbtnGArtenListenItem_click</summary>
        ///<remarks></remarks>
        private void TsddbtnGArtenListenItem_click(object sender, EventArgs e)
        {
            //txtSearch.Text = string.Empty;
            //ToolStripMenuItem tmpClicked = (ToolStripMenuItem)sender;
            //Int32 iTmp = 0;
            //Int32.TryParse(tmpClicked.Tag.ToString(), out iTmp);
            //MenuItemListArt = iTmp;
            //string strHeadline = string.Empty;
            //if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
            //{ 
            //    strHeadline = const_ExportFileName_GArt +" ["+tmpClicked.Text+"]";
            //}
            //else 
            //{
            //    strHeadline = const_ExportFileName_WG + " [" + tmpClicked.Text + "]";        
            //}
            //afColorLabel1.myText = strHeadline;
            //InitDGV();
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
            ResetCtrGueterArtListeWidth();
        }
        ///<summary>ctrGueterArtListe / dgvGArtList_ToolTipTextNeeded</summary>
        ///<remarks>Zeigt den Zelleninhalt als ToolTip an</remarks>
        private void dgvGArtList_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
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
            ResetCtrGueterArtListeWidth();
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
                ResetCtrGueterArtListeWidth();
            }
        }
        ///<summary>ctrGueterArtListe / miCloseCtr_Click</summary>
        ///<remarks></remarks>
        private void miCloseCtr_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrReihe();
            //if (this._frmGArtenAuftragserfassung != null)
            //{
            //    ClosefrmGArtenAuftragserfassung();
            //}
            //else
            //{
            //    Int32 Count = this.ParentForm.Controls.Count;

            //    for (Int32 i = 0; (i <= (Count - 1)); i++)
            //    {
            //        if (this.ParentForm.Controls[i].Name == "TempSplitterGut")
            //        {
            //            this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
            //            //i = Count - 1;      // ist nur ein Controll vorhanden
            //        }
            //        if (this.ParentForm.Controls[i].GetType() == typeof(ctrGueterArtListe))
            //        {
            //            this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
            //            i = Count - 1;      // ist nur ein Controll vorhanden
            //        }
            //    }
            //}
        }
        ///<summary>ctrGueterArtListe / dgvGArtList_MouseClick</summary>
        ///<remarks></remarks>
        private void dgvGArtList_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.scGArt.Panel2Collapsed)
            {
                this.scGArt.Panel2Collapsed = true;
                ResetCtrGueterArtListeWidth();
            }
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }
        ///<summary>ctrGueterArtListe / dgvGArtList_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvGArtList_CellClick(object sender, GridViewCellEventArgs e)
        {
            decimal decTmpReihe = 0;
            string strTmp = this.dgvReihe.Rows[this.dgvReihe.CurrentRow.Index].Cells["ID"].Value.ToString();
            if (Decimal.TryParse(strTmp, out decTmpReihe))
            {
                this.LagerOrt.Werk.Halle.Reihe.ID = decTmpReihe;
                this.LagerOrt.Werk.Halle.Reihe.FillDaten();
            }
        }
        ///<summary>ctrGueterArtListe / dgvGArtList_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvGArtList_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (this.LagerOrt.Werk.Halle.Reihe.ID > 0)
            {
                //Unterscheidung Suche und Bearbeitung der Daten
                this.scGArt.Panel2Collapsed = false;
                ResetCtrGueterArtListeWidth();
                bReiheUpdate = true;

                InitTabGArtEingabeFelder();
                SetReiheEingabeFelderEnabled(true);
                //}
                //else
                //{
                //   InitTabPageWG();
                //}
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
                ClearTabGArtEdit();
            }
            else
            {
                InitTabPageWG();
                ClearTabPageWGInputFields();
            }
            this.scGArt.Panel2Collapsed = false;
            ResetCtrGueterArtListeWidth();
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
        ///<summary>ctrGueterArtListe / DeleteGArtenDaten</summary>
        ///<remarks>Gewählten Datensatz löschen</remarks>
        private void DeleteGArtenDaten()
        {
            //if (GL_User.write_Gut)
            //{
            //    if (this.dgvReihe.Rows.Count > 0)
            //    {
            //        decimal decTmp = 0;
            //        string strTmp = this.dgvReihe.Rows[this.dgvReihe.CurrentRow.Index].Cells["ID"].Value.ToString();
            //        Decimal.TryParse(strTmp, out decTmp);

            //        //ID 1 ist ViewID 0 alle Güter
            //        if (decTmp > 1)
            //        {
            //            Gut.ID = decTmp;
            //            Gut.Fill();
            //            if (Gut.GArtIsUsed)
            //            {
            //                clsMessages.Gut_GueterartIsInUse();
            //                Gut.Aktiv = false;
            //                Gut.UpdateGueterArt();
            //                Gut.Fill();
            //            }
            //            else
            //            {
            //                if (clsMessages.DeleteAllgemein())
            //                {
            //                    Gut.Delete();
            //                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
            //                    {

            //                    }
            //                    else
            //                    {
            //                        ClearTabPageWGInputFields();
            //                    }                           
            //                }
            //            }
            //        }
            //        InitDGV();
            //    }
            //}
            //else
            //{
            //    clsMessages.User_NoAuthen();
            //}
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
        ///<summary>frmGueterArten/ ResetCtrGueterArtListeWidth</summary>
        ///<remarks></remarks>
        private void ResetCtrGueterArtListeWidth()
        {
            //breite neu ermitteln
            //iListWidth = 30;
            //for (Int32 i = 0; i <= this.dgvReihe.Columns.Count - 1; i++)
            //{
            //    if (this.dgvReihe.Columns[i].IsVisible)
            //    {
            //        iListWidth = iListWidth + this.dgvReihe.Columns[i].Width;
            //    }
            //}

            //if (this.scGArt.Panel2Collapsed)
            //{

            //    this.Width = iListWidth + 10;
            //    this.Width = 700;
            //    //this.scGArt.Panel1.Width = this.Width;
            //}
            //else
            //{
            //    this.Width = iListWidth + iTabGArtEditWidth + 10; 
            //    this.scGArt.SplitterDistance = iListWidth+20;
            //    this.Width = 700;
            //}

            //this.Refresh();
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
            ClearTabGArtEdit();
            this.scGArt.Panel2Collapsed = false;
            ResetCtrGueterArtListeWidth();
            SettsbtnOpenEditImage();
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
            TakeOverGueterArt(this.LagerOrt.Werk.Halle.Reihe.GArt.ID);
        }
        ///<summary>frmGueterArten/ ResetEnableTbBox</summary>
        ///<remarks></remarks>
        private void ClearTabGArtEdit()
        {


        }
        ///<summary>frmGueterArten/ tsbtnGArtenEditDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnGArtenEditDelete_Click(object sender, EventArgs e)
        {

        }
        ///<summary>frmGueterArten/ tsbSpeichern_Click</summary>
        ///<remarks></remarks>
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
        }
        ///<summary>frmGueterArten/ SaveGutDaten</summary>
        ///<remarks></remarks>
        private void SaveGutDaten()
        {
        }
        ///<summary>ctrADR_List / tbViewID_Validated</summary>
        ///<remarks>Bei Eingabe werden alle anderen Eingabefelder freigegeben</remarks>
        private void tbViewID_Validated(object sender, EventArgs e)
        {

        }
        ///<summary>ctrADR_List / SetTabGArtEditEingabeFelderEnabled</summary>
        ///<remarks></remarks>
        private void SetTabGArtEditEingabeFelderEnabled(bool bEnabled, Int32 iMassZahl)
        {

        }
        /************************************************************************
         *                    TABVerpackungEdit
         * **********************************************************************/
        ///<summary>ctrADR_List / InitTabVerpackungEdit</summary>
        ///<remarks></remarks>
        private void InitTabVerpackungEdit()
        {
            ClearTabVerpackungEdit();
            this.scGArt.Panel2Collapsed = false;
            ResetCtrGueterArtListeWidth();
            SettsbtnOpenEditImage();
            SetGArtenDatenToTabVerpackungenEdit();
        }
        ///<summary>ctrADR_List / ClearTabVerpackungEdit</summary>
        ///<remarks>Leert die Eingabefelder im TabVerpackungenEdit</remarks>
        private void ClearTabVerpackungEdit()
        {

        }
        ///<summary>ctrADR_List / SetGArtenDatenToTabVerpackungenEdit</summary>
        ///<remarks></remarks>
        private void SetGArtenDatenToTabVerpackungenEdit()
        {

        }
        ///<summary>ctrADR_List / tbBolzenME_Validated</summary>
        ///<remarks>Check Eingabe auf Decimal</remarks>
        private void tsbtnSaveVerpackungEdit_Click(object sender, EventArgs e)
        {

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
            ResetCtrGueterArtListeWidth();
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

            //decimal decTmp = 0;
            //Decimal.TryParse(Gut.Style.Length.ToString(), out decTmp);

        }
        ///<summary>ctrADR_List / SetStyleSheetDataToTabStyleSheet</summary>
        ///<remarks></remarks>
        private void AssignVarStyleSheet()
        {
            //decimal decTmpStyleID = Gut.Style.ID;

            //Gut.Style = new clsStyleSheetColumn();
            //Gut.Style._GL_User = this.GL_User;


        }
        ///<summary>ctrADR_List / SetStyleSheetDataToTabStyleSheet</summary>
        ///<remarks></remarks>
        private void tsbtnStyleDelete_Click(object sender, EventArgs e)
        {
            //if (clsMessages.StyleSheet_Delete())
            //{
            //    Gut.Style.Delete();
            //    ClearTabStyleSheet();
            //    InitTabStyleSheet();
            //    SetTabStyleSheetEingabefelderEnabled(false);
            //}
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
            SaveGutDaten();
            bUpdateGArtDaten = false;
        }
        ///<summary>ctrADR_List / tsbGArtenEditSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnGArtEditClose_Click(object sender, EventArgs e)
        {
            this.scGArt.Panel2Collapsed = true;
            ResetCtrGueterArtListeWidth();
        }
        /***************************************************************************************
         *                        tabPage WG
         * ************************************************************************************/
        ///<summary>ctrADR_List / tsbtnWGClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnWGClose_Click(object sender, EventArgs e)
        {
            this.scGArt.Panel2Collapsed = true;
            ResetCtrGueterArtListeWidth();
        }
        ///<summary>ctrADR_List / tsbtnWGSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnWGSave_Click(object sender, EventArgs e)
        {
            SaveGutDaten();
            bReiheUpdate = false;
        }
        ///<summary>frmGueterArten/ InitTabPageWG</summary>
        ///<remarks></remarks>
        private void InitTabPageWG()
        {
            MinMAxPanelExpand();
            ClearTabPageWGInputFields();
            this.scGArt.Panel2Collapsed = false;
            ResetCtrGueterArtListeWidth();
            SettsbtnOpenEditImage();
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

            tbGArtSearch.Enabled = bEnabled;
            btnGArt.Enabled = bEnabled;
            tbGArt.Enabled = bEnabled;

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

            tbGArtSearch.Text = string.Empty;
            tbGArt.Text = string.Empty;

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

            // MODUL MAXIMAL WERT für REIHEN ?
            //nudReiheOrderID.Maximum = (decimal)LagerOrt.Werk.Halle.maxOrderIDReihe;

            LagerOrt.Werk.Halle.Reihe.Init();
            //Datatable for dgvWerk
            if (LagerOrt.Werk.Halle.Reihe.dtReihe.Columns.Contains("Belegung"))
                LagerOrt.Werk.Halle.Reihe.dtReihe.Columns["Belegung"].SetOrdinal(5);
            this.dgvReihe.DataSource = LagerOrt.Werk.Halle.Reihe.dtReihe;
            this.dgvReihe.Columns["ID"].IsVisible = false;
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
            LagerOrt.Werk.Halle.Reihe.GArt.ID = TakeOver_ID;
            LagerOrt.Werk.Halle.Reihe.GArt.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System);
            LagerOrt.Werk.Halle.Reihe.GArt.Fill();
            SetGArtDatenFromSelectedGut();
        }

        private void SetGArtDatenFromSelectedGut()
        {
            tbGArtSearch.Text = LagerOrt.Werk.Halle.Reihe.GArt.ViewID;
            tbGArt.Text = LagerOrt.Werk.Halle.Reihe.GArt.Bezeichnung;
        }

        private void tbGArtSearch_TextChanged(object sender, EventArgs e)
        {
            //Güterarten laden
            DataTable dt = new DataTable();
            dt = clsGut.GetGArtenForCombo(this.GL_User.User_ID);
            string Filter = tbGArtSearch.Text.Trim();
            DataTable dtTmp = new DataTable();

            if (Filter != string.Empty)
            {
                dt.DefaultView.RowFilter = "ViewID ='" + Filter + "'";
                dtTmp = dt.DefaultView.ToTable();

                if (dtTmp.Rows.Count > 0)
                {
                    tbGArtSearch.Text = dtTmp.Rows[0]["ViewID"].ToString();
                    tbGArt.Text = dtTmp.Rows[0]["Bezeichnung"].ToString();
                    LagerOrt.Werk.Halle.Reihe.GArt.ID = (decimal)dtTmp.Rows[0]["ID"];

                    LagerOrt.Werk.Halle.Reihe.GArt.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System);
                }
                else
                {
                    tbGArt.Text = string.Empty;
                    LagerOrt.Werk.Halle.Reihe.GArt.ID = 0;
                }
            }
            else
            {
                tbGArt.Text = string.Empty;
                LagerOrt.Werk.Halle.Reihe.GArt.ID = 0;
            }
            SetLabelGArdIDInfo();
        }
        ///<summary>ctrEinlagerung / SetLabelGArdIDInfo</summary>
        ///<remarks>Set den GüterartID Info.</remarks>  
        private void SetLabelGArdIDInfo()
        {
            lGArtID.Text = _lTextGArtID + LagerOrt.Werk.Halle.Reihe.GArt.ID;
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
    }
}


