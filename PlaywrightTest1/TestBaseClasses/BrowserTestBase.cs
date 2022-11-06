using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightTest1.Init;
using System.Threading.Tasks;

namespace PlaywrightTest1.TestBaseClasses
{
    public class BrowserTestBase : BrowserTest
    {
        private readonly int width;
        private readonly int height;
        public PlaywrightServiceProvider _playwrightServiceProvider;

        public BrowserTestBase()
            : this(1280, 780)
        { }

        public BrowserTestBase(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public virtual async Task<IBrowserContext> CreateBrowserContext(BrowserNewContextOptions? options)
        {
            if (options == default)
            {
                options = ContextOptions();
            }

            IBrowserContext context = await Browser.NewContextAsync(options);

            return await BrowserContextConfigurator.Configure(context);
        }

        public virtual BrowserNewContextOptions ContextOptions()
        {
            return BrowserContextConfigurator.ConfigureOptions(new(), width, height);
        }

        [SetUp]
        public async Task SetupBase()
        {
            _playwrightServiceProvider = await PlaywrightServiceProvider.Register(this);
        }

        [TearDown]
        public async Task TearDownBase()
        {
            foreach (IBrowserContext context in Browser.Contexts)
            {
                await BrowserContextConfigurator.TearDown(context);
                await context.CloseAsync();
            }
        }
    }
}
