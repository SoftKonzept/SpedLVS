using LVS;
using LVS.ASN;
using System;
using System.Windows.Forms;

namespace Sped4.Controls.ASNCenter
{
    public partial class ctrOrga : UserControl
    {
        internal clsASNWizzard asnWizz;
        internal ctrMenu _ctrMenu;


        /// <summary>
        /// 
        /// </summary>
        public ctrOrga(ctrMenu myMenu, clsASNWizzard myAsnWizz)
        {
            InitializeComponent();
            _ctrMenu = myMenu;
            asnWizz = myAsnWizz;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrOrga_Load(object sender, EventArgs e)
        {
            if ((this.asnWizz is clsASNWizzard) && (this.asnWizz.Orga is clsOrga))
            {
                InitDgv();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOrga_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvOrga.Rows.Count > 0)
            {
                this.dgvOrga.CurrentRow.IsSelected = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOrga_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SetOrgaDataToCtr();
            SetOrgaInputfieldsEnabled(true);
        }
        /// <summary>
        ///             Werte der Klasse ADRVerweis werden den EIngabefeldern zugewiesen
        /// </summary>
        private void SetOrgaDataToCtr()
        {
            if (dgvOrga.RowCount > 0)
            {
                if (dgvOrga.SelectedRows.Count > 0)
                {
                    SetOrgaInputfieldsEnabled(true);
                    decimal decTmp = 0;
                    decimal.TryParse(dgvOrga.SelectedRows[0].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        asnWizz.InitOrgaById(decTmp);
                        this.tbOrgaId.Text = this.asnWizz.Orga.ID.ToString();
                        this.cbAktiv.Checked = asnWizz.Orga.activ;
                        this.nudSendeIdOld.Value = (decimal)asnWizz.Orga.SendNrOld;
                        this.nudSendeIdOld.Minimum = (decimal)asnWizz.Orga.SendNrOld;
                        this.nudSendeIdNew.Value = (decimal)asnWizz.Orga.SendNrNew;
                        this.nudSendeIdNew.Minimum = (decimal)asnWizz.Orga.SendNrNew;
                    }
                }
            }
        }
        /// <summary>
        ///             Eingabefelder aktivieren / deaktivieren
        /// </summary>
        /// <param name="bEnabled"></param>
        private void SetOrgaInputfieldsEnabled(bool bEnabled)
        {
            this.cbAktiv.Enabled = bEnabled;
            this.nudSendeIdOld.Enabled = bEnabled;
            this.nudSendeIdNew.Enabled = bEnabled;
        }
        /// <summary>
        ///             Standard Werte setzen
        /// </summary>
        private void ClearOrgaInputFields()
        {
            this.cbAktiv.Checked = false;
            this.nudSendeIdOld.Minimum = 0;
            this.nudSendeIdOld.Value = 0;
            this.nudSendeIdNew.Minimum = 1;
            this.nudSendeIdNew.Value = 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnOrgaSave_Click(object sender, EventArgs e)
        {
            if (this.asnWizz is clsASNWizzard)
            {
                SetValueToOrga();
                if (this.asnWizz.Orga.ID > 0)
                {
                    this.asnWizz.Orga.Update();
                }
                else
                {
                    this.asnWizz.Orga.ID = 0;
                    this.asnWizz.Orga.Add();
                }
                ClearOrgaInputFields();
                SetOrgaInputfieldsEnabled(false);
                this.asnWizz.Orga.FillByAdrID();
                InitDgv();
            }
        }
        /// <summary>
        ///             der Klasse ORga werden die editierten Werte zugewiesen
        /// </summary>
        private void SetValueToOrga()
        {
            this.asnWizz.Orga.AdrID = this.asnWizz.AuftragggeberAdr.ID;
            this.asnWizz.Orga.activ = cbAktiv.Checked;
            this.asnWizz.Orga.SendNrOld = (int)nudSendeIdOld.Value;
            this.asnWizz.Orga.SendNrNew = (int)nudSendeIdNew.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnOrgaDelete_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                string strTxt = string.Empty;
                if (this.asnWizz.Orga.Delete())
                {
                    strTxt = "Der Datensatz wurde erfolgreich gelöscht!";
                }
                else
                {
                    strTxt = "Beim Löschvorgang ist ein Fehler aufgetreten!";
                }
                clsMessages.Allgemein_ERRORTextShow(strTxt);
                ClearOrgaInputFields();
                SetOrgaInputfieldsEnabled(false);
                InitDgv();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnOrgaNew_Click(object sender, EventArgs e)
        {
            asnWizz.Orga = new clsOrga();
            ClearOrgaInputFields();
            SetOrgaInputfieldsEnabled(true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefreshAdrVerweis_Click(object sender, EventArgs e)
        {
            this.asnWizz.Orga.FillByAdrID();
            InitDgv();
        }
        /// <summary>
        ///             Datasource für das Datagrid wird gesetzt
        /// </summary>
        private void InitDgv()
        {
            dgvOrga.DataSource = asnWizz.Orga.dtOrgaByAdress;
            this.dgvOrga.BestFitColumns();
        }
    }
}
