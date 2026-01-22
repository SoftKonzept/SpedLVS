namespace LVS.Dokumente
{
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;
  using Telerik.Reporting;
  using Telerik.Reporting.Drawing;

  partial class docAuftragScan
  {
    #region Component Designer generated code
    /// <summary>
    /// Required method for telerik Reporting designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.detail = new Telerik.Reporting.DetailSection();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(29.700000762939453D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox1});
            this.detail.Name = "detail";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Docking = Telerik.Reporting.DockingStyle.Fill;
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(20.999900817871094D), Telerik.Reporting.Drawing.Unit.Cm(28.999998092651367D));
            // 
            // docAuftragScan
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "docAuftragScan";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(20.999900817871094D);
            this.NeedDataSource += new System.EventHandler(this.docAuftragScan_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }
    #endregion

    private Telerik.Reporting.DetailSection detail;
    private Telerik.Reporting.PictureBox pictureBox1;
  }
}