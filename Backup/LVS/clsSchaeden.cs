using System;
using System.Collections.Generic;
using System.Data;


namespace LVS
{
    public class clsSchaeden
    {
        public clsSchaeden()
        { }
        public clsSchaeden(int myUserId) : this()
        { }



        public const Int32 const_Art_SchadenUndMängel = -1;
        public const Int32 const_Art_Active = -2;
        public const Int32 const_Art_Passiv = -3;

        public const Int32 const_Art_Schaden = 0;
        public const string const_Art_strSchaden = "kritischer Schaden";
        public const Int32 const_Art_Mangel = 1;
        public const string const_Art_strMangel = "leichte Beschädigung/Mangel";



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

        //Table Tarife
        public decimal ID { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public bool aktiv { get; set; }
        public decimal ArtikelID { get; set; }
        public DateTime Datum { get; set; }
        private decimal _SchadenZuweisungID;
        public decimal SchadenZuweisungID
        {
            get { return _SchadenZuweisungID; }
            set { _SchadenZuweisungID = value; }
        }
        public Int32 Art { get; set; }
        public string _Schadensart;
        public string Schadensart
        {
            get
            {
                _Schadensart = string.Empty;
                switch (this.Art)
                {
                    case const_Art_Mangel:
                        _Schadensart = const_Art_strMangel;
                        break;
                    case const_Art_Schaden:
                        _Schadensart = const_Art_strSchaden;
                        break;
                    default:
                        _Schadensart = string.Empty;
                        break;
                }
                return _Schadensart;
            }
            set { _Schadensart = value; }
        }
        public string Code { get; set; }
        public bool AutoSPL { get; set; }

        /**********************************************************************************
         *                                  Methode
         * *******************************************************************************/
        ///<summary>clsSchaeden / ExistSchaden</summary>
        ///<remarks>Prüft anhand der SChadensbezeichnung ob der Schaden schon existiert.</remarks>
        public bool ExistSchaden()
        {
            string strResult = string.Empty;
            string strSql = string.Empty;
            strSql = "Select a.ID FROM Schaeden a " +
                                              "WHERE a.Bezeichnung='" + Bezeichnung + "' AND ID<>" + ID + "; ";
            strResult = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            if (strResult == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        ///<summary>clsSchaeden / Copy</summary>
        ///<remarks></remarks>
        public clsSchaeden Copy()
        {
            return (clsSchaeden)this.MemberwiseClone();
        }
        ///<summary>clsSchaeden / AddSchaden</summary>
        ///<remarks>Schreibt den neuen Datensatz in die Datenbank.</remarks>
        public void AddSchaden()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO Schaeden (Bezeichnung, Beschreibung, aktiv, Art, Code, AutoSPL) " +
                                            "VALUES ('" + Bezeichnung + "'" +
                                                     ",'" + Beschreibung + "'" +
                                                     "," + Convert.ToInt32(aktiv) +
                                                     "," + Art +
                                                     ",'" + Code + "'" +
                                                     "," + Convert.ToInt32(AutoSPL) +
                                                     "); ";
            strSql = strSql + " Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
                //Add Logbucheintrag Exception
                string myBeschreibung = "Schaden angelegt: " + Bezeichnung;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);
            }
        }
        ///<summary>clsSchaeden / AddSchaden</summary>
        ///<remarks>Schreibt den neuen Datensatz in die Datenbank.</remarks>
        public void UpdateSchaden()
        {
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update Schaeden SET Bezeichnung ='" + Bezeichnung + "'" +
                                                    ", Beschreibung ='" + Beschreibung + "'" +
                                                    ", aktiv =" + Convert.ToInt32(aktiv) +
                                                    ", Art =" + Convert.ToInt32(Art) +
                                                    ", Code ='" + Code + "'" +
                                                    ", AutoSPL =" + Convert.ToInt32(AutoSPL) +
                                                    " WHERE ID=" + ID + ";  ";

                if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
                {
                    //Add Logbucheintrag Exception
                    string myBeschreibung = "Schaden geändert: Schaden ID:" + ID;
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), myBeschreibung);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGL_User"></param>
        /// <param name="myListToShow"></param>
        /// <returns></returns>
        public static DataTable GetSchaeden(Globals._GL_USER myGL_User, Int32 myListToShow)
        {
            DataTable dt = new DataTable("Schaeden");
            string strSql = string.Empty;
            strSql = "Select " +
                            "CAST(0 as bit) as 'Select'" +
                            ", a.ID" +
                            ", a.Bezeichnung" +
                            ", a.Beschreibung" +
                            ", a.aktiv" +
                            ", a.Art " +
                            ", CASE " +
                                "WHEN a.Art = 0 THEN 'kritischer Schaden'" +
                                "WHEN a.Art = 1 THEN 'leichte Beschädigung/Mangel'" +
                                "ELSE '' " +
                                "END as Schadensart " +
                            ", a.Code" +
                            ", a.AutoSPL" +
                            " FROM Schaeden a ";
            switch (myListToShow)
            {
                //Alle
                case const_Art_SchadenUndMängel:
                    break;
                //Active
                case const_Art_Active:
                    strSql = strSql + "WHERE " +
                                      "a.aktiv=1 ";
                    break;
                //Passive
                case const_Art_Passiv:
                    strSql = strSql + "WHERE " +
                                       "a.aktiv=0 ";
                    break;
                //nur Schäden
                case const_Art_Schaden:
                    strSql = strSql + "WHERE " +
                                       "a.Art=" + const_Art_Schaden + " ";
                    break;
                //nur Mängel
                case const_Art_Mangel:
                    strSql = strSql + "WHERE " +
                                       "a.Art=" + const_Art_Mangel + " ";
                    break;
            }
            strSql = strSql + "ORDER BY a.ID ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGL_User.User_ID, "Schaeden");
            return dt;
        }
        ///<summary>clsArbeitsbereiche / GetArtikelSchäden</summary>
        ///<remarks>Ermittel die Schäden eines Aritkels</remarks>
        public DataTable GetArtikelSchäden(bool bWithUserID = false)
        {
            DataTable dt = new DataTable("Schaeden");
            if (ArtikelID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select " +
                                    "a.ID " +
                                    ", a.Datum " +
                                    ", b.Bezeichnung " +
                                    ", CAST(1 as bit) as 'Select'";
                if (bWithUserID)
                {
                    strSql += ",a.UserID " +
                              ",a.SchadenID ";
                }
                strSql += "FROM SchadenZuweisung a " +
                        "INNER JOIN Schaeden b ON b.ID=a.SchadenID " +
                        "WHERE ArtikelID=" + ArtikelID + ";";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Schaeden");
            }
            return dt;
        }
        ///<summary>clsSchaeden / FillByBezeichnung</summary>
        ///<remarks></remarks>
        public void FillByBezeichnung()
        {
            string strResult = string.Empty;
            string strSql = string.Empty;
            strSql = "Select a.* FROM Schaeden a " +
                                              "WHERE a.Bezeichnung='" + Bezeichnung + "' ";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Schaeden");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    this.ID = (decimal)dt.Rows[i]["ID"];
                    this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                    this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                    this.aktiv = (bool)dt.Rows[i]["aktiv"];
                }
            }
        }
        ///<summary>clsSchaeden / FillByID</summary>
        ///<remarks></remarks>
        public void FillByID()
        {
            string strResult = string.Empty;
            string strSql = string.Empty;
            strSql = "Select a.* FROM Schaeden a " +
                                              "WHERE a.ID=" + ID + "; ";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Schaeden");
            FillClsValue(dt);
        }
        ///<summary>clsSchaeden / FillClsValue</summary>
        ///<remarks></remarks>
        private void FillClsValue(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    this.ID = (decimal)dt.Rows[i]["ID"];
                    this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString().Trim();
                    this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString().Trim();
                    this.aktiv = (bool)dt.Rows[i]["aktiv"];
                    this.Art = (Int32)dt.Rows[i]["Art"];
                    this.Code = dt.Rows[i]["Code"].ToString();
                    this.AutoSPL = (bool)dt.Rows[i]["AutoSPL"];
                }
            }
        }
        ///<summary>clsSchaeden / FillByID</summary>
        ///<remarks></remarks>
        public void FillSchadensZuweisungByID()
        {
            string strResult = string.Empty;
            string strSql = string.Empty;
            strSql = "Select a.* FROM SchadenZuweisung a " +
                                              "WHERE a.ID=" + SchadenZuweisungID + "; ";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "SchaedenAssignment");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                    this.SchadenZuweisungID = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["ArtikelID"].ToString(), out decTmp);
                    this.ArtikelID = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["SchadenID"].ToString(), out decTmp);
                    this.ID = decTmp;
                    this.Datum = (DateTime)dt.Rows[i]["Datum"];

                    //clsSchaden füllen
                    this.FillByID();
                }
            }
        }
        ///<summary>clsSchaeden / DictSchadensArt</summary>
        ///<remarks></remarks>
        public static Dictionary<Int32, string> DictSchadensArt()
        {
            Dictionary<Int32, string> DictSchadensArt = new Dictionary<int, string>();
            DictSchadensArt = new Dictionary<int, string>();
            DictSchadensArt.Add(const_Art_Schaden, const_Art_strSchaden);
            DictSchadensArt.Add(const_Art_Mangel, const_Art_strMangel);
            return DictSchadensArt;
        }

        /*********************************************************************************************************
         *                              Schadenzuweisung
         * ******************************************************************************************************/

        public string AddSchadenZuweisungSqlString(int myArtId, int mySchadenId)
        {
            string strSql = string.Empty;
            //strSql = strSql + " INSERT INTO SchadenZuweisung (ArtikelID, SchadenID, UserID, Datum) " +
            //                        "VALUES (" + myArtId +
            //                                 "," + mySchadenId +
            //                                 "," + BenutzerID +
            //                                 ",'" + DateTime.Now + "'" +
            //                                 "); ";
            return strSql;
        }


        ///<summary>clsSchaeden / AddSchadenZuweisung</summary>
        ///<remarks></remarks>
        public void AddSchadenZuweisung(DataTable myDT, bool bUmbuchungKopie = false)
        {
            List<string> listSchadentext = new List<string>();
            decimal decArtID = this.ArtikelID;
            if (decArtID > 0)
            {
                string strSql = string.Empty;
                decimal decSchadenID = 0;
                if (!bUmbuchungKopie)
                {
                    for (Int32 i = 0; i <= myDT.Rows.Count - 1; i++)
                    {
                        if ((bool)myDT.Rows[i]["Select"])
                        {
                            Decimal.TryParse(myDT.Rows[i]["ID"].ToString(), out decSchadenID);
                            strSql = strSql + " INSERT INTO SchadenZuweisung (ArtikelID, SchadenID, UserID, Datum) " +
                                                            "VALUES (" + decArtID +
                                                                     "," + decSchadenID +
                                                                     "," + BenutzerID +
                                                                     ",'" + DateTime.Now + "'" +
                                                                     "); ";

                            clsSchaeden tmpSchaden = new clsSchaeden();
                            tmpSchaden = this.Copy();
                            tmpSchaden.ID = decSchadenID;
                            tmpSchaden.FillByID();
                            listSchadentext.Add(tmpSchaden.Bezeichnung.Trim());
                        }

                    }
                }
                else
                {
                    for (Int32 i = 0; i <= myDT.Rows.Count - 1; i++)
                    {

                        strSql = strSql + " INSERT INTO SchadenZuweisung (ArtikelID, SchadenID, UserID, Datum) " +
                                                        "VALUES (" + decArtID +
                                                                 "," + myDT.Rows[i]["SchadenID"] +
                                                                 "," + myDT.Rows[i]["UserID"] +
                                                                 ",'" + myDT.Rows[i]["Datum"] + "'" +
                                                                 "); ";


                    }
                }
                if (strSql != string.Empty)
                {
                    bool bOK = false;
                    try
                    {
                        bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "SchadenZusweisung", BenutzerID);
                    }
                    finally
                    {
                        //VITA
                        clsLager lg = new clsLager();
                        lg.InitSubClasses();
                        lg.Artikel._GL_User = this._GL_User;
                        lg.Artikel.ID = ArtikelID;
                        lg.Artikel.GetArtikeldatenByTableID();
                        lg.Eingang.LEingangTableID = lg.Artikel.LEingangTableID;
                        lg.Eingang.FillEingang();
                        string strSchadensText = string.Empty;
                        for (Int32 i = 0; i <= listSchadentext.Count - 1; i++)
                        {
                            if (!strSchadensText.Equals(string.Empty))
                            {
                                strSchadensText = strSchadensText + Environment.NewLine;
                            }
                            strSchadensText = strSchadensText + "- " + listSchadentext[i].ToString();
                        }
                        clsArtikelVita.ArtikelAddSchaden(BenutzerID, lg.Artikel.ID, lg.Eingang.LEingangID, lg.Artikel.LVS_ID, strSchadensText);

                        //Add Logbucheintrag Exception
                        string myBeschreibung = "Artikelschaden hinterlegt: LVSNr/ID: [" + ArtikelID.ToString() + "]";
                        myBeschreibung = myBeschreibung + Environment.NewLine + strSchadensText;
                        Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);
                    }
                }
            }
        }
        ///<summary>clsSchaeden / SchadenInUse</summary>
        ///<remarks></remarks>
        public bool SchadenInUse()
        {
            string strSql = string.Empty;
            strSql = "Select ID FROM SchadenZuweisung WHERE SchadenID=" + ID + ";";
            bool boRet = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            return boRet;
        }
        ///<summary>clsSchaeden / SchadenInUse</summary>
        ///<remarks></remarks>
        public void DeleteSchaden()
        {
            string strSql = string.Empty;
            strSql = "Delete Schaeden WHERE ID=" + ID + ";";
            clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            string myBeschreibung = "Schaden gelöscht: Schaden-ID/Bezeichnung: [" + ID.ToString() + "] - [" + this.Bezeichnung + "]";
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
        }
        ///<summary>clsSchaeden / DeleteSchadenFromArtikel</summary>
        ///<remarks>Schaden von Artikel löschen</remarks>
        public void DeleteSchadenFromArtikel()
        {
            string strSql = string.Empty;
            strSql = "Delete SchadenZuweisung WHERE ID=" + SchadenZuweisungID + ";";
            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
                string myBeschreibung = "Schaden von Artikel gelöscht: ID/Schadenstext: [" + ID.ToString() + "] / [" + this.Bezeichnung.Trim() + "]";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);

                //Vita Schaden löschen
                //VITA
                clsLager lg = new clsLager();
                lg.InitSubClasses();
                lg.Artikel._GL_User = this._GL_User;
                lg.Artikel.ID = ArtikelID;
                lg.Artikel.GetArtikeldatenByTableID();
                lg.Eingang.LEingangTableID = lg.Artikel.LEingangTableID;
                lg.Eingang.FillEingang();
                string strSchaden = "- " + this.Bezeichnung;
                clsArtikelVita.ArtikelDelSchaden(BenutzerID, lg.Artikel.ID, lg.Eingang.LEingangID, lg.Artikel.LVS_ID, strSchaden);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void AddSchadenForRetour(clsArtikel myRetourArt)
        {
            //---Schäden des alten Artikels müssen auf den neuen gebucht werden
            DataTable dtSchaden = this.GetArtikelSchäden(true);
            //Schäden hinzufügen, falls vorhanden
            if (dtSchaden.Rows.Count > 0)
            {
                string strSql = string.Empty;
                string strSchadensTexte = string.Empty;
                foreach (DataRow row in dtSchaden.Rows)
                {
                    int iSchadenId = 0;
                    int.TryParse(row["SchadenID"].ToString(), out iSchadenId);
                    if (iSchadenId > 0)
                    {
                        strSql += this.AddSchadenZuweisungSqlString((int)myRetourArt.ID, iSchadenId);
                        strSchadensTexte += row["Bezeichnung"].ToString() + ", ";
                    }
                }

                //Eintrag wenn vorhanden
                if (!strSql.Equals(string.Empty))
                {
                    bool bRetrun = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "RetourArtikelSchaden", this.BenutzerID);
                    if (bRetrun)
                    {
                        clsArtikelVita.ArtikelAddSchaden(BenutzerID, myRetourArt.ID, myRetourArt.Eingang.LEingangID, myRetourArt.LVS_ID, strSchadensTexte);

                        //Add Logbucheintrag Exception
                        string myBeschreibung = "Artikelschaden hinterlegt: LVSNr/ID [Retoure]: [" + myRetourArt.ID.ToString() + "]";
                        myBeschreibung = myBeschreibung + Environment.NewLine + strSchadensTexte;
                        Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);
                    }
                }
            }
        }
        ///<summary>clsSchaeden / GetArtikelWithSchaden</summary>
        ///<remarks></remarks>
        public static List<decimal> GetArtikelWithSchaden(Globals._GL_USER myGLUser, decimal myLEingangTableID)
        {
            List<decimal> listReturn = new List<decimal>();
            string strSQL = string.Empty;
            strSQL = "SELECT DISTINCT ArtikelID FROM SchadenZuweisung " +
                                "WHERE ArtikelID IN (SELECT ID FROM Artikel WHERE LEingangTableID=" + (Int32)myLEingangTableID + ");";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "ArtikelMitSchaden");
            foreach (DataRow row in dt.Rows)
            {
                decimal decTmp = 0;
                Decimal.TryParse(row["ArtikelID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    listReturn.Add(decTmp);
                }
            }
            return listReturn;
        }



    }
}
