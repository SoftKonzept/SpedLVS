using LVS;
using System;

namespace Sped4
{
    partial class ctrAppInfo : Telerik.WinControls.UI.RadForm
    {
        public Globals._GL_USER GLUser;
        public Globals._GL_SYSTEM GLSystem;

        public ctrAppInfo()
        {

            InitializeComponent();

            //  Initialize the AboutBox to display the product information from the assembly information.
            //  Change assembly information settings for your application through either:
            //  - Project->Properties->Application->Assembly Information
            //  - AssemblyInfo.cs
            //this.Text = String.Format("About {0}", AssemblyTitle);
            //this.radLabelProductInfo.Text = AssemblyProduct;
            //this.radLabelLizenz.Text = String.Format("Version {0}", AssemblyVersion);
            ///this.radLabelUserInfo.Text = AssemblyCopyright;
            //this.radLabelCompanyName.Text = AssemblyCompany;
            //this.radTextBoxDescription.Text = AssemblyDescription;
        }
        ///<summary>ctrAppInfo / ctrAppInfo_Load</summary>
        ///<remarks>.</remarks>
        private void ctrAppInfo_Load(object sender, EventArgs e)
        {
            InitCtr();
        }
        ///<summary>ctrAppInfo / InitCtr</summary>
        ///<remarks>.</remarks>
        private void InitCtr()
        {
            SetVersionInfo();
            SetLizenzInfo();
            tbInfoText.Text = string.Empty;
            ctrAppInfoClssText info = new ctrAppInfoClssText();
            this.tbInfoText.Text = info.InfoText;
            //this.tbInfoText.Text = "TEST";
        }
        ///<summary>ctrAppInfo / SetProductInfo</summary>
        ///<remarks>.</remarks>
        private void SetVersionInfo()
        {
            string strTmp = "Verion : " + this.GLSystem.sys_VersionApp + "/ Built :" + this.GLSystem.sys_VersionSystemBuilt;
            this.radLabelVersion.Text = strTmp;
        }
        ///<summary>ctrAppInfo / SetLizenzInfo</summary>
        ///<remarks>.</remarks>
        private void SetLizenzInfo()
        {
            Int32 iUser = 0;
            iUser = clsLogin.GetCountLoggedInUser(this.GLUser);

            string strTmp = "Lizenznehmer: " + Environment.NewLine +
                            this.GLSystem.client_CompanyName + Environment.NewLine +
                            this.GLSystem.client_Strasse + Environment.NewLine +
                            this.GLSystem.client_PLZOrt + Environment.NewLine +
                            "Userlizenzen: " + this.GLSystem.sys_UserQuantity + Environment.NewLine +
                            "aktuell angemeldete User: " + iUser.ToString();
            this.radLabelLizenz.Text = strTmp;
        }

        ///<summary>ctrAppInfo / okRadButton_Click</summary>
        ///<remarks>.</remarks>
        private void okRadButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
