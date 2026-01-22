using System;
using System.Data;

namespace LVS
{
    public class clsLogbuch
    {
        public clsLogbuch()
        {
        }
        public clsLogbuch(Decimal myBenutzerID, string myAktion, string myBeschreibung)
        {
            BenutzerID = myBenutzerID;
            Aktion = myAktion;
            Beschreibung = myBeschreibung;

            clsUser user = new clsUser((int)BenutzerID);
            BenutzerName = user.Name;
        }

        private decimal _ID;
        private decimal _BenutzerID;
        private string _BenutzerName;
        private string _Aktion;
        private string _Beschreibung;

        public decimal ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public decimal BenutzerID
        {
            get { return _BenutzerID; }
            set { _BenutzerID = value; }
        }
        public string BenutzerName
        {
            get
            {
                _BenutzerName = String.Empty;
                if (BenutzerID > 0)
                {
                    _BenutzerName = clsUser.GetBenutzerNameByID(BenutzerID);
                }
                return _BenutzerName;
            }
            set { _BenutzerName = value; }
        }
        public string Aktion
        {
            get { return _Aktion; }
            set { _Aktion = value; }
        }
        public string Beschreibung
        {
            get { return _Beschreibung; }
            set { _Beschreibung = value; }
        }


        /***************************************************************************************
         * 
         * 
         * *************************************************************************************/
        //
        //
        //
        public bool LogbuchInsert()
        {
            bool bReturn = false;
            try
            {
                string strSQL = string.Empty;
                strSQL = "INSERT INTO Logbuch (BenutzerID, " +
                                            "BenutzerName, " +
                                            "Datum, " +
                                            "Aktion, " +
                                            "Beschreibung) " +
                                "VALUES ('" + BenutzerID + "','"
                                            + BenutzerName + "', '"
                                            + DateTime.Now + "','"
                                            + Aktion + "','"
                                            + Beschreibung.Replace("'", "") + "')";
                bReturn = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                string str = ex.Message;
            }
            return bReturn;
        }
        //
        //----------- Read Logbuch komplett ---------------------------
        //
        public DataTable GetLogbuch(bool boAll, DateTime dtVon, DateTime dtBis)
        {
            DataTable dt = new DataTable("Logbuch");
            string sql = string.Empty;
            dt.Clear();
            try
            {
                string strSQL = string.Empty;
                if (boAll)
                {
                    strSQL = "SELECT * FROM Logbuch ORDER BY Datum Desc";
                }
                else
                {
                    strSQL = "SELECT * FROM Logbuch WHERE Datum>='" + dtVon + "' AND Datum<'" + dtBis.AddDays(1) + "' " +
                                                                            "ORDER BY Datum Desc";
                }

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Logbuch");
            }
            catch (Exception ex)
            {
                decimal decUser = -1.0M;
                Functions.AddLogbuch(decUser, "GetLogbuch", ex.ToString());
            }
            return dt;
        }
    }
}
