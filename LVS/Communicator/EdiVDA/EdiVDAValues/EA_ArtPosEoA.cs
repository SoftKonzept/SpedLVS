using System.Data;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class EA_ArtPosEoA
    {
        /// <summary>
        ///             Gibt die jeweilige Position des Artikels in E oder A an!
        /// </summary>

        public const string const_EA_ArtPosEoA = "#ArtPosEoA#";

        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;
            int iPos = 0;
            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                    foreach (DataRow row in myLager.Eingang.dtArtInLEingang.Rows)
                    {
                        iPos++;
                        string strLVS = row["LVS_ID"].ToString();
                        if (myLager.Artikel.LVS_ID.ToString().Equals(strLVS))
                        {
                            strTmp = iPos.ToString();
                            break;
                        }
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_TSE:
                case clsASNTyp.const_string_ASNTyp_TSL:
                case clsASNTyp.const_string_ASNTyp_STE:
                case clsASNTyp.const_string_ASNTyp_STL:
                case clsASNTyp.const_string_ASNTyp_UBE:
                case clsASNTyp.const_string_ASNTyp_UBL:
                    foreach (DataRow row in myLager.Ausgang.dtArtInLAusgang.Rows)
                    {
                        iPos++;
                        string strLVS = row["LVS_ID"].ToString();
                        if (myLager.Artikel.LVS_ID.ToString().Equals(strLVS))
                        {
                            strTmp = iPos.ToString();
                            break;
                        }
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    iPos++;
                    strTmp = iPos.ToString();
                    break;
            }
            return strTmp;
        }
    }
}
