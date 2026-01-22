using System;
using System.Data;


namespace LVS
{
    public class clsWerk
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

        public clsHalle Halle = new clsHalle();
        public DataTable dtWerk = new DataTable("Werk");

        private decimal _ID;
        private string _Bezeichnung;
        private string _Beschreibung;
        private Int32 _OrderID;
        private Int32 _maxOrderID;
        private bool _ExLagerOrt;

        private Int32 _maxOrderIDHalle;

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
        public bool ExLagerOrt
        {
            get { return _ExLagerOrt; }
            set { _ExLagerOrt = value; }
        }
        public Int32 maxOrderIDHalle
        {
            get
            {
                _maxOrderIDHalle = GetMaxOrderIDHalle();
                return _maxOrderIDHalle;
            }
            set { _maxOrderIDHalle = value; }
        }

        /**********************************************************************************************************************
         *                                               Methoden
         *********************************************************************************************************************/

        ///<summary>clsWerk / Add</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB.</remarks>
        public void Init()
        {
            GetWerkDatenForDataTable();
            //Halle.Init();
            if (ExistWerkByID())
            {
                FillDaten();
                Halle.WerkID = ID;
            }
            Halle.Init();
        }
        ///<summary>clsWerk / Add</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB.</remarks>
        public void Add()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO Werk (Bezeichnung, Beschreibung, OrderID, exLagerOrt) " +
                                            "VALUES ('" + Bezeichnung + "' " +
                                                   ", '" + Beschreibung + "' " +
                                                   ", " + OrderID + " " +
                                                   ", '" + ExLagerOrt + "' " +
                                                     ")";
            strSql = strSql + "Select @@IDENTITY as 'ID' ";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            ID = decTmp;

