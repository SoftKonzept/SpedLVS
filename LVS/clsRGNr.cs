using System;

namespace LVS
{
    class clsRGNr
    {
        public Globals._GL_USER GL_User;
        public decimal RGNr = 0;
        public decimal GSNr = 0;

        public void getNewRGNr(DateTime AbrZeitraumVon)
        {
            string sql = "select RGNr from Rechnungsnummern where Datum='" + AbrZeitraumVon.Month.ToString() + AbrZeitraumVon.Year.ToString() + "'";
            string Value = clsSQLcon.ExecuteSQL_GetValue(sql, GL_User.User_ID);
            decimal decTmp = 0;
            Decimal.TryParse(Value, out decTmp);
            decTmp++;
            RGNr = decTmp;
        }
        public void updateRGNr(DateTime AbrZeitraumVon)
        {
            string sql = "select RGNr from Rechnungsnummern where Datum='" + AbrZeitraumVon.Month.ToString() + AbrZeitraumVon.Year.ToString() + "'";
            string Value = clsSQLcon.ExecuteSQL_GetValue(sql, GL_User.User_ID);
            decimal decTmp = 0;
            Decimal.TryParse(Value, out decTmp);
            decTmp++;
            if (decTmp > 1)
            {
                sql = "UPDATE Rechnungsnummern SET RGNr = RGNr + 1 " +
                   " OUTPUT INSERTED.RGNr " +
                   "WHERE Datum = '" + AbrZeitraumVon.Month.ToString() + AbrZeitraumVon.Year.ToString() + "'";
                Value = clsSQLcon.ExecuteSQL_GetValue(sql, GL_User.User_ID);
                decTmp = 0;
                Decimal.TryParse(Value, out decTmp);
                decTmp++;
                RGNr = decTmp;
            }
            else
            {
                sql = "insert into Rechnungsnummern (Datum,RGNr,GSNr) Values('" + AbrZeitraumVon.Month.ToString() + AbrZeitraumVon.Year.ToString() + "',1,0)";
                Value = clsSQLcon.ExecuteSQL_GetValue(sql, GL_User.User_ID);
                decTmp = 0;
                Decimal.TryParse(Value, out decTmp);
                decTmp++;
                RGNr = decTmp;
            }
        }

    }
}
