using System.Windows.Forms;

namespace LVS.Helper
{
    public class helper_MandantenReportPath
    {
        public static string GetPath(clsMandanten myMandant)
        {
            string retVal = string.Empty;
            retVal = Application.StartupPath;
            //if (!retVal.EndsWith("\\"))
            //{
            //    retVal += "\\";
            //}

            if (myMandant.ReportPath.Equals(string.Empty))
            {
                retVal = retVal + "\\Reports";
                //if (!retVal.EndsWith("\\"))
                //{
                //    retVal += "\\";
                //}
            }
            else
            {
                if (myMandant.ReportPath.StartsWith("\\"))
                {
                    retVal = retVal + myMandant.ReportPath;
                }
                else
                {
                    retVal = retVal + "\\" + myMandant.ReportPath;
                }

                //if (!retVal.Equals(string.Empty))
                //{
                //    if (!retVal.EndsWith("\\"))
                //    {
                //        retVal += "\\";
                //    }
                //}
            }
            return retVal;
        }
    }
}
