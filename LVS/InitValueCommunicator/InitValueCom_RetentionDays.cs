namespace LVS.InitValueCommunicator
{
    public class InitValueCom_RetentionDays
    {
        public const string const_Section = "COMMUNICATOR";

        public const string const_Key_Matchcode = "RETENTIONDAYS";

        /// <summary>
        ///             Angabe Tage in int
        /// </summary>
        /// <param name="myClient"></param>
        /// <returns></returns>
        public static int Value()
        {
            string strClient = InitValue.InitValue_Client.Value();
            int iDays = 30;
            string strReturn = string.Empty;
            clsINI.clsINI ini = GlobalINI.GetINI();
            string tmpSection = strClient + const_Section;

            if (ini.SectionNames().Contains(tmpSection))
            {
                if (ini.ReadString(tmpSection, const_Key_Matchcode) != null)
                {
                    strReturn = ini.ReadString(tmpSection, const_Key_Matchcode);
                }
            }
            int.TryParse(strReturn, out iDays);
            if (iDays < 30)
            {
                iDays = 30; // Reset to default if invalid
            }
            return iDays;
        }

    }
}
