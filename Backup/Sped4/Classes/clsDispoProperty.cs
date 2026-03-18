namespace Sped4.Classes
{
    class clsDispoProperty
    {

        private decimal _m_dec_DispoMaxGewicht;


        public decimal m_dec_DispoMaxGewicht
        {
            get
            {
                _m_dec_DispoMaxGewicht = 28000m;
                return _m_dec_DispoMaxGewicht;
            }
            set { _m_dec_DispoMaxGewicht = value; }
        }

    }
}
