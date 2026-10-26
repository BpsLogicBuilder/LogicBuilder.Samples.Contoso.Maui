using LogicBuilder.App.Maui.Forms.Configuration.Navigation;
using System.Collections.Generic;

namespace Contoso.XPlatform.Flow.Cache
{
    public class FlowDataCache
    {
        public List<string> PersistentKeys { get; set; } = [];
        public Dictionary<string, object> Items { get; set; } = [];
        public NavigationBarDescriptor NavigationBar { get; set; } = new NavigationBarDescriptor("", "", []);
    }
}
