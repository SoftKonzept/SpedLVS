using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Import;
using LVS;

namespace Import
{
    public class impSchaden
    {
        internal clsSystemImport SysImport;
        internal GT Gueterart;
        public List<GT> ListGuterartenSource;
        public List<clsGut> ListGueterenDest;
        public List<string> ListLog;


        public delegate void SetProzessInfo();
        public event EventHandler SetProzessInfoEventHandler;

        public impSchaden(clsSystemImport mySys)
        {
            this.SysImport = mySys;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DoImport()
        {
            //string strTmp = string.Empty;
            bool bReturn = false;
            //ListLog = new List<string>();
            ////--- Source Bereinigen (TEXT1=null)
            CleanUpSource();
            ////--- Source Güterarten ermitteln
            //FillGArtListSource();

            //if (this.ListGuterartenSource.Count > 0)
            //{
            //    strTmp = string.Empty;
            //    strTmp = helper_LogStringCreater.CreateString("-------- Güterarten");
            //    ListLog.Add(strTmp);

            //    //--- Bestehende Güterarten ermitteln
            //    ListGueterenDest = helper_sql_clsGut.GetGutList(this.SysImport);
            //    int iCount = 0;
            //    foreach (GT item in this.ListGuterartenSource)
            //    {
            //        iCount++;
            //        strTmp = string.Empty;
            //        strTmp = helper_LogStringCreater.CreateString("["+iCount.ToString("00#") +"/"+this.ListGuterartenSource.Count.ToString("00#") +"] - ");

            //        if (this.ListGueterenDest.Count > 0)
            //        {
            //            //Check ob schon vorhanden
            //            //if (!clsGut.ViewIDExists(this.SysImport.GLUser, item.NR.ToString()))
            //            if (!this.ListGueterenDest.Any(x => x.ViewID == item.NR.ToString().Trim()))
            //            {
            //                InsertDestination(item);
            //                strTmp += "[add]   -> " + item.NR.ToString() + " - " + item.TEXT1;
            //                ListLog.Add(strTmp);
            //            }
            //            else
            //            {
            //                //strTmp += "[exist] -> "+ item.NR.ToString() + " - " + item.TEXT1;
            //            }
            //        }
            //        else
            //        {
            //            //Check ob schon vorhanden
            //            //if (!clsGut.ViewIDExists(this.SysImport.GLUser, item.TEXT1))
            //            if (!this.ListGueterenDest.Any(x => x.ViewID == item.NR.ToString().Trim()))
            //            {
            //                InsertDestination(item);
            //                strTmp += "[add]   -> " + item.NR.ToString() + " - " + item.TEXT1;
            //                ListLog.Add(strTmp);
            //            }
            //            else
            //            {
            //                //strTmp += "[exist] -> "+ item.NR.ToString() + " - " + item.TEXT1;
            //            }
            //        }

            //    }
            //}
            //else
            //{
            //    strTmp = string.Empty;
            //    strTmp = helper_LogStringCreater.CreateString("GT Source Anzahl: 0 !!!");
            //    ListLog.Add(strTmp);
            //}
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGT"></param>
        private void InsertDestination(GT myGT)
        {
            clsGut gut = new clsGut();
            gut.InitClass(this.SysImport.GLUser, this.SysImport.GLSystem);
            gut.ViewID = myGT.NR.ToString();
            gut.Bezeichnung = myGT.TEXT1.Trim();
            gut.Besonderheit = string.Empty;
            if (myGT.TEXT2 != null)
            {
                gut.Besonderheit = myGT.TEXT2.Trim();
            }
            gut.Dicke = 0;
            gut.Breite = 0;
            gut.Laenge = 0;
            gut.Hoehe = 0;
            gut.Netto = 0;
            gut.Brutto = 0;

            if (myGT.TEXT1.Contains("Coil"))
            {
                gut.ArtikelArt = "Coils";
            }
            else if (
                        myGT.TEXT1.Contains("Platin") ||
                        myGT.TEXT1.Contains("Tafeln") ||
                        myGT.TEXT1.Contains("Blech")
                    )
            {
                gut.ArtikelArt = "Platinen";
            }
            else if (myGT.TEXT1.Contains("Rohr"))
            {
                gut.ArtikelArt = "Rohre";
            }
            else if (myGT.TEXT1.Contains("Palette"))
            {
                gut.ArtikelArt = "Paletten";
            }
            else
            {
                gut.ArtikelArt = string.Empty;
            }
            gut.Besonderheit = string.Empty;

            gut.Verpackung = string.Empty;
            if (myGT.VERPACKUNGSNR != null)
            {
                gut.Verpackung = myGT.VERPACKUNGSNR.Trim();
            }
           
            gut.AbsteckBolzenNr = string.Empty;
            if (myGT.ABSTBOLZNR != null)
            {
                gut.AbsteckBolzenNr = myGT.ABSTBOLZNR.Trim();
            }

            gut.MEAbsteckBolzen = myGT.ME_ABSTBOLZ;
            gut.ArbeitsbereichID = this.SysImport.AbBereich.ID;
            gut.LieferantenID = 0;

            gut.Aktiv = true;
            if ((myGT.AKTIV != null) && (myGT.AKTIV.Equals("N")))
            {
                gut.Aktiv = false;
            }

            decimal decTmp = 0;
            decimal.TryParse(myGT.MINDEST.ToString(), out decTmp);
            gut.MindestBestand = decTmp;

            gut.BestellNr = "000001";
            if ((myGT.BESTELLNR != null) && (!myGT.BESTELLNR.Trim().Equals(string.Empty)))
            {
                gut.BestellNr = myGT.BESTELLNR.Trim();
            }

            gut.Zusatz = string.Empty;
            if (myGT.TEXT1.Contains("Coil"))
            {
                gut.Einheit = "kg";
            }
            else if (
                        myGT.TEXT1.Contains("Platin") ||
                        myGT.TEXT1.Contains("Tafeln") ||
                        myGT.TEXT1.Contains("Blech") ||
                        myGT.TEXT1.Contains("MENGE")
                    )
            {
                gut.Einheit = "Stück";
            }
            else
            {
                gut.Einheit = myGT.EINHEIT.Trim();
                if (gut.Einheit.Equals(string.Empty))
                {
                    gut.Einheit = "kg";
                }
            }
            gut.Verweis = myGT.VERWEIS.Trim();
            gut.Werksnummer = myGT.VERWEIS.Trim();
            gut.IsStackable = false;
            gut.UseProdNrCheck = true;

            gut.Add();
        }

        /// <summary>
        ///             SOURCE - Güterarten
        /// </summary>
        private void FillSchadensListSource()
        {
            //ListGuterartenSource = new List<GT>();
            //string strSql = string.Empty;
            //strSql = "SELECT * FROM SCH ;";
            //DataTable dt = clsSQLconImport.ExecuteSQL_GetDataTable(strSql, this.SysImport.GLUser.User_ID, "GT");
            //foreach (DataRow row in dt.Rows)
            //{
            //    SCH tmpGT = new GT();
            //    tmpGT = FillGTCls(row);

            //    if (

            //        (tmpGT.TEXT1 != null) &&
            //        (!tmpGT.TEXT1.Equals(string.Empty))
            //      )
            //    {
            //        ListGuterartenSource.Add(tmpGT);
            //    }
            //}
        }
        /// <summary>
        ///             SOURCE - Güterarten
        /// </summary>
        /// <param name="myRow"></param>
        /// <returns></returns>
        private GT FillGTCls(DataRow myRow)
        {
            GT tmpGT = new GT();
            int iTmp = 0;
            int.TryParse(myRow["NR"].ToString(), out iTmp);
            tmpGT.NR = iTmp;
            tmpGT.TEXT1 = myRow["TEXT1"].ToString().Trim();
            tmpGT.EINHEIT = myRow["EINHEIT"].ToString().Trim();
            tmpGT.VERWEIS = myRow["VERWEIS"].ToString().Trim();

            return tmpGT;
        }
        /// <summary>
        ///             DELETE WHERE SCHTEXT = null
        /// </summary>
        private void CleanUpSource()
        {
            string strSql = "Delete SCH WHERE SCHTEXT IS NULL;";
            clsSQLconImport.ExecuteSQLWithTRANSACTION(strSql, "CleanUpGT", this.SysImport.GLUser.User_ID);           
        }

    }
}
