using LVS;
using Sped4.Classes;
using Sped4.Struct;
using System;
using System.Data;
using System.Windows.Forms;
namespace Sped4
{
    public partial class frmDispoAufliegerListe : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        public clsFahrzeuge Auflieger = new clsFahrzeuge();

        public frmDispoAufliegerListe()
        {
            InitializeComponent();
        }

        private void frmDispoAufliegerListe_Load(object sender, EventArgs e)
        {
            Auflieger.BenutzerID = GL_User.User_ID;
            initList();
        }
        //
        //------------  init   -----------------------------------
        //
        public void initList()
        {
            grdList.DataSource = Auflieger.GetAufliegerListe();
            if (grdList.Rows.Count > 0)
            {
                grdList.Columns["ID"].Visible = false;
            }
        }
        //
        //---------- Infofeld mit Aufliegerdaten wird angezeigt ---------------
        //
        private void grdList_MouseClick(object sender, MouseEventArgs e)
        {

            //this.grdAuftrag.Rows[grdAuftrag.CurrentRow.Index].Cells["AuftragID"].Value.ToString();
            decimal AufliegerID = (decimal)this.grdList.Rows[grdList.CurrentRow.Index].Cells["ID"].Value;

            ToolTip info = new ToolTip();
            string strInfo = string.Empty;

            DataSet ds = new DataSet();
            ds = clsFahrzeuge.GetRecByID(this.GL_User, AufliegerID);

            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strInfo = "Aufliegerdaten: \n ";
                strInfo = strInfo + "Kennzeichen: " + ds.Tables[0].Rows[i]["KFZ"].ToString() + " \n";
                strInfo = strInfo + "Fabrikat: " + ds.Tables[0].Rows[i]["Fabrikat"].ToString() + " \n";
                strInfo = strInfo + "Bezeichnung: " + ds.Tables[0].Rows[i]["Bezeichnung"].ToString() + " \n";

                if (Convert.ToChar(ds.Tables[0].Rows[i]["Plane"]) == 'T')
                {
                    strInfo = strInfo + "Plane: ja \n";
                }
                else
                {
                    strInfo = strInfo + "Plane: nein \n";
                }
                if (Convert.ToChar(ds.Tables[0].Rows[i]["Sattel"]) == 'T')
                {
                    strInfo = strInfo + "Sattel: ja \n";
                }
                else
                {
                    strInfo = strInfo + "Sattel: nein \n";
                }
                if (Convert.ToChar(ds.Tables[0].Rows[i]["Coil"]) == 'T')
                {
                    strInfo = strInfo + "Coil: ja \n";
                }
                else
                {
                    strInfo = strInfo + "Coil: nein \n";
                }
                strInfo = strInfo + "Innenhöhe: " + ds.Tables[0].Rows[i]["Innenhoehe"].ToString() + " \n";
                strInfo = strInfo + "Stellplätze (EP): " + ds.Tables[0].Rows[i]["Stellplaetze"].ToString() + " \n";
                strInfo = strInfo + "zul. Gesamtgewicht: " + ds.Tables[0].Rows[i]["zlGG"].ToString() + " \n";
            }
            info.SetToolTip(this, strInfo);
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
                    if ((!object.ReferenceEquals(row.Cells["ID"].Value, DBNull.Value)) | (!object.ReferenceEquals(row.Cells["KFZ"].Value, DBNull.Value)))
                    {
                        //uebergabe Auftragnr und Positionsnummer
                        //Globals.strAuftPosRow IDAndRowID = default(Globals.strAuftPosRow);
                        structRecources Recource = default(structRecources);
                        clsResource RecourceCls = new clsResource();

                        Recource.RecourceTyp = "A";
                        Recource.VehicleID = (decimal)row.Cells["ID"].Value;   // ID des Aufflieger in Farhzeuge
                        Recource.TimeFrom = RecourceCls.m_dt_RecourceStart;
                        Recource.TimeTo = RecourceCls.m_dt_RecourceUnEnd;
                        Recource.KFZ = (string)row.Cells["KFZ"].Value;      // Kennzeichen Auflieger
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
            }
            this.BringToFront();
        }



    }
}
