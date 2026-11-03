using System.ComponentModel;

namespace LogicBuilder.App.Maui.ViewModels.ReadOnlys
{
    public interface IReadOnly : INotifyPropertyChanged, IFormField
    {
        string TemplateName { get; set; }
    }
}
