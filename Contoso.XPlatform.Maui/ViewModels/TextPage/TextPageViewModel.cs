using Contoso.XPlatform.Services;
using LogicBuilder.App.Maui.Business.Requests;
using LogicBuilder.App.Maui.Business.Settings.Screen;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.TextForm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contoso.XPlatform.ViewModels.TextPage
{
    public partial class TextPageViewModel : ViewModelBase, IDisposable
    {
        public TextPageViewModel(ScreenSettings<TextFormSettingsDescriptor> screenSettings)
        {
            FormSettings = screenSettings.Settings;
            Buttons = new ObservableCollection<CommandButtonDescriptor>(screenSettings.CommandButtons ?? []);
            Title = this.FormSettings.Title;
        }

        public TextFormSettingsDescriptor FormSettings { get; set; }
        public ObservableCollection<CommandButtonDescriptor> Buttons { get; set; }

        private string _title;
        public string Title
        {
            get => _title;
            [MemberNotNull(nameof(_title))]
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

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

        private static void Next(CommandButtonDescriptor button)
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
    }
}
