namespace Sped4
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;
  using Telerik.Reporting;
  using Telerik.Reporting.Drawing;
  using System.Data;
  using Sped4;


  /// <summary>
  /// Summary description for docTransportauftrag.
  /// </summary>
  public partial class docTransportauftrag : Sped4.docBriefkopf
  {
    public DataSet ds;

    public docTransportauftrag()
    {
      /// <summary>
      /// Required for telerik Reporting designer support
      /// </summary>
      InitializeComponent();
      //ds = _ds;
      init();
      //
      // TODO: Add any constructor code after InitializeComponent call
      //
    }
    //
    //----------- 
    //
    private void init()
    {
      InitBeladestelle();
      InitEntladestelle();
      InitTransportauftragdaten();
      InitArtikeldaten();
    }
    //Beladestelle
    private void InitBeladestelle()
    {
      for (int i = 0; i < ds.Tables["Versender"].Rows.Count; i++)
      {
        tbBFirma1.Value = ds.Tables["Versender"].Rows[i]["Name1"].ToString();
        tbBFirma2.Value = ds.Tables["Versender"].Rows[i]["Name2"].ToString();
        tbBStr.Value = ds.Tables["Versender"].Rows[i]["Str"].ToString();
        tbBPLZ.Value = ds.Tables["Versender"].Rows[i]["PLZ"].ToString();
        tbBOrt.Value = ds.Tables["Versender"].Rows[i]["Ort"].ToString();
      }
    }
    //Entladestelle
    private void InitEntladestelle()
    {
      for (int i = 0; i < ds.Tables["Empfänger"].Rows.Count; i++)
      {
        tbBFirma1.Value = ds.Tables["Empfänger"].Rows[i]["Name1"].ToString();
        tbBFirma2.Value = ds.Tables["Empfänger"].Rows[i]["Name2"].ToString();
        tbBStr.Value = ds.Tables["Empfänger"].Rows[i]["Str"].ToString();
        tbBPLZ.Value = ds.Tables["Empfänger"].Rows[i]["PLZ"].ToString();
        tbBOrt.Value = ds.Tables["Empfänger"].Rows[i]["Ort"].ToString();
      }
    }
    //Transportauftrag
    private void InitTransportauftragdaten()
    {
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
      dt.DataSource = ds.Tables["Artikel"];
    }



  }
}