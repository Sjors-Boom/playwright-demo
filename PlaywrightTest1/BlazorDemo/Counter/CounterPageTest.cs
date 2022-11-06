using NUnit.Framework;
using PlaywrightTest1.TestBaseClasses;
using System.Threading.Tasks;

namespace PlaywrightTest1.BlazorDemo.Counter
{
    [Parallelizable(ParallelScope.Self)]
    public class CounterPageTest : PageTestBase
    {
        [Test]
        public async Task CheckIncrement()
        {
            CounterPageObject counterPage = _playwrightServiceProvider.IPageObjectGetPageObject<CounterPageObject>(Page);
            await counterPage.OpenAsync();

            await Expect(counterPage.CurrentCount).ToHaveTextAsync("0");
            for (int i = 0; i < 10; i++)
            {
                await counterPage.IncementButton.ClickAsync();
                await Expect(counterPage.CurrentCount).ToHaveTextAsync($"{i + 1}");
            }
        }
    }
}
