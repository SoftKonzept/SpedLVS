using Common.Models;
using LVS.ASN.ASNFormatFunctions;
using LVS.Communicator.EdiVDA.EdiVDAValues;
using System.Collections.Generic;

namespace LVS
{
    public class clsASNFormatFunctions
    {
        public Articles article = new Articles();
        public clsArtikel articleCls = new clsArtikel();

        public const string const_Function_Diff1000 = Div1000.const_Diff1000; // falsch geschrieben
        public const string const_Function_Div1000 = Div1000.const_Div1000; // "#func_Diff1000#";  //  div. den Wert durch 1000 - Korrektur
        public const string const_Function_EinheitAssignment = Format_Einheit.const_Function_EinheitAssignment; // "#func_EinheitAss#";  //  

        public const string const_Function_AnzahlCheckforEinheit = Format_Einheit.const_Function_AnzahlCheckforEinheit; // "#func_CheckAnzahlEinheit#";  //  div. den Wert durch 1000
        public const string const_Function_CheckGArtSetAnzahl = Format_Anzahl.const_Function_CheckGArtSetAnzahl; // "#func_CheckGArtSetAnzahl#";  // ermittel anhand der Gart die Einheit und Anzahl
        public const string const_Function_CheckGArtSetEinheit = Format_Einheit.const_Function_CheckGArtSetEinheit; // "#func_CheckGArtSetEinheit#";  // ermittel anhand der Gart die Einheit und Anzahl
        public const string const_Function_Cut0ValueLeft = Cut0ValueLeft.const_Cut0ValueLeft; // "#func_Cut0ValueLeft#";  // schneidet führende 0 ab
        public const string const_Function_DeleteHyphen = DeleteHyphen.const_DeleteHyphen; //"#func_DeleteHyphen";
        public const string const_Function_DateFromEDI = Format_GlowDateFromEDI.const_Function_GlowDateFromEDI; // "#func_DateFromEDI#";
        public const string const_Function_DateFromEDI_ddMMyyyy = Format_GlowDateFromEDI.const_Function_GlowDateFromEDI_ddMMyyyy; // "#func_DateFromEDI#";

        //public const string const_Function_GlowDateToEdi = Format_GlowDateToEdi.const_Function_GlowDateToEdi;
        //public const string const_Function_GlowDateToEdi_yyyyMMdd = Format_GlowDateToEdi.const_Function_GlowDateToEdi_yyyyMMdd;
        //public const string const_Function_GlowDateToEdi_yyyyMMddOrBlank = Format_GlowDateToEdi.const_Function_GlowDateToEdi_yyyyMMddOrBlank;
        //public const string const_Function_GlowDateToEdi_yyMMdd = Format_GlowDateToEdi.const_Function_GlowDateToEdi_yyMMdd;
        //public const string const_Function_GlowDateToEdi_ddMMyyyy = Format_GlowDateToEdi.const_Function_GlowDateToEdi_ddMMyyyy;
        //public const string const_Function_GlowDateToEdi_ddMMyy = Format_GlowDateToEdi.const_Function_GlowDateToEdi_ddMMyy;

        public const string const_Function_WerksnummerWithHyphen = WerksnummerWithHyphen.const_WerksnummerWithHyphen;
        public const string const_Function_WerksnummerWithOutHyphen = WerksnummerWithOutHyphen.const_WerksnummerWithOutHyphen;

        public const string const_Function_FillTo9With0Left = FillTo9With0Left.const_FillTo9With0Left; // "#FillTo9With0LEFT#";
        public const string const_Function_715F10Abmessungen = S715F10Abmessungen.const_S715F10Abmessungen; //"#715F10Abmessungen#"; //Abmessung in /15F10 für Verpackun   
        public const string const_Function_SAG_715F10Abmessungen = S715F10AbmessungenSAG.const_S715F10AbmessungenSAG;

        public const string const_Function_Tata_715F10Length = S715F10LenghtTATA.const_S715F10LenghtTATA; //"#func_WISCO_Netto#";

        public const string const_Function_WISCO_Netto = Format_Netto_WISCO.const_Function_Format_Netto_WISCO; // "#func_WISCO_Netto#";  // schneidet führende 0 ab
        public const string const_Function_MENDRITZKI_Charge = MENDRITZKI_Charge.const_MENDRITZKI_Charge;
        public const string const_Function_MENDRITZKI_Produktionsnummer = MENDRITZKI_Produktionsnummer.const_MENDRITZKI_Produktionsnummer;

        public clsASNFormatFunctions()
        {
            article = new Articles();
            articleCls = new clsArtikel();
        }

