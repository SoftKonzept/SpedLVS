using System;

namespace Sped4.Classes
{
    class clsSQLTableColumn
    {
        public string Table { get; set; }
        public string Column { get; set; }
        public string ColViewName { get; set; }
        public string SQLText { get; set; }
        public Type Type { get; set; }
    }
}
