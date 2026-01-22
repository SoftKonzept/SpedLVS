using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Import;
using LVS;

namespace Import
{
    public class helper_sql_clsGut
    {
        public static List<clsGut> GetGutList(clsSystemImport mySysImport)
        {
            List<clsGut>  ListGuterartenSource = new List<clsGut>();
            DataTable dt = clsGut.GetGueterarten(mySysImport.GLUser, mySysImport.AbBereich.ID);
            foreach (DataRow row in dt.Rows)
            {
                decimal decTmp = 0;
                decimal.TryParse(row["ID"].ToString(), out decTmp);
                clsGut Gut = new clsGut();
                Gut.InitClass(mySysImport.GLUser, mySysImport.GLSystem);
                Gut.ID = decTmp;
                Gut.Fill();
                ListGuterartenSource.Add(Gut);
            }
            return ListGuterartenSource;
        }
        /// <summary>
        ///             FORMAT  0,00_x_000
        /// </summary>
        /// <param name="myGT"></param>
        /// <returns></returns>
        public static string CreateViewIDString(GT myGT)
        {
            string strReturn = string.Empty;
            decimal decDi = 0;
            decimal.TryParse(myGT.DI.ToString(), out decDi);
            int iBr = 0;
            int.TryParse(myGT.BR.ToString(), out iBr);
            strReturn = decDi.ToString("0.00").Replace(".", ",") + " x " + iBr.ToString("000");
            return strReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySysIm"></param>
        /// <param name="myGT"></param>
        /// <returns></returns>
        public static clsGut InsertGut(clsSystemImport mySysIm, GT myGT, List<clsADR> myAdrList)
        {
            clsGut gut = new clsGut();
            gut.InitClass(mySysIm.GLUser, mySysIm.GLSystem);
            //gut.ViewID = myGT.NR.ToString();
            gut.ViewID = myGT.TEXT1.ToString();
            //if ((myGT.DI > 0) && (myGT.BR > 0))
            //{
            //    gut.ViewID = helper_sql_clsGut.CreateViewIDString(myGT);
            //}
            //else
            //{
            //    gut.ViewID = myGT.TEXT1.ToString();
            //}
            gut.Bezeichnung = myGT.TEXT1.Trim();
            gut.Besonderheit = string.Empty;
            if (myGT.TEXT2 != null)
            {
                gut.Besonderheit = myGT.TEXT2.Trim();
            }
            decimal decTmp = 0;
            if (myGT.DI != null)
            {
                Decimal.TryParse(myGT.DI.ToString().Trim(), out decTmp);
            }
            gut.Dicke = decTmp;
            decTmp = 0;
            if (myGT.BR != null)
            {
                Decimal.TryParse(myGT.BR.ToString().Trim(), out decTmp);
            }
            gut.Breite = decTmp;
            decTmp = 0;
            if (myGT.LAE != null)
            {
                Decimal.TryParse(myGT.LAE.ToString().Trim(), out decTmp);
            }
            gut.Laenge = decTmp;
            gut.Hoehe = 0;
            decTmp = 0;
            if (myGT.NET != null)
            {
                Decimal.TryParse(myGT.NET.ToString().Trim(), out decTmp);
            }
            gut.Netto = decTmp;
            decTmp = 0;
            if (myGT.BRU != null)
            {
                Decimal.TryParse(myGT.BRU.ToString().Trim(), out decTmp);
            }
            gut.Brutto = decTmp;

            //if (myGT.TEXT1.Contains("Coil"))
            //{
            //    gut.ArtikelArt = "Coils";
            //}
            //else if (
            //            myGT.TEXT1.Contains("Platin") ||
            //            myGT.TEXT1.Contains("Tafeln") ||
            //            myGT.TEXT1.Contains("Blech")
            //        )
            //{
            //    gut.ArtikelArt = "Platinen";
            //}
            //else if (myGT.TEXT1.Contains("Rohr"))
            //{
            //    gut.ArtikelArt = "Rohre";
            //}
            //else if (myGT.TEXT1.Contains("Palette"))
            //{
            //    gut.ArtikelArt = "Paletten";
            //}
            //else
            //{
            //    gut.ArtikelArt = string.Empty;
            //}
            gut.ArtikelArt = myGT.ART.Trim();
            gut.Besonderheit =myGT.TEXT2.Trim() +" ### " + myGT.LIEFERANT.Trim();

            gut.Verpackung = string.Empty;
            if ((myGT.VERPACKUNGSNR != null) && (!myGT.VERPACKUNGSNR.Trim().Equals("LEER")))
            {
                gut.Verpackung = myGT.VERPACKUNGSNR.Trim();
            }

            gut.AbsteckBolzenNr = string.Empty;
            if ((myGT.ABSTBOLZNR != null) && (!myGT.ABSTBOLZNR.Trim().Equals("LEER")))
            {
                gut.AbsteckBolzenNr = myGT.ABSTBOLZNR.Trim();
            }

            gut.MEAbsteckBolzen = myGT.ME_ABSTBOLZ;
            gut.ArbeitsbereichID = mySysIm.AbBereich.ID;
            gut.LieferantenID = 0;

            gut.Aktiv = true;
            if ((myGT.AKTIV != null) && (myGT.AKTIV.Equals("N")))
            {
                gut.Aktiv = false;
            }

            decTmp = 0;
            decimal.TryParse(myGT.MINDEST.ToString(), out decTmp);
            gut.MindestBestand = decTmp;

            //gut.BestellNr = "000001";
            gut.BestellNr = string.Empty;
            if ((myGT.BESTELLNR != null) && (!myGT.BESTELLNR.Trim().Equals("LEER")))
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
            gut.IsStackable = true;
            gut.UseProdNrCheck = true;
            gut.tmpLiefVerweis = myGT.LIEFERANT.Trim();

            gut.Add();

            //Zusweisung Güterart / Adresse
            if (gut.ID > 0)
            {
                clsADR tmpAdr = myAdrList.FirstOrDefault(x=>x.Verweis == gut.tmpLiefVerweis);
                if ((tmpAdr is clsADR) && (tmpAdr.ID > 0) && (tmpAdr.Verweis == gut.tmpLiefVerweis))
                {
                    helper_sql_clsGut.InsertGutADR(mySysIm, gut, tmpAdr.ID);                    
                }
                gut.ViewID = gut.ID.ToString();
                gut.UpdateGueterArt();
            }
            return gut;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySysIm"></param>
        /// <param name="myGT"></param>
        /// <returns></returns>
        public static void InsertGutADR(clsSystemImport mySysIm, clsGut myGut, decimal myAdrId)
        {
            if ((mySysIm is clsSystemImport) && (myGut is clsGut) && (myAdrId > 0))
            {
                clsGueterartADR gAdr = new clsGueterartADR();
                gAdr.InitClass(mySysIm.GLUser, mySysIm.GLSystem);
                gAdr.AbBereichID = mySysIm.GLSystem.sys_ArbeitsbereichID;
                gAdr.AdrID = myAdrId;
                gAdr.GArtID = myGut.ID;
                gAdr.Add();

            }
        }
    }
}
