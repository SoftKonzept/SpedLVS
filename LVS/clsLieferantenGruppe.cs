using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsLieferantenGruppe
    {
        public clsLieferanten Lieferanten;
        public clsADR ADRKomPartner;
        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GL_System;

        public clsSystem Sys;

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

        public decimal ID { get; set; }
        public string Name { get; set; }
        public decimal AdrIDKomPartner { get; set; }
        public decimal AbBereichID { get; set; }
        public string Lieferantennummer { get; set; }
        public bool IsActiv { get; set; }

        public List<string> ListUsedLiefGroupNames { get; set; }
        public Dictionary<decimal, List<decimal>> DictGroupLieferanten { get; set; }

        /************************************************************************
         *                  Methoden / Prozedure
         * *********************************************************************/
        ///<summary>clsLieferantenGruppe / Add</summary>
        ///<remarks></remarks>>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, clsSystem mySys)
        {
            this._GL_User = myGLUser;
            this._GL_System = myGLSystem;
            this.Sys = mySys;

            Lieferanten = new clsLieferanten();
            Lieferanten.InitClass(this._GL_User, this.Sys);

            ADRKomPartner = new clsADR();
            ADRKomPartner.InitClass(this._GL_User, this._GL_System, 0, true);
        }
        ///<summary>clsLieferantenGruppe / Copy</summary>
        ///<remarks></remarks>
        public clsLieferantenGruppe Copy()
        {
            return (clsLieferantenGruppe)this.MemberwiseClone();
        }
        ///<summary>clsLieferantenGruppe / Add</summary>
        ///<remarks></remarks>>
        public bool Add()
        {
            bool bReturn = false;
            try
            {
                string strSQL = string.Empty;
                strSQL = "INSERT INTO LieferantenGruppe (Name" +
                                                        ", Lieferantennummer" +
                                                        ", AdrIDKomPartner " +
                                                        ", AbBereichID" +
                                                        ", activ" +
                                                        ") " +
                                "VALUES ('" + Name + "'" +
                                        ", '" + Lieferantennummer + "'" +
                                        ", " + AdrIDKomPartner +
                                        ", " + AbBereichID +
                                        ", " + Convert.ToInt32(IsActiv) +
                                        ")";
                strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                decimal decTmp = 0;
                decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    this.ID = decTmp;
                    this.Fill();
                    bReturn = true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

            }
            return bReturn;
        }
        ///<summary>clsLieferantenGruppe / Update</summary>
        ///<remarks></remarks>>
        public bool Update()
        {
            bool bReturn = false;
            string sql = string.Empty;
            sql = "Update LieferantenGruppe SET " +
                                        "Name ='" + this.Name + "'" +
                                        ", Lieferantennummer='" + this.Lieferantennummer + "'" +
                                        ", AdrIDKomPartner=" + (Int32)this.AdrIDKomPartner +
                                        ", AbBereichID=" + (Int32)this.AbBereichID +
                                        ", active=" + Convert.ToInt32(this.IsActiv) +
                                    " WHERE ID=" + this.ID + " ;";
            if (clsSQLcon.ExecuteSQL(sql, BenutzerID))
            {
                bReturn = true;
                this.Fill();
            }
            return bReturn;
        }
        ///<summary>clsLieferantenGruppe / Add</summary>
        ///<remarks></remarks>>
        public void Fill()
        {
            DataTable dt = new DataTable("LieferantenGruppen");
            string sql = string.Empty;
            sql = "SELECT * FROM LieferantenGruppe WHERE ID=" + this.ID + " ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(sql, BenutzerID, "LieferantenGruppen");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                this.ID = decTmp;
                this.Name = dt.Rows[i]["Name"].ToString();
                this.Lieferantennummer = dt.Rows[i]["Lieferantennummer"].ToString();
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AdrIDKomPartner"].ToString(), out decTmp);
                this.AdrIDKomPartner = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AbBereichID"].ToString(), out decTmp);
                this.AbBereichID = decTmp;
                this.IsActiv = (bool)dt.Rows[i]["activ"];

                Lieferanten = new clsLieferanten();
                Lieferanten.InitClass(this._GL_User, this.Sys);
                Lieferanten.LiefGruppenID = this.ID;

                ADRKomPartner = new clsADR();
                ADRKomPartner.InitClass(this._GL_User, this._GL_System, this.AdrIDKomPartner, true);
            }
        }

        private void InitDictGroupLieferanten()
        {

        }
        ///<summary>clsLieferantenGruppe / GetLieferantenGruppen</summary>
        ///<remarks></remarks>>
        public DataTable GetLieferantenGruppen()
        {
            ListUsedLiefGroupNames = new List<string>();
            DataTable dt = new DataTable("LieferantenGruppen");
            string sql = string.Empty;
            sql = "SELECT b.ID " +
                          ", b.Name" +
                          ", a.ViewID as Matchcode" +
                          ", b.Lieferantennummer" +
                          ", c.Name as Arbeitsbereich " +
                          ", b.activ" +
                          ", b.AdrIDKomPartner" +
                          ", b.AbBereichID" +

                                    " FROM LieferantenGruppe b " +
                                    "INNER JOIN ADR a ON a.ID = b.AdrIDKomPartner " +
                                    "INNER JOIN Arbeitsbereich c on c.ID=b.AbBereichID " +
                                    ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(sql, BenutzerID, "LieferantenGruppen");
            foreach (DataRow row in dt.Rows)
            {
                ListUsedLiefGroupNames.Add(row["Name"].ToString());
            }
            return dt;
        }
    }
}
