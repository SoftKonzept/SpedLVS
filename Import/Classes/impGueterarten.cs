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
    public class impGueterarten
    {
        internal clsSystemImport SysImport;
        internal GT Gueterart;
        public List<GT> ListGuterartenSource;
        public List<clsGut> ListGueterenDest;
        public List<string> ListLog;


        public delegate void SetProzessInfo();
        public event EventHandler SetProzessInfoEventHandler;

        public impGueterarten(clsSystemImport mySys)
        {
            this.SysImport = mySys;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DoImport()
        {
            string strTmp = string.Empty;
            bool bReturn = false;
            ListLog = new List<string>();
            if (!this.SysImport.Import_GutOnlyIsUsed)
            {
                //--- Source Bereinigen (TEXT1=null)
                CleanUpSource();
                //--- Source Güterarten ermitteln
                ListGuterartenSource = impGueterarten.GetGArtListSource(this.SysImport);

                if (this.ListGuterartenSource.Count > 0)
                {
                    strTmp = string.Empty;
                    strTmp = helper_LogStringCreater.CreateString("-------- Güterarten");
                    ListLog.Add(strTmp);

                    //--- Bestehende Güterarten ermitteln
                    ListGueterenDest = helper_sql_clsGut.GetGutList(this.SysImport);
                    List<clsADR> ListADR = (helper_sql_clsADR.GetAdrList(this.SysImport)).Where(x => x.Verweis != null && x.Verweis != string.Empty).ToList();

                    int iCount = 0;
                    foreach (GT item in this.ListGuterartenSource)
                    {
                        iCount++;
                        strTmp = string.Empty;
                        strTmp = helper_LogStringCreater.CreateString("[" + iCount.ToString("00#") + "/" + this.ListGuterartenSource.Count.ToString("00#") + "] - ");

                        clsGut AddGut = new clsGut();

                        if (!this.SysImport.Import_GutOnlyIsUsed)
                        {
                            ////InsertDestination(item);
                            //helper_sql_clsGut.InsertGut(this.SysImport, item, ListADR);
                            //strTmp += "[add]   -> " + item.NR.ToString() + " - " + item.TEXT1;
                            //ListLog.Add(strTmp);
                        }
                        else
                        {
                            if (this.ListGueterenDest.Count > 0)
                            {
                                //AddGut = this.ListGueterenDest.FirstOrDefault(x => x.ViewID == item.NR.ToString().Trim());
                                AddGut = this.ListGueterenDest.FirstOrDefault(x => x.Bezeichnung == item.TEXT1.ToString().Trim());
                                if ((AddGut is clsGut) && (AddGut.ID > 0))
                                {
                                    clsGut.AddGutArbeitsbereich(AddGut, this.SysImport.AbBereich.ID, AddGut.GLUser.User_ID);
                                    strTmp += "[exist] -> " + item.NR.ToString() + " - " + item.TEXT1;
                                }
                                else
                                {
                                    //InsertDestination(item);
                                    helper_sql_clsGut.InsertGut(this.SysImport, item, ListADR);
                                    strTmp += "[add]   -> " + item.NR.ToString() + " - " + item.TEXT1;
                                    ListLog.Add(strTmp);
                                }
                            }
                            else
                            {
                                AddGut = this.ListGueterenDest.FirstOrDefault(x => x.ViewID == item.NR.ToString().Trim());
                                if ((AddGut is clsGut) && (AddGut.ID > 0))
                                {
                                    //InsertDestination(item);
                                    helper_sql_clsGut.InsertGut(this.SysImport, item, ListADR);
                                    strTmp += "[add]   -> " + item.NR.ToString() + " - " + item.TEXT1;
                                    ListLog.Add(strTmp);
                                }
                            }
                        }

                    }
                }
                else
                {
                    strTmp = string.Empty;
                    strTmp = helper_LogStringCreater.CreateString("GT Source Anzahl: 0 !!!");
                    ListLog.Add(strTmp);
                }
            }
           return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGT"></param>
        //private void InsertDestination(GT myGT)
        //{
        //    clsGut gut = new clsGut();
        //    gut.InitClass(this.SysImport.GLUser, this.SysImport.GLSystem);
        //    //gut.ViewID = myGT.NR.ToString();
        //    gut.ViewID = myGT.TEXT1.ToString();
        //    gut.Bezeichnung = myGT.TEXT1.Trim();
        //    gut.Besonderheit = string.Empty;
        //    if (myGT.TEXT2 != null)
        //    {
        //        gut.Besonderheit = myGT.TEXT2.Trim();
        //    }
        //    decimal decTmp = 0;            
        //    if (myGT.DI != null)
        //    {
        //        Decimal.TryParse(myGT.DI.ToString().Trim(), out decTmp);
        //    }
        //    gut.Dicke = decTmp;
        //    decTmp = 0;
        //    if (myGT.BR != null)
        //    {
        //        Decimal.TryParse(myGT.BR.ToString().Trim(), out decTmp);
        //    }
        //    gut.Breite = decTmp;
        //    decTmp = 0;
        //    if (myGT.LAE != null)
        //    {
        //        Decimal.TryParse(myGT.LAE.ToString().Trim(), out decTmp);
        //    }
        //    gut.Laenge = decTmp;
        //    gut.Hoehe = 0;
        //    decTmp = 0;
        //    if (myGT.NET != null)
        //    {
        //        Decimal.TryParse(myGT.NET.ToString().Trim(), out decTmp);
        //    }
        //    gut.Netto = decTmp;
        //    decTmp = 0;
        //    if (myGT.BRU != null)
        //    {
        //        Decimal.TryParse(myGT.BRU.ToString().Trim(), out decTmp);
        //    }
        //    gut.Brutto = decTmp;

        //    if (myGT.TEXT1.Contains("Coil"))
        //    {
        //        gut.ArtikelArt = "Coils";
        //    }
        //    else if (
        //                myGT.TEXT1.Contains("Platin") ||
        //                myGT.TEXT1.Contains("Tafeln") ||
        //                myGT.TEXT1.Contains("Blech")
        //            )
        //    {
        //        gut.ArtikelArt = "Platinen";
        //    }
        //    else if (myGT.TEXT1.Contains("Rohr"))
        //    {
        //        gut.ArtikelArt = "Rohre";
        //    }
        //    else if (myGT.TEXT1.Contains("Palette"))
        //    {
        //        gut.ArtikelArt = "Paletten";
        //    }
        //    else
        //    {
        //        gut.ArtikelArt = string.Empty;
        //    }
        //    gut.Besonderheit = string.Empty;

        //    gut.Verpackung = string.Empty;
        //    if ((myGT.VERPACKUNGSNR != null) && (!myGT.VERPACKUNGSNR.Trim().Equals("LEER")))
        //    {
        //        gut.Verpackung = myGT.VERPACKUNGSNR.Trim();
        //    }

        //    gut.AbsteckBolzenNr = string.Empty;
        //    if ((myGT.ABSTBOLZNR != null) && (!myGT.ABSTBOLZNR.Trim().Equals("LEER")))
        //    {
        //        gut.AbsteckBolzenNr = myGT.ABSTBOLZNR.Trim();
        //    }

        //    gut.MEAbsteckBolzen = myGT.ME_ABSTBOLZ;
        //    gut.ArbeitsbereichID = this.SysImport.AbBereich.ID;
        //    gut.LieferantenID = 0;

        //    gut.Aktiv = true;
        //    if ((myGT.AKTIV != null) && (myGT.AKTIV.Equals("N")))
        //    {
        //        gut.Aktiv = false;
        //    }

        //    decTmp = 0;
        //    decimal.TryParse(myGT.MINDEST.ToString(), out decTmp);
        //    gut.MindestBestand = decTmp;

        //    //gut.BestellNr = "000001";
        //    gut.BestellNr = string.Empty;
        //    if ((myGT.BESTELLNR != null) && (!myGT.BESTELLNR.Trim().Equals("LEER")))
        //    {
        //        gut.BestellNr = myGT.BESTELLNR.Trim();
        //    }

        //    gut.Zusatz = string.Empty;
        //    if (myGT.TEXT1.Contains("Coil"))
        //    {
        //        gut.Einheit = "kg";
        //    }
        //    else if (
        //                myGT.TEXT1.Contains("Platin") ||
        //                myGT.TEXT1.Contains("Tafeln") ||
        //                myGT.TEXT1.Contains("Blech") ||
        //                myGT.TEXT1.Contains("MENGE")
        //            )
        //    {
        //        gut.Einheit = "Stück";
        //    }
        //    else
        //    {
        //        gut.Einheit = myGT.EINHEIT.Trim();
        //        if (gut.Einheit.Equals(string.Empty))
        //        {
        //            gut.Einheit = "kg";
        //        }
        //    }
        //    gut.Verweis = myGT.VERWEIS.Trim();
        //    gut.Werksnummer = myGT.VERWEIS.Trim();
        //    gut.IsStackable = false;
        //    gut.UseProdNrCheck = true;

        //    gut.Add();
        //}


        ///
        ///
        ///
        public static List<GT> GetGArtListSource(clsSystemImport mySysImp)
        {
            List<GT> ReturnList = new List<GT>();
            string strSql = string.Empty;
            strSql = "SELECT * FROM GT ;";
            DataTable dt = clsSQLconImport.ExecuteSQL_GetDataTable(strSql, mySysImp.GLUser.User_ID, "GT");
            foreach (DataRow row in dt.Rows)
            {
                GT tmpGT = new GT();
                tmpGT = impGueterarten.FillGTCls(row);

                if (

                    (tmpGT.TEXT1 != null) &&
                    (!tmpGT.TEXT1.Equals(string.Empty))
                  )
                {
                    ReturnList.Add(tmpGT);
                }
            }
            return ReturnList;
        }
        ///// <summary>
        /////             SOURCE - Güterarten
        ///// </summary>
        //private void FillGArtListSource()
        //{
        //    ListGuterartenSource = new List<GT>();
        //    string strSql = string.Empty;
        //    strSql = "SELECT * FROM GT ;";
        //    DataTable dt = clsSQLconImport.ExecuteSQL_GetDataTable(strSql, this.SysImport.GLUser.User_ID, "GT");
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        GT tmpGT = new GT();
        //        tmpGT = FillGTCls(row);

        //        if (

        //            (tmpGT.TEXT1 != null) &&
        //            (!tmpGT.TEXT1.Equals(string.Empty))
        //          )
        //        {
        //            ListGuterartenSource.Add(tmpGT);
        //        }
        //    }
        //}
        /// <summary>
        ///             SOURCE - Güterarten
        /// </summary>
        /// <param name="myRow"></param>
        /// <returns></returns>
        public static GT FillGTCls(DataRow myRow)
        {
            GT tmpGT = new GT();
            int iTmp = 0;
            int.TryParse(myRow["NR"].ToString(), out iTmp);
            tmpGT.NR = iTmp;
            tmpGT.TEXT1 = myRow["TEXT1"].ToString().Trim();
            tmpGT.TEXT2 = myRow["TEXT2"].ToString().Trim();
            tmpGT.EINHEIT = myRow["EINHEIT"].ToString().Trim();
            tmpGT.VERWEIS = myRow["VERWEIS"].ToString().Trim();
            double dbTmp = 0;
            Double.TryParse(myRow["BRU"].ToString().Trim(), out dbTmp);
            tmpGT.BRU = dbTmp;
            dbTmp = 0;
            Double.TryParse(myRow["NET"].ToString().Trim(), out dbTmp);
            tmpGT.NET = dbTmp;
            dbTmp = 0;
            Double.TryParse(myRow["DI"].ToString().Trim(), out dbTmp);
            tmpGT.DI = dbTmp;
            dbTmp = 0;
            Double.TryParse(myRow["BR"].ToString().Trim(), out dbTmp);
            tmpGT.BR = dbTmp;
            dbTmp = 0;
            Double.TryParse(myRow["LAE"].ToString().Trim(), out dbTmp);
            tmpGT.LAE = dbTmp;
            tmpGT.AKTIV = myRow["AKTIV"].ToString().Trim();
            tmpGT.VERPACKUNGSNR = myRow["VERPACKUNGSNR"].ToString().Trim();
            tmpGT.ABSTBOLZNR = myRow["ABSTBOLZNR"].ToString().Trim();
            iTmp = 0;
            int.TryParse(myRow["ME_ABSTBOLZ"].ToString().Trim(), out iTmp);
            tmpGT.ME_ABSTBOLZ = iTmp;
            tmpGT.BESTELLNR = myRow["BESTELLNR"].ToString().Trim();
            tmpGT.ART = myRow["ART"].ToString().Trim();
            tmpGT.LIEFERANT = myRow["LIEFERANT"].ToString().Trim();
            return tmpGT;
        }
        /// <summary>
        ///             DELETE WHERE TEXT1 = null
        /// </summary>
        private void CleanUpSource()
        {
            string strSql = "Delete [GT]   WHERE TEXT1 IS NULL;";
            clsSQLconImport.ExecuteSQLWithTRANSACTION(strSql, "CleanUpGT", this.SysImport.GLUser.User_ID);           
        }

    }
}
