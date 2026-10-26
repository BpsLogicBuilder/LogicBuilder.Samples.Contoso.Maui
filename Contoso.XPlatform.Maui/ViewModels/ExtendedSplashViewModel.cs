using Contoso.XPlatform.Flow;
using LogicBuilder.App.Utils.Rules;
using LogicBuilder.App.Utils.Rules.Interfaces;
using LogicBuilder.RulesDirector;
using LogicBuilder.Workflow.Activities.Rules;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Contoso.XPlatform.ViewModels
{
    public partial class ExtendedSplashViewModel(IRulesLoader rulesLoader) : ViewModelBase
    {
        private readonly IRulesLoader rulesLoader = rulesLoader;
        private double _progress;

        public double Progress
        {
            get { return _progress; }
            set
            {
                if (Math.Abs(value - _progress) < double.Epsilon)
                    return;

                _progress = value;
                this.OnPropertyChanged();
            }
        }

        public async Task AddRulesCacheService()
        {
            IRulesCache cache = await LoadRules(rulesLoader);
            App.ServiceCollection.AddSingleton<IRulesCache>(sp =>
            {
                return cache;
            });

        }

        async Task<RulesCache> LoadRules(IRulesLoader rulesLoader)
        {
            RulesCache cache = new(new ConcurrentDictionary<string, RuleEngine>(), new ConcurrentDictionary<string, string>());

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ExtendedSplashViewModel)).Assembly;
            string[] uwpResources = ExtendedSplashViewModel.GetResourceStrings(assembly);

            Dictionary<string, string> rules = uwpResources
                                                .Where(f => f.EndsWith(".module"))
                                                .ToDictionary(f => GetKey(f).ToLowerInvariant());

            Dictionary<string, string> resources = uwpResources
                                                .Where(f => f.EndsWith(".resources"))
                                                .ToDictionary(f => GetKey(f).ToLowerInvariant());


            int count = 0;

            await Task.WhenAll
            (
                rules.Keys.Select
                (
                    key => LoadRules
                    (
                        new RulesModule
                        (
                            key,
                            GetBytes(resources[key], assembly),
                            GetBytes(rules[key], assembly)
                        )
                    )
                )
            );

            return cache;

            Task LoadRules(RulesModule module)
            {
                return Task.Run(() =>
                {
                    rulesLoader.LoadRules
                    (
                        module, 
                        cache, 
                        new RulesLoaderRequest
                        (
                            "Contoso.XPlatform.Rulesets",
                            typeof(FlowActivity),
                            [
                                typeof(LogicBuilder.App.Utils.Interfaces.ITypeHelper).Assembly,
                                typeof(LogicBuilder.App.Maui.Forms.Parameters.CommandButtonParameters).Assembly,
                                typeof(LogicBuilder.App.Maui.Forms.Configuration.CommandButtonDescriptor).Assembly,
                                typeof(LogicBuilder.Forms.Parameters.Expansions.SelectExpandDefinitionParameters).Assembly,
                                typeof(Domain.Entities.StudentModel).Assembly,
                                typeof(Data.Entities.Course).Assembly,
                                typeof(DirectorBase).Assembly,
                                typeof(string).Assembly
                            ]
                        )
                    );
                    count++;
                    Progress = (double)count / rules.Count;
                });
            }

            string GetKey(string fullResourceName)
                => Path.GetExtension(Path.GetFileNameWithoutExtension(fullResourceName))[1..];
        }

        private static string[] GetResourceStrings(Assembly assembly)
         => [.. assembly.GetManifestResourceNames()
                    .Where
                    (
                        res => res.StartsWith
                        (
                            $"Contoso.XPlatform.Rulesets.",
                            StringComparison.InvariantCultureIgnoreCase
                        )
                    )];

        private static byte[] GetBytes(string file, Assembly assembly)
        {
            using Stream platformStream = assembly.GetManifestResourceStream(file)!;
            byte[] byteArray = new byte[platformStream.Length];
            platformStream.ReadExactly(byteArray, 0, byteArray.Length);
            return byteArray;
        }
    }
}
