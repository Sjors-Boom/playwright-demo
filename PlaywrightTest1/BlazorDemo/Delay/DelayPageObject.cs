using Microsoft.Playwright;
using PlaywrightTest1.Helpers;
using PlaywrightTest1.PageObjects;
using System.Threading.Tasks;

namespace PlaywrightTest1.BlazorDemo.Delay
{
    public class DelayPageObject : BasePageObject
    {
        public ILocator Button => _page.Locator("#startProcessing");
        public ILocator SuccesMessage => SnackbarLocatorService.BuildLocator(_page, "Action is completed", "success");
        public ISnackbarLocatorService SnackbarLocatorService { get; set; }
        public DelayPageObject(UrlService urlService, ISnackbarLocatorService snackbarLocatorService)
            : base(urlService)
        {
            SnackbarLocatorService = snackbarLocatorService;
        }

        public override async Task OpenAsync()
        {
            await Navigate("delay");
        }
    }
}
