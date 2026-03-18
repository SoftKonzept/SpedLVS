using System;
using System.Data;

namespace LVS
{
    public class clsLagerOrt
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

        public clsWerk Werk = new clsWerk();
        private decimal _LagerPlatzID;
        private string _WerkBezeichnung;
        private string _HalleBezeichnung;
        private string _ReiheBezeichnung;
        private string _EbeneBezeichnung;
        private string _PlatzBezeichnung;
        private string _LagerPlatzBezeichungKomplett;
        private string _LagerPlatzBezeichungListe;
        private decimal _WerkID;
        private decimal _ArtikelID;
        private string _LOTable;  //Lagerort Table    

        public decimal LagerPlatzID
        {
            get { return _LagerPlatzID; }
            set { _LagerPlatzID = value; }
        }
        public decimal _exLagerPlatzID
        {
            get { return _exLagerPlatzID; }
            set { _exLagerPlatzID = value; }
        }
        public decimal WerkID
        {
            get { return _WerkID; }
            set { _WerkID = value; }
        }
        public string WerkBezeichnung
        {
            get { return _WerkBezeichnung; }
            set { _WerkBezeichnung = value; }
        }
        public string HalleBezeichnung
        {
            get { return _HalleBezeichnung; }
            set { _HalleBezeichnung = value; }
        }
        public string ReiheBezeichnung
        {
            get { return _ReiheBezeichnung; }
            set { _ReiheBezeichnung = value; }
        }
        public string EbeneBezeichnung
        {
            get { return _EbeneBezeichnung; }
            set { _EbeneBezeichnung = value; }
        }
        public string PlatzBezeichnung
        {
            get { return _PlatzBezeichnung; }
            set { _PlatzBezeichnung = value; }
        }
        public string LagerPlatzBezeichungKomplett
        {
            get
            {
                _LagerPlatzBezeichungKomplett = string.Empty;
                //Check in wie weit die entsprechenden Lagerortbezeichnungen vorhanden sind 
                //und entsprechend den String für die Platzbezeichnung aufbauen
                if (PlatzBezeichnung != string.Empty)
                {
                    _LagerPlatzBezeichungKomplett = _LagerPlatzBezeichungKomplett + PlatzBezeichnung;
                }
                if (EbeneBezeichnung != string.Empty)
                {
                    if (_LagerPlatzBezeichungKomplett != string.Empty)
                    {
                        _LagerPlatzBezeichungKomplett = EbeneBezeichnung + " | " + _LagerPlatzBezeichungKomplett;
                    }
                    else
                    {
                        _LagerPlatzBezeichungKomplett = EbeneBezeichnung;
                    }
                }
                else
                {
                    _LagerPlatzBezeichungKomplett = EbeneBezeichnung;
                }
                if (ReiheBezeichnung != string.Empty)
                {
                    if (_LagerPlatzBezeichungKomplett != string.Empty)
                    {
                        _LagerPlatzBezeichungKomplett = ReiheBezeichnung + " | " + _LagerPlatzBezeichungKomplett;
                    }
                    else
                    {
                        _LagerPlatzBezeichungKomplett = ReiheBezeichnung;
                    }
                }
                else
                {
                    _LagerPlatzBezeichungKomplett = ReiheBezeichnung;
                }
                if (HalleBezeichnung != string.Empty)
                {
                    if (_LagerPlatzBezeichungKomplett != string.Empty)
                    {
                        _LagerPlatzBezeichungKomplett = HalleBezeichnung + " | " + _LagerPlatzBezeichungKomplett;
                    }
                    else
                    {
                        _LagerPlatzBezeichungKomplett = HalleBezeichnung;
                    }
                }
                else
                {
                    _LagerPlatzBezeichungKomplett = HalleBezeichnung;
                }
                if (WerkBezeichnung != string.Empty)
                {
                    if (_LagerPlatzBezeichungKomplett != string.Empty)
                    {
                        _LagerPlatzBezeichungKomplett = WerkBezeichnung + " | " + _LagerPlatzBezeichungKomplett;
                    }
                    else
                    {
                        _LagerPlatzBezeichungKomplett = WerkBezeichnung;
                    }
                }
                else
                {
                    _LagerPlatzBezeichungKomplett = WerkBezeichnung;
                }
                return _LagerPlatzBezeichungKomplett;
            }
            set { _LagerPlatzBezeichungKomplett = value; }
        }

