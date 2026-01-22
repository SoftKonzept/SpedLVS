using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class LogOut : System.Web.UI.Page
{
    clsLogin login;
    protected void Page_Load(object sender, EventArgs e)
    {
        login = new clsLogin();
        if (Session["Login"] != null)
        {
            login = (clsLogin)Session["Login"];
            login.bInfoText = true;
            login.InfoText = string.Empty;
        }
        else
        {
            login.InitClass();
            Session["Login"] = login;
        }

        if ((login != null) && (login.LoggedIn))
        {
            login.Status = "LogOut";
        }
        if (!IsPostBack)
        {
            if (login.Status == "LogOut")
            {
                Session.Clear();
                Page.Response.Redirect("Login.aspx");
            }
        }
    }
}