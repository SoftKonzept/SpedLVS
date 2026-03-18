using System;

namespace LVS
{
    public class clsArbeitsbereichGArten
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
        public decimal GArtID { get; set; }

        private bool _IsAssign;
        public bool IsAssign
        {
            get
            {
                _IsAssign = CheckGartIsAssign();
                return _IsAssign;
            }
            set { _IsAssign = value; }
        }


        /************************************************************************************
         *                      Methoden
         * *********************************************************************************/
        ///<summary>clsArbeitsbereichGArten / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            try
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO ArbeitsbereichGArten (AbBereichID, GArtID) " +
                                               "VALUES (" + (Int32)AbBereichID +
                                                        "," + (Int32)GArtID +
                                                        ")";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            catch (Exception ex)
            {

            }
        }
        ///<summary>clsArbeitsbereichGArten / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            try
            {
                string strSql = string.Empty;
                strSql = "Delete ArbeitsbereichGArten WHERE GArtID=" + this.GArtID + " " +
                                                           "AND AbBereichID=" + this.AbBereichID + " ";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            catch (Exception ex)
            {

            }
        }
        ///<summary>clsArbeitsbereichGArten / CheckTarifIsAssign</summary>
        ///<remarks></remarks>
        private bool CheckGartIsAssign()
        {
            bool RetVal = false;
            string strSQL = string.Empty;
            strSQL = "Select Count(*) as Anzahl  FROM ArbeitsbereichGArten " +
                                        "WHERE GArtID=" + this.GArtID + " " +
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
    }
}
