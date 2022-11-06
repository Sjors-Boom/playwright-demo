using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTest1.TestBaseClasses;
using System.Threading.Tasks;

namespace PlaywrightTest1.BlazorDemo.Cookie
{
    [Parallelizable(ParallelScope.Self)]
    public class CookieSameSessionTest : ContextTestBase
    {
        [Test]
        public async Task RefreshPageTest()
        {
            IPage page = await Context.NewPageAsync();
            CookiePageObject sut = _playwrightServiceProvider.IPageObjectGetPageObject<CookiePageObject>(page);

            await sut.OpenAsync();
            await Expect(sut.CookieValue).ToBeVisibleAsync();
            string cookie = await sut.CookieValue.InnerTextAsync();

            await sut.ReloadAsync();
            await Expect(sut.CookieValue).ToBeVisibleAsync();

            Assert.AreEqual(cookie, await sut.CookieValue.InnerTextAsync());
        }

        [Test]
        public async Task MultiplePagesSameContextTest()
        {
            IPage page1 = await Context.NewPageAsync();
            CookiePageObject sut1 = _playwrightServiceProvider.IPageObjectGetPageObject<CookiePageObject>(page1);

            await sut1.OpenAsync();
            await Expect(sut1.CookieValue).ToBeVisibleAsync();
            string cookie1 = await sut1.CookieValue.InnerTextAsync();

            IPage page2 = await Context.NewPageAsync();
            CookiePageObject sut2 = _playwrightServiceProvider.IPageObjectGetPageObject<CookiePageObject>(page2);

            await sut2.OpenAsync();
            await Expect(sut2.CookieValue).ToBeVisibleAsync();
            string cookie2 = await sut2.CookieValue.InnerTextAsync();

            Assert.AreEqual(cookie1, cookie2);
        }
    }
}
