namespace LVS.Clients
{
    public class szg_ctrEinlagerung_SetInputFieldReadOnly
    {
        //internal const int const_Arbeitsbereich_VW = 1;
        //internal const int const_Arbeitsbereich_BMW = 5;

        //public static void Execute(ref System.Windows.Forms.TextBox textbox)
        //{
        //    switch (textbox.Name)
        //    {
        //        //case "tbGArtZusatz":
        //        case "tbPos":
        //        case "tbexMaterialnummer":
        //        //case "tbexBezeichnung":
        //        //case "tbHoehe":
        //        //case "tbPackmittelGewicht":
        //        //case "tbExLagerOrt":
        //        //case "tbexAuftrag":
        //            textbox.ReadOnly = true;
        //            textbox.BackColor = System.Drawing.SystemColors.Control;
        //            break;
        //        default:
        //            textbox.ReadOnly = false;
        //            textbox.BackColor = System.Windows.Forms.TextBox.DefaultBackColor;
        //            break;
        //    }
        //    //textbox.ReadOnly = false;
        //    //textbox.BackColor = System.Windows.Forms.TextBox.DefaultBackColor;
        //}

        public static bool Execute(string myTbName)
        {
            //textbox.ReadOnly = true;
            //textbox.BackColor = System.Windows.Forms.TextBox.DefaultBackColor;
            bool bReturn = false;
            switch (myTbName)
            {
                case "tbGArtZusatz":
                case "tbPos":
                //case "tbexMaterialnummer":
                case "tbexBezeichnung":
                case "tbHoehe":
                case "tbPackmittelGewicht":
                case "tbExLagerOrt":
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
