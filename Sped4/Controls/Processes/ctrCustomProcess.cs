using Common.Models;
using LVS;
using LVS.Constants;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Sped4.Controls.Processes
{
    public partial class ctrCustomProcess : UserControl
    {
        public Globals._GL_USER GLUser;
        public ctrMenu _ctrMenu;

        internal DataTable dtSourceWorkspace;
        internal CustomProcessesViewData cpVD;
        internal CustomProcesses CustomProcess;
        internal Addresses SelectedAdr;
        public int SearchButton = 1;

        public ctrCustomProcess(ctrMenu myMenu)
        {
            InitializeComponent();
            this._ctrMenu = myMenu;
        }

        private void ctrCustomProcess_Load(object sender, EventArgs e)
        {

        }

        public void InitCtr()
        {
            CustomProcess = new CustomProcesses();
            dtSourceWorkspace = new DataTable();
            dtSourceWorkspace = clsArbeitsbereiche.GetArbeitsbereichList(this.GLUser.User_ID);

            this.CheckListWorkspace.DataSource = dtSourceWorkspace;
            this.CheckListWorkspace.DisplayMember = "Arbeitsbereich";
            this.CheckListWorkspace.ValueMember = "ID";

            comboProcesses.DataSource = constValue_CustomProcesses.GetCustomProcessList();
            InitDgv();
        }

        private void InitDgv()
        {
            cpVD = new CustomProcessesViewData();
            DataTable dtSource = cpVD.dtCustomProcesses;
            this.dgv.DataSource = dtSource;
            this.dgv.BestFitColumns();
        }

        private void ClearInputfields()
        {
            CustomProcess = new CustomProcesses();
            SelectedAdr = new Addresses();

            tbId.Text = "0";
            nudAdrIdDirect.Value = 0;
            tbVerweisADR.Text = "0";
            tbVerweisADRLong.Text = string.Empty;
            tbWorkspaceList.Text = string.Empty;
            comboProcesses.SelectedIndex = -1;
            CheckListWorkspace.UncheckAllItems();
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetValueToCtr()
        {
            if (CustomProcess.Id > 0)
            {
                if (CustomProcess.ListProcessWorkspaces.Count > 0)
                {
                    foreach (var ws in CheckListWorkspace.Items)
                    {
                        int iSelWs = 0;
                        int.TryParse(ws.Value.ToString(), out iSelWs);

                        bool bExist = CustomProcess.ListProcessWorkspaces.Contains(iSelWs);
                        if (bExist)
                        {
                            ws.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                        }
                        else
                        {
                            ws.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        }
                    }
                }
                tbId.Text = CustomProcess.Id.ToString();
                cbAktiv.Checked = CustomProcess.IsActive;
                SelectedAdr = new Addresses();
                this.tbVerweisADR.Text = string.Empty;
                this.tbVerweisADRLong.Text = string.Empty;
                if (CustomProcess.Adress is Addresses)
                {
                    SelectedAdr = CustomProcess.Adress.Copy();
                    this.tbVerweisADR.Text = SelectedAdr.ViewId.ToString();
                    this.tbVerweisADRLong.Text = SelectedAdr.AddressStringShort;
                }
                Functions.SetComboToSelecetedValue(ref comboProcesses, CustomProcess.ProcessName);
                tbWorkspaceList.Text = CustomProcess.ProcessWorkspaces.ToString();
            }
        }
        private void SetFieldToClsValue()
        {
            CustomProcess.AdrId = SelectedAdr.Id;
            CustomProcess.ProcessName = comboProcesses.SelectedValue.ToString();
            CustomProcess.IsActive = cbAktiv.Checked;
            CustomProcess.ProcessWorkspaces = this.tbWorkspaceList.Text;
        }

        private void tsbtnAddProcess_Click(object sender, EventArgs e)
        {
            ClearInputfields();
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            SetFieldToClsValue();
            CustomProcessesViewData vd = new CustomProcessesViewData(CustomProcess);
            if (CustomProcess.Id > 0)
            {
                //-- Update
                vd.Update();
            }
            else
            {
                //-- ADD
                vd.Add();
            }
            ClearInputfields();
            InitDgv();
        }

        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            if (CustomProcess.Id > 0)
            {
                if (clsMessages.CustomProcesses_DeleteItem())
                {
                    CustomProcessesViewData vd = new CustomProcessesViewData(CustomProcess);
                    vd.Delete();
                }
            }
            InitDgv();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckListWorkspace_SelectedItemChanged(object sender, EventArgs e)
        {
            string tmpWorkspaces = "0";
            if (CheckListWorkspace.CheckedItems.Count > 0)
            {
                tmpWorkspaces = string.Empty;
                foreach (var item in CheckListWorkspace.CheckedItems)
                {
                    int iSelWs = 0;
                    int.TryParse(item.Value.ToString(), out iSelWs);

                    if (!tmpWorkspaces.Equals(string.Empty))
                    {
                        tmpWorkspaces += "#";
                    }
                    tmpWorkspaces += iSelWs;
                }
            }
            this.tbWorkspaceList.Text = tmpWorkspaces;
        }

        private void CheckListWorkspace_ItemCheckedChanged(object sender, Telerik.WinControls.UI.ListViewItemEventArgs e)
        {
            string tmpWorkspaces = "0";
            if (CheckListWorkspace.CheckedItems.Count > 0)
            {
                tmpWorkspaces = string.Empty;
                foreach (var item in CheckListWorkspace.CheckedItems)
                {
                    int iSelWs = 0;
                    int.TryParse(item.Value.ToString(), out iSelWs);

                    if (!tmpWorkspaces.Equals(string.Empty))
                    {
                        tmpWorkspaces += "#";
                    }
                    tmpWorkspaces += iSelWs;
                }
            }
            this.tbWorkspaceList.Text = tmpWorkspaces;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchA_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            SelectedAdr = new Addresses();
            _ctrMenu.OpenADRSearch(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudAdrIdDirect_Leave(object sender, EventArgs e)
        {
            if (nudAdrIdDirect.Value > 0)
            {
                TakeOverAdrID(nudAdrIdDirect.Value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDecADR_ID"></param>
        public void TakeOverAdrID(decimal myDecADR_ID)
        {
            this.SearchButton = 1;
            this.tbVerweisADR.Text = string.Empty;
            this.tbVerweisADRLong.Text = string.Empty;

            AddressViewData vd = new AddressViewData(int.Parse(myDecADR_ID.ToString()), int.Parse(this.GLUser.User_ID.ToString()));
            if (vd.Address.Id > 0)
            {
                SelectedAdr = vd.Address.Copy();
            }
            this.tbVerweisADR.Text = SelectedAdr.ViewId.ToString();
            this.tbVerweisADRLong.Text = SelectedAdr.AddressStringShort;

        }

        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                this.dgv.CurrentRow.IsSelected = true;
            }
        }

        private void dgv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dgv.RowCount > 0)
            {
                if (this.dgv.SelectedRows.Count > 0)
                {
                    int iTmp = 0;
                    int.TryParse(dgv.SelectedRows[0].Cells["ID"].Value.ToString(), out iTmp);
                    if (iTmp > 0)
                    {
                        CustomProcesses tmpCP = cpVD.ListCustomProcesses.FirstOrDefault(x => x.Id == iTmp);
                        if ((tmpCP != null) && (tmpCP.Id == iTmp))
                        {
                            ClearInputfields();
                            this.CustomProcess = tmpCP.Copy();
                        }
                        SetValueToCtr();
                    }
                }
            }
        }

        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitDgv();
        }
    }
}
