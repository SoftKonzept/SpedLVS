using Common.Models;
using LVS.Constants;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.IO;

namespace LVS.Communicator.EdiVDA
{
    public class ediHelper_EdiXML_ZQMQALITY02_CheckProcessable
    {
        public static bool IsXMLFileProcessable(clsJobs myJob, string myFilePathName)
        {
            bool myReturn = false;
            string strLine = string.Empty;
            string strTmp = string.Empty;
            try
            {
                if (File.Exists(myFilePathName))
                {
                    using (StreamReader sr = new StreamReader(myFilePathName))
                    {
                        strLine = sr.ReadToEnd().Replace(Environment.NewLine, "");
                    }

                    if (strLine.Length > 0)
                    {
                        ZQM.ZQM_Quality02 zqm = new ZQM.ZQM_Quality02(strLine);
                        if (zqm.iRCVPRN.Length > 0)
                        {
                            Dictionary<string, AddressReferences> dict = AddressReferenceViewData.FillDictAdrVerweisAll(1, constValue_AsnArt.const_Art_XML_ZQM_QALITY02.ToString());
                            if (dict.ContainsKey(zqm.iRCVPRN))
                            {
                                AddressReferences adrRef = new AddressReferences();
                                dict.TryGetValue(zqm.iRCVPRN, out adrRef);
                                myReturn = (adrRef.Id > 0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Fehlermeldung
                string strEx = ex.ToString();
            }
            return myReturn;
        }
    }
}
