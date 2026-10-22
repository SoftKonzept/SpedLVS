using Common.Models;

namespace LVS.Models
{
    public class ReiheGutAssignment
    {
        private int _Id;
        private decimal _ReiheId;
        private decimal _GArtId;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public decimal ReiheId
        {
            get { return _ReiheId; }
            set { _ReiheId = value; }
        }

        public decimal GArtId
        {
            get { return _GArtId; }
            set { _GArtId = value; }
        }

        private Goodstypes _Gut;
        public Goodstypes Gut
        {
            get { return _Gut; }
            set { _Gut = value; }
        }
    }
}
