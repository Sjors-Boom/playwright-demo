using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorDemoProject.Pages
{
    [Route("/cookie")]
    public partial class Cookie : ComponentBase
    {
        private static readonly string CookieName = "MyBlazorCookie";

        [Inject]
        public IJSRuntime jSRuntime { get; set; }
        public string? CookieValue { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            await SetCookie();
            await base.OnInitializedAsync();
        }

        protected async Task SetCookie()
        {
            CookieValue = await ReadCookie();

            if (jSRuntime != null && string.IsNullOrEmpty(CookieValue))
            {
                if (string.IsNullOrWhiteSpace(CookieValue))
                {
                    CookieValue = await SetCookie(Guid.NewGuid());
                }
            }
        }

        private async Task<string?> ReadCookie()
        {
            return await jSRuntime.InvokeAsync<string?>("window.ReadCookie.ReadCookie", CookieName);
        }

        private async Task<string> SetCookie(Guid guid)
        {
            string val = guid.ToString();
            await jSRuntime.InvokeVoidAsync("window.WriteCookie.WriteCookie", CookieName, val, 5);

            return val;

        }
    }
}
