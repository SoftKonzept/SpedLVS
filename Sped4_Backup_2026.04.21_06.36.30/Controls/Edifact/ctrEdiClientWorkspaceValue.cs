using LVS;
using LVS.Enumerations;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Sped4.Controls.Edifact
{
    public partial class ctrEdiClientWorkspaceValue : UserControl
    {
        internal EdiClientWorkspaceValueViewData eawVD = new EdiClientWorkspaceValueViewData();
        public ctrMenu _ctrMenu;
        public int SearchButton = 0;
        public ctrEdiClientWorkspaceValue()
        {
            InitializeComponent();
        }

        public void InitCtr()
        {
            SearchButton = 1;
            eawVD = new EdiClientWorkspaceValueViewData((int)_ctrMenu._frmMain.GL_User.User_ID);
            ClearInputFields();
            //comboArbeitsbereich
            comboArbeitsbereich.DataSource = clsArbeitsbereiche.GetArbeitsbereichList(1);
            comboArbeitsbereich.DisplayMember = "Arbeitsbereich";
            comboArbeitsbereich.ValueMember = "ID";

            //-- comboASNArt
            comboAsnArt.DataSource = clsASNArt.GetASNArtList(this._ctrMenu._frmMain.GL_User.User_ID);
            comboAsnArt.ValueMember = "ID";
            comboAsnArt.DisplayMember = "Typ";

            //-- comboDirection
            comboDirection.DataSource = Enum.GetValues(typeof(Directions));

            InitDgv();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgv()
        {
            eawVD = new EdiClientWorkspaceValueViewData();
            dgv.DataSource = eawVD.ListEdiAdrWorkspaceAssignments;
            dgv.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        private void ClearInputFields()
        {
            lId.Text = string.Empty;
            nudAdrDirect.Value = 0;
            tbAdrMatchCode.Text = string.Empty;
            tbAdrShort.Text = string.Empty;
            comboArbeitsbereich.SelectedIndex = -1;
            comboAsnArt.SelectedIndex = -1;
            tbProperty.Text = string.Empty;
            tbValue.Text = string.Empty;
            comboDirection.SelectedIndex = -1;
            tbCreated.Text = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetClsValueToCtr()
        {
            lId.Text = eawVD.AdrWorkspaceAssingment.Id.ToString();
            nudAdrDirect.Value = 0;
            tbAdrMatchCode.Text = string.Empty;
            tbAdrShort.Text = string.Empty;
            if (eawVD.AdrWorkspaceAssingment.AdrId > 0)
            {
                nudAdrDirect.Value = eawVD.AdrWorkspaceAssingment.AdrId;
                tbAdrMatchCode.Text = eawVD.AdrWorkspaceAssingment.Address.ViewId.ToString();
                tbAdrShort.Text = eawVD.AdrWorkspaceAssingment.Address.AddressStringShort.ToString();
            }
            Functions.SetComboToSelecetedValue(ref comboArbeitsbereich, eawVD.AdrWorkspaceAssingment.WorkspaceId.ToString());
            Functions.SetComboToSelecetedValue(ref comboAsnArt, eawVD.AdrWorkspaceAssingment.AsnArtId.ToString());
            tbProperty.Text = eawVD.AdrWorkspaceAssingment.Property;
            tbValue.Text = eawVD.AdrWorkspaceAssingment.Value;
            Functions.SetComboToSelecetedValue(ref comboDirection, eawVD.AdrWorkspaceAssingment.Direction);
            tbCreated.Text = eawVD.AdrWorkspaceAssingment.Created.ToString("dd.MM.yyyy");
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetValueToCls()
        {
            eawVD.AdrWorkspaceAssingment.AdrId = (int)nudAdrDirect.Value;
            int iTmp = 0;
            int.TryParse(comboArbeitsbereich.SelectedValue.ToString(), out iTmp);
            eawVD.AdrWorkspaceAssingment.WorkspaceId = iTmp;
            iTmp = 0;
            int.TryParse(comboAsnArt.SelectedValue.ToString(), out iTmp);
            eawVD.AdrWorkspaceAssingment.AsnArtId = iTmp;
            eawVD.AdrWorkspaceAssingment.Property = tbProperty.Text;
            eawVD.AdrWorkspaceAssingment.Value = tbValue.Text;
            eawVD.AdrWorkspaceAssingment.Direction = comboDirection.SelectedValue.ToString();
        }

        private void tsbtnNewAsnArt_Click(object sender, EventArgs e)
        {
            eawVD = new EdiClientWorkspaceValueViewData();
            //eawVD.AdrWorkspaceAssingment = new EdiAdrWorkspaceAssignment();
            ClearInputFields();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnEdifactSave_Click(object sender, EventArgs e)
        {
            if (
                    (comboDirection.SelectedIndex > -1) &&
                    (comboArbeitsbereich.SelectedIndex > -1) &&
                    (comboAsnArt.SelectedIndex > -1)
               )
            {
                SetValueToCls();
                if (eawVD.AdrWorkspaceAssingment.Id > 0)
                {
                    eawVD.Update();
                }
                else
                {
                    eawVD.Add();
                }
                if (eawVD.AdrWorkspaceAssingment.Id > 0)
                {
                    ClearInputFields();
                }
                InitDgv();
            }
            else
            {
                string strError = "FEHLER - Bitte prüfen Sie die Eingaben!";
                clsMessages.Allgemein_ERRORTextShow(strError);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnEdifactDelete_Click(object sender, EventArgs e)
        {
            if (eawVD.AdrWorkspaceAssingment.Id > 0)
            {
                eawVD.Delete();
                InitDgv();
            }
            else
            {
                string strError = "FEHLER - Es wurde kein Datensatz ausgewählt!";
                clsMessages.Allgemein_ERRORTextShow(strError);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                this.dgv.CurrentRow.IsSelected = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    int iTmp = 0;
                    int.TryParse(dgv.SelectedRows[0].Cells["ID"].Value.ToString(), out iTmp);
                    if (iTmp > 0)
                    {
                        EdiClientWorkspaceValue tmp = eawVD.ListEdiAdrWorkspaceAssignments.FirstOrDefault(x => x.Id == iTmp);
                        if ((tmp != null) && (tmp.Id > 0))
                        {
                            this.eawVD.AdrWorkspaceAssingment = tmp.Copy();
                            ClearInputFields();
                            SetClsValueToCtr();
                        }
                        else
                        {
                            ClearInputFields();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitDgv();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudAdrDirect_Leave(object sender, EventArgs e)
        {
            if (nudAdrDirect.Value > 0)
            {
                //this.IsReceiverSearch = true;
                TakeOverAdrID((int)nudAdrDirect.Value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAdrId"></param>
        public void TakeOverAdrID(int myAdrId)
        {
            if (myAdrId > 0)
            {
                AddressViewData adrVD = new AddressViewData(myAdrId, (int)_ctrMenu._frmMain.GL_User.User_ID);
                if (adrVD.Address.Id == myAdrId)
                {
                    eawVD.AdrWorkspaceAssingment.AdrId = adrVD.Address.Id;
                    eawVD.AdrWorkspaceAssingment.Address = adrVD.Address.Copy();
                    tbAdrMatchCode.Text = eawVD.AdrWorkspaceAssingment.Address.ViewId.ToString();
                    tbAdrShort.Text = eawVD.AdrWorkspaceAssingment.Address.AddressStringShort.ToString();
                    nudAdrDirect.Value = eawVD.AdrWorkspaceAssingment.Address.Id;
                }
                else
                {
                    nudAdrDirect.Value = eawVD.AdrWorkspaceAssingment.AdrId;
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchAdr_Click(object sender, EventArgs e)
        {
            _ctrMenu.OpenADRSearch(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnCopy_Click(object sender, EventArgs e)
        {
            if (
                (eawVD.AdrWorkspaceAssingment != null) &&
                (eawVD.AdrWorkspaceAssingment.Id > 0)
              )
            {
                eawVD.AdrWorkspaceAssingment.Id = 0;
                eawVD.Add();
                InitDgv();
            }  
        }
    }
}
