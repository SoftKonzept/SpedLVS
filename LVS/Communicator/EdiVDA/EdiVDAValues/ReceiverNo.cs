namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class ReceiverNo
    {
        public const string const_ReceiverNo = "#ReceiverNo#";

        public static string Execute(Globals._GL_USER myGLUser, clsASN myASN)
        {
            string strTmp = string.Empty;
            string sql = "select SenderVerweis from ADRVerweis " +
                                                        "where VerweisAdrID=" + (int)myASN.Job.AdrVerweisID +
                                                             " AND SenderAdrID=" + (int)myASN.Job.AdrVerweisID +
                                                             " AND ArbeitsbereichID= " + (int)myASN.Job.ArbeitsbereichID;
            strTmp = LVS.clsSQLcon.ExecuteSQL_GetValue(sql, myGLUser.User_ID);
            return strTmp;
        }
    }
}
