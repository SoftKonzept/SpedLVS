using System;
using System.Data;

namespace LVS
{
    public class clsCompany
    {
        public Globals._GL_USER _GL_User;

        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        public Int32 ID { get; set; }
        public string Fullname { get; set; }
        public string Shortname { get; set; }
        public Int32 AbBereichID { get; set; }
        public Int32 AdrId { get; set; }

        public clsCompanyGroup CompanyGroup;


        /*****************************************************************************
        *                Methoden / Procedure
        * **************************************************************************/
        public void InitCls(Globals._GL_USER myGLUser, int myAbBereichId, int myCompAdrId)
        {
            this._GL_User = myGLUser;
            if ((myCompAdrId > 0) && (myAbBereichId > 0))
            {
                this.AdrId = myCompAdrId;
                this.AbBereichID = myAbBereichId;
                this.FillByAdrIdAndArbeitsbereichId();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void FillByAdrIdAndArbeitsbereichId()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select * " +
                                        "FROM Company " +
                                        "WHERE AdrId =" + this.AdrId +
                                        " AND AbBereichID =" + this.AbBereichID +
                                        ";";

            dt = clsSQLCall.ExecuteSQL_GetDataTable(strSQL, this.BenutzerID, "Company");
            if (dt.Rows.Count > 0)
            {
                SetClassValue(ref dt);
            }
        }
        ///<summary>clsCompany / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select * " +
                                        "FROM Company " +
                                        "WHERE ID=" + this.ID + ";";

            dt = clsSQLCall.ExecuteSQL_GetDataTable(strSQL, this.BenutzerID, "Company");
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
                //AdrId
                if (dt.Columns.Contains("AdrId"))
                {
                    iTmp = 0;
                    Int32.TryParse(dt.Rows[i]["AdrId"].ToString(), out iTmp);
                    this.AdrId = iTmp;
                }
            }
        }
    }
}
