using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Forms;
using Sped4;
using Sped4.Classes;

namespace Sped4
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
