using Microsoft.Playwright;
using PlaywrightTest1.Helpers;
using PlaywrightTest1.PageObjects;
using System.Threading.Tasks;

namespace PlaywrightTest1.BlazorDemo.Delay
{
    public class DelayPageObject : BasePageObject
    {
        public ILocator Button => default!;
        public ILocator SuccesMessage => default!;// Hint: Build SuccesMessageLocator with the SnackbarLocatorService
        public ISnackbarLocatorService SnackbarLocatorService { get; set; }
        public DelayPageObject(UrlService urlService, ISnackbarLocatorService snackbarLocatorService)
            : base(urlService)
        {
            SnackbarLocatorService = snackbarLocatorService;
        }

        public override Task OpenAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
