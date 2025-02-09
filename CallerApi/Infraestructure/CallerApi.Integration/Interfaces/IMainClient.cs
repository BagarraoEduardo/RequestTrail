using System;
using CallerApi.Integration.Generated.CalledApi;

namespace CallerApi.Integration.Interfaces;

public interface IMainClient
{
    Task<(bool Success, string ErrorMessage)> GetExample(bool error);
    Task<(bool Success, string ErrorMessage, BaseResponseDTO Data)> PostExample(BaseRequestDTO request);
}
