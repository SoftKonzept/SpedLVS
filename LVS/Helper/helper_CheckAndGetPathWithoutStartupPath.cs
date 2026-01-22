using System.IO;

namespace LVS
{
    public class helper_CheckAndGetPathWithoutStartupPath
    {
        public static string GetPathValueRange(string myStartupPath, string myFilePath)
        {
            string strReturn = string.Empty;
            if (!myStartupPath.Equals(string.Empty))
            {
                if (myFilePath.StartsWith(myStartupPath))
                {
                    strReturn = myFilePath.Replace(myStartupPath, "");
                }
                else
                {
                    strReturn = myFilePath;
                }
            }
            strReturn = @Path.GetDirectoryName(strReturn);
            string strEnds = "\\";
            if (!strReturn.EndsWith(strEnds))
            {
                strReturn += strEnds;
            }
            return strReturn;
        }

    }
}
