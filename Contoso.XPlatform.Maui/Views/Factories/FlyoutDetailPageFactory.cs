using Contoso.XPlatform.Flow.Settings.Screen;
using Microsoft.Maui.Controls;
using System;

namespace Contoso.XPlatform.Views.Factories
{
    public class FlyoutDetailPageFactory(
        IDetailFormFactory detailFormFactory,
        IEditFormFactory editFormFactory,
        IListPageFactory listPageFactory,
        ISearchPageFactory searchPageFactory,
        ITextPageFactory textPageFactory) : IFlyoutDetailPageFactory
    {
        private readonly IDetailFormFactory _detailFormFactory = detailFormFactory;
        private readonly IEditFormFactory _editFormFactory = editFormFactory;
        private readonly IListPageFactory _listPageFactory = listPageFactory;
        private readonly ISearchPageFactory _searchPageFactory = searchPageFactory;
        private readonly ITextPageFactory _textPageFactory = textPageFactory;

        public Page CreatePage(ScreenSettingsBase screenSettings)
        {
            if (!Enum.IsDefined(screenSettings.ViewType))
                throw new ArgumentException($"{nameof(screenSettings.ViewType)}: {{CD36EEFB-E368-4864-8445-42479915D72D}}");

            return screenSettings.ViewType switch
            {
                ViewType.DetailForm => _detailFormFactory.CreatePage(screenSettings),
                ViewType.EditForm => _editFormFactory.CreatePage(screenSettings),
                ViewType.ListPage => _listPageFactory.CreatePage(screenSettings),
                ViewType.SearchPage => _searchPageFactory.CreatePage(screenSettings),
                ViewType.TextPage => _textPageFactory.CreatePage(screenSettings),
                _ => throw new ArgumentException($"{nameof(screenSettings.ViewType)}: {{AD813669-B274-438F-85A3-7FC04A734C4A}}"),
            };
        }
    }
}
