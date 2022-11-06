using Microsoft.AspNetCore.Components;

namespace BlazorDemoProject.Components
{
    public partial class TextFieldWizardStep : WizardStepContent
    {
        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public string? Value { get; set; }

        [Parameter]
        public EventCallback<string?> ValueChanged { get; set; }
    }
}
