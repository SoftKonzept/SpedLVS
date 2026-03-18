namespace LVS.Dokumente
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    partial class docLieferschein
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
            Telerik.Reporting.TableGroup tableGroup19 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.Drawing.FormattingRule formattingRule1 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector1 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector2 = new Telerik.Reporting.Drawing.DescendantSelector();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.tbColWerksnummer = new Telerik.Reporting.TextBox();
            this.tbColAnzahl = new Telerik.Reporting.TextBox();
            this.tbColGut = new Telerik.Reporting.TextBox();
            this.tbColAbmessungen = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.tbColgemGewicht = new Telerik.Reporting.TextBox();
            this.lfs_pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.tbBKSteuer = new Telerik.Reporting.TextBox();
            this.tbBKUST = new Telerik.Reporting.TextBox();
            this.tbBKPLZOrt = new Telerik.Reporting.TextBox();
            this.tbBKStr = new Telerik.Reporting.TextBox();
            this.tbBKFirma2 = new Telerik.Reporting.TextBox();
            this.tbBKFirma1 = new Telerik.Reporting.TextBox();
            this.pbZert = new Telerik.Reporting.PictureBox();
            this.pbLogo = new Telerik.Reporting.PictureBox();
            this.panelVersender = new Telerik.Reporting.Panel();
            this.tbVHeadline = new Telerik.Reporting.TextBox();
            this.tbVName1 = new Telerik.Reporting.TextBox();
            this.tbVName2 = new Telerik.Reporting.TextBox();
            this.tbVName3 = new Telerik.Reporting.TextBox();
            this.tbVStr = new Telerik.Reporting.TextBox();
            this.tbVPLZ = new Telerik.Reporting.TextBox();
            this.tbVOrt = new Telerik.Reporting.TextBox();
            this.tbDocName = new Telerik.Reporting.TextBox();
            this.tbLieferscheinNr = new Telerik.Reporting.TextBox();
            this.tbOrtDatum = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.tbAuftrag = new Telerik.Reporting.TextBox();
            this.tbTelefon = new Telerik.Reporting.TextBox();
            this.tbFax = new Telerik.Reporting.TextBox();
            this.lfs_detail = new Telerik.Reporting.DetailSection();
            this.table1 = new Telerik.Reporting.Table();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.tbZM = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.tbAuflieger = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.tbFahrer = new Telerik.Reporting.TextBox();
            this.panel1 = new Telerik.Reporting.Panel();
            this.tbEHeadline = new Telerik.Reporting.TextBox();
            this.tbEName1 = new Telerik.Reporting.TextBox();
            this.tbEName2 = new Telerik.Reporting.TextBox();
            this.tbEName3 = new Telerik.Reporting.TextBox();
            this.tbEStr = new Telerik.Reporting.TextBox();
            this.tbEPLZ = new Telerik.Reporting.TextBox();
            this.tbEOrt = new Telerik.Reporting.TextBox();
            this.panelUnterschrift = new Telerik.Reporting.Panel();
            this.dtUnterschrift = new Telerik.Reporting.Table();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.panelArtikel = new Telerik.Reporting.Panel();
            this.dtArtikel = new Telerik.Reporting.Table();
            this.tbME = new Telerik.Reporting.TextBox();
            this.tbGut = new Telerik.Reporting.TextBox();
            this.tbAbmessung = new Telerik.Reporting.TextBox();
            this.tbGewicht = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox36 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.tbNotiz = new Telerik.Reporting.TextBox();
            this.tbLadenummer = new Telerik.Reporting.TextBox();
            this.tbZF = new Telerik.Reporting.TextBox();
            this.lfs_pageFooter = new Telerik.Reporting.PageFooterSection();
            this.tbText = new Telerik.Reporting.TextBox();
            this.tbHR = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // textBox6
            // 
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.0890769958496094, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.800000011920929, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // textBox9
            // 
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(10.372373580932617, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.800000011920929, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // tbColWerksnummer
            // 
            this.tbColWerksnummer.Name = "tbColWerksnummer";
            this.tbColWerksnummer.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.2118852138519287, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999985098838806, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbColWerksnummer.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbColWerksnummer.Style.Font.Bold = true;
            this.tbColWerksnummer.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbColWerksnummer.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.tbColWerksnummer.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.tbColWerksnummer.StyleName = "";
            this.tbColWerksnummer.Value = "Werksnr.";
            // 
            // tbColAnzahl
            // 
            this.tbColAnzahl.Name = "tbColAnzahl";
            this.tbColAnzahl.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.059617280960083, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999985098838806, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbColAnzahl.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbColAnzahl.Style.Font.Bold = true;
            this.tbColAnzahl.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbColAnzahl.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.tbColAnzahl.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.tbColAnzahl.Value = "ME";
            // 
            // tbColGut
            // 
            this.tbColGut.Name = "tbColGut";
            this.tbColGut.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.7352721691131592, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999985098838806, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbColGut.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbColGut.Style.Font.Bold = true;
            this.tbColGut.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbColGut.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.tbColGut.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.tbColGut.Value = "Gut";
            // 
            // tbColAbmessungen
            // 
            this.tbColAbmessungen.Name = "tbColAbmessungen";
            this.tbColAbmessungen.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.5883514881134033, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999985098838806, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbColAbmessungen.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbColAbmessungen.Style.Font.Bold = true;
            this.tbColAbmessungen.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbColAbmessungen.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.tbColAbmessungen.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.tbColAbmessungen.Value = "Abmessungen [mm]";
            // 
            // textBox16
            // 
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.6111979484558105, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999985098838806, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox16.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.Font.Bold = true;
            this.textBox16.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.StyleName = "";
            // 
            // tbColgemGewicht
            // 
            this.tbColgemGewicht.Format = "{0:N2}";
            this.tbColgemGewicht.Name = "tbColgemGewicht";
            this.tbColgemGewicht.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.1858620643615723, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999985098838806, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbColgemGewicht.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbColgemGewicht.Style.Font.Bold = true;
            this.tbColgemGewicht.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbColgemGewicht.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.tbColgemGewicht.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.tbColgemGewicht.StyleName = "";
            this.tbColgemGewicht.Value = " Gewicht [kg]";
            // 
            // lfs_pageHeader
            // 
            this.lfs_pageHeader.Height = new Telerik.Reporting.Drawing.Unit(7.8999996185302734, Telerik.Reporting.Drawing.UnitType.Cm);
            this.lfs_pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.tbBKSteuer,
            this.tbBKUST,
            this.tbBKPLZOrt,
            this.tbBKStr,
            this.tbBKFirma2,
            this.tbBKFirma1,
            this.pbZert,
            this.pbLogo,
            this.panelVersender,
            this.tbDocName,
            this.tbLieferscheinNr,
            this.tbOrtDatum,
            this.textBox1,
            this.textBox10,
            this.textBox2,
            this.tbAuftrag,
            this.tbTelefon,
            this.tbFax});
            this.lfs_pageHeader.Name = "lfs_pageHeader";
            // 
            // tbBKSteuer
            // 
            this.tbBKSteuer.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(4.6968746185302734, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbBKSteuer.Name = "tbBKSteuer";
            this.tbBKSteuer.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.2199001312255859, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbBKSteuer.Value = "Steuer-Nr.: ";
            // 
            // tbBKUST
            // 
            this.tbBKUST.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(4.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbBKUST.Name = "tbBKUST";
            this.tbBKUST.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.2199001312255859, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbBKUST.Value = "UST-ID";
            // 
            // tbBKPLZOrt
            // 
            this.tbBKPLZOrt.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.9031248092651367, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbBKPLZOrt.Name = "tbBKPLZOrt";
            this.tbBKPLZOrt.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.8199005126953125, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbBKPLZOrt.Value = "PLZ - Ort\t";
            // 
            // tbBKStr
            // 
            this.tbBKStr.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.5241260528564453, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.5095748901367188, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbBKStr.Name = "tbBKStr";
            this.tbBKStr.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.8199005126953125, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbBKStr.Value = "Straﬂe";
            // 
            // tbBKFirma2
            // 
            this.tbBKFirma2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.109375, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbBKFirma2.Name = "tbBKFirma2";
            this.tbBKFirma2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.8199005126953125, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbBKFirma2.Value = "Name2";
            // 
            // tbBKFirma1
            // 
            this.tbBKFirma1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.7124998569488525, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbBKFirma1.Name = "tbBKFirma1";
            this.tbBKFirma1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.3199009895324707, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbBKFirma1.Value = "Name1";
            // 
            // pbZert
            // 
            this.pbZert.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(14.8614501953125, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.7197837829589844, Telerik.Reporting.Drawing.UnitType.Cm));
            this.pbZert.Name = "pbZert";
            this.pbZert.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.6000003814697266, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.59999942779541, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.11958370357751846, Telerik.Reporting.Drawing.UnitType.Cm));
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.3199009895324707, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.5999999046325684, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // panelVersender
            // 
            this.panelVersender.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.tbVHeadline,
            this.tbVName1,
            this.tbVName2,
            this.tbVName3,
            this.tbVStr,
            this.tbVPLZ,
            this.tbVOrt});
            this.panelVersender.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.00010012308484874666, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.9031248092651367, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panelVersender.Name = "panelVersender";
            this.panelVersender.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.60040020942688, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // tbVHeadline
            // 
            this.tbVHeadline.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbVHeadline.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbVHeadline.Name = "tbVHeadline";
            this.tbVHeadline.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39959931373596191, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbVHeadline.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbVHeadline.Style.Font.Bold = true;
            this.tbVHeadline.Value = "Versender:";
            // 
            // tbVName1
            // 
            this.tbVName1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbVName1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39959931373596191, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbVName1.Name = "tbVName1";
            this.tbVName1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.79979997873306274, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbVName1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.tbVName1.Value = "textBox4";
            // 
            // tbVName2
            // 
            this.tbVName2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbVName2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.1993992328643799, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbVName2.Name = "tbVName2";
            this.tbVName2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbVName2.Value = "textBox4";
            // 
            // tbVName3
            // 
            this.tbVName3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbVName3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.599399209022522, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbVName3.Name = "tbVName3";
            this.tbVName3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.4201836884021759, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbVName3.Value = "textBox4";
            // 
            // tbVStr
            // 
            this.tbVStr.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbVStr.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.019582986831665, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbVStr.Name = "tbVStr";
            this.tbVStr.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.3000001907348633, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbVStr.Value = "textBox4";
            // 
            // tbVPLZ
            // 
            this.tbVPLZ.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbVPLZ.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.4195828437805176, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbVPLZ.Name = "tbVPLZ";
            this.tbVPLZ.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.8997992277145386, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.1808173656463623, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbVPLZ.Value = "textBox4";
            // 
            // tbVOrt
            // 
            this.tbVOrt.Dock = System.Windows.Forms.DockStyle.Right;
            this.tbVOrt.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.8999994993209839, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.4195828437805176, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbVOrt.Name = "tbVOrt";
            this.tbVOrt.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.40000057220459, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.1808173656463623, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbVOrt.Value = "textBox4";
            // 
            // tbDocName
            // 
            this.tbDocName.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.5241260528564453, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(6.0999999046325684, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbDocName.Name = "tbDocName";
            this.tbDocName.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.3371243476867676, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.599999725818634, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbDocName.Style.Font.Bold = true;
            this.tbDocName.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(12, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbDocName.Value = "DocName";
            // 
            // tbLieferscheinNr
            // 
            this.tbLieferscheinNr.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(14.8614501953125, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(6.0999999046325684, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbLieferscheinNr.Name = "tbLieferscheinNr";
            this.tbLieferscheinNr.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.5998992919921875, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.599999725818634, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbLieferscheinNr.Value = "123";
            // 
            // tbOrtDatum
            // 
            this.tbOrtDatum.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(7.3001995086669922, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbOrtDatum.Name = "tbOrtDatum";
            this.tbOrtDatum.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.8000006675720215, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.50000017881393433, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbOrtDatum.Value = "OrtDatum";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(14.389176368713379, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(7.3001995086669922, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.3108235597610474, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox1.Value = "Seite: ";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(15.700201034545898, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(7.3000984191894531, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox10.Name = "textBox2";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.7612491846084595, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.50010108947753906, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox10.Value = "=PageNumber";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(6.7999987602233887, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.5000007152557373, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.5, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox2.Value = "Auftrag:";
            // 
            // tbAuftrag
            // 
            this.tbAuftrag.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(11, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(6.7999987602233887, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbAuftrag.Name = "tbAuftrag";
            this.tbAuftrag.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(6.3922867774963379, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.50000017881393433, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbAuftrag.Value = "textBox14";
            // 
            // tbTelefon
            // 
            this.tbTelefon.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.5, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(5.13446044921875, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbTelefon.Name = "tbTelefon";
            this.tbTelefon.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.2198982238769531, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbTelefon.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbTelefon.Value = "Telefon";
            // 
            // tbFax
            // 
            this.tbFax.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.5241260528564453, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(5.5346603393554688, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbFax.Name = "tbFax";
            this.tbFax.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.1957736015319824, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbFax.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbFax.Value = "Fax";
            // 
            // lfs_detail
            // 
            this.lfs_detail.Height = new Telerik.Reporting.Drawing.Unit(16.999601364135742, Telerik.Reporting.Drawing.UnitType.Cm);
            this.lfs_detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.table1,
            this.panel1,
            this.panelUnterschrift,
            this.panelArtikel,
            this.tbNotiz,
            this.tbLadenummer,
            this.tbZF});
            this.lfs_detail.Name = "lfs_detail";
            // 
            // table1
            // 
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(2.4782631397247314, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(5.0976381301879883, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(0.60000008344650269, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(0.60000002384185791, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(0.60000002384185791, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.table1.Body.SetCellContent(0, 0, this.textBox8);
            this.table1.Body.SetCellContent(0, 1, this.tbZM);
            this.table1.Body.SetCellContent(1, 0, this.textBox7);
            this.table1.Body.SetCellContent(1, 1, this.tbAuflieger);
            this.table1.Body.SetCellContent(2, 0, this.textBox11);
            this.table1.Body.SetCellContent(2, 1, this.tbFahrer);
            this.table1.ColumnGroups.Add(tableGroup1);
            this.table1.ColumnGroups.Add(tableGroup2);
            this.table1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox8,
            this.tbZM,
            this.textBox7,
            this.tbAuflieger,
            this.textBox11,
            this.tbFahrer});
            this.table1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(9.5952262878417969, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.80019986629486084, Telerik.Reporting.Drawing.UnitType.Cm));
            this.table1.Name = "table1";
            tableGroup4.Name = "Group1";
            tableGroup5.Name = "Group2";
            tableGroup6.Name = "Group3";
            tableGroup3.ChildGroups.Add(tableGroup4);
            tableGroup3.ChildGroups.Add(tableGroup5);
            tableGroup3.ChildGroups.Add(tableGroup6);

            tableGroup3.Name = "DetailGroup";
            this.table1.RowGroups.Add(tableGroup3);
            this.table1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.5759010314941406, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.7999999523162842, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // textBox8
            // 
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.4782631397247314, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox8.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "Zugmaschine:";
            // 
            // tbZM
            // 
            this.tbZM.Name = "tbZM";
            this.tbZM.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.0976381301879883, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbZM.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbZM.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.tbZM.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbZM.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbZM.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            // 
            // textBox7
            // 
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.4782631397247314, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59999996423721313, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.StyleName = "";
            this.textBox7.Value = "Auflieger:";
            // 
            // tbAuflieger
            // 
            this.tbAuflieger.Name = "tbAuflieger";
            this.tbAuflieger.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.0976381301879883, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.59999996423721313, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbAuflieger.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbAuflieger.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbAuflieger.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbAuflieger.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.tbAuflieger.StyleName = "";
            // 
            // textBox11
            // 
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.4782631397247314, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox11.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.StyleName = "";
            this.textBox11.Value = "Fahrer:";
            // 
            // tbFahrer
            // 
            this.tbFahrer.Name = "tbFahrer";
            this.tbFahrer.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.0976381301879883, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.60000002384185791, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbFahrer.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbFahrer.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbFahrer.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbFahrer.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.tbFahrer.StyleName = "";
            // 
            // panel1
            // 
            this.panel1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.tbEHeadline,
            this.tbEName1,
            this.tbEName2,
            this.tbEName3,
            this.tbEStr,
            this.tbEPLZ,
            this.tbEOrt});
            this.panel1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.00010002215276472271, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40020003914833069, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panel1.Name = "panel1";
            this.panel1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.9001007080078125, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // tbEHeadline
            // 
            this.tbEHeadline.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbEHeadline.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEHeadline.Name = "tbEHeadline";
            this.tbEHeadline.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEHeadline.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.tbEHeadline.Style.Font.Bold = true;
            this.tbEHeadline.Value = "Empf‰nger:";
            // 
            // tbEName1
            // 
            this.tbEName1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbEName1.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEName1.Name = "tbEName1";
            this.tbEName1.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.80010074377059937, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEName1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.tbEName1.Value = "textBox4";
            // 
            // tbEName2
            // 
            this.tbEName2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbEName2.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.79979956150054932, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEName2.Name = "tbEName2";
            this.tbEName2.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEName2.Value = "textBox4";
            // 
            // tbEName3
            // 
            this.tbEName3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbEName3.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.1997995376586914, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEName3.Name = "tbEName3";
            this.tbEName3.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEName3.Value = "textBox4";
            // 
            // tbEStr
            // 
            this.tbEStr.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbEStr.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.5997995138168335, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEStr.Name = "tbEStr";
            this.tbEStr.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.40000000596046448, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEStr.Value = "textBox4";
            // 
            // tbEPLZ
            // 
            this.tbEPLZ.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbEPLZ.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.9997996091842651, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEPLZ.Name = "tbEPLZ";
            this.tbEPLZ.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.6999999284744263, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEPLZ.Value = "textBox4";
            // 
            // tbEOrt
            // 
            this.tbEOrt.Dock = System.Windows.Forms.DockStyle.Right;
            this.tbEOrt.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(1.8999994993209839, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.9997996091842651, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEOrt.Name = "tbEOrt";
            this.tbEOrt.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(5.40000057220459, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39999979734420776, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbEOrt.Value = "textBox4";
            // 
            // panelUnterschrift
            // 
            this.panelUnterschrift.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelUnterschrift.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.dtUnterschrift});
            this.panelUnterschrift.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(15.600000381469727, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panelUnterschrift.Name = "panelUnterschrift";
            this.panelUnterschrift.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(17.461450576782227, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.3996008634567261, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // dtUnterschrift
            // 
            this.dtUnterschrift.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(7.0890769958496094, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtUnterschrift.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(10.372373580932617, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtUnterschrift.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(0.49999994039535522, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtUnterschrift.Body.SetCellContent(0, 0, this.textBox12);
            this.dtUnterschrift.Body.SetCellContent(0, 1, this.textBox13);
            tableGroup7.ReportItem = this.textBox6;
            tableGroup8.ReportItem = this.textBox9;
            this.dtUnterschrift.ColumnGroups.Add(tableGroup7);
            this.dtUnterschrift.ColumnGroups.Add(tableGroup8);
            this.dtUnterschrift.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtUnterschrift.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox12,
            this.textBox13,
            this.textBox6,
            this.textBox9});
            this.dtUnterschrift.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.09960097074508667, Telerik.Reporting.Drawing.UnitType.Cm));
            this.dtUnterschrift.Name = "dtUnterschrift";
            tableGroup10.Name = "Group1";
            tableGroup9.ChildGroups.Add(tableGroup10);

            tableGroup9.Name = "DetailGroup";
            this.dtUnterschrift.RowGroups.Add(tableGroup9);
            this.dtUnterschrift.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(17.461450576782227, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.2999999523162842, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // textBox12
            // 
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(7.0890769958496094, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999994039535522, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox12.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Style.Font.Bold = true;
            this.textBox12.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox12.Value = "Ware erhalten :          Ort, Datum";
            // 
            // textBox13
            // 
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(10.372373580932617, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999994039535522, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox13.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Value = "(Unterschrift)";
            // 
            // panelArtikel
            // 
            this.panelArtikel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelArtikel.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.dtArtikel});
            this.panelArtikel.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.00010002215276472271, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(3.8000004291534424, Telerik.Reporting.Drawing.UnitType.Cm));
            this.panelArtikel.Name = "panelArtikel";
            this.panelArtikel.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(17.392187118530273, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(9.8997993469238281, Telerik.Reporting.Drawing.UnitType.Cm));
            // 
            // dtArtikel
            // 
            this.dtArtikel.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(2.2118852138519287, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikel.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(1.059617280960083, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikel.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(2.7352721691131592, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikel.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(3.5883517265319824, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikel.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(4.6111993789672852, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikel.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(new Telerik.Reporting.Drawing.Unit(3.1858620643615723, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikel.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(0.47937497496604919, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikel.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(0.49999991059303284, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikel.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(new Telerik.Reporting.Drawing.Unit(0.49999991059303284, Telerik.Reporting.Drawing.UnitType.Cm)));
            this.dtArtikel.Body.SetCellContent(0, 1, this.tbME);
            this.dtArtikel.Body.SetCellContent(0, 2, this.tbGut);
            this.dtArtikel.Body.SetCellContent(0, 3, this.tbAbmessung);
            this.dtArtikel.Body.SetCellContent(0, 5, this.tbGewicht);
            this.dtArtikel.Body.SetCellContent(1, 1, this.textBox19);
            this.dtArtikel.Body.SetCellContent(1, 2, this.textBox25);
            this.dtArtikel.Body.SetCellContent(1, 5, this.textBox36);
            this.dtArtikel.Body.SetCellContent(0, 0, this.textBox14);
            this.dtArtikel.Body.SetCellContent(1, 0, this.textBox15);
            this.dtArtikel.Body.SetCellContent(2, 0, this.textBox4);
            this.dtArtikel.Body.SetCellContent(2, 1, this.textBox5);
            this.dtArtikel.Body.SetCellContent(2, 2, this.textBox17);
            this.dtArtikel.Body.SetCellContent(2, 3, this.textBox18);
            this.dtArtikel.Body.SetCellContent(2, 5, this.textBox23);
            this.dtArtikel.Body.SetCellContent(1, 3, this.textBox30);
            this.dtArtikel.Body.SetCellContent(0, 4, this.textBox20);
            this.dtArtikel.Body.SetCellContent(1, 4, this.textBox21);
            this.dtArtikel.Body.SetCellContent(2, 4, this.textBox22);
            tableGroup11.Name = "Group6";
            tableGroup11.ReportItem = this.tbColWerksnummer;
            tableGroup12.ReportItem = this.tbColAnzahl;
            tableGroup13.ReportItem = this.tbColGut;
            tableGroup14.ReportItem = this.tbColAbmessungen;
            tableGroup15.Name = "Group2";
            tableGroup15.ReportItem = this.textBox16;
            tableGroup16.ReportItem = this.tbColgemGewicht;
            this.dtArtikel.ColumnGroups.Add(tableGroup11);
            this.dtArtikel.ColumnGroups.Add(tableGroup12);
            this.dtArtikel.ColumnGroups.Add(tableGroup13);
            this.dtArtikel.ColumnGroups.Add(tableGroup14);
            this.dtArtikel.ColumnGroups.Add(tableGroup15);
            this.dtArtikel.ColumnGroups.Add(tableGroup16);
            this.dtArtikel.ColumnHeadersPrintOnEveryPage = true;
            this.dtArtikel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtArtikel.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.tbME,
            this.tbGut,
            this.tbAbmessung,
            this.tbGewicht,
            this.textBox19,
            this.textBox25,
            this.textBox36,
            this.textBox14,
            this.textBox15,
            this.textBox4,
            this.textBox5,
            this.textBox17,
            this.textBox18,
            this.textBox23,
            this.textBox30,
            this.textBox20,
            this.textBox21,
            this.textBox22,
            this.tbColWerksnummer,
            this.tbColAnzahl,
            this.tbColGut,
            this.tbColAbmessungen,
            this.textBox16,
            this.tbColgemGewicht});
            this.dtArtikel.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm));
            this.dtArtikel.Name = "dtArtikel";

            tableGroup17.Name = "DetailGroup0";
            tableGroup18.Name = "Group1";
            tableGroup19.Name = "Group5";
            this.dtArtikel.RowGroups.Add(tableGroup17);
            this.dtArtikel.RowGroups.Add(tableGroup18);
            this.dtArtikel.RowGroups.Add(tableGroup19);
            this.dtArtikel.RowHeadersPrintOnEveryPage = true;
            this.dtArtikel.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(17.392187118530273, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(1.9793746471405029, Telerik.Reporting.Drawing.UnitType.Cm));
            this.dtArtikel.NeedDataSource += new System.EventHandler(this.docLieferschein_NeedDataSource);
            // 
            // tbME
            // 
            this.tbME.Format = "{0:#.}";
            this.tbME.Name = "tbME";
            this.tbME.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.059617280960083, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.47937497496604919, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbME.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbME.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.tbME.Value = "=Fields.ME";
            // 
            // tbGut
            // 
            this.tbGut.Name = "tbGut";
            this.tbGut.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.7352721691131592, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.47937497496604919, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbGut.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbGut.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.tbGut.Value = "=Fields.Gut";
            // 
            // tbAbmessung
            // 
            this.tbAbmessung.Format = "{0:N2}";
            this.tbAbmessung.Name = "tbAbmessung";
            this.tbAbmessung.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.5883514881134033, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.47937497496604919, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbAbmessung.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbAbmessung.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.tbAbmessung.Value = "=Fields.Abmessungen";
            // 
            // tbGewicht
            // 
            this.tbGewicht.Format = "{0:N2}";
            this.tbGewicht.Name = "tbGewicht";
            this.tbGewicht.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.1858620643615723, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.47937497496604919, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbGewicht.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbGewicht.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.tbGewicht.StyleName = "";
            this.tbGewicht.Value = "=Fields.Gewicht";
            // 
            // textBox19
            // 
            this.textBox19.Format = "{0:#.}";
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.059617280960083, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999991059303284, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox19.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.StyleName = "";
            // 
            // textBox25
            // 
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.7352721691131592, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999991059303284, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox25.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox25.StyleName = "";
            // 
            // textBox36
            // 
            this.textBox36.Format = "{0:N2}";
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.1858620643615723, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999991059303284, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox36.Style.Font.Bold = true;
            this.textBox36.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox36.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox36.StyleName = "";
            // 
            // textBox14
            // 
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.2118852138519287, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.47937497496604919, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox14.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.StyleName = "";
            this.textBox14.Value = "=Fields.Werksnummer";
            // 
            // textBox15
            // 
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.2118852138519287, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999991059303284, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox15.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox15.StyleName = "";
            // 
            // textBox4
            // 
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.2118852138519287, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999991059303284, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox4.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.StyleName = "";
            // 
            // textBox5
            // 
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(1.059617280960083, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999991059303284, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox5.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.StyleName = "";
            // 
            // textBox17
            // 
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(2.7352721691131592, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999991059303284, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox17.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox17.StyleName = "";
            // 
            // textBox18
            // 
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.5883514881134033, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999991059303284, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox18.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox18.StyleName = "";
            // 
            // textBox23
            // 
            this.textBox23.Format = "{0:N2}";
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.1858620643615723, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999991059303284, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox23.Style.Font.Bold = true;
            this.textBox23.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox23.StyleName = "";
            this.textBox23.Value = "=SUM(Fields.Gewicht)";
            // 
            // textBox30
            // 
            this.textBox30.Format = "{0:N2}";
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(3.5883514881134033, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999991059303284, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox30.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox30.StyleName = "";
            // 
            // textBox20
            // 
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.6111979484558105, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.47937497496604919, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox20.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.StyleName = "";
            this.textBox20.Value = "=Fields.Zusatz";
            // 
            // textBox21
            // 
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.6111979484558105, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999991059303284, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox21.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox21.StyleName = "";
            // 
            // textBox22
            // 
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(4.6111979484558105, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.49999991059303284, Telerik.Reporting.Drawing.UnitType.Cm));
            this.textBox22.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(9, Telerik.Reporting.Drawing.UnitType.Point);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox22.StyleName = "";
            // 
            // tbNotiz
            // 
            this.tbNotiz.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(14.800000190734863, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbNotiz.Name = "tbNotiz";
            this.tbNotiz.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(17.392288208007812, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39959931373596191, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbNotiz.Value = "";
            // 
            // tbLadenummer
            // 
            this.tbLadenummer.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(13.699999809265137, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbLadenummer.Name = "tbLadenummer";
            this.tbLadenummer.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(17.392288208007812, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.50000017881393433, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbLadenummer.Value = "";
            // 
            // tbZF
            // 
            this.tbZF.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(14.300000190734863, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbZF.Name = "tbZF";
            this.tbZF.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(17.392288208007812, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.50000017881393433, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbZF.Value = "";
            // 
            // lfs_pageFooter
            // 
            this.lfs_pageFooter.Height = new Telerik.Reporting.Drawing.Unit(3.2999997138977051, Telerik.Reporting.Drawing.UnitType.Cm);
            this.lfs_pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.tbText,
            this.tbHR});
            this.lfs_pageFooter.Name = "lfs_pageFooter";
            // 
            // tbText
            // 
            this.tbText.Format = "{0}";
            this.tbText.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.00010002215276472271, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.90049892663955688, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbText.Name = "tbText";
            this.tbText.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(17.461250305175781, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(2.3993997573852539, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbText.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(7, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbText.Value = "Text";
            // 
            // tbHR
            // 
            this.tbHR.Location = new Telerik.Reporting.Drawing.PointU(new Telerik.Reporting.Drawing.Unit(0.00010002215276472271, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.50039905309677124, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbHR.Name = "tbHR";
            this.tbHR.Size = new Telerik.Reporting.Drawing.SizeU(new Telerik.Reporting.Drawing.Unit(17.392187118530273, Telerik.Reporting.Drawing.UnitType.Cm), new Telerik.Reporting.Drawing.Unit(0.39989966154098511, Telerik.Reporting.Drawing.UnitType.Cm));
            this.tbHR.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(7, Telerik.Reporting.Drawing.UnitType.Point);
            this.tbHR.Value = "HR";
            // 
            // docLieferschein
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.lfs_pageHeader,
            this.lfs_detail,
            this.lfs_pageFooter});
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins.Bottom = new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Left = new Telerik.Reporting.Drawing.Unit(2, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Right = new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.Margins.Top = new Telerik.Reporting.Drawing.Unit(0, Telerik.Reporting.Drawing.UnitType.Cm);
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.Table), "Normal.TableNormal")});
            styleRule1.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule1.Style.BorderWidth.Default = new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Pixel);
            styleRule1.Style.Color = System.Drawing.Color.Black;
            styleRule1.Style.Font.Name = "Tahoma";
            styleRule1.Style.Font.Size = new Telerik.Reporting.Drawing.Unit(8, Telerik.Reporting.Drawing.UnitType.Point);
            descendantSelector1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableHeader")});
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector1});
            styleRule2.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule2.Style.BorderWidth.Default = new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Pixel);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            descendantSelector2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Normal.TableBody")});
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector2});
            styleRule3.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule3.Style.BorderWidth.Default = new Telerik.Reporting.Drawing.Unit(1, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3});
            this.Width = new Telerik.Reporting.Drawing.Unit(17.461450576782227, Telerik.Reporting.Drawing.UnitType.Cm);
            this.NeedDataSource += new System.EventHandler(this.docLieferschein_NeedDataSource);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.TextBox textBox1;
        protected Telerik.Reporting.TextBox tbBKSteuer;
        protected Telerik.Reporting.TextBox tbBKUST;
        protected Telerik.Reporting.TextBox tbBKPLZOrt;
        protected Telerik.Reporting.TextBox tbBKStr;
        protected Telerik.Reporting.TextBox tbBKFirma2;
        protected Telerik.Reporting.TextBox tbBKFirma1;
        protected Telerik.Reporting.PictureBox pbZert;
        protected Telerik.Reporting.TextBox tbOrtDatum;
        protected Telerik.Reporting.PictureBox pbLogo;
        private Telerik.Reporting.Panel panelVersender;
        private Telerik.Reporting.Panel panel1;
        private Table table1;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox tbFahrer;
        private Table dtUnterschrift;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox9;
        protected Telerik.Reporting.TextBox tbText;
        protected Telerik.Reporting.TextBox tbHR;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox2;
        public PageHeaderSection lfs_pageHeader;
        public DetailSection lfs_detail;
        public PageFooterSection lfs_pageFooter;
        public Telerik.Reporting.TextBox tbDocName;
        public Telerik.Reporting.TextBox tbEHeadline;
        public Telerik.Reporting.TextBox tbEName1;
        public Telerik.Reporting.TextBox tbEName2;
        public Telerik.Reporting.TextBox tbEName3;
        public Telerik.Reporting.TextBox tbEStr;
        public Telerik.Reporting.TextBox tbEPLZ;
        public Telerik.Reporting.TextBox tbEOrt;
        public Telerik.Reporting.TextBox tbVName1;
        public Telerik.Reporting.TextBox tbVName2;
        public Telerik.Reporting.TextBox tbVName3;
        public Telerik.Reporting.TextBox tbVStr;
        public Telerik.Reporting.TextBox tbVPLZ;
        public Telerik.Reporting.TextBox tbVOrt;
        public Telerik.Reporting.TextBox tbLieferscheinNr;
        public Telerik.Reporting.TextBox tbAuftrag;
        public Telerik.Reporting.Panel panelUnterschrift;
        protected Telerik.Reporting.TextBox tbNotiz;
        public Telerik.Reporting.TextBox tbZM;
        public Telerik.Reporting.TextBox tbAuflieger;
        private Telerik.Reporting.Panel panelArtikel;
        public Table dtArtikel;
        private Telerik.Reporting.TextBox tbME;
        private Telerik.Reporting.TextBox tbGut;
        private Telerik.Reporting.TextBox tbAbmessung;
        private Telerik.Reporting.TextBox tbGewicht;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox36;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox tbColWerksnummer;
        private Telerik.Reporting.TextBox tbColAnzahl;
        private Telerik.Reporting.TextBox tbColGut;
        private Telerik.Reporting.TextBox tbColAbmessungen;
        private Telerik.Reporting.TextBox tbColgemGewicht;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox16;
        protected Telerik.Reporting.TextBox tbLadenummer;
        protected Telerik.Reporting.TextBox tbZF;
        public Telerik.Reporting.TextBox tbTelefon;
        public Telerik.Reporting.TextBox tbFax;
        public Telerik.Reporting.TextBox tbVHeadline;
    }
}