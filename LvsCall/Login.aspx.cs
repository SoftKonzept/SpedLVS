using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    string strTmp; 
    List<string> listLog;
    MasterPage myMasterPage;
    private clsWebPages wPage = new clsWebPages();
    ///<summary>Login / InitializeCulture</summary>
    ///<remarks></remarks>
    protected override void InitializeCulture()
    {
        base.InitializeCulture();

        listLog = new List<string>();
        strTmp = string.Empty;
        if ((System.Globalization.CultureInfo)Session["Language"] != null)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = (System.Globalization.CultureInfo)Session["Language"];
        }
        //strTmp = "Current Culture: " + System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
        //listLog.Add(strTmp);
    }
    ///<summary>Login / Page_Load</summary>
    ///<remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Request.RawUrl.Contains(wPage.Page_LogIn))
        //{
        //    Response.Redirect(wPage.Page_LogOut);
        //}
        //else
        //{
            if (!IsPostBack)
            {
                Table1.Rows[5].Visible = false;
                Table1.Rows[5].Cells[0].Text = string.Empty;

                if (Request.QueryString["LOG"] != null && Request.QueryString["LOG"] == "OUT")
                {
                    Session.Clear();
                }
            }
        //}
    }
    ///<summary>Login / btnLogin_Click</summary>
    ///<remarks></remarks>
    protected void btnLogin_Click(object sender, EventArgs e)
    {
            if (
                (txtUser.Text != string.Empty) &&
                (txtCompany.Text != string.Empty) &&
                (txtPassword.Text != string.Empty)
                )
            {

                clsLogin Login = new clsLogin();
                if (Login.InitClass())
                {
                    try
                    {
                        Login.User.Loginname = txtUser.Text;
                            Login.User.Passwort = txtPassword.Text;
                            Login.User.CompanyName = txtCompany.Text;
                            if (Login.Login())
                            {
                                Table1.Rows[5].Cells[0].BackColor = System.Drawing.Color.Green;
                                Table1.Rows[5].Cells[0].Text = "Login okay";

                                Login.AppRelativeVirtualPath = "~/";
                                Login.NextPagePath = wPage.Page_Main;
                                Session["Login"] = Login;
                                Page.Response.Redirect(Login.NextPagePath);
                            }
                            else
                            {
                                Table1.Rows[5].Cells[0].BackColor = System.Drawing.Color.Red;
                                Table1.Rows[5].Cells[0].Text = Login.InfoText;
                            }
                    }
                    catch (Exception ex)
                    {
                        //Table1.Rows[5].Cells[0].Text = ex.ToString();
                        //Login.AppRelativeVirtualPath = "~/";
                        //Login.NextPagePath = wPage.Page_LogOut;
                        //Page.Response.Redirect(Login.NextPagePath);
                    }
                }
                else
                {
                }

            }
            else
            {
            }

    }

}