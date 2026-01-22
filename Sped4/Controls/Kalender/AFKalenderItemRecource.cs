using LVS;
using Sped4.Classes;
using Sped4.Struct;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Sped4.Controls
{
    public partial class AFKalenderItemRecource : Sped4.Controls.AFKalenderItem
    {
        public const string const_ctrName = "KalenderItemRecource";
        public const string const_ctrName_Fahrer = "KalenderItemRecource_Auflieger";
        public const string const_ctrName_Aufliefer = "KalenderItemRecource_Fahrer";
        public structRecources Recource;
        public clsFahrzeuge Fahrzeug = new clsFahrzeuge();
        internal clsPersonal Personal = new clsPersonal();
        //frmDispoKalender Kalender;
        frmDispo Kalender;


        ///<summary>AFKalenderItemRecource/ AFKalenderItemRecource</summary>
        ///<remarks></remarks>
        public AFKalenderItemRecource(frmDispo _Kalender)
        {
            InitializeComponent();
            Kalender = _Kalender;
            this.Name = AFKalenderItemRecource.const_ctrName;
            this.AllowDrop = true;
            GL_User = _Kalender.GL_User; //GL_User vererbt 
        }
        ///<summary>AFKalenderItemRecource/ AFKalenderItemRecource_Load</summary>
        ///<remarks></remarks>
        private void AFKalenderItemRecource_Load(object sender, EventArgs e)
        {
            string strAusgabe = string.Empty;
            if (Recource.RecourceTyp.ToString() == "F")
            {
                Personal = new clsPersonal();
                Personal.ID = this.Recource.PersonalID;
                Personal.Fill();
                this.ColorFrom = Color.White;
                this.ColorTo = Color.FromArgb(255, 255, 128);
                strAusgabe = Recource.Name;
            }
            if (Recource.RecourceTyp.ToString() == "A")
            {
                strAusgabe = Recource.KFZ;
                Fahrzeug = new clsFahrzeuge();
                Fahrzeug.ID = this.Recource.VehicleID;
                Fahrzeug.Fill();
            }
        }
        ///<summary>AFKalenderItemRecource/ AFKalenderItemRecource_Paint</summary>
        ///<remarks></remarks>
        private void AFKalenderItemRecource_Paint(object sender, PaintEventArgs e)
        {
            string strAusgabe = string.Empty;
            if (Recource.RecourceTyp.ToString() == "F")
            {
                strAusgabe = Recource.Name;
            }
            if (Recource.RecourceTyp.ToString() == "A")
            {
                //strAusgabe = Recource.KFZ;
                clsResource rec = new clsResource();
                rec.m_i_VehicleID_Trailer = Recource.VehicleID;
                strAusgabe = rec.InfoText;
            }

            Rectangle oRectText = new Rectangle(10, 0, this.Width - 30, this.Height);
            StringFormat DrawFormat = new StringFormat();
            DrawFormat.LineAlignment = StringAlignment.Center;
            DrawFormat.FormatFlags = StringFormatFlags.LineLimit;
            if (Recource.RecourceTyp.ToString() == "F")
            {
                DrawFormat.Alignment = StringAlignment.Near;
            }
            if (Recource.RecourceTyp.ToString() == "A")
            {
                DrawFormat.Alignment = StringAlignment.Center;
            }
            //oRectText.X++;
            //oRectText.Y++;
            e.Graphics.DrawString(strAusgabe, myFontStyle, Brushes.Black, oRectText, DrawFormat);
            //e.Graphics.DrawString(Recource.RecourceBez, myFontStyle, Brushes.Black, oRectText, DrawFormat);
            //oRectText.X--;
            //oRectText.Y--;
            //e.Graphics.DrawString(strAusgabe, myFontStyle, Brushes.Black, oRectText, DrawFormat);
            //e.Graphics.DrawString(Recource.RecourceBez, myFontStyle, Brushes.White, oRectText, DrawFormat);

            if (Recource.RecourceTyp.ToString() == "F")
            {
                DrawFormat.Alignment = StringAlignment.Far;
                //oRectText.X++;
                //oRectText.Y++;
                e.Graphics.DrawString(strAusgabe, myFontStyle, Brushes.Black, oRectText, DrawFormat);
                //e.Graphics.DrawString(Recource.ShortText, myFontStyle, Brushes.Black, oRectText, DrawFormat);
                //oRectText.X--;
                //oRectText.Y--;
                //e.Graphics.DrawString(strAusgabe, myFontStyle, Brushes.Black, oRectText, DrawFormat);
                //e.Graphics.DrawString("Test", myFontStyle, Brushes.White, oRectText, DrawFormat); 
            }
        }
        ///<summary>AFKalenderItemRecource/ panRecource_MouseHover</summary>
        ///<remarks></remarks>
        private void panRecource_MouseHover(object sender, EventArgs e)
        {
            ToolTip info = new ToolTip();
            string strInfo = string.Empty;
            //Unterscheidung der Fahrer / Auflieger
            if (this.Recource.RecourceTyp == "A")
            {
                strInfo = strInfo + "Kennzeichen: " + this.Fahrzeug.KFZ + " \n";
                strInfo = strInfo + "Hersteller : " + this.Fahrzeug.Fabrikat + "\n \n";
                if (this.Fahrzeug.Plane.ToString() == "T")
                {
                    strInfo = strInfo + "Plane : ja  \n";
                }
                if (this.Fahrzeug.Sattel.ToString() == "T")
                {
                    strInfo = strInfo + "Sattel : ja  \n";
                }
                if (this.Fahrzeug.Coil.ToString() == "T")
                {
                    strInfo = strInfo + "Coil : ja  \n \n";
                }
                strInfo = strInfo + "Leergewicht : " + this.Fahrzeug.Leergewicht + " \n ";
                strInfo = strInfo + "zul. Gesamtgewicht: " + this.Fahrzeug.zlGG + " \n";
                strInfo = strInfo + "Innenhöhe: " + this.Fahrzeug.Innenhoehe + " \n";
                strInfo = strInfo + "Stellplätze: " + this.Fahrzeug.Stellplaetze + " \n";
                strInfo = strInfo + "Besonderheit: " + this.Fahrzeug.Besonderheit + " \n";
            }
            if (this.Recource.RecourceTyp == "F")
            {
                strInfo = strInfo + "Name: " + Personal.Name + " \n";
                strInfo = strInfo + "Vorname: " + Personal.Vorname + "\n";
            }
            info.SetToolTip(this.panRecource, strInfo);
        }
        /////<summary>AFKalenderItemRecource/ AFKalenderItemRecource_MouseHover</summary>
        /////<remarks></remarks>
        private void AFKalenderItemRecource_MouseHover(object sender, EventArgs e)
        {
            //    ToolTip info = new ToolTip();
            //    string strInfo = string.Empty;
            //    //Unterscheidung der Fahrer / Auflieger
            //    if (this.Recource.RecourceTyp == "A")
            //    {
            //      strInfo = strInfo + "Kennzeichen: " + this.Fahrzeug.KFZ + " \n";
            //      strInfo = strInfo + "Hersteller : " + this.Fahrzeug.Fabrikat + "\n \n";
            //      if (this.Fahrzeug.Plane.ToString() == "T")
            //      {
            //          strInfo = strInfo + "Plane : ja  \n";
            //      }
            //      if (this.Fahrzeug.Sattel.ToString() == "T")
            //      {
            //          strInfo = strInfo + "Sattel : ja  \n";
            //      }
            //      if (this.Fahrzeug.Coil.ToString() == "T")
            //      {
            //          strInfo = strInfo + "Coil : ja  \n \n";
            //      }
            //      strInfo = strInfo + "Leergewicht : " + this.Fahrzeug.Leergewicht+ " \n ";
            //      strInfo = strInfo + "zul. Gesamtgewicht: " + this.Fahrzeug.zlGG+ " \n";
            //      strInfo = strInfo + "Innenhöhe: " +this.Fahrzeug.Innenhoehe + " \n";
            //      strInfo = strInfo + "Stellplätze: " + this.Fahrzeug.Stellplaetze + " \n";
            //      strInfo = strInfo + "Besonderheit: " + this.Fahrzeug.Besonderheit + " \n";
            //    }
            //    if (this.Recource.RecourceTyp == "F")
            //    {
            //        strInfo = strInfo + "Name: " + Personal.Name + " \n";
            //        strInfo = strInfo + "Vorname: " + Personal.Vorname + "\n";                         
            //    }
            //    info.SetToolTip(this,strInfo);

        }
        ///<summary>AFKalenderItemRecource/ panRecource_MouseDoubleClick</summary>
        ///<remarks></remarks>
        private void panRecource_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AFKalenderItemRecource_MouseDoubleClick(this, e);
        }
        ///<summary>AFKalenderItemRecource/ AFKalenderItemRecource_MouseDoubleClick</summary>
        ///<remarks></remarks>
        private void AFKalenderItemRecource_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {

                AFKalenderItemRecource RecourceCtr = (AFKalenderItemRecource)sender;
                Recource.RecourceID = RecourceCtr.Recource.RecourceID;
                Recource.TimeFrom = RecourceCtr.Recource.TimeFrom;
                Recource.TimeTo = RecourceCtr.Recource.TimeTo;
                Recource.PersonalID = RecourceCtr.Recource.PersonalID;
                contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));

            }

            if (e.Button == MouseButtons.Left)
            {
                AFKalenderItemRecource RecourceCtr = (AFKalenderItemRecource)sender;
                Recource.RecourceID = RecourceCtr.Recource.RecourceID;
                Recource.TimeFrom = RecourceCtr.Recource.TimeFrom;
                Recource.TimeTo = RecourceCtr.Recource.TimeTo;
                Recource.PersonalID = RecourceCtr.Recource.PersonalID;

                frmDispoRecourceChange rc = new frmDispoRecourceChange(Recource, Kalender);
                if (Functions.frm_IsFormAlreadyOpen(typeof(frmDispo)) != null)
                {
                    rc.MdiParent = ((frmDispo)Functions.frm_IsFormAlreadyOpen(typeof(frmDispo))).MdiParent;
                }
                rc.StartPosition = FormStartPosition.CenterScreen;
                rc.Show();
                rc.BringToFront();
            }

        }
        ///<summary>AFKalenderItemRecource/ miDelete_Click</summary>
        ///<remarks></remarks>
        private void miDelete_Click(object sender, EventArgs e)
        {
            clsResource RecourceCls = new clsResource();

            if (Recource.RecourceID != 0)
            {
                if (Recource.TimeTo >= DateTime.Now)
                {
                    //Die Recource kann nicht gelöscht werden, wenn sie verwendet wird. 
                    //Check ob und bis wann Sie verwendet wird. Wird sie verwendet, dann
                    //muss die Recourcenendzeit auf die Entladezeit des letzten Transportes 
                    //gesezt werden
                    DateTime tmpMaxREZ = Functions.GetMaxReccourcenEndzeit(ref Recource, this.GL_User);

                    //Ist der Recourcenstartzeitpunkt nach der MaxRecourcenEndzeit,
                    //dann kann die Recource gelöscht werden, sonst
                    //wird die RecourcenEndzeit neu gesetzt
                    if (Recource.TimeFrom > tmpMaxREZ)
                    {
                        //Recource wird gelöscht
                        RecourceCls.DeleteRecource(Recource.RecourceID);
                        //Kalender.KalenderRefresh();
                        if (Recource.RecourceTyp.ToString() == "F")
                        {
                            Kalender.SetFahrerChecked();
                        }
                        if (Recource.RecourceTyp.ToString() == "A")
                        {
                            Kalender.SetAufliegerChecked();
                        }
                    }
                    else   // Rescourcenendzeit wird neu gesetzt
                    {
                        RecourceCls.m_i_RecourceID = Recource.RecourceID;
                        RecourceCls.m_i_PersonalID = Recource.PersonalID;
                        RecourceCls.m_dt_TimeFrom = Recource.TimeFrom;
                        RecourceCls.m_dt_TimeTo = tmpMaxREZ;  // wird geändert
                        RecourceCls.m_i_VehicleID_Truck = RecourceCls.GetTruckIDbyRecourceID();

                        //Kalender.KalenderRefresh();
                        if (Recource.RecourceTyp.ToString() == "F")
                        {
                            RecourceCls.UpdateFahrerEnd();
                            RecourceCls.UpdateTruck();
                            Kalender.SetFahrerChecked();
                        }
                        if (Recource.RecourceTyp.ToString() == "A")
                        {
                            RecourceCls.m_i_VehicleID_Trailer = Recource.VehicleID;

                            RecourceCls.UpdateTruck();
                            RecourceCls.UpdateTrailerEnd();
                            Kalender.SetAufliegerChecked();
                        }
                    }
                }
            }
        }
        ///<summary>AFKalenderItemRecource/ panRecource_MouseClick</summary>
        ///<remarks></remarks>
        private void panRecource_MouseClick(object sender, MouseEventArgs e)
        {
            AFKalenderItemRecource_MouseClick(this, e);
        }
        ///<summary>AFKalenderItemRecource/ AFKalenderItemRecource_MouseClick</summary>
        ///<remarks></remarks>
        private void AFKalenderItemRecource_MouseClick(object sender, MouseEventArgs e)
        {
            //user Berechtigung 
            if (this.GL_User.write_Disposition)
            {
                if (e.Button == MouseButtons.Right)
                {
                    AFKalenderItemRecource RecourceCtr = (AFKalenderItemRecource)sender;
                    Recource.RecourceID = RecourceCtr.Recource.RecourceID;
                    Recource.TimeFrom = RecourceCtr.Recource.TimeFrom;
                    Recource.TimeTo = RecourceCtr.Recource.TimeTo;
                    Recource.PersonalID = RecourceCtr.Recource.PersonalID;
                    contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
            }
        }
        ///<summary>AFKalenderItemRecource/ panRecource_MouseDown</summary>
        ///<remarks></remarks>
        private void panRecource_MouseDown(object sender, MouseEventArgs e)
        {
            //try
            //{
            //    this.panRecource.DoDragDrop(Recource, DragDropEffects.Copy);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}                          
        }








    }
}
