using System;

public class clsLogin
{
    private clsSQLcon_Call SQLconCall;
    private clsSQLcon_LVS SQLconLVS;
    private clsUser _User = new clsUser();
    private clsCompany _Company = new clsCompany();
    private clsMenu MenuItems = new clsMenu();
    public clsJournal Journal = new clsJournal();
    public clsBestand Bestand = new clsBestand();
    public clsAbruf Abruf = new clsAbruf();
    public clsUser EditUser = new clsUser();
    public clsViews Views = new clsViews();
    //public clsWebPages Page = new clsWebPages();


    public Boolean LoggedIn { get; set; }
    public clsUser User { get; set; }
    public clsCompany Company { get; set; }

    private string _InfoText;
    public string InfoText
    {
        get
        {
            return _InfoText;
        }
        set
        {
            _InfoText = value;
        }
    }
    public bool bInfoText { get; set; }
    public string _NextPagePath;
    public string NextPagePath
    {
        get
        {
            return _NextPagePath;
        }
        set
        {
            _NextPagePath = value;
        }
    }
    public string _CurrentPagePath;
    public string CurrentPagePath
    {
        get
        {
            return _CurrentPagePath;
        }
        set
        {
            _CurrentPagePath = value;
            //if (!strTmp.Equals(_CurrentPagePath))
            //{
            //    LastPagePath = strTmp;
            //}
            //_CurrentPagePath = strTmp;
        }
    }

    public string _LastPagePath;
    public string LastPagePath
    {
        get
        {
            return _LastPagePath;
        }
        set
        {
            _LastPagePath = value;
        }
    }

    private string _AppRelativeVirualPath;
    public string AppRelativeVirtualPath
    {
        get
        {
            _AppRelativeVirualPath = "~/" + NextPagePath;
            return _AppRelativeVirualPath;
        }
        set
        {
            _AppRelativeVirualPath = value;
        }
    }
    public string Status { get; set; }

    public string SearchDataField { get; set; }
    public string SearchText { get; set; }
    public string SearchRowFilterString { get; set; }
    public string SearchLieferant { get; set; }
    public Int32 AID { get; set; }  //AbfrufNR
    /*****************************************************************************
     *                Methoden / Procedure
     * **************************************************************************/
    ///<summary>clsLogin / InitClass</summary>
    ///<remarks></remarks>
    public bool InitClass()
    {
        this.InfoText = string.Empty;

        this.SQLconCall = new clsSQLcon_Call();
        this.SQLconLVS = new clsSQLcon_LVS();
        if (
              (this.SQLconCall.init()) &&
              (this.SQLconLVS.init())
            )
        {
            this.User = new clsUser();
            return true;
        }
        else
        {
            this.InfoText += "| Conntection CALL: " + SQLconCall.Connection.ConnectionString + Environment.NewLine;
            this.InfoText += "| Conntection LVS: " + SQLconLVS.Connection.ConnectionString + Environment.NewLine;
            return false;
        }
    }
    ///<summary>clsLogin / Login</summary>
    ///<remarks></remarks>
    public bool Login()
    {
        LoggedIn = false;
        if (this.User == null)
        {
            //init Class
            this.User = new clsUser();
        }

        if (
            (this.User.Loginname != string.Empty) &&
            (this.User.Passwort != string.Empty) &&
            (this.User.CompanyName != string.Empty)
            )
        {
            //DBConnection prüfen
            if (this.SQLconCall.init())
            {
                if (User.CheckUserLogin())
                {
                    this.Company = new clsCompany();
                    this.Company.ID = this.User.CompanyID;
                    this.Company.Fill();

                    this.Views.InitClass(this.Company.ID);
                    LoggedIn = true;
                }
                else
                {
                    InfoText += User.InfoText;
                    LoggedIn = false;
                }
            }
            else
            {
                LoggedIn = false;
            }
        }
        return LoggedIn;
    }
}
