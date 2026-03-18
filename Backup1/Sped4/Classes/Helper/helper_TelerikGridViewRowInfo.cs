using System.Drawing;
using Telerik.WinControls.UI;

namespace Sped4.Classes.Helper
{
    public class helper_TelerikGridViewRowInfo
    {
        /// <summary>
        ///             lt. E-Invoice löst eine Rechnungspositon = 0 einen Fehler 
        ///             Lösung:
        ///             Gratisartikel
        ///             Menge = 1
        ///             Einzelpreis = 0 
        ///             Positonsbetrag = 0
        ///             ansonsten FEHLER
        ///             
        ///             False = keine Fehler
        ///             True = Fehler 
        ///             
        /// </summary>
        /// <param name="myInvoiceNo"></param>
        /// <returns></returns>
        public static Color GetBackroundColorForEInvoiceItem(GridViewRowInfo myRow)
        {
            Color ReturnColor = Color.White;
            if (myRow == null) return ReturnColor;
            else
            {
                if (myRow.Cells.Count > 0)
                {
                    decimal decNetAmount = 0;
                    decimal decUnitPrice = 0;
                    decimal decPricePerUnitFactor = 0;
                    decimal decMenge = 0;

                    if ((myRow.Cells["Netto €"] != null) && (myRow.Cells["Netto €"].Value != null))
                    {
                        decimal.TryParse(myRow.Cells["Netto €"].Value.ToString(), out decNetAmount);
                    }
                    if ((myRow.Cells["€/Einheit"] != null) && (myRow.Cells["€/Einheit"].Value != null))
                    {
                        decimal.TryParse(myRow.Cells["€/Einheit"].Value.ToString(), out decUnitPrice);
                    }
                    if ((myRow.Cells["Menge"] != null) && (myRow.Cells["Menge"].Value != null))
                    {
                        decimal.TryParse(myRow.Cells["Menge"].Value.ToString(), out decMenge);
                    }
                    if ((myRow.Cells["PricePerUnitFactor"] != null) && (myRow.Cells["PricePerUnitFactor"].Value != null))
                    {
                        decimal.TryParse(myRow.Cells["PricePerUnitFactor"].Value.ToString(), out decPricePerUnitFactor);
                    }

                    if (
                           ((decMenge > 0) && (decPricePerUnitFactor > 0) && (decUnitPrice == 0) && (decNetAmount == 0)) ||
                           ((decMenge == 0) && (decPricePerUnitFactor > 0) && (decUnitPrice == 0) && (decNetAmount == 0))
                       )
                    {
                        ReturnColor = Color.Orange;
                    }
                    else if (
                                ((decMenge > 0) && (decPricePerUnitFactor > 0) && (decUnitPrice > 0) && (decNetAmount == 0)) ||
                                ((decMenge == 0) && (decPricePerUnitFactor == 0) && (decUnitPrice == 0) && (decNetAmount == 0)) ||
                                ((decMenge == 0) && (decPricePerUnitFactor == 0) && (decUnitPrice == 0) && (decNetAmount > 0)) ||
                                ((decMenge == 0) && (decPricePerUnitFactor == 0) && (decUnitPrice > 0) && (decNetAmount == 0))
                            )
                    {
                        ReturnColor = Color.Tomato;
                    }
                }

            }
            return ReturnColor;
        }
    }
}
