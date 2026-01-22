using System;

namespace LVS.Helper
{
    public class helper_Queue_CreateQueueDetailString
    {
        ///<summary>Globals / List_KontoText</summary>
        ///<remarks>Liefert die Liste (Dictonary) der Kontenarten</remarks>
        public static string CreateQueueDetailString(clsQueue myQueue, string myErrorText)
        {
            return myErrorText + Environment.NewLine +
                                           "Details:" + Environment.NewLine +
                                           "ID: [ " + myQueue.ID.ToString() + " ] " + Environment.NewLine +
                                           "Tablename: [ " + myQueue.TableName + " ] " + Environment.NewLine +
                                           "TableID: [ " + myQueue.TableID.ToString() + " ] " + Environment.NewLine +
                                           "Datum: [ " + myQueue.Datum.ToString() + " ] " + Environment.NewLine +
                                           "ASNTypID: [ " + myQueue.ASNTypID.ToString() + " - " + myQueue.ASNTyp.Typ + " ]" + Environment.NewLine +
                                           "ASNID: [ " + myQueue.ASNID.ToString() + " ]" + Environment.NewLine +
                                           "AdrVerweisID: [ " + myQueue.AdrVerweisID.ToString() + " ]" + Environment.NewLine +
                                           "ASNAction: [ " + myQueue.ASNAction.ToString() + " ] " + Environment.NewLine;
        }
    }
}
