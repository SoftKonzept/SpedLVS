namespace LVS.Fakturierung
{
    public class Fakt_ConversionUnitBasicToCalcUnit
    {
        private decimal _ConversionFactor = 1;
        public decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }
        public Fakt_ConversionUnitBasicToCalcUnit(string myBasicUnit, string myCalcUnit)
        {
            if (myBasicUnit != myCalcUnit)
            {
                //Umrechnung für vorgegebene Einheiten
                switch (myBasicUnit)
                {
                    case "to":
                    case "TO":
                    case "To":
                        if ((myCalcUnit == "kg") || (myCalcUnit == "Kg") || (myCalcUnit == "KG"))
                        {
                            ConversionFactor = 1000;
                        }
                        break;

                    case "kg":
                    case "Kg":
                    case "KG":
                        if ((myCalcUnit == "to") || (myCalcUnit == "To") || (myCalcUnit == "TO"))
                        {
                            ConversionFactor = 0.001M;
                        }
                        break;
                }
            }
        }

    }
}
