using Common.Enumerations;
using Common.Models;
using System;

namespace Common.SqlStatementCreater
{
    public class sqlCreater_WizStoreOut_Ausgang
    {
        /// <summary>
        ///             Wizard StoreOut / Auslagerung Ausgang               
        /// </summary>

        public static string sql_String_StoreOut_Open(Ausgaenge ausgang, enumStoreOutArt_Steps myStoreOutArt_Steps)
        {
            string strSql = "Update LAusgang ";
            strSql += "SET ";

            switch (myStoreOutArt_Steps)
            {
                case enumStoreOutArt_Steps.wizStepInputAdr:

                    strSql += "Auftraggeber=" + ausgang.Auftraggeber;
                    strSql += ", Empfaenger=" + ausgang.Empfaenger;
                    strSql += ", Entladestelle=" + ausgang.Entladestelle;
                    break;

                case enumStoreOutArt_Steps.wizStepInputTermin:
                    strSql += " Termin='" + ausgang.Termin.ToString() + "' ";
                    break;

                case enumStoreOutArt_Steps.wizStepInputCarrier:

                    strSql += "SpedID=" + ausgang.SpedId;
                    break;

                case enumStoreOutArt_Steps.wizStepInputVehicle:

                    strSql += " KFZ='" + ausgang.KFZ + "' ";
                    strSql += ", Trailer='" + ausgang.Trailer + "' ";
                    strSql += ", Fahrer='" + ausgang.Fahrer + "' ";
                    strSql += ", WaggonNo='' ";
                    strSql += ", IsWaggon= 0 ";
                    break;

                case enumStoreOutArt_Steps.wizStepInputInfo:

                    strSql += "Info='" + ausgang.Info + "'";
                    strSql += ", LagerTransport=" + Convert.ToInt32(ausgang.LagerTransport);
                    break;

                case enumStoreOutArt_Steps.wizStepInputPrint:

                    strSql += " PrintActionScannerLfs=" + Convert.ToInt32(ausgang.PrintActionScannerLfs);
                    strSql += ", PrintActionScannerKVOFrachtbrief=" + Convert.ToInt32(ausgang.PrintActionScannerKVOFrachtbrief);
                    strSql += ", PrintActionScannerAusgangsliste=" + Convert.ToInt32(ausgang.PrintActionScannerAusgangsliste);
                    break;

                case enumStoreOutArt_Steps.wizStepLast:

                    strSql += "Checked = " + Convert.ToInt32(ausgang.Checked);
                    break;

            }
            strSql += " WHERE ID=" + ausgang.Id + " ";

            return strSql;
        }

    }
}
