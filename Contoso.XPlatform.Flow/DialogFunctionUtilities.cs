using LogicBuilder.App.Maui.Forms.Parameters.DataForm;
using LogicBuilder.App.Maui.Forms.Parameters.ListForm;
using LogicBuilder.App.Maui.Forms.Parameters.SearchForm;
using LogicBuilder.App.Maui.Forms.Parameters.TextForm;
using LogicBuilder.Attributes;
using LogicBuilder.Forms.Parameters;
using System.Collections.Generic;

namespace Contoso.XPlatform.Flow
{
    public static class DialogFunctionUtilities
    {
        [AlsoKnownAs("DisplayEditCollection")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        public static void DisplayEditCollection(IDialogFunctions dialogFunctions, [Comments("Configuration details for the form.")] SearchFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            dialogFunctions.DisplayEditCollection(setting, buttons);
        }

        [AlsoKnownAs("DisplayEditForm")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        public static void DisplayEditForm(IDialogFunctions dialogFunctions, [Comments("Configuration details for the form.")] DataFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            dialogFunctions.DisplayEditForm(setting, buttons);
        }

        [AlsoKnownAs("DisplayDetailForm")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        public static void DisplayDetailForm(IDialogFunctions dialogFunctions, [Comments("Configuration details for the form.")] DataFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            dialogFunctions.DisplayDetailForm(setting, buttons);
        }

        [AlsoKnownAs("DisplayReadOnlyCollection")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        public static void DisplayReadOnlyCollection(IDialogFunctions dialogFunctions, [Comments("Configuration details for the form.")] ListFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            dialogFunctions.DisplayReadOnlyCollection(setting, buttons);
        }

        [AlsoKnownAs("DisplayTextForm")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        public static void DisplayTextForm(IDialogFunctions dialogFunctions, [Comments("Configuration details for the form.")] TextFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            dialogFunctions.DisplayTextForm(setting, buttons);
        }
    }
}
