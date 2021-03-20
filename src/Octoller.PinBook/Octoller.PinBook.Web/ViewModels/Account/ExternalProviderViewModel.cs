using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;

namespace Octoller.PinBook.Web.ViewModels.Account
{
    public class ExternalProviderViewModel
    {
        public IEnumerable<AuthenticationScheme> Providers { get; set; }
    }
}
