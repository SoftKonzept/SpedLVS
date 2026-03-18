using Common.Models;
using System.Data;
using static LVS.Globals;

namespace LVS.ViewData
{
    public class WarehouseViewData
    {
        /// <summary>
        ///             angelehnt an die Klasse clsLager aus LVS
        /// </summary>
        /// 

        //public const Int32 const_ASNAction_Eingang = 1;
        //public const string const_ASNActionName_Eingang = "Eingang";
        //public const Int32 const_ASNAction_Ausgang = 2;
        //public const string const_ASNActionName_Ausgang = "Ausgang";
        //public const Int32 const_ASNAction_StornoKorrektur = 3;
        //public const string const_ASNActionName_StornoKorrektur = "Korrektur- Stornierverfahren";
        //public const Int32 const_ASNAction_RücklieferungSL = 4;
        //public const string const_ASNActionName_Rücklieferung = "Rücklieferung";
        //public const Int32 const_ASNAction_SPLIn = 5;
        //public const string const_ASNActionName_SPLIn = "SPL IN";
        //public const Int32 const_ASNAction_SPLOut = 6;
        //public const string const_ASNActionName_SPLOut = "SPL OUT";
        //public const Int32 const_ASNAction_Umbuchung = 7;
        //public const string const_ASNActionName_Umbuchung = "Umbuchung";

        public LVS.Globals._GL_USER GLUser = new _GL_USER();
        public int ArticleId { get; set; } = 0;
        public int EingangTableId { get; set; } = 0;
        public int AusgangTableId { get; set; } = 0;
        private int BenutzerID { get; set; } = 0;
        public int asnActionProcessId { get; set; } = 0;
        public ArticleViewData articleViewData { get; set; }
        public AusgangViewData ausgangViewData { get; set; }
        public EingangViewData eingangViewData { get; set; }
        //public WorkspaceViewData workspaceViewData { get; set; }
        public Workspaces Workspace { get; set; }
        //public clsASNAction asnAction { get; set; }
        //public clsASNTransfer asnTransfer { get; set; }



        //public int Id { get; set; } = 0;
        //public string ASNActionName { get; set; }
        //public int Auftraggeber { get; set; } = 0;
        //public int Empfaenger { get; set; } = 0;
        //public int ASNTypID { get; set; } = 0;
        //public int OrderID { get; set; } = 0;
        //public int MandantenID { get; set; } = 0;
        //public int AbBereichID { get; set; } = 0;
        //public string Bemerkung { get; set; }
        //public bool activ { get; set; }
        //public bool IsVirtFile { get; set; }
        //public bool UseOldPropertyValue { get; set; }


        //public Dictionary<decimal, clsASNAction> DictASNAction;
        //public Dictionary<decimal, string> DictASNActionASNTyp;
        //public List<int> ListASNActionASNTypActiv = new List<int>();

