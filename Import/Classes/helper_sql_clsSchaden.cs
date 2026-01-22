using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Import;
using LVS;

namespace Import
{
    public class helper_sql_clsSchaden
    {
        public static void AddSchadenL(impArtikel myArtImport)
        {
            if (myArtImport.Artikel.LVS_ID.Equals(181021))
            {
                string str = string.Empty;
            }

            clsSchaeden tSch = myArtImport.ListSchaden.FirstOrDefault(x => x.Bezeichnung == myArtImport.art.SCHADEN.Trim());
            if ((tSch is clsSchaeden) && (tSch.ID > 0))
            {
                string strSql = " INSERT INTO SchadenZuweisung (ArtikelID, SchadenID, UserID, Datum) " +
                                        "VALUES (" + (int)myArtImport.Artikel.ID +
                                                    "," + (int)tSch.ID +
                                                    "," + (int)tSch.BenutzerID +
                                                    ",'" + DateTime.Now + "'" +
                                                    "); ";
                bool bok = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "SchadenAdd", myArtImport.SysImport.GLUser.User_ID);
                if (bok)
                {
                    clsArtikelVita.ArtikelAddSchaden(myArtImport.SysImport.GLUser.User_ID, myArtImport.Artikel.ID, myArtImport.Eingang.LEingangID, myArtImport.Artikel.LVS_ID, tSch.Bezeichnung);

                    string strTmp = string.Empty;
                    strTmp = helper_LogStringCreater.CreateString("[add Schaden]-> EINGANG >>> Artikel/Schaden [" + myArtImport.Artikel.LVS_ID.ToString() + "] wurde zugewiesen");
                    myArtImport.ListLog.Add(strTmp);
                }
            }
            else
            {
                string strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("[Error Schaden]-> Eingang >>> Artikel/Schaden [" + myArtImport.art.SCHADEN.Trim() + "] fehlt !!!!");
                myArtImport.ListLog.Add(strTmp);
            }
        }

        public static void AddSchadenLByCls(clsSchaeden mySchaden, impArtikel myArtImport)
        {
            if ((mySchaden is clsSchaeden) && (mySchaden.ID > 0))
            {
                string strSql = " INSERT INTO SchadenZuweisung (ArtikelID, SchadenID, UserID, Datum) " +
                                        "VALUES (" + (int)myArtImport.Artikel.ID +
                                                    "," + (int)mySchaden.ID +
                                                    "," + (int)mySchaden.BenutzerID +
                                                    ",'" + DateTime.Now + "'" +
                                                    "); ";
                bool bok = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "SchadenAdd", myArtImport.SysImport.GLUser.User_ID);
                if (bok)
                {
                    clsArtikelVita.ArtikelAddSchaden(myArtImport.SysImport.GLUser.User_ID, myArtImport.Artikel.ID, myArtImport.Eingang.LEingangID, myArtImport.Artikel.LVS_ID, mySchaden.Bezeichnung);

                    string strTmp = string.Empty;
                    strTmp = helper_LogStringCreater.CreateString("[add Schaden]-> EINGANG >>> Artikel/Schaden [" + myArtImport.Artikel.LVS_ID.ToString() + "] wurde zugewiesen");
                    myArtImport.ListLog.Add(strTmp);
                }
            }
            else
            {
                string strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("[Error Schaden]-> Eingang >>> Artikel/Schaden [" + myArtImport.art.SCHADEN.Trim() + "] fehlt !!!!");
                myArtImport.ListLog.Add(strTmp);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArtImport"></param>
        /// <returns></returns>
        public static List<clsSchaeden> GetList(impArtikel myArtImport)
        {
            List<clsSchaeden> retList = new List<clsSchaeden>();
            DataTable dt= clsSchaeden.GetSchaeden(myArtImport.SysImport.GLUser, clsSchaeden.const_Art_SchadenUndMängel);
            foreach (DataRow row in dt.Rows)
            {
                decimal decTmp = 0;
                decimal.TryParse(row["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsSchaeden tmpSch = new clsSchaeden();
                    tmpSch._GL_User = myArtImport.SysImport.GLUser;
                    tmpSch.ID = decTmp;
                    tmpSch.FillByID();
                    retList.Add(tmpSch);
                }
            }
            return retList;
        }
    }
}
