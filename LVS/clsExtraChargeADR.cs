using System;
using System.Data;

namespace LVS
{
    public class clsExtraChargeADR
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
            set
            {
                _BenutzerID = value;
            }
        }
        public decimal ID { get; set; }
        public decimal AdrID { get; set; }
        public decimal ExtraChargeID { get; set; }
        internal decimal _Preis = 0;
        public decimal Preis
        {
            get
            {
                if (this._Preis > 0)
                {
                    return _Preis;
                }
                else
                {
                    clsExtraCharge tmpExtraCharge = new clsExtraCharge();
                    tmpExtraCharge.ID = ExtraChargeID;
                    tmpExtraCharge.Fill();
                    return tmpExtraCharge.Preis;
                }

            }
            set
            {
                _Preis = value;
            }
        }
        //public decimal MandantenID { get; set; }
        //public decimal ArbeitsbereichID { get; set; }
        //public string Verweis { get; set; }
        //public bool aktiv { get; set; }

        /*****************************************************************************
         * 
         * ***************************************************************************/
        ///<summary>clsExtraChargeADR / Add</summary>
        ///<remarks></remarks>>
        public void Add()
        {
            string strSQL = string.Empty;
            strSQL = "INSERT INTO ExtraChargeADR (ExtraChargeID, AdrID, Preis" + //, MandantenID, ArbeitsbereichID" +
                                    ") " +
                                     "VALUES (" + ExtraChargeID +
                                     ", " + AdrID +
                                     ", '" + _Preis.ToString().Replace(",", ".") + "'" +
                                     ");";
            strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
            }
        }
        ///<summary>clsExtraChargeADR / Update</summary>
        ///<remarks>Update Adressdaten.</remarks>
        public void Update()
        {
            string strSQL = string.Empty;
            strSQL = "Update ExtraChargeADR " +
                            "SET " +
                                " ExtraChargeID=" + ExtraChargeID +
                                ", AdrID=" + AdrID +
                                ", Preis='" + _Preis.ToString().Replace(",", ".") + "'" +
                                    //", MandantenID=" + MandantenID +
                                    //", ArbeitsbereichID=" + ArbeitsbereichID +
                                    " WHERE ID=" + ID + ";";

            clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            this.Fill();
        }
        ///<summary>clsExtraChargeADR / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ExtraChargeADR WHERE AdrID=" + AdrID + "AND ExtraChargeID=" + ExtraChargeID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ExtraChargeADR");
            decimal decTmp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.ExtraChargeID = (decimal)dt.Rows[i]["ExtraChargeID"];
                this.AdrID = (decimal)dt.Rows[i]["AdrID"];
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Preis"].ToString(), out decTmp);
                this._Preis = decTmp;
                //this.MandantenID = (decimal)dt.Rows[i]["MandantenID"];
                //this.ArbeitsbereichID = (decimal)dt.Rows[i]["ArbeitsbereichID"];
            }
        }

        public void Delete(decimal _ExtraChargeID = -1)
        {
            string strSql = "Delete from ExtraChargeADR WHERE ";
            if (_ExtraChargeID > -1)
            {
                strSql += " ExtraChargeID=" + _ExtraChargeID + ";";
            }
            else
            {
                strSql += " AdrID=" + AdrID + "AND ExtraChargeID=" + ExtraChargeID + ";";
            }
            clsSQLcon.ExecuteSQL(strSql, BenutzerID);
        }

        public static DataTable getExtraChargeADRListByExtraChargeID(decimal ExtraChargeId, decimal BenutzerID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ExtraChargeADR WHERE ExtraChargeID=" + ExtraChargeId + ";"; // AND ArbeitsbereichID=" + this.ArbeitsbereichID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ExtraCharge");
            return dt;
        }
    }
}
