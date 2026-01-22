using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsTableOfAccount
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

        public DataTable dtTableOfAccount = new DataTable("TableOfAccount");
        public Dictionary<string, Int32> DictFibuKto = new Dictionary<string, int>();
        public decimal ID { get; set; }
        public Int32 KontoNr { get; set; }
        public string KontoText { get; set; }
        public string Beschreibung { get; set; }
        public DateTime GueltigBis { get; set; }


        /***************************************************************************
         *                  Methoden / Procedure 
         * *************************************************************************/
        ///<summary>clsTableOfAccount / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser)
        {
            this._GL_User = myGLUser;
            FillDataTableOfAccount();
        }
        ///<summary>clsTableOfAccount / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            string strSQL = string.Empty;
            strSQL = "INSERT INTO TableOfAccount (KontoNr, KontoText, Beschreibung, Bis) " +
                                            "VALUES (" + KontoNr +
                                                    ", '" + KontoText + "'" +
                                                    ", '" + Beschreibung + "'" +
                                                    ", '" + GueltigBis + "'" +
                                                    "); ";
            strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
            }
        }
        ///<summary>clsTableOfAccount / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM TableOfAccount WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "TableOfAccount");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["KontoNr"].ToString(), out iTmp);
                this.KontoNr = iTmp;
                this.KontoText = dt.Rows[i]["KontoText"].ToString();
                this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                this.GueltigBis = (DateTime)dt.Rows[i]["Bis"];
            }
        }
        ///<summary>clsTableOfAccount / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            string strSql = string.Empty;
            strSql = "DELETE FROM TableOfAccount WHERE ID=" + ID;
            bool bDeleteOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            if (bDeleteOK)
            {
                //string logBeschreibung = "Sonderkosten: [" + this.ID + "] - " + Bezeichnung + "  gelöscht";
                //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), logBeschreibung);
            }
        }
        ///<summary>clsTableOfAccount / Update</summary>
        ///<remarks>Update Daten.</remarks>
        public void Update()
        {
            if (this.ID > 0)
            {
                string strSQL = string.Empty;
                strSQL = "Update TableOfAccount SET " +
                                            "KontoNr=" + KontoNr +
                                            ", KontoText ='" + KontoText + "'" +
                                            ", Beschreibung ='" + Beschreibung + "'" +
                                            ", Bis='" + GueltigBis + "'" +

                                            " WHERE ID=" + ID + ";";

                bool bUpdateOK = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
                if (bUpdateOK)
                {
                    //string logBeschreibung = "Sonderkosten: [" + this.ID + "] - " + Bezeichnung + "  geändert";
                    //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), logBeschreibung);
                }
            }
        }
        ///<summary>clsTableOfAccount / GetExtraChargeList</summary>
        ///<remarks>Filterung der gültigen Konten durch optionalen Bool-Wert</remarks>
        public void FillDataTableOfAccount(bool checkDate = false, decimal id = -1)
        {
            DictFibuKto = new Dictionary<string, int>();
            string strSql = string.Empty;
            strSql = "SELECT * FROM TableOfAccount ";
            if (checkDate == true)
            {
                strSql += " WHERE Bis > GETDATE() ";
                if (id > -1)
                {
                    strSql += "OR ID=" + id + " ";
                }
            }
            strSql += "Order By KontoText;";
            dtTableOfAccount = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "TableOfAccount");
            for (Int32 i = 0; i <= dtTableOfAccount.Rows.Count - 1; i++)
            {
                Int32 iTmp = 0;
                Int32.TryParse(dtTableOfAccount.Rows[i]["KontoNr"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    DictFibuKto.Add(dtTableOfAccount.Rows[i]["KontoText"].ToString(), iTmp);
                }
            }
        }


    }
}
