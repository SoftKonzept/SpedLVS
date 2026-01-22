using Common.Enumerations;
using Common.Models;
using LVS;
using LVS.Constants;
using LVS.Helper;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class frmArchiveView : Telerik.WinControls.UI.RadForm
    {
        internal ctrMenu _ctrMenu;
        internal List<string> CbValueSource { get; set; } = new List<string>();
        internal ArchiveViewData archivViewData { get; set; }
        internal clsDocScan DocScan { get; set; }

        public frmArchiveView()
        {
            InitializeComponent();
        }

        public void InitFrm(ctrMenu myCtrMenu)
        {
            _ctrMenu = myCtrMenu;
            this.Text = "Dokumenten / Bilder - Archiv | [" + _ctrMenu._frmMain.system.AbBereich.ABName + "]";
            CbValueSource = new List<string>
            {
                ArchiveViewData.const_Datafield_NotSet,
                ArchiveViewData.const_Datafield_LvsID,
                ArchiveViewData.const_Datafield_EingangID,
                ArchiveViewData.const_Datafield_AusgangID,
                ArchiveViewData.const_Datafield_RGId
            };
            comboSearchDataField.DataSource = CbValueSource;
            comboSearchDataField.SelectedIndex = 0;

            archivViewData = new ArchiveViewData(this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.system);

            pdfViewer.EnableThumbnails = true;
            foreach (var item in imageEditor.ImageEditorElement.CommandsElement.CommandsStackElement.Children)
            {
                if (item is RadMenuItem)
                {
                    var menuItem = (RadMenuItem)item;
                    menuItem.Visibility = ElementVisibility.Hidden;
                }
                if (item is RadMenuHeaderItem)
                {
                    var menuHeaderItem = (RadMenuHeaderItem)item;
                    menuHeaderItem.Visibility = ElementVisibility.Hidden;
                }
            }
        }
        private void InitDgvs()
        {
            if ((nudSearchValue.Value > 0) && (comboSearchDataField.SelectedIndex > 0))
            {
                InitDgvDocuments();
                InitDgvImages();
                if (dgvDocuments.RowCount > 0)
                {
                    tabMain.SelectedTab = tabDocumentList;
                    dgvDocuments.Rows[0].IsSelected = true;
                    dgvDocumentsLoadRowValue(0);
                }
                else
                {
                    if (dgvImages.RowCount > 0)
                    {
                        tabMain.SelectedTab = tabImageList;
                        dgvImages.Rows[0].IsSelected = true;
                        dgvImagesLoadRowValue(0);
                    }
                }
            }
            else
            {
                string strError = string.Empty;
                strError += "Folgende Fehler sind aufgetreten:" + Environment.NewLine;
                if (nudSearchValue.Value < 1)
                {
                    strError += "- Es wurde kein Suchwert eingegeben!" + Environment.NewLine;
                }
                if (comboSearchDataField.SelectedIndex == 0)
                {
                    strError += "- Es wurde kein Such-Datenfeld ausgewählt!" + Environment.NewLine;
                }
                clsMessages.Allgemein_ERRORTextShow(strError);
            }
        }

        private void InitDgvDocuments()
        {
            if ((nudSearchValue.Value > 0) && (comboSearchDataField.SelectedIndex > 0))
            {
                archivViewData.GetList(comboSearchDataField.SelectedValue.ToString(), (int)nudSearchValue.Value);
                dgvDocuments.DataSource = archivViewData.ListSearchArchiveData;

                foreach (GridViewColumn col in this.dgvDocuments.Columns)
                {
                    GridViewDataColumn tmpDataCol;
                    switch (col.Name)
                    {
                        case "Archive":
                            col.IsVisible = false;
                            break;

                        case "col1":
                            col.ImageLayout = ImageLayout.Center;
                            col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                            break;

                        default:
                            col.ImageLayout = ImageLayout.None;
                            col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                            break;
                    }
                }

                dgvDocuments.BestFitColumns();
            }
            //else
            //{
            //    string strError =string.Empty;
            //    strError += "Folgende Fehler sind aufgetreten:" + Environment.NewLine;
            //    if (nudSearchValue.Value > 0)
            //    {
            //        strError += "- Es wurde kein Suchwert eingegeben!" + Environment.NewLine;
            //    }
            //    if (comboSearchDataField.SelectedIndex > 0)
            //    {
            //        strError += "- Es wurde kein Such-Datenfeld ausgewählt!" + Environment.NewLine;
            //    }
            //    clsMessages.Allgemein_ERRORTextShow(strError);
            //}
        }

        private void InitDgvImages()
        {
            if ((nudSearchValue.Value > 0) && (comboSearchDataField.SelectedIndex > 0))
            {
                //DocScan = new clsDocScan();
                //DocScan.InitClass(_ctrMenu._frmMain.GL_User, _ctrMenu._frmMain.GL_System, _ctrMenu._frmMain.system);
                dgvImages.DataSource = ImageViewData.GetSearchValueList(_ctrMenu._frmMain.GL_User, (int)_ctrMenu._frmMain.system.AbBereich.ID, (int)nudSearchValue.Value, comboSearchDataField.SelectedValue.ToString());


                foreach (GridViewColumn col in this.dgvImages.Columns)
                {
                    GridViewDataColumn tmpDataCol;
                    switch (col.Name)
                    {
                        case "Image":
                        case "DocImage":
                        case "Thumbnail":
                        case "IsForSPLMessage":
                        case "TableName":
                        case "TableId":
                        case "Created":
                        case "AusftragTableID":
                        case "LEingangTableID":
                        case "LAusgangTableID":
                        case "AuftragPosTableID":
                            col.IsVisible = false;
                            break;

                        case "PicNum":
                            col.HeaderText = "LfdNr";
                            break;

                        case "ScanFilename":
                            col.HeaderText = "Name";
                            break;

                        default:
                            col.ImageLayout = ImageLayout.None;
                            col.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                            break;
                    }
                }
                dgvImages.BestFitColumns();
            }
            //else
            //{
            //    string strError = string.Empty;
            //    strError += "Folgende Fehler sind aufgetreten:" + Environment.NewLine;
            //    if (nudSearchValue.Value > 0)
            //    {
            //        strError += "- Es wurde kein Suchwert eingegeben!" + Environment.NewLine;
            //    }
            //    if (comboSearchDataField.SelectedIndex > 0)
            //    {
            //        strError += "- Es wurde kein Such-Datenfeld ausgewählt!" + Environment.NewLine;
            //    }
            //    clsMessages.Allgemein_ERRORTextShow(strError);
            //}
        }

        private void comboSearchDataField_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = string.Empty;
            str = comboSearchDataField.SelectedValue.ToString();
        }

        private void miBtnSearch_Click(object sender, EventArgs e)
        {
            InitDgvs();
        }

        private void miBtnRefresh_Click(object sender, EventArgs e)
        {
            InitDgvs();
        }

        private void dgv_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (this.dgvDocuments.Rows[e.RowIndex] != null)
                {
                    try
                    {
                        dgvDocumentsLoadRowValue(e.RowIndex);
                        //if (this.dgvDocuments.Rows[e.RowIndex].Cells["Archive"].Value != null)
                        //{
                        //    Archives tmpArchive = this.dgvDocuments.Rows[e.RowIndex].Cells["Archive"].Value as Archives;
                        //    if((tmpArchive != null) && (tmpArchive.FileData!=null)) 
                        //    {
                        //        string PdfFileName = tmpArchive.Filename;
                        //        string PdfFilePath = System.IO.Path.Combine(clsReportDocSetting.const_localTempPDFReportPath, PdfFileName);
                        //        helper_Image.SaveByteArrayToFileWithStaticMethod(tmpArchive.FileData, PdfFilePath);
                        //        if (System.IO.File.Exists(PdfFilePath))
                        //        {
                        //            pdfViewer.LoadDocument(PdfFilePath);
                        //        }
                        //    }
                        //}                
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message;
                    }
                }
            }
        }

        private void dgvDocumentsLoadRowValue(int myRowIndex)
        {
            if (this.dgvDocuments.Rows[myRowIndex].Cells["Archive"].Value != null)
            {
                Archives tmpArchive = this.dgvDocuments.Rows[myRowIndex].Cells["Archive"].Value as Archives;
                if ((tmpArchive != null) && (tmpArchive.FileData != null))
                {
                    string PdfFileName = tmpArchive.Filename;
                    //string PdfFilePath = System.IO.Path.Combine(clsReportDocSetting.const_localTempPDFReportPath, PdfFileName);
                    string PdfFilePath = System.IO.Path.Combine(constValue_Report.const_localTempPDFReportPath, PdfFileName);
                    helper_Image.SaveByteArrayToFileWithStaticMethod(tmpArchive.FileData, PdfFilePath);
                    if (System.IO.File.Exists(PdfFilePath))
                    {
                        pdfViewer.LoadDocument(PdfFilePath);
                    }
                }
            }
        }

        private void dgv_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (this.dgvDocuments.Rows.Count > 0)
            {
                if (e.RowIndex > -1)
                {
                    string ColName = e.Column.Name.ToString();
                    if (ColName.Equals("col1"))   /* && (e.CellElement.Value != null))*/
                    {
                        try
                        {
                            Archives tmpArchiv = e.CellElement.RowInfo.Cells["Archive"].Value as Archives;

                            enumPrintDocumentArt tmpPrintDoc = enumPrintDocumentArt.NotSet;
                            Enum.TryParse(tmpArchiv.DocKey.ToString(), out tmpPrintDoc);
                            e.CellElement.Image = null;
                            switch (tmpPrintDoc)
                            {
                                case enumPrintDocumentArt.Lagerrechnung:
                                case enumPrintDocumentArt.RGAnhang:
                                case enumPrintDocumentArt.RGBuch:
                                case enumPrintDocumentArt.ManuelleGutschrift:
                                case enumPrintDocumentArt.Manuellerechnung:
                                case enumPrintDocumentArt.Speditionsrechnung:
                                    e.CellElement.Image = global::Sped4.Properties.Resources.invoice_euro_16x16;
                                    break;

                                case enumPrintDocumentArt.Eingangsliste:
                                case enumPrintDocumentArt.Einlagerungsanzeige:
                                case enumPrintDocumentArt.Einlagerungsschein:
                                case enumPrintDocumentArt.Auslagerungsschein:
                                case enumPrintDocumentArt.Auslagerungsanzeige:
                                case enumPrintDocumentArt.AusgangLfs:
                                case enumPrintDocumentArt.Ausgangsliste:
                                case enumPrintDocumentArt.KVOFrachtbrief:
                                case enumPrintDocumentArt.CMRFrachtbrief:
                                    e.CellElement.Image = global::Sped4.Properties.Resources.document_text_16x16;
                                    break;

                                case enumPrintDocumentArt.Bestand:
                                case enumPrintDocumentArt.Journal:
                                case enumPrintDocumentArt.Inventur:
                                case enumPrintDocumentArt.Adressliste:
                                    e.CellElement.Image = global::Sped4.Properties.Resources.document_notebook_16x16;
                                    break;

                                case enumPrintDocumentArt.SPLDoc:
                                case enumPrintDocumentArt.LabelAll:
                                case enumPrintDocumentArt.SchadenLabel:
                                case enumPrintDocumentArt.SchadenDoc:
                                    e.CellElement.Image = global::Sped4.Properties.Resources.document_orientation_portrait_16x16;
                                    break;
                            }

                            tabFilesAndImages.SelectedTab = tabSelectedDocument;

                        }
                        catch (Exception ex)
                        {
                            string str = ex.Message;
                        }
                    }
                }
            }
        }

        private void dgvImages_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (this.dgvImages.Rows[e.RowIndex] != null)
                {
                    try
                    {
                        dgvImagesLoadRowValue(e.RowIndex);
                        //if (this.dgvImages.Rows[e.RowIndex].Cells["Image"].Value != null)
                        //{
                        //    Images tmpImage = this.dgvImages.Rows[e.RowIndex].Cells["Image"].Value as Images;                                                      
                        //    if ((tmpImage != null) && (tmpImage.DocImage!=null)) 
                        //    {
                        //        imageEditor.CurrentBitmap = helper_Image.ByteArrayToBitmap(tmpImage.DocImage);
                        //        imageEditor.Refresh();

                        //        tabFilesAndImages.SelectedTab = tabSelectedImage;
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message;
                    }
                }
            }
        }

        private void dgvImagesLoadRowValue(int myRowIndex)
        {
            if (this.dgvImages.Rows[myRowIndex].Cells["Image"].Value != null)
            {
                Images tmpImage = this.dgvImages.Rows[myRowIndex].Cells["Image"].Value as Images;
                if ((tmpImage != null) && (tmpImage.DocImage != null))
                {
                    imageEditor.CurrentBitmap = helper_Image.ByteArrayToBitmap(tmpImage.DocImage);
                    imageEditor.Refresh();

                    tabFilesAndImages.SelectedTab = tabSelectedImage;
                }
            }
        }



        private void tabMain_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Equals(tabDocumentList))
            {
                tabFilesAndImages.SelectedTab = tabDocumentList;
                if (dgvDocuments.Rows.Count > 0)
                {
                    dgvDocuments.Rows[0].IsSelected = true;
                    dgvDocumentsLoadRowValue(0);
                }
            }
            else if (e.TabPage.Equals(tabImageList))
            {
                tabFilesAndImages.SelectedTab = tabImageList;
                if (dgvImages.Rows.Count > 0)
                {
                    dgvImages.Rows[0].IsSelected = true;
                    dgvImagesLoadRowValue(0);
                }
            }
        }

        private void miClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
