using LogicBuilder.App.Maui.Business.Requests;
using LogicBuilder.App.Maui.Business.Settings;
using LogicBuilder.App.Maui.Forms.Configuration.Navigation;
using LogicBuilder.App.Maui.Utils;
using LogicBuilder.App.Maui.Utils.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Services
{
    public partial class ScopedFlowManagerService : IScopedFlowManagerService
    {
        private readonly IServiceScope scope;

        public ScopedFlowManagerService(IServiceScopeFactory serviceScopeFactory, IAppLogger appLogger, IUiNotificationService uiNotificationService)
        {
            this.appLogger = appLogger;
            this.uiNotificationService = uiNotificationService;
            scope = serviceScopeFactory.CreateScope();
            FlowManager = scope.ServiceProvider.GetRequiredService<IFlowManager>();
        }

        private readonly IAppLogger appLogger;
        private readonly IUiNotificationService uiNotificationService;

        public IFlowManager FlowManager { get; }

        public async Task Start()
        {
            DateTime dt = DateTime.Now;
            FlowSettings flowSettings = await this.FlowManager.Start("home");
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(ScopedFlowManagerService), $"Start (milliseconds) = {(dt2 - dt).TotalMilliseconds}");

            uiNotificationService.NotifyFlowSettingsChanged(flowSettings);
        }

        public async Task NewFlowStart(NewFlowRequest request)
        {
            DateTime dt = DateTime.Now;
            FlowSettings flowSettings = await this.FlowManager.NewFlowStart(request);
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(ScopedFlowManagerService), $"NavStart (milliseconds) = {(dt2 - dt).TotalMilliseconds}");

            uiNotificationService.NotifyFlowSettingsChanged(flowSettings);
        }

        public async Task RunFlow(NewFlowRequest request)
        {
            DateTime dt = DateTime.Now;
            await this.FlowManager.NewFlowStart(request);
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(ScopedFlowManagerService), $"RunFlow (milliseconds) = {(dt2 - dt).TotalMilliseconds}");
        }

        public async Task Next(CommandButtonRequest request)
        {
            DateTime dt = DateTime.Now;
            FlowSettings flowSettings = await this.FlowManager.Next(request);
            DateTime dt2 = DateTime.Now;

            appLogger.LogMessage(nameof(ScopedFlowManagerService), $"Next (milliseconds) = {(dt2 - dt).TotalMilliseconds}");

            uiNotificationService.NotifyFlowSettingsChanged(flowSettings);
        }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
                scope?.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SetFlowDataCacheItem(string key, object value)
        {
            if (this.FlowManager.FlowDataCache.Items == null)
                return;

            this.FlowManager.FlowDataCache.Items[key] = value;
        }

        public object? GetFlowDataCacheItem(string key)
        {
            if (this.FlowManager.FlowDataCache.Items == null)
                return null;

            return this.FlowManager.FlowDataCache.Items.TryGetValue(key, out object? value) ? value : null;
        }

        public void CopyFlowItems()
        {
            ((Director)this.FlowManager.Director).FlowState = uiNotificationService.FlowSettings!.FlowState;/*FlowSettings not null here*/
            this.FlowManager.FlowDataCache.PersistentKeys = [.. uiNotificationService.FlowSettings.FlowDataCache.PersistentKeys];
            this.FlowManager.FlowDataCache.Items = new Dictionary<string, object>(uiNotificationService.FlowSettings.FlowDataCache.Items);
            this.FlowManager.FlowDataCache.NavigationBar = new NavigationBarDescriptor
            (
                brandText: uiNotificationService.FlowSettings.FlowDataCache.NavigationBar.BrandText,
                currentModule: uiNotificationService.FlowSettings.FlowDataCache.NavigationBar.CurrentModule,
                menuItems: [.. uiNotificationService.FlowSettings.FlowDataCache.NavigationBar.MenuItems]
            );
        }

        public void CopyPersistentFlowItems()
        {
            this.FlowManager.FlowDataCache.PersistentKeys = [.. uiNotificationService.FlowSettings?.FlowDataCache.PersistentKeys ?? []];
            this.FlowManager.FlowDataCache.Items = new Dictionary<string, object>
            (
                uiNotificationService.FlowSettings?.FlowDataCache.Items.Where
                (
                    kvp => uiNotificationService.FlowSettings.FlowDataCache.PersistentKeys.Contains(kvp.Key)
                ) ?? []
            );
        }
    }
}
