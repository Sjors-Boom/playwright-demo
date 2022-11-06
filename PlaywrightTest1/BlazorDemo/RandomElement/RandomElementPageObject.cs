using Microsoft.Playwright;
using PlaywrightTest1.Helpers;
using PlaywrightTest1.PageObjects;
using System.Threading.Tasks;

namespace PlaywrightTest1.BlazorDemo.RandomElement
{
    public class RandomElementPageObject : BasePageObject
    {
        public ILocator RandomElementSelectedState => _page.Locator("#randomElementSelectedState");
        // Sometimes you dont have control over the target
        // so you need to find a different means to differentiate the target
        // Hint: https://www.w3schools.com/css/css_attribute_selectors.asp
        public ILocator RelevantDropdowns => _page.Locator("input[id^=do-select]");
        public RandomElementPageObject(UrlService urlService)
            : base(urlService)
        { }

        public int? CountParam { get; set; }

        public override async Task OpenAsync()
        {
            if (CountParam == null)
            {
                await Navigate("randomElement");
            }
            else
            {
                await Navigate($"randomElement/{CountParam}");
            }
        }
    }
}
