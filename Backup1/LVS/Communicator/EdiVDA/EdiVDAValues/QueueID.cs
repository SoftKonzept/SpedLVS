namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class QueueID
    {
        public const string const_SystemId_Queue = "#QueueId#";

        public static string Execute(clsASN myASN)
        {
            string strTmp = string.Empty;
            if (myASN is clsASN)
            {
                if ((myASN.Queue is clsQueue) && (myASN.Queue.ID > 0))
                {
                    strTmp = myASN.Queue.ID.ToString();
                }
            }
            return strTmp;
        }
    }
}
