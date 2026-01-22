namespace LVS.Dokumente
{
    using LVS;
    using System;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for docAuftragScan.
    /// </summary>
    public partial class docAuftragScan : Telerik.Reporting.Report
    {
        public clsDocScan DocScan;

        public Globals._GL_USER _GL_User;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }
        //************************************
        public decimal AuftragID;
        public decimal AuftragPos;
        public decimal AuftragTableID;
        public decimal AuftragPosTableID;
        public decimal ArtikelTableID;
        public decimal DocScanTableID;
        public string ImageArt;
        public Int32 PicNum;


        public docAuftragScan()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            this.pictureBox1.Dock = DockStyle.Fill;
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        //
        // 
        private void InitAuftragScan()
        {
            if (this.DocScan != null)
            {
                this.pictureBox1.Docking = DockingStyle.Fill;
                this.pictureBox1.Value = DocScan.AuftragImageOut;
                this.pictureBox1.Sizing = ImageSizeMode.ScaleProportional;
            }
        }
        private void docAuftragScan_NeedDataSource(object sender, EventArgs e)
        {
            InitAuftragScan();
        }

    }
}