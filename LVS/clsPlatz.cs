using System;
using System.Data;

namespace LVS
{
    public class clsPlatz
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

        public DataTable dtPlatz = new DataTable("Plätze");

        public decimal ID { get; set; }
        public decimal EbeneID { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public Int32 OrderID { get; set; }
        public decimal GArtID { get; set; }
        public decimal vGewicht { get; set; }
        public decimal bGewicht { get; set; }
        private Int32 _maxOrderID;
        public Int32 maxOrderID
        {
            get
            {
                _maxOrderID = GetMaxOrderID();
                return _maxOrderID;
            }
            set { _maxOrderID = value; }
        }


        /**********************************************************************************************************************
         *                                               Methoden
         *********************************************************************************************************************/
        ///<summary>clsPlatz / Init</summary>
        ///<remarks></remarks>
        public void Init()
        {
            GetPlatzDatenForDataTable();
            if (ExistPlatz())
            {
                FillDaten();
            }
        }
        ///<summary>clsPlatz / Add</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB.</remarks>
        public void Add()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO Platz (EbeneID, Bezeichnung, Beschreibung, GArt, vGewicht, bGewicht, OrderID) " +
                                            "VALUES (" +
                                                   " " + EbeneID + " " +
                                                   ", '" + Bezeichnung + "' " +
                                                   ", '" + Beschreibung + "' " +
                                                   ", " + GArtID + " " +
                                                   ", " + vGewicht + " " +
                                                   ", " + bGewicht + " " +
                                                   ", " + OrderID + " " +
                                                     ")";
            strSql = strSql + "Select @@IDENTITY as 'ID' ";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            ID = decTmp;

            //Add Logbucheintrag Eintrag
            string strInfo = "Stammdaten Platz erstellt: ID [" + ID.ToString() + "] / Bezeichnung: " + Bezeichnung;
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), strInfo);
        }
        ///<summary>clsPlatz / Update</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public bool Update()
        {
            if (ExistPlatz())
            {
                string strSql = string.Empty;
                strSql = "Update Platz SET" +
                                        " EbeneID =" + EbeneID + " " +
                                        ", Bezeichnung ='" + Bezeichnung + "'" +
                                        ", Beschreibung = '" + Beschreibung + "' " +
                                        ", GArt =" + GArtID + " " +
                                        ", vGewicht =" + vGewicht + " " +
                                        ", bGewicht =" + bGewicht + " " +
                                        ", OrderID =" + OrderID + " " +
                                        " WHERE ID=" + ID + " ;";
                bool bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
                // Logbucheintrag Eintrag
                string strInfo = "Stammdaten Platz geändert: ID: " + ID.ToString() + " / Bezeichnung: " + Bezeichnung;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), strInfo);

                return bReturn;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsPlatz / ExistWerk</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistPlatz()
        {
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Platz WHERE ID=" + ID + " ;";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsPlatz / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            if (ExistPlatz())
            {
                string strSql = string.Empty;
                strSql = "Delete FROM Platz WHERE ID=" + ID + " ;";
                strSql = strSql + "Update Artikel SET " +
                                    "LagerOrt=0 " +
                                    ", LOTable='' " +
                                    "WHERE BKZ=1 AND LOTable='Platz' AND LagerOrt=" + ID + " ";

                if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "PlatzDelete", this._GL_User.User_ID))
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Stammdaten Platz gelöscht: ID: " + ID.ToString() + " / Bezeichnung: " + Bezeichnung;
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
                }
            }
        }
        ///<summary>clsPlatz / FillDaten</summary>
        ///<remarks>Ermittel die Daten anhand der TableID.</remarks>
        public bool FillDaten()
        {
            if (ExistPlatz())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Platz WHERE ID=" + ID + " ;";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Platz");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        this.ID = (decimal)dt.Rows[i]["ID"];
                        this.EbeneID = (decimal)dt.Rows[i]["EbeneID"];
                        this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                        this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                        this.OrderID = (Int32)dt.Rows[i]["OrderID"];
                        this.GArtID = (decimal)dt.Rows[i]["GArt"];
                        this.vGewicht = (decimal)dt.Rows[i]["vGewicht"];
                        this.bGewicht = (decimal)dt.Rows[i]["bGewicht"];
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsPlatz / GetHallenDatenForDataTable</summary>
        ///<remarks>Ermittel die Daten anhand der WerkID.</remarks>
        public void GetPlatzDatenForDataTable()
        {
            dtPlatz.Clear();
            string strSql = string.Empty;
            if (ExistPlatzByEbeneID())
            {

                strSql = "Select * FROM Platz WHERE EbeneID=" + EbeneID + " Order By OrderID;";
                dtPlatz = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Plätze");
            }
            else
            {
                strSql = "Select * FROM Platz Order By OrderID;";
                dtPlatz = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Plätze");
                dtPlatz.Rows.Clear();
            }
        }
        ///<summary>clsPlatz / ExistWerk</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        private bool ExistPlatzByEbeneID()
        {
            if (EbeneID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Platz WHERE EbeneID=" + EbeneID + " ;";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsEbene / ExistPlatzByBezeichnung</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistPlatzByBezeichnung()
        {
            if (Bezeichnung != string.Empty)
            {
                string strSql = string.Empty;
                strSql = "Select a.ID FROM Platz a " +
                                        "INNER JOIN Ebene b ON b.ID = a.EbeneID " +
                                        "WHERE a.EbeneID=" + EbeneID + " AND a.Bezeichnung='" + Bezeichnung + "';";
                bool bResult = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
                return bResult;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsPlatz / GetMaxOrderID</summary>
        ///<remarks>Ermittelt die max. OrderID.</remarks>
        ///<returns>Returns Int32</returns>
        private Int32 GetMaxOrderID()
        {
            string strSql = string.Empty;
            strSql = "Select Count(OrderID) FROM Platz WHERE EbeneID=" + EbeneID + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }

        ///<summary>clsPlatz / UpdateOrderID</summary>
        ///<remarks></remarks>
        public void UpdateOrderID(Decimal myID, Int32 myOrderID)
        {
            if (ExistPlatzByEbeneID())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Platz WHERE EbeneID=" + EbeneID + " Order BY OrderID ;";

                string strUpSQL = string.Empty;
                Int32 iCount = 0;
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
                if (dt.Rows.Count > 0)
                {
                    if (myID > 0)
                    {
                        strUpSQL = strUpSQL + "Update Platz SET OrderID=" + myOrderID + " WHERE ID=" + myID + "; ";
                    }
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        iCount++;
                        decimal decTmp = (decimal)dt.Rows[i]["ID"];
                        Int32 iTmp = (Int32)dt.Rows[i]["OrderID"];
                        if (myID != decTmp)
                        {
                            if (myOrderID > 0)
                            {
                                if (iCount == myOrderID)
                                {
                                    iCount++;
                                }
                            }
                            strUpSQL = strUpSQL + "Update Platz SET OrderID=" + iCount + " WHERE ID=" + decTmp + "; ";
                        }
                        else
                        {
                            iCount--;
                        }
                    }
                    //Updte per Transaction
                    clsSQLcon.ExecuteSQLWithTRANSACTION(strUpSQL, "UpdateOrderID", _GL_User.User_ID);
                }
            }
        }


    }
}
