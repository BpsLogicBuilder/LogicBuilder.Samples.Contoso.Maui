using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Flow.Settings.Screen;

namespace Contoso.XPlatform.Flow.Settings
{
    public class FlowSettings(FlowState flowState, FlowDataCache flowDataCache, ScreenSettingsBase? screenSettings)
    {
        public FlowState FlowState { get; set; } = flowState;
        public FlowDataCache FlowDataCache { get; set; } = flowDataCache;
        public ScreenSettingsBase? ScreenSettings { get; set; } = screenSettings;
    }
}
