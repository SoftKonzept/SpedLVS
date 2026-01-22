using System;
using System.Collections.Generic;
using System.Data;

public class clsUser
{
    public const string const_UserAccess_Administrator = "Adminstrator";
    public const string const_UserAccess_Manager = "Manager";
    public const string const_UserAccess_User = "User";
    public const string const_UserAccess_Gast = "Gast";

    private DateTime DefaultMinDate = Convert.ToDateTime("01.01.1900");
    public clsSQLcon_Call SQLconCall = new clsSQLcon_Call();
    public clsRole clRole = new clsRole();
    public clsCompany clCompany = new clsCompany();
    public Int32 ID { get; set; }
    public string Name { get; set; }
    public string Vorname { get; set; }
    public string Loginname { get; set; }
    public string Passwort { get; set; }
    public string CompanyName { get; set; }
    private Int32 _CompanyID;
    public Int32 CompanyID
    {
        get
        {
            return _CompanyID;
        }
        set
        {
            _CompanyID = value;
            if (_CompanyID > 0)
            {
                clCompany = new clsCompany();
                clCompany.ID = _CompanyID;
                clCompany.Fill();
            }
        }
    }
    //public clsCompany Company { get; set; }
    public string Role { get; set; }
    private Int32 _RoleID;
    public Int32 RoleID
    {
        get
        {
            return _RoleID;
        }
        set
        {
            _RoleID = value;
            if (_RoleID > 0)
            {
                clRole = new clsRole();
                clRole.ID = _RoleID;
                clRole.Fill();
            }
        }
    }
    //public clsRole Role { get; set; }
    public string Schicht { get; set; }
    public DateTime AddDate { get; set; }
    public string InfoText { get; set; }
    public List<clsUser> ListUserClass { get; set; }
    private Dictionary<Int32, clsUser> _DictUserClass;
    public Dictionary<Int32, clsUser> DictUserClass
    {
        get
        {
            return _DictUserClass;
        }
        set
        {
            _DictUserClass = value;
        }
    }


