using Octoller.PinBook.Web.ViewModels.Base;

namespace Octoller.PinBook.Web.ViewModels.User
{
    public class NetworksViewModel : NamedBase
    {
        public bool VKontakte { get; set; } = false;
        public bool Yandex { get; set; } = false;
    }
}
