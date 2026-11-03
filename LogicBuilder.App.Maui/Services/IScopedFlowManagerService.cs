using LogicBuilder.App.Maui.Business.Requests;
using LogicBuilder.App.Maui.Utils.Interfaces;
using System;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Services
{
    public interface IScopedFlowManagerService : IDisposable
    {
        IFlowManager FlowManager { get; }
        Task Start();
        Task NewFlowStart(NewFlowRequest request);
        Task RunFlow(NewFlowRequest request);
        Task Next(CommandButtonRequest request);
        void CopyFlowItems();
        void CopyPersistentFlowItems();
        void SetFlowDataCacheItem(string key, object value);
        object? GetFlowDataCacheItem(string key);
    }
}
