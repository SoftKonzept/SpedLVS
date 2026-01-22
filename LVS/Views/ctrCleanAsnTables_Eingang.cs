using System;
using System.Collections.Generic;

namespace LVS.Views
{
    public class ctrCleanAsnTables_Eingang
    {
        public ctrCleanAsnTables_Eingang()
        {
        }

        public int AsnId { get; set; } = 0;
        public int EingangTableId { get; set; } = 0;
        public int EingangID { get; set; } = 0;
        public DateTime Eingangdatum { get; set; } = DateTime.MinValue;

        public List<ctrCleanAsnTables_EingangArtikel> ListEingangArtikel { get; set; } = new List<ctrCleanAsnTables_EingangArtikel>();

    }
}
