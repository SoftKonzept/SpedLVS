using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Import;
using LVS;

namespace Import
{
    public class helper_sql_clsArtikel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArtImp"></param>
        /// <returns></returns>
        public static clsArtikel AddArtikel(impArtikel myArtImp)
        {
            clsArtikel myCls = new clsArtikel();
            myCls.InitClass(myArtImp.SysImport.GLUser, myArtImp.SysImport.GLSystem);
            myCls.SetDefaultValueToDefaultProperties(true);
            myCls.AbBereichID = myArtImp.SysImport.AbBereich.ID;
            myCls.MandantenID = myArtImp.SysImport.AbBereich.MandantenID;

            myCls.AuftragID = 0;
            myCls.AuftragPos = 0;
            myCls.LVS_ID = (decimal)myArtImp.art.LNR;
            myCls.BKZ = (int)myArtImp.art.BKZ;
            myCls.gemGewicht = 0;
            myCls.Netto = (decimal)myArtImp.art.NET;
            myCls.Brutto = (decimal)myArtImp.art.BRU;
            myCls.Gut = string.Empty;

            clsGut tmpGut = new clsGut();
            if (myArtImp.ListGut.Count > 0)
            {
                tmpGut = myArtImp.ListGut.FirstOrDefault(x => x.ViewID == myArtImp.art.GTTEXT.ToString().Trim());
                if (tmpGut is null)
                {
                    //Gut muss hinzugefügt werden aus Artikel
                    List<GT> ListGuterartenSource = new List<GT>();
                    ListGuterartenSource = impGueterarten.GetGArtListSource(myArtImp.SysImport);
                    GT tmpGT = new GT();
                    tmpGT = ListGuterartenSource.FirstOrDefault(x => x.NR == myArtImp.art.GT);
                    if ((tmpGT is GT) && (tmpGT.NR > 0))
                    {
                        List<clsADR> ListADR = new List<clsADR>();
                        ListADR.Add(myArtImp.Eingang.AdrAuftraggeber);
                        tmpGut = helper_sql_clsGut.InsertGut(myArtImp.SysImport, tmpGT, ListADR);
                        //helper_sql_clsGut.InsertGutADR(myArtImp.SysImport, tmpGut, myArtImp.Eingang.Auftraggeber);
                    }
                }
            }
            if ((tmpGut is clsGut) && (tmpGut.ID > 0))
            {
                //myCls.GArt = tmpGut;
                myCls.GArtID = tmpGut.ID;
            }
            myCls.Dicke = (decimal)myArtImp.art.DI;
            myCls.Breite = (decimal)myArtImp.art.BR;
            myCls.Laenge = (decimal)myArtImp.art.LAE;
            myCls.Hoehe = 0;
            int iTmp = 0;
            int.TryParse(myArtImp.art.MENGE.ToString().Trim(), out iTmp);
            if (iTmp == 0)
            {
                iTmp = 1;
            }
            myCls.Anzahl = iTmp;
            if (
                (myArtImp.art.EINHEIT.Trim().Equals("Stück")) ||
                (myArtImp.art.EINHEIT.Trim().Equals("Sück")) ||
                (myArtImp.art.EINHEIT.Trim().Equals("ST")) ||
                (myArtImp.art.EINHEIT.Trim().Equals("Stk"))
              )
            {
                myCls.Einheit = "Stück";

            }
            else if (myArtImp.art.EINHEIT is null)
            {
                myCls.Einheit = "KG";
            }
            else
            {
                myCls.Einheit = "KG";
            }
            myCls.Werksnummer = myArtImp.art.WNR.Trim();
            myCls.Produktionsnummer = myArtImp.art.CPNR.Trim();
            myCls.exBezeichnung = string.Empty;
            myCls.Charge = myArtImp.art.VNR.Trim();
            myCls.Bestellnummer = myArtImp.art.KDBEST.Trim();
            myCls.exMaterialnummer = string.Empty;
            myCls.Position = ((int)myArtImp.art.POS).ToString();
            myCls.GutZusatz = string.Empty;
            myCls.EingangChecked = false;
            if ((myArtImp.art.ARTCHECK != null) && (myArtImp.art.ARTCHECK.Equals("Y")))
            {
                myCls.EingangChecked = true;
            }
            myCls.Umbuchung = false;
            if ((myArtImp.art.UB!=null) && (myArtImp.art.UB.Equals("Y")))
            {
                myCls.Umbuchung = true;
            }
            myCls.AusgangChecked = false;
            if ((myArtImp.art.ARTCHECKA!=null) && (myArtImp.art.ARTCHECKA.Equals("Y")))
            {
                myCls.AusgangChecked = true;
            }

            myCls.AbrufReferenz = string.Empty;
            myCls.ArtIDRef = myArtImp.art.INDART.Trim();
            myCls.AuftragPosTableID = 0;
            myCls.LVSNrVorUB = 0;
            myCls.Info = myArtImp.art.INFOORT.Trim();
            myCls.ArtIDAlt = 0;
            if (myArtImp.art.LNR_VORUB > 0)
            {
                decimal decTmp = 0;
                decTmp = helper_sql_clsArtikel.GetArtikelIDByLVSNrAndArbeitsbereich(myArtImp, myCls.LVS_ID);
                myCls.ArtIDAlt = decTmp;                
            }
            myCls.LagerOrt = 0;
            myCls.LagerOrtTable = string.Empty;
            myCls.exLagerOrt = string.Empty;
            myCls.IsLagerArtikel = true;
            myCls.FreigabeAbruf = false;
            myCls.LZZ = Globals.DefaultDateTimeMinValue;
            myCls.Werk = "Werk 1";
            myCls.Halle = myArtImp.art.HALLE.ToString();
            myCls.Reihe = myArtImp.art.REIHE.ToString();
            myCls.Ebene = string.Empty;
            myCls.Platz = myArtImp.art.PREIHE.ToString();
            myCls.exAuftrag = string.Empty;
            myCls.exAuftragPos = string.Empty;
            myCls.interneInfo = "Import EINGANG alt: " + myArtImp.Eingang.EAIDalteLVS;

            myCls.LEingangTableID = myArtImp.Eingang.LEingangTableID;
            myCls.AddArtikelLager(true);
            if (myCls.ID>0)
            {
                helper_sql_clsArtikel.UpdateArtikelAfterImportEA(myArtImp, myCls, true);
            }
            return myCls;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myANr"></param>
        /// <returns></returns>
        public static List<clsArtikel> GetArtikelByANr(string myANr, impArtikel myImpArt)
        {
            List<clsArtikel> retList = new List<clsArtikel>();
            string strSql = string.Empty;
            strSql = "SELECT ID FROM Artikel WHERE EAAusgangAltLVS='" + myANr + "';";
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Ausgänge", "Ausgang", myImpArt.SysImport.BenutzerID);
            foreach (DataRow row in dt.Rows)
            {
                decimal decTmp = 0;
                Decimal.TryParse(row["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsArtikel tmpArt = new clsArtikel();
                    tmpArt.InitClass(myImpArt.SysImport.GLUser, myImpArt.SysImport.GLSystem);
                    tmpArt.ID = decTmp;
                    tmpArt.ExistArtikelTableID();
                    if (!retList.Contains(tmpArt))
                    {
                        retList.Add(tmpArt);
                    }
                    else
                    {
                        string strTmp = string.Empty;
                    }
                }
            }
            return retList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myANr"></param>
        /// <param name="myImpArt"></param>
        /// <param name="myListLVSvorUB"></param>
        /// <returns></returns>
        public static List<clsArtikel> GetArtikelByANrUB(string myANr, impArtikel myImpArt, List<int> myListLVSvorUB)
        {
            List<clsArtikel> retList = new List<clsArtikel>();
            string strSql = string.Empty;
            strSql = "SELECT ID FROM Artikel WHERE EAAusgangAltLVS='" + myANr + "' AND LVS_ID IN ("+string.Join(",", myListLVSvorUB) +");";
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Ausgänge", "Ausgang", myImpArt.SysImport.BenutzerID);
            foreach (DataRow row in dt.Rows)
            {
                decimal decTmp = 0;
                Decimal.TryParse(row["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsArtikel tmpArt = new clsArtikel();
                    tmpArt.InitClass(myImpArt.SysImport.GLUser, myImpArt.SysImport.GLSystem);
                    tmpArt.ID = decTmp;
                    tmpArt.GetArtikeldatenByTableID();
                    tmpArt.ExistArtikelTableID();
                    if (!retList.Contains(tmpArt))
                    {
                        retList.Add(tmpArt);
                    }
                    else
                    {
                        string strTmp = string.Empty;
                    }
                }
            }
            return retList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArtImp"></param>
        /// <returns></returns>
        public static bool UpdateArtikelAfterImportEA(impArtikel myArtImp, clsArtikel myArtikel, bool IsEingang)
        {
            string strSql = string.Empty;
            if (IsEingang)
            {
                strSql = "Update Artikel SET " +
                                " LEingangTableID =" + (int)myArtImp.Eingang.LEingangTableID +
                                " , LAusgangTableID= 0" +
                                " , EAEingangAltLVS='" + myArtImp.art.ENR+ "' "+
                                " , EAAusgangAltLVS= '" + myArtImp.art.ANR + "' " +
                                " , LVSNr_ALTLvs =" + (int)myArtImp.art.LNR +

                                " WHERE ID = " + myArtikel.ID;
            }
            else
            {
                strSql = "Update Artikel SET " +
                                " LAusgangTableID =" + (int)myArtImp.Ausgang.LAusgangTableID +
                                " , EAAusgangAltLVS= '" + myArtImp.Ausgang.EAIDalteLVS + "' " +
                                " WHERE ID = " + myArtikel.ID;
            }

            bool bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "UpdateEingang", myArtImp.SysImport.GLUser.User_ID);
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArtImp"></param>
        /// <param name="myLVSNr"></param>
        /// <param name="myAbBereichId"></param>
        /// <returns></returns>
        public static decimal GetArtikelIDByLVSNrAndArbeitsbereich(impArtikel myArtImp, decimal myLVSNr)
        {
            decimal decTmp = 0;
            if (myLVSNr > 0)
            {
                string strSql = string.Empty;
                strSql = "Select a.ID FROM Artikel a " +
                              "INNER JOIN LEingang b on b.ID=a.LEingangTableID " +
                              " WHERE " +
                                    "a.LVS_ID=" + myLVSNr +
                                    " AND a.LEingangTableID>0 " +
                                    " AND a.AB_ID=" +(int) myArtImp.SysImport.AbBereich.ID + " ;";

                string tmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myArtImp.SysImport.GLUser.User_ID);
                if (tmp != string.Empty)
                {
                    decTmp = Convert.ToDecimal(tmp);
                }
            }
            return decTmp;
        }
    }
}
