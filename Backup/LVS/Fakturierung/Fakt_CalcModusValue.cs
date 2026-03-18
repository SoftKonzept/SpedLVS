using System;
using System.Data;
using System.Linq;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;


namespace LVS.Fakturierung
{
    public class Fakt_CalcModusValue
    {
        private decimal _SumQuantity = 0;
        public decimal SumQuantity
        {
            get { return _SumQuantity; }
            set { _SumQuantity = value; }
        }
        private decimal _SumKost = 0;
        public decimal SumKost
        {
            get { return _SumKost; }
            set { _SumKost = value; }
        }

        private int _SumDuration = 0;
        public int SumDuration
        {
            get { return _SumDuration; }
            set { _SumDuration = value; }
        }

        private decimal _SumPricePerUnitFactor = 0;
        public decimal SumPricePerUnitFactor
        {
            get { return _SumPricePerUnitFactor; }
            set { _SumPricePerUnitFactor = value; }
        }

        public Fakt_CalcModusValue(DataTable mySourceTable, clsTarifPosition myTarifPosition)
        {
            Fakturierung.Fakt_ConversionUnitBasicToCalcUnit fakt_ConversionUnitBasicToCalcUnit = new Fakturierung.Fakt_ConversionUnitBasicToCalcUnit(myTarifPosition.BasisEinheit, myTarifPosition.AbrEinheit);
            decimal decUFact = fakt_ConversionUnitBasicToCalcUnit.ConversionFactor;
            object SumMenge = new object();
            object SumKosten = new object();
            object SumDauer = new object();
            object SumPPUnitFactor = new object();
            string strFilter = "CalcModus=" + (int)myTarifPosition.CalcModus;
            decimal decTmp = 0;

            DataTable dtSourceTarifPosition = mySourceTable.Select("TarifPosID=" + myTarifPosition.ID.ToString()).CopyToDataTable();


            if (myTarifPosition.TarifPosArt.Equals(clsFaktLager.const_Abrechnungsart_Nebenkosten))
            {
                object obj = new object();

                // Summe der Spalte "Kosten" berechnen
                decimal sKosten = dtSourceTarifPosition.AsEnumerable().Sum(row => row.Field<decimal>("Kosten"));
                decTmp = 1M;
                SumQuantity = decTmp;
                SumPricePerUnitFactor = decTmp;
                SumKost = sKosten;
                SumDuration = 0;
            }
            else
            {
                switch (myTarifPosition.CalcModus)
                {
                    case enumCalcultationModus.täglich:
                        if (dtSourceTarifPosition != null)
                        {
                            if (dtSourceTarifPosition.Rows.Count > 0)
                            {
                                if (dtSourceTarifPosition.Columns.Contains("Menge"))
                                {
                                    SumMenge = dtSourceTarifPosition.Compute("SUM(Menge)", strFilter);
                                }
                                decTmp = 0;
                                Decimal.TryParse(SumMenge.ToString(), out decTmp);
                                SumQuantity = decTmp * decUFact;


                                if (dtSourceTarifPosition.Columns.Contains("PricePerUnitFactor"))
                                {
                                    SumPPUnitFactor = dtSourceTarifPosition.Compute("SUM(PricePerUnitFactor)", strFilter);
                                }
                                decTmp = 0;
                                Decimal.TryParse(SumPPUnitFactor.ToString(), out decTmp);
                                SumPricePerUnitFactor = decTmp;


                                if (dtSourceTarifPosition.Columns.Contains("Kosten"))
                                {
                                    SumKosten = dtSourceTarifPosition.Compute("SUM(Kosten)", strFilter);
                                }
                                decTmp = 0;
                                Decimal.TryParse(SumKosten.ToString(), out decTmp);
                                SumKost = decTmp;

                                if (dtSourceTarifPosition.Columns.Contains("Dauer"))
                                {
                                    SumDauer = dtSourceTarifPosition.Compute("SUM(Dauer)", strFilter);
                                }
                                int iTmp = 0;
                                int.TryParse(SumDauer.ToString(), out iTmp);
                                SumDuration = iTmp;
                            }
                        }
                        break;

                    case enumCalcultationModus.monatlich:
                    case enumCalcultationModus.Zeitraum30Tage:
                        if (myTarifPosition.QuantityCalcBase.Equals(clsTarifPosition.const_QuantityBase_Auslagerung))
                        {
                            if (dtSourceTarifPosition != null)
                            {
                                if (dtSourceTarifPosition.Rows.Count > 0)
                                {
                                    if (dtSourceTarifPosition.Columns.Contains("Menge"))
                                    {
                                        SumMenge = dtSourceTarifPosition.Compute("SUM(Menge)", strFilter);
                                    }
                                    decTmp = 0;
                                    Decimal.TryParse(SumMenge.ToString(), out decTmp);
                                    SumQuantity = decTmp * decUFact;

                                    if (dtSourceTarifPosition.Columns.Contains("PricePerUnitFactor"))
                                    {
                                        SumPPUnitFactor = dtSourceTarifPosition.Compute("SUM(PricePerUnitFactor)", strFilter);
                                    }
                                    decTmp = 0;
                                    Decimal.TryParse(SumPPUnitFactor.ToString(), out decTmp);
                                    SumPricePerUnitFactor = decTmp;

                                    if (dtSourceTarifPosition.Columns.Contains("Kosten"))
                                    {
                                        SumKosten = dtSourceTarifPosition.Compute("SUM(Kosten)", strFilter);
                                    }
                                    decTmp = 0;
                                    Decimal.TryParse(SumKosten.ToString(), out decTmp);
                                    SumKost = decTmp;

                                    if (dtSourceTarifPosition.Columns.Contains("Dauer"))
                                    {
                                        SumDauer = dtSourceTarifPosition.Compute("SUM(Dauer)", strFilter);
                                    }
                                    int iTmp = 0;
                                    int.TryParse(SumDauer.ToString(), out iTmp);
                                    SumDuration = iTmp;
                                }
                            }
                        }
                        else
                        {
                            if (dtSourceTarifPosition != null)
                            {
                                if (dtSourceTarifPosition.Rows.Count > 0)
                                {
                                    if (dtSourceTarifPosition.Columns.Contains("Menge"))
                                    {
                                        SumMenge = dtSourceTarifPosition.Compute("SUM(Menge)", strFilter);
                                    }
                                    decTmp = 0;
                                    Decimal.TryParse(SumMenge.ToString(), out decTmp);
                                    SumQuantity = decTmp * decUFact;

                                    if (dtSourceTarifPosition.Columns.Contains("PricePerUnitFactor"))
                                    {
                                        SumPPUnitFactor = dtSourceTarifPosition.Compute("SUM(PricePerUnitFactor)", strFilter);
                                    }
                                    decTmp = 0;
                                    Decimal.TryParse(SumPPUnitFactor.ToString(), out decTmp);
                                    SumPricePerUnitFactor = decTmp;

                                    if (dtSourceTarifPosition.Columns.Contains("Kosten"))
                                    {
                                        SumKosten = dtSourceTarifPosition.Compute("SUM(Kosten)", strFilter);
                                    }
                                    decTmp = 0;
                                    Decimal.TryParse(SumKosten.ToString(), out decTmp);
                                    SumKost = decTmp;

                                    if (dtSourceTarifPosition.Columns.Contains("Dauer"))
                                    {
                                        SumDauer = dtSourceTarifPosition.Compute("SUM(Dauer)", strFilter);
                                    }
                                    int iTmp = 0;
                                    int.TryParse(SumDauer.ToString(), out iTmp);
                                    SumDuration = iTmp;
                                }
                            }
                        }
                        break;

                    case enumCalcultationModus.Pauschal:
                        if (dtSourceTarifPosition != null)
                        {
                            if (dtSourceTarifPosition.Rows.Count > 0)
                            {
                                //if (dtSourceTarifPosition.Columns.Contains("Menge"))
                                //{
                                //    SumMenge = dtSourceTarifPosition.Compute("SUM(Menge)", strFilter);
                                //}
                                //decTmp = 0;
                                //Decimal.TryParse(SumMenge.ToString(), out decTmp);
                                //SumQuantity = decTmp * decUFact;
                                //SumPricePerUnitFactor = SumQuantity;

                                //SumKost = myTarifPosition.PreisEinheit;
                                if (dtSourceTarifPosition.Columns.Contains("Menge"))
                                {
                                    SumMenge = dtSourceTarifPosition.Compute("SUM(Menge)", strFilter);
                                }
                                decTmp = 0;
                                Decimal.TryParse(SumMenge.ToString(), out decTmp);
                                SumQuantity = decTmp * decUFact;

                                if (dtSourceTarifPosition.Columns.Contains("PricePerUnitFactor"))
                                {
                                    SumPPUnitFactor = dtSourceTarifPosition.Compute("SUM(PricePerUnitFactor)", strFilter);
                                }
                                decTmp = 0;
                                Decimal.TryParse(SumPPUnitFactor.ToString(), out decTmp);
                                SumPricePerUnitFactor = decTmp;

                                if (dtSourceTarifPosition.Columns.Contains("Kosten"))
                                {
                                    SumKosten = dtSourceTarifPosition.Compute("SUM(Kosten)", strFilter);
                                }
                                decTmp = 0;
                                Decimal.TryParse(SumKosten.ToString(), out decTmp);
                                SumKost = decTmp;

                                if (dtSourceTarifPosition.Columns.Contains("Dauer"))
                                {
                                    SumDauer = dtSourceTarifPosition.Compute("SUM(Dauer)", strFilter);
                                }
                                int iTmp = 0;
                                int.TryParse(SumDauer.ToString(), out iTmp);
                                SumDuration = iTmp;
                            }
                        }
                        break;

                    case enumCalcultationModus.Standard:
                        if (dtSourceTarifPosition != null)
                        {
                            if (dtSourceTarifPosition.Rows.Count > 0)
                            {
                                if (mySourceTable.Columns.Contains("Menge"))
                                {
                                    SumMenge = dtSourceTarifPosition.Compute("SUM(Menge)", strFilter);
                                }
                                decTmp = 0;
                                Decimal.TryParse(SumMenge.ToString(), out decTmp);
                                SumQuantity = decTmp * decUFact;

                                if (dtSourceTarifPosition.Columns.Contains("PricePerUnitFactor"))
                                {
                                    SumPPUnitFactor = dtSourceTarifPosition.Compute("SUM(PricePerUnitFactor)", strFilter);
                                }
                                decTmp = 0;
                                Decimal.TryParse(SumPPUnitFactor.ToString(), out decTmp);
                                SumPricePerUnitFactor = decTmp;

                                if (dtSourceTarifPosition.Columns.Contains("Kosten"))
                                {
                                    SumKosten = dtSourceTarifPosition.Compute("SUM(Kosten)", strFilter);
                                }
                                decTmp = 0;
                                Decimal.TryParse(SumKosten.ToString(), out decTmp);
                                SumKost = decTmp;

                                if (dtSourceTarifPosition.Columns.Contains("Dauer"))
                                {
                                    SumDauer = dtSourceTarifPosition.Compute("SUM(Dauer)", strFilter);
                                }
                                int iTmp = 0;
                                int.TryParse(SumDauer.ToString(), out iTmp);
                                SumDuration = iTmp;
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
