using System;

namespace LogicBuilder.App.Maui.ViewModels
{
    public interface IHasItemsSource
    {
        void Reload(object entity, Type entityType);
    }
}
