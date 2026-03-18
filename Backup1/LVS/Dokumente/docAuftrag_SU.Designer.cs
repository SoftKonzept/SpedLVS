namespace Sped4
{
  partial class docAuftrag_SU
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.group1 = new Telerik.Reporting.Group();
      this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
      this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // group1
      // 
      this.group1.GroupFooter = this.groupFooterSection1;
      this.group1.GroupHeader = this.groupHeaderSection1;
      this.group1.Name = "group1";
      // 
      // groupFooterSection1
      // 
      this.groupFooterSection1.Height = new Telerik.Reporting.Drawing.Unit(3, Telerik.Reporting.Drawing.UnitType.Cm);
      this.groupFooterSection1.Name = "groupFooterSection1";
      // 
      // groupHeaderSection1
      // 
      this.groupHeaderSection1.Height = new Telerik.Reporting.Drawing.Unit(3, Telerik.Reporting.Drawing.UnitType.Cm);
      this.groupHeaderSection1.Name = "groupHeaderSection1";
      // 
      // docAuftrag_SU
      // 
      this.Groups.AddRange(new Telerik.Reporting.Group[] {
            this.group1});
      this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1});
      this.PageSettings.Landscape = false;
      this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(2.5399999618530273, Telerik.Reporting.Drawing.UnitType.Cm);
      this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(2.5399999618530273, Telerik.Reporting.Drawing.UnitType.Cm);
      this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(2.5399999618530273, Telerik.Reporting.Drawing.UnitType.Cm);
      this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(2.5399999618530273, Telerik.Reporting.Drawing.UnitType.Cm);
      this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
      this.Style.BackgroundColor = System.Drawing.Color.White;
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private Telerik.Reporting.Group group1;
    private Telerik.Reporting.GroupFooterSection groupFooterSection1;
    private Telerik.Reporting.GroupHeaderSection groupHeaderSection1;
  }
}
