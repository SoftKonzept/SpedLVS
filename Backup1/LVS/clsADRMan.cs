using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsADRMan
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
        public Dictionary<Int32, clsADRMan> DictManuellADREinlagerung;
        public Dictionary<Int32, clsADRMan> DictManuellADRAuslagerung;
        public Dictionary<Int32, clsADRMan> DictManuellADRAuftrag;
        public Dictionary<int, string> DictAdrArt = new Dictionary<int, string>()
        {
            { 0, "Auftraggeber" },
            { 1, "Versender" },
            { 2, "Beladeadresse" },
            { 3, "Empfänger" },
            { 4, "Entladeadersse" },
            { 5, "Spedition" }
        };

        public const Int32 cont_AdrArtID_Auftraggeber = 0;
        public const Int32 cont_AdrArtID_Versender = 1;
        public const Int32 cont_AdrArtID_Beladeadresse = 2;
        public const Int32 cont_AdrArtID_Empfaenger = 3;
        public const Int32 cont_AdrArtID_Entladeadresse = 4;
        public const Int32 cont_AdrArtID_Spedition = 5;


        //************************************

        public decimal ID { get; set; }
        public string TableName { get; set; }
        public decimal TableID { get; set; }
        public string FBez { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Str { get; set; }
        public string HausNr { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Land { get; set; }
        public string LKZ { get; set; }
        public Int32 AdrArtID { get; set; }
        private string _AdrArt;
        public string AdrArt
        {
            get
            {
                string strArt = string.Empty;
                DictAdrArt.TryGetValue(this.AdrArtID, out strArt);
                _AdrArt = strArt;
                return _AdrArt;
            }
            set { _AdrArt = value; }
        }

        private string _AdrString;
        public string AdrString
        {
            get
            {
                _AdrString = this.Name1 + " - " + this.PLZ + " - " + this.Ort;
                return _AdrString;
            }
            set { _AdrString = value; }
        }
        /**********************************************************************************
         *                      Methoden / Procedure
         * ********************************************************************************/
        ///<summary>clsADRMan / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, decimal myTableID, string myTable)
        {
            this._GL_User = myGLUser;
            this.TableID = myTableID;
            this.TableName = myTable;
            FillDict();
        }
        ///<summary>clsADRMan / Add</summary>
        ///<remarks></remarks>>
        public void Add()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO ADRMan (TableName, TableID, FBez, Name1, Name2, Name3, Strasse, HausNr, PLZ, Ort, " +
                                      "LKZ,Land, AdrArtID) " +
                                        "VALUES ('" + TableName + "'" +
                                                ", " + TableID +
                                                ", '" + FBez + "'" +
                                                ", '" + Name1 + "'" +
                                                ", '" + Name2 + "'" +
                                                ", '" + Name3 + "'" +
                                                ", '" + Str + "'" +
                                                ", '" + HausNr + "'" +
                                                ", '" + PLZ + "'" +
                                                ", '" + Ort + "'" +
                                                ", '" + LKZ + "'" +
                                                ", '" + Land + "'" +
                                                ", " + AdrArtID +
                                                ");";

            strSql = strSql + " Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
                FillbyID();
                FillDict();
            }
        }
        ///<summary>clsADRMan / Update</summary>
        ///<remarks></remarks>
        public void Update()
        {
            string strSql = string.Empty;
            strSql = "Update ADRMan SET " +
                                        "TableName='" + TableName + "'" +
                                        ", TableID=" + TableID +
                                        ", FBez='" + FBez + "'" +
                                        ", Name1='" + Name1 + "'" +
                                        ", Name2='" + Name2 + "'" +
                                        ", Name3='" + Name3 + "'" +
                                        ", Strasse='" + Str + "'" +
                                        ", HausNr='" + HausNr + "'" +
                                        ", PLZ='" + PLZ + "'" +
                                        ", Ort='" + Ort + "'" +
                                        ", LKZ='" + LKZ + "'" +
                                        ", Land='" + Land + "'" +
                                        ", AdrArtID=" + AdrArtID +


                                        " WHERE ID=" + ID + ";";

            clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            this.FillbyID();
            FillDict();
        }
        ///<summary>clsADRMan / Fill</summary>
        ///<remarks></remarks>>
        public void FillDict()
        {
            switch (this.TableName)
            {
                case "LEingang":
                    DictManuellADREinlagerung = new Dictionary<int, clsADRMan>();
                    break;

                case "LAusgang":
                    DictManuellADRAuslagerung = new Dictionary<int, clsADRMan>();
                    break;
                case clsAuftrag.const_DBTableName:
                    DictManuellADRAuftrag = new Dictionary<int, clsADRMan>();
                    break;
            }

            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT ID FROM ADRMan WHERE TableName='" + TableName + "' AND TableID=" + TableID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Einheit");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                clsADRMan tmpMan = new clsADRMan();
                tmpMan._GL_User = this._GL_User;
                tmpMan.ID = (decimal)dt.Rows[i]["ID"];
                tmpMan.FillbyID();

                switch (this.TableName)
                {
                    case "LEingang":
                        DictManuellADREinlagerung.Add(tmpMan.AdrArtID, tmpMan);
                        break;

                    case "LAusgang":
                        DictManuellADRAuslagerung.Add(tmpMan.AdrArtID, tmpMan);
                        break;
                    case clsAuftrag.const_DBTableName:
                        DictManuellADRAuftrag.Add(tmpMan.AdrArtID, tmpMan);
                        break;
                }
            }
        }
        ///<summary>clsADRMan / Fill</summary>
        ///<remarks></remarks>>
        public void FillbyID()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRMan WHERE ID=" + this.ID + " ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "AdrMan");
            FillCls(ref dt);
        }
        ///<summary>clsADRMan / FillbyTableAndAdrArtID</summary>
        ///<remarks></remarks>>
        public void FillbyTableAndAdrArtID()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRMan WHERE TableName='" + TableName + "' AND TableID=" + TableID + " AND AdrArtID=" + AdrArtID + " ;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "AdrMan");
            FillCls(ref dt);
            FillDict();
        }
        ///<summary>clsADRMan / FillCls</summary>
        ///<remarks></remarks>>
        private void FillCls(ref DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.TableName = dt.Rows[i]["TableName"].ToString();
                this.TableID = (decimal)dt.Rows[i]["TableID"];
                this.FBez = dt.Rows[i]["FBez"].ToString();
                this.Name1 = dt.Rows[i]["Name1"].ToString();
                this.Name2 = dt.Rows[i]["Name2"].ToString();
                this.Name3 = dt.Rows[i]["Name3"].ToString();
                this.Str = dt.Rows[i]["Strasse"].ToString();
                this.HausNr = dt.Rows[i]["HausNr"].ToString();
                this.PLZ = dt.Rows[i]["PLZ"].ToString();
                this.Ort = dt.Rows[i]["Ort"].ToString();
                this.Land = dt.Rows[i]["Land"].ToString();
                this.LKZ = dt.Rows[i]["LKZ"].ToString();
                this.AdrArtID = (Int32)dt.Rows[i]["AdrArtID"];
            }
        }
        ///<summary>clsADRMan / DeleteADR</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            string strSql = string.Empty;
            strSql = "DELETE FROM ADRMan WHERE ID='" + ID + "'";
            bool bDeleteOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
        }
        ///<summary>clsADRMan / DeleteADR</summary>
        ///<remarks></remarks>
        public void DeleteAllByTableID()
        {
            string strSql = string.Empty;
            strSql = "DELETE FROM ADRMan WHERE TableName='" + TableName + "' AND TableID=" + TableID + " ";
            bool bDeleteOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
        }
        ///<summary>clsADRMan / FillbyTableAndAdrArtID</summary>
        ///<remarks></remarks>>
        public bool CheckManADRForAdrArt()
        {
            string strSql = string.Empty;
            strSql = "SELECT ID FROM ADRMan WHERE TableName='" + TableName + "' AND TableID=" + TableID + " AND AdrArtID=" + AdrArtID + " ;";
            bool bReturn = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            return bReturn;
        }

        /*************************************************************************************
         *                                  public Static
         * ***********************************************************************************/
        ///<summary>clsADRMan / DeleteAllByTableIDAndAdrArtID</summary>
        ///<remarks></remarks>
        public static void DeleteAllByTableIDAndAdrArtID(Globals._GL_USER myGLUser, string myTableName, decimal myTableID, Int32 myAdrArtID)
        {
            string strSql = string.Empty;
            strSql = "DELETE FROM ADRMan " +
                                        "WHERE " +
                                            "TableName='" + myTableName + "' " +
                                            "AND TableID=" + myTableID +
                                            " AND AdrArtID=" + myAdrArtID + " ;";
            bool bDeleteOK = clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }

    }
}
