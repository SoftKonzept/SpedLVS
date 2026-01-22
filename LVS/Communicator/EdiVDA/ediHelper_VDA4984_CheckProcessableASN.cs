using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LVS
{

    /// <summary>
    ///             VW sendet alle ASN an SZG / SIL, auch die, die nicht für SZG oder SIL bestimmt sind,
    ///             Deshalb muss jedes File erst daraufhin geprüft werden, ob es für SZG oder SIL auch 
    ///             bestimmt ist.
    ///             Kriterien:
    ///             
    /// </summary>
    public class ediHelper_VDA4984_CheckProcessableASN
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
                        bool bIsLE = false;
                        bool bVersenderOK = false;

                        string strTmpCheck = clsEdiVDA4984Read.const_Check_BGM236_LFE;
                        strTmp = string.Empty;
                        strTmp = tmpList.FirstOrDefault(x => x.Contains(strTmpCheck));
                        if (strTmp != null)
                        {
                            bIsLE = (strTmp.Contains(strTmpCheck));
                        }

                        string strVersender = clsEdiVDA4984Read.const_Check_NAD_Versender + myJob.VerweisVDA4905;
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
                        myReturn = (bIsLE) && (bVersenderOK);
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
