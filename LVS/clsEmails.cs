using System;
using System.Data;

namespace LVS
{
    public class clsEmails
    {

        public Globals._GL_SYSTEM _GL_System;
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

        public decimal ID { get; set; }
        public string Benutzer { get; set; }
        public string Absender { get; set; }
        public string Empfaenger { get; set; }
        public string Text { get; set; }
        public string Dateien { get; set; }
        public DateTime Datum { get; set; }
        public string Betreff { get; set; }



        /********************************************************************************************************
         *                                              Methoden
         * ******************************************************************************************************/
        ///<summary>clsEmails / InitClass</summary>
        ///<remarks>Initialisiert</remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem)
        {
            this._GL_User = myGLUser;
            this._GL_System = myGLSystem;
        }
        ///<summary>clsEmails / InitClass</summary>
        ///<remarks>Initialisiert</remarks>
        public void Add()
        {
            string strSQL = "INSERT INTO [Emails]([BenutzerID],[Absender],[Empfaenger] ,[Text],[Dateien],[Datum], Betreff) " +
                                         "VALUES( " +
                                           "  " + BenutzerID +
                                           " , '" + Absender + "'" +
                                           " , '" + Empfaenger + "'" +
                                           " , '" + Text + "'" +
                                           " , '" + Dateien + "'" +
                                           ", GetDate() " +
                                           ", '" + Betreff + "'" +
                                           ")";
            strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
            }
        }
        ///<summary>clsEmails / Fill</summary>
        ///<remarks>Initialisiert</remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM Emails WHERE ID=" + this.ID + " ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Emails");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["BenutzerID"].ToString(), out decTmp);
                this.BenutzerID = decTmp;
                this.Absender = dt.Rows[i]["Absender"].ToString(); ;
                this.Empfaenger = dt.Rows[i]["Empfaenger"].ToString();
                this.Text = dt.Rows[i]["Text"].ToString();
                this.Dateien = dt.Rows[i]["Dateien"].ToString();
                this.Datum = (DateTime)dt.Rows[i]["Datum"];
                this.Betreff = dt.Rows[i]["Betreff"].ToString();
            }
        }
        /// <summary>clsEmails / GetLogbuch</summary>
        /// <param name="boAll"></param>
        /// <param name="dtVon"></param>
        /// <param name="dtBis"></param>
        /// <returns></returns>
        public static DataTable GetLogbuch(Globals._GL_USER myGLUser, bool boAll, DateTime dtVon, DateTime dtBis)
        {
            DataTable dt = new DataTable("Logbuch");
            string sql = string.Empty;
            dt.Clear();
            try
            {
                string strSQL = string.Empty;
                if (boAll)
                {
                    strSQL = "SELECT * FROM Emails ORDER BY Datum Desc";
                }
                else
                {
                    strSQL = "SELECT * FROM Emails WHERE Datum>='" + dtVon + "' AND Datum<'" + dtBis.AddDays(1) + "' " +
                                                                            "ORDER BY Datum Desc";
                }

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Logbuch");
            }
            catch (Exception ex)
            {
                decimal decUser = -1.0M;
                Functions.AddLogbuch(decUser, "GetEmailLog", ex.ToString());
            }
            return dt;
        }
        ///<summary>clsMail / GetEMails</summary>
        ///<remarks> </remarks>
        public DataTable GetEMails()
        {
            DataTable dtReturn = new DataTable("Mails");
            string strSQL = string.Empty;
            strSQL = "SELECT TOP (5) * FROM Emails ORDER BY Datum Desc";
            dtReturn = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Mails");
            return dtReturn;
        }
    }
}
