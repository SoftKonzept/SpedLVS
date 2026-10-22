using System;

namespace LVS.Models
{
    public class Halle
    {
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private int _WerkId;
        public int WerkId
        {
            get { return _WerkId; }
            set { _WerkId = value; }
        }

        private string _Bezeichnung;
        public string Bezeichnung
        {
            get { return _Bezeichnung; }
            set { _Bezeichnung = value; }
        }

        private string _Beschreibung;
        public string Beschreibung
        {
            get { return _Beschreibung; }
            set { _Beschreibung = value; }
        }

        private int _OrderId;
        public int OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }

        private int _maxOrderId;
        public int maxOrderId
        {
            get
            {
                //_maxOrderId = GetMaxOrderID();
                return _maxOrderId;
            }
            set { _maxOrderId = value; }
        }

        //private bool _ExLagerOrt;
        //public bool ExLagerOrt
        //{
        //    get { return _ExLagerOrt; }
        //    set { _ExLagerOrt = value; }
        //}

        private int _maxOrderIDHalle;
        public Int32 maxOrderIDHalle
        {
            get
            {
                //_maxOrderIDHalle = GetMaxOrderIDHalle();
                return _maxOrderIDHalle;
            }
            set { _maxOrderIDHalle = value; }
        }
    }
}
