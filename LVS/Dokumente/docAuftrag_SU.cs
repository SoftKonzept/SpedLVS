using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;
using System.Data;
using System.Drawing.Printing;


namespace Sped4
{
  public partial class docAuftrag_SU : docBriefkopf
  {
    public docBriefkopf docBK;

    public docAuftrag_SU()
    {
      InitializeComponent();
    }

/***
    public void init()
    {
      Telerik.Reporting.DetailSection details = new Telerik.Reporting.DetailSection();



      InitBeladestelle();
      InitEntladestelle();
      InitTransportauftragdaten();
      InitArtikeldaten();
    }
    //Beladestelle
    private void InitBeladestelle()
    {
      Telerik.Reporting.TextBox tbBName1 = new Telerik.Reporting.TextBox();
      Telerik.Reporting.TextBox tbBName2 = new Telerik.Reporting.TextBox();
      Telerik.Reporting.TextBox tbBStr = new Telerik.Reporting.TextBox();
      Telerik.Reporting.TextBox tbBPLZ = new Telerik.Reporting.TextBox();
      Telerik.Reporting.TextBox tbBOrt = new Telerik.Reporting.TextBox();

      for (int i = 0; i < ds.Tables["Versender"].Rows.Count; i++)
      {
       
        tbBName1.Value = ds.Tables["Versender"].Rows[i]["Name1"].ToString();
        tbBName2.Value = ds.Tables["Versender"].Rows[i]["Name2"].ToString();
        tbBStr.Value = ds.Tables["Versender"].Rows[i]["Str"].ToString();
        tbBPLZ.Value = ds.Tables["Versender"].Rows[i]["PLZ"].ToString();
        tbBOrt.Value = ds.Tables["Versender"].Rows[i]["Ort"].ToString();
      }
    }
    //Entladestelle
    private void InitEntladestelle()
    {

      Telerik.Reporting.TextBox tbEName1 = new Telerik.Reporting.TextBox();
      Telerik.Reporting.TextBox tbEName2 = new Telerik.Reporting.TextBox();
      Telerik.Reporting.TextBox tbEStr = new Telerik.Reporting.TextBox();
      Telerik.Reporting.TextBox tbEPLZ = new Telerik.Reporting.TextBox();
      Telerik.Reporting.TextBox tbEOrt = new Telerik.Reporting.TextBox();

      for (int i = 0; i < ds.Tables["Empfänger"].Rows.Count; i++)
      {
        tbEName1.Value = ds.Tables["Empfänger"].Rows[i]["Name1"].ToString();
        tbEName2.Value = ds.Tables["Empfänger"].Rows[i]["Name2"].ToString();
        tbEStr.Value = ds.Tables["Empfänger"].Rows[i]["Str"].ToString();
        tbEPLZ.Value = ds.Tables["Empfänger"].Rows[i]["PLZ"].ToString();
        tbEOrt.Value = ds.Tables["Empfänger"].Rows[i]["Ort"].ToString();
      }
    }
    //Transportauftrag
    private void InitTransportauftragdaten()
    {
      Telerik.Reporting.TextBox tbBDatum = new Telerik.Reporting.TextBox();
      Telerik.Reporting.TextBox tbBZF = new Telerik.Reporting.TextBox();
      Telerik.Reporting.TextBox tbEDatum = new Telerik.Reporting.TextBox();
      Telerik.Reporting.TextBox tbEZF= new Telerik.Reporting.TextBox();

      for (int i = 0; i < ds.Tables["Transportauftrag"].Rows.Count; i++)
      {
        tbBDatum.Value = ds.Tables["Transportauftrag"].Rows[i]["B_Date"].ToString();
        tbBZF.Value = ds.Tables["Transportauftrag"].Rows[i]["B_Time"].ToString();
        tbEDatum.Value = ds.Tables["Transportauftrag"].Rows[i]["E_Date"].ToString();
        tbEZF.Value = ds.Tables["Transportauftrag"].Rows[i]["E_Time"].ToString();
        //tbBOrt.Value = ds.Tables["Transportauftrag"].Rows[i]["Ort"].ToString();
        //Fracht
        //Info
      }
    }
    //Artikel
    private void InitArtikeldaten()
    {
      //dt.DataSource = ds.Tables["Artikel"];
    }
 * ***/
  }
}
