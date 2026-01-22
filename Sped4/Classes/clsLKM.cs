using LVS;
using System;

namespace Sped4.Classes
{
    class clsLKM
    {
        internal clsDistance Distance = new clsDistance();
        internal clsTour vorTour = new clsTour();
        internal clsTour nachTour = new clsTour();

        public Globals._GL_USER _GL_User;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }
        //************************************

        private decimal _vorTourID;
        private decimal _nachTourID;
        private DateTime _StartZeit = default(DateTime);
        private DateTime _EndZeit = default(DateTime);
        private string _MouseOverInfo;
        private string _StartOrt;
        private string _EndOrt;
        private decimal _StartAdrID;
        private decimal _EndAdrID;
        private Int32 _km;
        public bool bToPlace = false;

        public decimal nachTourID
        {
            get { return _nachTourID; }
            set { _nachTourID = value; }
        }
        public decimal vorTourID
        {
            get { return _vorTourID; }
            set { _vorTourID = value; }
        }
        public decimal StartAdrID
        {
            get { return _StartAdrID; }
            set { _StartAdrID = value; }
        }
        public decimal EndAdrID
        {
            get { return _EndAdrID; }
            set { _EndAdrID = value; }
        }
        public DateTime StartZeit
        {
            get { return _StartZeit; }
            set { _StartZeit = value; }
        }
        public DateTime EndZeit
        {
            get { return _EndZeit; }
            set { _EndZeit = value; }
        }
        public Int32 km
        {
            get { return _km; }
            set { _km = value; }
        }
        public string StartOrt
        {
            get { return _StartOrt; }
            set { _StartOrt = value; }
        }
        public string EndOrt
        {
            get { return _EndOrt; }
            set { _EndOrt = value; }
        }
        public string MouseOverInfo
        {
            get { return _MouseOverInfo; }
            set { _MouseOverInfo = value; }
        }


        ///<summary>clsTour / INIT</summary>
        ///<remarks>Ermittel die ADR ID</remarks>
        public void Fill()
        {
            Distance.GL_User = this._GL_User;
            vorTour._GL_User = this._GL_User;
            nachTour._GL_User = this._GL_User;

            //Startangaben
            this.vorTour.ID = this.vorTourID;
            this.vorTour.Fill();
            this.StartOrt = this.vorTour.EndOrt;
            this.StartAdrID = this.vorTour.GetEndAdrID();
            //Endangaben
            this.nachTour.ID = this.nachTourID;
            this.nachTour.Fill();
            this.EndOrt = this.nachTour.StartOrt;
            this.EndAdrID = this.nachTour.GetStartAdrID();
            //Leekilometer werden in der Folgetour gespeichert
            this.km = this.nachTour.kmLeer;

            //Enfernungsermittlung
            this.Distance.FillByAdrID(this.StartAdrID, this.EndAdrID);
            if (this.km != this.Distance.km)
            {
                //Entfernung hat sich geändert, deshalb ein Update über die nachfolgende Tour
                this.nachTour.kmLeer = this.Distance.km;
                this.km = this.Distance.km;
                this.nachTour.UpdateTourDaten();
            }
            //MouseOverInfo
            GetMouseOverInfo();
        }

        //
        private void GetMouseOverInfo()
        {
            Int32 Col1 = 0;
            Int32 Col2 = 10;
            string strTmp = string.Empty;
            strTmp = strTmp + "Leerfahrt:  ";
            strTmp = strTmp + Environment.NewLine;
            strTmp = strTmp + Environment.NewLine;

            strTmp = strTmp + String.Format("{0}\t{1}", "von: ", this.StartOrt) + Environment.NewLine;
            strTmp = strTmp + String.Format("{0}\t{1}", "nach: ", this.EndOrt) + Environment.NewLine;
            strTmp = strTmp + String.Format("{0}\t{1}", "Strecke: ", this.km + " [km]") + Environment.NewLine;
            strTmp = strTmp + Environment.NewLine;
            MouseOverInfo = strTmp;
        }

    }
}
