namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class EA_AusgangId
    {
        public const string const_EA_AusgangId = "EA.AusgangId";

        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = "0";
            if (myLager.Ausgang is clsLAusgang)
            {
                strTmp = myLager.Ausgang.LAusgangID.ToString("000000");
            }
            return strTmp;
        }
    }
}
