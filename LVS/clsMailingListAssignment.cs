using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsMailingListAssignment
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
        //************************************

        public decimal MailingListID { get; set; }
        public decimal KontaktID { get; set; }


        public decimal AdrID { get; set; }
        public List<decimal> ListMailingListKontaktID { get; set; }
        public List<string> ListMailingListMailadressen { get; set; }
        public DataTable dtMailingListMemeber { get; set; }


        /****************************************************************************************************
         *                      Procedure MailingListAssignment
         * *************************************************************************************************/
        ///<summary>clsMailingListAssignment / Update</summary>
        ///<remarks></remarks>>
        public void Update(DataTable dtNewKontakte, DataTable dtMailingListMember, decimal myMailingListID)
        {
            string strSQL = string.Empty;
            strSQL = "Delete FROM MailingListAssignment WHERE MailingListID=" + myMailingListID + "; ";
            /************************************************************************************
             * zuerst die MailingList Member,
             *hier müssen die Daten übernommen werden wo Column "Select" = 0
             ***********************************************************************************/
            if (dtMailingListMember.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dtMailingListMember.Rows.Count - 1; i++)
                {
                    if (!(bool)dtMailingListMemeber.Rows[i]["Select"])
                    {
                        decimal decTmp = 0;
                        string strTmp = dtMailingListMemeber.Rows[i]["KontaktID"].ToString();
                        if (strTmp != string.Empty)
                        {
                            Decimal.TryParse(strTmp, out decTmp);
                            if (decTmp > 0)
                            {
                                strSQL = strSQL + "INSERT INTO MailingListAssignment (MailingListID, KontaktID) " +
                                                                                                    "VALUES (" + myMailingListID +
                                                                                                            ", " + decTmp +
                                                                                                            "); ";
                            }
                        }
                    }

                }
            }

            /*************************************************************************************
             *nun die Kontakte die neu hinzugefügt werden sollen
             *
             * **********************************************************************************/
            if (dtNewKontakte.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dtNewKontakte.Rows.Count - 1; i++)
                {
                    if ((bool)dtNewKontakte.Rows[i]["Select"])
                    {
                        decimal decTmp = 0;
                        string strTmp = dtNewKontakte.Rows[i]["ID"].ToString();
                        if (strTmp != string.Empty)
                        {
                            Decimal.TryParse(strTmp, out decTmp);
                            if (decTmp > 0)
                            {
                                strSQL = strSQL + "INSERT INTO MailingListAssignment (MailingListID, KontaktID) " +
                                                                                                    "VALUES (" + myMailingListID +
                                                                                                            ", " + decTmp +
                                                                                                            "); ";
                            }
                        }
                    }

                }
            }

            bool bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "MailingListAssignment", BenutzerID);
            if (bOK)
            {
                //BAustelle muss das ins Protokoll?
            }
        }
        ///<summary>clsMailingListAssignment / FillList</summary>
        ///<remarks>Füllt die List mit allen KontaktID der entsprechenden MailinglistID</remarks>>
        public void FillList(decimal myMailingListID)
        {
            dtMailingListMemeber = new DataTable();
            this.ListMailingListKontaktID = new List<decimal>();
            this.ListMailingListMailadressen = new List<string>();
            if (myMailingListID > 0)
            {
                string strSql = string.Empty;
                strSql = "SELECT CAST(0 as bit) as 'Select'" +
                                                ", m.* " +
                                                ", a.ViewID" +
                                                ", k.Nachname" +
                                                ", k.Vorname" +
                                                ", k.Mail" +
                                                " FROM MailingListAssignment m " +
                                                "INNER JOIN Kontakte k ON k.ID=m.KontaktID " +
                                                "INNER JOIN ADR a ON a.ID=k.ADR_ID " +
                                                "WHERE MailingListID=" + myMailingListID + ";";
                dtMailingListMemeber = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "MailingListAssignment");
                for (Int32 i = 0; i <= dtMailingListMemeber.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(dtMailingListMemeber.Rows[i]["KontaktID"].ToString(), out decTmp);
                    this.ListMailingListKontaktID.Add(decTmp);

                    string Mailadresse = dtMailingListMemeber.Rows[i]["Mail"].ToString();
                    ListMailingListMailadressen.Add(Mailadresse);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object GetAllKontakte()
        {
            dtMailingListMemeber = new DataTable();

            List<clsKontakte> lstKontakte = new List<clsKontakte>();
            this.ListMailingListKontaktID = new List<decimal>();
            this.ListMailingListMailadressen = new List<string>();
            if (this.MailingListID > 0)
            {
                string strSql = string.Empty;
                strSql = "SELECT CAST(0 as bit) as 'Select'" +
                                                ", m.* " +
                                                ", a.ViewID" +
                                                ", k.Nachname" +
                                                ", k.Vorname" +
                                                ", k.Mail" +
                                                " FROM MailingListAssignment m " +
                                                "INNER JOIN Kontakte k ON k.ID=m.KontaktID " +
                                                "INNER JOIN ADR a ON a.ID=k.ADR_ID " +
                                                "WHERE MailingListID=" + MailingListID + ";";
                dtMailingListMemeber = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "MailingListAssignment");
                for (Int32 i = 0; i <= dtMailingListMemeber.Rows.Count - 1; i++)
                {
                    clsKontakte kontakt = new clsKontakte();

                    decimal decTmp = 0;
                    Decimal.TryParse(dtMailingListMemeber.Rows[i]["KontaktID"].ToString(), out decTmp);
                    this.ListMailingListKontaktID.Add(decTmp);

                    kontakt.ID = decTmp;
                    kontakt.Fill();
                    lstKontakte.Add(kontakt);


                    string Mailadresse = dtMailingListMemeber.Rows[i]["Mail"].ToString();
                    ListMailingListMailadressen.Add(Mailadresse);
                }
            }
            return lstKontakte;
        }

    }
}
