using LVS;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sped4.Controls
{
    public partial class AFKalenderRow : UserControl
    {
        private Int32 _RowHight;

        //clsUserProperties up = new clsUserProperties();

        public AFKalenderRow()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Name = "KalenderRow";
        }

        public bool bPaintOtherColor = false;
        public Int32 xStartPoint = 0;
        public Int32 xEndPoint = 0;


        private clsFahrzeuge _Fahrzeug = new clsFahrzeuge();
        //private DateTime _Dispodate;
        private ArrayList _XLines = new ArrayList();

        public ArrayList XLines
        {
            get
            {
                return _XLines;
            }
            set
            {
                _XLines = value;
            }
        }

        private bool _RowHighChanged;
        public bool RowHighChanged
        {
            get
            {
                return _RowHighChanged;
            }
            set { _RowHighChanged = value; }
        }

        public Int32 RowHight
        {
            get
            {
                if (_RowHight <= 71)
                {
                    _RowHight = 71;
                }
                return _RowHight;
            }
            set { _RowHight = value; }
        }
        private Int32 _RowWidth;
        public Int32 RowWidth
        {
            get
            {
                return _RowWidth;
            }
            set { _RowHight = value; }
        }

        private Int32 _reqHight;
        public Int32 reqHight
        {
            get
            {
                return _reqHight;
            }
            set { _reqHight = value; }
        }

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
        //
        //
        //
        private void AFKalenderRow_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            this.Height = this.RowHight;
            //this.Width = this.RowWidth;
            Int32 x;
            Color Color = Color.FromArgb(230, 237, 247);
            Rectangle oRect = new Rectangle(0, 0, this.Width, this.Height);
            LinearGradientBrush oBrush = new LinearGradientBrush(oRect, Color, Color, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(oBrush, oRect);
            Pen pBl = new Pen(Color.FromArgb(191, 219, 255), 1);
            Pen pWh = new Pen(Color.White, 1);
            Pen p0 = new Pen(Color.DarkBlue, 1);        // zur Abgrenzung der Tage
            // Top
            e.Graphics.DrawLine(pBl, 0, 0, this.Width, 0);
            e.Graphics.DrawLine(pWh, 0, 1, this.Width, 1);
            // Bottom
            e.Graphics.DrawLine(pWh, 0, (this.Height - 2), this.Width, (this.Height - 2));
            e.Graphics.DrawLine(pBl, 0, (this.Height - 1), this.Width, (this.Height - 1));
            //****************************** KFZ-Kennzeichen
            //
            Rectangle oRectFahr1 = new Rectangle(0, 0, 1, 1);
            Rectangle oRectFahr2 = new Rectangle(0, 0, 1, 1);

            if (bPaintOtherColor)
            {
                if ((xStartPoint > 0) && (xEndPoint > 0))
                {
                    Rectangle RectOtherColor = new Rectangle(xStartPoint, 2, xEndPoint - xStartPoint, this.Height);
                    Color TmpColor = Color.Yellow;
                    LinearGradientBrush oBrushRectOtherColor = new LinearGradientBrush(oRect, TmpColor, TmpColor, LinearGradientMode.Vertical);
                    e.Graphics.FillRectangle(oBrushRectOtherColor, RectOtherColor);
                }
                else
                {
                    bPaintOtherColor = false;
                }
            }

            Int32 i = 0;
            Int32 tmpX = 1;

            foreach (Object XLi in XLines)
            {
                if (XLi.GetType() == typeof(Int32))
                {
                    x = (Int32)XLi;
                    if (i == 0)
                    {
                        e.Graphics.DrawLine(p0, x, 1, x, (this.Height - 2));
                    }
                    else        // Abgrenzung der Tage
                    {

                        tmpX = i % 48;

                        if (tmpX == 47)
                        {
                            e.Graphics.DrawLine(p0, x, 1, x, (this.Height - 2));
                        }
                        else
                        {
                            e.Graphics.DrawLine(pWh, x, 1, x, (this.Height - 2));
                        }
                    }

                    if ((i == 0))
                    {
                        //Hier wird das Rechteck mit den Fahrzeugdaten gezeichnet am linken
                        //Rand der Fahrzeugzeile
                        oRectFahr1.X = 0;
                        oRectFahr1.Y = 2;
                        oRectFahr1.Width = x - 7;
                        oRectFahr1.Height = this.Height - 4;

                        oRectFahr2.X = x - 7;
                        oRectFahr2.Y = 2;
                        oRectFahr2.Width = 7;
                        oRectFahr2.Height = this.Height - 4;

                        SolidBrush oBrushFahr1 = new SolidBrush(Color.FromArgb(109, 145, 200));
                        LinearGradientBrush oBrushFahr2 = new LinearGradientBrush(oRectFahr2, Color.FromArgb(109, 145, 200), Color.FromArgb(230, 237, 247), LinearGradientMode.Horizontal);
                        e.Graphics.FillRectangle(oBrushFahr1, oRectFahr1);
                        e.Graphics.FillRectangle(oBrushFahr2, oRectFahr2);
                    }


                }
                i++;
            }

            StringFormat DrawFormat = new StringFormat();
            DrawFormat.LineAlignment = StringAlignment.Center;
            DrawFormat.Alignment = StringAlignment.Near;

            //****************************** KFZ-Kennzeichen
            //  
            Font = new System.Drawing.Font("Microsoft Sans Serif",
                                                10,
                                                System.Drawing.FontStyle.Regular,
                                                System.Drawing.GraphicsUnit.Point,
                                                ((byte)(0)));

            //Schrift schwarz vor 
            e.Graphics.DrawString(Fahrzeug.KFZ, Font, Brushes.Black, oRectFahr1, DrawFormat);
            oRectFahr1.X--;
            oRectFahr1.Y--;
            //Schrift weiss nachgezogen
            e.Graphics.DrawString(Fahrzeug.KFZ, Font, Brushes.White, oRectFahr1, DrawFormat);


            //*********************************** Internes Kennzeichen
            //
            Graphics gIKenn = e.Graphics;
            Rectangle RecIKenn = new Rectangle(20, this.Height - this.Height + 10, 30, 15);
            gIKenn.FillRectangle(Brushes.Transparent, RecIKenn);
            e.Graphics.DrawString(Fahrzeug.KIntern.ToString(), Font, Brushes.Black, RecIKenn, DrawFormat);
            RecIKenn.X--;
            RecIKenn.Y--;
            e.Graphics.DrawString(Fahrzeug.KIntern.ToString(), Font, Brushes.White, RecIKenn, DrawFormat);
            oBrush.Dispose();
            pBl.Dispose();
            pWh.Dispose();


            //Anzeige Abgasnorm
            Graphics gRec = e.Graphics;
            Rectangle colorRect = new Rectangle(10, this.Height - 20, 60, 12);

            switch (Fahrzeug.AbgasNorm)
            {
                case "Euro3":
                    gRec.FillRectangle(Brushes.Red, colorRect);
                    break;
                case "Euro4":
                    gRec.FillRectangle(Brushes.Yellow, colorRect);
                    break;
                case "Euro5":
                    gRec.FillRectangle(Brushes.Green, colorRect);
                    break;
                case "Euro6":
                    gRec.FillRectangle(Brushes.Green, colorRect);
                    break;
            }
        }

    }
}
