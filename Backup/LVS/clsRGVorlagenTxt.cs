using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsRGVorlagenTxt
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

        public string Vorlage { get; set; }
        public string Vorlagentext { get; set; }
        public decimal EinzelPreis { get; set; }
        public Dictionary<string, clsRGVorlagenTxt> DictVorlagen = new Dictionary<string, clsRGVorlagenTxt>();
        public DataTable dtVorlagen = new DataTable("Vorlagen");
        public DataTable dtVorlagenDetails = new DataTable("Details");

        ///<summary>clsRGVorlagenTxt / Add</summary>
        ///<remarks></remarks>>
        public void Add()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO RGVorlagenTxt (Vorlage, Vorlagentext, EinzelPreis) " +
                                            "VALUES ( '" + Vorlage + "'" +
                                                    ", '" + Vorlagentext + "'" +
                                                    ",'" + EinzelPreis.ToString().Replace(",", ".") + "'" +

                                                    ");";
            bool bDeleteOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            if (bDeleteOK)
            {
                //Add Logbucheintrag Löschen
                string logBeschreibung = "RG Vorlagetext: " + this.Vorlage + " gespeichert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), logBeschreibung);
            }
        }
        ///<summary>clsRGVorlagenTxt / DeleteADR</summary> 
        ///<remarks></remarks>>
        public void Delete()
        {
            string strSql = string.Empty;
            strSql = "DELETE FROM RGVorlagenTxt WHERE Vorlage='" + Vorlage + "'";
            bool bDeleteOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);

            if (bDeleteOK)
            {
                //Add Logbucheintrag Löschen
                string logBeschreibung = "RG Vorlagetext: " + this.Vorlage + " gelöscht";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), logBeschreibung);
            }
        }
        ///<summary>clsRGVorlagenTxt / Fill</summary>
        ///<remarks></remarks>>
        public void FillDictRGVorlagenTxt()
        {
            dtVorlagen = new DataTable("Vorlagen");
            dtVorlagen.Columns.Add("Vorlage", typeof(string));
            dtVorlagenDetails = new DataTable("Details");

            DictVorlagen = new Dictionary<string, clsRGVorlagenTxt>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM RGVorlagenTxt ORDER BY Vorlage;";
            dtVorlagenDetails = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "RGVorlagenTxt");

            for (Int32 i = 0; i <= dtVorlagenDetails.Rows.Count - 1; i++)
            {
                try
                {
                    clsRGVorlagenTxt tmpVorlage = new clsRGVorlagenTxt();
                    tmpVorlage._GL_User = this._GL_User;
                    tmpVorlage.Vorlage = dtVorlagenDetails.Rows[i]["Vorlage"].ToString();
                    tmpVorlage.Vorlagentext = dtVorlagenDetails.Rows[i]["Vorlagentext"].ToString();
                    decimal decTmp = 0;
                    Decimal.TryParse(dtVorlagenDetails.Rows[i]["EinzelPreis"].ToString(), out decTmp);
                    tmpVorlage.EinzelPreis = decTmp;
                    DictVorlagen.Add(tmpVorlage.Vorlage, tmpVorlage);

                    DataRow row1 = dtVorlagen.NewRow();
                    row1["Vorlage"] = tmpVorlage.Vorlage;
                    dtVorlagen.Rows.Add(row1);
                }
                catch (Exception ex)
                {
                }
            }
        }
        ///<summary>clsRGVorlagenTxt / ExistVorlage</summary>
        ///<remarks></remarks>>
        public bool ExistVorlage()
        {
            string strSql = string.Empty;
            strSql = "Select * FROM RGVorlagenTxt WHERE Vorlage='" + Vorlage + "'";
            bool bExist = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            return bExist;
        }
    }
}
