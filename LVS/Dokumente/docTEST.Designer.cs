namespace Sped4.Dokumente
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class docTEST
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            // 
            // docTEST
            // 
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
																		   this.pageHeader,
																		   this.detail,
																		   this.pageFooter});
        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
    }
}