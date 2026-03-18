using LVS;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Sped4.Controls.Edifact
{
    public partial class ctrAsnArt : UserControl
    {
        internal AsnArtViewData asnArtVD = new AsnArtViewData();
        public ctrAsnArt()
        {
            InitializeComponent();
        }

        public void InitCtr()
        {
            asnArtVD = new AsnArtViewData();
            ClearInputField();
            InitDgv();
        }

        private void ClearInputField()
        {
            tbId.Text = "0";
            tbTyp.Text = string.Empty;
            tbBezeichnung.Text = string.Empty;
            tbBeschreibung.Text = string.Empty;
        }

        private void SetValueToField()
        {
            tbId.Text = asnArtVD.AsnArt.Id.ToString();
            tbTyp.Text = asnArtVD.AsnArt.Typ;
            tbBezeichnung.Text = asnArtVD.AsnArt.Bezeichnung;
            tbBeschreibung.Text = asnArtVD.AsnArt.Beschreibung;
        }
        private void SetFieldToClsValue()
        {
            int iTmp = 0;
            int.TryParse(tbId.Text, out iTmp);
            asnArtVD.AsnArt.Id = iTmp;
            asnArtVD.AsnArt.Typ = tbTyp.Text;
            asnArtVD.AsnArt.Bezeichnung = tbBezeichnung.Text;
            asnArtVD.AsnArt.Beschreibung = tbBeschreibung.Text;
        }
        private void tsbtnNewAsnArt_Click(object sender, EventArgs e)
        {
            asnArtVD = new AsnArtViewData();
            asnArtVD.AsnArt = new AsnArt();
            ClearInputField();
            SetValueToField();
        }

        private void InitDgv()
        {
            asnArtVD.GetAsnArtList(false);
            dgv.DataSource = asnArtVD.ListAsnArt;
            dgv.BestFitColumns();
        }

        private void tsbtnEdifactSave_Click(object sender, EventArgs e)
        {
            AsnArt tmpAsnArt = new AsnArt();
            tmpAsnArt = asnArtVD.ListAsnArt.FirstOrDefault(x => x.Typ.Equals(tbTyp.Text));
            if (tmpAsnArt is null)
            {
                SetFieldToClsValue();
                if (asnArtVD.AsnArt.Id > 0)
                {
                    asnArtVD.Update();
                }
                else
                {
                    asnArtVD.Add();
                }
                InitDgv();
            }
            else
            {
                string mes = string.Empty;
                mes = "Es existiert bereits ein Objekt mit dem selben Namen!";
                clsMessages.Allgemein_ERRORTextShow(mes);
            }
        }

        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                this.dgv.CurrentRow.IsSelected = true;
            }
        }

        private void dgv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dgv.RowCount > 0)
            {
                if (this.dgv.SelectedRows.Count > 0)
                {
                    int iTmp = 0;
                    int.TryParse(dgv.SelectedRows[0].Cells["ID"].Value.ToString(), out iTmp);
                    if (iTmp > 0)
                    {
                        this.asnArtVD.AsnArt = this.asnArtVD.ListAsnArt.FirstOrDefault(x => x.Id == iTmp);
                        ClearInputField();
                        SetValueToField();
                    }
                }
            }
        }

        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitDgv();
        }

        private void splitPanel1_Click(object sender, EventArgs e)
        {

        }

        private void btnAsnArtUpdate_Click(object sender, EventArgs e)
        {
            var arts = AsnArtViewData.CheckTableAsnArtForUpdate();
            if (arts.Count > 0)
            {
                foreach (AsnArt item in arts)
                {
                    AsnArtViewData artVD = new AsnArtViewData(item);
                    artVD.Add();
                }
            }
            InitDgv();
        }
    }
}
