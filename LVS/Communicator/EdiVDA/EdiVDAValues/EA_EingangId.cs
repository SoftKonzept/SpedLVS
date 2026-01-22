namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class EA_EingangId
    {
        public const string const_EA_EingangId = "EA.EingangId";

        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = "0";
            if (myLager.Eingang is clsLEingang)
            {
                strTmp = myLager.Eingang.LEingangID.ToString("000000");
            }
            return strTmp;
        }
    }
}
