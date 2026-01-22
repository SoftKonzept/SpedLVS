using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
//using Telerik.Web.UI;


/// <summary>
/// Zusammenfassungsbeschreibung für clsAbruf
/// </summary>
public class clsViews
{
    public const string const_ViewTableName_JournalCall = "JournalCall";
    public const string const_ViewTableName_Abruf = "AbrufView";
    public const string const_ViewTableName_Bestandsliste = "Bestandsliste";

    public clsSQLcon_Call SQLconCall = new clsSQLcon_Call();
    public clsSQLcon_LVS SQLconLVS = new clsSQLcon_LVS();

    public Int32 ID { get; set; }
    public string ViewTable { get; set; }
    public string ColumnName { get; set; }
    public string ColumnViewName { get; set; }
    public bool IsVisible { get; set; }
    public bool IsDisplayed { get; set; }
    public Int32 OrderIndex { get; set; }
    public Int32 SortIndex { get; set; }
    public string Page { get; set; }
    public bool SortOrderDesc { get; set; }
    public Int32 CompanyID { get; set; }
    public Dictionary<string, List<clsViews>> DictViews = new Dictionary<string, List<clsViews>>();
    public Dictionary<string, List<clsViews>> DictSort = new Dictionary<string, List<clsViews>>();
    public List<clsViews> ListTableView = new List<clsViews>();
    public List<clsViews> ListSort = new List<clsViews>();
    /*************************************************************************
     *                      Methoden / Procedure
     * ***********************************************************************/
    ///<summary>clsView / Copy</summary>
    ///<remarks></remarks>
    public clsViews Copy()
    {
        return (clsViews)this.MemberwiseClone();
    }
    ///<summary>clsView / InitClass</summary>
    ///<remarks></remarks>
    public void InitClass(Int32 iCompanyID)
    {
        this.CompanyID = iCompanyID;
        FillDictViews();
        FillDictSort();
    }
    ///<summary>clsView / FillDictSort</summary>
    ///<remarks></remarks>
    private void FillDictSort()
    {
        List<clsViews> listTmp = new List<clsViews>();
        DictSort = new Dictionary<string, List<clsViews>>();
        string strSql = string.Empty;
        DataTable dt = new DataTable();
        strSql = "Select * FROM Views " +
                                "  WHERE " +
                                    " SortIndex>0 " +
                                    " AND CompanyID=" + this.CompanyID +
                                    " order by ViewTable, SortIndex";
        dt = this.SQLconCall.ExecuteSQL_GetDataTable(strSql, "View");
        string strViewTable = string.Empty;
        clsViews tmpView = new clsViews();
        Int32 iRowCount = 0;
        foreach (DataRow row in dt.Rows)
        {
            if (
                    (!row["ViewTable"].ToString().Equals(strViewTable)) &&
                    (!strViewTable.Equals(string.Empty))
                )
            {
                if (!DictSort.ContainsKey(strViewTable))
                {
                    DictSort.Add(strViewTable, listTmp);
                }
                listTmp = new List<clsViews>();
            }
            tmpView = new clsViews();
            FillByRow(row, ref tmpView);
            listTmp.Add(tmpView);
            strViewTable = tmpView.ViewTable;
            iRowCount++;
            if (iRowCount >= dt.Rows.Count - 1)
            {
                if (!DictSort.ContainsKey(strViewTable))
                {
                    DictSort.Add(strViewTable, listTmp);
                }
            }
        }
    }
    ///<summary>clsView / FillDictionary</summary>
    ///<remarks></remarks>
    private void FillDictViews()
    {
        List<clsViews> listTmp = new List<clsViews>();
        DictViews = new Dictionary<string, List<clsViews>>();
        string strSql = string.Empty;
        DataTable dt = new DataTable();
        strSql = "Select * FROM Views " +
                                "WHERE " +
                                " CompanyID=" + this.CompanyID +
                                " Order By ViewTable, OrderIndex";
        dt = this.SQLconCall.ExecuteSQL_GetDataTable(strSql, "View");
        string strViewTable = string.Empty;
        clsViews tmpView = new clsViews();
        Int32 iRowCount = 0;
        foreach (DataRow row in dt.Rows)
        {
            if (
                    (!row["ViewTable"].ToString().Equals(strViewTable)) &&
                    (!strViewTable.Equals(string.Empty))
                )
            {
                if (!DictViews.ContainsKey(strViewTable))
                {
                    DictViews.Add(strViewTable, listTmp);
                }
                listTmp = new List<clsViews>();
            }
            tmpView = new clsViews();
            FillByRow(row, ref tmpView);
            listTmp.Add(tmpView);
            strViewTable = tmpView.ViewTable;
            iRowCount++;
            if (iRowCount == dt.Rows.Count - 1)
            {
                if (!DictViews.ContainsKey(strViewTable))
                {
                    DictViews.Add(strViewTable, listTmp);
                }
            }
        }
    }
    ///<summary>clsView / FillByRow</summary>
    ///<remarks></remarks>
    private void FillByRow(DataRow myRow, ref clsViews myView)
    {
        Int32 iTmp = 0;
        Int32.TryParse(myRow["ID"].ToString(), out iTmp);
        if (iTmp > 0)
        {
            myView.ID = iTmp;
            myView.ViewTable = myRow["ViewTable"].ToString();
            myView.ColumnName = myRow["ColumnName"].ToString();
            myView.ColumnViewName = myRow["ColumnViewName"].ToString();
            myView.IsVisible = (bool)myRow["IsVisible"];
            myView.IsDisplayed = (bool)myRow["IsDisplayed"];
            myView.Page = myRow["Page"].ToString();
            iTmp = 0;
            Int32.TryParse(myRow["OrderIndex"].ToString(), out iTmp);
            myView.OrderIndex = iTmp;
            iTmp = 0;
            Int32.TryParse(myRow["SortIndex"].ToString(), out iTmp);
            myView.SortIndex = iTmp;
            myView.SortOrderDesc = (bool)myRow["SortOrderDesc"];
            iTmp = 0;
            Int32.TryParse(myRow["CompanyID"].ToString(), out iTmp);
            myView.CompanyID = iTmp;
        }
    }
    ///<summary>clsView / FillListTableView</summary>
    ///<remarks></remarks>
    public void FillListTableView(string myTableViewName)
    {
        ListTableView = new List<clsViews>();
        if (DictViews.ContainsKey(myTableViewName))
        {
            ListTableView = DictViews[myTableViewName].ToList();
        }
    }
    ///<summary>clsView / FillListSort</summary>
    ///<remarks></remarks>
    public void FillListSort(string myTableViewName)
    {
        ListSort = new List<clsViews>();
        if (DictSort.ContainsKey(myTableViewName))
        {
            ListSort = DictSort[myTableViewName].ToList();
        }
    }

}