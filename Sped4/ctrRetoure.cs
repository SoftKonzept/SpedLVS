using LVS;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrRetoure : UserControl
    {
        public clsLager Lager;
        public ctrMenu ctrMenu;
        public ctrEinlagerung ctrEinlagerung;
        public Globals._GL_USER GLUser;
        public frmTmp _frmTmp;
        internal DataTable dtSource;


        ///<summary>ctrRetoure / ctrRetoure</summary>
        ///<remarks></remarks>
        public ctrRetoure()
        {
            InitializeComponent();
        }
        ///<summary>ctrRetoure / ctrRetoure_Load</summary>
        ///<remarks></remarks>
        private void ctrRetoure_Load(object sender, EventArgs e)
        {
            if (ctrEinlagerung is ctrEinlagerung)
            {
                Lager = this.ctrEinlagerung.Lager;
                ClearCtr();
            }
        }
        ///<summary>ctrRetoure / ClearCtr</summary>
        ///<remarks></remarks>
        private void ClearCtr()
        {
            this.dtSource = new DataTable();
            this.tstbLVSSearch.Text = string.Empty;
            this.tbAEmpf.Text = string.Empty;
            this.cbRetourAnzahl.Checked = false;
        }
        ///<summary>ctrRetoure / InitDGV</summary>
        ///<remarks></remarks>
        private void InitDGV()
        {
            dtSource = clsArtikel.GetArtikelStoredOutByAuftraggeber(this.GLUser, this.Lager.Ausgang.Auftraggeber, this.ctrMenu._frmMain.system.AbBereich.ID);
            this.dgv.DataSource = dtSource;
            for (Int32 i = 0; i <= this.dgv.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                string strTmp = string.Empty;
                strTmp = this.dgv.Rows[i].Cells["LVSNr"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp == this.Lager.Artikel.LVS_ID)
                {
                    this.dgv.Rows[i].IsSelected = true;
                    this.dgv.Rows[i].IsCurrent = true;
                    this.dgv.Rows[i].Cells["Selected"].Value = true;
                    i = this.dgv.Rows.Count;
                }
            }
            this.dgv.BestFitColumns();
        }
        ///<summary>ctrRetoure / tsbtnCloseCtr_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCloseCtr_Click(object sender, EventArgs e)
        {
            this._frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrRetoure / tsbtnLVSNrSearch_Click</summary>
        ///<remarks></remarks>
        private void tsbtnLVSNrSearch_Click(object sender, EventArgs e)
        {
            decimal decLvsNr = 0;
            if (Decimal.TryParse(tstbLVSSearch.Text, out decLvsNr))
            {
                decimal decArtID = clsArtikel.GetArtikelIDByLVSNr(this.GLUser, this.ctrMenu._frmMain.system, decLvsNr);
                if (decArtID > 0)
                {
                    Lager.Artikel.ID = decArtID;
                    Lager.Artikel.GetArtikeldatenByTableID();
                    Lager.Eingang.LEingangTableID = Lager.Artikel.LEingangTableID;
                    Lager.Eingang.FillEingang();
                    Lager.Ausgang.LAusgangTableID = Lager.Artikel.LAusgangTableID;
                    Lager.Ausgang.FillAusgang();

                    Lager.ADR.ID = Lager.Ausgang.Empfaenger;
                    Lager.ADR.FillClassOnly();
                    tbAEmpf.Text = Lager.ADR.ADRString;
                    InitDGV();
                }
                else
                {
                    clsMessages.Lager_Artikel_LVSNRNotExist(string.Empty);
                }
            }
        }
        ///<summary>ctrRetoure / dgv_CellClick</summary>
        ///<remarks></remarks>
        private void dgv_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //if (e.Column.UniqueName.Equals("Selected"))
            if (e.Column.Name.Equals("Selected"))
            {
                decimal decLVSNrSelected = 0;
                Decimal.TryParse(e.Row.Cells["LVSNr"].Value.ToString(), out decLVSNrSelected);
                foreach (DataRow row in dtSource.Rows)
                {
                    decimal decTmpLVSNr = 0;
                    if (decimal.TryParse(row["LVSNr"].ToString(), out decTmpLVSNr))
                    {
                        if (cbRetourAnzahl.Checked)
                        {
                            if (decLVSNrSelected.Equals(decTmpLVSNr))
                            {
                                if ((bool)row["Selected"])
                                {
                                    row["Selected"] = false;
                                }
                                else
                                {
                                    row["Selected"] = true;
                                }
                            }
                        }
                        else
                        {
                            //Normaler Vorgang - nur ein Artikel kann ausgewählt werden
                            if (decLVSNrSelected.Equals(decTmpLVSNr))
                            {
                                row["Selected"] = true;
                            }
                            else
                            {
                                row["Selected"] = false;
                            }
                        }
                    }
                }
            }
        }
        ///<summary>ctrRetoure / tsbtnCreateRetoure_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCreateRetoure_Click(object sender, EventArgs e)
        {
            //Check 
            //Es muss mindenst ein Artikel markiert sein
            if (dtSource.Rows.Count > 0)
            {
                dtSource.DefaultView.RowFilter = "Selected=true";
                DataTable dtSelected = dtSource.DefaultView.ToTable();
                if (dtSelected.Rows.Count > 0)
                {
                    decimal decTmpArtID = 0;
                    Decimal.TryParse(dtSelected.Rows[0]["ArtikelID"].ToString(), out decTmpArtID);
                    if (decTmpArtID > 0)
                    {
                        this.Lager.Artikel.ID = decTmpArtID;
                        this.Lager.Artikel.GetArtikeldatenByTableID();
                        this.Lager.Eingang.CreateEingangByRetoure(ref this.Lager, dtSelected);
                        //this.ctrEinlagerung.JumpToLEingang(this.Lager.Eingang.LEingangID);
                        this.ctrEinlagerung.EingangBrowse(this.Lager.Eingang.LEingangTableID, 0, enumBrowseAcivity.Item);
                    }
                }
                else
                {
                    clsMessages.Retoure_NoArtikelSelected();
                }
                dtSource.DefaultView.RowFilter = string.Empty;

                this._frmTmp.CloseFrmTmp();
            }
        }
    }
}
