using LVS.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace LVS
{
    public class clsLEingang
    {
        public clsADR AdrAuftraggeber;
        public clsADR AdrEmpfaenger;
        public clsADR AdrVersender;

        public const string const_DBTableName = "LEingang";
        public clsADRMan AdrManuell = new clsADRMan();

        public const string EingangField_LEingangID = "LEingang.LEingangID";
        public const string EingangField_Date = "LEingang.Date";
        public const string EingangField_Auftraggeber = "LEingang.Auftraggeber";
        public const string EingangField_Empfaenger = "LEingang.Empfaenger";
        public const string EingangField_Lieferant = "LEingang.Lieferant";
        public const string EingangField_LfsNr = "LEingang.LfsNr";
        public const string EingangField_Versender = "LEingang.Versender";
        public const string EingangField_SpedID = "LEingang.SpedID";
        public const string EingangField_KFZ = "LEingang.KFZ";
        public const string EingangField_WaggonNo = "LEingang.WaggonNo";
        public const string EingangField_BeladeID = "LEingang.BeladeID";
        public const string EingangField_EntladeID = "LEingang.EntladeID";
        public const string EingangField_ExTransportRef = "LEingang.ExTransportRef";
        public const string EingangField_ExAuftragRef = "LEingang.ExAuftragRef";
        public const string EingangField_ASNRef = "LEingang.ASNRef";
        public const string EingangField_Fahrer = "LEingang.Fahrer";
        public const string EingangField_Retoure = "LEingang.Retoure";
        public const string EingangField_Verlagerung = "LEingang.Verlagerung";
        public const string EingangField_Umbuchung = "LEingang.Umbuchung";
        public const string EingangField_LagerTransport = "LEingang.LagerTransport";
        public const string EingangField_Ship = "LEingang.Ship";
        public const string EingangField_IsShip = "LEingang.IsShip";
        public const string EingangField_DirektDelivery = "LEingang.DirektDelivery";



        public Globals._GL_USER _GL_User;
        //************  User  ***************
        private clsSystem _sys;
        public clsSystem sys
        {
            get { return _sys; }
            set
            {
                _sys = value;
                if ((this._sys != null) && (this._sys.AbBereich is clsArbeitsbereiche))
                {
                    this.AbBereichID = _sys.AbBereich.ID;
                    this.MandantenID = _sys.AbBereich.MandantenID;
                }
            }
        }
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

        public DataTable dtArtInLEingang = new DataTable("ArtikelLEingang");

        //Lagereingang Kopfdaten
        private decimal _LEingangTableID;
        public decimal LEingangTableID
        {
            get
            {
                return _LEingangTableID;
            }
            set
            {
                _LEingangTableID = value;
                this.Stat = ClsStatus.initialized;
            }
        }
        public decimal LEingangID { get; set; }

        private DateTime _LEingangDate = new DateTime(1900, 1, 1);
        public DateTime LEingangDate
        {
            get
            {
                //if (_LEingangDate < Globals.DefaultDateTimeMinValue)
                //{
                //    _LEingangDate = Globals.DefaultDateTimeMinValue;
                //}
                return _LEingangDate;
            }
            set
            {
                _LEingangDate = value;
            }
        }
        public decimal Auftraggeber { get; set; }
        public decimal Versender { get; set; }
        public decimal Empfaenger { get; set; }
        public string Lieferant { get; set; }
        public decimal AbBereichID { get; set; }
        public decimal MandantenID { get; set; }
        public string LEingangLfsNr { get; set; }
        public decimal ASN { get; set; }
        public bool Checked { get; set; }
        public decimal SpedID { get; set; }
        public string KFZ { get; set; }
        public bool DirektDelivery { get; set; }
        public bool bAllArtikelArePlacedInStore { get; set; }
        public Int32 Artikelanzahl { get; set; }
        public bool Retoure { get; set; }
        public bool Verlagerung { get; set; }
        public bool Umbuchung { get; set; }
        public bool Vorfracht { get; set; }
        public bool LagerTransport { get; set; }
        public string WaggonNr { get; set; }
        public decimal BeladeID { get; set; }
        public decimal EntladeID { get; set; }
        public bool IsPrintDoc { get; set; }
        public bool IsPrintAnzeige { get; set; }
        public bool IsPrintLfs { get; set; }
        public string ExTransportRef { get; set; }
        public string ExAuftragRef { get; set; }
        public string ASNRef { get; set; }
        public decimal LockedBy { get; set; }
        public bool IsWaggon { get; set; }
        public string Fahrer { get; set; }
        public string EAIDalteLVS { get; set; }
        public bool IsPrintList { get; set; }

        public ClsStatus Stat { get; set; }
        public string LEingangChangingText { get; set; }
        public string Brutto
        {
            get
            {
                string sql = "Select SUM(Cast(Brutto as int)) from Artikel where LEingangTableID=" + LEingangTableID;
                return LVS.clsSQLcon.ExecuteSQL_GetValue(sql, _GL_User.User_ID);
            }
        }
        public string Netto
        {
            get
            {
                string sql = "Select SUM(Cast(Netto as int)) from Artikel where LEingangTableID=" + LEingangTableID;
                return LVS.clsSQLcon.ExecuteSQL_GetValue(sql, _GL_User.User_ID);
            }
        }
        public string ArtikelCount
        {
            get
            {
                string sql = "Select Count(*) from Artikel where LEingangTableID=" + LEingangTableID;
                return LVS.clsSQLcon.ExecuteSQL_GetValue(sql, _GL_User.User_ID);
            }
        }
        public int ArtikelCountInStore
        {
            get
            {
                string sql = "Select Count(*) from Artikel " +
                                               " where " +
                                                    "LEingangTableID=" + LEingangTableID +
                                                    " AND LAusgangTableID=0 " +
                                                    " AND ID NOT IN (" +
                                                                   "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                                                                         "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                                                                 ");";
                string strTmp = LVS.clsSQLcon.ExecuteSQL_GetValue(sql, _GL_User.User_ID);
                int iTmp = 0;
                int.TryParse(strTmp, out iTmp);
                return iTmp;
            }
        }
        public bool AllArtikelChecked
        {
            get
            {
                bool bAllArtikelChecked = clsArtikel.CheckAllArtikelChecked_Eingang(BenutzerID, this.LEingangTableID);
                return bAllArtikelChecked;
            }
        }
        public bool AllArtikelLabelPrinted
        {
            get
            {
                bool bAllPrinted = clsArtikel.CheckAllArtikelLabelPrinted_Eingang(BenutzerID, this.LEingangTableID);
                return bAllPrinted;
            }
        }
        //Ermittel, ob bei einem Artikel das Korrektur udn Stornierverfahren angewendet wurde
        public bool IsKorrStVerfahrenInUse
        {
            get
            {
                bool retBool = false;
                string strReturn = string.Empty;
                string strSql = "Select Count(a.ID) FROM Artikel a " +
                                                "WHERE a.IsKorStVerUse=1 and a.LEingangTableID=" + (Int32)this.LEingangTableID + ";";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                Int32 iTmp = 0;
                Int32.TryParse(strTmp, out iTmp);
                if (iTmp > 0)
                {
                    retBool = true;
                }
                return retBool;
            }
        }
        //Eingang aus Umbuchung
        public bool IsUB
        {
            get
            {
                bool retBool = false;
                if (this.dtArtInLEingang.Rows.Count > 0)
                {
                    foreach (DataRow row in dtArtInLEingang.Rows)
                    {
                        decimal decTmp = 0;
                        Decimal.TryParse(row["ArtIDAlt"].ToString(), out decTmp);
                        if (decTmp > 0)
                        {

                            retBool = (decTmp > 0);
                            break;
                        }
                    }
                }
                return retBool;
            }
        }

        public DateTime FirstCheckDateTime { get; set; }
        //Zwischenspeicher beim Einlesen der ASN-XML
        public string ZP_AuftragPosNo { get; set; }
        public List<decimal> ListArtWithSchaden { get; set; }
        public List<decimal> ListArtInSPL { get; set; }
        public string Ship { get; set; }
        public bool IsShip { get; set; }
        public bool ExistEdiCommunication
        {
            get
            {
                bool bRet = false;


                return bRet;
            }
        }


        /**********************************************************************************************************************
         *                                             LEingang - Methoden
         * ********************************************************************************************************************/
        ///<summary>clsLEingang / InitDefaultClsEingang</summary>
        ///<remarks>Erstellt einen Standardeingang mit Standardwerten</remarks>
        public void InitDefaultClsEingang(Globals._GL_USER myGLUser, clsSystem mySys)
        {
            this.sys = mySys;
            this._GL_User = myGLUser;
            this.Stat = ClsStatus.initialized;


            this.AbBereichID = 0;
            if ((this.sys is clsSystem) && (this.sys.AbBereich is clsArbeitsbereiche))
            {
                this.AbBereichID = this.sys.AbBereich.ID;
            }
            this.MandantenID = 0;
            if ((this.sys is clsSystem) && (this.sys.AbBereich is clsArbeitsbereiche))
            {
                this.MandantenID = this.sys.AbBereich.MandantenID;
            }
            this.Auftraggeber = 0;
            this.Empfaenger = 0;
            this.Lieferant = string.Empty;
            this.LEingangDate = DateTime.Now;
            this.LEingangLfsNr = string.Empty;
            this.ASN = 0;
            this.Checked = false;
            this.Versender = 0;
            this.SpedID = 0;
            this.KFZ = string.Empty;
            this.DirektDelivery = false;
            this.Retoure = false;
            this.Umbuchung = false;
            this.Verlagerung = false;
            this.Vorfracht = false;
            this.LagerTransport = false;
            this.WaggonNr = string.Empty;
            this.BeladeID = 0;
            this.EntladeID = 0;
            this.IsPrintDoc = false;
            this.IsPrintAnzeige = false;
            this.IsPrintLfs = false;
            this.ExTransportRef = string.Empty;
            this.ASNRef = string.Empty;
            this.IsWaggon = false;
            this.Fahrer = string.Empty;
            this.Ship = string.Empty;
            this.IsShip = false;
        }
        ///<summary>clsLEingang / Copy</summary>
        ///<remarks></remarks>
        public clsLEingang Copy()
        {
            return (clsLEingang)this.MemberwiseClone();
        }
        ///<summary>clsLEingang / AddLagerEingangSQL</summary>
        ///<remarks></remarks>
        public string AddLagerEingangSQL()
        {
            string strSql = string.Empty;
            strSql =
                    //" BEGIN TRANSACTION " +
                    clsPrimeKeys.GetNEWLEingangIDSQL(MandantenID, AbBereichID, 0) +
                    " INSERT INTO LEingang (LEingangID, Date, Auftraggeber, Empfaenger, Lieferant, AbBereich, Mandant, LfsNr, " +
                                            "ASN, Versender, SpedID, KFZ, DirectDelivery, Retoure, Vorfracht, LagerTransport, " +
                                            "WaggonNo, BeladeID, EntladeID, ExTransportRef, ExAuftragRef, ASNRef, IsWaggon, " +
                                            "Fahrer , Ship, IsShip, Verlagerung, Umbuchung" +
                                           ") " +
                                            "VALUES (( " + clsPrimeKeys.GetNEWLEingangIDSQL(MandantenID, AbBereichID, 1) + ")" +
                                                    ", '" + LEingangDate + "'" +
                                                    ", " + (Int32)Auftraggeber +
                                                    ", " + (Int32)Empfaenger +
                                                    ", '" + Lieferant + "'" +
                                                    ", " + (Int32)AbBereichID +
                                                    ", " + (Int32)MandantenID +
                                                    ", '" + LEingangLfsNr + "'" +
                                                    ", " + (Int32)ASN +
                                                    ", " + (Int32)Versender +
                                                    ", " + (Int32)SpedID +
                                                    ", '" + KFZ + "'" +
                                                    ", " + Convert.ToInt32(DirektDelivery) +
                                                    ", " + Convert.ToInt32(Retoure) +
                                                    ", " + Convert.ToInt32(Vorfracht) +
                                                    ", " + Convert.ToInt32(LagerTransport) +
                                                    ", '" + WaggonNr + "'" +
                                                    ", " + (Int32)BeladeID +
                                                    ", " + (Int32)EntladeID +
                                                    ", '" + ExTransportRef + "'" +
                                                    ", '" + ExAuftragRef + "'" +
                                                    ", '" + ASNRef + "'" +
                                                    ", " + Convert.ToInt32(IsWaggon) +
                                                    ", '" + Fahrer + "'" +
                                                    ", '" + this.Ship + "'" +
                                                    ", " + Convert.ToInt32(this.IsShip) +
                                                    ", " + Convert.ToInt32(this.Verlagerung) +
                                                    ", " + Convert.ToInt32(this.Umbuchung) +
                                                    ");";
            // " COMMIT TRANSACTION ";
            return strSql;
        }
        ///<summary>clsLEingang / AddLagerEingang</summary>
        ///<remarks>Eintrag eines eines neuen Datensatzes in die DB.</remarks>
        public bool AddLagerEingang()
        {
            bool bAddOK = false;
            string strSql = string.Empty;
            strSql = AddLagerEingangSQL();
            strSql = strSql + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            LEingangTableID = decTmp;

            if (LEingangTableID > 0)
            {
                FillEingang();
                //ArtikelVita
                clsArtikelVita.AddEinlagerungManual(_GL_User.User_ID, LEingangTableID, LEingangID);
                //Add Logbucheintrag Eintrag
                string Beschreibung = "Lager Eingang erstellt: Nr [" + LEingangID.ToString() + "] / Mandant [" + MandantenID.ToString() + "] / Arbeitsbereich [" + AbBereichID.ToString() + "]";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
                bAddOK = true;
            }
            return bAddOK;
        }
        ///<summary>clsLEingang / UpdateLagerEingang</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public bool UpdateLagerEingang()
        {
            if (ExistLEingangTableID())
            {
                clsLEingang tmpEingang = this.Copy();
                tmpEingang.FillEingang();

                string strSql = string.Empty;
                strSql = "Update LEingang SET " +
                                "Date ='" + LEingangDate + "'" +
                                ", Auftraggeber = " + (Int32)Auftraggeber +
                                ", Empfaenger =" + (Int32)Empfaenger +
                                ", Versender = " + (Int32)Versender +
                                ", Lieferant = '" + Lieferant + "'" +
                                ", AbBereich = " + (Int32)AbBereichID +
                                ", Mandant =" + (Int32)MandantenID +
                                ", LfsNr ='" + LEingangLfsNr + "'" +
                                ", ASN =" + (Int32)ASN +
                                ", SpedID =" + (Int32)SpedID +
                                ", KFZ ='" + KFZ + "' " +
                                ", DirectDelivery =" + Convert.ToInt32(DirektDelivery) +
                                ", Retoure= " + Convert.ToInt32(Retoure) +
                                ", Vorfracht= " + Convert.ToInt32(Vorfracht) +
                                ", LagerTransport= " + Convert.ToInt32(LagerTransport) +
                                ", WaggonNo='" + WaggonNr + "' " +
                                ", BeladeID =" + (Int32)BeladeID +
                                ", EntladeID =" + (Int32)EntladeID +
                                ", IsPrintDoc=" + Convert.ToInt32(IsPrintDoc) +
                                ", IsPrintAnzeige=" + Convert.ToInt32(IsPrintAnzeige) +
                                ", IsPrintLfs=" + Convert.ToInt32(IsPrintLfs) +
                                ", ExTransportRef='" + ExTransportRef + "'" +
                                ", ExAuftragRef='" + ExAuftragRef + "'" +
                                ", ASNRef='" + ASNRef + "'" +
                                ", IsWaggon=" + Convert.ToInt32(IsWaggon) +
                                ", Fahrer='" + Fahrer + "'" +
                                ", Ship ='" + this.Ship + "'" +
                                ", IsShip=" + Convert.ToInt32(IsShip) +
                                ", Verlagerung = " + Convert.ToInt32(this.Verlagerung) +
                                ", Umbuchung = " + Convert.ToInt32(this.Umbuchung) +

                                " WHERE ID=" + (Int32)LEingangTableID;
                bool bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);

                //ArtikelVita
                GetLEingangTableID();
                if (bReturn)
                {
                    //ArtikelVita
                    string strChangeInfo = this.CheckEingangChangingValue(ref tmpEingang);
                    if (!strChangeInfo.Equals(string.Empty))
                    {
                        clsArtikelVita.LagerEingangChange(this._GL_User.User_ID, this.LEingangTableID, this.LEingangID, strChangeInfo);
                    }
                    //ArtikelVita
                    //clsArtikelVita.AddArtikelManualLEingang(_GL_User, LEingangTableID, LEingangID);

                    // Logbucheintrag Eintrag
                    string Beschreibung = "Lager Eingang geändert: Nr [" + LEingangID.ToString() + "] / Mandant [" + MandantenID.ToString() + "] / Arbeitsbereich [" + AbBereichID.ToString() + "]";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
                }
                return bReturn;
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsArtikel / CheckArtikelChangingValue</summary>
        ///<remarks></remarks>
        private string CheckEingangChangingValue(ref clsLEingang clsEingangToCompare)
        {
            LEingangChangingText = string.Empty;
            Type typeSource = this.GetType();
            PropertyInfo[] pInfoSource = typeSource.GetProperties();
            Type typeCompare = clsEingangToCompare.GetType();
            PropertyInfo[] pInfoCompare = typeCompare.GetProperties();

            foreach (PropertyInfo info in pInfoSource)
            {
                //Test
                if (info.Name.ToString().Equals(EingangField_DirektDelivery))
                {
                    string str = "stop";
                }

                if (info.Name.ToString().Equals("DirektDelivery"))
                {
                    string str = "stop";
                }
                if ((info.CanRead) & (info.CanWrite))
                {
                    object NewValue;
                    object oldValue;
                    string PropName = info.Name.ToString();


                    switch ("LEingang." + PropName)
                    {
                        case EingangField_Date:
                            NewValue = info.GetValue(this, null);
                            oldValue = typeCompare.GetProperty(PropName).GetValue(clsEingangToCompare, null);
                            DateTime dtNew = clsSystem.const_DefaultDateTimeValue_Min;
                            DateTime.TryParse(NewValue.ToString(), out dtNew);
                            DateTime dtOld = clsSystem.const_DefaultDateTimeValue_Min;
                            DateTime.TryParse(oldValue.ToString(), out dtOld);
                            if (dtNew != dtOld)
                            {
                                LEingangChangingText += PropName + ":  [" + dtOld.Date.ToShortDateString() + "] >>> [" + dtNew.Date.ToShortDateString() + "]" + Environment.NewLine;
                            }
                            break;

                        case EingangField_Auftraggeber:
                        case EingangField_Empfaenger:
                        case EingangField_Versender:
                        case EingangField_SpedID:
                        case EingangField_BeladeID:
                        case EingangField_EntladeID:
                            Int32 iNewValue = 0;
                            Int32 ioldValue = 0;
                            NewValue = info.GetValue(this, null);
                            oldValue = typeCompare.GetProperty(PropName).GetValue(clsEingangToCompare, null);
                            Int32.TryParse(NewValue.ToString(), out iNewValue);
                            Int32.TryParse(oldValue.ToString(), out ioldValue);
                            if (iNewValue != ioldValue)
                            {
                                LEingangChangingText += PropName + ":  [" + ioldValue.ToString() + "] >>> [" + iNewValue.ToString() + "]" + Environment.NewLine;
                            }
                            break;

                        case EingangField_Lieferant:
                        case EingangField_LfsNr:
                        case EingangField_KFZ:
                        case EingangField_WaggonNo:
                        case EingangField_ExTransportRef:
                        case EingangField_ExAuftragRef:
                        case EingangField_Fahrer:
                        case EingangField_Ship:

                            NewValue = string.Empty;
                            oldValue = string.Empty;
                            NewValue = info.GetValue(this, null);
                            oldValue = typeCompare.GetProperty(PropName).GetValue(clsEingangToCompare, null);
                            if (!NewValue.ToString().Equals(oldValue.ToString()))
                            {
                                LEingangChangingText += PropName + ":  [" + oldValue.ToString() + "] >>> [" + NewValue.ToString() + "]" + Environment.NewLine;
                            }
                            break;

                        case EingangField_Retoure:
                        case EingangField_Verlagerung:
                        case EingangField_Umbuchung:
                        case EingangField_LagerTransport:
                        case EingangField_DirektDelivery:
                        case EingangField_IsShip:
                            NewValue = string.Empty;
                            oldValue = string.Empty;
                            NewValue = info.GetValue(this, null);
                            oldValue = typeCompare.GetProperty(PropName).GetValue(clsEingangToCompare, null);
                            if (!NewValue.ToString().Equals(oldValue.ToString()))
                            {
                                LEingangChangingText += PropName + ":  [" + oldValue.ToString() + "] >>> [" + NewValue.ToString() + "]" + Environment.NewLine;
                            }
                            break;
                    }
                }

            }
            if (!LEingangChangingText.Equals(string.Empty))
            {
                LEingangChangingText = "Folgende Ängerungen wurden vorgenommen: " + Environment.NewLine + LEingangChangingText;
            }
            return LEingangChangingText;
        }
        ///<summary>clsLEingang / GetLEingangTableID</summary>
        ///<remarks>Ermittelt anhand der Lagereingangsnummer und der Mandanten ID die Table ID.</remarks>
        public void GetLEingangTableID()
        {
            if (LEingangID > 0)
            {
                string strSql = string.Empty;
                strSql = "SELECT ID FROM LEingang WHERE LEingangID='" + LEingangID + "' AND Mandant='" + MandantenID + "' AND AbBereich=" + AbBereichID + " ";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                LEingangTableID = decTmp;
            }
        }
        ///<summary>clsLEingang / GetLEingangTableID</summary>
        ///<remarks>Ermittelt anhand der Lagereingangsnummer und der Mandanten ID die Table ID.</remarks>
        public clsArtikel GetTopArtikelIDByLEingang()
        {
            clsArtikel tmpArt = new clsArtikel();
            tmpArt._GL_User = this._GL_User;
            tmpArt.sys = this.sys;

            if (LEingangID > 0)
            {
                string strSql = string.Empty;
                strSql = "SELECT Top(1) a.ID FROM LEingang b " +
                                    "INNER JOIN Artikel a ON a.LEingangTableID=b.ID " +
                                    "WHERE " +
                                         "b.ID=" + this.LEingangTableID + " ";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                tmpArt.ID = decTmp;
                if (decTmp > 0)
                {
                    tmpArt.GetArtikeldatenByTableID();
                }
            }
            return tmpArt.Copy();
        }
        ///<summary>clsLEingang / ExistLEingangTableID</summary>
        ///<remarks>Prüft ob der Datensatz mit der ID existiert.</remarks>
        ///<returns>Returns BOOL</returns>
        public bool ExistLEingangTableID()
        {
            if (LEingangTableID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select * FROM LEingang WHERE ID='" + LEingangTableID + "'";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsLEingang / CreateEingangByRetoure</summary>
        ///<remarks></remarks>
        public void CreateEingangByRetoure(ref clsLager myLager, DataTable dtArtikel)
        {
            if (myLager is clsLager)
            {
                //neuen LEingang erstellen
                clsLEingang RetEingang = new clsLEingang();
                RetEingang.InitDefaultClsEingang(this._GL_User, this.sys);
                RetEingang.LEingangID = clsLEingang.GetNewLEingangID(this._GL_User, this.sys);
                RetEingang.LEingangDate = DateTime.Now;
                RetEingang.Auftraggeber = myLager.Artikel.Ausgang.Empfaenger;
                RetEingang.Empfaenger = RetEingang.Auftraggeber;
                RetEingang.Lieferant = clsADRVerweis.GetLieferantenVerweisBySenderAndReceiverAdr(RetEingang.Auftraggeber, RetEingang.Empfaenger, this.BenutzerID, constValue_AsnArt.const_Art_VDA4913, this.sys.AbBereich.ID);
                RetEingang.AbBereichID = this.sys.AbBereich.ID;
                RetEingang.MandantenID = this.sys.Mandant.ID;
                RetEingang.LEingangLfsNr = "A" + myLager.Artikel.Ausgang.LAusgangID.ToString();
                RetEingang.ASN = 0;
                RetEingang.Checked = false;
                RetEingang.Versender = RetEingang.Auftraggeber;
                RetEingang.SpedID = 0;
                RetEingang.KFZ = string.Empty;
                RetEingang.DirektDelivery = false;
                RetEingang.Retoure = true;
                RetEingang.Verlagerung = false;
                RetEingang.Vorfracht = false;
                RetEingang.Umbuchung = false;
                RetEingang.LagerTransport = false;
                RetEingang.WaggonNr = string.Empty;
                RetEingang.BeladeID = 0;
                RetEingang.EntladeID = 0;
                RetEingang.Fahrer = string.Empty;
                RetEingang.Ship = string.Empty;
                RetEingang.IsShip = false;


                RetEingang.AddLagerEingang();

                //Artikel hinzufügen
                Int32 iPos = 1;
                foreach (DataRow row in dtArtikel.Rows)
                {
                    if ((bool)row["Selected"])
                    {
                        decimal decTmpArtID = 0;
                        Decimal.TryParse(row["ArtikelID"].ToString(), out decTmpArtID);
                        if (decTmpArtID > 0)
                        {
                            clsArtikel tmpArt = new clsArtikel();
                            tmpArt.InitClass(this._GL_User, myLager._GL_System);
                            tmpArt.sys = myLager.sys;
                            tmpArt.ID = decTmpArtID;
                            tmpArt.GetArtikeldatenByTableID();

                            //clsArtikel RetourArt = tmpArt.Copy();

                            //Werte zurück bzw. auf 0 setzen
                            tmpArt.ID = 0;
                            tmpArt.Netto = 0;
                            tmpArt.Brutto = 0;
                            tmpArt.gemGewicht = 0;
                            tmpArt.SetDefaultValueToDefaultProperties(true);
                            tmpArt.Position = iPos.ToString();
                            tmpArt.EingangChecked = false;
                            tmpArt.AusgangChecked = false;
                            tmpArt.AbBereichID = RetEingang.AbBereichID;
                            tmpArt.MandantenID = RetEingang.MandantenID;
                            tmpArt.LEingangTableID = RetEingang.LEingangTableID;
                            tmpArt.LAusgangTableID = 0;
                            tmpArt.ArtIDAlt = decTmpArtID;
                            tmpArt.Info = string.Empty;
                            tmpArt.interneInfo = "Retoure aus A" + myLager.Artikel.Ausgang.LAusgangID + " / LVSNr " + tmpArt.LVS_ID.ToString();
                            tmpArt.externeInfo = string.Empty;
                            tmpArt.ASNProduktionsnummer = string.Empty;
                            tmpArt.AddArtikelLager(false);

                            clsSchaeden sch = new clsSchaeden();
                            sch.ArtikelID = decTmpArtID;
                            sch._GL_User = this._GL_User;
                            sch.AddSchadenForRetour(tmpArt);

                            iPos++;
                        }
                    }
                }
                this.LEingangTableID = RetEingang.LEingangTableID;
                this.FillEingang();
            }
        }
        ///<summary>clsLEingang / DeleteLEingangByLEingangTableID</summary>
        ///<remarks>Löscht den Eingang anhand der Table ID. Hierfür werden unbeding folgende Angaben benötigt:
        ///         - MandantenID
        ///         - Arbeitsbereich
        ///         - LEingangsTableID</remarks>
        public void DeleteLEingangByLEingangTableID()
        {
            if ((LEingangTableID > 0) &&
                (MandantenID > 0) &&
                (AbBereichID > 0))
            {
                //Get Lagereingang Daten
                FillEingang();

                string strSql = string.Empty;
                strSql = "Delete FROM LEingang WHERE ID='" + LEingangTableID + "'; ";

                //ArtikelVita 
                clsArtikelVita artV = new clsArtikelVita();
                artV._GL_User = this._GL_User;

                strSql = strSql + artV.GetSQLDeleteLagerEingang(LEingangTableID);

                //Direkt werden nun auch die entsprechenden Artikeldatensätze gelöscht
                clsArtikel art = new clsArtikel();
                art._GL_User = this._GL_User;
                art.MandantenID = this.MandantenID;
                art.AbBereichID = this.AbBereichID;
                art.LEingangTableID = this.LEingangTableID;

                strSql = strSql + art.GetSQLDeleteArtikelLager();

                if (clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "LagerEingangDelete", this._GL_User.User_ID))
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Lager - Eingang gelöscht: NR [" + this.LEingangID + "] / Mandant [" + MandantenID + "] / Arbeitsbereich  [" + AbBereichID + "] ";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);

                    //manuelle Adressen für diesen Ausgang löschen
                    this.AdrManuell.TableName = "LAusgang";
                    this.AdrManuell.TableID = this.LEingangTableID;
                    this.AdrManuell.DeleteAllByTableID();
                }
            }
        }
        ///<summary>clsLEingang / GetLEingangsdatenByLEingangTableID</summary>
        ///<remarks>Ermittel die Daten des Lagereingangs anhand der TableID.</remarks>
        public static DataTable GetLEingangTableColumnSchema(Globals._GL_USER myGLUser)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select * FROM LEingang WHERE ID=0";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Lagereingang");
            return dt;
        }
        ///<summary>clsLEingang / GetLEingangsdatenByLEingangTableID</summary>
        ///<remarks>Ermittel die Daten des Lagereingangs anhand der TableID.</remarks>
        public bool FillEingang()
        {
            if (ExistLEingangTableID())
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM LEingang WHERE ID='" + LEingangTableID + "' ";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Lagereingang");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        decimal decTmp = 0;
                        this.LEingangTableID = (decimal)dt.Rows[i]["ID"];
                        this.LEingangID = (decimal)dt.Rows[i]["LEingangID"];

                        //DateTime dtTmp = LVS.Globals.DefaultDateTimeMinValue;
                        DateTime dtTmp = new DateTime(1900, 1, 1);
                        DateTime.TryParse(dt.Rows[i]["Date"].ToString(), out dtTmp);
                        this.LEingangDate = dtTmp;
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["Auftraggeber"].ToString(), out decTmp);
                        this.Auftraggeber = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["Empfaenger"].ToString(), out decTmp);
                        this.Empfaenger = decTmp;
                        this.Lieferant = dt.Rows[i]["Lieferant"].ToString();
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["AbBereich"].ToString(), out decTmp);
                        this.AbBereichID = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["Mandant"].ToString(), out decTmp);
                        this.MandantenID = decTmp;
                        this.LEingangLfsNr = dt.Rows[i]["LfsNr"].ToString();
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["ASN"].ToString(), out decTmp);
                        this.ASN = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["Versender"].ToString(), out decTmp);
                        this.Versender = decTmp;
                        this.Checked = (bool)dt.Rows[i]["Check"];
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["SpedID"].ToString(), out decTmp);
                        this.SpedID = decTmp;
                        this.KFZ = dt.Rows[i]["KFZ"].ToString();
                        this.DirektDelivery = (bool)dt.Rows[i]["DirectDelivery"];
                        this.Retoure = (bool)dt.Rows[i]["Retoure"];
                        this.Vorfracht = (bool)dt.Rows[i]["Vorfracht"];
                        this.LagerTransport = (bool)dt.Rows[i]["LagerTransport"];
                        this.WaggonNr = dt.Rows[i]["WaggonNo"].ToString();
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["BeladeID"].ToString(), out decTmp);
                        this.BeladeID = decTmp;
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["EntladeID"].ToString(), out decTmp);
                        this.EntladeID = decTmp;
                        this.IsPrintDoc = (bool)dt.Rows[i]["IsPrintDoc"];
                        this.IsPrintAnzeige = (bool)dt.Rows[i]["IsPrintAnzeige"];
                        this.IsPrintLfs = (bool)dt.Rows[i]["IsPrintLfs"];
                        this.ExTransportRef = dt.Rows[i]["ExTransportRef"].ToString();
                        this.ExAuftragRef = dt.Rows[i]["ExAuftragRef"].ToString();
                        decTmp = 0;
                        Decimal.TryParse(dt.Rows[i]["lockedBy"].ToString(), out decTmp);
                        this.LockedBy = decTmp;
                        // this.ASNRef = dt.Rows[i]["ASNRef"].ToString();
                        this.IsWaggon = (bool)dt.Rows[i]["IsWaggon"];
                        this.Fahrer = dt.Rows[i]["Fahrer"].ToString();
                        this.IsPrintList = (bool)dt.Rows[i]["IsPrintList"];
                        this.Ship = dt.Rows[i]["Ship"].ToString();
                        this.IsShip = (bool)dt.Rows[i]["IsShip"];
                        this.Verlagerung = (bool)dt.Rows[i]["Verlagerung"];
                        this.Umbuchung = (bool)dt.Rows[i]["Umbuchung"];

                        this.Stat = ClsStatus.loaded;
                    }
                    bAllArtikelArePlacedInStore = clsArtikel.CheckAllArtikelArePlacedInStore(BenutzerID, this.LEingangTableID, true);
                    dtArtInLEingang.Clear();
                    dtArtInLEingang = clsArtikel.GetArtikelInEingang(this._GL_User, this.LEingangTableID);
                    Artikelanzahl = dtArtInLEingang.Rows.Count;

                    //manuelle Adressen
                    AdrManuell = new clsADRMan();
                    AdrManuell.InitClass(this._GL_User, this.LEingangTableID, "LEingang");

                    if (this.Auftraggeber > 0)
                    {
                        AdrAuftraggeber = new clsADR();
                        AdrAuftraggeber.sys = this.sys;
                        AdrAuftraggeber.InitClass(this._GL_User, new Globals._GL_SYSTEM(), this.Auftraggeber, true);
                    }

                    if (this.Empfaenger > 0)
                    {
                        AdrEmpfaenger = new clsADR();
                        AdrEmpfaenger.sys = this.sys;
                        AdrEmpfaenger.InitClass(this._GL_User, new Globals._GL_SYSTEM(), this.Empfaenger, true);
                    }

                    if (this.Versender > 0)
                    {
                        AdrVersender = new clsADR();
                        AdrVersender.sys = this.sys;
                        AdrVersender.InitClass(this._GL_User, new Globals._GL_SYSTEM(), this.Versender, true);
                    }

                    ListArtWithSchaden = new List<decimal>();
                    ListArtWithSchaden = clsSchaeden.GetArtikelWithSchaden(this._GL_User, this.LEingangTableID);

                    ListArtInSPL = new List<decimal>();
                    ListArtInSPL = clsSPL.GetArtikelEingangInSPL(this._GL_User, this.LEingangTableID);

                    FirstCheckDateTime = clsArtikelVita.GetFirstDateTimeLEingangChecked(this._GL_User, this.LEingangTableID);
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
        ///<summary>clsLEingang / GetPrviousLEingangsID</summary>
        ///<remarks>Ermittel die vorhergehende LEingangID für den Mandanten und Arbeitsbereich.</remarks>
        public void GetNextLEingangsID(bool SearchDirection)
        {
            if (LEingangID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select Top(1) LEingangID FROM LEingang WHERE Mandant='" + MandantenID + "' AND AbBereich='" + AbBereichID + "' ";

                if (SearchDirection)
                {
                    //forward
                    strSql = strSql + "AND LEingangID>'" + LEingangID + "' ";
                }
                else
                {
                    //back
                    strSql = strSql + "AND LEingangID<'" + LEingangID + "' ORDER BY LEingangID DESC ";
                }

                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                if (strTmp != string.Empty)
                {
                    decimal decTmp = 0;
                    if (Decimal.TryParse(strTmp, out decTmp))
                    {
                        LEingangID = decTmp;
                    }
                }
                GetLEingangTableID();
            }
            else
            {
                string strSql = string.Empty;
                if (SearchDirection)
                {
                    //forward
                    strSql = "Select Top(1) LEingangID FROM LEingang WHERE Mandant='" + MandantenID + "' AND AbBereich='" + AbBereichID + "' ORDER BY LEingangID ";
                }
                else
                {
                    //back
                    strSql = "Select Top(1) LEingangID FROM LEingang WHERE Mandant='" + MandantenID + "' AND AbBereich='" + AbBereichID + "' ORDER BY LEingangID DESC";
                }

                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                if (strTmp != string.Empty)
                {
                    decimal decTmp = 0;
                    if (Decimal.TryParse(strTmp, out decTmp))
                    {
                        LEingangID = decTmp;
                    }
                }
                GetLEingangTableID();
            }
        }
        /// <summary>
        /// clsLEingang / lockEingang        /// </summary>
        public void lockEingang()
        {
            if (LEingangTableID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update LEingang set lockedBy = 0 where lockedby = " + this._GL_User.User_ID + ";" +
                         "Update LEingang set lockedBy = " + this._GL_User.User_ID + " where ID='" + LEingangTableID + "'" +
                         " AND lockedBy=0;";
                clsSQLcon.ExecuteSQL(strSql, this._GL_User.User_ID);
            }
        }
        /// <summary>
        /// clsLEingang / lockEingang        /// </summary>
        public static void unlockEingang(decimal userID = 0)
        {
            string strSql = string.Empty;

            if (userID > 0)
                strSql = "Update LEingang set lockedBy = 0 where lockedby = " + userID + ";";
            else
                strSql = "Update LEingang set lockedBy = 0;";
            clsSQLcon.ExecuteSQL(strSql, userID);
        }
        ///<summary>clsLEingang / GetPrviousLEingangsID</summary>
        ///<remarks>Ermittel die vorhergehende LEingangID für den Mandanten und Arbeitsbereich.</remarks>
        public void GetNextLAusgangID(bool SearchDirection)
        {
            if (LEingangID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select Top(1) LAusgangID FROM LAusgang WHERE Mandant='" + MandantenID + "' AND AbBereich='" + AbBereichID + "' ";

                if (SearchDirection)
                {
                    //forward
                    strSql = strSql + "AND LEingangID>'" + LEingangID + "' ";
                }
                else
                {
                    //back
                    strSql = strSql + "AND LEingangID<'" + LEingangID + "' ORDER BY LEingangID DESC ";
                }

                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                if (strTmp != string.Empty)
                {
                    decimal decTmp = 0;
                    if (Decimal.TryParse(strTmp, out decTmp))
                    {
                        LEingangID = decTmp;
                    }
                }
                GetLEingangTableID();
            }
            else
            {
                string strSql = string.Empty;
                if (SearchDirection)
                {
                    //forward
                    strSql = "Select Top(1) LAusgangID FROM LAusgang WHERE Mandant='" + MandantenID + "' AND AbBereich='" + AbBereichID + "' ORDER BY LEingangID ";
                }
                else
                {
                    //back
                    strSql = "Select Top(1) LAusgangID FROM LAusgang WHERE Mandant='" + MandantenID + "' AND AbBereich='" + AbBereichID + "' ORDER BY LEingangID DESC ";
                }

                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                if (strTmp != string.Empty)
                {
                    decimal decTmp = 0;
                    if (Decimal.TryParse(strTmp, out decTmp))
                    {
                        LEingangID = decTmp;
                    }
                }
                GetLEingangTableID();
            }
        }
        ///<summary>clsLEingang / GetNextLVSNr</summary>
        ///<remarks>Die nächste frei LvsNr wird ermittelt.</remarks>
        public static bool GetLEingangCheck(Globals._GL_USER myGL_User, decimal myLEingangTableID)
        {
            bool tmp = false;
            Decimal decTmpID = 0;
            if (myLEingangTableID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select [Check] FROM LEingang WHERE ID='" + myLEingangTableID + "' ";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGL_User.User_ID);
                if (strTmp.ToUpper() == "FALSE")
                {
                    tmp = false;
                }
                if (strTmp.ToUpper() == "TRUE")
                {
                    tmp = true; ;
                }
            }
            return tmp;
        }
        ///<summary>clsLEingang / UpdateArtikelLager</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public static void UpdateLEingangCheck(decimal myDecBenutzer, bool myArtCheck, decimal myDecTableID)
        {
            if (clsLEingang.ExistLEingangTableID(myDecBenutzer, myDecTableID))
            {
                string strSql = string.Empty;
                if (myArtCheck)
                {
                    strSql = "Update LEingang SET [Check]='1' WHERE ID=" + (Int32)myDecTableID + "; " +
                             "Update Artikel SET IsKorStVerUse=0 WHERE LEingangTableID=" + (Int32)myDecTableID + "; ";
                }
                else
                {
                    strSql = "Update LEingang SET [Check]='0' WHERE ID=" + (Int32)myDecTableID + "; ";
                }
                bool bExecOK = clsSQLcon.ExecuteSQL(strSql, myDecBenutzer);

                if (bExecOK)
                {
                    decimal tmpLEingangID = clsLager.GetLEingangIDByLEingangTableID(myDecBenutzer, myDecTableID);
                    if (myArtCheck)
                    {
                        //Eingang abgeschlossen
                        clsArtikelVita.LagerEingangChecked(myDecBenutzer, myDecTableID, tmpLEingangID);
                    }
                    else
                    {
                        //Reset Eingang
                        clsArtikelVita.LagerEingangCheckedReset(myDecBenutzer, myDecTableID, tmpLEingangID);
                    }
                }
            }
        }

        ///<summary>clsLEingang / UpdatePrintLEingang</summary>
        ///<remarks></remarks>
        public void UpdatePrintLEingang(string myDocArt, decimal AdrID = -1, DateTime? date = null)
        {
            string strSql = string.Empty;
            switch (myDocArt)
            {
                case "LagerEingangDoc":
                    //if(IsPrintDoc == false)
                    clsArtikelVita.LagerEingangPrintDoc(BenutzerID, this.LEingangTableID, LEingangID);
                    strSql = "Update LEingang SET IsPrintDoc=1 WHERE ID=" + (Int32)this.LEingangTableID + "; ";
                    break;

                case "Eingangsliste":
                    //if(IsPrintDoc == false)
                    clsArtikelVita.LagerEingangPrintDoc(BenutzerID, this.LEingangTableID, LEingangID);
                    strSql = "Update LEingang SET IsPrintList=1 WHERE ID=" + (Int32)this.LEingangTableID + "; ";
                    break;

                case "LagerEingangAnzeige":
                    //if (IsPrintAnzeige == false)
                    clsArtikelVita.LagerEingangPrintAnzeige(BenutzerID, this.LEingangTableID, LEingangID);
                    strSql = "Update LEingang SET IsPrintAnzeige=1 WHERE ID=" + (Int32)this.LEingangTableID + "; ";
                    break;
                case "LagerEingangAnzeigePerDay":
                    //if (IsPrintAnzeige == false)
                    clsArtikelVita.LagerEingangPrintPerDay(BenutzerID, this.LEingangTableID, LEingangID, AdrID, date);
                    strSql = "Update LEingang SET IsPrintAnzeige=1 WHERE Auftraggeber=" + AdrID + " AND Cast([Date] as Date)=Cast('" + date + "' as Date) and [Check]=1; ";
                    break;
                case "LagerEingangLfs":
                    //if (IsPrintLfs == false)
                    clsArtikelVita.LagerEingangPrintLfs(BenutzerID, this.LEingangTableID, LEingangID);
                    strSql = "Update LEingang SET IsPrintLfs=1 WHERE ID=" + (Int32)this.LEingangTableID + "; ";
                    break;
            }
            if (strSql != string.Empty)
            {
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
        }
        ///<summary>clsLEingang / ExistLEingangTableID</summary>
        ///<remarks>Prüft, ob die angegebene LagereingangstableID vorhanden ist.</remarks>
        public static bool ExistLEingangTableID(decimal decBenuzter, decimal myTableID)
        {
            bool bReval = false;
            if (myTableID > 0)
            {
                string strSQL = "SELECT ID FROM LEingang WHERE ID='" + myTableID + "';";
                bReval = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, decBenuzter);
            }
            return bReval;
        }
        ///<summary>clsLEingang / GetNextLVSNr</summary>
        ///<remarks>Die nächste frei LvsNr wird ermittelt.</remarks>
        public static decimal GetNextLVSNr(Globals._GL_USER myGL_User, clsSystem mySystem)
        {
            Decimal decTmpID = 0;
            if (mySystem != null)
            {
                clsPrimeKeys Lager = new clsPrimeKeys();
                Lager.sys = mySystem;
                Lager.AbBereichID = mySystem.AbBereich.ID;
                Lager._GL_User = myGL_User;
                Lager.Mandanten_ID = mySystem.AbBereich.MandantenID;
                Lager.GetNEWLvsNr();
                decTmpID = Lager.LvsNr;
            }
            return decTmpID;
        }
        ///<summary>clsLEingang / GetNewLEingangID</summary>
        ///<remarks>Die nächste frei LEingangID wird ermittelt.</remarks>
        public static decimal GetNewLEingangID(Globals._GL_USER myGL_User, clsSystem mySystem)
        {
            Decimal decTmpID = 0;
            if (mySystem != null)
            {
                clsPrimeKeys Lager = new clsPrimeKeys();
                Lager.sys = mySystem;
                Lager.AbBereichID = mySystem.AbBereich.ID;
                Lager._GL_User = myGL_User;
                Lager.Mandanten_ID = mySystem.AbBereich.MandantenID;
                Lager.GetNEWLEingnagID();
                decTmpID = Lager.LEingangID;
            }
            return decTmpID;
        }
        ///<summary>clsLEingang / GetLEingangIDByLEingangTableID</summary>
        ///<remarks>.</remarks>
        public static decimal GetLEingangIDByLEingangTableID(decimal myBenutzerID, decimal myLEingangTableID)
        {
            decimal myLEingangID = 0;
            if (myLEingangTableID > 0)
            {
                string strSql = string.Empty;
                strSql = "SELECT LEingangID FROM LEingang WHERE ID='" + myLEingangTableID + "'";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myBenutzerID);
                if (strTmp != string.Empty)
                {
                    myLEingangID = Convert.ToDecimal(strTmp);
                }
            }
            return myLEingangID;
        }
        ///<summary>clsLEingang / GetLEingangIDByLEingangTableID</summary>
        ///<remarks>.</remarks>
        public static decimal GetLEingangTableIDByLEingangID(decimal myBenutzerID, decimal myLEingangID, clsSystem mySystem)
        {
            decimal myID = 0;
            if (myLEingangID > 0)
            {
                string strSql = string.Empty;
                strSql = "SELECT ID FROM LEingang WHERE LEingangID=" + myLEingangID +
                                                        " AND Mandant=" + mySystem.AbBereich.MandantenID +
                                                        " AND AbBereich=" + mySystem.AbBereich.ID +
                                                        ";";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myBenutzerID);
                if (strTmp != string.Empty)
                {
                    myID = Convert.ToDecimal(strTmp);
                }
            }
            return myID;
        }
        ///<summary>clsLEingang / GetEingangAuftraggeberForEingangAnzeigeAtDate</summary>
        ///<remarks>Die Funktion ermittelt die Auftraggeber der Eingänge für das gewählte 
        ///         Datum, die noch keine Lagereingangsanzeige erhalten haben</remarks>
        //public static List<decimal> GetEingangAuftraggeberForEingangAnzeigeAtDate(decimal myUserID, DateTime myDate) // CF
        public static DataTable GetEingangAuftraggeberForEingangAnzeigeAtDate(decimal myUserID, DateTime myDate, decimal Auftraggeber = 0)
        {
            List<decimal> retList = new List<decimal>();
            string strSql = string.Empty;
            if (Auftraggeber == 0)
            {
                strSql = "SELECT " +
                                 "DISTINCT CAST([Date] as Date) as Datum " +
                                 ", Auftraggeber " +
                                 ", ADR.ViewID as ViewID" +
                                 ", 'LagerEingangAnzeigePerDay' as DokumentArt " +
                                     "FROM LEingang " +
                                     "Left join ADR on Auftraggeber=ADR.ID " +
                                         "WHERE  " +
                                             "(DATEDIFF(dd, Date, CAST('" + myDate + "' as Date))>=0) " +
                                             "AND IsPrintAnzeige=0 AND [Check]=1 " +
                                             "ORDER BY ViewID "; // sortierung nach VIewID der Auftraggeber nicht ID
            }
            else
            {
                strSql = "SELECT " +
                                 "DISTINCT CAST([Date] as Date) as Datum " +
                                 ", Auftraggeber " +
                                 ", ADR.ViewID as ViewID" +
                                 ", 'LagerEingangAnzeigePerDay' as DokumentArt " +
                                     "FROM LEingang " +
                                     "Left join ADR on Auftraggeber=ADR.ID " +
                                         "WHERE  " +
                                             "(DATEDIFF(dd, Date, CAST('" + myDate + "' as Date))=0) " +
                                             "AND [Check]=1 AND Auftraggeber=" + Auftraggeber;


            }
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myUserID, "Auftraggeber");
            return dt;
        }
        ///<summary>clsLEingang / GetLagerDaten</summary>
        ///<remarks>Ermittel anhand der LagerausgangstableID die entsprechenden Artikel.</remarks>
        public DataTable GetLagerArtikelDatenByAuftraggeber(Int32 iSelMenge, bool bDisplaySPLArtikel = false)
        {
            DataTable dt = new DataTable("Artikel");
            string strSQL = string.Empty;
            dt.Clear();
            strSQL = "Select " +
                             //"CAST(0 as bit) as Ausgang " +
                             "CAST(0 as bit) as Selected " +
                             ",LA_Checked as [Check]" + //für die Auslagerung
                              ", a.ID as ArtikelID" +
                              ", a.LVS_ID as LVSNr" +
                              ", b.LEingangID as Eingang " +
                              ", a.Produktionsnummer" +
                              ", e.Bezeichnung as Gut" +
                              ", a.Anzahl" +
                              ", b.Date as Eingangsdatum" +
                              //",CAST(DATEPART(DD,b.Date) as varchar)+ '.' + CAST(DATEPART(MM,b.Date) as varchar) + '.' + CAST(DATEPART(YYYY,b.Date) as varchar) as Eingangsdatum" +
                              //Abmessungen sollen einzeln aufgeführt werden
                              //", CAST( a.Dicke as varchar(30))+'x'+CAST(a.Breite as varchar(30))+'x'+CAST(a.Laenge as varchar(30))+'x'+CAST(a.Hoehe as varchar(30)) as 'Abmessung'" +
                              ", a.Dicke" +
                              ", a.Breite" +
                              ", a.Laenge" +
                              ", a.Hoehe" +
                              ", a.Netto" +
                              ", a.Brutto" +
                              ", a.Charge " +
                              ", a.Bestellnummer" +
                              ", a.FreigabeAbruf as Freigabe" +
                              ", CAST (0 as bit) as Selected  " +
                              ", a.UB_AltCalcEinlagerung" +
                              ", a.UB_AltCalcAuslagerung" +
                              ", a.UB_AltCalcLagergeld" +
                              ", a.UB_NeuCalcEinlagerung" +
                              ", a.UB_NeuCalcAuslagerung" +
                              ", a.UB_NeuCalcLagergeld" +
                              ", a.Werksnummer" +
                              //
                              ", c.KD_ID as Auftraggeber " +
                              ", a.Halle " +
                              ", a.Reihe " +
                              ", b.WaggonNo " +
                              ", b.LfsNr as Lieferschein " +
                              ", a.exInfo as Bemerkung " +
                              ", a.exMaterialnummer " +
                              ", a.ArtIDRef " +
                              ", d.IsWaggon " +
                              ", CAST(DATEPART(YYYY, a.LZZ) as varchar)+CAST(DATEPART(ISOWK, a.LZZ)as varchar) as LZZ" +
                              ", " + clsArtikel.GetStatusColumnSQL("d", "b");
            //Status, Auftraggeber, Halle, Feld, WaggonNo, Lieferschein

            if (bDisplaySPLArtikel)
            {
                strSQL += ",(SELECT ID FROM Sperrlager " +
                                     "WHERE BKZ = 'IN' AND ID NOT IN " +
                                     "(SELECT DISTINCT SPLIDIn FROM Sperrlager WHERE SPLIDIn>0) AND ArtikelID=a.ID ) as spl ";
            }


            strSQL += " FROM Artikel a " +
                                        "INNER JOIN LEingang b ON a.LEingangTableID=b.ID " +
                                        "LEFT JOIN LAUSGANG d ON a.LAusgangTableID = d.ID " +
                                        "INNER JOIN Gueterart e ON e.ID = a.GArtID " +
                                        "INNER JOIN ADR c ON c.ID = b.Auftraggeber " +
                                        "WHERE b.Auftraggeber=" + Auftraggeber + " " +
                                                "AND a.Mandanten_ID=" + MandantenID + " " +
                                                "AND AB_ID=" + AbBereichID + " " +
                                                "AND a.BKZ=1 " +
                                                "AND b.[Check]=1 " +
                                                "AND a.LAusgangTableID=0 ";

            // Modul anzeige gesperrter Artikel 
            if (!bDisplaySPLArtikel)
            {
                strSQL += " AND a.ID not in( " +
                                  "select ArtikelID from Sperrlager " +
                                  "where ID not in(Select SPLIDIn from Sperrlager) " +
                                                 "and SPLIDIn=0 )";
            }


            switch (iSelMenge)
            {
                //nur Freigaben=true
                case 1:
                    strSQL = strSQL + " AND a.FreigabeAbruf=1";
                    break;
                //nur Freigaben = false
                case 2:
                    strSQL = strSQL + " AND a.FreigabeAbruf=0";
                    break;
            }
            strSQL += " ORDER BY exMaterialnummer,LZZ ,LVS_ID ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, dt.TableName.ToString());
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DirectDeliveryTransformation()
        {
            bool bReturn = false;
            if (this.ArtikelCountInStore > 0)
            {
                this.DirektDelivery = true;
                bReturn = this.UpdateLagerEingang();
            }
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySystem"></param>
        /// <returns></returns>
        public static int GetLastEingangIdArbeitsbereich(clsSystem mySystem)
        {
            string strSql = string.Empty;
            strSql = "Select Max(ID) FROM LEingang WHERE AbBereich=" + mySystem.AbBereich.ID + " ;";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, mySystem._GL_User.User_ID);
            int iRet = 0;
            int.TryParse(strTmp, out iRet);
            return iRet;
        }
        public static int GetFirstEingangIdArbeitsbereich(clsSystem mySystem)
        {
            string strSql = string.Empty;
            strSql = "Select MIN(ID) FROM LEingang WHERE AbBereich=" + mySystem.AbBereich.ID + " ;";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, mySystem._GL_User.User_ID);
            int iRet = 0;
            int.TryParse(strTmp, out iRet);
            return iRet;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myEingang"></param>
        /// <param name="myDicChanges"></param>
        /// <returns></returns>
        public static clsLEingang ChangeEingangPorpertiesToOldValue(clsLEingang myEingang, Dictionary<string, clsObjPropertyChanges> myDicChanges)
        {
            clsLEingang retEingang = myEingang.Copy();
            if (myDicChanges.Count > 0)
            {
                Type typeEingang = myEingang.GetType();
                PropertyInfo[] pInfoEingang = typeEingang.GetProperties();

                Type typeEingangChanged = retEingang.GetType();
                PropertyInfo[] pInfoEingangChanged = typeEingangChanged.GetProperties();

                foreach (KeyValuePair<string, clsObjPropertyChanges> itm in myDicChanges)
                {
                    string strKey = itm.Key;
                    string strProperty = strKey.Replace(clsObjPropertyChanges.TableName_LEingang + ".", "");
                    clsObjPropertyChanges tmpOPC = (clsObjPropertyChanges)itm.Value;

                    string ValueOld = string.Empty;
                    switch (strKey)
                    {
                        case clsLEingang.EingangField_Lieferant:
                        case clsLEingang.EingangField_LfsNr:
                        case clsLEingang.EingangField_KFZ:
                        case clsLEingang.EingangField_WaggonNo:
                        case clsLEingang.EingangField_ExTransportRef:
                        case clsLEingang.EingangField_ExAuftragRef:
                        case clsLEingang.EingangField_ASNRef:
                        case clsLEingang.EingangField_Fahrer:
                            typeEingang.GetProperty(strProperty).SetValue(retEingang, tmpOPC.ValueOld, null);
                            break;

                        case clsLEingang.EingangField_Date:
                            int iValueOld = 0;
                            int.TryParse(tmpOPC.ValueOld, out iValueOld);
                            typeEingang.GetProperty(strProperty).SetValue(retEingang, iValueOld, null);
                            break;

                        case clsLEingang.EingangField_EntladeID:
                        case clsLEingang.EingangField_BeladeID:
                        case clsLEingang.EingangField_SpedID:
                        case clsLEingang.EingangField_Versender:
                        case clsLEingang.EingangField_Empfaenger:
                        case clsLEingang.EingangField_Auftraggeber:
                            decimal decValueOld = 0;
                            decimal.TryParse(tmpOPC.ValueOld, out decValueOld);
                            typeEingang.GetProperty(strProperty).SetValue(retEingang, decValueOld, null);
                            break;
                    }
                }
            }
            return retEingang;
        }
    }
}
