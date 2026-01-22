using LVS;
using Sped4.Controls.Processes;
using System;

namespace Sped4
{
    public partial class frmGArtenAuftragserfassung : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;
        public decimal TakeOver_ID = 0;
        public decimal AdrIDForGArtAssignment = 0;

        public ctrGueterArtListe _ctrGutArtenListe = new ctrGueterArtListe();
        public frmAuftrag_Fast _frmAuftragErfassung;
        public ctrEinlagerung _ctrEinlagerung;
        public ctrArtDetails _ctrArtDetails;
        public ctrTarifErfassung _ctrTarifErfassung;
        public ctrJournal _ctrJournal;
        public ctrSearch _ctrSearch;
        public ctrReihen _ctrReihen;
        public ctrADR_List _ctrAdrListGArtDefault;
        public ctrCustomProcessExcesption _ctrCustomProcessException;

        /**********************************************************************
         *              Procedure / Methoden
         * *******************************************************************/
        ///<summary>frmGArtenAuftragserfassung/frmGArtenAuftragserfassung</summary>
        ///<remarks></remarks>
        public frmGArtenAuftragserfassung()
        {
            InitializeComponent();
        }
        ///<summary>frmGArtenAuftragserfassung/frmGArtenAuftragserfassung_Load</summary>
        ///<remarks></remarks>
        private void frmGArtenAuftragserfassung_Load(object sender, EventArgs e)
        {
            this.AdrIDForGArtAssignment = 0;
            if (this._ctrEinlagerung != null)
            {
                this.AdrIDForGArtAssignment = this._ctrEinlagerung.Lager.Eingang.Auftraggeber;
            }
            if (this._ctrTarifErfassung is ctrTarifErfassung)
            {
                this.AdrIDForGArtAssignment = this._ctrTarifErfassung._decAdrID;
            }
            this.GL_User = this._ctrMenu.GL_User;
            this._ctrMenu.OpenCtrGArtenList(this);
        }
        ///<summary>frmGArtenAuftragserfassung/SetSearch_ID</summary>
        ///<remarks></remarks>
        public void SetSearch_ID(decimal ga_ID)
        {
            TakeOver_ID = ga_ID;
            if (_frmAuftragErfassung != null)
            {
                _frmAuftragErfassung.TakeOverGueterArt(TakeOver_ID);
            }
            if (_ctrEinlagerung != null)
            {
                _ctrEinlagerung.TakeOverGueterArt(TakeOver_ID);
            }
            if (_ctrArtDetails != null)
            {
                _ctrArtDetails.TakeOverGueterArt(TakeOver_ID);
            }
            if (this._ctrTarifErfassung != null)
            {
                _ctrTarifErfassung.TakeOverGueterArt(TakeOver_ID);
            }
            if (this._ctrSearch != null)
            {
                _ctrSearch._ctrArtSearchFilter.TakeOverGueterArt(TakeOver_ID);
            }
            if (this._ctrJournal != null)
            {
                this._ctrJournal._ctrArtSearchFilter.TakeOverGueterArt(TakeOver_ID);

            }
            if (this._ctrReihen != null)
            {
                this._ctrReihen.TakeOverGueterArt(TakeOver_ID);
            }
            if (this._ctrAdrListGArtDefault != null)
            {
                this._ctrAdrListGArtDefault.TakeOverGueterArt(TakeOver_ID);
            }
            if (this._ctrCustomProcessException != null)
            {
                this._ctrCustomProcessException.TakeOverGueterArt(TakeOver_ID);
            }
        }
        ///<summary>frmGArtenAuftragserfassung/CloseFrmGArtenAutfragsErfassung</summary>
        ///<remarks></remarks>
        public void CloseFrmGArtenAutfragsErfassung()
        {
            this.Close();
        }


    }
}
