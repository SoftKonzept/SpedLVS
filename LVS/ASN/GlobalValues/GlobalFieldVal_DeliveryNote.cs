using System.Collections.Generic;
using System.Data;

namespace LVS.ASN.GlobalValues
{
    public class GlobalFieldVal_DeliveryNote
    {
        public const string const_GlobalVar_DeliveryNote = "#GlobalVar_DeliveryNote#";

        public static string GetValue(
                                       ref Dictionary<string, clsASNArtFieldAssignment> myDictASNArtFieldAssignment,
                                       List<string> myListEdiValue)
        {
            string sReturn = string.Empty;
            if (myDictASNArtFieldAssignment != null)
            {
                if (myListEdiValue.Count > 0)
                {

                }
            }
            return sReturn;
        }

        public static int Check(
                                 ref Dictionary<string, clsASNArtFieldAssignment> myDictASNArtFieldAssignment,
                                 DataTable myDt719
                                 )
        {
            DataTable dt719Tmp = myDt719.DefaultView.ToTable();
            dt719Tmp.DefaultView.RowFilter = "ASNFieldID>=" + 123;
            DataTable dt719 = dt719Tmp.DefaultView.ToTable();
            int iCount714 = 0;
            int iCount715 = 0;
            int iCount716 = 0;
            int iCount717 = 0;
            int iCount718 = 0;

            int iCountReturn = 0;

            foreach (DataRow r in dt719.Rows)
            {
                int iValue = 0;
                int.TryParse(r["Value"].ToString(), out iValue);

                switch (r["Kennung"].ToString())
                {
                    case clsASN.const_VDA4913SatzField_SATZ719F06:
                        iCount714 = iValue;
                        break;
                    case clsASN.const_VDA4913SatzField_SATZ719F07:
                        iCount715 = iValue;
                        break;
                    case clsASN.const_VDA4913SatzField_SATZ719F08:
                        iCount716 = iValue;
                        break;
                    case clsASN.const_VDA4913SatzField_SATZ719F09:
                        iCount718 = iValue;
                        break;
                    case clsASN.const_VDA4913SatzField_SATZ719F11:
                        iCount717 = iValue;
                        break;
                }
            }

            foreach (KeyValuePair<string, clsASNArtFieldAssignment> itm in myDictASNArtFieldAssignment)
            {
                if (
                        ((clsASNArtFieldAssignment)itm.Value).IsGlobalFieldVar
                        &&
                        ((clsASNArtFieldAssignment)itm.Value).GlobalFieldVar.Length > 0
                        &&
                        ((clsASNArtFieldAssignment)itm.Value).GlobalFieldVar.Equals(GlobalFieldVal_ArticleCountInEdi.const_GlobalVar_ArticleCountInEdi)
                  )
                {
                    // do something with entry.Value or entry.Key
                    switch (itm.Key)
                    {
                        case clsASN.const_VDA4913SatzField_SATZ719F01:
                        case clsASN.const_VDA4913SatzField_SATZ719F02:
                        case clsASN.const_VDA4913SatzField_SATZ719F03:
                        case clsASN.const_VDA4913SatzField_SATZ719F04:
                        case clsASN.const_VDA4913SatzField_SATZ719F05:
                            break;
                        case clsASN.const_VDA4913SatzField_SATZ719F06:
                            iCountReturn = iCount714;
                            break;
                        case clsASN.const_VDA4913SatzField_SATZ719F07:
                            iCountReturn = iCount715;
                            break;
                        case clsASN.const_VDA4913SatzField_SATZ719F08:
                            iCountReturn = iCount716;
                            break;
                        case clsASN.const_VDA4913SatzField_SATZ719F09:
                            iCountReturn = iCount718;
                            break;
                        case clsASN.const_VDA4913SatzField_SATZ719F10:
                            break;
                        case clsASN.const_VDA4913SatzField_SATZ719F11:
                            iCountReturn = iCount717;
                            break;
                        case clsASN.const_VDA4913SatzField_SATZ719F12:
                            break;
                    }
                }
            }
            return iCountReturn;
        }
    }
}
