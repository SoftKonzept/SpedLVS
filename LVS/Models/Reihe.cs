using System;
using System.Collections.Generic;
using System.Text;

namespace LVS.Models
{
    public class Reihe
    {
     
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private int _HalleId;
        public int HalleId
        {
            get { return _HalleId; }
            set { _HalleId = value; }
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

        private Int32 _OrderId;
        public Int32 OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }

        private Int32 _maxOrderId;
        public Int32 maxOrderID
        {
            get
            {
                //_maxOrderId = GetMaxOrderID();
                return _maxOrderId;
            }
            set { _maxOrderId = value; }
        }

        private Int32 _maxOrderIdEbene;
        public Int32 maxOrderIdEbene
        {
            get
            {
                //_maxOrderIdEbene = GetMaxOrderIDEbene();
                return _maxOrderIdEbene;
            }
            set { _maxOrderIdEbene = value; }
        }


        private decimal _DickeVon;

        public decimal DickeVon
        {
            get { return _DickeVon; }
            set { _DickeVon = value; }
        }

        private decimal _BreiteVon;

        public decimal BreiteVon
        {
            get { return _BreiteVon; }
            set { _BreiteVon = value; }
        }
        private decimal _LaengeVon;

        public decimal LaengeVon
        {
            get { return _LaengeVon; }
            set { _LaengeVon = value; }
        }
        private decimal _HoeheVon;

        public decimal HoeheVon
        {
            get { return _HoeheVon; }
            set { _HoeheVon = value; }
        }
        private decimal _BruttoVon;

        public decimal BruttoVon
        {
            get { return _BruttoVon; }
            set { _BruttoVon = value; }
        }

        private decimal _DickeBis;

        public decimal DickeBis
        {
            get { return _DickeBis; }
            set { _DickeBis = value; }
        }

        private decimal _BreiteBis;

        public decimal BreiteBis
        {
            get { return _BreiteBis; }
            set { _BreiteBis = value; }
        }
        private decimal _LaengeBis;

        public decimal LaengeBis
        {
            get { return _LaengeBis; }
            set { _LaengeBis = value; }
        }
        private decimal _HoeheBis;

        public decimal HoeheBis
        {
            get { return _HoeheBis; }
            set { _HoeheBis = value; }
        }
        private decimal _BruttoBis;

        public decimal BruttoBis
        {
            get { return _BruttoBis; }
            set { _BruttoBis = value; }
        }

        private decimal _Anzahl;

        public decimal Anzahl
        {
            get { return _Anzahl; }
            set { _Anzahl = value; }
        }

        private List<ReiheGutAssignment> _ReiheGutAssignmentsList;
        public List<ReiheGutAssignment> ReiheGutAssignmentsList
        {
            get { return _ReiheGutAssignmentsList; }
            set { _ReiheGutAssignmentsList = value; }
        }
    }
}
