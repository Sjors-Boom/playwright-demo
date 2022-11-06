using Microsoft.Playwright;
using Newtonsoft.Json;
using PlaywrightTest1.Helpers;
using PlaywrightTest1.PageObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaywrightTest1.BlazorDemo.Wizard
{
    public class WizardPageObject : BasePageObject
    {
        public ILocator Table => _page.Locator("#wizardTable");
        public ILocator NextButton => _page.Locator("#next");
        public ILocator PreviousButton => _page.Locator("#previous");
        public ILocator WizardStartMessage => _page.Locator("#previous");
        public ILocator WizardEndMessage => _page.Locator("#previous");
        public ILocator WizardField => _page.Locator("input[type=text]");

        public IDictionary<string, string>? TemplateParam { get; internal set; }

        public WizardPageObject(UrlService urlService, IServiceProvider serviceProvider)
            : base(urlService)
        { }

        public override async Task OpenAsync()
        {
            if (TemplateParam == null)
            {
                await Navigate("wizard");
            }
            else
            {
                await Navigate($"wizard/{JsonConvert.SerializeObject(TemplateParam)}");
            }
        }

        public ILocator TableValue(string key)
        {
            return Table.Locator("tbody tr")
                .Filter(new()
                {
                    Has = _page.Locator("td")
                        .Filter(new()
                        {
                            HasTextString = key
                        })
                })
                .Locator("td:nth-of-type(2)");
        }
    }
}
