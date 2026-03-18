namespace Sped4
{
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;
  using Telerik.Reporting;
  using Telerik.Reporting.Drawing;

  partial class docTransportauftrag
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
      this.textBox1 = new Telerik.Reporting.TextBox();
      this.textBox2 = new Telerik.Reporting.TextBox();
      this.textBox3 = new Telerik.Reporting.TextBox();
      this.DetailTransportauftrag = new Telerik.Reporting.DetailSection();
      this.tbBeladestelle = new Telerik.Reporting.TextBox();
      this.dt = new Telerik.Reporting.Table();
      this.textBox4 = new Telerik.Reporting.TextBox();
      this.textBox5 = new Telerik.Reporting.TextBox();
      this.textBox6 = new Telerik.Reporting.TextBox();
      this.tbTransportauftrag = new Telerik.Reporting.TextBox();
      this.tbBFirma1 = new Telerik.Reporting.TextBox();
      this.tbBFirma2 = new Telerik.Reporting.TextBox();
      this.tbBPLZ = new Telerik.Reporting.TextBox();
      this.tbBStr = new Telerik.Reporting.TextBox();
      this.tbBOrt = new Telerik.Reporting.TextBox();
      this.tbEOrt = new Telerik.Reporting.TextBox();
      this.tbEStr = new Telerik.Reporting.TextBox();
      this.tbEPLZ = new Telerik.Reporting.TextBox();
      this.tbEFirma2 = new Telerik.Reporting.TextBox();
      this.tbEFirma1 = new Telerik.Reporting.TextBox();
      this.tbEntladestelle = new Telerik.Reporting.TextBox();
      this.tbB_Datum = new Telerik.Reporting.TextBox();
      this.tbBDatum = new Telerik.Reporting.TextBox();
      this.textBox7 = new Telerik.Reporting.TextBox();
      this.tbBZF = new Telerik.Reporting.TextBox();
      this.textBox8 = new Telerik.Reporting.TextBox();
      this.textBox9 = new Telerik.Reporting.TextBox();
      this.tbEDatum = new Telerik.Reporting.TextBox();
      this.tbEZF = new Telerik.Reporting.TextBox();
      this.textBox10 = new Telerik.Reporting.TextBox();
      this.textBox11 = new Telerik.Reporting.TextBox();
      this.tbLadenummer = new Telerik.Reporting.TextBox();
      this.textBox12 = new Telerik.Reporting.TextBox();
      this.textBox13 = new Telerik.Reporting.TextBox();
      // 
      // textBox1
      // 
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5281243324279785, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm));
      // 
      // textBox2
      // 
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5281243324279785, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm));
      // 
      // textBox3
      // 
      this.textBox3.Name = "textBox3";
      this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5281243324279785, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm));
      // 
      // DetailTransportauftrag
      // 
      this.DetailTransportauftrag.Height = new Telerik.Reporting.Drawing.Unit(11.09999942779541, Telerik.Reporting.Drawing.UnitType.Cm);
      this.DetailTransportauftrag.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.tbBeladestelle,
            this.dt,
            this.tbTransportauftrag,
            this.tbBFirma1,
            this.tbBFirma2,
            this.tbBPLZ,
            this.tbBStr,
            this.tbBOrt,
            this.tbEOrt,
            this.tbEStr,
            this.tbEPLZ,
            this.tbEFirma2,
            this.tbEFirma1,
            this.tbEntladestelle,
            this.tbB_Datum,
            this.tbBDatum,
            this.textBox7,
            this.tbBZF,
            this.textBox8,
            this.textBox9,
            this.tbEDatum,
            this.tbEZF,
            this.textBox10,
            this.textBox11,
            this.tbLadenummer,
            this.textBox12,
            this.textBox13});
      this.DetailTransportauftrag.Name = "DetailTransportauftrag";
      // 
      // tbBeladestelle
      // 
      this.tbBeladestelle.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.9921220680698752E-05, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.063872218132019, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBeladestelle.Name = "tbBeladestelle";
      this.tbBeladestelle.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBeladestelle.Style.Font.Bold = true;
      this.tbBeladestelle.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbBeladestelle.Value = "Beladestelle:";
      // 
      // dt
      // 
      this.dt.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(4.5281243324279785, Telerik.Reporting.Drawing.UnitType.Cm)));
      this.dt.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(4.5281243324279785, Telerik.Reporting.Drawing.UnitType.Cm)));
      this.dt.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(4.5281243324279785, Telerik.Reporting.Drawing.UnitType.Cm)));
      this.dt.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm)));
      this.dt.Body.SetCellContent(0, 0, this.textBox4);
      this.dt.Body.SetCellContent(0, 1, this.textBox5);
      this.dt.Body.SetCellContent(0, 2, this.textBox6);
      tableGroup1.ReportItem = this.textBox1;
      tableGroup2.ReportItem = this.textBox2;
      tableGroup3.ReportItem = this.textBox3;
      this.dt.ColumnGroups.Add(tableGroup1);
      this.dt.ColumnGroups.Add(tableGroup2);
      this.dt.ColumnGroups.Add(tableGroup3);
      this.dt.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox5,
            this.textBox6,
            this.textBox1,
            this.textBox2,
            this.textBox3});
      this.dt.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.34375160932540894, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(6.4000000953674316, Telerik.Reporting.Drawing.UnitType.Cm));
      this.dt.Name = "dt";
      tableGroup4.Grouping.AddRange(new Telerik.Reporting.Data.Grouping[] {
            new Telerik.Reporting.Data.Grouping("")});
      tableGroup4.Name = "DetailGroup";
      this.dt.RowGroups.Add(tableGroup4);
      this.dt.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(13.584373474121094, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(4, Telerik.Reporting.Drawing.UnitType.Cm));
      // 
      // textBox4
      // 
      this.textBox4.Name = "textBox4";
      this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5281243324279785, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm));
      // 
      // textBox5
      // 
      this.textBox5.Name = "textBox5";
      this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5281243324279785, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm));
      // 
      // textBox6
      // 
      this.textBox6.Name = "textBox6";
      this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5281243324279785, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm));
      // 
      // tbTransportauftrag
      // 
      this.tbTransportauftrag.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.9921220680698752E-05, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbTransportauftrag.Name = "tbTransportauftrag";
      this.tbTransportauftrag.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.7999000549316406, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000008344650269, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbTransportauftrag.Style.Font.Bold = true;
      this.tbTransportauftrag.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(12, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbTransportauftrag.Value = "Transportauftrag ";
      // 
      // tbBFirma1
      // 
      this.tbBFirma1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.00010012308484874666, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.4640722274780273, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBFirma1.Name = "tbBFirma1";
      this.tbBFirma1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBFirma1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbBFirma1.Value = "textBox1";
      // 
      // tbBFirma2
      // 
      this.tbBFirma2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.8642722368240356, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBFirma2.Name = "tbBFirma2";
      this.tbBFirma2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBFirma2.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbBFirma2.Value = "textBox1";
      // 
      // tbBPLZ
      // 
      this.tbBPLZ.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.6646726131439209, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBPLZ.Name = "tbBPLZ";
      this.tbBPLZ.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.6000000238418579, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBPLZ.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbBPLZ.Value = "textBox1";
      // 
      // tbBStr
      // 
      this.tbBStr.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.264472484588623, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBStr.Name = "tbBStr";
      this.tbBStr.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBStr.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbBStr.Value = "textBox1";
      // 
      // tbBOrt
      // 
      this.tbBOrt.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.9999997615814209, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.6646726131439209, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBOrt.Name = "tbBOrt";
      this.tbBOrt.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBOrt.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbBOrt.Value = "textBox1";
      // 
      // tbEOrt
      // 
      this.tbEOrt.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.2843751907348633, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.5999999046325684, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEOrt.Name = "tbEOrt";
      this.tbEOrt.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEOrt.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbEOrt.Value = "textBox1";
      // 
      // tbEStr
      // 
      this.tbEStr.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.2000000476837158, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEStr.Name = "tbEStr";
      this.tbEStr.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEStr.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbEStr.Value = "textBox1";
      // 
      // tbEPLZ
      // 
      this.tbEPLZ.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.5999999046325684, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEPLZ.Name = "tbEPLZ";
      this.tbEPLZ.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.6000000238418579, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEPLZ.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbEPLZ.Value = "textBox1";
      // 
      // tbEFirma2
      // 
      this.tbEFirma2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.7999999523162842, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEFirma2.Name = "tbEFirma2";
      this.tbEFirma2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEFirma2.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbEFirma2.Value = "textBox1";
      // 
      // tbEFirma1
      // 
      this.tbEFirma1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.4000000953674316, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEFirma1.Name = "tbEFirma1";
      this.tbEFirma1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEFirma1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbEFirma1.Value = "textBox1";
      // 
      // tbEntladestelle
      // 
      this.tbEntladestelle.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.99999988079071045, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEntladestelle.Name = "tbEntladestelle";
      this.tbEntladestelle.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEntladestelle.Style.Font.Bold = true;
      this.tbEntladestelle.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbEntladestelle.Value = "Entladestelle";
      // 
      // tbB_Datum
      // 
      this.tbB_Datum.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.00010012308484874666, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.3999998569488525, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbB_Datum.Name = "tbB_Datum";
      this.tbB_Datum.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.0999999046325684, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbB_Datum.Style.Font.Bold = true;
      this.tbB_Datum.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbB_Datum.Value = "Beladedatum:";
      // 
      // tbBDatum
      // 
      this.tbBDatum.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.2999999523162842, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.3999998569488525, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBDatum.Name = "tbBDatum";
      this.tbBDatum.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.7000999450683594, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBDatum.Style.Font.Bold = false;
      this.tbBDatum.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbBDatum.Value = "Beladedatum";
      // 
      // textBox7
      // 
      this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.00010012308484874666, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.8001999855041504, Telerik.Reporting.Drawing.UnitType.Cm));
      this.textBox7.Name = "tbB_Datum";
      this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.0999999046325684, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.textBox7.Style.Font.Bold = true;
      this.textBox7.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.textBox7.Value = "Zeitfenster:";
      // 
      // tbBZF
      // 
      this.tbBZF.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(2.2999999523162842, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.8001999855041504, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBZF.Name = "tbBZF";
      this.tbBZF.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.7000999450683594, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbBZF.Style.Font.Bold = false;
      this.tbBZF.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbBZF.Value = "Zeitfenster";
      // 
      // textBox8
      // 
      this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.3999998569488525, Telerik.Reporting.Drawing.UnitType.Cm));
      this.textBox8.Name = "tbB_Datum";
      this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.0999999046325684, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.textBox8.Style.Font.Bold = true;
      this.textBox8.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.textBox8.Value = "Beladedatum:";
      // 
      // textBox9
      // 
      this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.8001999855041504, Telerik.Reporting.Drawing.UnitType.Cm));
      this.textBox9.Name = "tbB_Datum";
      this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.0999999046325684, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.textBox9.Style.Font.Bold = true;
      this.textBox9.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.textBox9.Value = "Zeitfenster:";
      // 
      // tbEDatum
      // 
      this.tbEDatum.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.7000007629394531, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.3999998569488525, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEDatum.Name = "tbEDatum";
      this.tbEDatum.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.5843734741210938, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEDatum.Style.Font.Bold = false;
      this.tbEDatum.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbEDatum.Value = "Entladedatum";
      // 
      // tbEZF
      // 
      this.tbEZF.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.7000007629394531, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.8001999855041504, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEZF.Name = "tbEZF";
      this.tbEZF.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.7000999450683594, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbEZF.Style.Font.Bold = false;
      this.tbEZF.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbEZF.Value = "Zeitfenster";
      // 
      // textBox10
      // 
      this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.00010012308484874666, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(4.9000000953674316, Telerik.Reporting.Drawing.UnitType.Cm));
      this.textBox10.Name = "tbBeladestelle";
      this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.textBox10.Style.Font.Bold = true;
      this.textBox10.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
      this.textBox10.Style.Font.Underline = true;
      this.textBox10.Value = "Materialangaben";
      // 
      // textBox11
      // 
      this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.70000004768371582, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(5.5, Telerik.Reporting.Drawing.UnitType.Cm));
      this.textBox11.Name = "tbBeladestelle";
      this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.2999999523162842, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.textBox11.Style.Font.Bold = true;
      this.textBox11.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.textBox11.Value = "Ladenummer:";
      // 
      // tbLadenummer
      // 
      this.tbLadenummer.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(3.3999998569488525, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(5.5, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbLadenummer.Name = "tbLadenummer";
      this.tbLadenummer.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.6000998020172119, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.tbLadenummer.Style.Font.Bold = false;
      this.tbLadenummer.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.tbLadenummer.Value = "Zeitfenster";
      // 
      // textBox12
      // 
      this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(7.4562482833862305, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(4.9000000953674316, Telerik.Reporting.Drawing.UnitType.Cm));
      this.textBox12.Name = "tbBeladestelle";
      this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.2999999523162842, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.textBox12.Style.Font.Bold = true;
      this.textBox12.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.textBox12.Value = "Fracht €:";
      // 
      // textBox13
      // 
      this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(7.4562482833862305, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(5.5, Telerik.Reporting.Drawing.UnitType.Cm));
      this.textBox13.Name = "textBox13";
      this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.2999999523162842, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
      this.textBox13.Style.Font.Bold = true;
      this.textBox13.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
      this.textBox13.Value = "Info:";

    }
    #endregion

    private Telerik.Reporting.DetailSection DetailTransportauftrag;
    private Telerik.Reporting.TextBox tbBeladestelle;
    private Table dt;
    private Telerik.Reporting.TextBox textBox4;
    private Telerik.Reporting.TextBox textBox5;
    private Telerik.Reporting.TextBox textBox6;
    private Telerik.Reporting.TextBox textBox1;
    private Telerik.Reporting.TextBox textBox2;
    private Telerik.Reporting.TextBox textBox3;
    private Telerik.Reporting.TextBox tbTransportauftrag;
    private Telerik.Reporting.TextBox tbBFirma1;
    private Telerik.Reporting.TextBox tbBFirma2;
    private Telerik.Reporting.TextBox tbBPLZ;
    private Telerik.Reporting.TextBox tbBStr;
    private Telerik.Reporting.TextBox tbBOrt;
    private Telerik.Reporting.TextBox tbEOrt;
    private Telerik.Reporting.TextBox tbEStr;
    private Telerik.Reporting.TextBox tbEPLZ;
    private Telerik.Reporting.TextBox tbEFirma2;
    private Telerik.Reporting.TextBox tbEFirma1;
    private Telerik.Reporting.TextBox tbEntladestelle;
    private Telerik.Reporting.TextBox tbB_Datum;
    private Telerik.Reporting.TextBox tbBDatum;
    private Telerik.Reporting.TextBox textBox7;
    private Telerik.Reporting.TextBox tbBZF;
    private Telerik.Reporting.TextBox textBox8;
    private Telerik.Reporting.TextBox textBox9;
    private Telerik.Reporting.TextBox tbEDatum;
    private Telerik.Reporting.TextBox tbEZF;
    private Telerik.Reporting.TextBox textBox10;
    private Telerik.Reporting.TextBox textBox11;
    private Telerik.Reporting.TextBox tbLadenummer;
    private Telerik.Reporting.TextBox textBox12;
    private Telerik.Reporting.TextBox textBox13;
  }
}