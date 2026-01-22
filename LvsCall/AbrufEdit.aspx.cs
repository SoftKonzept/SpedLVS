using System;
using System.Web.UI;
using System.Web.UI.WebControls;
//sing System.Web.ModelBinding;


public partial class AbrufEdit : System.Web.UI.Page
{
    private clsWebPages wPage = new clsWebPages();
    MasterPage myMasterPage;
    clsLogin Login;
    private clsUser Benutzer;
    internal const Int32 const_Role_AdministratorID = 1;
    internal const Int32 const_Role_GastID = 4;
    private clsAbruf editAbruf;

    private Int32 iCount = 0;
    private string Default_SiteUserInfotext = "Abrufe -> Abruf bearbeiten";

    ///<summary>AbrufEdit / Page_Load</summary>
    ///<remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            Login = new clsLogin();
            if (Session["Login"] != null)
            {
                Login = (clsLogin)Session["Login"];
                Login.bInfoText = true;
                Login.InfoText = string.Empty;
                this.Benutzer = new clsUser();
                this.Benutzer = Login.User.Copy();
                if (Login.Abruf != null)
                {
                    //EditMode
                    this.editAbruf = Login.Abruf;
                    ClearInputFields();
                    SetValueToFrm();
                }
                else
                {
                    editAbruf = new clsAbruf();
                }
            }
            else
            {
                Login.InitClass();
                Session["Login"] = Login;
            }

