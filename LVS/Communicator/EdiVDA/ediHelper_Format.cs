namespace LVS
{
    public class ediHelper_Format
    {

        public static int GetMinLength(string myFormat, enumEdifactStatus myStatus)
        {
            int iTmp = 0;
            switch (myStatus)
            {
                //muss
                case enumEdifactStatus.M:
                    if (myFormat.Contains("an"))
                    {
                        iTmp = 1;
                    }
                    else
                    {
                        if (myFormat.Contains("a"))
                        {
                            myFormat = myFormat.Replace("a", "");
                            int.TryParse(myFormat, out iTmp);
                        }
                        else if (myFormat.Contains("n"))
                        {
                            myFormat = myFormat.Replace("n", "");
                            if (int.TryParse(myFormat, out iTmp))
                            {
                                iTmp = 0;
                            }
                        }
                    }
                    break;
                default:
                    iTmp = 0;
                    break;
            }
            return iTmp;
        }

        public static int GetMaxLength(string myFormat)
        {
            int iTmp = 0;
            if (myFormat.Contains("an"))
            {
                myFormat = myFormat.Replace("an", "");
                int.TryParse(myFormat, out iTmp);
            }
            else
            {
                if (myFormat.Contains("a"))
                {
                    myFormat = myFormat.Replace("a", "");
                    int.TryParse(myFormat, out iTmp);
                }
                else if (myFormat.Contains("n"))
                {
                    myFormat = myFormat.Replace("n", "");
                    int.TryParse(myFormat, out iTmp);
                }
                else if (myFormat.Contains("c"))
                {
                    myFormat = myFormat.Replace("c", "");
                    int.TryParse(myFormat, out iTmp);
                }
                else if (myFormat.Contains("R"))
                {
                    myFormat = myFormat.Replace("R", "");
                    int.TryParse(myFormat, out iTmp);
                }
            }
            return iTmp;
        }
    }
}
