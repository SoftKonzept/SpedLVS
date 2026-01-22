using LvsScan.Portable.Interfaces;
using LvsScan.Portable.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        internal readonly IMessageService MessageService;

        public delegate void SetWizardNextButtonEnabled(bool bEnabled);
        public SetWizardNextButtonEnabled delegateSetWizardNextButtonEnabled;
        public BaseViewModel()
        {
            this.MessageService = DependencyService.Get<IMessageService>();
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private bool _isBaseNextEnabeld;
        public bool IsBaseNextEnabeld
        {
            get { return _isBaseNextEnabeld; }

            set
            {
                SetProperty(ref _isBaseNextEnabeld, value);
                if (delegateSetWizardNextButtonEnabled != null)
                {
                    delegateSetWizardNextButtonEnabled(_isBaseNextEnabeld);
                }
            }
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
                ShowBusyIndicator = isBusy;
                ShowContent = (!isBusy);
            }
        }

        private bool _isCustomButtonEnabled;
        public bool IsCustomButtonEnabled
        {
            get { return _isCustomButtonEnabled; }
            set { SetProperty(ref _isCustomButtonEnabled, value); }
        }

        bool showBusyIndicator;
        public bool ShowBusyIndicator
        {
            get { return showBusyIndicator; }
            set { SetProperty(ref showBusyIndicator, value); }
        }

        bool showContent = false;
        public bool ShowContent
        {
            get { return showContent; }
            set { SetProperty(ref showContent, value); }
        }

        private WizardData wizardData;  //=new WizardData();
        internal WizardData WizardData
        {
            get { return wizardData; }
            set { SetProperty(ref wizardData, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
