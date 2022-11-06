using Microsoft.AspNetCore.Components;

namespace BlazorDemoProject.Components
{
    public partial class Wizard
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        public List<WizardStep> Steps { get; set; } = new List<WizardStep>();
        public WizardStep? CurrentStep => Steps.ElementAtOrDefault(stepIndex);
        private int stepIndex = 0;

        internal void AddStep(WizardStep wizardStep)
        {
            Steps.Add(wizardStep);
        }

        protected void Previous()
        {
            if (stepIndex > 0)
            {
                stepIndex--;
            }
        }

        protected void Next()
        {
            if (stepIndex < Steps.Count)
            {
                stepIndex++;
            }
        }

        internal void SetStepCompleted(WizardStep wizardStep)
        {
            throw new NotImplementedException();
        }
    }
}
