using AutoMapper;
using LogicBuilder.App.Maui.AutoMapperProfiles;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Parameters;
using LogicBuilder.Forms.Parameters;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.Test
{
    public class CommandButtonTests
    {
        static CommandButtonTests()
        {
            SetupAutoMapper();
        }
        #region Fields
        static IMapper mapper;
        #endregion Fields

        [Fact]
        public void Map_ConnectorParameters_To_CommandButtonDescriptor()
        {
            ConnectorParameters parameters = new()
            {
                Id = 1,
                ShortString = "EDT",
                LongString = "Edit",
                ConnectorData = new CommandButtonParameters("SubmitCommand", "Save")
            };

            CommandButtonDescriptor button = mapper.Map<CommandButtonDescriptor>(parameters);
            Assert.Equal(1, button.Id);
            Assert.Equal("EDT", button.ShortString);
            Assert.Equal("Edit", button.LongString);
            Assert.Equal("Save", button.ButtonIcon);
            Assert.Equal("SubmitCommand", button.Command);
        }

        [MemberNotNull(nameof(mapper))]
        private static void SetupAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CommandButtonProfile>();
            }, new NullLoggerFactory());
            config.AssertConfigurationIsValid();
            mapper = config.CreateMapper();
        }
    }
}
