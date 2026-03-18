using LVS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrComMailingLists : UserControl
    {
        internal ctrMenu _ctrMenu;

        private clsCronJobs crons;
        internal clsADR ADR;

        public ctrComMailingLists(ctrMenu myMenu)
        {
            InitializeComponent();
            _ctrMenu = myMenu;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrComMailingLists_Load(object sender, EventArgs e)
        {

            ADR = new clsADR();
            ADR._GL_User = this._ctrMenu._frmMain.GL_User;

            List<decimal> lstMailingListID = clsMailingList.GetAutoMailingList(this._ctrMenu._frmMain.GL_User, "#" + enumAutoMailingListTypes.AutoBestandExcel + "#");


            List<clsMailingList> lstMailingLists = clsMailingList.GetAllAuto(this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.GL_System, "#" + enumAutoMailingListTypes.AutoBestandExcel + "#");
            lbMailingLists.DataSource = lstMailingLists;
            lbMailingLists.DisplayMember = "DisplayName";

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbMailingLists_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((sender as ListBox).SelectedValue.GetType() == typeof(clsMailingList))
            {
                clsMailingList tmp = ((sender as ListBox).SelectedValue as clsMailingList);
                clsMailingListAssignment mla = new clsMailingListAssignment();
                mla._GL_User = tmp._GL_User;
                mla.MailingListID = tmp.ID;
                mla.FillList(tmp.ID);

                lbMailAdressen.DataSource = mla.GetAllKontakte();
                lbMailAdressen.DisplayMember = "FullName";

                List<clsMailingListCombiAdr> lstMlca = clsMailingListCombiAdr.GetAll(tmp);
                lbCombiAdr.DataSource = lstMlca;
                lbCombiAdr.DisplayMember = "KundenViewID";
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDelArtFromAAusgang_Click(object sender, EventArgs e)
        {


            if (clsMailingListCombiAdr.Remove(lbCombiAdr.SelectedItem as clsMailingListCombiAdr))
            {
                clsMailingList tmp = (lbMailingLists.SelectedValue as clsMailingList);
                List<clsMailingListCombiAdr> lstMlca = clsMailingListCombiAdr.GetAll(tmp);
                lbCombiAdr.DataSource = lstMlca;
                lbCombiAdr.DisplayMember = "KundenViewID";

            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnAllToAusgang_Click(object sender, EventArgs e)
        {
            if ((!tbAuftraggeber.Text.Equals(string.Empty)) && (lbMailingLists is clsMailingList))
            {
                clsMailingList tmp = (lbMailingLists.SelectedValue as clsMailingList);
                clsMailingListCombiAdr mlca = new clsMailingListCombiAdr();
                mlca.AdrId = (Int32)ADR.ID;
                mlca.MailingListId = (Int32)(lbMailingLists.SelectedValue as clsMailingList).ID;
                mlca.Save();
                List<clsMailingListCombiAdr> lstMlca = clsMailingListCombiAdr.GetAll(tmp);
                lbCombiAdr.DataSource = lstMlca;
                lbCombiAdr.DisplayMember = "KundenViewID";
                tbSearchA.Text = string.Empty;

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearchA_TextChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = Functions.GetADRTableSearchResultTable(tbSearchA.Text, this._ctrMenu._frmMain.GL_User);
            if (dtTmp.Rows.Count > 0)
            {
                tbAuftraggeber.Text = Functions.GetADRStringFromTable(dtTmp);
                ADR.ID = Functions.GetADR_IDFromTable(dtTmp);
            }
            else
            {
                tbAuftraggeber.Text = string.Empty;
                ADR.ID = 0;
            }
            ADR.Fill();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDelAllFromAAusgang_Click(object sender, EventArgs e)
        {
            //foreach()
        }


    }
}
