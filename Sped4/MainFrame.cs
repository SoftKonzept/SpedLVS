using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Sped4.Classes;
using Sped4;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

using TwainLib;

namespace TwainGui
{
public class MainFrame : System.Windows.Forms.Form, IMessageFilter
	{

    public frmAuftragView _frmAV;

	private System.Windows.Forms.MdiClient mdiClient1;
	private System.Windows.Forms.MenuItem menuMainFile;
	private System.Windows.Forms.MenuItem menuItemScan;
	private System.Windows.Forms.MenuItem menuItemSelSrc;
	private System.Windows.Forms.MenuItem menuMainWindow;
	private System.Windows.Forms.MenuItem menuItemExit;
	private System.Windows.Forms.MenuItem menuItemSepr;
  private System.Windows.Forms.MainMenu mainFrameMenu;
  private IContainer components;

  public string AuftragID;
  public string ImageArt;
  private MenuItem menuItem1;
  private MenuItem menuItem2;
  private MenuItem menuItem3;      // Auftragsnummer
  public Int32 Count = 0;
  public Int32 FehlerCount = 0;




	//public MainFrame(string strAuftrgID, string _ImageArt)
     public MainFrame(decimal iAuftragID)
	{
 
	    InitializeComponent();
	    tw = new Twain();
	    tw.Init( this.Handle );
        AuftragID = iAuftragID.ToString();
        this.Text= this.Text+"["+AuftragID+"]";    
	}

