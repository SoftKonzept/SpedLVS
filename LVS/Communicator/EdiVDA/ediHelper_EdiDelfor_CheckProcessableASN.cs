using LVS.Communicator.EdiVDA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LVS
{

    /// <summary>
    ///             novelis / Benteler
    ///             
    /// </summary>
    public class ediHelper_EdiDelfor_CheckProcessableASN
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

                    char cSplit = clsEdiVDA4984Read.const_Check_UNA6_SegmentEndzeichen[0];
                    List<string> tmpList = strLine.Split(new char[] { cSplit }).ToList();

                    if (
                         (tmpList != null) &&
                         (tmpList.Count > 0) &&
                         (myJob is clsJobs)
                       )
                    {
                        bool bIsPS = false;
                        bool bVersenderOK = false;

                        string strTmpCheck = clsEdiDelforD97A_Read.const_Check_BGM236_PS;
                        strTmp = string.Empty;
                        strTmp = tmpList.FirstOrDefault(x => x.Contains(strTmpCheck));
                        if (strTmp != null)
                        {
                            bIsPS = (strTmp.Contains(strTmpCheck));
                        }

                        string strVersender = "NAD+SU+" + myJob.DelforVerweis;
                        strTmp = string.Empty;
                        strTmp = tmpList.FirstOrDefault(x => x.Contains(strVersender));
                        if (strTmp != null)
                        {
                            bVersenderOK = (strTmp.Contains(strVersender));
                        }
                        else
                        {
                            bVersenderOK = false;
                        }
                        myReturn = (bIsPS) && (bVersenderOK);
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
