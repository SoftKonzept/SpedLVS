using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class ArtikelListToSelect : System.Web.UI.Page
{
    private clsWebPages wPage = new clsWebPages();
    public const string const_MenuButtonComman_AddAbruf = "AddAbruf";
    public const string const_MenuButtonComman_AddUB = "AddUB";
    public const string const_MenuButtonComman_StartSearch = "StartSearch";
    public const string const_MenuButtonComman_ClearSearch = "ClearSearch";
    public const string const_MenuButtonComman_weiter = "NextStep";
    public const string const_MenuButtonComman_Back = "Back";

    public const string const_ViewTableName_AbrufArtToSelect = "AbrufArtToSelect";
    public const string const_ViewTableName_ExcelExport = "ExcelExport";
    public const string const_ViewTableName_Refresh = "Refresh";
    MasterPage myMasterPage;
    clsLogin login;
    clsAbruf abruf;
    string strAbrufAktion = string.Empty;
    private string Default_FileName_ToExport = DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + "_" + "Abruf";
    private string Default_SiteUserInfotext = string.Empty;
    private string Abruf_SiteUserInfoText = "Abruf -> Artikelauswahl";
    private string UB_SiteUserInfoText = "Umbuchung -> Artikelauswahl";
    Int32 AID = 0;
    private Int32 iStartOrderIndex = 0;
    private Int32 SelectedArtikelAnzahl = 0;
    private decimal SelectedArtikelBrutto = 0;
    private Int32 GesamtAnzahl = 0;
    private Int32 GesamtMenge = 0;
    private decimal GesamtBrutto = 0;

    /******************************************************************
     * 
     * ***************************************************************/
    ///<summary>ArtikelListToSelect / Page_Load</summary>
    ///<remarks></remarks
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Login"] != null)
        {
            login = new clsLogin();
            login = (clsLogin)Session["Login"];
            login.bInfoText = true;
            login.InfoText = string.Empty;
            abruf = login.Abruf.Copy();
            //Journalart ermitteln
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
                this.abruf.Abladestelle = string.Empty;
                this.abruf.Referenz = string.Empty;
                this.abruf.EintreffDatum = DateTime.Today;
               
                DateTime dtTime = Convert.ToDateTime("01.01.1900 " + DateTime.Now.AddHours(1).Hour.ToString("00") + ":00:00");
                this.abruf.EintreffZeit = dtTime;

                tbAbladestelle.Text = this.abruf.Abladestelle;
                tbReferenz.Text = this.abruf.Referenz;

                dtpEintrefftermin.MinDate = DateTime.Now.Date;
                if (this.abruf.EintreffDatum < dtpEintrefftermin.MinDate)
                {
                    dtpEintrefftermin.SelectedDate = dtpEintrefftermin.MinDate;
                }
                else
                {
                    dtpEintrefftermin.SelectedDate = this.abruf.EintreffDatum;
                }
  
                dtpEintreffZeit.MinDate = LVS.clsSystem.const_DefaultDateTimeValue_Min;
                if (this.abruf.EintreffZeit < LVS.clsSystem.const_DefaultDateTimeValue_Min)
                {
                    dtpEintreffZeit.SelectedDate = LVS.clsSystem.const_DefaultDateTimeValue_Min;
                }
                else
                {
                    dtpEintreffZeit.SelectedDate = this.abruf.EintreffZeit;
                }
            }
            else
            {
                this.abruf.Abladestelle = this.tbAbladestelle.Text.ToString();
                this.abruf.Referenz = this.tbReferenz.Text.ToString();
                this.abruf.EintreffDatum = (DateTime)dtpEintrefftermin.SelectedDate;
                this.abruf.EintreffZeit = (DateTime)dtpEintreffZeit.SelectedDate;
            }
            login.LastPagePath = Page.AppRelativeVirtualPath;
            if (Page.AppRelativeVirtualPath == login.AppRelativeVirtualPath)
            {
                login.AppRelativeVirtualPath = string.Empty;
                login.NextPagePath = string.Empty;
                this.login.Abruf = this.abruf;
                Session["Login"] = login;
            }
        }
        else
        {
            login = new clsLogin();
            login.LoggedIn = false;
            login.AppRelativeVirtualPath = "~/";
            login.NextPagePath = wPage.Page_LogOut;
            login.LastPagePath = string.Empty;
            Session["Login"] = null;
            Page.Response.Redirect(login.NextPagePath);
        }
    }
    ///<summary>ArtikelListToSelect / dgv_NeedDataSource</summary>
    ///<remarks></remarks>
    protected void dgv_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        SelectedArtikelAnzahl = 0;
        SelectedArtikelBrutto = 0;
        this.tbAnzahlArtikel.Value = SelectedArtikelAnzahl;
        this.tbGewichtArtikel.Value = (double)SelectedArtikelBrutto;
        
        bool bDateTimePicker = true;
        string strBestandArt = string.Empty;
        strAbrufAktion = string.Empty;
        switch (AID)
        {
            case 0:
                this.dgv.CommandItemStyle.BackColor = myMasterPage.dgvBackColor_Abruf;
                strBestandArt = clsBestand.const_Bestandart_Tagesbestand;
                strAbrufAktion = clsAbruf.const_AbrufAktion_Abruf;
                Default_SiteUserInfotext = Abruf_SiteUserInfoText;
                login.Views.FillListTableView(const_ViewTableName_AbrufArtToSelect);
                login.Views.FillListSort(const_ViewTableName_AbrufArtToSelect);  
                break;
            case 1:
                this.dgv.CommandItemStyle.BackColor = myMasterPage.dgvBackColor_UB;
                strBestandArt = clsBestand.const_Bestandart_Tagesbestand;
                strAbrufAktion = clsAbruf.const_AbrufAktion_UB;
                Default_SiteUserInfotext = UB_SiteUserInfoText;
                login.Views.FillListTableView(const_ViewTableName_AbrufArtToSelect);
                login.Views.FillListSort(const_ViewTableName_AbrufArtToSelect); 
                break;
        }
        DataTable dtTmp = this.abruf.GetBestandToday(strAbrufAktion, login.User.clCompany, true, false);
        if(dtTmp.Rows.Count>0)
        {
            if (this.abruf.ListSelectedGArtID.Count > 0)
            {
                dtTmp.DefaultView.RowFilter = "GArtID IN (" + String.Join(",", this.abruf.ListSelectedGArtID.ToArray()) + ")";
            }
        }
        this.abruf.Bestand.dtBestand = dtTmp.DefaultView.ToTable();
        myMasterPage.SetLableCaption(true, this.Default_SiteUserInfotext);
        this.dgv.MasterTableView.VirtualItemCount = this.abruf.Bestand.dtBestand.DefaultView.Count;
        this.dgv.ExportSettings.FileName = Default_FileName_ToExport;
        this.dgv.ExportSettings.ExportOnlyData = true;
        this.dgv.ExportSettings.Pdf.PageTitle = this.Default_FileName_ToExport;
        this.dgv.ClientSettings.Resizing.AllowColumnResize = true;

        if (this.abruf.DictAbrufeToInsert.Count > 0)
        {
            foreach (DataRow row in this.abruf.Bestand.dtBestand.Rows)
            {
                Int32 iTmp = 0;
                Int32.TryParse(row["ArtikelID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    if (this.abruf.DictAbrufeToInsert.ContainsKey(iTmp))
                    {
                        row["Selected"] = true;
                    }
                    else
                    {
                        row["Selected"] = false;
                    }
                }
            }
            this.abruf.Bestand.dtBestand.DefaultView.RowFilter = "Selected=false";
        }
        if (this.login.SearchRowFilterString != null)
        {
            if (!this.login.SearchRowFilterString.Equals(string.Empty))
            {
                this.abruf.Bestand.dtBestand.DefaultView.RowFilter = this.login.SearchRowFilterString;
            }
        }
        //Gesamtgewicht und Gesamtanzahl ermitteln
        if (this.abruf.Bestand.dtBestand.Columns.Contains("ArtikelID"))
        {
            GesamtAnzahl = 0;
            object countAnzahl =(this.abruf.Bestand.dtBestand.DefaultView.ToTable()).Compute("COUNT(ArtikelID)", "");
            Int32.TryParse(countAnzahl.ToString(), out GesamtAnzahl);
            tbGeamtAnzahl.Value = GesamtAnzahl;
        }
        if (this.abruf.Bestand.dtBestand.Columns.Contains("Anzahl"))
        {
            GesamtMenge = 0;
            object sumMenge = (this.abruf.Bestand.dtBestand.DefaultView.ToTable()).Compute("Sum(Anzahl)", "");
            //Decimal.TryParse(sumMenge.ToString(), out GesamtMenge);
            Int32.TryParse(sumMenge.ToString(), out GesamtMenge);
            tbGesamtMenge.Value = (double)GesamtMenge;
        }
        if (this.abruf.Bestand.dtBestand.Columns.Contains("Brutto"))
        {
            GesamtBrutto = 0;
            object sumBrutto = (this.abruf.Bestand.dtBestand.DefaultView.ToTable()).Compute("Sum(Brutto)", "");
            Decimal.TryParse(sumBrutto.ToString(), out GesamtBrutto);
            tbGesamtGewicht.Value = (double)GesamtBrutto;
        }
        //ReorderDataTableSource();
        clsTelerikGLHelper.ReorderGridSourceDataTable(ref this.abruf.Bestand.dtBestand, ref login.Views);
        this.dgv.DataSource = this.abruf.Bestand.dtBestand.DefaultView;
    }
    ///<summary>ArtikelListToSelect / ReorderDataTableSource</summary>
    ///<remarks></remarks>
    protected void ReorderDataTableSource()
    {
        if (this.abruf.Bestand.dtBestand.Columns.Contains("ArtikelID"))
        {
            this.abruf.Bestand.dtBestand.Columns["ArtikelID"].SetOrdinal(this.abruf.Bestand.dtBestand.Columns.Count - 1);
        }

        if (this.login.Views.ListTableView.Count > 0)
        {
            for (Int32 i = 0; i <= this.login.Views.ListTableView.Count - 1; i++)
            { 
                clsViews tmpView =(clsViews)this.login.Views.ListTableView[i];
                if (tmpView != null)
                {
                    if (this.abruf.Bestand.dtBestand.Columns.Contains(tmpView.ColumnName))
                    {
                        this.abruf.Bestand.dtBestand.Columns[tmpView.ColumnName].SetOrdinal(tmpView.OrderIndex);
                    }
                }
            }
        }
    }
    ///<summary>ArtikelListToSelect / RadToolBar1_ButtonClick</summary>
    ///<remarks></remarks>
    protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
    {
        login.SearchRowFilterString = string.Empty;
        RadToolBarButton btn = e.Item as RadToolBarButton;
        string strCommand = btn.CommandName.ToString();

        switch (strCommand)
        {
            case const_MenuButtonComman_Back:
                login.Abruf.ListSelectedGArtID.Clear();
                login.AppRelativeVirtualPath = "~/";
                switch (AID)
                {
                    case 0:
                        login.NextPagePath = wPage.Page_AbrufBestand;
                        break;
                    case 1:
                        login.NextPagePath = wPage.Page_UmbuchungBestand;
                        break;
                }
                login.CurrentPagePath = login.NextPagePath;
                Session["Login"] = login;
                Page.Response.Redirect(login.NextPagePath);
                break;

            case const_MenuButtonComman_AddAbruf:
                //ausgewählte Artikel zur Abruftabelle hinzufügen
                AddArtToAbrufList();
                break;

            case const_MenuButtonComman_AddUB:
                AddArtToAbrufList();
                break;

            case const_MenuButtonComman_StartSearch:
                string SearchString  = string.Empty;
                if (this.abruf.Bestand.dtBestand.Rows.Count > 0)
                {
                    SearchString = SearchRowFilter.GetSearchstring(login.SearchDataField, login.SearchText, SearchString);
                }
                login.SearchRowFilterString = SearchString;
                Session["Login"] = login;
                this.dgv.Rebind();
                break;

           case const_MenuButtonComman_ClearSearch:
                this.comboSearch.SelectedIndex = 0;
                this.txtSearch.Text = string.Empty;
                login.SearchDataField = string.Empty;
                login.SearchText = string.Empty;
                login.SearchRowFilterString = string.Empty;
                Session["Login"] = login;
                this.dgv.Rebind();
                break;

            case const_MenuButtonComman_weiter:
                login.Abruf.ListSelectedGArtID.Clear();
                login.AppRelativeVirtualPath = "~/";
                switch (AID)
                {
                    case 0:
                        login.NextPagePath = wPage.Page_AbrufList_vorgemerkteAbrufe;

                        break;
                    case 1:
                        login.NextPagePath = wPage.Page_AbrufList_vorgemerkteUB;
                        break;
                }
                login.CurrentPagePath = login.NextPagePath;
                Session["Login"] = login;
                Page.Response.Redirect(login.NextPagePath);
                break;

            case const_ViewTableName_ExcelExport:

                break;

            case const_ViewTableName_Refresh:
                this.dgv.Rebind();
                break;

            default:
                login.Abruf.ListSelectedGArtID.Clear();
                login.AppRelativeVirtualPath = "~/";
                login.NextPagePath = wPage.Page_Abruf;
                login.CurrentPagePath = login.NextPagePath;
                Session["Login"] = login;
                Page.Response.Redirect(login.NextPagePath);
                break;
        }
    }
    ///<summary>ArtikelListToSelect / AddArtToAbrufList</summary>
    ///<remarks></remarks>
    private void AddArtToAbrufList()
    {
        bool bItemsSelected = false;
        foreach (GridDataItem dataItem in this.dgv.MasterTableView.GetSelectedItems())
        {
            Int32 ID = 0;
            Int32.TryParse(dataItem["ArtikelID"].Text.ToString(), out ID);
            if (ID > 0)
            {
                bItemsSelected = true;
                clsAbruf tmpAb = new clsAbruf();
                tmpAb.ArtikelID = ID;
                tmpAb.Werksnummer = dataItem["Werksnummer"].Text.ToString();
                tmpAb.Produktionsnummer = dataItem["Produktionsnummer"].Text.ToString();
                tmpAb.Charge = dataItem["Charge"].Text.ToString();
                Int32 iTmp = 0;
                Int32.TryParse(dataItem["LVSNr"].Text.ToString(), out iTmp);
                tmpAb.LVSNr = iTmp;
                decimal decTmp = 0;
                Decimal.TryParse(dataItem["Netto"].Text.ToString(), out decTmp);
                tmpAb.Netto = decTmp;
                decTmp = 0;
                Decimal.TryParse(dataItem["Brutto"].Text.ToString(), out decTmp);
                tmpAb.Brutto = decTmp;
                tmpAb.CompanyID = this.login.User.clCompany.ID;
                tmpAb.CompanyName = this.login.User.clCompany.Fullname;
                tmpAb.AbBereichID = this.login.User.clCompany.AbBereichID;
                tmpAb.Datum = DateTime.Now;
                tmpAb.EintreffDatum = (DateTime)this.dtpEintrefftermin.SelectedDate;
                DateTime dtTmp = Convert.ToDateTime("01.01.1900 " + ((DateTime)this.dtpEintreffZeit.SelectedDate).Hour.ToString("00")
                                              + ":" + ((DateTime)this.dtpEintreffZeit.SelectedDate).Minute.ToString("00")
                                              + ":00");
                tmpAb.EintreffZeit = dtTmp;
                tmpAb.BenutzerID = this.login.User.ID;
                tmpAb.Benutzername = this.login.User.Name;
                tmpAb.Schicht = this.login.User.Schicht;
                tmpAb.Referenz = tbReferenz.Text;
                tmpAb.Abladestelle = tbAbladestelle.Text;
                iTmp = 0;
                Int32.TryParse(dataItem["AuftraggeberID"].Text.ToString(), out iTmp);
                tmpAb.LiefAdrID = iTmp;
                tmpAb.EmpAdrID = login.Company.CompanyGroup.AdrID;
                tmpAb.Status = clsAbruf.const_Status_erstellt;
                switch (AID)
                {
                    case 0:
                        strAbrufAktion = clsAbruf.const_AbrufAktion_Abruf;

                        break;
                    case 1:
                        strAbrufAktion = clsAbruf.const_AbrufAktion_UB;
                        break;
                }
                tmpAb.Aktion = strAbrufAktion;
                tmpAb.IsRead = false;
                tmpAb.Erstellt = DateTime.Now;
                tmpAb.IsCreated = false;
                tmpAb.Add();
            }
            else
            {
                myMasterPage.SetLInfoText(false, "mind. ein Artikel konnte nicht ermittelt werden...");
            }
        }
        //info Ausgabe
        if (!bItemsSelected)
        {
            myMasterPage.SetLInfoText(false, "es wurden keine Aritkel ausgewählt...");
        }
        tbReferenz.Text = string.Empty;
        tbAbladestelle.Text = string.Empty;
        dtpEintrefftermin.MinDate = DateTime.Now.Date;
        DateTime dtTime = Convert.ToDateTime("01.01.1900 " + DateTime.Now.Hour.ToString("00") + ":00:00");
        dtpEintreffZeit.MinDate = dtTime;

        login.Abruf = this.abruf.Copy();
        Session["Login"] = login;
        this.dgv.Rebind();        
    }
    ///<summary>ArtikelListToSelect / ToggleSelectedState</summary>
    ///<remarks></remarks>
    protected void ToggleSelectedState(object sender, EventArgs e)
    {
        SelectedArtikelAnzahl = 0;
        SelectedArtikelBrutto = 0;
        CheckBox headerCheckBox = (sender as CheckBox);
        foreach (GridDataItem dataItem in this.dgv.MasterTableView.Items)
        {
            (dataItem.FindControl("cbSelect") as CheckBox).Checked = headerCheckBox.Checked;
            dataItem.Selected = headerCheckBox.Checked;
            if (headerCheckBox.Checked)
            {
                GridDataItem tmpItm = dataItem;
                SumAndCountSelcetedArt(ref tmpItm);
            }
        }
        this.tbAnzahlArtikel.Value = SelectedArtikelAnzahl;
        this.tbGewichtArtikel.Value = (double)SelectedArtikelBrutto;
    }
    ///<summary>ArtikelListToSelect / ToggleRowSelection</summary>
    ///<remarks></remarks>
    protected void ToggleRowSelection(object sender, EventArgs e)
    {
        SelectedArtikelAnzahl = 0;
        SelectedArtikelBrutto = 0;
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
                SumAndCountSelcetedArt(ref tmpItm);
            }
        }
        GridHeaderItem headerItem = this.dgv.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
        (headerItem.FindControl("headerChkbox") as CheckBox).Checked = checkHeader;

        this.tbAnzahlArtikel.Value = SelectedArtikelAnzahl;
        this.tbGewichtArtikel.Value = (double)SelectedArtikelBrutto;
    }
    ///<summary>ArtikelListToSelect / AddArtToAbrufList</summary>
    ///<remarks></remarks>
    private void SumAndCountSelcetedArt(ref GridDataItem myItem)
    {
        Int32 ID = 0;
        Int32.TryParse(myItem["ArtikelID"].Text.ToString(), out ID);
        if (ID >= 0)
        {
            SelectedArtikelAnzahl++;
            decimal decTmp = 0;
            Decimal.TryParse(myItem["Brutto"].Text.ToString(), out decTmp);
            SelectedArtikelBrutto = SelectedArtikelBrutto + decTmp;
        }
    }
    ///<summary>ArtikelListToSelect / dtpEintrefftermin_SelectedDateChanged</summary>
    ///<remarks></remarks>
    protected void dtpEintrefftermin_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        string str = ((DateTime)dtpEintrefftermin.SelectedDate).ToString();
        if (DateTime.Today.Date > ((DateTime)dtpEintrefftermin.SelectedDate).Date)
        {
            this.abruf.EintreffDatum = DateTime.Today;
            dtpEintrefftermin.SelectedDate = DateTime.Today;
        }
        else
        {
            this.abruf.EintreffDatum = (DateTime)dtpEintrefftermin.SelectedDate;
        }
        this.abruf.Abladestelle = this.tbAbladestelle.Text;
        this.abruf.Referenz = this.tbReferenz.Text;
        login.Abruf = this.abruf.Copy();
        Session["Login"] = login;
    }
    ///<summary>ArtikelListToSelect / dtpEintreffZeit_SelectedDateChanged</summary>
    ///<remarks></remarks>
    protected void dtpEintreffZeit_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        DateTime dtTmp = Convert.ToDateTime("01.01.1900 " + ((DateTime)this.dtpEintreffZeit.SelectedDate).Hour.ToString("00")
                                                      + ":" + ((DateTime)this.dtpEintreffZeit.SelectedDate).Minute.ToString("00")
                                                      + ":00");
        string str1 = dtTmp.ToString();
        this.abruf.EintreffZeit = dtTmp;
        this.abruf.Abladestelle = this.tbAbladestelle.Text;
        this.abruf.Referenz = this.tbReferenz.Text;
        login.Abruf = this.abruf.Copy();
        Session["Login"] = login;
    }

    ///<summary>ArtikelListToSelect / tbAbladestelle_TextChanged</summary>
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
    ///<summary>ArtikelListToSelect / tbReferenz_TextChanged</summary>
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
    ///<summary>ArtikelListToSelect / dgv_ColumnCreated</summary>
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
                        case "ArtikelID":
                        case "AuftraggeberID":
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
                        case "AuftraggeberID":
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
    ///<summary>Abruf / dgv_ItemDataBound</summary>
    ///<remarks></remarks>
    protected void dgv_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            if (dataItem != null)
            {
                clsTelerikGLHelper.GridFormatting(ref this.dgv, ref dataItem, ref login.Views.ListTableView);
            }
        }
    }
    ///<summary>Abruf / comboSearch_SelectedIndexChanged</summary>
    ///<remarks></remarks>
    protected void comboSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strDataField = this.comboSearch.Text;
        login.SearchDataField = this.comboSearch.Text;
        Session["Login"] = login;
    }
    ///<summary>Abruf / txtSearch_TextChanged</summary>
    ///<remarks></remarks>
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        string strTxtSerach = this.txtSearch.Text;
        login.SearchText = this.txtSearch.Text;
        Session["Login"] = login;
    }
}