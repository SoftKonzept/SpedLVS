namespace LVS.Clients
{
    public class sil_ctrEinlagerung_SetInputFieldReadOnly
    {
        /// <summary>
        ///             tbexAuftrag -> Bestellnummer von TATA gespeichert und wird für EDI gebraucht
        /// </summary>
        //    public static void Execute(ref System.Windows.Forms.TextBox textbox)
        //    {
        //        switch (textbox.Name)
        //        {
        //            case "tbexAuftrag":
        //                textbox.ReadOnly = true;
        //                textbox.BackColor = System.Drawing.SystemColors.Control;
        //                break;
        //            default:
        //                textbox.ReadOnly = false;
        //                textbox.BackColor = System.Windows.Forms.TextBox.DefaultBackColor;
        //                break;
        //        }
        //    }
        //}
        public static bool Execute(string myTbName)
        {
            //textbox.ReadOnly = true;
            //textbox.BackColor = System.Windows.Forms.TextBox.DefaultBackColor;
            bool bReturn = false;
            switch (myTbName)
            {
                case "tbexAuftrag":
                    bReturn = true;
                    //textbox.BackColor = System.Drawing.SystemColors.Control;
                    break;
                default:
                    bReturn = false;
                    //textbox.BackColor = System.Windows.Forms.TextBox.DefaultBackColor;
                    break;
            }
            return bReturn;
        }
    }
}
