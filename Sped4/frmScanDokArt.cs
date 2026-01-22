using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sped4.Classes;
using TwainLib;
//using TwainGui;
using Sped4;

namespace Sped4
{
  public partial class frmScanDokArt : Sped4.frmTEMPLATE
  {
    public string DokuArt = string.Empty;
    public frmTwain mf;
    
    public frmScanDokArt(frmTwain _mf)
    {
      InitializeComponent();
      mf = _mf;
    }

    private void InitForm()
    { 
      // Text in der Textbox
      //string strAusgabe = "Das Dokument wird eingescannt.\n" +
      //                    "Bitte wählen Sie noch die Art des eingescanntens Dokuments aus:";
    }
/*
    private void btn1_Click(object sender, EventArgs e)
    {
      string DokuArt = cbDokuArt.Text;
      mf.ImageArt = DokuArt;
      mf.menuItemScan_Click(mf, e);
      this.Close();
    }
***/
    private void cbDokuArt_SelectedValueChanged(object sender, EventArgs e)
    {
      string DokuArt = cbDokuArt.Text;
      btn1.Visible = true;
    }

    private void frmScanDokArt_Load(object sender, EventArgs e)
    {
      cbDokuArt.DataSource = Enum.GetNames(typeof(Globals.enumDokumentenart));
      cbDokuArt.SelectedIndex = -1;
      // Text in der Textbox
      string strAusgabe ="Bitte wählen Sie die Art des eingescanntens Dokuments aus:";

      tbInfo.Text = strAusgabe;
      btn1.Visible = false;
      btn1.Text = "weiter";
    }

  }
}
