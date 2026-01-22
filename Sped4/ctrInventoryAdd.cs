using LVS;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrInventoryAdd : UserControl
    {
        internal const string dgvView_KategorieName = "InventurAdd";
        public ctrMenu _ctrMenu;
        //internal ctrBestand _ctrBestand;
        //public Globals._GL_USER GLUser;
        public frmTmp _frmTmp;
        public DataTable dtSource;
        public List<int> ListArtikelId;

        //public Inventories Inventory;
        public InventoryViewData InventoryVM;
        public ctrInventoryAdd()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrInventoryAdd_Load(object sender, EventArgs e)
        {
            ListArtikelId = new List<int>();
            InitCtr();
            InitDGV();
        }
        private void InitCtr()
        {
            tsbtnCreateInventory.Enabled = false;

            //Inventory = new clsInventory();
            InventoryVM = new InventoryViewData(false);

            string strName = "Inventur vom " + DateTime.Now.ToString("dd.MM.yyyy");
            tbName.Text = strName;
            comboInventoryArt.DataSource = Enum.GetValues(typeof(enumInventoryArt));
            comboInventoryArt.Enabled = true;
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDGV()
        {
            //--- Init DGV
            this.dgv.DataSource = dtSource;

            Functions.setView(ref dtSource, ref dgv, dgvView_KategorieName, "Default", this._ctrMenu._frmMain.GL_System, false);

            GridViewSummaryItem countAnzahl = new GridViewSummaryItem("ArtikelID", "Gesamt [Stk]: {0}", GridAggregateFunction.Count);
            GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem(
                                    new GridViewSummaryItem[] { countAnzahl });
            this.dgv.SummaryRowsTop.Add(summaryRowItem);
            this.dgv.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnCreateInventory_Click(object sender, EventArgs e)
        {
            if (clsMessages.Inventory_Add())
            {
                SetValueToCtr();
                //this.Inventory.ListArticleAddToInventory = Classes.TelerikCls.TelerikFunktions.GetColumnArtikelIdFrom(ref this.dgv);
                InventoryVM.ListArticleAddToInventory = Classes.TelerikCls.TelerikFunktions.GetColumnArtikelIdFrom(ref this.dgv);
                if (InventoryVM.Inventory is Common.Models.Inventories)
                {
                    InventoryVM.AddAll();
                    if (InventoryVM.Inventory.Id > 0)
                    {
                        string strMess = "Die Inventurdaten wurden gespeichert! Der Vorgang wird geschlossen.";
                        clsMessages.Allgemein_InfoTextShow(strMess);
                    }
                    else
                    {
                        string strMess = "Es ist ein Fehler aufgetreten. Die Daten konnten nicht gespeichert werden! Der Vorgang wird geschlossen.";
                        clsMessages.Allgemein_ERRORTextShow(strMess);
                    }
                    this._frmTmp.CloseFrmTmp();
                }
            }
        }

        private void SetValueToCtr()
        {
            InventoryVM = new InventoryViewData(false);
            //InventoryVM.Inventory = new Inv();
            InventoryVM.Inventory.Name = tbName.Text.Trim();
            InventoryVM.Inventory.Description = tbDescription.Text.Trim();
            Enum.TryParse(comboInventoryArt.Text, out Common.Enumerations.enumInventoryArt enumTmp);
            InventoryVM.Inventory.Art = enumTmp;
            InventoryVM.Inventory.UserId = (int)_ctrMenu.GL_User.User_ID;
            InventoryVM.Inventory.ArbeitsbereichId = (int)_ctrMenu._frmMain.system.AbBereich.ID;
        }

        private void tsbtnCloseCtr_Click(object sender, EventArgs e)
        {
            this._frmTmp.CloseFrmTmp();
        }

        private void comboInventoryArt_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = comboInventoryArt.SelectedItem.ToString();
            enumInventoryArt eArt = (enumInventoryArt)comboInventoryArt.SelectedItem;
            tsbtnCreateInventory.Enabled = (eArt != enumInventoryArt.NotSet);
        }


    }
}
