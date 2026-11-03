using Contoso.XPlatform.Validators;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public partial class LabelValidatableObject<T> : ValidatableObjectBase<T>
    {
        public LabelValidatableObject(IUiNotificationService uiNotificationService, string name, string templateName, string title, string placeholder, string stringFormat, IEnumerable<IValidationRule>? validations)
            : base(name, templateName, validations, uiNotificationService)
        {
            Placeholder = placeholder;
            Title = title;
            this.stringFormat = stringFormat;
        }

        private readonly string stringFormat;

        public string DisplayText
        {
            get
            {
                if (EqualityComparer<T>.Default.Equals(Value, default))
                    return string.Empty;

                if (string.IsNullOrEmpty(this.stringFormat))
                    return Value?.ToString() ?? string.Empty;

                return string.Format(CultureInfo.CurrentCulture, this.stringFormat, Value);
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

        private string _placeholder;
        public string Placeholder
        {
            get => _placeholder;
            [MemberNotNull(nameof(_placeholder))]
            set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }
    }
}
