using System;
using System.Data;

namespace LVS
{
    public class clsEbene
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

        public clsPlatz Platz = new clsPlatz();
        public DataTable dtEbene = new DataTable("Ebenen");



        private decimal _ID;
        private string _Bezeichnung;
        private string _Beschreibung;
        private decimal _ReiheID;
        private Int32 _OrderID;
        private Int32 _maxOrderID;
        private Int32 _maxOrderIDPlatz;

        public decimal ID { get; set; }
        public decimal ReiheID { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public Int32 OrderID { get; set; }
        public Int32 maxOrderID
        {
            get
            {
                _maxOrderID = GetMaxOrderID();
                return _maxOrderID;
            }
            set { _maxOrderID = value; }
        }
        public Int32 maxOrderIDPlatz
        {
            get
            {
                _maxOrderIDPlatz = GetMaxOrderIDPlatz();
                return _maxOrderIDPlatz;
            }
            set { _maxOrderIDPlatz = value; }
        }


        /**********************************************************************************************************************
        *                                               Methoden
        *********************************************************************************************************************/
        ///<summary>clsReihe / Init</summary>
        ///<remarks></remarks>
        public void Init()
        {
            GetEbenenDatenForDataTable();
            if (ExistEbene())
            {
                FillDaten();
                Platz.EbeneID = ID;
            }
            if (ExistEbeneByReiheID())
            {
                Platz.Init();
            }
        }
        ///<summary>clsEbene / Add</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB.</remarks>
        public void Add()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO Ebene (ReiheID, OrderID, Bezeichnung, Beschreibung) " +
                                            "VALUES (" +
                                                   " " + ReiheID + " " +
                                                   ", " + OrderID + " " +
                                                   ", '" + Bezeichnung + "' " +
                                                   ", '" + Beschreibung + "' " +
                                                     ")";
            strSql = strSql + "Select @@IDENTITY as 'ID' ";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            ID = decTmp;

            //Add Logbucheintrag Eintrag
            string strInfo = "Stammdaten Ebene erstellt: ID [" + ID.ToString() + "] / Bezeichnung: " + Bezeichnung;
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), strInfo);
        }
        ///<summary>clsEbene / UpdateLagerEingang</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public bool Update()
        {
            if (ExistEbene())
            {
                string strSql = string.Empty;
                strSql = "Update Ebene SET" +
                                        " ReiheID =" + ReiheID + " " +
                                        ", OrderID =" + OrderID + " " +
                                        ", Bezeichnung ='" + Bezeichnung + "'" +
                                        ", Beschreibung = '" + Beschreibung + "' " +
                                        " WHERE ID=" + ID + " ;";
                bool bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
                // Logbucheintrag Eintrag
                string strInfo = "Stammdaten Ebene geändert: ID: " + ID.ToString() + " / Bezeichnung: " + Bezeichnung;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), strInfo);

                return bReturn;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsEbene / ExistWerk</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        private bool ExistEbene()
        {
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Ebene WHERE ID=" + ID + " ;";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsEbene / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            if (ExistEbene())
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                    "LagerOrt=0 " +
                                    ", LOTable='' " +
                                        "WHERE ID IN (" +
                                                        //Alle Artikel der Ebenen
                                                        "Select a.ID FROM Artikel a " +
                                                                            "WHERE BKZ=1 AND a.LagerOrt=" + ID + " AND a.LOTable='Ebene' " +
                                                        " UNION " +
                                                        //Alle Artikel Platz
                                                        "Select a.ID FROM Artikel a " +
                                                                            "WHERE BKZ=1  AND a.LOTable='Platz' " +
                                                                            "AND a.LagerOrt IN (" +
                                                                                                "Select p.ID FROM Platz p INNER JOIN Ebene e ON p.EbeneID=e.ID WHERE e.ReiheID=" + ID + " " +
                                                                                               ") " +
                                                    ") ";
                strSql = strSql + "Delete FROM Ebene WHERE ID=" + ID + " ;";

                if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "ReiheDelete", this._GL_User.User_ID))
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Stammdaten Ebene gelöscht: ID: " + ID.ToString() + " / Bezeichnung: " + Bezeichnung;
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
                }
            }
        }
        ///<summary>clsEbene / FillDaten</summary>
        ///<remarks>Ermittel die Daten anhand der TableID.</remarks>
        public bool FillDaten()
        {
            if (ExistEbene())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Ebene WHERE ID=" + ID + " ;";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Ebene");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        this.ID = (decimal)dt.Rows[i]["ID"];
                        this.ReiheID = (decimal)dt.Rows[i]["ReiheID"];
                        this.OrderID = (Int32)dt.Rows[i]["OrderID"];
                        this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                        this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
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
        ///<summary>clsEbene / GetHallenDatenForDataTable</summary>
        ///<remarks>Ermittel die Daten anhand der WerkID.</remarks>
        public void GetEbenenDatenForDataTable()
        {
            dtEbene.Clear();
            string strSql = string.Empty;
            if (ExistEbeneByReiheID())
            {

                strSql = "Select * FROM Ebene WHERE ReiheID=" + ReiheID + " Order By OrderID;"; ;

                dtEbene = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Reihen");
            }
            else
            {
                strSql = "Select * FROM Ebene Order By OrderID;";
                dtEbene = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
                dtEbene.Rows.Clear();
            }
        }
        ///<summary>clsEbene / GetHallenDatenForDataTable</summary>
        ///<remarks>Ermittel die Daten anhand der WerkID.</remarks>
        public void GetReihenDatenForDataTable()
        {
            dtEbene.Clear();
            string strSql = string.Empty;
            if (ExistEbeneByReiheID())
            {

                strSql = "Select * FROM Ebene WHERE ReiheID=" + ReiheID + " Order By OrderID;";

                dtEbene = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Ebene");
            }
            else
            {
                strSql = "Select * FROM Ebene Order By OrderID;";
                dtEbene = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Ebene");
                dtEbene.Rows.Clear();
            }
        }
        ///<summary>clsEbene / ExistWerk</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistEbeneByReiheID()
        {
            if (ReiheID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Ebene WHERE ReiheID=" + ReiheID + " ;";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsEbene / ExistReiheByBezeichnung</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistEbeneByBezeichnung()
        {
            if (Bezeichnung != string.Empty)
            {
                string strSql = string.Empty;
                strSql = "Select a.ID FROM Ebene a " +
                                        "INNER JOIN Reihe d ON d.ID = a.ReiheID " +
                                        "WHERE a.ReiheID=" + ReiheID + " AND a.Bezeichnung='" + Bezeichnung + "' ;";
                bool bResult = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
                return bResult;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsEbene / GetMaxOrderID</summary>
        ///<remarks>Ermittelt die max. OrderID.</remarks>
        ///<returns>Returns Int32</returns>
        private Int32 GetMaxOrderID()
        {
            string strSql = string.Empty;
            strSql = "Select Count(OrderID) FROM Ebene WHERE ReiheID=" + ReiheID + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }
        ///<summary>clsEbene / GetMaxOrderIDPlatz</summary>
        ///<remarks>Ermittelt die max. OrderID der Halle für das einzelne Werk.</remarks>
        ///<returns>Returns Int32</returns>
        private Int32 GetMaxOrderIDPlatz()
        {
            string strSql = string.Empty;
            strSql = "Select COUNT(a.OrderID) FROM Ebene a " +
                                                //"INNER JOIN Halle c ON c.ID= d.HalleID " +
                                                //"INNER JOIN Werk b ON b.ID=c.WerkID " +
                                                //"INNER JOIN Reihe d ON d.ID=a.ReiheID "+
                                                "WHERE a.ReiheID =" + ID + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }
        ///<summary>clsEbene / UpdateOrderID</summary>
        ///<remarks></remarks>
        public void UpdateOrderID(Decimal myID, Int32 myOrderID)
        {
            if (ExistEbeneByReiheID())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Ebene WHERE  ReiheID=" + ReiheID + " Order BY OrderID ;";

                string strUpSQL = string.Empty;
                Int32 iCount = 0;
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
                if (dt.Rows.Count > 0)
                {
                    if (myID > 0)
                    {
                        strUpSQL = strUpSQL + "Update Ebene SET OrderID=" + myOrderID + " WHERE ID=" + myID + "; ";
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
                            strUpSQL = strUpSQL + "Update Ebene SET OrderID=" + iCount + " WHERE ID=" + decTmp + "; ";
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
