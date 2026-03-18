using Sped4.Classes;
using Sped4.Struct;
using System;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmDispoFahrer : Sped4.frmTEMPLATE
    {



        public frmDispoFahrer()
        {
            InitializeComponent();
        }
        //
        //-------------     ----------------------------------
        //
        private void frmDispoFahrer_Load(object sender, EventArgs e)
        {
            initList();
        }

        public void initList()
        {
            grdList.DataSource = clsPersonal.GetFahrerListe();
            grdList.Columns["ID"].Visible = false;
            grdList.Columns["Vorname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        //
        //----------------- Daten für Drag Drop werden gesetzt  -----------------------------
        //
        private void grdList_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = grdList.HitTest(e.X, e.Y);
            if ((info.RowIndex >= 0))
            {
                DataGridViewRow row = grdList.Rows[info.RowIndex];
                if ((row != null))
                {
                    if (!object.ReferenceEquals(row.Cells["ID"].Value, DBNull.Value))
                    {
                        //uebergabe Auftragnr und Positionsnummer
                        //Globals.strAuftPosRow IDAndRowID = default(Globals.strAuftPosRow);
                        structRecources Recource = default(structRecources);
                        clsResource RecourceCls = new clsResource();

                        //--------- Übergabe der Recourcendaten ------------------
                        Recource.RecourceTyp = "F";
                        Recource.PersonalID = (decimal)row.Cells["ID"].Value;   // Personal ID aus DB Personal
                        Recource.TimeFrom = RecourceCls.m_dt_RecourceStart;
                        Recource.TimeTo = RecourceCls.m_dt_RecourceUnEnd;
                        Recource.Name = clsPersonal.GetNameByID(Recource.PersonalID);
                        try
                        {
                            grdList.DoDragDrop(Recource, DragDropEffects.Copy);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }

                }
                this.BringToFront();
            }
        }
        //
        //
        //


    }
}
