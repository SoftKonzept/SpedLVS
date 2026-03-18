namespace LVS.Dokumente
{
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;
  using Telerik.Reporting;
  using Telerik.Reporting.Drawing;

  partial class docManRGGS
  {
    #region Component Designer generated code
    /// <summary>
    /// Required method for telerik Reporting designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup5 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup6 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup7 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup8 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup9 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup10 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup11 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup12 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup13 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup14 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup15 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup16 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup17 = new Telerik.Reporting.TableGroup();
        Telerik.Reporting.TableGroup tableGroup18 = new Telerik.Reporting.TableGroup();
        this.detail_RGGS = new Telerik.Reporting.DetailSection();
        this.panelPos = new Telerik.Reporting.Panel();
        this.RGHeadPositionen = new Telerik.Reporting.List();
        this.textBox7 = new Telerik.Reporting.TextBox();
        this.textBox8 = new Telerik.Reporting.TextBox();
        this.textBox11 = new Telerik.Reporting.TextBox();
        this.RGPositionen = new Telerik.Reporting.List();
        this.panel1 = new Telerik.Reporting.Panel();
        this.textBox4 = new Telerik.Reporting.TextBox();
        this.textBox5 = new Telerik.Reporting.TextBox();
        this.textBox6 = new Telerik.Reporting.TextBox();
        this.textBox21 = new Telerik.Reporting.TextBox();
        this.textBox22 = new Telerik.Reporting.TextBox();
        this.panelGesamt = new Telerik.Reporting.Panel();
        this.ListSumme = new Telerik.Reporting.List();
        this.textBox9 = new Telerik.Reporting.TextBox();
        this.textBox10 = new Telerik.Reporting.TextBox();
        this.textBox12 = new Telerik.Reporting.TextBox();
        this.textBox14 = new Telerik.Reporting.TextBox();
        this.textBox15 = new Telerik.Reporting.TextBox();
        this.textBox16 = new Telerik.Reporting.TextBox();
        this.textBox13 = new Telerik.Reporting.TextBox();
        this.textBox17 = new Telerik.Reporting.TextBox();
        this.textBox20 = new Telerik.Reporting.TextBox();
        this.textBox19 = new Telerik.Reporting.TextBox();
        this.textBox18 = new Telerik.Reporting.TextBox();
        this.textBox3 = new Telerik.Reporting.TextBox();
        this.textBox2 = new Telerik.Reporting.TextBox();
        this.textBox1 = new Telerik.Reporting.TextBox();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // detail_RGGS
        // 
        this.detail_RGGS.Height = new Telerik.Reporting.Drawing.Unit(12.600000381469727, Telerik.Reporting.Drawing.UnitType.Cm);
        this.detail_RGGS.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.panelPos,
            this.panelGesamt});
        this.detail_RGGS.Name = "detail_RGGS";
        // 
        // panelPos
        // 
        this.panelPos.Dock = System.Windows.Forms.DockStyle.Top;
        this.panelPos.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.RGHeadPositionen,
            this.RGPositionen});
        this.panelPos.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm));
        this.panelPos.Name = "panelPos";
        this.panelPos.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(15.920000076293945, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(10.899999618530273, Telerik.Reporting.Drawing.UnitType.Cm));
        // 
        // RGHeadPositionen
        // 
        this.RGHeadPositionen.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(0.81321042776107788, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.RGHeadPositionen.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(12.783838272094727, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.RGHeadPositionen.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(2.3229498863220215, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.RGHeadPositionen.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(0.44437503814697266, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.RGHeadPositionen.Body.SetCellContent(0, 0, this.textBox7);
        this.RGHeadPositionen.Body.SetCellContent(0, 1, this.textBox8);
        this.RGHeadPositionen.Body.SetCellContent(0, 2, this.textBox11);
        tableGroup1.Name = "Group1";
        tableGroup2.Name = "Group2";
        tableGroup3.Name = "ColumnGroup1";
        this.RGHeadPositionen.ColumnGroups.Add(tableGroup1);
        this.RGHeadPositionen.ColumnGroups.Add(tableGroup2);
        this.RGHeadPositionen.ColumnGroups.Add(tableGroup3);
        this.RGHeadPositionen.ColumnHeadersPrintOnEveryPage = true;
        this.RGHeadPositionen.Dock = System.Windows.Forms.DockStyle.Top;
        this.RGHeadPositionen.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox7,
            this.textBox8,
            this.textBox11});
        this.RGHeadPositionen.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.9921220680698752E-05, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.00010012308484874666, Telerik.Reporting.Drawing.UnitType.Cm));
        this.RGHeadPositionen.Name = "RGHeadPositionen";
        tableGroup5.Name = "Group8";
        tableGroup4.ChildGroups.Add(tableGroup5);
        tableGroup4.Name = "Group6";
        this.RGHeadPositionen.RowGroups.Add(tableGroup4);
        this.RGHeadPositionen.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(15.920000076293945, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.44437503814697266, Telerik.Reporting.Drawing.UnitType.Cm));
        // 
        // textBox7
        // 
        this.textBox7.Name = "textBox7";
        this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.81321042776107788, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.44437503814697266, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
        this.textBox7.Style.Font.Bold = true;
        this.textBox7.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
        this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
        this.textBox7.StyleName = "";
        this.textBox7.Value = "Pos";
        // 
        // textBox8
        // 
        this.textBox8.Name = "textBox8";
        this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(12.783839225769043, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.44437503814697266, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox8.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
        this.textBox8.Style.Font.Bold = true;
        this.textBox8.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
        this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
        this.textBox8.StyleName = "";
        this.textBox8.Value = "Text / Bemerkung";
        // 
        // textBox11
        // 
        this.textBox11.Name = "textBox11";
        this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.3229501247406006, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.44437503814697266, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox11.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
        this.textBox11.Style.Font.Bold = true;
        this.textBox11.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
        this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
        this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
        this.textBox11.StyleName = "";
        this.textBox11.Value = "Betrag";
        // 
        // RGPositionen
        // 
        this.RGPositionen.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(0.86229234933853149, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.RGPositionen.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(12.55687427520752, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.RGPositionen.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(2.4914605617523193, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.RGPositionen.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(0.25, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.RGPositionen.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(0.40000003576278687, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.RGPositionen.Body.SetCellContent(0, 2, this.panel1);
        this.RGPositionen.Body.SetCellContent(0, 0, this.textBox4);
        this.RGPositionen.Body.SetCellContent(0, 1, this.textBox5);
        this.RGPositionen.Body.SetCellContent(1, 0, this.textBox6);
        this.RGPositionen.Body.SetCellContent(1, 1, this.textBox21);
        this.RGPositionen.Body.SetCellContent(1, 2, this.textBox22);
        tableGroup6.Name = "Group1";
        tableGroup7.Name = "Group2";
        tableGroup8.Name = "ColumnGroup1";
        this.RGPositionen.ColumnGroups.Add(tableGroup6);
        this.RGPositionen.ColumnGroups.Add(tableGroup7);
        this.RGPositionen.ColumnGroups.Add(tableGroup8);
        this.RGPositionen.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.panel1,
            this.textBox4,
            this.textBox5,
            this.textBox6,
            this.textBox21,
            this.textBox22});
        this.RGPositionen.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.444675087928772, Telerik.Reporting.Drawing.UnitType.Cm));
        this.RGPositionen.Name = "RGPositionen";
        tableGroup10.Name = "Group3";
        tableGroup11.Name = "Group4";
        tableGroup9.ChildGroups.Add(tableGroup10);
        tableGroup9.ChildGroups.Add(tableGroup11);
        tableGroup9.Name = "RowGroup1";
        this.RGPositionen.RowGroups.Add(tableGroup9);
        this.RGPositionen.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(15.910626411437988, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.65000003576278687, Telerik.Reporting.Drawing.UnitType.Cm));
        // 
        // panel1
        // 
        this.panel1.Name = "panel1";
        this.panel1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.4914603233337402, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.25, Telerik.Reporting.Drawing.UnitType.Cm));
        // 
        // textBox4
        // 
        this.textBox4.Name = "textBox4";
        this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.86229193210601807, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.25, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox4.StyleName = "";
        // 
        // textBox5
        // 
        this.textBox5.Name = "textBox5";
        this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(12.556873321533203, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.25, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox5.StyleName = "";
        // 
        // textBox6
        // 
        this.textBox6.Name = "textBox6";
        this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.86229193210601807, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000003576278687, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
        this.textBox6.StyleName = "";
        this.textBox6.Value = "=Fields.Pos\t";
        // 
        // textBox21
        // 
        this.textBox21.Name = "textBox21";
        this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(12.556873321533203, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000003576278687, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
        this.textBox21.StyleName = "";
        this.textBox21.Value = "=Fields.Frachttext";
        // 
        // textBox22
        // 
        this.textBox22.Format = "{0:C2}";
        this.textBox22.Name = "textBox22";
        this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.4914603233337402, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000003576278687, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
        this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
        this.textBox22.StyleName = "";
        this.textBox22.Value = "=Fields.Fracht";
        // 
        // panelGesamt
        // 
        this.panelGesamt.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.panelGesamt.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.ListSumme});
        this.panelGesamt.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(11.300000190734863, Telerik.Reporting.Drawing.UnitType.Cm));
        this.panelGesamt.Name = "panelGesamt";
        this.panelGesamt.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(15.920000076293945, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.2999999523162842, Telerik.Reporting.Drawing.UnitType.Cm));
        // 
        // ListSumme
        // 
        this.ListSumme.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(3.9800000190734863, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.ListSumme.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(3.9800000190734863, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.ListSumme.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(3.9800000190734863, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.ListSumme.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(3.9800000190734863, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.ListSumme.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(0.49999994039535522, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.ListSumme.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(0.50000005960464478, Telerik.Reporting.Drawing.UnitType.Cm)));
        this.ListSumme.Body.SetCellContent(0, 1, this.textBox9);
        this.ListSumme.Body.SetCellContent(0, 2, this.textBox10);
        this.ListSumme.Body.SetCellContent(0, 3, this.textBox12);
        this.ListSumme.Body.SetCellContent(1, 1, this.textBox14);
        this.ListSumme.Body.SetCellContent(1, 2, this.textBox15);
        this.ListSumme.Body.SetCellContent(1, 3, this.textBox16);
        this.ListSumme.Body.SetCellContent(0, 0, this.textBox13);
        this.ListSumme.Body.SetCellContent(1, 0, this.textBox17);
        tableGroup12.Name = "Group6";
        tableGroup13.Name = "Group1";
        tableGroup14.Name = "Group2";
        tableGroup15.Name = "Group3";
        this.ListSumme.ColumnGroups.Add(tableGroup12);
        this.ListSumme.ColumnGroups.Add(tableGroup13);
        this.ListSumme.ColumnGroups.Add(tableGroup14);
        this.ListSumme.ColumnGroups.Add(tableGroup15);
        this.ListSumme.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.ListSumme.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox9,
            this.textBox10,
            this.textBox12,
            this.textBox14,
            this.textBox15,
            this.textBox16,
            this.textBox13,
            this.textBox17});
        this.ListSumme.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.29999995231628418, Telerik.Reporting.Drawing.UnitType.Cm));
        this.ListSumme.Name = "ListSumme";
        tableGroup17.Name = "Group4";
        tableGroup18.Name = "Group5";
        tableGroup16.ChildGroups.Add(tableGroup17);
        tableGroup16.ChildGroups.Add(tableGroup18);

        tableGroup16.Name = "RowGroup1";
        this.ListSumme.RowGroups.Add(tableGroup16);
        this.ListSumme.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(15.920000076293945, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Cm));
        // 
        // textBox9
        // 
        this.textBox9.Name = "textBox9";
        this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.9800000190734863, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999994039535522, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox9.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
        this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
        this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
        this.textBox9.StyleName = "";
        this.textBox9.Value = "MwSt-%";
        // 
        // textBox10
        // 
        this.textBox10.Name = "textBox10";
        this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.9800000190734863, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999994039535522, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox10.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
        this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
        this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
        this.textBox10.StyleName = "";
        this.textBox10.Value = "MwSt-Betrag";
        // 
        // textBox12
        // 
        this.textBox12.Name = "textBox12";
        this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.9800000190734863, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999994039535522, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox12.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
        this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
        this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
        this.textBox12.StyleName = "";
        this.textBox12.Value = "Brutto-Betrag";
        // 
        // textBox14
        // 
        this.textBox14.Format = "{0:N2}";
        this.textBox14.Name = "textBox14";
        this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.9800000190734863, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.50000005960464478, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
        this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
        this.textBox14.StyleName = "";
        this.textBox14.Value = "=MwStP";
        // 
        // textBox15
        // 
        this.textBox15.Format = "{0:C2}";
        this.textBox15.Name = "textBox15";
        this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.9800000190734863, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.50000005960464478, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
        this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
        this.textBox15.StyleName = "";
        this.textBox15.Value = "=MwStEuro";
        // 
        // textBox16
        // 
        this.textBox16.Format = "{0:C2}";
        this.textBox16.Name = "textBox16";
        this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.9800000190734863, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.50000005960464478, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
        this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
        this.textBox16.StyleName = "";
        this.textBox16.Value = "=Brutto";
        // 
        // textBox13
        // 
        this.textBox13.Name = "textBox13";
        this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.9800000190734863, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999994039535522, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox13.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
        this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
        this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
        this.textBox13.StyleName = "";
        this.textBox13.Value = "Netto-Betrag";
        // 
        // textBox17
        // 
        this.textBox17.Format = "{0:C2}";
        this.textBox17.Name = "textBox17";
        this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.9800000190734863, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.50000005960464478, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
        this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
        this.textBox17.StyleName = "";
        this.textBox17.Value = "=Netto";
        // 
        // textBox20
        // 
        this.textBox20.Name = "textBox20";
        this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.17185115814209, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.24999997019767761, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox20.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
        this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
        this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
        this.textBox20.StyleName = "";
        // 
        // textBox19
        // 
        this.textBox19.Name = "textBox19";
        this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(9.9362869262695312, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.24999997019767761, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox19.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
        this.textBox19.StyleName = "";
        // 
        // textBox18
        // 
        this.textBox18.Name = "textBox18";
        this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.81186145544052124, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.24999997019767761, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox18.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
        this.textBox18.StyleName = "";
        // 
        // textBox3
        // 
        this.textBox3.Format = "{0:C2}";
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.17185115814209, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999985694885254, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox3.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
        this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
        this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
        this.textBox3.StyleName = "";
        this.textBox3.Value = "=Fields.Fracht\t";
        // 
        // textBox2
        // 
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(9.9362869262695312, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999985694885254, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox2.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
        this.textBox2.StyleName = "";
        this.textBox2.Value = "=Fields.Frachttext\t";
        // 
        // textBox1
        // 
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(0.81186145544052124, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999985694885254, Telerik.Reporting.Drawing.UnitType.Cm));
        this.textBox1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
        this.textBox1.StyleName = "";
        this.textBox1.Value = "=Fields.Pos";
        // 
        // docManRGGS
        // 
        this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail_RGGS});
        this.PageSettings.Landscape = false;
        this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(2.5399999618530273, Telerik.Reporting.Drawing.UnitType.Cm);
        this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(2.5399999618530273, Telerik.Reporting.Drawing.UnitType.Cm);
        this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(2.5399999618530273, Telerik.Reporting.Drawing.UnitType.Cm);
        this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(2.5399999618530273, Telerik.Reporting.Drawing.UnitType.Cm);
        this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
        this.Style.BackgroundColor = System.Drawing.Color.White;
        this.Width = new Telerik.Reporting.Drawing.Unit(15.920000076293945, Telerik.Reporting.Drawing.UnitType.Cm);
        this.NeedDataSource += new System.EventHandler(this.docManRGGS_NeedDataSource);
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }
    #endregion

    private Telerik.Reporting.DetailSection detail_RGGS;
    private Telerik.Reporting.Panel panelGesamt;
    private List ListSumme;
    private Telerik.Reporting.TextBox textBox9;
    private Telerik.Reporting.TextBox textBox10;
    private Telerik.Reporting.TextBox textBox12;
    private Telerik.Reporting.TextBox textBox14;
    private Telerik.Reporting.TextBox textBox15;
    private Telerik.Reporting.TextBox textBox16;
    private Telerik.Reporting.TextBox textBox13;
    private Telerik.Reporting.TextBox textBox17;
    private Telerik.Reporting.Panel panelPos;
    private List RGHeadPositionen;
    private Telerik.Reporting.TextBox textBox7;
    private Telerik.Reporting.TextBox textBox8;
    private Telerik.Reporting.TextBox textBox11;
    private Telerik.Reporting.TextBox textBox20;
    private Telerik.Reporting.TextBox textBox19;
    private Telerik.Reporting.TextBox textBox18;
    private Telerik.Reporting.TextBox textBox3;
    private Telerik.Reporting.TextBox textBox2;
    private Telerik.Reporting.TextBox textBox1;
    private List RGPositionen;
    private Telerik.Reporting.Panel panel1;
    private Telerik.Reporting.TextBox textBox4;
    private Telerik.Reporting.TextBox textBox5;
    private Telerik.Reporting.TextBox textBox6;
    private Telerik.Reporting.TextBox textBox21;
    private Telerik.Reporting.TextBox textBox22;
  }
}