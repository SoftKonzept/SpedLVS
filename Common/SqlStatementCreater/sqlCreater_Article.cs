using Common.Enumerations;
using Common.Models;
using System;
using System.Collections.Generic;

namespace Common.SqlStatementCreater
{
    public class sqlCreater_Article
    {
        public const string const_Placeholder_AuftragID = "#AuftragId#";
        public const string const_Placeholder_AuftragPos = "#AuftragPos#";
        public const string const_Placeholder_LVS = "#Lvs#";
        public const string const_Placeholder_LEingangTableID = "#LEingangTableID#";

        private Articles articles;
        public sqlCreater_Article(Articles myArticle)
        {
            articles = myArticle;
        }

        public string sqlInsert
        {
            get
            {
                string sql = string.Empty;
                sql += "INSERT INTO Artikel (";
                sql += "AuftragID, AuftragPos, LVS_ID, Mandanten_ID, AB_ID, BKZ, GArtID, Dicke, Breite, Laenge, Hoehe, Anzahl, Einheit, gemGewicht, Netto, ";
                sql += "Brutto, Werksnummer, Produktionsnummer, exBezeichnung, Charge, Bestellnummer, exMaterialnummer, Position, GutZusatz, CheckArt, ";
                sql += "AbrufRef, TARef, LEingangTableID, LAusgangTableID, ArtIDRef, AuftragPosTableID, ArtIDAlt, Info, LagerOrt, LOTable, exLagerOrt, ";
                sql += "ADRLagerNr, FreigabeAbruf, LZZ, Werk, Halle, Ebene, Reihe, Platz, exAuftrag, exAuftragPos, ASNVerbraucher, UB_AltCalcEinlagerung, ";
                sql += "UB_AltCalcAuslagerung, UB_AltCalcLagergeld, UB_NeuCalcEinlagerung, UB_NeuCalcAuslagerung, UB_NeuCalcLagergeld, IsVerpackt, intInfo, ";
                sql += "exInfo, Guete, IsStackable, GlowDate,IdentifiedByScan, CreatedByScanner";
                sql += ")";
                return sql;
            }
        }

        public string sqlInsertValues
        {
            get
            {
                string sql = string.Empty;
                //sql += " VALUES (";
                sql += const_Placeholder_AuftragID;              //"#AuftragId# ";  //AuftragID
                sql += ", " + const_Placeholder_AuftragPos;      //  ", #AuftragPos#, ";  //AuftragPos
                sql += ", " + const_Placeholder_LVS;             // ", #Lvs# ";  //LVSNR
                sql += "," + articles.MandantenID;
                sql += "," + articles.AbBereichID;
                sql += ",1";              //BKZ
                sql += "," + articles.GArtID;
                sql += ",'" + articles.Dicke.ToString().Replace(",", ".") + "'";
                sql += ",'" + articles.Breite.ToString().Replace(",", ".") + "'";
                sql += ",'" + articles.Laenge.ToString().Replace(",", ".") + "'";
                sql += ",'" + articles.Hoehe.ToString().Replace(",", ".") + "'";
                sql += "," + articles.Anzahl;
                sql += ",'" + articles.Einheit + "'";
                sql += ",'" + articles.gemGewicht.ToString().Replace(",", ".") + "'";
                sql += ",'" + articles.Netto.ToString().Replace(",", ".") + "'";
                sql += ",'" + articles.Brutto.ToString().Replace(",", ".") + "'";
                sql += ",'" + articles.Werksnummer + "'";
                sql += ",'" + articles.Produktionsnummer + "'";
                sql += ",'" + articles.exBezeichnung + "'";
                sql += ",'" + articles.Charge + "'";
                sql += ",'" + articles.Bestellnummer + "'";
                sql += ",'" + articles.exMaterialnummer + "'";
                sql += ",'" + articles.Position + "'";
                sql += ",'" + articles.GutZusatz + "'";
                sql += "," + Convert.ToInt32(articles.EingangChecked);
                sql += ",'" + articles.AbrufReferenz + "'";                       //AbrufRef
                sql += ",'" + articles.TARef + "'";                               //TARef
                sql += ", " + const_Placeholder_LEingangTableID;                  //"," + articles.LEingangTableID;
                sql += "," + articles.LAusgangTableID;                            //LagerausgangTableID              
                sql += ",'" + articles.ArtIDRef + "'";
                sql += "," + articles.AuftragPosTableID;
                sql += "," + articles.ArtIDAlt;
                sql += ",'" + articles.Info + "'";
                sql += "," + articles.LagerOrt;
                sql += ", '" + articles.LagerOrtTable + "'";
                sql += ",'" + articles.exLagerOrt + "'";
                sql += ", " + articles.ADRLagerNr;
                sql += ", " + Convert.ToInt32(articles.FreigabeAbruf);
                sql += ", '" + articles.LZZ + "'";
                sql += ", '" + articles.Werk + "' ";
                sql += ", '" + articles.Halle + "'";
                sql += ", '" + articles.Ebene + "'";
                sql += ", '" + articles.Reihe + "'";
                sql += ", '" + articles.Platz + "'";
                sql += ", '" + articles.exAuftrag + "'";
                sql += ", '" + articles.exAuftragPos + "'";
                sql += ", '" + articles.ASNVerbraucher + "'";
                sql += ", " + Convert.ToInt32(articles.UB_AltCalcEinlagerung);
                sql += ", " + Convert.ToInt32(articles.UB_AltCalcAuslagerung);
                sql += ", " + Convert.ToInt32(articles.UB_AltCalcLagergeld);
                sql += ", " + Convert.ToInt32(articles.UB_NeuCalcEinlagerung);
                sql += ", " + Convert.ToInt32(articles.UB_NeuCalcAuslagerung);
                sql += ", " + Convert.ToInt32(articles.UB_NeuCalcLagergeld);
                sql += ", " + Convert.ToInt32(articles.IsVerpackt);
                sql += ", '" + articles.interneInfo + "'";
                sql += ", '" + articles.externeInfo + "'";
                sql += ", '" + articles.Guete + "'";
                sql += ", " + Convert.ToInt32(articles.IsStackable);
                sql += ", '" + articles.GlowDate + "'";
                sql += ", '" + articles.IdentifiedByScan + "'";
                sql += ", " + Convert.ToInt32(articles.CreatedByScanner);

                //sql += "); ";
                return sql;
            }
        }