        public WarehouseViewData(int myArticleId, int myEingangId, int myAusgangId, int myUserId, Workspaces myWorkspace, int myAsnActionProcessId)
        {
            BenutzerID = myUserId;
            ArticleId = myArticleId;
            EingangTableId = myEingangId;
            AusgangTableId = myAusgangId;
            Workspace = myWorkspace;
            asnActionProcessId = myAsnActionProcessId;
        }
        //public WarehouseViewData(Articles myArticle, int myUserId, int myAsnActionProcessId)
        //{
        //    BenutzerID = myUserId;
        //    GLUser.User_ID = BenutzerID;
        //    asnActionProcessId = myAsnActionProcessId;
        //    articleViewData = new ArticleViewData(myArticle, BenutzerID);
        //    eingangViewData = new EingangViewData(articleViewData.Artikel.LEingangTableID, BenutzerID, true);
        //    Workspace = eingangViewData.workspaceViewData.Workspace.Copy();
        //    if (articleViewData.Artikel.LAusgangTableID > 0)
        //    {
        //        ausgangViewData = new AusgangViewData(articleViewData.Artikel.LAusgangTableID, BenutzerID, true);
        //    }
        //    InitCls();
        //}
        //public WarehouseViewData(Eingaenge myEingang, int myUserId, int myAsnActionProcessId)
        //{
        //    BenutzerID = myUserId;
        //    GLUser.User_ID = BenutzerID;
        //    asnActionProcessId = myAsnActionProcessId;
        //    eingangViewData = new EingangViewData(myEingang, BenutzerID);
        //    Workspace = eingangViewData.Eingang.Workspace.Copy();
        //    InitCls();
        //}
        //public WarehouseViewData(Ausgaenge myAusgang, int myUserId, int myAsnActionProcessId)
        //{
        //    BenutzerID = myUserId;
        //    GLUser.User_ID = BenutzerID;
        //    asnActionProcessId = myAsnActionProcessId;
        //    ausgangViewData = new AusgangViewData(myAusgang, BenutzerID);
        //    Workspace = ausgangViewData.Ausgang.Workspace.Copy();
        //    InitCls();
        //}
        private void InitCls()
        {
            //asnAction = new clsASNAction();
            ////asnAction.InitClassByAction()

            //switch (asnActionProcessId)
            //{
            //    case clsASNAction.const_ASNAction_Eingang:
            //    case clsASNAction.const_ASNAction_StornoKorrektur:
            //    case clsASNAction.const_ASNAction_SPLIn:
            //    case clsASNAction.const_ASNAction_SPLOut:
            //    case clsASNAction.const_ASNAction_RücklieferungSL:
            //    case clsASNAction.const_ASNAction_Umbuchung:
            //        //this.ASNActionProcessNr = iAction;
            //        this.Auftraggeber = eingangViewData.Eingang.Auftraggeber;
            //        this.Empfaenger = eingangViewData.Eingang.Empfaenger;
            //        this.MandantenID = eingangViewData.Eingang.MandantenId;
            //        this.AbBereichID = eingangViewData.Eingang.ArbeitsbereichId;
            //        FillDictASNAction();
            //        break;

            //    case clsASNAction.const_ASNAction_Ausgang:
            //        //this.ASNActionProcessNr = iAction;
            //        this.Auftraggeber = ausgangViewData.Ausgang.Auftraggeber;
            //        this.Empfaenger = ausgangViewData.Ausgang.Empfaenger;
            //        this.MandantenID = ausgangViewData.Ausgang.MandantenID;
            //        this.AbBereichID = ausgangViewData.Ausgang.ArbeitsbereichId;
            //        FillDictASNAction();
            //        break;
            //}
        }

        public void FillDictASNAction()
        {
            //DictASNAction = new Dictionary<decimal, clsASNAction>();
            //DictASNActionASNTyp = new Dictionary<decimal, string>();
            //ListASNActionASNTypActiv = new List<int>();
            //DataTable dt = new DataTable("ASNAction");
            //string strSQL = sqlCreater_asn_FillDictASNAction.sqlString(this.asnActionProcessId
            //                                                    , (Int32)this.Auftraggeber
            //                                                    , (Int32)this.Empfaenger
            //                                                    , (Int32)this.MandantenID
            //                                                    , (Int32)this.AbBereichID);

            //dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, this.BenutzerID, "ASNAction");
            //for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            //{
            //    decimal decTmp = 0;
            //    Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
            //    string strTyp = dt.Rows[i]["Typ"].ToString();
            //    if (decTmp > 0)
            //    {
            //        clsASNAction tmpAction = new clsASNAction();
            //        tmpAction.GL_User = GLUser;
            //        tmpAction.ID = decTmp;
            //        tmpAction.Fill();

            //        if (!this.DictASNAction.ContainsKey(tmpAction.ID))
            //        {
            //            this.DictASNAction.Add(tmpAction.ID, tmpAction);
            //        }
            //        if (!this.DictASNActionASNTyp.ContainsKey(tmpAction.ASNTypID))
            //        {
            //            this.DictASNActionASNTyp.Add(tmpAction.ASNTypID, strTyp);
            //        }
            //        if (!ListASNActionASNTypActiv.Contains((int)tmpAction.ASNTypID))
            //        {
            //            ListASNActionASNTypActiv.Add((int)tmpAction.ASNTypID);
            //        }
            //    }
            //}
        }

        public void CreateLM()
        {
            //asnTransfer = new clsASNTransfer();
            //asnTransfer.QueueDescription = tbQueueDescription.Text.Trim();
            //asnTransfer.MessageForSupporter = this.cbMesToSupporter.Checked;
            //asnTransfer.MessageForReceiver = this.cbMesToReceiver.Checked;

            //asnTransfer.CreateLM();

        }

        private void SetValue(DataRow row)
        {
        }

        public void GetOpenStoreOutList()
        {

        }
    }
}

