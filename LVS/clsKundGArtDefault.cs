using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsKundGArtDefault
    {
        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GL_System;

        public clsGut Gut;
        public clsArbeitsbereiche Arbeitsbereich;

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
        public Int32 ID { get; set; }
        public decimal AdrID { get; set; }
        public decimal GArtID { get; set; }
        public decimal AbBereichID { get; set; }
        public Dictionary<decimal, clsKundGArtDefault> DictKundeGartDefault { get; set; }
        public DataTable dtDefaultGueterarten = new DataTable();


        public bool ExistDefaultGueterartForSeletektedWorkspace
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT d.*" +
                                ", a.Name as Arbeitsbereichname " +
                                ", g.Bezeichnung as Gut " +
                                " FROM KundGArtDefault d " +
                                " INNER JOIN Arbeitsbereich a on d.AbBereichID=a.ID " +
                                " INNER JOIN Gueterart g on g.ID = d.GArtID " +
                                " INNER JOIN ADR adr on adr.ID= d.AdrId " +
                                " WHERE " +
                                    "d.AdrID=" + (int)this.AdrID +
                                    " AND d.AbBereichID=" + this.AbBereichID +
                                    " ;";
                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "GArtDefault");
                return (dt.Rows.Count > 0);
            }
        }

        /********************************************************************************************************
         *                                  Methoden
         * *****************************************************************************************************/
        ///<summary>clsKundGArtDefault / Copy</summary>
        ///<remarks></remarks>
        public clsKundGArtDefault Copy()
        {
            return (clsKundGArtDefault)this.MemberwiseClone();
        }
        ///<summary>clsKundGArtDefault / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, decimal myAdrID)
        {
            this._GL_System = myGLSystem;
            this._GL_User = myGLUser;
            this.AdrID = myAdrID;
            FillDict();
        }
        ///<summary>clsKundGArtDefault / FillDict</summary>
        ///<remarks></remarks>
        private void FillDict()
        {
            DictKundeGartDefault = new Dictionary<decimal, clsKundGArtDefault>();
            dtDefaultGueterarten = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT d.*" +
                            ", a.Name as Arbeitsbereichname " +
                            ", g.Bezeichnung as Gut " +
                            " FROM KundGArtDefault d " +
                            " INNER JOIN Arbeitsbereich a on d.AbBereichID=a.ID " +
                            " INNER JOIN Gueterart g on g.ID = d.GArtID " +
                            " INNER JOIN ADR adr on adr.ID= d.AdrId " +
                            " WHERE " +
                                "d.AdrID=" + (int)this.AdrID + " ;";
            dtDefaultGueterarten = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "GArtDefault");
            FillCls();
        }
        /// <summary>
        /// 
        /// </summary>
        public void FillById()
        {
            DictKundeGartDefault = new Dictionary<decimal, clsKundGArtDefault>();
            dtDefaultGueterarten = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT d.*" +
                            ", a.Name as Arbeitsbereichname " +
                            ", g.Bezeichnung as Gut " +
                            " FROM KundGArtDefault d " +
                            " INNER JOIN Arbeitsbereich a on d.AbBereichID=a.ID " +
                            " INNER JOIN Gueterart g on g.ID = d.GArtID " +
                            " INNER JOIN ADR adr on adr.ID= d.AdrId " +
                            " WHERE " +
                                "d.ID=" + (int)this.ID + ";";
            dtDefaultGueterarten = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "GArtDefault");
            FillCls();
        }
        /// <summary>
        /// 
        /// </summary>
        private void FillCls()
        {
            decimal decTmp = 0;
            for (Int32 i = 0; i <= dtDefaultGueterarten.Rows.Count - 1; i++)
            {
                this.ID = (Int32)dtDefaultGueterarten.Rows[i]["ID"];
                this.AdrID = (decimal)dtDefaultGueterarten.Rows[i]["AdrID"];
                this.GArtID = (decimal)dtDefaultGueterarten.Rows[i]["GArtID"];
                if (this.GArtID > 0)
                {
                    this.Gut = new clsGut();
                    this.Gut.InitClass(this._GL_User, this._GL_System);
                    this.Gut.ID = this.GArtID;
                    this.Gut.Fill();
                }
                this.AbBereichID = (decimal)dtDefaultGueterarten.Rows[i]["AbBereichID"];
                if (this.AbBereichID > 0)
                {
                    this.Arbeitsbereich = new clsArbeitsbereiche();
                    this.Arbeitsbereich.InitCls(this._GL_User, this.AbBereichID);
                }

                clsKundGArtDefault tmpKundeGArtDefault = this.Copy();
                DictKundeGartDefault.Add(this.AbBereichID, tmpKundeGArtDefault);
            }
        }
        /// <summary>
        ///                 update 
        /// </summary>
        public void Update()
        {
            if (this.ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update KundGArtDefault SET " +
                                        " AdrID= " + this.AdrID +
                                        ", GArtID= " + this.GArtID +
                                        ", AbBereichID=" + this.AbBereichID +
                                " WHERE " +
                                    " ID =" + (int)this.ID + ";";
                clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "Update", _GL_User.User_ID);
            }
        }
        /// <summary>
        ///             Add 
        /// </summary>
        public bool Add()
        {
            bool bReturn = false;
            string strSql = string.Empty;
            strSql = "INSERT INTO KundGArtDefault ([AdrID],[GArtID],[AbBereichID])" +
                                            " VALUES (" + (int)this.AdrID +
                                                      ", " + (int)this.GArtID +
                                                      ", " + (int)this.AbBereichID +
                                                      ")";
            strSql = strSql + " Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            int iTmp = 0;
            int.TryParse(strTmp, out iTmp);
            if (iTmp > 0)
            {
                this.ID = iTmp;
                bReturn = true;
            }
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            bool bReturn = false;
            if (this.ID > 0)
            {
                string strSql = string.Empty;
                strSql = "DELETE FROM KundGArtDefault where ID=" + this.ID + " ;";
                bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "DELETE", BenutzerID);
            }
            return bReturn;
        }
    }
}
