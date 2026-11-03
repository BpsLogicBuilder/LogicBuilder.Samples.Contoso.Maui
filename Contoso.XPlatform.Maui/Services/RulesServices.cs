using Contoso.XPlatform.Flow;
using LogicBuilder.App.Utils.Rules;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable IDE0130 // Microsoft recommended for service registrations
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130 // Microsoft recommended for service registrations
{
    internal static class RulesServices
    {
        internal static IServiceCollection AddRulesCache(this IServiceCollection services)
        {
            return services.AddRulesCacheService
            (
                new RulesLoaderRequest
                (
                    "Contoso.XPlatform.Flow.Rulesets",
                    typeof(FlowActivity),
                    [
                        typeof(LogicBuilder.App.Utils.Interfaces.ITypeHelper).Assembly,
                        typeof(LogicBuilder.App.Maui.Forms.Parameters.CommandButtonParameters).Assembly,
                        typeof(LogicBuilder.App.Maui.Forms.Configuration.CommandButtonDescriptor).Assembly,
                        typeof(LogicBuilder.Forms.Parameters.ConnectorParameters).Assembly,
                        typeof(Contoso.Domain.Entities.StudentModel).Assembly,
                        typeof(Contoso.Data.Entities.Course).Assembly,
                        typeof(DirectorBase).Assembly,
                        typeof(string).Assembly
                    ]
                )
            );
        }
    }
}
