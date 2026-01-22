using System;
using System.Collections.Generic;
using System.Data;

public class clsRole
{
    public const string const_Role_Administrator = "Administrator";
    public const string const_Role_Manager = "Manager";
    public const string const_Role_User = "User";
    public const string const_Role_Gast = "Gast";

    public clsSQLcon_Call SQLconCall = new clsSQLcon_Call();
    public Int32 ID { get; set; }
    public string Bezeichnung { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsManager { get; set; }
    public bool IsUser { get; set; }
    public string Access { get; set; }
    private Dictionary<Int32, clsRole> _DictRoleClass;
    public Dictionary<Int32, clsRole> DictRoleClass
    {
        get
        {
            return _DictRoleClass;
        }
        set
        {
            _DictRoleClass = value;
        }
    }

    /*****************************************************************************
    *                Methoden / Procedure
    * **************************************************************************/
    ///<summary>clsRole / Copy</summary>
    ///<remarks></remarks>
    public clsRole Copy()
    {
        return (clsRole)this.MemberwiseClone();
    }
    ///<summary>clsRole / Fill</summary>
    ///<remarks></remarks>
    public void Fill()
    {
        DataTable dt = new DataTable();
        string strSQL = "Select * " +
                                    "FROM Role " +
                                    "WHERE ID=" + this.ID + ";";

        dt = this.SQLconCall.ExecuteSQL_GetDataTable(strSQL, "User");
        if (dt.Rows.Count > 0)
        {
            SetClassValue(ref dt);
        }
    }
    ///<summary>clsRole / GetRoleForDataTable</summary>
    ///<remarks></remarks>
    public DataTable GetRoleDataTable()
    {
        DataTable dtRole = new DataTable();
        string strSQL = "Select * " +
                            "FROM Role ";
        dtRole = this.SQLconCall.ExecuteSQL_GetDataTable(strSQL, "Role");
        return dtRole;
    }
    ///<summary>clsRole / SetClassValue</summary>
    ///<remarks></remarks>
    public void SetClassValue(ref DataTable dt)
    {
        DictRoleClass = new Dictionary<int, clsRole>();
        for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
        {
            //Vorinitialisierung
            this.ID = 0;
            this.Bezeichnung = string.Empty;
            this.IsAdmin = false;
            this.IsManager = false;
            this.IsUser = false;
            this.Access = clsRole.const_Role_User;
            //*********************************************
            //ID
            Int32 iTmp = 0;
            if (dt.Columns.Contains("ID"))
            {
                Int32.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                this.ID = iTmp;
            }
            //Bezeichnung
            if (dt.Columns.Contains("Bezeichnung"))
            {
                this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
            }
            //IsAdmin
            if (dt.Columns.Contains("IsAdmin"))
            {
                this.IsAdmin = (bool)dt.Rows[i]["IsAdmin"];
            }
            //IsManager
            if (dt.Columns.Contains("IsManager"))
            {
                this.IsManager = (bool)dt.Rows[i]["IsManager"];
            }
            //IsUser
            if (dt.Columns.Contains("IsUser"))
            {
                this.IsUser = (bool)dt.Rows[i]["IsUser"];
            }
            //Acess
            if (dt.Columns.Contains("Access"))
            {
                this.Access = dt.Rows[i]["Access"].ToString();
            }
            clsRole tmpRole = this.Copy();
            this.DictRoleClass.Add(tmpRole.ID, tmpRole);
        }
    }
    ///<summary>clsRole / GetDictRoleClass</summary>
    ///<remarks></remarks>
    public Dictionary<Int32, clsRole> GetDictRoleClass()
    {
        DataTable dt = new DataTable();
        string strSQL = "Select * FROM Role ;";

        dt = this.SQLconCall.ExecuteSQL_GetDataTable(strSQL, "Role");
        if (dt.Rows.Count > 0)
        {
            SetClassValue(ref dt);
        }
        return this.DictRoleClass;
    }

}
