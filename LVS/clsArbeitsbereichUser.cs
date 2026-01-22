using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsArbeitsbereichUser
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
        public decimal AbBereichID { get; set; }
        public decimal UserID { get; set; }
        private List<decimal> _ListArbeitsbereichAccess;
        public List<decimal> ListArbeitsbereichAccess
        {
            get
            {
                _ListArbeitsbereichAccess = GetArbeitsbereichByUser();
                return _ListArbeitsbereichAccess;
            }
            set { _ListArbeitsbereichAccess = value; }
        }
        private bool _IsAssign;
        public bool IsAssign
        {
            get
            {
                _IsAssign = CheckUserIsAssign();
                return _IsAssign;
            }
            set { _IsAssign = value; }
        }

        /************************************************************************************
         *                      Methoden
         * *********************************************************************************/
        ///<summary>clsArbeitsbereichUser / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER GLUser)
        {
            this._GL_User = GLUser;
            this.UserID = this.BenutzerID;
        }
        ///<summary>clsArbeitsbereichUser / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            try
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO ArbeitsbereichUser (AbBereichID, UserID) " +
                                               "VALUES (" + (Int32)AbBereichID +
                                                        "," + (Int32)UserID +
                                                        ")";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            catch (Exception ex)
            {

            }
        }
        ///<summary>clsArbeitsbereichUser / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            try
            {
                string strSql = string.Empty;
                strSql = "Delete ArbeitsbereichUser WHERE AbBereichID=" + this.AbBereichID + " AND UserID=" + this.UserID + ";";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            catch (Exception ex)
            {

            }
        }
        ///<summary>clsArbeitsbereichUser / CheckTarifIsAssign</summary>
        ///<remarks></remarks>
        private bool CheckUserIsAssign()
        {
            bool RetVal = false;
            string strSQL = string.Empty;
            strSQL = "Select UserID FROM ArbeitsbereichUser " +
                                        "WHERE UserID=" + this.UserID + " " +
                                               "AND AbBereichID=" + this.AbBereichID + " ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                RetVal = true;
            }
            return RetVal;
        }
        ///<summary>clsArbeitsbereichUser / GetArbeitsbereichByUser</summary>
        ///<remarks></remarks>
        private List<decimal> GetArbeitsbereichByUser()
        {
            List<decimal> listReturn = new List<decimal>();
            DataTable dt = new DataTable();
            string strSQL = string.Empty;
            strSQL = "Select DISTINCT AbBereichID FROM ArbeitsbereichUser WHERE UserID=" + UserID + " ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ArbeitsbereichAccess");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AbBereichID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    listReturn.Add(decTmp);
                }
            }
            return listReturn;
        }
        ///<summary>clsArbeitsbereichUser / AdminUpdate</summary>
        ///<remarks></remarks>
        public void AdminUpdate(ref clsUser myUser)
        {
            //zur Sicherheit alle Einträge löschen und alle wieder hinzufügen
            string strSQL = "DELETE ArbeitsbereichUser " +
                                        "WHERE " +
                                               "UserID=" + myUser.ID +
                                               " AND AbBereichID IN (" + string.Join(",", myUser.ListArbeitsbereichAccess.ToArray()) + ");";
            //hinzufügen

            strSQL = strSQL + "INSERT INTO ArbeitsbereichUser (AbBereichID, UserID) " +
                              "SELECT ID, " + myUser.ID + " FROM Arbeitsbereich";
            clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "AdminUpdate", BenutzerID);
            myUser.Fill();
        }

        public void AdminUpdate(UsersViewData myUserVM)
        {
            //zur Sicherheit alle Einträge löschen und alle wieder hinzufügen
            string strSQL = "DELETE ArbeitsbereichUser " +
                                        "WHERE " +
                                               "UserID=" + myUserVM.User.Id +
                                               " AND AbBereichID IN (" + string.Join(",", myUserVM.UserAuthorizationsVM.ArbeitsbereichAccess.ListArbeitsbereichAccess.ToArray()) + ");";
            //hinzufügen

            strSQL = strSQL + "INSERT INTO ArbeitsbereichUser (AbBereichID, UserID) " +
                              "SELECT ID, " + myUserVM.User.Id + " FROM Arbeitsbereich";
            clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "AdminUpdate", BenutzerID);
            //myUser.Fill();
        }
    }
}
