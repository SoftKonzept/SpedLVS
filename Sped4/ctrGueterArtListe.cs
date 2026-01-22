using LVS;
using Sped4.Classes;
using Sped4.Classes.TelerikCls;
using Sped4.Controls;
using Sped4.TelerikControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;

namespace Sped4
{
    public partial class ctrGueterArtListe : UserControl
    {
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;
        internal frmGArtenAuftragserfassung _frmGArtenAuftragserfassung;
        internal LVS.clsGut Gut;
        internal const string const_Headline_GArt = "Güterarten";
        internal const string const_Headline_WG = "Wargengruppen";
        internal const string const_ExportFileName_GArt = "Güterartenliste.xls";
        internal const string const_ExportFileName_WG = "Warengruppenliste.xls";
        public delegate void IDTakeOverEventHandler(decimal TakeOverID);
        public event IDTakeOverEventHandler getIDTakeOver;

        internal const string const_dgvGuterart_ColName_Matchcode = "ViewID";

        public delegate void frmGArtenAuftragsErfassungEventHandler();
        public event frmGArtenAuftragsErfassungEventHandler ClosefrmGArtenAuftragserfassung;

        public bool bUpdateGArtDaten = false;
        internal bool bUpdateStyleSheet = false;
        internal Int32 iListWidth = 0;
        internal Int32 iTabGArtEditWidth = 0;
        internal Int32 MenuItemListArt = 0;
        internal string GArtFilter = string.Empty;
        internal string FileExportName = string.Empty;
        public Int32 SearchButton = 0;
        public bool SearchMatchcode = false;
        public bool SearchGArt = false;//Standardsuche
        public decimal AdrIDForGArtAssignment = 0;
        public DataTable dtGArt = new DataTable();
        public DataTable tempTable = new DataTable();
        bool IsFirstLoad = true;
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
        public ctrGueterArtListe()
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

            if (SearchGArt)
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
            this.dgvGArtList.CustomSorting += new GridViewCustomSortingEventHandler(dgvGArtList_CustomSorting);
            this.cbProdNrCheck.Visible = this._ctrMenu._frmMain.system.Client.Modul.Lager_Einlagerung_Artikel_RequiredValue_Produktionsnummer;

            SetMenuGArtListeEnabled(!SearchGArt);
            //Headline
            this.GL_User = this._ctrMenu.GL_User;
            MinMAxPanelExpand();

            Gut = new clsGut();
            Gut.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System);
            Gut.sys = this._ctrMenu._frmMain.system;
            Gut.GutADR.FillDictionaries();
            MenuItemListArt = (Int32)this._ctrMenu._frmMain.system.AbBereich.ID;

            if ((this._frmGArtenAuftragserfassung != null) && (this._frmGArtenAuftragserfassung.AdrIDForGArtAssignment > 0))
            {
                this.AdrIDForGArtAssignment = this._frmGArtenAuftragserfassung.AdrIDForGArtAssignment;
                this.cbAdrGArtAssignmentFilter.Visible = false; //this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_UseGutAdrAssignment;
                this.cbAdrGArtAssignmentFilter.Checked = this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_UseGutAdrAssignment;
            }
            //Einheit
            cbEinheit.DataSource = clsEinheiten.GetEinheiten(this.GL_User);
            cbEinheit.DisplayMember = "Bezeichnung";
            cbEinheit.ValueMember = "Bezeichnung";
            InitDGV();

