using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main : System.Web.UI.Page
{
    MasterPage myMasterPage;
    clsLogin login;
    private clsWebPages wPage = new clsWebPages();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Login"] != null)
        {
            login = new clsLogin();
            login = (clsLogin)Session["Login"];
            login.bInfoText = true;
            login.InfoText = string.Empty;
        }
        if ((login != null) && (login.LoggedIn))
        {
            myMasterPage = (MasterPage)this.Master;
            Table masterClientInfoTable = (Table)this.Master.FindControl("TableUserInfo");
            if (masterClientInfoTable is Table)
            {
                masterClientInfoTable.Visible = false;
                myMasterPage.SetLInfoText(true, string.Empty);
            }
        }
        else
        {
            login = new clsLogin();
            login.LoggedIn = false;
            login.AppRelativeVirtualPath = "~/";
            login.NextPagePath = wPage.Page_LogOut;
            Session["Login"] = null;
            Page.Response.Redirect(login.NextPagePath);
        }
    }
}