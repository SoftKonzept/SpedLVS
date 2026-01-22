using System;
using System.Data;

namespace LVS
{
    public class clsStyleSheetColumn
    {

        public Globals._GL_USER _GL_User;

        public DataTable dtCheckOut = new DataTable();
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

        public decimal ID { get; set; }
        public string Bezeichnung { get; set; }
        public string Typ { get; set; }
        public string FTable { get; set; }
        public decimal FTableID { get; set; }
        public string TableToFormat { get; set; }
        public string ColToFormat { get; set; }
        public Int32 Length { get; set; }
        public bool CutLength { get; set; }
        public string Beschreibung { get; set; }
        public DataTable dtStyleSheet { get; set; }

        /******************************************************************************
         *                  Methoden / Procedure
         * ***************************************************************************/
        ///<summary>clsStyleSheetColumn/ Add</summary>
        ///<remarks>Fügt einen neue Datensatz hinzu</remarks>
        public void Add()
        {
            string strSQL = "INSERT INTO StyleSheetColumn (" +
                                                    "Bezeichnung, Typ, FTable, FTableID, TableToFormat, ColToFormat, Length, " +
                                                    "CutLength, Beschreibung" +
                                                    ") " +
                                                     "VALUES (" +
                                                     "'" + Bezeichnung + "'" +
                                                     ", '" + Typ + "'" +
                                                     ", '" + FTable + "'" +
                                                     ", " + FTableID +
                                                     ", '" + TableToFormat + "'" +
                                                     ", '" + ColToFormat + "'" +
                                                     ", " + Length +
                                                     ", " + Convert.ToInt32(CutLength) +
                                                     ", '" + Beschreibung + "' " +
                                                     ")";
            strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
            }
        }
        ///<summary>clsStyleSheetColumn/ Update</summary>
        ///<remarks>Datensatz ändern</remarks>
        public void Update()
        {
            string strTmp = string.Empty;
            string strSql = "Update StyleSheetColumn SET " +
                                          "Bezeichnung ='" + Bezeichnung + "' " +
                                          ", Typ ='" + Typ + "' " +
                                          ", FTable ='" + FTable + "' " +
                                          ", FTableID =" + FTableID +
                                          ", TableToFormat ='" + TableToFormat + "' " +
                                          ", ColToFormat ='" + ColToFormat + "' " +
                                          ", Length =" + Length +
                                          ", CutLength=" + Convert.ToInt32(CutLength) +
                                          ", Beschreibung='" + Beschreibung + "' " +

                                          " WHERE ID=" + ID + ";";
            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
            }
        }
        ///<summary>clsStyleSheetColumn/ Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM StyleSheetColumn WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "StyleSheetColumn");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                this.Typ = dt.Rows[i]["Typ"].ToString();
                this.FTable = dt.Rows[i]["FTable"].ToString();
                this.FTableID = (decimal)dt.Rows[i]["FTableID"];
                this.TableToFormat = dt.Rows[i]["TableToFormat"].ToString();
                this.ColToFormat = dt.Rows[i]["ColToFormat"].ToString();
                this.Length = (Int32)dt.Rows[i]["Length"];
                this.CutLength = (bool)dt.Rows[i]["CutLength"];
                this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();

                //StyleSheetTable
                GetDataTableStyleSheet();
            }
        }
        ///<summary>clsStyleSheetColumn/ GetDataTableStyleSheet</summary>
        ///<remarks></remarks>
        public void GetDataTableStyleSheet()
        {
            string strSQL = "Select * FROM StyleSheetColumn " +
                                        "WHERE FTable='" + FTable + "' AND FTableID=" + FTableID + ";";

            dtStyleSheet = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "StyleSheetColumn");
        }
        ///<summary>clsStyleSheetColumn/ Delete</summary>
        ///<remarks>Löscht den Datensatz</remarks>
        public void Delete()
        {
            string strSQL = "DELETE FROM StyleSheetColumn WHERE ID=" + ID + ";";
            clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
        }
    }
}
