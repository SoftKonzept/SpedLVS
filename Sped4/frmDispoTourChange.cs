using LVS;
using Sped4.Controls;
using System;

namespace Sped4
{
    public partial class frmDispoTourChange : frmTEMPLATE //Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        clsAuftragsstatus aufStatus = new clsAuftragsstatus();
        frmDispoKalender Kalender;

        public AFKalenderItemTour ctrTour;
        public clsKommission clsKommi;
        public clsTour clsTour;
        public bool beenden = true;

        ///<summary>frmDispoKommiChange / frmDispoKommiChange</summary>
        ///<remarks>Initialisierung</remarks>
        public frmDispoTourChange(frmDispoKalender _Kalender)
        {
            InitializeComponent();


            Kalender = _Kalender;
        }
        ///<summary>frmDispoKommiChange / frmDispoKommiChange_Load</summary>
        ///<remarks></remarks>
        private void frmDispoKommiChange_Load(object sender, EventArgs e)
        {
            if (Kalender._ctrKalenderItemTour.GetType() == typeof(AFKalenderItemTour))
            {
                clsTour = Kalender._ctrKalenderItemTour.Tour;
            }
            else
            {
                clsKommi = (clsKommission)sender;
            }
            GL_User = Kalender.GL_User;
            SetValueToFrm();
        }
        ///<summary>frmDispoKommiChange / SetKommiValueToFrm</summary>
        ///<remarks>Setzt die entsprechenden Daten der Klasse auf die Form.</remarks>
        private void SetValueToFrm()
        {
            if (clsTour != null)
            {
                //Beladezeit
                tbBeladezeitAlt.Text = clsTour.StartZeit.ToString();
                tbEntladezeitAlt.Text = clsTour.EndZeit.ToString();

                dtpNewBeladezeit.Value = clsTour.StartZeit;
                dtpNewEntladezeit.Value = clsTour.EndZeit;

                nud_NBeZeitStd.Value = (decimal)clsTour.StartZeit.Hour;
                nud_NBeZeitMin.Value = (decimal)clsTour.StartZeit.Minute;

                nud_NEntZeitStd.Value = (decimal)clsTour.EndZeit.Hour;
                nud_NEntZeitMin.Value = (decimal)clsTour.EndZeit.Minute;

            }
            else
            {
                //Beladezeit
                tbBeladezeitAlt.Text = clsKommi.BeladeZeit.ToString();
                tbEntladezeitAlt.Text = clsKommi.EntladeZeit.ToString();

                dtpNewBeladezeit.Value = clsKommi.BeladeZeit;
                dtpNewEntladezeit.Value = clsKommi.EntladeZeit;

                nud_NBeZeitStd.Value = (decimal)clsKommi.BeladeZeit.Hour;
                nud_NBeZeitMin.Value = (decimal)clsKommi.BeladeZeit.Minute;

                nud_NEntZeitStd.Value = (decimal)clsKommi.EntladeZeit.Hour;
                nud_NEntZeitMin.Value = (decimal)clsKommi.EntladeZeit.Minute;
            }
        }
        ///<summary>frmDispoKommiChange / toolStripButton1_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DoSave();
        }
        ///<summary>frmDispoKommiChange / DoSave</summary>
        ///<remarks></remarks>
        private void DoSave()
        {
            if (GL_User.write_Disposition)
            {
                SetValue();
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        ///<summary>frmDispoKommiChange / SetValue</summary>
        ///<remarks></remarks>
        private void SetValue()
        {
            CheckStartDateTime();
            CheckEndDateTime();
            if (clsTour != null)
            {
                clsTour.StartZeit = dtpNewBeladezeit.Value;
                clsTour.EndZeit = dtpNewEntladezeit.Value;

                if (clsTour.StartZeit < clsTour.EndZeit)
                {
                    clsTour.UpdateStartZeit();
                    clsTour.UpdateEndZeit();
                    beenden = true;
                }
                else
                {
                    beenden = false;
                    clsMessages.Disposition_EntladeZeitZuKlein();
                }
            }
            else
            {
                clsKommi.BeladeZeit = dtpNewBeladezeit.Value;
                clsKommi.EntladeZeit = dtpNewEntladezeit.Value;
                if (clsKommi.EntladeZeit > clsKommi.BeladeZeit)
                {
                    clsKommi.UpdateKommission();
                    beenden = true;
                }
                else
                {
                    beenden = false;
                    clsMessages.Disposition_EntladeZeitZuKlein();
                }
            }

            if (beenden)
            {
                this.Close();
                Kalender.KalenderRefresh();
            }
        }
        ///<summary>frmDispoKommiChange / tsbClose_Click</summary>
        ///<remarks>Schliesst die Form</remarks>
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ///<summary>frmDispoKommiChange / nud_NBeZeitMin_ValueChanged</summary>
        ///<remarks></remarks>
        private void nud_NBeZeitMin_ValueChanged(object sender, EventArgs e)
        {
            if (nud_NBeZeitMin.Value == 60)
            {
                nud_NBeZeitMin.Value = 0;
            }
        }
        ///<summary>frmDispoKommiChange / nud_NEntZeitMin_ValueChanged</summary>
        ///<remarks></remarks>
        private void nud_NEntZeitMin_ValueChanged(object sender, EventArgs e)
        {
            if (nud_NEntZeitMin.Value == 60)
            {
                nud_NEntZeitMin.Value = 0;
            }
        }
        ///<summary>frmDispoKommiChange / CheckStartDateTime</summary>
        ///<remarks>Emittelt das Startdatum</remarks>
        private void CheckStartDateTime()
        {
            string strTmp = GetBeladeDateTimeString();
            DateTime dtTmp;

            if (clsTour != null)
            {
                if (DateTime.TryParse(strTmp, out dtTmp))
                {
                    dtpNewBeladezeit.Value = dtTmp;
                }
                else
                {
                    dtpNewBeladezeit.Value = clsTour.StartZeit;
                }
            }
            else
            {
                if (DateTime.TryParse(strTmp, out dtTmp))
                {
                    dtpNewBeladezeit.Value = dtTmp;
                }
                else
                {
                    dtpNewBeladezeit.Value = clsKommi.BeladeZeit;
                }
            }
        }
        ///<summary>frmDispoKommiChange / GetBeladeDateTimeString</summary>
        ///<remarks>Setz das Beladedatum als String zusammen</remarks>
        private string GetBeladeDateTimeString()
        {
            string strTmp = dtpNewBeladezeit.Value.ToShortDateString()
                            + " " + nud_NBeZeitStd.Value.ToString()
                            + ":" + nud_NBeZeitMin.Value.ToString()
                            + ":00";
            return strTmp;
        }
        ///<summary>frmDispoKommiChange / CheckEndDateTime</summary>
        ///<remarks>Ermittelt das Enddatum</remarks>
        private void CheckEndDateTime()
        {
            string strTmp = GetEntladeDateTimeString();
            DateTime dtTmp;
            if (clsTour != null)
            {
                if (DateTime.TryParse(strTmp, out dtTmp))
                {
                    dtpNewEntladezeit.Value = dtTmp;
                }
                else
                {
                    dtpNewEntladezeit.Value = clsTour.StartZeit;
                }
            }
            else
            {
                if (DateTime.TryParse(strTmp, out dtTmp))
                {
                    dtpNewEntladezeit.Value = dtTmp;
                }
                else
                {
                    dtpNewEntladezeit.Value = clsKommi.EntladeZeit;
                }
            }
        }
        ///<summary>frmDispoKommiChange / GetEntladeDateTimeString</summary>
        ///<remarks>Setzt das Enddatum als String zusammen</remarks>
        private string GetEntladeDateTimeString()
        {
            string strTmp = dtpNewEntladezeit.Value.ToShortDateString()
                            + " " + nud_NEntZeitStd.Value.ToString()
                            + ":" + nud_NEntZeitMin.Value.ToString()
                            + ":00";
            return strTmp;
        }






    }
}
