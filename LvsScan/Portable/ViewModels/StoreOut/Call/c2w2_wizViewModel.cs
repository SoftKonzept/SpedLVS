using Common.Enumerations;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LvsScan.Portable.ViewModels.StoreOut.Call
{
    public class c2w2_wizViewModel : BaseViewModel
    {
        public c2w2_wizViewModel()
        {
        }

        public enumStoreOutArt StoreOutArt = enumStoreOutArt.call;
        public enumStoreOutArt_Steps StoreOutArt_Steps = enumStoreOutArt_Steps.wizStepLast;

        private ObservableCollection<Calls> _CallArticles = new ObservableCollection<Calls>();
        public ObservableCollection<Calls> CallArticles
        {
            get { return _CallArticles; }
            set
            {
                SetProperty(ref _CallArticles, value);
                CountAll = _CallArticles.Count;
                GetSubLists();
            }
        }
        private void GetSubLists()
        {
            List<Calls> tmpOriginal = CallArticles.ToList();
            CallArticlesUnChecked = new ObservableCollection<Calls>(tmpOriginal.Where(x => x.ScanCheckForStoreOut <= new DateTime(1900, 1, 1)).ToList());
        }

        private ObservableCollection<Calls> _CallArticlesUnChecked = new ObservableCollection<Calls>();
        public ObservableCollection<Calls> CallArticlesUnChecked
        {
            get { return _CallArticlesUnChecked; }
            set
            {
                SetProperty(ref _CallArticlesUnChecked, value);
                CountUnCheck = _CallArticlesUnChecked.Count;
            }
        }

        private int countAll;
        public int CountAll
        {
            get { return countAll; }
            set { SetProperty(ref countAll, value); }
        }

        private int countUnCheck;
        public int CountUnCheck
        {
            get { return countUnCheck; }
            set
            {
                SetProperty(ref countUnCheck, value);

            }
        }
        private int countUncheckedArticles = 0;
        public int CountUncheckedArticles
        {
            get { return countUncheckedArticles; }
            set { SetProperty(ref countUncheckedArticles, value); }
        }
    }
}
