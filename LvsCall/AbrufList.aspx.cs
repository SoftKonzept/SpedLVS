using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using LVS;


public partial class AbrufList : System.Web.UI.Page
{
    //------ Menü ButtonComman -
    private clsWebPages wPage = new clsWebPages();

    public const string const_MenuButtonComman_BackCallBestand = "BackCallBestand";
    public const string const_MenuButtonComman_BackReservationCall = "BackReservationCall";
    public const string const_MenuButtonComman_DoCall = "DoCall";
    public const string const_MenuButtonComman_NextStepCall = "NextStepCall";
    public const string const_MenuButtonComman_NextStepCallFinish = "NextStepCallFinish";

    public const string const_MenuButtonComman_BackRebookingBestand = "BackRebookingBestand";
    public const string const_MenuButtonComman_BackReservationRebooking = "BackReservationRebooking";
    public const string const_MenuButtonComman_DoRebooking = "DoRebooking";
    public const string const_MenuButtonComman_NextStepRebooking = "NextStepRebooking";
    public const string const_MenuButtonComman_NextStepRebookingFinish = "NextStepRebookingFinish";

    public const string const_MenuButtonComman_ClearList = "ClearList";
    public const string const_MenuButtonComman_StartSearch = "StartSearch";
    public const string const_MenuButtonComman_ClearSearch = "ClearSearch";


    public const string const_ViewTableName_AbrufList_ReservationCall = "AbrufListReservationCall"; //vorgemerkte Abrufe
    public const string const_ViewTableName_AbrufList_CreatedCall = "AbrufListCreatedCall"; //erstellte Abrufe

    public const string const_ViewTableName_AbrufList_ReservationRebooking = "AbrufListReservationRebooking"; // vorgemerkte UB
    public const string const_ViewTableName_AbrufList_CreatedRebooking = "AbrufListCreatedRebooking";         // ersttellte UB



    MasterPage myMasterPage;
    clsLogin login;
    clsAbruf abruf;
    
    string strAbrufAktion = string.Empty;
    private string Default_FileName_ToExport = DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + "_" + "Abrufliste";

    private string Default_SiteUserInfotext = "Abruf -> Abrufliste";
    private string Abruf_SiteUserInfotext_vorgemerkt = "Abruf -> vorgemerkte Abrufliste";
    private string Umbuchung_SiteUserInfotext_vorgemerkt = "Umbuchung -> vorgemerkte Umbuchungsliste";
    private string Abruf_SiteUserInfotext_offen = "Abrufe -> offene Abrufe";
    private string Umbuchung_SiteUserInfotext_offen = "Umbuchung -> offene Umbuchungen";
    
    Int32 AID = 0;
    List<clsAbruf> listAbrufeList;
    private Int32 SelectedArtikelAnzahl = 0;
    private decimal SelectedArtikelBrutto = 0;
    private Int32 GesamtAnzahl = 0;
    private Int32 GesamtMenge = 0;
    private decimal GesamtBrutto = 0;

