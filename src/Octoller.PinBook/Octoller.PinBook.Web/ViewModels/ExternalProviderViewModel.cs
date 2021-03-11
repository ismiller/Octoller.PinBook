using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;

namespace Octoller.PinBook.Web.ViewModels
{
    public class ExternalProviderViewModel
    {
        public IEnumerable<AuthenticationScheme> Providers { get; set; }
    }
}
