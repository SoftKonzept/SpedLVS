using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sped4;
using System.Data.SqlClient;

namespace Sped4
{
    public partial class frmADRupdate : Form
    {
        public frmADRupdate()
        {
            InitializeComponent();
        }
        private void frmADRupdate_Load(object sender, EventArgs e)
        {
            try
            {
                //--- Initialisierung der Connection ------------------
                DataTable ADRTable = new DataTable();
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();

                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;

                Command.CommandText = "SELECT " +
                                                "ViewID as 'Suchbegriff', " +
                                               // "KD_ID as 'KDNr', " +       // wir vorerst raus genommen
                                                "FBez, " +
                                                "Name1, " +
                                                "Name2, " +
                                                "Name3, " +
                                                "Str as 'Strasse', " +
                                                "PF as 'Postfach', " +
                                                "PLZ, " +
                                                "PLZPF, " +
                                                "Ort, " +
                                                "OrtPF, " +
                                                "Land " +
                                                             "FROM ADR";


                ada.Fill(ADRTable); 
                ada.Dispose();

                Command.Dispose();

                mccb1.DataSource = ADRTable;
                mccb1.DisplayMember = "Suchbegriff";
                mccb1.ValueMember = "Name1";
                
                mccb1.SelectedIndex = -1;

                upADRFormClean();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //-------------------------------------- Eingabeformular wird neutral gesetzt ------------
        private void upADRFormClean()
        {
            tbUpSuchname.Text = "";
            tbUpFBez.Text = "";
            tbUpName1.Text = "";
            tbUpName2.Text = "";
            tbUpName3.Text = "";
            tbUpStr.Text = "";
            tbUpPLZ.Text = "";
            tbUpOrt.Text = "";
            tbUpPLZPF.Text = "";
            tbUpOrtPF.Text = "";
            tbUpLand.Text = "";
        }

        //-------------------------------------- Suchcombobox wird geladen ------------------------
        private void mccb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mccb1.SelectedIndex >= 0)
            {
                DataRowView drv = (DataRowView)mccb1.SelectedItem;

                tbUpSuchname.Text   = drv.Row.ItemArray[0].ToString();
                tbUpFBez.Text       = drv.Row.ItemArray[1].ToString();
                tbUpName1.Text      = drv.Row.ItemArray[2].ToString();
                tbUpName2.Text      = drv.Row.ItemArray[3].ToString();
                tbUpName3.Text      = drv.Row.ItemArray[4].ToString();
                tbUpStr.Text        = drv.Row.ItemArray[5].ToString();
                tbUpPLZ.Text        = drv.Row.ItemArray[6].ToString();
                tbUpOrt.Text        = drv.Row.ItemArray[7].ToString();
                tbUpPLZPF.Text      = drv.Row.ItemArray[8].ToString();
                tbUpPLZPF.Text      = drv.Row.ItemArray[9].ToString();
                tbUpOrtPF.Text      = drv.Row.ItemArray[10].ToString();
                tbUpLand.Text       = drv.Row.ItemArray[11].ToString();

 
            }



        }


                
        
        
        
        
        }
}
