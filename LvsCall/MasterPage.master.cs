using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class MasterPage : System.Web.UI.MasterPage
{
    private clsWebPages wPage = new clsWebPages();
    private clsLogin Login;
    public System.Drawing.Color dgvBackColor_Abruf = System.Drawing.Color.Green;
    public System.Drawing.Color dgvBackColor_UB = System.Drawing.Color.Yellow;
    ///<summary>MasterPage / Page_Load</summary>
    ///<remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((System.Globalization.CultureInfo)Session["Language"] != null)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)Session["Language"];
        }
        Login = new clsLogin();
        if (Session["Login"] != null)
        {
            Login = (clsLogin)Session["Login"];
        }
        else
        {
            Login.InitClass();
            Session["Login"] = Login;
        }
        PanMenu.Visible = Login.LoggedIn;
        btnEnglish.Visible = false;
        btnGerman.Visible = Login.LoggedIn;
        btnFrance.Visible = false;

        InitMenu(Login.LoggedIn);

        if ((Login != null) && (Login.LoggedIn))
        {
            if (Page.AppRelativeVirtualPath == Login.AppRelativeVirtualPath)
            {
                //Page.Response.Redirect(Login.NextPagePath);
                Login.AppRelativeVirtualPath = string.Empty;
                Login.NextPagePath = string.Empty;
                Session["Login"] = Login;
            }
            else
            {
                //wir sind auf der korrekten Seite nun können die Daten angezeigt werden
            }
            this.lblCaption.Visible = true;
            //this.RadBinaryImage1.Visible = true;
            this.RadBinaryImage1.Visible = false;
            this.lUserInfoHead.Visible = true;

            this.LInfoText.Text = string.Empty;
            this.LInfoText.Visible = true;
            SetLInfoText(this.Login.bInfoText, this.Login.InfoText);
            SetLUserInfoHead(this.Login.User.Vorname + " " + this.Login.User.Name);
        }
        else
        {
            if (Page.AppRelativeVirtualPath != "~/Login.aspx")
            {
                //this.TableUserInfo.Visible = false;
                this.lblCaption.Visible = false;
                this.RadBinaryImage1.Visible = false;
                this.lUserInfoHead.Visible = false;
                this.LInfoText.Text = string.Empty;
                this.LInfoText.Visible = false;
                Page.Response.Redirect(wPage.Page_LogIn);                
            }
        }
    }
    ///<summary>MasterPage / SetLableCaption</summary>
    ///<remarks></remarks>
    public void SetLableCaption(bool bFill, string myCaption)
    {
        string strTxt = string.Empty;
        if (bFill)
        {
            strTxt = "Navigationsinformation >>> [" + myCaption + "]";
        }
        this.lblCaption.Text = strTxt;
    }
    ///<summary>MasterPage / SetLUserInfoHead</summary>
    ///<remarks></remarks>
    public void SetLUserInfoHead(string strUserName)
    {
        this.lUserInfoHead.Text = "User [" + strUserName + "] :";
    }
    ///<summary>MasterPage / SetLInfoText</summary>
    ///<remarks></remarks>
    public void SetLInfoText(bool bError, string strInfoTxt)
    {
        if (!bError)
        {
            this.LInfoText.ForeColor = System.Drawing.Color.Blue;
        }
        else
        {
            this.LInfoText.ForeColor = System.Drawing.Color.Red;
        }
        this.LInfoText.Text = strInfoTxt;
    }
    ///<summary>MasterPage / InitMenu</summary>
    ///<remarks></remarks>
    public void InitMenu(bool bLoggedIn)
    {
        if (bLoggedIn)
        {
            clsMenu LinkMenu = new clsMenu();
            Menu.DataSource = LinkMenu.GetMenuItems(this.Login.User);
            Menu.DataFieldID = "ID";
            Menu.DataFieldParentID = "ParentID";
            Menu.DataTextField = "Designation";
            Menu.DataNavigateUrlField = "Link";
        }
        else
        {
            Menu.DataSource = null;
        }
        Menu.DataBind();
    }
    ///<summary>MasterPage / btnGerman_Click</summary>
    ///<remarks></remarks>
    protected void btnGerman_Click(object sender, ImageClickEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
        Session["Language"] = System.Threading.Thread.CurrentThread.CurrentUICulture;
        Response.Redirect(Request.Url.OriginalString);
    }
    ///<summary>MasterPage / btnEnglish_Click</summary>
    ///<remarks></remarks>
    protected void btnEnglish_Click(object sender, ImageClickEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
        Session["Language"] = System.Threading.Thread.CurrentThread.CurrentUICulture;
        Response.Redirect(Request.Url.OriginalString);
    }
    ///<summary>MasterPage / btnFrance_Click</summary>
    ///<remarks></remarks>
    protected void btnFrance_Click(object sender, ImageClickEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr");
        Session["Language"] = System.Threading.Thread.CurrentThread.CurrentUICulture;
        Response.Redirect(Request.Url.OriginalString);
    }

}
