using Common.Enumerations;
using Common.Models;
using System;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class PrimeKeyViewData
    {
        public decimal BenutzerID { get; set; } = 0;
        public PrimeKeys PrimeKey { get; set; }
        public PrimeKeyViewData()
        {
            InitCls();
        }
        public PrimeKeyViewData(int myUserId) : this()
        {
            BenutzerID = myUserId;
        }
        public PrimeKeyViewData(int myWorkspaceId, int myMandantenId, int myUserId) : this()
        {
            BenutzerID = myUserId;
            PrimeKey.MandantenId = myMandantenId;
            PrimeKey.ArbeitsbereichId = myWorkspaceId;
        }

        private void InitCls()
        {
            PrimeKey = new PrimeKeys();
        }
        public void Fill()
        {
            //string strSQL = sql_Get;
            //DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "GoodArt", "GoodArt", BenutzerID);
            //if (dt.Rows.Count > 0)
            //{ 
            //    foreach(DataRow dr in dt.Rows) 
            //    {
            //        SetValue(dr);
            //    }
            //}
        }
        private void SetValue(DataRow row)
        {
            PrimeKey = new PrimeKeys();

            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            PrimeKey.Id = iTmp;
            iTmp = 0;
            int.TryParse(row["Mandanten_ID"].ToString(), out iTmp);
            PrimeKey.MandantenId = iTmp;

            iTmp = 0;
            int.TryParse(row["AuftragsNr"].ToString(), out iTmp);
            PrimeKey.AuftragsNr = iTmp;
            iTmp = 0;
            int.TryParse(row["LfsNr"].ToString(), out iTmp);
            PrimeKey.LfsNr = iTmp;
            iTmp = 0;
            int.TryParse(row["LvsNr"].ToString(), out iTmp);
            PrimeKey.LvsNr = iTmp;
            iTmp = 0;
            int.TryParse(row["RGNr"].ToString(), out iTmp);
            PrimeKey.RGNr = iTmp;
            iTmp = 0;
            int.TryParse(row["GSNr"].ToString(), out iTmp);
            PrimeKey.GSNr = iTmp;
            iTmp = 0;
            int.TryParse(row["LEingangID"].ToString(), out iTmp);
            PrimeKey.LEingangID = iTmp;
            iTmp = 0;
            int.TryParse(row["LAusgangID"].ToString(), out iTmp);
            PrimeKey.LAusgangID = iTmp;
            iTmp = 0;
            int.TryParse(row["AbBereichID"].ToString(), out iTmp);
            PrimeKey.ArbeitsbereichId = iTmp;

            FillMAXValue();

        }
        public void FillMAXValue()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;

            strSql = "SELECT " +
                "(Select ISNULL(MAX(ANr),1) FROM Auftrag WHERE MandantenID=a.Mandanten_ID AND ArbeitsbereichID=a.AbBereichID) as MaxAuftragsNr " +
                ", (Select ISNULL(MAX(LfsNr),1) FROM Lieferschein d " +
                                        "INNER JOIN AuftragPos b ON b.ID=d.AP_ID " +
                                        "INNER JOIN Auftrag c ON c.ID=b.AuftragTableID " +
                                        "WHERE c.MandantenID=a.Mandanten_ID AND c.ArbeitsbereichID=a.AbBereichID) as MaxLfsNr " +
                ", (Select ISNULL(MAX(b.LVS_ID),1) FROM Artikel b " +
                                        "INNER JOIN LEingang c ON c.ID=b.LEingangTableID " +
                                        "WHERE c.Mandant=a.Mandanten_ID AND c.AbBereich=a.AbBereichID ) as MaxLvsNr " +
                ", (SELECT ISNULL(MAX(RGNr),1) FROM Rechnungen WHERE GS=0 AND MandantenID=a.Mandanten_ID) as MaxRGNr " +
                ", (SELECT ISNULL(MAX(RGNr),1) FROM Rechnungen WHERE GS=1 AND MandantenID=a.Mandanten_ID) as MaxGSNr " +
                ", (SELECT ISNULL(MAX(LEingangID),1) FROM LEingang WHERE Mandanten_ID=a.Mandanten_ID AND AbBereich=a.AbBereichID) as MaxLEingangID " +
                ", (SELECT ISNULL(MAX(LAusgangID),1) FROM lAusgang WHERE MandantenID=a.Mandanten_ID AND AbBereich=a.AbBereichID)as MaxLAusgangID " +

                "FROM PrimeKeys a WHERE a.Mandanten_ID=" + PrimeKey.MandantenId + "; ";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Primekeys");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                PrimeKey.MAXAuftragsNr = (int)dt.Rows[i]["MaxAuftragsNr"];
                PrimeKey.MAXLfsNr = (int)dt.Rows[i]["MaxLfsNr"];
                PrimeKey.MAXLvsNr = (int)dt.Rows[i]["MaxLvsNr"];
                PrimeKey.MAXRGNr = (int)dt.Rows[i]["MaxRGNr"];
                PrimeKey.MAXGSNr = (int)dt.Rows[i]["MaxGSNr"];
                PrimeKey.MAXLEingangID = (int)dt.Rows[i]["MaxLEingangID"];
                PrimeKey.MAXLAusgangID = (int)dt.Rows[i]["MaxLAusgangID"];
            }
        }
        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            string strSql = sql_Add;
        }

        /// <summary>
        ///             GET sql - String
        /// </summary>
        public string sql_Get_aktiv
        {
            get
            {
                string strSql = string.Empty;
                //strSql += "SELECT DISTINCT g.* ";
                //strSql += " FROM Gueterart g ";
                //strSql += " INNER JOIN ArbeitsbereichGArten ag on ag.AbBereichID=g.Arbeitsbereich ";
                //strSql += " LEFT JOIN GueterartADR ga on ga.GArtID = g.ID ";
                //strSql += " where ";
                //strSql += " (g.Id=1) or ";
                //strSql += " g.aktiv=1";
                return strSql;
            }
        }


        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public bool Update_WizStoreOut(enumStoreOutArt myStoreOutArt, enumStoreOutArt_Steps myStoreOutArt_Steps)
        {
            bool retVal = false;
            string strSql = string.Empty;
            bool IsLastStep = false;
            //switch (myStoreOutArt)
            //{
            //    case enumStoreOutArt.call:
            //        strSql = sqlCreater_WizStoreOut_Call.sql_String_StoreOut_Call(Abruf, myStoreOutArt_Steps);
            //        retVal = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "AusgangUpdate", BenutzerID);
            //        break;

            //    case enumStoreOutArt.manually:
            //    case enumStoreOutArt.open:                    
            //        break;
            //}
            return retVal;
        }

        ///-----------------------------------------------------------------------------------------------------
        ///                             sql Statements
        ///-----------------------------------------------------------------------------------------------------

        /// <summary>
        ///             Add sql - String
        /// </summary>
        public string sql_Add
        {
            get
            {
                string strSql = string.Empty;
                return strSql;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_Get
        {
            get
            {
                string strSql = string.Empty;
                //strSql = "SELECT * FROM Gueterart WHERE ID=" + Gut.Id + ";";
                return strSql;
            }
        }

        //public string sql_ExistsPrimeKeysMandant
        //{
        //    get
        //    {
        //        string strSql = string.Empty;
        //        strSql = "Select * FROM PrimeKeys WHERE Mandanten_ID=" + PrimeKey.MandantenId + " AND AbBereichID=" + PrimeKey.ArbeitsbereichId + ";";
        //        return strSql;
        //    }
        //}


        /// <summary>
        ///             DELETE sql - String
        /// </summary>
        public string sql_Delete
        {
            get
            {
                string strSql = string.Empty;
                //strSql = "Delete FROM LAusgang WHERE ID =" + Ausgang.Id; ;
                return strSql;
            }
        }
        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Update
        {
            get
            {
                string strSql = string.Empty;

                return strSql;
            }
        }



        //-----------------------------------------------------------------------------------------------------
        //                              static
        //-----------------------------------------------------------------------------------------------------
        public static bool ExistPrimeKeysMandant(decimal decMandant, decimal decAbBereich, decimal decBenutzerID)
        {
            bool boVal = false;
            if (decMandant > 0)
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM PrimeKeys WHERE Mandanten_ID=" + decMandant + " AND AbBereichID=" + decAbBereich + ";";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "PrimeKeys");

                if (dt.Rows.Count > 0)
                {
                    boVal = true;
                }
            }
            return boVal;
        }
        ///<summary>clsPrimeKeys / GetNEWLvsNr</summary>
        ///<remarks>Ermittelt die Lvs-Nr anhand der Mandanten ID.</remarks>
        ///<returns>Returns DataTable</returns>
        public static int GetNEWLvsNr(int myWorkspaceId, int myMandantenId, int myUserId, bool LVSNRUseOneIDRange = true, bool bUpdate = true)
        {
            int iReturn = 0;
            if (myMandantenId > 0)
            {
                string strSql = string.Empty;
                if (bUpdate)
                {
                    strSql = "DECLARE @NewID table( NewAID decimal ); ";

                    if (LVSNRUseOneIDRange)
                    {
                        strSql += "UPDATE PrimeKeys SET " +
                                   "LvsNr =" + PrimeKeyViewData.SQLStringNewLVSNr(myWorkspaceId, myMandantenId, LVSNRUseOneIDRange) +
                                           " OUTPUT INSERTED.LvsNr INTO @NewID; ";
                    }
                    else
                    {
                        strSql += "UPDATE PrimeKeys SET " +
                                   "LvsNr =" + PrimeKeyViewData.SQLStringNewLVSNr(myWorkspaceId, myMandantenId, LVSNRUseOneIDRange) +
                                      " OUTPUT INSERTED.LvsNr INTO @NewID WHERE Mandanten_ID=" + myMandantenId + " AND AbBereichID=" + myWorkspaceId + "; ";
                    }
                    strSql += "SELECT * FROM @NewId; ";
                }
                else
                {

                    if (LVSNRUseOneIDRange)
                    {
                        strSql = "Select MAX(LVS_ID) from PrimeKeys; "; // WHERE Mandanten_ID=" + Mandanten_ID + "; ";
                    }
                    else
                    {
                        strSql = "Select LVS_ID from PrimeKeys WHERE Mandanten_ID=" + myMandantenId + "; ";
                    }
                }
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myUserId);
                int iTmp = 0;
                int.TryParse(strTmp, out iTmp);
                iReturn = iTmp;
            }
            else
            {
                iReturn = 0;
            }
            return iReturn;
        }

        public static string SQLStringNewLVSNr(int myWorkspaceId, int myMandantenId, bool LVSNRUseOneIDRange)
        {
            string strSQLReturn = string.Empty;
            if (LVSNRUseOneIDRange)
            {
                strSQLReturn += "( " +
                                       "CASE " +
                                           "WHEN ((SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID ) IS NULL) THEN 1 " +
                                           "WHEN ((SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID) = 0) THEN 1 " +
                                           "ELSE (SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID )+1 " +
                                         "END " +
                                   ") ";
            }
            else
            {
                strSQLReturn += "( " +
                                   "CASE " +
                                       "WHEN ((SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + myMandantenId + " AND LEingang.AbBereich=" + myWorkspaceId + ") IS NULL) THEN 1 " +
                                       "WHEN ((SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + myMandantenId + " AND LEingang.AbBereich=" + myWorkspaceId + ") = 0) THEN 1 " +
                                   "ELSE (SELECT MAX(Artikel.LVS_ID) FROM Artikel INNER JOIN LEingang ON LEingang.ID=Artikel.LEingangTableID WHERE LEingang.Mandant=" + myMandantenId + " AND LEingang.AbBereich=" + myWorkspaceId + ")+1 " +
                                   "END " +
                               ") ";
            }
            return strSQLReturn;
        }
    }
}

