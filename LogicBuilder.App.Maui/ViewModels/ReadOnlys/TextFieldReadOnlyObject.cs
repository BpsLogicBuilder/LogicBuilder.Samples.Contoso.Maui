using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace LogicBuilder.App.Maui.ViewModels.ReadOnlys
{
    public partial class TextFieldReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public TextFieldReadOnlyObject(
            IUiNotificationService uiNotificationService,
            string name,
            string templateName,
            string title,
            string stringFormat) : base(name, templateName, uiNotificationService)
        {
            this.Title = title;
            this._stringFormat = stringFormat;
        }

        private readonly string _stringFormat;

        public string DisplayText
        {
            get
            {
                if (EqualityComparer<T>.Default.Equals(Value, default))
                    return string.Empty;

                if (string.IsNullOrEmpty(this._stringFormat))
                    return Value?.ToString() ?? string.Empty;

                return string.Format(CultureInfo.CurrentCulture, this._stringFormat, Value);
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            [MemberNotNull(nameof(_title))]
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged();
            }
        }

#pragma warning disable S4275 //using base.Value
        public override T? Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                OnPropertyChanged(nameof(DisplayText));
            }
        }
#pragma warning restore S4275 //using base.Value
    }
}
