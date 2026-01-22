using Common.Enumerations;
using Common.Models;

namespace Common.SqlStatementCreater
{
    public class sqlCreater_WizStoreOut_Call
    {
        /// <summary>
        ///             Wizard StoreOut / Auslagerung Ausgang
        ///                     
        /// </summary>


        public static string sql_String_StoreOut_Call(Calls call, enumStoreOutArt_Steps myStoreOut_Steps)
        {
            string strSql = "Update Abrufe ";
            strSql += "SET ";

            switch (myStoreOut_Steps)
            {
                case enumStoreOutArt_Steps.wizStepLast:

                    strSql += "ScanCheckForStoreOut = '" + call.ScanCheckForStoreOut.ToString() + "' ";
                    strSql += ",ScanUserId = " + call.ScanUserId;
                    break;
            }
            strSql += " WHERE ID=" + call.Id + " ";
            return strSql;
        }

        //public static string sql_String_StoreOut_Manually(Ausgaenge ausgang, enumStoreOutArt_Steps myStoreOut_Steps)
        //{
        //    string strSql = "Update LAusgang ";
        //    strSql += "SET ";

        //    switch (myStoreOut_Steps)
        //    {
        //        case enumStoreOutArt_Steps.wizStepOne:

        //            //strSql += "Auftraggeber=" + ausgang.Auftraggeber;
        //            //strSql += ", Empfaenger=" + ausgang.Empfaenger;
        //            //strSql += ", Entladestelle=" + ausgang.Entladestelle;
        //            //strSql += ", Termin='" + ausgang.Termin.ToString() + "' ";
        //            break;

        //        case enumStoreOutArt_Steps.wizStepTwo:

        //            //strSql += "SpedID=" + ausgang.SpedId;
        //            //strSql += ", KFZ='" + ausgang.KFZ + "' ";
        //            //strSql += ", Trailer='" + ausgang.Trailer + "' ";
        //            //strSql += ", Fahrer='" + ausgang.Fahrer + "' ";
        //            break;

        //        case enumStoreOutArt_Steps.wizStepThree:

        //            //strSql += "Info='" + ausgang.Info + "'";
        //            //strSql += ", LagerTransport=" + Convert.ToInt32(ausgang.LagerTransport);
        //            //strSql += ", PrintActionByScanner=" + Convert.ToInt32(ausgang.PrintActionByScanner);
        //            break;

        //        case enumStoreOutArt_Steps.wizStepLast:

        //            //strSql += "Checked = " + Convert.ToInt32(ausgang.PrintActionByScanner);
        //            break;

        //    }
        //    strSql += " WHERE ID=" + ausgang.Id + " ";

        //    return strSql;
        //}

    }
}
