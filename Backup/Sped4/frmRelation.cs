using LVS;
using Sped4.Classes;
using System;
using System.Collections;


namespace Sped4
{
    public partial class frmRelation : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        ctrRelationen ctrRelation;
        public bool _update;
        public decimal relationID;

        public frmRelation(ctrRelationen _ctrRelation, bool update)
        {
            InitializeComponent();
            ctrRelation = _ctrRelation;
            _update = update;
            initForm();
        }
        //
        //
        //
        private void initForm()
        {
            tbRelation.Text = string.Empty;

            //update
            if (_update)
            {
                this.Text = "Relation ändern";

                ArrayList arrayL = new ArrayList();
                arrayL = ctrRelation.RelationList();
                relationID = Convert.ToInt32(arrayL[0].ToString());
                tbRelation.Text = arrayL[1].ToString();
            }
            else
            {
                this.Text = "Relation neu anlegen";
            }
        }
        //
        //
        private void AssignValue()
        {
            clsRelationen relation = new clsRelationen();
            relation.BenutzerID = GL_User.User_ID;
            tbRelation.Text = tbRelation.Text.ToString().Trim();

            if ((tbRelation.Text != ""))
            {
                if (_update)
                {
                    if (GL_User.write_Relation)
                    {
                        relation.ID = relationID;
                        relation.Relation = tbRelation.Text;
                        relation.UpdateRelation();
                        _update = false;
                        this.Close();
                    }
                    else
                    {
                        clsMessages.User_NoAuthen();
                    }
                }
                else
                {
                    if (GL_User.write_Relation)
                    {
                        relation.Relation = tbRelation.Text;
                        relation.AddRelation();
                    }
                    else
                    {
                        clsMessages.User_NoAuthen();
                    }
                }
            }

        }
        //
        //--------- Menu
        //
        private void tsbtnFrmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        //------------------ Speichern Relation ---------------------
        //
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!clsRelationen.RelationExists(tbRelation.Text))
            {
                AssignValue();
                initForm();
                ctrRelation.initCtr();
            }
            else
            {
                clsMessages.Relation_RelationIsUsed();
            }
        }
    }
}
