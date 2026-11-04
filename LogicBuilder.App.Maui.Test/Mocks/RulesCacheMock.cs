using LogicBuilder.RulesDirector;
using LogicBuilder.Workflow.Activities.Rules;

namespace LogicBuilder.App.Maui.Test.Mocks
{
    public class RulesCacheMock : IRulesCache
    {
        public IDictionary<string, RuleEngine> RuleEngines => throw new NotImplementedException();

        public IDictionary<string, string> ResourceStrings => throw new NotImplementedException();

        public RuleEngine GetRuleEngine(string ruleSet)
        {
            throw new NotImplementedException();
        }
    }
}
