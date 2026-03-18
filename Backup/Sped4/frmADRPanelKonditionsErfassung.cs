using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sped4;
using Sped4.Classes;
using Sped4.Controls;

namespace Sped4
{

  public partial class frmADRPanelKonditionsErfassung : Sped4.frmTEMPLATE
  {
    ctrADR_List ctrADR = new ctrADR_List();
    public DataTable dtTable = new DataTable();

    //Tarifarten
    public bool TarifeHinterlegt = false;
    public bool GNT = false;
    public bool GNTalt=false;
    public bool GNT200=false;
    public bool GFT=false;
    public decimal KD_ID;

    public frmADRPanelKonditionsErfassung(ctrADR_List _ctrADR)
    {
      InitializeComponent();
      ctrADR = _ctrADR;
    }
    //
    //------ übergibt die ADR ID aus der Suche ------
    //
    public void SetSearchADR_ID(decimal ADR_ID)
    {
      string Name = string.Empty;
      string Str = string.Empty;
      string Ort = string.Empty;
      string KDNr = string.Empty;

      DataSet ds = clsADR.ReadADRbyID(ADR_ID);

      Name = ds.Tables[0].Rows[0]["Name1"].ToString();
      Str = ds.Tables[0].Rows[0]["Str"].ToString();
      Ort = ds.Tables[0].Rows[0]["PLZ"].ToString() + " - " + ds.Tables[0].Rows[0]["Ort"].ToString(); ;
      KDNr = ds.Tables[0].Rows[0]["KD_ID"].ToString();
      KD_ID = Convert.ToInt32(KDNr);
      tbKDADR.Text = Name + " \r\n" +
                     Str + "\r\n" +
                     Ort + "\r\n" +
                     "Kundennummer: " + KDNr + "\r\n";

    }
    //
    //-------- Abfrage = Sind schon Tarife hinterlegt --------
    //
    public void SetExistTarifKonditionen(ref frmADRPanelKonditionsErfassung Kondi)
    {
      clsFrachtKonditionen fk = new clsFrachtKonditionen();
      fk.KD_ID = KD_ID;
      fk.LoadTarife(ref Kondi );

      if (GNT)
      {
        cbGNT.Checked = true;
      }
      else
      {
        cbGNT.Checked = false;
      }
      if (GNTalt)
      {
        cbGNTalt.Checked = true;
      }
      else
      {
        cbGNTalt.Checked = false;
      }
      if (GNT200)
      {
        cbGNT200.Checked = true;
      }
      else
      {
        cbGNT200.Checked = false;
      }
      if (GFT)
      {
        cbGFT.Checked = true;
      }
      else
      {
        cbGFT.Checked = false;
      }
    }

    //-------------------- TARIFE ---------------------------
    //----GNT
    private void cbGNT_CheckedChanged(object sender, EventArgs e)
    {
      if (cbGNT.Checked == true)
      {
        GNT = true;
      }
      else
      {
        GNT = false;
      }
    }
    //--- GNTalt
    private void cbGNTalt_CheckedChanged(object sender, EventArgs e)
    {
      if (cbGNTalt.Checked == true)
      {
        GNTalt = true;
      }
      else
      {
        GNTalt = false;
      }
    }
    //GNT200
    private void cbGNT200_CheckedChanged(object sender, EventArgs e)
    {
      if (cbGNT200.Checked == true)
      {
        GNT200 = true;
      }
      else 
      {
        GNT200 = false;
      }
    }
    //GFT
    private void cbGFT_CheckedChanged(object sender, EventArgs e)
    {
      if (cbGFT.Checked == true)
      {
        GFT = true;
      }
      else
      {
        GFT = false;
      }
    }

    /*************************************************************************************************
     *                           hinzufügen kundenspezifischer Konditionen 
     *************************************************************************************************/
    //
    //
    private void OpenFrmKonditionen(Int32 Konditionsart)
    {
      if (Functions.IsFormAlreadyOpen(typeof(frmKDFrachtKonditionen)) != null)
      {
        Functions.FormClose(typeof(frmKDFrachtKonditionen));
      }
      frmKDFrachtKonditionen fk = new frmKDFrachtKonditionen(KD_ID, Konditionsart);
      fk.StartPosition = FormStartPosition.CenterScreen;
      fk.Show();
      fk.BringToFront();
    }
    //
    //
    private void btnGewicht_Click(object sender, EventArgs e)
    {
      // Konditionsart 1
      OpenFrmKonditionen(1);
    }
    //
    //
    private void btnEP_Click(object sender, EventArgs e)
    {
      //Konditionsart =2
      OpenFrmKonditionen(2);
    }
    //
    //
    private void btnPkm_Click(object sender, EventArgs e)
    {
      //Konditionsart 3
      OpenFrmKonditionen(3);
    }
      //
      //---------------- Konditionen speichern -------------
      //
    private void tsbSpeichern_Click(object sender, EventArgs e)
    {
        clsFrachtKonditionen fk = new clsFrachtKonditionen();
        string Tarif = string.Empty;
        fk.KD_ID = KD_ID;
        fk.GNT = GNT;
        fk.GNTalt = GNTalt;
        fk.GNT200 = GNT200;
        fk.GFT = GFT;

        if (fk.KD_Tarif())
        {
            fk.UpdateTarife();
        }
        else
        {
            fk.AddTarife();
        }
        clsMessages.Tarife_KonditionenSaved();
    }
    //
      //-------------- Form schlissen ----------------
      //
    private void tsbtnClose_Click(object sender, EventArgs e)
    {
        this.Close();
    }

  }
}
 