    /*****************************************************************************
    *                Methoden / Procedure
    * **************************************************************************/
    ///<summary>clsUser / Copy</summary>
    ///<remarks></remarks>
    public clsUser Copy()
    {
        return (clsUser)this.MemberwiseClone();
    }
    ///<summary>clsUser / Add</summary>
    ///<remarks></remarks>
    public bool Add()
    {
        bool bOK = false;
        string strSQL = string.Empty;
        strSQL = "INSERT INTO Benutzer (Name, Vorname, Loginname, Passwort, RoleID, Schicht, AddDate, CompanyID) " +
                 "VALUES ('" + this.Name + "'" +
                             ", '" + this.Vorname + "'" +
                             ", '" + this.Loginname + "'" +
                             ", '" + this.Passwort + "'" +
                             ", " + this.RoleID +
                             ", '" + this.Schicht + "'" +
                             ", '" + this.AddDate + "'" +
                             ", " + this.CompanyID +
                             ");";
        //strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
        try
        {
            bOK = this.SQLconCall.ExecuteSQL(strSQL);
            if (bOK)
            {
                this.InfoText = "Der Datensatz konnte erfolgreich hinzugefügt werden.";
            }
            else
            {
                this.InfoText = "Der Datensatz konnte NICHT hinzugefügt werden.";
            }
        }
        catch (Exception ex)
        {
            this.InfoText = "Der neue Datensatz konnte nicht eingetragen werden. Fehlercode [" + clsErrorCode.const_ErrorCode_100 + "]";
        }
        return bOK;
    }
    ///<summary>clsUser / CheckUserLogin</summary>
    ///<remarks></remarks>
    public bool CheckUserLogin()
    {
        InfoText = "";
        if (Loginname == "")
        {
            InfoText = "No User choosed.";
            return false;
        }
        else
        {
            try
            {
                SQLconCall.init();
            }
            catch
            {
                InfoText = "Can't connect to Database [U].";
                return false;
            }
            DataTable dt = new DataTable();
            string strSQL = "Select TOP(1) b.* " +
                                    "FROM Benutzer b " +
                                    "INNER JOIN COMPANY c on c.ID=b.CompanyID " +
                                    "WHERE b.Loginname='" + this.Loginname + "' " +
                                            "AND b.Passwort='" + this.Passwort + "' " +
                                            "AND c.SHORTNAME='" + this.CompanyName + "' ";

            dt = this.SQLconCall.ExecuteSQL_GetDataTable(strSQL, "User");
            if (dt.Rows.Count > 0)
            {
                SetClassValue(ref dt);
                return true;
            }
            else
            {
                InfoText = "User not found.";
                return false;
            }
        }
    }
    ///<summary>clsUser / Delete</summary>
    ///<remarks></remarks>
    public bool Delete()
    {
        bool bOK = false;
        string strSQL = "Delete Benutzer WHERE ID=" + this.ID + " ;";
        bOK = this.SQLconCall.ExecuteSQL(strSQL);
        if (bOK)
        {
            this.InfoText = "Der Datensatz konnte erfolgreich gelöscht werden.";
        }
        else
        {
            this.InfoText = "Der Datensatz konnte nicht gelöscht werden.";
        }
        return bOK;
    }
    ///<summary>clsUser / Fill</summary>
    ///<remarks></remarks>
    public void Fill()
    {
        DataTable dt = new DataTable();
        string strSQL = "Select b.* " +
                                "FROM Benutzer b " +
                                "WHERE b.ID=" + this.ID + ";";

        dt = this.SQLconCall.ExecuteSQL_GetDataTable(strSQL, "User");
        if (dt.Rows.Count > 0)
        {
            SetClassValue(ref dt);
        }
    }
    ///<summary>clsUser / SetClassValue</summary>
    ///<remarks></remarks>
    public void SetClassValue(ref DataTable dt)
    {
        ListUserClass = new List<clsUser>();
        DictUserClass = new Dictionary<int, clsUser>();
        for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
        {
            //Vorinitialisierung
            this.ID = 0;
            this.Name = string.Empty;
            this.Vorname = string.Empty;
            this.Loginname = string.Empty;
            this.Passwort = string.Empty;
            this.clRole = new clsRole();
            this.clRole.ID = 0;
            this.clCompany = new clsCompany();
            this.clCompany.ID = 0;
            this.Schicht = string.Empty;
            this.AddDate = this.DefaultMinDate;

            //****************************

            //ID
            Int32 iTmp = 0;
            if (dt.Columns.Contains("ID"))
            {
                Int32.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                this.ID = iTmp;
            }
            //Name
            if (dt.Columns.Contains("Name"))
            {
                this.Name = dt.Rows[i]["Name"].ToString();
            }
            //Vorname
            if (dt.Columns.Contains("Vorname"))
            {
                this.Vorname = dt.Rows[i]["Vorname"].ToString();
            }
            //Loginname
            if (dt.Columns.Contains("Loginname"))
            {
                this.Loginname = dt.Rows[i]["Loginname"].ToString();
            }
            //Role
            iTmp = 0;
            if (dt.Columns.Contains("RoleID"))
            {
                Int32.TryParse(dt.Rows[i]["RoleID"].ToString(), out iTmp);
                this.RoleID = iTmp;
            }
            //CompanyID
            iTmp = 0;
            if (dt.Columns.Contains("CompanyID"))
            {
                Int32.TryParse(dt.Rows[i]["CompanyID"].ToString(), out iTmp);
                this.CompanyID = iTmp;
            }
            //Schicht
            if (dt.Columns.Contains("Schicht"))
            {
                this.Schicht = dt.Rows[i]["Schicht"].ToString();
            }
            //Password
            if (dt.Columns.Contains("Passwort"))
            {
                this.Passwort = dt.Rows[i]["Passwort"].ToString();
            }
            //DateAdd
            if (dt.Columns.Contains("AddDate"))
            {
                DateTime dtTmp = this.DefaultMinDate;
                DateTime.TryParse(dt.Rows[i]["AddDate"].ToString(), out dtTmp);
                this.AddDate = dtTmp;
            }
            this.CompanyName = this.clCompany.Shortname;
            this.Role = this.clRole.Bezeichnung;

            clsUser TmpUser = this.Copy();
            this.ListUserClass.Add(TmpUser);
            this.DictUserClass.Add(TmpUser.ID, TmpUser);
        }
    }
    ///<summary>clsUser / Update</summary>
    ///<remarks></remarks>
    public bool Update()
    {
        bool bOK = false;
        if (this.ID > 0)
        {
            DataTable dt = new DataTable();
            string strSQL = "Update Benutzer SET " +
                                    "Name ='" + this.Name + "'" +
                                    ", Vorname='" + this.Vorname + "'" +
                                    ", Loginname ='" + this.Loginname + "'" +
                                    ", Passwort ='" + this.Passwort + "'" +
                                    ", RoleID =" + this.RoleID +
                                    ", Schicht='" + this.Schicht + "'" +
                                    //", AddDate ='"this.AddDate.ToString()+"'"+
                                    ", CompanyID=" + this.CompanyID +

                                    " WHERE ID=" + this.ID + ";";
            bOK = this.SQLconCall.ExecuteSQL(strSQL);
            if (bOK)
            {
                this.InfoText = "Der Datensatz konnte erfolgreich upgedatet werden!";
            }
            else
            {
                this.InfoText = "Der Datensatz konnte nicht upgedatet werden. Fehlercode[" + clsErrorCode.const_ErrorCode_101 + "]";
            }
        }
        else
        {
            this.InfoText = "Der Datensatz konnte nicht upgedatet werden. Fehlercode[" + clsErrorCode.const_ErrorCode_101 + "]";
        }
        return bOK;
    }
    ///<summary>clsUser / GetDictUserClass</summary>
    ///<remarks></remarks>
    public Dictionary<Int32, clsUser> GetDictUserClass()
    {
        DataTable dt = new DataTable();
        string strSQL = "Select * FROM Benutzer ;";

        dt = this.SQLconCall.ExecuteSQL_GetDataTable(strSQL, "Benutzer");
        if (dt.Rows.Count > 0)
        {
            SetClassValue(ref dt);
        }
        return DictUserClass;
    }
    /******************************************************************************************************
     *                              static procedure
     *****************************************************************************************************/
    ///<summary>clsUser / Fill</summary>
    ///<remarks></remarks>
    public List<clsUser> GetUserList()
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT a.* " +
                                    " FROM Benutzer a " +
                                    "INNER JOIN Company b ON b.ID=a.CompanyID " +
                                    "INNER JOIN Role r on r.ID=a.RoleID " +
                                    "  WHERE a.CompanyID=" + this.CompanyID + ";";

        dt = this.SQLconCall.ExecuteSQL_GetDataTable(strSQL, "User");
        SetClassValue(ref dt);
        return ListUserClass;
    }
    ///<summary>clsUser / Fill</summary>
    ///<remarks></remarks>
    public DataTable GetUserTable()
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT a.ID" +
                            ", a.Name" +
                            ", a.Vorname" +
                            ", a.Loginname" +
                            ", a.Password" +
                            ", a.Schicht" +

                            " FROM Benutzer a " +
                            "INNER JOIN Company b ON b.ID=a.CompanyID " +
                            "  WHERE a.CompanyID=" + this.CompanyID + ";";

        dt = this.SQLconCall.ExecuteSQL_GetDataTable(strSQL, "User");
        return dt;
    }
}

