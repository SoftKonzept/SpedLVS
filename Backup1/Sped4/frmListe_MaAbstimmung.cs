using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LVS;
using Sped4;
using System.Collections;

namespace Sped4
{
    public partial class frmListe_MaAbstimmung : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        public DataTable dt1 = new DataTable();
        public DataTable dt2 = new DataTable();

        internal decimal iADR_ID1 = 0;
        internal decimal iADR_ID2 = 0;


        /********************
         * iTypen
         * 0: 1. Suchkriterium ist Auftraggeber / Versender
         * 1: 1. Suchkriterium ist Versender / Auftraggeber
         * *****************/
        Int32 iType = 0;



        public frmListe_MaAbstimmung()
        {
            InitializeComponent();
        }
        //
        //
        //
        private void frmListe_MaAbstimmung_Load(object sender, EventArgs e)
        {
            InitSearchType();
        }
        //
        //
        //
        private void InitSearchType()
        {
            dgv1.DataSource = null;
            dgv2.DataSource = null;
            dt1.Clear();
            dt2.Clear();
            
            switch (iType.ToString())
            {
                case "0":
                    btnSort.Text = "Sortierung Auftraggeber / Versender";
                    dt1 = clsAuftragPos.GetAuftraggeberByVSB();
                    break;

                case "1":
                    btnSort.Text = "Sortierung Versender / Auftraggeber";
                    dt1 = clsAuftragPos.GetVersenderByVSB();
                    break;
            }

            if (dt1.Rows.Count > 0)
            {
                dgv1.DataSource = dt1;
                if (dgv1.Columns["KD_ID"] != null)
                {
                    dgv1.Columns["KD_ID"].Visible = false;
                }
                if (dgv1.Columns["B_ID"] != null)
                {
                    dgv1.Columns["B_ID"].Visible = false;
                }
                Functions.dgv_ColAutoResize_Fill(ref dgv1);
                this.dgv1.Refresh();
            }
        }
        //
        //
        //
        private void dgv1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgv1.Rows.Count > 0)
            {
                switch (iType.ToString())
                {
                    //Auftraggeber/Versender
                    case "0":
                        if (dgv1.Columns["KD_ID"] != null)
                        {
                            iADR_ID1 = (decimal)this.dgv1.Rows[dgv1.CurrentRow.Index].Cells["KD_ID"].Value;
                        }
                        break;
                    //Versender / Auftraggeber
                    case "1":
                        if (dgv1.Columns["B_ID"] != null)
                        {
                            iADR_ID1 = (decimal)this.dgv1.Rows[dgv1.CurrentRow.Index].Cells["B_ID"].Value;
                        }
                        break;
                }
                SelectItemFromDGV1();
            }
        }
        //
        //
        //
        private void SelectItemFromDGV1()
        {
            switch (iType.ToString())
            {
                //Auftraggeber/Versender
                case "0":
                    dt2 = clsAuftragPos.GetVersenderByKD_ID(iADR_ID1);
                    break;
                //Versender / Auftraggeber
                case "1":
                    dt2 = clsAuftragPos.GetAuftraggeberByB_ID(iADR_ID1);
                    break;
            }
            if (dt2.Rows.Count > 0)
            {
                dgv2.DataSource = dt2;
                if (dgv2.Columns["KD_ID"] != null)
                {
                    dgv2.Columns["KD_ID"].Visible = false;
                }
                if (dgv2.Columns["B_ID"] != null)
                {
                    dgv2.Columns["B_ID"].Visible = false;
                }
                Functions.dgv_ColAutoResize_Fill(ref dgv2);
                this.dgv2.Refresh();
            }

        }
        //
        //
        private void dgv2_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv2.Rows.Count > 0)
            {
                switch (iType.ToString())
                {
                    //Auftraggeber/Versender
                    case "0":
                        if (dgv2.Columns["B_ID"] != null)
                        {
                            iADR_ID2 = (decimal)this.dgv2.Rows[dgv2.CurrentRow.Index].Cells["B_ID"].Value;
                        }
                        break;
                    //Versender / Auftraggeber
                    case "1":
                        if (dgv2.Columns["KD_ID"] != null)
                        {
                            iADR_ID2 = (decimal)this.dgv2.Rows[dgv2.CurrentRow.Index].Cells["KD_ID"].Value;
                        }
                        break;
                }
            }
        }

        private void dgv2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (iADR_ID2 > 0)
            {
                decimal iTakeOver = 0;
                bool Kunde = true;
                //Übergabe der richtigen ID
                switch (iType.ToString())
                {
                    //Auftraggeber/Versender
                    case "0":
                        /*************************************
                         * Weitergabe der ID des Auftraggebers
                         * Auftraggeber = iADR_ID1
                         * Versender = iADR_ID2
                         * ***********************************/
                        Kunde = true;
                        iTakeOver = iADR_ID1;
                        break;
                    //Versender / Auftraggeber
                    case "1":
                        /*************************************
                         * Weitergabe der ID des Versenders
                         * Auftraggeber = iADR_ID2
                         * Versender = iADR_ID1
                         * ***********************************/
                        Kunde = false;
                        iTakeOver = iADR_ID1;
                        break;
                }

                //ListViewer öffnen
                Functions.frm_FormTypeClose(typeof(frmLiestViewer));
                frmLiestViewer av = new frmLiestViewer(Kunde, iTakeOver);
                av.GL_User = GL_User;
                av.Show();
                av.BringToFront();
            }
        }
        //
        //
        //
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        //------------ Change Sortierung -----------------
        //
        private void btnSort_Click(object sender, EventArgs e)
        {
            if(btnSort.Text== "Sortierung Auftraggeber / Versender")
            {
                iType=1;
                btnSort.Text = "Sortierung Versender / Auftraggeber";
            }
            else
            {
                iType=0;
                btnSort.Text = "Sortierung Auftraggeber / Versender";
            }
            
            //Dgv1 neu laden
            InitSearchType();
        }



    }
}
