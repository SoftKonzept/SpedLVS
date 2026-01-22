using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using LVS;

public partial class Journal : System.Web.UI.Page
{

    MasterPage myMasterPage;
    clsLogin login;
    clsJournal journal;
    public const string const_MenuButtonComman_StartSearch = "StartSearch";
    public const string const_MenuButtonComman_ClearSearch = "ClearSearch";
    private string Default_FileName_ToExport = DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + "_" + "Journal";
    private string Default_SiteUserInfotext = "Journal";
    Int32 MID = 0;
    private Int32 ArtikelAnzahl = 0;
    private Int32 Anzahl = 0;
    private decimal Netto = 0;
    private decimal Brutto = 0;
    private Int32 GesamtAnzahl = 0;
    private decimal GesamtBrutto = 0;
    private Int32 iStartOrderIndex = 0;

    ///<summary>Journal / Page_Load</summary>
    ///<remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        login = new clsLogin();
        if (Session["Login"] != null)
        {
            login = (clsLogin)Session["Login"];
            login.bInfoText = true;
            login.InfoText = string.Empty;
            journal = login.Journal.Copy();

            //Journalart ermitteln
            if (Request.QueryString["MID"] != null)
            {
                MID = Convert.ToInt32(Request.QueryString["MID"]);
            }
            else
            {
                MID = 0;
            }
            journal = new clsJournal();
        }
        else
        {
            login.InitClass();
            Session["Login"] = login;
        }