        public static List<string> ListASNFormatFunktions()
        {
            List<string> tmpList = new List<string>();
            tmpList.Add(clsASNFormatFunctions.const_Function_Div1000);
            tmpList.Add(clsASNFormatFunctions.const_Function_EinheitAssignment);
            tmpList.Add(clsASNFormatFunctions.const_Function_AnzahlCheckforEinheit);
            tmpList.Add(clsASNFormatFunctions.const_Function_CheckGArtSetAnzahl);
            tmpList.Add(clsASNFormatFunctions.const_Function_CheckGArtSetEinheit);
            tmpList.Add(clsASNFormatFunctions.const_Function_Cut0ValueLeft);
            tmpList.Add(clsASNFormatFunctions.const_Function_DateFromEDI);
            tmpList.Add(clsASNFormatFunctions.const_Function_DateFromEDI_ddMMyyyy);
            tmpList.Add(clsASNFormatFunctions.const_Function_DeleteHyphen);
            tmpList.Add(clsASNFormatFunctions.const_Function_FillTo9With0Left);
            tmpList.Add(clsASNFormatFunctions.const_Function_715F10Abmessungen);
            tmpList.Add(clsASNFormatFunctions.const_Function_MENDRITZKI_Charge);
            tmpList.Add(clsASNFormatFunctions.const_Function_MENDRITZKI_Produktionsnummer);
            tmpList.Add(clsASNFormatFunctions.const_Function_SAG_715F10Abmessungen);
            tmpList.Add(clsASNFormatFunctions.const_Function_Tata_715F10Length);
            tmpList.Add(clsASNFormatFunctions.const_Function_WISCO_Netto);
            return tmpList;
        }

        /*****************************************************************************
         *                          Methoden
         * **************************************************************************/

        public string CustomFormatValue(string myFunction, string strValueIn, Articles myAddArt)
        {
            //-- convert Articles to clsArticle
            articleCls = ArticleClassConverter.GetClsArtikelByArticels(myAddArt);
            clsASNFormatFunctions asnFormatFunction = new clsASNFormatFunctions();
            string strReturn = asnFormatFunction.CustomFormatValue(myFunction, strValueIn, ref articleCls);
            //--- convert und Zuweisung zu clsArtikel zu Articles
            article = asnFormatFunction.article.Copy();
            return strReturn;
        }
        ///<summary>clsASNFromatFunctions / GetArtikelFieldAssignment</summary>
        ///<remarks></remarks>>
        public string CustomFormatValue(string myFunction, string strValueIn, ref clsArtikel myAddArt)
        {
            string strReturn = string.Empty;
            switch (myFunction)
            {
                case const_Function_Diff1000:
                case const_Function_Div1000:
                    strReturn = Div1000.Execute(strValueIn);
                    break;
                case const_Function_EinheitAssignment:
                    //strReturn = func_EinheitAss(strValueIn);
                    strReturn = Format_Einheit.Execute_EinheitAss(strValueIn);
                    break;
                case const_Function_AnzahlCheckforEinheit:
                    //strReturn = func_CheckAnzahlEinheit(ref myAddArt, strValueIn);
                    strReturn = Format_Einheit.Execute_CheckAnzahlEinheit(ref myAddArt, strValueIn);
                    break;
                case const_Function_CheckGArtSetAnzahl:
                    //strReturn = func_CheckGArtSetAnzahl(ref myAddArt, strValueIn);
                    strReturn = Format_Anzahl.Execute(ref myAddArt, strValueIn);
                    break;
                case const_Function_CheckGArtSetEinheit:
                    //strReturn = func_CheckGArtSetEinheit(ref myAddArt, strValueIn);
                    strReturn = Format_Einheit.Execute_CheckGArtSetEinheit(ref myAddArt, strValueIn);
                    break;
                case const_Function_Cut0ValueLeft:
                    //strReturn = func_Cut0ValueLeft(strValueIn);
                    strReturn = Cut0ValueLeft.Execute(strValueIn);
                    break;
                case const_Function_DeleteHyphen:
                    strReturn = DeleteHyphen.Execute(strValueIn);
                    break;
                case const_Function_WISCO_Netto:
                    //strReturn = func_WISCO_Netto(ref myAddArt, strValueIn);
                    strReturn = Format_Netto_WISCO.Execute(ref myAddArt, strValueIn);
                    break;
                case const_Function_FillTo9With0Left:
                    strReturn = FillTo9With0Left.Execute(strValueIn);
                    break;
                case const_Function_715F10Abmessungen:
                    myAddArt = S715F10Abmessungen.Execute(strValueIn, myAddArt);
                    break;
                case const_Function_SAG_715F10Abmessungen:
                    myAddArt = S715F10AbmessungenSAG.Execute(strValueIn, myAddArt);
                    break;
                case const_Function_Tata_715F10Length:
                    strReturn = S715F10LenghtTATA.Execute(strValueIn);
                    break;
                case const_Function_WerksnummerWithHyphen:
                    strReturn = WerksnummerWithHyphen.Execute(strValueIn);
                    break;
                case const_Function_WerksnummerWithOutHyphen:
                    strReturn = WerksnummerWithOutHyphen.Execute(strValueIn);
                    break;
                case const_Function_DateFromEDI:
                    //strReturn = func_GetDateFromEDI(strValueIn);
                    strReturn = Format_GlowDateFromEDI.Execute(strValueIn);
                    break;
                case const_Function_DateFromEDI_ddMMyyyy:
                    //strReturn = func_GetDateFromEDI(strValueIn);
                    strReturn = Format_GlowDateFromEDI.Execute_ddMMyyyy(strValueIn);
                    break;
                case const_Function_MENDRITZKI_Produktionsnummer:
                    strReturn = MENDRITZKI_Produktionsnummer.Execute(myAddArt);
                    break;
                case const_Function_MENDRITZKI_Charge:
                    strReturn = MENDRITZKI_Charge.Execute(myAddArt);
                    break;
                default:
                    strReturn = strValueIn;
                    break;
            }
            articleCls = myAddArt.Copy();
            article = ArticleClassConverter.GetArticlesByClsArtikel(articleCls);
            return strReturn;
        }


    }
}
