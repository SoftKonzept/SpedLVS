using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsLieferanten
    {
        public Globals._GL_USER _GL_User;
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

        public decimal LiefGruppenID { get; set; }
        public decimal AdrIDLieferant { get; set; }
        private List<decimal> _ListLieferantenAdrID;
        public List<decimal> ListLieferantenAdrID
        {
            get
            {
                FillListLieferantenAdrID();
                return _ListLieferantenAdrID;
            }
            set { _ListLieferantenAdrID = value; }
        }
        public Dictionary<decimal, List<decimal>> DictGroupLieferanten { get; set; }

        /************************************************************************
         *                  Methoden / Prozedure
         * *********************************************************************/
        ///<summary>clsLieferanten / InitClass</summary>
        ///<remarks></remarks>>
        public void InitClass(Globals._GL_USER myGLUser, clsSystem mySys)
        {
            this._GL_User = myGLUser;
            this.Sys = mySys;
            FillDictGroupLieferanten();
        }
        ///<summary>clsLieferanten / Copy</summary>
        ///<remarks></remarks>
        public clsLieferanten Copy()
        {
            return (clsLieferanten)this.MemberwiseClone();
        }
        ///<summary>clsLieferantenGruppe / Add</summary>
        ///<remarks></remarks>>
        public bool Add()
        {
            bool RetVal = false;
            try
            {
                string strSQL = string.Empty;
                strSQL = "INSERT INTO Lieferanten (LiefGruppenID" +
                                                        ", AdrIDLieferant" +
                                                        ") " +
                                "VALUES (" + LiefGruppenID +
                                        ", " + AdrIDLieferant +
                                        ")";
                RetVal = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                RetVal = false;
            }
            finally
            {
                FillDictGroupLieferanten();
            }
            return RetVal;
        }
        ///<summary>clsLieferantenGruppe / Add</summary>
        ///<remarks></remarks>>
        public bool Delete()
        {
            bool RetVal = false;
            try
            {
                string strSQL = string.Empty;
                strSQL = "Delete Lieferanten WHERE " +
                                                "LiefGruppenID =" + (Int32)LiefGruppenID +
                                                " AND AdrIDLieferant=" + (Int32)AdrIDLieferant +
                                        ";";
                RetVal = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                RetVal = false;
            }
            finally
            {
                FillDictGroupLieferanten();
            }
            return RetVal;
        }
        ///<summary>clsLieferanten / FillListLieferantenAdrID</summary>
        ///<remarks></remarks>>
        public void FillListLieferantenAdrID()
        {
            _ListLieferantenAdrID = new List<decimal>();
            DataTable dt = new DataTable("Lieferanten");
            string sql = string.Empty;
            sql = "SELECT a.* FROM Lieferanten a " +
                                "INNER JOIN LieferantenGruppe b ON b.ID=a.LiefGruppenID " +
                                "WHERE " +
                                        "b.ID=" + this.LiefGruppenID + " ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(sql, BenutzerID, "Lieferanten");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decLfGrID = 0;
                Decimal.TryParse(dt.Rows[i]["LiefGruppenID"].ToString(), out decLfGrID);
                decimal decAdrIDLieferant = 0;
                Decimal.TryParse(dt.Rows[i]["AdrIDLieferant"].ToString(), out decAdrIDLieferant);
                if (decAdrIDLieferant > 0)
                {
                    _ListLieferantenAdrID.Add(decAdrIDLieferant);
                }
            }
        }
        ///<summary>clsLieferanten / GetLieferantenGruppen</summary>
        ///<remarks></remarks>>
        public DataTable GetLieferanten()
        {
            DataTable dt = new DataTable("Lieferanten");
            string sql = string.Empty;
            sql = "SELECT a.*, c.ViewID, c.Name1 " +
                                "FROM Lieferanten a " +
                                "INNER JOIN LieferantenGruppe b ON b.ID=a.LiefGruppenID " +
                                "INNER JOIN ADR c on c.ID = a.AdrIDLieferant " +
                                " ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(sql, BenutzerID, "Lieferanten");
            return dt;
        }
        ///<summary>clsLieferanten / FillListLieferantenAdrID</summary>
        ///<remarks></remarks>>
        public void FillDictGroupLieferanten()
        {
            DictGroupLieferanten = new Dictionary<decimal, List<decimal>>();
            DataTable dt = new DataTable("Group");
            string sql = string.Empty;
            sql = "SELECT DISTINCT b.ID FROM Lieferanten a " +
                                "INNER JOIN LieferantenGruppe b ON b.ID=a.LiefGruppenID " +
                                ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(sql, BenutzerID, "Group");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decLfGrID = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decLfGrID);
                if (decLfGrID > 0)
                {
                    clsLieferanten tmpLief = new clsLieferanten();
                    tmpLief = this.Copy();
                    tmpLief.LiefGruppenID = decLfGrID;
                    //tmpLief.FillListLieferantenAdrID();
                    DictGroupLieferanten.Add(decLfGrID, tmpLief.ListLieferantenAdrID);
                }
            }
        }
    }
}
