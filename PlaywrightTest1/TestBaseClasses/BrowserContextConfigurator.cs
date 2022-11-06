using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace PlaywrightTest1.TestBaseClasses
{
    public static class BrowserContextConfigurator
    {
        public static string FormattedNow => DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ffff");

        public static BrowserNewContextOptions ConfigureOptions(BrowserNewContextOptions? browserNewContextOptions, int width, int height)
        {
            browserNewContextOptions ??= new();
            browserNewContextOptions.ViewportSize = new() { Height = height, Width = width };
            browserNewContextOptions.RecordVideoDir = "video";
            browserNewContextOptions.RecordVideoSize = new() { Height = height, Width = width };
            browserNewContextOptions.IgnoreHTTPSErrors = true;

            return browserNewContextOptions;
        }

        public static async Task<IBrowserContext> Configure(IBrowserContext context)
        {
            string fileName = $"{TestContext.CurrentContext.Test.FullName}";
            await context.Tracing.StartAsync(new TracingStartOptions()
            {
                Name = fileName,
                Screenshots = true,
                Snapshots = true,
                Sources = true,
                Title = fileName
            });

            return context;
        }

        public static async Task TearDown(IBrowserContext context)
        {
            await SaveTrace(context);

            await SaveVideo(context);
        }

        private static async Task SaveVideo(IBrowserContext context)
        {
            foreach (IPage page in context.Pages)
            {
                IVideo? video = page.Video;
                if (video != null)
                {
                    string? videoPath = await video.PathAsync();
                    if (!string.IsNullOrWhiteSpace(videoPath))
                    {
                        TestContext.AddTestAttachment(videoPath, "Video");
                    }
                }
            }
        }

        private static async Task SaveTrace(IBrowserContext context)
        {
            string path = $"traces/{TestContext.CurrentContext.Test.FullName}_{FormattedNow}.zip";
            await context.Tracing.StopAsync(new()
            {
                Path = path
            });

            TestContext.AddTestAttachment(path, "TestTrace");
        }
    }
}
