using Common.Enumerations;
using Common.Models;
using System;
using System.Data;

namespace LVS
{
    public class clsArtikelVita
    {
        public Globals._GL_USER _GL_User;
        ///<summary>clsArtikelVita
        ///         Eigenschaften:
        ///             - ID
        ///             - ABName
        ///             - Bemerkung
        ///             - aktiv
        ///             - BenutzerID</summary>
        ///             
        //

        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get { return _BenutzerID; }
            set { _BenutzerID = value; }
        }
        //************************************


        ///<summary>clsArtikelVita / IsActionIsRealised</summary>
        ///<remarks></remarks>
        public static bool IsActionIsRealised(Globals._GL_USER myGLUser, string myTable, decimal myTableID, string myAction)
        {
            bool bReturn = false;
            string strSql = string.Empty;
            if (myTableID > 0)
            {
                strSql = "Select ID FROM ArtikelVita WHERE " +
                              "TableName='" + myTable + "' " +
                              " AND TableID=" + (Int32)myTableID + " " +
                              " AND Aktion='" + myAction + "' ;";
                bReturn = clsSQLcon.ExecuteSQL_GetValueBool(strSql, myGLUser.User_ID);
            }
            return bReturn;
        }
        ///<summary>clsArtikelVita / GetArtikelVitaByLEingangTableID</summary>
        ///<remarks>Laden der Artikel-Vita-Daten aus der Datenbank.</remarks>
        public static DataTable GetArtikelVitaByLEingangTableID(Globals._GL_USER myGLUser, decimal myLEingangTableID, decimal myArtikelTableID, decimal myLAusgangTableID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            if (myLEingangTableID > 0)
            {
                strSql = "Select *, (Select Initialen FROM [USER] WHERE ID=ArtikelVita.UserID) as [User] " +
                              "FROM ArtikelVita WHERE " +
                              "(TableName='LEingang' AND TableID =" + (Int32)myLEingangTableID + ") " +
                              "OR " +
                              "(TableName='Artikel' AND TableID =" + (Int32)myArtikelTableID + ")";

                strSql += "OR " +
                          "(TableName='LAusgang' AND TableID =(SELECT LAusgangTableID From Artikel where ID=" + (Int32)myArtikelTableID + "))" +
                          " and TableID > 0" +
                          " ORDER BY ID;";


                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "ArtikelVita");
            }
            return dt;
        }

        /****************************************************************************************
         *                                  Lagereingang
         * *************************************************************************************/

        ///<summary>clsArtikelVita / AddEinlagerungManual</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public static string GetInsertSQL(decimal myDecTableID, string myStrTableName, string myStrAktion, decimal myDecBenutzer, string myStrBeschreibung)
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung" +
                                ") " +
                                "VALUES ('" + myDecTableID + "','"
                                            + myStrTableName + "','"
                                            + myStrAktion + "','"
                                            + DateTime.Now + "','"
                                            + myDecBenutzer + "','"
                                            + myStrBeschreibung + "'); ";
            return strSql;
        }
        ///<summary>clsArtikelVita / AddEinlagerungManual</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public static void AddEinlagerungManual(decimal myBenuzter, decimal myTableID, decimal myLEingangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            string tmpAktion = enumLagerAktionen.EingangErstellt.ToString();
            string tmpBeschreibung = "Lagereingang [" + tmpLEingangID.ToString() + "] manuell erstellt";
            string tmpTableName = "LEingang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / AddEinlagerungManualByScanner</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public static void AddEinlagerungManualByScanner(decimal myBenuzter, decimal myTableID, decimal myLEingangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            string tmpAktion = enumLagerAktionen.EingangErstellt.ToString();
            string tmpBeschreibung = "Lagereingang [" + tmpLEingangID.ToString() + "] manuell per Scanner erstellt";
            string tmpTableName = "LEingang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / AddEinlagerungAuto</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public static string AddEinlagerungAuto(decimal myBenuzter, decimal myTableID, decimal myLEingangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            string tmpAktion = enumLagerAktionen.EingangErstellt.ToString();
            string tmpBeschreibung = "Lagereingang [" + tmpLEingangID.ToString() + "] autom. erstellt";
            string tmpTableName = "LEingang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
            return strSql;
        }
        ///<summary>clsArtikelVita / GetSQLDeleteLagerEingang</summary>
        ///<remarks>Löschen der Artikel eines LEingangs. Durchgeführt wird der Delete über eine Transaction in der
        ///         Klasse Lager.</remarks>
        public string GetSQLDeleteLagerEingang(decimal myLEingangTableID)
        {
            string strSql = string.Empty;
            if (myLEingangTableID > 0)
            {
                strSql = "DELETE FROM ArtikelVita WHERE " +
                              "(TableName='LEingang' AND TableID IN(Select LEingangTableID FROM Artikel WHERE LEingangTableID='" + myLEingangTableID + "')) " +
                              "OR " +
                              "(TableName='Artikel' AND TableID IN(Select ID FROM Artikel WHERE LEingangTableID='" + myLEingangTableID + "'));";
            }
            return strSql;
        }
        ///<summary>clsArtikelVita / LagerEingangChange</summary>
        ///<remarks>Lagereingangsänderung.</remarks>
        public static void LagerEingangChange(decimal myBenuzter, decimal myTableID, decimal myLEingangID, string myZusatzInfo)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            string tmpAktion = enumLagerAktionen.EingangChanged.ToString();
            string tmpBeschreibung = "Eingang geändert:  [" + tmpLEingangID.ToString() + "]";
            tmpBeschreibung = tmpBeschreibung + Environment.NewLine + myZusatzInfo;
            string tmpTableName = "LEingang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / LagerEingangChange</summary>
        ///<remarks>Lagereingangsänderung.</remarks>
        public static void LagerEingangChangeBySanner(decimal myBenuzter, decimal myTableID, decimal myLEingangID, string myZusatzInfo)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            string tmpAktion = enumLagerAktionen.EingangChanged.ToString();
            string tmpBeschreibung = "Eingang geändert per Scanner:  [" + tmpLEingangID.ToString() + "]";
            tmpBeschreibung = tmpBeschreibung + Environment.NewLine + myZusatzInfo;
            string tmpTableName = "LEingang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / LagerEingangChecked</summary>
        ///<remarks>Lagereingang wird abgeschlossen.</remarks>
        public static void LagerEingangChecked(decimal myBenuzter, decimal myTableID, decimal myLEingangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            string tmpAktion = enumLagerAktionen.EingangChecked.ToString();
            string tmpBeschreibung = "Eingang abgeschlossen:  [" + tmpLEingangID.ToString() + "]";
            string tmpTableName = "LEingang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / LagerEingangChecked</summary>
        ///<remarks>Lagereingang wird abgeschlossen.</remarks>
        public static void LagerEingangAutoChecked(decimal myBenuzter, decimal myTableID, decimal myLEingangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            string tmpAktion = enumLagerAktionen.EingangChecked.ToString();
            string tmpBeschreibung = "Eingang autom. abgeschlossen:  [" + tmpLEingangID.ToString() + "]";
            string tmpTableName = "LEingang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / LagerEingangCheckedReset</summary>
        ///<remarks>abgeschlossener Lagereingang wird wieder zurückgesetzt.</remarks>
        public static void LagerEingangCheckedReset(decimal myBenuzter, decimal myTableID, decimal myLEingangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            string tmpAktion = enumLagerAktionen.EingangReset.ToString();
            string tmpBeschreibung = "Eingang Reset:  [" + tmpLEingangID.ToString() + "]";
            string tmpTableName = "LEingang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / LagerEingangChange</summary>
        ///<remarks>Lagereingangsänderung.</remarks>
        public static void LagerAusgangChange(decimal myBenuzter, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLAusgangID = myLAusgangID;
            string tmpAktion = enumLagerAktionen.EingangChanged.ToString();
            string tmpBeschreibung = "Ausgang geändert:  [" + tmpLAusgangID.ToString() + "]";
            string tmpTableName = "LAusgang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / LagerEingangPrintDoc</summary>
        ///<remarks>Druck des Lager Eingangs Dokumentes</remarks>
        public static void LagerEingangPrintDoc(decimal myBenuzter, decimal myTableID, decimal myLEingangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            string tmpAktion = enumLagerAktionen.PrintEingangDoc.ToString();
            string tmpBeschreibung = "Lagereingangsdokument gedruckt:  [" + tmpLEingangID.ToString() + "]";
            string tmpTableName = "LEingang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / LagerEingangPrintAnzeige</summary>
        ///<remarks></remarks>
        public static void LagerEingangPrintAnzeige(decimal myBenuzter, decimal myTableID, decimal myLEingangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            string tmpAktion = enumLagerAktionen.PrintEingangAnzeige.ToString();
            string tmpBeschreibung = "Lagereingangsanzeige gedruckt:  [" + tmpLEingangID.ToString() + "]";
            string tmpTableName = "LEingang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);

        }
        ///<summary>clsArtikelVita / LagerEingangPrintLfs</summary>
        ///<remarks></remarks>
        public static void LagerEingangPrintLfs(decimal myBenuzter, decimal myTableID, decimal myLEingangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            string tmpAktion = enumLagerAktionen.PrintEingangLfs.ToString();
            string tmpBeschreibung = "Lagereingangslieferschein gedruckt:  [" + tmpLEingangID.ToString() + "]";
            string tmpTableName = "LEingang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / LagerEingangPrintPerDay</summary>
        ///<remarks></remarks>
        public static void LagerEingangPrintPerDay(decimal myBenuzter, decimal myTableID, decimal myLEingangID, decimal AdrId = -1, DateTime? date = null)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            string tmpAktion = enumLagerAktionen.PrintEingangPerDay.ToString();
            string tmpBeschreibung = string.Empty;
            string tmpTableName = "LEingang";

            strSql = "Select ID from LEingang where Auftraggeber=" + AdrId + " AND isPrintAnzeige=0 AND Cast([Date] as Date)=Cast('" + date + "' as Date) and Check=1;";
            DataTable dtEingaenge = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myBenuzter, "Eingang");
            for (int i = 0; i < dtEingaenge.Rows.Count; i++)
            {
                tmpBeschreibung = "Lagereingang (Tag) gedruckt:  [" + dtEingaenge.Rows[i]["ID"].ToString() + "]";
                strSql = clsArtikelVita.GetInsertSQL(Decimal.Parse(dtEingaenge.Rows[i]["ID"].ToString()), tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
                clsSQLcon.ExecuteSQL(strSql, myBenuzter);
            }

        }

        /*******************************************************************************************************
         *                                        Artikel
         * ****************************************************************************************************/
        ///<summary>clsArtikelVita / AddArtikelManual</summary>
        ///<remarks>Artikel wird zu einem Eingang hinzugefügt.</remarks>
        public static string AddArtikelLEingangAuto(Globals._GL_USER myGLUser, decimal myTableID, decimal myLEingangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.ArtikelAdd_Eingang.ToString();
            string tmpBeschreibung = "Artikel autom. hinzugefügt: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "]";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            //clsSQLconLVS.ExecuteSQL(strSql, myGLUser.User_ID);
            return strSql;
        }

        ///<summary>clsArtikelVita / AddArtikelManual</summary>
        ///<remarks>Artikel wird zu einem Eingang hinzugefügt.</remarks>
        public static string AddArtikelLAusgangAutoSQL(Globals._GL_USER myGLUser, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLAusgangID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.ArtikelAdd_Eingang.ToString();
            string tmpBeschreibung = "Artikel autom. zum Ausgang hinzugefügt: LVSNr [" + tmpLVSNr.ToString() + "] / Ausgang [" + tmpLEingangID.ToString() + "]";
            string tmpTableName = enumDatabaseSped4_TableNames.Artikel.ToString();
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            //clsSQLconLVS.ExecuteSQL(strSql, myGLUser.User_ID);
            return strSql;
        }
        ///<summary>clsArtikelVita / AddArtikelManual</summary>
        ///<remarks>Artikel wird zu einem Eingang hinzugefügt.</remarks>
        public static void AddArtikelLAusgangAuto(Globals._GL_USER myGLUser, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLAusgangID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.ArtikelAdd_Eingang.ToString();
            string tmpBeschreibung = "Artikel autom. zum Ausgang hinzugefügt: LVSNr [" + tmpLVSNr.ToString() + "] / Ausgang [" + tmpLEingangID.ToString() + "]";
            string tmpTableName = enumDatabaseSped4_TableNames.Artikel.ToString();
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / AddArtikelManual</summary>
        ///<remarks>Artikel wird zu einem Eingang hinzugefügt.</remarks>
        public static void AddArtikelLRL(Globals._GL_USER myGLUser, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, myTableID);
            string tmpBeschreibung = "Rücklieferung an SL: ArtikelID [" + myTableID.ToString() + "] / LVSNr [" + tmpLVSNr.ToString() + "]";
            string tmpAktion = enumLagerAktionen.ArtikelRL.ToString();
            string tmpTableName = enumDatabaseSped4_TableNames.Artikel.ToString();
            strSql = strSql + "INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung) " +
                                "VALUES (" + myTableID +
                                        ",'" + tmpTableName + "'" +
                                        ",'" + tmpAktion + "'" +
                                        ",'" + DateTime.Now + "'" +
                                        ",'" + myGLUser.User_ID + "'" +
                                        ",'" + tmpBeschreibung + "'" +
                                        ")";
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / AddArtikelManual</summary>
        ///<remarks>Artikel wird zu einem Eingang hinzugefügt.</remarks>
        public static string AddArtikelLRLSQL(Globals._GL_USER myGLUser, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, myTableID);
            string tmpBeschreibung = "Rücklieferung an SL: ArtikelID [" + myTableID.ToString() + "] / LVSNr [" + tmpLVSNr.ToString() + "]";
            string tmpAktion = enumLagerAktionen.ArtikelRL.ToString();
            string tmpTableName = enumDatabaseSped4_TableNames.Artikel.ToString();
            strSql = strSql + "INSERT INTO ArtikelVita (TableID, TableName, Aktion, Datum, UserID, Beschreibung) " +
                                "VALUES (" + myTableID +
                                        ",'" + tmpTableName + "'" +
                                        ",'" + tmpAktion + "'" +
                                        ",'" + DateTime.Now + "'" +
                                        ",'" + myGLUser.User_ID + "'" +
                                        ",'" + tmpBeschreibung + "'" +
                                        ")";
            return strSql;
        }
        ///<summary>clsArtikelVita / ArtikelChange</summary>
        ///<remarks>Lagereingangsänderung.</remarks>
        public static void ArtikelChangeAuto(Globals._GL_USER myGLUser, decimal myTableID, decimal myLEingangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.ArtikelChange.ToString();
            string tmpBeschreibung = "Artikel autom. geändert: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "]";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / ArtikelChange</summary>
        ///<remarks>Lagereingangsänderung.</remarks>
        public static void ArtikelDeleteAuto(Globals._GL_USER myGLUser, decimal myTableID, decimal myLEingangID, decimal myLVSNr)
        {
            //Hier muss der Artikel nun in ArtikelVita auf den Eingang eingetragen werden
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = myLVSNr;
            string tmpAktion = enumLagerAktionen.ArtikelDelete.ToString();
            string tmpBeschreibung = "Artikel autom. gelöscht: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "]";
            //nicht Artikel, damit der Delete dem Eingang zugewiesen wird
            string tmpTableName = "LEingang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / AddArtikelManual</summary>
        ///<remarks>Artikel wird zu einem Eingang hinzugefügt.</remarks>
        public static void AddArtikelManualLEingang(Globals._GL_USER myGLUser, decimal myTableID, decimal myLEingangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.ArtikelAdd_Eingang.ToString();
            string tmpBeschreibung = "Artikel hinzugefügt: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "]";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        /****
        ///<summary>clsArtikelVita / AddArtikelLEingang_UB_ArtikelNeu</summary>
        ///<remarks>Artikel wird umgebucht.</remarks>
        public static void AddArtikelLEingang_UB_ArtikelNeu(Globals._GL_USER myGLUser, decimal myTableID, decimal myLEingangID, decimal myTableIDAlt, decimal myLvsNrVorUB)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = Globals.enumLagerAktionen.ArtikelUmbuchung.ToString();
            string tmpBeschreibung = "UB Artikel neu: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "]  / LVSNr alt [" + myLvsNrVorUB.ToString() +"] / ID alt [" + myTableIDAlt.ToString() + "]";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
         * ***/
        ///<summary>clsArtikelVita / AddArtikelLEingang_UB_ArtikelNeu</summary>
        ///<remarks>Artikel wird umgebucht.</remarks>
        public static void AddArtikelLEingang_UB_ArtikelNeu(Globals._GL_USER myGLUser, clsArtikel artikelALT, clsArtikel artikelNEU)
        {
            decimal tmpLEingangIDNEU = clsLEingang.GetLEingangIDByLEingangTableID(myGLUser.User_ID, artikelNEU.LEingangTableID);
            decimal tmpLEingangIDALT = clsLEingang.GetLEingangIDByLEingangTableID(myGLUser.User_ID, artikelALT.LEingangTableID);
            string strSql = string.Empty;
            string tmpAktion = enumLagerAktionen.ArtikelUmbuchung.ToString();
            string tmpBeschreibung = "UB Artikel neu: LVSNr [" + artikelNEU.LVS_ID.ToString() + "] / Eingang [" + tmpLEingangIDNEU.ToString() + "]  <<< LVSNr alt [" + artikelALT.LVS_ID.ToString() + "] / ID alt [" + artikelALT.ID.ToString() + "] / Eingang alt [" + tmpLEingangIDALT.ToString() + "]";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(artikelNEU.ID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }

        /***
        ///<summary>clsArtikelVita / AddArtikelLEingang_UB_ArtikelAlt</summary>
        ///<remarks>Artikel wird umgebucht.</remarks>
        public static void AddArtikelLEingang_UB_ArtikelAlt(Globals._GL_USER myGLUser, decimal myTableID, decimal myLEingangID, decimal myTableIDNeu, decimal myLVSNrNeu)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = Globals.enumLagerAktionen.ArtikelUmbuchung.ToString();
            string tmpBeschreibung = "UB Artikel: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "] / LVSNr neu [" + myLVSNrNeu.ToString() +"] / ID neu [" + myTableIDNeu.ToString() + "]";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ****/
        ///<summary>clsArtikelVita / AddArtikelLEingang_UB_ArtikelAlt</summary>
        ///<remarks>Artikel wird umgebucht.</remarks>
        public static void AddArtikelLEingang_UB_ArtikelAlt(Globals._GL_USER myGLUser, clsArtikel artikelALT, clsArtikel artikelNEU)
        {
            string strSql = string.Empty;
            decimal tmpLEingangIDNEU = clsLEingang.GetLEingangIDByLEingangTableID(myGLUser.User_ID, artikelNEU.LEingangTableID);
            decimal tmpLEingangIDALT = clsLEingang.GetLEingangIDByLEingangTableID(myGLUser.User_ID, artikelALT.LEingangTableID);
            string tmpAktion = enumLagerAktionen.ArtikelUmbuchung.ToString();
            string tmpBeschreibung = "UB Artikel: LVSNr [" + artikelALT.LVS_ID.ToString() + "] / Eingang [" + tmpLEingangIDALT.ToString() + "] >>> LVSNr neu [" + artikelNEU.LVS_ID.ToString() + "] / ID neu [" + artikelNEU.ID.ToString() + "] / Eingang neu [" + tmpLEingangIDNEU.ToString() + "]";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(artikelALT.ID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / ArtikelChange</summary>
        ///<remarks>Lagereingangsänderung.</remarks>
        public static void ArtikelChange(Globals._GL_USER myGLUser, decimal myTableID, decimal myLEingangID, string strZusatzInfo)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.ArtikelChange.ToString();
            string tmpBeschreibung = "Artikel geändert: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "] ";
            tmpBeschreibung = tmpBeschreibung + Environment.NewLine + strZusatzInfo;
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            bool retVal = clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / ArtikelChange</summary>
        ///<remarks>Lagereingangsänderung.</remarks>
        public static void ArtikelChangeByScan(Globals._GL_USER myGLUser, decimal myTableID, decimal myLEingangID, string strZusatzInfo)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.ArtikelChange.ToString();
            string tmpBeschreibung = "SCAN - Artikel geändert: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "] ";
            tmpBeschreibung = tmpBeschreibung + Environment.NewLine + strZusatzInfo;
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            bool retVal = clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / ArtikelChange</summary>
        ///<remarks>Lagereingangsänderung.</remarks>
        public static void ArtikelDelete(Globals._GL_USER myGLUser, decimal myTableID, decimal myLEingangID, decimal myLVSNr)
        {
            //Hier muss der Artikel nun in ArtikelVita auf den Eingang eingetragen werden
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = myLVSNr;
            string tmpAktion = enumLagerAktionen.ArtikelDelete.ToString();
            string tmpBeschreibung = "Artikel gelöscht: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "]";
            //nicht Artikel, damit der Delete dem Eingang zugewiesen wird
            string tmpTableName = "LEingang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / ArtikelChecked</summary>
        ///<remarks>Artikel im Eingang geprüft.</remarks>
        public static void ArtikelChecked(decimal myBenuzter, decimal myTableID, decimal myLEingangID, decimal myLVSNr)
        {
            //Hier muss der Artikel nun in ArtikelVita auf den Eingang eingetragen werden
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = myLVSNr;
            string tmpAktion = enumLagerAktionen.ArtikelChecked.ToString();
            string tmpBeschreibung = "Artikel geprüft: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "]";
            //nicht Artikel, damit der Delete dem Eingang zugewiesen wird
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / ArtikelChecked</summary>
        ///<remarks>Artikel im Eingang per Scaner geprüft.</remarks>
        public static void ArtikelCheckedByScan(decimal myBenuzter, decimal myTableID, decimal myLEingangID, decimal myLVSNr)
        {
            //Hier muss der Artikel nun in ArtikelVita auf den Eingang eingetragen werden
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = myLVSNr;
            string tmpAktion = enumLagerAktionen.ArtikelChecked.ToString();
            string tmpBeschreibung = "SCAN - Artikel geprüft: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "]";
            //nicht Artikel, damit der Delete dem Eingang zugewiesen wird
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            bool bOK = clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / ArtikelAddSchaden</summary>
        ///<remarks>Schaden bei dem Aritkel hinterlegt.</remarks>
        public static void ArtikelAddSchaden(decimal myBenuzter, decimal myTableID, decimal myLEingangID, decimal myLVSNr, string mySchadenstext)
        {
            //Hier muss der Artikel nun in ArtikelVita auf den Eingang eingetragen werden
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = myLVSNr;
            string tmpAktion = enumLagerAktionen.ArtikelSchadenAdd.ToString();
            string tmpBeschreibung = "Artikelschaden hinterlegt: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "] wurden folgende Schäden hinterlegt: ";
            tmpBeschreibung = tmpBeschreibung + Environment.NewLine + mySchadenstext;
            //nicht Artikel, damit der Delete dem Eingang zugewiesen wird
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }

        ///<summary>clsArtikelVita / ArtikelAddSchaden</summary>
        ///<remarks>Schaden bei dem Aritkel hinterlegt.</remarks>
        public static void ArtikelAddSchadenByScan(decimal myBenuzter, decimal myTableID, decimal myLEingangID, decimal myLVSNr, string mySchadenstext)
        {
            //Hier muss der Artikel nun in ArtikelVita auf den Eingang eingetragen werden
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = myLVSNr;
            string tmpAktion = enumLagerAktionen.ArtikelSchadenAdd.ToString();
            string tmpBeschreibung = "SCAN - Artikelschaden hinterlegt: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "] wurden folgende Schäden hinterlegt: ";
            tmpBeschreibung = tmpBeschreibung + Environment.NewLine + mySchadenstext;
            //nicht Artikel, damit der Delete dem Eingang zugewiesen wird
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }

        ///<summary>clsArtikelVita / ArtikelDelSchaden</summary>
        ///<remarks>Schaden bei Artikel gelöscht.</remarks>
        public static void ArtikelDelSchaden(decimal myBenuzter, decimal myTableID, decimal myLEingangID, decimal myLVSNr, string mySchadenstext)
        {
            //Hier muss der Artikel nun in ArtikelVita auf den Eingang eingetragen werden
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = myLVSNr;
            string tmpAktion = enumLagerAktionen.ArtikelSchadenDel.ToString();
            string tmpBeschreibung = "Artikelschaden gelöscht: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "] wurde folgender Schaden gelöscht: ";
            tmpBeschreibung = tmpBeschreibung + Environment.NewLine + mySchadenstext;

            //nicht Artikel, damit der Delete dem Eingang zugewiesen wird
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / ArtikelDelSchaden</summary>
        ///<remarks>Schaden bei Artikel gelöscht.</remarks>
        public static void ArtikelDelSchadenByScan(decimal myBenuzter, decimal myTableID, decimal myLEingangID, decimal myLVSNr, string mySchadenstext)
        {
            //Hier muss der Artikel nun in ArtikelVita auf den Eingang eingetragen werden
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = myLVSNr;
            string tmpAktion = enumLagerAktionen.ArtikelSchadenDel.ToString();
            string tmpBeschreibung = "SCAN - Artikelschaden gelöscht: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "] wurde folgender Schaden gelöscht: ";
            tmpBeschreibung = tmpBeschreibung + Environment.NewLine + mySchadenstext;

            //nicht Artikel, damit der Delete dem Eingang zugewiesen wird
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / Sonderkosten Zuweisung</summary>
        ///<remarks>Eintrag Zuweisung von Sonderkosten.</remarks>
        public static string ArtikelSonderkostenAdd(decimal myBenuzter, decimal myTableID, decimal myLEingangID, decimal myLVSNr, decimal myExtraChargeID, string myExtraChargeBezeichnung)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = myLVSNr;
            string tmpAktion = enumLagerAktionen.ArtikelSonderkosteAdd.ToString();
            string tmpBeschreibung = "Artikel Sonderkosten zugewiesen: ArtikelID [" + tmpTableID.ToString() + "] / " +
                                                                       "Sonderkosten ID [" + myExtraChargeID.ToString() + "] - " +
                                                                       myExtraChargeBezeichnung + " ";
            //nicht Artikel, damit der Delete dem Eingang zugewiesen wird
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            return strSql;
        }
        ///<summary>clsArtikelVita / Sonderkosten Zuweisung</summary>
        ///<remarks>Eintrag Zuweisung von Sonderkosten.</remarks>
        public static string ArtikelSonderkostenDelete(decimal myBenuzter, decimal myTableID, decimal myLEingangID, decimal myLVSNr, decimal myExtraChargeID, string myExtraChargeBezeichnung)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = myLVSNr;
            string tmpAktion = enumLagerAktionen.ArtikelSonderkostenDel.ToString();
            string tmpBeschreibung = "Artikel Sonderkosten gelöscht: ArtikelID [" + tmpTableID.ToString() + "] / " +
                                                                       "Sonderkosten ID [" + myExtraChargeID.ToString() + "] - " +
                                                                       myExtraChargeBezeichnung + " ";
            //nicht Artikel, damit der Delete dem Eingang zugewiesen wird
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            return strSql;
        }


        ///<summary>clsArtikelVita / AddArtikelManual</summary>
        ///<remarks>Artikel wird zu einem Eingang hinzugefügt.</remarks>
        public static void AddArtikelLEingangScanner(Articles myArticle, int myUserId)
        {
            string strSql = string.Empty;
            //decimal tmpTableID = myTableID;
            //decimal tmpLEingangID = myLEingangID;
            //decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.ArtikelAdd_Eingang.ToString();
            string tmpBeschreibung = "Artikel per Scanner hinzugefügt: LVSNr [" + myArticle.LVS_ID.ToString() + "] / Eingang [" + myArticle.Eingang.LEingangID.ToString() + "]";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(myArticle.Id, tmpTableName, tmpAktion, myUserId, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myUserId);
            //return strSql;
        }


        ///<summary>clsArtikelVita / ArtikelSetInfoCustomerProcessExceptionExist</summary>
        ///<remarks>Eintrag Anwendung Customer Prozess Ausnahme.</remarks>
        public static string ArtikelSetInfoCustomerProcessExceptionExist(int myBenuzter, int myTableID, int myLEingangID, int myLVSNr, LVS.Models.CustomProcesses myCustomerProcess, LVS.Models.CustomProcessExceptions myCustomerProcessException)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = myLVSNr;
            string tmpAktion = enumLagerAktionen.ArtikelCustomerProcessExceptionExist.ToString();
            string tmpBeschreibung = "Ausnahme kundenspezifischer Prozess angewendet: ArtikelID / LVSNR: [" + tmpTableID.ToString() + "] / [" + myLVSNr + "]" +
                                                                                     " | Kundenspezifischer Prozess ID [" + myCustomerProcess.Id + "] - " + myCustomerProcess.ProcessName +
                                                                                     " | Ausnahme ID [" + myCustomerProcessException.Id + "] - " +
                                                                                     " | Gut Id / Name: [" + myCustomerProcessException.GoodsTypeId + "] - " + myCustomerProcessException.GoodsTypeName;
            //nicht Artikel, damit der Delete dem Eingang zugewiesen wird
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            return strSql;
        }


        /*************************************************************************************************************
         *                                      Lagerausgang 
         * **********************************************************************************************************/
        ///<summary>clsArtikelVita / GetSQLDeleteLagerAusgang</summary>
        ///<remarks>Löschen der Artikel eines LEingangs. Durchgeführt wird der Delete über eine Transaction in der
        ///         Klasse Lager.</remarks>
        public string GetSQLDeleteLagerAusgang(decimal myLAusgangTableID)
        {
            string strSql = string.Empty;
            if (myLAusgangTableID > 0)
            {
                strSql = "DELETE FROM ArtikelVita WHERE " +
                              "(TableName='LAusgang' AND TableID IN(Select LAusgangTableID FROM Artikel WHERE LAusgangTableID='" + myLAusgangTableID + "')) " +
                              "OR " +
                              "(TableName='Artikel' AND TableID IN(Select ID FROM Artikel WHERE LAusgangTableID='" + myLAusgangTableID + "'));";
            }
            return strSql;
        }
        ///<summary>clsArtikelVita / AddArtikelManualLAusgang</summary>
        ///<remarks>Artikel wird zu einem Eingang hinzugefügt.</remarks>
        public static void AddArtikelManualLAusgang(Globals._GL_USER myGLUser, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLAusgangID = myLAusgangID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.ArtikelAdd_Ausgang.ToString();
            string tmpBeschreibung = "Artikel Ausgang hinzugefügt: LVSNr [" + tmpLVSNr.ToString() + "] / Ausgang [" + tmpLAusgangID.ToString() + "]";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / DeleteArtikelManualFROMLAusgang</summary>
        ///<remarks>Artikel wird zu einem Eingang hinzugefügt.</remarks>
        public static void DeleteArtikelManualFROMLAusgang(Globals._GL_USER myGLUser, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLAusgangID = myLAusgangID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.ArtikelAdd_Ausgang.ToString();
            string tmpBeschreibung = "Artikel aus Ausgang entfernt: LVSNr [" + tmpLVSNr.ToString() + "] / Ausgang [" + tmpLAusgangID.ToString() + "]";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / AddAuslagerungManual</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public static void AddAuslagerungManual(decimal myBenuzter, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLAusgangID = myLAusgangID;
            string tmpAktion = enumLagerAktionen.AusgangErstellt.ToString();
            string tmpBeschreibung = "Lagerausgang [" + tmpLAusgangID.ToString() + "] manuell erstellt";
            string tmpTableName = "LAusgang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / LagerAusgangChecked</summary>
        ///<remarks>Lagerausgang abgeschlossen.</remarks>
        public static void LagerAusgangChecked(decimal myBenuzter, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLAusgangID = myLAusgangID;
            string tmpAktion = enumLagerAktionen.AusgangChecked.ToString();
            string tmpBeschreibung = "Ausgang abgeschlossen:  [" + tmpLAusgangID.ToString() + "]";
            string tmpTableName = "LAusgang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / LagerAusgangAutoChecked</summary>
        ///<remarks>Lagerausgang abgeschlossen.</remarks>
        public static void LagerAusgangAutoChecked(decimal myBenuzter, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLAusgangID = myLAusgangID;
            string tmpAktion = enumLagerAktionen.AusgangChecked.ToString();
            string tmpBeschreibung = "Ausgang autom. abgeschlossen:  [" + tmpLAusgangID.ToString() + "]";
            string tmpTableName = "LAusgang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / LagerAusgangPrintDoc</summary>
        ///<remarks></remarks>
        public static void LagerAusgangPrintDoc(decimal myBenuzter, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLAusgangID = myLAusgangID;
            string tmpAktion = enumLagerAktionen.PrintAusgangDoc.ToString();
            string tmpBeschreibung = "Lagerausgangsdokument gedruckt:  [" + tmpLAusgangID.ToString() + "]";
            string tmpTableName = "LAusgang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / LagerAusgangPrintAnzeige</summary>x
        ///<remarks></remarks>
        public static void LagerAusgangPrintAnzeige(decimal myBenuzter, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLAusgangID = myLAusgangID;
            string tmpAktion = enumLagerAktionen.PrintEingangAnzeige.ToString();
            string tmpBeschreibung = "Lagerausgangsanzeige gedruckt:  [" + tmpLAusgangID.ToString() + "]";
            string tmpTableName = "LAusgang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);

        }
        ///<summary>clsArtikelVita / LagerAusgangPrintLfs</summary>
        ///<remarks></remarks>
        public static void LagerAusgangPrintLfs(decimal myBenuzter, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLAusgangID = myLAusgangID;
            string tmpAktion = enumLagerAktionen.PrintEingangLfs.ToString();
            string tmpBeschreibung = "Lagerausgangslieferschein gedruckt:  [" + tmpLAusgangID.ToString() + "]";
            string tmpTableName = "LAusgang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
        }
        ///<summary>clsArtikelVita / LagerAusgangPrintPerDay</summary>
        ///<remarks></remarks>
        public static void LagerAusgangPrintPerDay(decimal myBenuzter, decimal myTableID, decimal myLAusgangID, decimal AdrId = -1, DateTime? date = null)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLAusgangID = myLAusgangID;
            string tmpAktion = enumLagerAktionen.PrintAusgangPerDay.ToString();
            string tmpBeschreibung = "Lagerausgang (Tag) gedruckt:  [" + tmpLAusgangID.ToString() + "]";
            string tmpTableName = "LAusgang";
            strSql = "Select ID from LAusgang where Auftraggeber=" + AdrId + " AND isPrintAnzeige=0 AND Cast(Datum as Date)=Cast('" + date + "' as Date)  and Checked=1;";
            DataTable dtEingaenge = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myBenuzter, tmpTableName);
            for (int i = 0; i < dtEingaenge.Rows.Count; i++)
            {
                strSql = clsArtikelVita.GetInsertSQL(Decimal.Parse(dtEingaenge.Rows[i]["ID"].ToString()), tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
                clsSQLcon.ExecuteSQL(strSql, myBenuzter);
            }
        }
        ///<summary>clsArtikelVita / AddAuslagerungAuto</summary>
        ///<remarks>.</remarks>
        public static string AddAuslagerungAutoSQL(decimal myBenuzter, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLAusgangID;
            string tmpAktion = enumLagerAktionen.AusgangErstellt.ToString();
            string tmpBeschreibung = "Lagerausgang [" + tmpLEingangID.ToString() + "] autom. erstellt";
            string tmpTableName = "LAusgang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            //clsSQLconLVS.ExecuteSQL(strSql, myBenuzter);
            return strSql;
        }
        ///<summary>clsArtikelVita / AddAuslagerungAuto</summary>
        ///<remarks>.</remarks>
        public static void AddAuslagerungAuto(decimal myBenuzter, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLAusgangID;
            string tmpAktion = enumLagerAktionen.AusgangErstellt.ToString();
            string tmpBeschreibung = "Lagerausgang [" + tmpLEingangID.ToString() + "] autom. erstellt";
            string tmpTableName = "LAusgang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
            //return strSql;
        }
        ///<summary>clsArtikelVita / AddAuslagerungAuto</summary>
        ///<remarks>.</remarks>
        public static void AddAuslagerungAutoSPL(decimal myBenuzter, decimal myTableID, decimal myLAusgangID)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLAusgangID;
            string tmpAktion = enumLagerAktionen.AusgangErstellt.ToString();
            string tmpBeschreibung = "Lagerausgang autom. aus SPL [" + tmpLEingangID.ToString() + "] erstellt";
            string tmpTableName = "LAusgang";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myBenuzter, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myBenuzter);
            //return strSql;
        }
        /****************************************************************************************
         *                          Sperrlager
         * *************************************************************************************/
        ///<summary>clsArtikelVita / SperrlagerCheckIN</summary>
        ///<remarks>Artikel wird in Sperrlager umgebucht.</remarks>
        public static void SperrlagerCheckIN(Globals._GL_USER myGLUser, decimal myTableID, decimal myLEingangID, decimal myTableIDNeu, decimal myLVSNrNeu)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.SperrlagerIN.ToString();
            string tmpBeschreibung = "Sperrlager IN: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "] / LVSNr [" + myLVSNrNeu.ToString() + "] / ID [" + myTableIDNeu.ToString() + "]";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / SperrlagerCheckOUT</summary>
        ///<remarks>Artikel wird aus Sperrlager ausgebucht.</remarks>
        public static void SperrlagerCheckOUT(Globals._GL_USER myGLUser, decimal myTableID, decimal myLEingangID, decimal myTableIDNeu, decimal myLVSNrNeu)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLEingangID = myLEingangID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.SperrlagerOUT.ToString();
            string tmpBeschreibung = "Sperrlager IN: LVSNr [" + tmpLVSNr.ToString() + "] / Eingang [" + tmpLEingangID.ToString() + "] / LVSNr [" + myLVSNrNeu.ToString() + "] / ID  [" + myTableIDNeu.ToString() + "]";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / getCheckDate</summary>
        ///<remarks>Holt das Datum an dem der Eingang das erste mal abgeschlossen worden ist oder ein "leeres" Datum Jahr = 0001</remarks>
        public static DateTime getCheckDate(Globals._GL_USER myGLUser, decimal LEingangID)
        {
            DateTime retDate = new DateTime();
            string strSql = "SELECT Top 1 [Datum] " +
                            "FROM [ArtikelVita] " +
                            "where Aktion='EingangChecked' AND TableID=" + LEingangID + " AND TableName='LEingang'" +
                            " Order by Datum;";
            DateTime.TryParse(clsSQLcon.ExecuteSQL_GetValue(strSql, myGLUser.User_ID), out retDate);
            return retDate;
        }
        /************************************************************************************************************
          *                                              Lagermeldungen
          * **********************************************************************************************************/
        ///<summary>clsArtikelVita / AddArtikelLagermeldungen</summary>
        ///<remarks></remarks>
        public static string AddArtikelLagermeldungen(Globals._GL_USER myGLUser, decimal myTableID, string myAktion)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpBeschreibung = myAktion + " - Lagermeldung für LVSNr [" + tmpLVSNr.ToString() + "] eingelesen/erstellt";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, myAktion, myGLUser.User_ID, tmpBeschreibung);
            //clsSQLconLVS.ExecuteSQL(strSql, myGLUser.User_ID);
            return strSql;
        }
        ///<summary>clsArtikelVita / AddArtikelManualLAusgang</summary>
        ///<remarks>Artikel wird zu einem Eingang hinzugefügt.</remarks>
        public static void AddArtikelKorrekturStVerfahren(Globals._GL_USER myGLUser, decimal myTableID, string myAktion)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpBeschreibung = myAktion + " - Stornierung- und Korrekturverfahren für LVSNr [" + tmpLVSNr.ToString() + "] angewendet";
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, myAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }

        /************************************************************************************************************
          *                                            Abrufe /Call
          * **********************************************************************************************************/
        ///<summary>clsArtikelVita / AddCall</summary>
        ///<remarks>Abruf für einen Aritkel wird manuell erstellt</remarks>
        public static void Call_AddCall(Globals._GL_USER myGLUser, decimal myTableID, Int32 iAbrufID, string myCallTxt)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.AbrufCreate.ToString();
            string tmpBeschreibung = "Abruf man. erstellt: LVSNr [" + tmpLVSNr.ToString() + "] / ID [" + tmpTableID.ToString() + "] / AbrufID [" + iAbrufID.ToString() + "] ";
            tmpBeschreibung = tmpBeschreibung + Environment.NewLine + myCallTxt;
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / AddCall</summary>
        ///<remarks>Abruf für einen Aritkel wird manuell geändert</remarks>
        public static void Call_ChangeCall(Globals._GL_USER myGLUser, decimal myTableID, Int32 iAbrufID, string myCallTxt)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.AbrufChange.ToString();
            string tmpBeschreibung = "Abruf bearbeitet: LVSNr [" + tmpLVSNr.ToString() + "] / ID [" + tmpTableID.ToString() + "] / AbrufID [" + iAbrufID.ToString() + "] ";
            tmpBeschreibung = tmpBeschreibung + Environment.NewLine + myCallTxt;
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / AddCall</summary>
        ///<remarks>Abruf für einen Aritkel wird manuell erstellt</remarks>
        public static void Call_DeleteCall(Globals._GL_USER myGLUser, decimal myTableID, Int32 iAbrufID, string myCallTxt)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.AbrufDelete.ToString();
            string tmpBeschreibung = "Abruf gelöscht: LVSNr [" + tmpLVSNr.ToString() + "] / ID [" + tmpTableID.ToString() + "] / AbrufID [" + iAbrufID.ToString() + "]";
            tmpBeschreibung = tmpBeschreibung + Environment.NewLine + myCallTxt;
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / DeactivateCall</summary>
        ///<remarks>Abruf wird manuell deaktiviert</remarks>
        public static string Call_DeactivateCall(Globals._GL_USER myGLUser, decimal myTableID, Int32 iAbrufID, string myCallTxt)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpAktion = enumLagerAktionen.AbrufDelete.ToString();
            string tmpBeschreibung = "Abruf deaktiviert: LVSNr [" + tmpLVSNr.ToString() + "] / ID [" + tmpTableID.ToString() + "] / AbrufID [" + iAbrufID.ToString() + "]";
            tmpBeschreibung = tmpBeschreibung + Environment.NewLine + myCallTxt;
            string tmpTableName = "Artikel";
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, tmpAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
            return tmpBeschreibung;
        }

        /************************************************************************************************************
          *                                              Schadensbilder / Fotos
          * **********************************************************************************************************/
        ///<summary>clsArtikelVita / AddImageToArtikel</summary>
        ///<remarks></remarks>
        public static void AddImageToArtikel(Globals._GL_USER myGLUser, decimal myTableID, string myAktion, string myImageText)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpBeschreibung = "Bild/Foto [" + myImageText + "] für ArtikelID/LVSNr [" + myTableID.ToString() + "/" + tmpLVSNr.ToString() + "] hinzugefügt";
            string tmpTableName = enumDatabaseSped4_TableNames.Artikel.ToString();
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, myAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }
        ///<summary>clsArtikelVita / DeleteImageToArtikel</summary>
        ///<remarks></remarks>
        public static void DeleteImageFromArtikel(Globals._GL_USER myGLUser, decimal myTableID, string myAktion, string myImageText)
        {
            string strSql = string.Empty;
            decimal tmpTableID = myTableID;
            decimal tmpLVSNr = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, tmpTableID);
            string tmpBeschreibung = "Bild/Foto [" + myImageText + "] für ArtikelID/LVSNr [" + myTableID.ToString() + "/" + tmpLVSNr.ToString() + "] entfernt";
            string tmpTableName = enumDatabaseSped4_TableNames.Artikel.ToString();
            strSql = clsArtikelVita.GetInsertSQL(tmpTableID, tmpTableName, myAktion, myGLUser.User_ID, tmpBeschreibung);
            clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);
        }


        /************************************************************************************************************
        *                                             
        * **********************************************************************************************************/
        ///<summary>clsArtikelVita / AddImageToArtikel</summary>
        ///<remarks></remarks>
        public static DateTime GetFirstDateTimeLEingangChecked(Globals._GL_USER myGLUser, decimal myTableID)
        {
            string strSql = string.Empty;
            strSql = "Select Top(1) ArtikelVita.Datum from ArtikelVita " +
                                                    "WHERE " +
                                                    "ArtikelVita.Aktion='EingangChecked' " +
                                                    "and ArtikelVita.TableID=" + (Int32)myTableID + " " +
                                                    "and ArtikelVita.TableName='LEingang'";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGLUser.User_ID);
            DateTime returnDate = clsSystem.const_DefaultDateTimeValue_Min;
            if (!DateTime.TryParse(strTmp, out returnDate))
            {
                returnDate = clsSystem.const_DefaultDateTimeValue_Min;
            }
            return returnDate;
        }

    }
}
