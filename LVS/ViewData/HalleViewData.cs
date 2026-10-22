using DocumentFormat.OpenXml.Office2010.Excel;
using LVS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class HalleViewData
    {
        public Globals._GL_USER _GL_User;
        //public Werk werk = new Werk();
        //public List<Werk> ListWerk = new List<Werk>();

        public Halle halle = new Halle();
        public List<Halle> ListHalle = new List<Halle>();
        public Werk lo_Werk = new Werk();
        public ReiheViewData reiheVD = new ReiheViewData();

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


        public HalleViewData()
        {
            InitCls();
            GetHalleList();
            if (ListHalle.Count > 0)
            {
                halle = ListHalle[0];
                reiheVD = new ReiheViewData();
                reiheVD.Reihe.HalleId = halle.Id;
            }
        }
        public HalleViewData(Werk myWerk, int myUserId) : this()
        {
            this.BenutzerID = myUserId;
            lo_Werk = myWerk;
            //werk.Id = myWerkId;
            if (ExistHalle())
            {
                Fill();
                halle.WerkId = lo_Werk.Id;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            ListHalle = new List<Halle>();
            reiheVD = new ReiheViewData();
        }
        ///<summary>Werk / GetHallenDatenForDataTable</summary>
        ///<remarks>Ermittel die Daten anhand der WerkID.</remarks>
        public void GetHalleList()
        {
            ListHalle = new List<Halle>();
            string strSql = string.Empty;
            strSql =sql_Get_Main  +" Order By OrderID;";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "HalleList");
            foreach (DataRow dr in dt.Rows)
            {
                SetValue(dr);
                if(!ListHalle.Contains(halle))
                {
                    ListHalle.Add(halle);
                }
            }
            //dtWerk = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Werk");
        }

        ///<summary>clsHalle / GetHallenDatenForDataTable</summary>
        ///<remarks>Ermittel die Daten anhand der WerkID.</remarks>
        public void GetHallenDatenForList(bool bWODelete = false)
        {
            string strSql = string.Empty;
            ListHalle.Clear();            
            if (ExistHalleByWerkID())
            {
                DataTable dt = new DataTable();
                strSql = sql_Get_Main;
                strSql += " WHERE WerkID=" + lo_Werk.Id + " Order By OrderID;";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    if (!ListHalle.Contains(halle))
                    {
                        ListHalle.Add(halle);
                    }
                }
            }
            else
            {
                GetHalleList();
                if (!bWODelete)
                    ListHalle.Clear();
            }
        }
        ///<summary>clsHalle / ExistWerk</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistHalleByWerkID()
        {
            if (lo_Werk.Id > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Halle WHERE WerkID=" + lo_Werk.Id + " ;";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
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
            if (halle.Id > 0)
            {
                string strSql = string.Empty;
                strSql = sql_Get_Main;
                strSql += " WHERE ID=" + halle.Id + " ;";
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
            if (halle.Bezeichnung != string.Empty)
            {
                string strSql = string.Empty;
                strSql = "Select a.ID FROM Halle a " +
                                            "INNER JOIN Werk b ON b.ID=a.WerkID " +
                                            "WHERE a.ID=" + halle.Id + " AND a.Bezeichnung='" + halle.Bezeichnung + "' ;";
                bool bResult = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
                return bResult;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public bool Fill()
        {
            if (ExistHalle())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = sql_Get_Main + " WHERE Id = " + halle.Id;

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "halle");
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
                return (dt.Rows.Count > 0);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public void SetValue(DataRow row)
        {
            halle = new Halle();

            int iTmp = 0;
            int.TryParse(row["Id"].ToString(), out iTmp);
            halle.Id = iTmp;

            iTmp = 0;
            int.TryParse(row["WerkID"].ToString(), out iTmp);
            halle.WerkId = iTmp;

            halle.Bezeichnung = row["Bezeichnung"].ToString();
            halle.Beschreibung = row["Beschreibung"].ToString();

            iTmp = 0;
            int.TryParse(row["OrderID"].ToString(), out iTmp);
            halle.OrderId = iTmp;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void FillList(bool mybInclSub)
        {
        }
        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            string strSql = string.Empty;
            strSql = this.sql_Add;

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            halle.Id = iTmp;

            //Add Logbucheintrag Eintrag
            string strInfo = "Stammdaten Halle erstellt: ID [" + halle.Id.ToString() + "] / Bezeichnung: " + halle.Bezeichnung;
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), strInfo);

            //Nach dem Eintrag muss der DataTable Halle aktualisiert werden
            GetHallenDatenForList();
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        { 
            if (ExistHalle())
            {
                string strSql = string.Empty;
                strSql = sql_Delete;

                if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "HalleDelete", this._GL_User.User_ID))
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Stammdaten Halle gelöscht: ID: " + halle.Id.ToString() + " / Bezeichnung: " + halle.Bezeichnung;
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);

                    //Nach dem Löschen muss der DataTable Halle aktualisiert werden
                    GetHallenDatenForList();
                }
            }
        }
        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            if (ExistHalle())
            {
                string strSql = string.Empty;
                strSql = sql_Update;
                bool bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
                // Logbucheintrag Eintrag
                string strInfo = "Stammdaten Halle geändert: ID: " + halle.Id.ToString() + " / Bezeichnung: " + halle.Bezeichnung;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), strInfo);

                return bReturn;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsWerk / UpdateOrderID</summary>
        ///<remarks></remarks>
        public void UpdateOrderID(int myID, Int32 myOrderID)
        {
            if (ExistHalleByWerkID())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Halle WHERE WerkID=" + this.lo_Werk.Id + " Order BY OrderID ;";

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

        ///<summary>clsHalle / GetMaxOrderID</summary>
        ///<remarks>Ermittelt die max. OrderID.</remarks>
        ///<returns>Returns Int32</returns>
        private Int32 GetMaxOrderID()
        {
            string strSql = string.Empty;
            strSql = "Select COUNT(a.OrderID) FROM Halle a " +
                                                "INNER JOIN Werk b ON b.ID=a.WerkID " +
                                                "WHERE a.WerkID =" + this.lo_Werk.Id + ";";
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
                                                "WHERE a.HalleID =" + this.lo_Werk.Id + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }
        ///-----------------------------------------------------------------------------------------------------
        ///                             sql Statements
        ///-----------------------------------------------------------------------------------------------------

        /// <summary>
        ///             Add sql - String
        /// </summary>
        public string sql_Add
        {
            get
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO Halle (WerkID, Bezeichnung, Beschreibung, OrderID) " +
                                                "VALUES (" +
                                                       " '" + halle.WerkId + "' " +
                                                       ", '" + halle.Bezeichnung + "' " +
                                                       ", '" + halle.Beschreibung + "' " +
                                                       ", " + halle.OrderId + " " +
                                                         ")";
                strSql = strSql + "Select @@IDENTITY as 'ID' ";
                return strSql;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_Get
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Halle WHERE Id =" + this.halle.Id + " ";
                return strSql;
            }
        }

        /// <summary>
        ///             GET List
        /// </summary>
        public string sql_GetList
        {
            get
            {
                string strSql = string.Empty;
                strSql = this.sql_Get_Main;
                return strSql;
            }
        }
        /// <summary>
        ///             GET_Main
        /// </summary>
        public string sql_Get_Main
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Halle ";
                return strSql;
            }
        }

        /// <summary>
        ///             DELETE sql - String
        /// </summary>
        public string sql_Delete
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                    "LagerOrt=0 " +
                                    ", LOTable='' " +
                                    "WHERE ID IN (" +
                                                    //Alle Artikel der Halle
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1 AND a.LagerOrt=" + halle.Id + " AND a.LOTable='Halle' " +
                                                    " UNION " +
                                                    //Alle Artikel der Reihe
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Reihe' " +
                                                                        "AND a.LagerOrt IN (Select x.ID FROM Reihe x WHERE x.HalleID=" + halle.Id + ") " +
                                                    " UNION " +
                                                    //Alle Artikel der Ebenen
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Ebene' " +
                                                                        "AND a.LagerOrt IN (" +
                                                                                            "Select e.ID FROM Ebene e " +
                                                                                                          "INNER JOIN Reihe r ON e.ReiheID=r.ID " +
                                                                                                          "WHERE r.HalleID=" + halle.Id + " " +
                                                                                           ")" +
                                                    " UNION " +
                                                    //Alle Artikel Platz
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Platz' " +
                                                                        "AND a.LagerOrt IN (" +
                                                                                            "Select p.ID FROM Platz p " +
                                                                                                            "INNER JOIN Ebene e ON p.EbeneID=e.ID " +
                                                                                                            "INNER JOIN Reihe r ON r.ID=e.ReiheID " +
                                                                                                            "WHERE r.HalleID=" + halle.Id + " " +
                                                                                           ") " +
                                              ") ";

                strSql = strSql + "Delete FROM Halle WHERE ID=" + halle.Id + " ;";
                return strSql;
            }
        }
        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Update
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Update Halle SET" +
                                        " WerkID =" + halle.WerkId + " " +
                                        ", Bezeichnung ='" + halle.Bezeichnung + "'" +
                                        ", Beschreibung = '" + halle.Beschreibung + "' " +
                                        ", OrderID = " + halle.OrderId + " " +
                                        " WHERE ID=" + halle.Id + " ;";
                return strSql;
            }
        }


    }
}

