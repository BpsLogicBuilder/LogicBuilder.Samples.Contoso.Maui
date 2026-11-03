using LogicBuilder.App.Maui.Validators;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace LogicBuilder.App.Maui.ViewModels.Validatables
{
    public partial class SwitchValidatableObject : ValidatableObjectBase<bool>
    {
        public SwitchValidatableObject(IUiNotificationService uiNotificationService, string name, string templateName, string switchLabel, IEnumerable<IValidationRule>? validations)
            : base(name, templateName, validations, uiNotificationService)
        {
            SwitchLabel = switchLabel;
        }

        private string _switchLabel;
        public string SwitchLabel
        {
            get => _switchLabel;
            [MemberNotNull(nameof(_switchLabel))]
            set
            {
                if (_switchLabel == value)
                    return;

                _switchLabel = value;
                OnPropertyChanged();
            }
        }
#pragma warning disable S4275 //using base.Value
        public override bool Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                this.uiNotificationService.NotifyPropertyChanged(this.Name);
                OnPropertyChanged();
            }
        }
#pragma warning restore S4275 //using base.Value
        public ICommand ToggledCommand => new Command
        (
            () =>
            {
                IsDirty = true;
                IsValid = Validate();
            }
        );
    }
}
