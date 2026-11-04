using LogicBuilder.App.Maui.Business.Settings.Screen;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Forms.Configuration.ListForm;
using LogicBuilder.App.Maui.Forms.Configuration.SearchForm;
using LogicBuilder.App.Maui.Forms.Configuration.TextForm;
using LogicBuilder.App.Maui.Test.Helpers;
using LogicBuilder.App.Maui.Test.ScreenDescriptors;
using LogicBuilder.App.Maui.ViewModels.DetailForm;
using LogicBuilder.App.Maui.ViewModels.EditForm;
using LogicBuilder.App.Maui.ViewModels.ListPage;
using LogicBuilder.App.Maui.ViewModels.SearchPage;
using LogicBuilder.App.Maui.ViewModels.TextPage;

namespace LogicBuilder.App.Maui.Test
{
    public class DependencyResolverTests
    {
        public DependencyResolverTests()
        {
            serviceProvider = ServiceProviderHelper.GetServiceProvider();
        }

        #region Fields
        private readonly IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void CanResolveEditFormViewModel()
        {
            ScreenSettingsBase settings = new ScreenSettings<DataFormSettingsDescriptor>(Descriptors.DepartmentForm, Descriptors.ButtonDescriptors, ViewType.EditForm);
            Func<ScreenSettingsBase, EditFormViewModelBase> factoryFunc = serviceProvider.GetRequiredService<Func<ScreenSettingsBase, EditFormViewModelBase>>();
            EditFormViewModelBase editFormViewModel = factoryFunc(settings);
            Assert.NotNull(editFormViewModel);
        }

        [Fact]
        public void CanResolveEditFormViewModelWithNonGenericConstructor()
        {
            ScreenSettingsBase settings = new ScreenSettings<DataFormSettingsDescriptor>(Descriptors.DepartmentForm, Descriptors.ButtonDescriptors, ViewType.EditForm);
            Func<ScreenSettingsBase, EditFormViewModelBase> factoryFunc = (Func<ScreenSettingsBase, EditFormViewModelBase>)serviceProvider.GetRequiredService(typeof(Func<ScreenSettingsBase, EditFormViewModelBase>));
            EditFormViewModelBase editFormViewModel = factoryFunc(settings);
            Assert.NotNull(editFormViewModel);
        }

        [Fact]
        public void CanCreateDetailViewMode()
        {
            ScreenSettingsBase settings = new ScreenSettings<DataFormSettingsDescriptor>(ReadOnlyDescriptors.DepartmentForm, Descriptors.ButtonDescriptors, ViewType.DetailForm);
            Func<ScreenSettingsBase, DetailFormViewModelBase> factoryFunc = serviceProvider.GetRequiredService<Func<ScreenSettingsBase, DetailFormViewModelBase>>();
            DetailFormViewModelBase viewModel = factoryFunc(settings);
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void CanCreateListPageViewMode()
        {
            ScreenSettingsBase settings = new ScreenSettings<ListFormSettingsDescriptor>(ListFormDescriptors.AboutForm, Descriptors.ButtonDescriptors, ViewType.ListPage);
            Func<ScreenSettingsBase, ListPageViewModelBase> factoryFunc = serviceProvider.GetRequiredService<Func<ScreenSettingsBase, ListPageViewModelBase>>();
            ListPageViewModelBase viewModel = factoryFunc(settings);
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void CanCreateSearchPageViewMode()
        {
            ScreenSettingsBase settings = new ScreenSettings<SearchFormSettingsDescriptor>(SearchFormDescriptors.SudentsForm, Descriptors.ButtonDescriptors, ViewType.SearchPage);
            Func<ScreenSettingsBase, SearchPageViewModelBase> factoryFunc = serviceProvider.GetRequiredService<Func<ScreenSettingsBase, SearchPageViewModelBase>>();
            SearchPageViewModelBase viewModel = factoryFunc(settings);
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void CanCreateTextPageViewMode()
        {
            ScreenSettingsBase settings = new ScreenSettings<TextFormSettingsDescriptor>(TextFormDescriptors.HomePage, Descriptors.ButtonDescriptors, ViewType.TextPage);
            Func<ScreenSettingsBase, TextPageViewModel> factoryFunc = serviceProvider.GetRequiredService<Func<ScreenSettingsBase, TextPageViewModel>>();
            TextPageViewModel viewModel = factoryFunc(settings);
            Assert.NotNull(viewModel);
        }
    }
}
