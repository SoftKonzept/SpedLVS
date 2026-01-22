using Common.Models;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrDelforDeliveryForecast : UserControl
    {
        public ctrMenu _ctrMenu;

        internal bool ShowArticleInStockInGrid = true;
        internal int iForecastQuantity = 0;
        internal int iStoreOutQuantity = 0;
        internal string ForecastWerksnummer = string.Empty;
        internal DataTable dtStockSource;

        internal EdiDelforViewData delforVD = new EdiDelforViewData();
        internal List<EdiDelforD97AValues> ListEdiDelfor = new List<EdiDelforD97AValues>();
        internal List<EdiDelforD97AValues> ListEdiDelforDelivered = new List<EdiDelforD97AValues>();

        internal ArticleViewData artVD = new ArticleViewData();
        internal List<Articles> ListArticlesToCall = new List<Articles>();
        internal EdiDelforD97AValues SelectedEdiDelforValue = new EdiDelforD97AValues();
        internal List<string> ListWerksnummer = new List<string>();
        internal const string const_FirstItemComboWerksnummer = "-- ALL --";
        internal bool DgvArticle_ShowSelectionColumn = true;
        public ctrDelforDeliveryForecast()
        {
            InitializeComponent();
        }
        public void InitCtr(ctrMenu ctrMenu)
        {
            _ctrMenu = ctrMenu;
            InitDgvDelfor();
        }

        private void InitComboWerksnummer()
        {
            ListWerksnummer = new List<string>();
            ListWerksnummer.Add(const_FirstItemComboWerksnummer);
            foreach (EdiDelforD97AValues item in ListEdiDelfor)
            {
                if (item.Werksnummer.Length > 0)
                {
                    if (!ListWerksnummer.Contains(item.Werksnummer))
                    {
                        ListWerksnummer.Add(item.Werksnummer);
                    }
                }
            }
            comboWerksnummer.DataSource = ListWerksnummer;
            comboWerksnummer.SelectedIndex = 0;
        }

        public void InitDgvDelfor()
        {
            //-- dgvArticle leeren
            dtStockSource = new DataTable();
            dgvArticle.DataSource = dtStockSource;

            iForecastQuantity = 0;
            iStoreOutQuantity = 0;

            delforVD.FillList(true);
            ListEdiDelfor = new List<EdiDelforD97AValues>();
            ListEdiDelfor = delforVD.ListEdiDelforValue.ToList();
            dgvDelfor.DataSource = ListEdiDelfor;

            bool IsUserComtecAdmin = Functions.CheckUserForAdminComtec(_ctrMenu.GL_User);
            for (Int32 i = 0; i <= this.dgvDelfor.Columns.Count - 1; i++)
            {
                string ColName = dgvDelfor.Columns[i].Name.ToString();
                switch (ColName)
                {
                    case "Select":
                        this.dgvDelfor.Columns[i].Width = 55;
                        this.dgvDelfor.Columns.Move(i, 0);
                        break;
                    case "Deaktivate":
                        this.dgvDelfor.Columns[i].Width = 55;
                        this.dgvDelfor.Columns.Move(i, 0);
                        break;
                    case "DeliveryDate":
                        this.dgvDelfor.Columns[i].HeaderText = "Termin";
                        this.dgvDelfor.Columns[i].Width = 100;
                        this.dgvDelfor.Columns[i].FormatString = "{0:d}";
                        this.dgvDelfor.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        this.dgvDelfor.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                        //this.dgvDelfor.Columns[i].IsVisible = false;
                        this.dgvDelfor.Columns.Move(i, 1);
                        break;
                    case "CallQuantity":
                        this.dgvDelfor.Columns[i].Width = 70;
                        this.dgvDelfor.Columns[i].HeaderText = "Menge";
                        this.dgvDelfor.Columns[i].FormatString = "{0:N2}";
                        this.dgvDelfor.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        this.dgvDelfor.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                        //this.dgvDelfor.Columns[i].IsVisible = false;
                        this.dgvDelfor.Columns.Move(i, 2);
                        break;
                    case "DocumentDate":
                        this.dgvDelfor.Columns[i].HeaderText = "Doc-Datum";
                        this.dgvDelfor.Columns[i].FormatString = "{0:d}";
                        this.dgvDelfor.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        this.dgvDelfor.Columns[i].Width = 100;
                        //this.dgvDelfor.Columns.Move(i, 1);
                        break;
                    case "DocumentNo":
                        this.dgvDelfor.Columns[i].HeaderText = "Doc-No";
                        this.dgvDelfor.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        this.dgvDelfor.Columns[i].Width = 60;
                        //this.dgvDelfor.Columns.Move(i, 1);
                        break;
                    case "Position":
                        this.dgvDelfor.Columns[i].HeaderText = "Pos";
                        this.dgvDelfor.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        this.dgvDelfor.Columns[i].Width = 50;
                        //this.dgvDelfor.Columns.Move(i, 2);
                        break;
                    case "Client":
                        this.dgvDelfor.Columns[i].HeaderText = "Auftraggeber";
                        this.dgvDelfor.Columns[i].IsVisible = false;
                        this.dgvDelfor.Columns[i].Width = 50;
                        //this.dgvDelfor.Columns.Move(i, 2);
                        break;
                    case "Supplier":
                        this.dgvDelfor.Columns[i].HeaderText = "Lieferant";
                        this.dgvDelfor.Columns[i].IsVisible = false;
                        this.dgvDelfor.Columns[i].Width = 50;
                        //this.dgvDelfor.Columns.Move(i, 2);
                        break;
                    case "Recipient":
                        this.dgvDelfor.Columns[i].HeaderText = "Recipient";
                        this.dgvDelfor.Columns[i].IsVisible = false;
                        this.dgvDelfor.Columns[i].Width = 50;
                        //this.dgvDelfor.Columns.Move(i, 2);
                        break;
                    case "RecipientAdr":
                        this.dgvDelfor.Columns[i].HeaderText = "RecipientAdr";
                        this.dgvDelfor.Columns[i].IsVisible = false;
                        this.dgvDelfor.Columns[i].Width = 80;
                        //this.dgvDelfor.Columns.Move(i, 2);
                        break;
                    case "RecipientAdrMatchCode":
                        this.dgvDelfor.Columns[i].HeaderText = "ADR";
                        this.dgvDelfor.Columns[i].IsVisible = true;
                        this.dgvDelfor.Columns[i].Width = 80;
                        //this.dgvDelfor.Columns.Move(i, 2);
                        break;
                    case "Werksnummer":
                        this.dgvDelfor.Columns[i].Width = 80;
                        this.dgvDelfor.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        //this.dgvDelfor.Columns.Move(i, 3);
                        break;
                    case "OrderNo":
                        this.dgvDelfor.Columns[i].HeaderText = "Bestellnummer";
                        this.dgvDelfor.Columns[i].IsVisible = false;
                        this.dgvDelfor.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        this.dgvDelfor.Columns[i].Width = 100;
                        //this.dgvDelfor.Columns.Move(i, 4);
                        break;
                    case "DeliveryScheduleNumber":
                        this.dgvDelfor.Columns[i].Width = 100;
                        this.dgvDelfor.Columns[i].IsVisible = false;
                        //this.dgv.Columns.Move(i, 5);
                        //this.dgvDelfor.Columns[i].IsVisible = IsUserComtecAdmin;
                        break;
                    case "CumQuantityStartDate":
                        this.dgvDelfor.Columns[i].Width = 100;
                        //this.dgv.Columns.Move(i, 6);
                        this.dgvDelfor.Columns[i].IsVisible = false;
                        break;
                    case "ReceivedQuantity":
                        this.dgvDelfor.Columns[i].Width = 100;
                        this.dgvDelfor.Columns[i].IsVisible = false;
                        //this.dgvDelfor.Columns.Move(i, 7);
                        break;
                    case "SID":
                        this.dgvDelfor.Columns[i].Width = 100;
                        //this.dgvDelfor.Columns.Move(i, 8);
                        this.dgvDelfor.Columns[i].IsVisible = false;
                        break;
                    case "GoodReceiptDate":
                        this.dgvDelfor.Columns[i].Width = 100;
                        //this.dgv.Columns.Move(i, 9);
                        this.dgvDelfor.Columns[i].IsVisible = false;
                        break;
                    case "SchedulingConditions":
                        this.dgvDelfor.Columns[i].Width = 100;
                        this.dgvDelfor.Columns[i].IsVisible = false;
                        break;
                    case "IsActive":
                        this.dgvDelfor.Columns[i].Width = 100;
                        this.dgvDelfor.Columns[i].IsVisible = false;
                        break;
                    case "WorkspaceId":
                        this.dgvDelfor.Columns[i].HeaderText = "Arbeitsbereich";
                        this.dgvDelfor.Columns[i].Width = 100;
                        this.dgvDelfor.Columns[i].IsVisible = false;
                        break;

                    default:
                        this.dgvDelfor.Columns[i].IsVisible = false;
                        break;
                }
            }
            dgvDelfor.BestFitColumns();

            SortDescriptor descriptor = new SortDescriptor();
            descriptor.Direction = ListSortDirection.Ascending;
            descriptor.PropertyName = "DocumentDate";
            //descriptor.PropertyName = "Termin";
            dgvDelfor.SortDescriptors.Clear();
            dgvDelfor.SortDescriptors.Add(descriptor);

            Calculate();
            InitComboWerksnummer();
        }

        public void InitDgvDelivered()
        {
            //ARtikeldaten leeren
            dgvArticle.DataSource = new DataTable();

            ListEdiDelforDelivered = new List<EdiDelforD97AValues>(delforVD.FillListDelivered(true));
            dgvDelivery.DataSource = ListEdiDelforDelivered;

            bool IsUserComtecAdmin = Functions.CheckUserForAdminComtec(_ctrMenu.GL_User);
            for (Int32 i = 0; i <= this.dgvDelivery.Columns.Count - 1; i++)
            {
                string ColName = dgvDelivery.Columns[i].Name.ToString();
                switch (ColName)
                {
                    case "Select":
                        this.dgvDelivery.Columns[i].Width = 55;
                        this.dgvDelivery.Columns.Move(i, 0);
                        break;
                    case "Deaktivate":
                        this.dgvDelivery.Columns[i].Width = 55;
                        this.dgvDelivery.Columns.Move(i, 0);
                        break;
                    case "DeliveryDate":
                        this.dgvDelivery.Columns[i].HeaderText = "Termin";
                        this.dgvDelivery.Columns[i].Width = 100;
                        this.dgvDelivery.Columns[i].FormatString = "{0:d}";
                        this.dgvDelivery.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        this.dgvDelivery.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                        //this.dgvDelfor.Columns[i].IsVisible = false;
                        this.dgvDelfor.Columns.Move(i, 1);
                        break;
                    case "CallQuantity":
                        this.dgvDelivery.Columns[i].Width = 70;
                        this.dgvDelivery.Columns[i].HeaderText = "Menge";
                        this.dgvDelivery.Columns[i].FormatString = "{0:N2}";
                        this.dgvDelivery.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        this.dgvDelivery.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                        //this.dgvDelfor.Columns[i].IsVisible = false;
                        this.dgvDelfor.Columns.Move(i, 2);
                        break;
                    case "DocumentDate":
                        this.dgvDelivery.Columns[i].HeaderText = "Doc-Datum";
                        this.dgvDelivery.Columns[i].FormatString = "{0:d}";
                        this.dgvDelivery.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        this.dgvDelivery.Columns[i].Width = 100;
                        //this.dgvDelfor.Columns.Move(i, 1);
                        break;
                    case "DocumentNo":
                        this.dgvDelivery.Columns[i].HeaderText = "Doc-No";
                        this.dgvDelivery.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        this.dgvDelivery.Columns[i].Width = 60;
                        //this.dgvDelfor.Columns.Move(i, 1);
                        break;
                    case "Position":
                        this.dgvDelivery.Columns[i].HeaderText = "Pos";
                        this.dgvDelivery.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        this.dgvDelivery.Columns[i].Width = 50;
                        //this.dgvDelfor.Columns.Move(i, 2);
                        break;
                    case "Client":
                        this.dgvDelivery.Columns[i].HeaderText = "Auftraggeber";
                        this.dgvDelivery.Columns[i].IsVisible = false;
                        this.dgvDelivery.Columns[i].Width = 50;
                        //this.dgvDelfor.Columns.Move(i, 2);
                        break;
                    case "Supplier":
                        this.dgvDelivery.Columns[i].HeaderText = "Lieferant";
                        this.dgvDelivery.Columns[i].IsVisible = false;
                        this.dgvDelivery.Columns[i].Width = 50;
                        //this.dgvDelfor.Columns.Move(i, 2);
                        break;
                    case "Recipient":
                        this.dgvDelivery.Columns[i].HeaderText = "Recipient";
                        this.dgvDelivery.Columns[i].IsVisible = false;
                        this.dgvDelivery.Columns[i].Width = 50;
                        //this.dgvDelfor.Columns.Move(i, 2);
                        break;
                    case "RecipientAdr":
                        this.dgvDelivery.Columns[i].HeaderText = "RecipientAdr";
                        this.dgvDelivery.Columns[i].IsVisible = false;
                        this.dgvDelivery.Columns[i].Width = 80;
                        //this.dgvDelfor.Columns.Move(i, 2);
                        break;
                    case "RecipientAdrMatchCode":
                        this.dgvDelivery.Columns[i].HeaderText = "ADR";
                        this.dgvDelivery.Columns[i].IsVisible = true;
                        this.dgvDelivery.Columns[i].Width = 80;
                        //this.dgvDelfor.Columns.Move(i, 2);
                        break;
                    case "Werksnummer":
                        this.dgvDelivery.Columns[i].Width = 80;
                        this.dgvDelivery.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        //this.dgvDelfor.Columns.Move(i, 3);
                        break;
                    case "OrderNo":
                        this.dgvDelivery.Columns[i].HeaderText = "Bestellnummer";
                        this.dgvDelivery.Columns[i].IsVisible = false;
                        this.dgvDelivery.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        this.dgvDelivery.Columns[i].Width = 100;
                        //this.dgvDelfor.Columns.Move(i, 4);
                        break;
                    case "DeliveryScheduleNumber":
                        this.dgvDelivery.Columns[i].Width = 100;
                        this.dgvDelivery.Columns[i].IsVisible = false;
                        //this.dgv.Columns.Move(i, 5);
                        //this.dgvDelfor.Columns[i].IsVisible = IsUserComtecAdmin;
                        break;
                    case "CumQuantityStartDate":
                        this.dgvDelivery.Columns[i].Width = 100;
                        //this.dgv.Columns.Move(i, 6);
                        this.dgvDelivery.Columns[i].IsVisible = false;
                        break;
                    case "ReceivedQuantity":
                        this.dgvDelivery.Columns[i].Width = 100;
                        this.dgvDelivery.Columns[i].IsVisible = false;
                        //this.dgvDelfor.Columns.Move(i, 7);
                        break;
                    case "SID":
                        this.dgvDelivery.Columns[i].Width = 100;
                        //this.dgvDelfor.Columns.Move(i, 8);
                        this.dgvDelivery.Columns[i].IsVisible = false;
                        break;
                    case "GoodReceiptDate":
                        this.dgvDelivery.Columns[i].Width = 100;
                        //this.dgv.Columns.Move(i, 9);
                        this.dgvDelivery.Columns[i].IsVisible = false;
                        break;
                    case "SchedulingConditions":
                        this.dgvDelivery.Columns[i].Width = 100;
                        this.dgvDelivery.Columns[i].IsVisible = false;
                        break;
                    case "IsActive":
                        this.dgvDelivery.Columns[i].Width = 100;
                        this.dgvDelivery.Columns[i].IsVisible = false;
                        break;
                    case "WorkspaceId":
                        this.dgvDelivery.Columns[i].HeaderText = "Arbeitsbereich";
                        this.dgvDelivery.Columns[i].Width = 100;
                        this.dgvDelivery.Columns[i].IsVisible = false;
                        break;

                    default:
                        this.dgvDelivery.Columns[i].IsVisible = false;
                        break;
                }
            }
            dgvDelivery.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myWerksnummerVerweis"></param>
        public void InitDgvArticle(bool myShowArticleInStock)
        {
            //-- dgvArticle leeren
            dtStockSource = new DataTable();
            dgvArticle.DataSource = dtStockSource;

            if (SelectedEdiDelforValue.IsActive)
            {
                dtStockSource = StockViewData.GetList_AvailableStockForDeliveryForecast(SelectedEdiDelforValue.Werksnummer);
            }
            else
            {
                dtStockSource = StockViewData.GetList_DeliveredArticleDelfor(SelectedEdiDelforValue);
            }
            if ((dtStockSource.Rows.Count > 0) && (!dtStockSource.Columns.Contains("Select")))
            {
                DataColumn c = new DataColumn();
                c.ColumnName = "Select";
                c.DataType = typeof(Boolean);
                dtStockSource.Columns.Add(c);
                foreach (DataRow dr in dtStockSource.Rows)
                {
                    dr["Select"] = false;
                }
            }
            dgvArticle.DataSource = dtStockSource;

            bool IsUserComtecAdmin = Functions.CheckUserForAdminComtec(_ctrMenu.GL_User);
            for (Int32 i = 0; i <= this.dgvArticle.Columns.Count - 1; i++)
            {
                string ColName = dgvArticle.Columns[i].Name.ToString();
                switch (ColName)
                {
                    case "Select":
                        this.dgvArticle.Columns[i].Width = 55;
                        this.dgvArticle.Columns.Move(i, 0);
                        break;
                    case "Deaktivate":
                        this.dgvArticle.Columns[i].Width = 55;
                        this.dgvArticle.Columns.Move(i, 0);
                        break;
                    case "EDatum":
                        this.dgvArticle.Columns[i].HeaderText = "E-Datum";
                        this.dgvArticle.Columns[i].Width = 100;
                        this.dgvArticle.Columns[i].FormatString = "{0:d}";
                        this.dgvArticle.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        //this.dgvArticle.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                        break;
                    case "Anzahl":
                        this.dgvArticle.Columns[i].Width = 70;
                        this.dgvArticle.Columns[i].HeaderText = "Menge";
                        this.dgvArticle.Columns[i].FormatString = "{0:N2}";
                        this.dgvArticle.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        //this.dgvArticle.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                        break;
                    case "Glühdatum":
                        this.dgvArticle.Columns[i].FormatString = "{0:d}";
                        this.dgvArticle.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        this.dgvArticle.Columns[i].Width = 80;
                        break;
                    case "Produktionsnummer":
                        this.dgvArticle.Columns[i].Width = 80;
                        this.dgvArticle.Columns[i].HeaderText = "P-Nr";
                        this.dgvArticle.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        //this.dgvArticle.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                        break;
                    case "Werksnummer":
                        this.dgvArticle.Columns[i].Width = 80;
                        this.dgvArticle.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        this.dgvArticle.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                        break;
                    case "Brutto":
                        this.dgvArticle.Columns[i].Width = 80;
                        this.dgvArticle.Columns[i].FormatString = "{0:N2}";
                        this.dgvArticle.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        //this.dgvArticle.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                        break;
                    case "Bezeichnung":
                        this.dgvArticle.Columns[i].Width = 80;
                        this.dgvArticle.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        //this.dgvArticle.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                        break;
                    case "DelforVerweis":
                        this.dgvArticle.Columns[i].Width = 80;
                        this.dgvArticle.Columns[i].HeaderText = "Delfor-Nr";
                        this.dgvArticle.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                        //this.dgvArticle.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                        break;
                    case "ID":
                        this.dgvArticle.Columns[i].IsVisible = (this._ctrMenu.GL_User.User_ID == 1);
                        this.dgvArticle.Columns[i].Width = 80;
                        //this.dgvArticle.Columns.Move(i, this.dgvArticle.Columns.Count - 1);
                        break;
                    case "LVSNr":
                        this.dgvArticle.Columns[i].Width = 80;
                        //this.dgvArticle.Columns.Move(i, this.dgvArticle.Columns.Count - 1);
                        break;
                    case "AbrufId":
                        this.dgvArticle.Columns[i].Width = 80;
                        //this.dgvArticle.Columns.Move(i, this.dgvArticle.Columns.Count - 1);
                        break;
                    case "DelforId":
                        this.dgvArticle.Columns[i].Width = 80;
                        //this.dgvArticle.Columns.Move(i, this.dgvArticle.Columns.Count - 1);
                        break;

                    default:
                        this.dgvArticle.Columns[i].IsVisible = true;
                        break;
                }
            }
            dgvArticle.BestFitColumns();
            SortArticleGridView();

            CalculateForecast();
            SetAllArtikelStockSelectOrUnselect(ref dgvArticle, true);

            //if (myShowArticleInStock)
            //{
            //    CalculateForecast();
            //    SetAllArtikelStockSelectOrUnselect(ref dgvArticle, true);
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        private void SortArticleGridView()
        {
            //--- sort by EDate or Glowdate
            dgvArticle.SortDescriptors.Clear();
            if ((bool)switchSort.Value)
            {
                SortDescriptor descEDate = new SortDescriptor();
                descEDate.Direction = ListSortDirection.Ascending;
                descEDate.PropertyName = "EDatum";
                dgvArticle.SortDescriptors.Add(descEDate);

                SortDescriptor descAId = new SortDescriptor();
                descAId.Direction = ListSortDirection.Ascending;
                descAId.PropertyName = "ID";
                dgvArticle.SortDescriptors.Add(descAId);
            }
            else
            {
                SortDescriptor descGlow = new SortDescriptor();
                descGlow.Direction = ListSortDirection.Ascending;
                descGlow.PropertyName = "Glühdatum";
                dgvArticle.SortDescriptors.Add(descGlow);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void FilterDelforGridView()
        {
            dgvDelfor.FilterDescriptors.Clear();
            if (comboWerksnummer.SelectedIndex > 0)
            {
                CompositeFilterDescriptor filterDescriptor = new CompositeFilterDescriptor();
                filterDescriptor.FilterDescriptors.Add(new FilterDescriptor("Werksnummer", FilterOperator.IsEqualTo, comboWerksnummer.SelectedValue.ToString()));
                dgvDelfor.FilterDescriptors.Add(filterDescriptor);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            Int32 Count = this.ParentForm.Controls.Count;
            for (Int32 i = 0; (i <= (Count - 1)); i++)
            {
                if (this.ParentForm.Controls[i].Name == "TempCtrDelforDeliveryForecast")
                {
                    this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                }
            }
            this.Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvArticle_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (!this.dgvArticle.Rows[e.RowIndex].Cells["Select"].ReadOnly)
                {
                    if (this.dgvArticle.Rows[e.RowIndex].Cells["Select"].Value == null)
                    {
                        this.dgvArticle.Rows[e.RowIndex].Cells["Select"].Value = false;
                    }
                    bool CellValue = (bool)this.dgvArticle.Rows[e.RowIndex].Cells["Select"].Value;
                    if (CellValue == true)
                    {
                        this.dgvArticle.Rows[e.RowIndex].Cells["Select"].Value = false;
                    }
                    else
                    {
                        this.dgvArticle.Rows[e.RowIndex].Cells["Select"].Value = true;
                    }
                }
                if (ShowArticleInStockInGrid)
                {
                    Calculate();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDelfor_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.Column.Name == "Deaktivate")
            {
                int iId = Decimal.ToInt32((decimal)this.dgvDelfor.Rows[e.RowIndex].Cells["Id"].Value);
                if (iId > 0)
                {
                    SelectedEdiDelforValue = ListEdiDelfor.FirstOrDefault(x => x.Id == iId);
                    SelectedEdiDelforValue.IsActive = false;

                    delforVD = new EdiDelforViewData(SelectedEdiDelforValue);
                    delforVD.Update();
                    InitDgvDelfor();
                }
            }
            if (e.Column.Name == "Select")
            {
                if (!this.dgvDelfor.Rows[e.RowIndex].Cells["Select"].ReadOnly)
                {
                    if (this.dgvDelfor.Rows[e.RowIndex].Cells["Select"].Value == null)
                    {
                        this.dgvDelfor.Rows[e.RowIndex].Cells["Select"].Value = false;
                    }
                    bool CellValue = (bool)this.dgvDelfor.Rows[e.RowIndex].Cells["Select"].Value;
                    if (CellValue == true)
                    {
                        this.dgvDelfor.Rows[e.RowIndex].Cells["Select"].Value = false;
                    }
                    else
                    {
                        SetAllASNSelectOrUnselect(ref dgvDelfor, false);
                        this.dgvDelfor.Rows[e.RowIndex].Cells["Select"].Value = true;
                        int iId = Decimal.ToInt32((decimal)this.dgvDelfor.Rows[e.RowIndex].Cells["Id"].Value);
                        if (iId > 0)
                        {
                            SelectedEdiDelforValue = ListEdiDelfor.FirstOrDefault(x => x.Id == iId);
                            ForecastWerksnummer = SelectedEdiDelforValue.Werksnummer;
                            InitDgvArticle(ShowArticleInStockInGrid);
                        }
                    }
                }
                Calculate();
            }
        }

        private void dgvDelivery_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (!this.dgvDelivery.Rows[e.RowIndex].Cells["Select"].ReadOnly)
                {
                    if (this.dgvDelivery.Rows[e.RowIndex].Cells["Select"].Value == null)
                    {
                        this.dgvDelivery.Rows[e.RowIndex].Cells["Select"].Value = false;
                    }
                    bool CellValue = (bool)this.dgvDelivery.Rows[e.RowIndex].Cells["Select"].Value;
                    if (CellValue == true)
                    {
                        this.dgvDelivery.Rows[e.RowIndex].Cells["Select"].Value = false;
                    }
                    else
                    {
                        SetAllASNSelectOrUnselect(ref dgvDelivery, false);
                        this.dgvDelivery.Rows[e.RowIndex].Cells["Select"].Value = true;
                        int iId = Decimal.ToInt32((decimal)this.dgvDelivery.Rows[e.RowIndex].Cells["Id"].Value);
                        if (iId > 0)
                        {
                            SelectedEdiDelforValue = ListEdiDelforDelivered.FirstOrDefault(x => x.Id == iId);
                            InitDgvArticle(ShowArticleInStockInGrid);

                        }
                    }
                }
                Calculate();


                //if (tabDelfor.SelectedTab.Equals(tabPage_Forecast))
                //{
                //    if (!this.dgvDelivery.Rows[e.RowIndex].Cells["Select"].ReadOnly)
                //    {
                //        if (this.dgvDelivery.Rows[e.RowIndex].Cells["Select"].Value == null)
                //        {
                //            this.dgvDelivery.Rows[e.RowIndex].Cells["Select"].Value = false;
                //        }
                //        bool CellValue = (bool)this.dgvDelivery.Rows[e.RowIndex].Cells["Select"].Value;
                //        if (CellValue == true)
                //        {
                //            this.dgvDelivery.Rows[e.RowIndex].Cells["Select"].Value = false;
                //        }
                //        else
                //        {
                //            //SetAllASNSelectOrUnselect(ref dgvDelfor, false);
                //            this.dgvDelivery.Rows[e.RowIndex].Cells["Select"].Value = true;
                //            int iId = Decimal.ToInt32((decimal)this.dgvDelivery.Rows[e.RowIndex].Cells["Id"].Value);
                //            if (iId > 0)
                //            {
                //                SelectedEdiDelforValue = ListEdiDelforDelivered.FirstOrDefault(x => x.Id == iId);
                //                InitDgvArticle(ShowArticleInStockInGrid);
                //            }
                //        }
                //    }
                //    Calculate();
                //}
            }
        }

        private void Calculate()
        {
            CalculateForecast();
            CalculateStockOut();
        }
        private void CalculateStockOut()
        {
            iStoreOutQuantity = 0;
            for (Int32 i = 0; i <= this.dgvArticle.Rows.Count - 1; i++)
            {
                if (this.dgvArticle.Rows[i].Cells["Select"].Value == null)
                {
                    this.dgvArticle.Rows[i].Cells["Select"].Value = false;
                }
                bool CellValue = (bool)this.dgvArticle.Rows[i].Cells["Select"].Value;

                if (CellValue)
                {
                    int iCell = 0;
                    if ((bool)switch_QuantitySelectionMode.Value)
                    {
                        //KG
                        int iBrutto = Decimal.ToInt32((decimal)dgvArticle.Rows[i].Cells["Brutto"].Value);
                        iStoreOutQuantity += iBrutto;
                    }
                    else
                    {
                        //Stück / Anzahl
                        string strAnzahl = dgvArticle.Rows[i].Cells["Anzahl"].Value.ToString();
                        int iAnzahl = 0;
                        int.TryParse(strAnzahl, out iAnzahl);
                        iStoreOutQuantity += iAnzahl;
                    }
                }
            }
            SetOptionSettings();
        }

        private void CalculateForecast()
        {
            iForecastQuantity = 0;
            if (tabDelfor.SelectedTab.Equals(tabPage_Forecast))
            {
                for (Int32 i = 0; i <= this.dgvDelfor.Rows.Count - 1; i++)
                {
                    if (this.dgvDelfor.Rows[i].Cells["Select"].Value == null)
                    {
                        this.dgvDelfor.Rows[i].Cells["Select"].Value = false;
                    }
                    if ((bool)this.dgvDelfor.Rows[i].Cells["Select"].Value == true)
                    {
                        int iCell = 0;
                        bool CellValue = (bool)this.dgvDelfor.Rows[i].Cells["Select"].Value;
                        iCell = (int)dgvDelfor.Rows[i].Cells["CallQuantity"].Value;
                        if (CellValue == true)
                        {
                            iForecastQuantity += iCell;
                        }
                    }
                }
            }
            if (tabDelfor.SelectedTab.Equals(tabPage_Delivery))
            {
                for (Int32 i = 0; i <= this.dgvDelivery.Rows.Count - 1; i++)
                {
                    if (this.dgvDelivery.Rows[i].Cells["Select"].Value == null)
                    {
                        this.dgvDelivery.Rows[i].Cells["Select"].Value = false;
                    }

                    if (
                            ((bool)this.dgvDelivery.Rows[i].Cells["Select"].Value == true) &&
                            ((bool)this.dgvDelivery.Rows[i].IsCurrent == true)
                       )
                    {
                        int iCell = 0;
                        bool CellValue = (bool)this.dgvDelfor.Rows[i].Cells["Select"].Value;
                        iCell = (int)dgvDelivery.Rows[i].Cells["CallQuantity"].Value;
                        if (CellValue == true)
                        {
                            iForecastQuantity += iCell;
                        }
                    }
                }
            }
            //tbForecastQuantity.Text = iForecastQuantity.ToString();
            SetOptionSettings();
        }

        private void SetOptionSettings()
        {
            if (iForecastQuantity < 1)
            {
                iStoreOutQuantity = 0;
                iForecastQuantity = 0;
                ForecastWerksnummer = string.Empty;
                //InitDgvArticle(ForecastWerksnummer);
            }

            tbStoreOutQuantity.Text = iStoreOutQuantity.ToString();
            tbForecastQuantity.Text = iForecastQuantity.ToString();
            //tbForecastWerksnummer.Text = ForecastWerksnummer;
        }

        /// <summary>
        ///             Change Sort Selektion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void switchSort_ValueChanged(object sender, EventArgs e)
        {
            SortArticleGridView();
        }

        private void tsbtnAllCheck_Click(object sender, EventArgs e)
        {
            SetAllASNSelectOrUnselect(ref dgvDelfor, true);
        }

        private void tsbtnAllUncheck_Click(object sender, EventArgs e)
        {
            SetAllASNSelectOrUnselect(ref dgvDelfor, false);
        }

        private void SetAllASNSelectOrUnselect(ref RadGridView myDgv, bool bSelect)
        {
            for (Int32 i = 0; i <= myDgv.Rows.Count - 1; i++)
            {
                myDgv.Rows[i].Cells["Select"].Value = bSelect;
            }
        }


        private void tsbtnCreateCall_Click(object sender, EventArgs e)
        {
            if (tabDelfor.SelectedTab.Equals(tabPage_Forecast))
            {
                if (iStoreOutQuantity > 0)
                {
                    for (Int32 i = 0; i <= this.dgvDelfor.Rows.Count - 1; i++)
                    {
                        if ((bool)this.dgvDelfor.Rows[i].Cells["Select"].Value == true)
                        {
                            int iCell = 0;
                            int.TryParse(dgvDelfor.Rows[i].Cells["ID"].Value.ToString(), out iCell);
                            if (iCell > 0)
                            {
                                EdiDelforViewData delforVD = new EdiDelforViewData(iCell, _ctrMenu._frmMain.GL_User, false);
                                if (delforVD.EdiDelforValue.Id > 0)
                                {
                                    ArticleViewData artVD = new ArticleViewData();
                                    for (Int32 x = 0; x <= this.dgvArticle.Rows.Count - 1; x++)
                                    {
                                        if ((bool)this.dgvArticle.Rows[x].Cells["Select"].Value == true)
                                        {
                                            int iArtId = 0;
                                            int.TryParse(dgvArticle.Rows[x].Cells["ID"].Value.ToString(), out iArtId);
                                            if (iArtId > 0)
                                            {
                                                artVD = new ArticleViewData(iArtId, (int)_ctrMenu._frmMain.GL_User.User_ID, false);
                                                if (artVD.Artikel.Id > 0)
                                                {
                                                    delforVD.CreateCallInsert(artVD.Artikel);
                                                }
                                            }
                                        }
                                    }
                                    delforVD.EdiDelforValue.IsActive = false;
                                    delforVD.Update();
                                }
                            }
                            i = this.dgvDelfor.Columns.Count;
                        }
                    }
                    //Delfor Liste neu laden
                    InitDgvDelfor();
                }
            }

            if (tabDelfor.SelectedTab.Equals(tabPage_Delivery))
            {
                try
                {
                    List<string> listSql = new List<string>();
                    //-- Artikel soll zurückgebucht werden
                    for (Int32 x = 0; x <= this.dgvArticle.Rows.Count - 1; x++)
                    {
                        if ((bool)this.dgvArticle.Rows[x].Cells["Select"].Value == true)
                        {
                            int iCallId = 0;
                            int.TryParse(dgvArticle.Rows[x].Cells["AbrufId"].Value.ToString(), out iCallId);
                            if (iCallId > 0)
                            {
                                CallViewData callVD = new CallViewData(iCallId, (int)_ctrMenu.GL_User.User_ID);
                                listSql.Add(callVD.sql_Delete);
                                //callVD.Delete();
                            }
                        }
                    }
                    bool bOk = CallViewData.ExecuteSQLProcessTransaction(listSql);
                    if ((bOk) && (this.SelectedEdiDelforValue.Id > 0))
                    {
                        EdiDelforViewData delforVD = new EdiDelforViewData(SelectedEdiDelforValue);
                        delforVD.EdiDelforValue.IsActive = true;
                        delforVD.Update_RebookCall();
                    }
                    InitDgvDelivered();
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                }
            }
        }

        private void switch_QuantitySelectionMode_ValueChanged(object sender, EventArgs e)
        {
            if ((bool)switch_QuantitySelectionMode.Value)
            {
                //KG
                lForecastQuantity.Text = "KG";
                lStoreOutQuantity.Text = "KG";
            }
            else
            {
                //Stück / Anzahl
                lForecastQuantity.Text = "STK";
                lStoreOutQuantity.Text = "STK";
            }
            SetAllASNSelectOrUnselect(ref dgvDelfor, false);
            Calculate();
            SetOptionSettings();
        }

        private void comboWerksnummer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDelforGridView();
        }

        private void tabDelfor_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage selTap = ((TabControl)sender).SelectedTab;
            dgvArticle.DataSource = new DataTable();
            if (selTap.Equals(tabPage_Forecast))
            {
                ShowArticleInStockInGrid = true;
                InitDgvDelfor();
            }
            else if (selTap.Equals(tabPage_Delivery))
            {
                ShowArticleInStockInGrid = false;
                InitDgvDelivered();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnArticleRefresh_Click(object sender, EventArgs e)
        {
            InitDgvArticle(ShowArticleInStockInGrid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitDgvDelfor();
        }
        private void dgvDelfor_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "Deaktivate")
            {
                e.CellElement.Image = Sped4.Properties.Resources.breakpoint_edit_16x16;
            }
        }

        private void dgvDelfor_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement dataCell = sender as GridDataCellElement;

            if ((dataCell != null) && (dataCell.Name == "Deaktivate"))
            {
                e.ToolTipText = "Datensatz deaktivieren!";
            }
        }
        private void tsbtnArticleSetSelection_Click(object sender, EventArgs e)
        {
            SetAllArtikelStockSelectOrUnselect(ref dgvArticle, true);
        }

        private void tsbtnArticleSetUnSelection_Click(object sender, EventArgs e)
        {
            SetAllArtikelStockSelectOrUnselect(ref dgvArticle, false);
        }

        private void SetAllArtikelStockSelectOrUnselect(ref RadGridView myDgv, bool bSelect)
        {
            iStoreOutQuantity = 0;
            //for (Int32 i = 0; i <= myDgv.Rows.Count - 1; i++)
            //{
            //    if (bSelect)
            //    {
            //        decimal tmpBru = 0;
            //        decimal.TryParse(myDgv.Rows[i].Cells["Brutto"].Value.ToString(), out tmpBru);
            //        if (tmpBru > 0)
            //        {
            //            int iBrutto = (int)tmpBru;
            //            //int.TryParse(tmpBru.ToString(), out iBrutto);
            //            if (iBrutto > 0)
            //            {
            //                iStoreOutQuantity += iBrutto;                            
            //                myDgv.Rows[i].Cells["Select"].Value = true;
            //                if (iStoreOutQuantity > iForecastQuantity)
            //                {
            //                    i = myDgv.Rows.Count;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        myDgv.Rows[i].Cells["Select"].Value = bSelect;
            //    }              
            //}

            if (tabDelfor.SelectedTab.Equals(tabPage_Forecast))
            {
                for (Int32 i = 0; i <= myDgv.Rows.Count - 1; i++)
                {
                    if (bSelect)
                    {
                        decimal tmpBru = 0;
                        decimal.TryParse(myDgv.Rows[i].Cells["Brutto"].Value.ToString(), out tmpBru);
                        if (tmpBru > 0)
                        {
                            int iBrutto = (int)tmpBru;
                            //int.TryParse(tmpBru.ToString(), out iBrutto);
                            if (iBrutto > 0)
                            {
                                iStoreOutQuantity += iBrutto;
                                myDgv.Rows[i].Cells["Select"].Value = true;
                                if (iStoreOutQuantity > iForecastQuantity)
                                {
                                    i = myDgv.Rows.Count;
                                }
                            }
                        }
                    }
                    else
                    {
                        myDgv.Rows[i].Cells["Select"].Value = bSelect;
                    }
                }
            }
            if (tabDelfor.SelectedTab.Equals(tabPage_Delivery))
            {
                for (Int32 i = 0; i <= myDgv.Rows.Count - 1; i++)
                {
                    myDgv.Rows[i].Cells["Select"].Value = bSelect;
                }
            }
            CalculateStockOut();
        }


    }
}
