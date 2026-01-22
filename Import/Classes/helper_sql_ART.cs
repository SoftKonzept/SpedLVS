using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Import;
using Import.Enumerations;
using LVS;


namespace Import
{
    public class helper_sql_ART
    {
        public static string GetSQLFilterE(clsSystemImport mySysImport)
        {
            string strFilter = " (ANR is NULL AND ADAT is NULL) OR " +
                                    " ((ADAT > CAST('" + mySysImport.CalcDateToKeep.Date.ToString("dd.MM.yyyy") + "' as Date)) AND (ANR <> '')) ";

            return strFilter;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySysImport"></param>
        /// <returns></returns>
        public static string GetSQLFilterA(clsSystemImport mySysImport)
        {
            string strFilter = " ((ADAT > CAST('" + mySysImport.CalcDateToKeep.Date.ToString("dd.MM.yyyy") + "' as Date)) AND (ANR <> '')) ";

            return strFilter;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySysImport"></param>
        /// <returns></returns>
        public static List<string> GetENRToImport(clsSystemImport mySysImport)
        {
            List<string> retList = new List<string>();
            string strSql = string.Empty;
            strSql = "SELECT DISTINCT ENR, EDAT " +
                                "FROM art " +
                                    "where " +
                                    helper_sql_ART.GetSQLFilterE(mySysImport) +
                                    " ORDER BY EDAT;";
            DataTable dt = clsSQLconImport.ExecuteSQL_GetDataTable(strSql, mySysImport.GLUser.User_ID, "ENR");
            foreach (DataRow row in dt.Rows)
            {
                string strENR = row["ENR"].ToString().Trim();
                if (!retList.Contains(strENR))
                {
                    retList.Add(strENR);
                }
            }
            return retList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySysImport"></param>
        /// <returns></returns>
        public static List<string> GetENRToImportUB(clsSystemImport mySysImport)
        {
            List<string> retList = new List<string>();
            string strSql = string.Empty;
            strSql = "SELECT DISTINCT ENR, EDAT "+
                                          " FROM ART "+
                                           " WHERE LNR IN(SELECT LNR_VORUB FROM ART where LNR_VORUB > 0 AND BKZ = 1) "+
                                            " ORDER BY EDAT;";
            DataTable dt = clsSQLconImport.ExecuteSQL_GetDataTable(strSql, mySysImport.GLUser.User_ID, "ENR");
            foreach (DataRow row in dt.Rows)
            {
                string strENR = row["ENR"].ToString().Trim();
                if (!retList.Contains(strENR))
                {
                    retList.Add(strENR);
                }
            }
            return retList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySysImport"></param>
        /// <returns></returns>
        public static List<string> GetANRToImportUB(clsSystemImport mySysImport)
        {
            List<string> retList = new List<string>();
            string strSql = string.Empty;
            strSql = "SELECT DISTINCT ANR, ADAT " +
                                          " FROM ART " +
                                           " WHERE LNR IN(SELECT LNR_VORUB FROM ART where LNR_VORUB > 0 AND BKZ = 1) ";
                                            
            DataTable dt = clsSQLconImport.ExecuteSQL_GetDataTable(strSql, mySysImport.GLUser.User_ID, "ANR");
            foreach (DataRow row in dt.Rows)
            {
                string strANR = row["ANR"].ToString().Trim();
                if (!retList.Contains(strANR))
                {
                    retList.Add(strANR);
                }
            }
            return retList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySysImport"></param>
        /// <returns></returns>
        public static List<string> GetANRToImport(clsSystemImport mySysImport)
        {
            List<string> retList = new List<string>();
            string strSql = string.Empty;
            strSql = "SELECT DISTINCT ANR, ADAT " +
                                "FROM art " +
                                    "where " +
                                    helper_sql_ART.GetSQLFilterA(mySysImport) +
                                    " ORDER BY ADAT;";
            DataTable dt = clsSQLconImport.ExecuteSQL_GetDataTable(strSql, mySysImport.GLUser.User_ID, "ENR");
            foreach (DataRow row in dt.Rows)
            {
                string strANR = row["ANR"].ToString().Trim();
                if (!retList.Contains(strANR))
                {
                    retList.Add(strANR);
                }
            }
            return retList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySysImport"></param>
        /// <param name="myEANr"></param>
        /// <returns></returns>
        public static List<ART> GetARTByENR(clsSystemImport mySysImport, string myEANr)
        {
            string strSql = string.Empty;
            strSql = "SELECT * FROM ART  where ENR='" + myEANr + "' AND (" + helper_sql_ART.GetSQLFilterE(mySysImport) + ") ORDER BY LNR ;";
            DataTable dt = clsSQLconImport.ExecuteSQL_GetDataTable(strSql, mySysImport.GLUser.User_ID, "ART");
            List<ART> ListArt = new List<ART>();
            ListArt = helper_sql_ART.FillCls(dt);
            return ListArt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySysImport"></param>
        /// <param name="myEANr"></param>
        /// <returns></returns>
        public static List<ART> GetARTByENRUB(clsSystemImport mySysImport, string myEANr, List<int>myListLVSvorUB)
        {
            string strSql = string.Empty;
            strSql = "SELECT * FROM ART  where ENR='" + myEANr + "' AND LNR IN ("+string.Join(",", myListLVSvorUB) + ");";
            DataTable dt = clsSQLconImport.ExecuteSQL_GetDataTable(strSql, mySysImport.GLUser.User_ID, "ART");
            List<ART> ListArt = new List<ART>();
            ListArt = helper_sql_ART.FillCls(dt);
            return ListArt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<int> GetLNRVorUBList(clsSystemImport mySysImport)
        {
            string strSql = string.Empty;
            strSql = "SELECT LNR_VORUB FROM ART where LNR_VORUB> 0 AND BKZ = 1;";
            DataTable dt = clsSQLconImport.ExecuteSQL_GetDataTable(strSql, mySysImport.GLUser.User_ID, "ART");
            List<int> ListArt = new List<int>();
            foreach (DataRow row in dt.Rows)
            {
                int iTmp = 0;
                int.TryParse(row["LNR_VORUB"].ToString().Trim(), out iTmp);
                if (!ListArt.Contains(iTmp))
                {
                    ListArt.Add(iTmp);
                }
            }
            return ListArt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<ART> FillCls(DataTable dt)
        {
            List<ART> ListArt = new List<ART>();
            foreach (DataRow row in dt.Rows)
            {
                //---EA Daten
                ART a = new ART();
                int iTmp = 0;
                int.TryParse(row["BKZ"].ToString().Trim(), out iTmp);
                a.BKZ = iTmp;
                double dTmp = 0;
                double.TryParse(row["NET"].ToString().Trim(), out dTmp);
                a.NET = dTmp;
                dTmp = 0;
                double.TryParse(row["BRU"].ToString().Trim(), out dTmp);
                a.BRU = dTmp;
                dTmp = 0;
                double.TryParse(row["DI"].ToString().Trim(), out dTmp);
                a.DI = dTmp;
                dTmp = 0;
                double.TryParse(row["BR"].ToString().Trim(), out dTmp);
                a.BR = dTmp;
                dTmp = 0;
                double.TryParse(row["LAE"].ToString().Trim(), out dTmp);
                a.LAE = dTmp;
                iTmp = 0;
                int.TryParse(row["GT"].ToString().Trim(), out iTmp);
                a.GT = iTmp;
                a.GTTEXT = row["GTTEXT"].ToString().Trim();
                dTmp = 0;
                double.TryParse(row["MENGE"].ToString().Trim(), out dTmp);
                a.MENGE = dTmp;
                a.EINHEIT = row["EINHEIT"].ToString().Trim();
                a.WNR = row["WNR"].ToString().Trim();
                a.CPNR = row["CPNR"].ToString().Trim();
                a.VNR = row["VNR"].ToString().Trim();
                a.KDBEST = row["KDBEST"].ToString().Trim();
                iTmp = 0;
                int.TryParse(row["POS"].ToString().Trim(), out iTmp);
                a.POS = iTmp;
                a.ARTCHECK = row["ARTCHECK"].ToString().Trim();
                iTmp = 0;
                int.TryParse(row["STORNO"].ToString().Trim(), out iTmp);
                a.STORNO = iTmp;
                a.INDART = row["INDART"].ToString().Trim();
                a.ARTCHECKA = row["ARTCHECKA"].ToString().Trim();
                iTmp = 0;
                int.TryParse(row["LNR_VORUB"].ToString().Trim(), out iTmp);
                a.LNR_VORUB = iTmp;
                a.INFOORT = row["INFOORT"].ToString().Trim();
                a.ENR = row["ENR"].ToString().Trim();
                a.ANR = string.Empty;
                if (!row.IsNull("ANR"))
                {
                    a.ANR = row["ANR"].ToString().Trim();
                }
                iTmp = 0;
                int.TryParse(row["LNR"].ToString().Trim(), out iTmp);
                a.LNR = iTmp;
                iTmp = 0;
                int.TryParse(row["HALLE"].ToString().Trim(), out iTmp);
                a.HALLE = iTmp;
                iTmp = 0;
                int.TryParse(row["PREIHE"].ToString().Trim(), out iTmp);
                a.PREIHE = iTmp;
                iTmp = 0;
                int.TryParse(row["REIHE"].ToString().Trim(), out iTmp);
                a.REIHE = iTmp;
                a.BEM = row["BEM"].ToString().Trim();
                a.ABLADESTELLE = row["ABLADESTELLE"].ToString().Trim();
                DateTime dtTmp = Globals.DefaultDateTimeMinValue;
                DateTime.TryParse(row["EDAT"].ToString(), out dtTmp);
                a.EDAT = dtTmp;

                if (row["ADAT"] != null)
                {
                    dtTmp = Globals.DefaultDateTimeMinValue;
                    DateTime.TryParse(row["ADAT"].ToString(), out dtTmp);
                    a.ADAT = dtTmp;
                }
                a.SCHADEN = row["SCHADEN"].ToString().Trim();
                a.SPL = row["SPL"].ToString().Trim();

                ListArt.Add(a);
            }
            return ListArt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool UpdateArtADRDaten(Globals._GL_USER myGLUser, Enumerations.enumADRArt myAdrArt, string myAdrViewIDNew, string myAdrViewIDOld)
        {
            bool bReturn = false;
            string strSql = string.Empty;

            switch(myAdrArt)
            {
                case enumADRArt.Auftraggeber:
                    strSql = "Update ART SET KDNR='" + myAdrViewIDNew + "' WHERE LTRIM(RTRIM(KDNR))='"+ myAdrViewIDOld + "';";
                    break;

                case enumADRArt.Empfänger:
                    strSql = "Update ART SET EMNR='" + myAdrViewIDNew + "' WHERE LTRIM(RTRIM(EMNR))='" + myAdrViewIDOld + "';";
                    break;
            }
            bReturn = clsSQLconImport. ExecuteSQLWithTRANSACTION(strSql,"UpdateArt", myGLUser.User_ID);
            return bReturn;
        }

    }
}
