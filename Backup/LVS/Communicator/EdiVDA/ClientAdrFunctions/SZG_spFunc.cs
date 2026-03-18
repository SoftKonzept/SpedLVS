namespace LVS
{
    class SZG_spFunc
    {
        ///<summary>clsVDACreate / GetTMN</summary>
        ///<remarks>TMN = Transportmittelnummer</remarks>
        //public string GetTMN(string myASNTyp, ref clsLagerdaten myLager )
        //{
        //    string strReturn = string.Empty;
        //    switch (myASNTyp)
        //    {
        //        case "EML":
        //        case "EME":
        //        case "BML":
        //        case "BME":
        //            strReturn = myLager.Eingang.KFZ;
        //            break;

        //        case "AML":
        //        case "AME":
        //        case "AVL":
        //        case "AVE":
        //        case "RLL":
        //        case "RLE":
        //            strReturn = myLager.Ausgang.KFZ;
        //            DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
        //            if (DateTime.TryParse(myLager.Ausgang.MAT.ToString(), out dtTmp))
        //            {
        //                strReturn = string.Format("{0:HHMM}", dtTmp);
        //            }
        //            break;
        //    }
        //    if (strReturn.Equals(string.Empty))
        //    {
        //        strReturn = "GC-SL 121";
        //    }
        //    return strReturn;
        //}
        /// <summary>
        ///             Bei VW muss bei Umgebuchten Artikel die original LVS des "alten" Artikels an VW übermittelt werden
        ///             Also muss bei dem Artikel auf UB geprüft werden
        /// </summary>
        /// <param name="myLager"></param>
        /// <returns></returns>

        public string GetLVSIdForVW(ref clsLagerdaten myLager)
        {
            string strReturn = myLager.Artikel.LVS_ID.ToString();
            if ((!myLager.Artikel.Umbuchung) && (myLager.Artikel.ArtIDAlt > 0))
            {
                strReturn = myLager.Artikel.LVSNrBeforeUB.ToString();
            }
            return strReturn;
        }

    }
}
