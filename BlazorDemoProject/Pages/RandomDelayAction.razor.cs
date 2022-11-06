using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorDemoProject.Pages
{
    [Route("/delay")]
    public partial class RandomDelayAction
    {
        private readonly int _maxDelay = (int)TimeSpan.FromSeconds(5).TotalMilliseconds;
        private readonly int _minDelay = (int)TimeSpan.FromSeconds(1).TotalMilliseconds;

        private bool _processing = false;
        [Inject]
        public Random Random { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }
        private async Task StartAction()
        {
            _processing = true;
            int delay = Random.Next(_minDelay, _maxDelay);
            await Task.Delay(delay);

            Snackbar.Add("Action is completed", Severity.Success, conf =>
            {
                conf.SnackbarVariant = Variant.Filled;
                conf.BackgroundBlurred = true;
            });
            _processing = false;
        }
    }
}
