using LVS.Models;
using LVS.sqlStatementCreater;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;


namespace LVS.ViewData
{
    /// <summary>
    ///             clsRGPositions
    /// </summary>
    public class InvoiceItemViewData
    {
        public InvoiceItems InvoiceItem { get; set; }
        internal Invoices Invoice { get; set; }
        private int BenutzerID { get; set; } = 0;
        public Globals._GL_SYSTEM GLSystem { get; set; }
        public Globals._GL_USER GL_USER { get; set; }
        public clsSystem System { get; set; }
        public List<InvoiceItems> ListInvoiceItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public InvoiceItemViewData()
        {
            InitCls();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myUserId"></param>
        public InvoiceItemViewData(int myUserId) : this()
        {
            BenutzerID = myUserId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myInvoice"></param>
        /// <param name="myUserId"></param>
        public InvoiceItemViewData(Invoices myInvoice, int myUserId) : this()
        {
            Invoice = myInvoice.Copy();
            BenutzerID = myUserId;
            if (myInvoice.Id > 0)
            {
                //Fill();
                FillInvoiceItems();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            Invoice = new Invoices();
            ListInvoiceItems = new List<InvoiceItems>();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Fill()
        {
            if (InvoiceItem.Id > 0)
            {
                string strSQL = sqlCreater_InvoiceItem.sql_GetInvoiceItem(InvoiceItem.Id);
                DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "InvoiceItems", "InvoiceItems", BenutzerID);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SetValue(dr);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void FillInvoiceItems()
        {
            string strSQL = sqlCreater_InvoiceItem.sql_GetInvoiceItems(Invoice);
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "InvoiceItems", "InvoiceItems", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    if (!ListInvoiceItems.Contains(InvoiceItem))
                    {
                        ListInvoiceItems.Add(InvoiceItem);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        private void SetValue(DataRow row)
        {
            InvoiceItem = new InvoiceItems();
            Int32 iTmp = 0;
            Int32.TryParse(row["ID"].ToString(), out iTmp);
            InvoiceItem.Id = iTmp;
            iTmp = 0;
            Int32.TryParse(row["RGTableID"].ToString(), out iTmp);
            InvoiceItem.InvoiceId = iTmp;
            iTmp = 0;
            Int32.TryParse(row["Position"].ToString(), out iTmp);
            InvoiceItem.Position = iTmp;
            InvoiceItem.RGText = row["RGText"].ToString();
            InvoiceItem.BillingUnit = row["AbrechnungsEinheit"].ToString();
            decimal decTmp = 0M;
            decimal.TryParse(row["Menge"].ToString(), out decTmp);
            InvoiceItem.Qunatity = decTmp;
            decTmp = 0M;
            decimal.TryParse(row["EinzelPreis"].ToString(), out decTmp);
            InvoiceItem.UnitPrice = decTmp;
            decTmp = 0M;
            decimal.TryParse(row["NettoPreis"].ToString(), out decTmp);
            InvoiceItem.NetAmount = decTmp;
            InvoiceItem.BillingType = row["AbrechnungsArt"].ToString();
            iTmp = 0;
            Int32.TryParse(row["TarifPosID"].ToString(), out iTmp);
            InvoiceItem.TarifPosId = iTmp;
            InvoiceItem.TarifText = row["Tariftext"].ToString();
            decTmp = 0M;
            decimal.TryParse(row["MargeEuro"].ToString(), out decTmp);
            InvoiceItem.MarginEuro = decTmp;
            decTmp = 0M;
            decimal.TryParse(row["MargeProzent"].ToString(), out decTmp);
            InvoiceItem.MarginRate = decTmp;
            decTmp = 0M;
            decimal.TryParse(row["Anfangsbestand"].ToString(), out decTmp);
            InvoiceItem.InventoryStart = decTmp;
            decTmp = 0M;
            decimal.TryParse(row["Abgang"].ToString(), out decTmp);
            InvoiceItem.InventoryOutgoing = decTmp;
            decTmp = 0M;
            decimal.TryParse(row["Zugang"].ToString(), out decTmp);
            InvoiceItem.InventoryAccess = decTmp;
            decTmp = 0M;
            decimal.TryParse(row["Endbestand"].ToString(), out decTmp);
            InvoiceItem.InventoryEnd = decTmp;
            InvoiceItem.InvoiceItemText = row["RGPosText"].ToString();
            iTmp = 0;
            Int32.TryParse(row["FibuKto"].ToString(), out iTmp);
            InvoiceItem.FibuAccount = iTmp;
            iTmp = 0;
            Int32.TryParse(row["CalcModus"].ToString(), out iTmp);
            InvoiceItem.CalcModus = EnumConverter.GetEnumObjectByValue<enumCalcultationModus>(iTmp);
            iTmp = 0;
            Int32.TryParse(row["CalcModValue"].ToString(), out iTmp);
            InvoiceItem.CalcModValue = iTmp;
            decTmp = 0M;
            decimal.TryParse(row["PricePerUnitFactor"].ToString(), out decTmp);
            InvoiceItem.PricePerUnitFactor = decTmp;

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

