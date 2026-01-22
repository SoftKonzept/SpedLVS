namespace Sped4
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class repTransportAuftrag
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
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.Auftrag = new Telerik.Reporting.SqlDataSource();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.tbAuftrag = new Telerik.Reporting.TextBox();
            this.tbDateAuftrag = new Telerik.Reporting.TextBox();
            this.tbFBez = new Telerik.Reporting.TextBox();
            this.tbSUFirma1 = new Telerik.Reporting.TextBox();
            this.tbSUFirma2 = new Telerik.Reporting.TextBox();
            this.tbSUAp = new Telerik.Reporting.TextBox();
            this.tbSUStr = new Telerik.Reporting.TextBox();
            this.tbSUPLZ = new Telerik.Reporting.TextBox();
            this.tbSUOrt = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.TableArtikel = new Telerik.Reporting.Table();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.tbFracht = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = new Telerik.Reporting.Drawing.Unit(10.100000381469727, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.tbFBez,
            this.tbSUFirma1,
            this.tbSUFirma2,
            this.tbSUAp,
            this.tbSUStr,
            this.tbSUPLZ,
            this.tbSUOrt,
            this.textBox2,
            this.tbAuftrag,
            this.textBox3,
            this.tbDateAuftrag});
            this.pageHeader.Name = "pageHeader";
            // 
            // detail
            // 
            this.detail.Height = new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Cm);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox4,
            this.textBox5,
            this.TableArtikel,
            this.tbFracht,
            this.textBox12});
            this.detail.Name = "detail";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = new Telerik.Reporting.Drawing.Unit(2.1999988555908203, Telerik.Reporting.Drawing.UnitType.Cm);
            this.pageFooter.Name = "pageFooter";
            // 
            // Auftrag
            // 
            this.Auftrag.Name = "Auftrag";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.00010012308484874666, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.4000000953674316, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.69990032911300659, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox1.Style.Font.Name = "Arial Black";
            this.textBox1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(12, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox1.Value = "Transportauftrag";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(10.819900512695313, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.90000057220459, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49979948997497559, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox2.Style.Font.Name = "Arial Black";
            this.textBox2.Value = "Auftrag/Pos.";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(12.019902229309082, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(9.6002006530761719, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.7000004053115845, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49979948997497559, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox3.Style.Font.Name = "Arial Black";
            this.textBox3.Value = "Datum";
            // 
            // tbAuftrag
            // 
            this.tbAuftrag.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(13.720100402832031, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbAuftrag.Name = "tbAuftrag";
            this.tbAuftrag.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.1997995376586914, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49979948997497559, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbAuftrag.Value = "textBox5";
            // 
            // tbDateAuftrag
            // 
            this.tbDateAuftrag.Format = "{0:d}";
            this.tbDateAuftrag.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(13.720100402832031, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(9.6002006530761719, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbDateAuftrag.Name = "tbDateAuftrag";
            this.tbDateAuftrag.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.1997995376586914, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.499799907207489, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbDateAuftrag.Value = "textBox7";
            // 
            // tbFBez
            // 
            this.tbFBez.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.9921220680698752E-05, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(4, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbFBez.Name = "tbFBez";
            this.tbFBez.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.39989972114563, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49899649620056152, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbFBez.Value = "Firmenbezeichnung";
            // 
            // tbSUFirma1
            // 
            this.tbSUFirma1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.9921220680698752E-05, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(4.6002001762390137, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbSUFirma1.Name = "tbSUFirma1";
            this.tbSUFirma1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.39989972114563, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.498997300863266, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbSUFirma1.Value = "Firma1";
            // 
            // tbSUFirma2
            // 
            this.tbSUFirma2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.9921220680698752E-05, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(5.2004013061523438, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbSUFirma2.Name = "tbSUFirma2";
            this.tbSUFirma2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.39989972114563, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.498997300863266, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbSUFirma2.Value = "Firma2";
            // 
            // tbSUAp
            // 
            this.tbSUAp.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(5.8006019592285156, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbSUAp.Name = "tbSUAp";
            this.tbSUAp.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.3999998569488525, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49899649620056152, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbSUAp.Value = "z.HD. Herr.....";
            // 
            // tbSUStr
            // 
            this.tbSUStr.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.9921220680698752E-05, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(6.4008016586303711, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbSUStr.Name = "tbSUStr";
            this.tbSUStr.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.3999001979827881, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.498997300863266, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbSUStr.Value = "Strasse";
            // 
            // tbSUPLZ
            // 
            this.tbSUPLZ.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.9921220680698752E-05, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(7.001002311706543, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbSUPLZ.Name = "tbSUPLZ";
            this.tbSUPLZ.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.4999001026153565, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49899649620056152, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbSUPLZ.Value = "PLZ";
            // 
            // tbSUOrt
            // 
            this.tbSUOrt.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.8999998569488525, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(7.001002311706543, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbSUOrt.Name = "tbSUOrt";
            this.tbSUOrt.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.0000002384185791, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.498997300863266, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbSUOrt.Value = "Ort";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.0999999046325684, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.4999996423721314, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.5000004768371582, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.0000009536743164, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox4.Value = "Versender:";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(8.30000114440918, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.4999988079071045, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.4999995231628418, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.0000009536743164, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox5.Value = "Empfänger:";
            // 
            // TableArtikel
            // 
            this.TableArtikel.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(4.5725464820861816, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.TableArtikel.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(4.5725464820861816, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.TableArtikel.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(4.5725464820861816, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.TableArtikel.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(1.9999997615814209, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.TableArtikel.Body.SetCellContent(0, 0, this.textBox9);
            this.TableArtikel.Body.SetCellContent(0, 1, this.textBox10);
            this.TableArtikel.Body.SetCellContent(0, 2, this.textBox11);
            tableGroup1.ReportItem = this.textBox6;
            tableGroup2.ReportItem = this.textBox7;
            tableGroup3.ReportItem = this.textBox8;
            this.TableArtikel.ColumnGroups.Add(tableGroup1);
            this.TableArtikel.ColumnGroups.Add(tableGroup2);
            this.TableArtikel.ColumnGroups.Add(tableGroup3);
            this.TableArtikel.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.textBox10,
            this.textBox11});
            this.TableArtikel.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.0823612213134766, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(5.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.TableArtikel.Name = "TableArtikel";
            tableGroup4.Grouping.AddRange(new Telerik.Reporting.Data.Grouping[] {
            new Telerik.Reporting.Data.Grouping("")});
            tableGroup4.Name = "DetailGroup";
            this.TableArtikel.RowGroups.Add(tableGroup4);
            this.TableArtikel.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(13.717638969421387, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.9999995231628418, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5725464820861816, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.9999997615814209, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // textBox7
            // 
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5725464820861816, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.9999997615814209, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // textBox8
            // 
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5725464820861816, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.9999997615814209, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // textBox9
            // 
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5725464820861816, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.9999997615814209, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // textBox10
            // 
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5725464820861816, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.9999997615814209, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // textBox11
            // 
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5725464820861816, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.9999997615814209, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // tbFracht
            // 
            this.tbFracht.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.9000000953674316, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(10, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbFracht.Name = "tbFracht";
            this.tbFracht.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.9999997615814209, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000050067901611, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbFracht.Value = "textBox12";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.0823612213134766, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(10.000000953674316, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.0000002384185791, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59999889135360718, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox12.Value = "Fracht €:";
            // 
            // repTransportAuftrag
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeader,
            this.detail,
            this.pageFooter});
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(2.5399999618530273, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(2.5399999618530273, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(2.5399999618530273, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(2.5399999618530273, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = new Telerik.Reporting.Drawing.Unit(15.920000076293945, Telerik.Reporting.Drawing.UnitType.Cm);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private SqlDataSource Auftrag;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox tbAuftrag;
        private Telerik.Reporting.TextBox tbDateAuftrag;
        private Telerik.Reporting.TextBox tbFBez;
        private Telerik.Reporting.TextBox tbSUFirma1;
        private Telerik.Reporting.TextBox tbSUFirma2;
        private Telerik.Reporting.TextBox tbSUAp;
        private Telerik.Reporting.TextBox tbSUStr;
        private Telerik.Reporting.TextBox tbSUPLZ;
        private Telerik.Reporting.TextBox tbSUOrt;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Table TableArtikel;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox tbFracht;
        private Telerik.Reporting.TextBox textBox12;
    }
}