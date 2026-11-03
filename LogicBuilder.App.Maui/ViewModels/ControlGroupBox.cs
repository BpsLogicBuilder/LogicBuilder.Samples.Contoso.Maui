using LogicBuilder.App.Maui.Utils;
using LogicBuilder.App.Maui.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LogicBuilder.App.Maui.ViewModels
{
    public partial class ControlGroupBox(IFormGroupBoxSettings groupBoxSettings) : ObservableCollection<IValidatable>([])
    {
        public string GroupHeader { get; set; } = groupBoxSettings.GroupHeader;
        public bool IsVisible { get; set; } = !groupBoxSettings.IsHidden;
        public MultiBindingDescriptor? HeaderBindings { get; set; } = groupBoxSettings.HeaderBindings;
        public IFormGroupBoxSettings GroupBoxSettings { get; set; } = groupBoxSettings;
        public Dictionary<string, IValidatable> BindingPropertiesDictionary
            => this.ToDictionary(p => p.Name.ToBindingDictionaryKey());
    }
}
