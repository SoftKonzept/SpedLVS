using LVS;
using System;
using System.Data;

namespace Sped4.Classes
{
    class clsUserList
    {
        internal clsSQLStatement sqlStatement = new clsSQLStatement();

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

        public DataTable dtNewList = new DataTable();
        internal clsUser User;
        internal clsUserListDaten UserListDaten;

        private decimal _ID;
        private string _Bezeichnung;
        private decimal _UserID;
        private DateTime _erstellt;
        private string _Action;
        private bool _IsPublic;
        private bool _IsFilter;

        private DataTable _dtUserListAvailable;
        private DataTable _dtUserFilterAvailable;
        public DataTable dtBestandAB = new DataTable("Anfangsbestand");
        public DataTable dtBestandLE = new DataTable("Lagereingang");
        public DataTable dtBestandLA = new DataTable("Lagerausgang");

        public decimal ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
            }
        }
        public string Bezeichnung
        {
            get { return _Bezeichnung; }
            set { _Bezeichnung = value; }
        }
        public decimal UserID
        {
            get { return _UserID; }
            set
            {
                _UserID = value;
                User = new clsUser();
                User._GL_User = this._GL_User;
                User.ID = this._UserID;
                User.Fill();
            }
        }
        public DateTime erstellt
        {
            get { return _erstellt; }
            set { _erstellt = value; }
        }
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
        public bool IsPublic
        {
            get { return _IsPublic; }
            set { _IsPublic = value; }
        }
        public bool IsFilter
        {
            get { return _IsFilter; }
            set { _IsFilter = value; }
        }
        public DataTable dtUserListAvailable
        {
            get
            {
                GetUserListAvailable();
                return _dtUserListAvailable;
            }
            set { _dtUserListAvailable = value; }
        }
        public DataTable dtUserFilterAvailable
        {
            get
            {
                GetUserFilterAvailable();
                return _dtUserFilterAvailable;
            }
            set { _dtUserFilterAvailable = value; }
        }
        /*******************************************************************************
         *                              Mehtoden
         * *****************************************************************************/
        ///<summary>clsUserList / InitSubClass</summary>
        ///<remarks></remarks>
        public void InitSubClass()
        {
            UserListDaten = new clsUserListDaten();
            UserListDaten._GL_User = this._GL_User;

            sqlStatement.InitClass(this._GL_User);
        }
        ///<summary>clsUserList / GetUserListAvailable</summary>
        ///<remarks>Ermittelt alle freiggegbenen und vom USer selbst angelegten Listen.</remarks>
        private void GetUserListAvailable()
        {
            string strSQL = string.Empty;
            strSQL = "Select DISTINCT a.* " +
                                "FROM UserList a " +
                                "INNER JOIN UserListDaten b ON b.UserListID=a.ID " +
                                "WHERE " +
                                    "a.Action='" + Action + "' AND " +
                                    "a.IsFilter=0 " +
                                    "AND (" +
                                            "a.UserID=" + BenutzerID + " OR " +
                                            "a.[public] =1" +
                                        ")";
            _dtUserListAvailable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Filter");
        }
        ///<summary>clsUserList / GetUserFilterAvailable</summary>
        ///<remarks>Ermittelt alle freiggegbenen und vom USer selbst angelegten Filter.</remarks>
        private void GetUserFilterAvailable()
        {
            string strSQL = string.Empty;
            strSQL = "Select DISTINCT a.* " +
                                "FROM UserList a " +
                                "INNER JOIN UserListDaten b ON b.UserListID=a.ID " +
                                "WHERE " +
                                    "a.Action='" + Action + "' AND " +
                                    "a.IsFilter=1 " +
                                    "AND (" +
                                            "a.UserID=" + BenutzerID + " OR " +
                                            "a.[public] =1" +
                                        ")";
            _dtUserFilterAvailable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Filter");
        }
        ///<summary>clsUserList / CreateSQLStatementStatistik</summary>
        ///<remarks></remarks>
        private void CreateSQLStatementStatistik()
        {

        }
        ///<summary>clsUserList / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            string strSQL = string.Empty;
            strSQL = "Select a.* " +
                                "FROM UserList a " +
                                "WHERE " +
                                    "a.ID=" + ID + " ;";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Filter/Liste");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["UserID"].ToString(), out decTmp);
                this.UserID = decTmp;
                DateTime dtTmp = DateTime.MinValue;
                DateTime.TryParse(dt.Rows[i]["erstellt"].ToString(), out dtTmp);
                this.erstellt = dtTmp;
                this.Action = dt.Rows[i]["Action"].ToString();
                this.IsPublic = (bool)dt.Rows[i]["public"];
                this.IsFilter = (bool)dt.Rows[i]["IsFilter"];
            }
        }
        ///<summary>clsUserList / GetStatistikData</summary>
        ///<remarks></remarks>
        public DataTable GetStatistikData(DataTable dtSQLFilter)
        {
            DataTable dtReturn = new DataTable();
            if (dtSQLFilter.Rows.Count > 0)
            {
                //SQL zusammenbauen

                //sqlFilter 
                string strSQLFilter = string.Empty;
                strSQLFilter = CreateSQLStatementFilter(dtSQLFilter);
                if (strSQLFilter != string.Empty)
                {
                    strSQLFilter = "WHERE " + strSQLFilter;
                }
                //sqlcolumns
                string strSQLCol = string.Empty;
                strSQLCol = CreateSQLStatementColumns();
                if (strSQLCol != string.Empty)
                {
                    strSQLCol = "Select " + strSQLCol;
                }

                string strSQLJoin = " From Artikel " +
                                "INNER JOIN LEingang ON LEingang.ID = Artikel.LEingangTableID " +
                                "INNER JOIN Gueterart ON Gueterart.ID= Artikel.GArtID " +
                                "LEFT JOIN LAusgang c ON LAusgang.ID = Artikel.LAusgangTableID ";


                string strSQL = string.Empty;
                strSQLFilter = strSQLCol + strSQLJoin + strSQLFilter;
                dtReturn = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Statistik");


            }
            return dtReturn;
        }
        ///<summary>clsUserList / CreateSQLStatementColumns</summary>
        ///<remarks></remarks>
        private string CreateSQLStatementColumns()
        {
            string SQLReturn = string.Empty;
            Int32 iRows = dtUserListAvailable.Rows.Count;
            for (Int32 i = 0; i <= iRows - 1; i++)
            {
                if (iRows == 1)
                {
                    SQLReturn = dtUserListAvailable.Rows[i]["Table"].ToString() + "." +
                                dtUserListAvailable.Rows[i]["Columns"].ToString() + " as " +
                                dtUserListAvailable.Rows[i]["ColViewName"].ToString();
                }
                else
                {
                    SQLReturn = SQLReturn +
                                dtUserListAvailable.Rows[i]["Table"].ToString() + "." +
                                dtUserListAvailable.Rows[i]["Columns"].ToString() + " as " +
                                dtUserListAvailable.Rows[i]["ColViewName"].ToString();
                    //Komma setzen zur Spaltentrennung
                    if (i < iRows)
                    {
                        SQLReturn = SQLReturn + ", ";
                    }
                }
            }
            return SQLReturn;
        }
        ///<summary>clsUserList / CreateSQLStatementFilter</summary>
        ///<remarks>setzt anhand der Filtertabelle das SQL-Statement für den Filterbereich (WHERE...) zusammen.</remarks>
        private string CreateSQLStatementFilter(DataTable myDtFilter)
        {
            string SQLReturn = string.Empty;
            Int32 iRows = myDtFilter.Rows.Count;
            for (Int32 i = 0; i <= iRows - 1; i++)
            {
                if (iRows == 1)
                {
                    SQLReturn = myDtFilter.Rows[i]["Table"].ToString() + "." +
                                myDtFilter.Rows[i]["Columns"].ToString() + " " +
                                myDtFilter.Rows[i]["Opterator"].ToString() + " " +
                                myDtFilter.Rows[i]["Filterwert"].ToString() + " ";

                }
                else
                {
                    SQLReturn = SQLReturn +
                                myDtFilter.Rows[i]["Table"].ToString() + "." +
                                myDtFilter.Rows[i]["Columns"].ToString() + " " +
                                myDtFilter.Rows[i]["Opterator"].ToString() + " " +
                                myDtFilter.Rows[i]["Filterwert"].ToString() + " ";
                    //Komma setzen zur Spaltentrennung
                    if (i < iRows)
                    {
                        SQLReturn = SQLReturn + " AND ";
                    }
                }
            }
            return SQLReturn;
        }


        ///<summary>clsUserList / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            if (dtNewList.Rows.Count > 0)
            {
                string strSQL = string.Empty;
                //Füllt strSQL für eine Transaction
                for (Int32 i = 0; i <= dtNewList.Rows.Count - 1; i++)
                {
                    /****
                    this.Bezeichnung = string.Empty;
                    this.Table = string.Empty;
                    this.Column = string.Empty;
                    this.ColViewName = string.Empty;
                    this.Aktion = string.Empty;

                    this.Bezeichnung = dtNewList.Rows[0]["Bezeichnung"].ToString();
                    this.Table = dtNewList.Rows[i]["Table"].ToString();
                    this.Column = dtNewList.Rows[i]["Column"].ToString();
                    this.ColViewName = dtNewList.Rows[i]["Spalte"].ToString();
                    this.Aktion = dtNewList.Rows[i]["Aktion"].ToString();

                    strSQL = strSQL + "INSERT INTO UserList (Bezeichnung, [Table], [Column], ColViewName, Aktion, UserID, [public]) " +
                                                        "VALUES ('" + Bezeichnung + "'" +
                                                                 ",'" + Table + "'" +
                                                                 ",'" + Column + "'" +
                                                                 ",'" + ColViewName + "'" +
                                                                 ",'" + Aktion + "'" +
                                                                 "," + BenutzerID +
                                                                 ",'" + Private +"'"+
                                                                 "); ";
                     * 
                     * ***/
                }
                if (strSQL != string.Empty)
                {
                    clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "NewUserList", BenutzerID);
                }
            }
        }
        ///<summary>clsUserList / Add</summary>
        ///<remarks></remarks>
        public void Update()
        {
            if (dtNewList.Rows.Count > 0)
            {
                string strSQL = string.Empty;
                string strIDs = string.Empty;

                //ermittelt die zu löschenden ID
                for (Int32 i = 0; i <= dtNewList.Rows.Count - 1; i++)
                {
                    this.ID = (decimal)dtNewList.Rows[i]["ID"];
                    if (ID > 0)
                    {
                        if (i == 0)
                        {
                            strIDs = ID.ToString();
                        }
                        else
                        {
                            strIDs = strIDs + ", " + ID.ToString();
                        }
                    }
                }
                DataTable dtOldID = GetUserListIDToDelete(strIDs);

                strIDs = string.Empty;
                for (Int32 j = 0; j <= dtOldID.Rows.Count - 1; j++)
                {
                    this.ID = (decimal)dtOldID.Rows[j]["ID"];
                    strSQL = strSQL + "Delete FROM UserList WHERE ID=" + ID + "; ";
                }
                //Füllt strSQL für eine Transaction
                for (Int32 i = 0; i <= dtNewList.Rows.Count - 1; i++)
                {
                    this.ID = 0;
                    this.Bezeichnung = string.Empty;
                    //this.Table = string.Empty;
                    //this.Column = string.Empty;
                    //this.ColViewName = string.Empty;
                    //this.Aktion = string.Empty;

                    //this.ID = (decimal)dtNewList.Rows[i]["ID"];

                    //this.Bezeichnung = dtNewList.Rows[i]["Bezeichnung"].ToString();
                    //this.Table = dtNewList.Rows[i]["Table"].ToString();
                    //this.Column = dtNewList.Rows[i]["Column"].ToString();
                    //this.ColViewName = dtNewList.Rows[i]["Spalte"].ToString();
                    //this.Aktion = dtNewList.Rows[i]["Aktion"].ToString();

                    /****
                    if (ID == 0)
                    {
                        //ID=0 Neuer Eintrag
                        //Neuer Datensatz wird eingetragen
                        strSQL = strSQL + "INSERT INTO UserList (Bezeichnung, [Table], [Column], ColViewName, Aktion, UserID, [public]) " +
                                                            "VALUES ('" + Bezeichnung + "'" +
                                                                        ",'" + Table + "'" +
                                                                        ",'" + Column + "'" +
                                                                        ",'" + ColViewName + "'" +
                                                                        ",'" + Aktion + "'" +
                                                                        "," + BenutzerID +
                                                                        ",'" + Private + "'" +
                                                                        "); ";
                    }
                    else
                    {
                        strSQL = strSQL + "Update UserList SET " +
                                                        "Bezeichnung='" + Bezeichnung + "'" +
                                                        ", [Table]='" + Table + "'" +
                                                        ", [Column]='" + Column + "'" +
                                                        ", ColViewName='" + ColViewName + "'" +
                                                        ", Aktion='" + Aktion + "'" +
                                                        ", UserID=" + BenutzerID +
                                                        ", [public]='" + Private + "'" +
                                                        " WHERE ID=" + ID + "; ";                    
                    }
                     * ****/
                }
                if (strSQL != string.Empty)
                {
                    clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "NewUserList", BenutzerID);
                }
            }
        }
        ///<summary>clsUserList / Delete</summary>
        ///<remarks>Löscht alle Datensätze der entsprechenden Liste</remarks>
        public void Delete()
        {
            if (dtNewList.Rows.Count > 0)
            {
                string strSQL = string.Empty;
                //Füllt strSQL für eine Transaction
                for (Int32 i = 0; i <= dtNewList.Rows.Count - 1; i++)
                {
                    this.ID = 0;
                    this.ID = (decimal)dtNewList.Rows[i]["ID"];

                    if (ID > 0)
                    {
                        strSQL = strSQL + "Delete UserList WHERE ID=" + ID + "; ";
                    }
                }
                if (strSQL != string.Empty)
                {
                    clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "DeleteUserList", BenutzerID);
                }
            }
        }
        ///<summary>clsUserList / GetUserListByUser</summary>
        ///<remarks>Ermittel die Listen des User und alle öffentlichen Listen</remarks>
        public DataTable GetUserListByUser()
        {
            string strSQL = string.Empty;
            strSQL = "Select DISTINCT Bezeichnung FROM UserList WHERE UserID=" + BenutzerID + " OR [public]=0;";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "UserList");
            return dt;
        }
        ///<summary>clsUserList / GetUserListID</summary>
        ///<remarks></remarks>
        private DataTable GetUserListIDToDelete(string strIDsNotIn)
        {
            string strSQL = string.Empty;
            strSQL = "Select ID FROM UserList WHERE Bezeichnung='" + Bezeichnung + "' AND ID NOT IN (" + strIDsNotIn + ");";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "UserList");
            return dt;
        }
        ///<summary>clsUserList / GetUserListByBezeichnung</summary>
        ///<remarks>Ermittel die Listen mit der entsprechenden Bezeichnung</remarks>
        public DataTable GetUserListByBezeichnung()
        {

            DataTable dt = new DataTable();
            /****
          string strSQL = string.Empty;
          strSQL = "Select ID, Bezeichnung, [Table], [Column], ColViewName as Spalte, Aktion, UserID, [public] "+
                                      "FROM UserList WHERE UserID=" + BenutzerID + " AND "+
                                                  "Bezeichnung ='"+Bezeichnung+"';";
          dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "UserList");
          if (dt.Rows.Count > 0)
          {
              this.Bezeichnung = dt.Rows[0]["Bezeichnung"].ToString();
              this.Private = (bool)dt.Rows[0]["public"];
              this.Aktion = dt.Rows[0]["Aktion"].ToString();
          }
           * ****/
            return dt;
        }










    }
}
