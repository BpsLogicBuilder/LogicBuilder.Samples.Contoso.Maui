using AutoMapper;
using Contoso.XPlatform.Constants;
using LogicBuilder.App.Maui.Business.Requests;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using LogicBuilder.Forms.Parameters.Expressions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Services
{
    internal class PagingSelectorBuilder(IMapper mapper, IServiceProvider serviceProvider) : IPagingSelectorBuilder
    {
        private readonly IMapper mapper = mapper;
        private readonly IServiceProvider serviceProvider = serviceProvider;

        public async Task<SelectorLambdaDescriptor> CreateSelector(int skip, string? searchText, string createPagingSelectorFlowName)
        {
            using IScopedFlowManagerService flowManagerService = serviceProvider.GetRequiredService<IScopedFlowManagerService>();
            flowManagerService.SetFlowDataCacheItem
            (
                FlowDataCacheItemKeys.SkipCount,
                skip
            );

            flowManagerService.SetFlowDataCacheItem
            (
                FlowDataCacheItemKeys.SearchText,
                searchText ?? ""
            );

            await flowManagerService.RunFlow
            (
                new NewFlowRequest
                {
                    InitialModuleName = createPagingSelectorFlowName
                }
            );

            return this.mapper.Map<SelectorLambdaDescriptor>
            (
                flowManagerService.GetFlowDataCacheItem(typeof(SelectorLambdaOperatorParameters).FullName!)/*FullName of known type*/
            );
        }
    }
}
