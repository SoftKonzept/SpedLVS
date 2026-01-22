using LVS;
using Sped4.Struct;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sped4.Controls
{
    public partial class AFKalenderItem : UserControl
    {
        public Globals._GL_USER GL_User;
        Int32 _BordreSize;
        Font _Font;
        public event LocationPositionChangedEventHandler LocationPositionChanged;
        public delegate void LocationPositionChangedEventHandler(Object sender, Point point);
        private bool xResizeWidth = false;
        private bool xMove = false;
        private Int32 oldX;
        private Int32 oldY;
        private bool bLocationPositionChanged = false;
        public bool bMouseClick = false;
        private Rectangle rectPicLeft = new Rectangle();
        private Rectangle rectPicRight = new Rectangle();
        Image _imgPicLeft = default(Image);
        Image _imgPicRight = default(Image);
        Color _ColorFrom;// = Color.FromArgb(55, 210, 47);
        Color _ColorTo;// = Color.FromArgb(17, 67, 14); 



        public AFKalenderItem()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Name = "KalenderItem";
        }

        public Image ImagePicLeft
        {
            get
            {
                return _imgPicLeft;
            }
            set
            {
                _imgPicLeft = value;
            }
        }

        public Image ImagePicRight
        {
            get
            {
                return _imgPicRight;
            }
            set
            {
                _imgPicRight = value;
            }
        }

        public Color ColorFrom
        {
            get
            {
                return _ColorFrom;
            }
            set
            {
                _ColorFrom = value;
            }
        }

        public Color ColorTo
        {
            get
            {
                return _ColorTo;
            }
            set
            {
                _ColorTo = value;
            }
        }

        public Font myFontStyle
        {
            get
            {
                return _Font;
            }
            set
            {
                _Font = value;
            }
        }

        public Int32 myBorderSize
        {
            get
            {
                return _BordreSize;
            }
            set
            {
                _BordreSize = value;
            }
        }

        private bool ResizeControl(AFKalenderItem objControl, Int32 intWidth, Int32 intHeight)
        {
            try
            {
                if (xResizeWidth)
                {
                    objControl.Width = intWidth;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private void AFKalenderKommi_Load(object sender, System.EventArgs e)
        {
            this.Size = new System.Drawing.Size(100, 22);
            this.myBorderSize = 1;
            this.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
        //
        //-------------------- DRAG & DROP   -------------------------------------
        //
        private void AFKalenderKommi_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            bool DateCheck = true;

            if (sender.GetType() == typeof(AFKalenderItemRecource))
            {
                AFKalenderItemRecource RecourceCtr = (AFKalenderItemRecource)sender;
                if (RecourceCtr.Recource.TimeFrom < DateTime.Now)
                {
                    DateCheck = false;
                }
            }
            if (sender.GetType() == typeof(AFKalenderItemTour))
            {
                AFKalenderItemTour myTour = (AFKalenderItemTour)sender;
                myTour.GL_User = GL_User;
                //Da sonst die Beladezeit mit geändert wird, diese muss nach dem setzen der Entladezeit wieder zugewiesen werden
                //Kommi.Kommission.BeladeZeitFixed = Kommi.Kommission.BeladeZeit;

                if (myTour.Tour.StatusTour != 5)
                {
                    if (myTour.Tour.StartZeit < DateTime.Now)
                    {
                        DateCheck = false;
                    }
                    structAuftPosRow IDandRow = default(structAuftPosRow);
                    IDandRow.TourID = myTour.Tour.ID;
                }
                else
                {
                    DateCheck = false;
                }

            }
            /****
                      if (sender.GetType() == typeof(AFKalenderItemKommi))
                      {
                        AFKalenderItemKommi Kommi = (AFKalenderItemKommi)sender;
                        Kommi.GL_User = GL_User;
                        //Da sonst die Beladezeit mit geändert wird, diese muss nach dem setzen der Entladezeit wieder zugewiesen werden
                        //Kommi.Kommission.BeladeZeitFixed = Kommi.Kommission.BeladeZeit;
                          /****
                        if (clsAuftragPos.GetStatus(Kommi.Kommission.AuftragID, Kommi.Kommission.AuftragPos) != 5)
                        {
                          if (Kommi.Kommission.BeladeZeit < DateTime.Now)
                          {
                            DateCheck = false;
                          }
                          Globals.strAuftPosRow IDandRow = default(Globals.strAuftPosRow);
                          IDandRow.KommissionsID = Kommi.Kommission.ID;
                        }
                        else
                        {
                          DateCheck = false;
                        }
                      }
             ***/
            if (sender.GetType() != typeof(AFKalenderItemLeerKM))
            {
                if ((e.Button == MouseButtons.Left) & (DateCheck))
                {

                    if ((e.X <= this.Width) && (e.X >= (this.Width - 10)))
                    {
                        //größe KalenderItem wird verändert
                        try
                        {

                            xResizeWidth = true;
                            bLocationPositionChanged = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else
                    {
                        xMove = true;
                        oldX = e.X;
                        oldY = e.Y;
                        bLocationPositionChanged = true;
                    }
                    this.BringToFront();
                }
            }
        }
        //
        //--------------- mouse bewegung    ----------------------------------
        //
        private void AFKalenderKommi_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                // resize control
                ResizeControl(this, e.X, e.Y);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (xMove == false)
            {
                Cursor = Cursors.Default;
            }
            else
            {
                this.Left = this.Left + (e.X - oldX);
                this.Top = this.Top + (e.Y - oldY);
            }
        }
        //
        //------------------    mouse Down für Drag & Drop  ------------------
        //
        private void AFKalenderKommi_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            xResizeWidth = false;
            xMove = false;
            this.Refresh();

            if (!bMouseClick)
            {
                if (bLocationPositionChanged)
                {
                    if (LocationPositionChanged != null)
                    {
                        LocationPositionChanged(this, this.PointToClient(Cursor.Position));         // null Referenz kein Objektverweis
                        bLocationPositionChanged = false;
                    }
                }
            }
            bMouseClick = false;
        }
        //
        //--------------- Paint  Kommi    ------------------------
        //
        private void AFKalenderKommi_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Color ColorOKAY1 = ColorFrom;
            Color ColorOKAY2 = ColorTo;
            Rectangle oRect = new Rectangle(0, 0, this.Width, this.Height);
            LinearGradientBrush oBrush = new LinearGradientBrush(oRect, ColorOKAY1, ColorOKAY2, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(oBrush, oRect);

            e.Graphics.DrawLine(new Pen(Color.White, 1), 0, 0, (this.Width - 1), 0);
            e.Graphics.DrawLine(new Pen(Color.White, 1), 0, (this.Height - 1), (this.Width - 1), (this.Height - 1));
            e.Graphics.DrawLine(new Pen(Color.White, 1), 0, 0, 0, (this.Height - 1));
            e.Graphics.DrawLine(new Pen(Color.White, 1), (this.Width - 1), 0, (this.Width - 1), (this.Height - 1));
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 1, 1, (this.Width - 2), 1);
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 1, (this.Height - 2), (this.Width - 2), (this.Height - 2));
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 1, 1, 1, (this.Height - 2));
            e.Graphics.DrawLine(new Pen(Color.Black, 1), (this.Width - 2), 1, (this.Width - 2), (this.Height - 2));

            try
            {
                if (ImagePicLeft != null)
                {
                    rectPicLeft = new Rectangle(7,
                                           (Int32)((double)(this.Height - 16) / 2),
                                           16,
                                           16);
                    e.Graphics.DrawImage(ImagePicLeft, rectPicLeft);
                }
                if (ImagePicRight != null)
                {
                    rectPicRight = new Rectangle(this.Width - 6 - 16,
                                            (Int32)((double)(this.Height - 16) / 2),
                                            16,
                                            16);
                    e.Graphics.DrawImage(ImagePicRight, rectPicRight);
                }
            }
            catch { }

        }

        private void AFKalenderKommi_Resize(object sender, System.EventArgs e)
        {
            this.Refresh();
        }
    }
}
