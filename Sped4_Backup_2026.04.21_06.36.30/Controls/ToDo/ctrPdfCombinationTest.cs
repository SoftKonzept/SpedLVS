using LVS.ZUGFeRD;
using System.IO;
using System.Windows.Forms;


namespace Sped4.Controls.ToDo
{
    public partial class ctrPdfCombinationTest : UserControl
    {
        internal ctrMenu _ctrMenu { get; set; } = null;
        internal string eInvoicePath { get; set; } = string.Empty;
        internal string eInvoiceFileName { get; set; } = string.Empty;
        internal string einvoiceFilePath { get; set; } = string.Empty;
        internal string eInvoiceFilePath
        {
            get
            {
                einvoiceFilePath = Path.Combine(eInvoicePath, eInvoiceFileName);
                return einvoiceFilePath;
            }
            set
            {
                einvoiceFilePath = value;
                if (!einvoiceFilePath.Equals(string.Empty))
                {
                    eInvoiceFileName = Path.GetFileName(einvoiceFilePath);
                    eInvoicePath = Path.GetDirectoryName(einvoiceFilePath);
                }
            }
        }
        internal string AttachmentPath { get; set; } = string.Empty;
        internal string AttachmentFileName { get; set; } = string.Empty;
        internal string attachmentFilePath { get; set; } = string.Empty;
        internal string AttachmentFilePath
        {
            get
            {
                attachmentFilePath = Path.Combine(AttachmentPath, AttachmentFileName);
                return attachmentFilePath;
            }
            set
            {
                attachmentFilePath = value;
                if (!attachmentFilePath.Equals(string.Empty))
                {
                    AttachmentFileName = Path.GetFileName(attachmentFilePath);
                    AttachmentPath = Path.GetDirectoryName(attachmentFilePath);
                }
            }
        }

        internal string XmlPath { get; set; } = string.Empty;
        internal string XmlFileName { get; set; } = string.Empty;
        internal string xmltFilePath { get; set; } = string.Empty;
        internal string XmlFilePath
        {
            get
            {
                xmltFilePath = Path.Combine(XmlPath, XmlFileName);
                return xmltFilePath;
            }
            set
            {
                xmltFilePath = value;
                if (!xmltFilePath.Equals(string.Empty))
                {
                    XmlFileName = Path.GetFileName(xmltFilePath);
                    XmlPath = Path.GetDirectoryName(xmltFilePath);
                }
            }
        }
        public ctrPdfCombinationTest(ctrMenu myCtrMenu)
        {
            InitializeComponent();
            _ctrMenu = myCtrMenu;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///        
        private void ctrTest_Load(object sender, System.EventArgs e)
        {
            eInvoicePath = string.Empty;
            eInvoiceFileName = string.Empty;
            AttachmentFileName = string.Empty;
            AttachmentPath = string.Empty;
            eInvoiceFilePath = string.Empty;
            AttachmentFilePath = string.Empty;

        }
        private void btnPdfCombination_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(eInvoiceFilePath) || string.IsNullOrEmpty(AttachmentFilePath))
            {
                MessageBox.Show("Bitte beide Dateien auswählen.");
                return;
            }
            else
            {
                MergeEInvoicewithPDF merge = new MergeEInvoicewithPDF(eInvoiceFilePath, AttachmentFilePath, eInvoicePath);
            }
        }


        private void btnSearchEInvoiceFilePath_Click(object sender, System.EventArgs e)
        {
            eInvoiceFilePath = string.Empty;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Wähle eine PDF-Datei";
                dialog.Filter = "Alle Dateien (*.pdf)|*.pdf";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    eInvoiceFilePath = dialog.FileName;

                    //MessageBox.Show("Ausgewählte Datei:\n" + filePath);
                }
            }
            tbPathInvoice.Text = eInvoiceFilePath;
        }

        private void btnSearchAttachemntFileName_Click(object sender, System.EventArgs e)
        {
            AttachmentFilePath = string.Empty;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Wähle eine PDF-Datei";
                dialog.Filter = "Alle Dateien (*.pdf)|*.pdf";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    AttachmentFilePath = dialog.FileName;
                    //MessageBox.Show("Ausgewählte Datei:\n" + filePath);
                }
            }
            tbPathAttachment.Text = AttachmentFilePath;
        }

        private void btnXmlPathSearch_Click(object sender, System.EventArgs e)
        {
            XmlFilePath = string.Empty;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Wähle eine PDF-Datei";
                dialog.Filter = "Alle Dateien (*.xml)|*.xml";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    XmlFilePath = dialog.FileName;
                    //MessageBox.Show("Ausgewählte Datei:\n" + filePath);
                }
            }
            tbXmlPath.Text = XmlFilePath;
        }
    }
}
