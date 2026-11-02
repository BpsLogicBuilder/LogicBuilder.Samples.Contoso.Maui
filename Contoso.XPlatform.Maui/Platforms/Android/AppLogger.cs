using Android.Util;
using LogicBuilder.App.Maui.Utils.Interfaces;

namespace Contoso.XPlatform
{
    public class AppLogger : IAppLogger
    {
        public void LogMessage(string group, string message)
        {
            Log.Debug($"X:{group}", message);
        }
    }
}
