using Microsoft.AspNetCore.Components;

namespace BlazorDemoProject.Pages
{
    [Route("/randomElement")]
    [Route("/randomElement/{RelevantElementCount:int}")]
    public partial class RandomElement : ComponentBase
    {
        public static readonly int MaxElements = 20;

        [Parameter]
        public int RelevantElementCount { get; set; } = 3;

        public IDictionary<Guid, bool> Values { get; private set; }
        public IList<Guid> RelevantValues { get; private set; } = new List<Guid>();

        public bool IsValid { get; private set; }

        [Inject]
        public Random Random { get; set; }

        protected override Task OnInitializedAsync()
        {
            if (RelevantElementCount == 0)
            {
                RelevantElementCount = 3;
            }
            else if (RelevantElementCount > MaxElements)
            {
                RelevantElementCount /= 2;
            }

            SetValues();

            return base.OnInitializedAsync();
        }

        public string GetId(Guid guid)
        {
            return $"{(RelevantValues.Contains(guid) ? "do" : "dont")}-select-{guid}";
        }

        public void UpdateChecked(Guid key, bool val)
        {
            if (!Values.ContainsKey(key))
            {
                return;
            }

            Values[key] = val;

            UpdateIsValid();
        }

        private void UpdateIsValid()
        {
            IsValid = Values.All(x => RelevantValues.Contains(x.Key) == x.Value);
        }

        private void SetValues()
        {
            Values = Enumerable.Range(0, MaxElements).Select(x => Guid.NewGuid()).ToDictionary(x => x, x => false);

            while (RelevantValues.Count < RelevantElementCount)
            {
                Guid key = Values.ElementAt(Random.Next(Values.Count)).Key;
                if (!RelevantValues.Contains(key))
                {
                    RelevantValues.Add(key);
                }
            }
        }
    }
}
