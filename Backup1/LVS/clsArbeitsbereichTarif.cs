using System;

namespace LVS
{
    public class clsArbeitsbereichTarif
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
        public decimal ID { get; set; }
        public decimal AbBereichID { get; set; }
        public decimal TarifID { get; set; }

        private bool _IsAssign;
        public bool IsAssign
        {
            get
            {
                _IsAssign = CheckTarifIsAssign();
                return _IsAssign;
            }
            set { _IsAssign = value; }
        }


        /************************************************************************************
         *                      Methoden
         * *********************************************************************************/


        ///<summary>clsArbeitsbereichTairf / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            try
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO ArbeitsbereichTarif (AbBereichID, TarifID) " +
                                               "VALUES (" + (Int32)AbBereichID +
                                                        "," + (Int32)TarifID +
                                                        ")";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            catch (Exception ex)
            {

            }
        }
        ///<summary>clsArbeitsbereichTairf / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            try
            {
                string strSql = string.Empty;
                strSql = "Delete ArbeitsbereichTarif WHERE ID=" + this.ID + ";";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            catch (Exception ex)
            {

            }
        }
        ///<summary>clsArbeitsbereichTairf / CheckTarifIsAssign</summary>
        ///<remarks></remarks>
        private bool CheckTarifIsAssign()
        {
            bool RetVal = false;
            string strSQL = string.Empty;
            strSQL = "Select ID FROM ArbeitsbereichTarif " +
                                        "WHERE TarifID=" + this.TarifID + " " +
                                               "AND AbBereichID=" + this.AbBereichID + " ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                RetVal = true;
            }
            return RetVal;
        }
        /////<summary>clsArbeitsbereichTairf / GetKundenTarifeForGArtRelation</summary>
        /////<remarks></remarks>
        //public static DataTable GetCustomRateForGArtRelation(decimal myGLUserID, decimal myAdrID)
        //{
        //    DataTable dt = new DataTable();
        //    if (myAdrID > 0)
        //    {
        //        string strSQL = string.Empty;
        //        strSQL = "Select " +
        //                    "t.ID as TarifID " +
        //                    ", t.Tarifname " +

        //                 "FROM  Tarife t " +
        //                    "INNER JOIN KundenTarife kdT ON kdT.TarifID=t.ID " +
        //                    "INNER JOIN ADR a ON a.ID = kdT.AdrID " +
        //                 "WHERE " +
        //                    "a.ID=" + myAdrID + " ";
        //        dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUserID, "Tarife");
        //        DataRow row = dt.NewRow();
        //        row[0] = -1;
        //        row[1] = "-bitte Tarif wählen-";
        //        dt.Rows.Add(row);
        //        dt.DefaultView.Sort = "TarifID";
        //    }
        //    return dt;
        //}
        /////<summary>clsArbeitsbereichTairf / GetGetKundenWithTarif</summary>
        /////<remarks></remarks>
        //public static DataTable GetGetKundenWithTarif(Globals._GL_USER myGLUser)
        //{
        //    DataTable dt = new DataTable();
        //    string strSQL = string.Empty;
        //    strSQL = "SELECT DISTINCT a.ID " +
        //                        ", a.ViewID+ ' - ' + a.Name1 as Auftraggeber " +
        //                        ", a.ViewID " +
        //                        "FROM KundenTarife kdT " +
        //                        "INNER JOIN Tarife t ON t.ID=kdT.TarifID " +
        //                        "INNER JOIN ADR a ON a.ID=kdT.AdrID order by a.ViewID+ ' - ' + a.Name1 ";

        //    dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "KundeTarif");
        //    DataRow row = dt.NewRow();
        //    row[0] = -1;
        //    row[1] = "-bitte Auftraggeber wählen-";
        //    row[2] = "0";
        //    dt.Rows.Add(row);
        //    dt.DefaultView.Sort = "ViewID";
        //    return dt;
        //}
    }
}
