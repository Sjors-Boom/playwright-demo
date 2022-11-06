using Microsoft.Playwright;
using PlaywrightTest1.Helpers;
using PlaywrightTest1.PageObjects;
using System.Threading.Tasks;

namespace PlaywrightTest1.BlazorDemo.Counter
{
    public class CounterPageObject : BasePageObject
    {
        public ILocator IncementButton => _page.Locator("#incrementCountbutton");
        public ILocator CurrentCount => _page.Locator("currentCount");
        public CounterPageObject(UrlService urlService) : base(urlService)
        { }

        public override async Task OpenAsync()
        {
            await Navigate("counter");
        }
    }
}
