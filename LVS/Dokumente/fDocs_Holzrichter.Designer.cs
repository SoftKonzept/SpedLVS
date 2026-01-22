namespace LVS.Dokumente
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class fDocs_Holzrichter
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
            this.tbIhrZeichen = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.tbBestellNr = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.tbAbruf = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.dt = new Telerik.Reporting.Table();
            this.dtArtikelliste = new Telerik.Reporting.Table();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.pbFussBK = new Telerik.Reporting.PictureBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.pbLogo = new Telerik.Reporting.PictureBox();
            this.tbDocName = new Telerik.Reporting.TextBox();
            this.tbDatum = new Telerik.Reporting.TextBox();
            this.tbLfsDatum = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.tbADROrt = new Telerik.Reporting.TextBox();
            this.tbADRStr = new Telerik.Reporting.TextBox();
            this.tbADRName1 = new Telerik.Reporting.TextBox();
            this.tbFirmaAbsender = new Telerik.Reporting.TextBox();
            this.tbADRPLZ = new Telerik.Reporting.TextBox();
            this.tbEPLZ = new Telerik.Reporting.TextBox();
            this.tbEStr = new Telerik.Reporting.TextBox();
            this.tbEOrt = new Telerik.Reporting.TextBox();
            this.tbEFirma1 = new Telerik.Reporting.TextBox();
            this.tbEFirma2 = new Telerik.Reporting.TextBox();
            this.tbDate = new Telerik.Reporting.TextBox();
            this.tbLfsNr = new Telerik.Reporting.TextBox();
            this.tbLfsDate = new Telerik.Reporting.TextBox();
            this.tbFahrer = new Telerik.Reporting.TextBox();
            this.tbAuflieger = new Telerik.Reporting.TextBox();
            this.tbZM = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // tbIhrZeichen
            // 
            this.tbIhrZeichen.Name = "tbIhrZeichen";
            this.tbIhrZeichen.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.1954154968261719, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbIhrZeichen.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbIhrZeichen.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbIhrZeichen.StyleName = "";
            this.tbIhrZeichen.Value = "[=Fields.IhrZeichen]";
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.1954169273376465, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox1.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Value = "Ihr Zeichen\t";
            // 
            // tbBestellNr
            // 
            this.tbBestellNr.Name = "tbBestellNr";
            this.tbBestellNr.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.572197437286377, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbBestellNr.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbBestellNr.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbBestellNr.StyleName = "";
            this.tbBestellNr.Value = "[=Fields.BestellNr]";
            // 
            // textBox2
            // 
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.572197437286377, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.3999999463558197, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox2.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Value = "Ihre Best.-Nr.:";
            // 
            // tbAbruf
            // 
            this.tbAbruf.Name = "tbAbruf";
            this.tbAbruf.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.4590544700622559, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbAbruf.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbAbruf.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbAbruf.StyleName = "";
            this.tbAbruf.Value = "[=Fields.Abruf]";
            // 
            // textBox3
            // 
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.4590535163879395, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox3.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Value = "Bestellung | Abruf vom";
            // 
            // textBox8
            // 
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.00000262260437, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox8.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Value = "Artikel";
            // 
            // textBox9
            // 
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.7835431098937988, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox9.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Value = "Bezeichnung";
            // 
            // textBox14
            // 
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox14.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.StyleName = "";
            this.textBox14.Value = "Menge";
            // 
            // textBox10
            // 
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.7500007152557373, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox10.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Value = "Netto";
            // 
            // textBox16
            // 
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.7500007152557373, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox16.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.Font.Bold = true;
            this.textBox16.StyleName = "";
            this.textBox16.Value = "Brutto";
            // 
            // detail
            // 
            this.detail.Height = new Telerik.Reporting.Drawing.Unit(14.399999618530273, Telerik.Reporting.Drawing.UnitType.Cm);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.dt,
            this.dtArtikelliste});
            this.detail.Name = "detail";
            // 
            // dt
            // 
            this.dt.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(5.19541597366333, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dt.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(5.5721964836120605, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dt.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(6.4590544700622559, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dt.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm)));
            tableGroup3.Name = "Group1";
            tableGroup2.ChildGroups.Add(tableGroup3);
            tableGroup2.Name = "Group4";
            tableGroup2.ReportItem = this.tbIhrZeichen;
            tableGroup1.ChildGroups.Add(tableGroup2);
            tableGroup1.ReportItem = this.textBox1;
            tableGroup6.Name = "Group2";
            tableGroup5.ChildGroups.Add(tableGroup6);
            tableGroup5.Name = "Group5";
            tableGroup5.ReportItem = this.tbBestellNr;
            tableGroup4.ChildGroups.Add(tableGroup5);
            tableGroup4.ReportItem = this.textBox2;
            tableGroup9.Name = "Group3";
            tableGroup8.ChildGroups.Add(tableGroup9);
            tableGroup8.Name = "Group6";
            tableGroup8.ReportItem = this.tbAbruf;
            tableGroup7.ChildGroups.Add(tableGroup8);
            tableGroup7.ReportItem = this.textBox3;
            this.dt.ColumnGroups.Add(tableGroup1);
            this.dt.ColumnGroups.Add(tableGroup4);
            this.dt.ColumnGroups.Add(tableGroup7);
            this.dt.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.tbIhrZeichen,
            this.textBox2,
            this.tbBestellNr,
            this.textBox3,
            this.tbAbruf});
            this.dt.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.60812419652938843, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.50000017881393433, Telerik.Reporting.Drawing.UnitType.Cm));
            this.dt.Name = "dt";

            tableGroup10.Name = "DetailGroup";
            this.dt.RowGroups.Add(tableGroup10);
            this.dt.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(17.226667404174805, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.2000000476837158, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // dtArtikelliste
            // 
            this.dtArtikelliste.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(3.00000262260437, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikelliste.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(6.7835431098937988, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikelliste.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikelliste.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(2.7500007152557373, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikelliste.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(2.7500007152557373, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikelliste.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikelliste.Body.SetCellContent(0, 0, this.textBox11);
            this.dtArtikelliste.Body.SetCellContent(0, 1, this.textBox12);
            this.dtArtikelliste.Body.SetCellContent(0, 3, this.textBox13);
            this.dtArtikelliste.Body.SetCellContent(0, 2, this.textBox15);
            this.dtArtikelliste.Body.SetCellContent(0, 4, this.textBox17);
            tableGroup11.ReportItem = this.textBox8;
            tableGroup12.ReportItem = this.textBox9;
            tableGroup13.Name = "Group1";
            tableGroup13.ReportItem = this.textBox14;
            tableGroup14.ReportItem = this.textBox10;
            tableGroup15.Name = "Group2";
            tableGroup15.ReportItem = this.textBox16;
            this.dtArtikelliste.ColumnGroups.Add(tableGroup11);
            this.dtArtikelliste.ColumnGroups.Add(tableGroup12);
            this.dtArtikelliste.ColumnGroups.Add(tableGroup13);
            this.dtArtikelliste.ColumnGroups.Add(tableGroup14);
            this.dtArtikelliste.ColumnGroups.Add(tableGroup15);
            this.dtArtikelliste.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox11,
            this.textBox12,
            this.textBox13,
            this.textBox15,
            this.textBox17,
            this.textBox8,
            this.textBox9,
            this.textBox14,
            this.textBox10,
            this.textBox16});
            this.dtArtikelliste.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.60812419652938843, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.9000002145767212, Telerik.Reporting.Drawing.UnitType.Cm));
            this.dtArtikelliste.Name = "dtArtikelliste";
            tableGroup16.Name = "DetailGroup";
            this.dtArtikelliste.RowGroups.Add(tableGroup16);
            this.dtArtikelliste.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(17.283546447753906, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Cm));
            this.dtArtikelliste.NeedDataSource += new System.EventHandler(this.dtArtikelliste_NeedDataSource);
            // 
            // textBox11
            // 
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.00000262260437, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox11.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Value = "=Fields.Werksnummer";
            // 
            // textBox12
            // 
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.7835431098937988, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox12.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Value = "=Fields.Zusatz";
            // 
            // textBox13
            // 
            this.textBox13.Format = "{0:N2}";
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.7500007152557373, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox13.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox13.Value = "=Fields.Netto";
            // 
            // textBox15
            // 
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox15.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15.StyleName = "";
            this.textBox15.Value = "=Fields.ME";
            // 
            // textBox17
            // 
            this.textBox17.Format = "{0:N2}";
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.7500007152557373, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox17.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.StyleName = "";
            this.textBox17.Value = "=Fields.Brutto";
            // 
            // textBox4
            // 
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.1954169273376465, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox4.Style.BackgroundImage.Repeat = Telerik.Reporting.Drawing.BackgroundRepeat.NoRepeat;
            this.textBox4.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.5986557006835938, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox6.Style.BackgroundImage.Repeat = Telerik.Reporting.Drawing.BackgroundRepeat.NoRepeat;
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // textBox7
            // 
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.4590535163879395, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox7.Style.BackgroundImage.Repeat = Telerik.Reporting.Drawing.BackgroundRepeat.NoRepeat;
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = new Telerik.Reporting.Drawing.Unit(4.6000003814697266, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pbFussBK});
            this.pageFooter.Name = "pageFooter";
            // 
            // pbFussBK
            // 
            this.pbFussBK.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.60812419652938843, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm));
            this.pbFussBK.Name = "pbFussBK";
            this.pbFussBK.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(17.291778564453125, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(4.4000020027160645, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // pageHeader
            // 
            this.pageHeader.Height = new Telerik.Reporting.Drawing.Unit(9.6000003814697266, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pbLogo,
            this.tbDocName,
            this.tbDatum,
            this.tbLfsDatum,
            this.textBox5,
            this.tbADROrt,
            this.tbADRStr,
            this.tbADRName1,
            this.tbFirmaAbsender,
            this.tbADRPLZ,
            this.tbEPLZ,
            this.tbEStr,
            this.tbEOrt,
            this.tbEFirma1,
            this.tbEFirma2,
            this.tbDate,
            this.tbLfsNr,
            this.tbLfsDate,
            this.tbFahrer,
            this.tbAuflieger,
            this.tbZM});
            this.pageHeader.Name = "pageHeader";
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(8.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(9.9921220680698752E-05, Telerik.Reporting.Drawing.UnitType.Cm));
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(9.69990062713623, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(4.2999000549316406, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // tbDocName
            // 
            this.tbDocName.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(8.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(4.30019998550415, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbDocName.Name = "tbDocName";
            this.tbDocName.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(9.3999996185302734, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.0999990701675415, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbDocName.Style.Font.Bold = true;
            this.tbDocName.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(25, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbDocName.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.tbDocName.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.tbDocName.Value = "Lieferschein";
            // 
            // tbDatum
            // 
            this.tbDatum.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(8.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(6.7000002861022949, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbDatum.Name = "tbDatum";
            this.tbDatum.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39979961514472961, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbDatum.Value = "Datum";
            // 
            // tbLfsDatum
            // 
            this.tbLfsDatum.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(8.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(7.0999999046325684, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbLfsDatum.Name = "tbLfsDatum";
            this.tbLfsDatum.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.3000011444091797, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbLfsDatum.Value = "Lieferscheindatum: ";
            // 
            // textBox5
            // 
            this.textBox5.Format = "";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.599999725818634, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(7.1597871780395508, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.0999999046325684, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox5.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.BorderWidth.Bottom = new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox5.Style.LineWidth = new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.Value = "Empfänger";
            // 
            // tbADROrt
            // 
            this.tbADROrt.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.1081247329711914, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(6.0999999046325684, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbADROrt.Name = "tbADROrt";
            this.tbADROrt.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5999999046325684, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbADROrt.Value = "Wuppteral";
            // 
            // tbADRStr
            // 
            this.tbADRStr.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.599999725818634, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(5.59999942779541, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbADRStr.Name = "tbADRStr";
            this.tbADRStr.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.1000003814697266, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbADRStr.Value = "Schönebecker Platz 11";
            // 
            // tbADRName1
            // 
            this.tbADRName1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.599999725818634, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(5.148953914642334, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbADRName1.Name = "tbADRName1";
            this.tbADRName1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.1000003814697266, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbADRName1.Value = "Peter Holzrichter GmbH";
            // 
            // tbFirmaAbsender
            // 
            this.tbFirmaAbsender.Format = "";
            this.tbFirmaAbsender.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.599999725818634, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(4.7000002861022949, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbFirmaAbsender.Name = "tbFirmaAbsender";
            this.tbFirmaAbsender.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.0999999046325684, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbFirmaAbsender.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.tbFirmaAbsender.Style.BorderWidth.Bottom = new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.tbFirmaAbsender.Style.Font.Bold = true;
            this.tbFirmaAbsender.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbFirmaAbsender.Style.LineWidth = new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.tbFirmaAbsender.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.tbFirmaAbsender.Value = "Absender";
            // 
            // tbADRPLZ
            // 
            this.tbADRPLZ.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.599999725818634, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(6.0999999046325684, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbADRPLZ.Name = "tbADRPLZ";
            this.tbADRPLZ.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbADRPLZ.Value = "42283";
            // 
            // tbEPLZ
            // 
            this.tbEPLZ.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.60812419652938843, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(8.7999992370605469, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEPLZ.Name = "tbEPLZ";
            this.tbEPLZ.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.1999998092651367, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEPLZ.Value = "";
            // 
            // tbEStr
            // 
            this.tbEStr.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.59999889135360718, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(8.3997993469238281, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEStr.Name = "tbEStr";
            this.tbEStr.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.1000003814697266, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEStr.Value = "";
            // 
            // tbEOrt
            // 
            this.tbEOrt.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.1081247329711914, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(8.7999992370605469, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEOrt.Name = "tbEOrt";
            this.tbEOrt.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5999999046325684, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEOrt.Value = "";
            // 
            // tbEFirma1
            // 
            this.tbEFirma1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.59999889135360718, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(7.5993995666503906, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEFirma1.Name = "tbEFirma1";
            this.tbEFirma1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.1000003814697266, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEFirma1.Value = "";
            // 
            // tbEFirma2
            // 
            this.tbEFirma2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.60812419652938843, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(7.9996004104614258, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEFirma2.Name = "tbEFirma2";
            this.tbEFirma2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.1000003814697266, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEFirma2.Value = "";
            // 
            // tbDate
            // 
            this.tbDate.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(11.741669654846191, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(6.7000002861022949, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbDate.Name = "tbDate";
            this.tbDate.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.1978037357330322, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39979961514472961, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbDate.Value = "[=Fields.Datum]";
            // 
            // tbLfsNr
            // 
            this.tbLfsNr.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(12.391670227050781, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(5.4003992080688477, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbLfsNr.Name = "tbLfsNr";
            this.tbLfsNr.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.1693758964538574, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.0999990701675415, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbLfsNr.Style.Font.Bold = true;
            this.tbLfsNr.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(18, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbLfsNr.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.tbLfsNr.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.tbLfsNr.Value = "[Fields.LfsNr]";
            // 
            // tbLfsDate
            // 
            this.tbLfsDate.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(11.741669654846191, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(7.1599874496459961, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbLfsDate.Name = "tbLfsDate";
            this.tbLfsDate.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.4000008106231689, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39979961514472961, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbLfsDate.Value = "[=Fields.Lieferscheindatum]";
            // 
            // tbFahrer
            // 
            this.tbFahrer.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.40000057220459, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(8.8000001907348633, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbFahrer.Name = "tbFahrer";
            this.tbFahrer.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(8.0757369995117188, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbFahrer.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbFahrer.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbFahrer.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbFahrer.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbFahrer.Value = "";
            // 
            // tbAuflieger
            // 
            this.tbAuflieger.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.40000057220459, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(8.3998003005981445, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbAuflieger.Name = "tbAuflieger";
            this.tbAuflieger.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(8.0757369995117188, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbAuflieger.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbAuflieger.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbAuflieger.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbAuflieger.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbAuflieger.Value = "";
            // 
            // tbZM
            // 
            this.tbZM.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.40000057220459, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(7.9996004104614258, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbZM.Name = "tbZM";
            this.tbZM.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(8.0757369995117188, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbZM.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbZM.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbZM.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbZM.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbZM.Value = "";
            // 
            // fDocs_Holzrichter
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = new Telerik.Reporting.Drawing.Unit(17.89990234375, Telerik.Reporting.Drawing.UnitType.Cm);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        protected Telerik.Reporting.PictureBox pbFussBK;
        private Table dt;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Table dtArtikelliste;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox tbIhrZeichen;
        private Telerik.Reporting.TextBox tbBestellNr;
        private Telerik.Reporting.TextBox tbAbruf;
        private PageHeaderSection pageHeader;
        protected Telerik.Reporting.PictureBox pbLogo;
        public Telerik.Reporting.TextBox tbDocName;
        protected Telerik.Reporting.TextBox tbDatum;
        private Telerik.Reporting.TextBox tbLfsDatum;
        protected Telerik.Reporting.TextBox textBox5;
        protected Telerik.Reporting.TextBox tbADROrt;
        protected Telerik.Reporting.TextBox tbADRStr;
        protected Telerik.Reporting.TextBox tbADRName1;
        protected Telerik.Reporting.TextBox tbFirmaAbsender;
        protected Telerik.Reporting.TextBox tbADRPLZ;
        protected Telerik.Reporting.TextBox tbEPLZ;
        protected Telerik.Reporting.TextBox tbEStr;
        protected Telerik.Reporting.TextBox tbEOrt;
        protected Telerik.Reporting.TextBox tbEFirma1;
        protected Telerik.Reporting.TextBox tbEFirma2;
        protected Telerik.Reporting.TextBox tbDate;
        public Telerik.Reporting.TextBox tbLfsNr;
        protected Telerik.Reporting.TextBox tbLfsDate;
        protected Telerik.Reporting.TextBox tbFahrer;
        protected Telerik.Reporting.TextBox tbAuflieger;
        protected Telerik.Reporting.TextBox tbZM;
    }
}