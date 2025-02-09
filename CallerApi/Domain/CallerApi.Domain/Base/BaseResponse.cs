using System;

namespace CallerApi.Domain.Base;

public class BaseResponse
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
}