	protected override void Dispose( bool disposing )
	{
	    if( disposing )
		    {
		    tw.Finish();
		    if (components != null) 
			    {
			    components.Dispose();
			    }
		    }
	    base.Dispose( disposing );
	}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      this.menuMainFile = new System.Windows.Forms.MenuItem();
      this.menuItemSelSrc = new System.Windows.Forms.MenuItem();
      this.menuItemScan = new System.Windows.Forms.MenuItem();
      this.menuItemSepr = new System.Windows.Forms.MenuItem();
      this.menuItemExit = new System.Windows.Forms.MenuItem();
      this.mainFrameMenu = new System.Windows.Forms.MainMenu(this.components);
      this.menuMainWindow = new System.Windows.Forms.MenuItem();
      this.menuItem2 = new System.Windows.Forms.MenuItem();
      this.menuItem3 = new System.Windows.Forms.MenuItem();
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      this.mdiClient1 = new System.Windows.Forms.MdiClient();
      this.SuspendLayout();
      // 
      // menuMainFile
      // 
      this.menuMainFile.Index = 0;
      this.menuMainFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSelSrc,
            this.menuItemScan,
            this.menuItemSepr,
            this.menuItemExit});
      this.menuMainFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.menuMainFile.Text = "&File";
      // 
      // menuItemSelSrc
      // 
      this.menuItemSelSrc.Index = 0;
      this.menuItemSelSrc.MergeOrder = 11;
      this.menuItemSelSrc.Text = "&Select Source...";
      this.menuItemSelSrc.Click += new System.EventHandler(this.menuItemSelSrc_Click);
      // 
      // menuItemScan
      // 
      this.menuItemScan.Index = 1;
      this.menuItemScan.MergeOrder = 12;
      this.menuItemScan.Text = "&Scannen...";
      this.menuItemScan.Click += new System.EventHandler(this.menuItemScan_Click);
      // 
      // menuItemSepr
      // 
      this.menuItemSepr.Index = 2;
      this.menuItemSepr.MergeOrder = 19;
      this.menuItemSepr.Text = "-";
      // 
      // menuItemExit
      // 
      this.menuItemExit.Index = 3;
      this.menuItemExit.MergeOrder = 21;
      this.menuItemExit.Text = "&Exit";
      this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
      // 
      // mainFrameMenu
      // 
      this.mainFrameMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuMainFile,
            this.menuMainWindow,
            this.menuItem2,
            this.menuItem3,
            this.menuItem1});
      // 
      // menuMainWindow
      // 
      this.menuMainWindow.Index = 1;
      this.menuMainWindow.MdiList = true;
      this.menuMainWindow.Text = "&Window";
      // 
      // menuItem2
      // 
      this.menuItem2.Index = 2;
      this.menuItem2.Text = "Start Scannen";
      this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
      // 
      // menuItem3
      // 
      this.menuItem3.Index = 3;
      this.menuItem3.Text = "Exit Scannen";
      this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 4;
      this.menuItem1.Text = "";
      // 
      // mdiClient1
      // 
      this.mdiClient1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mdiClient1.Location = new System.Drawing.Point(0, 0);
      this.mdiClient1.Name = "mdiClient1";
      this.mdiClient1.Size = new System.Drawing.Size(600, 345);
      this.mdiClient1.TabIndex = 0;
      this.mdiClient1.Text = "Scanvorgang: Auftrag";
      // 
      // MainFrame
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(600, 345);
      this.Controls.Add(this.mdiClient1);
      this.IsMdiContainer = true;
      this.Menu = this.mainFrameMenu;
      this.Name = "MainFrame";
      this.Text = "Scanvorgang Auftrag";
      this.Load += new System.EventHandler(this.MainFrame_Load);
      this.ResumeLayout(false);

		}
		#endregion



	private void menuItemExit_Click(object sender, System.EventArgs e)
		{
		Close();
		}
  //Menüpunkt Start Scannen
  private void menuItem2_Click(object sender, EventArgs e)
  {
/***
      if (Functions.frm_IsFormAlreadyOpen(typeof(frmScanDokArt)) != null)
      {
          Functions.frm_FormClose(typeof(frmScanDokArt));
      }
      frmScanDokArt Doku = new frmScanDokArt(this);
      Doku.Show();
      Doku.BringToFront();
 ****/  

      ImageArt = string.Empty;
      menuItemScan_Click(sender, e);

  }
	public void menuItemScan_Click(object sender, System.EventArgs e)
	{
	    if( ! msgfilter )
	    {
	        this.Enabled = false;
	        msgfilter = true;
	        Application.AddMessageFilter( this );
	    }
	    tw.Acquire();
	}

	private void menuItemSelSrc_Click(object sender, System.EventArgs e)
		{
		tw.Select();
		}


	bool IMessageFilter.PreFilterMessage( ref Message m )
		{
		TwainCommand cmd = tw.PassMessage( ref m );
		if( cmd == TwainCommand.Not )
			return false;

		switch( cmd )
			{
			case TwainCommand.CloseRequest:
				{
				EndingScan();
				tw.CloseSrc();
				break;
				}
			case TwainCommand.CloseOk:
				{
				EndingScan();
				tw.CloseSrc();
				break;
				}
			case TwainCommand.DeviceEvent:
				{
				break;
				}
			case TwainCommand.TransferReady:
				{
				    ArrayList pics = tw.TransferPictures();
                    // ArrayList pics1 = tw.TransferPictures();
				    EndingScan();
				    tw.CloseSrc();
				    picnumber++;
                    for (int i = 0; i < pics.Count; i++)
                    {

                        Bitmap bmp = GetBitMap(pics, i);
                        clsAuftragScan Scan = new clsAuftragScan();
                        Scan.m_i_AuftragID = Convert.ToInt32(AuftragID);
                        Scan.m_str_ImageArt = ImageArt;
                        if (Scan.CheckAuftragScanIsIn())
                        {
                            Count = Scan.GetMaxPicNumByAuftrag();
                        }
                        Count++;
                        Scan.m_i_picnum = Count;
                        Scan.AuftragImageIn = bmp;
                        Scan.WriteToAuftragImage();

                        IntPtr img = (IntPtr)pics[i];
                        //IntPtr img = BitmapToIntPtr(bmp);
                        PicForm newpic = new PicForm(img, AuftragID, picnumber.ToString());    // Aufruf PicForm hier könnte ID übergeben werden
                        newpic.MdiParent = this;
                        int picnum = i + 1;
                        //newpic.Text = "ScanPass" + picnumber.ToString() + "_Pic" + picnum.ToString();

                        newpic.Text = AuftragID + "_Nr._" + Count.ToString() + " - Dokument wurde in der Datenbank gespeichert ";
                        newpic.Show();

                    }
	                //Form soll direkt nach dem Scan geschlossen werden
                    EndingScan();
                    tw.CloseSrc();
                    
                    //Refresh FrmAuftragView
                    if (_frmAV != null)
                    {
                        decimal decTmp = 0.0M;
                        if (Decimal.TryParse(AuftragID.ToString(), out decTmp))
                        {
                            _frmAV.initGrd(decTmp);
                        }
                    }

                    this.Close();

				    break;
				}
			}

        if (FehlerCount > 1)
        {
           /// MessageBox.Show("Es ist ein Fehler beim Scan-Vorgang aufgetreten" + Environment.NewLine+
           //                 "Der Scan-Vorgang wird abgebrochen!", "ACHTUNG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            EndingScan();
            tw.CloseSrc();
            this.Close();
        }
        FehlerCount++;
		return true;
		}

	private void EndingScan()
		{
		if( msgfilter )
			{
			Application.RemoveMessageFilter( this );
			msgfilter = false;
			this.Enabled = true;
			this.Activate();
			}
		}

	private bool	msgfilter;
	private Twain	tw;
	private int		picnumber = 0;

  private void menuItem3_Click(object sender, EventArgs e)
  {
    Close();
  }


  // eigen
  public IntPtr BitmapToIntPtr(Bitmap bmp)
  {
    IntPtr ptr = IntPtr.Zero;
    System.Drawing.Imaging.BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
    ptr = bd.Scan0;

    return ptr;
  }

  // ausm Netz
  public Bitmap GetBitMap(ArrayList alPics, Int32 iPic)
  {
    //int i = 0; // das habe ich der Methode hinzugefügt
    Rectangle rPic = new Rectangle(0, 0, 0, 0);
    IntPtr ipNonLockedBitMap = (IntPtr)alPics[iPic];
    IntPtr ipBitMap =GlobalLock(ipNonLockedBitMap); // TODO: Grab GlobalLock & GlobalFree from PicForm.cs    
    BITMAPINFOHEADER bmihInfo = new BITMAPINFOHEADER(); // TODO: Grab BITMAPINFOHEADER from PicForm.cs
    Marshal.PtrToStructure(ipBitMap, bmihInfo);
    rPic.X = rPic.Y = 0;
    rPic.Width = bmihInfo.biWidth;
    rPic.Height = bmihInfo.biHeight;

    if (bmihInfo.biSizeImage == 0)
      bmihInfo.biSizeImage = ((((bmihInfo.biWidth * bmihInfo.biBitCount) + 31) & ~31) >> 3) * bmihInfo.biHeight;

    int p = bmihInfo.biClrUsed;
    if ((p == 0) && (bmihInfo.biBitCount <= 8))
      p = 1 << bmihInfo.biBitCount;
    p = (p * 4) + bmihInfo.biSize + (int)ipBitMap;

    IntPtr ipPixel = (IntPtr)p;

    Bitmap bmp = new Bitmap(rPic.Width, rPic.Height);
    Graphics g = Graphics.FromImage((Image)bmp);
    IntPtr hdc = g.GetHdc();

    SetDIBitsToDevice(hdc, 0, 0, bmp.Width, bmp.Height, 0, 0, 0, bmp.Height, ipPixel, ipBitMap, 0);
    g.ReleaseHdc(hdc);

    //if (ipNonLockedBitMap != IntPtr.Zero)
    //{
    //  GlobalFree(ipNonLockedBitMap);
    //  ipNonLockedBitMap = IntPtr.Zero;
    //}
    return bmp;
  }
  [DllImport("gdi32.dll", ExactSpelling = true)]
  internal static extern int SetDIBitsToDevice(IntPtr hdc, int xdst, int ydst,
                      int width, int height, int xsrc, int ysrc, int start, int lines,
                      IntPtr bitsptr, IntPtr bmiptr, int color);

  [DllImport("kernel32.dll", ExactSpelling = true)]
  internal static extern IntPtr GlobalLock(IntPtr handle);
  [DllImport("kernel32.dll", ExactSpelling = true)]
  internal static extern IntPtr GlobalFree(IntPtr handle);

  [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
  public static extern void OutputDebugString(string outstr);

  private void MainFrame_Load(object sender, EventArgs e)
  {

  }

/***
	[STAThread]
	static void Main() 
		{
		if( Twain.ScreenBitDepth < 15 )
			{
			MessageBox.Show( "Need high/true-color video mode!", "Screen Bit Depth", MessageBoxButtons.OK, MessageBoxIcon.Information );
			return;
			}

		MainFrame mf = new MainFrame();
		Application.Run( mf );
		}
***/
	} // class MainFrame

} // namespace TwainGui
