using System;

namespace LVS.Helper
{
    public class helper_VDALogText
    {
        public static string VDACreate(clsVDACreate myVDACreate)
        {
            string strReturn = string.Empty;

            strReturn += "[Task_WriteVDA].[VDA4913].[VDACreate]: " + Environment.NewLine;
            strReturn += "Table: " + myVDACreate.tmpLog.TableName + Environment.NewLine;
            strReturn += "TableID: " + myVDACreate.tmpLog.TableID.ToString() + Environment.NewLine;
            strReturn += "Queue [Id/Table/TableID]:  [" + myVDACreate.ASN.Queue.ID.ToString() + "/" + myVDACreate.ASN.Queue.TableName + "/" + myVDACreate.ASN.Queue.TableID.ToString() + "]" + Environment.NewLine;
            strReturn += "Auftraggeber: [" + myVDACreate.ASN.Job.ADR.ID.ToString() + "] - " + myVDACreate.ASN.Job.ADR.ADRStringShort + " " + Environment.NewLine;
            strReturn += "Job [JobID/TypId/Typ]: [" + myVDACreate.ASN.Job.ID.ToString() + "] - [" + myVDACreate.ASN.Job.AsnTyp.TypID.ToString() + "] - [" + myVDACreate.ASN.Job.AsnTyp.Typ + "]" + Environment.NewLine;

            strReturn += Environment.NewLine;
            return strReturn;
        }
    }
}
