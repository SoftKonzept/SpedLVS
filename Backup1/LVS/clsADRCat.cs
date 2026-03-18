using System;
using System.Data;

namespace LVS
{
    public class clsADRCat
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
        //************************************

        public decimal ID { get; set; }
        public string Bezeichnung { get; set; }
        public string ViewID { get; set; }
        public bool activ { get; set; }


        /******************************************************************************************
         *                                  Methoden
         * ****************************************************************************************/
        ///<summary>clsADRCat / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, decimal myAdrID)
        {
            //this._GL_User = myGLUser;
            //this. = myID;
            //Fill();
            //InitSubClasses();
        }
        ///<summary>clsADRCat / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            string strSQL = string.Empty;
            strSQL = "INSERT INTO ADRCat (Bezeichnung, activ) " +
                                            "VALUES ('" + Bezeichnung + "'" +
                                                    "," + Convert.ToInt32(activ) +
                                                    "); ";
            strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
                //Fill();

                //Add Logbucheintrag Eintrag
                string Beschreibung = "Adress-Kategorie: " + Bezeichnung + " hinzugefügt";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
            }
        }
        ///<summary>clsADRCat / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRCat WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ADRKategorien");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                this.activ = (bool)dt.Rows[i]["activ"];
            }
        }
        ///<summary>clsADRCat / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            string strSql = string.Empty;
            strSql = "DELETE FROM ADRCat WHERE ID=" + ID;
            bool bDeleteOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
        }
        ///<summary>clsADRCat / Update</summary>
        ///<remarks></remarks>
        public void Update()
        {
            string strSQL = string.Empty;
            strSQL = "Update ADRCategory SET Bezeichnung='" + Bezeichnung + "'" +
                                                        " WHERE AdrID=" + ID + ";";

            clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            //Add Logbucheintrag update
            ViewID = clsADR.GetMatchCodeByID(ID, BenutzerID);
            string Beschreibung = "Adresse: " + ViewID + "  ID:" + ID + " geändert";
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
        }
    }
}
