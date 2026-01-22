namespace Common.SqlStatementCreater
{
    public class sqlCreater_asn_FillDictASNAction
    {
        public static string sqlString(int myAsnActionProcessId, int myAuftraggeber, int myEmpfaenger, int myMandantId, int myArbeitsbereichId)
        {
            string strSQL = string.Empty;
            strSQL = "SELECT a.*, b.Typ FROM ASNAction a " +
                                    "INNER JOIN ASNTyp b on b.TypID=a.ASNTypID " +
                                    "WHERE " +
                                        "a.ActionASN=" + myAsnActionProcessId + " " +
                                        "AND a.Auftraggeber =" + myAuftraggeber + " " +

                                        //Test wenn Empfänger 0 dann Meldungen sollen an Lieferanten raus
                                        "AND (" +
                                                    "(a.Empfaenger=" + myEmpfaenger + ") OR " +
                                                    "(a.Empfaenger=0) " +
                                              ") " +
                                        //"AND (" +
                                        //           "(a.Empfaenger=" + (Int32)this.Empfaenger + ") OR " +
                                        //           "(a.Empfaenger=0) OR "+
                                        //           "(a.Empfaenger=" + (Int32)this.Auftraggeber + ")"+
                                        //     ")" +

                                        //"AND a.Empfaenger=" +(Int32) this.Empfaenger +" "+

                                        "AND a.MandantID=" + myMandantId + " " +
                                        "AND a.AbBereichID=" + myArbeitsbereichId + " " +
                                        "AND a.activ=1 " +
                                        "Order by OrderID;";
            return strSQL;
        }
    }
}
