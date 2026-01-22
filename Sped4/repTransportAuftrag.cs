namespace Sped4
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for repLieferschein.
    /// </summary>
    public partial class repTransportAuftrag : Telerik.Reporting.Report
    {
        public Int32 AuftragID;
        public Int32 AuftragPos;

        public repTransportAuftrag(Int32 _Auftrag, Int32 _AuftragPos)
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();
            AuftragID = _Auftrag;
            AuftragPos = _AuftragPos;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        //
        //
        //
        private void InitAuftrag()
        { 
            
        
        }





    }
}