using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class VehicleViewData
    {
        public Vehicles Vehicle { get; set; }
        private int BenutzerID { get; set; } = 0;
        public List<Vehicles> ListVehicles { get; set; }

        public VehicleViewData()
        {
            BenutzerID = 1;
            InitCls();
        }
        public VehicleViewData(int myUserId) : this()
        {
            BenutzerID = myUserId;
        }

        public VehicleViewData(int myId, int myUserId) : this()
        {
            Vehicle.Id = myId;
            BenutzerID = myUserId;
            if (Vehicle.Id > 0)
            {
                Fill();
            }
        }

        private void InitCls()
        {
            Vehicle = new Vehicles();
        }
        public void Fill()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Vehicles", "Vehicle", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        private void SetValue(DataRow row)
        {
            Vehicle = new Vehicles();
            Int32 iTmp = 0;
            Int32.TryParse(row["ID"].ToString(), out iTmp);

            Vehicle.Id = iTmp;
            Vehicle.KFZ = row["KFZ"].ToString();
            iTmp = 0;
            Int32.TryParse(row["KIntern"].ToString(), out iTmp);
            Vehicle.KIntern = iTmp;
            Vehicle.Fabrikat = row["Fabrikat"].ToString();
            Vehicle.Bezeichnung = row["Bezeichnung"].ToString();
            Vehicle.FGNr = row["FGNr"].ToString();
            DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
            DateTime.TryParse(row["Tuev"].ToString(), out dtTmp);
            Vehicle.Tuev = dtTmp;
            dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
            DateTime.TryParse(row["SP"].ToString(), out dtTmp);
            Vehicle.SP = dtTmp;
            dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
            DateTime.TryParse(row["BJ"].ToString(), out dtTmp);
            Vehicle.BJ = dtTmp;
            dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
            DateTime.TryParse(row["seit"].ToString(), out dtTmp);
            Vehicle.seit = dtTmp;
            Vehicle.ZM = Convert.ToChar(row["ZM"]);
            Vehicle.Anhaenger = Convert.ToChar(row["Anhaenger"]);
            Vehicle.Plane = Convert.ToChar(row["Plane"]);
            Vehicle.Sattel = Convert.ToChar(row["Sattel"]);
            Vehicle.Coil = Convert.ToChar(row["Coil"]);
            iTmp = 0;
            Int32.TryParse(row["Leergewicht"].ToString(), out iTmp);
            Vehicle.Leergewicht = iTmp;
            iTmp = 0;
            Int32.TryParse(row["zlGG"].ToString(), out iTmp);
            Vehicle.zlGG = iTmp;
            decimal decTmp = 0;
            Decimal.TryParse(row["Innenhoehe"].ToString(), out decTmp);
            Vehicle.Innenhoehe = decTmp;
            iTmp = 0;
            Int32.TryParse(row["Stellplaetze"].ToString(), out iTmp);
            Vehicle.Stellplaetze = iTmp;

            if (row["Besonderheit"] is DBNull)
            {
                Vehicle.Besonderheit = string.Empty;
            }
            else
            {
                Vehicle.Besonderheit = (string)row["Besonderheit"];
            }
            dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
            DateTime.TryParse(row["bis"].ToString(), out dtTmp);
            Vehicle.Abmeldung = dtTmp;
            decTmp = 0;
            Decimal.TryParse(row["Laenge"].ToString(), out decTmp);
            Vehicle.Laenge = decTmp;
            if (row["Abgas"] is DBNull)
            {
                Vehicle.AbgasNorm = string.Empty;
            }
            else
            {
                Vehicle.AbgasNorm = row["Abgas"].ToString();
            }
            iTmp = 0;
            Int32.TryParse(row["Achsen"].ToString(), out iTmp);
            Vehicle.Achsen = iTmp;
            decTmp = 0;
            Decimal.TryParse(row["MandantenID"].ToString(), out decTmp);
            Vehicle.MandantenID = decTmp;
        }
        /// <summary>
        /// 
        /// </summary>
        public void GetVehicleList()
        {
            ListVehicles = new List<Vehicles>();
            string strSQL = sql_GetList;
            DataTable dt = new DataTable("Vehicle");
            dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "GetVehicle", "Vehicles", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    ListVehicles.Add(Vehicle);
                }
            }
        }
        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            string strSql = sql_Add;
            strSql = strSql + "Select @@IDENTITY as 'ID';";

            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Insert", BenutzerID);
            int.TryParse(strTmp, out int iTmp);
            Vehicle.Id = iTmp;
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        {
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
                string strSQL = string.Empty;
                strSQL = "INSERT INTO Fahrzeuge " +
                                                  "( KFZ, " +
                                                  "KIntern, " +
                                                  "Fabrikat, " +
                                                  "Bezeichnung, " +
                                                  "FGNr, " +
                                                  "Tuev, " +
                                                  "SP, " +
                                                  "BJ, " +
                                                  "seit, " +
                                                  "Laufleistung, " +
                                                  "ZM, " +
                                                  "Anhaenger, " +
                                                  "Plane, " +
                                                  "Sattel, " +
                                                  "Coil, " +
                                                  "Leergewicht, " +
                                                  "zlGG, " +
                                                  "Innenhoehe, " +
                                                  "Stellplaetze, " +
                                                  "Besonderheit, " +
                                                  "bis, " +
                                                  "Laenge," +
                                                  "Abgas, " +
                                                  "Achsen, " +
                                                  "MandantenID)" +

                                          "VALUES " +
                                                   "('" + Vehicle.KFZ + "','"
                                                   + Vehicle.KIntern + "','"
                                                   + Vehicle.Fabrikat + "','"
                                                   + Vehicle.Bezeichnung + "','"
                                                   + Vehicle.FGNr + "','"
                                                   + Vehicle.Tuev + "','"
                                                   + Vehicle.SP + "','"
                                                   + Vehicle.BJ + "','"
                                                   + Vehicle.seit + "','"
                                                   + Vehicle.Laufleistung + "','"
                                                   + Vehicle.ZM + "','"
                                                  + Vehicle.Anhaenger + "','"
                                                  + Vehicle.Plane + "','"
                                                  + Vehicle.Sattel + "','"
                                                  + Vehicle.Coil + "','"
                                                  + Vehicle.Leergewicht + "','"
                                                  + Vehicle.zlGG + "','"
                                                  + Vehicle.Innenhoehe.ToString().Replace(",", ".") + "','"
                                                  + Vehicle.Stellplaetze + "','"
                                                  + Vehicle.Besonderheit + "','"
                                                  + Vehicle.Abmeldung + "', '"
                                                  + Vehicle.Laenge.ToString().Replace(",", ".") + "', '"
                                                  + Vehicle.AbgasNorm + "', '"
                                                  + Vehicle.Achsen + "', '"
                                                  + Vehicle.MandantenID + "');";
                return strSQL;
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
                //strSql = "Select * FROM LAusgang WHERE ID=" + Ausgang.Id + ";";
                strSql = "Select a.*" +
                              ", (Select Matchcode FROM Mandanten WHERE ID=a.MandantenID) as Besitzer" +
                              " From Fahrzeuge a " +
                              "WHERE ID=" + Vehicle.Id + ";";
                return strSql;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_GetList
        {
            get
            {
                string strSql = string.Empty;
                //strSql = "Select * FROM LAusgang WHERE ID=" + Ausgang.Id + ";";
                strSql = "Select a.*" +
                              ", (Select Matchcode FROM Mandanten WHERE ID=a.MandantenID) as Besitzer" +
                              " From Fahrzeuge a " +
                              "WHERE bis>'" + DateTime.Now.ToString() + "';";
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
                //strSql = "Delete FROM LAusgang WHERE ID =" + Ausgang.Id; ;
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
                string strSQL = string.Empty;
                strSQL = "Update Fahrzeuge SET KFZ='" + Vehicle.KFZ + "'" +
                                               ", KIntern=" + Vehicle.KIntern +
                                               ", Fabrikat='" + Vehicle.Fabrikat + "'" +
                                               ", Bezeichnung='" + Vehicle.Bezeichnung + "'" +
                                               ", FGNr='" + Vehicle.FGNr + "'" +
                                               ", Tuev='" + Vehicle.Tuev + "'" +
                                               ", SP='" + Vehicle.SP + "'" +
                                               ", BJ='" + Vehicle.BJ + "'" +
                                               ", seit='" + Vehicle.seit + "'" +
                                               ", Laufleistung='" + Vehicle.Laufleistung + "'" +
                                               ", ZM='" + Vehicle.ZM + "'" +
                                               ", Anhaenger='" + Vehicle.Anhaenger + "'" +
                                               ", Plane='" + Vehicle.Plane + "'" +
                                               ", Sattel='" + Vehicle.Sattel + "'" +
                                               ", Coil='" + Vehicle.Coil + "'" +
                                               ", Leergewicht=" + Vehicle.Leergewicht +
                                               ", zlGG=" + Vehicle.zlGG +
                                               ", Innenhoehe='" + Vehicle.Innenhoehe.ToString().Replace(",", ".") + "'" +
                                               ", Stellplaetze=" + Vehicle.Stellplaetze +
                                               ", Besonderheit='" + Vehicle.Besonderheit + "'" +
                                               ", bis ='" + Vehicle.Abmeldung + "'" +
                                               ", Laenge ='" + Vehicle.Laenge.ToString().Replace(",", ".") + "'" +
                                               ", Abgas='" + Vehicle.AbgasNorm + "'" +
                                               ", Achsen =" + Vehicle.Achsen +
                                               ", MandantenID =" + Vehicle.MandantenID +
                                               " " +
                                               " WHERE ID=" + Vehicle.Id + ";";
                return strSQL;
            }
        }





    }
}

