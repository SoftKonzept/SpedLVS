using LVS;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace Sped4
{
    public partial class ctrFaktArtikelList : UserControl
    {
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;
        public ctrFaktLager _ctrFaktLager;
        public frmTmp _frmTmp;


        public ctrFaktArtikelList()
        {
            InitializeComponent();
        }
        ///<summary>ctrFaktLager / InitCtr</summary>
        ///<remarks></remarks>
        public void InitCtr()
        {
            if (this._ctrFaktLager != null)
            {
                string strFrmTxt = string.Empty;
                //Unterscheidung RG und Vorschau
                if (this._ctrFaktLager.FaktLager.Rechnung.ID > 0)
                {
                    strFrmTxt = this._frmTmp.Text + " Rechnung / Gutschrift: [ "
                                                           + this._ctrFaktLager.FaktLager.Rechnung.RGNr.ToString()
                                                           + " / "
                                                           + this._ctrFaktLager.FaktLager.Rechnung.Datum.ToString("dd.MM.yyyy")
                                                           + " ]"
                                                           + " Zeitraum: [ "
                                                           + this._ctrFaktLager.FaktLager.Rechnung.AbrZeitraumVon.ToString("dd.MM.yyyy")
                                                           + " bis "
                                                           + this._ctrFaktLager.FaktLager.Rechnung.AbrZeitraumBis.ToString("dd.MM.yyyy")
                                                           + " ]";

                }
                else
                {
                    strFrmTxt = this._frmTmp.Text + " Vorschau - Zeitraum: [ "
                                       + this._ctrFaktLager.dtpAbrVon.Value.ToString("dd.MM.yyyy")
                                       + " bis "
                                       + this._ctrFaktLager.dtpAbrBis.Value.ToString("dd.MM.yyyy")
                                       + " ]";
                }
                this._frmTmp.Text = strFrmTxt;
                InitDGV();
            }
            else
            {
                this._frmTmp.CloseFrmTmp();
            }
        }
        ///<summary>ctrFaktLager / tsbtnCloseFakt_Click</summary>
        ///<remarks>Schliesst das CTR</remarks>
        private void tsbtnCloseFakt_Click(object sender, EventArgs e)
        {
            this._frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrFaktLager / InitDGV</summary>
        ///<remarks>Erstellt das Datagrid</remarks>
        private void InitDGV()
        {
            //Unterscheidung RG und Vorschau
            if (this._ctrFaktLager.FaktLager.Rechnung.ID > 0)
            {
                //REchnung
                //decimal decRGTmp = this._ctrFaktLager.FaktLager.Rechnung.RGNr;
                //decimal decMandant = this._ctrFaktLager.FaktLager.MandantenID;
                //this.dgv.DataSource = this._ctrFaktLager.FaktLager.Rechnung.RGPosArtikel.GetArtikelListByRGID(decRGTmp, decMandant);
                this.dgv.DataSource = this._ctrFaktLager.FaktLager.Rechnung.RGPosArtikel.GetArtikelListByRGID((int)this._ctrFaktLager.FaktLager.Rechnung.ID, (int)this._ctrFaktLager.FaktLager.MandantenID);
            }
            else
            {
                DataTable dtSammel = new DataTable();
                DataTable dtTmp = new DataTable();
                //Vorschau
                //Hier müssen nun alle Bestände einzelen Ermittel werden und dann zusammengefügt
                //Einlagerung
                dtTmp = new DataTable();
                if (this._ctrFaktLager.FaktLager.dtArtikelEinlagerung.Rows.Count > 0)
                {
                    dtTmp = this._ctrFaktLager.FaktLager.dtArtikelEinlagerung.DefaultView.ToTable(true, "ID", "Abrechnungsart");
                }
                dtSammel = this._ctrFaktLager.FaktLager.Rechnung.RGPosArtikel.GetArtikelListVorschau(dtTmp);
                //Auslagerung
                dtTmp = new DataTable();
                if (this._ctrFaktLager.FaktLager.dtArtikelAuslagerung.Rows.Count > 0)
                {
                    dtTmp = this._ctrFaktLager.FaktLager.dtArtikelAuslagerung.DefaultView.ToTable(true, "ID", "Abrechnungsart");
                }
                dtSammel.Merge(this._ctrFaktLager.FaktLager.Rechnung.RGPosArtikel.GetArtikelListVorschau(dtTmp));
                //Lagerkosten
                dtTmp = new DataTable();
                if (this._ctrFaktLager.FaktLager.dtArtikelLagerbestand.Rows.Count > 0)
                {
                    dtTmp = this._ctrFaktLager.FaktLager.dtArtikelLagerbestand.DefaultView.ToTable(true, "ID", "Abrechnungsart");
                }
                dtSammel.Merge(this._ctrFaktLager.FaktLager.Rechnung.RGPosArtikel.GetArtikelListVorschau(dtTmp));
                //LagerTransportkosten
                dtTmp = new DataTable();
                if (this._ctrFaktLager.FaktLager.dtArtikelLagerTransporte.Rows.Count > 0)
                {
                    dtTmp = this._ctrFaktLager.FaktLager.dtArtikelLagerTransporte.DefaultView.ToTable(true, "ID", "Abrechnungsart");
                }
                dtSammel.Merge(this._ctrFaktLager.FaktLager.Rechnung.RGPosArtikel.GetArtikelListVorschau(dtTmp));
                //Sperrlagerkosten
                dtTmp = new DataTable();
                if (this._ctrFaktLager.FaktLager.dtArtikelSperrlager.Rows.Count > 0)
                {
                    dtTmp = this._ctrFaktLager.FaktLager.dtArtikelSperrlager.DefaultView.ToTable(true, "ID", "Abrechnungsart");
                }
                dtSammel.Merge(this._ctrFaktLager.FaktLager.Rechnung.RGPosArtikel.GetArtikelListVorschau(dtTmp));
                //Direktanlieferung
                dtTmp = new DataTable();
                if (this._ctrFaktLager.FaktLager.dtArtikelDirektanlieferung.Rows.Count > 0)
                {
                    dtTmp = this._ctrFaktLager.FaktLager.dtArtikelDirektanlieferung.DefaultView.ToTable(true, "ID", "Abrechnungsart");
                }
                dtSammel.Merge(this._ctrFaktLager.FaktLager.Rechnung.RGPosArtikel.GetArtikelListVorschau(dtTmp));
                //Rücklieferung
                dtTmp = new DataTable();
                if (this._ctrFaktLager.FaktLager.dtArtikelRuecklieferung.Rows.Count > 0)
                {
                    dtTmp = this._ctrFaktLager.FaktLager.dtArtikelRuecklieferung.DefaultView.ToTable(true, "ID", "Abrechnungsart");
                }
                dtSammel.Merge(this._ctrFaktLager.FaktLager.Rechnung.RGPosArtikel.GetArtikelListVorschau(dtTmp));
                //Vorfracht
                dtTmp = new DataTable();
                if (this._ctrFaktLager.FaktLager.dtArtikelVorfracht.Rows.Count > 0)
                {
                    dtTmp = this._ctrFaktLager.FaktLager.dtArtikelVorfracht.DefaultView.ToTable(true, "ID", "Abrechnungsart");
                }

                dtSammel.Merge(this._ctrFaktLager.FaktLager.Rechnung.RGPosArtikel.GetArtikelListVorschau(dtTmp));
                this.dgv.DataSource = dtSammel;
            }
            GridViewSummaryItem sumDaten = new GridViewSummaryItem("ArtikelID", "Datensätze: {0}", GridAggregateFunction.Count);
            GridViewSummaryItem sumAnzahl = new GridViewSummaryItem("Anzahl", "Gesamt: {0}", GridAggregateFunction.Sum);
            GridViewSummaryItem sumNetto = new GridViewSummaryItem("Netto", "Gesamt: {0}", GridAggregateFunction.Sum);
            GridViewSummaryItem sumBrutto = new GridViewSummaryItem("Brutto", "Gesamt: {0}", GridAggregateFunction.Sum);
            GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem(
                new GridViewSummaryItem[] { sumDaten, sumAnzahl, sumNetto, sumBrutto });
            this.dgv.SummaryRowsTop.Add(summaryRowItem);

            GroupDescriptor group = new GroupDescriptor();
            group.GroupNames.Add("Abrechnungsart", ListSortDirection.Ascending);
            this.dgv.GroupDescriptors.Add(group);

            this.dgv.BestFitColumns();
        }
        ///<summary>ctrFaktLager / InitDGV</summary>
        ///<remarks>Erstellt das Datagrid</remarks>
        private void tsbtnFreigabeFrm_Click(object sender, EventArgs e)
        {

        }
        ///<summary>ctrFaktLager / saveFileDialog_FileOk</summary>
        ///<remarks></remarks>
        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            ExportDGVToExcel();
        }
        ///<summary>ctrFaktLager / ExportDGVToExcel</summary>
        ///<remarks>Export zu Excel</remarks>
        private void ExportDGVToExcel()
        {
            this.dgv.LoadElementTree();
            var execlML = new ExportToExcelML(this.dgv);
            execlML.HiddenColumnOption = HiddenOption.DoNotExport;
            execlML.ExportVisualSettings = false;
            execlML.SheetMaxRows = ExcelMaxRows._1048576;
            execlML.SheetName = "Journal";
            execlML.RunExport(saveFileDialog1.FileName);
            System.Diagnostics.Process.Start(saveFileDialog1.FileName);
        }
        ///<summary>ctrFaktLager / tsbtnExcel_Click</summary>
        ///<remarks>Export zu Excel über SaveFileDialog</remarks>
        private void tsbtnExcel_Click(object sender, EventArgs e)
        {
            string strFileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss")
                          + "_Fakt_ArtikelListe.xls";
            saveFileDialog1.FileName = strFileName;
            saveFileDialog1.ShowDialog();
        }
    }
}
