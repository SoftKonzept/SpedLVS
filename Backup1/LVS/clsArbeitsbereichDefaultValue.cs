using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsArbeitsbereichDefaultValue
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

        //Table KundenTarife
        public int ID { get; set; }
        public decimal AbBereichID { get; set; }
        public string Property { get; set; }
        public string Value { get; set; }

        public Dictionary<string, string> DictArbeitsbereichDefaultValue;

        /************************************************************************************
         *                      Methoden
         * *********************************************************************************/
        public void InitCls(decimal myArBereichId)
        {
            DictArbeitsbereichDefaultValue = new Dictionary<string, string>();
            this.AbBereichID = myArBereichId;
            if (this.AbBereichID > 0)
            {
                FillDict();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void FillDict()
        {
            DictArbeitsbereichDefaultValue = new Dictionary<string, string>();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ArbeitsbereichDefaultValue " +
                                    " WHERE AbBereichID = " + (int)this.AbBereichID + ";";
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "SELECT", "ArbeitsbereichDefaultValue", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    decimal decTmp = 0;
                    if (decimal.TryParse(row["ID"].ToString(), out decTmp))
                    {
                        if (
                            (!row["Property"].ToString().Equals(string.Empty)) &&
                            (!row["Value"].ToString().Equals(string.Empty))
                          )
                        {
                            if (!DictArbeitsbereichDefaultValue.ContainsKey(row["Property"].ToString()))
                            {
                                DictArbeitsbereichDefaultValue.Add(row["Property"].ToString(), row["Value"].ToString());
                            }
                        }
                    }
                }
                FillCls(dt);
            }

        }

        ///<summary>clsArbeitsbereichTairf / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            try
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO ArbeitsbereichDefaultValue (AbBereichID, Proptery, Value) " +
                                               "VALUES (" + (Int32)AbBereichID +
                                                        ",'" + this.Property + "'" +
                                                        ",'" + this.Value + "'" +
                                                        ")";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            catch (Exception ex)
            {

            }
        }
        ///<summary>clsArbeitsbereichTairf / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            try
            {
                string strSql = string.Empty;
                strSql = "Delete ArbeitsbereichDefaultValue WHERE ID=" + this.ID + ";";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            try
            {
                string strSql = string.Empty;
                strSql = "Update ArbeitsbereichDefaultValue SET " +
                                       "AbBereichID = " + (Int32)AbBereichID +
                                       ", Proptery = '" + this.Property + "'" +
                                       ", Value = '" + this.Value + "'" +
                                        " WHERE ID = " + this.ID + ";";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            catch (Exception ex)
            {

            }
        }

        public void Fill()
        {
            string strSql = string.Empty;
            strSql = "SELECT * FROM ArbeitsbereichDefaultValue " +
                                    " WHERE ID = " + this.ID + ";";
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "SELECT", "ArbeitsbereichDefaultValue", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                FillCls(dt);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        private void FillCls(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                int iTmp = 0;
                int.TryParse(row["ID"].ToString(), out iTmp);
                this.ID = iTmp;

                decimal decTmp = 0;
                decimal.TryParse(row["AbBereichID"].ToString(), out decTmp);
                this.AbBereichID = decTmp;

                this.Property = row["Property"].ToString();
                this.Value = row["Value"].ToString();
            }
        }
        /// <summary>
        ///              provisorisch erst mal so   
        /// </summary>
        /// <param name="art"></param>
        public void SetDefaultValue(ref clsArtikel art)
        {
            foreach (KeyValuePair<string, string> itm in this.DictArbeitsbereichDefaultValue)
            {
                switch (itm.Key)
                {
                    case clsArtikel.ArtikelField_Einheit:
                        art.Einheit = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_Produktionsnummer:
                        art.Produktionsnummer = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_Werksnummer:
                        art.Werksnummer = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_Charge:
                        art.Charge = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_Bestellnummer:
                        art.Bestellnummer = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_exBezeichnung:
                        art.exBezeichnung = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_exMaterialnummer:
                        art.exMaterialnummer = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_exAuftrag:
                        art.exAuftrag = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_exAuftragPos:
                        art.exAuftragPos = itm.Value;
                        break;

                    case clsArtikel.ArtikelField_Anzahl:
                    case clsArtikel.ArtikelField_LVSID:
                    case clsArtikel.ArtikelField_Dicke:
                    case clsArtikel.ArtikelField_Breite:
                    case clsArtikel.ArtikelField_Länge:
                    case clsArtikel.ArtikelField_Höhe:
                    case clsArtikel.ArtikelField_Netto:
                    case clsArtikel.ArtikelField_Brutto:
                    case clsArtikel.ArtikelField_Gut:
                    case clsArtikel.ArtikelField_Güte:
                    case clsArtikel.ArtikelField_Position:
                    case clsArtikel.ArtikelField_ArtikelIDRef:
                        break;

                    default:
                        break;
                }
            }
        }
        public void SetDefaultValue(ref Articles art)
        {
            foreach (KeyValuePair<string, string> itm in this.DictArbeitsbereichDefaultValue)
            {
                switch (itm.Key)
                {
                    case clsArtikel.ArtikelField_Einheit:
                        art.Einheit = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_Produktionsnummer:
                        art.Produktionsnummer = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_Werksnummer:
                        art.Werksnummer = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_Charge:
                        art.Charge = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_Bestellnummer:
                        art.Bestellnummer = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_exBezeichnung:
                        art.exBezeichnung = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_exMaterialnummer:
                        art.exMaterialnummer = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_exAuftrag:
                        art.exAuftrag = itm.Value;
                        break;
                    case clsArtikel.ArtikelField_exAuftragPos:
                        art.exAuftragPos = itm.Value;
                        break;

                    case clsArtikel.ArtikelField_Anzahl:
                    case clsArtikel.ArtikelField_LVSID:
                    case clsArtikel.ArtikelField_Dicke:
                    case clsArtikel.ArtikelField_Breite:
                    case clsArtikel.ArtikelField_Länge:
                    case clsArtikel.ArtikelField_Höhe:
                    case clsArtikel.ArtikelField_Netto:
                    case clsArtikel.ArtikelField_Brutto:
                    case clsArtikel.ArtikelField_Gut:
                    case clsArtikel.ArtikelField_Güte:
                    case clsArtikel.ArtikelField_Position:
                    case clsArtikel.ArtikelField_ArtikelIDRef:
                        break;

                    default:
                        break;
                }
            }
        }

    }
}
