using LogicBuilder.App.Maui.Forms.Configuration.TextForm;

namespace LogicBuilder.App.Maui.Test.ScreenDescriptors
{
    internal static class TextFormDescriptors
    {
        internal static TextFormSettingsDescriptor HomePage = new
        (
            title: "Home",
            textGroups:
             [
                 new TextGroupDescriptor
                 (
                     title: "Sub Heading",
                     labels:
                     [
                         new LabelItemDescriptor("Description")
                     ]
                 )
             ]
        );
    }
}
