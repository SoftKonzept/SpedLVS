using LVS;

namespace Communicator.Classes
{
    public class up1032
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// 
        public const string const_up1032 = "1032";
        public static string SqlString()
        {
            string sql = string.Empty;

            sql += "IF (SELECT ID FROM ASNTyp where TypID=" + clsASNTyp.const_ASNTyp_UBL + ") IS NULL " +
                  "BEGIN ";

            clsASNTyp tmpTyp = new clsASNTyp();
            tmpTyp.Typ = clsASNTyp.const_string_ASNTyp_UBL;
            tmpTyp.Beschreibung = "UB Meldung an BMW / Auftraggeber (Eingang)";
            tmpTyp.TypID = clsASNTyp.const_ASNTyp_UBL;

            sql += tmpTyp.AddSql();
            sql += "END ";

            sql += "IF (SELECT ID FROM ASNTyp where TypID=" + clsASNTyp.const_ASNTyp_UBE + ") IS NULL " +
                    "BEGIN ";
            tmpTyp = new clsASNTyp();
            tmpTyp.Typ = clsASNTyp.const_string_ASNTyp_UBE;
            tmpTyp.Beschreibung = "UB Meldung (Eingang)";
            tmpTyp.TypID = clsASNTyp.const_ASNTyp_UBE;
            sql += tmpTyp.AddSql();
            sql += "END ";

            return sql;
        }
    }
}
