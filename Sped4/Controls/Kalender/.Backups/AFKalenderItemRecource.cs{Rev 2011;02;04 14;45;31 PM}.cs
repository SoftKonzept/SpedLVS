using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Sped4.Classes;

namespace Sped4.Controls
{
    public partial class AFKalenderItemRecource : Sped4.Controls.AFKalenderItem
    {
        //public clsResource RecourceCls = new clsResource();
        public Globals._Recources Recource;
        public clsFahrzeuge Fahrzeug= new clsFahrzeuge();
        frmDispoKalender Kalender;

        public AFKalenderItemRecource(frmDispoKalender _Kalender)
        {
            InitializeComponent();
            Kalender = _Kalender;
            this.Name = "KalenderItemRecource";
            this.AllowDrop = true;
        }
        //
        //
        private void AFKalenderItemRecource_Load(object sender, EventArgs e)
        {
            string strAusgabe = string.Empty;
            if (Recource.RecourceTyp.ToString() == "F")
            {            
                this.ColorFrom = Color.White;
                this.ColorTo = Color.FromArgb(255, 255, 128);
                strAusgabe = Recource.Name;
            }
            if (Recource.RecourceTyp.ToString() == "A")
            {
                strAusgabe = Recource.KFZ;
            }
        }
        //
        //
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

        private void AFKalenderItemRecource_MouseHover(object sender, EventArgs e)
        {
                   
            ToolTip info = new ToolTip();
            string strInfo = string.Empty;
            
            AFKalenderItemRecource RecCtr = (AFKalenderItemRecource)sender;
            DataSet ds = new DataSet();

            //Unterscheidung der Fahrer / Auflieger
            if (RecCtr.Recource.RecourceTyp == "A")
            {
              ds = clsFahrzeuge.GetRecByID(RecCtr.Recource.VehicleID);

              for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
              {
                strInfo = strInfo + "Kennzeichen: " + ds.Tables[0].Rows[i]["KFZ"].ToString() + " \n";
                strInfo = strInfo + "Hersteller : " + ds.Tables[0].Rows[i]["Fabrikat"].ToString() + "\n \n";

                if (ds.Tables[0].Rows[i]["Plane"].ToString().ToString() == "T")
                {
                  strInfo = strInfo + "Plane : ja  \n";
                }
                if (ds.Tables[0].Rows[i]["Sattel"].ToString().ToString() == "T")
                {
                  strInfo = strInfo + "Sattel : ja  \n";
                }
                if (ds.Tables[0].Rows[i]["Coil"].ToString().ToString() == "T")
                {
                  strInfo = strInfo + "Coil : ja  \n \n";
                }

                strInfo = strInfo + "Leergewicht : " + ds.Tables[0].Rows[i]["Leergewicht"].ToString() + " \n ";
                strInfo = strInfo + "zul. Gesamtgewicht: " + ds.Tables[0].Rows[i]["zlGG"].ToString() + " \n";
                strInfo = strInfo + "Innenhöhe: " + ds.Tables[0].Rows[i]["Innenhoehe"].ToString() + " \n";
                strInfo = strInfo + "Stellplätze: " + ds.Tables[0].Rows[i]["Stellplaetze"].ToString() + " \n";
                strInfo = strInfo + "Besonderheit: " + ds.Tables[0].Rows[i]["Besonderheit"].ToString() + " \n";
              }
            }
            if (RecCtr.Recource.RecourceTyp == "F")
            {
              ds = clsPersonal.ReadDataByID(RecCtr.Recource.PersonalID);

              for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
              {
                strInfo = strInfo + "Name: " + ds.Tables[0].Rows[i]["Name"].ToString() + " \n";
                strInfo = strInfo + "Vorname: " + ds.Tables[0].Rows[i]["Vorname"].ToString() + "\n";
              }            
            }
            info.SetToolTip(this,strInfo);
        }

        private void AFKalenderItemRecource_MouseDoubleClick(object sender, MouseEventArgs e)
        {
/***
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

              if (Functions.IsFormAlreadyOpen(typeof(frmDispoKalender)) != null)
              {
                Functions.FormClose(typeof(frmDispoKalender));
              }
              frmDispoRecourceChange rc = new frmDispoRecourceChange(Recource, Kalender);
              if (Functions.IsFormAlreadyOpen(typeof(frmDispoKalender)) != null)
              { 
                rc.MdiParent =((frmDispoKalender)Functions.IsFormAlreadyOpen(typeof(frmDispoKalender))).MdiParent;
              }
              rc.StartPosition = FormStartPosition.CenterScreen;
              rc.Show();
              rc.BringToFront();            
            }
      *   * * ***/  
        }
        //
        //
        private void miDelete_Click(object sender, EventArgs e)
        {
            clsResource RecourceCls = new clsResource();

            if (Recource.RecourceID !=0)
            {
                if (Recource.TimeTo >= DateTime.Now)
                {
                    RecourceCls.DeleteRecource(Recource.RecourceID);
                    //Kalender.KalenderRefresh();
                    if (Recource.RecourceTyp.ToString()=="F")
                    {
                      Kalender.SetFahrerChecked();
                    }
                    if (Recource.RecourceTyp.ToString() =="A")
                    {
                      Kalender.SetAufliegerChecked();
                    }
                }   
            }
         }  
        //
        //--------------- Endzeit / DateTo ändern  ---------------
        private void miChangeDateTo_Click(object sender, EventArgs e)
        {
          frmDispoRecourceChange rc = new frmDispoRecourceChange(Recource, Kalender);
          rc.StartPosition = FormStartPosition.CenterScreen;
          rc.Show();
          rc.BringToFront();
        }
        //
        //
        //
        private void AFKalenderItemRecource_MouseClick(object sender, MouseEventArgs e)
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
}
