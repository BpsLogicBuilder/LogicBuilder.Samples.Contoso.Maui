using LogicBuilder.App.Maui.ViewModels.ReadOnlys;
using LogicBuilder.App.Maui.ViewModels.Validatables;

namespace LogicBuilder.App.Maui.Views.Factories
{
    public interface IPopupFormFactory
    {
        ChildFormArrayPageCS CreateChildFormArrayPage(IValidatable formArrayValidatable);
        ChildFormPageCS CreateChildFormPage(IValidatable formValidatable);
        MultiSelectPageCS CreateMultiSelectPage(IValidatable multiSelectValidatable);
        ReadOnlyChildFormArrayPageCS CreateReadOnlyChildFormArrayPage(IReadOnly formArrayReadOnly);
        ReadOnlyChildFormPageCS CreateReadOnlyChildFormPage(IReadOnly formReadOnly);
        ReadOnlyMultiSelectPageCS CreateReadOnlyMultiSelectPage(IReadOnly multiSelectReadOnly);
    }
}
