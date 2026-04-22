using LVS;
using System;
using System.Data;

namespace Sped4.Classes
{
    class clsEinheiten
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

        private decimal _ID;
        private string _Bezeichnung;


        public decimal ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Bezeichnung
        {
            get { return _Bezeichnung; }
            set { _Bezeichnung = value; }
        }


        /****************************************************************************************
         *                              Methoden
         * *************************************************************************************/
        ///<summary>clsEinheiten / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            Bezeichnung = Bezeichnung.Trim();
            string strSql = string.Empty;
            strSql = "INSERT INTO Einheiten (Bezeichnung) VALUES (" +
                                                        "'" + Bezeichnung + "'" +
                                                        "); " +
                     "Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            ID = decTmp;
            //Eingtrag in Artikel Vita
            if (ID > 0)
            {
                //Add Logbucheintrag 
                string myBeschreibung = "Einheit hinzugefügt: ID [" + ID.ToString() + "] - Bezeichnung[" + Bezeichnung + "]";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);
            }
        }
        ///<summary>clsEinheiten / Fill</summary>
        ///<remarks>Läd den entsprechenden Datensatz</remarks>
        public bool Fill()
        {
            if (ID > 0)
            {
                try
                {

                    DataTable dt = new DataTable();
                    string strSql = string.Empty;
                    strSql = "SELECT * FROM Einheiten WHERE ID=" + ID + ";";
                    dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Einheit");

                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        this.ID = (decimal)dt.Rows[i]["ID"];
                        this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsEinheiten / Update</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public void Update()
        {
            if (ID > 0)
            {
                Bezeichnung = Bezeichnung.Trim();
                string strSql = string.Empty;
                strSql = "Update Einheiten SET " +
                                        "Bezeichnung = '" + Bezeichnung + "' " +
                                        "WHERE ID=" + ID + "; ";

                bool bExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);

                if (bExecOK)
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Einheit geändert: ID [" + ID.ToString() + "] - Bezeichnung[" + Bezeichnung + "]";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), myBeschreibung);
                }
            }
        }
        ///<summary>clsEinheiten / Delete</summary>
        ///<remarks>Löschen des Datensatzes.</remarks>
        public void Delete()
        {
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Delete Einheiten WHERE ID=" + ID + "; ";
                bool bExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);

                if (bExecOK)
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Einheit gelöscht: ID [" + ID.ToString() + "] - Bezeichnung[" + Bezeichnung + "]";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
                }
            }
        }
        ///<summary>clsEinheiten / Delete</summary>
        ///<remarks>Löschen des Datensatzes.</remarks>
        public static DataTable GetEinheiten(Globals._GL_USER myGLUser)
        {
            DataTable dt = new DataTable("Einheiten");
            string strSql = string.Empty;
            strSql = "Select * FROM Einheiten ORDER BY Bezeichnung; ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Einheiten");
            return dt;
        }
        ///<summary>clsEinheiten / Delete</summary>
        ///<remarks>Löschen des Datensatzes.</remarks>
        public bool ExistEinheitsbezeichnung()
        {
            string strSql = string.Empty;
            strSql = "Select ID FROM Einheiten WHERE Bezeichnung='" + Bezeichnung + "'; ";
            bool bExist = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            return bExist;
        }
    }
}
