using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsTarifGArtZuweisung
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

        // public string strSQLGArtID = string.Empty; //GüterartID mit Komma getrennt

        private decimal _ID;
        private decimal _TarifID;
        private decimal _GArtID;
        private DataTable _dtTarifGArt;
        private DataTable _dtGArtID;  //beinhaltet die GArtenID für den zugewiesenen Tarif
        private bool _GArtAssign;
        private List<decimal> _ListGArten;
        private string _SQLGArtIDString;


        public decimal ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public decimal TarifID
        {
            get { return _TarifID; }
            set { _TarifID = value; }
        }
        public decimal GArtID
        {
            get { return _GArtID; }
            set { _GArtID = value; }
        }
        public DataTable dtTarifGArt
        {
            get
            {
                return _dtTarifGArt;
            }
            set { _dtTarifGArt = value; }
        }

        public DataTable dtGArtID
        {
            get
            {
                //_dtGArtID = new DataTable();
                //_dtGArtID = dtTarifGArt.DefaultView.ToTable(true, "GArtID");
                return _dtGArtID;
            }
            set { _dtGArtID = value; }
        }
        public List<decimal> ListGArten
        {
            get
            {
                return _ListGArten;
            }
            set { _ListGArten = value; }
        }
        public bool GArtAssign
        {
            get
            {
                _GArtAssign = (dtTarifGArt.Rows.Count > 0);
                return _GArtAssign;
            }
            set { _GArtAssign = value; }
        }
        public string SQLGArtIDString
        {
            get
            {
                _SQLGArtIDString = string.Empty;
                if (ListGArten != null)
                {
                    if (ListGArten.Count > 0)
                    {
                        _SQLGArtIDString = string.Join(",", ListGArten.ToArray());
                    }
                }
                return _SQLGArtIDString;
            }
            set { _SQLGArtIDString = value; }
        }
        /************************************************************************************
         *                              Methoden 
         * *********************************************************************************/
        ///<summary>clsTarifGArtZuweisung / Add</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB.Vorerst muss geprüft werden, ob es die Zuweisung mit der 
        ///         Kombination Tarif / Güterart bereits gibt.</remarks>
        public void Add()
        {
            if (!ExistTarifGArtZuweisung())
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO TarifGArtZuweisung (TarifID, GArtID) " +
                                               "VALUES (" + TarifID +
                                                       "," + GArtID +
                                                       ");" +
                        " Select @@IDENTITY; ";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    ID = decTmp;
                    Fill();
                }
            }
        }
        ///<summary>clsTarifGArtZuweisung / Delete</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB.</remarks>
        public void Delete()
        {
            string strSql = string.Empty;
            strSql = "Delete TarifGArtZuweisung WHERE ID=" + ID + ";";
            clsSQLcon.ExecuteSQL(strSql, BenutzerID);
        }
        ///<summary>clsTarifGArtZuweisung / ExistTarifGArtZuweisung</summary>
        ///<remarks>Prüft, ob es die Zuweisung mit der Kombination TArif und Gart bereits gibt.</remarks>
        private bool ExistTarifGArtZuweisung()
        {
            string strSql = string.Empty;
            strSql = "Select ID FROM TArifGArtZuweisung WHERE TarifID=" + TarifID + " AND GArtID=" + GArtID + ";";
            bool bCheck = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            return bCheck;
        }
        ///<summary>clsTarifGArtZuweisung / GetTarifGArten</summary>
        ///<remarks></remarks>
        public void GetTarifGArten()
        {
            dtTarifGArt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select a.*, b.ViewID as Matchcode, b.Bezeichnung FROM TArifGArtZuweisung a " +
                                                "INNER JOIN Gueterart b ON b.ID = a.GArtID " +
                                                "WHERE " +
                                                        "TarifID=" + TarifID + ";";
            dtTarifGArt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "TarifGArten");
            SetListGArten();
        }
        ///<summary>clsTarifGArtZuweisung / Fill</summary>
        ///<remarks>Füll die Klasse anhand der ID.</remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select a.* " +
                            "FROM TArifGArtZuweisung a WHERE a.ID=" + ID + "; ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "TarifGArten");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    this.ID = (decimal)dt.Rows[i]["ID"];
                    this.TarifID = (decimal)dt.Rows[i]["TarifID"]; ;
                    this.GArtID = (decimal)dt.Rows[i]["GArtID"];
                }
            }
            GetTarifGArten();
        }
        ///<summary>clsTarifGArtZuweisung / Fill</summary>
        ///<remarks>Füll die Klasse anhand der ID.</remarks>
        public void FillByTarifID()
        {
            ListGArten = new List<decimal>();

            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select Top(1) a.* " +
                            "FROM TArifGArtZuweisung a WHERE a.TarifID=" + this.TarifID + "; ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "TarifGArten");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    this.ID = (decimal)dt.Rows[i]["ID"];
                    this.TarifID = (decimal)dt.Rows[i]["TarifID"]; ;
                    this.GArtID = (decimal)dt.Rows[i]["GArtID"];
                }
            }
            GetTarifGArten();
        }
        ///<summary>clsTarifGArtZuweisung / SetListGArten</summary>
        ///<remarks></remarks>
        private void SetListGArten()
        {
            ListGArten = new List<decimal>();
            for (Int32 i = 0; i <= dtTarifGArt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                if (Decimal.TryParse(dtTarifGArt.Rows[i]["GArtID"].ToString(), out decTmp))
                {
                    ListGArten.Add(decTmp);
                }
            }
        }
    }
}