    /****************************************************************
     *                  Methoden / Procedure
     * **************************************************************/
    ///<summary>AbrufList / Page_Load</summary>
    ///<remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        // login = new clsLogin();
        if (Session["Login"] != null)
        {
            login = new clsLogin();
            login = (clsLogin)Session["Login"];
            login.bInfoText = true;
            login.InfoText = string.Empty;
            if (login.Abruf == null)
            {
                login.Abruf = new clsAbruf();                
            }
            abruf = login.Abruf.Copy();
            if (Request.QueryString["AID"] != null)
            {
                AID = Convert.ToInt32(Request.QueryString["AID"]);
                this.login.AID = AID;
            }
            else
            {
                AID = this.login.AID;
            }
            ManipulateMenu();
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
            }
            login.LastPagePath = Page.AppRelativeVirtualPath + "?AID=" + this.login.AID.ToString();
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
    ///<summary>AbrufList / ManipulateMenu</summary>
    ///<remarks>Value der Buttons:
    ///         0 = immer anzeigen
    ///         1 = AID -> vorgemerkte Liste Abruf
    ///         3 = AID -> vorgemerkte Liste UB
    ///         </remarks>
    private void ManipulateMenu()
    {
        if (AID > 0)
        {
            for (Int32 i = 0; i <= this.RadToolBar1.Items.Count - 1; i++)
            {
                Int32 iVal = 0;
                Int32.TryParse(this.RadToolBar1.Items[i].Value.ToString(), out iVal);
                string strbutton = this.RadToolBar1.Items[i].Text;
                if (iVal > 0)
                {
                    if (iVal == AID)
                    {
                        this.RadToolBar1.Items[i].Visible = true;
                    }
                    else
                    {
                        this.RadToolBar1.Items[i].Visible = false;
                    }
                }               
            }
        }
    }
    ///<summary>AbrufList / dgv_NeedDataSource</summary>
    ///<remarks></remarks>
    protected void dgv_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        this.dgv.ClientSettings.Resizing.AllowColumnResize = true;
        this.dgv.ExportSettings.FileName = Default_FileName_ToExport;
        this.dgv.ExportSettings.ExportOnlyData = true;
        this.dgv.ExportSettings.Pdf.PageTitle = this.Default_FileName_ToExport;
        DataTable dtList = new DataTable();
        switch (AID)
        {
            case 0:
                break;

            //ReservationCall
            case 1:
                //BackColor zur Unterscheidung Abruf/UB
                this.dgv.CommandItemStyle.BackColor = myMasterPage.dgvBackColor_Abruf;
                //Ermitteln der Daten
                this.abruf.GetdVorgemerkteAbrufUBList(this.login.User.clCompany, clsAbruf.const_AbrufAktion_Abruf);
                //Userinfo auf Masterpage
                Default_SiteUserInfotext = Abruf_SiteUserInfotext_vorgemerkt;
                myMasterPage.SetLableCaption(true, this.Default_SiteUserInfotext);
                //Views laden
                login.Views.FillListTableView(const_ViewTableName_AbrufList_ReservationCall);
                login.Views.FillListSort(const_ViewTableName_AbrufList_ReservationCall);  
                break;

            //CreatedCall
            case 2:
                //BackColor zur Unterscheidung Abruf/UB
                this.dgv.CommandItemStyle.BackColor = myMasterPage.dgvBackColor_Abruf;
                //Ermitteln der Daten
                this.abruf.GetCreatedAbrufUBList(this.login.User.clCompany, clsAbruf.const_AbrufAktion_Abruf);
                //Userinfo auf Masterpage                
                Default_SiteUserInfotext = Abruf_SiteUserInfotext_offen;
                myMasterPage.SetLableCaption(true, this.Default_SiteUserInfotext);
                //Views laden
                login.Views.FillListTableView(const_ViewTableName_AbrufList_CreatedCall);
                login.Views.FillListSort(const_ViewTableName_AbrufList_CreatedCall); 
                break;

            //Reservation Rebooking
            case 3:
                //BackColor zur Unterscheidung Abruf/UB
                this.dgv.CommandItemStyle.BackColor = myMasterPage.dgvBackColor_UB;
                //Ermitteln der Daten
                this.abruf.GetdVorgemerkteAbrufUBList(this.login.User.clCompany, clsAbruf.const_AbrufAktion_UB);
                //Userinfo auf Masterpage 
                Default_SiteUserInfotext = Umbuchung_SiteUserInfotext_vorgemerkt;
                myMasterPage.SetLableCaption(true, this.Default_SiteUserInfotext);
                //Views laden
                login.Views.FillListTableView(const_ViewTableName_AbrufList_ReservationRebooking);
                login.Views.FillListSort(const_ViewTableName_AbrufList_ReservationRebooking); 
                break;

            //Created Rebooking
            case 4:
                //BackColor zur Unterscheidung Abruf/UB
                this.dgv.CommandItemStyle.BackColor = myMasterPage.dgvBackColor_UB;
                //Ermitteln der Daten
                this.abruf.GetCreatedAbrufUBList(this.login.User.clCompany, clsAbruf.const_AbrufAktion_UB);
                //Userinfo auf Masterpage                 
                Default_SiteUserInfotext = Umbuchung_SiteUserInfotext_offen;
                myMasterPage.SetLableCaption(true, this.Default_SiteUserInfotext);
                //Views laden
                login.Views.FillListTableView(const_ViewTableName_AbrufList_CreatedRebooking);
                login.Views.FillListSort(const_ViewTableName_AbrufList_CreatedRebooking); 
                break;
        }
        
