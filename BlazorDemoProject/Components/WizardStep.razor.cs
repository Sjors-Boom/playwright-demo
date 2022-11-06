using Microsoft.AspNetCore.Components;

namespace BlazorDemoProject.Components
{
    public partial class WizardStep : ComponentBase
    {
        [CascadingParameter]
        public Wizard? Parent { get; set; }

        public bool Complete { get; private set; }
        public bool IsCurrentStep => Parent?.CurrentStep == this;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        protected override Task OnInitializedAsync()
        {
            Parent?.AddStep(this);
            return base.OnInitializedAsync();
        }

        protected void MarkComplete()
        {
            Complete = true;
        }
    }
}
