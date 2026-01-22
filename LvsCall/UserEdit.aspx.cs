using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.ModelBinding;
using Telerik.Web.UI;
using LVS;


public partial class UserList : System.Web.UI.Page
{
    MasterPage myMasterPage;
    clsLogin Login;
    private clsUser Benutzer;
    internal const Int32 const_Role_AdministratorID = 1;
    internal const Int32 const_Role_GastID = 4;
    private clsUser _editUser;
    public clsUser editUser
    {
        get {
                return _editUser; 
            }
        set {
                _editUser = value;
            }
    }
    //clsUser EditedUser;
    private Int32 iCount = 0;
    //private string Default_FileName_ToExport = DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + "_" + "LvsCallUserList";
    private string Default_SiteUserInfotext = "System -> Userverwaltung -> Bearbeitung";

    ///<summary>UserEdit / Page_Load</summary>
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
                if (Login.EditUser != null)
                {
                    //EditMode
                    editUser = Login.EditUser;
                    InitComboBoxRole(ref cbRole);
                    ClearInputFields();
                    SetValueToFrm();
                    //cbRole.Enabled = (this.Benutzer.RoleID <= editUser.RoleID);
                }
                else
                {
                    editUser = new clsUser();
                    editUser.ID = 0;
                    editUser.RoleID = const_Role_GastID;
                    InitComboBoxRole(ref cbRole);
                    ClearInputFields();
                }
            }
            else
            {
                Login.InitClass();
                Session["Login"] = Login;
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
                if (Login.EditUser != null)
                {
                    //EditMode
                    editUser = Login.EditUser;
                }
                else
                {
                    editUser = new clsUser();
                    editUser.ID = 0;
                    editUser.RoleID = const_Role_GastID;
                }
            }
            InitComboBoxRole(ref cbRole);
        }
    }
    ///<summary>UserEdit / ClearInputFields</summary>
    ///<remarks></remarks>
    private void ClearInputFields()
    {
        tbName.Text = string.Empty;
        tbVorname.Text = string.Empty;
        tbLogin.Text = string.Empty;
        tbPass.Text = string.Empty;
        cbRole.SelectedIndex = 0;
        tbSchicht.Text = string.Empty;
        tbDateAdd.Text = string.Empty;
    }
    ///<summary>UserEdit / SetComboValue</summary>
    ///<remarks></remarks>
    private void SetValueToFrm()
    {
        try
        {
            tbName.Text = editUser.Name;
            tbVorname.Text = editUser.Vorname;
            tbLogin.Text = editUser.Loginname;
            tbPass.Text = editUser.Passwort;
            cbRole.SelectedIndex = 0;
            SetComboValue();
            tbSchicht.Text = editUser.Schicht;
            tbDateAdd.Text = editUser.AddDate.ToString();
            tbFirma.Text = editUser.clCompany.Shortname;

            SetEnableInputField(editUser.clRole);
        }
        catch (Exception ex)
        {
            string strError = ex.ToString();
        }
    }

    ///<summary>UserEdit / SetComboValue</summary>
    ///<remarks></remarks>
    private void SetEnableInputField(clsRole myRole)
    {
        bool bEnabled = !myRole.IsAdmin;
        try
        {
            tbName.Enabled = bEnabled;
            tbVorname.Enabled = bEnabled;
            tbLogin.Enabled = bEnabled;
            tbPass.Enabled = bEnabled;
            tbSchicht.Enabled = bEnabled;
            tbDateAdd.Enabled = bEnabled;
            tbFirma.Enabled = bEnabled;
        }
        catch (Exception ex)
        {
            string strError = ex.ToString();
        }
    }
    ///<summary>UserEdit / SetComboValue</summary>
    ///<remarks></remarks>
    private void SetComboValue()
    {
        if (editUser != null)
        {
            for (Int32 x = 0; x <= cbRole.Items.Count - 1; x++)
            {
                cbRole.SelectedIndex = x;
                Int32 iTmp = 0;
                Int32.TryParse(cbRole.SelectedValue.ToString(), out iTmp);
                if (cbRole.SelectedValue.Equals(editUser.RoleID.ToString()))
                {
                    cbRole.Items[x].Selected = true;
                    x = cbRole.Items.Count;
                }
                else
                {
                    cbRole.SelectedIndex = x;
                }
            }
        }
    }
    ///<summary>UserEdit / SetComboSelectedValueToUser</summary>
    ///<remarks></remarks>
    private void SetComboSelectedValueToUser(ref clsUser myUser)
    {
        if (editUser != null)
        {
            string strTmpCBText = cbRole.Text;
            for (Int32 x = 0; x <= cbRole.Items.Count - 1; x++)
            {
                cbRole.SelectedIndex = x;
                if (cbRole.Items[x].Text.Equals(strTmpCBText))
                {
                    cbRole.Items[x].Selected = true;
                    string strVal = cbRole.SelectedValue.ToString();
                    Int32 iTmp = 0;
                    Int32.TryParse(strVal, out iTmp);
                    myUser.RoleID = iTmp;
                    x = cbRole.Items.Count;
                }
                else
                {
                    myUser.RoleID = const_Role_GastID; //GAST
                }
            }
        }
    }
    ///<summary>UserEdit / InitCheckBoxRole</summary>
    ///<remarks></remarks>
    private void InitComboBoxRole(ref RadComboBox myComboBox)
    {
        clsRole role = new clsRole();
        DataTable dtRole=role.GetRoleDataTable();
        myComboBox.DataValueField = "ID";
        myComboBox.DataTextField = "Bezeichnung";
        myComboBox.DataSource = dtRole;
        myComboBox.DataBind();
        if (this.Benutzer != null)
        {
            for (Int32 x = 0; x <= cbRole.Items.Count - 1; x++)
            {
                cbRole.SelectedIndex = x;
                Int32 iSelVal = 0;
                Int32.TryParse(cbRole.SelectedValue.ToString(), out iSelVal);
                switch (iSelVal)
                {
                    case const_Role_AdministratorID:
                        if (this.Benutzer.RoleID >= const_Role_AdministratorID)
                        {
                            if (this.editUser.ID > 0)
                            {
                                if (this.editUser.RoleID > const_Role_AdministratorID)
                                {
                                    cbRole.Items[x].Remove();
                                    x = -1;
                                }
                                else
                                {
                                    cbRole.Enabled = false;
                                }
                            }
                            else
                            {
                                cbRole.Items[x].Remove();
                                x = -1;
                            }
                        }
                        break;
                    default:
                        if (this.editUser.ID > 0)
                        {
                            if (this.Benutzer.RoleID >= this.editUser.RoleID)
                            {
                                cbRole.Enabled = false;
                            }
                        }
                        break;
                }
            }
        }               
    }
    ///<summary>UserEdit / InitCheckBoxRole</summary>
    ///<remarks></remarks>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Login.EditUser = null;
        Login.AppRelativeVirtualPath = "~/";
        Login.NextPagePath = "UserList.aspx";
        Session["Login"] = Login;
        Page.Response.Redirect(Login.NextPagePath);
    }
    ///<summary>UserEdit / btnSave_Click</summary>
    ///<remarks></remarks>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strName = this.tbName.Text;
        //Check EIngabefelder
        if (CheckInputValue())
        {
            tbPass.ReadOnly = true;

            clsUser TmpUser = new clsUser();
            TmpUser.Name = tbName.Text.Trim();
            TmpUser.Vorname = tbVorname.Text.Trim();
            TmpUser.Loginname = tbLogin.Text.Trim();
            TmpUser.Passwort = tbPass.Text.Trim();
            TmpUser.Schicht = tbSchicht.Text.Trim();
            SetComboSelectedValueToUser(ref TmpUser);
            bool bProzessOK = false;
            //Unterscheidung Update / Insert
            if (editUser.ID > 0)
            {
                //update
                TmpUser.CompanyID = editUser.CompanyID;
                TmpUser.AddDate = editUser.AddDate;
                TmpUser.ID = editUser.ID;
                bProzessOK = TmpUser.Update();
            }
            else
            {
                //insert
                TmpUser.CompanyID = this.Benutzer.CompanyID;
                TmpUser.AddDate = DateTime.Now;
                TmpUser.ID = 0;
                bProzessOK = TmpUser.Add();
            }
            editUser = TmpUser;
            //Check Prozess
            if (bProzessOK)
            {
                //Ausgabe Textinfo
                //zurück zur Userlist
                Login.EditUser = null;
                Login.AppRelativeVirtualPath = "~/";
                Login.NextPagePath = "UserList.aspx";
              
                Session["Login"] = Login;
                Page.Response.Redirect(Login.NextPagePath);
            }
            else
            {
                //Ausgabe Textinfo
                //Zurück zur Eingabe
                tbName.Focus();
            }
        }
    }    
    ///<summary>UserEdit / CheckInputValue</summary>
    ///<remarks></remarks>
    private bool CheckInputValue()
    {
        bool bReturn = true;
        string strError = string.Empty;
        if (tbName.Text.Equals(string.Empty))
        {
            if (!strError.Equals(string.Empty))
            {
                strError += ", ";
            }
            strError += "Name";// +Environment.NewLine;
        }
        if (tbVorname.Text.Equals(string.Empty))
        {
            if (!strError.Equals(string.Empty))
            {
                strError += ", ";
            }
            strError += "Vorname";// +Environment.NewLine;
        }
        if (tbLogin.Text.Equals(string.Empty))
        {
            if (!strError.Equals(string.Empty))
            {
                strError += ", ";
            }
            strError += "Login";// +Environment.NewLine;
        }
        if (!clsRandomPass.CheckPasswort(tbPass.Text.Trim()))
        {
            if (!strError.Equals(string.Empty))
            {
                strError += ", ";
            }
            strError += "Passwort";// +Environment.NewLine;
        }
        string strValue = cbRole.SelectedValue.ToString();
        Int32 iSelVal = 0;
        Int32.TryParse(strValue, out iSelVal);
        if (iSelVal < 1)
        {
            if (!strError.Equals(string.Empty))
            {
                strError += ", ";
            }
            strError += "Role nicht gewählt";// +Environment.NewLine;
        }
        if (!strError.Equals(string.Empty))
        {
            strError = "Folgende Datenfelder sind nicht korrekt ausgefüllt >>> "+ strError;
            bReturn = false;
            if (myMasterPage == null)
            {
                myMasterPage = (MasterPage)this.Master;
            }
            myMasterPage.SetLableCaption(true, this.Default_SiteUserInfotext);
            myMasterPage.SetLUserInfoHead(this.Login.User.Vorname + " " + this.Login.User.Name);
            myMasterPage.SetLInfoText(true, strError);
        }
        return bReturn;
    }
    ///<summary>UserEdit / btnRefresh_Click</summary>
    ///<remarks></remarks>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Login.EditUser = editUser;
        Login.AppRelativeVirtualPath = "~/";
        Login.NextPagePath = "UserEdit.aspx";
        Session["Login"] = Login;
        Page.Response.Redirect(Login.NextPagePath);
        
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPasswort_Click(object sender, EventArgs e)
    {
        //tbPass.Text = clsRandomPass.RandomPasswortGenerator();
        editUser.Passwort= clsRandomPass.RandomPasswortGenerator();
        SetValueToFrm();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPassEdit_Click(object sender, EventArgs e)
    {
        tbPass.ReadOnly = false;
        editUser.Passwort = string.Empty; 
        SetValueToFrm();
        tbPass.Focus();
    }
}