using System;
using System.Data;


namespace LVS
{
    public class clsHalle
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

        public clsReihe Reihe = new clsReihe();
        public DataTable dtHalle = new DataTable("Halle");

        private decimal _ID;
        private string _Bezeichnung;
        private string _Beschreibung;
        private decimal _WerkID;
        private Int32 _OrderID;
        private Int32 _maxOrderID;
        private Int32 _maxOrderIDReihe;

        public decimal ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public decimal WerkID
        {
            get { return _WerkID; }
            set { _WerkID = value; }
        }
        public string Bezeichnung
        {
            get { return _Bezeichnung; }
            set { _Bezeichnung = value; }
        }
        public string Beschreibung
        {
            get { return _Beschreibung; }
            set { _Beschreibung = value; }
        }
        public Int32 OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }
        public Int32 maxOrderID
        {
            get
            {
                _maxOrderID = GetMaxOrderID();
                return _maxOrderID;
            }
            set { _maxOrderID = value; }
        }
        public Int32 maxOrderIDReihe
        {
            get
            {
                _maxOrderIDReihe = GetMaxOrderIDHalle();
                return _maxOrderIDReihe;
            }
            set { _maxOrderIDReihe = value; }
        }

        /**********************************************************************************************************************
         *                                               Methoden
         *********************************************************************************************************************/

        ///<summary>clsHalle / Add</summary>
        ///<remarks></remarks>
        public void Init()
        {
            GetHallenDatenForDataTable();
            if (ExistHalle())
            {
                FillDaten();
                Reihe.HalleID = ID;
            }
            if (ExistHalleByWerkID())
            {
                Reihe.Init();
            }
        }
        ///<summary>clsHalle / Add</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB.</remarks>
        public void Add()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO Halle (WerkID, Bezeichnung, Beschreibung, OrderID) " +
                                            "VALUES (" +
                                                   " '" + WerkID + "' " +
                                                   ", '" + Bezeichnung + "' " +
                                                   ", '" + Beschreibung + "' " +
                                                   ", " + OrderID + " " +
                                                     ")";
            strSql = strSql + "Select @@IDENTITY as 'ID' ";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            ID = decTmp;

            //Add Logbucheintrag Eintrag
            string strInfo = "Stammdaten Halle erstellt: ID [" + ID.ToString() + "] / Bezeichnung: " + Bezeichnung;
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), strInfo);

            //Nach dem Eintrag muss der DataTable Halle aktualisiert werden
            GetHallenDatenForDataTable();
        }
        ///<summary>clsHalle / Update</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public bool Update()
        {
            if (ExistHalle())
            {
                string strSql = string.Empty;
                strSql = "Update Halle SET" +
                                        " WerkID =" + WerkID + " " +
                                        ", Bezeichnung ='" + Bezeichnung + "'" +
                                        ", Beschreibung = '" + Beschreibung + "' " +
                                        ", OrderID = " + OrderID + " " +
                                        " WHERE ID=" + ID + " ;";
                bool bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
                // Logbucheintrag Eintrag
                string strInfo = "Stammdaten Halle geändert: ID: " + ID.ToString() + " / Bezeichnung: " + Bezeichnung;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), strInfo);
                return bReturn;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsHalle / ExistHalle</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        private bool ExistHalle()
        {
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Halle WHERE ID=" + ID + " ;";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsHalle / DeleteLEingangByLEingangTableID</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            if (ExistHalle())
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                    "LagerOrt=0 " +
                                    ", LOTable='' " +
                                    "WHERE ID IN (" +
                                                    //Alle Artikel der Halle
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1 AND a.LagerOrt=" + ID + " AND a.LOTable='Halle' " +
                                                    " UNION " +
                                                    //Alle Artikel der Reihe
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Reihe' " +
                                                                        "AND a.LagerOrt IN (Select x.ID FROM Reihe x WHERE x.HalleID=" + ID + ") " +
                                                    " UNION " +
                                                    //Alle Artikel der Ebenen
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Ebene' " +
                                                                        "AND a.LagerOrt IN (" +
                                                                                            "Select e.ID FROM Ebene e " +
                                                                                                          "INNER JOIN Reihe r ON e.ReiheID=r.ID " +
                                                                                                          "WHERE r.HalleID=" + ID + " " +
                                                                                           ")" +
                                                    " UNION " +
                                                    //Alle Artikel Platz
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Platz' " +
                                                                        "AND a.LagerOrt IN (" +
                                                                                            "Select p.ID FROM Platz p " +
                                                                                                            "INNER JOIN Ebene e ON p.EbeneID=e.ID " +
                                                                                                            "INNER JOIN Reihe r ON r.ID=e.ReiheID " +
                                                                                                            "WHERE r.HalleID=" + ID + " " +
                                                                                           ") " +
                                              ") ";

                strSql = strSql + "Delete FROM Halle WHERE ID=" + ID + " ;";

                if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "WerkDelete", this._GL_User.User_ID))
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Stammdaten Halle gelöscht: ID: " + ID.ToString() + " / Bezeichnung: " + Bezeichnung;
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);

                    //Nach dem Löschen muss der DataTable Halle aktualisiert werden
                    GetHallenDatenForDataTable();
                }
            }
        }
        ///<summary>clsHalle / FillDaten</summary>
        ///<remarks>Ermittel die Daten des Lagereingangs anhand der TableID.</remarks>
        public bool FillDaten()
        {
            if (ExistHalle())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Halle WHERE ID=" + ID + " ;";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        this.ID = (decimal)dt.Rows[i]["ID"];
                        this.WerkID = (decimal)dt.Rows[i]["WerkID"];
                        this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                        this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                        this.OrderID = (Int32)dt.Rows[i]["OrderID"];


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
        ///<summary>clsHalle / GetHallenDatenForDataTable</summary>
        ///<remarks>Ermittel die Daten anhand der WerkID.</remarks>
        public void GetHallenDatenForDataTable(bool bWODelete = false)
        {
            string strSql = string.Empty;
            dtHalle.Clear();
            if (ExistHalleByWerkID())
            {
                dtHalle.Clear();
                strSql = "Select * FROM Halle WHERE WerkID=" + WerkID + " Order By OrderID;";
                dtHalle = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
            }
            else
            {
                strSql = "Select * FROM Halle Order By OrderID;";
                dtHalle = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
                if (!bWODelete)
                    dtHalle.Rows.Clear();
            }
        }

        ///<summary>clsHalle / GetHallenDatenForDataTable</summary>
        ///<remarks>Ermittel die Daten anhand der WerkID.</remarks>
        public void GetHallenDatenForDataTableExtra(bool bWODelete = false)
        {
            string strSql = string.Empty;
            dtHalle.Clear();
            if (ExistHalleByWerkID())
            {
                dtHalle.Clear();
                strSql = "Select * FROM Halle WHERE WerkID=" + WerkID + " Order By OrderID;";
                dtHalle = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
            }
            else
            {
                strSql = "Select * FROM Halle Order By OrderID;";
                dtHalle = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
                if (!bWODelete)
                    dtHalle.Rows.Clear();
            }
        }

        ///<summary>clsHalle / ExistWerk</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistHalleByWerkID()
        {
            if (WerkID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Halle WHERE WerkID=" + WerkID + " ;";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsWerk / ExistHalleByBezeichnung</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistHalleByBezeichnung()
        {
            if (Bezeichnung != string.Empty)
            {
                string strSql = string.Empty;
                strSql = "Select a.ID FROM Halle a " +
                                            "INNER JOIN Werk b ON b.ID=a.WerkID " +
                                            "WHERE a.ID=" + WerkID + " AND a.Bezeichnung='" + Bezeichnung + "' ;";
                bool bResult = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
                return bResult;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsHalle / GetMaxOrderID</summary>
        ///<remarks>Ermittelt die max. OrderID.</remarks>
        ///<returns>Returns Int32</returns>
        private Int32 GetMaxOrderID()
        {
            string strSql = string.Empty;
            strSql = "Select COUNT(a.OrderID) FROM Halle a " +
                                                "INNER JOIN Werk b ON b.ID=a.WerkID " +
                                                "WHERE a.WerkID =" + this.WerkID + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }
        ///<summary>clsWerk / GetMaxOrderIDHalle</summary>
        ///<remarks>Ermittelt die max. OrderID der Halle für das einzelne Werk.</remarks>
        ///<returns>Returns Int32</returns>
        private Int32 GetMaxOrderIDHalle()
        {
            string strSql = string.Empty;
            strSql = "Select COUNT(a.OrderID) FROM Reihe a " +
                                                "INNER JOIN Halle c ON c.ID= a.HalleID " +
                                                "INNER JOIN Werk b ON b.ID=c.WerkID " +
                                                "WHERE a.HalleID =" + ID + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }
        ///<summary>clsWerk / UpdateOrderID</summary>
        ///<remarks></remarks>
        public void UpdateOrderID(Decimal myID, Int32 myOrderID)
        {
            if (ExistHalleByWerkID())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Halle WHERE WerkID=" + WerkID + " Order BY OrderID ;";

                string strUpSQL = string.Empty;
                Int32 iCount = 0;
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
                if (dt.Rows.Count > 0)
                {
                    if (myID > 0)
                    {
                        strUpSQL = strUpSQL + "Update Halle SET OrderID=" + myOrderID + " WHERE ID=" + myID + "; ";
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
                            strUpSQL = strUpSQL + "Update Halle SET OrderID=" + iCount + " WHERE ID=" + decTmp + "; ";
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
