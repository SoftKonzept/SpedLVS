using System.Collections.Generic;

namespace LvsScan.Portable.ViewModels.Test
{
    public class ListViewModel : BaseViewModel
    {
        public ListViewModel()
        {
            StringSourceList = new List<TestString>()
            {
                new TestString(){No=1, Value="Item1" },
                new TestString(){No=2, Value="Item2" },
                new TestString(){No=3, Value="Item3" },
                new TestString(){No=4, Value="Item4" },
                new TestString(){No=5, Value="Item5" }
            };
        }

        private List<TestString> _StringSourceList;
        public List<TestString> StringSourceList
        {
            get { return _StringSourceList; }
            set
            {
                SetProperty(ref _StringSourceList, value);
            }
        }

        private TestString _SelectedItem;
        public TestString SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                SetProperty(ref _SelectedItem, value);
            }
        }

        private List<TestString> _ListSelectedItems;
        public List<TestString> ListSelectedItems
        {
            get { return _ListSelectedItems; }
            set
            {
                SetProperty(ref _ListSelectedItems, value);
            }
        }
    }
}
