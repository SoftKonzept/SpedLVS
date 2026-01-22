using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Reporting;
using Telerik.Reporting.Drawing;

namespace Sped4
{
  class clsBriefkopf_Sped : Report
  {

    protected Telerik.Reporting.TextBox textBox1;
    protected Telerik.Reporting.PictureBox pictureBox1;
    private DetailSection detail;

    public BaseReport()
    {
        /// <summary>
        /// Required for telerik Reporting designer support
        /// </summary>
        InitializeComponent();
    }
  }
}
