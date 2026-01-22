using System.Collections.Generic;
using System.Linq;

namespace Common.Barcodes
{
    public class Code39
    {
        public Code39(string ValToBarcode)
        {
            ValueToBarcode = ValToBarcode;
            GetCheckDigit();
            BarcodeCode39WihtoutCheckDigit = ValueToBarcode;
            BarcodeCode39WihtCheckDigit = ValueToBarcode + CheckCode.ToString();
        }



        public string BarcodeCode39WihtoutCheckDigit { get; set; }
        public string BarcodeCode39WihtCheckDigit { get; set; }
        public string ValueToBarcode { get; set; }
        public char CheckCode { get; set; }
        public int SumCode { get; set; }
        public int Length { get; set; }

        public Dictionary<char, int> DictCheckSumCode39s = new Dictionary<char, int>()
        {
            {'0', 0 },
            {'1', 1 },
            {'2', 2 },
            {'3', 3 },
            {'4', 4 },
            {'5', 5 },
            {'6', 6 },
            {'7', 7 },
            {'8', 8 },
            {'9', 9 },
            {'A', 10 },
            {'B', 11 },
            {'C', 12 },
            {'D', 13 },
            {'E', 14 },
            {'F', 15 },
            {'G', 16 },
            {'H', 17 },
            {'I', 18 },
            {'J', 19 },
            {'K', 20 },
            {'L', 21 },
            {'M', 22 },
            {'N', 23 },
            {'O', 24 },
            {'P', 25 },
            {'Q', 26 },
            {'R', 27 },
            {'S', 28 },
            {'T', 29 },
            {'U', 30 },
            {'V', 31 },
            {'W', 32 },
            {'X', 33 },
            {'Y', 34 },
            {'Z', 35 },
            {'-', 36 },
            {'.', 37 },
            {' ', 38 },
            {'$', 39 },
            {'/', 40 },
            {'+', 41 },
            {'%', 42 },
        };

        private void GetCheckDigit()
        {
            SumCode = 0;
            CheckCode = new char();
            List<char> ValueList = ValueToBarcode.ToCharArray().ToList();

            foreach (char c in ValueList)
            {
                int iIndex = 0;
                DictCheckSumCode39s.TryGetValue(c, out iIndex);
                if (iIndex > 0)
                {
                    SumCode += iIndex;
                }
            }

            int iCD = SumCode % 43;
            CheckCode = DictCheckSumCode39s.FirstOrDefault(x => x.Value == iCD).Key;
        }
    }
}
