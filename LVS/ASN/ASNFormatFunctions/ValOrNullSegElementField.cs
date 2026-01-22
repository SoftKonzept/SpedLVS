namespace LVS
{
    public class ValOrNullSegElementField
    {
        /// <summary>
        ///             ValOrNull
        /// </summary>
        public static string Execute(object myObject)
        {
            string strReturn = "NULL";
            if (myObject != null)
            {
                strReturn = myObject.ToString();
            }
            return strReturn;
        }
    }
}
