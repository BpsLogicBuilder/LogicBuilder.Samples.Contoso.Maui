using AutoMapper;
using LogicBuilder.App.Maui.AutoMapperProfiles;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.Test
{
    public class AutoMapperProfilesTest
    {
        static AutoMapperProfilesTest()
        {
            SetupAutoMapper();
        }

        [Fact]
        public void TestConfigurationIsValid()
        {
            //assert
            config.AssertConfigurationIsValid();
        }

        static MapperConfiguration config;
        [MemberNotNull(nameof(config))]
        private static void SetupAutoMapper()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(ExpressionOperatorsMappingProfile), typeof(CommandButtonProfile));//AddMaps adds all other profiles
            }, new NullLoggerFactory());
        }
    }
}
