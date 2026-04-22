using System;
using System.ComponentModel;
using System.Drawing.Printing;
using Telerik.WinControls.UI;

namespace Sped4.Classes
{
    public class ComboBoxCellElement : GridDataCellElement, INotifyPropertyChanged
    {
        private RadDropDownListElement DropDown;
        public event PropertyChangedEventHandler PropertyChanged;

        public ComboBoxCellElement(GridViewColumn column, GridRowElement row)
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
            this.DropDown = new RadDropDownListElement();
            DropDown.ValueChanged += new EventHandler(ValChanged);
            PrinterSettings ps = new PrinterSettings();
            string defaultPrinter = "";
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                ps.PrinterName = printer;
                if (ps.IsDefaultPrinter)
                    defaultPrinter = printer;
                DropDown.Items.Add(printer);
            }
            this.DropDown.DefaultValue = defaultPrinter;
            this.Children.Add(this.DropDown);
        }

        private void ValChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("Value");
        }

        public override bool IsCompatible(GridViewColumn data, object context)
        {
            return data.Index > 1 && context is GridDataRowElement;
        }

        protected override void SetContentCore(object value)
        {

        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
