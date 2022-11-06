using NUnit.Framework;
using PlaywrightTest1.TestBaseClasses;
using System.Threading.Tasks;

namespace PlaywrightTest1.BlazorDemo.Delay
{
    [Parallelizable(ParallelScope.Self)]
    public class DelayTests : PageTestBase
    {
        [Test]
        public async Task PushTheButton()
        {
            DelayPageObject sut = _playwrightServiceProvider.IPageObjectGetPageObject<DelayPageObject>(Page);
            await sut.OpenAsync();

            await sut.Button.ClickAsync();

            await Expect(sut.SuccesMessage).ToBeVisibleAsync();
        }
    }
}
