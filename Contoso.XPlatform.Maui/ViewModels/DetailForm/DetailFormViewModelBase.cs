using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using LogicBuilder.App.Maui.Business.Requests;
using LogicBuilder.App.Maui.Business.Settings.Screen;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contoso.XPlatform.ViewModels.DetailForm
{
    public abstract class DetailFormViewModelBase(
        ScreenSettings<DataFormSettingsDescriptor> screenSettings,
        IUiNotificationService uiNotificationService) : ViewModelBase
    {
        public Dictionary<string, IReadOnly> BindingPropertiesDictionary
            => FormLayout.Properties.ToDictionary(p => p.Name.ToBindingDictionaryKey());

        public DataFormSettingsDescriptor FormSettings { get; set; } = screenSettings.Settings;
        abstract public DetailFormLayout FormLayout { get; set; }
        public IUiNotificationService UiNotificationService { get; set; } = uiNotificationService;
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; } = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons ?? []);

        private ICommand? _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (_nextCommand != null)
                    return _nextCommand;

                _nextCommand = new Command<CommandButtonDescriptor>
                (
                     Next
                );

                return _nextCommand;
            }
        }

        protected static void Next(CommandButtonDescriptor button)
        {
            _ = NavigateNext(button);
        }

        protected static async Task NavigateNext(CommandButtonDescriptor button)
        {
            using IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>();
            flowManagerService.CopyFlowItems();
            await flowManagerService.Next
            (
                new CommandButtonRequest
                {
                    NewSelection = button.ShortString
                }
            );
        }
    }
}
