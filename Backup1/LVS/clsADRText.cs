using LVS.Helper;
using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsADRText
    {
        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GLSystem;
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
        public decimal AdrID { get; set; }
        public decimal DocumentArtID { get; set; }
        public string DocumentArtName { get; set; }
        public string Text { get; set; }
        private DataTable _dtAdrText;
        public DataTable dtAdrText
        {
            get
            {
                _dtAdrText = new DataTable();
                string strSql = string.Empty;
                strSql = "SELECT ADRText.*, Arbeitsbereich.Name FROM ADRText  " +
                                        " LEFT JOIN Arbeitsbereich on Arbeitsbereich.ID=ADRText.ArbeitsbereichID " +
                                        "WHERE " +
                                            "ADRText.AdrID=" + AdrID + " ";

                _dtAdrText = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Text");
                return _dtAdrText;
            }
            set { _dtAdrText = value; }
        }

        public clsArbeitsbereiche Arbeitsbereich;
        private int _ArbeitsbereichID;
        public int ArbeitsbereichID
        {
            get
            {
                return _ArbeitsbereichID;
            }
            set
            {
                _ArbeitsbereichID = value;
                this.Arbeitsbereich = new clsArbeitsbereiche();

                if (_ArbeitsbereichID > 0)
                {
                    this.Arbeitsbereich = new clsArbeitsbereiche();
                    this.Arbeitsbereich.InitCls(this._GL_User, (decimal)_ArbeitsbereichID);
                }

            }
        }
        public bool UseForAll { get; set; }

        public bool IsReceiver { get; set; }

        private List<string> _ListUsedDocArtID { get; set; }
        public List<string> ListUsedDocArtID
        {
            get { return FillListUseDocArt(); }
            set { _ListUsedDocArtID = value; }
        }
        private Dictionary<Int32, string> _DictUnUsedDocumentArtID;
        public Dictionary<Int32, string> DictUnUsedDocumentArtID
        {
            get
            {
                _DictUnUsedDocumentArtID = new Dictionary<int, string>();
                _DictUnUsedDocumentArtID.Add(-1, "-Dokument wählen-");
                FillListUseDocArt();

                Dictionary<Int32, string> dictDocArtIDSource = helper_DokumentenArt.DicLagerDocumentArt();
                if (this.Sys.Client.Modul.Print_OldVersion)
                {
                    dictDocArtIDSource = helper_DokumentenArt.DicLagerDocumentArt();
                    //Check welche von den Lagerdokumenten bereits einen Textbaustein zugewiesen haben
                    foreach (KeyValuePair<Int32, string> tmpPair in dictDocArtIDSource)
                    {
                        if (this.Sys != null)
                        {
                            if (this.Sys.ListDocArtInUse.IndexOf(tmpPair.Value.ToString()) > -1)
                            {
                                _DictUnUsedDocumentArtID.Add(tmpPair.Key, tmpPair.Value);
                            }
                        }
                    }
                }
                else
                {
                    dictDocArtIDSource = helper_DokumentenArt.DicLagerDocumentArt();
                    this.Sys.ReportDocSetting.InitClass(this._GL_User, this._GLSystem, this.Sys, this.AdrID, this.Sys.AbBereich.ID);
                    //Check welche von den Lagerdokumenten bereits einen Textbaustein zugewiesen haben
                    foreach (KeyValuePair<Int32, string> tmpPair in dictDocArtIDSource)
                    {
                        if (this.Sys != null)
                        {
                            if (this.Sys.ListDocArtInUse.IndexOf(tmpPair.Value.ToString()) > -1)
                            {
                                _DictUnUsedDocumentArtID.Add(tmpPair.Key, tmpPair.Value);
                            }
                        }
                    }
                }

                return _DictUnUsedDocumentArtID;
            }
            set { _DictUnUsedDocumentArtID = value; }
        }

        //******        if (this.Sys.Client.Modul.Print_OldVersion)
        /*******************************************
                 {0,  "<Dokumentenart wählen>"},
                 {20, "Adressliste"},
                 {13, "Ausgangsliste"},
                 {5,  "AusgangLfs"},
                 {2,  "Auslagerungsschein"},
                 {4,  "Auslagerungsanzeige"},
                 {10, "Bescheinigung"},
                 {17, "Bestand"},
                 {16, "CMRFrachtbrief"},
                 {1,  "Einlagerungsschein"},                           
                 {3,  "Einlagerungsanzeige"}, 
                 {14, "Eingangsliste"},
                 {19, "Inventur"},
                 {18, "Journal"},
                 {15, "KVOFrachtbrief"},
                 {6,  "Kunden-Ausgangslieferschein"},
                 {7,  "Lagerrechnung"},
                 {11, "Lagerschein-§475-BGB"},
                 {12, "Manuellerechnung"},
                 {21, "ManuelleGutschrift"},
                 {9,  "RGAnhang"},
                 {22, "RGBuch"},
                 {8,  "Speditionsrechnung"}
         * *********************************************/



        public string Lagerrechnung_Text { get; set; }
        public string RechnungManuell_Text { get; set; }
        public string AusgangsLfs_Text { get; set; }
        public string Adressliste_Text { get; set; }
        public string Ausgangsliste_Text { get; set; }
        public string Auslagerungsschein_Text { get; set; }
        public string Auslagerungsanzeige_Text { get; set; }
        public string Bescheinigung_Text { get; set; }
        public string Bestand_Text { get; set; }
        public string CMRFrachtbrief_Text { get; set; }
        public string Einlagerungsschein_Text { get; set; }
        public string Einlagerungsanzeige_Text { get; set; }
        public string Eingangsliste_Text { get; set; }
        public string Inventur_Text { get; set; }
        public string Journal_Text { get; set; }
        public string KVOFrachtbrief_Text { get; set; }
        public string Lagerschein_475BGB_Text { get; set; }
        public string GSManuell_Text { get; set; }
        public string RGAnhang { get; set; }
        public string RGBuch { get; set; }
        public string SpeditionsRechnung_Text { get; set; }
        // string Rechnung_Text { get; set; }

        /************************************************************************
         *                  Methoden / Prozedure
         * *********************************************************************/
        ///<summary>clsADRText / InitClass</summary>
        ///<remarks></remarks>>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, clsSystem mySys, decimal myAdrID)
        {
            this._GL_User = myGLUser;
            this._GLSystem = myGLSystem;
            this.AdrID = myAdrID;
            this.Sys = mySys;
            if (myAdrID > 0)
            {
                FillByAdrID();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public clsADRText Copy()
        {
            return (clsADRText)this.MemberwiseClone();
        }
        ///<summary>clsADRText / Add</summary>
        ///<remarks></remarks>>
        public void Add()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO ADRText (AdrID, DocumentArtID, DocumentArtName, Text, ArbeitsbereichID, UseForAll, IsReceiver) " +
                                            "VALUES (" + AdrID +
                                                    ", " + DocumentArtID +
                                                    ", '" + DocumentArtName + "'" +
                                                    ", '" + Text + "'" +
                                                    ", " + this.ArbeitsbereichID +
                                                    ", " + Convert.ToInt32(this.UseForAll) +
                                                    ", " + Convert.ToInt32(this.IsReceiver) +
                                                    ");";
            strSql = strSql + " Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
                Fill();
                //Add Logbucheintrag Eintrag
                string logBeschreibung = "Adresse Textbaustein: " + DocumentArtName + " für ADR-ID[" + AdrID + "] hinzugefügt";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), logBeschreibung);
            }
        }
        ///<summary>clsADRText / Update</summary>
        ///<remarks></remarks>
        public void Update()
        {
            string strSql = string.Empty;
            strSql = "Update ADRText SET AdrID=" + AdrID +
                                        ", DocumentArtID=" + DocumentArtID +
                                        ", DocumentArtName='" + DocumentArtName + "'" +
                                        ", Text='" + Text + "'" +
                                        ", ArbeitsbereichID = " + this.ArbeitsbereichID +
                                        ", UseForAll = " + Convert.ToInt32(this.UseForAll) +
                                        ", IsReceiver = " + Convert.ToInt32(this.IsReceiver) +
                                                        " WHERE ID=" + ID + ";";

            clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            //Add Logbucheintrag update
            string logBeschreibung = "Adresse Textbaustein: " + DocumentArtName + " für ADR-ID[" + AdrID + "]  geändert";
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), logBeschreibung);
            this.Fill();
        }
        ///<summary>clsADRText / FillByAdrID</summary>
        ///<remarks></remarks>>
        public void FillByAdrID()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT TOP(1) * FROM ADRText WHERE AdrID=" + this.AdrID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Text");
            SetClassValue(ref dt);
        }
        ///<summary>clsADRText / Fill</summary>
        ///<remarks></remarks>>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ADRText WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Text");
            SetClassValue(ref dt);
        }
        ///<summary>clsADRText / SetClassValue</summary>
        ///<remarks></remarks>>
        private void SetClassValue(ref DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.AdrID = (decimal)dt.Rows[i]["AdrID"];
                this.DocumentArtID = (decimal)dt.Rows[i]["DocumentArtID"];
                this.DocumentArtName = dt.Rows[i]["DocumentArtName"].ToString();
                this.Text = dt.Rows[i]["Text"].ToString();
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ArbeitsbereichID"].ToString(), out iTmp);
                this.ArbeitsbereichID = iTmp;
                this.UseForAll = (bool)dt.Rows[i]["UseForAll"];
                this.IsReceiver = (bool)dt.Rows[i]["IsReceiver"];
            }
            if (this.AdrID > 0)
            {
                DataTable dtText = this.dtAdrText;
                foreach (DataRow row in dtText.Rows)
                {
                    Int32 iTmp = 0;
                    Int32.TryParse(row["DocumentArtID"].ToString(), out iTmp);

                    if (iTmp > 0)
                    {
                        switch (iTmp)
                        {
                            //Einlagerungsschein
                            case 1:
                                this.Einlagerungsschein_Text = row["Text"].ToString();
                                break;

                            ////--- neu clsDocKey
                            ////Eingang
                            //case 107:
                            //case 108:
                            //case 109:
                            //case 110:
                            //case 111:
                            //case 112:
                            //case 113:
                            //case 114:
                            //case 115:
                            //    this.Einlagerungsschein_Text = row["Text"].ToString();
                            //    break;

                            //Auslagerungsschein
                            case 2:
                                this.Auslagerungsschein_Text = row["Text"].ToString();
                                break;
                            //Einlagerungsanzeige
                            case 3:
                                this.Einlagerungsanzeige_Text = row["Text"].ToString();
                                break;
                            //Auslagerungsanzeige
                            case 4:
                                this.Auslagerungsanzeige_Text = row["Text"].ToString();
                                break;
                            //AusgangsLfs
                            case 5:
                            case 204:
                                this.AusgangsLfs_Text = row["Text"].ToString();
                                break;
                            //Kunden Ausgansglieferschein
                            case 6:
                                //this.AusgangsLfs_Text = row["Text"].ToString();
                                break;
                            //Lagerrechnung
                            case 7:
                            case 301:
                                this.Lagerrechnung_Text = row["Text"].ToString();
                                break;
                            //Speditionsrechnung
                            case 8:
                                this.SpeditionsRechnung_Text = row["Text"].ToString();
                                break;
                            //RGAnhang
                            case 9:
                                this.RGAnhang = row["Text"].ToString();
                                break;
                            //Bescheinigung
                            case 10:
                                this.Bescheinigung_Text = row["Text"].ToString();
                                break;
                            //Lagerschein-§475-BGB
                            case 11:
                                this.Lagerschein_475BGB_Text = row["Text"].ToString();
                                break;
                            //manuelle Rechnung
                            case 12:
                            case 302:
                                this.RechnungManuell_Text = row["Text"].ToString();
                                break;
                            //Ausgangsliste
                            case 13:
                            case 205:
                                this.Ausgangsliste_Text = row["Text"].ToString();
                                break;
                            //Eingangsliste
                            case 14:
                            case 112:
                                this.Eingangsliste_Text = row["Text"].ToString();
                                break;
                            //KVOFrachtbrief
                            case 15:
                            case 208:
                                this.KVOFrachtbrief_Text = row["Text"].ToString();
                                break;

                            default:
                                break;

                        }
                    }
                }
            }
        }
        ///<summary>clsADRText / Fill</summary>
        ///<remarks></remarks>>
        public List<string> FillListUseDocArt()
        {
            List<string> listTmp = new List<string>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            //....DocumentArtID = DocKeyId
            strSql = "SELECT DocumentArtID FROM ADRText WHERE AdrID=" + AdrID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Text");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                string strTmpArtID = dt.Rows[i]["DocumentArtID"].ToString();
                listTmp.Add(strTmpArtID);
            }
            return listTmp;
        }
        ///<summary>clsADRText / DeleteADR</summary> 
        ///<remarks></remarks>>
        public void Delete()
        {
            string strSql = string.Empty;
            strSql = "DELETE FROM ADRText WHERE ID=" + ID;
            bool bDeleteOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);

            if (bDeleteOK)
            {
                //Add Logbucheintrag Löschen
                string logBeschreibung = "Adressen Textbaustein: " + this.DocumentArtName + " ID[" + this.ID + "] gelöscht";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), logBeschreibung);
            }
        }


    }
}
