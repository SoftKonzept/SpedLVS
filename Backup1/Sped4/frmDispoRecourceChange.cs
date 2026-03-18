using LVS;
using Sped4.Classes;
using Sped4.Struct;
using System;

namespace Sped4
{
    public partial class frmDispoRecourceChange : frmTEMPLATE //Sped4.frmTEMPLATE
    {
        frmDispo Kalender;
        public structRecources Recource = new structRecources();
        internal DateTime tmpMaxREZ = new DateTime();
        internal DateTime tmpMaxRSZ = new DateTime();

        public frmDispoRecourceChange(object sender, frmDispo _Kalender)
        {
            InitializeComponent();
            Recource = (structRecources)sender;
            Kalender = _Kalender;
            initForm();
        }
        //
        //
        //
        private void initForm()
        {
            tbRessourcenStartAlt.Text = Recource.TimeFrom.ToString();
            tbRecourcenentzeitAlt.Text = Recource.TimeTo.ToString();

            tmpMaxRSZ = Recource.fRecEndTime;
            tbMaxRSZ.Text = tmpMaxRSZ.ToString();

            tmpMaxREZ = Recource.nRecStartTime;
            tbMaxREZ.Text = tmpMaxREZ.ToString();

            SetStartDateTime();
            SetEndDateTime();
        }
        //
        private void SetStartDateTime()
        {
            //neue Zeiten = aktuelle Zeit setzen
            if (Recource.TimeFrom.Date == DateTime.MinValue.Date)
            {
                dtpNewSDate.Value = dtpNewSDate.MinDate;
            }
            else
            {
                dtpNewSDate.Value = Recource.TimeFrom;
            }
            nudSHour.Value = (decimal)Recource.TimeFrom.Hour;
            nudSMin.Value = (decimal)Recource.TimeFrom.Minute;
        }
        //
        private void SetEndDateTime()
        {
            if (cbNewDateTo.Checked)
            {
                dtp_NewEDate.Value = dtp_NewEDate.MaxDate;
            }
            else
            {
                if (Recource.TimeTo.Date == DateTime.MaxValue.Date)
                {
                    dtp_NewEDate.Value = dtp_NewEDate.MaxDate;
                }
                else
                {
                    dtp_NewEDate.Value = Recource.TimeTo;
                }
            }
            nudEHour.Value = (decimal)Recource.TimeTo.Hour;
            nudEMin.Value = (decimal)Recource.TimeTo.Minute;
        }
        //
        private void DoSave()
        {
            string strSDateTime = GetRecourceStartDateTimeString();
            DateTime dtStart;

            //Endzeit
            string strEDateTime = GetRecourceEndDateTimeString();
            DateTime dtEnd;

            bool bDateTimeParseOK = true;
            //DateTime convert Startzeit        
            if (!DateTime.TryParse(strSDateTime, out dtStart))
            {
                bDateTimeParseOK = false;
            }
            //DateTime convert Endzeit
            if (!DateTime.TryParse(strEDateTime, out dtEnd))
            {
                bDateTimeParseOK = false;
            }

            if (bDateTimeParseOK)
            {
                clsResource RecourceCls = new clsResource();
                RecourceCls.m_i_RecourceID = Recource.RecourceID;
                RecourceCls.m_i_PersonalID = Recource.PersonalID;
                RecourceCls.m_i_VehicleID_Trailer = Recource.VehicleID;
                RecourceCls.m_i_VehicleID_Truck = RecourceCls.GetTruckIDbyRecourceID();
                RecourceCls.m_ch_RecourceTyp = Convert.ToChar(Recource.RecourceTyp);

                if (dtEnd.Date == dtp_NewEDate.MaxDate.Date)
                {
                    dtEnd = DateTime.MaxValue;
                }
                if (dtStart.Date == dtpNewSDate.MinDate.Date)
                {
                    dtStart = DateTime.MinValue;
                }

                RecourceCls.m_dt_TimeTo = dtEnd;
                RecourceCls.m_dt_TimeFrom = dtStart;

                if (RecourceCls.m_ch_RecourceTyp.ToString() == "A")
                {
                    RecourceCls.UpdateTrailerEnd();
                    RecourceCls.UpdateTruck();
                }
                if (RecourceCls.m_ch_RecourceTyp.ToString() == "F")
                {
                    RecourceCls.UpdateFahrerEnd();
                    RecourceCls.UpdateTruck();
                }
                this.Close();
                Kalender.KalenderRefresh();
            }
            else
            {
                clsMessages.Allgemein_ConvertFehlerDateTime();
            }
            initForm();
        }
        //
        //---------------- Speichern - Save ----------------
        //
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DoSave();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nudSMin_ValueChanged(object sender, EventArgs e)
        {
            if (nudSMin.Value == 60)
            {
                nudSMin.Value = 0;
            }
            CheckRecourceStartDateTime();
        }