            //Add Logbucheintrag Eintrag
            string strInfo = "Stammdaten Werk erstellt: ID [" + ID.ToString() + "] / Bezeichnung: " + Bezeichnung;
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), strInfo);
            UpdateOrderID(ID, OrderID);
        }
        ///<summary>clsWerk / UpdateLagerEingang</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public bool Update()
        {
            if (ExistWerk())
            {
                string strSql = string.Empty;
                strSql = "Update Werk SET" +
                                        " Bezeichnung ='" + Bezeichnung + "'" +
                                        ", Beschreibung = '" + Beschreibung + "' " +
                                        ", OrderID = " + OrderID + " " +
                                        ", exLagerOrt = '" + ExLagerOrt + "' " +
                                        " WHERE ID='" + ID + "'";
                bool bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
                // Logbucheintrag Eintrag
                string strInfo = "Stammdaten Werk geändert: ID: " + ID.ToString() + " / Bezeichnung: " + Bezeichnung;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), strInfo);

                return bReturn;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsWerk / ExistWerk</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        private bool ExistWerk()
        {
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Werk WHERE ID=" + ID + " ;";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsWerk / DeleteLEingangByLEingangTableID</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            if (ExistWerk())
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                    "LagerOrt=0 " +
                                    ", LOTable='' " +
                                    "WHERE ID IN (" +
                                                    //Alle Artikel Werk
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1 AND a.LagerOrt=" + ID + " AND a.LOTable='Werk' " +
                                                    " UNION " +
                                                    //Alle Artikel der Halle
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Halle' " +
                                                                        "AND a.LagerOrt IN (Select x.ID FROM Halle x WHERE x.WerkID=" + ID + ") " +

                                                    " UNION " +
                                                    //Alle Artikel der Reihe
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Reihe' " +
                                                                        "AND a.LagerOrt IN (" +
                                                                                            "Select r.ID FROM Reihe r " +
                                                                                                          "INNER JOIN Halle h ON h.ID=r.HalleID " +
                                                                                                          "WHERE h.WerkID=" + ID + " " +
                                                                                           ")" +
                                                    " UNION " +
                                                    //Alle Artikel der Ebenen
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Ebene' " +
                                                                        "AND a.LagerOrt IN (" +
                                                                                            "Select e.ID FROM Ebene e " +
                                                                                                          "INNER JOIN Reihe r ON r.ID=e.ReiheID " +
                                                                                                          "INNER JOIN Halle h ON h.ID=r.HalleID " +
                                                                                                          "WHERE h.WerkID=" + ID + " " +
                                                                                           ")" +
                                                    " UNION " +
                                                    //Alle Artikel Platz
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Platz' " +
                                                                        "AND a.LagerOrt IN (" +
                                                                                            "Select p.ID FROM Platz p " +
                                                                                                            "INNER JOIN Ebene e ON p.EbeneID=e.ID " +
                                                                                                            "INNER JOIN Reihe r ON r.ID=e.ReiheID " +
                                                                                                            "INNER JOIN Halle h ON h.ID=r.HalleID " +
                                                                                                            "WHERE h.WerkID=" + ID + " " +
                                                                                           ") " +
                                                   ") ";
                strSql = strSql + "Delete FROM Werk WHERE ID=" + ID + " ;";

                if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "WerkDelete", this._GL_User.User_ID))
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Stammdaten Werk gelöscht: ID: " + ID.ToString() + " / Bezeichnung: " + Bezeichnung;
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
                }
            }
        }
        ///<summary>clsWerk / FillDaten</summary>
        ///<remarks>Ermittel die Daten des Lagereingangs anhand der TableID.</remarks>
        public bool FillDaten()
        {
            if (ExistWerk())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Werk WHERE ID=" + ID + " ;";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Werk");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        this.ID = (decimal)dt.Rows[i]["ID"];
                        this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                        this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                        this.OrderID = (Int32)dt.Rows[i]["OrderID"];
                        this.ExLagerOrt = (bool)dt.Rows[i]["exLagerOrt"];
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
        ///<summary>clsWerk / GetHallenDatenForDataTable</summary>
        ///<remarks>Ermittel die Daten anhand der WerkID.</remarks>
        public void GetWerkDatenForDataTable()
        {
            dtWerk.Clear();
            string strSql = string.Empty;
            strSql = "Select * FROM Werk Order By OrderID;";

            dtWerk = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Werk");
        }
        ///<summary>clsWerk / ExistWerk</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistWerkByID()
        {
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Werk WHERE ID=" + ID + " ;";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsWerk / ExistWerkByBezeichnung</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistWerkByBezeichnung()
        {
            if (Bezeichnung != string.Empty)
            {
                string strSql = string.Empty;
                strSql = "Select *FROM Werk WHERE Bezeichnung='" + Bezeichnung + "';";
                bool bResult = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
                return bResult;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsWerk / GetMaxOrderID</summary>
        ///<remarks>Ermittelt die max. OrderID.</remarks>
        ///<returns>Returns Int32</returns>
        private Int32 GetMaxOrderID()
        {
            string strSql = string.Empty;
            strSql = "Select Count(OrderID) FROM Werk;";
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
            strSql = "Select COUNT(a.OrderID) FROM Halle a " +
                                                // "INNER JOIN Werk b ON b.ID=a.WerkID "+
                                                "WHERE a.WerkID =" + ID + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }
        ///<summary>clsWerk / UpdateOrderID</summary>
        ///<remarks></remarks>
        public void UpdateOrderID(Decimal myID, Int32 myOrderID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select * FROM Werk Order BY OrderID ;";

            string strUpSQL = string.Empty;
            Int32 iCount = 0;
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Werk");
            if (dt.Rows.Count > 0)
            {
                if (myID > 0)
                {
                    strUpSQL = strUpSQL + "Update Werk SET OrderID=" + myOrderID + " WHERE ID=" + myID + "; ";
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
                        strUpSQL = strUpSQL + "Update Werk SET OrderID=" + iCount + " WHERE ID=" + decTmp + "; ";
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
