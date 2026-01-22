namespace LVS.Dokumente
{
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;
  using Telerik.Reporting;
  using Telerik.Reporting.Drawing;

  partial class docAbholschein
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
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // pageHeader
      // 
      this.pageHeader.Height = new Telerik.Reporting.Drawing.Unit(3, Telerik.Reporting.Drawing.UnitType.Cm);
      this.pageHeader.Name = "pageHeader";
      // 
      // detail
      // 
      this.detail.Height = new Telerik.Reporting.Drawing.Unit(3, Telerik.Reporting.Drawing.UnitType.Cm);
      this.detail.Name = "detail";
      // 
      // pageFooter
      // 
      this.pageFooter.Height = new Telerik.Reporting.Drawing.Unit(3, Telerik.Reporting.Drawing.UnitType.Cm);
      this.pageFooter.Name = "pageFooter";
      // 
      // docAbholschein
      // 
      this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooter});
      this.PageSettings.Landscape = false;
      this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm);
      this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm);
      this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm);
      this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm);
      this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
      this.Style.BackgroundColor = System.Drawing.Color.White;
      this.NeedDataSource += new System.EventHandler(this.docAbholschein_NeedDataSource);
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }
    #endregion

    private Telerik.Reporting.PageHeaderSection pageHeader;
    private Telerik.Reporting.DetailSection detail;
    private Telerik.Reporting.PageFooterSection pageFooter;
  }
}