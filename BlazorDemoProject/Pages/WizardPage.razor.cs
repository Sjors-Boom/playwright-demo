using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BlazorDemoProject.Pages
{
    [Route("/wizard")]
    [Route("/wizard/{ProppertyListJSON}")]
    public partial class WizardPage
    {
        [Parameter]
        public IDictionary<string, string>? ProppertyList { get; set; }

        [Parameter]
        public string? ProppertyListJSON { get; set; }


        protected override void OnInitialized()
        {
            if (ProppertyList == null)
            {
                IDictionary<string, string>? result = JsonConvert.DeserializeObject<IDictionary<string, string>>(ProppertyListJSON ?? string.Empty);
                if (result != null)
                {
                    ProppertyList = result;
                }
                else
                {
                    ProppertyList = new Dictionary<string, string>{
                        { "Field 1", "" },
                        { "Field 2", "default value" },
                        { "Field 3", "" }
                    };
                }
            }
            base.OnInitialized();
        }

        private void ValueChanged(string key, string value)
        {
            if (ProppertyList?.ContainsKey(key) ?? false)
            {
                ProppertyList[key] = value;
            }
        }
    }
}