        private void nudEMin_ValueChanged(object sender, EventArgs e)
        {
            if (nudEMin.Value == 60)
            {
                nudEMin.Value = 0;
            }
            CheckRecourceEndDateTime();
        }

        private void nudSHour_ValueChanged(object sender, EventArgs e)
        {
            CheckRecourceStartDateTime();
        }
        //

        private void dtpNewSDate_ValueChanged(object sender, EventArgs e)
        {
            CheckRecourceStartDateTime();
        }
        //
        private string GetRecourceStartDateTimeString()
        {
            return dtpNewSDate.Value.ToShortDateString() + " " + nudSHour.Value.ToString() + ":" + nudSMin.Value.ToString();
        }
        //
        private void CheckRecourceStartDateTime()
        {
            //Datum darf das Max Startdatum und das Recourcenenddatum nicht überschreiten
            string strTmp = GetRecourceStartDateTimeString();
            DateTime dtTmp;
            if (DateTime.TryParse(strTmp, out dtTmp))
            {

                //check MAX Startdatum
                if (dtTmp >= tmpMaxRSZ)
                {
                    dtpNewSDate.Value = dtTmp;
                }
                else
                {
                    SetStartDateTime();
                }
                //check Recourcenenddatum
                if (dtTmp > Recource.TimeTo)
                {
                    SetStartDateTime();
                }
            }
            else
            {
                SetStartDateTime();
            }
        }
        //
        private string GetRecourceEndDateTimeString()
        {
            return dtp_NewEDate.Value.ToShortDateString() + " " + nudEHour.Value.ToString() + ":" + nudEHour.Value.ToString();
        }
        //
        private void CheckRecourceEndDateTime()
        {
            //Datum darf das Max Startdatum und das Recourcenenddatum nicht überschreiten
            string strTmp = GetRecourceEndDateTimeString();
            DateTime dtTmp;
            if (DateTime.TryParse(strTmp, out dtTmp))
            {
                if (dtTmp.Date == dtpNewSDate.MaxDate.Date)
                {
                    SetEndDateTime();
                }
                else
                {
                    //check Enddatum
                    if (dtTmp <= tmpMaxREZ)
                    {
                        dtpNewSDate.Value = dtTmp;
                    }
                    else
                    {
                        SetEndDateTime();
                    }
                    //check Recourenddatum
                    if (dtTmp < Recource.TimeFrom)
                    {
                        SetEndDateTime();
                    }
                }
            }
            else
            {
                SetEndDateTime();
            }
        }

        private void nudEHour_ValueChanged(object sender, EventArgs e)
        {
            CheckRecourceEndDateTime();
        }

        private void dtp_NewEDate_ValueChanged(object sender, EventArgs e)
        {
            CheckRecourceEndDateTime();
        }

        private void cbNewDateTo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNewDateTo.Checked)
            {
                dtp_NewEDate.Value = dtp_NewEDate.MaxDate;
            }
            else
            {
                SetEndDateTime();
            }
        }




    }
}
