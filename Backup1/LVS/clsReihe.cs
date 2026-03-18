using System;
using System.Data;


namespace LVS
{
    public class clsReihe
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

        public clsEbene Ebene = new clsEbene();
        public DataTable dtReihe = new DataTable("Reihen");

        private decimal _ID;
        private string _Bezeichnung;
        private string _Beschreibung;
        private decimal _HalleID;
        private Int32 _OrderID;
        private Int32 _maxOrderID;
        private Int32 _maxOrderIDEbene;

        public decimal ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public decimal HalleID
        {
            get { return _HalleID; }
            set { _HalleID = value; }
        }
        public string Bezeichnung
        {
            get { return _Bezeichnung; }
            set { _Bezeichnung = value; }
        }
        public string Beschreibung
        {
            get { return _Beschreibung; }
            set { _Beschreibung = value; }
        }
        public Int32 OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }
        public Int32 maxOrderID
        {
            get
            {
                _maxOrderID = GetMaxOrderID();
                return _maxOrderID;
            }
            set { _maxOrderID = value; }
        }
        public Int32 maxOrderIDEbene
        {
            get
            {
                _maxOrderIDEbene = GetMaxOrderIDEbene();
                return _maxOrderIDEbene;
            }
            set { _maxOrderIDEbene = value; }
        }


        private decimal _DickeVon;

        public decimal DickeVon
        {
            get { return _DickeVon; }
            set { _DickeVon = value; }
        }

        private decimal _BreiteVon;

        public decimal BreiteVon
        {
            get { return _BreiteVon; }
            set { _BreiteVon = value; }
        }
        private decimal _LaengeVon;

        public decimal LaengeVon
        {
            get { return _LaengeVon; }
            set { _LaengeVon = value; }
        }
        private decimal _HoeheVon;

        public decimal HoeheVon
        {
            get { return _HoeheVon; }
            set { _HoeheVon = value; }
        }
        private decimal _BruttoVon;

        public decimal BruttoVon
        {
            get { return _BruttoVon; }
            set { _BruttoVon = value; }
        }

        private decimal _DickeBis;

        public decimal DickeBis
        {
            get { return _DickeBis; }
            set { _DickeBis = value; }
        }

        private decimal _BreiteBis;

        public decimal BreiteBis
        {
            get { return _BreiteBis; }
            set { _BreiteBis = value; }
        }
        private decimal _LaengeBis;

        public decimal LaengeBis
        {
            get { return _LaengeBis; }
            set { _LaengeBis = value; }
        }
        private decimal _HoeheBis;

        public decimal HoeheBis
        {
            get { return _HoeheBis; }
            set { _HoeheBis = value; }
        }
        private decimal _BruttoBis;

        public decimal BruttoBis
        {
            get { return _BruttoBis; }
            set { _BruttoBis = value; }
        }

        private decimal _Anzahl;

        public decimal Anzahl
        {
            get { return _Anzahl; }
            set { _Anzahl = value; }
        }
        private clsGut _GArt;

        public clsGut GArt
        {
            get
            {
                if (_GArt == null) _GArt = new clsGut();
                return _GArt;
            }
            set { _GArt = value; }
        }

        /**********************************************************************************************************************
         *                                               Methoden
         *********************************************************************************************************************/
        ///<summary>clsReihe / Init</summary>
        ///<remarks></remarks>
        public void Init()
        {
            GetReihenDatenForDataTableExtra();
            if (ExistReihe())
            {
                FillDaten();
                Ebene.ReiheID = ID;
            }
            if (ExistReiheByHallenID())
            {
                Ebene.Init();
            }
        }
        ///<summary>clsReihe / Add</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB.</remarks>
        public void Add()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO Reihe (HalleID, Bezeichnung, Beschreibung, OrderID,DickeVon,DickeBis,BreiteVon,BreiteBis,HoeheVon,HoeheBis,LaengeVon,LaengeBis,BruttoVon,BruttoBis,Anzahl,GArtID) " +
                                            "VALUES (" +
                                                   HalleID + " " +
                                                   ", '" + Bezeichnung + "' " +
                                                   ", '" + Beschreibung + "' " +
                                                   ", " + OrderID + " " +
                                                   ", " + DickeVon.ToString().Replace(',', '.') + " " +
                                                   ", " + DickeBis.ToString().Replace(',', '.') + " " +
                                                   ", " + BreiteVon.ToString().Replace(',', '.') + " " +
                                                   ", " + BreiteBis.ToString().Replace(',', '.') + " " +
                                                   ", " + HoeheVon.ToString().Replace(',', '.') + " " +
                                                   ", " + HoeheBis.ToString().Replace(',', '.') + " " +
                                                   ", " + LaengeVon.ToString().Replace(',', '.') + " " +
                                                   ", " + LaengeBis.ToString().Replace(',', '.') + " " +
                                                   ", " + BruttoVon.ToString().Replace(',', '.') + " " +
                                                   ", " + BruttoBis.ToString().Replace(',', '.') + " " +
                                                   ", " + Anzahl + " " +
                                                   ", " + GArt.ID + " " +
                                                     ")";
            strSql = strSql + "Select @@IDENTITY as 'ID' ";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            ID = decTmp;

            //Add Logbucheintrag Eintrag
            string strInfo = "Stammdaten Reihe erstellt: ID [" + ID.ToString() + "] / Bezeichnung: " + Bezeichnung;
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), strInfo);

            GetReihenDatenForDataTableExtra();
        }
        ///<summary>clsReihe / UpdateLagerEingang</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public bool Update()
        {
            if (ExistReihe())
            {
                string strSql = string.Empty;
                strSql = "Update Reihe SET" +
                                        " HalleID =" + HalleID + " " +
                                        ", Bezeichnung ='" + Bezeichnung + "'" +
                                        ", Beschreibung = '" + Beschreibung + "' " +
                                        ", OrderID = " + OrderID + " " +
                                        ",DickeVon= '" + DickeVon.ToString().Replace(',', '.') + "' " +
                                        ",DickeBis= '" + DickeBis.ToString().Replace(',', '.') + "' " +
                                          ",BreiteVon= '" + BreiteVon.ToString().Replace(',', '.') + "' " +
                                          ",BreiteBis= '" + BreiteBis.ToString().Replace(',', '.') + "' " +
                                          ",HoeheVon= '" + HoeheVon.ToString().Replace(',', '.') + "' " +
                                          ",HoeheBis= '" + HoeheBis.ToString().Replace(',', '.') + "' " +
                                          ",LaengeVon= '" + LaengeVon.ToString().Replace(',', '.') + "' " +
                                          ",LaengeBis= '" + LaengeBis.ToString().Replace(',', '.') + "' " +
                                          ",BruttoVon= '" + BruttoVon.ToString().Replace(',', '.') + "' " +
                                          ",BruttoBis= '" + BruttoBis.ToString().Replace(',', '.') + "' " +
                                          ",Anzahl= '" + Anzahl.ToString().Replace(',', '.') + "' " +
                                          ",GArtID= " + (Int32)GArt.ID + " " +
                                        " WHERE ID='" + ID + "'";
                bool bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
                // Logbucheintrag Eintrag
                string strInfo = "Stammdaten Reihe geändert: ID: " + ID.ToString() + " / Bezeichnung: " + Bezeichnung;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), strInfo);

                GetReihenDatenForDataTableExtra();
                return bReturn;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsReihe / ExistWerk</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        private bool ExistReihe()
        {
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Reihe WHERE ID=" + ID + " ;";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsReihe / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            if (ExistReihe())
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                    "LagerOrt=0 " +
                                    ", LOTable='' " +
                                    "WHERE ID IN (" +
                                        //Alle Artikel der Reihe
                                        "Select a.ID FROM Artikel a " +
                                                            "WHERE BKZ=1 AND a.LagerOrt=" + ID + " AND a.LOTable='Reihe' " +
                                        " UNION " +
                                        //Alle Artikel der Ebenen
                                        "Select a.ID FROM Artikel a " +
                                                            "WHERE BKZ=1  AND a.LOTable='Ebene' " +
                                                            "AND a.LagerOrt IN (Select x.ID FROM Ebene x WHERE x.ReiheID=" + ID + ") " +
                                        " UNION " +
                                        //Alle Artikel Platz
                                        "Select a.ID FROM Artikel a " +
                                                            "WHERE BKZ=1  AND a.LOTable='Platz' " +
                                                            "AND a.LagerOrt IN (" +
                                                                                "Select p.ID FROM Platz p INNER JOIN Ebene e ON p.EbeneID=e.ID WHERE e.ReiheID=" + ID + " " +
                                                                               ") " +
                                            ") ";

                strSql = strSql + "Delete FROM Reihe WHERE ID=" + ID + " ";


                if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "ReiheDelete", this._GL_User.User_ID))
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Stammdaten Reihe gelöscht: ID: " + ID.ToString() + " / Bezeichnung: " + Bezeichnung;
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);

                    GetReihenDatenForDataTableExtra();
                }
            }
        }
        ///<summary>clsReihe / FillDaten</summary>
        ///<remarks>Ermittel die Daten anhand der TableID.</remarks>
        public bool FillDaten()
        {
            if (ExistReihe())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Reihe WHERE ID=" + ID + " ;";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Reihe");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        this.ID = (decimal)dt.Rows[i]["ID"];
                        this.HalleID = (decimal)dt.Rows[i]["HalleID"];
                        this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                        this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                        this.OrderID = (Int32)dt.Rows[i]["OrderID"];
                        this.DickeVon = Convert.ToDecimal(dt.Rows[i]["DickeVon"]);
                        this.DickeBis = Convert.ToDecimal(dt.Rows[i]["DickeBis"]);
                        this.BreiteVon = Convert.ToDecimal(dt.Rows[i]["BreiteVon"]);
                        this.BreiteBis = Convert.ToDecimal(dt.Rows[i]["BreiteBis"]);
                        this.LaengeVon = Convert.ToDecimal(dt.Rows[i]["LaengeVon"]);
                        this.LaengeBis = Convert.ToDecimal(dt.Rows[i]["LaengeBis"]);
                        this.HoeheVon = Convert.ToDecimal(dt.Rows[i]["HoeheVon"]);
                        this.HoeheBis = Convert.ToDecimal(dt.Rows[i]["HoeheBis"]);
                        this.BruttoVon = Convert.ToDecimal(dt.Rows[i]["BruttoVon"]);
                        this.BruttoBis = Convert.ToDecimal(dt.Rows[i]["BruttoBis"]);
                        this.Anzahl = Convert.ToInt32(dt.Rows[i]["Anzahl"]);
                        this.GArt.ID = Convert.ToInt32(dt.Rows[i]["GArtID"]);
                        this.GArt.Fill();

                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsReihe / GetHallenDatenForDataTable</summary>
        ///<remarks>Ermittel die Daten anhand der WerkID.</remarks>
        public void GetReihenDatenForDataTable()
        {
            dtReihe.Clear();
            string strSql = string.Empty;
            if (ExistReiheByHallenID())
            {

                strSql = "Select * FROM Reihe WHERE HalleID=" + HalleID + " Order By OrderID;";

                dtReihe = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Reihen");
            }
            else
            {
                strSql = "Select * FROM Reihe Order By OrderID;";
                dtReihe = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
                dtReihe.Rows.Clear();
            }
        }
        ///<summary>clsReihe / GetHallenDatenForDataTable</summary>
        ///<remarks>Ermittel die Daten anhand der WerkID.</remarks>
        public void GetReihenDatenForDataTableExtra()
        {
            dtReihe.Clear();
            string strSql = string.Empty;
            if (ExistReiheByHallenID())
            {

                strSql = "Select *" +
                                ", (Select count(*) from Artikel a " +
                                                        "INNER JOIN LEingang e on e.id=a.LEingangTableID " +
                                                        "LEFT JOIN Lausgang c on a.LAusgangTableID = c.ID " +
                                                         "where " +
                                                            "Reihe=r.Bezeichnung " +
                                                            " and (c.Checked=0 or checked is null)) as Belegung " +
                                 ", (select ViewID from GueterArt where ID=GArtID) as Gut " +
                                 "FROM Reihe r " +
                                    "WHERE  HalleID=" + HalleID + " Order By OrderID;";

                dtReihe = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Reihen");
            }
            else
            {
                strSql = "Select * FROM Reihe Order By OrderID;";
                dtReihe = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
                dtReihe.Rows.Clear();
            }
        }


        ///<summary>clsReihe / ExistWerk</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistReiheByHallenID()
        {
            if (HalleID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM Reihe WHERE HalleID=" + HalleID + " ;";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsReihe / ExistReiheByBezeichnung</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks> 
        ///<returns>Returns BOOL</returns>
        public bool ExistReiheByBezeichnung()
        {
            if (Bezeichnung != string.Empty)
            {
                string strSql = string.Empty;
                strSql = "Select a.ID FROM Reihe a " +
                                            "INNER JOIN Halle b ON b.ID = a.HalleID " +
                                            "INNER JOIN Werk c ON c.ID=b.WerkID " +
                                            "WHERE a.HalleID=" + HalleID + " AND  " +
                                                   "a.Bezeichnung='" + Bezeichnung + "' ;";
                bool bResult = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
                return bResult;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsReihe / GetMaxOrderID</summary>
        ///<remarks>Ermittelt die max. OrderID.</remarks>
        ///<returns>Returns Int32</returns>
        private Int32 GetMaxOrderID()
        {
            string strSql = string.Empty;
            strSql = "Select Count(OrderID) FROM Reihe WHERE HalleID=" + HalleID + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }
        ///<summary>clsWerk / GetMaxOrderIDHalle</summary>
        ///<remarks>Ermittelt die max. OrderID der Halle für das einzelne Werk.</remarks>
        ///<returns>Returns Int32</returns>
        private Int32 GetMaxOrderIDEbene()
        {
            string strSql = string.Empty;
            strSql = "Select COUNT(a.OrderID) FROM Ebene a " +
                                                //"INNER JOIN Halle c ON c.ID= d.HalleID " +
                                                //"INNER JOIN Werk b ON b.ID=c.WerkID " +
                                                //"INNER JOIN Reihe d ON d.ID=a.ReiheID "+
                                                "WHERE a.ReiheID =" + ID + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }
        ///<summary>clsReihe / UpdateOrderID</summary>
        ///<remarks></remarks>
        public void UpdateOrderID(Decimal myID, Int32 myOrderID)
        {
            if (ExistReiheByHallenID())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Reihe WHERE HalleID=" + HalleID + " Order BY OrderID ;";

                string strUpSQL = string.Empty;
                Int32 iCount = 0;
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Halle");
                if (dt.Rows.Count > 0)
                {
                    if (myID > 0)
                    {
                        strUpSQL = strUpSQL + "Update Reihe SET OrderID=" + myOrderID + " WHERE ID=" + myID + "; ";
                    }
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        iCount++;
                        decimal decTmp = (decimal)dt.Rows[i]["ID"];
                        Int32 iTmp = (Int32)dt.Rows[i]["OrderID"];
                        if (myID != decTmp)
                        {
                            if (myOrderID > 0)
                            {
                                if (iCount == myOrderID)
                                {
                                    iCount++;
                                }
                            }
                            strUpSQL = strUpSQL + "Update Reihe SET OrderID=" + iCount + " WHERE ID=" + decTmp + "; ";
                        }
                        else
                        {
                            iCount--;
                        }
                    }
                    //Updte per Transaction
                    clsSQLcon.ExecuteSQLWithTRANSACTION(strUpSQL, "UpdateOrderID", _GL_User.User_ID);
                }
            }
        }

        public string GetVorschlag(string dicke, string breite, string laenge, string hoehe, string brutto, string GArtID)
        {
            string sql = "Select " +
                            "* " +
                            ",(Select count(a.ID) from Artikel a " +
                                                        "inner join LEingang e on a.LEingangTableID = e.ID " +
                                                        "left join Lausgang c on a.LAusgangTableID = c.ID " +
                                                            "where Reihe=r.Bezeichnung and (c.Checked=0 or c.Checked is null))" +
                                                "as Anz " +
                         "from Reihe r " +
                                "where " +
                                    "(DickeVon<=" + dicke.Replace(".", "").Replace(",", ".") + " and DickeBis>=" + dicke.Replace(".", "").Replace(',', '.') + " or (DickeVon = 0 and DickeBis=0)) and  " +
                                    "(BreiteVon<=" + breite.Replace(".", "").Replace(',', '.') + " and BreiteBis>=" + breite.Replace(".", "").Replace(',', '.') + " or (BreiteVon = 0 and BreiteBis=0)) and " +
                                    "(LaengeVon<=" + laenge.Replace(".", "").Replace(',', '.') + " and LaengeBis>=" + laenge.Replace(".", "").Replace(',', '.') + " or (LaengeVon = 0 and LaengeBis=0)) and " +
                                    "(HoeheVon<=" + hoehe.Replace(".", "").Replace(',', '.') + " and HoeheBis>=" + hoehe.Replace(".", "").Replace(',', '.') + " or (HoeheVon = 0 and HoeheBis=0)) and " +
                                    "(BruttoVon<=" + brutto.Replace(".", "").Replace(',', '.') + " and BruttoBis>=" + brutto.Replace(".", "").Replace(',', '.') + " or (BruttoVon = 0 and BruttoBis=0)) " +
                                    "and (GArtID=" + GArtID + " or  GArtID=0) " +
                                    "and (Select count(a.ID) from Artikel a " +
                                                                    "inner join LEingang e on a.LEingangTableID = e.ID " +
                                                                    "left join Lausgang c on a.LAusgangTableID = c.ID " +
                                                                        "where " +
                                                                            "a.Reihe=r.Bezeichnung and (c.Checked=0 or checked is null))<Anzahl " +
                        " order by OrderID desc";

            DataTable dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(sql, _GL_User.User_ID, "Reihen");

            // Console.WriteLine(sql);

            string retVal = string.Empty;
            if (dtTmp.Rows.Count > 0)
            {
                retVal = dtTmp.Rows[0]["Bezeichnung"].ToString();
            }
            return retVal;
        }
    }
}
