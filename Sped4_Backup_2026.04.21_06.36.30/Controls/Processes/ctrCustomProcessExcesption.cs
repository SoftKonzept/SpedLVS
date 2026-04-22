using Common.Models;
using LVS;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4.Controls.Processes
{
    public partial class ctrCustomProcessExcesption : UserControl
    {
        internal CustomProcessesViewData cpVD;
        internal CustomProcessExceptionsViewData cpeVD;
        internal GoodstypeViewData GoodsTypeVD;
        internal ctrMenu ctrMenu;
        internal CustomProcesses CustomerProcess { get; set; }
        internal CustomProcessExceptions SelectedCustomerProcessExcetption { get; set; } = new CustomProcessExceptions();
        internal List<CustomProcessExceptions> ListCustomProcessExceptionSource { get; set; } = new List<CustomProcessExceptions>();
        public ctrCustomProcessExcesption(ctrMenu myCtrMenu)
        {
            InitializeComponent();
            this.ctrMenu = myCtrMenu;
        }

        private void ctrCustomProcessExcesption_Load(object sender, EventArgs e)
        {
            InitCtr();
        }

        private void InitCtr()
        {
            ListCustomProcessExceptionSource = new List<CustomProcessExceptions>();
            SelectedCustomerProcessExcetption = new CustomProcessExceptions();
            ClearCustomerProcessValueOnCtr();
            InitDgvCustomerProcesses();
        }

        private void InitDgvCustomerProcesses()
        {
            cpVD = new CustomProcessesViewData();
            dgvCustomProcesses.DataSource = cpVD.ListCustomProcesses;

            foreach (GridViewDataColumn col in dgvCustomProcesses.Columns)
            {
                switch (col.Name)
                {
                    case "Id":
                        col.IsVisible = true;
                        //col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        //dgvCustomProcesses.Columns.Move(col.Index, 1);
                        break;
                    case "AdrId":
                        col.IsVisible = true;
                        ///dgvCustomProcesses.Columns.Move(col.Index, 2);
                        break;
                    case "ProcessName":
                        col.IsVisible = true;
                        col.HeaderText = "ProcessName";
                        ///dgvCustomProcesses.Columns.Move(col.Index, 3);
                        break;
                    case "ProcessWorkspaces":
                        col.IsVisible = true;
                        col.HeaderText = "Workspace";
                        ///dgvCustomProcesses.Columns.Move(col.Index, 3);
                        break;
                    case "IsActive":
                        col.IsVisible = true;
                        //dgvCustomProcesses.Columns.Move(col.Index, 4);
                        break;
                    case "Created":
                        col.IsVisible = true;
                        //col.FormatString = "MM.dd.yyyy";
                        //dgvCustomProcesses.Columns.Move(col.Index, 5);
                        break;
                    case "Adress":
                        col.IsVisible = true;
                        //dgvCustomProcesses.Columns.Move(col.Index, 6);
                        break;
                    default:
                        col.IsVisible = true;
                        break;
                }
                col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
            }
            dgvCustomProcesses.BestFitColumns();
        }

        private void dgvCustomProcesses_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvCustomProcesses.Rows.Count > 0)
            {
                this.dgvCustomProcesses.CurrentRow.IsSelected = true;
            }
        }

        private void dgvCustomProcesses_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dgvCustomProcesses.RowCount > 0)
            {
                if (this.dgvCustomProcesses.SelectedRows.Count > 0)
                {
                    int iTmp = 0;
                    int.TryParse(dgvCustomProcesses.SelectedRows[0].Cells["Id"].Value.ToString(), out iTmp);
                    if (iTmp > 0)
                    {
                        CustomerProcess = cpVD.ListCustomProcesses.FirstOrDefault(x => x.Id == iTmp);
                        if ((CustomerProcess != null) && (CustomerProcess.Id == iTmp))
                        {
                            ClearCustomerProcessValueOnCtr();
                            SetSelectedCustomerProcessValueToCtr();
                            InitDgvCustomerProcessException();
                        }
                    }
                }
            }
        }

        private void dgvExceptions_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvExceptions.Rows.Count > 0)
            {
                this.dgvExceptions.CurrentRow.IsSelected = true;
            }
        }

        private void dgvExceptions_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dgvExceptions.RowCount > 0)
            {
                if (this.dgvExceptions.SelectedRows.Count > 0)
                {
                    int iTmp = 0;
                    int.TryParse(dgvExceptions.SelectedRows[0].Cells["Id"].Value.ToString(), out iTmp);
                    if (iTmp > 0)
                    {
                        SelectedCustomerProcessExcetption = new CustomProcessExceptions();
                        SelectedCustomerProcessExcetption = ListCustomProcessExceptionSource.FirstOrDefault(x => x.Id == iTmp);
                    }
                }
            }
        }
        private void SetSelectedCustomerProcessValueToCtr()
        {
            if (CustomerProcess.Id > 0)
            {
                tbId.Text = CustomerProcess.Id.ToString();
                cbAktiv.Checked = CustomerProcess.IsActive;
                if (CustomerProcess.Adress is Addresses)
                {
                    this.tbAdrId.Text = CustomerProcess.AdrId.ToString();
                    this.tbVerweisADRLong.Text = CustomerProcess.Adress.AddressStringShort;
                }
                tbWorkspaceList.Text = CustomerProcess.ProcessWorkspaces.ToString();
            }
        }
        private void ClearCustomerProcessValueOnCtr()
        {
            cpVD = new CustomProcessesViewData();
            //cpVD.CustomProcess = new CustomProcesses();
            tbId.Text = string.Empty;
            cbAktiv.Checked = false;
            tbAdrId.Text = string.Empty;
            tbVerweisADRLong.Text = string.Empty;
            tbWorkspaceList.Text = string.Empty;
        }

        private void btnSearchGoodsTypes_Click(object sender, EventArgs e)
        {
            ctrMenu.OpenFrmGArtenList(this);
        }

        public void TakeOverGueterArt(decimal gaID)
        {
            if (gaID > 0)
            {
                cpeVD = new CustomProcessExceptionsViewData();
                cpeVD.CustomProcessException = new CustomProcessExceptions();
                cpeVD.CustomProcessException.CustomProcessId = CustomerProcess.Id;
                cpeVD.CustomProcessException.GoodsTypeId = (int)gaID;
                cpeVD.CustomProcessException.Created = DateTime.Now;

                cpeVD.Add();
            }
            InitDgvCustomerProcessException();
        }

        private void tsbtnCustomerProcessException_Click(object sender, EventArgs e)
        {
            InitDgvCustomerProcessException();
        }

        private void InitDgvCustomerProcessException()
        {
            ListCustomProcessExceptionSource.Clear();
            ListCustomProcessExceptionSource = CustomProcessExceptionsViewData.GetListCustomerProcessExceptionByCustomerProcessId(CustomerProcess.Id);
            dgvExceptions.DataSource = ListCustomProcessExceptionSource;

            foreach (GridViewDataColumn col in dgvExceptions.Columns)
            {

                switch (col.Name)
                {
                    case "Id":
                        col.IsVisible = true;
                        //col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        //dgvCustomProcesses.Columns.Move(col.Index, 1);
                        break;
                    case "CustomProcessId":
                        col.IsVisible = true;
                        ///dgvCustomProcesses.Columns.Move(col.Index, 2);
                        break;
                    case "ProcessName":
                        col.IsVisible = true;
                        col.HeaderText = "ProcessName";
                        ///dgvCustomProcesses.Columns.Move(col.Index, 3);
                        break;
                    case "GoodsTypeId":
                        col.IsVisible = true;
                        col.HeaderText = "GutId";
                        ///dgvCustomProcesses.Columns.Move(col.Index, 3);
                        break;
                    case "GoodsTypeName":
                        col.IsVisible = true;
                        col.HeaderText = "Gut";
                        //dgvCustomProcesses.Columns.Move(col.Index, 4);
                        break;
                    case "Created":
                        col.IsVisible = true;
                        col.FormatString = "{0:MM.dd.yyyy}";
                        //dgvCustomProcesses.Columns.Move(col.Index, 5);
                        break;
                    //case "Adress":
                    //    col.IsVisible = true;
                    //    //dgvCustomProcesses.Columns.Move(col.Index, 6);
                    //    break;
                    default:
                        col.IsVisible = false;
                        break;
                }
                col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
            }
            dgvExceptions.BestFitColumns();
        }

        private void tsbtnDeleteException_Click(object sender, EventArgs e)
        {
            if ((SelectedCustomerProcessExcetption is CustomProcessExceptions) && (SelectedCustomerProcessExcetption.Id > 0))
            {
                string Mess = string.Empty;
                Mess = "Soll der folgende Datensatz gelöscht werden?" + Environment.NewLine;
                Mess += String.Format("Ausnahme  Id : {0}", SelectedCustomerProcessExcetption.Id) + Environment.NewLine;
                Mess += String.Format("Prozess - Id : {0}", SelectedCustomerProcessExcetption.CustomProcessId) + Environment.NewLine;
                Mess += String.Format("Prozess      : {0}", SelectedCustomerProcessExcetption.CustomProcess.ProcessName) + Environment.NewLine;
                Mess += String.Format("Gut - Id     : {0}", SelectedCustomerProcessExcetption.GoodsTypeId) + Environment.NewLine;
                Mess += String.Format("Gut          : {0}", SelectedCustomerProcessExcetption.GoodsTypeName) + Environment.NewLine;
                Mess += Environment.NewLine;

                if (clsMessages.Allgemein_SelectionInfoTextShow(Mess))
                {
                    CustomProcessExceptionsViewData customProcessExceptionsViewData = new CustomProcessExceptionsViewData(SelectedCustomerProcessExcetption);
                    customProcessExceptionsViewData.Delete();
                    InitCtr();
                }
            }
            else
            {
                string Mess = "Es wurde keine Datensatz ausgewählt!";
                clsMessages.Allgemein_InfoTextShow(Mess);
            }
        }

        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitCtr();
        }
    }
}
