namespace Sped4
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;
  using System.Data;
  using Telerik.Reporting;
  using Telerik.Reporting.Drawing;

  /// <summary>
  /// Summary description for docReport1.
  /// </summary>
  public partial class docReport1 : docBriefkopf
  {
    public DataSet ds = new DataSet();
    clsFrachtauftragSU FrachtauftragSU = new clsFrachtauftragSU();
    public docReport1()
    {
      /// <summary>
      /// Required for telerik Reporting designer support
      /// </summary>
      InitializeComponent();

      //
      // TODO: Add any constructor code after InitializeComponent call
      //
    }
    //
    //----------- Adressat / SU ------------------------------
    //
    private void InitADR()
    {
      if (ds.Tables["SU"].Rows.Count > 0)
      {
        //ds.Tables["SU"].Rows[1]["ID"].ToString();
        base.tbAnrede.Value = ds.Tables["SU"].Rows[0]["FBez"].ToString();
        base.tbADRFirma1.Value = ds.Tables["SU"].Rows[0]["Name1"].ToString();
        base.tbADRFirma2.Value = ds.Tables["SU"].Rows[0]["Name2"].ToString();
        //ds.Tables["SU"].Rows[1]["Name3"].ToString();
        base.tbADRStr.Value = ds.Tables["SU"].Rows[0]["Str"].ToString();
        base.tbADRPLZ.Value = ds.Tables["SU"].Rows[0]["PLZ"].ToString();
        base.tbADROrt.Value = ds.Tables["SU"].Rows[0]["Ort"].ToString();
      }
      else
      {
        base.tbAnrede.Value = string.Empty;
        base.tbADRFirma1.Value = string.Empty;
        base.tbADRFirma2.Value = string.Empty;
        //ds.Tables["SU"].Rows[1]["Name3"].ToString();
        base.tbADRStr.Value = string.Empty;
        base.tbADRPLZ.Value = string.Empty;
        base.tbADROrt.Value = string.Empty;
      }
    
    }
    public void InitDetail(DataSet _ds)
    {
      ds = _ds;
      InitADR();
      InitBeladestelle();
      InitEntladestelle();
      this.detail = FrachtauftragSU.detail_Frachtauftrag;
    }
    //
    //----------- init Beladestelle  -------------------
    //
    private void InitBeladestelle()
    {
      if (ds.Tables["Versender"].Rows.Count > 0)
      {
        FrachtauftragSU.tbVName1.Value=ds.Tables["Versender"].Rows[0]["Name1"].ToString();
        FrachtauftragSU.tbVName2.Value = ds.Tables["Versender"].Rows[0]["Name2"].ToString();
        FrachtauftragSU.tbVName3.Value = ds.Tables["Versender"].Rows[0]["Name3"].ToString();
        FrachtauftragSU.tbVStr.Value = ds.Tables["Versender"].Rows[0]["Str"].ToString(); 
        FrachtauftragSU.tbVPLZOrt.Value = ds.Tables["Versender"].Rows[0]["PLZ"].ToString()+ " - " + ds.Tables["Versender"].Rows[0]["Ort"].ToString(); 
      }
      else
      {
        FrachtauftragSU.tbVName1.Value = string.Empty;
        FrachtauftragSU.tbVName2.Value = string.Empty;
        FrachtauftragSU.tbVName3.Value = string.Empty;
        FrachtauftragSU.tbVStr.Value = string.Empty;
        FrachtauftragSU.tbVPLZOrt.Value = string.Empty;
      }   
    }
    //
    //----------- init Empfänger -------------------
    //
    private void InitEntladestelle()
    {
      if (ds.Tables["Empfänger"].Rows.Count > 0)
      {
        FrachtauftragSU.tbEName1.Value = ds.Tables["Empfänger"].Rows[0]["Name1"].ToString();
        FrachtauftragSU.tbEName2.Value = ds.Tables["Empfänger"].Rows[0]["Name2"].ToString();
        FrachtauftragSU.tbEName3.Value = ds.Tables["Empfänger"].Rows[0]["Name3"].ToString();
        FrachtauftragSU.tbEStr.Value = ds.Tables["Empfänger"].Rows[0]["Str"].ToString();
        FrachtauftragSU.tbEPLZOrt.Value = ds.Tables["Empfänger"].Rows[0]["PLZ"].ToString() + " - " + ds.Tables["Versender"].Rows[0]["Ort"].ToString();
      }
      else
      {
        FrachtauftragSU.tbEName1.Value = string.Empty;
        FrachtauftragSU.tbEName2.Value = string.Empty;
        FrachtauftragSU.tbEName3.Value = string.Empty;
        FrachtauftragSU.tbEStr.Value = string.Empty;
        FrachtauftragSU.tbEPLZOrt.Value = string.Empty;
      }
    
    }
  }
}