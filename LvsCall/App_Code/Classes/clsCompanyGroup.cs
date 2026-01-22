using System;
using System.Collections.Generic;
using System.Data;

public class clsCompanyGroup
{
    public clsSQLcon_Call SQLconCall = new clsSQLcon_Call();
    public Int32 ID { get; set; }
    public Int32 CompanyID { get; set; }
    public Int32 AdrID { get; set; }
    public bool IsLieferant { get; set; }
    public List<Int32> ListCompanyGroupAdrID { get; set; }


    /*****************************************************************************
    *                Methoden / Procedure
    ***************************************************************************/
    ///<summary>clsCompanyGroup / InitClass</summary>
    ///<remarks></remarks>
    public void InitClass(Int32 myCompanyID)
    {
        this.CompanyID = myCompanyID;
        if (this.CompanyID > 0)
        {
            FillList();
        }
    }
    ///<summary>clsCompanyGroup / FillList</summary>
    ///<remarks></remarks>
    public void FillList()
    {
        ListCompanyGroupAdrID = new List<Int32>();
        DataTable dt = new DataTable();
        string strSQL = "Select a.ID, a.AdrID " +
                                    "FROM CompanyGroup a " +
                                    "WHERE a.CompanyID=" + this.CompanyID + ";";

        dt = this.SQLconCall.ExecuteSQL_GetDataTable(strSQL, "CompanyGroup");
        if (dt.Rows.Count > 0)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                Int32 iTmp = 0;
                if (i == 0)
                {
                    iTmp = 0;
                    Int32.TryParse(dt.Rows[0]["ID"].ToString(), out iTmp);
                    this.ID = iTmp;

                    this.Fill();
                }
                Int32.TryParse(dt.Rows[i]["AdrID"].ToString(), out iTmp);
                ListCompanyGroupAdrID.Add(iTmp);
            }
        }
    }
    ///<summary>clsCompanyGroup / Fill</summary>
    ///<remarks></remarks>
    public void Fill()
    {
        ListCompanyGroupAdrID = new List<Int32>();
        DataTable dt = new DataTable();
        string strSQL = "Select a.* " +
                                    "FROM CompanyGroup a " +
                                    "WHERE a.ID=" + this.ID + ";";

        dt = this.SQLconCall.ExecuteSQL_GetDataTable(strSQL, "CompanyGroup");
        if (dt.Rows.Count > 0)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                //Vorinitialisierung
                this.ID = 0;
                this.CompanyID = 0;
                this.AdrID = 0;
                //****************************
                //ID
                Int32 iTmp = 0;
                if (dt.Columns.Contains("ID"))
                {
                    Int32.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                    this.ID = iTmp;
                }
                //CompanyID
                iTmp = 0;
                if (dt.Columns.Contains("CompanyID"))
                {
                    Int32.TryParse(dt.Rows[i]["CompanyID"].ToString(), out iTmp);
                    this.CompanyID = iTmp;
                }
                //AdrID
                iTmp = 0;
                if (dt.Columns.Contains("AdrID"))
                {
                    Int32.TryParse(dt.Rows[i]["AdrID"].ToString(), out iTmp);
                    this.AdrID = iTmp;
                }
                //IsLieferant
                iTmp = 0;
                if (dt.Columns.Contains("IsLieferant"))
                {
                    //Int32.TryParse(dt.Rows[i]["IsLieferant"].ToString(), out iTmp);
                    this.IsLieferant = (bool)dt.Rows[i]["IsLieferant"];
                }
            }
        }
    }

}
