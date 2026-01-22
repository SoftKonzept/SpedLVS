using LvsScan.Portable.ViewModels.Wizard;
using System;
using System.Collections.Generic;

namespace LvsScan.Portable.EventHandlers
{
    public class WizardFinishedEventArgs : EventArgs
    {
        public WizardFinishedEventArgs(IEnumerable<WizardItemViewModel> wizardItemViewModels)
        {
            WizardItemViewModels = wizardItemViewModels;
        }


        public IEnumerable<WizardItemViewModel> WizardItemViewModels { get; private set; }
    }
}
