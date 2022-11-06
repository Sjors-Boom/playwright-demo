using Microsoft.Playwright;

namespace PlaywrightTest1.Helpers
{
    public interface ISnackbarLocatorService
    {
        ILocator BuildLocator(IPage page, string message, string severity);
    }
}
