using Common.Models;
using System;

namespace Common.SqlStatementCreater
{
    public class sqlCreater_AddressCustomer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAdrCustomer"></param>
        /// <returns></returns>
        public static string sql_Add(AddressCustomer myAdrCustomer)
        {
            string strSQL = string.Empty;
            strSQL = "INSERT INTO Kunde (KD_ID, ADR_ID, SteuerNr, USt_ID, MwSt, MwStSatz, Bank1, BLZ1, Kto1, " +
                                       "Swift1, IBAN1, Bank2, BLZ2, Kto2,Swift2, IBAN2, Kreditor, Debitor, ZZ, " +
                                       "SalesTaxKeyDebitor, SalesTaxKeyKreditor, Contact, Phone, Mailaddress, Organisation) " +
                                        "VALUES (" +//+ KD_ID +
                                                "(SELECT MAX(KD_ID)+1 FROM Kunde)" +
                                                ", " + myAdrCustomer.AdrId +
                                                ", '" + myAdrCustomer.SteuerNr + "'" +
                                                ", '" + myAdrCustomer.UstId + "'" +
                                                ", " + Convert.ToInt32(myAdrCustomer.MwSt) +
                                                ", '" + myAdrCustomer.MwStSatz.ToString().Replace(",", ".") + "'" +
                                                ", '" + myAdrCustomer.Bank1 + "'" +
                                                ", '" + myAdrCustomer.BLZ1 + "'" +
                                                ", " + myAdrCustomer.Kto1 +
                                                ", '" + myAdrCustomer.Swift1 + "'" +
                                                ", '" + myAdrCustomer.IBAN1 + "'" +
                                                ", '" + myAdrCustomer.Bank2 + "'" +
                                                ", '" + myAdrCustomer.BLZ2 + "'" +
                                                ", " + myAdrCustomer.Kto2 +
                                                ", '" + myAdrCustomer.Swift2 + "'" +
                                                ", '" + myAdrCustomer.IBAN2 + "'" +
                                                ", " + myAdrCustomer.Kreditor +
                                                ", " + myAdrCustomer.Debitor +
                                                ", " + myAdrCustomer.Zahlungziel +
                                                ", " + myAdrCustomer.SalesTaxKeyDebitor +
                                                ", " + myAdrCustomer.SalesTaxKeyKreditor +
                                                ", '" + myAdrCustomer.Contact + "'" +
                                                ", '" + myAdrCustomer.Phone + "'" +
                                                ", '" + myAdrCustomer.Mailaddress + "'" +
                                                ", '" + myAdrCustomer.Organisation + "'" +
                                                "); ";

            return strSQL;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAdrCustomer"></param>
        /// <returns></returns>
        public static string sql_Update(AddressCustomer myAdrCustomer)
        {
            string strSQL = string.Empty;
            strSQL = "Update Kunde SET KD_ID=" + myAdrCustomer.KD_ID +
                                    ", ADR_ID=" + myAdrCustomer.AdrId +
                                    ", SteuerNr='" + myAdrCustomer.SteuerNr + "'" +
                                    ", USt_ID='" + myAdrCustomer.UstId + "'" +
                                    ", MwSt=" + Convert.ToInt32(myAdrCustomer.MwSt) +
                                    ", MwStSatz='" + myAdrCustomer.MwStSatz.ToString().Replace(",", ".") + "'" +
                                    ", Bank1='" + myAdrCustomer.Bank1 + "'" +
                                    ", BLZ1='" + myAdrCustomer.BLZ1 + "'" +
                                    ", Kto1=" + myAdrCustomer.Kto1 +
                                    ", Swift1='" + myAdrCustomer.Swift1 + "'" +
                                    ", IBAN1='" + myAdrCustomer.IBAN1 + "'" +

                                    ", Bank2='" + myAdrCustomer.Bank2 + "'" +
                                    ", BLZ2=" + myAdrCustomer.BLZ2 +
                                    ", Kto2=" + myAdrCustomer.Kto2 +
                                    ", Swift2='" + myAdrCustomer.Swift2 + "'" +
                                    ", IBAN2='" + myAdrCustomer.IBAN2 + "'" +
                                    ", Kreditor=" + myAdrCustomer.Kreditor +
                                    ", Debitor=" + myAdrCustomer.Debitor +
                                    ", ZZ=" + myAdrCustomer.Zahlungziel +
                                    ", KD_IDman=" + (Int32)myAdrCustomer.KD_IDman + " " +
                                    ", SalesTaxKeyDebitor=" + myAdrCustomer.SalesTaxKeyDebitor +
                                    ", SalesTaxKeyKreditor=" + myAdrCustomer.SalesTaxKeyKreditor +

                                    ", Contact='" + myAdrCustomer.Contact + "'" +
                                    ", Phone='" + myAdrCustomer.Phone + "'" +
                                    ", Mailaddress='" + myAdrCustomer.Mailaddress + "'" +
                                    ", Organisation='" + myAdrCustomer.Organisation + "'" +

                                    "WHERE Id=" + myAdrCustomer.Id;

            return strSQL;
        }





    }
}