        //Filter wird gesetzt
        if (this.login.SearchRowFilterString != null)
        {
            if (!this.login.SearchRowFilterString.Equals(string.Empty))
            {
                this.abruf.dtAbrufUBList.DefaultView.RowFilter = this.login.SearchRowFilterString;
            }
        }
        
        //Gesamtgewicht und Gesamtanzahl ermitteln
        if (this.abruf.dtAbrufUBList.Columns.Contains("ArtikelID"))
        {
            GesamtAnzahl = 0;
            object countAnzahl = (this.abruf.dtAbrufUBList.DefaultView.ToTable()).Compute("COUNT(ArtikelID)", "");
            Int32.TryParse(countAnzahl.ToString(), out GesamtAnzahl);
            tbGeamtAnzahl.Value = GesamtAnzahl;
        }
        if (this.abruf.dtAbrufUBList.Columns.Contains("Anzahl"))
        {
            GesamtMenge = 0;
            object sumMenge = (this.abruf.dtAbrufUBList.DefaultView.ToTable()).Compute("Sum(Anzahl)", "");
            //Decimal.TryParse(sumMenge.ToString(), out GesamtMenge);
            Int32.TryParse(sumMenge.ToString(), out GesamtMenge);
            tbGesamtMenge.Value = (double)GesamtMenge;
        }
        if (this.abruf.dtAbrufUBList.Columns.Contains("Brutto"))
        {
            GesamtBrutto = 0;
            object sumBrutto = (this.abruf.dtAbrufUBList.DefaultView.ToTable()).Compute("Sum(Brutto)", "");
            Decimal.TryParse(sumBrutto.ToString(), out GesamtBrutto);
            tbGesamtGewicht.Value = (double)GesamtBrutto;
        }
        //this.dgv.DataSource = listAbrufeList;
        ReorderDataTableSource();
        clsTelerikGLHelper.SortDataTableSource(ref this.abruf.dtAbrufUBList, ref this.login.Views);
        this.dgv.DataSource = this.abruf.dtAbrufUBList.DefaultView;


