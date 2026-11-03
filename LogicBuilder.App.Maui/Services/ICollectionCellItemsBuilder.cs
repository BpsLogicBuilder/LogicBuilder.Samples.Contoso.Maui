using LogicBuilder.App.Maui.ViewModels.ReadOnlys;
using System.Collections.Generic;

namespace LogicBuilder.App.Maui.Services
{
    public interface ICollectionCellItemsBuilder
    {
        ICollection<IReadOnly> CreateFields();
    }
}
