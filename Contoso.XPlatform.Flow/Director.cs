using Contoso.XPlatform.Flow.Settings;
using LogicBuilder.RulesDirector;
using System;
using System.Linq;

namespace Contoso.XPlatform.Flow
{
    public class Director(IFlowManager flowManager) : AppDirectorBase
    {

        #region Fields
        private readonly IFlowManager _flowManager = flowManager;
        #endregion Fields

        #region Properties
        protected override Progress Progress => this._flowManager.Progress;
        protected override IRulesCache RulesCache => _flowManager.RulesCache;
        protected override IFlowActivity FlowActivity => this._flowManager.FlowActivity;

        public FlowState FlowState
        {
            get
            {
                return new FlowState
                {
                    Driver = this._driver,
                    Selection = this._selection,
                    CallingModuleDriverStack = [.. this._callingModuleDriverStack.OfType<string>()],
                    CallingModuleStack = [.. this._callingModuleStack.OfType<string>()],
                    ModuleBeginName = this._moduleBeginName,
                    ModuleEndName = this._moduleEndName
                };
            }
            set
            {
                this._driver = value.Driver;
                this._selection = value.Selection;
                this._callingModuleDriverStack = new System.Collections.Stack(value.CallingModuleDriverStack.Reverse<string>().ToList());
                this._callingModuleStack = new System.Collections.Stack(value.CallingModuleStack.Reverse<string>().ToList());
                this._moduleBeginName = value.ModuleBeginName;
                this._moduleEndName = value.ModuleEndName;
            }
        }
        #endregion Properties

        #region Methods
        public override void SetCurrentBusinessBackupData() => this._flowManager.SetCurrentBusinessBackupData();
        #endregion Methods
    }
}
