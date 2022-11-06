using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightTest1.Init;
using System.Threading.Tasks;

namespace PlaywrightTest1.TestBaseClasses
{
    [TestFixture]
    public class PageTestBase : PageTest
    {
        protected PlaywrightServiceProvider _playwrightServiceProvider;

        private readonly int width;
        private readonly int height;


        public PageTestBase()
            : this(1280, 720)
        { }

        public PageTestBase(int width, int height)
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
        public async Task TearDown()
        {
            await BrowserContextConfigurator.TearDown(Context);
        }
    }
}
