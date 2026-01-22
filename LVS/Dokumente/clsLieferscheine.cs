using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace LVS.Dokumente
{
    public class clsLieferscheine
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

        private decimal _AuftragID;
        private decimal _AuftragPos;

        private string _ZM;
        private string _Auflieger;
        private string _Fahrer;

        private string _AName1;
        private string _AName2;
        private string _AName3;
        private string _AStr;
        private string _APLZ;
        private string _AOrt;

        private string _VName1;
        private string _VName2;
        private string _VName3;
        private string _VStr;
        private string _VPLZ;
        private string _VOrt;

        private string _EName1;
        private string _EName2;
        private string _EName3;
        private string _EStr;
        private string _EPLZ;
        private string _EOrt;

        private string _LieferscheinNr;
        private string _DocName;
        private string _Ladenummer;
        private string _Notiz;
        private string _ZF;



        public DataSet dsLieferschein = new DataSet();
        public bool LfsExist = false;
        public bool boPrintNotiz = false;
        public decimal MandantenID;
        public decimal AuftragPosTableID;
        public DateTime Datum;

        private decimal _AP_ID;

        public string ZM
        {
            get { return _ZM; }
            set { _ZM = value; }
        }
        public string Auflieger
        {
            get { return _Auflieger; }
            set { _Auflieger = value; }
        }
        public string Fahrer
        {
            get { return _Fahrer; }
            set { _Fahrer = value; }
        }

        //Auftraggeber
        public string AName1
        {
            get { return _AName1; }
            set { _AName1 = value; }
        }
        public string AName2
        {
            get { return _AName2; }
            set { _AName2 = value; }
        }
        public string AName3
        {
            get { return _AName3; }
            set { _AName3 = value; }
        }
        public string AStr
        {
            get { return _AStr; }
            set { _AStr = value; }
        }
        public string APLZ
        {
            get { return _APLZ; }
            set { _APLZ = value; }
        }
        public string AOrt
        {
            get { return _AOrt; }
            set { _AOrt = value; }
        }


        //Versender
        public string VName1
        {
            get { return _VName1; }
            set { _VName1 = value; }
        }
        public string VName2
        {
            get { return _VName2; }
            set { _VName2 = value; }
        }
        public string VName3
        {
            get { return _VName3; }
            set { _VName3 = value; }
        }
        public string VStr
        {
            get { return _VStr; }
            set { _VStr = value; }
        }
        public string VPLZ
        {
            get { return _VPLZ; }
            set { _VPLZ = value; }
        }
        public string VOrt
        {
            get { return _VOrt; }
            set { _VOrt = value; }
        }
        //Empfänger
        public string EName1
        {
            get { return _EName1; }
            set { _EName1 = value; }
        }
        public string EName2
        {
            get { return _EName2; }
            set { _EName2 = value; }
        }
        public string EName3
        {
            get { return _EName3; }
            set { _EName3 = value; }
        }
        public string EStr
        {
            get { return _EStr; }
            set { _EStr = value; }
        }
        public string EPLZ
        {
            get { return _EPLZ; }
            set { _EPLZ = value; }
        }
        public string EOrt
        {
            get { return _EOrt; }
            set { _EOrt = value; }
        }


        public string LieferscheinNr
        {
            get { return _LieferscheinNr; }
            set { _LieferscheinNr = value; }
        }

        //Auftrag
        public decimal AuftragPos
        {
            get { return _AuftragPos; }
            set { _AuftragPos = value; }
        }
        public decimal AuftragID
        {
            get { return _AuftragID; }
            set { _AuftragID = value; }
        }


        public decimal AP_ID
        {
            get { return _AP_ID; }
            set { _AP_ID = value; }
        }

        public string Ladenummer
        {
            get { return _Ladenummer; }
            set { _Ladenummer = value; }
        }

        public string Notiz
        {
            get { return _Notiz; }
            set { _Notiz = value; }
        }

        public string ZF
        {
            get { return _ZF; }
            set { _ZF = value; }
        }
        public string DocName
        {
            get { return _DocName; }
            set { _DocName = value; }
        }


        public bool neutrLieferschein = false;
        public bool boOwnDoc = false;
        public bool boIsLieferschein = true;
        public DataTable dtArtikelDetails = new DataTable("Artikeldetails");
        public DataTable dtPrintdaten = new DataTable("Printdaten");

        decimal V_ADR_ID = 0;
        decimal E_ADR_ID = 0;
        /*************************************************************************************
         *                      Abholschein
         * **********************************************************************************/
        //
        //
        //
        public void GetAbholscheinDaten(decimal _AuftragPosTableID)
        {
            AuftragPosTableID = _AuftragPosTableID;
            AuftragPos = clsAuftragPos.GetAuftragPosByID(AuftragPosTableID); ;
            AuftragID = clsAuftragPos.GetAuftragIDByID(AuftragPosTableID);

            //ADR (Versender und Auftraggeber)
            GetADRForDoc();

            GetArtikelForLieferschein();
            //KommiDaten
            GetAP_IDfromKommiDatenForLieferschein();

            //decimal LfsNr = GetLieferscheinnummer();
            LieferscheinNr = "";

            //Versender
            VName1 = dsLieferschein.Tables["Versender"].Rows[0]["Name1"].ToString();
            VName2 = dsLieferschein.Tables["Versender"].Rows[0]["Name2"].ToString();
            VName3 = dsLieferschein.Tables["Versender"].Rows[0]["Name3"].ToString();
            VStr = dsLieferschein.Tables["Versender"].Rows[0]["Str"].ToString();
            VPLZ = dsLieferschein.Tables["Versender"].Rows[0]["PLZ"].ToString();
            VOrt = dsLieferschein.Tables["Versender"].Rows[0]["Ort"].ToString();

            //Empfänger hier Auftraggeber oder Auswahl
            //die richtige ADR ID wird schon im PrintCenter beim Erstellen des Printdaten-Tables gemacht
            EName1 = dsLieferschein.Tables["Empfänger"].Rows[0]["Name1"].ToString().Trim();
            EName2 = dsLieferschein.Tables["Empfänger"].Rows[0]["Name2"].ToString().Trim();
            EName3 = dsLieferschein.Tables["Empfänger"].Rows[0]["Name3"].ToString().Trim();
            EStr = dsLieferschein.Tables["Empfänger"].Rows[0]["Str"].ToString().Trim();
            EPLZ = dsLieferschein.Tables["Empfänger"].Rows[0]["PLZ"].ToString().Trim();
            EOrt = dsLieferschein.Tables["Empfänger"].Rows[0]["Ort"].ToString().Trim();
        }
        /******************************************************************************
         *                          Lieferschein
         * ****************************************************************************/
        //
        //
        public void GetLieferscheinDaten()
        {
            //ADR (Versender und Empfänger)
            GetADRForDoc();

            //Artikel
            GetArtikelForLieferschein();

            //KommiDaten
            GetAP_IDfromKommiDatenForLieferschein();
            LfsExist = clsLieferscheine.LieferscheinExist(AuftragPosTableID);

            //Wenn der Lieferschein bereits exisitiert, dann 
            if (LfsExist)
            {
                //Datum = GetLieferscheinDatum();
                LieferscheinNr = GetExistLieferscheinNr();
            }
            else
            {
                clsPrimeKeys lfsnr = new clsPrimeKeys();
                lfsnr._GL_User = this._GL_User;
                lfsnr.Mandanten_ID = MandantenID;
                lfsnr.GetNEWLfsNr();
                LieferscheinNr = lfsnr.LfsNr.ToString();
            }
            //Auftraggeber
            //AName1 = dsLieferschein.Tables["Auftraggeber"].Rows[0]["Name1"].ToString().Trim();
            //AName2 = dsLieferschein.Tables["Auftraggeber"].Rows[0]["Name2"].ToString().Trim();
            //AName3 = dsLieferschein.Tables["Auftraggeber"].Rows[0]["Name3"].ToString().Trim();
            //AStr = dsLieferschein.Tables["Auftraggeber"].Rows[0]["Str"].ToString().Trim();
            //APLZ = dsLieferschein.Tables["Auftraggeber"].Rows[0]["PLZ"].ToString().Trim();
            //AOrt = dsLieferschein.Tables["Auftraggeber"].Rows[0]["Ort"].ToString().Trim();

            //Versender
            VName1 = dsLieferschein.Tables["Versender"].Rows[0]["Name1"].ToString().Trim();
            VName2 = dsLieferschein.Tables["Versender"].Rows[0]["Name2"].ToString().Trim();
            VName3 = dsLieferschein.Tables["Versender"].Rows[0]["Name3"].ToString().Trim();
            VStr = dsLieferschein.Tables["Versender"].Rows[0]["Str"].ToString().Trim();
            VPLZ = dsLieferschein.Tables["Versender"].Rows[0]["PLZ"].ToString().Trim();
            VOrt = dsLieferschein.Tables["Versender"].Rows[0]["Ort"].ToString().Trim();

            //Empfänger
            EName1 = dsLieferschein.Tables["Empfänger"].Rows[0]["Name1"].ToString().Trim();
            EName2 = dsLieferschein.Tables["Empfänger"].Rows[0]["Name2"].ToString().Trim();
            EName3 = dsLieferschein.Tables["Empfänger"].Rows[0]["Name3"].ToString().Trim();
            EStr = dsLieferschein.Tables["Empfänger"].Rows[0]["Str"].ToString().Trim();
            EPLZ = dsLieferschein.Tables["Empfänger"].Rows[0]["PLZ"].ToString().Trim();
            EOrt = dsLieferschein.Tables["Empfänger"].Rows[0]["Ort"].ToString().Trim();

            //Kommisssionsdaten / Resourcen
            if (dsLieferschein.Tables["Kommission"].Rows.Count > 0)
            {
                ZM = dsLieferschein.Tables["Kommission"].Rows[0]["ZM"].ToString();
                Auflieger = dsLieferschein.Tables["Kommission"].Rows[0]["Auflieger"].ToString();
                Fahrer = dsLieferschein.Tables["Kommission"].Rows[0]["Nachname"].ToString() + " , " +
                         dsLieferschein.Tables["Kommission"].Rows[0]["Vorname"].ToString();
            }
            else
            {
                ZM = string.Empty;
                Auflieger = string.Empty;
                Fahrer = string.Empty;
            }

            //Eintrag in DB Lieferschein
            if ((!LfsExist) && (!boOwnDoc))
            {
                AddLieferschein();
                clsKommission.UpdateDocsByAuftragPosTableID(this._GL_User, true, AP_ID);
            }

        }
        //
        private string GetExistLieferscheinNr()
        {
            string strVal = string.Empty;
            if (AuftragPosTableID > 0)
            {
                string strSQL = string.Empty;
                strSQL = "Select LfsNr FROM Lieferschein WHERE AP_ID='" + AP_ID + "';";
                strVal = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            }
            return strVal;
        }
        //
        private DateTime GetExistLfsDatum()
        {
            DateTime dtVal = new DateTime();
            if (AuftragPosTableID > 0)
            {
                string strSQL = string.Empty;
                strSQL = "Select Datum FROM Lieferschein WHERE AP_ID='" + AuftragPosTableID + "';";
                string strVal = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                dtVal = Convert.ToDateTime(strVal);
            }
            return dtVal;
        }

        //
        //---------- Artikel --------------
        //
        private void GetArtikelForLieferschein()
        {
            if (AuftragID > 0)
            {
                if (dsLieferschein.Tables["Artikel"] != null)
                {
                    dsLieferschein.Tables.Remove("Artikel");
                }

                DataTable dt = clsArtikel.GetDataTableArtikelForDocPrint(this._GL_User, AuftragPosTableID);
                dt.TableName = "Artikel";
                if (dt.Columns["Zusatz"] == null)
                {
                    System.Data.DataColumn col = new System.Data.DataColumn();
                    col.DataType = System.Type.GetType("System.String");
                    col.Caption = "Zusatz";
                    col.ColumnName = "Zusatz";
                    dt.Columns.Add(col);
                }
                if (dt.Columns["Abmessungen"] == null)
                {
                    System.Data.DataColumn col1 = new System.Data.DataColumn();
                    col1.DataType = System.Type.GetType("System.String");
                    col1.Caption = "Abmessungen";
                    col1.ColumnName = "Abmessungen";
                    dt.Columns.Add(col1);
                }
                if (dt.Columns["Gut"] == null)
                {
                    System.Data.DataColumn col2 = new System.Data.DataColumn();
                    col2.DataType = System.Type.GetType("System.String");
                    col2.Caption = "Gut";
                    col2.ColumnName = "Gut";
                    dt.Columns.Add(col2);
                }

                string strZusatz = string.Empty;
                string strAbmessungen = string.Empty;
                string strDicke = string.Empty;
                string strBreite = string.Empty;
                string strLänge = string.Empty;
                string strHöhe = string.Empty;
                string strGArt = string.Empty;
                string strGut = string.Empty;  // Gut + GutZusatz
                string strGutZusatz = string.Empty;

                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    for (Int32 k = 0; k <= dt.Columns.Count - 1; k++)
                    {
                        String strSpalteDT = string.Empty;
                        strSpalteDT = dt.Columns[k].ColumnName.ToString();

                        for (Int32 j = 0; j <= dtArtikelDetails.Rows.Count - 1; j++)
                        {
                            //Vergleich Spaltenname
                            String strSpalteDTDetails = string.Empty;
                            strSpalteDTDetails = dtArtikelDetails.Rows[j]["Spalte"].ToString();
                            bool boSelected = false;
                            bool boStandard = false;
                            boSelected = (bool)dtArtikelDetails.Rows[j]["Selected"];
                            boStandard = (bool)dtArtikelDetails.Rows[j]["Standard"];

                            if (strSpalteDT == strSpalteDTDetails)
                            {
                                //Abmessungen
                                /***************************************************************
                                 * Abmessungen sollen nur angezeigt werden, wenn der Wert >0
                                 * *************************************************************/
                                decimal tmpDec = 0.0M;
                                switch (strSpalteDT)
                                {
                                    case "Dicke":
                                        tmpDec = (decimal)dt.Rows[i][strSpalteDT];
                                        strDicke = Functions.CheckAndGetDimension(tmpDec);
                                        break;
                                    case "Breite":
                                        tmpDec = (decimal)dt.Rows[i][strSpalteDT];
                                        strBreite = Functions.CheckAndGetDimension(tmpDec);
                                        break;
                                    case "Laenge":
                                        tmpDec = (decimal)dt.Rows[i][strSpalteDT];
                                        strLänge = Functions.CheckAndGetDimension(tmpDec);
                                        break;
                                    case "Hoehe":
                                        tmpDec = (decimal)dt.Rows[i][strSpalteDT];
                                        strHöhe = Functions.CheckAndGetDimension(tmpDec);
                                        break;
                                }

                                //Standard

                                if ((boSelected) &
                                    (!boStandard))
                                {
                                    strZusatz = strZusatz +
                                                dtArtikelDetails.Rows[j]["Text"].ToString() + ": " +
                                                dt.Rows[i][strSpalteDT].ToString() + ";" + Environment.NewLine;
                                }

                                // strGArt=dt.Rows[j]["GArt"].ToString();
                                j = dtArtikelDetails.Rows.Count;
                            }
                        }

                    }
                    /*********************************************
                     * Eintrag von strZusatz in die Zusatzspalte
                     * nur Lieferschein
                     * *******************************************/
                    if (boIsLieferschein)
                    {
                        dt.Rows[i]["Zusatz"] = strZusatz;
                    }
                    else
                    {
                        dt.Rows[i]["Zusatz"] = string.Empty;
                    }

                    /************************************************
                     * Dem Gut soll eine Zusatzzeile hinzugefügt werden
                     * für weitere Güterartinfos
                     * **********************************************/
                    strGArt = dt.Rows[i]["GArt"].ToString();
                    strGutZusatz = dt.Rows[i]["GutZusatz"].ToString();

                    if (strGutZusatz != string.Empty)
                    {
                        strGut = strGArt +
                                 Environment.NewLine +
                                 strGutZusatz;
                    }
                    else
                    {
                        strGut = strGArt;
                    }
                    //Abmessungen
                    //Maßzahl ermitteln 
                    Int32 iMassZahl = 4;
                    if (clsGut.ExistGArtByBezeichnung(this._GL_User, strGArt))
                    {
                        //Baustelle
                        //GüterartID fehlt
                        //iMassZahl = clsGut.GetGArtMZByID(clsGut.ExistGArtByBezeichnung(strGArt));
                    }
                    /***************************************************************
                     * Abmessungen sollen nur angezeigt werden, wenn der Wert >0
                     * *************************************************************/
                    switch (iMassZahl)
                    {
                        case 2:
                            //Dicke+Breite
                            if (strDicke != string.Empty)
                            {
                                strDicke = strDicke + " / ";
                            }
                            strAbmessungen = strDicke + strBreite;
                            break;

                        case 3:
                            //Dicke+Breite+Länge
                            if (strDicke != string.Empty)
                            {
                                strDicke = strDicke + " / ";
                            }
                            if (strBreite != string.Empty)
                            {
                                strBreite = strBreite + " / ";
                            }
                            strAbmessungen = strDicke + strBreite + strLänge;
                            break;

                        default:
                            //Dicke+Breite+Länge
                            if (strDicke != string.Empty)
                            {
                                strDicke = strDicke + " / ";
                            }
                            if (strBreite != string.Empty)
                            {
                                strBreite = strBreite + " / ";
                            }
                            if (strLänge != string.Empty)
                            {
                                strLänge = strLänge + " / ";
                            }
                            strAbmessungen = strDicke + strBreite + strLänge + strHöhe;
                            break;

                    }
                    dt.Rows[i]["Abmessungen"] = strAbmessungen;
                    dt.Rows[i]["Gut"] = strGut;

                    strZusatz = string.Empty;
                    strAbmessungen = string.Empty;
                    strDicke = string.Empty;
                    strBreite = string.Empty;
                    strLänge = string.Empty;
                    strHöhe = string.Empty;
                    strGArt = string.Empty;
                    strGut = string.Empty;
                    strGutZusatz = string.Empty;

                }
                //dtDetails enthält die Spalten der DB Artikel
                dsLieferschein.Tables.Add(dt);
            }
        }
        /*********************************************************************************
         *                        ADR
         *                        
         * *******************************************************************************/
        //
        //----- ADR (Versender / Empfangs) -----------
        //
        //private void GetADRForLieferschein()
        private void GetADRForDoc()
        {
            if (AuftragID > 0)
            {
                DataSet ds;
                //Versender
                ds = clsADR.ReadADRbyID(V_ADR_ID);
                InitADRTable("Versender");
                SetRowToADRTable("Versender", ds);
                ds.Clear();
                //Empfänger
                ds = clsADR.ReadADRbyID(E_ADR_ID);
                InitADRTable("Empfänger");
                SetRowToADRTable("Empfänger", ds);
                ds.Clear();
            }
        }
        //
        //
        //

        //
        //------------ Init ADR Table ----------
        //
        private void InitADRTable(string TableName)
        {
            if (dsLieferschein.Tables[TableName] != null)
            {
                dsLieferschein.Tables.Remove(TableName);
            }
            dsLieferschein.Tables.Add(TableName);
            dsLieferschein.Tables[TableName].Columns.Add("ID");
            dsLieferschein.Tables[TableName].Columns.Add("FBez");
            dsLieferschein.Tables[TableName].Columns.Add("Name1");
            dsLieferschein.Tables[TableName].Columns.Add("Name2");
            dsLieferschein.Tables[TableName].Columns.Add("Name3");
            dsLieferschein.Tables[TableName].Columns.Add("Str");
            dsLieferschein.Tables[TableName].Columns.Add("PLZ");
            dsLieferschein.Tables[TableName].Columns.Add("Ort");
            dsLieferschein.Tables[TableName].Columns.Add("zHd");
        }
        //
        private void SetRowToADRTable(string TableName, DataSet ds)
        {
            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row;
                row = dsLieferschein.Tables[TableName].NewRow();
                row["ID"] = ds.Tables[0].Rows[i]["ID"];
                row["FBez"] = ds.Tables[0].Rows[i]["FBez"];
                row["Name1"] = ds.Tables[0].Rows[i]["Name1"];
                row["Name2"] = ds.Tables[0].Rows[i]["Name2"];
                row["Name3"] = ds.Tables[0].Rows[i]["Name3"];
                row["Str"] = ds.Tables[0].Rows[i]["Str"];
                row["PLZ"] = ds.Tables[0].Rows[i]["PLZ"];
                row["Ort"] = ds.Tables[0].Rows[i]["Ort"];
                row["zHd"] = string.Empty;
                dsLieferschein.Tables[TableName].Rows.Add(row);
            }
        }
        /****************************************************************************/
        //
        //---------- Set Kommi for DS Lieferschein ---------
        //
        private void GetAP_IDfromKommiDatenForLieferschein()
        {

            DataTable dt = clsKommission.GetKommiDatenForLieferschein(this._GL_User, AuftragPosTableID);
            dt.TableName = "Kommission";
            if (dsLieferschein.Tables["Kommission"] != null)
            {
                dsLieferschein.Tables.Remove("Kommission");
            }
            //dsLieferschein.Tables.Add(dt);
            AP_ID = (decimal)dt.Rows[0]["ID"];
            dsLieferschein.Tables.Add(dt);
        }
        /***********************************************************************************************
         *                                  DB Lieferschein
         * *********************************************************************************************/
        //
        //----------- LIeferscheinnummer ----------
        //
        public decimal GetLieferscheinnummer()
        {
            decimal neueLSNr = 0;
            try
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;

                if (LfsExist)
                {
                    Command.CommandText = "SELECT LfsNr FROM Lieferschein WHERE AP_ID='" + AP_ID + "'";
                }
                else
                {
                    Command.CommandText = "DECLARE @NewID table( NewLSID decimal ); " +
                                          "UPDATE Lieferscheinnummer SET ID= ID + 1 " +
                                          "OUTPUT INSERTED.ID INTO @NewID; " +
                                          "SELECT * FROM @NewId;";
                }

                Globals.SQLcon.Open();

                object obj = Command.ExecuteScalar();

                if ((obj == null) | (obj is DBNull))
                {
                    neueLSNr = 0;
                }
                else
                {
                    neueLSNr = (decimal)obj;
                }
                Command.Dispose();
                Globals.SQLcon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return neueLSNr;
        }
        //
        //
        //
        private void AddLieferschein()
        {
            //User noch frei
            decimal DoneUser = _GL_User.User_ID;
            decimal drUser = 0;
            try
            {
                SqlCommand InsertCommand = new SqlCommand();
                InsertCommand.Connection = Globals.SQLcon.Connection;
                InsertCommand.CommandText = "INSERT INTO Lieferschein " +
                                                         "(AP_ID, LfsNr, Datum, DoneUser, drDate, drUser) " +
                                                         "VALUES " +
                                                         "('" +
                                                         AP_ID + "', '" +
                                                         LieferscheinNr + "', '" +
                                                         DateTime.Today + "', '" +
                                                         DoneUser + "', '" +
                                                         DateTime.MaxValue + "', '" +
                                                         drUser + "')";

                Globals.SQLcon.Open();
                InsertCommand.ExecuteNonQuery();

                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        //
        //---------- Lieferschein schon erstellt ? -----------
        //
        public static bool LieferscheinExist(decimal AP_ID)
        {
            bool exist = false;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID FROM Lieferschein WHERE AP_ID='" + AP_ID + "'";

            Globals.SQLcon.Open();
            object obj = Command.ExecuteScalar();
            if (obj != null)
            {
                exist = true;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return exist;
        }
        //
        //---------- Lieferschein Datum ---------------
        //
        public DateTime GetLieferscheinDatum()
        {
            DateTime LfsDate;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT Datum FROM Lieferschein WHERE AP_ID='" + AP_ID + "'";

            Globals.SQLcon.Open();
            if (Command.ExecuteScalar() == null)
            {
                LfsDate = DateTime.Today;
            }
            else
            {
                LfsDate = (DateTime)Command.ExecuteScalar();
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return LfsDate;
        }
        //
        //-------------- Delete Lieferschein ----------
        //
        public static void DeleteLieferscheinByLfsNr(decimal LfsNr)
        {
            if (LfsNr > 0)
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = "DELETE FROM Lieferschein WHERE LfsNr='" + LfsNr + "'";
                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
        }
        public static void DeleteLieferscheinByAP_ID(decimal AP_ID)
        {
            if (AP_ID > 0)
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = "DELETE FROM Lieferschein WHERE AP_ID='" + AP_ID + "'";
                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
        }
        //
        //--------- Update Lieferschein Druck Datum und User ----------------
        //
        public static void UpdatePrint(decimal AP_ID)
        {
            if (AP_ID > 0)
            {
                decimal drUser = 0;
                try
                {
                    //--- initialisierung des sqlcommand---
                    SqlCommand Command = new SqlCommand();
                    Command.Connection = Globals.SQLcon.Connection;

                    //----- SQL Abfrage -----------------------
                    Command.CommandText = "Update [Sped4].[dbo].[Lieferschein] SET drDate='" + DateTime.Now + "', " +
                                                                   "drUser ='" + drUser + "' " +
                                                                   "WHERE AP_ID='" + AP_ID + "'";

                    Globals.SQLcon.Open();
                    Command.ExecuteNonQuery();
                    Command.Dispose();
                    Globals.SQLcon.Close();
                    if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                    {
                        Command.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        //
        //----------------------- Druck Lieferschein rückgängig --------------------------
        //
        public static void DeletePrint(decimal AP_ID)
        {
            if (AP_ID > 0)
            {
                //decimal drUser = 0;
                try
                {
                    //--- initialisierung des sqlcommand---
                    SqlCommand Command = new SqlCommand();
                    Command.Connection = Globals.SQLcon.Connection;

                    //----- SQL Abfrage -----------------------
                    /**
                      Command.CommandText = "Update [Sped4].[dbo].[Lieferschein] SET drDate='" + DateTime.MaxValue + "', " +
                                                                   "drUser ='" + drUser + "' " +
                                                                   "WHERE AP_ID='" + AP_ID + "'";
                      ***/
                    Command.CommandText = "Delete Lieferschein WHERE AP_ID='" + AP_ID + "'";

                    Globals.SQLcon.Open();
                    Command.ExecuteNonQuery();
                    Command.Dispose();
                    Globals.SQLcon.Close();
                    if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                    {
                        Command.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        public void SetPrintDaten()
        {
            //Info auf Document
            if (dtPrintdaten.Rows.Count > 0)
            {
                AuftragID = (decimal)dtPrintdaten.Rows[0]["AuftragID"];
                AuftragPos = (decimal)dtPrintdaten.Rows[0]["AuftragPos"];
                ZM = (string)dtPrintdaten.Rows[0]["ZM"].ToString();
                Auflieger = (string)dtPrintdaten.Rows[0]["Auflieger"].ToString();
                Fahrer = (string)dtPrintdaten.Rows[0]["Fahrer"].ToString();

                DocName = (string)dtPrintdaten.Rows[0]["DocName"].ToString().ToUpper();
                Notiz = (string)dtPrintdaten.Rows[0]["Notiz"].ToString();

                if ((string)dtPrintdaten.Rows[0]["Ladenummer"] != string.Empty)
                {
                    Ladenummer = "Ladenummer: " + (string)dtPrintdaten.Rows[0]["Ladenummer"].ToString();
                }
                else
                {
                    Ladenummer = string.Empty;
                }
                if ((string)dtPrintdaten.Rows[0]["ZF"].ToString() != string.Empty)
                {
                    ZF = "Zeitfenster: " + (string)dtPrintdaten.Rows[0]["ZF"].ToString() + " Uhr";
                }
                else
                {
                    ZF = string.Empty;
                }
                V_ADR_ID = (decimal)dtPrintdaten.Rows[0]["ADR_ID_V"];
                E_ADR_ID = (decimal)dtPrintdaten.Rows[0]["ADR_ID_E"];
                boPrintNotiz = (bool)dtPrintdaten.Rows[0]["PrintNotiz"];

                if (!boPrintNotiz)
                {
                    Notiz = string.Empty;
                    ZF = string.Empty;
                    //Ladenummer = string.Empty;
                }

                if (boIsLieferschein)
                {
                    Ladenummer = string.Empty;
                }
                else //Abholschein
                {

                }

            }
        }

    }
}
