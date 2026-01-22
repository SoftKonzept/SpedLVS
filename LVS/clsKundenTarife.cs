using System.Data;

namespace LVS
{
    public class clsKundenTarife
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

        //Table KundenTarife
        private decimal _ID;
        private decimal _AdrID;
        private decimal _TarifID;

        public decimal ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public decimal TarifID
        {
            get { return _TarifID; }
            set { _TarifID = value; }
        }
        public decimal AdrID
        {
            get { return _AdrID; }
            set { _AdrID = value; }
        }

        /************************************************************************************
         *                      Methoden
         * *********************************************************************************/
        ///<summary>clsKundenTarife / GetKundenTarifeForGArtRelation</summary>
        ///<remarks></remarks>
        public static DataTable GetCustomRateForGArtRelation(decimal myGLUserID, decimal myAdrID)
        {
            DataTable dt = new DataTable();
            if (myAdrID > 0)
            {
                string strSQL = string.Empty;
                strSQL = "Select " +
                            "t.ID as TarifID " +
                            ", t.Tarifname " +

                         "FROM  Tarife t " +
                            "INNER JOIN KundenTarife kdT ON kdT.TarifID=t.ID " +
                            "INNER JOIN ADR a ON a.ID = kdT.AdrID " +
                         "WHERE " +
                            "a.ID=" + myAdrID + " ";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUserID, "Tarife");
                DataRow row = dt.NewRow();
                row[0] = -1;
                row[1] = "-bitte Tarif wählen-";
                dt.Rows.Add(row);
                dt.DefaultView.Sort = "TarifID";
            }
            return dt;
        }
        ///<summary>clsKundenTarife / GetGetKundenWithTarif</summary>
        ///<remarks></remarks>
        public static DataTable GetGetKundenWithTarif(Globals._GL_USER myGLUser)
        {
            DataTable dt = new DataTable();
            string strSQL = string.Empty;
            strSQL = "SELECT DISTINCT a.ID " +
                                ", a.ViewID+ ' - ' + a.Name1 as Auftraggeber " +
                                ", a.ViewID " +
                                "FROM KundenTarife kdT " +
                                "INNER JOIN Tarife t ON t.ID=kdT.TarifID " +
                                "INNER JOIN ADR a ON a.ID=kdT.AdrID order by a.ViewID+ ' - ' + a.Name1 ";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "KundeTarif");
            DataRow row = dt.NewRow();
            row[0] = -1;
            row[1] = "-bitte Auftraggeber wählen-";
            row[2] = "0";
            dt.Rows.Add(row);
            dt.DefaultView.Sort = "ViewID";
            return dt;
        }
    }
}
