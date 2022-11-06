using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightTest1.Init;
using System.Threading.Tasks;

namespace PlaywrightTest1.TestBaseClasses
{
    public class ContextTestBase : ContextTest
    {
        private readonly int width;
        private readonly int height;
        public PlaywrightServiceProvider _playwrightServiceProvider;

        public ContextTestBase()
            : this(1280, 780)
        { }

        public ContextTestBase(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public override BrowserNewContextOptions ContextOptions()
        {
            return BrowserContextConfigurator.ConfigureOptions(base.ContextOptions(), width, height);
        }

        [SetUp]
        public async Task SetupBase()
        {
            _playwrightServiceProvider = await PlaywrightServiceProvider.Register(this);
            await BrowserContextConfigurator.Configure(Context);
        }

        [TearDown]
        public async Task TearDownBase()
        {
            await BrowserContextConfigurator.TearDown(Context);
        }
    }
}
