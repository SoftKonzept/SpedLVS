using LVS;
using LVS.ViewData;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrInventory : UserControl
    {
        internal const string dgvViewInventur_KategorieName = "Inventur";
        internal const string dgvViewInventurArticle_KategorieName = "InventurArtikel";
        public const string const_ControlName = "Inventuren";
        const string const_Headline = "Inventuren";
        public Globals._GL_USER GL_User;
        internal ctrMenu _ctrMenu;
        //internal Inventories Inventory;
        internal Common.Enumerations.enumInventoryArt InventoryArtFilter;
        internal Common.Enumerations.enumInventoryStatus InventoryStatus;
        internal Common.Enumerations.enumInventoryArticleStatus InventoryArticleStatusFilter;
        public delegate void ThreadCtrInventoryHandler();

        private int SelectedInventoryId { get; set; } = 0;
        private int SelectedInventoryArticleId { get; set; } = 0;

        public InventoryViewData InventoryVM = new InventoryViewData(false);
        public ctrInventory()
        {
            InitializeComponent();
            afColorLabel1.myText = const_Headline;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrInventory_Load(object sender, EventArgs e)
        {
            comboInventoryArt.DataSource = Enum.GetValues(typeof(Common.Enumerations.enumInventoryArt));
            comboInventoryArt.Enabled = true;

            InventoryArtFilter = Common.Enumerations.enumInventoryArt.Komplett;
            InventoryArticleStatusFilter = Common.Enumerations.enumInventoryArticleStatus.NotSet;
            InventoryStatus = Common.Enumerations.enumInventoryStatus.NotSet;
            SetComTECSettings();
            InitDgvInventories();
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetComTECSettings()
        {
            string strAdmin = "Administrator";
            if (
                (this.GL_User.LoginName.ToString().ToUpper() == "ADMINISTRATOR")
                ||
                (this.GL_User.LoginName.ToString().ToUpper() == "ADMIN")
                )
            {                //this.rDgv.ShowFilteringRow = true;
            }
            else
            {
                //this.rDgv.ShowFilteringRow = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgvInventories()
        {
            ClearInventoryValueToInputFields();
            ClearInventoryArticleValueToImputFields();

            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInventoryHandler(
                                                                        delegate ()
                                                                        {
                                                                            InitDgvInventories();
                                                                        }
                                                                    )
                                    );
                return;
            }

            InventoryViewData InventoryVM = new InventoryViewData(false);

            DataTable dtSourceInventories = InventoryVM.dtInventories;
            this.dgvInventories.DataSource = dtSourceInventories;
            //this.dgvInventories.ShowFilteringRow = false;
            this.dgvInventories.EnableFiltering = true;
            this.dgvInventories.MasterTemplate.EnableFiltering = true;

            Functions.setView(ref dtSourceInventories, ref dgvInventories, dgvViewInventur_KategorieName, "Default", this._ctrMenu._frmMain.GL_System, false);

            foreach (GridViewColumn col in this.dgvInventories.Columns)
            {
                GridViewDataColumn tmpDataCol;
                switch (col.Name)
                {
                    case "Status":
                    case "Art":
                        col.ImageLayout = ImageLayout.Center;
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        break;

                    case "Id":
                    case "UserId":
                    case "ArbeitsbereichId":
                        col.IsVisible = Functions.CheckUserForAdminComtec(this.GL_User);
                        tmpDataCol = col as GridViewDataColumn;
                        tmpDataCol.FormatString = "{0:n0}";
                        break;

                    case "Name":
                    case "Description":
                    case "Text":
                        break;

                    case "Created":
                        tmpDataCol = col as GridViewDataColumn;
                        tmpDataCol.FormatString = "{0:d}";
                        break;

                    default:
                        col.IsVisible = Functions.CheckUserForAdminComtec(this.GL_User);
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        break;
                }
            }
            this.dgvInventories.BestFitColumns();
            SetDgvInvnetoryFilter();
            SetSelectedRowInDgvInventories();
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetDgvInvnetoryFilter()
        {
            switch (InventoryArtFilter)
            {
                case Common.Enumerations.enumInventoryArt.NotSet:
                case Common.Enumerations.enumInventoryArt.Auftraggeber:
                case Common.Enumerations.enumInventoryArt.Reihe:

                    this.dgvInventories.FilterDescriptors.Clear();
                    FilterDescriptor filter = new FilterDescriptor();
                    filter.PropertyName = "Art";
                    filter.Operator = FilterOperator.IsEqualTo;
                    filter.Value = (int)InventoryArtFilter;
                    filter.IsFilterEditor = true;
                    if (this.dgvInventories.Columns.Contains("Art"))
                    {
                        this.dgvInventories.FilterDescriptors.Add(filter);
                    }
                    break;

                case Common.Enumerations.enumInventoryArt.Komplett:
                    this.dgvInventories.FilterDescriptors.Clear();
                    break;
                default:
                    this.dgvInventories.FilterDescriptors.Clear();
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetSelectedRowInDgvInventories()
        {
            if (this.dgvInventories.MasterTemplate.CurrentView.Rows.Count > 0)
            {
                int iTmp = 0;
                int.TryParse(this.dgvInventories.MasterTemplate.CurrentView.Rows[0].Cells["Id"].Value.ToString(), out iTmp);
                if (iTmp > 0)
                {
                    SelectedInventoryId = iTmp;
                    this.dgvInventories.MasterTemplate.CurrentView.Rows[0].IsSelected = true;
                    this.dgvInventories.MasterTemplate.CurrentView.Rows[0].IsCurrent = true;
                    InventoryVM = new InventoryViewData(SelectedInventoryId, 0);
                    InventoryVM.InventoryArticleViewData = new InvnetoryArticleViewData(SelectedInventoryId, true);
                    InitDgvInventoryArticle();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgvInventoryArticle()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInventoryHandler(
                                                                        delegate ()
                                                                        {
                                                                            InitDgvInventoryArticle();
                                                                        }
                                                                    )
                                    );
                return;
            }

            //DataTable dtSourceInventoriesArticle = Inventory.InventoryArticle.dtInventoryArticleList.Copy();
            InventoryVM.InventoryArticleViewData.GetInventoryArticleDatatable();
            DataTable dtSourceInventoriesArticle = InventoryVM.InventoryArticleViewData.dtInventoryArticleList;
            this.dgvInventoryArticle.DataSource = dtSourceInventoriesArticle;
            Functions.setView(ref dtSourceInventoriesArticle, ref dgvInventoryArticle, dgvViewInventurArticle_KategorieName, "Default", this._ctrMenu._frmMain.GL_System, false);

            foreach (GridViewColumn col in this.dgvInventories.Columns)
            {
                GridViewDataColumn tmpDataCol;
                switch (col.Name)
                {
                    case "Status":
                    case "Art":
                        col.ImageLayout = ImageLayout.Center;
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        break;

                    case "Id":
                    case "InventoryId":
                    //case "LVSNr":
                    case "ArtikelId":
                        col.IsVisible = Functions.CheckUserForAdminComtec(this.GL_User);
                        tmpDataCol = col as GridViewDataColumn;
                        tmpDataCol.FormatString = "{0:n0}";
                        tmpDataCol.TextAlignment = ContentAlignment.MiddleCenter;
                        break;

                    //case "Id":
                    //case "InventoryId":
                    case "LVSNr":
                        //case "ArtikelId":
                        tmpDataCol = col as GridViewDataColumn;
                        tmpDataCol.FormatString = "{0:n0}";
                        tmpDataCol.TextAlignment = ContentAlignment.MiddleCenter;
                        break;

                    case "Name":
                    case "Description":
                    case "Text":
                    case "Produktionsnummer":
                    case "Werksnummer":
                    case "Werk":
                    case "Halle":
                    case "Reihe":
                    case "Ebene":
                    case "Platz":
                        tmpDataCol = col as GridViewDataColumn;
                        tmpDataCol.TextAlignment = ContentAlignment.MiddleCenter;
                        break;

                    //case "Created":
                    //    tmpDataCol = col as GridViewDataColumn;
                    //    tmpDataCol.FormatString = "{0:d}";
                    //    break;

                    default:
                        col.IsVisible = Functions.CheckUserForAdminComtec(this.GL_User);
                        col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        break;
                }
            }
            this.dgvInventories.BestFitColumns();
            SetDgvInvnetoryArticleFilter();
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetDgvInvnetoryArticleFilter()
        {
            switch (InventoryArticleStatusFilter)
            {
                case Common.Enumerations.enumInventoryArticleStatus.NotSet:
                    this.dgvInventoryArticle.FilterDescriptors.Clear();
                    break;
                case Common.Enumerations.enumInventoryArticleStatus.Neu:
                case Common.Enumerations.enumInventoryArticleStatus.OK:
                case Common.Enumerations.enumInventoryArticleStatus.Fehlt:
                    //case Common.Enumerations.enumInventoryArticleStatus.LOChanged:
                    this.dgvInventoryArticle.FilterDescriptors.Clear();

                    FilterDescriptor filter = new FilterDescriptor();
                    filter.PropertyName = "Status";
                    filter.Operator = FilterOperator.IsEqualTo;
                    filter.Value = (int)InventoryArticleStatusFilter;
                    filter.IsFilterEditor = true;
                    if (this.dgvInventoryArticle.Columns.Contains("Status"))
                    {
                        this.dgvInventoryArticle.FilterDescriptors.Add(filter);
                    }
                    break;
                default:
                    this.dgvInventoryArticle.FilterDescriptors.Clear();
                    break;
            }
        }
        private void dgvInventories_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {

            //if (this.dgvInventories.SelectedRows.Count > 0)
            //{
            //    if (
            //        (Inventory is clsInventory) && 
            //        (Inventory.Id > 0)
            //       )
            //    {
            //        RadMenuSeparatorItem separator;
            //        RadMenuItem customMenuItem;

            //        separator = new RadMenuSeparatorItem();
            //        e.ContextMenu.Items.Add(separator);
            //        customMenuItem = new RadMenuItem();
            //        customMenuItem.Text = "Inventur löschen";
            //        customMenuItem.Click += new EventHandler(dgvInventories_DeleteItem);
            //        e.ContextMenu.Items.Add(customMenuItem);

            //        //separator = new RadMenuSeparatorItem();
            //        //e.ContextMenu.Items.Add(separator);
            //        //customMenuItem = new RadMenuItem();
            //        //customMenuItem.Text = "Inventur löschen";
            //        //customMenuItem.Click += new EventHandler(dgvInventories_DeleteItem);
            //        //e.ContextMenu.Items.Add(customMenuItem);
            //    }
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvInventories_DeleteItem(object sender, EventArgs e)
        {
            if (clsMessages.Inventory_Delete())
            {
                //Inventory.Delete();
                InventoryVM.Delete();
                InitDgvInventories();
            }
        }

        private void dgvInventories_SetStatus(object sender, EventArgs e)
        {
            if (clsMessages.Inventory_SetStatus())
            {
                //Inventory.Delete();
                InventoryVM.Delete();
                InitDgvInventories();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvInventories_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgvInventories.Rows.Count > 0)
                {
                    if (this.dgvInventories.CurrentRow != null)
                    {
                        int iTmp = 0;
                        int.TryParse(this.dgvInventories.Rows[e.RowIndex].Cells["Id"].Value.ToString(), out iTmp);
                        if (SelectedInventoryId > 0)
                        {
                            SelectedInventoryId = iTmp;
                            //Inventory = new clsInventory(iTmp);
                            InventoryVM = new InventoryViewData(SelectedInventoryId, 0);
                            //InventoryVM.InventoryArticleViewData = new InvnetoryArticleViewData(iTmp, 0);
                            InventoryVM.InventoryArticleViewData = new InvnetoryArticleViewData(SelectedInventoryId, true);
                            InitDgvInventoryArticle();
                            SetInventoryValueToInput();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvInventories_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (this.dgvInventories.RowCount > 0)
            {
                if (e.RowIndex > -1)
                {
                    if (e.CellElement.Value != null)
                    {
                        switch (e.Column.Name)
                        {
                            case "Status":
                                string strValue = e.CellElement.RowInfo.Cells["Status"].Value.ToString();
                                int iValue = 0;
                                int.TryParse(strValue, out iValue);

                                Common.Enumerations.enumInventoryStatus tmpStatus = Common.Enumerations.enumInventoryStatus.NotSet;
                                Enum.TryParse(iValue.ToString(), out tmpStatus);

                                switch (tmpStatus)
                                {
                                    case Common.Enumerations.enumInventoryStatus.NotSet:
                                        e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_red_16x16;
                                        //e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_red;
                                        break;
                                    case Common.Enumerations.enumInventoryStatus.erstellt:
                                        e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_grey_16x16;
                                        //e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_grey;
                                        break;
                                    case Common.Enumerations.enumInventoryStatus.InBearbeitung:
                                        e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_yellow_16x16;
                                        //e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_yellow;
                                        break;
                                    case Common.Enumerations.enumInventoryStatus.erledigt:
                                        e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_green_16x16;
                                        //e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_green;
                                        break;
                                }
                                e.CellElement.ForeColor = System.Drawing.Color.Transparent;
                                e.CellElement.ImageAlignment = ContentAlignment.MiddleCenter;
                                break;
                            default:
                                e.CellElement.Image = null;
                                e.CellElement.ImageAlignment = ContentAlignment.MiddleCenter;
                                e.CellElement.ForeColor = System.Drawing.Color.Black;
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvInventoryArticle_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (this.dgvInventoryArticle.RowCount > 0)
            {
                if (e.RowIndex > -1)
                {
                    if (e.CellElement.Value != null)
                    {
                        switch (e.Column.Name)
                        {
                            case "Status":
                                string strValue = e.CellElement.RowInfo.Cells["Status"].Value.ToString();
                                int iValue = 0;
                                int.TryParse(strValue, out iValue);

                                Common.Enumerations.enumInventoryArticleStatus tmpStatus = Common.Enumerations.enumInventoryArticleStatus.NotSet;
                                Enum.TryParse(iValue.ToString(), out tmpStatus);

                                switch (e.Column.Name)
                                {
                                    case "Status":
                                        switch (tmpStatus)
                                        {
                                            case Common.Enumerations.enumInventoryArticleStatus.NotSet:
                                                e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_red_16x16;
                                                break;
                                            case Common.Enumerations.enumInventoryArticleStatus.Neu:
                                                e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_grey_16x16;
                                                break;
                                            case Common.Enumerations.enumInventoryArticleStatus.OK:
                                                e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_green_16x16;
                                                break;
                                            case Common.Enumerations.enumInventoryArticleStatus.Fehlt:
                                                e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_red_16x16;
                                                break;
                                                //Lagerort changed
                                                //case Common.Enumerations.enumInventoryArticleStatus.LOChanged:
                                                //    e.CellElement.Image = Sped4.Properties.Resources.bullet_ball_yellow_16x16;
                                                //    break;
                                        }
                                        break;
                                }
                                e.CellElement.ForeColor = System.Drawing.Color.Transparent;
                                e.CellElement.ImageAlignment = ContentAlignment.MiddleCenter;
                                break;
                            default:
                                e.CellElement.Image = null;
                                e.CellElement.ImageAlignment = ContentAlignment.MiddleCenter;
                                e.CellElement.ForeColor = System.Drawing.Color.Black;
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnCloseCtr_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrInventory();
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetInventoryValueToInput()
        {
            ClearInventoryValueToInputFields();
            if (InventoryVM.Inventory is Common.Models.Inventories)
            {
                tbInventoryId.Text = InventoryVM.Inventory.Id.ToString();
                tbInventoryName.Text = InventoryVM.Inventory.Name;
                tbInventoryDescription.Text = InventoryVM.Inventory.Description;
                Functions.SetComboToSelecetedItem(ref comboInventoryArt, InventoryVM.Inventory.Art.ToString());
                switch (InventoryVM.Inventory.Status)
                {
                    case Common.Enumerations.enumInventoryStatus.erledigt:
                        cbInventoryDone.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
                        cbInventoryInProcess.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        cbInventoryNeu.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        pbInventoryStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_green;
                        break;
                    case Common.Enumerations.enumInventoryStatus.erstellt:
                    case Common.Enumerations.enumInventoryStatus.NotSet:
                        cbInventoryDone.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        cbInventoryInProcess.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        cbInventoryNeu.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
                        pbInventoryStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_grey;
                        break;
                    case Common.Enumerations.enumInventoryStatus.InBearbeitung:
                        cbInventoryDone.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        cbInventoryInProcess.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
                        cbInventoryNeu.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        pbInventoryStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_yellow;
                        break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void ClearInventoryValueToInputFields()
        {
            tbInventoryId.Text = string.Empty;
            tbInventoryName.Text = string.Empty;
            tbInventoryDescription.Text = string.Empty;
            comboInventoryArt.SelectedIndex = 0;

            cbInventoryDone.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
            cbInventoryInProcess.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
            cbInventoryNeu.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;

        }
        /// <summary>
        ///             Inventory Ansicht Komplett
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterKomplettToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryArtFilter = Common.Enumerations.enumInventoryArt.Komplett;
            InitDgvInventories();
        }
        /// <summary>
        ///             Inventory Ansicht nach Auftraggeber
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterAuftraggeberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryArtFilter = Common.Enumerations.enumInventoryArt.Auftraggeber;
            InitDgvInventories();
        }
        /// <summary>
        ///             Inventory Ansicht nach Reihe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterReiheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryArtFilter = Common.Enumerations.enumInventoryArt.Reihe;
            InitDgvInventories();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterNotSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryArtFilter = Common.Enumerations.enumInventoryArt.NotSet;
            InitDgvInventories();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshInventories_Click(object sender, EventArgs e)
        {
            InitDgvInventories();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInventorySave_Click(object sender, EventArgs e)
        {
            if (InventoryVM.Inventory is Common.Models.Inventories)
            {
                InventoryVM.Inventory.Name = tbInventoryName.Text.Trim();
                InventoryVM.Inventory.Description = tbInventoryDescription.Text.Trim();
                InventoryVM.Inventory.Art = (Common.Enumerations.enumInventoryArt)comboInventoryArt.SelectedItem;
                InventoryVM.Inventory.UserId = (int)this._ctrMenu._frmMain.system.BenutzerID;
                InventoryVM.Inventory.ArbeitsbereichId = (int)this._ctrMenu._frmMain.system.AbBereich.ID;
                InventoryVM.Inventory.Status = InventoryStatus;
                if (InventoryVM.Inventory.Status == Common.Enumerations.enumInventoryStatus.erledigt)
                {
                    InventoryVM.Inventory.CloseDate = DateTime.Now;
                    InventoryVM.Inventory.CloseUserId = (int)this._ctrMenu._frmMain.system.BenutzerID;
                }
                else
                {
                    InventoryVM.Inventory.CloseDate = Globals.DefaultDateTimeMinValue;
                    InventoryVM.Inventory.CloseUserId = 0;
                }
                InventoryVM.Update();
            }
            ClearInventoryValueToInputFields();
            InitDgvInventories();
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetInventoryArticleValueToInput()
        {
            if (InventoryVM.Inventory is Common.Models.Inventories)
            {
                if (InventoryVM.InventoryArticleViewData.InventoryArticle is Common.Models.InventoryArticles)
                {
                    tbInventoryArticleId.Text = InventoryVM.InventoryArticleViewData.InventoryArticle.Id.ToString();

                    tbInvnetoryArticleLVSNr.Text = ((int)InventoryVM.InventoryArticleViewData.InventoryArticle.Artikel.LVS_ID).ToString()
                                                   + " [" + ((int)InventoryVM.InventoryArticleViewData.InventoryArticle.ArtikelId).ToString() + "]";

                    tbInventoryArticleDescription.Text = InventoryVM.InventoryArticleViewData.InventoryArticle.Description;
                    dtpCheckDate.Value = InventoryVM.InventoryArticleViewData.InventoryArticle.Scanned;

                    switch (InventoryVM.InventoryArticleViewData.InventoryArticle.Status)
                    {
                        case Common.Enumerations.enumInventoryArticleStatus.Neu:
                            cbStatNeu.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
                            break;
                        case Common.Enumerations.enumInventoryArticleStatus.OK:
                            cbStatOK.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
                            break;
                        case Common.Enumerations.enumInventoryArticleStatus.Fehlt:
                            cbStatFehlt.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
                            break;
                        case Common.Enumerations.enumInventoryArticleStatus.NotSet:
                            //cbStat.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
                            break;
                            //case Common.Enumerations.enumInventoryArticleStatus.LOChanged:
                            //    cbStatLOChange.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
                            //    break;

                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void ClearInventoryArticleValueToImputFields()
        {
            tbInventoryArticleId.Text = string.Empty;
            tbInvnetoryArticleLVSNr.Text = string.Empty;
            tbInventoryArticleText.Text = string.Empty;
            tbInventoryArticleDescription.Text = string.Empty;

            dtpCheckDate.Value = new DateTime(1900, 1, 1);

            cbStatFehlt.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
            cbStatNeu.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
            cbStatOK.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
            //cbStatLOChange.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void cbInventoryNeu_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (InventoryVM.Inventory is Common.Models.Inventories)
            {
                switch (args.ToggleState)
                {
                    case Telerik.WinControls.Enumerations.ToggleState.On:
                        InventoryStatus = Common.Enumerations.enumInventoryStatus.erstellt;
                        pbInventoryStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_grey;
                        cbInventoryDone.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        cbInventoryInProcess.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        break;
                    case Telerik.WinControls.Enumerations.ToggleState.Off:
                        //InventoryStatus = enumInventoryStatus.NotSet;
                        //pbInventoryStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_grey;
                        break;
                    case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
                        //InventoryStatus = enumInventoryStatus.NotSet;
                        //pbInventoryStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_grey;
                        break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void cbInventoryInProcess_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (InventoryVM.Inventory is Common.Models.Inventories)
            {
                switch (args.ToggleState)
                {
                    case Telerik.WinControls.Enumerations.ToggleState.On:
                        InventoryStatus = Common.Enumerations.enumInventoryStatus.InBearbeitung;
                        pbInventoryStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_yellow;
                        cbInventoryDone.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        cbInventoryNeu.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        break;
                    case Telerik.WinControls.Enumerations.ToggleState.Off:
                        //InventoryStatus = enumInventoryStatus.NotSet;
                        //pbInventoryStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_grey;
                        break;
                    case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
                        //InventoryStatus = enumInventoryStatus.NotSet;
                        //pbInventoryStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_grey;
                        break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void cbInventoryDone_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (InventoryVM.Inventory is Common.Models.Inventories)
            {
                switch (args.ToggleState)
                {
                    case Telerik.WinControls.Enumerations.ToggleState.On:
                        InventoryStatus = Common.Enumerations.enumInventoryStatus.erledigt;
                        pbInventoryStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_green;
                        cbInventoryInProcess.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        cbInventoryNeu.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        break;
                    case Telerik.WinControls.Enumerations.ToggleState.Off:
                        //InventoryStatus = enumInventoryStatus.NotSet;
                        //pbInventoryStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_grey;
                        break;
                    case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
                        //InventoryStatus = enumInventoryStatus.NotSet;
                        //pbInventoryStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_grey;
                        break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void cbStatNeu_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            string str = string.Empty;
            if (InventoryVM.InventoryArticleViewData.InventoryArticle is Common.Models.InventoryArticles)
            {
                switch (args.ToggleState)
                {
                    case Telerik.WinControls.Enumerations.ToggleState.On:
                        InventoryVM.InventoryArticleViewData.InventoryArticle.Status = Common.Enumerations.enumInventoryArticleStatus.Neu;
                        pbStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_grey;

                        cbStatFehlt.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        cbStatOK.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        //cbStatLOChange.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void cbStatOK_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            string str = string.Empty;
            if (InventoryVM.InventoryArticleViewData.InventoryArticle is Common.Models.InventoryArticles)
            {
                switch (args.ToggleState)
                {
                    case Telerik.WinControls.Enumerations.ToggleState.On:
                        InventoryVM.InventoryArticleViewData.InventoryArticle.Status = Common.Enumerations.enumInventoryArticleStatus.OK;
                        pbStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_green;

                        cbStatFehlt.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        cbStatNeu.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        //cbStatLOChange.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        break;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void cbStatFehlt_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            string str = string.Empty;
            if (InventoryVM.InventoryArticleViewData.InventoryArticle is Common.Models.InventoryArticles)
            {
                switch (args.ToggleState)
                {
                    case Telerik.WinControls.Enumerations.ToggleState.On:
                        InventoryVM.InventoryArticleViewData.InventoryArticle.Status = Common.Enumerations.enumInventoryArticleStatus.Fehlt;
                        pbStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_red;

                        cbStatOK.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        cbStatNeu.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        //cbStatLOChange.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        break;
                }
            }
        }

        private void cbStatLOChange_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //if (InventoryVM.InventoryArticleViewData.InventoryArticle is Common.Models.InventoryArticles)
            //{
            //    switch (args.ToggleState)
            //    {
            //        case Telerik.WinControls.Enumerations.ToggleState.On:
            //            InventoryVM.InventoryArticleViewData.InventoryArticle.Status = Common.Enumerations.enumInventoryArticleStatus.LOChanged;
            //            pbStatus.Image = (Image)Sped4.Properties.Resources.bullet_ball_yellow;

            //            cbStatOK.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
            //            cbStatNeu.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
            //            cbStatFehlt.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
            //            break;
            //    }
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvInventoryArticle_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgvInventoryArticle.Rows.Count > 0)
                {
                    if (dgvInventoryArticle.CurrentRow != null)
                    {
                        int iTmp = 0;
                        int.TryParse(this.dgvInventoryArticle.Rows[e.RowIndex].Cells["Id"].Value.ToString(), out iTmp);
                        if (iTmp > 0)
                        {
                            SelectedInventoryArticleId = iTmp;
                            //Inventory.InventoryArticle = new clsInventoryArticle(iTmp, InventoryVM.Inventory.Id);
                            InventoryVM.InventoryArticleViewData = new InvnetoryArticleViewData(SelectedInventoryId, SelectedInventoryArticleId, true);
                            ClearInventoryArticleValueToImputFields();
                            SetInventoryArticleValueToInput();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterNotSetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InventoryArticleStatusFilter = Common.Enumerations.enumInventoryArticleStatus.NotSet;
            InitDgvInventoryArticle();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterKeinFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryArticleStatusFilter = Common.Enumerations.enumInventoryArticleStatus.NotSet;
            InitDgvInventoryArticle();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterNeuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryArticleStatusFilter = Common.Enumerations.enumInventoryArticleStatus.Neu;
            InitDgvInventoryArticle();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterOKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryArticleStatusFilter = Common.Enumerations.enumInventoryArticleStatus.OK;
            InitDgvInventoryArticle();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterFehltToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryArticleStatusFilter = Common.Enumerations.enumInventoryArticleStatus.Fehlt;
            InitDgvInventoryArticle();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterLagerortGeändertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //InventoryArticleStatusFilter = Common.Enumerations.enumInventoryArticleStatus.LOChanged;
            //InitDgvInventoryArticle();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnInventoryArticleRefresh_Click(object sender, EventArgs e)
        {
            InitDgvInventoryArticle();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnInventoryArticleDelete_Click(object sender, EventArgs e)
        {
            if (clsMessages.Inventory_Delete())
            {
                InventoryVM.InventoryArticleViewData.Delete();
                InitDgvInventoryArticle();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnInventoryArticleSave_Click(object sender, EventArgs e)
        {
            if (InventoryVM.InventoryArticleViewData.InventoryArticle is Common.Models.InventoryArticles)
            {
                InventoryVM.InventoryArticleViewData.InventoryArticle.Description = tbInventoryArticleDescription.Text;
                InventoryVM.InventoryArticleViewData.InventoryArticle.Text = tbInventoryArticleText.Text;
                InventoryVM.InventoryArticleViewData.InventoryArticle.Scanned = dtpCheckDate.Value;

                InventoryVM.InventoryArticleViewData.Update();
                InitDgvInventoryArticle();
            }
        }


    }
}
