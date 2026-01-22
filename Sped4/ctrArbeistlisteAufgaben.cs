using LVS;
using Sped4.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrArbeistlisteAufgaben : UserControl
    {
        private frmDialog frmDialog;
        internal DataTable data;
        internal clsExtraCharge ExtraCharge;
        internal string AdrID;

        public ctrArbeistlisteAufgaben(DataTable dt, string _AdrID)
        {
            InitializeComponent();

            data = dt;

            AdrID = _AdrID;




        }

        public void add(frmDialog fd)
        {
            this.frmDialog = fd;
            InitDGV();
            InitExtraCharge();
            cbEinheit.DataSource = clsEinheiten.GetEinheiten(this.frmDialog.ctrMenu._frmMain.GL_User);
            cbEinheit.DisplayMember = "Bezeichnung";
            cbEinheit.ValueMember = "Bezeichnung";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (cbExtraCharge.SelectedIndex > -1)
            {

                decimal decPreis = 0;
                decimal.TryParse(tbPreis.Text, out decPreis);
                if (decPreis > 0)
                {
                    DataTable tmp = data.Copy();
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        decimal decTmp = 0;
                        Decimal.TryParse(data.Rows[i]["ArtikelID"].ToString(), out decTmp);
                        if (!frmDialog.ctrArbeitsliste.aufgaben.ContainsKey(decTmp))
                        {
                            clsAufgabe aufg = new clsAufgabe();
                            aufg.ArtikelID = decTmp;

                            aufg.addAufgabe(ExtraCharge);
                            frmDialog.ctrArbeitsliste.aufgaben.Add(decTmp, aufg);
                            data.Rows[i]["Aufgaben"] = ExtraCharge;
                            data.Rows[i]["Aufgaben"] = ExtraCharge;
                        }
                        else
                        {
                            if (!frmDialog.ctrArbeitsliste.aufgaben[decTmp].contains(ExtraCharge))
                            {
                                frmDialog.ctrArbeitsliste.aufgaben[decTmp].addAufgabe(ExtraCharge);
                                data.Rows[i]["Aufgaben"] = ExtraCharge;
                            }
                            else
                            {
                                data.Rows.Remove(data.Rows[i--]);
                            }
                        }


                    }
                    this.frmDialog.DialogResult = DialogResult.OK;
                }
                else
                {
                    clsMessages.Allgemein_EingabeIstKeineDecimalzahl();
                }
            }

        }


        private void InitDGV()
        {
            if (data.Columns["Aufgaben"] == null)
                data.Columns.Add("Aufgaben", typeof(clsExtraCharge));
            dgvArtikel.DataSource = data;
            dgvArtikel.Columns["Selected"].IsVisible = false;
        }

        private void InitExtraCharge()
        {
            cbExtraCharge.Items.Clear();
            ExtraCharge = new clsExtraCharge();
            ExtraCharge.InitClass(this.frmDialog.ctrMenu._frmMain.GL_User);
            ExtraCharge.ArbeitsbereichID = this.frmDialog.ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID;
            ExtraCharge.AdrID = clsADR.GetIDByMatchcode(AdrID);
            //ExtraCharge.Fill();
            cbExtraCharge.DataSource = ExtraCharge.GetExtraChargeList().DefaultView;
            cbExtraCharge.DisplayMember = "Bezeichnung";
        }

        private void cbExtraCharge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbExtraCharge.SelectedIndex > -1)
            {
                decimal decTmp = 0;
                string strTmp = ((DataRowView)cbExtraCharge.SelectedItem)["ID"].ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > -1)
                {



                    ExtraCharge.ID = decTmp;
                    ExtraCharge.Fill();
                    tbPreis.Text = Functions.FormatDecimalMoney(ExtraCharge.Preis);
                    tbECAssRGText.Text = ExtraCharge.RGText;
                    Functions.SetComboToSelecetedValue(ref cbEinheit, ExtraCharge.Einheit);
                }

            }
            else
            {
                // init Fields
            }
        }

        private void tsBtnCancel_Click(object sender, EventArgs e)
        {
            this.frmDialog.DialogResult = DialogResult.Cancel;
        }
    }
}




//Decimal decTmpECAssID = ExtraChargeAssignment.ID;
//            ExtraChargeAssignment = new clsExtraChargeAssignment();
//            ExtraChargeAssignment.InitClass(this.GLUser);

//            Int32 iTmp = 1;
//            Int32.TryParse(nudECAssMenge.Value.ToString(), out iTmp);
//            if (bUpdate)
//            {
//                ExtraChargeAssignment.ID = decTmpECAssID;
//                ExtraChargeAssignment.Einheit = cbEinheit.SelectedValue.ToString();
//                decimal decTmp = 0;
//                decimal.TryParse(tbECAssPreis.Text, out decTmp);
//                ExtraChargeAssignment.Preis = decTmp;
//                ExtraChargeAssignment.Menge = iTmp;
//                ExtraChargeAssignment.RGText = tbECAssRGText.Text.ToString().Trim();

//                ExtraChargeAssignment.Update();
//            }
//            else
//            {
//                ExtraChargeAssignment.Add(ExtraCharge, this.bExtraChargeAssignmentForArt, this.ArtikelTableID, this.LEingangTableID, iTmp);
//            }
//            if (ctrArbeitsliste != null)
//            {

//                if (!ctrArbeitsliste.aufgaben.ContainsKey(this.ArtikelTableID))
//                {
//                    clsAufgabe aufg = new clsAufgabe();
//                    aufg.ArtikelID = this.ArtikelTableID;
//                    aufg.addAufgabe(ExtraCharge.Bezeichnung);
//                    ctrArbeitsliste.aufgaben.Add(this.ArtikelTableID, aufg);
//                }
//                else
//                {
//                    ctrArbeitsliste.aufgaben[ArtikelTableID].addAufgabe(ExtraCharge.Bezeichnung);
//               }
//                ctrArbeitsliste.test();
//            }
//            ClearExtraChargeAssignmentInputFields();
//            SetExtraChargeAssignmentInputFieldsEnabled(false);
//            bUpdate = false;
//            InitDGVExtraChargeAssignment();
//        }