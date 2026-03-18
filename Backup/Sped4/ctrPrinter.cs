using LVS;
using LVS.Constants;
using LVS.DataObjects;
using Sped4.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;




namespace Sped4
{
    public partial class ctrPrinter : UserControl
    {
        clsINI.clsINI ini = new clsINI.clsINI();
        public bool IsUsedByAdminCockpit = false;
        ctrMenu _ctrMenu;
        List<Dokument> dokumente = new List<Dokument>();
        private RadDropDownListEditor comboBoxEditor;

        public constValue_PrinterIni constPrinterIni;


        /******************************************************************************
        *                       Procedure / Methoden
        ******************************************************************************/
        ///<summary>ctrPrinter / ctrPrinter</summary>
        ///<remarks>.</remarks>
        public ctrPrinter(ctrMenu myMenu)
        {
            InitializeComponent();
            this._ctrMenu = myMenu;
            constPrinterIni = new constValue_PrinterIni((int)this._ctrMenu._frmMain.GL_User.User_ID);
        }
        ///<summary>ctrPrinter / UserControl1_Load</summary>
        ///<remarks>.</remarks>
        private void UserControl1_Load(object sender, EventArgs e)
        {
            this.tsbtnClose.Visible = !(this.IsUsedByAdminCockpit);

            GridViewTextBoxColumn textBoxColumn = new GridViewTextBoxColumn();
            textBoxColumn.FieldName = "Bezeichner";
            textBoxColumn.Name = "Bezeichner";
            textBoxColumn.HeaderText = "Bezeichner";
            textBoxColumn.ReadOnly = true;
            this.dgv.Columns.Add(textBoxColumn);

            GridViewComboBoxColumn comboboxColumn = new GridViewComboBoxColumn();
            comboboxColumn.FieldName = "Drucker1";
            comboboxColumn.Name = "Drucker1";
            comboboxColumn.HeaderText = "Drucker";

            //PrinterSettings ps = new PrinterSettings();
            //selectedPrinter = new PrinterSettings().PrinterName;
            string defaultPrinter = "";

            comboboxColumn.DataSource = PrinterSettings.InstalledPrinters;
            comboboxColumn.IsVisible = true;
            comboboxColumn.ReadOnly = false;
            dgv.Columns.Add(comboboxColumn);

            comboboxColumn = new GridViewComboBoxColumn();
            comboboxColumn.FieldName = "Fach";
            comboboxColumn.Name = "Fach";
            comboboxColumn.HeaderText = "Fach";

            comboboxColumn.IsVisible = true;

            dgv.Columns.Add(comboboxColumn);

            if (File.Exists(constPrinterIni.IniFilePaht))
            {
                LoadGrid(constPrinterIni.IniFilePaht);
            }
            else
            {
                string strMessage = "Es konnte keine Drucker - Eintstellungsdatei gefunden werden. Möchten Sie nun eine printer.ini Datei importieren?";
                DialogResult result = MessageBox.Show(strMessage, "ACHTUNG", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    ImportIniPrinter();

                    //OpenFileDialog openFileDialog = new OpenFileDialog();
                    //openFileDialog.Title = "Open File";
                    //openFileDialog.Filter = "Text Files|*.ini";

                    //if (openFileDialog.ShowDialog() == DialogResult.OK)
                    //{
                    //    string selectedFilePath = openFileDialog.FileName;
                    //    LoadGrid(selectedFilePath);
                    //    SaveIniPrinter();    
                    //}
                }
                else
                {
                    LoadGrid(constPrinterIni.IniFilePaht);
                }
            }
        }

