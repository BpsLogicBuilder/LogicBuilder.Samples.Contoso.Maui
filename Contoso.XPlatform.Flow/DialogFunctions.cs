using AutoMapper;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Flow.Settings.Screen;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Forms.Configuration.ListForm;
using LogicBuilder.App.Maui.Forms.Configuration.SearchForm;
using LogicBuilder.App.Maui.Forms.Configuration.TextForm;
using LogicBuilder.App.Maui.Forms.Parameters.DataForm;
using LogicBuilder.App.Maui.Forms.Parameters.ListForm;
using LogicBuilder.App.Maui.Forms.Parameters.SearchForm;
using LogicBuilder.App.Maui.Forms.Parameters.TextForm;
using LogicBuilder.Attributes;
using LogicBuilder.Forms.Parameters;
using System.Collections.Generic;

namespace Contoso.XPlatform.Flow
{
    public class DialogFunctions(ScreenData screenData, IMapper mapper) : IDialogFunctions
    {

        #region Fields
        private readonly ScreenData screenData = screenData;
        private readonly IMapper mapper = mapper;
        #endregion Fields

        public void DisplayEditCollection([Comments("Configuration details for the form.")] SearchFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<SearchFormSettingsDescriptor>
            (
                mapper.Map<SearchFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.SearchPage
            );
        }

        public void DisplayEditForm([Comments("Configuration details for the form.")] DataFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<DataFormSettingsDescriptor>
            (
                mapper.Map<DataFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.EditForm
            );
        }

        public void DisplayDetailForm([Comments("Configuration details for the form.")] DataFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<DataFormSettingsDescriptor>
            (
                mapper.Map<DataFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.DetailForm
            );
        }

        public void DisplayReadOnlyCollection([Comments("Configuration details for the form.")] ListFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<ListFormSettingsDescriptor>
            (
                mapper.Map<ListFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.ListPage
            );
        }

        public void DisplayTextForm([Comments("Configuration details for the form.")] TextFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<TextFormSettingsDescriptor>
            (
                mapper.Map<TextFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.TextPage
            );
        }
    }
}
