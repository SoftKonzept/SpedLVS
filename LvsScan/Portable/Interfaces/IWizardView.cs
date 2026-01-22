using LvsScan.Portable.ViewModels;
using System.Threading.Tasks;

namespace LvsScan.Portable.Interfaces
{
    public interface IWizardView
    {
        Task<bool> OnNext(BaseViewModel viewModel);
        Task<bool> OnPrevious(BaseViewModel viewModel);
        Task OnAppearing();
        Task OnDissapearing();
    }
}
