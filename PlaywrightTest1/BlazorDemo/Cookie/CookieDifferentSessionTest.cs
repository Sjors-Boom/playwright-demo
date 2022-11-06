using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTest1.TestBaseClasses;
using System.Threading.Tasks;

namespace PlaywrightTest1.BlazorDemo.Cookie
{
    [Parallelizable(ParallelScope.Self)]
    public class CookieDifferentSessionTest : BrowserTestBase
    {
        [Test]
        public async Task MultiplePagesDifferentContextTest()
        {
            IBrowserContext context1 = await CreateBrowserContext(ContextOptions());
            IPage page1 = await context1.NewPageAsync();
            CookiePageObject sut1 = _playwrightServiceProvider.IPageObjectGetPageObject<CookiePageObject>(page1);

            await sut1.OpenAsync();
            await Expect(sut1.CookieValue).ToBeVisibleAsync();
            string cookie1 = await sut1.CookieValue.InnerTextAsync();

            IBrowserContext context2 = await CreateBrowserContext(ContextOptions());
            IPage page2 = await context2.NewPageAsync();
            CookiePageObject sut2 = _playwrightServiceProvider.IPageObjectGetPageObject<CookiePageObject>(page2);

            await sut2.OpenAsync();
            await Expect(sut2.CookieValue).ToBeVisibleAsync();
            string cookie2 = await sut2.CookieValue.InnerTextAsync();

            Assert.AreNotEqual(cookie1, cookie2);

        }
    }
}