        Int32 iColIDIndex = 0;
        for (Int32 i = 0; i <= this.dgv.Columns.Count - 1; i++)
        {
            string strText = this.dgv.Columns[i].UniqueName;
            if (this.dgv.Columns[i].UniqueName.Equals("ID"))
            {
                iColIDIndex = i;
                i = this.dgv.Columns.Count;
            }
        }   
        this.login.Abruf = this.abruf;
        Session["Login"] = login;

    }
    ///<summary>ReorderDataTableSource / RadToolBar1_ButtonClick</summary>
    ///<remarks></remarks>
    protected void ReorderDataTableSource()
    {
        if (this.abruf.dtAbrufUBList.Columns.Count > 0)
        {
            if (this.abruf.dtAbrufUBList.Columns.Contains("ArtikelID"))
            {
                this.abruf.dtAbrufUBList.Columns["ArtikelID"].SetOrdinal(this.abruf.dtAbrufUBList.Columns.Count - 1);
            }

            if (this.login.Views.ListTableView.Count > 0)
            {
                for (Int32 i = 0; i <= this.login.Views.ListTableView.Count - 1; i++)
                {
                    clsViews tmpView = (clsViews)this.login.Views.ListTableView[i];
                    if (tmpView != null)
                    {
                        if (this.abruf.dtAbrufUBList.Columns.Contains(tmpView.ColumnName))
                        {
                            this.abruf.dtAbrufUBList.Columns[tmpView.ColumnName].SetOrdinal(tmpView.OrderIndex);
                        }
                    }
                }
            }
        }
        else
        {
            myMasterPage.SetLInfoText(false, "Die Liste enthält keine Daten....");
        }
    }
    ///<summary>AbrufList / RadToolBar1_ButtonClick</summary>
    ///<remarks></remarks>
    protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
    {
        RadToolBarButton btn = e.Item as RadToolBarButton;
        string strCommand = btn.CommandName.ToString();

        switch (strCommand)
        {
            //------------------ Abrufe / CALL -----------------------------------------
            case const_MenuButtonComman_BackCallBestand:
                login.AppRelativeVirtualPath = "~/";
                login.NextPagePath = wPage.Page_AbrufBestand;
                login.CurrentPagePath = login.NextPagePath;
                login.Abruf = this.abruf;
                Session["Login"] = login;
                Page.Response.Redirect(login.NextPagePath);
                break;

            case const_MenuButtonComman_BackReservationCall:
                //this.abruf.DictAbrufeToInsert.Clear();
                //login.Abruf.ListSelectedGArtID.Clear();
                login.AppRelativeVirtualPath = "~/";
                login.NextPagePath = wPage.Page_AbrufList_vorgemerkteAbrufe;
                login.CurrentPagePath = login.NextPagePath;
                login.Abruf = this.abruf;
                Session["Login"] = login;
                Page.Response.Redirect(login.NextPagePath);
                break;

            case const_MenuButtonComman_DoCall:
                if (InsertProzessList())
                {
                    this.abruf.DictAbrufeToInsert.Clear();
                    login.Abruf.ListSelectedGArtID.Clear();
                    login.AppRelativeVirtualPath = "~/";
                    login.NextPagePath = wPage.Page_AbrufList_offeneAbrufe;
                    login.CurrentPagePath = login.NextPagePath;
                    this.login.Abruf = this.abruf;
                    Session["Login"] = login;
                    Page.Response.Redirect(login.NextPagePath);
                }
                //else
                //{
                //    this.dgv.Rebind();
                //}
                break;

            case const_MenuButtonComman_NextStepCall:
                this.abruf.DictAbrufeToInsert.Clear();
                login.Abruf.ListSelectedGArtID.Clear();
                login.AppRelativeVirtualPath = "~/";
                login.NextPagePath = wPage.Page_AbrufList_offeneAbrufe;
                login.CurrentPagePath = login.NextPagePath;
                login.Abruf = this.abruf;
                Session["Login"] = login;
                Page.Response.Redirect(login.NextPagePath);
                break;

            //----------------- Umbuchungen / Rebooking -----------------------------
            case const_MenuButtonComman_BackRebookingBestand:
                //this.abruf.DictAbrufeToInsert.Clear();
                //login.Abruf.ListSelectedGArtID.Clear();
                login.AppRelativeVirtualPath = "~/";
                login.NextPagePath = wPage.Page_UmbuchungBestand;
                login.CurrentPagePath = login.NextPagePath;
                login.Abruf = this.abruf;
                Session["Login"] = login;
                Page.Response.Redirect(login.NextPagePath);
                break;

            case const_MenuButtonComman_BackReservationRebooking:
                //this.abruf.DictAbrufeToInsert.Clear();
                //login.Abruf.ListSelectedGArtID.Clear();
                login.AppRelativeVirtualPath = "~/";
                login.NextPagePath = wPage.Page_Abruf_ArtikelToSelect;
                login.CurrentPagePath = login.NextPagePath;
                login.Abruf = this.abruf;
                Session["Login"] = login;
                Page.Response.Redirect(login.NextPagePath);                
                break;

            case const_MenuButtonComman_DoRebooking:
                if (InsertProzessList())
                {
                    this.abruf.DictAbrufeToInsert.Clear();
                    login.Abruf.ListSelectedGArtID.Clear();
                    login.AppRelativeVirtualPath = "~/";
                    login.NextPagePath = wPage.Page_AbrufList_offeneUmbuchungen;
                    login.CurrentPagePath = login.NextPagePath;
                    this.login.Abruf = this.abruf;
                    Session["Login"] = login;
                    Page.Response.Redirect(login.NextPagePath);
                }
                //else
                //{
                //    this.dgv.Rebind();
                //}
                break;

            case const_MenuButtonComman_NextStepRebooking:
                this.abruf.DictAbrufeToInsert.Clear();
                login.Abruf.ListSelectedGArtID.Clear();
                login.AppRelativeVirtualPath = "~/";
                login.NextPagePath = wPage.Page_AbrufList_offeneUmbuchungen;
                login.CurrentPagePath = login.NextPagePath;
                login.Abruf = this.abruf;
                Session["Login"] = login;
                Page.Response.Redirect(login.NextPagePath);
                break;

            //--------------------  Diverse und Zusammenfassungen -----------------
            case const_MenuButtonComman_NextStepRebookingFinish:
            case const_MenuButtonComman_NextStepCallFinish:
                this.abruf.DictAbrufeToInsert.Clear();
                login.Abruf.ListSelectedGArtID.Clear();
                login.AppRelativeVirtualPath = "~/";
                login.NextPagePath = wPage.Page_Main;
                login.CurrentPagePath = login.NextPagePath;
                login.Abruf = this.abruf;
                Session["Login"] = login;
                Page.Response.Redirect(login.NextPagePath);
                break;


            case const_MenuButtonComman_StartSearch:
                string SearchString = string.Empty;
                if (this.abruf.dtAbrufUBList.Rows.Count > 0)
                {
                    SearchString =  SearchRowFilter.GetSearchstring(login.SearchDataField, login.SearchText, SearchString);
                }
                login.SearchRowFilterString = SearchString;
                //Session["Login"] = login;
                //this.dgv.Rebind();
                break;

            case const_MenuButtonComman_ClearSearch:
                this.comboSearch.SelectedIndex = 0;
                this.txtSearch.Text = string.Empty;
                login.SearchDataField = string.Empty;
                login.SearchText = string.Empty;
                login.SearchRowFilterString = string.Empty;
                //Session["Login"] = login;
                //this.dgv.Rebind();
                break;

            case const_MenuButtonComman_ClearList:
                DeleteSelectedItem();
                break;
        }
        Session["Login"] = login;
        this.dgv.Rebind();
    }
    ///<summary>AbrufList / InsertAbrufList</summary>
    ///<remarks></remarks>
    private bool InsertProzessList()
    {
        bool bItemsSelected = false;
        switch (AID)
        {
            case 0:
                break;
            case 1:    //Default_SiteUserInfotext = "Abrufe -> vorgemerkte Abrufliste";
            case 3:    //Default_SiteUserInfotext = "Umbuchungen -> vorgemerkte Umbuchungen";   
                foreach (GridDataItem dataItem in this.dgv.MasterTableView.GetSelectedItems())
                {
                    Int32 AbRufID = 0;
                    Int32.TryParse(dataItem["AbrufID"].Text.ToString(), out AbRufID);
                    if (AbRufID > 0)
                    {
                        bItemsSelected = true;
                        clsAbruf tmpAb = new clsAbruf();
                        tmpAb.ID = AbRufID;
                        tmpAb.Status = clsAbruf.const_Status_erstellt;
                        tmpAb.IsCreated = true;
                        tmpAb.Update_SetCreated();
                    }
                }
                //info Ausgabe
                if (!bItemsSelected)
                {
                    myMasterPage.SetLInfoText(false, "es wurden keine Aritkel ausgewählt...");
                }
                break;
            case 2:   // Default_SiteUserInfotext = "Abrufe -> offene Abrufe";
            case 4:   //Default_SiteUserInfotext = "Umbuchungen -> offene Umbuchungsliste";
                break;
        }
        return bItemsSelected;
    }
    ///<summary>ArtikelListToSelect / AddArtToAbrufList</summary>
    ///<remarks></remarks>
    private void DeleteSelectedItem()
    {
        if (this.login.User.clRole.IsUser)
        {
            string strError = string.Empty;
            foreach (GridDataItem dataItem in this.dgv.MasterTableView.GetSelectedItems())
            {
                Int32 ID = 0;
                Int32.TryParse(dataItem["AbrufID"].Text.ToString(), out ID);
                if (ID > 0)
                {
                    clsAbruf tmpAb = new clsAbruf();
                    tmpAb.ID = ID;
                    string strLVSNR = dataItem["LVsNr"].Text.ToString();
                    if (!tmpAb.Delete())
                    {
                        strError = strError + "LVSNr. [" + strLVSNR + "] konnte nicht entfernt werden";
                    }

                }
                if (!strError.Equals(string.Empty))
                {
                    myMasterPage.SetLInfoText(true, strError);
                }
            }
        }
        else
        {
            myMasterPage.SetLInfoText(false, "Ihnen fehlt die entsprechende Berechtigung!!!");
        }
        login.Abruf = this.abruf.Copy();
        Session["Login"] = login;
        this.dgv.Rebind();
    }
    ///<summary>AbrufList / dgv_ColumnCreating</summary>
    ///<remarks></remarks>
    protected void dgv_ColumnCreating(object sender, GridColumnCreatingEventArgs e)
    {
        switch (e.Column.UniqueName)
        {
            case "CheckBoxTemplateColumn":
            case "ExpandColumn":
                break;

            case "AutoGeneratedDeleteColumn":
            case "AutoGeneratedEditColumn":
                e.Column.Display = true;
                e.Column.Visible = true;
                e.Column.ItemStyle.Width = 100;
                break;
        }
    }
    ///<summary>UserList / dgv_ColumnCreated</summary>
    ///<remarks>Ein-/ Ausblenden der einzelnen Spalten</remarks>
    protected void dgv_ColumnCreated(object sender, Telerik.Web.UI.GridColumnCreatedEventArgs e)
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
                    //iStartOrderIndex = cSet.OrderIndex;
                    //e.Column.OrderIndex = tmpView.OrderIndex + iStartOrderIndex;
                    e.Column.Display = tmpView.IsDisplayed;
                    e.Column.Visible = tmpView.IsVisible;
                    e.Column.HeaderText = tmpView.ColumnViewName;
                    if (tmpView.ColumnName.Equals("Werksnummer"))
                    {
                        e.Column.HeaderText = tmpView.ColumnViewName;
                    }

                    switch (e.Column.UniqueName)
                    {
                            //diese müssen immer Visible = true sein, da diese Werte im System abgegriffen werden
                        case "ArtikelID":
                        case "AbrufID":    
                        case "AuftraggeberID":
                            if (!tmpView.IsVisible)
                            {
                                e.Column.Visible = true;
                            }
                            break;
                    }
                }
                else
                {
                    switch (e.Column.UniqueName)
                    {
                        case "ArtikelID":
                        case "AbrufID":
                        case "AuftraggeberID":
                            e.Column.Display = false;
                            //ArtikelID muss immer Visible = true sein, damit der Wert ermittelt werden kann
                            e.Column.Visible = true;
                            break;

                        case "CheckBoxTemplateColumn":
                        case "ExpandColumn":
                            break;

                        case "AutoGeneratedDeleteColumn":
                            e.Column.Display = true;
                            e.Column.Visible = true;
                            GridButtonColumn btnDel = e.Column as GridButtonColumn;
                            if (btnDel is GridButtonColumn)
                            {
                                btnDel.ButtonType = GridButtonColumnType.ImageButton;
                                btnDel.ImageUrl = "Images/Delete.gif";
                                btnDel.HeaderTooltip = "Datensatz löschen";
                            }
                            break;

                        case "AutoGeneratedEditColumn":
                            e.Column.Display = true;
                            e.Column.Visible = true;
                            GridEditCommandColumn btnEdit = e.Column as GridEditCommandColumn;
                            if (btnEdit is GridEditCommandColumn)
                            {
                                btnEdit.ButtonType = GridButtonColumnType.ImageButton;
                                btnEdit.EditImageUrl = "Images/Edit.gif";
                                btnEdit.HeaderTooltip = "Datensatz bearbeiten";
                            }
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
    ///<summary>AbrufList / ToggleSelectedState</summary>
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
    ///<summary>AbrufList / ToggleRowSelection</summary>
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
    ///<summary>AbrufList / AddArtToAbrufList</summary>
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
    ///<summary>AbrufList / dgv_GridExporting</summary>
    ///<remarks></remarks>
    protected void dgv_GridExporting(object sender, GridExportingArgs e)
    {
        switch (e.ExportType)
        {
            case ExportType.Excel:
                //do something with the e.ExportOutput value  
                this.dgv.MasterTableView.ExportToExcel();
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
                //you can't change the output here - use the PdfExporting event instead   
                foreach (GridDataItem item in dgv.Items)
                    item.Style["background-color"] = "#888888";
                this.dgv.MasterTableView.ExportToPdf();
                break;
        }
    }
    ///<summary>AbrufList / dgv_ItemCommand</summary>
    ///<remarks></remarks>
    protected void dgv_ItemCommand(object sender, GridCommandEventArgs e)
    {
        Int32 iIndex = 0;
        switch (e.CommandName)
        {
            case "Edit":
                if (Int32.TryParse(e.Item.ItemIndex.ToString(), out iIndex))
                {
                    GridDataItem itm = (GridDataItem)this.dgv.Items[iIndex];
                    string strl = itm["AbrufID"].Text;
                    string strAbrufTableID = itm["AbrufID"].Text;
                    Int32 iID = 0;
                    if (Int32.TryParse(strAbrufTableID, out iID))
                    {
                        if (iID > 0)
                        {
                            this.login.Abruf = new clsAbruf();
                            this.login.Abruf.ID = iID;
                            this.login.Abruf.Fill();

                            //this.login.LastPagePath = this.login.AppRelativeVirtualPath;
                            this.login.AppRelativeVirtualPath = "~/";
                            this.login.NextPagePath = clsMenu.const_Path_Abruf_Edit;
                            login.CurrentPagePath = login.NextPagePath;
                            Session["Login"] = this.login;
                            Page.Response.Redirect(this.login.NextPagePath);
                        }
                        else
                        {
                            myMasterPage.SetLInfoText(false, "Es konnte kein Datensatz ermittelt werden.");
                        }
                    }
                }
                break;

            case "Delete":
                //Check Userrolle für Berechting
                //Löschen nur als Manager
                if (this.login.User.clRole.IsUser)
                {
                    if (Int32.TryParse(e.Item.ItemIndex.ToString(), out iIndex))
                    {
                        GridDataItem itm = (GridDataItem)this.dgv.Items[iIndex];
                        string strAbrufTableID = itm["AbrufID"].Text;
                        Int32 IDToDelete = 0;
                        if (Int32.TryParse(strAbrufTableID, out IDToDelete))
                        {
                            if (IDToDelete > 0)
                            {
                                //offene UB / Abrufe aus DB
                                clsAbruf DelAb = new clsAbruf();
                                DelAb.ID = IDToDelete;
                                DelAb.Fill();
                                if (!DelAb.Delete())
                                {
                                    string strError = "LVSNr. [" + DelAb.LVSNr.ToString() + "] konnte nicht entfernt werden...";
                                    myMasterPage.SetLInfoText(false, strError);
                                }
                                else
                                {
                                    string strTxt = "LVSNr. [" + DelAb.LVSNr.ToString() + "] wurde erfolgreich entfernt...";
                                    myMasterPage.SetLInfoText(true, strTxt);
                                }
                                login.Abruf = this.abruf.Copy();
                                Session["Login"] = login;
                                this.dgv.Rebind();
                            }
                        }
                    }
                }
                else
                {
                    myMasterPage.SetLInfoText(false, "Ihnen fehlt die entsprechende Berechtigung!!!");
                }
                break;

        }
    }
    ///<summary>AbrufList / comboSearch_SelectedIndexChanged</summary>
    ///<remarks></remarks>
    protected void comboSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strDataField = this.comboSearch.Text;
        login.SearchDataField = this.comboSearch.Text;
        Session["Login"] = login;
    }
    ///<summary>AbrufList / txtSearch_TextChanged</summary>
    ///<remarks></remarks>
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        string strTxtSerach = this.txtSearch.Text;
        login.SearchText = this.txtSearch.Text;
        Session["Login"] = login;
    }
    ///<summary>AbrufList / dgv_ItemDataBound</summary>
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



}