            string strTmpHeadLine = string.Empty;
            //für die Verwendung von Güterarten bzw. Warengruppen müssen hier die 
            //richtigen Tab ein-/bzw. ausgeblendet werden
            if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
            {
                //tabPageStyleSheetEdit muss noch ausgeblendet werden
                Functions.HideTabPage(ref tabGArtWG, "tabPageWG");
                Functions.HideTabPage(ref tabGArtWG, "tabPageStyleSheetEdit");
                strTmpHeadLine = const_Headline_GArt;
                FileExportName = const_ExportFileName_GArt;
                cbArtikelArt.DataSource = this.Gut.ListArtikelArt;
            }
            else
            {
                Functions.HideTabPage(ref tabGArtWG, "tabPageGArtEdit");
                Functions.HideTabPage(ref tabGArtWG, "tabPageStyleSheetEdit");
                strTmpHeadLine = const_Headline_WG;
                //Buttonbeschriftung muss geändert werden
                tsbtnListNeu.Text = "Neue Warengruppe anlegen";
                tsbtnListDelete.Text = "Warengruppe löschen";
                tsbtnListGArtDatenChange.Text = "Warengruppe ändern";
                tsbtnListGArtenListen.Text = "Warengruppenlisten";
                //Filename
                FileExportName = const_ExportFileName_WG;
            }
            afColorLabel1.myText = strTmpHeadLine;
            IsFirstLoad = false;
        }
        /// <summary>
        ///             SortResult: 
        ///             returns negative value when Row1 is before Row2, 
        ///             positive value if Row1 is after Row2 
        ///             and zero if the rows are have equal values in a specified column.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvGArtList_CustomSorting(object sender, GridViewCustomSortingEventArgs e)
        {
            //throw new NotImplementedException();
            if (this.dgvGArtList.Columns[const_dgvGuterart_ColName_Matchcode] != null)
            {
                string strRowvalue1 = e.Row1.Cells[const_dgvGuterart_ColName_Matchcode].Value.ToString();
                string strRowvalue2 = e.Row2.Cells[const_dgvGuterart_ColName_Matchcode].Value.ToString();
                e.SortResult = GridViewCustomerSort.GridViewCustomSorting(strRowvalue1, strRowvalue2, const_dgvGuterart_ColName_Matchcode, this.dgvGArtList.Columns[const_dgvGuterart_ColName_Matchcode].SortOrder);
            }
        }
        ///<summary>ctrGueterArtListe / SetMenuGArtListeEnabled</summary>
        ///<remarks></remarks>
        private void SetMenuGArtListeEnabled(bool bEnabled)
        {
            this.tsbtnOpenEdit.Enabled = bEnabled;
            this.tsbtnListNeu.Enabled = bEnabled;
            this.tsbtnListGArtDatenChange.Enabled = bEnabled;
            this.tsbtnCopy.Enabled = bEnabled;
            this.tsbtnListExcel.Enabled = bEnabled;
            this.tsbtnListRefresh.Enabled = bEnabled;
            this.tsbtnListDelete.Enabled = bEnabled;

            this.tsbClose.Enabled = true;
            this.tsbtnListGArtenListen.Enabled = bEnabled;
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
            tsbtnListGArtenListen.DropDownItems.Clear();
            DataTable dtArbeitsbereiche = clsArbeitsbereiche.GetArbeitsbereichList(this.GL_User.User_ID);
            //Alle Güterarten
            ToolStripMenuItem tsmItem = new ToolStripMenuItem("Alle");
            tsmItem.Tag = "0";
            tsmItem.Text = "Alle";
            tsmItem.Click += new EventHandler(TsddbtnGArtenListenItem_click);
            tsbtnListGArtenListen.DropDownItems.Add(tsmItem);

            tsmItem = new ToolStripMenuItem("aktiv");
            tsmItem.Tag = "-1";
            tsmItem.Text = "aktiv";
            tsmItem.Click += new EventHandler(TsddbtnGArtenListenItem_click);
            tsbtnListGArtenListen.DropDownItems.Add(tsmItem);

            tsmItem = new ToolStripMenuItem("deaktiviert");
            tsmItem.Tag = "-2";
            tsmItem.Text = "deaktiviert";
            tsmItem.Click += new EventHandler(TsddbtnGArtenListenItem_click);
            tsbtnListGArtenListen.DropDownItems.Add(tsmItem);

            for (Int32 i = 0; i <= dtArbeitsbereiche.Rows.Count - 1; i++)
            {
                //Liste der Arbeitsbereiche
                string strABName = dtArbeitsbereiche.Rows[i]["Arbeitsbereich"].ToString();
                tsmItem = new ToolStripMenuItem(strABName);
                tsmItem.Tag = dtArbeitsbereiche.Rows[i]["ID"].ToString();
                tsmItem.Text = strABName;
                tsmItem.Click += new EventHandler(TsddbtnGArtenListenItem_click);

                tsbtnListGArtenListen.DropDownItems.Add(tsmItem);
            }
        }
        ///<summary>ctrGueterArtListe / TsddbtnGArtenListenItem_click</summary>
        ///<remarks></remarks>
        private void TsddbtnGArtenListenItem_click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            ToolStripMenuItem tmpClicked = (ToolStripMenuItem)sender;
            Int32 iTmp = 0;
            Int32.TryParse(tmpClicked.Tag.ToString(), out iTmp);
            MenuItemListArt = iTmp;
            string strHeadline = string.Empty;
            if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
            {
                strHeadline = const_ExportFileName_GArt + " [" + tmpClicked.Text + "]";
            }
            else
            {
                strHeadline = const_ExportFileName_WG + " [" + tmpClicked.Text + "]";
            }
            afColorLabel1.myText = strHeadline;
            InitDGV();
        }
        ///<summary>ctrGueterArtListe / InitDGVGart</summary>
        ///<remarks></remarks>
        public void InitDGV()
        {
            this.dgvGArtList.EnableCustomSorting = (!this.cbSortSwitch.Checked);
            dtGArt.Clear();
            dtGArt = clsGut.GetGueterartenForList(this.GL_User, MenuItemListArt);
            this.dgvGArtList.DataSource = dtGArt;
            //clsClient.ctrGueterArtListe_DGVGueterArtenView(this._ctrMenu._frmMain.system.Client.MatchCode, ref this.dgvGArtList);
            GridViewCustomizedView.ctrGueterArtListe_DGVGueterArtenView(this._ctrMenu._frmMain.system.Client.MatchCode, ref this.dgvGArtList);
            if (this.dgvGArtList.Rows.Count > 0)
            {
                //for (Int32 i = 0; i <= this.dgvGArtList.Columns.Count - 1; i++)
                //{
                //    string colName = this.dgvGArtList.Columns[i].Name.ToString();

                //    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                //    {
                //        this.dgvGArtList.Columns[i].IsVisible = clsClient.ctrGueterArtListe_DGVGueterArtenView(this._ctrMenu._frmMain.system.Client.MatchCode,colName);
                //        //Güterarten
                //        switch (colName)
                //        {
                //            case "ID":
                //            case "Arbeitsbereich":
                //                //this.dgvGArtList.Columns[i].IsVisible = false;
                //                break;

                //            case "ViewID":
                //                this.dgvGArtList.Columns[i].HeaderText = "Matchcode";
                //                this.dgvGArtList.Columns.Move(i, 1);
                //                //this.dgvGArtList.Columns[i].IsVisible = true;
                //                break;

                //            case "Bezeichnung":
                //                this.dgvGArtList.Columns.Move(i, 2);
                //                //this.dgvGArtList.Columns[i].IsVisible = true;
                //                break;

                //            case "Name":
                //                this.dgvGArtList.Columns[i].HeaderText = "Auftraggeber";
                //                this.dgvGArtList.Columns.Move(i, 3);
                //                break;

                //            case "ArtikelArt":
                //                this.dgvGArtList.Columns[i].HeaderText = "Art";
                //                this.dgvGArtList.Columns.Move(i, 4);
                //                //this.dgvGArtList.Columns[i].IsVisible = true;
                //                break;

                //            case "Verweis":
                //                //this.dgvGArtList.Columns[i].IsVisible = true;
                //                if (this._ctrMenu._frmMain.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SZG + "_"))
                //                {
                //                    this.dgvGArtList.Columns[i].HeaderText = "Werksnummer";
                //                }
                //                this.dgvGArtList.Columns.Move(i, 5);
                //                break;

                //            case "Brutto":
                //                //this.dgvGArtList.Columns[i].IsVisible = false;
                //                break;

                //            case "IsStackable":
                //                this.dgvGArtList.Columns[i].HeaderText = "stapelbar";
                //                break;

                //            case "NichtStapelbar":
                //                this.dgvGArtList.Columns[i].HeaderText = "Nicht stapelbar";
                //                break;

                //            default:
                //                //this.dgvGArtList.Columns[i].IsVisible = false;
                //                break;
                //        }
                //        this.dgvGArtList.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells;
                //    }
                //    else
                //    {
                //        //Warengruppen
                //        switch (colName)
                //        {
                //            case "ViewID":
                //                this.dgvGArtList.Columns[i].HeaderText = "Matchcode";
                //                this.dgvGArtList.Columns.Move(i, 1);
                //                break;
                //            case "Bezeichnung":
                //                this.dgvGArtList.Columns.Move(i, 2);
                //                break;
                //            default:
                //                this.dgvGArtList.Columns[i].IsVisible = false;
                //                break;
                //        }
                //    }
                //}
                this.dgvGArtList.BestFitColumns();
                ResetCtrGueterArtListeWidth();

                //SetSelected and Current Row
                for (Int32 i = 0; i <= this.dgvGArtList.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(this.dgvGArtList.Rows[i].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp == Gut.ID)
                    {
                        this.dgvGArtList.Rows[i].IsSelected = true;
                        this.dgvGArtList.Rows[i].IsCurrent = true;
                    }
                }
            }
            //Filter verwenden bei der Suche
            SetFilterforDGV(ref this.dgvGArtList, (cbAdrGArtAssignmentFilter.Checked));
        }
        ///<summary>ctrGueterArtListe / SetFilterforDGV</summary>
        ///<remarks></remarks>
        public void SetFilterforDGV(ref RadGridView myDGV, bool bClearFilter)
        {
            myDGV.EnableFiltering = true;
            myDGV.FilterDescriptors.Clear();
            if (bClearFilter)
            {
                CompositeFilterDescriptor compositeFilter = new CompositeFilterDescriptor();
                //GArtID
                if ((this.Gut.GutADR.DictAdrListGArtenActiv.Count > 0) && (this.AdrIDForGArtAssignment > 0))
                {
                    List<decimal> tmpGutID = new List<decimal>();
                    if (this.Gut.GutADR.DictAdrListGArtenActiv.TryGetValue(this.AdrIDForGArtAssignment, out tmpGutID))
                    {
                        for (Int32 i = 0; i <= tmpGutID.Count - 1; i++)
                        {
                            if (compositeFilter.FilterDescriptors.Count > 0)
                            {
                                compositeFilter.LogicalOperator = FilterLogicalOperator.Or;
                            }
                            compositeFilter.FilterDescriptors.Add(new FilterDescriptor("ID", FilterOperator.IsEqualTo, tmpGutID[i]));
                            myDGV.FilterDescriptors.Add(compositeFilter);
                        }
                    }
                }
                else
                {
                    myDGV.FilterDescriptors.Clear();
                }
            }
        }
        ///<summary>ctrGueterArtListe / tabGArt_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tabGArt_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tmpTab = (TabControl)sender;
            InitSelectedTabPage(tmpTab.SelectedTab.Name.ToString());
        }
        ///<summary>ctrGueterArtListe / InitSelectedTabPage</summary>
        ///<remarks></remarks>
        private void InitSelectedTabPage(string myPageName)
        {
            switch (myPageName)
            {
                case "tabGArtWG":

                    break;
                case "tabPageAbBereichZuweisung":
                    InitLVAbBereich();
                    break;
                case "tabPageADRZuweisung":
                    InitDGVAdr();
                    break;
                case "tabPageGArtEdit":
                    InitTabGArtEdit();
                    //MinMaxPanel
                    MinMAxPanelExpand();
                    break;
                case "tabPageStyleSheetEdit":
                    break;
                case "tabPageWG":
                    InitTabPageWG();
                    break;
            }
        }
        ///<summary>ctrGueterArtListe / MinMAxPanelExpandonGArtenEdit</summary>
        ///<remarks></remarks>
        private void MinMAxPanelExpand()
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
            {
                this.mmPanelGArtEdit.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                this.mmPanelGArtenVerpackungen.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
            }
            else
            {
                this.mmpWGEdit.SetExpandCollapse(AFMinMaxPanel.EStatus.Collapsed);
            }
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
            //InitTabGArtEingabeFelder();
            InitTabGArtEdit();
        }
        ///<summary>ctrGueterArtListe / tsbtnVerpackungsdatenChange_Click</summary>
        ///<remarks>TabVerpackungenEdit anzeigen</remarks>
        private void tsbGArtDatenChange_Click(object sender, EventArgs e)
        {
            bUpdateGArtDaten = true;
            if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
            {
                this.tabGArtWG.SelectedTab = tabPageGArtEdit;
                //InitTabGArtEingabeFelder();
                InitTabGArtEdit();
            }
            else
            {
                this.tabGArtWG.SelectedTab = tabPageWG;
                //InitTabGArtEingabeFelder();
                InitTabGArtEdit();
            }
            scGArt.Panel2Collapsed = false;
            ResetCtrGueterArtListeWidth();
        }
        ///<summary>ctrGueterArtListe / miCloseCtr_Click</summary>
        ///<remarks></remarks>
        private void miCloseCtr_Click(object sender, EventArgs e)
        {
            if (this._frmGArtenAuftragserfassung != null)
            {
                ClosefrmGArtenAuftragserfassung();
            }
            else
            {
                Int32 Count = this.ParentForm.Controls.Count;

                for (Int32 i = 0; (i <= (Count - 1)); i++)
                {
                    if (this.ParentForm.Controls[i].Name == "TempSplitterGut")
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        //i = Count - 1;      // ist nur ein Controll vorhanden
                    }
                    if (this.ParentForm.Controls[i].GetType() == typeof(ctrGueterArtListe))
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        i = Count - 1;      // ist nur ein Controll vorhanden
                    }
                }
            }
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
            if (this.dgvGArtList.Rows.Count > 0)
            {
                if (e.RowIndex > -1)
                {
                    decimal decGArtID = 0;
                    //string strTmp = this.dgvGArtList.Rows[this.dgvGArtList.CurrentRow.Index].Cells["ID"].Value.ToString();
                    string strTmp = this.dgvGArtList.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                    if (Decimal.TryParse(strTmp, out decGArtID))
                    {
                        Gut.ID = decGArtID;
                        Gut.Fill();
                    }
                }
            }
        }
        ///<summary>ctrGueterArtListe / dgvGArtList_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvGArtList_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (Gut.ID > 0)
            {
                //Unterscheidung Suche und Bearbeitung der Daten
                if (SearchGArt)
                {
                    getIDTakeOver(Gut.ID);
                    ClosefrmGArtenAuftragserfassung();
                }
                else
                {
                    this.scGArt.Panel2Collapsed = false;
                    ResetCtrGueterArtListeWidth();
                    bUpdateGArtDaten = true;
                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                    {
                        tabGArtWG.SelectedTab = tabPageGArtEdit;
                        InitSelectedTabPage(tabPageGArtEdit.Name);
                    }
                    else
                    {
                        tabGArtWG.SelectedTab = tabPageWG;
                        InitSelectedTabPage(tabPageWG.Name);
                    }
                }
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
                this.tabGArtWG.SelectedTab = tabPageGArtEdit;
                //InitTabGArtEingabeFelder();
                InitTabGArtEdit();
                ClearTabGArtEdit();
                tbViewID.Focus();
            }
            else
            {
                this.tabGArtWG.SelectedTab = tabPageWG;
                InitTabPageWG();
                ClearTabPageWGInputFields();
                tbWGMatchcode.Focus();
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
            DeleteGArtenDaten();
            ShowAndHideTabPage();
        }
        ///<summary>ctrGueterArtListe / DeleteGArtenDaten</summary>
        ///<remarks>Gewählten Datensatz löschen</remarks>
        private void DeleteGArtenDaten()
        {
            if (GL_User.write_Gut)
            {
                if (this.dgvGArtList.Rows.Count > 0)
                {
                    decimal decTmp = 0;
                    string strTmp = this.dgvGArtList.Rows[this.dgvGArtList.CurrentRow.Index].Cells["ID"].Value.ToString();
                    Decimal.TryParse(strTmp, out decTmp);

                    //ID 1 ist ViewID 0 alle Güter
                    if (decTmp > 1)
                    {
                        Gut.ID = decTmp;
                        Gut.Fill();
                        if (Gut.GArtIsUsed)
                        {
                            clsMessages.Gut_GueterartIsInUse();
                            Gut.Aktiv = false;
                            Gut.UpdateGueterArt();
                            Gut.Fill();
                        }
                        else
                        {
                            if (clsMessages.DeleteAllgemein())
                            {
                                Gut.Delete();
                                if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                                {

                                }
                                else
                                {
                                    ClearTabPageWGInputFields();
                                }
                            }
                        }
                    }
                    InitDGV();
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        ///<summary>frmGueterArten/ tsbtnListExcel_Click</summary>
        ///<remarks></remarks>
        private void tsbtnListExcel_Click(object sender, EventArgs e)
        {
            DoExcelExport();
        }
        ///<summary>frmGueterArten/ DoExcelExport</summary>
        ///<remarks></remarks>
        private void DoExcelExport()
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

            Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgvGArtList, strFileName, ref openExportFile, this.GL_User, true);

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
        ///<summary>ctrGueterArtList/ txtSearch_TextChanged</summary>
        ///<remarks>Suche im Grid Güterarten</remarks>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (cbGArt.Checked)
            {
                SearchGrd(txtSearch.Text.ToUpper());
            }
            else
            {
                if (txtSearch.Text.ToString() == "")
                {
                    InitDGV();
                }
                else
                {
                    string SearchText = txtSearch.Text.ToString();
                    string strSuchSpalte = string.Empty;
                    if (cbMC.Checked == true)
                    {
                        strSuchSpalte = "ViewID";
                    }
                    GArtFilter = strSuchSpalte + " LIKE '%" + SearchText + "%'";
                    dtGArt.DefaultView.RowFilter = GArtFilter;
                }
            }
        }
        ///<summary>ctrGueterArtList / SearchGrdADRList</summary>
        ///<remarks></remarks>
        private void SearchGrd(string Search)
        {
            dtGArt = clsGut.GetGueterartenForList(this.GL_User, MenuItemListArt);
            //Spalte hinzufügen
            DataColumn col1 = dtGArt.Columns.Add("Find", typeof(Boolean));
            bool isFound = false;
            if (Search.ToString() == "")
            {
                InitDGV();
            }
            else
            {
                if (Convert.ToBoolean(Search.Length))
                {
                    // If the item is not found and you haven't looked at every cell, keep searching
                    //while ((!isFound) & (idx < maxSearches))
                    for (Int32 _Row = 0; _Row <= dtGArt.Rows.Count - 1; _Row++)
                    {
                        for (Int32 _Column = 1; _Column <= dtGArt.Columns.Count - 1; _Column++)
                        {
                            // Do all comparing in UpperCase so it is case insensitive
                            //if (grdADRList[_Column, _Row].Value.ToString().ToUpper().Contains(Search))
                            if (dtGArt.Rows[_Row][_Column].ToString().ToUpper().Contains(Search))
                            {
                                // If found position on the item
                                //grdADRList.FirstDisplayedScrollingRowIndex = _Row;
                                //grdADRList[_Column, _Row].Selected = true;
                                string test = dtGArt.Rows[_Row][_Column].ToString().ToUpper().Contains(Search).ToString();
                                isFound = true;
                                _Column = dtGArt.Columns.Count;
                            }

                        }
                        dtGArt.Rows[_Row]["Find"] = isFound;
                        isFound = false;
                    }
                }
                string Ausgabe = string.Empty;
                DataRow[] rows = dtGArt.Select("Find =true", "Find");
                tempTable.Clear();
                tempTable = dtGArt.Clone();
                foreach (DataRow row in rows)
                {
                    Ausgabe = Ausgabe + row["Find"].ToString() + "\n";
                    tempTable.ImportRow(row);
                }
                tempTable.Columns.Remove("Find");
                dtGArt.Columns.Remove("Find");
                dtGArt.Clear();
                dtGArt = tempTable;
                dgvGArtList.DataSource = dtGArt;
            }
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
            ////breite neu ermitteln
            iListWidth = 30;
            for (Int32 i = 0; i <= this.dgvGArtList.Columns.Count - 1; i++)
            {
                if (this.dgvGArtList.Columns[i].IsVisible)
                {
                    if (iListWidth < 500)
                    {
                        iListWidth = iListWidth + this.dgvGArtList.Columns[i].Width;
                    }
                    else
                    {
                        iListWidth = 500;
                    }
                }
            }

            if (this.scGArt.Panel2Collapsed)
            {
                this.Width = iListWidth + 10;
                //this.scGArt.Panel1.Width = this.Width;
            }
            else
            {
                this.Width = iListWidth + iTabGArtEditWidth + 10;
                this.scGArt.SplitterDistance = iListWidth + 20;
            }
            this.Refresh();
        }
        ///<summary>frmGueterArten/ InitTabGArtEingabeFelder</summary>
        ///<remarks></remarks>
        //private void InitTabGArtEingabeFelder()
        //{

        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MmPanelGArtenVerpackungen_SizeChanged(object sender, EventArgs e)
        {
            //string size = string.Empty;
            //if ((this.Gut is clsGut) && (this.Gut.ID > 0))
            //{
            //    InitTabGArtEingabeFelder();
            //}
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

            //TabVerpackungEdit
            InitTabVerpackungEdit();
        }
        ///<summary>frmGueterArten/ ResetEnableTbBox</summary>
        ///<remarks>Daten auf das Ctr in die Eingabefelder setzen</remarks>
        private void SetGArtenWGDatenToTab()
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
            {
                //tabGArtEdit
                this.cbActiv.Checked = Gut.Aktiv;
                this.tbViewID.Text = Gut.ViewID;
                this.tbBezeichung.Text = Gut.Bezeichnung;
                this.tbZusatz.Text = Gut.Zusatz;
                this.nudMassAnzahl.Value = Convert.ToDecimal(Gut.MassAnzahl);
                this.nudBreite.Value = Gut.Breite;
                this.nudDicke.Value = Gut.Dicke;
                this.nudLaenge.Value = Gut.Laenge;
                this.nudHoehe.Value = Gut.Hoehe;
                this.nudNetto.Value = Gut.Netto;
                this.nudBrutto.Value = Gut.Brutto;
                this.tbBesonderheit.Text = Gut.Besonderheit;
                Functions.SetComboToSelecetedValue(ref cbEinheit, Gut.Einheit);
                //muss hier auch mit rein
                Functions.SetComboToSelecetedValue(ref cbArtikelArt, Gut.ArtikelArt);
                this.tbVerweis.Text = Gut.Verweis;
                this.tbWerksnummer.Text = Gut.Werksnummer;

                this.nudLieferantenID.Value = Gut.LieferantenID;
                this.nudMinBestand.Value = Gut.MindestBestand;
                this.tbBestellNr.Text = Gut.BestellNr;
                this.tbDelforVerweis.Text = Gut.DelforVerweis;
                this.cbIsNOTStackable.Checked = (!Gut.IsStackable);
                this.cbProdNrCheck.Checked = Gut.UseProdNrCheck;
                this.cbIgnoreEdi.Checked = Gut.IgnoreEdi;
                //this.tbWerksnummer.Text = Gut.Werksnummer;
            }
            else
            {
                //tabGArtEdit
                this.tbWGMatchcode.Text = Gut.ViewID;
                this.tbWGBezeichnung.Text = Gut.Bezeichnung;
                this.tbWGBemerkung.Text = Gut.Besonderheit;
                this.cbWGAktiv.Checked = Gut.Aktiv;
            }
        }
        ///<summary>frmGueterArten/ ResetEnableTbBox</summary>
        ///<remarks></remarks>
        private void ClearTabGArtEdit()
        {
            tbBezeichung.Text = string.Empty;
            //tbViewID.Text = string.Empty;
            //--- Vorschlag für neue Güterart (nummerisch)
            tbViewID.Text = clsGut.GetViewIDOffer(this._ctrMenu._frmMain.GL_User);

            tbBezeichung.Text = string.Empty;
            tbZusatz.Text = string.Empty;
            nudMassAnzahl.Value = 4;
            nudDicke.Value = 0;
            nudBreite.Value = 0;
            nudHoehe.Value = 0;
            nudLaenge.Value = 0;
            if (cbEinheit.Items.Count > 0)
            {
                cbEinheit.SelectedIndex = 0;
            }
            nudNetto.Value = 0;
            nudBrutto.Value = 0;
            tbBesonderheit.Text = string.Empty;
            tbWerksnummer.Text = string.Empty;
            tbVerweis.Text = string.Empty;
            tbDelforVerweis.Text = string.Empty;
            cbActiv.Checked = true;

            //Zusatz Angaben
            nudLieferantenID.Value = 0;
            nudMinBestand.Value = 0;
            tbBestellNr.Text = string.Empty;
            this.cbIsNOTStackable.Checked = false;
            this.cbProdNrCheck.Checked = true;
            this.cbIgnoreEdi.Checked = false;

            SetTabGArtEditEingabeFelderEnabled(true, (Int32)nudMassAnzahl.Value);
        }
        ///<summary>frmGueterArten/ tsbtnGArtenEditDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnGArtenEditDelete_Click(object sender, EventArgs e)
        {
            DeleteGArtenDaten();
        }
        ///<summary>frmGueterArten/ tsbSpeichern_Click</summary>
        ///<remarks></remarks>
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            SaveGutDaten();
            bUpdateGArtDaten = false;
        }
        ///<summary>frmGueterArten/ SaveGutDaten</summary>
        ///<remarks></remarks>
        private void SaveGutDaten()
        {
            if (GL_User.write_Gut)
            {
                if (
                        (
                            (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition) &&
                            (!tbViewID.Text.Equals(string.Empty))
                        )
                        ||
                        (
                            (!this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition) &&
                            (!tbWGMatchcode.Text.Equals(string.Empty))
                        )
                   )
                {
                    decimal decTmpGutID = Gut.ID;
                    Gut = new clsGut();
                    Gut.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System);

                    if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
                    {
                        //Daten GArtenEdit
                        Gut.Aktiv = cbActiv.Checked;
                        Gut.Bezeichnung = tbBezeichung.Text;
                        Gut.ViewID = tbViewID.Text;
                        Gut.Dicke = nudDicke.Value;
                        Gut.Breite = nudBreite.Value;
                        Gut.Laenge = nudLaenge.Value;
                        Gut.Hoehe = nudHoehe.Value;
                        Gut.MassAnzahl = (Int32)nudMassAnzahl.Value;
                        Gut.Netto = nudNetto.Value;
                        Gut.Brutto = nudBrutto.Value;
                        Gut.LieferantenID = nudLieferantenID.Value;
                        Gut.MindestBestand = nudMinBestand.Value;
                        Gut.BestellNr = tbBestellNr.Text;
                        Gut.IsStackable = (!this.cbIsNOTStackable.Checked);
                        Gut.UseProdNrCheck = this.cbProdNrCheck.Checked;
                        Gut.Besonderheit = tbBesonderheit.Text;
                        Gut.Zusatz = tbZusatz.Text;
                        Gut.Einheit = cbEinheit.Text;
                        Gut.Verweis = tbVerweis.Text;
                        Gut.Werksnummer = tbWerksnummer.Text;

                        //Daten VerpackungEdit
                        Gut.ArtikelArt = cbArtikelArt.Text;
                        Int32 iTmp = 0;
                        string strTmp = nudBolzenME.Value.ToString();
                        Int32.TryParse(strTmp, out iTmp);
                        Gut.MEAbsteckBolzen = iTmp;
                        Gut.AbsteckBolzenNr = tbBolzenNr.Text;
                        Gut.Verpackung = tbVerpackung.Text;
                        Gut.DelforVerweis = tbDelforVerweis.Text;
                        Gut.IgnoreEdi = cbIgnoreEdi.Checked;
                    }
                    else
                    {
                        Gut.Aktiv = this.cbWGAktiv.Checked;
                        Gut.Bezeichnung = this.tbWGBezeichnung.Text;
                        Gut.ViewID = this.tbWGMatchcode.Text;
                        decimal decDefault = 0;
                        Int32 iDefault = 0;
                        Gut.Dicke = decDefault;
                        Gut.Breite = decDefault;
                        Gut.Laenge = decDefault;
                        Gut.Hoehe = decDefault;
                        Gut.MassAnzahl = iDefault;
                        Gut.Netto = decDefault;
                        Gut.Brutto = decDefault;
                        Gut.LieferantenID = decDefault;
                        Gut.MindestBestand = decDefault;
                        Gut.BestellNr = string.Empty;
                        Gut.Besonderheit = this.tbWGBemerkung.Text;
                        Gut.Zusatz = string.Empty;
                        Gut.Einheit = string.Empty;
                        Gut.Verweis = tbVerweis.Text;
                        Gut.Werksnummer = tbWerksnummer.Text;

                        //Daten VerpackungEdit
                        Gut.ArtikelArt = string.Empty;
                        Gut.MEAbsteckBolzen = iDefault;
                        Gut.AbsteckBolzenNr = string.Empty;
                        Gut.Verpackung = string.Empty;
                        Gut.DelforVerweis = tbDelforVerweis.Text;
                        Gut.IgnoreEdi = cbIgnoreEdi.Checked;
                    }

                    Gut.ArbeitsbereichID = this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID;
                    if (bUpdateGArtDaten)
                    {
                        Gut.ID = decTmpGutID;
                        Gut.UpdateGueterArt();
                    }
                    else
                    {
                        Gut.Add();
                    }
                    InitDGV();
                    ClearTabGArtEdit();
                    this.scGArt.Panel2Collapsed = true;
                    ResetCtrGueterArtListeWidth();
                }
                else
                {
                    string strTxt = "Das Feld Suchbegriff ist nicht ausgefüllt!";
                    clsMessages.Allgemein_ERRORTextShow(strTxt);
                    tbViewID.Focus();
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        ///<summary>ctrADR_List / tbViewID_Validated</summary>
        ///<remarks>Bei Eingabe werden alle anderen Eingabefelder freigegeben</remarks>
        private void tbViewID_Validated(object sender, EventArgs e)
        {
            if (tbViewID.Text == string.Empty)
            {
                ClearTabGArtEdit();
            }
            else
            {
                nudMassAnzahl.Focus();
            }
            SetTabGArtEditEingabeFelderEnabled(!(tbViewID.Text == string.Empty), (Int32)nudMassAnzahl.Value);
        }
        ///<summary>ctrADR_List / SetTabGArtEditEingabeFelderEnabled</summary>
        ///<remarks></remarks>
        private void SetTabGArtEditEingabeFelderEnabled(bool bEnabled, Int32 iMassZahl)
        {
            Int32 i = iMassZahl;
            tbViewID.Enabled = bEnabled;
            tbBezeichung.Enabled = bEnabled;
            tbZusatz.Enabled = bEnabled;
            tbBesonderheit.Enabled = bEnabled;
            tbWerksnummer.Enabled = bEnabled;
            nudMassAnzahl.Enabled = bEnabled;

            nudLieferantenID.Enabled = bEnabled;
            nudMinBestand.Enabled = bEnabled;
            tbBestellNr.Enabled = bEnabled;
            tbDelforVerweis.Enabled = bEnabled;
            this.cbIsNOTStackable.Enabled = bEnabled;
            this.cbProdNrCheck.Enabled = bEnabled;

            switch (i)
            {
                case 0:
                    nudBreite.Enabled = !bEnabled;
                    nudDicke.Enabled = !bEnabled;
                    nudLaenge.Enabled = !bEnabled;
                    nudHoehe.Enabled = !bEnabled;
                    break;
                case 1:
                    nudBreite.Enabled = bEnabled;
                    nudDicke.Enabled = !bEnabled;
                    nudLaenge.Enabled = !bEnabled;
                    nudHoehe.Enabled = !bEnabled;
                    break;
                case 2:
                    nudBreite.Enabled = bEnabled;
                    nudDicke.Enabled = bEnabled;
                    nudLaenge.Enabled = !bEnabled;
                    nudHoehe.Enabled = !bEnabled;
                    break;

                case 3:
                    nudBreite.Enabled = bEnabled;
                    nudDicke.Enabled = bEnabled;
                    nudLaenge.Enabled = bEnabled;
                    nudHoehe.Enabled = !bEnabled;
                    break;

                case 4:
                    nudBreite.Enabled = bEnabled;
                    nudDicke.Enabled = bEnabled;
                    nudLaenge.Enabled = bEnabled;
                    nudHoehe.Enabled = bEnabled;
                    break;
                default:
                    nudBreite.Enabled = bEnabled;
                    nudDicke.Enabled = bEnabled;
                    nudLaenge.Enabled = bEnabled;
                    nudHoehe.Enabled = bEnabled;
                    break;
            }
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
            //cbArtikelArt.Text = string.Empty;
            cbArtikelArt.SelectedIndex = -1;
            tbVerpackung.Text = string.Empty;
            tbBolzenNr.Text = string.Empty;
            nudBolzenME.Value = Convert.ToDecimal("0");
        }
        ///<summary>ctrADR_List / SetGArtenDatenToTabVerpackungenEdit</summary>
        ///<remarks></remarks>
        private void SetGArtenDatenToTabVerpackungenEdit()
        {
            //cbArtikelArt.Text = Gut.ArtikelArt;
            //Functions.SetComboToSelecetedText(ref cbArtikelArt, Gut.ArtikelArt);
            Functions.SetComboToSelecetedValue(ref cbArtikelArt, Gut.ArtikelArt);
            tbVerpackung.Text = Gut.Verpackung;
            tbBolzenNr.Text = Gut.AbsteckBolzenNr;
            nudBolzenME.Value = Convert.ToDecimal(Gut.MEAbsteckBolzen);
        }
        ///<summary>ctrADR_List / tbBolzenME_Validated</summary>
        ///<remarks>Check Eingabe auf Decimal</remarks>
        private void tsbtnSaveVerpackungEdit_Click(object sender, EventArgs e)
        {
            bUpdateGArtDaten = true;
            SaveGutDaten();
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
            tbStyleBezeichnung.Enabled = bEnabled;
            tbStyleTyp.Enabled = bEnabled;
            cbStyleTableToFormat.Enabled = bEnabled;
            cbStyleColToFormat.Enabled = bEnabled;
            nudStyleLength.Enabled = bEnabled;
            cbStyleCutLength.Enabled = bEnabled;
            tbStyleBeschreibung.Enabled = bEnabled;
        }
        ///<summary>ctrADR_List / ClearTabStyleSheet</summary>
        ///<remarks></remarks>
        private void ClearTabStyleSheet()
        {
            tbStyleBezeichnung.Text = string.Empty;
            tbStyleTyp.Text = string.Empty;
            cbStyleTableToFormat.SelectedIndex = 0;
            cbStyleColToFormat.SelectedIndex = 0;
            nudStyleLength.Value = 0;
            cbStyleCutLength.Checked = false;
            tbStyleBeschreibung.Text = string.Empty;
        }
        ///<summary>ctrADR_List / tsbtnCloseFormat_Click</summary>
        ///<remarks>Check Eingabe auf Decimal</remarks>
        private void InitTabStyleSheet()
        {
            Gut.Fill();
            this.dgvStyleSheet.DataSource = Gut.Style.dtStyleSheet;
            if (this.dgvStyleSheet.Rows.Count > 0)
            {
                this.dgvStyleSheet.Columns["ID"].IsVisible = false;
                this.dgvStyleSheet.BestFitColumns();

                //Selected Row auf ADR.Kontakt.ID setzen
                for (Int32 i = 0; i <= this.dgvStyleSheet.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    //strTmp = string.Empty;
                    string strTmp = this.dgvStyleSheet.Rows[i].Cells["ID"].Value.ToString();
                    Decimal.TryParse(strTmp, out decTmp);
                    if (Gut.Style.ID == decTmp)
                    {
                        this.dgvStyleSheet.Rows[i].IsSelected = true;
                        this.dgvStyleSheet.Rows[i].IsCurrent = true;
                    }
                }
            }
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
            string strTmp = this.dgvStyleSheet.Rows[e.RowIndex].Cells["ID"].Value.ToString();
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                Gut.Style.ID = decTmp;
                Gut.Style.Fill();
            }
        }
        ///<summary>ctrADR_List / dgvStyleSheet_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvStyleSheet_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            SetStyleSheetDataToTabStyleSheet();
            SetTabStyleSheetEingabefelderEnabled(true);
            tsbtnStyleDelete.Enabled = true;
        }
        ///<summary>ctrADR_List / SetStyleSheetDataToTabStyleSheet</summary>
        ///<remarks></remarks>
        private void SetStyleSheetDataToTabStyleSheet()
        {
            tbStyleBezeichnung.Text = Gut.Style.Bezeichnung;
            tbStyleTyp.Text = Gut.Style.Typ;
            cbStyleTableToFormat.Text = Gut.Style.TableToFormat;
            cbStyleColToFormat.Text = Gut.Style.ColToFormat;
            decimal decTmp = 0;
            Decimal.TryParse(Gut.Style.Length.ToString(), out decTmp);
            nudStyleLength.Value = decTmp;
            cbStyleCutLength.Checked = Gut.Style.CutLength;
            tbStyleBeschreibung.Text = Gut.Style.Beschreibung;
        }
        ///<summary>ctrADR_List / SetStyleSheetDataToTabStyleSheet</summary>
        ///<remarks></remarks>
        private void AssignVarStyleSheet()
        {
            decimal decTmpStyleID = Gut.Style.ID;

            Gut.Style = new clsStyleSheetColumn();
            Gut.Style._GL_User = this.GL_User;

            Gut.Style.Bezeichnung = tbStyleBezeichnung.Text;
            Gut.Style.Typ = tbStyleTyp.Text;
            Gut.Style.TableToFormat = cbStyleTableToFormat.Text;
            Gut.Style.ColToFormat = cbStyleColToFormat.Text;
            Int32 iTmp = 0;
            Int32.TryParse(nudStyleLength.Value.ToString(), out iTmp);
            Gut.Style.Length = iTmp;
            Gut.Style.CutLength = cbStyleCutLength.Checked;
            Gut.Style.Beschreibung = tbStyleBeschreibung.Text;

            if (bUpdateStyleSheet)
            {
                Gut.Style.ID = decTmpStyleID;
                Gut.Style.Update();
            }
            else
            {
                Gut.Style.Add();
            }
        }
        ///<summary>ctrADR_List / SetStyleSheetDataToTabStyleSheet</summary>
        ///<remarks></remarks>
        private void tsbtnStyleDelete_Click(object sender, EventArgs e)
        {
            if (clsMessages.StyleSheet_Delete())
            {
                Gut.Style.Delete();
                ClearTabStyleSheet();
                InitTabStyleSheet();
                SetTabStyleSheetEingabefelderEnabled(false);
                tsbtnStyleDelete.Enabled = false;
            }
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
        ///<summary>ctrADR_List / button1_Click</summary>
        ///<remarks></remarks>
        private void button1_Click(object sender, EventArgs e)
        {
            string strTextValue = tbStyleBezeichnung.Text;
            RadMaskedEditBox tbMask = new RadMaskedEditBox();
            tbMask.MaskType = MaskType.Standard;
            tbMask.Mask = tbStyleTyp.Text;
            tbMask.PromptChar = ' ';
            tbMask.Value = strTextValue;
            tbTest.Text = tbMask.Value.ToString();
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
            bUpdateGArtDaten = false;
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
            this.tbWGMatchcode.Text = string.Empty;
            this.tbWGBezeichnung.Text = string.Empty;
            this.tbWGBemerkung.Text = string.Empty;
            this.cbWGAktiv.Checked = true;
        }
        ///<summary>ctrGArtenListe/InitLVAbBereich</summary>
        ///<remarks></remarks>
        private void InitLVAbBereich()
        {
            lvAbBereiche.ShowCheckBoxes = true;
            DataTable dtAbBereich = clsArbeitsbereiche.GetArbeitsbereichList(this.GL_User.User_ID);
            this.lvAbBereiche.Items.Clear();

            foreach (DataRow row in dtAbBereich.Rows)
            {
                string ArbeitsbereichName = row["Arbeitsbereich"].ToString();
                decimal decTmp = 0;
                if (Decimal.TryParse(row["ID"].ToString(), out decTmp))
                {
                    clsArbeitsbereichGArten tmpAssign = new clsArbeitsbereichGArten();
                    tmpAssign.AbBereichID = decTmp;
                    tmpAssign.GArtID = this.Gut.ID;
                    bool bIsAssin = tmpAssign.IsAssign;

                    ListViewDataItem Item = new ListViewDataItem();
                    Item.Tag = (object)tmpAssign;
                    Item.Key = decTmp;
                    Item.Text = ArbeitsbereichName;
                    Item.Value = ArbeitsbereichName;
                    if (bIsAssin)
                    {
                        Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                    }
                    else
                    {
                        Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                    }
                    Item.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
                    lvAbBereiche.Items.Add(Item);
                }
            }
        }
        ///<summary>ctrGArtenListe/lvAbBereiche_ItemCheckedChanged</summary>
        ///<remarks></remarks>
        private void lvAbBereiche_ItemCheckedChanged(object sender, ListViewItemEventArgs e)
        {
            //ListViewDataItem Item = (ListViewDataItem)e.Item;
            //string strTest = Item.CheckState.ToString();            
            clsArbeitsbereichGArten AbGArtAssign = (clsArbeitsbereichGArten)((ListViewDataItem)e.Item).Tag;
            if (AbGArtAssign.IsAssign)
            {
                //Daten in DB ArbeitsbereichTarif löschen
                AbGArtAssign.Delete();
            }
            else
            {
                //Daten in DB ArbeitsbereichTarif hinzufügen
                AbGArtAssign.ID = 0;
                AbGArtAssign.Add();
            }
            InitLVAbBereich();
        }
        ///<summary>ctrGArtenListe/lvAbBereiche_ItemCheckedChanged</summary>
        ///<remarks></remarks>
        private void tsbtnCopy_Click(object sender, EventArgs e)
        {
            if (this.Gut.ID > 0)
            {
                clsGut CopyGut = this.Gut.Copy();
                CopyGut.ID = 0;
                CopyGut.ViewID = CopyGut.ViewID + "_Kopie";
                CopyGut.Bezeichnung = CopyGut.Bezeichnung + "_Kopie";
                CopyGut.Add();
                InitDGV();
            }
        }
        ///<summary>ctrGArtenListe/InitDGVdAdr</summary>
        ///<remarks></remarks>
        private void InitDGVAdr()
        {
            this.dgvADR.DataSource = this.Gut.GutADR.GetAssignADRTable();
            foreach (GridViewColumn col in this.dgvADR.Columns)
            {
                switch (col.Name)
                {
                    case "AdrID":
                        col.IsVisible = false;
                        break;
                    case "ViewID":
                        col.HeaderText = "Matchcode";
                        col.Width = (Int32)(this.dgvADR.Width * 0.3);
                        break;
                    case "Name1":
                        col.HeaderText = "Name";
                        col.Width = (Int32)(this.dgvADR.Width * 0.7);
                        break;
                    default:
                        break;
                }
            }
            if (this.dgvADR.Rows.Count > 0)
            {
                this.dgvADR.Rows[0].IsCurrent = true;
                decimal decTmp = 0;
                Decimal.TryParse(this.dgvADR.Rows[0].Cells["AdrID"].Value.ToString(), out decTmp);
                if (decTmp > 0)
                {
                    this.Gut.GutADR.AdrID = decTmp;
                }
            }
            this.tsbtnAdrDelete.Enabled = this.dgvADR.Rows.Count > 0;
        }
        ///<summary>ctrGArtenListe/dgvADR_CellClick</summary>
        ///<remarks></remarks>
        private void dgvADR_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                decimal decTmp = 0;
                Decimal.TryParse(this.dgvADR.Rows[e.RowIndex].Cells["AdrID"].Value.ToString(), out decTmp);
                if (decTmp > 0)
                {
                    this.Gut.GutADR.AdrID = decTmp;
                }
            }
        }
        ///<summary>ctrGArtenListe/tsbtnAdrDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAdrDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvADR.Rows.Count > 0)
            {
                this.Gut.GutADR.Delete();
                InitDGVAdr();
            }
        }
        ///<summary>ctrGArtenListe/tsbtnAddADR_Click</summary>
        ///<remarks>Öffnen der Adressauswahl</remarks>
        private void tsbtnAddADR_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrGArtenListe/TakeOverAdrID</summary>
        ///<remarks></remarks>
        public void TakeOverAdrID(decimal myDecADR_ID)
        {
            if (myDecADR_ID > 0)
            {
                if (!this.Gut.GutADR.ListAssignADR.Contains(myDecADR_ID))
                {
                    this.Gut.GutADR.AdrID = myDecADR_ID;
                    this.Gut.GutADR.Add();
                    InitDGVAdr();
                }
            }
        }
        ///<summary>ctrGArtenListe/tsbtnRefreshADR_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefreshADR_Click(object sender, EventArgs e)
        {
            InitDGVAdr();
        }
        ///<summary>ctrGArtenListe/cbAdrGArtAssignmentFilter_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbAdrGArtAssignmentFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsFirstLoad)
            {
                InitDGV();
            }
        }
        /// <summary>
        ///             Switch von nummerische auf alphanumerische Sortierung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSortSwitch_CheckedChanged(object sender, EventArgs e)
        {
            this.dgvGArtList.EnableCustomSorting = (!cbSortSwitch.Checked);
            InitDGV();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbArtikelArt_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = string.Empty;
        }
    }
}


