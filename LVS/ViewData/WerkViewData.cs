using DocumentFormat.OpenXml.Office2010.Excel;
using LVS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class WerkViewData
    {
        public Globals._GL_USER _GL_User;
        public Werk werk = new Werk();
        public List<Werk> ListWerk = new List<Werk>();

        public Halle halle = new Halle();
       

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


        public WerkViewData()
        {
            InitCls();
            GetWerkList();
            if (ListWerk.Count > 0)
            {
                werk = ListWerk[0];
                
            }
        }
        public WerkViewData(int myWerkId, int myUserId) : this()
        {
            this.BenutzerID = myUserId;
            werk.Id = myWerkId;
            if (ExistWerkByID())
            {
                Fill();
                halle.WerkId = werk.Id;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            ListWerk = new List<Werk>();

        }
        ///<summary>Werk / GetHallenDatenForDataTable</summary>
        ///<remarks>Ermittel die Daten anhand der WerkID.</remarks>
        public void GetWerkList()
        {
            ListWerk = new List<Werk>();
            string strSql = string.Empty;
            strSql =sql_Get_Main  +" Order By OrderID;";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ReiheGutAssignment");
            foreach (DataRow dr in dt.Rows)
            {
                SetValue(dr);
                if(!ListWerk.Contains(werk))
                {
                    ListWerk.Add(werk);
                }
            }
            //dtWerk = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Werk");
        }
        ///<summary>clsWerk / ExistWerk</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistWerkByID()
        {
            if (werk.Id > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Werk WHERE ID=" + werk.Id + " ;";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
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
            if (ExistWerkByID())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = sql_Get_Main + " WHERE Id = " + werk.Id;

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Werk");
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
            werk = new Werk();

            int iTmp = 0;
            int.TryParse(row["Id"].ToString(), out iTmp);
            werk.Id = iTmp;
            werk.Bezeichnung = row["Bezeichnung"].ToString();
            werk.Beschreibung = row["Beschreibung"].ToString();

            iTmp = 0;
            int.TryParse(row["OrderID"].ToString(), out iTmp);
            werk.OrderId = iTmp;
            werk.ExLagerOrt = (bool)row["exLagerOrt"];
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
            werk.Id = iTmp;

            //Add Logbucheintrag Eintrag
            string strInfo = "Stammdaten Werk erstellt: ID [" + werk.Id.ToString() + "] / Bezeichnung: " + werk.Bezeichnung;
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), strInfo);
            UpdateOrderID(werk.Id, werk.OrderId);
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        { 
            if (ExistWerkByID())
            {
                string strSql = string.Empty;
                strSql = sql_Delete;

                if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "WerkDelete", this._GL_User.User_ID))
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Stammdaten Werk gelöscht: ID: " + werk.Id.ToString() + " / Bezeichnung: " + werk.Bezeichnung;
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
                }
            }
        }
        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            if (ExistWerkByID())
            {
                string strSql = string.Empty;
                strSql = sql_Update;
                bool bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
                // Logbucheintrag Eintrag
                string strInfo = "Stammdaten Werk geändert: ID: " + werk.Id.ToString() + " / Bezeichnung: " + werk.Bezeichnung;
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
                strSql = "INSERT INTO Werk (Bezeichnung, Beschreibung, OrderID, exLagerOrt) " +
                                                "VALUES ('" + werk.Bezeichnung + "' " +
                                                       ", '" + werk.Beschreibung + "' " +
                                                       ", " + werk.OrderId + " " +
                                                       ", '" + werk.ExLagerOrt + "' " +
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
                strSql = "Select * FROM Reihe WHERE Id =" + this.werk.Id + " ";
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
                strSql = "Select * FROM Werk ";
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
                                                    //Alle Artikel Werk
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1 AND a.LagerOrt=" + werk.Id + " AND a.LOTable='Werk' " +
                                                    " UNION " +
                                                    //Alle Artikel der Halle
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Halle' " +
                                                                        "AND a.LagerOrt IN (Select x.ID FROM Halle x WHERE x.WerkID=" + werk.Id + ") " +

                                                    " UNION " +
                                                    //Alle Artikel der Reihe
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Reihe' " +
                                                                        "AND a.LagerOrt IN (" +
                                                                                            "Select r.ID FROM Reihe r " +
                                                                                                          "INNER JOIN Halle h ON h.ID=r.HalleID " +
                                                                                                          "WHERE h.WerkID=" + werk.Id + " " +
                                                                                           ")" +
                                                    " UNION " +
                                                    //Alle Artikel der Ebenen
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Ebene' " +
                                                                        "AND a.LagerOrt IN (" +
                                                                                            "Select e.ID FROM Ebene e " +
                                                                                                          "INNER JOIN Reihe r ON r.ID=e.ReiheID " +
                                                                                                          "INNER JOIN Halle h ON h.ID=r.HalleID " +
                                                                                                          "WHERE h.WerkID=" + werk.Id + " " +
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
                                                                                                            "WHERE h.WerkID=" + werk.Id + " " +
                                                                                           ") " +
                                                   ") ";
                strSql = strSql + "Delete FROM Werk WHERE ID=" + werk.Id + " ;";
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
                strSql = "Update Werk SET" +
                                        " Bezeichnung ='" + werk.Bezeichnung + "'" +
                                        ", Beschreibung = '" + werk.Beschreibung + "' " +
                                        ", OrderID = " + werk.OrderId + " " +
                                        ", exLagerOrt = '" + werk.ExLagerOrt + "' " +
                                        " WHERE ID='" + werk.Id + "'";
                return strSql;
            }
        }


    }
}

