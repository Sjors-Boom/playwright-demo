using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTest1.TestBaseClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaywrightTest1.BlazorDemo.RandomElement
{
    [Parallelizable(ParallelScope.Self)]
    public class RandomElementTests : PageTestBase
    {
        [Test]
        [TestCase(null)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task SelectCorrectElements(int? count)
        {
            RandomElementPageObject sut = _playwrightServiceProvider.IPageObjectGetPageObject<RandomElementPageObject>(Page);
            sut.CountParam = count;
            await sut.OpenAsync();

            await Expect(sut.RandomElementSelectedState).ToContainTextAsync("Not the right values are selected");

            await Expect(sut.RelevantDropdowns).ToHaveCountAsync(count ?? 3);

            //Get the list of the found dropdownvalues
            IReadOnlyList<IElementHandle> handles = await sut.RelevantDropdowns.ElementHandlesAsync();

            foreach (IElementHandle handle in handles)
            {
                await handle.ClickAsync();
            }

            await Expect(sut.RandomElementSelectedState).ToContainTextAsync("The correct values are selected");

        }
    }
}