        public string sqlAdd
        {
            get
            {
                string sql = string.Empty;
                sql += this.sqlInsert + sqlInsertValues;
                return sql;
            }
        }




        public static string sql_String_Update(Articles art, enumArticleEdit_Steps myArticleEdit_Step)
        {
            string strSql = string.Empty;

            switch (myArticleEdit_Step)
            {
                case enumArticleEdit_Steps.editDimension:

                    strSql += " Einheit='" + art.Einheit + "'";
                    strSql += ", Dicke='" + art.Dicke.ToString().Replace(",", ".") + "'";
                    strSql += ", Breite='" + art.Breite.ToString().Replace(",", ".") + "'";
                    strSql += ", Laenge='" + art.Laenge.ToString().Replace(",", ".") + "'";
                    strSql += ", Hoehe='" + art.Hoehe.ToString().Replace(",", ".") + "'";
                    break;

                case enumArticleEdit_Steps.ediWeight:
                    strSql += " Anzahl=" + art.Anzahl;
                    strSql += ", Netto='" + art.Netto.ToString().Replace(",", ".") + "'";
                    strSql += ", Brutto='" + art.Brutto.ToString().Replace(",", ".") + "'";

                    break;

                case enumArticleEdit_Steps.editReferences:

                    strSql += " exBezeichnung='" + art.exBezeichnung + "'";
                    strSql += ", exMaterialnummer='" + art.exMaterialnummer + "'";
                    strSql += ", GlowDate='" + art.GlowDate + "'";
                    break;

                case enumArticleEdit_Steps.ediCheckStoreIn:

                    strSql += " CheckArt='" + art.EingangChecked + "'";
                    break;

                case enumArticleEdit_Steps.editCheckStoreOut:

                    strSql += " LA_Checked='" + art.AusgangChecked + "'";
                    break;
            }
            string strReturn = string.Empty;
            if (strSql.Length > 0)
            {
                strReturn = "Update Artikel SET ";
                strReturn += strSql;
                strReturn += " WHERE ID=" + art.Id + " ";
            }
            return strReturn;
        }

        public static string sqlString_Article_UpdateIdentifiedByScan(List<Common.Models.Articles> myArticleList)
        {
            List<int> myIdList = new List<int>();
            foreach (var i in myArticleList)
            {
                if (!myIdList.Contains(i.Id))
                {
                    myIdList.Add(i.Id);
                }
            }

            string strSql = string.Empty;
            if (myIdList.Count > 0)
            {
                strSql = "Update Artikel SET ";
                strSql += " IdentifiedByScan='" + DateTime.Now.ToString() + "' ";
                strSql += " WHERE ID IN (" + string.Join(", ", myIdList.ToArray()) + "); ";
            }
            return strSql;
        }
    }
}
