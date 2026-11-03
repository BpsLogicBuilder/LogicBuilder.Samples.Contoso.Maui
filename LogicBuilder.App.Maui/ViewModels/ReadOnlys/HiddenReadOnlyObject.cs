using LogicBuilder.App.Maui.Services;

namespace LogicBuilder.App.Maui.ViewModels.ReadOnlys
{
    public class HiddenReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public HiddenReadOnlyObject(
            IUiNotificationService uiNotificationService,
            string name,
            string templateName) : base(name, templateName, uiNotificationService)
        {
        }
    }
}
