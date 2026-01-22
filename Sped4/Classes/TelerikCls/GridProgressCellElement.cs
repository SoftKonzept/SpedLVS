using System;
using Telerik.WinControls.UI;

namespace Sped4.Classes
{
    public class GridProgressCellElement : GridDataCellElement
    {
        private RadProgressBarElement progressBar;


        public GridProgressCellElement(GridViewColumn column, GridRowElement row)
            : base(column, row)
        {
        }

        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(GridDataCellElement);
            }
        }

        public override bool IsEditable
        {
            get
            {
                return false;
            }
        }

        protected override void CreateChildElements()
        {
            base.CreateChildElements();
            this.progressBar = new RadProgressBarElement();
            this.progressBar.StretchVertically = true;
            this.progressBar.StretchHorizontally = true;
            this.progressBar.Maximum = 100;
            this.Children.Add(this.progressBar);
        }

        public override bool IsCompatible(GridViewColumn data, object context)
        {
            return data.Name == "Progress" && context is GridDataRowElement;
        }

        protected override void SetContentCore(object value)
        {
            Int32 iValue = 0;
            Int32.TryParse(value.ToString(), out iValue);
            this.progressBar.Value1 = iValue;
        }
    }
}
