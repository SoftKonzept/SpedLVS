using System.Collections.Generic;

namespace LVS.ASN.GlobalValues
{
    public class GlobalFieldVal_ListVar
    {
        public static List<string> ListVar()
        {
            List<string> list = new List<string>();
            list.Add(GlobalFieldVal_ArticleCountInEdi.const_GlobalVar_ArticleCountInEdi);
            list.Add(GlobalFieldVal_DeliveryNote.const_GlobalVar_DeliveryNote);
            return list;
        }
    }
}
