using System;
using System.Data;


public class clsCompany
{
    //--- hinterlegen der Firmennamen für spezielle Funktionen
    public const string const_CompanyName_VWSachsen = "VWSachsen";

    public clsSQLcon_Call SQLconCall = new clsSQLcon_Call();
    public clsCompanyGroup CompanyGroup = new clsCompanyGroup();
    public Int32 ID { get; set; }
    public string Fullname { get; set; }
    public string Shortname { get; set; }
    public Int32 AbBereichID { get; set; }



    /*****************************************************************************
    *                Methoden / Procedure
    * **************************************************************************/
    ///<summary>clsCompany / Fill</summary>
    ///<remarks></remarks>
    public void Fill()
    {
        DataTable dt = new DataTable();
        string strSQL = "Select * " +
                                    "FROM Company " +
                                    "WHERE ID=" + this.ID + ";";

        dt = this.SQLconCall.ExecuteSQL_GetDataTable(strSQL, "Company");
        if (dt.Rows.Count > 0)
        {
            SetClassValue(ref dt);
        }
    }
    ///<summary>clsCompany / SetClassValue</summary>
    ///<remarks></remarks>
    public void SetClassValue(ref DataTable dt)
    {
        for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
        {
            //Vorinitialisierung
            this.ID = 0;
            this.Fullname = string.Empty;
            this.Shortname = string.Empty;
            //*********************************************
            //ID
            Int32 iTmp = 0;
            if (dt.Columns.Contains("ID"))
            {
                Int32.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                this.ID = iTmp;
                CompanyGroup = new clsCompanyGroup();
                CompanyGroup.InitClass(this.ID);
            }
            //Fullname
            if (dt.Columns.Contains("Fullname"))
            {
                this.Fullname = dt.Rows[i]["Fullname"].ToString();
            }
            //Shortname
            if (dt.Columns.Contains("Shortname"))
            {
                this.Shortname = dt.Rows[i]["Shortname"].ToString();
            }
            //Arbeitsbereich
            if (dt.Columns.Contains("AbBereichID"))
            {
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["AbBereichID"].ToString(), out iTmp);
                this.AbBereichID = iTmp;
            }
        }
    }
}



