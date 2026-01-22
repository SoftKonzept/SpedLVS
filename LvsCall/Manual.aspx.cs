using System;
using System.Collections.Generic;
using System.Linq;

public partial class Manual : System.Web.UI.Page
{
    string myPDFPath = string.Empty;
    string myPDFFile = "Handbuch_LVSCall.pdf";
    protected void Page_Load(object sender, EventArgs e)
    {
        myPDFPath = @"" + this.AppRelativeTemplateSourceDirectory + "/Hilfe/" + myPDFFile;
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Handbuch_LVSCall.pdf");
        Response.TransmitFile(myPDFPath);
        Response.End(); 
    }
}