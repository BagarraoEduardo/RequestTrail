using System;
using CallerApi.Domain;
using CallerApi.Domain.Base;
using CallerApi.Integration.Interfaces;

namespace CallerApi.Business.Interfaces;

public interface IRequestService
{
    public Task<BaseResponse> GetExample(bool error);
    public Task<PostExampleResponse> PostExample(PostExampleRequest request);
}
