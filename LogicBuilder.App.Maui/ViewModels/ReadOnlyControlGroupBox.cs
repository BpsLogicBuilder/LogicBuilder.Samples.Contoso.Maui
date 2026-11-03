using LogicBuilder.App.Maui.Utils;
using LogicBuilder.App.Maui.ViewModels.ReadOnlys;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LogicBuilder.App.Maui.ViewModels
{
    public partial class ReadOnlyControlGroupBox(IFormGroupBoxSettings groupBoxSettings) : ObservableCollection<IReadOnly>([])
    {
        public string GroupHeader { get; set; } = groupBoxSettings.GroupHeader;
        public bool IsVisible { get; set; } = !groupBoxSettings.IsHidden;
        public MultiBindingDescriptor? HeaderBindings { get; set; } = groupBoxSettings.HeaderBindings;
        public IFormGroupBoxSettings GroupBoxSettings { get; set; } = groupBoxSettings;
        public Dictionary<string, IReadOnly> BindingPropertiesDictionary
            => this.ToDictionary(p => p.Name.ToBindingDictionaryKey());
    }
}
