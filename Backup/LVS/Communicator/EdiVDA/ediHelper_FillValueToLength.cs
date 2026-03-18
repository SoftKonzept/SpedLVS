namespace LVS
{
    public class ediHelper_FillValueToLength
    {
        public static string FillValueToLength(string myVal, string myFillVal, int myFillLenght, bool myFillLeft)
        {
            if (myVal.Length < myFillLenght)
            {
                while (myVal.Length < myFillLenght)
                {
                    if (myFillLeft)
                    {
                        myVal = myFillVal + myVal;
                    }
                    else
                    {
                        myVal = myVal + myFillVal;
                    }
                }
            }
            return myVal;
        }


    }
}
