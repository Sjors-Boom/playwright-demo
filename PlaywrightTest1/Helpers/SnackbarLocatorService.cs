using Microsoft.Playwright;

namespace PlaywrightTest1.Helpers
{
    public class SnackbarLocatorService : ISnackbarLocatorService
    {
        public ILocator BuildLocator(IPage page, string message, string severity)
        {
            return page.Locator($"div[class*=mud-snackbar][class*=success] div[class=mud-snackbar-content-message]:has-text('{message}')");
        }
    }
}
