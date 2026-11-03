using Akavache;
using LogicBuilder.App.Maui.Utils;
using LogicBuilder.App.Bsl.Business.Requests;
using LogicBuilder.App.Bsl.Business.Responses;
using LogicBuilder.App.Maui.Services;
using System;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LogicBuilder.App.Maui.Services
{
    public class HttpService(IHttpClientFactory factory, IUrlService urlService) : IHttpService
    {
        private readonly IHttpClientFactory factory = factory;
        private readonly IUrlService urlService = urlService;
        private readonly IBlobCache cache = CacheDatabase.LocalMachine;

        public async Task<BaseResponse> GetObjectDropDown(GetTypedListRequest request, string? url = null)
        {
            string jsonRequest = JsonSerializer.Serialize(request);
            var response = await GetFromCache<GetListResponse>(jsonRequest);

            if (response?.Success == true && response.List != null)
                return response;

            response = await PollyHelpers.ExecutePolicyAsync
            (
                () => this.factory.PostAsync<GetListResponse>
                (
                    url ?? "api/List/GetList",
                    jsonRequest,
                    urlService.GetBaseUrl()
                )
            );

            await AddToCache(jsonRequest, response);

            return response;
        }

        public Task<BaseResponse> GetList(GetTypedListRequest request, string? url = null) 
            => PollyHelpers.ExecutePolicyAsync
            (
                () => this.factory.PostAsync<BaseResponse>
                (
                    url ?? "api/List/GetList",
                    JsonSerializer.Serialize(request),
                    urlService.GetBaseUrl()
                )
            );

        public Task<BaseResponse> GetEntity(GetEntityRequest request, string? url = null)
            => PollyHelpers.ExecutePolicyAsync
            (
                () => this.factory.PostAsync<BaseResponse>
                (
                    url ?? "api/Entity/GetEntity",
                    JsonSerializer.Serialize(request),
                    urlService.GetBaseUrl()
                )
            );

        public Task<BaseResponse> DeleteEntity(DeleteEntityRequest request, string url)
            => PollyHelpers.ExecutePolicyAsync
            (
                () => this.factory.PostAsync<BaseResponse>
                (
                    url,
                    JsonSerializer.Serialize(request),
                    urlService.GetBaseUrl()
                )
            );

        public Task<BaseResponse> SaveEntity(SaveEntityRequest request, string url) 
            => PollyHelpers.ExecutePolicyAsync
            (
                () => this.factory.PostAsync<BaseResponse>
                (
                    url,
                    JsonSerializer.Serialize(request),
                    urlService.GetBaseUrl()
                )
            );

        public async Task AddToCache<T>(string cacheName, T objectToAdd)
        {
            await cache.Insert
            (
                cacheName,
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(objectToAdd)),
                DateTimeOffset.Now.AddDays(1)
            );
        }

        private async Task<T?> GetFromCache<T>(string cacheName)
        {
            try
            {
                return JsonSerializer.Deserialize<T>
                (
                    Encoding.UTF8.GetString(await cache.Get(cacheName)),
                    SerializationOptions.Default
                );
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
