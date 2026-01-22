using System.Collections.Generic;

namespace LVS
{
    public class set_Muster
    {

        /***********************************************************************************************
         *                              SZG
         * ********************************************************************************************/

        public void InitSettings(clsClient ClientToSet)
        {
            SetModulToClient(ref ClientToSet.Modul);
            SetViewsToClient(ref ClientToSet);

            ////weitere nötige Einstellungen
            //ClientToSet.Eingang_Artikel_DefaulEinheit = "kg";
            //ClientToSet.Eingang_Artikel_DefaulAnzahl = "1";
            //ClientToSet.UserAnzahl = 10;
            //ClientToSet.AdrID = 1;  //manuell hinzugefügt
            //ClientToSet.DefaultASNParnter_Emp = 0;


            //ClientToSet.ListVDA4905Sender.Clear();
            //ClientToSet.ListVDA4905Sender.Add(17);  //AdrID von VW

            //Abrufe
            ClientToSet.DictArbeitsbereich_Abrufe_DefaulEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Abrufe_DefaultCompanyAdrID = new Dictionary<decimal, decimal>();

            //Eingänge
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Eingang_DefaultEntladeAdrID = new Dictionary<decimal, decimal>();

            //Ausgänge
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultEntladeAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultVersenderAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Ausgang_DefaultBeladeAdrID = new Dictionary<decimal, decimal>();

            //Umbuchung
            ClientToSet.DictArbeitsbereich_Umbuchung_DefaultEmpfaengerAdrID = new Dictionary<decimal, decimal>();
            ClientToSet.DictArbeitsbereich_Umbuchung_DefaultAuftraggeberNeuAdrID = new Dictionary<decimal, decimal>();
        }
        ///<summary>clsClient / SetModulToClientSZG_</summary>
        ///<remarks>SZG
        ///         Datum:
        ///         Änderungen:</remarks>
        //public static void SetModulToClient(ref clsModule myModul)
        private void SetModulToClient(ref clsModule myModul)
        {
            //Hauptmenu
            //myClient.Modul.MainMenu_Stammdaten = true;
            //myModul.MainMenu_Statistik = true;

        }
        ///<summary>clsClient / SetViewsToClientSLE_</summary>
        ///<remarks>Test für Schulung</remarks>
        //public static void SetViewsToClient(clsClient myClient)
        private void SetViewsToClient(ref clsClient myClient)
        {

        }
    }
}