        if ((login != null) && (login.LoggedIn))
        {
            myMasterPage = (MasterPage)this.Master;
            Table masterClientInfoTable = (Table)this.Master.FindControl("TableUserInfo");
            if (masterClientInfoTable is Table)
            {
                masterClientInfoTable.Visible = true;
                myMasterPage.SetLableCaption(true, this.Default_SiteUserInfotext);
                myMasterPage.SetLUserInfoHead(this.login.User.Vorname + " " + this.login.User.Name);
            }

            if (Page.AppRelativeVirtualPath == login.AppRelativeVirtualPath)
            {
                login.AppRelativeVirtualPath = string.Empty;
                login.NextPagePath = string.Empty;
                Session["Login"] = login;
            }
            this.login.CurrentPagePath = Page.AppRelativeVirtualPath.ToString().Replace(login.AppRelativeVirtualPath, "");
            if (!IsPostBack)
            {
                this.journal.dtVon = Functions.GetFirstDayOfMonth(DateTime.Now);
                this.journal.dtBis = Functions.GetLastDayOfMonth(this.journal.dtVon);
                dtpVon.SelectedDate = this.journal.dtVon;
                dtpBis.SelectedDate = this.journal.dtBis;
            }
            else
            {
                this.journal.dtVon = (DateTime)this.dtpVon.SelectedDate;
                this.journal.dtBis = (DateTime)this.dtpBis.SelectedDate;
            }
        }
    }
    ///<summary>Journal / dgv_NeedDataSource</summary>
    ///<remarks></remarks>
    protected void dgv_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        string strJournalart = string.Empty;
        switch (MID)
        {
            case 0:
                strJournalart = clsJournal.const_Journalart_Abrufe;
                login.Views.FillListTableView(clsViews.const_ViewTableName_JournalCall); 
                login.Views.FillListSort(clsViews.const_ViewTableName_JournalCall); 
                Default_SiteUserInfotext = "Journal -> Abrufe";

                break;
            case 1:
                strJournalart = clsJournal.const_Journalart_EingangJournal;
                Default_SiteUserInfotext = "Journal -> Eingänge";
                break;
            case 2:
                strJournalart = clsJournal.const_Journalart_AusgangJournal;
                Default_SiteUserInfotext = "Journal  -> Ausgänge";
                break;
        }
        myMasterPage.SetLableCaption(true, this.Default_SiteUserInfotext);
        this.journal.GetJournalDaten(strJournalart, login.User.clCompany);

        this.journal.dtJournal.DefaultView.RowFilter = string.Empty;
        //Filter
        if (
            (this.login.SearchLieferant != null) &&
            (
                (!this.login.SearchLieferant.Equals(string.Empty)) &&
                (!this.login.SearchLieferant.Equals(clsJournal.const_ComboLieferant_FirstItem))
            )
           )
        {
            this.journal.dtJournal.DefaultView.RowFilter = "Auftraggeber LIKE '" + this.login.SearchLieferant + "'";
        }
        if (this.login.SearchRowFilterString != null)
        {
            if (!this.login.SearchRowFilterString.Equals(string.Empty))
            {
                if (this.journal.dtJournal.DefaultView.RowFilter.ToString().Equals(string.Empty))
                {
                    this.journal.dtJournal.DefaultView.RowFilter = this.login.SearchRowFilterString;
                }
                else
                {
                    this.journal.dtJournal.DefaultView.RowFilter = this.journal.dtJournal.DefaultView.RowFilter +" AND " + this.login.SearchRowFilterString;
                }
            }
        }

        //Ermitteln der Gesamtanzahl
        if (journal.dtJournal.Columns.Contains("Brutto"))
        {
            object sumBrutto =((DataTable)journal.dtJournal.DefaultView.ToTable()).Compute("Sum(Brutto)", "");
            Decimal.TryParse(sumBrutto.ToString(), out GesamtBrutto);
            this.tbGesamtGewicht.Value = (double)GesamtBrutto;
        }
        if (journal.dtJournal.Columns.Contains("ArtikelID"))
        {
            object countAnzahl = ((DataTable)journal.dtJournal.DefaultView.ToTable()).Compute("COUNT(ArtikelID)", "");
            Int32.TryParse(countAnzahl.ToString(), out GesamtAnzahl);
            this.tbGeamtAnzahl.Value = (double)GesamtAnzahl;
        }
        this.dgv.MasterTableView.VirtualItemCount = journal.dtJournal.DefaultView.Count;
        this.dgv.ExportSettings.FileName = Default_FileName_ToExport;
        this.dgv.ExportSettings.ExportOnlyData = true;
        this.dgv.ExportSettings.Pdf.PageTitle = this.Default_FileName_ToExport;
        clsTelerikGLHelper.ReorderGridSourceDataTable(ref this.journal.dtJournal, ref this.login.Views);
        clsTelerikGLHelper.SortDataTableSource(ref this.journal.dtJournal, ref this.login.Views);
        InitComboLieferant();
        this.dgv.DataSource = journal.dtJournal.DefaultView;
    }
    ///<summary>Journal / InitComboLieferant</summary>
    ///<remarks></remarks>
    private void InitComboLieferant()
    {
        this.comboLieferant.DataValueField = "Auftraggeber";
        this.comboLieferant.DataTextField="Auftraggeber";
        this.comboLieferant.DataSource = this.journal.dtLieferanten;
        this.comboLieferant.DataBind();

        for (Int32 i = 0; i <= this.comboLieferant.Items.Count - 1; i++)
        {
            string stSearch = this.comboLieferant.Items[i].ToString();
            if(stSearch.Equals(clsJournal.const_ComboLieferant_FirstItem))
            {
                this.comboLieferant.Items[i].Selected=true;
                i = this.comboLieferant.Items.Count;
            }
        }
    }
    ///<summary>Journal / dgv_GridExporting</summary>
    ///<remarks></remarks>
    protected void dgv_GridExporting(object sender, Telerik.Web.UI.GridExportingArgs e)
    {
        switch (e.ExportType)
        {
            case ExportType.Excel:
                //do something with the e.ExportOutput value  
                this.dgv.MasterTableView.ExportToExcel();
                myMasterPage.SetLInfoText(false, "Export wurde erfolgreich durchgeführt!");
                break;
            case ExportType.ExcelML:
                //do something with the e.ExportOutput value     
                break;
            case ExportType.Word:
                //do something with the e.ExportOutput value       
                break;
            case ExportType.Csv:
                //do something with the e.ExportOutput value    
                break;
            case ExportType.Pdf:
                ////you can't change the output here - use the PdfExporting event instead   
                //foreach (GridDataItem item in dgv.Items)
                //    item.Style["background-color"] = "#888888";
                //this.dgv.MasterTableView.ExportToPdf();
                break;
        }
    }
    ///<summary>Journal / dtpVon_SelectedDateChanged</summary>
    ///<remarks></remarks>
    protected void dtpVon_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        if ((DateTime)e.NewDate < this.dtpVon.MinDate)
        {
            this.journal.dtVon = this.dtpVon.MinDate;
        }
        else
        if ((DateTime)e.NewDate > this.dtpVon.MaxDate)
        {
            this.journal.dtVon = this.dtpVon.MinDate;
        }
        else
        {
            this.journal.dtVon = (DateTime)e.NewDate;
        }
        this.dtpBis.MinDate = (DateTime)dtpVon.SelectedDate;
        this.journal.dtBis = Functions.GetLastDayOfMonth(this.journal.dtVon);

        login.Journal = this.journal.Copy();
        Session["Login"] = login;
        this.dtpBis.SelectedDate = this.login.Journal.dtBis;
        this.dgv.Rebind();
    }
    ///<summary>Journal / dtpBis_SelectedDateChanged</summary>
    ///<remarks></remarks>
    protected void dtpBis_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        if ((DateTime)e.NewDate < this.dtpBis.MinDate)
        {
            this.journal.dtBis = this.dtpBis.MinDate;
        }
        else
            if ((DateTime)e.NewDate > this.dtpBis.MaxDate)
            {
                this.journal.dtBis = this.dtpBis.MinDate;
            }
            else
            {
                this.journal.dtBis = (DateTime)e.NewDate;
            }
        this.journal.dtVon = (DateTime)this.dtpVon.SelectedDate;
        login.Journal = this.journal.Copy();
        Session["Login"] = login;
        this.dgv.Rebind();
    }
    ///<summary>Journal / dgv_ItemDataBound</summary>
    ///<remarks></remarks>
    protected void dgv_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            if (dataItem is GridDataItem)
            {
                string strCallStatus = string.Empty;
                if (this.dgv.MasterTableView.GetColumnSafe("CallStatus") != null)
                {
                    strCallStatus = dataItem["CallStatus"].Text;
                }
                clsTelerikGLHelper.GridFormatting(ref this.dgv, ref dataItem, ref login.Views.ListTableView);
            }
        }
    }
    ///<summary>Journal / dgv_ColumnCreated</summary>
    ///<remarks></remarks>
    protected void dgv_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
    {
        if (login != null)
        {
            if (login.Views.ListTableView.Count > 0)
            {
                clsViews tmpView = login.Views.ListTableView.Find(x => x.ColumnName == e.Column.UniqueName);
                if (tmpView != null)
                {
                    GridColumnCollection cols = this.dgv.MasterTableView.Columns;
                    try
                    {
                        GridColumn cSet = cols.FindByUniqueName("CheckBoxTemplateColumn");
                        iStartOrderIndex = cSet.OrderIndex;
                    }
                    catch (Exception ex)
                    { 
                    }
                    e.Column.OrderIndex = tmpView.OrderIndex + iStartOrderIndex;
                    e.Column.Display = tmpView.IsDisplayed;
                    e.Column.Visible = tmpView.IsVisible;
                    e.Column.HeaderText = tmpView.ColumnViewName;
                    if (tmpView.ColumnName.Equals("Werksnummer"))
                    {
                        e.Column.HeaderText = tmpView.ColumnViewName;
                    }
                    switch (e.Column.UniqueName)
                    {
                        case "ArtikelID":
                        case "AbrufID":
                            //ArtikelID muss immer Visible = true sein, damit der Wert ermittelt werden kann
                            if (!tmpView.IsVisible)
                            {
                                e.Column.Visible = true;
                            }
                            break;
                        case "GArtID":
                            (e.Column as GridBoundColumn).Aggregate = GridAggregateFunction.Count;
                            (e.Column as GridBoundColumn).FooterAggregateFormatString = "{0:N0}";
                            break;

                        case "Brutto":
                            (e.Column as GridBoundColumn).Aggregate = GridAggregateFunction.Sum;
                            (e.Column as GridBoundColumn).FooterAggregateFormatString = "{0:N2}";
                            break;

                        case "Dicke":
                        case "Breite":
                        case "Menge":
                            e.Column.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                            break;
                    }
                }
                else
                {
                    switch (e.Column.UniqueName)
                    {
                        case "ArtikelID":
                        case "AbrufID":
                            e.Column.Display = false;
                            //ArtikelID muss immer Visible = true sein, damit der Wert ermittelt werden kann
                            e.Column.Visible = true;
                            break;
                        case "CheckBoxTemplateColumn":
                        case "ExpandColumn":
                            break;
                        case "Selected":
                            e.Column.Visible = false;
                            break;
                        default:
                            e.Column.Visible = false;
                            break;
                    }
                }
            }
        }
    }
    ///<summary>Journal / comboSearch_SelectedIndexChanged</summary>
    ///<remarks></remarks>
    protected void comboSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strDataField = this.comboSearch.Text;
        login.SearchDataField = this.comboSearch.Text;
        Session["Login"] = login;
    }
    ///<summary>Journal / comboLieferant_SelectedIndexChanged</summary>
    ///<remarks></remarks>
    protected void comboLieferant_SelectedIndexChanged(object sender, EventArgs e)
    {
        login.SearchLieferant = this.comboSearch.Text;
        Session["Login"] = login;     
    }
    ///<summary>Journal / txtSearch_TextChanged</summary>
    ///<remarks></remarks>
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        string strTxtSerach = this.txtSearch.Text;
        login.SearchText = this.txtSearch.Text;
        Session["Login"] = login;
    }
    ///<summary>AbrufList / RadToolBar1_ButtonClick</summary>
    ///<remarks></remarks>
    protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
    {
        RadToolBarButton btn = e.Item as RadToolBarButton;
        string strCommand = btn.CommandName.ToString();
        switch (strCommand)
        {
            case const_MenuButtonComman_StartSearch:
                string SearchString = string.Empty;
                SearchString = SearchRowFilter.GetSearchstring(login.SearchDataField, login.SearchText, SearchString);
                login.SearchRowFilterString = SearchString;
                string strLieferant = string.Empty;
                if (!comboLieferant.Text.Equals(clsJournal.const_ComboLieferant_FirstItem))
                {
                    strLieferant = comboLieferant.Text;
                }
                login.SearchLieferant = strLieferant;
                break;

            case const_MenuButtonComman_ClearSearch:
                this.journal.dtVon = Functions.GetFirstDayOfMonth(DateTime.Now);
                this.journal.dtBis = Functions.GetLastDayOfMonth(DateTime.Now);
                dtpVon.SelectedDate = this.journal.dtVon;
                dtpBis.SelectedDate = this.journal.dtBis;

                this.comboSearch.SelectedIndex = 0;
                this.txtSearch.Text = string.Empty;
                this.comboLieferant.SelectedIndex = -1;
                login.SearchDataField = string.Empty;
                login.SearchText = string.Empty;
                login.SearchLieferant = string.Empty;
                login.SearchRowFilterString = string.Empty;
                login.Journal = this.journal.Copy();
                break;
        }
        Session["Login"] = login;
        this.dgv.Rebind();
    }
}