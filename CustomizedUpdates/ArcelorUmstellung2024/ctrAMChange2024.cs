using CustomizedUpdates.MainSystem;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CustomizedUpdates.ArcelorUmstellung2024
{
    public partial class ctrAMChange2024 : UserControl
    {
        public UpdateViewData upVD = new UpdateViewData();
        internal SystemMain systemMain = new SystemMain();
        public ctrAMChange2024()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        public void InitCtr()
        {
            upVD = new UpdateViewData();
            tbLiefNrALT.Text = UpdateViewData.const_LieferantennummerALT;
            tbLiefNrNEU.Text = UpdateViewData.const_LieferantennummerNEU;
        }

        private void btnUpdateLiefNr_Click(object sender, EventArgs e)
        {
            tbInfo1.Text = string.Empty;
            upVD = new UpdateViewData();
            upVD.EingangToUpdate();
            SetInfoText(upVD.ListInfos);
        }

        private void SetInfoText(List<string> list)
        {
            string str = string.Empty;
            if (list != null && list.Count > 0)
            {
                tbInfo1.Text = str;
                foreach (string item in list)
                {
                    str += item + Environment.NewLine;
                }
            }
            tbInfo1.Text = str;
        }

        private void btnUpdateGueterart_Click(object sender, EventArgs e)
        {
            tbInfo1.Text = string.Empty;
            upVD = new UpdateViewData();
            upVD.UpdateGueterartenBestellnummer();
            SetInfoText(upVD.ListInfos);
        }

        private void btnUpdateArticleData_Click(object sender, EventArgs e)
        {
            tbInfo1.Text = string.Empty;
            upVD = new UpdateViewData();
            upVD.UpdateArticleBestellnummer();
            SetInfoText(upVD.ListInfos);
        }
    }
}
