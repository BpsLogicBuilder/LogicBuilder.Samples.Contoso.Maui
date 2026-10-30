using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contoso.XPlatform.ViewModels.EditForm
{
    public abstract class EditFormViewModelBase(
        ScreenSettings<DataFormSettingsDescriptor> screenSettings,
        IUiNotificationService uiNotificationService) : ViewModelBase, IDisposable
    {
        abstract public EditFormLayout FormLayout { get; set; }

        public Dictionary<string, IValidatable> BindingPropertiesDictionary
            => FormLayout.Properties.ToDictionary(p => p.Name.ToBindingDictionaryKey());

        public DataFormSettingsDescriptor FormSettings { get; set; } = screenSettings.Settings;
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
                     EditFormViewModelBase.Next
                );

                return _nextCommand;
            }
        }

        protected static void Next(CommandButtonDescriptor button)
        {
            _ = NavigateNext(button);
        }

        private static async Task NavigateNext(CommandButtonDescriptor button)
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

        public bool AreFieldsValid()
            => FormLayout.Properties.Aggregate
            (
                true,
                (isTrue, next) => next.Validate() && isTrue
            );

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
