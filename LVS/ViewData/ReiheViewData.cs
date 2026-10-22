using LVS.Models;
using System;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class ReiheViewData
    {
        public Globals._GL_USER _GL_User;
        //public ReiheGutAssignment ReiheGutAssignment;
        //public Dictionary<int, ReiheGutAssignment> DictReiheGutAssignment;
        public DataTable dtReihe = new DataTable("Reihen");
        public ReiheGutAssignmentViewData rgAssignment = new ReiheGutAssignmentViewData();
        public Reihe Reihe = new Reihe();


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


        public ReiheViewData()
        {
            InitCls();
        }
        public ReiheViewData(int myReiheId) : this()
        {
            Reihe.Id = myReiheId;
            if (Reihe.Id > 0)
            {
                Fill();
            }
        }
 
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            Reihe = new Reihe();
            rgAssignment = new ReiheGutAssignmentViewData();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public bool Fill()
        {
            if (Reihe.Id>0)
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = sql_Get_Main + " WHERE Id = " + Reihe.Id;

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ReiheGutAssignment");
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
            Reihe = new Reihe();

            int iTmp = 0;
            int.TryParse(row["Id"].ToString(), out iTmp);
            Reihe.Id = iTmp;

            iTmp = 0;
            int.TryParse(row["HalleID"].ToString(), out iTmp);
            Reihe.HalleId = iTmp;
            Reihe.Bezeichnung = row["Bezeichnung"].ToString();
            Reihe.Beschreibung = row["Beschreibung"].ToString();

            iTmp = 0;
            int.TryParse(row["OrderID"].ToString(), out iTmp);
            Reihe.OrderId = iTmp;

            decimal dTmp = 0;
            decimal.TryParse(row["DickeVon"].ToString(), out dTmp);
            Reihe.DickeVon = dTmp;

            dTmp = 0;
            decimal.TryParse(row["DickeBis"].ToString(), out dTmp);
            Reihe.DickeBis = dTmp;

            dTmp = 0;
            decimal.TryParse(row["BreiteVon"].ToString(), out dTmp);
            Reihe.BreiteVon = dTmp;

            dTmp = 0;
            decimal.TryParse(row["BreiteBis"].ToString(), out dTmp);
            Reihe.BreiteBis = dTmp;

            dTmp = 0;
            decimal.TryParse(row["LaengeVon"].ToString(), out dTmp);
            Reihe.LaengeVon = dTmp;

            dTmp = 0;
            decimal.TryParse(row["LaengeBis"].ToString(), out dTmp);
            Reihe.LaengeBis = dTmp;

            dTmp = 0;
            decimal.TryParse(row["HoeheVon"].ToString(), out dTmp);
            Reihe.HoeheVon = dTmp;

            dTmp = 0;
            decimal.TryParse(row["HoeheBis"].ToString(), out dTmp);
            Reihe.HoeheBis = dTmp;

            dTmp = 0;
            decimal.TryParse(row["BruttoVon"].ToString(), out dTmp);
            Reihe.BruttoVon = dTmp;

            dTmp = 0;
            decimal.TryParse(row["BruttoBis"].ToString(), out dTmp);
            Reihe.BruttoBis = dTmp;

            iTmp = 0;
            int.TryParse(row["Anzahl"].ToString(), out iTmp);
            Reihe.Anzahl = iTmp;

            ReiheGutAssignmentViewData rgaVD = new ReiheGutAssignmentViewData(0,Reihe.Id);
            Reihe.ReiheGutAssignmentsList = new System.Collections.Generic.List<ReiheGutAssignment>(rgaVD.ListReiheGutAssignment);
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
            Reihe.Id = iTmp;

            //Add Logbucheintrag Eintrag
            string strInfo = "Stammdaten Reihe erstellt: ID [" + Reihe.Id.ToString() + "] / Bezeichnung: " + Reihe.Bezeichnung;
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), strInfo);
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        {
            if (ExistReihe())
            {
                string strSql = string.Empty;
                strSql = sql_Delete;

                if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "ReiheDelete", this._GL_User.User_ID))
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Stammdaten Reihe gelöscht: ID: " + Reihe.Id.ToString() + " / Bezeichnung: " + Reihe.Bezeichnung;
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);

                    GetReihenDatenForDataTableExtra();
                }
            }
        }
        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            if (ExistReihe())
            {
                string strSql = string.Empty;
                strSql = sql_Update;
                bool bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
                // Logbucheintrag Eintrag
                string strInfo = "Stammdaten Reihe geändert: ID: " + Reihe.Id.ToString() + " / Bezeichnung: " + Reihe.Bezeichnung;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), strInfo);

                GetReihenDatenForDataTableExtra();
                return bReturn;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsReihe / ExistWerk</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        private bool ExistReihe()
        {
            if (Reihe.Id > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Reihe WHERE ID=" + Reihe.Id + " ;";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsReihe / GetHallenDatenForDataTable</summary>
        ///<remarks>Ermittel die Daten anhand der WerkID.</remarks>
        public void GetReihenDatenForDataTable()
        {
            dtReihe.Clear();
            string strSql = string.Empty;
            if (ExistReiheByHallenID())
            {

                strSql = "Select * FROM Reihe WHERE HalleID=" + Reihe.HalleId + " Order By OrderID;";

                dtReihe = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Reihen");
            }
            else
            {
                strSql = "Select * FROM Reihe Order By OrderID;";
                dtReihe = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
                dtReihe.Rows.Clear();
            }
        }
        ///<summary>clsReihe / GetHallenDatenForDataTable</summary>
        ///<remarks>Ermittel die Daten anhand der WerkID.</remarks>
        public void GetReihenDatenForDataTableExtra()
        {
            dtReihe.Clear();
            string strSql = string.Empty;
            if (ExistReiheByHallenID())
            {

                strSql = "Select *" +
                                ", (Select count(*) from Artikel a " +
                                                        "INNER JOIN LEingang e on e.id=a.LEingangTableID " +
                                                        "LEFT JOIN Lausgang c on a.LAusgangTableID = c.ID " +
                                                         "where " +
                                                            "Reihe=r.Bezeichnung " +
                                                            " and (c.Checked=0 or checked is null)) as Belegung " +
                                 ", (select ViewID from GueterArt where ID=GArtID) as Gut " +
                                 "FROM Reihe r " +
                                    "WHERE  HalleID=" + Reihe.HalleId + " Order By OrderID;";

                dtReihe = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Reihen");
            }
            else
            {
                strSql = "Select * FROM Reihe Order By OrderID;";
                dtReihe = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
                dtReihe.Rows.Clear();
            }
        }
        ///<summary>clsReihe / ExistWerk</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistReiheByHallenID()
        {
            if (Reihe.HalleId > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Reihe WHERE HalleID=" + Reihe.HalleId + " ;";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsReihe / ExistReiheByBezeichnung</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks> 
        ///<returns>Returns BOOL</returns>
        public bool ExistReiheByBezeichnung()
        {
            if (Reihe.Bezeichnung != string.Empty)
            {
                string strSql = string.Empty;
                strSql = "Select a.ID FROM Reihe a " +
                                            "INNER JOIN Halle b ON b.ID = a.HalleID " +
                                            "INNER JOIN Werk c ON c.ID=b.WerkID " +
                                            "WHERE a.HalleID=" + Reihe.HalleId + " AND  " +
                                                   "a.Bezeichnung='" + Reihe.Bezeichnung + "' ;";
                bool bResult = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
                return bResult;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsReihe / GetMaxOrderID</summary>
        ///<remarks>Ermittelt die max. OrderID.</remarks>
        ///<returns>Returns Int32</returns>
        private Int32 GetMaxOrderID()
        {
            string strSql = string.Empty;
            strSql = "Select Count(OrderID) FROM Reihe WHERE HalleID=" + Reihe.HalleId + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }
        ///<summary>clsWerk / GetMaxOrderIDHalle</summary>
        ///<remarks>Ermittelt die max. OrderID der Halle für das einzelne Werk.</remarks>
        ///<returns>Returns Int32</returns>
        private Int32 GetMaxOrderIDEbene()
        {
            string strSql = string.Empty;
            strSql = "Select COUNT(a.OrderID) FROM Ebene a " +
                                                //"INNER JOIN Halle c ON c.ID= d.HalleID " +
                                                //"INNER JOIN Werk b ON b.ID=c.WerkID " +
                                                //"INNER JOIN Reihe d ON d.ID=a.ReiheID "+
                                                "WHERE a.ReiheID =" + Reihe.Id + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }
        ///<summary>clsReihe / UpdateOrderID</summary>
        ///<remarks></remarks>
        public void UpdateOrderID(Decimal myID, Int32 myOrderID)
        {
            if (ExistReiheByHallenID())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Reihe WHERE HalleID=" + Reihe.HalleId + " Order BY OrderID ;";

                string strUpSQL = string.Empty;
                Int32 iCount = 0;
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
                if (dt.Rows.Count > 0)
                {
                    if (myID > 0)
                    {
                        strUpSQL = strUpSQL + "Update Reihe SET OrderID=" + myOrderID + " WHERE ID=" + myID + "; ";
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
                            strUpSQL = strUpSQL + "Update Reihe SET OrderID=" + iCount + " WHERE ID=" + decTmp + "; ";
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
                 string strSql = "INSERT INTO Reihe (HalleID, Bezeichnung, Beschreibung, OrderID,DickeVon,DickeBis,BreiteVon,BreiteBis,HoeheVon,HoeheBis,LaengeVon,LaengeBis,BruttoVon,BruttoBis,Anzahl,GArtID) " +
                                            "VALUES (" +
                                                   Reihe.HalleId + " " +
                                                   ", '" + Reihe.Bezeichnung + "' " +
                                                   ", '" + Reihe.Beschreibung + "' " +
                                                   ", " + Reihe.OrderId + " " +
                                                   ", " + Reihe.DickeVon.ToString().Replace(',', '.') + " " +
                                                   ", " + Reihe.DickeBis.ToString().Replace(',', '.') + " " +
                                                   ", " + Reihe.BreiteVon.ToString().Replace(',', '.') + " " +
                                                   ", " + Reihe.BreiteBis.ToString().Replace(',', '.') + " " +
                                                   ", " + Reihe.HoeheVon.ToString().Replace(',', '.') + " " +
                                                   ", " + Reihe.HoeheBis.ToString().Replace(',', '.') + " " +
                                                   ", " + Reihe.LaengeVon.ToString().Replace(',', '.') + " " +
                                                   ", " + Reihe.LaengeBis.ToString().Replace(',', '.') + " " +
                                                   ", " + Reihe.BruttoVon.ToString().Replace(',', '.') + " " +
                                                   ", " + Reihe.BruttoBis.ToString().Replace(',', '.') + " " +
                                                   ", " + Reihe.Anzahl + " " +
                                                   ", 0 " + //+ Reihe.GArt.ID + " " +
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
                strSql = "Select * FROM Reihe WHERE Id =" + this.Reihe.Id + " ";
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
                strSql = "Select * FROM Reihe ";
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
                                        //Alle Artikel der Reihe
                                        "Select a.ID FROM Artikel a " +
                                                            "WHERE BKZ=1 AND a.LagerOrt=" + Reihe.Id + " AND a.LOTable='Reihe' " +
                                        " UNION " +
                                        //Alle Artikel der Ebenen
                                        "Select a.ID FROM Artikel a " +
                                                            "WHERE BKZ=1  AND a.LOTable='Ebene' " +
                                                            "AND a.LagerOrt IN (Select x.ID FROM Ebene x WHERE x.ReiheID=" + Reihe.Id + ") " +
                                        " UNION " +
                                        //Alle Artikel Platz
                                        "Select a.ID FROM Artikel a " +
                                                            "WHERE BKZ=1  AND a.LOTable='Platz' " +
                                                            "AND a.LagerOrt IN (" +
                                                                                "Select p.ID FROM Platz p INNER JOIN Ebene e ON p.EbeneID=e.ID WHERE e.ReiheID=" + Reihe.Id + " " +
                                                                               ") " +
                                                                               ") ";

                strSql = strSql + "Delete FROM Reihe WHERE ID=" + Reihe.Id + " ";
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
                strSql = "Update Reihe SET" +
                                        " HalleID =" + Reihe.HalleId + " " +
                                        ", Bezeichnung ='" + Reihe.Bezeichnung + "'" +
                                        ", Beschreibung = '" + Reihe.Beschreibung + "' " +
                                        ", OrderID = " + Reihe.OrderId + " " +
                                        ",DickeVon= '" + Reihe.DickeVon.ToString().Replace(',', '.') + "' " +
                                        ",DickeBis= '" + Reihe.DickeBis.ToString().Replace(',', '.') + "' " +
                                          ",BreiteVon= '" + Reihe.BreiteVon.ToString().Replace(',', '.') + "' " +
                                          ",BreiteBis= '" + Reihe.BreiteBis.ToString().Replace(',', '.') + "' " +
                                          ",HoeheVon= '" + Reihe.HoeheVon.ToString().Replace(',', '.') + "' " +
                                          ",HoeheBis= '" + Reihe.HoeheBis.ToString().Replace(',', '.') + "' " +
                                          ",LaengeVon= '" + Reihe.LaengeVon.ToString().Replace(',', '.') + "' " +
                                          ",LaengeBis= '" + Reihe.LaengeBis.ToString().Replace(',', '.') + "' " +
                                          ",BruttoVon= '" + Reihe.BruttoVon.ToString().Replace(',', '.') + "' " +
                                          ",BruttoBis= '" + Reihe.BruttoBis.ToString().Replace(',', '.') + "' " +
                                          ",Anzahl= '" + Reihe.Anzahl.ToString().Replace(',', '.') + "' " +
                                          //",GArtID= " + (Int32)GArt.ID + " " +
                                        " WHERE ID='" + Reihe.Id + "'";
                return strSql;
            }
        }


    }
}

