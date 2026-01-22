using LVS;
using Sped4.Struct;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace Sped4
{
    public partial class frmAuftragView : Sped4.frmTEMPLATE
    {
        public const string const_ColImageVorschau = "ImageIcon";
        internal clsTour Tour;

        public delegate void AuftragViewResizeEventHandler();
        public event AuftragViewResizeEventHandler ResizeAuftragView;

        public delegate void KommiDetailPanelCloseEventHandler();
        public event KommiDetailPanelCloseEventHandler CloseKommiDetailsPanel;

        public bool DidDragDrop = false;
        public Globals._GL_USER GL_User;
        public ctrAufträge _ctrAuftrag;
        public clsDocScan DocScan;
        public ctrSUList _ctrSUListe;
        public ctrArtDetails _artD;
        public frmAuftrag_Splitting _AuftragSplit;
        public frmKommiDetailsPanel KommiDetailPanel;
        public ctrMenu _ctrMenu;

        internal byte[] bShowDoc;
        internal byte[] bRotateShowDoc;
        internal Image imgShowDoc;
        internal DataTable dt = new DataTable("Docs");

        structDocuments Docs;
        public bool CanDoDragDrop = false;
        public string ImageArt = string.Empty;
        public Int32 PicNum;

        public bool DokumenteLiegenVor = true;

        ///<summary>frmAuftragView/ frmAuftragView</summary>
        ///<remarks></remarks> 
        public frmAuftragView()
        {
            InitializeComponent();
        }
        ///<summary>frmAuftragView/ frmAuftragView_Load</summary>
        ///<remarks></remarks> 
        private void frmAuftragView_Load(object sender, EventArgs e)
        {
            DocScan = new clsDocScan();
            DocScan.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
            //Ermittel der Daten
            if (this.Tour != null)
            {
                this.Text = "Auftrag / Position #: " + this.Tour.Auftrag.ANr.ToString() + "/" + this.Tour.Auftrag.AuftragPos.AuftragPos.ToString();
                InitArtDetailsCtr();
                InitDGV();
            }
            else
            {
                this.Close();
            }
        }
        ///<summary>frmAuftragView/ InitDGV</summary>
        ///<remarks></remarks>
        public void InitDGV()
        {
            dt = this.Tour.Auftrag.Docs.GetDocScanImageTable(this.Tour.Auftrag.ID);
            if (!dt.Columns.Contains(frmAuftragView.const_ColImageVorschau))
            {
                dt.Columns.Add(frmAuftragView.const_ColImageVorschau, typeof(Image));
                dt.Columns[frmAuftragView.const_ColImageVorschau].SetOrdinal(0);
            }
            if (dt.Rows.Count > 0)
            {
                this.dgvImg.DataSource = dt;
                foreach (GridViewColumn col in this.dgvImg.Columns)
                {
                    string ColName = col.Name.ToString();
                    switch (ColName)
                    {
                        case frmAuftragView.const_ColImageVorschau:
                            GridViewImageColumn imgCol = (GridViewImageColumn)col;
                            imgCol.IsVisible = true;
                            imgCol.ImageLayout = ImageLayout.Stretch;
                            imgCol.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                            imgCol.Width = 75;
                            break;
                        default:
                            col.IsVisible = false;
                            break;
                    }
                }
                //this.dgvImg.BestFitColumns();  
                if (this.dgvImg.Rows.Count > 0)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(this.dgvImg.Rows[0].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        DocScan.ID = decTmp;
                        DocScan.Fill();
                        this.pdfViewer.LoadDocument(this.DocScan.DocPath);
                    }
                }
            }
            else
            {
                DokumenteLiegenVor = false;
            }
        }
        ///<summary>frmAuftragView/ dgvImg_CellClick</summary>
        ///<remarks></remarks>
        private void dgvImg_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                decimal decTmp = 0;
                Decimal.TryParse(this.dgvImg.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out decTmp);
                if (decTmp > 0)
                {
                    DocScan.ID = decTmp;
                    DocScan.Fill();
                }
            }
        }
        ///<summary>frmAuftragView/ dgvImg_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvImg_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (this.Docs.ArtikelTableID > 0)
                {
                    this.pdfViewer.LoadDocument(this.DocScan.DocPath);
                }
            }
        }
        ///<summary>frmAuftragView/ dgvImg_CellFormatting</summary>
        ///<remarks></remarks>
        private void dgvImg_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (this.dgvImg.Rows.Count > 0)
            {
                try
                {
                    string ColName = e.Column.Name.ToString();
                    string Dateityp = string.Empty;
                    switch (ColName)
                    {
                        case frmAuftragView.const_ColImageVorschau:
                            string strPath = e.CellElement.RowInfo.Cells["ScanFilename"].Value.ToString();
                            Dateityp = System.IO.Path.GetExtension(strPath);
                            e.CellElement.Image = Functions.GetFileExtensionImage(Dateityp);
                            break;
                        default:
                            e.CellElement.Image = Functions.GetFileExtensionImage(Dateityp);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
        }
        ///<summary>frmAuftragView/ dgvImg_RowFormatting</summary>
        ///<remarks></remarks>
        private void dgvImg_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (this.dgvImg.Rows.Count > 0)
            {
                e.RowElement.RowInfo.Height = 85;
            }
        }
        ///<summary>frmAuftragView/ InitArtDetailsCtr</summary>
        ///<remarks></remarks>
        private void InitArtDetailsCtr()
        {
            _artD = new ctrArtDetails();
            _artD.Auftrag = this.Tour.Auftrag;
            _artD.GL_User = GL_User;
            _artD._ctrMenu = this._ctrMenu;
            _artD.Parent = this.scMainPage.Panel1;
            _artD.Dock = DockStyle.Fill;
            _artD.do_ChangesArtikel = false;
            _artD.do_ChangesAuftragdetails = false;
            _artD.CheckWarning();
            _artD.Show();
            _artD.BringToFront();
            this.scMainPage.SplitterDistance = _artD.Width + 50;
            //this.splitPanel1.Width = _artD.Width + 50;
        }
        ///<summary>frmAuftragView/ imgGrd_CellFormatting</summary>
        ///<remarks></remarks>
        private void imgGrd_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //clsDocScan AuftragScan = new clsDocScan();

                ////Auftrag
                //if ((!object.ReferenceEquals(imgGrd.Rows[e.RowIndex].Cells["ID"].Value, DBNull.Value)))
                //{
                //    AuftragScan.m_dec_DocScanID = (decimal)imgGrd.Rows[e.RowIndex].Cells["ID"].Value;
                //}
                ////Auftrag
                //if ((!object.ReferenceEquals(imgGrd.Rows[e.RowIndex].Cells["AuftragID"].Value, DBNull.Value)))
                //{
                //    AuftragScan.m_dec_AuftragID = (decimal)imgGrd.Rows[e.RowIndex].Cells["AuftragID"].Value;
                //}
                ////LEingangID
                //if ((!object.ReferenceEquals(imgGrd.Rows[e.RowIndex].Cells["LEingangID"].Value, DBNull.Value)))
                //{
                //    AuftragScan.m_dec_AuftragID = (decimal)imgGrd.Rows[e.RowIndex].Cells["LEingangID"].Value;
                //}
                ////LAusgangID
                //if ((!object.ReferenceEquals(imgGrd.Rows[e.RowIndex].Cells["LAusgangID"].Value, DBNull.Value)))
                //{
                //    AuftragScan.m_dec_AuftragID = (decimal)imgGrd.Rows[e.RowIndex].Cells["LAusgangID"].Value;
                //}
                ////DocImage
                //if ((!object.ReferenceEquals(imgGrd.Rows[e.RowIndex].Cells["DocImage"].Value, DBNull.Value)))
                //{
                //    AuftragScan.bImage = (byte[])imgGrd.Rows[e.RowIndex].Cells["DocImage"].Value;
                //}
                ////Filename
                //if ((!object.ReferenceEquals(imgGrd.Rows[e.RowIndex].Cells["ScanFilename"].Value, DBNull.Value)))
                //{
                //    AuftragScan.m_str_Filename = (string)imgGrd.Rows[e.RowIndex].Cells["ScanFilename"].Value;
                //}
                ////PicNum
                //if ((!object.ReferenceEquals(imgGrd.Rows[e.RowIndex].Cells["PicNum"].Value, DBNull.Value)))
                //{
                //    AuftragScan.m_i_picnum = (Int32)imgGrd.Rows[e.RowIndex].Cells["PicNum"].Value;
                //    //PicNum = (Int32)imgGrd.Rows[e.RowIndex].Cells["PicNum"].Value;
                //}
                ////Image Art
                //if ((!object.ReferenceEquals(imgGrd.Rows[e.RowIndex].Cells["ImageArt"].Value, DBNull.Value)))
                //{
                //    AuftragScan.m_str_ImageArt = (string)imgGrd.Rows[e.RowIndex].Cells["ImageArt"].Value;
                //    //ImageArt = (string)imgGrd.Rows[e.RowIndex].Cells["ImageArt"].Value;
                //}
                //Grafik
                if (e.ColumnIndex == 0)
                {
                    //e.Value = AuftragScan.Thumb;
                    e.Value = clsDocScan.MakeThumb(Sped4.Properties.Resources.docPDF);

                    //if (AuftragScan.m_dec_DocScanID > 0)
                    //{
                    //    ////setzt die Hintergrundfarbe für gelesen und nicht gelesen
                    //    //if (clsAuftragRead.UserReadDoc(this.GL_User, AuftragScan.m_dec_DocScanID))
                    //    //{
                    //    //    e.CellStyle.BackColor = Color.White;
                    //    //}
                    //    //else
                    //    //{
                    //    //    e.CellStyle.BackColor = Color.Red;
                    //    //}
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        ///<summary>frmAuftragView/ tsbClose_Click</summary>
        ///<remarks></remarks>
        private void tsbClose_Click(object sender, EventArgs e)
        {
            if (_artD != null)
            {
                if ((_artD.do_ChangesArtikel) |
                    (_artD.do_ChangesAuftragdetails))
                {
                    if (clsMessages.Allgemein_ChangesToSave())
                    {
                        if (_artD.do_ChangesAuftragdetails)
                        {
                            _artD.SaveAuftragsDaten();
                        }
                        if (_artD.do_ChangesArtikel)
                        {
                            _artD.SaveArtDaten();
                        }
                    }
                }
            }
            CloseForm();
        }
        ///<summary>frmAuftragView/ CloseForm</summary>
        ///<remarks></remarks>
        private void CloseForm()
        {
            if (DidDragDrop)
            {
                DidDragDrop = false;
            }
            else
            {
                if (_ctrAuftrag != null)
                {
                    _ctrAuftrag.InitDGV();
                    //_ctrAuftrag.ctrAuftragRefresh();
                }
                if (this._ctrMenu != null)
                {
                    if (this._ctrMenu._ctrTourDetails != null)
                    {
                        this._ctrMenu._ctrTourDetails.RefreshCtrTourDetails();
                    }
                }
            }

            if (KommiDetailPanel != null)
            {
                KommiDetailPanel.CloseFrmKommiDetailsPanel();
            }
            this.Close();
        }
        ///<summary>frmAuftragView/ AddAuftragRead</summary>
        ///<remarks></remarks>
        private void AddAuftragRead(decimal mydecDocScanID)
        {
            clsAuftragRead read = new clsAuftragRead();
            read.AuftragPosID = clsDocScan.GetAuftragPosTableIDByDocScanTableID(this.GL_User, mydecDocScanID);
            if (DokumenteLiegenVor)
            {
                if (mydecDocScanID > 0)
                {
                    read.IDAuftragScan = mydecDocScanID;
                    read.UserID = GL_User.User_ID;
                    if (!clsAuftragRead.UserReadAuftragPosDoc(this.GL_User, read.IDAuftragScan, read.AuftragPosID))
                    {
                        read.Add();
                    }
                }
            }
        }
        ///<summary>frmAuftragView/ imgGrd_CellContentClick</summary>
        ///<remarks></remarks>
        public void imgGrd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > -1)
            //{
            //    decimal decTmp = 0;
            //    Decimal.TryParse(this.imgGrd
            //}
            //CleanCtrFromOrderPosRectangle();
            ////Auswahl des Images
            //if (DokumenteLiegenVor)
            //{
            //    //clsAuftragScan AuftragScan = new clsAuftragScan();
            //    if (this.imgGrd.Rows.Count > 0)
            //    {
            //        SetDatenShownDoc(sender, e);
            //        if (imgShowDoc != null)
            //        {
            //            //vervollständigung der daten für Global._Dokumente
            //            PicNum = Convert.ToInt32(imgGrd.Rows[e.RowIndex].Cells["PicNum"].Value);

            //            Docs.AuftragID = Convert.ToInt32(this.imgGrd.Rows[e.RowIndex].Cells["AuftragID"].Value);
            //            Docs.PicNumber = PicNum;
            //            Docs.DocScanTableID = Convert.ToDecimal(imgGrd.Rows[e.RowIndex].Cells["ID"].Value);

            //            this.pictureBox1.Image = ResizeDocsImage(imgShowDoc);
            //            this.DisplayScrollBars();
            //            this.SetScrollBarValues();


            //            ////---- Nun werden die Positionsmarkierungen Rectangle Position gelesen zum entsprechenden Auftrag
            //            //clsOrderPosRectangle or = new clsOrderPosRectangle();
            //            //or.AuftragID = Convert.ToInt32(this.imgGrd.Rows[e.RowIndex].Cells["AuftragID"].Value);
            //            //or.PicNum = Convert.ToInt32(imgGrd.Rows[e.RowIndex].Cells["PicNum"].Value);
            //            //or.ImageArt = imgGrd.Rows[e.RowIndex].Cells["ImageArt"].Value.ToString();

            //            //DataTable dataTable = or.GetRectanglePos();
            //            //SetRectangle(dataTable);


            //            //Auftrag als gelesen markieren
            //            //AddAuftragRead(Docs.DocScanTableID);
            //        }
            //        else
            //        {
            //            clsMessages mes = new clsMessages();
            //            MessageBox.Show(mes.ctrAuftrag_AuftragPDFnotExists);
            //        }
            //    }
            //    else
            //    {
            //        clsMessages mes = new clsMessages();
            //        MessageBox.Show(mes.ctrAuftrag_AuftragPDFnotExists);
            //    }
            //}
        }
        ///<summary>frmAuftragView/ SetGL_DocValue</summary>
        ///<remarks></remarks>
        private void SetGL_DocValue(Int32 iRowIndex)
        {
            //if (this.imgGrd.CurrentRow != null)
            //{
            //    // clsAuftragPos APos = new clsAuftragPos();
            //    // APos._GL_User = this.GL_User;
            //    //// _AuftragPosTableID = (decimal)this.imgGrd.Rows[this.imgGrd.CurrentCell.RowIndex].Cells["AuftragPosTableID"].Value;
            //    // _AuftragPosTableID = (decimal)this.imgGrd.Rows[iRowIndex].Cells["AuftragPosTableID"].Value;
            //    // APos.ID = _AuftragPosTableID;
            //    // APos.Fill();
            //    //decimal decTmp = 0;
            //    //Decimal.TryParse(this.imgGrd.Rows[this.imgGrd.CurrentCell.RowIndex].Cells["AuftragPosTableID"].Value.ToString(), out decTmp);
            //    //if (decTmp > 0)
            //    //{
            //    //    this.Tour.Auftrag.AuftragPos.ID = decTmp;
            //    //    this.Tour.Auftrag.AuftragPos.Fill();
            //    //    this.Tour.Auftrag.ID = this.Tour.Auftrag.AuftragPos.AuftragTableID;
            //    //    this.Tour.Auftrag.Fill();                    
            //    //}
            //    decimal decTmp = 0;
            //    Decimal.TryParse(this.imgGrd.Rows[this.imgGrd.CurrentCell.RowIndex].Cells["ID"].Value.ToString(), out decTmp);
            //    if (decTmp > 0)
            //    {
            //        DocScan.ID = decTmp;
            //        DocScan.Fill();

            //        //Docs.DocScanTableID = (decimal)this.imgGrd.Rows[iRowIndex].Cells["ID"].Value;
            //        //Docs.AuftragTableID = (decimal)this.imgGrd.Rows[iRowIndex].Cells["AuftragID"].Value;
            //        //Docs.AuftragPosTableID = this.Tour.Auftrag.AuftragPos.ID;
            //        //Docs.LEingangID = (decimal)this.imgGrd.Rows[iRowIndex].Cells["LEingangID"].Value;
            //        //Docs.LAusgangID = (decimal)this.imgGrd.Rows[iRowIndex].Cells["LAusgangID"].Value;
            //        //Docs.PicNumber = (Int32)this.imgGrd.Rows[iRowIndex].Cells["PicNum"].Value;
            //        //Docs.Pfad = this.imgGrd.Rows[iRowIndex].Cells["Pfad"].Value.ToString().Trim();
            //        //Docs.ScanFilename = this.imgGrd.Rows[iRowIndex].Cells["ScanFilename"].Value.ToString().Trim();
            //        //Docs.AuftragsNr = this.Tour.Auftrag.AuftragPos.Auftrag_ID;
            //        //Docs.AuftragPosNr = this.Tour.Auftrag.AuftragPos.AuftragPos;
            //    }
            //}
        }
        ///<summary>frmAuftragView/ SetDatenShownDoc</summary>
        ///<remarks></remarks>
        private void SetDatenShownDoc(object sender, DataGridViewCellEventArgs e)
        {
            clsDocScan AuftragScan = new clsDocScan();
            //AuftragScan.m_dec_DocScanID = Docs.DocScanTableID;
            //AuftragScan.m_dec_AuftragID = Docs.AuftragTableID;
            //AuftragScan.m_dec_LEingangID = Docs.LEingangID;
            //AuftragScan.m_dec_LAusgangID = Docs.LAusgangID;

            //if (this.imgGrd.Rows[e.RowIndex].Cells["DocImage"].Value != DBNull.Value)
            //{
            //    bShowDoc = (byte[])this.imgGrd.Rows[e.RowIndex].Cells["DocImage"].Value;
            //    AuftragScan.bImage = bShowDoc;
            //}
            //else
            //{
            //    string strFilePath = Application.StartupPath + this.imgGrd.Rows[e.RowIndex].Cells["Pfad"].Value.ToString() + this.imgGrd.Rows[e.RowIndex].Cells["ScanFilename"].Value.ToString();
            //    //Pfadprüfen 
            //    if (File.Exists(strFilePath))
            //    {
            //        Bitmap bmp = new Bitmap(strFilePath);
            //        MemoryStream ms = new MemoryStream();
            //        bmp.Save(ms, ImageFormat.Jpeg);
            //        bShowDoc = ms.ToArray();
            //        AuftragScan.bImage = bShowDoc;
            //    }
            //}
            //AuftragScan.m_i_picnum = Convert.ToInt32(imgGrd.Rows[e.RowIndex].Cells["PicNum"].Value);
            //imgShowDoc = AuftragScan.AuftragImageOut;
        }
        ///<summary>frmAuftragView/ ResizeDocsImage</summary>
        ///<remarks></remarks>
        private Image ResizeDocsImage(Image MasterImage)
        {
            //  GetRectanglePos(PicNum, ImageArt);    // setzt die gespeicherten Rectangle
            //Der Auftrag wird auf eine Bildschirmgröße gebracht, damit die ganze Seite sichtbar ist
            //Größenbeschreibung siehe clsImages  
            //Picturebox hat die Größe Width= 656 und Height 912 an diese Größe wird das Image angepasst
            //double factWidth = Convert.ToDouble(pictureBox1.Width) / Convert.ToDouble(MasterImage.Width);
            //double factHeigt = Convert.ToDouble(pictureBox1.Height) / Convert.ToDouble(MasterImage.Height);
            clsImages img = new clsImages();
            //img.fHeight = factHeigt;
            //img.fWidth = factWidth;
            //img.ImageIn = MasterImage;
            //img.ResizeImageByWidthHeight();

            return img.returnImage;
        }
        ///<summary>frmAuftragView/ SetRectangle</summary>
        ///<remarks></remarks>
        private void SetRectangle(DataTable dt)
        {
            //Point recPoint = new Point();
            //recPoint.Y = 0;
            //recPoint.X = 0;

            //if (dt.Rows.Count > 0)
            //{
            //    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            //    {
            //        AFOrderPosRectangle ORP = new AFOrderPosRectangle((decimal)dt.Rows[i]["Auftrag"], (decimal)dt.Rows[i]["AuftragPos"], this);
            //        ORP._OrderPosRec.DocScanTableID = (decimal)dt.Rows[i]["ID"];
            //        ORP._OrderPosRec.AuftragsNr = (decimal)dt.Rows[i]["Auftrag"];
            //        ORP._OrderPosRec.AuftragPosNr = (decimal)dt.Rows[i]["AuftragPos"];
            //        ORP._OrderPosRec.OrderPosRecID = (decimal)dt.Rows[i]["ID"];
            //        ORP._OrderPosRec.PicNumber = (Int32)dt.Rows[i]["PicNum"];
            //        ORP._OrderPosRec.x_Pos = (Int32)dt.Rows[i]["x_Pos"];
            //        ORP._OrderPosRec.y_Pos = (Int32)dt.Rows[i]["y_Pos"];
            //        ORP._OrderPosRec.init = false;

            //        recPoint.X = (Int32)dt.Rows[i]["x_Pos"];
            //        recPoint.Y = (Int32)dt.Rows[i]["y_Pos"];

            //        ORP.Left = recPoint.X;
            //        ORP.Location = this.pictureBox1.FindForm().PointToClient(pictureBox1.Parent.PointToScreen(recPoint));

            //        this.pictureBox1.Controls.Add(ORP);
            //        this.pictureBox1.Controls.SetChildIndex(ORP, 0);
            //        ORP.Show();
            //        ORP.BringToFront();
            //        this.pictureBox1.Refresh();
            //    }
            //    this.Refresh();
            //}
        }

        ///<summary>frmAuftragView/ CleanCtrFromOrderPosRectangle</summary>
        ///<remarks></remarks>
        private void CleanCtrFromOrderPosRectangle()
        {
            //foreach (AFOrderPosRectangle ctr in this.pictureBox1.Controls.Find("OrderPosRectangle", true))
            //{
            //    this.pictureBox1.Controls.Remove(ctr);
            //}
        }
        ///<summary>frmAuftragView/ tsbImagePlus_Click</summary>
        ///<remarks></remarks>
        private void tsbImagePlus_Click(object sender, EventArgs e)
        {
            ////Image vergrößern
            //if (pictureBox1.Image != null)
            //{
            //    clsImages img = new clsImages();
            //    img.ImageIn = pictureBox1.Image;
            //    img.fImageSize = 1.1;
            //    img.ResizeImageByOneFactor();
            //    pictureBox1.Image = img.returnImage;
            //    pictureBox1.Refresh();
            //}
        }
        ///<summary>frmAuftragView/ tsbImageMinus_Click</summary>
        ///<remarks></remarks>
        private void tsbImageMinus_Click(object sender, EventArgs e)
        {
            //if (pictureBox1.Image != null)
            //{
            //    clsImages img = new clsImages();
            //    img.ImageIn = pictureBox1.Image;
            //    img.fImageSize = 0.9;
            //    img.ResizeImageByOneFactor();
            //    pictureBox1.Image = img.returnImage;
            //    pictureBox1.Refresh();
            //}
        }



        ///<summary>frmAuftragView/ miDocAusgabe_Click</summary>
        ///<remarks></remarks>
        private void miDocAusgabe_Click(object sender, EventArgs e)
        {
            DocAusgabe();
        }
        ///<summary>frmAuftragView/ DocAusgabe</summary>
        ///<remarks></remarks>
        private void DocAusgabe()
        {
            //if (pictureBox1.Image != null)
            //{
            //    DataSet ds = new DataSet();
            //    //Panel für ADR CTR öffnen
            //    if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmReportViewer)) != null)
            //    {
            //        Functions.frm_FormTypeClose(typeof(frmReportViewer));
            //    }

            //    frmReportViewer reportview = new frmReportViewer(ds, Globals.enumDokumentenart.AuftragScan.ToString());
            //    reportview.GL_User = GL_User;
            //    reportview._AuftragPosTableID = this.Tour.Auftrag.AuftragPos.ID;
            //    reportview._DocScanTableID = Docs.DocScanTableID;
            //    reportview._ArtikelTableID = -1; //für ImageDruck
            //    reportview.boFDocs = true;
            //    reportview.PicNum = Docs.PicNumber;
            //    reportview.StartPosition = FormStartPosition.CenterParent;
            //    reportview.Show();
            //    reportview.BringToFront();

            //}
        }
        ///<summary>frmAuftragView/ scanLöschenToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void scanLöschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScanDelete();
        }
        ///<summary>frmAuftragView/ ScanDelete</summary>
        ///<remarks></remarks>
        private void ScanDelete()
        {
            clsDocScan scan = new clsDocScan();
            scan.BenutzerID = GL_User.User_ID;
            scan.m_dec_DocScanID = Docs.DocScanTableID;
            scan.m_dec_AuftragID = Docs.AuftragID;
            scan.m_dec_LEingangID = Docs.LEingangID;
            scan.m_dec_LAusgangID = Docs.LAusgangID;
            scan.m_i_picnum = Docs.PicNumber;


            if (scan.m_dec_DocScanID > 0)
            {
                if (clsMessages.DeleteAllgemein())
                {
                    //scan.DeleteAuftragScanByDocScanID();
                    //InitDGV();
                    //pictureBox1.Image = null;
                    //DataGridViewCellEventArgs dgvcea = new DataGridViewCellEventArgs(0, 0);
                    //this.imgGrd_CellContentClick(this, dgvcea);
                    //this.pictureBox1.Refresh();
                }
            }
        }
        ///<summary>frmAuftragView/ tsbtnRotate_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRotate_Click(object sender, EventArgs e)
        {
            //if (pictureBox1.Image != null)
            //{
            //    clsDocScan scan = new clsDocScan();
            //    scan.bImage = bShowDoc;
            //    Bitmap tmpBMP = new Bitmap(scan.AuftragImageOut);
            //    tmpBMP.RotateFlip(RotateFlipType.Rotate90FlipNone);

            //    Image rotImage = (Image)tmpBMP;
            //    bRotateShowDoc = clsImages.ImageToByteArray(rotImage);
            //    SetRotateByteToTable();
            //    pictureBox1.Image = ResizeDocsImage(rotImage);
            //    pictureBox1.Refresh();
            //}
        }
        ///<summary>frmAuftragView/ SetRotateByteToTable</summary>
        ///<remarks></remarks>
        private void SetRotateByteToTable()
        {
            //for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            //{
            //    if ((decimal)dt.Rows[i]["ID"] == Docs.DocScanTableID)
            //    {
            //        dt.Rows[i]["DocImage"] = bRotateShowDoc;
            //        bShowDoc = bRotateShowDoc;
            //        imgGrd.Refresh();
            //    }
            //}
        }
        ///<summary>frmAuftragView / tsbtnUpdate_Click</summary>
        ///<remarks></remarks>
        ///<return>myIMaxPicNum</return>
        private void tsbtnUpdate_Click(object sender, EventArgs e)
        {
            //if (bRotateShowDoc != null)
            //{
            //    clsImages img = new clsImages();
            //    img.byteArrayIn = bRotateShowDoc;
            //    Image imgRotate = img.ConvertByteArrayToImage();

            //    clsDocScan myScan = new clsDocScan();
            //    myScan.AuftragImageIn = imgRotate;
            //    GetDataFromSelectedGrdRow(ref myScan);
            //    myScan.UpdateRotateDocImage();
            //    bRotateShowDoc = null;
            //    InitDGV();
            //}
        }
        ///<summary>frmAuftragView / GetDataFromSelectedGrdRow</summary>
        ///<remarks>Im Grid wird die selectierte Row ermittel und die Daten dieser Row werden
        ///         der Klassen-Eigenschaften zugewiesen.</remarks>
        ///<param name="myDocScan">Instanz der erzeugten Klasse wird als Ref. übergeben</param>
        private void GetDataFromSelectedGrdRow(ref clsDocScan myDocScan)
        {
            //Int32 iSelRow = this.imgGrd.CurrentRow.Index;
            //myDocScan.m_dec_DocScanID = (decimal)this.imgGrd.Rows[iSelRow].Cells["ID"].Value;
            //myDocScan.m_dec_AuftragID = (decimal)this.imgGrd.Rows[iSelRow].Cells["AuftragID"].Value;
            //myDocScan.m_dec_LEingangID = (decimal)this.imgGrd.Rows[iSelRow].Cells["LEingangID"].Value;
            //myDocScan.m_dec_LAusgangID = (decimal)this.imgGrd.Rows[iSelRow].Cells["LAusgangID"].Value;
            //myDocScan.m_str_Path = this.imgGrd.Rows[iSelRow].Cells["Pfad"].Value.ToString().Trim();
            //myDocScan.m_str_Filename = this.imgGrd.Rows[iSelRow].Cells["ScanFilename"].Value.ToString().Trim();
        }
        ///<summary>frmAuftragView/ tsbtnAusgabe_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAusgabe_Click(object sender, EventArgs e)
        {
            DocAusgabe();
        }
        ///<summary>frmAuftragView/ tsbtnScanDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnScanDelete_Click(object sender, EventArgs e)
        {
            ScanDelete();
        }
        ///<summary>frmAuftragView/ tsbtnArtikelDetails_Click</summary>
        ///<remarks></remarks>
        private void tsbtnArtikelDetails_Click(object sender, EventArgs e)
        {
            if (scMainPage.Panel1Collapsed == true)
            {
                scMainPage.Panel1Collapsed = false;
                tsbtnArtikelDetails.Image = Sped4.Properties.Resources.layout;
                this.Width = scMainPage.Panel2.Width + scMainPage.Panel1.Width;
            }
            else
            {
                scMainPage.Panel1Collapsed = true;
                tsbtnArtikelDetails.Image = Sped4.Properties.Resources.layout_left;
                this.Width = scMainPage.Panel2.Width;// -splitContainer1.Panel1.Width;
            }
            this.Size = new Size(this.Width, this.Height);
            if (ResizeAuftragView != null)
            {
                ResizeAuftragView();
            }
        }
        ///<summary>frmAuftragView/ SplitContainerCollaped</summary>
        ///<remarks></remarks>
        public void SplitContainerCollaped(bool boCollaped)
        {
            scMainPage.Panel1Collapsed = boCollaped;
        }
        ///<summary>frmAuftragView/ tsbtnScan_Click</summary>
        ///<remarks></remarks>
        private void tsbtnScan_Click(object sender, EventArgs e)
        {
            //Baustelle
            decimal myDecAuftragTableID = clsAuftrag.GetAuftragIDbyANr(GL_User, this.Tour.Auftrag.ID);
            if (this._ctrAuftrag != null)
            {
                this._ctrAuftrag._ctrMenu.OpenScanFrm(myDecAuftragTableID, this.Tour.Auftrag.AuftragPos.ID, 0, 0, this);
            }
            else if (this._ctrMenu != null)
            {
                this._ctrMenu.OpenScanFrm(myDecAuftragTableID, this.Tour.Auftrag.AuftragPos.ID, 0, 0, this);
            }
            InitDGV();
        }


        ///<summary>frmAuftragView/ imgGrd_CellEnter</summary>
        ///<remarks></remarks>
        private void imgGrd_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0)
            //{
            //    SetGL_DocValue(e.RowIndex);
            //    //Baustelle
            //    ToolTip info = new ToolTip();
            //    info.ToolTipIcon = ToolTipIcon.Info;
            //    info.ToolTipTitle = "Dokumenten Info";
            //    string strInfo = string.Empty;

            //    if (this.imgGrd.Rows.Count > 0)
            //    {
            //        strInfo = strInfo + "ID: " + Docs.AuftragTableID + " \n";
            //        strInfo = strInfo + "Auftrag: " + Docs.AuftragsNr + " \n";
            //        strInfo = strInfo + "Auftragposition: " + Docs.AuftragPosNr + " \n";
            //        strInfo = strInfo + "Dokument-Nr.: " + Docs.PicNumber + " \n";
            //        info.SetToolTip(imgGrd, strInfo);
            //    }
            //}
        }






    }
}