            dtpEintrefftermin.MinDate = DateTime.Now.Date;
            DateTime dtTime = Convert.ToDateTime("01.01.1900 " + DateTime.Now.AddHours(1).Hour.ToString("00") + ":00:00");
            dtpEintreffZeit.MinDate = dtTime;
        }
        else
        {
            Login = new clsLogin();
            if (Session["Login"] != null)
            {
                Login = (clsLogin)Session["Login"];
                Login.bInfoText = true;
                Login.InfoText = string.Empty;
                this.Benutzer = new clsUser();
                this.Benutzer = Login.User.Copy();
                if (Login.Abruf != null)
                {
                    //EditMode
                    this.editAbruf = Login.Abruf;
                }
                else
                {
                    editAbruf = new clsAbruf();
                    ClearInputFields();
                }
            }
        }
        if ((Login != null) && (Login.LoggedIn))
        {
            myMasterPage = (MasterPage)this.Master;
            Table masterClientInfoTable = (Table)this.Master.FindControl("TableUserInfo");
            if (masterClientInfoTable is Table)
            {
                masterClientInfoTable.Visible = true;
                myMasterPage.SetLableCaption(true, this.Default_SiteUserInfotext);
                myMasterPage.SetLUserInfoHead(this.Login.User.Vorname + " " + this.Login.User.Name);
            }
            if (Page.AppRelativeVirtualPath == Login.AppRelativeVirtualPath)
            {
                //Page.Response.Redirect(Login.NextPagePath);
                Login.AppRelativeVirtualPath = string.Empty;
                Login.NextPagePath = string.Empty;
                Session["Login"] = Login;
            }
        }
        else
        {
            Login = new clsLogin();
            Login.AppRelativeVirtualPath = "~/";
            Login.NextPagePath = wPage.Page_LogOut;
            Session["Login"] = null;
            Page.Response.Redirect(Login.NextPagePath);

        }
    }
    ///<summary>AbrufEdit / dtpEintrefftermin_SelectedDateChanged</summary>
    ///<remarks></remarks>
    protected void dtpEintrefftermin_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        string str = ((DateTime)dtpEintrefftermin.SelectedDate).ToString();
        if (DateTime.Today.Date > ((DateTime)dtpEintrefftermin.SelectedDate).Date)
        {
            this.editAbruf.EintreffDatum = DateTime.Today;
            dtpEintrefftermin.SelectedDate = DateTime.Today;
        }
        else
        {
            this.editAbruf.EintreffDatum = (DateTime)dtpEintrefftermin.SelectedDate;
        }
        this.editAbruf.Abladestelle = this.tbAbladestelle.Text;
        this.editAbruf.Referenz = this.tbReferenz.Text;
        Login.Abruf = this.editAbruf.Copy();
        Session["Login"] = Login;
    }
    ///<summary>AbrufEdit / dtpEintreffZeit_SelectedDateChanged</summary>
    ///<remarks></remarks>
    protected void dtpEintreffZeit_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        DateTime dtTmp = Convert.ToDateTime("01.01.1900 " + ((DateTime)this.dtpEintreffZeit.SelectedDate).Hour.ToString("00")
                                                      + ":" + ((DateTime)this.dtpEintreffZeit.SelectedDate).Minute.ToString("00")
                                                      + ":00");
        string str1 = dtTmp.ToString();
        this.editAbruf.EintreffZeit = dtTmp;
        this.editAbruf.Abladestelle = this.tbAbladestelle.Text;
        this.editAbruf.Referenz = this.tbReferenz.Text;
        Login.Abruf = this.editAbruf.Copy();
        Session["Login"] = Login;
    }
    ///<summary>AbrufEdit / ClearInputFields</summary>
    ///<remarks></remarks>
    private void ClearInputFields()
    {
        tbLVSNrArtikel.Text = string.Empty;
        tbAbladestelle.Text = string.Empty;
        tbReferenz.Text = string.Empty;
    }
    ///<summary>AbrufEdit / btnCancel_Click</summary>
    ///<remarks></remarks>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        LinkToAbrufListOpen();
    }
    ///<summary>AbrufEdit / btnSave_Click</summary>
    ///<remarks></remarks>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strName = this.tbLVSNrArtikel.Text;
        //Check EIngabefelder
        if (CheckInputValue())
        {
            bool bProzessOK = false;
            this.editAbruf.EintreffDatum = (DateTime)this.dtpEintrefftermin.SelectedDate;

            DateTime dtTmp = Convert.ToDateTime("01.01.1900 " + ((DateTime)this.dtpEintreffZeit.SelectedDate).Hour.ToString("00")
                                              + ":" + ((DateTime)this.dtpEintreffZeit.SelectedDate).Minute.ToString("00")
                                              + ":00");
            this.editAbruf.EintreffZeit = dtTmp;
            this.editAbruf.Abladestelle = this.tbAbladestelle.Text;
            this.editAbruf.Referenz = this.tbReferenz.Text;
            if (this.editAbruf.ID > 0)
            {
                bProzessOK = this.editAbruf.Update();
            }
            //Check Prozess
            if (bProzessOK)
            {
                //Ausgabe Textinfo
                if (this.myMasterPage is MasterPage)
                {
                    this.myMasterPage.SetLInfoText(false, "Daten erfolgreich geändert...");
                }
                //zurück zur Userlist
                //LinkToAbrufListOpen();
            }
            else
            {
                this.myMasterPage.SetLInfoText(true, "Daten konnten nicht geändert werden...");
            }
            LinkToAbrufListOpen();
        }
    }
    ///<summary>AbrufEdit / SetComboValue</summary>
    ///<remarks></remarks>
    private void SetValueToFrm()
    {
        try
        {
            tbLVSNrArtikel.Text = this.editAbruf.LVSNr.ToString() + "/" + this.editAbruf.ArtikelID.ToString();
            tbAbladestelle.Text = this.editAbruf.Abladestelle;
            tbReferenz.Text = this.editAbruf.Referenz;
            dtpEintrefftermin.SelectedDate = this.editAbruf.EintreffDatum;
            dtpEintreffZeit.SelectedDate = this.editAbruf.EintreffZeit;
        }
        catch (Exception ex)
        {
            string strError = ex.ToString();
        }
    }
    ///<summary>AbrufEdit / CheckInputValue</summary>
    ///<remarks></remarks>
    private bool CheckInputValue()
    {
        bool bReturn = true;
        return bReturn;
    }
    ///<summary>AbrufEdit / btnRefresh_Click</summary>
    ///<remarks></remarks>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        LinkToAbrufListOpen();        
    }
    ///<summary>AbrufEdit / LinkToAbrufListOpen</summary>
    ///<remarks></remarks>
    private void LinkToAbrufListOpen()
    {
        Login.Abruf = null;
        Login.AppRelativeVirtualPath = "~/";
        //Login.NextPagePath = wPage.Page_AbrufList_vorgemerkteAbrufe;
        Login.NextPagePath = Login.LastPagePath;
        Login.CurrentPagePath = Login.NextPagePath;
        //login.NextPagePath = wPage.Page_AbrufList_offeneUmbuchungen;
        //login.CurrentPagePath = login.NextPagePath;

        Session["Login"] = Login;
        Page.Response.Redirect(Login.NextPagePath);
    }
}