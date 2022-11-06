using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTest1.TestBaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywrightTest1.BlazorDemo.Wizard
{
    [Parallelizable(ParallelScope.Self)]
    public class WizardTest : PageTestBase
    {
        [Test]
        [TestCaseSource(nameof(WizardTestCases))]
        public async Task FillWizard(TestCase testcase)
        {
            WizardPageObject sut = _playwrightServiceProvider.IPageObjectGetPageObject<WizardPageObject>(Page);
            sut.TemplateParam = testcase.Template;
            await sut.OpenAsync();

            testcase.Template ??= new Dictionary<string, string>()
            {
                { "Field 1", "" },
                { "Field 2", "default value" },
                { "Field 3", "" }
            };

            await Expect(sut.PreviousButton).ToBeDisabledAsync();
            await Expect(sut.WizardStartMessage).ToBeVisibleAsync();
            await sut.NextButton.ClickAsync();

            foreach (KeyValuePair<string, string> pair in testcase.Template)
            {
                //Random new value to fill in field
                string value = Guid.NewGuid().ToString();

                //Locator to find the value of the model in the table
                ILocator tableValueLocator = sut.TableValue(pair.Key);

                //Check current value
                await Expect(tableValueLocator).ToHaveTextAsync(pair.Value);
                await Expect(sut.WizardField).ToHaveValueAsync(pair.Value);

                //Update to new value
                await sut.WizardField.FillAsync(value);

                await sut.NextButton.ClickAsync();

                //Check if new value has updated in table
                await Expect(tableValueLocator).ToHaveTextAsync(value);
            }
            await Expect(sut.WizardEndMessage).ToBeVisibleAsync();
            await Expect(sut.NextButton).ToBeDisabledAsync();

        }

        //This method generates 3 different testcases for the above test
        public static IEnumerable<TestCase> WizardTestCases()
        {
            yield return new() { Name = "TestDataNull", Template = null };
            yield return new() { Name = "GenericDefaultValue", Template = new Dictionary<string, string>() { { "Empty", "" }, { "WithDefaultValue", "DefaultValue" }, { "Integer", "1" } } };
            yield return new() { Name = "LargeSet", Template = Enumerable.Range(0, 25).ToDictionary(x => Guid.NewGuid().ToString(), x => x % 2 == 0 ? "" : "odd") };
        }

        public class TestCase
        {
            public string Name { get; set; }
            public IDictionary<string, string> Template { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
