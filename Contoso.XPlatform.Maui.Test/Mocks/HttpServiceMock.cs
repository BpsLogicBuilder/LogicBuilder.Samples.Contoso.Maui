using Contoso.Domain.Entities;
using LogicBuilder.App.Bsl.Business.Requests;
using LogicBuilder.App.Bsl.Business.Responses;
using LogicBuilder.App.Maui.Services;

namespace Contoso.XPlatform.Maui.Test.Mocks
{
    public class HttpServiceMock : IHttpService
    {
        public Task<BaseResponse> GetObjectDropDown(GetTypedListRequest request, string? url = null)
        {
            return Task.FromResult<BaseResponse>(new GetListResponse { Success = true, List = new List<EntityModelBase> { } });
        }

        public Task<BaseResponse> GetList(GetTypedListRequest request, string? url = null)
        {
            return Task.FromResult<BaseResponse>(new GetListResponse { Success = true, List = new List<EntityModelBase> { } });
        }

        public Task<BaseResponse> GetEntity(GetEntityRequest request, string? url = null)
        {
            return Task.FromResult<BaseResponse>(new GetEntityResponse { Success = true });
        }

        public Task<BaseResponse> SaveEntity(SaveEntityRequest request, string url)
        {
            return Task.FromResult<BaseResponse>(new SaveEntityResponse { Success = true });
        }

        public Task<BaseResponse> DeleteEntity(DeleteEntityRequest request, string url)
        {
            return Task.FromResult<BaseResponse>(new DeleteEntityResponse { Success = true });
        }
    }
}
