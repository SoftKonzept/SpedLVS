using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Abruf : System.Web.UI.Page
{
    private clsWebPages wPage = new clsWebPages();
    public const string const_MenuButtonComman_AddAbruf = "AddAbruf";
    public const string const_MenuButtonComman_AddUB = "AddUB";
    public const string const_MenuButtonComman_zurueck = "BackMain";
    public const string const_MenuButtonComman_ArtikelAufruf = "ShowArtikel";
    public const string const_MenuButtonComman_weiter = "NextStep";

    MasterPage myMasterPage;
    clsLogin login;
    clsAbruf abruf;
    string strAbrufAktion = string.Empty;
    private string Default_FileName_ToExport = DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + "_" + "Abruf";
    private string Default_SiteUserInfotext = "Abruf";
    Int32 AID = 0;
    private Int32 Anzahl = 0;
    private decimal Brutto = 0;
    private Int32 GesamtAnzahl = 0;
    private decimal GesamtBrutto = 0;
    private Int32 SelectedAnzahl = 0;
    private decimal SelectedGewicht = 0;
    private Int32 iStartOrderIndex = 0;

    /******************************************************************
     * 
     * ***************************************************************/
    ///<summary>Abruf / Page_Load</summary>
    ///<remarks></remarks
    protected void Page_Load(object sender, EventArgs e)
    {
        login = new clsLogin();

        //Table Enabled
        this.TableEingabe.Enabled = false;

        if (Session["Login"] != null)
        {
            login = (clsLogin)Session["Login"];
            login.bInfoText = true;
            login.InfoText = string.Empty;
            abruf = login.Abruf.Copy();
            //abruf.SelectedGArtID = 0;

            if (Request.QueryString["AID"] != null)
            {
                AID = Convert.ToInt32(Request.QueryString["AID"]);
                this.login.AID = AID;
            }
            else
            {
                AID = this.login.AID;
            }
        }
        else
        {
            login.InitClass();
            Session["Login"] = login;
            Session["AID"] = AID;
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

            if (!IsPostBack)
            {
            }
            else
            {
                CheckForSelectedGut();
            }
            if (Page.AppRelativeVirtualPath == login.AppRelativeVirtualPath)
            {
                login.AppRelativeVirtualPath = string.Empty;
                login.NextPagePath = string.Empty;
                this.login.Abruf = this.abruf;
                Session["Login"] = login;
                Session["AID"] = AID;
            }
        }        
    }
    ///<summary>Abruf / dgv_NeedDataSource</summary>
    ///<remarks></remarks>
    protected void dgv_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        bool bDateTimePicker = true;
        string strBestandArt = string.Empty;
        strAbrufAktion = string.Empty;
        switch (AID)
        {
            case 0:
                this.dgv.CommandItemStyle.BackColor = myMasterPage.dgvBackColor_Abruf;
                strBestandArt = clsBestand.const_Bestandart_Tagesbestand;
                strAbrufAktion = clsAbruf.const_AbrufAktion_Abruf;
                Default_SiteUserInfotext = "Abruf";
                login.Views.FillListTableView(clsViews.const_ViewTableName_Abruf);
                login.Views.FillListSort(clsViews.const_ViewTableName_Abruf);  
                break;
            case 1:
                this.dgv.CommandItemStyle.BackColor = myMasterPage.dgvBackColor_UB;
                strBestandArt = clsBestand.const_Bestandart_Tagesbestand;
                strAbrufAktion = clsAbruf.const_AbrufAktion_UB;
                Default_SiteUserInfotext = "Umbuchung";
                login.Views.FillListTableView(clsViews.const_ViewTableName_Abruf);
                login.Views.FillListSort(clsViews.const_ViewTableName_Abruf);  
                break;
        }
        this.abruf.Bestand.dtBestand = this.abruf.GetBestandToday(strAbrufAktion, login.User.clCompany, true, true);
        myMasterPage.SetLableCaption(true, this.Default_SiteUserInfotext);
        this.dgv.MasterTableView.VirtualItemCount = this.abruf.Bestand.dtBestand.DefaultView.Count;

        this.dgv.ExportSettings.FileName = Default_FileName_ToExport;
        this.dgv.ExportSettings.ExportOnlyData = true;
        this.dgv.ExportSettings.Pdf.PageTitle = this.Default_FileName_ToExport;
        
        this.dgv.ClientSettings.Resizing.AllowColumnResize = true;
        //Gesamtgewicht und Gesamtanzahl ermitteln
        if (this.abruf.Bestand.dtBestand.Columns.Contains("GArtID"))
        {
            GesamtAnzahl = 0;
            object countAnzahl = this.abruf.Bestand.dtBestand.Compute("COUNT(GArtID)", "");
            Int32.TryParse(countAnzahl.ToString(), out GesamtAnzahl);
            tbGeamtAnzahl.Value = GesamtAnzahl;
        }
        if (this.abruf.Bestand.dtBestand.Columns.Contains("Brutto"))
        {
            GesamtBrutto = 0;
            object sumBrutto = (this.abruf.Bestand.dtBestand.DefaultView.ToTable()).Compute("Sum(Brutto)", "");
            Decimal.TryParse(sumBrutto.ToString(), out GesamtBrutto);
            tbGesamtGewicht.Value = (double)GesamtBrutto;
        }
        //order dtBestand.DefaultView        
        //SortDataTableSource(ref this.abruf.Bestand.dtBestand);
        clsTelerikGLHelper.SortDataTableSource(ref this.abruf.Bestand.dtBestand, ref this.login.Views);
        this.dgv.DataSource = this.abruf.Bestand.dtBestand.DefaultView;
    }
    /////<summary>Abruf / SortDataTableSource</summary>
    /////<remarks></remarks>
    //protected void SortDataTableSource(ref DataTable mydt)
    //{
    //    mydt.DefaultView.Sort = string.Empty;
    //    if (mydt.Rows.Count > 0)
    //    {
    //        string strSort = string.Empty;
    //        for (Int32 i = 0; i <= this.login.Views.ListSort.Count - 1; i++)
    //        {
    //            clsViews tmpView = (clsViews)this.login.Views.ListSort[i];
    //            if (tmpView != null)
    //            {
    //                if (!strSort.Equals(string.Empty))
    //                {
    //                    strSort = strSort + ", ";
    //                }
    //                strSort = strSort + tmpView.ColumnName;
    //                if (tmpView.SortOrderDesc)
    //                {
    //                    strSort = strSort + " DESC ";
    //                }
    //                else
    //                {
    //                    strSort = strSort + " ASC ";
    //                }
    //            }
    //        }
    //        mydt.DefaultView.Sort = strSort;
    //    }
    //}
    ///<summary>Abruf / ToggleSelectedState</summary>
    ///<remarks></remarks>
    protected void ToggleSelectedState(object sender, EventArgs e)
    {
        SelectedAnzahl = 0;
        SelectedGewicht = 0;
        CheckBox headerCheckBox = (sender as CheckBox);
        foreach (GridDataItem dataItem in this.dgv.MasterTableView.Items)
        {
            (dataItem.FindControl("cbSelect") as CheckBox).Checked = headerCheckBox.Checked;
            dataItem.Selected = headerCheckBox.Checked;
            if (headerCheckBox.Checked)
            {
                GridDataItem tmpItm = dataItem;
                SumAndCountSelcetedGArt(ref tmpItm);
            }
        }
        this.tbAnzahlArtikel.Value = SelectedAnzahl;
        this.tbGewichtArtikel.Value = (double)SelectedGewicht;
    }
    ///<summary>Abruf / ToggleRowSelection</summary>
    ///<remarks></remarks>
    protected void ToggleRowSelection(object sender, EventArgs e)
    {
        SelectedAnzahl = 0;
        SelectedGewicht = 0;
        ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
        bool checkHeader = true;
        foreach (GridDataItem dataItem in this.dgv.MasterTableView.Items)
        {
            if (!(dataItem.FindControl("cbSelect") as CheckBox).Checked)
            {
                checkHeader = false;
                //break;
            }
            else
            {
                GridDataItem tmpItm = dataItem;
                SumAndCountSelcetedGArt(ref tmpItm);
            }
        }
        GridHeaderItem headerItem = this.dgv.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
        (headerItem.FindControl("headerChkbox") as CheckBox).Checked = false;
        this.tbAnzahlArtikel.Value = SelectedAnzahl;
        this.tbGewichtArtikel.Value = (double)SelectedGewicht;
    }
    ///<summary>Abruf / SumAndCountSelcetedGArt</summary>
    ///<remarks></remarks>
    private void SumAndCountSelcetedGArt(ref GridDataItem myItem)
    {
        Int32 ID = 0;
        Int32.TryParse(myItem["GArtID"].Text.ToString(), out ID);
        if (ID >= 0)
        {
            SelectedAnzahl++;
            decimal decTmp = 0;
            Decimal.TryParse(myItem["Brutto"].Text.ToString(), out decTmp);
            SelectedGewicht = SelectedGewicht + decTmp;
        }
    }
    ///<summary>Abruf / tbAbladestelle_TextChanged</summary>
    ///<remarks></remarks>
    protected void tbAbladestelle_TextChanged(object sender, EventArgs e)
    {
        if (sender is RadTextBox)
        {
            RadTextBox tb = (RadTextBox)sender;
            this.abruf.Abladestelle = tb.Text;
            login.Abruf = this.abruf.Copy();
            Session["Login"] = login;
        }
    }
    ///<summary>Abruf / tbReferenz_TextChanged</summary>
    ///<remarks></remarks>
    protected void tbReferenz_TextChanged(object sender, EventArgs e)
    {
        if (sender is RadTextBox)
        {
            RadTextBox tb = (RadTextBox)sender;
            this.abruf.Referenz = tb.Text;
            login.Abruf = this.abruf.Copy();
            Session["Login"] = login;
        }
    }
    ///<summary>Abruf / dgv_ColumnCreated</summary>
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
                    GridColumn cSet = cols.FindByUniqueName("CheckBoxTemplateColumn");
                    iStartOrderIndex = cSet.OrderIndex;

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
    ///<summary>Abruf / dgv_ItemDataBound</summary>
    ///<remarks></remarks>
    protected void dgv_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            if (dataItem != null)
            {
                string Errortext = clsTelerikGLHelper.GridFormatting(ref this.dgv, ref dataItem, ref login.Views.ListTableView);
                 //-----------------------------------Footer
                if (e.Item is GridFooterItem)
                {
                    //GridFooterItem footerItem = e.Item as GridFooterItem;
                    //try
                    //{
                    //    if (footerItem["GArtID"] != null)
                    //    {
                    //        //footerItem["LVSNr"].Text = "Datensätze Seite [Stk.]: " + ArtikelAnzahl.ToString();
                    //        footerItem["GArtID"].Controls.Add(new LiteralControl("Datensätze Seite [Stk.]: "));
                    //        footerItem["GArtID"].Controls.Add(new LiteralControl(Anzahl.ToString() + "<br/>"));
                    //        footerItem["GArtID"].Controls.Add(new LiteralControl("Gesamtdatensätze [Stk.]: "));
                    //        footerItem["GArtID"].Controls.Add(new LiteralControl(GesamtAnzahl.ToString() + "<br/>"));
                    //    }
                    //}
                    //catch (Exception ex)
                    //{ }
                    //try
                    //{
                    //    if (footerItem["Brutto"] != null)
                    //    {
                    //        //footerItem["Brutto"].Text = "Gewicht Seite [kg]: " + Functions.FormatDecimal(Brutto) + "<br/>";
                    //        footerItem["Brutto"].Controls.Add(new LiteralControl("Gewicht Seite [kg]: "));
                    //        footerItem["Brutto"].Controls.Add(new LiteralControl(Functions.FormatDecimal(Brutto) + "<br/>"));
                    //        footerItem["Brutto"].Controls.Add(new LiteralControl("Gesamt-Brutto [kg]: "));
                    //        footerItem["Brutto"].Controls.Add(new LiteralControl(Functions.FormatDecimal(GesamtBrutto) + "<br/>"));
                    //    }
                    //}
                    //catch (Exception ex)
                    //{ }
                }
            }
        }
    }
    ///<summary>Abruf / dgv_ItemDataBound</summary>
    ///<remarks></remarks>
    protected void tbarMenu_ButtonClick(object sender, RadToolBarEventArgs e)
    {
        login.SearchRowFilterString = string.Empty;
        RadToolBarButton btn = e.Item as RadToolBarButton;
        string strCommand = btn.CommandName.ToString();

        switch (strCommand)
        {
            case const_MenuButtonComman_AddAbruf:
            case const_MenuButtonComman_AddUB:
                break;

            case const_MenuButtonComman_zurueck:
                //login.Abruf.SelectedGArtID = 0;
                login.Abruf.ListSelectedGArtID.Clear();
                login.AppRelativeVirtualPath = "~/";
                login.NextPagePath = wPage.Page_Main;
                Session["Login"] = login;
                Page.Response.Redirect(login.NextPagePath);
                break;

            case const_MenuButtonComman_weiter:
                if (CheckForSelectedGut())
                {
                    //Ausgabe liste wird geladen
                    myMasterPage.SetLInfoText(true, "Artikelliste der gewählten Güterarten wird geladen...");
                    login.AppRelativeVirtualPath = "~/";
                    login.NextPagePath = wPage.Page_Abruf_ArtikelToSelect;
                    Session["Login"] = login;
                    Page.Response.Redirect(login.NextPagePath);
                }
                else
                { 
                    //es wurden keine Daten ausgewählt
                    myMasterPage.SetLInfoText(true, "Es wurde keine Güterart ausgewählt - Bitte Güterart auswählen...");
                }
                break;

            default:
                login.Abruf.ListSelectedGArtID.Clear();
                login.AppRelativeVirtualPath = "~/";
                login.NextPagePath = wPage.Page_Abruf;
                Session["Login"] = login;
                Page.Response.Redirect(login.NextPagePath);
                break;
        }
    }
    ///<summary>Abruf / CheckForSelectedGut</summary>
    ///<remarks></remarks>
    private bool CheckForSelectedGut()
    {
        bool bReturn = false;
        login.Abruf.ListSelectedGArtID.Clear();
        foreach (GridDataItem dataItem in this.dgv.MasterTableView.GetSelectedItems())
        {
            Int32 GArtID = 0;
            Int32.TryParse(dataItem["GArtID"].Text.ToString(), out GArtID);
            if (GArtID > 0)
            {
                if (!login.Abruf.ListSelectedGArtID.Contains(GArtID))
                {
                    login.Abruf.ListSelectedGArtID.Add(GArtID);
                }
            }
        }
        bReturn = (login.Abruf.ListSelectedGArtID.Count > 0);
        return bReturn;
    }


}