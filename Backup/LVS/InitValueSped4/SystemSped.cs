using LVS.InitValue;

namespace LVS.InitValueSped4
{
    public class SystemSped
    {
        public SystemSped()
        {
            // Constructor logic if needed
        }


        //public string Matchcode_Client => SystemSped_InitValue_Client.Matchcode();


        //------------------------------------------------------------------------------ Default Settings
        public string DefaultPath_LVS => DefaultPath_Lvs.DefaultPath();
        public string DefaultPath_LVS_Export => DefaultPath_LvsExport.DefaultPath();


        //------------------------------------------------------------------------------- Voreinstellungen - VE System
        /// <summary>
        ///             ON/OFF Protokollierung der eRechnungsvorgänge incl. Mailversand an Support
        /// </summary>
        public bool System_VE_eInvoiceLogActivatet
        {
            get { return InitValue.InitValue_System_VE_eInvoiceLogActivatet.Value(); }
        }
    }
}
