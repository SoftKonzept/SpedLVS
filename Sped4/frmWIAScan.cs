using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using WIA;
using WIATest;

namespace WIATest
{
    public partial class frmWIAScan : Form
    {
        public frmWIAScan()
        {
            InitializeComponent();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                string myStrSelectedItem = lbDevices.SelectedItem.ToString();
                if (myStrSelectedItem != string.Empty)
                {
                    List<Image> images = WIAScanner.Scan(myStrSelectedItem);
                    foreach (Image image in images)
                    {
                        image.Save(@"D:\" + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".jpeg", ImageFormat.Jpeg);
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            /***
            List<string> devices = WIAScanner.GetDevices();

            foreach (string device in devices)
            {
                lbDevices.Items.Add(device);
            }

            if (lbDevices.Items.Count == 0)
            {
                MessageBox.Show("You do not have any WIA devices.");
                this.Close();
            }
            else
            {
                lbDevices.SelectedIndex = 0;
            }
            ****/

            lbDevices.Items.Clear();
            //Create Manager instance
            DeviceManager deviceManager = new WIA.DeviceManager();

            // Loop through the list of devices and add the name to the listbox
            for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
            {
                var deviceName = deviceManager.DeviceInfos[i].Properties["Name"].get_Value().ToString();
                lbDevices.Items.Add(deviceName);                
            }
            lbDevices.SelectedIndex = 0;
        }
    }
}

