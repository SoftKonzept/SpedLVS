using LVS.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LVS.Communicator.EdiVDA
{
    public class ediHelper_EdiQuality_CheckProcessableASN
    {

        public static bool IsASNFileProcessable(clsJobs myJob, string myFilePathName)
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

                    char cSplit = constValue_Edifact.const_Edifact_UNA6_SegmentEndzeichen[0]; //  clsEdiVDA4984Read.const_Check_UNA6_SegmentEndzeichen[0];
                    List<string> tmpList = strLine.Split(new char[] { cSplit }).ToList();

                    if (
                         (tmpList != null) &&
                         (tmpList.Count > 0) &&
                         (myJob is clsJobs)
                       )
                    {
                        bool bIsQalityEdi = false;
                        bool bVersenderOK = false;

                        string strTmpCheck = constValue_Edifact.const_Edifact_QalityD96A_UNHSegment;
                        strTmp = string.Empty;
                        strTmp = tmpList.FirstOrDefault(x => x.Contains(strTmpCheck));
                        if (strTmp != null)
                        {
                            bIsQalityEdi = (strTmp.Contains(strTmpCheck));


                            bVersenderOK = true;
                        }

                        myReturn = (bIsQalityEdi) && (bVersenderOK);
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
