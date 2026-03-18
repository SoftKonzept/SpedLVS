using Sped4.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrFormList : UserControl
    {
        internal List<Control> listCtr;
        internal string ActivCtrName = string.Empty;
        internal string ctrText = string.Empty;
        internal AFButton_Form btnCtrActivTmp;  //sicherung der Einstellungen activ
        internal AFButton_Form btnCtrPassTmp;   // Sicherung der Einstellungen passiv
        internal AFButton_Form btnControlMuster;
        internal frmMAIN _frmMain;

        internal delegate void ControlBtnClickEventHandler(object sender, EventArgs e);


        //delegate 

        ///<summary>ctrFormList</summary>
        ///<remarks></remarks> 
        public ctrFormList()
        {
            InitializeComponent();
            this.btnCtrActivTmp = this.btnCtrActiv;
            this.btnCtrPassTmp = this.btnCtrPassiv;


            //this.btnCtrActiv.Visible = false;
            //this.btnCtr.Visible = false;
            this.Controls.Clear();
        }
        ///<summary>ctrFormList / AddCtrToList</summary>
        ///<remarks></remarks>
        public void AddCtrToList(ref frmMAIN myMain)
        {
            this._frmMain = myMain;
            DoWork();
        }
        ///<summary>ctrFormList / DoWork</summary>
        ///<remarks></remarks> 
        private void DoWork()
        {
            ClearCtrList();
            listCtr = new List<Control>();

            for (Int32 i = 0; i <= this._frmMain.Controls.Count - 1; i++)
            {
                if (CanAddCtrToCtrFrmList((object)this._frmMain.Controls[i]))
                {
                    if (i == 0)
                    {
                        listCtr.Add(CreateAFButtonForm(true, this._frmMain.Controls[i]));
                    }
                    else
                    {
                        listCtr.Add(CreateAFButtonForm(false, this._frmMain.Controls[i]));
                    }
                }
            }
            FillFormList();
        }
        ///<summary>ctrFormList / CreateAFButtonForm</summary>
        ///<remarks></remarks> 
        private Control CreateAFButtonForm(bool bIsActiv, Control myCtr)
        {
            AFButton_Form btnControl = new AFButton_Form();
            if (bIsActiv)
            {
                btnControl = this.btnCtrActivTmp;
            }
            else
            {
                btnControl = this.btnCtrPassTmp;
            }
            btnControl.myText = ctrText;
            btnControl.IsActiv = bIsActiv;
            btnControl.MyFormObject = (object)myCtr;
            btnControl.Visible = true;
            btnControl.Dock = System.Windows.Forms.DockStyle.Left;
            //btnCtrPassiv.Click += new System.EventHandler(this.btnControlClick);

            //ChangeAFButton_FormProperty(bIsActiv, ref btnControl);           
            return (Control)btnControl;
        }
        ///<summary>ctrFormList / ChangeAFButton_FormProperty</summary>
        ///<remarks>Verändert die Eigenschaften entsprechend aktiv / passiv</remarks> 
        private void ChangeAFButton_FormProperty(bool bIsActiv, ref AFButton_Form myafButton)
        {
            if (bIsActiv)
            {
                myafButton.myColorActivate = this.btnCtrActivTmp.myColorActivate;
                myafButton.myColorBase = this.btnCtrActivTmp.myColorBase;
            }
            else
            {
                myafButton.myColorActivate = this.btnCtrPassTmp.myColorActivate;
                myafButton.myColorBase = this.btnCtrPassTmp.myColorBase;
            }
        }
        ///<summary>ctrFormList / FillFormList</summary>
        ///<remarks>Füllt anhand der Liste die Controllist</remarks> 
        private void FillFormList()
        {
            this.Controls.Clear();
            foreach (Control ctrAdd in listCtr)
            {
                this.Controls.Add(ctrAdd);
                //this.Controls.SetChildIndex(passCtrTmp, listCtr.Count - 1);
            }
            //aktiv einblenden udn passiv ausblenden
            for (Int32 i = 0; i <= this._frmMain.Controls.Count - 1; i++)
            {
                if (CanAddCtrToCtrFrmList((object)this._frmMain.Controls[i]))
                {
                    if (this._frmMain.Controls[i].Name.ToString() == ActivCtrName)
                    {
                        this._frmMain.Controls[i].Show();
                    }
                    else
                    {
                        this._frmMain.Controls[i].Hide();
                    }
                }
            }

        }


        ///<summary>ctrFormList / ClearCtrList</summary>
        ///<remarks>Entfernt alle Controlls aus der Controlllist</remarks> 
        private void ClearCtrList()
        {
            //kann raus
            foreach (Control ctr in this.Controls)
            {
                if (
                    (ctr.Name == "btnCtrActiv") |
                    (ctr.Name == "btnCtrPassiv")
                  )
                {
                    ctr.Visible = false;
                }
                else
                {
                    ctr.Dispose();
                }
            }
        }
        ///<summary>ctrFormList / CanCtrAddToCtrFrmList</summary>
        ///<remarks>Prüft, welche Controlls in der Controlllist angezeigt werden sollen.</remarks> 
        private bool CanAddCtrToCtrFrmList(object obj)
        {
            bool bReturn = false;
            string strObjName = obj.GetType().Name.ToString();

            switch (strObjName)
            {
                case "ctrBestand":
                    ctrText = ((ctrBestand)obj).afColorLabel1.myText;
                    bReturn = true;
                    break;
                case "ctrJournal":
                    ctrText = ((ctrJournal)obj).afColorLabel1.myText;
                    bReturn = true;
                    break;

            }
            return bReturn;
        }





        //Baustelle
        private void button1_Click(object sender, EventArgs e)
        {
            frmSettingsView SettingView = new frmSettingsView();
            SettingView.MdiParent = this.ParentForm;
            btnCtrPassiv.MyForm = SettingView;
            SettingView.Show();
            this.BringToFront();
        }


        private void btnControlClick(object sender, EventArgs e)
        {

        }

        private void SwitchShownCtr(Control myCtr)
        {
            bool bSwitchCtrToActPass = false;
            foreach (Control ctrSwitch in this.Controls)
            {
                //Check auf AFButton
                if (ctrSwitch.GetType() == myCtr.GetType())
                {
                    string strCtrName = ((AFButton_Form)myCtr).MyFormObject.GetType().Name.ToString();
                    bool bCtrFount = false;
                    foreach (Control ctr in this._frmMain.Controls)
                    {
                        //Check ist es ein Ctr, dass in Controllist ist
                        if (CanAddCtrToCtrFrmList((object)ctr))
                        {
                            if (!bCtrFount)
                            {
                                if (ctr.GetType().Name == strCtrName)
                                {
                                    bSwitchCtrToActPass = true;
                                    ctr.Show();
                                }
                                else
                                {
                                    ctr.Hide();
                                }
                            }
                            else
                            {
                                ctr.Hide();
                            }
                        }
                    }
                }
                //Angezeigtes Ctr aktiv setzen
                if (bSwitchCtrToActPass)
                {
                    AFButton_Form ctrChange = this.btnCtrActiv;
                    ctrChange.myText = ((AFButton_Form)ctrSwitch).myText;
                    ctrChange.MyFormObject = ((AFButton_Form)ctrSwitch).MyFormObject;
                    ctrChange.TabIndex = ctrSwitch.TabIndex;

                    bSwitchCtrToActPass = false;
                }
            }
        }

        private void btnCtrActiv_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

        }

        private void btnCtrPassiv_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

        }



















    }
}
