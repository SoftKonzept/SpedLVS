using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sped4;
using Sped4.Classes;

namespace Sped4
{
    public partial class frmSchaden : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        internal bool _bUpdate;
        internal DataTable _dtSchaden;
        internal decimal _decSchadenID = 0;


        public frmSchaden()
        {
            InitializeComponent();
        }
        ///<summary>frmSchaden / frmSchaden_Load</summary>
        ///<remarks></remarks>
        private void frmSchaden_Load(object sender, EventArgs e)
        {
            tsbtnSchadenNew.Enabled = true;
            tsbtnSchadenSave.Enabled = false;
            tsbtnSchadenDelete.Enabled = false;

            FillGrd();
        }


        private void FillGrd()
        {
            _dtSchaden = new DataTable();
            _dtSchaden = clsSchaeden.GetSchaeden(GL_User);
            this.grd.DataSource = _dtSchaden;

            //Automatische Spaltenbreite einstellen
            for (Int32 i = 0; i <= this.grd.Columns.Count - 1; i++)
            {
                if (this.grd.Columns[i].Name == "Beschreibung")
                {
                    this.grd.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    this.grd.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
            }
        }
             

        ///<summary>frmSchaden / tsbtnSchadenNew_Click</summary>
        ///<remarks>Neuen Schaden hinzufügen.</remarks>
        private void tsbtnSchadenNew_Click(object sender, EventArgs e)
        {
            tsbtnSchadenSave.Enabled = true;
            tsbtnSchadenDelete.Enabled = false;
            ClearSchadenInputFields();
            _bUpdate = false;
        }
        ///<summary>frmSchaden / tsbtnSchadenNew_Click</summary>
        ///<remarks>Neuen Schaden hinzufügen.</remarks>
        private void ClearSchadenInputFields()
        {
            tbBeschreibung.Text = string.Empty;
            tbSchadensbezeichnung.Text = string.Empty;
            cbSchadenAktiv.Checked = true;
        }
        ///<summary>frmSchaden / tsbtnSchadenSave_Click</summary>
        ///<remarks>Speichern der eingebenen Schadensdaten</remarks>
        private void tsbtnSchadenSave_Click(object sender, EventArgs e)
        {
            if (CheckInputData())
            {
                clsSchaeden myDemage = new clsSchaeden();
                myDemage._GL_User = GL_User;
                myDemage.ID = _decSchadenID;
                myDemage.Bezeichnung = tbSchadensbezeichnung.Text.ToString().Trim();
                myDemage.Beschreibung = tbBeschreibung.Text.ToString().Trim();
                myDemage.aktiv = cbSchadenAktiv.Checked;

                if (_bUpdate)
                {
                    myDemage.UpdateSchaden();
                }
                else
                {
                    myDemage.AddSchaden();
                }

                ClearSchadenInputFields();
                tsbtnSchadenNew.Enabled = true;
                tsbtnSchadenSave.Enabled = false;
                tsbtnSchadenDelete.Enabled = false;
                FillGrd();
            }
        }
        ///<summary>frmSchaden / tsbtnSchadenSave_Click</summary>
        ///<remarks>Speichern der eingebenen Schadensdaten</remarks>
        private bool CheckInputData()
        {
            string myErrMes = String.Empty;
            //Tarifname
            if (tbSchadensbezeichnung.Text == string.Empty)
            {
                if (myErrMes != string.Empty)
                {
                    myErrMes = myErrMes + Environment.NewLine;
                }
                myErrMes = myErrMes + "Das Feld Bezeichnung ist leer!";
            }
            if((!_bUpdate) && (clsSchaeden.ExistSchaden(GL_User, tbSchadensbezeichnung.Text.ToString().Trim())))
            {
                if (myErrMes != string.Empty)
                {
                    myErrMes = myErrMes + Environment.NewLine;
                }
                myErrMes = myErrMes + "Der Schaden mit der Bezeichnung existiert bereits!";   
            }

            if (myErrMes != string.Empty)
            {
                clsMessages.Allgemein_EingabeDatenFehlerhaft(myErrMes);
                return false;
            }
            else
            {
                return true;
            }       
        }
        ///<summary>frmSchaden / tsbtnClose_Click</summary>
        ///<remarks>Form Schaeden wird geschlossen.</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ///<summary>frmSchaden / grd_CellClick</summary>
        ///<remarks>Eine Zeile im Grid wird selectiert und die ID in der Variable hinterlegt.</remarks>
        private void grd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.grd.CurrentRow != null)
            {
                _bUpdate = true;
                tsbtnSchadenSave.Enabled = true;
                tsbtnSchadenDelete.Enabled = true;

                //Ermittelt die TarifId aus dem grdTarif 
                string strTmp = this.grd.Rows[this.grd.CurrentRow.Index].Cells["ID"].Value.ToString();
                if (!decimal.TryParse(strTmp, out _decSchadenID))
                {
                    _decSchadenID = 0;
                }
                else
                {
                    SetGrdValueToFrm();
                }
            }
        }
        ///<summary>frmSchaden / grd_CellClick</summary>
        ///<remarks>Eine Zeile im Grid wird selectiert und die ID in der Variable hinterlegt.</remarks>
        private void SetGrdValueToFrm()
        {
            if (_decSchadenID > 0)
            {
                tbSchadensbezeichnung.Text = this.grd.Rows[this.grd.CurrentRow.Index].Cells["Bezeichnung"].Value.ToString();
                tbBeschreibung.Text = this.grd.Rows[this.grd.CurrentRow.Index].Cells["Beschreibung"].Value.ToString();
                cbSchadenAktiv.Checked = Convert.ToBoolean(this.grd.Rows[this.grd.CurrentRow.Index].Cells["aktiv"].Value.ToString());
            }
        }


    }      
}
