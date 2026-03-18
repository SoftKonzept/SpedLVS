using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsTarifPosition
    {
        public const string const_QuantityBase_strStandard = "Standard";
        public const string const_QuantityBase_strEinlagerung = "Einlagerung";
        public const string const_QuantityBase_strAuslagerung = "Auslagerung";
        public const string const_QuantityBase_strRetoure = "Retoure";
        public const string const_QuantityBase_strVerlagerung = "Verlagerung";
        public const string const_QuantityBase_strUmbuchung = "Umbuchung";

        public const Int32 const_QuantityBase_Standard = 0;
        public const Int32 const_QuantityBase_Einlagerung = 1;
        public const Int32 const_QuantityBase_Auslagerung = 2;
        public const Int32 const_QuantityBase_Retoure = 3;
        public const Int32 const_QuantityBase_Verlagerung = 4;
        public const Int32 const_QuantityBase_Umbuchung = 5;


        //public const Int32 const_CalcModus_Default = 0;
        //public const Int32 const_CalcModus_monatlich = 1;
        //public const Int32 const_CalcModus_halbmonatlich = 2;
        //public const Int32 const_CalcModus_taeglich = 3;

        //public const string const_CalcModusText_default = "Standard";
        //public const string const_CalcModusText_monatlich = "monatlich";
        //public const string const_CalcModusText_halbmonatlich = "halbmonatlich";
        //public const string const_CalcModusText_taeglich = "täglich";

        //public static Dictionary<Int32, string> DictCalcModus()
        //{
        //    Dictionary<Int32, string> tmpDict = new Dictionary<int, string>()
        //    {
        //        {const_CalcModus_Default, const_CalcModusText_default},
        //        {const_CalcModus_monatlich, const_CalcModusText_monatlich},
        //        {const_CalcModus_halbmonatlich, const_CalcModusText_halbmonatlich},
        //        {const_CalcModus_taeglich, const_CalcModusText_taeglich},
        //    };
        //    return tmpDict;
        //}


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

        public string TransDirection = string.Empty;  //für LagerTransprot zur identifizierung IN / OUT Store

        private bool _ExistTarifPos;
        private Int32 _SortIndex;       //legt die angezeigte Reihenfolge der Tarifarten im Grid fest
        public Int32 MaxMengeVorgabe = 100000;   //Vorgabe (quasi unendlich / größte anzunehmende Mengenwert)
        private Int32 _NextOrderID;

        public decimal ID { get; set; }
        public decimal TarifID { get; set; }
        public string BasisEinheit { get; set; }
        public string AbrEinheit { get; set; }
        public bool zeitraumbezogen { get; set; }
        public bool einheitenbezogen { get; set; }
        public decimal PreisEinheit { get; set; }
        public Int32 EinheitenBis { get; set; }
        public Int32 EinheitenVon { get; set; }
        public Int32 Lagerdauer { get; set; }
        public decimal MargePreisEinheit { get; set; }
        public decimal MargeProzentEinheit { get; set; }
        public string TarifPosArt { get; set; }
        public decimal AdrID { get; set; }
        public bool ExistTarifPos
        {
            get
            {
                string strSQL = string.Empty;
                strSQL = "Select ID FROM TarifPositionen WHERE ID=" + ID + ";";
                _ExistTarifPos = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
                return _ExistTarifPos;
            }
            set { _ExistTarifPos = value; }
        }
        public bool MasterPos { get; set; }
        public bool StaffelPos { get; set; }
        public Int32 OrderID { get; set; }
        public bool aktiv { get; set; }
        public string DatenfeldArtikel { get; set; }
        public string Beschreibung { get; set; }
        public string TEinheiten { get; set; }
        public Int32 TEinheitenVon { get; set; }
        public Int32 TEinheitenBis { get; set; }
        public Int32 MaxMengeEinheiten { get; set; }
        public Int32 NextOrderID
        {
            get
            {
                _NextOrderID = GetNextOrderID() + 1;
                return _NextOrderID;
            }
            set { _NextOrderID = value; }
        }
        public Int32 MinMengenEinheiten { get; set; }
        public Int32 MaxMengenEinheitenNewItem { get; set; }
        public Int32 MinMengenEinheitenNewItem { get; set; }
        public bool Pauschal { get; set; }
        public Int32 SortIndex
        {
            get
            {
                switch (this.TarifPosArt)
                {
                    case "Einlagerungskosten":
                        _SortIndex = 1;
                        break;
                    case "Auslagerungskosten":
                        _SortIndex = 2;
                        break;
                    case "Lagerkosten":
                        _SortIndex = 3;
                        break;
                    case "Sperrlagerkosten":
                        _SortIndex = 4;
                        break;
                    case "Vorfracht":
                        _SortIndex = 5;
                        break;
                    case "Direktanlieferung":
                        _SortIndex = 6;
                        break;
                    case "LagerTransportkosten":
                        _SortIndex = 7;
                        break;
                    case "Rücklieferung":
                        _SortIndex = 8;
                        break;
                    case "Nebenkosten":
                        _SortIndex = 9;
                        break;
                    default:
                        _SortIndex = 10;
                        break;
                }
                return _SortIndex;
            }
            set { _SortIndex = value; }
        }
        public Int32 TarifPosVerweis { get; set; } //Verweis für die Staffel, gleiche Staffelteil bekommt den gleichen Verweis und kann darüber gruppiert werden
        public decimal BruttoVon { get; set; }
        public decimal BruttoBis { get; set; }
        public decimal DickeVon { get; set; }
        public decimal DickeBis { get; set; }
        public decimal BreiteVon { get; set; }
        public decimal BreiteBis { get; set; }
        public decimal LaengeVon { get; set; }
        public decimal LaengeBis { get; set; }
        private Int32 _QuantityCalcBase;
        public Int32 QuantityCalcBase
        {
            get
            {
                switch (this.QuantityBase)
                {
                    case const_QuantityBase_strStandard:
                        _QuantityCalcBase = const_QuantityBase_Standard;
                        break;
                    case const_QuantityBase_strEinlagerung:
                        _QuantityCalcBase = const_QuantityBase_Einlagerung;
                        break;
                    case const_QuantityBase_strAuslagerung:
                        _QuantityCalcBase = const_QuantityBase_Auslagerung;
                        break;
                    case const_QuantityBase_strRetoure:
                        _QuantityCalcBase = const_QuantityBase_Retoure;
                        break;
                    case const_QuantityBase_strVerlagerung:
                        _QuantityCalcBase = const_QuantityBase_Verlagerung;
                        break;
                    case const_QuantityBase_strUmbuchung:
                        _QuantityCalcBase = const_QuantityBase_Umbuchung;
                        break;
                    default:
                        _QuantityCalcBase = const_QuantityBase_Standard;
                        break;
                }
                return _QuantityCalcBase;
            }
            set { _QuantityCalcBase = value; }
        }
        public string QuantityBase { get; set; }
        //public Int32 CalcModus { get; set; }
        public enumCalcultationModus CalcModus { get; set; }
        public Int32 CalcModValue { get; set; }  //zP für Abrechnung
        public string _CalcModusText;
        public string CalcModusText
        {
            get
            {
                //switch (this.CalcModus)
                //{
                //    case const_CalcModus_Default:
                //        _CalcModusText = const_CalcModusText_default;
                //        break;
                //    case const_CalcModus_monatlich:
                //        _CalcModusText = const_CalcModusText_monatlich;
                //        break;
                //    case const_CalcModus_halbmonatlich:
                //        _CalcModusText = const_CalcModusText_halbmonatlich;
                //        break;
                //    default:
                //        _CalcModusText = string.Empty;
                //        break;
                //}
                _CalcModusText = CalcModus.ToString();
                return _CalcModusText;
            }
            set { _CalcModusText = value; }
        }


        /****************************************************************************************************
         *                                  Methoden
         * *************************************************************************************************/
        ///<summary>clsTarifPosition / Fill</summary>
        ///<remarks>Füll die Klasse anhand der ID.</remarks>
        public void Fill()
        {
            if (clsTarifPosition.ExistTarifPosition(ID, BenutzerID))
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select a.* " +
                                "FROM TarifPositionen a WHERE a.ID=" + ID + "; ";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "TarifPosition");
                FillClass(ref dt);
            }
        }
        ///<summary>clsTarifPosition / FillByTarifID</summary>
        ///<remarks></remarks>
        public void FillByTarifID()
        {
            if (clsTarifPosition.ExistTarifPosition(ID, BenutzerID))
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select TOP(1) a.* " +
                                "FROM TarifPositionen a WHERE a.TarifID=" + TarifID + "; ";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "TarifPosition");
                FillClass(ref dt);
            }
        }
        ///<summary>clsTarifPosition / FillClass</summary>
        ///<remarks>Füll die Klasse anhand der ID.</remarks>
        private void FillClass(ref DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                decimal decTmp = 0;
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    this.ID = (decimal)dt.Rows[i]["ID"];
                    this.TarifID = (decimal)dt.Rows[i]["TarifID"]; ;
                    this.BasisEinheit = dt.Rows[i]["BasisEinheit"].ToString();
                    this.AbrEinheit = dt.Rows[i]["AbrEinheit"].ToString();
                    this.Lagerdauer = (Int32)dt.Rows[i]["Lagerdauer"];
                    this.zeitraumbezogen = (bool)dt.Rows[i]["Zeitraumbezogen"];
                    this.PreisEinheit = (decimal)dt.Rows[i]["PreisEinheit"];
                    this.einheitenbezogen = (bool)dt.Rows[i]["einheitenbezogen"];
                    this.EinheitenVon = (Int32)dt.Rows[i]["EinheitVon"];
                    this.EinheitenBis = (Int32)dt.Rows[i]["EinheitBis"];
                    this.MargeProzentEinheit = (decimal)dt.Rows[i]["MargeProzentEinheit"];
                    this.MargePreisEinheit = (decimal)dt.Rows[i]["MargePreisEinheit"];
                    this.TarifPosArt = dt.Rows[i]["TarifPosArt"].ToString();
                    this.aktiv = (bool)dt.Rows[i]["aktiv"];
                    this.MasterPos = (bool)dt.Rows[i]["MasterPos"];
                    this.StaffelPos = (bool)dt.Rows[i]["StaffelPos"];
                    this.OrderID = (Int32)dt.Rows[i]["OrderID"];
                    this.DatenfeldArtikel = dt.Rows[i]["DatenfeldArtikel"].ToString();
                    this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                    this.TEinheiten = dt.Rows[i]["TEinheiten"].ToString();
                    this.TEinheitenVon = (Int32)dt.Rows[i]["TEinheitVon"];
                    this.TEinheitenBis = (Int32)dt.Rows[i]["TEinheitBis"];
                    this.Pauschal = (bool)dt.Rows[i]["Pauschal"];
                    //this.SortIndex = (Int32)dt.Rows[i]["SortIndex"]; -> wird automatisch gesetzt
                    this.TarifPosVerweis = (Int32)dt.Rows[i]["TPosVerweis"];

                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["BruttoVon"].ToString(), out decTmp);
                    this.BruttoVon = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["BruttoBis"].ToString(), out decTmp);
                    this.BruttoBis = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["DickeVon"].ToString(), out decTmp);
                    this.DickeVon = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["DickeBis"].ToString(), out decTmp);
                    this.DickeBis = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["BreiteVon"].ToString(), out decTmp);
                    this.BreiteVon = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["BreiteBis"].ToString(), out decTmp);
                    this.BreiteBis = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["LaengeVon"].ToString(), out decTmp);
                    this.LaengeVon = decTmp;
                    decTmp = 0;
                    Decimal.TryParse(dt.Rows[i]["LaengeBis"].ToString(), out decTmp);
                    this.LaengeBis = decTmp;
                    //QuantityBase
                    Int32 iTmp = (Int32)dt.Rows[i]["QuantityCalcBase"];
                    switch (iTmp)
                    {
                        case const_QuantityBase_Standard:
                            this.QuantityBase = const_QuantityBase_strStandard;
                            break;
                        case const_QuantityBase_Einlagerung:
                            this.QuantityBase = const_QuantityBase_strEinlagerung;
                            break;
                        case const_QuantityBase_Auslagerung:
                            this.QuantityBase = const_QuantityBase_strAuslagerung;
                            break;
                        case const_QuantityBase_Retoure:
                            this.QuantityBase = const_QuantityBase_strRetoure;
                            break;
                        case const_QuantityBase_Verlagerung:
                            this.QuantityBase = const_QuantityBase_strVerlagerung;
                            break;
                        case const_QuantityBase_Umbuchung:
                            this.QuantityBase = const_QuantityBase_strUmbuchung;
                            break;
                        default:
                            this.QuantityBase = const_QuantityBase_strStandard;
                            break;
                    }
                    iTmp = 0;
                    Int32.TryParse(dt.Rows[i]["CalcModus"].ToString(), out iTmp);
                    this.CalcModus = EnumConverter.GetEnumObjectByValue<enumCalcultationModus>(iTmp);
                    MinMengenEinheiten = 0;
                    MaxMengeEinheiten = 0;
                    if (StaffelPos)
                    {
                        GetMinMaxValueEinheiten();
                    }
                }
            }
        }
        ///<summary>clsTarifPosition / ExistTarifPosition</summary>
        ///<remarks>Prüft, ob der entsprechende Datensatz existiert.</remarks>
        public static bool ExistTarifPosition(decimal myTarifPosID, decimal myBenutzer)
        {
            if (myTarifPosID > 0)
            {
                string strSQL = string.Empty;
                strSQL = "Select ID FROM TarifPositionen WHERE ID=" + myTarifPosID + ";";
                bool reVal = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, myBenutzer);
                return reVal;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsTarifPosition / AddArbreitsbereich</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB.</remarks>
        public void AddTarifPositionen()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO TarifPositionen (TarifID, BasisEinheit, AbrEinheit, Lagerdauer, Zeitraumbezogen, " +
                                                   "PreisEinheit, EinheitVon, EinheitBis, MargeProzentEinheit, MargePreisEinheit," +
                                                   "TarifPosArt, aktiv, MasterPos, StaffelPos, OrderID, DatenfeldArtikel, Beschreibung, " +
                                                   "einheitenbezogen, TEinheiten, TEinheitVon, Pauschal, SortIndex, TPosVerweis, TEinheitBis" +
                                                   ", BruttoVon, BruttoBis, DickeVon, DickeBis, BreiteVon, BreiteBis, LaengeVon, LaengeBis" +
                                                   ", QuantityCalcBase, CalcModus" +
                                                   ") " +
                                            "VALUES (" + TarifID +
                                                    ",'" + BasisEinheit + "'" +
                                                    ",'" + AbrEinheit + "'" +
                                                    "," + Lagerdauer +
                                                    "," + Convert.ToInt32(zeitraumbezogen) +
                                                    ",'" + PreisEinheit.ToString().Replace(",", ".") + "'" +
                                                    "," + EinheitenVon +
                                                    "," + EinheitenBis +
                                                    ",'" + MargeProzentEinheit.ToString().Replace(",", ".") + "'" +
                                                    ",'" + MargePreisEinheit.ToString().Replace(",", ".") + "'" +
                                                    ",'" + TarifPosArt + "'" +
                                                    "," + Convert.ToInt32(aktiv) +
                                                    "," + Convert.ToInt32(MasterPos) +
                                                    "," + Convert.ToInt32(StaffelPos) +
                                                    "," + OrderID +
                                                    ",'" + DatenfeldArtikel + "'" +
                                                    ",'" + Beschreibung + "'" +
                                                    "," + Convert.ToInt32(einheitenbezogen) +
                                                    ",'" + TEinheiten + "'" +
                                                    "," + TEinheitenVon +
                                                    "," + Convert.ToInt32(Pauschal) +
                                                    "," + SortIndex +
                                                    "," + TarifPosVerweis +
                                                    "," + TEinheitenBis +
                                                    ",'" + BruttoVon.ToString().Replace(",", ".") + "'" +
                                                    ",'" + BruttoBis.ToString().Replace(",", ".") + "'" +
                                                    ",'" + DickeVon.ToString().Replace(",", ".") + "'" +
                                                    ",'" + DickeBis.ToString().Replace(",", ".") + "'" +
                                                    ",'" + BreiteVon.ToString().Replace(",", ".") + "'" +
                                                    ",'" + BreiteBis.ToString().Replace(",", ".") + "'" +
                                                    ",'" + LaengeVon.ToString().Replace(",", ".") + "'" +
                                                    ",'" + LaengeBis.ToString().Replace(",", ".") + "'" +
                                                    ", " + this.QuantityCalcBase +
                                                    ", " + (int)this.CalcModus +
                                                    "); ";
            strSql = strSql + " Select @@IDENTITY; ";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                //Update der Order IDS
                CheckOrderID();
                //Add Logbucheintrag Exception
                clsTarif tmpTarif = new clsTarif();
                tmpTarif._GL_User = this._GL_User;
                tmpTarif.ID = TarifID;
                tmpTarif.Fill();
                string myBeschreibung = "Tarifposition angelegt: " + tmpTarif.Tarifname + " für Adress ID :" + AdrID.ToString();
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);
            }
            Fill();
        }
        ///<summary>clsTarifPosition / CheckOrderID</summary>
        ///<remarks>Organisiert die korrekte Reihenfolge der OrderID. Es werden alle Tarifpositionen ermittelt nach sortierter OrderID.
        ///         Dann wird die OrderID noch einmal durchgezählt und alle entsprechenden Datenfelder aktualisiert. Dies bewirkt, 
        ///         das die OrderID immer in der richtigen Reihenfolgen vergeben wurde und es können auch Datensätze zwischendurch 
        ///         eingesetzt werden.</remarks>
        private void CheckOrderID()
        {
            string strSql = string.Empty;
            strSql = "Select ID, OrderID FROM TarifPositionen WHERE TarifID=" + TarifID + " AND TarifPosArt='" + TarifPosArt + "' Order By OrderID;";

            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "OrderID");

            if (dt.Rows.Count > 0)
            {
                strSql = string.Empty;
                Int32 iCount = 0;

                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    string strID = string.Empty;
                    string strOrderID = string.Empty;
                    decimal decID = 0;
                    Int32 iOrderID = 0;

                    strID = dt.Rows[i]["ID"].ToString();
                    strOrderID = dt.Rows[i]["OrderID"].ToString();

                    Decimal.TryParse(strID, out decID);
                    Int32.TryParse(strOrderID, out iOrderID);
                    if (
                         (this.ID != decID)
                       )
                    {
                        //wenn die gleiche OrderID vorliegt, dann muss doppelt erhöht werden
                        if (iOrderID == this.OrderID)
                        {
                            iCount++;
                        }
                        iCount++;
                        strSql = strSql + "Update TarifPositionen Set OrderID=" + iCount + " WHERE ID=" + decID + ";";
                    }
                    /***
                else
                {
                    iCount--;
                }
                ***/
                }

                //Update per Transaktion
                if (strSql != string.Empty)
                {
                    clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "UpdateTarifPositionen", BenutzerID);
                }
            }
        }
        ///<summary>clsTarifPosition / AddArbreitsbereich</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB.</remarks>
        public void UpdateTarifPositionen()
        {
            if (clsTarifPosition.ExistTarifPosition(ID, BenutzerID))
            {
                string strSql = string.Empty;
                strSql = "Update TarifPositionen SET BasisEinheit ='" + BasisEinheit + "'" +
                                                    ", AbrEinheit ='" + AbrEinheit + "'" +
                                                    ", Lagerdauer =" + Lagerdauer +
                                                    ", Zeitraumbezogen =" + Convert.ToInt32(zeitraumbezogen) +
                                                    ", PreisEinheit ='" + PreisEinheit.ToString().Replace(",", ".") + "'" +
                                                    ", EinheitVon =" + EinheitenVon +
                                                    ", EinheitBis =" + EinheitenBis +
                                                    ", MargeProzentEinheit ='" + MargeProzentEinheit.ToString().Replace(",", ".") + "'" +
                                                    ", MargePreisEinheit ='" + MargePreisEinheit.ToString().Replace(",", ".") + "'" +
                                                    ", TarifPosArt='" + TarifPosArt + "'" +
                                                    ", aktiv=" + Convert.ToInt32(aktiv) +
                                                    ", MasterPos=" + Convert.ToInt32(MasterPos) +
                                                    ", StaffelPos=" + Convert.ToInt32(StaffelPos) +
                                                    ", OrderID=" + OrderID +
                                                    ", DatenfeldArtikel='" + DatenfeldArtikel + "'" +
                                                    ", Beschreibung='" + Beschreibung + "'" +
                                                    ", einheitenbezogen=" + Convert.ToInt32(einheitenbezogen) +
                                                    ", TEinheiten = '" + TEinheiten + "'" +
                                                    ", TEinheitVon=" + TEinheitenVon +
                                                    ", Pauschal=" + Convert.ToInt32(Pauschal) +
                                                    ", SortIndex=" + SortIndex +
                                                    ", TPosVerweis=" + TarifPosVerweis +
                                                    ", TEinheitBis=" + TEinheitenBis +
                                                    ", BruttoVon='" + BruttoVon.ToString().Replace(",", ".") + "'" +
                                                    ", BruttoBis='" + BruttoBis.ToString().Replace(",", ".") + "'" +
                                                    ", DickeVon='" + DickeVon.ToString().Replace(",", ".") + "'" +
                                                    ", DickeBis='" + DickeBis.ToString().Replace(",", ".") + "'" +
                                                    ", BreiteVon='" + BreiteVon.ToString().Replace(",", ".") + "'" +
                                                    ", BreiteBis='" + BreiteBis.ToString().Replace(",", ".") + "'" +
                                                    ", LaengeVon='" + LaengeVon.ToString().Replace(",", ".") + "'" +
                                                    ", LaengeBis='" + LaengeBis.ToString().Replace(",", ".") + "'" +
                                                    ", QuantityCalcBase=" + this.QuantityCalcBase +
                                                    ", CalcModus =" + (int)this.CalcModus +

                                                    " WHERE ID=" + ID + ";";

                if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
                {
                    //Add Logbucheintrag Exception
                    string myBeschreibung = "Tarifposition geändert: TarifpositionsID:" + ID.ToString();
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), myBeschreibung);
                }
                Fill();
            }
        }
        ///<summary>clsTarifPosition / AddArbreitsbereich</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB.</remarks>
        public void UpdateTarifPositionenStaffel(bool bIsMasterPos)
        {
            string strSql = string.Empty;
            strSql = "Update TarifPositionen SET BasisEinheit ='" + BasisEinheit + "'" +
                                                ", AbrEinheit ='" + AbrEinheit + "'" +
                                                ", EinheitVon =" + EinheitenVon +
                                                ", EinheitBis =" + EinheitenBis +
                                                ", StaffelPos=" + Convert.ToInt32(StaffelPos) +
                                                //", OrderID=" + OrderID +
                                                ", DatenfeldArtikel='" + DatenfeldArtikel + "'" +
                                                ", Beschreibung='" + Beschreibung + "'" +
                                                ", einheitenbezogen=" + Convert.ToInt32(einheitenbezogen) + " ";

            if (bIsMasterPos)
            {
                strSql = strSql + ", TPosVerweis=" + TarifPosVerweis +
                                    " WHERE " +
                                        "TarifID=" + TarifID + " " +
                                        "AND TarifPosArt='" + TarifPosArt + "' " +
                                        "AND MasterPos=1";
            }
            else
            {
                strSql = strSql + " WHERE " +
                                                    "TarifID=" + TarifID + " " +
                                                    "AND TarifPosArt='" + TarifPosArt + "' " +
                                                    "AND TPosVerweis=" + TarifPosVerweis + ";";
            }
            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
                /***
                //Add Logbucheintrag Exception
                string myBeschreibung = "Tarifposition geändert: TarifpositionsID:" + ID.ToString();
                Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), myBeschreibung);
                ***/
            }
        }
        ///<summary>clsTarifPosition / AddArbreitsbereich</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB.</remarks>
        public void UpdateTarifPositionenAllStaffelPositonBezeichnung()
        {
            string strSql = string.Empty;
            strSql = "Update TarifPositionen SET Beschreibung ='" + Beschreibung + "' " +
                                            " WHERE " +
                                                "TarifID=" + TarifID + " " +
                                                "AND TPosVerweis='" + TarifPosVerweis + "' ;";
            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
                /***
                //Add Logbucheintrag Exception
                string myBeschreibung = "Tarifposition geändert: TarifpositionsID:" + ID.ToString();
                Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), myBeschreibung);
                ***/
            }
        }
        ///<summary>clsTarifPosition / UpdateTarifPositionenSetRestStaffelMaster</summary>
        ///<remarks></remarks>
        public void UpdateTarifPositionenSetRestStaffelMaster(string myTarifPosArt,
                                                              string myBeschreibung,
                                                              decimal myTarifID,
                                                              Int32 myTPosVerweis,
                                                              bool SetStaffel)
        {
            string strSql = string.Empty;

            strSql = "Update TarifPositionen SET BasisEinheit ='kg'" +
                                                ", AbrEinheit ='to'" +
                                                ", EinheitVon = 0 " +
                                                ", EinheitBis =0 " +
                                                ", StaffelPos=" + Convert.ToInt32(SetStaffel) +
                                                ", OrderID=1 " +
                                                ", DatenfeldArtikel='Brutto'" +
                                                ", Beschreibung='" + myBeschreibung + "'" +
                                                ", einheitenbezogen=" + Convert.ToInt32(SetStaffel) +
                                                ", PreisEinheit=0 " +

                                                ", TPosVerweis=" + myTPosVerweis +
                                                " WHERE " +
                                                    "TarifID=" + myTarifID + " " +
                                                    "AND TarifPosArt='" + myTarifPosArt + "' " +
                                                    "AND MasterPos=1";

            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
                /***
                //Add Logbucheintrag Exception
                string myBeschreibung = "Tarifposition geändert: TarifpositionsID:" + ID.ToString();
                Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Aenderung.ToString(), myBeschreibung);
                ***/
            }
        }
        ///<summary>clsTarifPosition / DeleteTarifPositionenByID</summary>
        ///<remarks>Datensatzes in die DB wird gelöscht.</remarks>
        public void DeleteTarifPositionenByID()
        {
            string strSql = string.Empty;
            strSql = "Delete TarifPositionen WHERE ID=" + ID + ";";

            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
                CheckOrderID();
                //Add Logbucheintrag Exception
                string myBeschreibung = "Tarifposition gelöscht: TarifpositionsID" + ID.ToString();
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
            }
        }
        ///<summary>clsTarifPosition / DeleteTarifPositionenSubTarifPos</summary>
        ///<remarks>Löscht alle SubTarifPos einer Aberchnungsart. (MasterPos=false)</remarks>
        public void DeleteStaffelTarifPositionenByTarifPosVerweis(decimal myTarifID, Int32 myTarifPosVerweis)
        {
            string strSql = string.Empty;
            strSql = "Delete TarifPositionen WHERE " +
                                                "TarifID=" + myTarifID + " AND  TPosVerweis=" + myTarifPosVerweis + " " +
                                                "AND MasterPos=0;";

            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
                //BAustelle gelöschte Positionen 
                //Add Logbucheintrag Exception
                //string myBeschreibung = "Tarifposition gelöscht: TarifpositionsID" + ID.ToString();
                //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
            }
        }
        ///<summary>clsTarifPosition / DeleteStaffelTarifPositionen</summary>
        ///<remarks>Löscht alle Positionen einer Staffel</remarks>
        public void DeleteStaffelTarifPositionen(decimal myTarifID, string myTarifPosArt)
        {
            string strSql = string.Empty;
            strSql = "Delete TarifPositionen WHERE " +
                                                "TarifID=" + TarifID + " AND  TarifPosArt='" + myTarifPosArt + "' " +
                                                "AND MasterPos=0;";

            if (clsSQLcon.ExecuteSQL(strSql, BenutzerID))
            {
                //BAustelle gelöschte Positionen 
                //Add Logbucheintrag Exception
                //string myBeschreibung = "Tarifposition gelöscht: TarifpositionsID" + ID.ToString();
                //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
            }
        }
        ///<summary>clsTarifPosition / DeleteTarifPositionenSubTarifPos</summary>
        ///<remarks></remarks>
        public static decimal GetTarifPosIDFromMasterPos(Globals._GL_USER myGLUser, string strTarifPosArt, decimal decTarifID)
        {
            string strSql = string.Empty;
            strSql = "Select ID FROM TarifPositionen WHERE " +
                                                     "TarifPosArt='" + strTarifPosArt + "' AND " +
                                                     "TarifID=" + decTarifID + " AND " +
                                                     "MasterPos='True' ;";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGLUser.User_ID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        ///<summary>clsTarifPosition / DeleteTarifPositionenSubTarifPos</summary>
        ///<remarks></remarks>
        public static DataTable GetTarifPositionStaffelbyAbrArtAndTarifID(Globals._GL_USER myGLUser, string strTarifPosArt, decimal decTarifID)
        {
            string strSql = string.Empty;
            strSql = "Select ID, MasterPos FROM TarifPositionen WHERE " +
                                                     "TarifPosArt='" + strTarifPosArt + "' AND " +
                                                     "TarifID=" + decTarifID + " AND " +
                                                     "aktiv=1;";

            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "TarifPositionenID");
            return dt;
        }
        ///<summary>clsTarifPosition / DeleteTarifPositionenSubTarifPos</summary>
        ///<remarks></remarks>
        public decimal GetLastTarifPositionStaffel(string strTarifPosArt, decimal decTarifID)
        {
            string strSql = string.Empty;
            strSql = "Select TOP(1) ID FROM TarifPositionen " +
                                        "WHERE TarifPosArt='" + strTarifPosArt + "' " +
                                              "AND TarifID=" + decTarifID + " " +
                                              "ORDER BY TPosVerweis DESC ;";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        ///<summary>clsTarifPosition / GetTarifePositionen</summary>
        ///<remarks>Ermittel anhand der Tarif ID die Tarifpositionen eines Tarifs</remarks>
        ///<returns>Returns DataTable</returns>
        public static DataTable GetTarifePositionen(Globals._GL_USER myGL_User, decimal myDecTarifID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select" +
                            " a.TarifPosArt AS Art" +
                            ", a.Beschreibung" +
                            ", a.BasisEinheit" +
                            ", a.AbrEinheit" +
                            ", a.EinheitVon AS [von Einheit] " +
                            ", a.EinheitBis AS [bis Einheit]" +
                            ", a.PreisEinheit AS [€/Einheit]" +
                            ", a.MargeProzentEinheit AS [Marge %]" +
                            ", a.MargePreisEinheit AS [Marge €]" +
                            ", a.Lagerdauer" +
                            ", a.Zeitraumbezogen" +
                            ", a.aktiv" +
                            ", a.MasterPos" +
                            ", a.ID" +
                            ", a.TarifID" +
                            ", a.OrderID" +
                            ", a.StaffelPos" +
                            ", a.TPosVerweis" +
                            ", a.SortIndex" +
                            ", a.BruttoVon as [Gewicht von]" +
                            ", a.BruttoBis as [Gewicht bis]" +
                            ", a.DickeVon as [Dicke von]" +
                            ", a.DickeBis as [Dicke bis]" +
                            ", a.BreiteVon as [Breite von]" +
                            ", a.BreiteBis as [Breite bis]" +
                            ", a.LaengeVon as [Länge von]" +
                            ", a.LaengeBis as [Länge bis]" +

                            " FROM TarifPositionen a " +
                                "INNER JOIN Tarife ON Tarife.ID=a.TarifID " +
                                "INNER JOIN KundenTarife ON KundenTarife.TarifID=Tarife.ID " +
                                "WHERE Tarife.ID=" + myDecTarifID + " " +
                                //"AND a.aktiv=1 " +
                                "ORDER BY a.SortIndex, a.OrderID ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGL_User.User_ID, "Tarifpositionen");
            return dt;
        }
        ///<summary>clsTarifPosition / GetTarifePositionenStaffel</summary>
        ///<remarks>Ermittel anhand der Tarif ID die Staffel einer Tarifpositionen eines Tarifs</remarks>
        ///<returns>Returns DataTable</returns>
        public DataTable GetTarifePositionenStaffel(decimal myTarifID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select" +
                            " a.Beschreibung" +
                            ", a.EinheitVon" +
                            ", a.EinheitBis" +
                            ", a.DatenfeldArtikel" +
                            ", a.BasisEinheit" +
                            ", a.AbrEinheit" +
                            ", a.MasterPos" +
                            ",  CASE " +
                                "WHEN TPosVerweis=0 " +
                                "THEN (Select MAX(TPosVerweis)+1 FROM TarifPositionen) " +
                                "ELSE TPosVerweis " +
                                "END as TPosVerweisID " +

                            " FROM TarifPositionen a " +
                                "INNER JOIN Tarife ON Tarife.ID=a.TarifID " +
                                "INNER JOIN KundenTarife ON KundenTarife.TarifID=Tarife.ID " +
                                "WHERE Tarife.ID=" + myTarifID + " " +
                                "AND a.aktiv=1 AND TPosVerweis>0 " +
                                "Group BY " +
                                    " a.Beschreibung" +
                                    ", a.TPosVerweis " +
                                    ", a.EinheitVon" +
                                    ", a.EinheitBis" +
                                    ", a.DatenfeldArtikel" +
                                    ", a.BasisEinheit" +
                                    ", a.AbrEinheit" +
                                    ", a.MasterPos " +
                                "ORDER BY TPosVerweisID;";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "StaffelTarifpositionen");
            return dt;
        }
        ///<summary>clsTarifPosition / UmrechnungPreisEinheit</summary>
        ///<remarks></remarks>
        public void UmrechnungPreisEinheit()
        {
            if (BasisEinheit != AbrEinheit)
            {
                //Umrechnung für vorgegebene Einheiten
                switch (BasisEinheit)
                {
                    case "to":
                    case "TO":
                    case "To":
                        if ((AbrEinheit == "kg") || (AbrEinheit == "Kg") || (AbrEinheit == "KG"))
                        {
                            PreisEinheit = PreisEinheit / 1000;
                        }
                        break;

                    case "kg":
                    case "Kg":
                    case "KG":
                        if ((AbrEinheit == "to") || (AbrEinheit == "To") || (AbrEinheit == "TO"))
                        {
                            PreisEinheit = PreisEinheit / 1000;
                        }
                        break;
                }

            }
        }

        ///<summary>clsTarifPosition / GetMaxEinheiten</summary>
        ///<remarks>Ermittel den Maxwert Einheiten Bis im entsprechenden Tarifteil</remarks>
        public Int32 GetNextOrderID()
        {
            string strSql = string.Empty;
            strSql = "Select MAX(OrderID) FROM TarifPositionen WHERE " +
                                                "TarifPosArt='" + TarifPosArt + "' AND " +
                                                "TarifID=" + TarifID + ";";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }
        ///<summary>clsTarifPosition / GetMaxEinheiten</summary>
        ///<remarks>Ermittel den Maxwert Einheiten Bis im entsprechenden Tarifteil</remarks>
        public bool CheckStaffelMember(decimal myTarifID, Int32 myVerweisID, string myTarifPosArt)
        {
            string strSql = string.Empty;
            strSql = "Select ID FROM TarifPositionen WHERE " +
                                                        "TarifID=" + myTarifID + " " +
                                                        "AND TPosVerweis=" + myVerweisID + " " +
                                                        "AND TarifPosArt='" + myTarifPosArt + "' ;";


            bool bResult = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            return bResult;
        }
        ///<summary>clsTarifPosition / GetNextTarifPosVerweis</summary>
        ///<remarks>Ermittel den Maxwert für den TArifPosVerweis und erhöht Ihn um eins.</remarks>
        public Int32 GetNextTarifPosVerweis()
        {
            string strSql = string.Empty;
            strSql = "Select MAX(TPosVerweis) FROM TarifPositionen ;";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp + 1;
        }
        ///<summary>clsTarifPosition / GetMinMaxValueEinheiten</summary>
        ///<remarks></remarks>
        private void GetMinMaxValueEinheiten()
        {
            string strSql = string.Empty;
            strSql =
                      "Select " +
                            "a.EinheitVon as MinMengenEinheiten " +
                            ", a.EinheitBis as MaxMengenEinheiten " +
                            ", CASE " +
                                "WHEN (Select Top(1) x.EinheitBis FROM TarifPositionen x " +
                                                                  "WHERE x.TarifID=a.TarifID AND x.TarifPosArt='" + TarifPosArt + "' " +
                                                                  "AND x.OrderID=(" + OrderID + ") ORDER BY ID DESC) >0 " +
                                "THEN (Select Top(1) x.EinheitBis + 1 FROM TarifPositionen x " +
                                                                  "WHERE x.TarifID=a.TarifID AND x.TarifPosArt='" + TarifPosArt + "' " +
                                                                  "AND x.OrderID=(" + OrderID + ") ORDER BY ID DESC) " +
                                "ELSE a.EinheitBis +1 " +
                            "END as MinMengenEinheitenNewItem " +
                            ", CASE " +
                                "WHEN (Select Top(1) x.EinheitVon FROM TarifPositionen x " +
                                                                  "WHERE x.TarifID=a.TarifID AND x.TarifPosArt='" + TarifPosArt + "' " +
                                                                  "AND x.OrderID=(" + OrderID + "+1) ORDER BY ID DESC)>0 " +
                                "THEN (Select Top(1) x.EinheitVon - 1 FROM TarifPositionen x " +
                                                                 "WHERE x.TarifID=a.TarifID AND x.TarifPosArt='" + TarifPosArt + "' " +
                                                                 "AND x.OrderID=(" + OrderID + "+1) ORDER BY ID DESC) " +
                                "ELSE " + MaxMengeVorgabe + " " +
                            "END as MaxMengenEinheitenNewItem " +

                           "FROM TarifPositionen a " +
                                        "INNER JOIN Tarife ON Tarife.ID=a.TarifID " +
                                        "INNER JOIN KundenTarife ON KundenTarife.TarifID=Tarife.ID " +
                                                "WHERE a.TarifPosArt='" + TarifPosArt + "' AND " +
                                                "a.TarifID=" + TarifID + " AND " +
                                                "a.OrderID=" + OrderID + " ;";

            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "MinMaxValue");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    string strTmp = string.Empty;
                    Int32 iTmp = 0;
                    strTmp = dt.Rows[i]["MinMengenEinheiten"].ToString();
                    Int32.TryParse(strTmp, out iTmp);
                    MinMengenEinheiten = iTmp;

                    strTmp = string.Empty;
                    iTmp = 0;
                    strTmp = dt.Rows[i]["MaxMengenEinheiten"].ToString();
                    Int32.TryParse(strTmp, out iTmp);
                    MaxMengeEinheiten = iTmp;

                    strTmp = string.Empty;
                    iTmp = 0;
                    strTmp = dt.Rows[i]["MinMengenEinheitenNewItem"].ToString();
                    Int32.TryParse(strTmp, out iTmp);
                    MinMengenEinheitenNewItem = iTmp;

                    strTmp = string.Empty;
                    iTmp = 0;
                    strTmp = dt.Rows[i]["MaxMengenEinheitenNewItem"].ToString();
                    Int32.TryParse(strTmp, out iTmp);
                    MaxMengenEinheitenNewItem = iTmp;
                }
            }
        }
        ///<summary>clsTarifPosition / ListStringQuantityBase</summary>
        ///<remarks></remarks>
        public static List<string> ListStringQuantityBase()
        {
            List<string> ListQuantityBase = new List<string>();
            ListQuantityBase.Add(const_QuantityBase_strStandard);
            ListQuantityBase.Add(const_QuantityBase_strAuslagerung);
            ListQuantityBase.Add(const_QuantityBase_strEinlagerung);
            ListQuantityBase.Add(const_QuantityBase_strRetoure);
            ListQuantityBase.Add(const_QuantityBase_strUmbuchung);
            ListQuantityBase.Add(const_QuantityBase_strVerlagerung);

            return ListQuantityBase;
        }
        ///<summary>clsTarifPosition / AddTarifMasterPosByTarifID</summary>
        ///<remarks></remarks>
        public static void AddTarifMasterPosByTarifID(Globals._GL_USER _GL_USER, string strBezeichnung, decimal TarifID)
        {
            string sql = "insert into TarifPositionen " +
                         "(TarifID,BasisEinheit,AbrEinheit,Lagerdauer,Zeitraumbezogen,PreisEinheit,EinheitVon,EinheitBis, " +
                         "MargeProzentEinheit,MargePreisEinheit,TarifPosArt,aktiv,MasterPos, " +
                         "StaffelPos,OrderID,DatenfeldArtikel,Beschreibung,einheitenbezogen, " +
                         "TEinheiten,TEinheitVon,Pauschal,SortIndex,TPosVerweis,TEinheitBis)	" +
                         " VALUES(" + TarifID + ",'KG','KG',0,0,0,0,0 ,0.00,0.00,'" + strBezeichnung + "',1,1,0,1," +
                         " 'Brutto','Standart',0,'',0,0,10,0,0)";
            clsSQLcon.ExecuteSQL(sql, _GL_USER.User_ID);
        }

        /////<summary>clsTarifPosition / AddTarifMasterPosByTarifID</summary>
        /////<remarks></remarks>
        //public static void AddTarifMasterPosByTarifID(Globals._GL_USER _GL_USER, string strBezeichnung, decimal TarifID)
        //{
        //    string sql = "insert into TarifPositionen " +
        //                 "(TarifID,BasisEinheit,AbrEinheit,Lagerdauer,Zeitraumbezogen,PreisEinheit,EinheitVon,EinheitBis, " +
        //                 "MargeProzentEinheit,MargePreisEinheit,TarifPosArt,aktiv,MasterPos, " +
        //                 "StaffelPos,OrderID,DatenfeldArtikel,Beschreibung,einheitenbezogen, " +
        //                 "TEinheiten,TEinheitVon,Pauschal,SortIndex,TPosVerweis,TEinheitBis)	" +
        //                 " VALUES(" + TarifID + ",'KG','KG',0,0,0,0,0 ,0.00,0.00,'" + strBezeichnung + "',1,1,0,1," +
        //                 " 'Brutto','Standart',0,'',0,0,10,0,0)";
        //    clsSQLcon.ExecuteSQL(sql, _GL_USER.User_ID);
        //}

    }
}
