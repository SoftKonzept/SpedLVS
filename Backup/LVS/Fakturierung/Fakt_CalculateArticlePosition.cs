using System;
using System.Data;
using System.Linq;

namespace LVS.Fakturierung
{
    public class Fakt_CalculateArticlePosition
    {
        private DataTable _dtDistinctArtID = new DataTable();
        public DataTable dtDistinctArtID
        {
            get { return _dtDistinctArtID; }
            set { _dtDistinctArtID = value; }
        }

        private clsTarif _Tarif = new clsTarif();
        public clsTarif Tarif
        {
            get { return _Tarif; }
            set { _Tarif = value; }
        }
        public Fakt_CalculateArticlePosition(DataTable mySourceTable, clsTarif myTarif, DateTime VonZeitraum, DateTime BisZeitraum)
        {
            // Constructor logic here
            dtDistinctArtID = mySourceTable.Clone();
            dtDistinctArtID.Rows.Clear();

            Tarif = myTarif;
            Fakturierung.Fakt_ConversionUnitBasicToCalcUnit fakt_ConversionUnitBasicToCalcUnit = new Fakturierung.Fakt_ConversionUnitBasicToCalcUnit(Tarif.TarifPosition.BasisEinheit, Tarif.TarifPosition.AbrEinheit);
            decimal decUFact = fakt_ConversionUnitBasicToCalcUnit.ConversionFactor;

            if ((mySourceTable.Rows.Count > 0) && (myTarif.ID > 0))
            {
                DataRow[] selRow = mySourceTable.Select("TarifPosID=" + myTarif.TarifPosition.ID.ToString());
                foreach (DataRow rowTmp in selRow)
                {
                    //--- Eingangsdatum + Freie Tage ergibt das Datum ab dem berechnet werden muss
                    DateTime EDate = new DateTime(((DateTime)rowTmp["EDatum"]).Year, ((DateTime)rowTmp["EDatum"]).Month, ((DateTime)rowTmp["EDatum"]).Day, 0, 0, 0);
                    TimeSpan DaysAdd = new TimeSpan(Tarif.TarifPosition.Lagerdauer, 0, 0, 0, 0);
                    DateTime DateToCalc = EDate.Add(DaysAdd);

                    //-- BerechnungszeitraumEndPunkt = BisZeitraum
                    DateTime DateCalEndPoint = BisZeitraum;
                    if (!string.IsNullOrEmpty(rowTmp["ADatum"].ToString()))
                    {
                        ///--Vergleich zwischen Abrechnungsdatum = DateCalEndPoint und Ausgangsdatum
                        ///--sobald das Ausgangsdatum vor dem Abrechnungsdatum liegt wird das Ausgangsdatum zum Abrechnungsdatum
                        ///-- 30.04.2025 war  hier ein Datum ab dem ein Updat der Berechnung durchgeführt wurde
                        if (BisZeitraum > new DateTime(2025, 4, 30))
                        {
                            DateTime ADate = new DateTime(((DateTime)rowTmp["ADatum"]).Year, ((DateTime)rowTmp["ADatum"]).Month, ((DateTime)rowTmp["ADatum"]).Day, 0, 0, 0);
                            if (DateCalEndPoint > ADate)
                            {
                                //DateCalEndPoint = new DateTime(((DateTime)rowTmp["ADatum"]).Year, ((DateTime)rowTmp["ADatum"]).Month, ((DateTime)rowTmp["ADatum"]).Day, 0, 0, 0); // bis 30.04.2025
                                DateCalEndPoint = ADate;
                            }
                        }
                        else
                        {
                            DateCalEndPoint = new DateTime(((DateTime)rowTmp["ADatum"]).Year, ((DateTime)rowTmp["ADatum"]).Month, ((DateTime)rowTmp["ADatum"]).Day, 0, 0, 0);
                        }
                    }

                    if (Tarif.TarifPosition.TarifPosArt.Equals(clsFaktLager.const_Abrechnungsart_Nebenkosten))
                    {
                        //object obj = new object();
                        //if (dtTmp.Rows.Count > 0)
                        //{
                        //    if (dtTmp.Columns.Contains("Kosten"))
                        //    {
                        //        obj = dtTmp.Compute("SUM(Kosten)", string.Empty);
                        //    }
                        //    decTmp = 0;
                        //    Decimal.TryParse(obj.ToString(), out decTmp);
                        //    Rechnung.RGPosNebenkosten.NettoPreis = decTmp;
                        //    Rechnung.RGPosNebenkosten.EinzelPreis = decTmp;
                        //    Rechnung.RGPosNebenkosten.Menge = 1;
                        //    Rechnung.RGPosNebenkosten.PricePerUnitFactor = Rechnung.RGPosNebenkosten.Menge;
                        //}
                        decimal decMenge = 1M;
                        rowTmp["Menge"] = decMenge;
                        rowTmp["PricePerUnitFactor"] = decMenge;
                        //rowTmp["Kosten"] =
                        dtDistinctArtID.ImportRow(rowTmp);

                    }
                    else
                    {
                        ////--- Eingangsdatum + Freie Tage ergibt das Datum ab dem berechnet werden muss
                        //DateTime EDate = new DateTime(((DateTime)rowTmp["EDatum"]).Year, ((DateTime)rowTmp["EDatum"]).Month, ((DateTime)rowTmp["EDatum"]).Day, 0, 0, 0);
                        //TimeSpan DaysAdd = new TimeSpan(Tarif.TarifPosition.Lagerdauer, 0, 0, 0, 0);
                        //DateTime DateToCalc = EDate.Add(DaysAdd);

                        ////-- BerechnungszeitraumEndPunkt = BisZeitraum
                        //DateTime DateCalEndPoint = BisZeitraum;
                        //if (!string.IsNullOrEmpty(rowTmp["ADatum"].ToString()))
                        //{
                        //    ///--Vergleich zwischen Abrechnungsdatum = DateCalEndPoint und Ausgangsdatum
                        //    ///--sobald das Ausgangsdatum vor dem Abrechnungsdatum liegt wird das Ausgangsdatum zum Abrechnungsdatum
                        //    ///-- 30.04.2025 war  hier ein Datum ab dem ein Updat der Berechnung durchgeführt wurde
                        //    if (BisZeitraum > new DateTime(2025, 4, 30))
                        //    {
                        //        DateTime ADate = new DateTime(((DateTime)rowTmp["ADatum"]).Year, ((DateTime)rowTmp["ADatum"]).Month, ((DateTime)rowTmp["ADatum"]).Day, 0, 0, 0);
                        //        if (DateCalEndPoint > ADate)
                        //        {
                        //            //DateCalEndPoint = new DateTime(((DateTime)rowTmp["ADatum"]).Year, ((DateTime)rowTmp["ADatum"]).Month, ((DateTime)rowTmp["ADatum"]).Day, 0, 0, 0); // bis 30.04.2025
                        //            DateCalEndPoint = ADate;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        DateCalEndPoint = new DateTime(((DateTime)rowTmp["ADatum"]).Year, ((DateTime)rowTmp["ADatum"]).Month, ((DateTime)rowTmp["ADatum"]).Day, 0, 0, 0);
                        //    }
                        //}

                        decimal decKosten = 0;
                        TimeSpan DayDiff = new TimeSpan();
                        int iDays = 0;
                        decimal decPricePerUnitFactor = 0;
                        string strMenge = string.Empty;

                        switch (Tarif.TarifPosition.CalcModus)
                        {
                            case enumCalcultationModus.monatlich:
                                switch (Tarif.TarifPosition.QuantityCalcBase)
                                {
                                    case clsTarifPosition.const_QuantityBase_Standard:
                                        decimal decTmpPricePerUnitFactor_Standard = 0;
                                        decimal.TryParse(rowTmp["Menge"].ToString(), out decTmpPricePerUnitFactor_Standard);
                                        decPricePerUnitFactor = decTmpPricePerUnitFactor_Standard * decUFact;
                                        decPricePerUnitFactor = Math.Round(decPricePerUnitFactor, 3);
                                        rowTmp["PricePerUnitFactor"] = decPricePerUnitFactor;

                                        //decKosten = ((decimal)rowTmp["Menge"] * decUFact) * (decimal)rowTmp["Preis"];
                                        decKosten = decPricePerUnitFactor * (decimal)rowTmp["Preis"];
                                        //rowTmp["Kosten"] = Math.Round(decKosten, 2, MidpointRounding.AwayFromZero);
                                        rowTmp["Kosten"] = decKosten;
                                        if (Tarif.TarifPosition.Lagerdauer > 0)
                                        {
                                            int iDauer = 0;
                                            int.TryParse(rowTmp["Dauer"].ToString(), out iDauer);
                                            if (iDauer > 0)
                                            {
                                                dtDistinctArtID.ImportRow(rowTmp);
                                            }
                                        }
                                        else
                                        {
                                            dtDistinctArtID.ImportRow(rowTmp);
                                        }
                                        break;

                                    case clsTarifPosition.const_QuantityBase_Einlagerung:
                                        decTmpPricePerUnitFactor_Standard = 0;
                                        decimal.TryParse(rowTmp["Menge"].ToString(), out decTmpPricePerUnitFactor_Standard);
                                        decPricePerUnitFactor = decTmpPricePerUnitFactor_Standard * decUFact;
                                        decPricePerUnitFactor = Math.Round(decPricePerUnitFactor, 3);
                                        rowTmp["PricePerUnitFactor"] = decPricePerUnitFactor;

                                        //decKosten = ((decimal)rowTmp["Menge"] * decUFact) * (decimal)rowTmp["Preis"];
                                        decKosten = decPricePerUnitFactor * (decimal)rowTmp["Preis"];
                                        //rowTmp["Kosten"] = Math.Round(decKosten, 2, MidpointRounding.AwayFromZero);
                                        rowTmp["Kosten"] = decKosten;
                                        //dtDistinctArtID.ImportRow(rowTmp);

                                        if (Tarif.TarifPosition.Lagerdauer > 0)
                                        {
                                            int iDauer = 0;
                                            int.TryParse(rowTmp["Dauer"].ToString(), out iDauer);
                                            if (iDauer > 0)
                                            {
                                                dtDistinctArtID.ImportRow(rowTmp);
                                            }
                                        }
                                        else
                                        {
                                            dtDistinctArtID.ImportRow(rowTmp);
                                        }
                                        break;

                                    case clsTarifPosition.const_QuantityBase_Auslagerung:
                                        int iDauerMonth = 0;
                                        if (DateCalEndPoint >= DateToCalc)
                                        {
                                            iDauerMonth = Functions.TotalMonths(DateCalEndPoint, DateToCalc) + 1;
                                        }
                                        rowTmp["Dauer"] = iDauerMonth;

                                        decimal decTmpPricePerUnitFactor = 0;
                                        decimal.TryParse(rowTmp["Menge"].ToString(), out decTmpPricePerUnitFactor);
                                        decPricePerUnitFactor = decPricePerUnitFactor * decUFact * iDauerMonth;
                                        decPricePerUnitFactor = Math.Round(decPricePerUnitFactor, 3);
                                        rowTmp["PricePerUnitFactor"] = decPricePerUnitFactor;

                                        if (iDauerMonth > 0)
                                        {
                                            //decKosten = (Int32)rowTmp["Dauer"] * ((decimal)rowTmp["Menge"] * decUFact) * (decimal)rowTmp["Preis"];
                                            decKosten = decPricePerUnitFactor * (decimal)rowTmp["Preis"];
                                            //rowTmp["Kosten"] = Math.Round(decKosten, 2, MidpointRounding.AwayFromZero);
                                            rowTmp["Kosten"] = decKosten;
                                        }
                                        else
                                        {
                                            rowTmp["Kosten"] = 0.0M;
                                            rowTmp["Dauer"] = 0;
                                        }
                                        dtDistinctArtID.ImportRow(rowTmp);
                                        break;



                                    default:
                                        if (Tarif.TarifPosition.Lagerdauer > 0)
                                        {
                                            int iDauer = 0;
                                            int.TryParse(rowTmp["Dauer"].ToString(), out iDauer);
                                            if (iDauer > 0)
                                            {
                                                decimal decTmpPricePerUnitFactor_Default = 0;
                                                decimal.TryParse(rowTmp["Menge"].ToString(), out decTmpPricePerUnitFactor_Default);
                                                decPricePerUnitFactor = decTmpPricePerUnitFactor_Default * decUFact;
                                                decPricePerUnitFactor = Math.Round(decPricePerUnitFactor, 3);
                                                rowTmp["PricePerUnitFactor"] = decPricePerUnitFactor;

                                                //decKosten = ((decimal)rowTmp["Menge"] * decUFact) * (decimal)rowTmp["Preis"];
                                                decKosten = decPricePerUnitFactor * (decimal)rowTmp["Preis"];
                                                //rowTmp["Kosten"] = Math.Round(decKosten, 2, MidpointRounding.AwayFromZero);
                                                rowTmp["Kosten"] = decKosten;
                                                dtDistinctArtID.ImportRow(rowTmp);
                                            }
                                        }
                                        else
                                        {
                                            dtDistinctArtID.ImportRow(rowTmp);
                                        }
                                        break;
                                }

                                break;

                            case enumCalcultationModus.täglich:
                                //Zeitdifferenz zwischen BerechnungszeitraumEndPunkt und DateToCalc
                                DayDiff = new TimeSpan();
                                DayDiff = DateCalEndPoint - DateToCalc;
                                rowTmp["Dauer"] = DayDiff.Days + 1;

                                iDays = 0;
                                int.TryParse(rowTmp["Dauer"].ToString(), out iDays);
                                if (iDays > 0)
                                {
                                    decimal decTmpPricePerUnitFactor_täglich = 0;
                                    decimal.TryParse(rowTmp["Menge"].ToString(), out decTmpPricePerUnitFactor_täglich);
                                    decPricePerUnitFactor = Math.Round((decTmpPricePerUnitFactor_täglich * decUFact), 3) * iDays; // decTmpPricePerUnitFactor_täglich * decUFact * iDays;
                                                                                                                                  //decPricePerUnitFactor = Math.Round(decPricePerUnitFactor, 3);
                                    rowTmp["PricePerUnitFactor"] = decPricePerUnitFactor;

                                    //decKosten = iDays * ((decimal)rowTmp["Menge"] * decUFact) * (decimal)rowTmp["Preis"];
                                    decKosten = decPricePerUnitFactor * (decimal)rowTmp["Preis"];
                                    //rowTmp["Kosten"] = Math.Round(decKosten, 2, MidpointRounding.AwayFromZero);
                                    rowTmp["Kosten"] = decKosten;
                                }
                                else
                                {
                                    rowTmp["Kosten"] = 0.0M;
                                    rowTmp["PricePerUnitFactor"] = 0.0M;
                                    rowTmp["Dauer"] = 0;
                                }
                                dtDistinctArtID.ImportRow(rowTmp);
                                break;

                            case enumCalcultationModus.Zeitraum30Tage:
                                if (Tarif.TarifPosition.QuantityCalcBase.Equals(clsTarifPosition.const_QuantityBase_Auslagerung))
                                {
                                    TimeSpan ZRDayDiff = (DateCalEndPoint - DateToCalc);
                                    int iDayToCalc = ZRDayDiff.Days + 1;
                                    int iDauerZR = 0;

                                    if (Tarif.TarifPosition.Lagerdauer > 0)
                                    {
                                        if (iDayToCalc > 0)
                                        {
                                            decimal decTmp = 1M;
                                            decTmp = (decimal)iDayToCalc / (decimal)30;
                                            iDauerZR = (int)Math.Ceiling(decTmp);
                                        }
                                    }
                                    else
                                    {
                                        iDauerZR = iDayToCalc;
                                    }

                                    if (iDauerZR > 0)
                                    {
                                        rowTmp["Dauer"] = iDauerZR;

                                        decimal decTmpPricePerUnitFactor_30Tage = 0;
                                        decimal.TryParse(rowTmp["Menge"].ToString(), out decTmpPricePerUnitFactor_30Tage);
                                        decPricePerUnitFactor = decTmpPricePerUnitFactor_30Tage * decUFact * iDauerZR;
                                        decPricePerUnitFactor = Math.Round(decPricePerUnitFactor, 3);
                                        rowTmp["PricePerUnitFactor"] = decPricePerUnitFactor;

                                        //decKosten = (Int32)rowTmp["Dauer"] * ((decimal)rowTmp["Menge"] * decUFact) * (decimal)rowTmp["Preis"];
                                        decKosten = decPricePerUnitFactor * (decimal)rowTmp["Preis"];
                                        //rowTmp["Kosten"] = Math.Round(decKosten, 2, MidpointRounding.AwayFromZero);
                                        rowTmp["Kosten"] = decKosten;
                                    }
                                    else
                                    {
                                        rowTmp["PricePerUnitFactor"] = 0.0M;
                                        rowTmp["Kosten"] = 0.0M;
                                        rowTmp["Dauer"] = 0;
                                    }
                                    dtDistinctArtID.ImportRow(rowTmp);
                                }
                                else
                                {
                                    if (Tarif.TarifPosition.Lagerdauer > 0)
                                    {
                                        int iDauer = 0;
                                        int.TryParse(rowTmp["Dauer"].ToString(), out iDauer);
                                        if (iDauer > 0)
                                        {
                                            decimal decTmpPricePerUnitFactor_30Tage = 0;
                                            decimal.TryParse(rowTmp["Menge"].ToString(), out decTmpPricePerUnitFactor_30Tage);
                                            decPricePerUnitFactor = decTmpPricePerUnitFactor_30Tage * decUFact * iDauer;
                                            decPricePerUnitFactor = Math.Round(decPricePerUnitFactor, 3);
                                            rowTmp["PricePerUnitFactor"] = decPricePerUnitFactor;

                                            //decKosten = (Int32)rowTmp["Dauer"] * ((decimal)rowTmp["Menge"] * decUFact) * (decimal)rowTmp["Preis"];
                                            decKosten = decPricePerUnitFactor * (decimal)rowTmp["Preis"];
                                            //rowTmp["Kosten"] = Math.Round(decKosten, 2, MidpointRounding.AwayFromZero);
                                            rowTmp["Kosten"] = decKosten;
                                            dtDistinctArtID.ImportRow(rowTmp);
                                        }
                                    }
                                    else
                                    {
                                        dtDistinctArtID.ImportRow(rowTmp);
                                    }
                                }
                                break;


                            case enumCalcultationModus.Standard:
                                int iCheck = 0;
                                if (rowTmp["ID"] != null)
                                {
                                    int iTmpArtikelId = 0;
                                    if (rowTmp["ID"] != DBNull.Value && int.TryParse(rowTmp["ID"].ToString(), out iTmpArtikelId))
                                    {
                                        if (iTmpArtikelId == 722578)
                                        {
                                            string s = string.Empty;
                                        }
                                    }
                                }
                                //Zeitdifferenz zwischen BerechnungszeitraumEndPunkt und DateToCalc
                                DayDiff = new TimeSpan();
                                DayDiff = DateCalEndPoint - DateToCalc;
                                iDays = DayDiff.Days + 1;
                                if (iDays > 0)
                                {
                                    strMenge = rowTmp["Menge"].ToString();
                                    decimal decMenge = 1;
                                    decimal.TryParse(strMenge, out decMenge);

                                    decimal decPricePerUnitFactorStandard = decMenge * decUFact;
                                    decPricePerUnitFactor = Math.Round(decPricePerUnitFactorStandard, 3);
                                    rowTmp["PricePerUnitFactor"] = decPricePerUnitFactor;

                                    string strPreis = rowTmp["Preis"].ToString();
                                    decimal decPreis = 1;
                                    decimal.TryParse(strPreis, out decPreis);

                                    //decKosten = iDays * (decMenge / 1000) * decPreis;
                                    //decKosten = (decMenge * decUFact) * decPreis;
                                    decKosten = decPricePerUnitFactorStandard * decPreis;

                                    //decKosten = iDays * ((decimal)rowTmp["Menge"] / 1000) * (decimal)rowTmp["Preis"];
                                    rowTmp["Kosten"] = Math.Round(decKosten, 2, MidpointRounding.AwayFromZero);
                                    rowTmp["Kosten"] = decKosten;

                                    if (
                                         (dtDistinctArtID.AsEnumerable().FirstOrDefault(x => x.Field<decimal>("ID") == (decimal)rowTmp["ID"]) == null)
                                       )
                                    {
                                        dtDistinctArtID.ImportRow(rowTmp);
                                    }
                                    else
                                    {
                                        string str = "doppelt!!";
                                    }
                                }
                                break;

                            default:
                                switch (Tarif.TarifPosition.DatenfeldArtikel)
                                {
                                    case "ID":
                                    case "Anzahl":
                                        int iMenge = 0;
                                        string strMengeDefault = rowTmp["Menge"].ToString();
                                        int.TryParse(strMengeDefault, out iMenge);

                                        decimal decPricePerUnitFactorDefalault = iMenge;
                                        decPricePerUnitFactor = Math.Round(decPricePerUnitFactorDefalault, 3);
                                        rowTmp["PricePerUnitFactor"] = decPricePerUnitFactor;
                                        decKosten = 0;
                                        decKosten = (int)decPricePerUnitFactorDefalault * (decimal)rowTmp["Preis"];
                                        rowTmp["Kosten"] = decKosten;
                                        if (decPricePerUnitFactorDefalault > 0)
                                        {
                                            //rowTmp["Kosten"] = iMenge * (decimal)rowTmp["Preis"];
                                            //rowTmp["Kosten"] = (int)decPricePerUnitFactorDefalault * (decimal)rowTmp["Preis"];
                                            rowTmp["Kosten"] = decKosten;
                                        }
                                        else
                                        {
                                            rowTmp["Kosten"] = 0;
                                        }
                                        //Math.Round(decKosten, 2, MidpointRounding.AwayFromZero);
                                        break;

                                    case "Netto":
                                    case "Brutto":
                                        string strMengeDefaultTo = string.Empty;
                                        strMengeDefaultTo = rowTmp["Menge"].ToString();
                                        decimal decMenge = 1;
                                        decimal.TryParse(strMengeDefaultTo, out decMenge);

                                        decimal decPricePerUnitFactorDefaultTo = decMenge * decUFact;
                                        decPricePerUnitFactor = Math.Round(decPricePerUnitFactorDefaultTo, 3);
                                        rowTmp["PricePerUnitFactor"] = decPricePerUnitFactor;

                                        decKosten = 0;
                                        if (decPricePerUnitFactorDefaultTo > 0)
                                        {
                                            //rowTmp["Kosten"] = iMenge * (decimal)rowTmp["Preis"];
                                            //rowTmp["Kosten"] = decPricePerUnitFactorDefaultTo * (decimal)rowTmp["Preis"];
                                            decKosten = decPricePerUnitFactorDefaultTo * (decimal)rowTmp["Preis"];
                                        }
                                        else
                                        {
                                            //rowTmp["Kosten"] = 0;
                                            decKosten = 0;
                                        }
                                        //rowTmp["Kosten"] = ((decimal)rowTmp["Menge"] * decUFact) * (decimal)rowTmp["Preis"];
                                        //rowTmp["Kosten"] = Math.Round(decKosten, 2, MidpointRounding.AwayFromZero);
                                        rowTmp["Kosten"] = decKosten;
                                        break;
                                }
                                if (Tarif.TarifPosition.Lagerdauer > 0)
                                {
                                    int iDauer = 0;
                                    int.TryParse(rowTmp["Dauer"].ToString(), out iDauer);
                                    if (iDauer > 0)
                                    {
                                        dtDistinctArtID.ImportRow(rowTmp);
                                    }
                                }
                                else
                                {
                                    if (
                                         (dtDistinctArtID.AsEnumerable().FirstOrDefault(x => x.Field<decimal>("ID") == (decimal)rowTmp["ID"]) == null)
                                       )
                                    {
                                        dtDistinctArtID.ImportRow(rowTmp);
                                    }
                                    else
                                    {
                                        string str = "doppelt!!";
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
