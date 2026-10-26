using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface ICollectionCellItemsBuilder
    {
        ICollection<IReadOnly> CreateFields();
    }
}
