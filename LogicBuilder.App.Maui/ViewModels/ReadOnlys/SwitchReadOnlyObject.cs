using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.ViewModels.ReadOnlys
{
    public partial class SwitchReadOnlyObject : ReadOnlyObjectBase<bool>
    {
        public SwitchReadOnlyObject(
            IUiNotificationService uiNotificationService,
            string name,
            string templateName,
            string switchLabel) : base(name, templateName, uiNotificationService)
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
    }
}
