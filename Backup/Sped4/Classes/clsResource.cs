using LVS;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Sped4.Classes
{
    class clsResource
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
        internal decimal decDef = 1.0M;
        public ArrayList AuflRecourceList = new ArrayList();
        public ArrayList FahrRecourceList = new ArrayList();
        public ArrayList VehiRecourceList = new ArrayList();

        private decimal _m_i_ID;
        private decimal _newRecourceID;
        private string _m_str_Name;
        private string _InfoText;

        private DateTime _m_dt_RecourceUnEnd;
        public DateTime m_dt_RecourceUnEnd
        {
            get
            {
                _m_dt_RecourceUnEnd = DateTime.MaxValue;
                return _m_dt_RecourceUnEnd;
            }
            set { _m_dt_RecourceUnEnd = value; }
        }
        private DateTime _m_dt_RecourceStart;
        public DateTime m_dt_RecourceStart
        {
            get
            {
                _m_dt_RecourceStart = DateTime.Now;
                return _m_dt_RecourceStart;
            }
            set { _m_dt_RecourceStart = value; }
        }
        public DateTime m_dt_RecourceEnd { get; set; }
        public decimal m_i_ID { get; set; }
        public decimal m_i_RecourceID { get; set; }
        public string m_str_KFZ { get; set; }
        public decimal m_i_PersonalID { get; set; }
        public DateTime m_dt_TimeFrom { get; set; }
        public DateTime m_dt_TimeTo { get; set; }
        public decimal m_i_VehicleID { get; set; }
        public decimal m_i_VehicleID_Truck { get; set; }
        public decimal m_i_VehicleID_Trailer { get; set; }
        public char m_ch_RecourceTyp { get; set; }

        public string m_str_Name
        {
            get
            {
                _m_str_Name = clsPersonal.GetNameByID(m_i_PersonalID);
                return _m_str_Name;
            }
            set { _m_str_Name = value; }
        }

        public string InfoText
        {
            get
            {
                _InfoText = GetRecourceInfoAuflieger();
                return _InfoText;
            }
            set { _InfoText = value; }
        }

        /************************************************************************************************************
         *                                      Methoden / Procedure
         ************************************************************************************************************/
        ///<summary>ctrCalRow/ LoadRecouceBy</summary>
        ///<remarks></remarks>
        public DataTable LoadRecouceByTruckID(decimal myZMID, string RecourceTyp, bool RecourceLoaded)
        {
            DataTable RecourceTable = new DataTable();
            RecourceTable.Clear();
            if (!RecourceLoaded)
            {
                string strSQL = "SELECT * " +
                                             "FROM " +
                                                  "VehicleRecource " +
                                             "WHERE (" +

                                                   //
                                                   "((CONVERT(varchar(10),[DateFrom], 104) <= '" + m_dt_TimeFrom.ToShortDateString() + "') AND (CONVERT(varchar(10),[DateTo], 104) >='" + m_dt_TimeTo.ToShortDateString() + "')) " +
                                                   "OR " +
                                                   //Resourcen, die über den gesamt Abfragezeitraum laufen
                                                   "((CONVERT(varchar(10),[DateFrom], 104) < '" + m_dt_TimeFrom.ToShortDateString() + "') AND (CONVERT(varchar(10),[DateTo], 104) > '" + m_dt_TimeTo.ToShortDateString() + "')) " +
                                                   "OR " +
                                                   //Resourcen, = oder innerhalb Abfragezeitraum laufen
                                                   "((CONVERT(varchar(10),[DateFrom], 104) >= '" + m_dt_TimeFrom.ToShortDateString() + "') AND (CONVERT(varchar(10),[DateTo], 104) <= '" + m_dt_TimeTo.ToShortDateString() + "')) " +
                                                   "OR " +
                                                   //Resourcenstartpunkt = TimeTo (die beginnende Resource wird angezeigt
                                                   "((CONVERT(varchar(10),[DateFrom], 104) = '" + m_dt_TimeTo.ToShortDateString() + "') AND (CONVERT(varchar(10),[DateTo], 104) > '" + m_dt_TimeTo.ToShortDateString() + "')) " +
                                                   "OR " +
                                                   //Resourcenendpunkt = TimeFrom (die endende Resource wird angezeigt
                                                   "((CONVERT(varchar(10),[DateFrom], 104) < '" + m_dt_TimeFrom.ToShortDateString() + "') AND (CONVERT(varchar(10),[DateTo], 104) = '" + m_dt_TimeTo.ToShortDateString() + "')) " +
                                                   "OR " +
                                                   //Resourcenstart- und -endpunkt liegen innerhalb des Abfragezeitraums (Abfragezeitraum über mehrere Tage)
                                                   "(" +
                                                       //Resourcenstartzeitpunkt ist > DateFrom und < DateTo  
                                                       "(CONVERT(varchar(10),[DateFrom], 104) > '" + m_dt_TimeFrom.ToShortDateString() + "') " +
                                                       "AND " +
                                                       "(CONVERT(varchar(10),[DateFrom], 104) < '" + m_dt_TimeTo.ToShortDateString() + "') " +
                                                       //Resourcenendzeitpunkt ist > DateFrom and < DateTo
                                                       "AND " +
                                                       "(CONVERT(varchar(10),[DateTo], 104) > '" + m_dt_TimeFrom.ToShortDateString() + "') " +
                                                       "AND " +
                                                       "(CONVERT(varchar(10),[DateTo], 104) < '" + m_dt_TimeTo.ToShortDateString() + "') " +

                                                   ")" +
                                                    "OR " +
                                                   //Resourcenstartpunkt liegen innerhalb des Abfragezeitraums (Abfragezeitraum über mehrere Tage)
                                                   "(" +
                                                       //Resourcenstartzeitpunkt ist > DateFrom und > DateTo  
                                                       "(CONVERT(varchar(10),[DateFrom], 104) >= '" + m_dt_TimeFrom.ToShortDateString() + "') " +
                                                       "AND " +
                                                       "(CONVERT(varchar(10),[DateTo], 104) > '" + m_dt_TimeTo.ToShortDateString() + "') " +
                                                   ")" +
                                                   "OR " +
                                                   //Resourcenendzeit liegen innerhalb des Abfragezeitraums (Abfragezeitraum über mehrere Tage)
                                                   "(" +
                                                       //Resourcenstartzeitpunkt ist < DateFrom und < DateTo  
                                                       "(CONVERT(varchar(10),[DateFrom], 104) < '" + m_dt_TimeFrom.ToShortDateString() + "') " +
                                                       "AND " +
                                                       "(CONVERT(varchar(10),[DateTo], 104) <= '" + m_dt_TimeTo.ToShortDateString() + "') " +
                                                   ")" +

                                                   ") AND (RecourceTyp='" + m_ch_RecourceTyp + "') " + // OR RecourceTyp='Z') "+
                                                   " AND (RecourceID IN (Select RecourceID FROM VehicleRecource WHERE RecourceTyp='Z' AND VehicleID=" + myZMID + ")) " +

                                                   " ORDER BY DateFrom";
                RecourceTable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Ressourcen");

            }
            return RecourceTable;
        }
        ///<summary>ctrCalRow/ GetNextResscourceStartDateTime</summary>
        ///<remarks></remarks>
        public static DateTime GetNextResscourceStartDateTime(Globals._GL_USER myGLUser, DateTime dtResEnd, decimal myRecourceID, bool bUseDirektVehicleID)
        {
            string strSQL = string.Empty;
            if (bUseDirektVehicleID)
            {
                strSQL = "Select TOP(1) DateFrom FROM VehicleRecource " +
                                                    "WHERE VehicleID='" + myRecourceID + "' " +
                                                    "AND DateFrom >='" + dtResEnd + "' Order By DateFrom";
            }
            else
            {
                strSQL = "Select TOP(1) DateFrom FROM VehicleRecource " +
                                                   "WHERE VehicleID=(Select VehicleID FROM VehicleRecource WHERE RecourceID='" + myRecourceID + "' AND RecourceTyp='Z') " +
                                                   "AND DateFrom >='" + dtResEnd + "'  Order By DateFrom";
            }
            DateTime reDateTime = DateTime.MaxValue;
            if (strSQL != string.Empty)
            {
                string strDateTime = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
                if (strDateTime != string.Empty)
                {
                    DateTime.TryParse(strDateTime, out reDateTime);
                }
            }
            return reDateTime;
        }
        //
        //
        public static DateTime GetFormerResscourceEndtDateTime(Globals._GL_USER myGLUser, DateTime dtResStart, decimal myRecourceID, bool bUseDirektVehicleID)
        {
            string strSQL = string.Empty;
            if (bUseDirektVehicleID)
            {
                strSQL = "Select TOP(1) DateTo FROM VehicleRecource " +
                                                    "WHERE VehicleID='" + myRecourceID + "' " +
                                                    "AND DateTo <='" + dtResStart + "' Order By DateTo DESC ";
            }
            else
            {
                strSQL = "Select TOP(1) DateTo FROM VehicleRecource " +
                                                    "WHERE VehicleID=(Select VehicleID FROM VehicleRecource WHERE RecourceID='" + myRecourceID + "' AND RecourceTyp='Z') " +
                                                    "AND DateTo <='" + dtResStart + "' Order By DateTo DESC ";
            }
            DateTime reDateTime = DateTime.MinValue;
            if (strSQL != string.Empty)
            {
                string strDateTime = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
                if (strDateTime != string.Empty)
                {
                    DateTime.TryParse(strDateTime, out reDateTime);
                }
            }
            return reDateTime;
        }
        //
        //-------------  ZM ID über die Recource ID --------------------
        //
        public decimal GetTruckIDbyRecourceID()
        {
            decimal VehicleID = 0;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT VehicleID FROM VehicleRecource " +
                                           "WHERE RecourceID='" + m_i_RecourceID + "' AND RecourceTyp='Z'";

            Globals.SQLcon.Open();
            if (Command.ExecuteScalar() == null)
            {
                VehicleID = 0;
            }
            else
            {
                VehicleID = (decimal)Command.ExecuteScalar();
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return VehicleID;
        }
        //
        //-------------- Auflieger ID über Recource   --------------------
        //
        public static decimal GetVehicleIDFromRecource(decimal RecourceID, string Typ)
        {

            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT Distinct VehicleID FROM VehicleRecource " +
                                 "WHERE (RecourceID= " + RecourceID + ") AND (RecourceTyp='" + Typ + "')";

            Globals.SQLcon.Open();
            decimal VehicleID = (decimal)Command.ExecuteScalar();
            Command.Dispose();
            Globals.SQLcon.Close();
            return VehicleID;
        }
        //
        // --------  GET Recource ID -------------------------------
        //
        public static decimal GetRecourceIDbyVehicle(DateTime TimeFrom, DateTime TimeTo, decimal VehicleID)
        {
            decimal RecID = 0;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT RecourceID FROM VehicleRecource " +
                                           "WHERE " +
                                              "(((DateFrom >= '" + TimeFrom + "') AND ((DateTo >= '" + TimeFrom + "') OR (DateTo<='" + TimeTo + "'))) " +
                                                  "OR " +
                                                   "(((DateFrom >='" + TimeFrom + "') OR (DateFrom<='" + TimeTo + "')) AND (DateTo > '" + TimeTo + "')) " +
                                                  "OR " +
                                                   "((DateFrom >= '" + TimeFrom + "') AND (DateTo <= '" + TimeTo + "'))) " + "AND " +

                                                "(VehicleID = " + VehicleID + ") " +
                                             "ORDER BY DateFrom";


            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();

            if (obj != null)
            {
                RecID = (decimal)obj;
            }
            else
            {
                RecID = 0;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return RecID;
        }
        //
        public static decimal GetRecourceIDbyPersonal(DateTime TimeFrom, DateTime TimeTo, decimal PersonalID)
        {
            decimal RecID = 0;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;

            Command.CommandText = "SELECT RecourceID FROM VehicleRecource " +
                                           "WHERE " +
                                              "(((DateFrom >= '" + TimeFrom + "') AND ((DateTo >= '" + TimeFrom + "') OR (DateTo<='" + TimeTo + "'))) " +
                                                  "OR " +
                                                   "(((DateFrom >='" + TimeFrom + "') OR (DateFrom<='" + TimeTo + "')) AND (DateTo > '" + TimeTo + "')) " +
                                                  "OR " +
                                                   "((DateFrom >= '" + TimeFrom + "') AND (DateTo <= '" + TimeTo + "'))) " +
                                             "AND " +
                                                "(PersonalID = " + PersonalID + ") " +
                                             "ORDER BY DateFrom";

            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();

            if (obj != null)
            {
                RecID = (decimal)obj;
            }
            else
            {
                RecID = 0;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return RecID;
        }

        //wird zurzeit nicht verwendet
        //
        //----------- Zugmaschine by Ressource
        //
        public static DataSet GetTrailerAsRecourceByRecourceID(DateTime TimeFrom, DateTime TimeTo, decimal RecourceID)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT VehicleID FROM VehicleRecource " +
                                           "WHERE " +
                                              "(((DateFrom >= '" + TimeFrom + "') AND ((DateTo >= '" + TimeFrom + "') OR (DateTo<='" + TimeTo + "'))) " +
                                              "OR " +
                                              "(((DateFrom >='" + TimeFrom + "') OR (DateFrom<='" + TimeTo + "')) AND (DateTo > '" + TimeTo + "')) " +
                                              "OR " +
                                              "((DateFrom >= '" + TimeFrom + "') AND (DateTo <= '" + TimeTo + "'))) " +
                                             "AND " +
                                                "(RecourceID = " + RecourceID + ") " +
                                                "ORDER BY DateFrom";
            Globals.SQLcon.Open();

            ada.Fill(ds);
            Command.Dispose();
            Globals.SQLcon.Close();
            return ds;
        }
        //
        //-------- Fill Data für die Disposition ---------------
        //liest Daten aus Table Fahrzeuge für die Dispo  
        public void FillData()
        {
            DataTable FahrzTable = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = ("SELECT ID, KFZ FROM FAHRZEUGE WHERE ID = " + _m_i_ID);
            ada.Fill(FahrzTable);
            if (FahrzTable.Rows.Count > 0)
            {
                m_i_ID = (decimal)FahrzTable.Rows[0]["ID"];
                m_str_KFZ = (string)FahrzTable.Rows[0]["KFZ"];
            }
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        //
        //--------------------------------  Insert neue Recourcen  ------------------
        //
        public void Insert_Truck()
        {
            //neue Recource ID wird nur hier ausgelesen - sonster immer 
            decimal decTmp = GetMaxRecourceID();
            _newRecourceID = decTmp + decDef;
            m_i_RecourceID = _newRecourceID;
            try
            {
                SqlCommand InsertCommand = new SqlCommand();
                InsertCommand.Connection = Globals.SQLcon.Connection;
                InsertCommand.CommandText = "INSERT INTO VehicleRecource " +
                                                         "(RecourceID, RecourceTyp, DateFrom, DateTo, VehicleID) " +
                                                     "VALUES (" + m_i_RecourceID.ToString().Replace(",", ".") + ", " +
                                                                     "'Z' , " +
                                                                   "'" + m_dt_TimeFrom + "', " +
                                                                   "'" + m_dt_TimeTo + "', " +
                                                                    m_i_VehicleID_Truck.ToString().Replace(",", ".") + ")";

                Globals.SQLcon.Open();
                InsertCommand.ExecuteNonQuery();
                InsertCommand.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        //
        //------------- Einfügen Auflieger / Trailer --------------
        //
        public void Insert_Trailer()
        {
            try
            {
                SqlCommand InsertCommand = new SqlCommand();
                InsertCommand.Connection = Globals.SQLcon.Connection;
                InsertCommand.CommandText = "INSERT INTO VehicleRecource " +
                                                         "(RecourceID, RecourceTyp, DateFrom, DateTo, VehicleID) " +
                                                     "VALUES (" + m_i_RecourceID.ToString().Replace(",", ".") + ", " +
                                                                    "'" + m_ch_RecourceTyp + "', " +
                                                                    "'" + m_dt_TimeFrom + "', " +
                                                                    "'" + m_dt_TimeTo + "', " +
                                                                    m_i_VehicleID_Trailer.ToString().Replace(",", ".") + ")";

                Globals.SQLcon.Open();
                InsertCommand.ExecuteNonQuery();
                InsertCommand.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        //
        //------------- Einfügen Fahrer  -------------------------
        //
        public void Insert_Fahrer()
        {
            try
            {
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = "INSERT INTO VehicleRecource " +
                                                         "(RecourceID, RecourceTyp, DateFrom, DateTo, PersonalID) " +
                                                     "VALUES (" + m_i_RecourceID.ToString().Replace(",", ".") + ", " +
                                                                    "'" + m_ch_RecourceTyp + "', " +
                                                                    "'" + m_dt_TimeFrom + "', " +
                                                                    "'" + m_dt_TimeTo + "', " +
                                                                    m_i_PersonalID.ToString().Replace(",", ".") + ")";

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        //
        //
        //
        private decimal GetMaxRecourceID()
        {
            decimal reVal = 0.0M;
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Select MAX(RecourceID) FROM VehicleRecource ";
            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();

            if ((obj == null) | (obj is DBNull))
            {
                //reVal = (decimal)obj;
                reVal = 1.0M;
            }
            else
            {
                //reVal = 1.0M;
                reVal = (decimal)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
            return reVal;
        }

        //
        //------------------------------ UPDATE der Recource DB  ---------------
        //  
        public void UpdateTrailer()
        {
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Update VehicleRecource SET VehicleID='" + m_i_VehicleID_Trailer.ToString().Replace(",", ".") + "' " +
                                                                           "WHERE RecourceID='" + m_i_RecourceID + "' AND RecourceTyp='A'";

            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close(); ;
        }
        //
        public void UpdateTrailerStart()
        {
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Update VehicleRecource SET DateFrom='" + m_dt_TimeFrom + "' " +
                                                              ",DateTo='" + m_dt_TimeTo + "' " +
                                                                           "WHERE RecourceID='" + m_i_RecourceID + "' AND VehicleID='" + m_i_VehicleID_Trailer + "'";
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        //
        public void UpdateTrailerEnd()
        {
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Update VehicleRecource SET DateFrom='" + m_dt_TimeFrom + "' " +
                                                              ",DateTo='" + m_dt_TimeTo + "' " +
                                                                           "WHERE RecourceID='" + m_i_RecourceID + "' AND VehicleID='" + m_i_VehicleID_Trailer + "'";
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        //
        public void UpdateTruck()
        {
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Update VehicleRecource SET DateFrom='" + m_dt_TimeFrom + "', " +
                                                             "DateTo='" + m_dt_TimeTo + "', " +
                                                             "VehicleID='" + m_i_VehicleID_Truck.ToString().Replace(",", ".") + "' " +
                                                                               "WHERE RecourceID='" + m_i_RecourceID + "' and RecourceTyp='Z'";
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        //
        public void UpdateFahrer()
        {
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Update VehicleRecource SET PersonalID='" + m_i_PersonalID.ToString().Replace(",", ".") + "' " +
                                                                           "WHERE RecourceID='" + m_i_RecourceID + "' AND RecourceTyp='F'";

            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close(); ;
        }
        //
        public void UpdateFahrerStart()
        {
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Update VehicleRecource SET DateFrom='" + m_dt_TimeFrom + "' " +
                                                                           "WHERE RecourceID='" + m_i_RecourceID + "' AND PersonalID='" + m_i_PersonalID + "'";
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
        }
        //
        public void UpdateFahrerEnd()
        {
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "Update VehicleRecource SET DateTo='" + m_dt_TimeTo + "' " +
                                                                           "WHERE RecourceID='" + m_i_RecourceID + "' AND PersonalID='" + m_i_PersonalID + "'";
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
        }

        //
        //----------- Delete / Löschen der Recource vom TimePanel  -------------------
        //
        public void DeleteRecource(decimal RecourceID)
        {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "DELETE FROM VehicleRecource WHERE RecourceID='" + RecourceID + "'";
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
        }
        //
        //
        //
        public static DataSet GetUsedTruckRecource(Globals._GL_USER myGLUser, DateTime DateFrom, DateTime DateTo, decimal ZM_ID)
        {
            DataSet ds = new DataSet();
            string strSQL = string.Empty;
            strSQL = "SELECT RecourceTyp, VehicleID, PersonalID, " +
                                         "(Select Top(1) Leergewicht FROM Fahrzeuge WHERE Fahrzeuge.ID=VehicleID AND Fahrzeuge.ZM='F') as 'LG_A', " +
                                         "(Select Top(1) Leergewicht FROM Fahrzeuge WHERE Fahrzeuge.ID=VehicleID AND Fahrzeuge.ZM='T') as 'LG_ZM', " +
                                         "(Select Top(1) zlGG FROM Fahrzeuge WHERE Fahrzeuge.ID=VehicleID AND Fahrzeuge.ZM='F') as 'zlGG' " +
                                         "FROM VehicleRecource " +
                                          "WHERE " +
                                                 "(" +
                                                 "((DateFrom <= '" + DateFrom + "') AND (DateTo >='" + DateTo + "')) " +
                                                 ") " +
                                                 "AND RecourceID IN " +
                                                     "(" +
                                                       "SELECT RecourceID FROM VehicleRecource WHERE " +
                                                       "(" +
                                                       "((DateFrom <= '" + DateFrom + "') AND (DateTo >='" + DateTo + "')) " +
                                                       ") " +
                                                        "AND VehicleID='" + ZM_ID + "' " +
                                                      ")";

            ds.Tables.Add(clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Resource"));
            return ds;
        }
        //
        // Suche über  ID und Zeitraum ------------------
        //
        public static bool IsTrailerUsed(DateTime DateFrom, DateTime DateTo, decimal VehicleID, string Typ, decimal RecourceID)
        {
            bool IsUsed = false;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID FROM VehicleRecource " +
                                           "WHERE " +
                                                  "(((DateFrom <= '" + DateFrom + "') AND (DateTo >'" + DateFrom + "')) " +
                                                  "OR " +
                                                  "((DateFrom = '" + DateFrom + "') AND (DateTo <'" + DateTo + "'))" +
                                                  "OR " +
                                                  "((DateFrom <'" + DateTo + "') AND (DateTo > '" + DateTo + "')) " +
                                                  "OR " +
                                                  "((DateFrom >'" + DateFrom + "') AND (DateTo = '" + DateTo + "')) " +
                                                  "OR " +
                                                   "((DateFrom < '" + DateFrom + "') AND (DateTo > '" + DateTo + "')) " +
                                                  "OR " +
                                                   "((DateFrom > '" + DateFrom + "') AND (DateTo < '" + DateTo + "'))) " +

                                             "AND " +
                                                "(VehicleID= " + VehicleID + ") AND (RecourceTyp='" + Typ + "') AND (RecourceID<>'" + RecourceID + "')";

            Globals.SQLcon.Open();
            if (Command.ExecuteScalar() == null)
            {
                IsUsed = false;
            }
            else
            {
                IsUsed = true;
                decimal ID = (decimal)Command.ExecuteScalar();
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return IsUsed;
        }
        //
        // ---- Der ZM wurde bereits eine Recource zugewiesen ? -------------------
        //
        public static bool ZMRecourceIsUse(DateTime DateFrom, DateTime DateTo, decimal VehicleID, string RecTyp, decimal RecourceID)
        {
            bool IsUsed = false;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID FROM VehicleRecource " +
                                                  "WHERE RecourceTyp ='" + RecTyp + "' AND " +
                                                  "RecourceID = " +
                                                                "(SELECT TOP(1) RecourceID FROM VehicleRecource " +
                                                                  "WHERE " +
                                                                  "(((DateFrom <= '" + DateFrom + "') AND (DateTo >'" + DateFrom + "')) " +
                                                                  "OR " +
                                                                  "((DateFrom = '" + DateFrom + "') AND (DateTo <'" + DateTo + "'))" +
                                                                  "OR " +
                                                                  "((DateFrom <'" + DateTo + "') AND (DateTo > '" + DateTo + "')) " +
                                                                  "OR " +
                                                                  "((DateFrom >'" + DateFrom + "') AND (DateTo = '" + DateTo + "')) " +
                                                                  "OR " +
                                                                   "((DateFrom < '" + DateFrom + "') AND (DateTo > '" + DateTo + "')) " +
                                                                  "OR " +
                                                                   "((DateFrom > '" + DateFrom + "') AND (DateTo < '" + DateTo + "'))) " +
                                                                  "AND " +
                                                                  "((VehicleID= " + VehicleID + ") AND (RecourceTyp='Z')))";

            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();
            if ((obj == null) | (obj is DBNull))
            {
                IsUsed = false;
            }
            else
            {
                IsUsed = true;
                decimal ID = (decimal)Command.ExecuteScalar();
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return IsUsed;
        }
        //
        public static DataTable IsTrailerUsed___(DateTime DateFrom, DateTime DateTo, decimal VehicleID, string Typ, decimal RecourceID)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID" +
                                            ", RecourceID" +
                                            ", RecourceTyp" +
                                            ", VehicleID " +
                                            "FROM VehicleRecource " +
                                           "WHERE " +
                                                  "(" +
                                                  "((DateFrom = '" + DateFrom + "') AND (DateTo <'" + DateTo + "'))" +
                                                  "OR " +
                                                  "((DateFrom >'" + DateFrom + "') AND (DateTo = '" + DateTo + "')) " +
                                                  "OR " +
                                                   "((DateFrom > '" + DateFrom + "') AND (DateTo < '" + DateTo + "'))) " +

                                             "AND " +
                                                "(RecourceID<>'" + RecourceID + "')";

            Globals.SQLcon.Open();
            ada.Fill(dt);
            Command.Dispose();
            Globals.SQLcon.Close();

            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                Command.Connection.Close();

            return dt;
        }

        //
        //
        //
        public static bool IsFahrerUsed(DateTime DateFrom, DateTime DateTo, decimal PersonalID, string Typ, decimal RecourceID)
        {
            bool IsUsed = false;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID FROM VehicleRecource " +
                                           "WHERE " +

                                                  "(((DateFrom <= '" + DateFrom + "') AND (DateTo >'" + DateFrom + "')) " +
                                                  "OR " +
                                                  "((DateFrom = '" + DateFrom + "') AND (DateTo <'" + DateTo + "'))" +
                                                  "OR " +
                                                  "((DateFrom <'" + DateTo + "') AND (DateTo > '" + DateTo + "')) " +
                                                  "OR " +
                                                  "((DateFrom >'" + DateFrom + "') AND (DateTo = '" + DateTo + "')) " +
                                                  "OR " +
                                                   "((DateFrom < '" + DateFrom + "') AND (DateTo > '" + DateTo + "')) " +
                                                  "OR " +
                                                   "((DateFrom > '" + DateFrom + "') AND (DateTo < '" + DateTo + "'))) " +

                                             "AND " +
                                                "(PersonalID= " + PersonalID + ") AND (RecourceTyp='" + Typ + "') AND (RecourceID<>'" + RecourceID + "')";


            Globals.SQLcon.Open();
            if (Command.ExecuteScalar() == null)
            {
                IsUsed = false;
            }
            else
            {
                IsUsed = true;
                decimal ID = (decimal)Command.ExecuteScalar();
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return IsUsed;
        }
        //
        //------ Check ob Personaldatensatz verwendet wird-------------
        //kann nur gelöscht werden, wenn Sie nicht vorhanden ist
        public static bool IsPersonalIDIn(decimal _PersonalID)
        {
            bool IsUsed = false;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID From VehicleRecource WHERE PersonalID= '" + _PersonalID + "'";

            Globals.SQLcon.Open();
            if (Command.ExecuteScalar() == null)
            {
                IsUsed = false;
            }
            else
            {
                IsUsed = true;
                decimal ID = (decimal)Command.ExecuteScalar();
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return IsUsed;
        }
        //
        //------ Check ob FahrzeugID verwendet wird-------------
        //kann nur gelöscht werden, wenn Sie nicht vorhanden ist
        //
        public static bool IsFahrzeugIDIn(decimal _VehicleID)
        {
            bool IsUsed = false;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID From VehicleRecource WHERE VehicleID= '" + _VehicleID + "'";

            Globals.SQLcon.Open();
            if (Command.ExecuteScalar() == null)
            {
                IsUsed = false;
            }
            else
            {
                IsUsed = true;
                decimal ID = (decimal)Command.ExecuteScalar();
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return IsUsed;
        }
        //
        //--- erstellt den Infotext der Recource für den Auflieger ----------------------
        //
        private string GetRecourceInfoAuflieger()
        {
            string strInfo = string.Empty;
            DataSet ds = new DataSet();
            ds = clsFahrzeuge.GetRecByID(this._GL_User, m_i_VehicleID_Trailer);

            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strInfo = ds.Tables[0].Rows[i]["KFZ"].ToString();
                strInfo = strInfo + "-Achsen:" + ds.Tables[0].Rows[i]["Achsen"].ToString();
                strInfo = strInfo + "-Länge:" + ds.Tables[0].Rows[i]["Laenge"].ToString() + "m";
                strInfo = strInfo + "-LG:" + ds.Tables[0].Rows[i]["Leergewicht"].ToString() + "kg";
                strInfo = strInfo + "-IH:" + ds.Tables[0].Rows[i]["Innenhoehe"].ToString() + "m";
                if (ds.Tables[0].Rows[i]["Innenhoehe"].ToString() == "T")
                {
                    strInfo = strInfo + "-Mulde";
                }
            }
            return strInfo;
        }
        //
        //
        //
        public DataTable LoadRecouce(string RecourceTyp, bool RecourceLoaded)
        {
            DataTable RecourceTable = new DataTable();
            RecourceTable.Clear();
            if (!RecourceLoaded)
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT" +
                                                  " * " +

                                             "FROM " +
                                                  "VehicleRecource " +
                                             "WHERE (" +

                                                   //
                                                   "((CONVERT(varchar(10),[DateFrom], 104) <= '" + m_dt_TimeFrom.ToShortDateString() + "') AND (CONVERT(varchar(10),[DateTo], 104) >='" + m_dt_TimeTo.ToShortDateString() + "')) " +
                                                   "OR " +
                                                   //Resourcen, die über den gesamt Abfragezeitraum laufen
                                                   "((CONVERT(varchar(10),[DateFrom], 104) < '" + m_dt_TimeFrom.ToShortDateString() + "') AND (CONVERT(varchar(10),[DateTo], 104) > '" + m_dt_TimeTo.ToShortDateString() + "')) " +
                                                   "OR " +
                                                   //Resourcen, = oder innerhalb Abfragezeitraum laufen
                                                   "((CONVERT(varchar(10),[DateFrom], 104) >= '" + m_dt_TimeFrom.ToShortDateString() + "') AND (CONVERT(varchar(10),[DateTo], 104) <= '" + m_dt_TimeTo.ToShortDateString() + "')) " +
                                                   "OR " +
                                                   //Resourcenstartpunkt = TimeTo (die beginnende Resource wird angezeigt
                                                   "((CONVERT(varchar(10),[DateFrom], 104) = '" + m_dt_TimeTo.ToShortDateString() + "') AND (CONVERT(varchar(10),[DateTo], 104) > '" + m_dt_TimeTo.ToShortDateString() + "')) " +
                                                   "OR " +
                                                   //Resourcenendpunkt = TimeFrom (die endende Resource wird angezeigt
                                                   "((CONVERT(varchar(10),[DateFrom], 104) < '" + m_dt_TimeFrom.ToShortDateString() + "') AND (CONVERT(varchar(10),[DateTo], 104) = '" + m_dt_TimeTo.ToShortDateString() + "')) " +
                                                   "OR " +
                                                   //Resourcenstart- und -endpunkt liegen innerhalb des Abfragezeitraums (Abfragezeitraum über mehrere Tage)
                                                   "(" +
                                                       //Resourcenstartzeitpunkt ist > DateFrom und < DateTo  
                                                       "(CONVERT(varchar(10),[DateFrom], 104) > '" + m_dt_TimeFrom.ToShortDateString() + "') " +
                                                       "AND " +
                                                       "(CONVERT(varchar(10),[DateFrom], 104) < '" + m_dt_TimeTo.ToShortDateString() + "') " +
                                                       //Resourcenendzeitpunkt ist > DateFrom and < DateTo
                                                       "AND " +
                                                       "(CONVERT(varchar(10),[DateTo], 104) > '" + m_dt_TimeFrom.ToShortDateString() + "') " +
                                                       "AND " +
                                                       "(CONVERT(varchar(10),[DateTo], 104) < '" + m_dt_TimeTo.ToShortDateString() + "') " +

                                                   ")" +
                                                    "OR " +
                                                   //Resourcenstartpunkt liegen innerhalb des Abfragezeitraums (Abfragezeitraum über mehrere Tage)
                                                   "(" +
                                                       //Resourcenstartzeitpunkt ist > DateFrom und > DateTo  
                                                       "(CONVERT(varchar(10),[DateFrom], 104) >= '" + m_dt_TimeFrom.ToShortDateString() + "') " +
                                                       "AND " +
                                                       "(CONVERT(varchar(10),[DateTo], 104) > '" + m_dt_TimeTo.ToShortDateString() + "') " +
                                                   ")" +
                                                   "OR " +
                                                   //Resourcenendzeit liegen innerhalb des Abfragezeitraums (Abfragezeitraum über mehrere Tage)
                                                   "(" +
                                                       //Resourcenstartzeitpunkt ist < DateFrom und < DateTo  
                                                       "(CONVERT(varchar(10),[DateFrom], 104) < '" + m_dt_TimeFrom.ToShortDateString() + "') " +
                                                       "AND " +
                                                       "(CONVERT(varchar(10),[DateTo], 104) <= '" + m_dt_TimeTo.ToShortDateString() + "') " +
                                                   ")" +

                                                   ") AND (RecourceTyp='" + m_ch_RecourceTyp + "' OR RecourceTyp='Z') ORDER BY DateFrom";




                /****
                     "(((DateFrom < '" + m_dt_TimeFrom.ToShortDateString() + "') AND (DateTo >'" + m_dt_TimeFrom.ToShortDateString() + "')) " +
                     "OR " +
                     "((DateFrom = '" + m_dt_TimeFrom.ToShortDateString() + "') AND (DateTo <'" + m_dt_TimeTo.ToShortDateString() + "'))" +
                     "OR " +
                     "((DateFrom <'" + m_dt_TimeTo.ToShortDateString() + "') AND (DateTo > '" + m_dt_TimeTo.ToShortDateString() + "')) " +
                     "OR " +
                     "((DateFrom >'" + m_dt_TimeFrom.ToShortDateString() + "') AND (DateTo = '" + m_dt_TimeTo.ToShortDateString() + "')) " +
                     "OR " +
                      "((DateFrom < '" + m_dt_TimeFrom.ToShortDateString() + "') AND (DateTo > '" + m_dt_TimeTo.ToShortDateString() + "')) " +
                     "OR " +
                      "((DateFrom > '" + m_dt_TimeFrom.ToShortDateString() + "') AND (DateTo < '" + m_dt_TimeTo.ToShortDateString() + "'))) " +

                  "AND (RecourceTyp='" + m_ch_RecourceTyp + "' OR RecourceTyp='Z') ORDER BY DateFrom";
                 * ****/

                ada.Fill(RecourceTable);
                Command.Dispose();
                Globals.SQLcon.Close();

                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                    Command.Connection.Close();
            }
            return RecourceTable;
        }
    }
}


