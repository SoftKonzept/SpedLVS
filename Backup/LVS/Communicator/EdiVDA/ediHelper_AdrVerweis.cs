using System;

namespace LVS
{
    public class ediHelper_AdrVerweis
    {

        public static clsADRVerweis GetAdrVerweis4913(clsASN myASN)
        {
            clsADRVerweis adrverweis = new clsADRVerweis();
            string strSenderVerweis = string.Empty;
            if (myASN is clsASN)
            {
                if (myASN.ASNArt.asnSatz.ListASNValue.Count > 0)
                {
                    string s711F03 = string.Empty;
                    string s711F04 = string.Empty;
                    string s712F04 = string.Empty;
                    string s713F13 = string.Empty;

                    string s511F04 = string.Empty;
                    string s511F03 = string.Empty;

                    for (Int32 i = 0; i <= myASN.ASNArt.asnSatz.ListASNValue.Count - 1; i++)
                    {
                        clsASNValue tmpASNVal = (clsASNValue)myASN.ASNArt.asnSatz.ListASNValue[i];
                        switch (tmpASNVal.Kennung)
                        {
                            case clsASN.const_VDA4905SatzField_SATZ511F03:
                                s511F03 = tmpASNVal.Value.ToString().Trim();
                                break;
                            case clsASN.const_VDA4905SatzField_SATZ511F04:
                                s511F04 = tmpASNVal.Value.ToString().Trim();
                                strSenderVerweis = s511F04 + "#" + s511F03;
                                break;

                            case clsASN.const_VDA4913SatzField_SATZ711F03:
                                s711F03 = tmpASNVal.Value.ToString().Trim();
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ711F04:
                                s711F04 = tmpASNVal.Value.ToString().Trim();
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ712F04:
                                s712F04 = tmpASNVal.Value.ToString().Trim();
                                strSenderVerweis = s711F04 + "#" + s711F03 + "#" + s712F04;
                                break;
                            case clsASN.const_VDA4913SatzField_SATZ712F05:
                                //Abbruch - Schleife, da alle notwendigen Informationen ermittelt wurden
                                i = myASN.ASNArt.asnSatz.ListASNValue.Count;
                                break;

                                //case clsASN.const_VDA4913SatzField_SATZ713F13:
                                //    s713F13 = tmpASNVal.Value.ToString().Trim();
                                //    strReceiverVerweis = s711F03 + "#" + s711F04 + "#" + s713F13;
                                //    i = this.ASNArt.asnSatz.ListASNValue.Count;
                                //    break;
                        }
                    }
                }
            }
            adrverweis.FillClassByVerweis(strSenderVerweis, myASN.Job.ASNFileTyp);
            return adrverweis;
        }
    }
}
