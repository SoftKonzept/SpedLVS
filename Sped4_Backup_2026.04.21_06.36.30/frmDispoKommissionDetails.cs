using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sped4;
using Sped4.Classes;

namespace Sped4
{
    public partial class frmDispoKommissionDetails : Sped4.frmTEMPLATE
    {
        public clsKommission Kommission = new clsKommission();
        public frmDispoKommissionDetails(Int32 KommiID)
        {
            InitializeComponent();
            Kommission.ID = KommiID;
            Kommission.getData();
            initForm();
        }
        //
        //
        //
        private void initForm()
        {
            tbKontaktZeit.Text = DateTime.Now.ToString();

            tbANr.Text = Kommission.AuftragID.ToString();

            if (Kommission.document)
            {
                cbPapiereJa.Checked = true;
                cbPapiereNein.Checked = false;
            }
            else
            {
                cbPapiereJa.Checked = false;
                cbPapiereNein.Checked = true;
            }

            tbAlteInfos.Text = Kommission.KontaktInfo;
        
        }
        //
        //
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        private void button1_Click(object sender, EventArgs e)
        {
            AssignValue();
            initForm();
        }
        //
        //
        private void AssignValue()
        {
            // -- INFO
            string neueInfo = string.Empty;

            if(tbNeueInfo.Text=="")
            {
                Kommission.FahrerKontakt=false;
                neueInfo = tbNeueInfo.Text;
            }
            else
            {
                Kommission.FahrerKontakt=true;
                neueInfo =  tbNeueInfo.Text + "\r\n";
            }

            string alteInfo = tbAlteInfos.Text.ToString()+"\r\n";

            string InsertInfo =tbKontaktZeit.Text.ToString() + "\r\n" +neueInfo +  alteInfo;
            Kommission.KontaktInfo = InsertInfo;

            // -- Papiere
            if ((cbPapiereJa.Checked==true) & (cbPapiereNein.Checked==false))
            {
                Kommission.document = true;
            }
            if ((cbPapiereJa.Checked==false) & (cbPapiereNein.Checked==true))
            {
                Kommission.document = false;
            }

            Kommission.InsertFahrerInfo();                
        }

        private void cbPapiereJa_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPapiereJa.Checked==true)
            {
                cbPapiereNein.Checked = false;
            }
            else
            {
                cbPapiereNein.Checked = true;
            }
        }

        private void cbPapiereNein_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPapiereNein.Checked==true)
            {
                cbPapiereJa.Checked = false;
            }
            else
            {
                cbPapiereJa.Checked = true;
            }
        }
        
    }
}
