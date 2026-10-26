using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.Validatables;
using System;

namespace Contoso.XPlatform.Views.Factories
{
    internal class PopupFormFactory(
        Func<IValidatable, ChildFormArrayPageCS> getChildFormArrayPage,
        Func<IValidatable, ChildFormPageCS> getChildFormPage,
        Func<IValidatable, MultiSelectPageCS> getMultiSelectPage,
        Func<IReadOnly, ReadOnlyChildFormArrayPageCS> getReadOnlyChildFormArrayPage,
        Func<IReadOnly, ReadOnlyChildFormPageCS> getReadOnlyChildFormPage,
        Func<IReadOnly, ReadOnlyMultiSelectPageCS> getReadOnlyMultiSelectPage) : IPopupFormFactory
    {
        private readonly Func<IValidatable, ChildFormArrayPageCS> getChildFormArrayPage = getChildFormArrayPage;
        private readonly Func<IValidatable, ChildFormPageCS> getChildFormPage = getChildFormPage;
        private readonly Func<IValidatable, MultiSelectPageCS> getMultiSelectPage = getMultiSelectPage;
        private readonly Func<IReadOnly, ReadOnlyChildFormArrayPageCS> getReadOnlyChildFormArrayPage = getReadOnlyChildFormArrayPage;
        private readonly Func<IReadOnly, ReadOnlyChildFormPageCS> getReadOnlyChildFormPage = getReadOnlyChildFormPage;
        private readonly Func<IReadOnly, ReadOnlyMultiSelectPageCS> getReadOnlyMultiSelectPage = getReadOnlyMultiSelectPage;

        public ChildFormArrayPageCS CreateChildFormArrayPage(IValidatable formArrayValidatable) 
            => getChildFormArrayPage(formArrayValidatable);

        public ChildFormPageCS CreateChildFormPage(IValidatable formValidatable)
            => getChildFormPage(formValidatable);

        public MultiSelectPageCS CreateMultiSelectPage(IValidatable multiSelectValidatable)
            => getMultiSelectPage(multiSelectValidatable);

        public ReadOnlyChildFormArrayPageCS CreateReadOnlyChildFormArrayPage(IReadOnly formArrayReadOnly)
            => getReadOnlyChildFormArrayPage(formArrayReadOnly);

        public ReadOnlyChildFormPageCS CreateReadOnlyChildFormPage(IReadOnly formReadOnly)
            => getReadOnlyChildFormPage(formReadOnly);

        public ReadOnlyMultiSelectPageCS CreateReadOnlyMultiSelectPage(IReadOnly multiSelectReadOnly)
            => getReadOnlyMultiSelectPage(multiSelectReadOnly);
    }
}
