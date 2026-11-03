using LogicBuilder.App.Maui.Business.Settings;
using System.Reactive.Subjects;

namespace LogicBuilder.App.Maui
{
    public interface IUiNotificationService
    {
        FlowSettings? FlowSettings { get; }
        Subject<FlowSettings> FlowSettingsSubject { get; }
        Subject<string> ValueChanged { get; }

        void NotifyFlowSettingsChanged(FlowSettings flowSettings);
        void NotifyPropertyChanged(string fieldName);
    }
}
