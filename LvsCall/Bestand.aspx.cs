using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


public partial class Bestand : System.Web.UI.Page
{

    MasterPage myMasterPage;
    clsLogin login;
    clsBestand bestand;
    private string Default_FileName_ToExport = DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + "_" + "Bestand";
    private string Default_SiteUserInfotext = "Bestand:";
    Int32 BID = 0;
    private Int32 ArtikelAnzahl = 0;
    private Int32 Anzahl = 0;
    private decimal Netto = 0;
    private decimal Brutto = 0;
    private Int32 GesamtAnzahl = 0;
    private decimal GesamtBrutto = 0;
    private Int32 iStartOrderIndex = 0;


    ///<summary>Bestand / Page_Load</summary>
    ///<remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        login = new clsLogin();
        if (Session["Login"] != null)
        {
            login = (clsLogin)Session["Login"];
            login.bInfoText = true;
            login.InfoText = string.Empty;
            bestand = login.Bestand.Copy();
            //Bestandart ermitteln
            if (Request.QueryString["BID"] != null)
            {
                BID = Convert.ToInt32(Request.QueryString["BID"]);

            }
            else
            {
                BID = 0;
            }
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
            if (!IsPostBack)
            {
                //this.bestand.BestandVon = Functions.GetFirstDayOfMonth(DateTime.Now);
                //this.bestand.BestandBis = Functions.GetLastDayOfMonth(this.bestand.BestandVon);
                //dtpVon.SelectedDate = this.bestand.BestandVon;
                //dtpBis.SelectedDate = this.bestand.BestandBis;
            }
            else
            {
                //this.bestand.BestandVon = (DateTime)this.dtpVon.SelectedDate;
                //this.bestand.BestandBis = (DateTime)this.dtpBis.SelectedDate;
            }
        }
    }
    ///<summary>Bestand / dtpVon_SelectedDateChanged</summary>
    ///<remarks></remarks>
    protected void dtpVon_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        //if ((DateTime)e.NewDate < this.dtpVon.MinDate)
        //{
        //    this.bestand.BestandVon = this.dtpVon.MinDate;
        //}
        //else
        //    if ((DateTime)e.NewDate > this.dtpVon.MaxDate)
        //    {
        //        this.bestand.BestandVon = this.dtpVon.MinDate;
        //    }
        //    else
        //    {
        //        this.bestand.BestandVon = (DateTime)e.NewDate;
        //    }
        //this.bestand.BestandBis = Functions.GetLastDayOfMonth(this.bestand.BestandVon);

        //login.Bestand = this.bestand.Copy();
        //Session["Login"] = login;
        //this.dtpBis.SelectedDate = this.bestand.BestandBis;
        //this.dgv.Rebind();
    }
    ///<summary>Bestand / dtpBis_SelectedDateChanged</summary>
    ///<remarks></remarks>
    protected void dtpBis_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        //if ((DateTime)e.NewDate < this.dtpBis.MinDate)
        //{
        //    this.bestand.BestandBis = this.dtpBis.MinDate;
        //}
        //else
        //    if ((DateTime)e.NewDate > this.dtpBis.MaxDate)
        //    {
        //        this.bestand.BestandBis = this.dtpBis.MinDate;
        //    }
        //    else
        //    {
        //        this.bestand.BestandBis = (DateTime)e.NewDate;
        //    }
        //this.bestand.BestandVon = (DateTime)this.dtpVon.SelectedDate;
        //login.Bestand = this.bestand.Copy();
        //Session["Login"] = login;
        //this.dgv.Rebind();
    }
    ///<summary>Bestand / dgv_NeedDataSource</summary>
    ///<remarks></remarks>
    protected void dgv_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        bool bDateTimePicker = true;
        string strBEstandArt = string.Empty;
        login.Views.FillListTableView(clsViews.const_ViewTableName_Bestandsliste);
        login.Views.FillListSort(clsViews.const_ViewTableName_Bestandsliste); 
        switch (BID)
        {
            case 0:
                strBEstandArt = clsBestand.const_Bestandart_Tagesbestand;
                Default_SiteUserInfotext = "Bestand -> Tagesbestand";
                //Zeitraumauswahl ausblenden
                //this.dtpVon.Enabled = false;
                //this.dtpBis.Enabled = false;
                this.bestand.BestandVon = DateTime.Now.Date.AddDays(1);
                break;

            case 1:
                //strBEstandArt = clsBestand.const_Bestandart_BestandMaterial;
                //Default_SiteUserInfotext = "Bestand -> Materialauswahl";
                break;
            case 2:
                strBEstandArt = clsBestand.const_Bestandart_BestandSPL;
                Default_SiteUserInfotext = "Bestand -> Sperrlager";
                //Datetimepicker können ausgeblendet werden
                //this.dtpVon.Enabled = false;
                //this.dtpBis.Enabled = false;
                this.bestand.BestandVon = DateTime.Now.Date.AddDays(1);
                this.bestand.BestandBis = DateTime.Now.Date;
                break;

            case 3:
                strBEstandArt = clsBestand.const_Bestandart_TagesbestandEigen;
                Default_SiteUserInfotext = "Bestand -> Tagesbestand [VW]";
                //this.dtpVon.Enabled = false;
                //this.dtpBis.Enabled = false;
                this.bestand.BestandVon = DateTime.Now.Date.AddDays(1);

                break;
        }
        myMasterPage.SetLableCaption(true, this.Default_SiteUserInfotext);
        DataTable dtBestand = bestand.GetBestand(strBEstandArt, login.User.clCompany, false,true);
        
        //Gesamtgewicht und Gesamtanzahl ermitteln
        if (dtBestand.Columns.Contains("GArtID"))
        {
            GesamtAnzahl = 0;
            object countAnzahl = dtBestand.Compute("COUNT(GArtID)", "");
            Int32.TryParse(countAnzahl.ToString(), out GesamtAnzahl);
            tbGeamtAnzahl.Value = GesamtAnzahl;
        }
        if (dtBestand.Columns.Contains("Brutto"))
        {
            GesamtBrutto = 0;
            object sumBrutto = dtBestand.Compute("Sum(Brutto)", "");
            Decimal.TryParse(sumBrutto.ToString(), out GesamtBrutto);
            tbGesamtGewicht.Value = (double)GesamtBrutto;
        }

        this.dgv.MasterTableView.VirtualItemCount = dtBestand.DefaultView.Count;
        this.dgv.ExportSettings.FileName = Default_FileName_ToExport;
        this.dgv.ExportSettings.ExportOnlyData = true;
        this.dgv.ExportSettings.Pdf.PageTitle = this.Default_FileName_ToExport;
        this.dgv.ClientSettings.Resizing.AllowColumnResize = true;

        //order dtBestand.DefaultView        
        clsTelerikGLHelper.ReorderGridSourceDataTable(ref dtBestand, ref login.Views);
        clsTelerikGLHelper.SortDataTableSource(ref dtBestand, ref this.login.Views);
        this.dgv.DataSource = dtBestand.DefaultView;
    }
    ///<summary>Bestand / dgv_ItemDataBound</summary>
    ///<remarks></remarks>
    protected void dgv_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            if (dataItem != null)
            {
                string Errortext = clsTelerikGLHelper.GridFormatting(ref this.dgv, ref dataItem, ref login.Views.ListTableView);
            }
        }
    }
    ///<summary>Bestand / dgv_ColumnCreating</summary>
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
                        iStartOrderIndex = 0; //; cSet.OrderIndex;
                    }
                    catch (Exception ex)
                    { }
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
                        case "CheckBoxTemplateColumn":
                        case "ExpandColumn":
                            break;

                        case "GArtID":
                            (e.Column as GridBoundColumn).Aggregate = GridAggregateFunction.Count;
                            (e.Column as GridBoundColumn).FooterAggregateFormatString = "{0:N0}";

                            break;
                        case "Brutto":
                            (e.Column as GridBoundColumn).Aggregate = GridAggregateFunction.Sum;
                            (e.Column as GridBoundColumn).FooterAggregateFormatString = "{0:N2}";
                            e.Column.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
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

}