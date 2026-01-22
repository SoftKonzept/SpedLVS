using System.IO;

namespace LVS
{
    public class helper_CheckAndGetFilePathWithStartupPath
    {
        public const string const_StringToDelete = "\\";
        public static string GetPathValueRange(string myStartupPath, string myFilePath)
        {
            string strReturn = string.Empty;
            if ((!myStartupPath.Equals(string.Empty)) && (!myFilePath.Equals(string.Empty)))
            {
                // Entferne abschließende Backslashes aus myStartupPath
                myStartupPath = myStartupPath.TrimEnd(const_StringToDelete.ToCharArray());

                // Entferne führende Backslashes aus myFilePath
                myFilePath = myFilePath.TrimStart(const_StringToDelete.ToCharArray());

                if (!myFilePath.StartsWith(myStartupPath))
                {
                    strReturn = Path.Combine(myStartupPath, myFilePath);
                }
                else
                {
                    strReturn = myFilePath;
                }
            }
            //strReturn = @Path.GetDirectoryName(strReturn);
            //string strEnds = "\\";
            //if(!strReturn.EndsWith(strEnds)) 
            //{
            //    strReturn += strEnds;
            //}
            return strReturn;
        }

    }
}
