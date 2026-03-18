namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class EA_AnzahlX1000
    {
        /// <summary>
        /// </summary>
        public const string const_EA_AnzahlX1000 = "#EA_AnzahlX1000#";

        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = EA_Anzahl.Execute(myAsnTyp, myLager);
            int iSum = 0;
            int.TryParse(strTmp, out iSum);
            iSum = iSum * 1000;
            return iSum.ToString();
        }
    }
}