        public string LagerPlatzBezeichungListe
        {
            get
            {
                _LagerPlatzBezeichungListe = String.Format("{0}\t{1}", "Werk: ", WerkBezeichnung) + Environment.NewLine +
                                            String.Format("{0}\t{1}", "Halle: ", HalleBezeichnung) + Environment.NewLine +
                                            String.Format("{0}\t{1}", "Reihe: ", ReiheBezeichnung) + Environment.NewLine +
                                            String.Format("{0}\t{1}", "Ebene: ", EbeneBezeichnung) + Environment.NewLine +
                                            String.Format("{0}\t{1}", "Platz: ", PlatzBezeichnung);

                return _LagerPlatzBezeichungListe;
            }
            set { _LagerPlatzBezeichungListe = value; }
        }
        public decimal ArtikelID
        {
            get { return _ArtikelID; }
            set { _ArtikelID = value; }
        }
        public string LOTable
        {
            get { return _LOTable; }
            set { _LOTable = value; }
        }
        /*************************************************************************************
         * 
         * **********************************************************************************/
        ///<summary>clsLagerOrt / Init</summary>
        ///<remarks></remarks>
        public void Init()
        {
            Werk._GL_User = this._GL_User;
            Werk.ID = WerkID;
            //Init läuft durch bis zum Platz
            Werk.Init();
        }
        ///<summary>clsLagerOrt / GetLagerortDataTable</summary>
        ///<remarks></remarks>
        public DataTable GetLagerortDataTable(string myLagerPlatzOrt)
        {
            bool myBExLagerOrt = false;

            DataTable dt = new DataTable();
            string strSql = string.Empty;


            switch (myLagerPlatzOrt)
            {
                case "Werk":
                    strSql = "Select " +
                                     "e.Bezeichnung as Werk" +
                                     ", '' as Halle" +
                                     ", '' as Reihe" +
                                     ", '' as Ebene" +
                                     ", '' as Platz" +
                                     ", '0' as ArtikelID" +
                                     ", '0' as LVSNr" +
                                     ", '' as AbBereich" +
                                     ", e.ID as TableID" +
                                     " FROM Werk e " +
                                     //"INNER JOIN Ebene b ON b.ID=a.EbeneID " +
                                     //"INNER JOIN Reihe c ON c.ID=b.ReiheID " +
                                     //"INNER JOIN Halle d ON d.ID=c.HalleID " +
                                     // "INNER JOIN Werk e ON e.ID = d.WerkID " +
                                     //"LEFT JOIN Artikel f ON f.LagerOrt=a.ID " +
                                     "WHERE e.exLagerOrt='False' " +
                                         "ORDER BY " +
                                             "e.Bezeichnung" +
                                             //", d.Bezeichnung" +
                                             //", c.Bezeichnung"+
                                             //", b.Bezeichnung"+
                                             ";";
                    break;

                case "Halle":
                    strSql = "Select " +
                                      "e.Bezeichnung as Werk" +
                                      ", d.Bezeichnung as Halle" +
                                      ", '' as Reihe" +
                                      ", '' as Ebene" +
                                      ", '' as Platz" +
                                      ", '0' as ArtikelID" +
                                      ", '0' as LVSNr" +
                                      ", '' as AbBereich" +
                                      ", d.ID as TableID" +
                                      " FROM Halle d " +
                                      //"INNER JOIN Ebene b ON b.ID=a.EbeneID " +
                                      //"INNER JOIN Reihe c ON c.ID=b.ReiheID " +
                                      //"INNER JOIN Halle d ON d.ID=c.HalleID " +
                                      "INNER JOIN Werk e ON e.ID = d.WerkID " +
                                      //"LEFT JOIN Artikel f ON f.LagerOrt=a.ID " +
                                      "WHERE e.exLagerOrt='False' " +
                                          "ORDER BY " +
                                              "e.Bezeichnung" +
                                              ", d.Bezeichnung" +
                                              //", c.Bezeichnung"+
                                              //", b.Bezeichnung"+
                                              ";";
                    break;


                case "Reihe":
                    strSql = "Select " +
                                      "e.Bezeichnung as Werk" +
                                      ", d.Bezeichnung as Halle" +
                                      ", c.Bezeichnung as Reihe" +
                                      ", '' as Ebene" +
                                      ", '' as Platz" +
                                      ", '0' as ArtikelID" +
                                      ", '0' as LVSNr" +
                                      ",''as AbBereich" +
                                      ", c.ID as TableID" +
                                      " FROM Reihe c " +
                                      //"INNER JOIN Ebene b ON b.ID=a.EbeneID " +
                                      //"INNER JOIN Reihe c ON c.ID=b.ReiheID " +
                                      "INNER JOIN Halle d ON d.ID=c.HalleID " +
                                      "INNER JOIN Werk e ON e.ID = d.WerkID " +
                                      //"LEFT JOIN Artikel f ON f.LagerOrt=a.ID " +
                                      "WHERE e.exLagerOrt='False' " +
                                          "ORDER BY " +
                                              "e.Bezeichnung" +
                                              ", d.Bezeichnung" +
                                              ", c.Bezeichnung" +
                                              //", b.Bezeichnung"+
                                              ";";
                    break;

                case "Ebene":
                    strSql = "Select " +
                                    "e.Bezeichnung as Werk" +
                                    ", d.Bezeichnung as Halle" +
                                    ", c.Bezeichnung as Reihe" +
                                    ", b.Bezeichnung as Ebene" +
                                    ", '' as Platz" +
                                    ", '0' as ArtikelID" +
                                    ", '0' as LVSNr" +
                                    ", '' as AbBereich" +
                                    ", b.ID as TableID" +
                                    " FROM Ebene b " +
                                    //"INNER JOIN Ebene b ON b.ID=a.EbeneID " +
                                    "INNER JOIN Reihe c ON c.ID=b.ReiheID " +
                                    "INNER JOIN Halle d ON d.ID=c.HalleID " +
                                    "INNER JOIN Werk e ON e.ID = d.WerkID " +
                                    //"LEFT JOIN Artikel f ON f.LagerOrt=a.ID " +
                                    "WHERE e.exLagerOrt='False' " +
                                        "ORDER BY " +
                                            "e.Bezeichnung" +
                                            ", d.Bezeichnung" +
                                            ", c.Bezeichnung" +
                                            ", b.Bezeichnung" +
                                            ";";
                    break;

                case "Platz":
                    strSql = "Select " +
                                        "e.Bezeichnung as Werk" +
                                        ", d.Bezeichnung as Halle" +
                                        ", c.Bezeichnung as Reihe" +
                                        ", b.Bezeichnung as Ebene" +
                                        ", a.Bezeichnung as Platz" +
                                        ", a.ID as TableID" +
                                        ", f.LVS_ID as LVSNr" +
                                         ",(Select Arbeitsbereich.Name FROM Arbeitsbereich WHERE Arbeitsbereich.ID=f.Ab_ID) as AbBereich" +
                                        " FROM Platz a " +
                                        "INNER JOIN Ebene b ON b.ID=a.EbeneID " +
                                        "INNER JOIN Reihe c ON c.ID=b.ReiheID " +
                                        "INNER JOIN Halle d ON d.ID=c.HalleID " +
                                        "INNER JOIN Werk e ON e.ID = d.WerkID " +
                                        "LEFT JOIN Artikel f ON f.LagerOrt=a.ID AND f.BKZ=1 AND f.LOTable='Platz' " +
                                        "WHERE e.exLagerOrt='False' " +
                                        "ORDER BY " +
                                            "e.Bezeichnung" +
                                            ", d.Bezeichnung" +
                                            ", c.Bezeichnung" +
                                            ", b.Bezeichnung" +
                                            ";";
                    break;

                case "exLagerort":
                    strSql = "Select " +
                                        "e.Bezeichnung as Werk" +
                                        ", d.Bezeichnung as Halle" +
                                        ", c.Bezeichnung as Reihe" +
                                        ", b.Bezeichnung as Ebene" +
                                        ", a.Bezeichnung as Platz" +
                                        ", a.ID as TableID" +
                                        ", f.LVS_ID as LVSNr" +
                                         ",(Select Arbeitsbereich.Name FROM Arbeitsbereich WHERE Arbeitsbereich.ID=f.Ab_ID) as AbBereich" +
                                        " FROM Platz a " +
                                        "INNER JOIN Ebene b ON b.ID=a.EbeneID " +
                                        "INNER JOIN Reihe c ON c.ID=b.ReiheID " +
                                        "INNER JOIN Halle d ON d.ID=c.HalleID " +
                                        "INNER JOIN Werk e ON e.ID = d.WerkID " +
                                        "LEFT JOIN Artikel f ON f.LagerOrt=a.ID OR f.exLagerOrt=a.ID AND f.BKZ=1 " +
                                        "WHERE e.exLagerOrt='True' " +
                                        "ORDER BY " +
                                            "e.Bezeichnung" +
                                            ", d.Bezeichnung" +
                                            ", c.Bezeichnung" +
                                            ", b.Bezeichnung" +
                                            ";";
                    break;
            }

            if (strSql != string.Empty)
            {
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Lagerort");
            }
            return dt;
        }
        ///<summary>clsLagerOrt / GetLagerortAsString</summary>
        ///<remarks></remarks>
        public void InitLagerPlatz()
        {
            //Check LOTable darf nicht leer sein
            if (LOTable != string.Empty)
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                //Unterscheidung nach LagerOrtTable
                switch (LOTable)
                {
                    case "Werk":
                        strSql = "Select " +
                                         "e.Bezeichnung as Werk" +
                                         ", '' as Halle" +
                                         ", '' as Reihe" +
                                         ", '' as Ebene " +
                                         ", '' as Platz " +
                                         ", e.ID as TableID" +
                                         " FROM Werk e " +
                                         //"INNER JOIN Ebene b ON b.ID=a.EbeneID " +
                                         //"INNER JOIN Reihe c ON c.ID=b.ReiheID " +
                                         //"INNER JOIN Halle d ON d.ID=c.HalleID " +
                                         //"INNER JOIN Werk e ON e.ID = d.WerkID " +
                                         "WHERE e.ID=" + LagerPlatzID +
                                         ";";
                        break;

                    case "Halle":
                        strSql = "Select " +
                                        "e.Bezeichnung as Werk" +
                                        ",d.Bezeichnung as Halle" +
                                        ", '' as Reihe" +
                                        ", '' as Ebene " +
                                        ", '' as Platz " +
                                         ", d.ID as TableID" +
                                        " FROM Halle d " +
                                        //"INNER JOIN Ebene b ON b.ID=a.EbeneID " +
                                        //"INNER JOIN Reihe c ON c.ID=b.ReiheID " +
                                        //"INNER JOIN Halle d ON d.ID=c.HalleID " +
                                        "INNER JOIN Werk e ON e.ID = d.WerkID " +
                                        "WHERE d.ID=" + LagerPlatzID +
                                        ";";
                        break;


                    case "Reihe":
                        strSql = "Select " +
                                        "e.Bezeichnung as Werk" +
                                        ",d.Bezeichnung as Halle" +
                                        ",c.Bezeichnung as Reihe" +
                                        ", '' as Ebene " +
                                        ", '' as Platz " +
                                         ", c.ID as TableID" +
                                        " FROM Reihe c " +
                                        //"INNER JOIN Ebene b ON b.ID=a.EbeneID " +
                                        //"INNER JOIN Reihe c ON c.ID=b.ReiheID " +
                                        "INNER JOIN Halle d ON d.ID=c.HalleID " +
                                        "INNER JOIN Werk e ON e.ID = d.WerkID " +
                                        "WHERE c.ID=" + LagerPlatzID +
                                        ";";
                        break;

                    case "Ebene":
                        strSql = "Select " +
                                        "e.Bezeichnung as Werk" +
                                        ",d.Bezeichnung as Halle" +
                                        ",c.Bezeichnung as Reihe" +
                                        ",b.Bezeichnung as Ebene " +
                                        ", '' as Platz " +
                                         ", b.ID as TableID" +
                                        " FROM Ebene b " +
                                        //"INNER JOIN Ebene b ON b.ID=a.EbeneID " +
                                        "INNER JOIN Reihe c ON c.ID=b.ReiheID " +
                                        "INNER JOIN Halle d ON d.ID=c.HalleID " +
                                        "INNER JOIN Werk e ON e.ID = d.WerkID " +
                                        "WHERE b.ID=" + LagerPlatzID +
                                        ";";
                        break;

                    case "Platz":
                        clsPlatz pl = new clsPlatz();
                        pl._GL_User = this._GL_User;
                        pl.ID = LagerPlatzID;
                        if (pl.ExistPlatz())
                        {
                            strSql = "Select " +
                                            "e.Bezeichnung as Werk" +
                                            ",d.Bezeichnung as Halle" +
                                            ",c.Bezeichnung as Reihe" +
                                            ",b.Bezeichnung as Ebene " +
                                            ",a.Bezeichnung as Platz " +
                                             ", a.ID as TableID" +
                                            " FROM Platz a " +
                                            "INNER JOIN Ebene b ON b.ID=a.EbeneID " +
                                            "INNER JOIN Reihe c ON c.ID=b.ReiheID " +
                                            "INNER JOIN Halle d ON d.ID=c.HalleID " +
                                            "INNER JOIN Werk e ON e.ID = d.WerkID " +
                                            "WHERE a.ID=" + pl.ID +
                                            ";";
                        }
                        break;

                    case "exLagerort":
                        pl = new clsPlatz();
                        pl._GL_User = this._GL_User;
                        pl.ID = LagerPlatzID;
                        if (pl.ExistPlatz())
                        {
                            strSql = "Select " +
                                            "e.Bezeichnung as Werk" +
                                            ",d.Bezeichnung as Halle" +
                                            ",c.Bezeichnung as Reihe" +
                                            ",b.Bezeichnung as Ebene " +
                                            ",a.Bezeichnung as Platz " +
                                             ", a.ID as TableID" +
                                            " FROM Platz a " +
                                            "INNER JOIN Ebene b ON b.ID=a.EbeneID " +
                                            "INNER JOIN Reihe c ON c.ID=b.ReiheID " +
                                            "INNER JOIN Halle d ON d.ID=c.HalleID " +
                                            "INNER JOIN Werk e ON e.ID = d.WerkID " +
                                            "WHERE e.exLagerOrt='True' AND a.ID=" + pl.ID +
                                            ";";
                        }
                        break;
                }
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Lagerort");

                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        string strLPID = dt.Rows[i]["TableID"].ToString();
                        decimal decTmp = 0;
                        Decimal.TryParse(strLPID, out decTmp);
                        this.LagerPlatzID = decTmp;
                        this.WerkBezeichnung = dt.Rows[i]["Werk"].ToString();
                        this.HalleBezeichnung = dt.Rows[i]["Halle"].ToString();
                        this.ReiheBezeichnung = dt.Rows[i]["Reihe"].ToString();
                        this.EbeneBezeichnung = dt.Rows[i]["Ebene"].ToString();
                        this.PlatzBezeichnung = dt.Rows[i]["Platz"].ToString();
                    }
                }
            }
            else
            {
                ClearClsValue();
            }
        }
        ///<summary>clsLagerOrt / GetLagerortAsString</summary>
        ///<remarks>Reset Klassen - Value, da sonst die alten Werte stehen bleiben.</remarks>    
        private void ClearClsValue()
        {
            this.LagerPlatzID = 0;
            this.WerkBezeichnung = string.Empty;
            this.HalleBezeichnung = string.Empty;
            this.ReiheBezeichnung = string.Empty;
            this.EbeneBezeichnung = string.Empty;
            this.PlatzBezeichnung = string.Empty;
        }
        ///<summary>clsLagerOrt / UpdateArtikelLagerOrt</summary>
        ///<remarks></remarks>  
        public bool UpdateArtikelLagerOrt()
        {
            bool bVal = false;
            clsArtikel art = new clsArtikel();
            art._GL_User = this._GL_User;
            art.ID = this.ArtikelID;
            art.GetArtikeldatenByTableID();

            if (art.ExistArtikelTableID())
            {
                InitLagerPlatz();
                art.LagerOrt = LagerPlatzID;
                art.Werk = this.WerkBezeichnung;
                art.Halle = this.HalleBezeichnung;
                art.Reihe = this.ReiheBezeichnung;
                art.Ebene = this.EbeneBezeichnung;
                art.Platz = this.PlatzBezeichnung;
                //art.UpdateArtikelLagerOrt();
                art.LagerOrtTable = LOTable;
                art.UpdateArtikelLagerOrt();
                bVal = true;
            }
            return bVal;
        }
        ///<summary>clsLager / CheckLagerOrt</summary>
        ///<remarks>Ermittelt die Artikel für die entsprechende Artikel ID für die DISPO.</remarks>
        private bool IsLagerOrtAvailable()
        {
            string strSql = string.Empty;
            decimal decVal = 0;
            strSql = "SELECT ID FROM Artikel WHERE BKZ=1 AND LagerOrt=" + LagerPlatzID + ";";

            bool bVal = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            if (bVal)
            {
                //die Abfrage hat gezeigt, dass bereits ein Artikel auf diesen Lagerplatz eingelagert wurde
                //deshalb ist der Lagerort nicht verfügbar
                bVal = false;
            }
            else
            {
                bVal = true;
            }
            return bVal;
        }
        ///<summary>clsLager / DeleteLagerOrt</summary>
        ///<remarks></remarks>
        public bool DeleteLagerOrt(string myCheck)
        {
            bool reVal = false;
            //Check ob belegte Lagerplätze vorhanden sind
            if (CheckLagerOrt(myCheck))
            {
                //Löschen der entsprechenden Lagerplatzbestandteile
                switch (myCheck)
                {
                    case "Werk":
                        this.Werk.Delete();
                        reVal = true;
                        break;
                    case "Halle":
                        this.Werk.Halle.Delete();
                        reVal = true;
                        break;
                    case "Reihe":
                        this.Werk.Halle.Reihe.Delete();
                        reVal = true;
                        break;
                    case "Ebene":
                        this.Werk.Halle.Reihe.Ebene.Delete();
                        reVal = true;
                        break;
                    case "Platz":
                        this.Werk.Halle.Reihe.Ebene.Platz.Delete();
                        reVal = true;
                        break;
                }
            }
            else
            {
                //Info Löschen nicht erfolgreich
                clsMessages.Lager_DeleteLagerplatzFailed();
                reVal = false;
            }
            return reVal;
        }
        ///<summary>clsLager / CheckLagerOrt</summary>
        ///<remarks></remarks>
        private bool CheckLagerOrt(string myCheck)
        {
            bool bVal = false;
            string strSql = string.Empty;
            strSql = "Select a.ID FROM Artikel a " +
                    "WHERE ";
            //Check Übergabestring myCheck
            string strSQLWHERE = string.Empty;
            switch (myCheck)
            {
                case "Werk":
                    //this.Werk.ID = WerkID;
                    if (this.Werk.ExistWerkByID())
                    {
                        strSQLWHERE = "BKZ=1 AND a.LagerOrt=" + this.Werk.ID + " AND a.LOTable='Werk' " +
                                                    " UNION " +
                                                    //Alle Artikel der Halle
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Halle' " +
                                                                        "AND a.LagerOrt IN (Select x.ID FROM Halle x WHERE x.WerkID=" + this.Werk.ID + ") " +
                                                    " UNION " +
                                                    //Alle Artikel der Reihe
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Reihe' " +
                                                                        "AND a.LagerOrt IN (" +
                                                                                            "Select r.ID FROM Reihe r " +
                                                                                                          "INNER JOIN Halle h ON h.ID=r.HalleID " +
                                                                                                          "WHERE h.WerkID=" + this.Werk.ID + " " +
                                                                                           ")" +
                                                    " UNION " +
                                                    //Alle Artikel der Ebenen
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Ebene' " +
                                                                        "AND a.LagerOrt IN (" +
                                                                                            "Select e.ID FROM Ebene e " +
                                                                                                          "INNER JOIN Reihe r ON r.ID=e.ReiheID " +
                                                                                                          "INNER JOIN Halle h ON h.ID=r.HalleID " +
                                                                                                          "WHERE h.WerkID=" + this.Werk.ID + " " +
                                                                                           ")" +
                                                    " UNION " +
                                                    //Alle Artikel Platz
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Platz' " +
                                                                        "AND a.LagerOrt IN (" +
                                                                                            "Select p.ID FROM Platz p " +
                                                                                                            "INNER JOIN Ebene e ON p.EbeneID=e.ID " +
                                                                                                            "INNER JOIN Reihe r ON r.ID=e.ReiheID " +
                                                                                                            "INNER JOIN Halle h ON h.ID=r.HalleID " +
                                                                                                            "WHERE h.WerkID=" + this.Werk.ID + " " +
                                                                                           ") ";
                    }
                    break;
                case "Halle":
                    //this.Werk.ID = WerkID;
                    //this.Werk.Halle.ID = HallenID;
                    if (this.Werk.Halle.ExistHalleByWerkID())
                    {
                        strSQLWHERE = "BKZ=1 AND a.LagerOrt=" + this.Werk.Halle.ID + " AND a.LOTable='Halle' " +
                                                    " UNION " +
                                                    //Alle Artikel der Reihe
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Reihe' " +
                                                                        "AND a.LagerOrt IN (Select x.ID FROM Reihe x WHERE x.HalleID=" + this.Werk.Halle.ID + ") " +
                                                    " UNION " +
                                                    //Alle Artikel der Ebenen
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Ebene' " +
                                                                        "AND a.LagerOrt IN (" +
                                                                                            "Select e.ID FROM Ebene e " +
                                                                                                          "INNER JOIN Reihe r ON e.ReiheID=r.ID " +
                                                                                                          "WHERE r.HalleID=" + this.Werk.Halle.ID + " " +
                                                                                           ")" +
                                                    " UNION " +
                                                    //Alle Artikel Platz
                                                    "Select a.ID FROM Artikel a " +
                                                                        "WHERE BKZ=1  AND a.LOTable='Platz' " +
                                                                        "AND a.LagerOrt IN (" +
                                                                                            "Select p.ID FROM Platz p " +
                                                                                                            "INNER JOIN Ebene e ON p.EbeneID=e.ID " +
                                                                                                            "INNER JOIN Reihe r ON r.ID=e.ReiheID " +
                                                                                                            "WHERE r.HalleID=" + this.Werk.Halle.ID + " " +
                                                                                           ") ";
                    }
                    break;
                case "Reihe":
                    //this.Werk.ID = WerkID;
                    //this.Werk.Halle.ID = HallenID;
                    //this.Werk.Halle.Reihe.ID = ReihenID;
                    if (this.Werk.Halle.Reihe.ExistReiheByHallenID())
                    {
                        strSQLWHERE = "BKZ=1 AND a.LagerOrt=" + this.Werk.Halle.Reihe.ID + " AND a.LOTable='Reihe' " +
                                                        " UNION " +
                                                        //Alle Artikel der Ebenen
                                                        "Select a.ID FROM Artikel a " +
                                                                            "WHERE BKZ=1  AND a.LOTable='Ebene' " +
                                                                            "AND a.LagerOrt IN (Select x.ID FROM Ebene x WHERE x.ReiheID=" + this.Werk.Halle.Reihe.ID + ") " +
                                                        " UNION " +
                                                        //Alle Artikel Platz
                                                        "Select a.ID FROM Artikel a " +
                                                                            "WHERE BKZ=1  AND a.LOTable='Platz' " +
                                                                            "AND a.LagerOrt IN (" +
                                                                                                "Select p.ID FROM Platz p INNER JOIN Ebene e ON p.EbeneID=e.ID WHERE e.ReiheID=" + this.Werk.Halle.Reihe.ID + " " +
                                                                                               ") ";
                    }
                    break;
                case "Ebene":
                    //this.Werk.ID = WerkID;
                    //this.Werk.Halle.ID = HallenID;
                    //this.Werk.Halle.Reihe.ID = ReihenID;
                    if (this.Werk.Halle.Reihe.Ebene.ExistEbeneByReiheID())
                    {
                        strSQLWHERE = "BKZ=1 AND a.LagerOrt=" + this.Werk.Halle.Reihe.Ebene.ID + " AND a.LOTable='Ebene' " +
                                                        " UNION " +
                                                        //Alle Artikel Platz
                                                        "Select a.ID FROM Artikel a " +
                                                                            "WHERE BKZ=1  AND a.LOTable='Platz' " +
                                                                            "AND a.LagerOrt IN (" +
                                                                                                "Select p.ID FROM Platz p INNER JOIN Ebene e ON p.EbeneID=e.ID WHERE e.ReiheID=" + this.Werk.Halle.Reihe.Ebene.ID + " " +
                                                                                               ") ";
                    }
                    break;
                case "Platz":
                    //this.Werk.ID = WerkID;
                    //this.Werk.Halle.ID = HallenID;
                    //this.Werk.Halle.Reihe.ID = ReihenID;
                    if (this.Werk.Halle.Reihe.Ebene.Platz.ExistPlatz())
                    {
                        strSQLWHERE = "BKZ=1 AND LOTable='Platz' AND LagerOrt=" + this.Werk.Halle.Reihe.Ebene.Platz.ID + " ";
                    }
                    break;
            }
            //strSQLWHere muss gefüllt sein, damit die Abfrage erstellt werden kann
            if (strSQLWHERE != string.Empty)
            {
                strSql = strSql + strSQLWHERE;
                bVal = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
                if (bVal)
                {
                    //die Abfrage hat gezeigt, dass bereits ein Artikel auf diesen Lagerplatz eingelagert wurde
                    //deshalb ist der Lagerort nicht verfügbar
                    bVal = false;
                }
                else
                {
                    bVal = true;
                }

            }
            return bVal;
        }
    }
}
