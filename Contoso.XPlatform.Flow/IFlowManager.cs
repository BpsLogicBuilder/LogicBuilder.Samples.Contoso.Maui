using AutoMapper;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Flow.Settings;
using LogicBuilder.RulesDirector;
using System;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Flow
{
    public interface IFlowManager
    {
        Progress Progress { get; }
        DirectorBase Director { get; }
        FlowDataCache FlowDataCache { get; }
        IFlowActivity FlowActivity { get; }
        IRulesCache RulesCache { get; }
        IServiceProvider ServiceProvider { get; }

        Task<FlowSettings> Start(string module);
        Task<FlowSettings> Next(CommandButtonRequest request);
        Task<FlowSettings> NewFlowStart(NewFlowRequest request);
        void FlowComplete();
        void Terminate();
        void SetCurrentBusinessBackupData();
    }
}
