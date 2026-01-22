using System;
using System.Collections.Generic;
using System.Data;

public class clsMenu
{

    public const string const_Path_Abruf_offene = "AbrufList.aspx?AID=2";
    public const string const_Path_Abruf_Edit = "AbrufEdit.aspx";


    public clsSQLcon_Call SQLconCall = new clsSQLcon_Call();
    public decimal ID { get; set; }
    public decimal ParentID { get; set; }
    public string Designation { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
    public Int32 OrderID { get; set; }
    public string RoleForAction { get; set; }
    public bool IsActiv { get; set; }

    //private DataTable _dtMenuItems;
    //public DataTable dtMenuItems
    //{
    //    get
    //    {
    //        _dtMenuItems = GetMenuItems();
    //        return _dtMenuItems;
    //    }
    //    set
    //    {
    //        _dtMenuItems = value;
    //    }
    //}

    /*****************************************************************************
    *                Methoden / Procedure
    * **************************************************************************/

    ///<summary>clsMenu / GetMenuItems</summary>
    ///<remarks></remarks>
    public DataTable GetMenuItems(clsUser myUser)
    {
        DataTable dt = new DataTable();
        string strSQL = "Select " +
                            "ID " +
                            ", CASE " +
                                "WHEN ParentID=0 THEN NULL " +
                                "ELSE ParentID " +
                                "END as ParentID " +
                            ", Designation" +
                            ", Description" +
                            ", Link" +
                            ", OrderID" +
                            ", RoleForAction" +
                            " FROM Menu WHERE IsActiv=1 " +
                            "AND RoleForAction >=" + myUser.RoleID + " " +
                            "Order By ParentID, OrderID";

        dt = this.SQLconCall.ExecuteSQL_GetDataTable(strSQL, "Menü");
        return dt;
    }
    ///<summary>clsMenu / GetMenuItems</summary>
    ///<remarks></remarks>
    public List<Int32> GetListMenuItemToAccess(clsUser myUser)
    {
        DataTable dt = new DataTable();
        string strSQL = "Select " +
                            "ID" +
                            " FROM Menu " +
                                "WHERE " +
                                    "IsActiv=1 " +
                                    "AND RoleForAction >=" + myUser.RoleID + " " +
                                    "Order By ID";

        dt = this.SQLconCall.ExecuteSQL_GetDataTable(strSQL, "Menü");
        List<Int32> listReturn = new List<int>();
        for (Int32 x = 0; x <= dt.Rows.Count - 1; x++)
        {
            Int32 iTmp = 0;
            string strID = dt.Rows[x]["ID"].ToString();
            Int32.TryParse(strID, out iTmp);
            if (iTmp > 0)
            {
                listReturn.Add(iTmp);
            }
        }
        return listReturn;
    }
}