        private void ImportIniPrinter()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = constValue_PrinterIni.const_localConfigPath;
            openFileDialog.Title = "Open File";
            openFileDialog.Filter = "Text Files|*.ini";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                LoadGrid(selectedFilePath);
                SaveIniPrinter();
            }
        }
        private void LoadGrid(string myFilePath)
        {
            List<String> Docs = new List<string>();
            if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                Docs = _ctrMenu._frmMain.GL_System.docPath;
            }
            else
            {
                Docs = clsReportDocSetting.GetDocKey(this._ctrMenu._frmMain.GL_User);
            }
            Docs.Sort();

            PrinterSettings ps = new PrinterSettings();
            if (File.Exists(myFilePath))
            {
                clsINI.clsINI ini = new clsINI.clsINI(myFilePath);
                foreach (string doc in Docs)
                {
                    if (ini.SectionNames().Contains("Druckereinstellungen"))
                    {
                        if (ini.ReadString("Druckereinstellungen", doc + "_Drucker", "") != null)
                        {
                            string drucker = ini.ReadString("Druckereinstellungen", doc + "_Drucker", "");
                            if (drucker.Equals(string.Empty))
                            {
                                drucker = ps.PrinterName;
                            }
                            string fach = ini.ReadString("Druckereinstellungen", doc + "_Fach", "");

                            dokumente.Add(new Dokument() { Bezeichner = doc, drucker1 = drucker, Fach = fach });
                        }
                    }
                }
            }
            else
            {
                foreach (string doc in Docs)
                {
                    string drucker = ps.PrinterName; // ini.ReadString("Druckereinstellungen", doc + "_Drucker", "");
                    string fach = string.Empty;
                    dokumente.Add(new Dokument() { Bezeichner = doc, drucker1 = drucker, Fach = fach });
                }
            }
            dgv.DataSource = dokumente;
        }
        ///<summary>ctrPrinter / radGridView1_CellFormatting</summary>
        ///<remarks>.</remarks>
        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.RowInfo is GridViewFilteringRowInfo)
            {
                return;
            }
            if (e.CellElement.ColumnInfo.Name == "ProgressBar")
            {
                RadProgressBarElement progressBarElement;
                if (e.CellElement.Children.Count == 0)
                {
                    progressBarElement = new RadProgressBarElement();
                    e.CellElement.Children.Add(progressBarElement);
                    progressBarElement.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                }
                else
                {
                    progressBarElement = e.CellElement.Children[0] as RadProgressBarElement;
                }
                progressBarElement.Margin = new Padding(15);
                progressBarElement.StretchHorizontally = true;
                int value = 0;

                if (e.CellElement.Value != null)
                {
                    try
                    {
                        Int32.TryParse(((GridDataCellElement)e.CellElement).Value.ToString(), out value);
                    }
                    catch
                    {
                        value = 0;
                    }
                }
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > 100)
                {
                    value = 100;
                }
                progressBarElement.Value1 = value;
            }
            else if (e.CellElement.ColumnInfo.Name == "Rating")
            {
                e.CellElement.Padding = new Padding(0, 15, 0, 15);
            }
            else
            {
                if (
                        e.CellElement.ColumnInfo.Name != "CheckBox" &&
                        e.CellElement.ColumnInfo.Name != "Hyperlink" &&
                        e.CellElement.ColumnInfo.Name != "Color" &&
                        !(e.CellElement.IsCurrent && this.dgv.IsInEditMode)
                    )
                {
                    e.CellElement.Children.Clear();
                }
                if (e.CellElement.ColumnInfo.Name == "MaskBox")
                {
                    long result;
                    if (e.CellElement.Text.Contains("(") || !long.TryParse(e.CellElement.Text, out result))
                    {
                        return;
                    }
                    e.CellElement.Text = String.Format("{0:(000) 000-0000}", result);
                }
                if (e.CellElement.ColumnInfo.Name == "Color")
                {
                    GridColorCellElement cell = e.CellElement as GridColorCellElement;
                    cell.ColorBox.StretchVertically = false;
                    cell.ColorBox.Alignment = ContentAlignment.MiddleCenter;
                    cell.ColorBox.MinSize = new Size(20, 20);
                }
            }
        }
        ///<summary>ctrPrinter / ValueChanged</summary>
        ///<remarks>.</remarks>
        private void ValueChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(((ComboBoxCellElement)sender).Value);
        }
        ///<summary>ctrPrinter / radGridView1_CellBeginEdit</summary>
        ///<remarks>.</remarks>
        private void radGridView1_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            comboBoxEditor = this.dgv.ActiveEditor as RadDropDownListEditor;
            if (comboBoxEditor != null)
            {
                comboBoxEditor.EditorElement.StretchVertically = false;
                comboBoxEditor.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
                comboBoxEditor.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
            }

            //GridViewComboBoxColumn col = dgv.Columns[2];
            Console.WriteLine(dgv.Rows[e.RowIndex].Cells[1].Value);
            //if (sender == typeof(GridViewComboBoxColumn))
            //{
            //    //((GridViewComboBoxColumn)dgv.Rows[e.RowIndex].Cells[2]).DataSource = ps.PaperSources;
            //}
        }
        ///<summary>ctrPrinter / dgv_CellEndEdit</summary>
        ///<remarks>.</remarks>
        private void dgv_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (e.Value != null)
                {

                    dokumente.ElementAt(e.RowIndex).drucker1 = e.Value.ToString();
                    dgv.Rows[e.RowIndex].Cells[2].BeginEdit();

                }

            }
            if (e.ColumnIndex == 2)
            {
                if (e.Value != null)
                {
                    dokumente.ElementAt(e.RowIndex).Fach = e.Value.ToString();
                }
            }
        }
        ///<summary>ctrPrinter / dgv_CellEditorInitialized</summary>
        ///<remarks>.</remarks>
        private void dgv_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                RadDropDownListEditor editor = (RadDropDownListEditor)this.dgv.ActiveEditor;

                RadDropDownListEditorElement editorElement = (RadDropDownListEditorElement)editor.EditorElement;
                PrinterSettings ps = new PrinterSettings();
                ps.PrinterName = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();

                List<string> paperSoruce = new List<string>();
                foreach (PaperSource myPS in ps.PaperSources)
                    paperSoruce.Add(myPS.SourceName);
                editorElement.DataSource = paperSoruce;
            }
        }
        ///<summary>ctrPrinter / tsbtnSave_Click</summary>
        ///<remarks>.</remarks>
        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            string Mes = string.Empty;
            try
            {
                SaveIniPrinter();
                Mes = "Die Einstellungen wurde gespeichert!";
            }
            catch (Exception ex)
            {
                Mes = ex.ToString();
            }
            clsMessages.Allgemein_InfoTextShow(Mes);
        }

        public void SaveIniPrinter()
        {
            if (!File.Exists(constPrinterIni.IniFilePaht))
            {
                //File.Create(IniFilePaht);
                List<string> listWrite = new List<string>();
                listWrite.Add("[Druckereinstellungen]");
                helper_IOFile.WriteFileInLine(constPrinterIni.IniFilePaht, listWrite);

                clsINI.clsINI ini = new clsINI.clsINI(constPrinterIni.IniFilePaht);
                ini.DeleteSection("Druckereinstellungen");
                foreach (Dokument doc in dokumente)
                {
                    ini.WriteString("Druckereinstellungen", doc.Bezeichner + "_Drucker", doc.drucker1);
                    ini.WriteString("Druckereinstellungen", doc.Bezeichner + "_Fach", doc.Fach);
                }
            }
            else
            {
                Directory.CreateDirectory(constValue_PrinterIni.const_localConfigPath);
                clsINI.clsINI ini = new clsINI.clsINI(constPrinterIni.IniFilePaht);
                ini.DeleteSection("Druckereinstellungen");
                foreach (Dokument doc in dokumente)
                {
                    ini.WriteString("Druckereinstellungen", doc.Bezeichner + "_Drucker", doc.drucker1);
                    ini.WriteString("Druckereinstellungen", doc.Bezeichner + "_Fach", doc.Fach);
                }
            }
        }
        ///<summary>ctrPrinter / tsbtnClose_Click</summary>
        ///<remarks>.</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnImportSetting_Click(object sender, EventArgs e)
        {
            ImportIniPrinter();
        }
    }

}
