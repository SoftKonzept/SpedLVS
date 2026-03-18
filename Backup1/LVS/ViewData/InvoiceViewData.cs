using Common.Models;
using LVS.Models;
using LVS.sqlStatementCreater;
using LVS.ZUGFeRD;
using System;
using System.Data;
using DataTable = System.Data.DataTable;


namespace LVS.ViewData
{
    public class InvoiceViewData
    {
        public Invoices Invoice { get; set; }
        private int BenutzerID { get; set; } = 0;
        public Globals._GL_SYSTEM GLSystem { get; set; }
        public Globals._GL_USER GL_USER { get; set; }
        public clsSystem System { get; set; }

        public MandantenViewData mandantenVD { get; set; }
        public AddressViewData adrVD { get; set; }

        //-- Invoice Empfänger
        public Addresses AdrReceiver { get; set; }
        //-- Invoice Ersteller / Mandant
        public Addresses AdrClient { get; set; }

        public InvoiceItemViewData InvoiceItemVD { get; set; }

        public ZUGFeRD_IsAvailable ZugferdCheck { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public InvoiceViewData()
        {
            InitCls();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myUserId"></param>
        public InvoiceViewData(int myUserId) : this()
        {
            BenutzerID = myUserId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myInvoice"></param>
        /// <param name="myUserId"></param>
        public InvoiceViewData(Invoices myInvoice, int myUserId) : this()
        {
            Invoice = myInvoice.Copy();
            BenutzerID = myUserId;
            if (Invoice.Id > 0)
            {
                FillClass();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myInvoiceId"></param>
        /// <param name="myUserId"></param>
        public InvoiceViewData(int myInvoiceId, int myUserId) : this()
        {
            Invoice.Id = myInvoiceId;
            BenutzerID = myUserId;
            if (Invoice.Id > 0)
            {
                FillClass();
            }
        }
        public InvoiceViewData(int myInvoiceNo, int myUserId, bool FillAll = true) : this()
        {
            if (myInvoiceNo > 0)
            {
                Invoice.InvoiceNo = myInvoiceNo;
                BenutzerID = myUserId;
                FillByInvoiceNo();
            }
        }

        private void FillClass()
        {
            Fill();
            InvoiceItemVD = new InvoiceItemViewData(Invoice, BenutzerID);

            //-- Adressen
            adrVD = new AddressViewData(Invoice.Receiver, BenutzerID);
            AdrReceiver = adrVD.Address.Copy();
            mandantenVD = new MandantenViewData(Invoice.ClientId);
            //AdrClient = mandantenVD.Mandant.Copy();

            ZugferdCheck = new ZUGFeRD_IsAvailable(mandantenVD.Mandant, AdrReceiver);
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            Invoice = new Invoices();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Fill()
        {
            string strSQL = sqlCreater_Invoice.sql_GetInvoice(Invoice.Id);
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Invoice", "Invoice", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        public void FillByInvoiceNo()
        {
            string strSQL = sqlCreater_Invoice.sql_GetInvoice(Invoice.Id);
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "Invoice", "Invoice", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        private void SetValue(DataRow row)
        {
            Invoice = new Invoices();
            Int32 iTmp = 0;
            Int32.TryParse(row["ID"].ToString(), out iTmp);
            Invoice.Id = iTmp;
            iTmp = 0;
            Int32.TryParse(row["RGNr"].ToString(), out iTmp);
            Invoice.InvoiceNo = iTmp;
            DateTime dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["Datum"].ToString(), out dtTmp);
            Invoice.Datum = dtTmp;
            dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["faellig"].ToString(), out dtTmp);
            Invoice.DueDate = dtTmp;
            decimal decTmp = 0M;
            decimal.TryParse(row["MwStSatz"].ToString(), out decTmp);
            Invoice.Vat = decTmp;
            decTmp = 0M;
            decimal.TryParse(row["MwStBetrag"].ToString(), out decTmp);
            Invoice.VatRate = decTmp;
            decTmp = 0M;
            decimal.TryParse(row["NettoBetrag"].ToString(), out decTmp);
            Invoice.NetAmount = decTmp;
            decTmp = 0M;
            decimal.TryParse(row["BruttoBetrag"].ToString(), out decTmp);
            Invoice.GrossAmount = decTmp;
            bool bTmp = false;
            bool.TryParse(row["Storno"].ToString(), out bTmp);
            Invoice.IsCancelation = bTmp;
            bTmp = false;
            bool.TryParse(row["GS"].ToString(), out bTmp);
            if (bTmp)
            {
                Invoice.IsInvoice = false;
            }
            else
            {
                Invoice.IsInvoice = true;
            }
            dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["Bezahlt"].ToString(), out dtTmp);
            Invoice.Paid = dtTmp;
            bTmp = false;
            bool.TryParse(row["Druck"].ToString(), out bTmp);
            Invoice.IsPrinted = bTmp;
            dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["Druckdatum"].ToString(), out dtTmp);
            Invoice.PrintDate = dtTmp;
            Invoice.InvoiceType = row["RGArt"].ToString();
            iTmp = 0;
            Int32.TryParse(row["MandantenID"].ToString(), out iTmp);
            Invoice.ClientId = iTmp;
            if (Invoice.ClientId > 0)
            {
                mandantenVD = new MandantenViewData(Invoice.ClientId);
                Invoice.Client = mandantenVD.Mandant;
            }

            iTmp = 0;
            Int32.TryParse(row["ArBereichID"].ToString(), out iTmp);
            Invoice.WorkspaceId = iTmp;
            if (Invoice.WorkspaceId > 0)
            {
                WorkspaceViewData wVD = new WorkspaceViewData(Invoice.WorkspaceId);
                Invoice.Workspace = wVD.Workspace;
            }
            bTmp = false;
            bool.TryParse(row["exFibu"].ToString(), out bTmp);
            Invoice.ExFibu = bTmp;
            dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["AbrZeitraumVon"].ToString(), out dtTmp);
            Invoice.BillingPeriodStart = dtTmp;
            dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["AbrZeitraumBis"].ToString(), out dtTmp);
            Invoice.BillingPeriodEnd = dtTmp;
            iTmp = 0;
            Int32.TryParse(row["Empfaenger"].ToString(), out iTmp);
            Invoice.Receiver = iTmp;
            if (Invoice.Receiver > 0)
            {
                AddressViewData aVD = new AddressViewData(Invoice.Receiver, 1);
                Invoice.AdrReceiver = aVD.Address;
            }


            iTmp = 0;
            Int32.TryParse(row["StornoID"].ToString(), out iTmp);
            Invoice.StornoId = iTmp;
            Invoice.TarifName = row["AbrTarifName"].ToString();
            decTmp = 0M;
            decimal.TryParse(row["VersPraemie"].ToString(), out decTmp);
            Invoice.InsuranceRate = decTmp;
            dtTmp = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["RGBookPrintDate"].ToString(), out dtTmp);
            Invoice.InvoiceBookPrintDate = dtTmp;
            Invoice.InfoText = row["InfoText"].ToString();
            Invoice.FibuInfo = row["FibuInfo"].ToString();
            Invoice.DocName = row["DocName"].ToString();
        }
        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            //string strSql = sql_Add;
            //strSql = strSql + "Select @@IDENTITY as 'ID';";

            //string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Insert", BenutzerID);
            //int.TryParse(strTmp, out int iTmp);
            //Abruf.Id = iTmp;
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public bool Delete()
        {
            bool bReturn = false;
            //if (this.Abruf.Id > 0)
            //{
            //    string strSql = string.Empty;
            //    strSql = sql_Delete;
            //    bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "DELETE", BenutzerID);
            //}
            return bReturn;
        }


    }
}

