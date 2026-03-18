using System;

namespace LVS.Views
{
    public class ctrCleanAsnTables_EingangArtikel
    {
        public ctrCleanAsnTables_EingangArtikel()
        {
        }

        public int AsnId { get; set; } = 0;
        public int AritkelId { get; set; } = 0;
        public int LvsNr { get; set; } = 0;
        public int LAusgangTableId { get; set; } = 0;
        public DateTime Ausgangdatum { get; set; } = DateTime.MinValue;
    }
}
