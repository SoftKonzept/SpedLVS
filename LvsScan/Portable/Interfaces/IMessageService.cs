using System.Threading.Tasks;

namespace LvsScan.Portable.Interfaces
{
    interface IMessageService
    {
        Task ShowAsync(string info, string message);
    }

    public class MessageService : IMessageService
    {
        public async Task ShowAsync(string info, string message)
        {
            await App.Current.MainPage.DisplayAlert(info, message, "Ok");
        }
    }
}
