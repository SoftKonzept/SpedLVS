using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sped4.Controls.Kalender
{
    public partial class AFKalenderRecource :  Sped4.Controls.AFKalenderItem
    {
        public AFKalenderRecource()
        {
            InitializeComponent();
            this.Name = "KalenderRecource";
            //this.ColorFrom = Color.FromArgb(55, 210, 47);
            //this.ColorTo = Color.FromArgb(17, 67, 14);
        }

        private clsFahrzeuge _Fahrzeug = new clsFahrzeuge();  // Test

        public clsFahrzeuge Fahrzeug
        {
            get
            {
                return _Fahrzeug;
            }
            set
            {
                _Fahrzeug = value;
                FillVehicleData();
            }
        }

        private void FillVehicleData()
        {
            this.Invalidate();
        }


        //__________________ TEST


        private void AFKalenderRecource_Paint(object sender, PaintEventArgs e)
        {
            Rectangle oRectText = new Rectangle(0, 0, this.Width, this.Height);
            StringFormat DrawFormat = new StringFormat();
            DrawFormat.LineAlignment = StringAlignment.Center;
            DrawFormat.FormatFlags = StringFormatFlags.LineLimit;
            DrawFormat.Alignment = StringAlignment.Near;
            
            oRectText.X++;
            oRectText.Y++;
            
            // Schrift für KFZ
            Font = new System.Drawing.Font("Microsoft Sans Serif",
                                                11,
                                                System.Drawing.FontStyle.Regular,
                                                System.Drawing.GraphicsUnit.Point,
                                                ((byte)(0)));

            // Daten auf das Rectangel geschrieben
            e.Graphics.DrawString (Fahrzeug.KFZ, myFontStyle, Brushes.Black, oRectText, DrawFormat);
            oRectText.X--;
            oRectText.Y--;
            //e.Graphics.DrawString(Kommission.Beladestelle, myFontStyle, Brushes.White, oRectText, DrawFormat);
            e.Graphics.DrawString(Fahrzeug.KFZ, myFontStyle, Brushes.White, oRectText, DrawFormat);
            
            //DrawFormat.Alignment = StringAlignment.Far;
            oRectText.X++;
            oRectText.Y++;

          //  e.Graphics.DrawString(Fahrzeug.KFZ, Font, Brushes.Black, oRectText, DrawFormat);
            //e.Graphics.DrawString(Kommission.Entladestelle, myFontStyle, Brushes.Black, oRectText, DrawFormat);
          //  oRectText.X--;
          //  oRectText.Y--;
          //  e.Graphics.DrawString(Fahrzeug.KFZ, Font, Brushes.White, oRectText, DrawFormat);
        }
        //
        // ---------  Load Formatierungsangaben  ---------------------
        //
        private void AFKalenderRecource_Load(object sender, EventArgs e)
        {                
            int height = 33;
            int width = 100;
            int abstand = 0;
 
            if (sender.GetType() == typeof(AFKalenderRecource))
            {
                AFKalenderRecource ctr = (AFKalenderRecource)sender;
                // Unterteilung ZM und Auflieger 
                if (ctr.Name == "KalenderRecource_ZM")
                {
                    abstand = 0;
                    height = 70;
                    ctr.AllowDrop = true;
                }
                if (ctr.Name == "KalenderRecource_A")
                {
                    abstand = height;
                    ctr.AllowDrop = true;
                }
           
            }
            
            //-- hier wird die Größe usw. für das Control festgelegt
            this.Size = new System.Drawing.Size(width, height);;
            this.myBorderSize = 1;
            this.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Anchor = AnchorStyles.Top;
            this.Left = 1;
            this.Top = 2 + abstand;
            

        }
        //
        //-------------- DRAG DATEN AUFNEHMEN   ----------------------TEST TEST TEST
        //
        private void AFKalenderRecource_MouseDown(object sender, MouseEventArgs e)
        {
            AFKalenderRecource RecourceSender = sender as AFKalenderRecource;

            RecourceSender.DoDragDrop(RecourceSender,DragDropEffects.Move);         

        }


        //
        //--------- Mouse up für Drag & Drop   ------------------------
        //
 /***       private void AFKalenderRecource_MouseUp(object sender, MouseEventArgs e)
        {
            xResizeWidth = false;
            xMove = false;
            this.Refresh();
            if (bLocationPositionChanged)
            {
                LocationPositionChanged(this, this.PointToClient(Cursor.Position));
                bLocationPositionChanged = false;
            }
        }
   ***/
    }
}
