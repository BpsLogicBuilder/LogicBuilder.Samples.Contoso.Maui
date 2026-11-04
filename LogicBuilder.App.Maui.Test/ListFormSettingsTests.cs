using AutoMapper;
using Contoso.Domain.Entities;
using LogicBuilder.App.Maui.AutoMapperProfiles;
using LogicBuilder.App.Maui.Forms.Configuration.ListForm;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.App.Maui.Forms.Parameters.Bindings;
using LogicBuilder.App.Maui.Forms.Parameters.ListForm;
using LogicBuilder.EntityFrameworkCore.Mapping;
using LogicBuilder.Forms.Parameters.Expressions;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LogicBuilder.App.Maui.Test
{
    public class ListFormSettingsTests
    {
        public ListFormSettingsTests()
        {
            SetupAutoMapper();
        }

        #region Fields
        IMapper mapper;
        #endregion Fields

        [Fact]
        public void Map_ConnectorParameters_To_CommandButtonDescriptor()
        {
            ListFormSettingsParameters parameters = new            
            (
                "About",
                typeof(LookUpsModel),
                "Loading ...",
                "TextDetailTemplate",
                [
                    new TextItemBindingParameters
                    (
                        "Text",
                        "DateTimeValue",
                        "DateTimeValue",
                        "Enrollment Date: {0:MM/dd/yyyy}",
                        new TextFieldTemplateParameters("TextTemplate")
                    )
                ],
                new SelectorLambdaOperatorParameters(new ConstantOperatorParameters(null), typeof(object), "e"),
                new RequestDetailsParameters(typeof(object), typeof(object), typeof(object), typeof(object), "")
            );
            ListFormSettingsDescriptor settings = mapper.Map<ListFormSettingsDescriptor>(parameters);
            Assert.Equal("TextDetailTemplate", settings.ItemTemplateName);
        }

        [MemberNotNull(nameof(mapper))]
        private void SetupAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(ExpressionOperatorsMappingProfile), typeof(CommandButtonProfile));
            }, new NullLoggerFactory());
            config.AssertConfigurationIsValid();
            mapper = config.CreateMapper();
        }
    }
}
