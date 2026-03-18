using LVS;
using Sped4.Struct;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sped4
{
    public partial class AFOrderPosRectangle : UserControl //Sped4.Controls.AFKalenderItem
    {

        public structDocuments _OrderPosRec = new structDocuments();
        Int32 _BordreSize;
        Font _Font;
        //public event LocationPositionChangedEventHandler LocationPositionChanged;
        public delegate void LocationPositionChangedEventHandler(Object sender);
        //private bool xResizeWidth = false;
        //private bool xMove = false;
        //private Int32 oldX;
        //private Int32 oldY;
        //private bool bLocationPositionChanged = false;
        //private Rectangle rectPicLeft = new Rectangle();
        //private Rectangle rectPicRight = new Rectangle();
        Color _ColorFrom;// = Color.FromArgb(55, 210, 47);
        Color _ColorTo;// = Color.FromArgb(17, 67, 14); 
        public string strAuftragID = string.Empty;
        public string strNewAuftragPos = string.Empty;
        public frmAuftragView av;


        public AFOrderPosRectangle(decimal _AuftragID, decimal _AuftragPos, frmAuftragView _av)
        {
            InitializeComponent();
            strAuftragID = _AuftragID.ToString();
            strNewAuftragPos = _AuftragPos.ToString();
            av = _av;
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

        //
        //
        //
        private void AFOrderPosRectangle_Load(object sender, EventArgs e)
        {
            this.Name = "OrderPosRectangle";
            this.AllowDrop = true;
            this.Width = 70;
            this.Height = 15;
            this.ColorFrom = Color.White;
            this.ColorTo = Color.FromArgb(255, 255, 128);
            this.Size = new System.Drawing.Size(100, 16);  // größe des Rectangle
            this.myBorderSize = 1;
            this.myFontStyle = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void AFOrderPosRectangle_Paint(object sender, PaintEventArgs e)
        {
            Rectangle oRect = new Rectangle(0, 0, this.Width, this.Height);
            string strAusgabe = "A: " + strAuftragID + " - P: " + strNewAuftragPos;
            Color ColorOKAY1 = ColorFrom;
            Color ColorOKAY2 = ColorTo;

            //OrderPosRectangle Fill
            LinearGradientBrush oBrush = new LinearGradientBrush(oRect, ColorOKAY1, ColorOKAY2, LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(oBrush, oRect);

            //Rahmen
            e.Graphics.DrawLine(new Pen(Color.White, 1), 0, 0, (this.Width - 1), 0);
            e.Graphics.DrawLine(new Pen(Color.White, 1), 0, (this.Height - 1), (this.Width - 1), (this.Height - 1));
            e.Graphics.DrawLine(new Pen(Color.White, 1), 0, 0, 0, (this.Height - 1));
            e.Graphics.DrawLine(new Pen(Color.White, 1), (this.Width - 1), 0, (this.Width - 1), (this.Height - 1));
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 1, 1, (this.Width - 2), 1);
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 1, (this.Height - 2), (this.Width - 2), (this.Height - 2));
            e.Graphics.DrawLine(new Pen(Color.Black, 1), 1, 1, 1, (this.Height - 2));
            e.Graphics.DrawLine(new Pen(Color.Black, 1), (this.Width - 2), 1, (this.Width - 2), (this.Height - 2));

            //Text
            Rectangle oRectText = new Rectangle(0, 0, this.Width - 7, this.Height - 2);
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.FormatFlags = StringFormatFlags.LineLimit;
            sf.Alignment = StringAlignment.Center;
            oRectText.X++;
            oRectText.Y++;
            e.Graphics.DrawString(strAusgabe, myFontStyle, Brushes.Black, oRectText, sf);
        }
        //
        //
        //
        private void AFOrderPosRectangle_MouseDown(object sender, MouseEventArgs e)
        {

            if ((e.Button == MouseButtons.Left))
            {
                AFOrderPosRectangle oprCtr = (AFOrderPosRectangle)sender;
                if (oprCtr._OrderPosRec.OrderPosRecID > 0)
                {
                    //LocationPositionChanged(this);

                    try
                    {
                        this.DoDragDrop(_OrderPosRec, DragDropEffects.Move);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    _OrderPosRec.AuftragID = Convert.ToInt32(strAuftragID);
                    //_OrderPosRec.AuftragPos = Convert.ToInt32(strNewAuftragPos);
                    try
                    {
                        this.DoDragDrop(_OrderPosRec, DragDropEffects.Copy);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }
        //
        //
        //
        private void AFOrderPosRectangle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                AFOrderPosRectangle or = (AFOrderPosRectangle)sender;
                _OrderPosRec.AuftragID = or._OrderPosRec.AuftragID;
                _OrderPosRec.DocScanTableID = or._OrderPosRec.DocScanTableID;
                _OrderPosRec.AuftragsNr = or._OrderPosRec.AuftragsNr;
                _OrderPosRec.AuftragPosNr = or._OrderPosRec.AuftragPosNr;
                _OrderPosRec.OrderPosRecID = or._OrderPosRec.OrderPosRecID;

                contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            clsMessages mes = new clsMessages();
            if (mes.OderPosRectangle_Delete())
            {
                //clsOrderPosRectangle OrderPosRec = new clsOrderPosRectangle();
                //OrderPosRec.OrderPosRecID = _OrderPosRec.OrderPosRecID;
                //OrderPosRec.DeleteRectanglePos();
                ////av.Refresh();
                //av.initGrd(_OrderPosRec.AuftragID);
            }
        }

    }
}
