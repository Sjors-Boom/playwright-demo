using Microsoft.Playwright;
using PlaywrightTest1.Helpers;
using PlaywrightTest1.PageObjects;
using System.Threading.Tasks;

namespace PlaywrightTest1.BlazorDemo.Cookie
{
    public class CookiePageObject : BasePageObject
    {
        public ILocator CookieValue => _page.Locator("#cookieValue");
        public CookiePageObject(UrlService urlService) : base(urlService)
        { }

        public override async Task OpenAsync()
        {
            await Navigate("cookie");
        }
    }
}
