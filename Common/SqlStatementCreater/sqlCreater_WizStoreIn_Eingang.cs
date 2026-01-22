using Common.Enumerations;
using Common.Models;
using System;

namespace Common.SqlStatementCreater
{
    public class sqlCreater_WizStoreIn_Eingang
    {
        /// <summary>
        ///             Wizard StoreIn / Einlagerung , Eingang                
        /// </summary>

        public static string sql_String_Update_StoreIn_Edi(Eingaenge eingang, enumStoreInArt_Steps myStoreInArt_Steps)
        {
            string strSql = sqlCreater_WizStoreIn_Eingang.sql_String_Update_StoreIn_Open(eingang, myStoreInArt_Steps);
            return strSql;
        }

        public static string sql_String_Update_StoreIn_Manual(Eingaenge eingang, enumStoreInArt_Steps myStoreInArt_Steps)
        {
            string strSql = sqlCreater_WizStoreIn_Eingang.sql_String_Update_StoreIn_Open(eingang, myStoreInArt_Steps);
            return strSql;
        }

        public static string sql_String_Update_StoreIn_Open(Eingaenge eingang, enumStoreInArt_Steps myStoreInArt_Steps)
        {
            if (eingang.Lieferant == null)
            {
                eingang.Lieferant = string.Empty;
            }


            string strSql = "Update LEingang ";
            strSql += "SET ";

            switch (myStoreInArt_Steps)
            {
                case enumStoreInArt_Steps.wizStepInputAdr:

                    strSql += " Auftraggeber=" + eingang.Auftraggeber;
                    strSql += ", Lieferant='" + eingang.Lieferant + "' ";
                    strSql += ", Empfaenger=" + eingang.Empfaenger;
                    strSql += ", Versender=" + eingang.Versender;
                    strSql += ", BeladeID=" + eingang.BeladeID;
                    strSql += ", EntladeID=" + eingang.EntladeID;

                    break;

                case enumStoreInArt_Steps.wizStepInputLfs:

                    strSql += "Date= '" + eingang.Eingangsdatum.ToString("dd.MM.yyyy") + "'";
                    strSql += ", LfsNr='" + eingang.LfsNr + "' ";

                    break;

                case enumStoreInArt_Steps.wizStepInputCarrier:

                    strSql += "SpedID=" + eingang.SpedId;

                    break;

                case enumStoreInArt_Steps.wizStepInputVehicle:

                    strSql += " KFZ='" + eingang.KFZ + "' ";
                    strSql += ", WaggonNo='" + eingang.WaggonNr + "' ";
                    strSql += ", Fahrer='" + eingang.Fahrer + "' ";
                    strSql += ", Ship='" + eingang.Ship + "' ";
                    strSql += ", IsWaggon= " + Convert.ToInt32(eingang.IsWaggon);
                    strSql += ", IsShip= " + Convert.ToInt32(eingang.IsShip);

                    break;

                case enumStoreInArt_Steps.wizStepInputArt:

                    strSql += " LagerTransport=" + Convert.ToInt32(eingang.LagerTransport);
                    strSql += ", DirectDelivery=" + Convert.ToInt32(eingang.DirektDelivery);
                    strSql += ", Retoure=" + Convert.ToInt32(eingang.Retoure);
                    strSql += ", Verlagerung=" + Convert.ToInt32(eingang.Verlagerung);

                    break;


                case enumStoreInArt_Steps.wizStepInputPrintAction:

                    strSql += " PrintActionByScanner=" + Convert.ToInt32(eingang.PrintActionByScanner);
                    strSql += ", PrintActionScannerAllLable=" + Convert.ToInt32(eingang.PrintActionScannerAllLable);
                    strSql += ", PrintActionScannerEingangsliste=" + Convert.ToInt32(eingang.PrintActionScannerEingangsliste);
                    break;


                case enumStoreInArt_Steps.wizStepLastCheckComplete:

                    strSql += "[Check] = " + Convert.ToInt32(eingang.Check);
                    break;

            }
            strSql += " WHERE ID=" + eingang.Id + " ";

            return strSql;
        }
    }
}
