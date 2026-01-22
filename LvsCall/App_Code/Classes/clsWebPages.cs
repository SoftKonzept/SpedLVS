public class clsWebPages
{

    private string _Page_Main;
    public string Page_Main
    {
        get
        {
            _Page_Main = "Main.aspx";
            return _Page_Main;
        }
    }
    private string _Page_Abruf;
    public string Page_Abruf
    {
        get
        {
            _Page_Abruf = "Abruf.aspx";
            return _Page_Abruf;
        }
    }

    private string _Page_AbrufBestand;
    public string Page_AbrufBestand
    {
        get
        {
            _Page_AbrufBestand = "Abruf.aspx?AID=0";
            return _Page_AbrufBestand;
        }
    }
    private string _Page_UmbuchungBestand;
    public string Page_UmbuchungBestand
    {
        get
        {
            _Page_UmbuchungBestand = "Abruf.aspx?AID=1";
            return _Page_UmbuchungBestand;
        }
    }

    private string _Page_Abruf_ArtikelToSelect;
    public string Page_Abruf_ArtikelToSelect
    {
        get
        {
            _Page_Abruf_ArtikelToSelect = "ArtikelListToSelect.aspx";
            return _Page_Abruf_ArtikelToSelect;
        }
    }
    private string _Page_AbrufList_vorgemerkteAbrufe;
    public string Page_AbrufList_vorgemerkteAbrufe
    {
        get
        {
            _Page_AbrufList_vorgemerkteAbrufe = "AbrufList.aspx?AID=1";
            return _Page_AbrufList_vorgemerkteAbrufe;
        }
    }
    private string _Page_AbrufList_offeneAbrufe;
    public string Page_AbrufList_offeneAbrufe
    {
        get
        {
            _Page_AbrufList_offeneAbrufe = "AbrufList.aspx?AID=2";
            return _Page_AbrufList_offeneAbrufe;
        }
    }


    private string _Page_AbrufList_vorgemerkteUB;
    public string Page_AbrufList_vorgemerkteUB
    {
        get
        {
            _Page_AbrufList_vorgemerkteUB = "AbrufList.aspx?AID=3";
            return _Page_AbrufList_vorgemerkteUB;
        }
    }
    private string _Page_AbrufList_offeneUmbuchungen;
    public string Page_AbrufList_offeneUmbuchungen
    {
        get
        {
            _Page_AbrufList_offeneUmbuchungen = "AbrufList.aspx?AID=4";
            return _Page_AbrufList_offeneUmbuchungen;
        }
    }

    private string _Page_LogIn;
    public string Page_LogIn
    {
        get
        {
            _Page_LogIn = "Login.aspx";
            return _Page_LogIn;
        }
    }

    private string _Page_LogOut;
    public string Page_LogOut
    {
        get
        {
            _Page_LogOut = "LogOut.aspx";
            return _Page_LogOut;
        }
    }
    private string _Page_UserList;
    public string Page_UserList
    {
        get
        {
            _Page_UserList = "UserList.aspx";
            return _Page_UserList;
        }
    }
    private string _Page_JournalCall;
    public string Page_JournalCall
    {
        get
        {
            _Page_JournalCall = "Journal.aspx?MID=0.aspx";
            return Page_JournalCall;
        }
    }
    private string _Page_JournalIN;
    public string Page_JournalIN
    {
        get
        {
            _Page_JournalIN = "Journal.aspx?MID=1.aspx";
            return _Page_JournalIN;
        }
    }
    private string _Page_JournalOUT;
    public string Page_JournalOUT
    {
        get
        {
            _Page_JournalOUT = "Journal.aspx?MID=2.aspx";
            return _Page_JournalOUT;
        }
    }
}
